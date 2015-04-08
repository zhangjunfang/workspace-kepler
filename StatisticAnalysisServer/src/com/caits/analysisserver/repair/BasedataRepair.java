package com.caits.analysisserver.repair;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.TreeMap;
import java.util.Vector;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import oracle.jdbc.OracleConnection;





import com.caits.analysisserver.bean.OilMonitorBean2;
import com.caits.analysisserver.bean.OilmassChangedDetail;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.LoadFile;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.OracleDBAdapter;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.services.AlarmAnalyserService;
import com.caits.analysisserver.services.DriverAnalyserService;
import com.caits.analysisserver.services.IllegalOperationAnalyserService;
import com.caits.analysisserver.services.SpeedAnomalousAnalyserService;
import com.caits.analysisserver.services.StopstartAnalyserService;
import com.caits.analysisserver.services.VechicleExcStatisticService;
import com.caits.analysisserver.services.VehicleStatusAnalyserService;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.FileUtils;
import com.caits.analysisserver.utils.MathUtils;
import com.caits.analysisserver.utils.Utils;

/**
 * 基础数据重跑任务
 * @author yujch
 *
 */
@SuppressWarnings("unused")
public class BasedataRepair extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(BasedataRepair.class);
	
	private final String keyWord = "track";
	

	private VehicleStatus vehicleStatus = null;
	
	private String lastLocInfos[]=null; 
	
	// 油箱油量监控
	private boolean oilFlag = false; // 
	private File oilFile = null;
	private boolean isHasOil = false;
	
	private OilMonitorBean2 oilMonitorBean2 = null;
	
	private AlarmAnalyserService alarmAnalyserService=null;
	
	private SpeedAnomalousAnalyserService speedAnomalousAnalyserService = null;
	
	private VehicleStatusAnalyserService vehicleStatusAnalyserService = null;
	
	private VechicleExcStatisticService vechicleExcStatisticService = null;
	
	private StopstartAnalyserService stopstartAnalyserService = null;
	
	private IllegalOperationAnalyserService illegalOperationAnalyserService = null;
	
	private DriverAnalyserService driverAnalyserService = null;
	
	private List<String> driverClockinList = new ArrayList<String>();
	
	private long utc = 0;
	
	// 默认获取指定设置日期
	private boolean isUse = true;
	
	private boolean isrepairAlarm = false;//1、是否恢复告警明细数据
	
	private boolean isrepairState = false;//2、是否恢复状态事件数据
	
	private boolean isrepairStopstart = false;//3、是否恢复起步停车数据
	
	private boolean isrepairOpendoorDetail = false;//4、是否恢复开门明细数据
	
	private boolean isrepairServiceDaystat = false;//5、是否恢复日统计数据
	
	private boolean isrepairSigelVehicleAnalyzer = false;//6、是否恢复中间表数据
	
	private boolean isrepairSpeedAnomalous = false;//7、是否恢复速度异常记录数据
	
	private boolean isillegalOperationAnalyzer = false;//8、是否分析夜间非法运营数据
	
	private boolean isDriverDetailAnalyzer = false;//9、是否分析驾驶明细数据
	
	private String fileNameStr = "";//欲恢复数据的文件列表，多个文件用逗号隔开
	
	private String repairDataType;
	
	public BasedataRepair(){

	}
	
	/******
	 * 统计指定时间数据
	 */
	@Override
	public void run() {
		long startTime = System.currentTimeMillis();
		AnalysisDBAdapter dba = new AnalysisDBAdapter();
		Vector<File> fileList = null;
		try {
			if(fileNameStr!=null&&!"".equals(fileNameStr)){
				String[] fileNames = fileNameStr.split(",");
				fileList = LoadFile.loadAssignFile(FilePool.getinstance().getFile(this.utc,"trackfileurl"), this.utc,fileNames);
			}else{
				fileList = LoadFile.findFile(FilePool.getinstance().getFile(this.utc,"trackfileurl"), this.utc);
			}
			dba.initDBAdapter();
			dba.querySoftAlarmDetail(utc);
			
			// 创建一个可重用固定线程数的线程池
			ExecutorService pool = Executors.newFixedThreadPool(5);
			
			// 日统计文件 单项数据恢复用单线程做
			logger.info("BaseData Repair job open!");
			for (int idx = 0; idx < fileList.size(); idx++) {
				File file = fileList.get(idx);
				if (file.exists()){
					//启动线程池，处理数据
					BasedataRepairThread t1 = new BasedataRepairThread(utc,file);
					t1.setRepairDataType(this.repairDataType);
					// 将线程放入池中进行执行
					pool.execute(t1);
				}else{
					logger.info(CDate.utc2Str(this.utc, "yyyyMMdd")+" VID："+file.getName()+" 轨迹记录文件不存在");
				}
			}// End for
			
			// 关闭线程池
			pool.shutdown();
			logger.debug("i will closeing");
			//清空缓存对象
			while(!pool.awaitTermination(1, TimeUnit.SECONDS)){
				//线程池没有关闭时一直等待
			}
			logger.debug("i closed");
			
		} catch (InterruptedException e) {
			logger.error("统计线程休眠出错。", e);
		} catch (Exception e) {
			logger.error("统计主线程出错。",e);
		}finally{
			//清理缓存
			AnalysisDBAdapter.clearCollections();
			if(fileList != null && fileList.size() > 0){
				fileList.clear();
			}
			//driverClockinList.clear();
		}
		
		long endTime = System.currentTimeMillis();
		long costTime = (endTime - startTime);
		Calendar cl = Calendar.getInstance();
		cl.setTimeInMillis(this.utc);
	
 		logger.info("BaseData Repair End " + cl.get(Calendar.YEAR) + "-" + (cl.get(Calendar.MONTH) +1) + "-" + cl.get(Calendar.DAY_OF_MONTH) + " use time:" + (costTime) / 1000 + "s");
	}
	
	/******
	 * 统计指定时间数据
	 */
	public void run2() {
		long startTime = System.currentTimeMillis();
		AnalysisDBAdapter dba = new AnalysisDBAdapter();
		Vector<File> fileList = null;
		try {
			if(fileNameStr!=null&&!"".equals(fileNameStr)){
				String[] fileNames = fileNameStr.split(",");
				fileList = LoadFile.loadAssignFile(FilePool.getinstance().getFile(this.utc,"trackfileurl"), this.utc,fileNames);
			}else{
				fileList = LoadFile.findFile(FilePool.getinstance().getFile(this.utc,"trackfileurl"), this.utc);
			}
			dba.initDBAdapter();
			
			// 日统计文件 单项数据恢复用单线程做
			logger.info("BaseData Repair job open!");
			for (int idx = 0; idx < fileList.size(); idx++) {
				File file = fileList.get(idx);
				statisticStatus(file);
			}// End for
			
		} catch (InterruptedException e) {
			logger.error("统计线程休眠出错。", e);
		} catch (Exception e) {
			logger.error("统计主线程出错。",e);
		}finally{
			if(fileList != null && fileList.size() > 0){
				fileList.clear();
			}
		}
		
		long endTime = System.currentTimeMillis();
		long costTime = (endTime - startTime);
		Calendar cl = Calendar.getInstance();
		cl.setTimeInMillis(this.utc);
	
 		logger.info("BaseData Repair End " + cl.get(Calendar.YEAR) + "-" + (cl.get(Calendar.MONTH) +1) + "-" + cl.get(Calendar.DAY_OF_MONTH) + " use time:" + (costTime) / 1000 + "s");
	}
	
	private void statisticStatus(File file) throws Exception{
		OracleConnection dbCon = null;
		try{
		
		String vid =  file.getName().replaceAll("\\.txt", "");
		
		if(AnalysisDBAdapter.queryVechileInfo(vid) == null){ // 找不到该车对应的企业相关
			return;
		}
		long start = System.currentTimeMillis();
		
		//初始获取一个数据库连接，防止保存方法获取链接时出现死锁
		dbCon = (OracleConnection) OracleConnectionPool.getConnection();
		
		/****************************************************************************************************/
		//告警分析服务初始化
		String eventfilePath = FileUtils.replaceRoot(file.getPath(), FilePool.getinstance().getFile(this.utc,"eventfileurl"),keyWord); 
		alarmAnalyserService = new AlarmAnalyserService(dbCon,eventfilePath,vid,utc,driverClockinList);
		
		//值区间分布分析服务初始化
		vechicleExcStatisticService = new VechicleExcStatisticService(utc,vid);
		
		//日统计分析服务初始化
		vehicleStatusAnalyserService = new VehicleStatusAnalyserService(utc,vid);
		
		//起步停车分析服务初始化
		stopstartAnalyserService = new StopstartAnalyserService(vid,utc);
		
		//速度异常分析初始化
		speedAnomalousAnalyserService = new SpeedAnomalousAnalyserService(utc,""+vid);
		
		//非法营运告警分析初始化
		illegalOperationAnalyserService = new IllegalOperationAnalyserService(dbCon,vid,utc);
		
		//驾驶明细分析服务初始化
		driverAnalyserService = new DriverAnalyserService(vid,utc);
		
		/****************************************************************************************************/
		//读取文件，执行分析过程
		readTrackFile(vid,file);
		logger.info("VID = " + vid +  "; File Analyzer: " + (System.currentTimeMillis() - start)/1000+"s");
		
		/****************************************************************************************************/
		//存储驾驶行为事件
		if (isrepairState){
			long start0 = System.currentTimeMillis();
			OracleDBAdapter.saveStateEventInfo(dbCon,vid,alarmAnalyserService.getStateEventList());
			logger.info("VID = " + vid +  "; save state event: " + (System.currentTimeMillis() - start0)/1000+"s");
		}
		//存储告警服务分析结果
		if (isrepairAlarm){
			long start1 = System.currentTimeMillis();
			OracleDBAdapter.saveVehicleAlarmEvent(dbCon,vid,alarmAnalyserService.getAlarmList());
			logger.info("VID = " + vid +  "; save alarm event : " + (System.currentTimeMillis() - start1)/1000+"s");
		}
		
		if (isrepairStopstart){
			//起步停车填充告警状态时间统计结果
			long startx = System.currentTimeMillis();
			stopstartAnalyserService.fillStopstartList(alarmAnalyserService.getStateEventList());
			//logger.info("VID = " + vid +  "; fill stopstart : " + (System.currentTimeMillis() - startx)+"ms");
			
			long start4 = System.currentTimeMillis();
			OracleDBAdapter.saveStopstartInfo(dbCon,vid,stopstartAnalyserService.getStopstartlist());//存储起步停车信息
			logger.info("VID = " + vid +  "; save stopstart : " + (System.currentTimeMillis() - start4)/1000+"s");
		}
		
		if (this.isrepairServiceDaystat){
			long start5 = System.currentTimeMillis();
			//合并告警分析和日统计分析生成的数据
			VehicleStatus vs = vehicleStatusAnalyserService.getVehicleStatus();
			vs.setStateCountMap(alarmAnalyserService.getStateCountMap());
			VehicleStatus vs0 = alarmAnalyserService.getVehicleStatus();
			vs.setDoor1(vs0.getDoor1());
			vs.setDoor2(vs0.getDoor2());
			vs.setDoor3(vs0.getDoor3());
			vs.setDoor4(vs0.getDoor4());
			vs.setAreaOpenDoorNum(vs0.getAreaOpenDoorNum());
			vs.setAreaOpenDoorTime(vs0.getAreaOpenDoorTime());
			
			OracleDBAdapter.saveStaDayInfo(dbCon,vid,vs); //存储车辆日统计信息
			logger.info("VID = " + vid +  "; save vehicleservice daystat : " + (System.currentTimeMillis() - start5)/1000+"s");
		}
		
		if (this.isrepairSigelVehicleAnalyzer){
			// 存储机油压力
			long start6 = System.currentTimeMillis();
			OracleDBAdapter.saveOilPressureDay(dbCon,vid,vechicleExcStatisticService.getOilPressureBean());
			logger.info("VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start6)/1000+"s");
			
	
			// 存储冷却液温度
			long start7 = System.currentTimeMillis();
			OracleDBAdapter.saveCoolLiquidtemDayStat(dbCon,vid,vechicleExcStatisticService.getCoolLiquidtemBean());
			logger.info("VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start7)/1000+"s");
			
			// 存储进气压力
			long start8 = System.currentTimeMillis();
			OracleDBAdapter.saveGasPressure(dbCon,vid,vechicleExcStatisticService.getGsPressureBean());
			logger.info("VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start8)/1000+"s");
			
			// -----存储车速分析
			// 统计车速时间
			long start9 = System.currentTimeMillis();
			OracleDBAdapter.saveSpeeddistDay(dbCon,vid,vechicleExcStatisticService.getSpeeddistDay());
			logger.info(",VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start9)/1000+"s");
			
			// -----存储转速分析
			// 统计转速时间
			long start10 = System.currentTimeMillis();
			OracleDBAdapter.saveRotatedistDay(dbCon,vid,vechicleExcStatisticService.getRotateSpeedDay());
			logger.info(",VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start10)/1000+"s");
			
			// 统计蓄电压时间
			long start11 = System.currentTimeMillis();
			OracleDBAdapter.saveVoltageDayStat(dbCon,vid,vechicleExcStatisticService.getVoltagedistDay());
			logger.info(" VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start11)/1000+"s");
			
			// 存储进气温度
			long start12 = System.currentTimeMillis();
			OracleDBAdapter.saveAirTemperture(dbCon,vid,vechicleExcStatisticService.getAirTempertureBean());
			logger.info(",VID = " + vid +  "; save OilPressure Analyzer  : " + (System.currentTimeMillis() - start12)/1000+"s");
		}
		//long start13 = System.currentTimeMillis();
		//OracleDBAdapter.saveOutLineEventInfo(vid,alarmAnalyserService.getvAlarmEventList()); // 存储营运事件表违规  （路段行驶时间不足或过长告警、开门状态）
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存违规运营 : " + (System.currentTimeMillis() - start13)/1000+"s");
		
		//long start14 = System.currentTimeMillis();
		//OracleDBAdapter.saveVehicleIsOverLoad(vid,utc); // 单独汇总统计超员
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 汇总超员 : " + (System.currentTimeMillis() - start14)/1000+"s");
		
		//long start15 = System.currentTimeMillis();
		//OracleDBAdapter.saveOutLineInfo(vid,utc); // 存储非法营运日统计信息
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 非法营运 : " + (System.currentTimeMillis() - start15)/1000+"s");
		
		if (this.isrepairOpendoorDetail){
			long start16 = System.currentTimeMillis();
			OracleDBAdapter.saveOpenningDoorDetail(dbCon,vid,alarmAnalyserService.getOpeningDoorList()); //存储开门信息
			logger.info(",VID = " + vid +  "; save OilPressure Analyzer : " + (System.currentTimeMillis() - start16)/1000+"s");
		}
		
		//long start17 = System.currentTimeMillis();
		//OracleDBAdapter.saveOpenningDoorDay(vid,utc); //存储车辆开门运营违规日统计
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 运营违规日统计 : " + (System.currentTimeMillis() - start17)/1000+"s");
		
		/*long start18 = System.currentTimeMillis();
		if(oilFlag && isHasOil){
			
			// 存储文件油量变化记录
			OracleDBAdapter.writeOilChangedMassFile(vid,oilFile,oilMonitorBean2);
			// 存储数据库油量变化.
			//saveOilChangedMass();
			
			if(oilFile.exists()){ // CHECK是否生成油量文件
				OilService oilService = new OilService(utc,vid);
				oilService.analysisOilRecords(oilFile);
			}
		}
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 油量变化 : " + (System.currentTimeMillis() - start18)/1000+"s");
		
		*/
		if (this.isrepairSpeedAnomalous){
			//存储速度异常记录
			long start19 = System.currentTimeMillis();
			OracleDBAdapter.saveSpeedAnomalous(speedAnomalousAnalyserService.getThVehicleSpeedAnomalous());
			logger.info("VID = " + vid +  "; 速度异常记录 : " + (System.currentTimeMillis() - start19)/1000+"s");
		}
		
		if (this.isillegalOperationAnalyzer){
			//夜间非法运营告警数据保存  告警处理和告警明细表一块保存
			long start19 = System.currentTimeMillis();
			OracleDBAdapter.saveVehicleAlarm(dbCon,vid,illegalOperationAnalyserService.getAlarmList());
			OracleDBAdapter.saveVehicleAlarmEvent(dbCon,vid,illegalOperationAnalyserService.getAlarmList());
			logger.info("VID = " + vid +  "; 夜间非法运营告警记录 : " + (System.currentTimeMillis() - start19)/1000+"s");
		}
		
		}catch(Exception ex){
			logger.error("VID = " + file.getAbsolutePath() +  ";文件分析过程中出错!",ex);
		}finally{
			if (dbCon!=null){
				dbCon.close();
			}
		}
	}

	/**
	 * 读取轨迹文件
	 * @param file
	 * @throws SQLException 
	 * @throws IOException 
	 */
	private void readTrackFile(String vid,File file) throws Exception{

		BufferedReader buf = null;
		String number = "";
		

		TreeMap<Long, String> statusMap = new TreeMap<Long, String>();
		int rowCount = 0;
		int currentRow =0;

		try{
			buf = new BufferedReader(new FileReader(file));
			statusMap = getTrackMap(buf);
			rowCount = statusMap.size();
			Set<Long> key = statusMap.keySet();
			Long keys = null;		
			String[] cols = null;

			for (Iterator<Long> it = key.iterator(); it.hasNext();) {
				keys = (Long) it.next();
				try{
						String trackStr = statusMap.get(keys);
						cols = trackStr.split(":");
						
						boolean isLastRow = false;
						currentRow++;
						if (rowCount==currentRow){
							isLastRow = true;
						}
						
						//分析车辆告警数据
						VehicleMessageBean trackBean = this.changTxtVMB(cols);
						trackBean.setTrackStr(trackStr);
						
						
						if (this.isrepairAlarm||this.isrepairOpendoorDetail||this.isrepairServiceDaystat||this.isrepairState||this.isrepairStopstart){
							alarmAnalyserService.executeAnalyser(trackBean,isLastRow);
						}
						
						if (this.isrepairSpeedAnomalous){
						//分析速度异常记录
						speedAnomalousAnalyserService.executeAnalyser(trackBean);
						}
						
						if (this.isrepairStopstart){
						//分析轨迹文件生成起步停车数据和行车数据
						stopstartAnalyserService.executeAnalyser(trackBean,isLastRow);
						}
						
						if (this.isrepairSigelVehicleAnalyzer){
						// 分析车速、转速等值区间分布
						vechicleExcStatisticService.executeAnalyser(trackBean, isLastRow);
						}

						if (this.isrepairServiceDaystat){
						//车辆日统计数据分析
						vehicleStatusAnalyserService.executeAnalyser(trackBean,isLastRow);
						}
						
						if (this.isillegalOperationAnalyzer){
						//车辆夜间非法营运分析
						illegalOperationAnalyserService.executeAnalyser(trackBean, isLastRow);
						}
						
						if (this.isDriverDetailAnalyzer){
						//驾驶明细分析
						driverAnalyserService.executeAnalyser(trackBean, isLastRow);
						}
						
						//缓存本次位置信息
						lastLocInfos = cols;
				
				}catch(Exception ex){
					logger.error(",file Analyzer Failure:" + file.getAbsolutePath() ,ex);
				}
			}// End for	
	
		}catch(Exception ex){
			logger.error("vid="+vid+" 分析轨迹文件过程中出错！"+ex.getMessage(),ex);
		}finally{
			if(statusMap != null && statusMap.size() > 0){
				statusMap.clear();
			}
			if(buf != null){
				buf.close();
			}
		}
	}
	
	/**
	 * 根据gps时间将读取的轨迹文件数据进行排序
	 */
	private TreeMap<Long, String> getTrackMap(BufferedReader buf) {
		
		TreeMap<Long, String> returnTrackMap = new TreeMap<Long, String>();
		String readLine = null;
		String gpsdate = null;
		String[] track = null;
		Long gpstime = null;
		try {
			
			while ((readLine = buf.readLine()) != null) {
				
				// 轨迹文件每行的数据分割
				track = readLine.split(":");
				
				if(track.length >1){
					
					gpsdate = track[2];
					// 将gpsdate转换成utc格式
					gpstime = CDate.stringConvertUtc(gpsdate);
					int oilBox = Integer.parseInt(Utils.checkEmpty(track[25]) ? "0":track[25]);
					if(oilBox > 0 ){
						isHasOil = true;
					}
					returnTrackMap.put(gpstime, readLine);	
					
				}		
			}// End while		
		
		} catch (Exception e) {
			logger.error("读取轨迹文件信息出错",e);
		}finally{
			
			if(buf != null){
				try {
					buf.close();
				} catch (IOException e) {
					logger.error(e.getMessage(), e);
				}
			}
		}

		return returnTrackMap;
		
	}
	
	//启动时间  发动机转速>100 车速<5
	//起步时间  发动机转速>100 车速>5
	//停止时间  发动机转速>100 车速<5
	//熄火时间  发动机转速<100 车速<5
	private Object formatValueByType(String str,String defaultVal,char type){
		Object obj = null;
		switch (type)	{
			case 'S': obj=((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'L': obj=Long.parseLong((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'I': obj=Integer.parseInt((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
		}
		return obj;
	}


	/****
	 * 设置统计时间
	 */
	public void setTime(long utc) {
		this.utc = utc;
		
	}

	/**
	 * 分析油量数据，产生油量监控记录
	 * @param cols
	 */
	public void analyseOilmassData(String[] cols){
		String spdFrom = cols[24];	//车速来源
		int speed = Utils.getSpeed(spdFrom,cols);
		int oilBox = Integer.parseInt(Utils.checkEmpty(cols[25]) ? "0":cols[25]);
		String charGpsTime = cols[2];
		long gpsTime = CDate.stringConvertUtc(charGpsTime);
		Long lon =  Long.parseLong(cols[7]==null||"".equals(cols[7])?"0":cols[7]);
		Long lat = Long.parseLong(cols[8]==null||"".equals(cols[8])?"0":cols[8]);
		Long mapLon =  Long.parseLong(cols[0]==null||"".equals(cols[0])?"0":cols[0]);
		Long mapLat = Long.parseLong(cols[1]==null||"".equals(cols[1])?"0":cols[1]);
		
		Long rpm = -1L;
		if(!Utils.checkEmpty(cols[13])){ 
			rpm = Long.parseLong(cols[13]==null||"".equals(cols[13])?"0":cols[13]);
		}
		
		String binaryStus = "";
		if(!Utils.checkEmpty(cols[14])){
			binaryStus = Long.toBinaryString(Long.parseLong(cols[14]));
		}
		
		if(oilMonitorBean2 == null){
			oilMonitorBean2 = new OilMonitorBean2();
		}
		
		String currStatus = "";
		//判断当前是否行车 车速大于5 点火状态为1 转速大于800
		if (speed>5*10&&binaryStus.endsWith("1")&&checkRpm(rpm)){
			//行车状态
			currStatus = "R";
		}else{
			//停车状态
			currStatus = "S";
		}
		
		//拼装油量监控点对象
		OilmassChangedDetail oil = new OilmassChangedDetail();
		oil.setDirection(Integer.parseInt(Utils.checkEmpty(cols[4]) ? "0":cols[4]));
		oil.setElevation(Integer.parseInt(Utils.checkEmpty(cols[9]) ? "0":cols[9]));
		oil.setGps_speed(speed);
		oil.setUtc(gpsTime);
		oil.setLat(lat);
		oil.setLon(lon);
		oil.setMapLat(mapLat);
		oil.setMapLon(mapLon);
		oil.setChangeType("00");
		oil.setCurr_oilmass(oilBox);
		oil.setGpsTime(charGpsTime);
		oil.setStatus(currStatus);
		
		//金旅建议：当前油量为20L以下的点过滤掉
		//4月17日文件：过滤掉0值、负值、小于10升的值、大于1200升的值、FF值
		if (/*"S".equals(currStatus)||("R".equals(currStatus)&&*/oilBox>=100 && oilBox <= 12000 /*&& oilBox != 32760*/){
			//oilMonitorBean2.addTrackPoint2(currStatus,oil);
			//需求调整：要求只把结果油耗数据展示即可，不进行算法过滤，直接展示原始点
			oilMonitorBean2.addTrackPoint3(currStatus,oil);
		}
	}
	
	/**
	 * 判断车辆转速是否处于行车状态
	 * @param rpm
	 * @return
	 */
	public boolean checkRpm(Long rpm){
		if (rpm < 0){
			return true;
		}else if (rpm * 0.125 > 800 ){
			return true;
		}else{
			return false;
		}
	}
	
	private VehicleMessageBean changTxtVMB(String[] track){
		
		VehicleMessageBean po = new VehicleMessageBean();
		
		po.setCommanddr("");
		po.setMaplon((Long)formatValueByType(track[0],"0",'L'));
		po.setMaplat((Long)formatValueByType(track[1],"0",'L'));
		po.setDateString(track[2]);
		po.setUtc(CDate.stringConvertUtc(track[2]));
		String speedForm = (String)formatValueByType(track[24],"1",'S');
		Long spd = 0L;
		Long gpsSpeed = (Long)formatValueByType(track[3],"0",'L');
		po.setGpsSpeed(gpsSpeed);
		Long vssSpeed = (Long)formatValueByType(track[19],"0",'L');
		po.setVssSpeed(vssSpeed);
		
		if ("0".equals(speedForm)){
			if(vssSpeed<0){
				po.setSpeed(0L);
			}else{
				po.setSpeed(vssSpeed);
				}
		}else{
			if(gpsSpeed<0){
				po.setSpeed(0L); // 1：来自GPS
			}else{
				po.setSpeed(gpsSpeed);
			}
		}
		po.setSpeedFrom(speedForm);
		po.setDir((Integer)formatValueByType(track[4],"0",'I'));
		po.setAlarmcode((String)formatValueByType(track[6],"",'S'));
		po.setLon((Long)formatValueByType(track[7],"0",'L'));
		po.setLat((Long)formatValueByType(track[8],"0",'L'));
		po.setElevation((Integer)formatValueByType(track[9],"0",'I'));
		
		
		po.setMileage((Long)formatValueByType(track[10],"-1",'L'));
		po.setOil((Long)formatValueByType(track[11],"-1",'L'));
		po.setRpm((Long)formatValueByType(track[13],"-1",'L'));
		String baseStatus = (String)formatValueByType(track[14],"0",'S');
		po.setBaseStatus(baseStatus);
		String number = MathUtils.getBinaryString(baseStatus);
		po.setAccState(Utils.check("0", number));
		po.setGpsState(Utils.check("1", number));
		po.setAreaAlarmAdditional((String)formatValueByType(track[15],"",'S'));
		po.setCoolLiquidtem((Long)formatValueByType(track[16],"-1",'L'));
		po.setVolgate((Long)formatValueByType(track[17],"-1",'L'));
		po.setOilPres((Long)formatValueByType(track[20],"-1",'L'));
		po.setGsPres((Long)formatValueByType(track[21],"-1",'L'));
		po.setTorque((String)formatValueByType(track[22],"",'S'));
		po.setExtendStatus((String)formatValueByType(track[23],"0",'S'));
		po.setOverspeedAlarmAdditional((String)formatValueByType(track[26],"",'S'));
		po.setAlarmAdditional((String)formatValueByType(track[27],"",'S'));
		
		po.setAirTemperture((Long)formatValueByType(track[32],"-1",'L'));
		po.setOpendoorState((String)formatValueByType(track[33],"",'S'));
		po.setMetOil((Long)formatValueByType(track[35],"-1",'L'));
		
		if (track.length>=39){
			String driverId = (String)formatValueByType(track[36],"",'S');
			if (driverId!=null&&!"".equals(driverId)){
				po.setDriverId(driverId);
				po.setDriverName(driverId);
				po.setDriverSrc((String)formatValueByType(track[37],"",'S'));
			}else{
				//如果当前轨迹没有匹配驾驶员信息则需要从刷卡记录中按时间段进行匹配
				String driverinfo = "";//getCurrectDriver(utc);
				if (driverinfo!=null&&!"".equals(driverinfo)){
					String driver[] = driverinfo.split(";");
					po.setDriverId(driver[0]);
					po.setDriverName(driver[1]);
					po.setDriverSrc(driver[2]);
				}
			}
		}
		
		
		return po;
	}
	
	/**
	 * @description:删除数据（已生成的统计数据）
	 * @param:
	 * @author: cuis
	 * @creatTime:  2013-9-26下午02:35:37
	 * @modifyInformation：
	 */
	public static void deleteStatisticsDatas(Long utc){
		CallableStatement dbCstmt = null;
		Connection dbConnection = null;
		try{
			   dbConnection = OracleConnectionPool.getConnection();
			  if (dbConnection!=null){
				dbCstmt = dbConnection.prepareCall(SQLPool.getinstance().getSql("sql_procDeleteStatisticDatas"));
				dbCstmt.setLong(1,utc); 
				dbCstmt.setLong(2,utc + 12 * 60 * 60 * 1000); 
				dbCstmt.setLong(3,utc + 24 * 60 * 60 * 1000); 
				dbCstmt.registerOutParameter(4, Types.INTEGER); 
				dbCstmt.execute();
			  }
			  int successTag = dbCstmt.getInt(4); 
				
				if (successTag==1){
					logger.debug("删除数据（已生成的统计数据）信息成功！");
				}else{
					logger.debug("删除数据（已生成的统计数据）信息出错!");
				}
     		}catch(SQLException e){
				logger.error(" 删除数据（已生成的统计数据）信息出错!",e);
			}finally{
				try {
					if(dbCstmt != null){
						dbCstmt.close();
					}
					if(dbConnection != null){
						dbConnection.close();
					}
				} catch (SQLException e) {
					logger.error("连接放回连接池出错.",e);
				}
		}
	}
	
	/**
	 * 设置将要恢复的数据类别，
	 * @param type
	 */
	public void setRepairDataType(String param){
		this.repairDataType = param;
	}

	public void setFileNameStr(String fileNameStr) {
		this.fileNameStr = fileNameStr;
	}
	
	public void setDate(long date) {
		this.utc = date;
	}

}
