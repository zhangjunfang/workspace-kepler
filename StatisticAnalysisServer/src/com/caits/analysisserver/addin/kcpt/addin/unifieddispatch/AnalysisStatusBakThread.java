package com.caits.analysisserver.addin.kcpt.addin.unifieddispatch;

import java.io.File;
import java.io.IOException;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.TreeMap;
import java.util.concurrent.ArrayBlockingQueue;

import oracle.jdbc.OracleConnection;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.bean.OilMonitorBean2;
import com.caits.analysisserver.bean.OilmassChangedDetail;
import com.caits.analysisserver.bean.StaPool;
import com.caits.analysisserver.bean.SystemBaseInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.OracleDBAdapter;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.services.AlarmAnalyserService;
import com.caits.analysisserver.services.DriverAnalyserService;
import com.caits.analysisserver.services.OilService;
import com.caits.analysisserver.services.SpeedAnomalousAnalyserService;
import com.caits.analysisserver.services.StopstartAnalyserService;
import com.caits.analysisserver.services.VechicleExcStatisticService;
import com.caits.analysisserver.services.VehicleStatusAnalyserService;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.FileUtils;
import com.caits.analysisserver.utils.MathUtils;
import com.caits.analysisserver.utils.Utils;
/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AnalysisStatusThread <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-10-18</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6
 * @ Description: 用于统计车辆相关信息
 */
@SuppressWarnings("unused")
public class AnalysisStatusBakThread extends UnifiedFileDispatch {
	
	private static final Logger logger = LoggerFactory.getLogger(AnalysisStatusBakThread.class);
	
	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(100000);
	
	private final String keyWord = "track";
	

	private VehicleStatus vehicleStatus = null;
	
	private String vid;
	
	private String lastLocInfos[]=null; 
	
	private int threadId = 0;

	private long utc = 0; // 统计那一天UTC时间。
	
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
	
	private DriverAnalyserService driverAnalyserService = null;
	
	private List<String> driverClockinList = new ArrayList<String>();
	
	private SystemBaseInfo limit;
	/**	速度阀值 默认140 （单位：0.1公里） */
	private int speedLimit = 1400;
	
	public void initAnalyser(){

	}
	
	public void run(){ 
		limit = SystemBaseInfoPool.getinstance().getBaseInfoMap("speedLimit");
		if(limit != null && StringUtils.isNumeric(limit.getValue())){
			speedLimit = Integer.parseInt(limit.getValue());
		}
		logger.info("---------------车辆信息统计分析线程-[{}]启动, 速度阀值[{}]", threadId, speedLimit);
		while(true){ 
			try {
				File file = vPacket.take();
				long  startTime=System.currentTimeMillis();
				logger.info( "----车辆信息统计分析线程Thread Id : "+ this.threadId + ", 处理文件名称:" + file.getName());
				vid = file.getName().replaceAll("\\.txt", "");
				
				//核实是否该车安装专属应用油箱油量监控
				String filePath = FileUtils.replaceRoot(file.getPath(), FilePool.getinstance().getFile(this.utc,"oilUrl"),keyWord); 
				oilFile = new File(filePath);
				if(!oilFile.exists()){
					// 判断是否油箱液位传感器
					if(null != AnalysisDBAdapter.oilMonitorMap.get(vid) && "000100060003".equals(AnalysisDBAdapter.oilMonitorMap.get(vid))){
						oilFlag = true;
					}
				}
				
				statisticStatus(file); //开始统计
				long endTime=System.currentTimeMillis();
				logger.info("----车辆信息统计分析线程Thread Id : "+ this.threadId + ", 处理文件名称:" + file.getName()+"，处理时长：" +(endTime-startTime)/1000+"s");
				
			} catch (Exception e) {
				logger.error("Thread Id : "+ this.threadId + ",车辆统计线程  ",e);
			}finally{
				StaPool.addCountListVid(vid);
				
				driverClockinList.clear();
				
				lastLocInfos = null;
				
				oilFlag = false;
				isHasOil = false;
				/*oilList.clear();
				oilFileQuen.clear();
				if(null != oilFileMonitorBean)
				oilFileMonitorBean.resetValue();
				if(null != oilMonitorBean)
				oilMonitorBean.resetValue();
				oilmassChangedDetail = null;
				lastOil = Integer.MAX_VALUE;*/
				oilMonitorBean2 = null;
			}
		}// End while
	}

	public void addPacket(File file) {
		try {
			logger.info("------------file queue length："+vPacket.size());
			vPacket.put(file);
		} catch (InterruptedException e) {
			logger.error("Thread Id : "+ this.threadId + ",车辆总线统计线程出错", e);
		}
	}

	private void statisticStatus(File file) throws Exception{
		OracleConnection dbCon = null;
		try{
		
		if(AnalysisDBAdapter.queryVechileInfo(vid) == null){ // 找不到该车对应的企业相关
			return;
		}
		long start = System.currentTimeMillis();
		
		//初始获取一个数据库连接，防止保存方法获取链接时出现死锁
		dbCon = (OracleConnection) OracleConnectionPool.getConnection();
		
		//查询当前车辆对应日期的驾驶员打卡明细
		selectDriverClockinDetail(dbCon);
		
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
		
		//驾驶明细分析服务初始化
		driverAnalyserService = new DriverAnalyserService(vid,utc);
		
		//速度异常分析初始化
		speedAnomalousAnalyserService = new SpeedAnomalousAnalyserService(utc,""+vid);
		
		/****************************************************************************************************/
		//读取文件，执行分析过程
		readTrackFile(file);
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 文件分析时长 : " + (System.currentTimeMillis() - start)/1000+"s");
		
		/****************************************************************************************************/
		//存储驾驶行为事件
		//oracleDBAdapter.delDriverEvent(vid);
		//oracleDBAdapter.saveDriverEvent(vid,alarmAnalyserService.getDriverEventList());
		long start0 = System.currentTimeMillis();
		OracleDBAdapter.saveStateEventInfo(dbCon,vid,alarmAnalyserService.getStateEventList());
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存状态事件时长 : " + (System.currentTimeMillis() - start0)/1000+"s");
		
		//存储告警服务分析结果
		long start1 = System.currentTimeMillis();
		OracleDBAdapter.saveVehicleAlarmEvent(dbCon,vid,alarmAnalyserService.getAlarmList());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存告警事件时长 : " + (System.currentTimeMillis() - start1)/1000+"s");

		//起步停车填充告警状态时间统计结果
		long startx = System.currentTimeMillis();
		stopstartAnalyserService.fillStopstartList(alarmAnalyserService.getStateEventList());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 填充起步停车数据 : " + (System.currentTimeMillis() - startx)+"ms");
		
		long start4 = System.currentTimeMillis();
		OracleDBAdapter.saveStopstartInfo(dbCon,vid,stopstartAnalyserService.getStopstartlist());//存储起步停车信息
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存起步停车数据 : " + (System.currentTimeMillis() - start4)/1000+"s");
		
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
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存车辆日统计 : " + (System.currentTimeMillis() - start5)/1000+"s");
		//logger.info("Thread Id : "+ this.threadId + ",vid="+ vid + ";存储车辆日统计信息完成." );
		
		//保存驾驶明细数据信息
		long starty = System.currentTimeMillis();
		driverAnalyserService.fillDriverDetailList(alarmAnalyserService.getStateEventList(),alarmAnalyserService.getAlarmList());
		
		OracleDBAdapter.saveDriverDetail(dbCon,vid,driverAnalyserService.getDriverDetaillist());
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存驾驶明细数据 : " + (System.currentTimeMillis() - starty)/1000+"s");
		
		
		// 存储机油压力
		long start6 = System.currentTimeMillis();
		OracleDBAdapter.saveOilPressureDay(dbCon,vid,vechicleExcStatisticService.getOilPressureBean());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存机油压力分布 : " + (System.currentTimeMillis() - start6)/1000+"s");
		

		// 存储冷却液温度
		long start7 = System.currentTimeMillis();
		OracleDBAdapter.saveCoolLiquidtemDayStat(dbCon,vid,vechicleExcStatisticService.getCoolLiquidtemBean());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存冷却液温度分布 : " + (System.currentTimeMillis() - start7)/1000+"s");
		
		// 存储进气压力
		long start8 = System.currentTimeMillis();
		OracleDBAdapter.saveGasPressure(dbCon,vid,vechicleExcStatisticService.getGsPressureBean());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存进气压力分布 : " + (System.currentTimeMillis() - start8)/1000+"s");
		
		// -----存储车速分析
		// 统计车速时间
		long start9 = System.currentTimeMillis();
		OracleDBAdapter.saveSpeeddistDay(dbCon,vid,vechicleExcStatisticService.getSpeeddistDay());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存车速分布 : " + (System.currentTimeMillis() - start9)/1000+"s");
		
		// -----存储转速分析
		// 统计转速时间
		long start10 = System.currentTimeMillis();
		OracleDBAdapter.saveRotatedistDay(dbCon,vid,vechicleExcStatisticService.getRotateSpeedDay());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存转速分布 : " + (System.currentTimeMillis() - start10)/1000+"s");
		
		// 统计蓄电压时间
		long start11 = System.currentTimeMillis();
		OracleDBAdapter.saveVoltageDayStat(dbCon,vid,vechicleExcStatisticService.getVoltagedistDay());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存蓄电压分布 : " + (System.currentTimeMillis() - start11)/1000+"s");
		
		// 存储进气温度
		long start12 = System.currentTimeMillis();
		OracleDBAdapter.saveAirTemperture(dbCon,vid,vechicleExcStatisticService.getAirTempertureBean());
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存进气温度分布 : " + (System.currentTimeMillis() - start12)/1000+"s");
		
		long start13 = System.currentTimeMillis();
		OracleDBAdapter.saveOutLineEventInfo(dbCon,vid,alarmAnalyserService.getvAlarmEventList()); // 存储营运事件表违规  （路段行驶时间不足或过长告警、开门状态）
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 保存违规运营 : " + (System.currentTimeMillis() - start13)/1000+"s");
		
		long start14 = System.currentTimeMillis();
		OracleDBAdapter.saveVehicleIsOverLoad(dbCon,vid,utc); // 单独汇总统计超员
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 汇总超员 : " + (System.currentTimeMillis() - start14)/1000+"s");
		
		long start15 = System.currentTimeMillis();
		OracleDBAdapter.saveOutLineInfo(dbCon,vid,utc); // 存储非法营运日统计信息
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 非法营运 : " + (System.currentTimeMillis() - start15)/1000+"s");
		
		long start16 = System.currentTimeMillis();
		OracleDBAdapter.saveOpenningDoorDetail(dbCon,vid,alarmAnalyserService.getOpeningDoorList()); //存储开门信息
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 存储开门明细 : " + (System.currentTimeMillis() - start16)/1000+"s");
		
		long start17 = System.currentTimeMillis();
		OracleDBAdapter.saveOpenningDoorDay(dbCon,vid,utc); //存储车辆开门运营违规日统计
		//logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 运营违规日统计 : " + (System.currentTimeMillis() - start17)/1000+"s");
		
		long start18 = System.currentTimeMillis();
		if(oilFlag && isHasOil){
			
			// 存储文件油量变化记录
			OracleDBAdapter.writeOilChangedMassFile(vid,oilFile,oilMonitorBean2);
			// 存储数据库油量变化.
			//saveOilChangedMass();
			
			if(oilFile.exists()){ // CHECK是否生成油量文件
				OilService oilService = new OilService(dbCon,utc,vid);
				oilService.analysisOilRecords(oilFile);
			}
		}
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 油量变化 : " + (System.currentTimeMillis() - start18)/1000+"s");
		
		
		//存储速度异常记录
		long start19 = System.currentTimeMillis();
		OracleDBAdapter.saveSpeedAnomalous(dbCon,speedAnomalousAnalyserService.getThVehicleSpeedAnomalous());
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; 速度异常记录 : " + (System.currentTimeMillis() - start19)/1000+"s");
		
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; Total of costing time : " + (System.currentTimeMillis() - start)/1000);
	
		}catch(Exception ex){
			logger.error("Thread Id : "+ this.threadId + ",VID = " + vid +  ";文件分析过程中出错!",ex);
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
	private void readTrackFile(File file) throws Exception{

//		BufferedReader buf = null;
		String number = "";
		

		TreeMap<Long, String> statusMap = new TreeMap<Long, String>();
		int rowCount = 0;
		int currentRow =0;

		try{
//			buf = new BufferedReader(new FileReader(file));
//			statusMap = getTrackMap(buf);
			statusMap = getTrackMap(file);
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
						
						alarmAnalyserService.executeAnalyser(trackBean,isLastRow);
						
						//分析速度异常记录
						speedAnomalousAnalyserService.executeAnalyser(trackBean);
						
						//分析轨迹文件生成起步停车数据和行车数据
						stopstartAnalyserService.executeAnalyser(trackBean,isLastRow);
						
						//驾驶明细分析
						driverAnalyserService.executeAnalyser(trackBean, isLastRow);
						
						// 分析车速、转速等值区间分布
						vechicleExcStatisticService.executeAnalyser(trackBean, isLastRow);

						
						//计算油箱油量监控
						if(oilFlag && isHasOil){
							//checkOilMonitor(cols);
							analyseOilmassData(cols);
						}
						
						//车辆日统计数据分析
						vehicleStatusAnalyserService.executeAnalyser(trackBean,isLastRow);

						//缓存本次位置信息
						lastLocInfos = cols;
//						businessOutLine(cols);
						//lastGpsTime = gpsTime;
				
				}catch(Exception ex){
					logger.error("Thread Id : "+ this.threadId + ",文件:" + file.getAbsolutePath() ,ex);
				}
			}// End for
			
			//当日数据处理结束时，合并未完成的油量监控数据
			if (this.oilMonitorBean2!=null){
				//this.oilMonitorBean2.mergeAndCloseMonitorPoint();
				//this.oilMonitorBean2.antiAliased();
				//this.oilMonitorBean2.signMonitorPoint();
				this.oilMonitorBean2.copyMonitorData();
			}			
	
		}catch(Exception ex){
			logger.error("Thread Id : "+ this.threadId + ",vid="+this.vid+" 分析轨迹文件过程中出错！"+ex.getMessage(),ex);
		}finally{
			if(statusMap != null && statusMap.size() > 0){
				statusMap.clear();
			}
//			if(buf != null){
//				buf.close();
//			}
		}
	}
	
	/**
	 * 根据gps时间将读取的轨迹文件数据进行排序
	 */
	private TreeMap<Long, String> getTrackMap(File file) {
		
		TreeMap<Long, String> returnTrackMap = new TreeMap<Long, String>();
//		String readLine = null;
		String gpsdate = null;
		String[] track = null;
		Long gpstime = null;
		try {
			List<String> list = FileUtils.readLines(file, null);
			for(String str : list){
				// 轨迹文件每行的数据分割
				track = StringUtils.splitPreserveAllTokens(str, ":");
				if(track.length > 35){
//					速度来源是行驶记录仪
					int speed = 0;
					if(StringUtils.isNumeric(track[19]) && StringUtils.isNumeric(track[24]) && track[24].equals("0")){ // 解析速度
						speed = Integer.parseInt(track[19]);
					}else{
//					速度来源是GPS
						if(StringUtils.isNumeric(track[3]) ){
							speed = Integer.parseInt(track[3]);
						} else{
							speed = -1;
						}
					}
					if(speed < 0 && speed >= speedLimit ){ // 非法速度数据过滤
						continue;
					}
					gpsdate = track[2];
					// 将gpsdate转换成utc格式
					gpstime = CDate.stringConvertUtc(gpsdate);
					if(StringUtils.isNumeric(track[25])){ // 解析油量
						int oilBox = Integer.parseInt(track[25]);
						if(oilBox > 0 ){
							isHasOil = true;
						}
					}
					returnTrackMap.put(gpstime, str);	
				}
//			while ((readLine = buf.readLine()) != null) {
//				// 轨迹文件每行的数据分割
//				track = readLine.split(":");
//				if(track.length >1){
//					gpsdate = track[2];
//					// 将gpsdate转换成utc格式
//					gpstime = CDate.stringConvertUtc(gpsdate);
//					int oilBox = Integer.parseInt(Utils.checkEmpty(track[25]) ? "0":track[25]);
//					if(oilBox > 0 ){
//						isHasOil = true;
//					}
//					returnTrackMap.put(gpstime, readLine);	
//				}		
			}// End while		
		
		} catch (Exception e) {
			logger.error("Thread Id : "+ this.threadId + ",读取轨迹文件信息出错",e);
		}
//		finally{
//			
//			if(buf != null){
//				try {
//					buf.close();
//				} catch (IOException e) {
//					logger.error(e.getMessage(), e);
//				}
//			}
//		}

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


	@Override
	public void costTime() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
	}
	
	/****
	 * 设置统计时间
	 */
	@Override
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
		long utc = CDate.stringConvertUtc(track[2]);
		po.setUtc(utc);
		String speedForm = (String)formatValueByType(track[24],"1",'S');
//		Long spd = 0L;
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
				po.setDriverName(getCurrectDriverName(driverId));
				po.setDriverSrc((String)formatValueByType(track[37],"",'S'));
			}else{
				//如果当前轨迹没有匹配驾驶员信息则需要从刷卡记录中按时间段进行匹配
				String driverinfo = getCurrectDriver(utc);
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
	
	/*****
	 * 查询指定时间内驾驶员打卡明细
	 * 
	 * @throws SQLException
	 */
	private void selectDriverClockinDetail(OracleConnection dbCon) {
		try {
			driverClockinList = OracleDBAdapter.selectDriverClockinDetail(dbCon,
					vid, this.utc, this.utc + 24 * 60 * 60 * 1000);
		} catch (Exception e) {
			logger.error("查询指定时段内驾驶员打卡明细过程中出错！", e);
		}
	}
	
	/*****
	 * 根据GPS时间对应当前车辆驾驶员信息
	 * 
	 * @param utc
	 * @return
	 */
	private String getCurrectDriver(long utc) {
		Iterator<String> picUrIt = driverClockinList.iterator();
		String tmpstr = "";
		while (picUrIt.hasNext()) {
			String str = picUrIt.next();
			String[] ky = str.split(";");
			Long beginUtc = Long.parseLong(ky[3]);
			Long endUtc = Long.parseLong(ky[4]);
			if ("0".equals(ky[2])){
				tmpstr = ky[0]+";"+ky[1]+";"+ky[2];
				continue;
			}
			if (utc>=beginUtc && utc <=endUtc) {
				return ky[0]+";"+ky[1]+";"+ky[2];
			}
		}// End while

		return tmpstr;
	}
	
	private String getCurrectDriverName(String driverId) {
		Iterator<String> picUrIt = driverClockinList.iterator();
		while (picUrIt.hasNext()) {
			String str = picUrIt.next();
			String[] ky = str.split(";");
			String id = ky[0];
			if (driverId.equals(id)){
				return ky[1];
			}
		}// End while
		return "";
	}
	
	
}
