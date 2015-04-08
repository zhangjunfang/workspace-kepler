package com.caits.analysisserver.addin.kcpt.addin.unifieddispatch;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.OilMonitorBean2;
import com.caits.analysisserver.bean.OilmassChangedDetail;
import com.caits.analysisserver.bean.RunningBean;
import com.caits.analysisserver.bean.StaPool;
import com.caits.analysisserver.bean.StopstartBean;
import com.caits.analysisserver.bean.ThVehicleSpeedAnomalous;
import com.caits.analysisserver.bean.VehicleAlarm;
import com.caits.analysisserver.bean.VehicleAlarmEvent;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.services.OilService;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.FileUtils;
import com.caits.analysisserver.utils.GetAddressUtil;
import com.caits.analysisserver.utils.GisAccountMeg;
import com.caits.analysisserver.utils.MathUtils;
import com.caits.analysisserver.utils.Utils;
import com.ctfo.generator.pk.GeneratorPK;
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
public class AnalysisStatusThread extends UnifiedFileDispatch {
	
	private static final Logger logger = LoggerFactory.getLogger(AnalysisStatusThread.class);
	
	//记录丢弃数据
	private static final Logger discardData = LoggerFactory.getLogger("discardData");
	
	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(100000);
	
	private final String keyWord = "track";
	
	private VehicleStatus vehicleStatus = null;
	
//	// 存储当日该车非法运营信息
//	private VehicleAlarmEvent outLineBean = null; 
	
	private String vid ;
	
	private Connection dbCon = null;
	
	private List<StopstartBean> stopstartlist = null; 
	
	private String lastLocInfos[]=null; 
	
	private Long tmpLastMileage=-1L;
	
	private Long tmpLastOil = -1L;
	
	private Long tmpLastPreciseOil= -1L;
	
	private int threadId = 0;
	
	//统计状态事件时使用
	private Map<String, String[]> stateMap = new HashMap<String, String[]>();
	private Map<String, Integer> maxSpeedMap = new HashMap<String, Integer>();
	private List<VehicleAlarmEvent> vAlarmEventList = new ArrayList<VehicleAlarmEvent>();
	private List<VehicleAlarmEvent> stateEventList = new ArrayList<VehicleAlarmEvent>();
	private List<String> openningDoorPicList = new ArrayList<String>();
	private List<VehicleAlarmEvent> openingDoorList = new ArrayList<VehicleAlarmEvent>(); //存储开门信息
	
	private long utc = 0; // 统计那一天UTC时间。
	
	// 油箱油量监控
	private boolean oilFlag = false; // 
	/*private OilMonitorBean oilMonitorBean = null;
	private OilMonitorBean oilFileMonitorBean = new OilMonitorBean(); // 存文件列表
	private List<OilmassChangedDetail> oilList = new ArrayList<OilmassChangedDetail>(); // 存入数据库列表
	private Deque<OilmassChangedDetail> oilFileQuen = new ArrayDeque<OilmassChangedDetail>(); // 存入文件列表
	private int lastOil = Integer.MAX_VALUE; // 记录上一次油量值
	private OilmassChangedDetail  oilmassChangedDetail = null;*/
	private File oilFile = null;
	private boolean isHasOil = false;
	
	private OilMonitorBean2 oilMonitorBean2 = null;
	
	private List<VehicleMessageBean> inspectionRecorder = null;
	
	private String inspectionResult[] = null;
	
	private ThVehicleSpeedAnomalous thVehicleSpeedAnomalous=null;
	
	public void initAnalyser(){
		
	}
	
	public void run(){
		logger.info("车辆信息统计分析线程" + this.threadId + "启动");
		AnalysisAlarmThread analysisAlarmThread = new AnalysisAlarmThread();
		analysisAlarmThread.initAnalyser();
		
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
				
				
				//从连接池获取连接
				dbCon = OracleConnectionPool.getConnection();
				
				//新进行告警统计
				analysisAlarmThread.setDBCon(dbCon);
				analysisAlarmThread.analysisAlarm(file.getPath(),utc);
				
				statisticStatus(file); //开始统计
				long endTime=System.currentTimeMillis();
				logger.info("----车辆信息统计分析线程Thread Id : "+ this.threadId + ", 处理文件名称:" + file.getName()+"，处理时长：" +(endTime-startTime)/1000+"s");
				
			} catch (Exception e) {
				logger.error("Thread Id : "+ this.threadId + ",车辆统计线程  ",e);
			}finally{
				StaPool.addCountListVid(vid);
				if(dbCon != null){
					try {
						dbCon.close();
					} catch (SQLException e) {
						logger.error("连接放回连接池出错.",e);
					}
				}
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
			vPacket.put(file);
		} catch (InterruptedException e) {
			logger.error("Thread Id : "+ this.threadId + ",车辆总线统计线程出错", e);
		}
	}

	private void statisticStatus(File file) throws Exception{

		if(AnalysisDBAdapter.queryVechileInfo(vid) == null){ // 找不到该车对应的企业相关
			return;
		}
		long start = System.currentTimeMillis();
		
		//清理状态事件表中信息（每日凌晨统计上一日数据）
		//deleteStateEvent();
		
		//读取轨迹文件
		vehicleStatus = new VehicleStatus();
		//outLineBean = new VehicleAlarmEvent();
		
		//起步停车信息
		stopstartlist = new ArrayList<StopstartBean>();
		
		stateMap.clear();
		maxSpeedMap.clear();
		selectOpenningDoorPic(); // 查询当前车当天所有图片列表
		
		//同步该车告警事件信息
		//syncAlarmEvent(vid); //修改为存储过程同步
		
		readTrackFile(file);
		
		String alarmFile = FileUtils.replaceRoot(file.getAbsolutePath(), FilePool.getinstance().getFile(this.utc,"alarmfileurl"),keyWord);

		saveStateEventInfo();// 存储临时表
		
		readAlarmFile(alarmFile); // 读取报警文件

		searchAlarmDays(); // 查询前一天报警
		accountOpenningCount(); //统计开门次数及区域内开门次数和时间。
		
		saveStopstartInfo();//存储起步停车信息
		logger.info( "Thread Id : "+ this.threadId + ",vid="+ vid + ";存储起步停车信息完成." );
		saveStaDayInfo(); //  存储车辆日统计信息
		logger.info("Thread Id : "+ this.threadId + ",vid="+ vid + ";存储车辆日统计信息完成." );
		updateVehicleDayStat();

		saveOutLineEventInfo(); // 存储营运事件表违规
		
		saveVehicleIsOverLoad(); // 单独汇总统计超员
		
		saveOutLineInfo(); // 存储非法营运日统计信息
		
		saveOpenningDoorDetail(); //存储开门信息
		saveOpenningDoorDay(); //存储车辆开门运营违规日统计
		
		if(oilFlag && isHasOil){
			
			// 存储文件油量变化记录
			writeOilChangedMassFile();
			// 存储数据库油量变化.
			//saveOilChangedMass();
			
			if(oilFile.exists()){ // CHECK是否生成油量文件
				OilService oilService = new OilService(dbCon,utc,vid);
				oilService.analysisOilRecords(oilFile);
			}
		}
		
		//存储GPS巡检记录
		//saveGpsInspection();
		
		//存储速度异常数据记录
		if (thVehicleSpeedAnomalous!=null&&thVehicleSpeedAnomalous.getCount()>0){
			saveSpeedAnomalous();
		}
		
		logger.info("Thread Id : "+ this.threadId + ",VID = " + vid +  "; Total of costing time : " + (System.currentTimeMillis() - start)/1000);
	}
	
	/*****
	 * 查询前一天报警
	 * @throws SQLException 
	 */
	private void searchAlarmDays() throws SQLException{
		PreparedStatement stSearchAlarmDays = null;
		ResultSet rs = null;
		try {
			stSearchAlarmDays = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_searchAlarmDays"));
			stSearchAlarmDays.setString(1, vid);
			stSearchAlarmDays.setLong(2, this.utc); // 指定时间凌晨UTC时间
			stSearchAlarmDays.setLong(3, this.utc + 24 * 60 * 60 * 1000); // 指定时间24小时后时间
			
			rs = stSearchAlarmDays.executeQuery();
			while(rs.next()){
				int num = rs.getInt("ALARM_NUM");
				long time = rs.getInt("ALARM_TIME");
				VehicleAlarm vAlarm = new VehicleAlarm();
				vAlarm.addTime(time);
				vAlarm.addCount(num);
				vehicleStatus.addCountAlarm(num); //总报警次数
				vehicleStatus.getAlarmList().put(rs.getString("ALARM_CODE"), vAlarm);
			}//End while
			
		} catch (SQLException e) {
			logger.error("Thread Id : "+ this.threadId + ",查询前一天报警出错." + vid,e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stSearchAlarmDays != null){
				stSearchAlarmDays.close();
			}
		}
	}
	
	/***
	 *  存储车辆日统计信息
	 * @throws SQLException
	 */
	private void saveStaDayInfo() throws SQLException{
	
		PreparedStatement stSaveDayStInfo = null;
		
		try{
			String cfgFlag = AnalysisDBAdapter.queryVechileInfo(vid).getCfgFlag();
			stSaveDayStInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveDayStInfo"));
			stSaveDayStInfo.setLong(1, this.utc + 12 * 60 * 60 * 1000); // 日期UTC
			stSaveDayStInfo.setString(2, vid); // 车辆编号
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveDayStInfo.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo()); // 车牌号码
				stSaveDayStInfo.setString(4, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode()); // 车架号(VIN)
				stSaveDayStInfo.setString(5, AnalysisDBAdapter.queryVechileInfo(vid).getEntId()); // 企业ID
				stSaveDayStInfo.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName()); // 企业名称
				stSaveDayStInfo.setString(7, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId()); // 车队ID
				stSaveDayStInfo.setString(8, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName()); // 车队名称
			} else {
				stSaveDayStInfo.setString(3, null); // 车牌号码
				stSaveDayStInfo.setString(4, null); // 车架号(VIN)
				stSaveDayStInfo.setString(5, null); // 企业ID
				stSaveDayStInfo.setString(6, null); // 企业名称
				stSaveDayStInfo.setString(7, null); // 车队ID
				stSaveDayStInfo.setString(8, null); // 车队名称
			}
			stSaveDayStInfo.setLong(9, AnalysisDBAdapter.queryVechileInfo(vid).getCheckNum()); // 车辆上线次数(终端成功鉴权次数)
			stSaveDayStInfo.setLong(10, vehicleStatus.getOnOffLine()); // 车辆在线时长
			stSaveDayStInfo.setLong(11, vehicleStatus.getEngineTime()); // 当日发动机运行时间
			stSaveDayStInfo.setLong(12, vehicleStatus.getMileage()); // 当日行驶里程
			if ("1".equals(cfgFlag)){
				stSaveDayStInfo.setDouble(13, vehicleStatus.getPrecise_oil()*0.02); // 当日油耗
			}else{
				stSaveDayStInfo.setLong(13, vehicleStatus.getEcuOil()); // 当日油耗
			}
			
			if(vehicleStatus.getAlarmList().containsKey("1")){
				stSaveDayStInfo.setLong(14,vehicleStatus.getAlarmList().get("1").getSpeedingOil()); // 本日超速下油耗
				stSaveDayStInfo.setLong(15,vehicleStatus.getAlarmList().get("1").getSpeedingMileage()); // 本日超速下行驶里程
			}else{
				stSaveDayStInfo.setLong(14,Types.NULL); // 本日超速下油耗
				stSaveDayStInfo.setLong(15,Types.NULL); // 本日超速下行驶里程
			}
			stSaveDayStInfo.setLong(16, vehicleStatus.getMaxSpeed()); // 本日最大车速
			stSaveDayStInfo.setLong(17, vehicleStatus.getMaxRpm()); // 本日最大发动机转速
			stSaveDayStInfo.setLong(18, vehicleStatus.getCountGPSValid()); // 定位有效数量
			stSaveDayStInfo.setLong(19, vehicleStatus.getCountGPSInvalid()); // 定位无效数量
			stSaveDayStInfo.setLong(20, vehicleStatus.getGpsTimeInvild()); // GPS时间无效数量
			stSaveDayStInfo.setLong(21, vehicleStatus.getCountLatLonInvalid()); // 经纬度无效数量
			stSaveDayStInfo.setLong(22, vehicleStatus.getCountAlarm()); // 报警总数
			int alarmNum = queryCountAlarmInfo();
			stSaveDayStInfo.setLong(23, alarmNum); // 报警总处理数
			
			if(vehicleStatus.getAlarmList().containsKey("0")){
				stSaveDayStInfo.setLong(24, vehicleStatus.getAlarmList().get("0").getCount()); // 紧急报警总数
			}else{
				stSaveDayStInfo.setLong(24, Types.NULL); // 紧急报警总数
			}
			
			stSaveDayStInfo.setLong(25, vehicleStatus.getAccCount()); // ACC开次数
			stSaveDayStInfo.setLong(26, vehicleStatus.getAccTime()); // ACC开时长
			if(vehicleStatus.getAlarmList().containsKey("1")){
				stSaveDayStInfo.setLong(27, vehicleStatus.getAlarmList().get("1").getCount()); // 超速报警
				if(vehicleStatus.getAlarmList().get("1").getTime() > 0){
					stSaveDayStInfo.setLong(28, vehicleStatus.getAlarmList().get("1").getTime()); //超速持续时间
				}else{
					stSaveDayStInfo.setLong(28, 0); //超速持续时间
				}
			}else{
				stSaveDayStInfo.setLong(27, Types.NULL); // 超速报警总数
				stSaveDayStInfo.setLong(28, Types.NULL); // 超速持续时间
			}
			
			if(vehicleStatus.getAlarmList().containsKey("2")){
				stSaveDayStInfo.setLong(29, vehicleStatus.getAlarmList().get("2").getCount()); // 疲劳驾驶次数
				if(vehicleStatus.getAlarmList().get("2").getTime() > 0){
					stSaveDayStInfo.setLong(30, vehicleStatus.getAlarmList().get("2").getTime()); // 疲劳驾驶时间
				}else{
					stSaveDayStInfo.setLong(30, 0); // 疲劳驾驶时间
				}
			}else{
				stSaveDayStInfo.setLong(29, Types.NULL); // 疲劳驾驶次数
				stSaveDayStInfo.setLong(30, Types.NULL); // 疲劳驾驶时间
			}
			
			if(vehicleStatus.getAlarmList().containsKey("4")){
				stSaveDayStInfo.setLong(31, vehicleStatus.getAlarmList().get("4").getCount()); // GNSS模块故障次数
				stSaveDayStInfo.setLong(32, vehicleStatus.getAlarmList().get("4").getTime()); // GNSS模块故障时长
			}else{
				stSaveDayStInfo.setLong(31, Types.NULL); // GNSS模块故障次数
				stSaveDayStInfo.setLong(32, Types.NULL); // GNSS模块故障时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("5")){
				stSaveDayStInfo.setLong(33, vehicleStatus.getAlarmList().get("5").getCount()); // GNSS模块天线未接或被剪断次数
				stSaveDayStInfo.setLong(34, vehicleStatus.getAlarmList().get("5").getTime()); // GNSS模块天线未接或被剪断时长
			}else{
				stSaveDayStInfo.setLong(33, Types.NULL); // GNSS模块天线未接或被剪断次数
				stSaveDayStInfo.setLong(34, Types.NULL); //GNSS模块天线未接或被剪断时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("6")){
				stSaveDayStInfo.setLong(35, vehicleStatus.getAlarmList().get("6").getCount()); // GNSS模块天线短路次数
				stSaveDayStInfo.setLong(36, vehicleStatus.getAlarmList().get("6").getTime()); // GNSS模块天线短路时长
			}else{
				stSaveDayStInfo.setLong(35, Types.NULL); // GNSS模块天线短路次数
				stSaveDayStInfo.setLong(36, Types.NULL); //GNSS模块天线短路时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("7")){
				stSaveDayStInfo.setLong(37, vehicleStatus.getAlarmList().get("7").getCount()); // 终端主电源欠压次数
				stSaveDayStInfo.setLong(38, vehicleStatus.getAlarmList().get("7").getTime()); // 终端主电源欠压时长
			}else{
				stSaveDayStInfo.setLong(37, Types.NULL); // 终端主电源欠压次数
				stSaveDayStInfo.setLong(38, Types.NULL); //终端主电源欠压时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("8")){
				stSaveDayStInfo.setLong(39, vehicleStatus.getAlarmList().get("8").getCount()); // 终端主电源掉电次数
				stSaveDayStInfo.setLong(40, vehicleStatus.getAlarmList().get("8").getTime()); // 终端主电源掉电时长
			}else{
				stSaveDayStInfo.setLong(39, Types.NULL); // 终端主电源掉电次数
				stSaveDayStInfo.setLong(40, Types.NULL); // 终端主电源掉电时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("9")){
				stSaveDayStInfo.setLong(41, vehicleStatus.getAlarmList().get("9").getCount()); // 终端LCD或显示器故障次数
				stSaveDayStInfo.setLong(42, vehicleStatus.getAlarmList().get("9").getTime()); // 终端LCD或显示器故障时长
			}else{
				stSaveDayStInfo.setLong(41, Types.NULL); // 终端LCD或显示器故障次数
				stSaveDayStInfo.setLong(42, Types.NULL); // 终端LCD或显示器故障时长

			}
	
			if(vehicleStatus.getAlarmList().containsKey("10")){
				stSaveDayStInfo.setLong(43, vehicleStatus.getAlarmList().get("10").getCount()); // TIS模块故障次数
				stSaveDayStInfo.setLong(44, vehicleStatus.getAlarmList().get("10").getTime()); // TIS模块故障时长
			}else{
				stSaveDayStInfo.setLong(43, Types.NULL); // TIS模块故障次数
				stSaveDayStInfo.setLong(44, Types.NULL); // TIS模块故障时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("11")){
				stSaveDayStInfo.setLong(45, vehicleStatus.getAlarmList().get("11").getCount()); // 摄像头故障次数
				stSaveDayStInfo.setLong(46, vehicleStatus.getAlarmList().get("11").getTime()); // 摄像头故障时长
			}else{
				stSaveDayStInfo.setLong(45, Types.NULL); // 摄像头故障次数
				stSaveDayStInfo.setLong(46, Types.NULL); // 摄像头故障时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("18")){
				stSaveDayStInfo.setLong(47, vehicleStatus.getAlarmList().get("18").getCount()); // 当天累计驾驶超时时长
			}else{
				stSaveDayStInfo.setLong(47, Types.NULL); // 当天累计驾驶超时时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("19")){
				stSaveDayStInfo.setLong(48, vehicleStatus.getAlarmList().get("19").getCount()); // 超时停车次数
				stSaveDayStInfo.setLong(49, vehicleStatus.getAlarmList().get("19").getTime()); // 超时停车时长
			}else{
				stSaveDayStInfo.setLong(48, Types.NULL); // 超时停车次数
				stSaveDayStInfo.setLong(49, Types.NULL); // 超时停车时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("20")){
				stSaveDayStInfo.setLong(50, vehicleStatus.getAlarmList().get("20").getCount()); // 进区告警次数
			}else{
				stSaveDayStInfo.setLong(50, Types.NULL); // 进区告警次数
			}
			
			if(vehicleStatus.getAlarmList().containsKey("20")){
				stSaveDayStInfo.setLong(51, vehicleStatus.getAlarmList().get("20").getCount()); // 出区告警次数
			}else{
				stSaveDayStInfo.setLong(51, Types.NULL); // 进区告警次数
			}
			
			if(vehicleStatus.getAlarmList().containsKey("21")){
				stSaveDayStInfo.setLong(52, vehicleStatus.getAlarmList().get("21").getCount()); // 进线路次数
			}else{
				stSaveDayStInfo.setLong(52, Types.NULL); // 进线路次数
			}
			
			if(vehicleStatus.getAlarmList().containsKey("21")){
				stSaveDayStInfo.setLong(53, vehicleStatus.getAlarmList().get("21").getCount()); // 出线路次数
			}else{
				stSaveDayStInfo.setLong(53, Types.NULL); // 出线路次数
			}
			
			stSaveDayStInfo.setLong(54, vehicleStatus.getRunCount()); // 路段行驶时间不足次数
	
			stSaveDayStInfo.setLong(55, vehicleStatus.getRunDiffCount()); // 路段行驶时间过长次数
		
			if(vehicleStatus.getAlarmList().containsKey("23")){
				stSaveDayStInfo.setLong(56, vehicleStatus.getAlarmList().get("23").getCount()); // 偏航告警次数
				stSaveDayStInfo.setLong(57, vehicleStatus.getAlarmList().get("23").getTime()); // 偏航告警时长
			}else{
				stSaveDayStInfo.setLong(56, Types.NULL); // 偏航告警次数
				stSaveDayStInfo.setLong(57, Types.NULL); // 偏航告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("24")){
				stSaveDayStInfo.setLong(58, vehicleStatus.getAlarmList().get("24").getCount()); // 车辆VSS故障告警次数
				stSaveDayStInfo.setLong(59, vehicleStatus.getAlarmList().get("24").getTime()); // 车辆VSS故障告警时长
			}else{
				stSaveDayStInfo.setLong(58, Types.NULL); // 车辆VSS故障告警次数
				stSaveDayStInfo.setLong(59, Types.NULL); // 车辆VSS故障告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("25")){
				stSaveDayStInfo.setLong(60, vehicleStatus.getAlarmList().get("25").getCount()); // 车辆油量异常告警次数
				stSaveDayStInfo.setLong(61, vehicleStatus.getAlarmList().get("25").getTime()); // 车辆油量异常告警时长
			}else{
				stSaveDayStInfo.setLong(60, Types.NULL); // 车辆油量异常告警次数
				stSaveDayStInfo.setLong(61, Types.NULL); // 车辆油量异常告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("26")){
				stSaveDayStInfo.setLong(62, vehicleStatus.getAlarmList().get("26").getTime()); // 车辆被盗时长
			}else{
				stSaveDayStInfo.setLong(62, Types.NULL); // 车辆被盗时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("27")){
				stSaveDayStInfo.setLong(63, vehicleStatus.getAlarmList().get("27").getCount()); // 车辆非法点火次数
			}else{
				stSaveDayStInfo.setLong(63, Types.NULL); // 车辆非法点火次数
			}
			
			if(vehicleStatus.getAlarmList().containsKey("28")){
				stSaveDayStInfo.setLong(64, vehicleStatus.getAlarmList().get("28").getCount()); // 车辆非法移位次数
			}else{
				stSaveDayStInfo.setLong(64, Types.NULL); // 车辆非法移位次数
			}
			
			if(vehicleStatus.getAlarmList().containsKey("29")){
				stSaveDayStInfo.setLong(65, vehicleStatus.getAlarmList().get("29").getCount()); // 碰撞侧翻报警次数
				stSaveDayStInfo.setLong(66, vehicleStatus.getAlarmList().get("29").getTime()); // 碰撞侧翻报警时长
			}else{
				stSaveDayStInfo.setLong(65, Types.NULL); // 碰撞侧翻报警次数this.utc + 12 * 60 * 60 * 1000
				stSaveDayStInfo.setLong(66, Types.NULL); // 碰撞侧翻报警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("51")){
				stSaveDayStInfo.setLong(67, vehicleStatus.getAlarmList().get("51").getCount()); // 冷却液温度告警次数
				stSaveDayStInfo.setLong(68, vehicleStatus.getAlarmList().get("51").getTime()); // 冷却液温度告警时长
			}else{
				stSaveDayStInfo.setLong(67, Types.NULL); // 冷却液温度告警次数
				stSaveDayStInfo.setLong(68, Types.NULL); // 冷却液温度告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("34")){
				stSaveDayStInfo.setLong(69, vehicleStatus.getAlarmList().get("34").getCount()); // 机油压力告警次数
				stSaveDayStInfo.setLong(70, vehicleStatus.getAlarmList().get("34").getTime()); // 机油压力告警时长
			}else{
				stSaveDayStInfo.setLong(69, Types.NULL); // 机油压力告警次数
				stSaveDayStInfo.setLong(70, Types.NULL); // 机油压力告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("52")){
				stSaveDayStInfo.setLong(71, vehicleStatus.getAlarmList().get("52").getCount()); // 蓄电池电压告警次数
				stSaveDayStInfo.setLong(72, vehicleStatus.getAlarmList().get("52").getTime()); // 蓄电池电压告警时长
			}else{
				stSaveDayStInfo.setLong(71, Types.NULL); // 蓄电池电压告警次数
				stSaveDayStInfo.setLong(72, Types.NULL); // 蓄电池电压告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("33")){
				stSaveDayStInfo.setLong(73, vehicleStatus.getAlarmList().get("33").getCount()); // 制动气压告警次数
				stSaveDayStInfo.setLong(74, vehicleStatus.getAlarmList().get("33").getTime()); // 制动气压告警时长
			}else{
				stSaveDayStInfo.setLong(73, Types.NULL); // 制动气压告警次数
				stSaveDayStInfo.setLong(74, Types.NULL); // 制动气压告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("43")){
				stSaveDayStInfo.setLong(75, vehicleStatus.getAlarmList().get("43").getCount()); // 燃油告警次数
				stSaveDayStInfo.setLong(76, vehicleStatus.getAlarmList().get("43").getTime()); // 燃油告警时长
			}else{
				stSaveDayStInfo.setLong(75, Types.NULL); // 燃油告警次数
				stSaveDayStInfo.setLong(76, Types.NULL); // 燃油告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("35")){
				stSaveDayStInfo.setLong(77, vehicleStatus.getAlarmList().get("35").getCount()); // 水位低告警次数
				stSaveDayStInfo.setLong(78, vehicleStatus.getAlarmList().get("35").getTime()); // 水位低告警时长
			}else{
				stSaveDayStInfo.setLong(77, Types.NULL); // 水位低告警次数
				stSaveDayStInfo.setLong(78, Types.NULL); // 水位低告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("41")){
				stSaveDayStInfo.setLong(79, vehicleStatus.getAlarmList().get("41").getCount()); // 燃油堵塞次数
				stSaveDayStInfo.setLong(80, vehicleStatus.getAlarmList().get("41").getTime()); // 燃油堵塞时长
			}else{
				stSaveDayStInfo.setLong(79, Types.NULL); // 燃油堵塞次数
				stSaveDayStInfo.setLong(80, Types.NULL); // 燃油堵塞时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("42")){
				stSaveDayStInfo.setLong(81, vehicleStatus.getAlarmList().get("42").getCount()); // 机油温度次数
				stSaveDayStInfo.setLong(82, vehicleStatus.getAlarmList().get("42").getTime()); // 机油温度时长
			}else{
				stSaveDayStInfo.setLong(81, Types.NULL); // 机油温度次数
				stSaveDayStInfo.setLong(82, Types.NULL); // 机油温度时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("38")){
				stSaveDayStInfo.setLong(83, vehicleStatus.getAlarmList().get("38").getCount()); // 缓速器高温次数
				stSaveDayStInfo.setLong(84, vehicleStatus.getAlarmList().get("38").getTime()); // 缓速器高温时长
			}else{
				stSaveDayStInfo.setLong(83, Types.NULL); // 缓速器高温次数
				stSaveDayStInfo.setLong(84, Types.NULL); // 缓速器高温时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("39")){
				stSaveDayStInfo.setLong(85, vehicleStatus.getAlarmList().get("39").getCount()); // 仓温高告警次数
				stSaveDayStInfo.setLong(86, vehicleStatus.getAlarmList().get("39").getTime()); // 仓温高告警时长
			}else{
				stSaveDayStInfo.setLong(85, Types.NULL); // 仓温高告警次数
				stSaveDayStInfo.setLong(86, Types.NULL); // 仓温高告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("47")){
				stSaveDayStInfo.setLong(87, vehicleStatus.getAlarmList().get("47").getCount()); // 发动机超转次数
				if(vehicleStatus.getAlarmList().get("47").getTime() >0){
					stSaveDayStInfo.setLong(88, vehicleStatus.getAlarmList().get("47").getTime()); // 发动机超转时长
				}else{
					stSaveDayStInfo.setLong(88, 0); // 发动机超转时长
				}
			}else{
				stSaveDayStInfo.setLong(87, Types.NULL); // 发动机超转次数
				stSaveDayStInfo.setLong(88, Types.NULL); // 发动机超转时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("e8")){
				stSaveDayStInfo.setLong(89, vehicleStatus.getAlarmList().get("e8").getCount()); // 二档起步次数
				stSaveDayStInfo.setLong(90, vehicleStatus.getAlarmList().get("e8").getTime()); // 二档起步时长
			}else{
				stSaveDayStInfo.setLong(89, Types.NULL); // 二档起步次数
				stSaveDayStInfo.setLong(90, Types.NULL); // 二档起步时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("44")){
				stSaveDayStInfo.setLong(91, vehicleStatus.getAlarmList().get("44").getCount()); // 空档滑行次数
				if(vehicleStatus.getAlarmList().get("44").getTime() > 0 ){
					stSaveDayStInfo.setLong(92, vehicleStatus.getAlarmList().get("44").getTime()); // 空档滑行时长
				}else{
					stSaveDayStInfo.setLong(92, 0); // 空档滑行时长
				}
			}else{
				stSaveDayStInfo.setLong(91, Types.NULL); // 空档滑行次数
				stSaveDayStInfo.setLong(92, Types.NULL); // 空档滑行时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("48")){
				stSaveDayStInfo.setLong(93, vehicleStatus.getAlarmList().get("48").getCount()); // 急加速次数
				stSaveDayStInfo.setLong(94, vehicleStatus.getAlarmList().get("48").getTime()); // 急加速时长
			}else{
				stSaveDayStInfo.setLong(93, Types.NULL); // 急加速次数
				stSaveDayStInfo.setLong(94, Types.NULL); // 急加速时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("49")){
				stSaveDayStInfo.setLong(95, vehicleStatus.getAlarmList().get("49").getCount()); // 急减速次数
				stSaveDayStInfo.setLong(96, vehicleStatus.getAlarmList().get("49").getTime()); // 急减速时长
			}else{
				stSaveDayStInfo.setLong(95, Types.NULL); // 急减速次数
				stSaveDayStInfo.setLong(96, Types.NULL); // 急减速时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("45")){
				stSaveDayStInfo.setLong(97, vehicleStatus.getAlarmList().get("45").getCount()); // 超长怠速次数
				if(vehicleStatus.getAlarmList().get("45").getTime() > 0){
					stSaveDayStInfo.setLong(98, vehicleStatus.getAlarmList().get("45").getTime()); // 超长怠速时长
				}else{
					stSaveDayStInfo.setLong(98, 0); // 超长怠速时长
				}
			}else{
				stSaveDayStInfo.setLong(97, Types.NULL); // 超长怠速次数
				stSaveDayStInfo.setLong(98, Types.NULL); // 超长怠速时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("46")){
				stSaveDayStInfo.setLong(99, vehicleStatus.getAlarmList().get("46").getCount()); // 怠速空调次数
				if(vehicleStatus.getAlarmList().get("46").getTime() > 0){
					stSaveDayStInfo.setLong(100, vehicleStatus.getAlarmList().get("46").getTime()); // 怠速空调时长
				}else{
					stSaveDayStInfo.setLong(100, 0); // 怠速空调时长
				}
			}else{
				stSaveDayStInfo.setLong(99, Types.NULL); // 怠速空调次数
				stSaveDayStInfo.setLong(100, Types.NULL); // 怠速空调时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("36")){
				stSaveDayStInfo.setLong(101, vehicleStatus.getAlarmList().get("36").getCount()); // 制动蹄片磨损次数
				stSaveDayStInfo.setLong(102, vehicleStatus.getAlarmList().get("36").getTime()); // 制动蹄片磨损时长
			}else{
				stSaveDayStInfo.setLong(101, Types.NULL); // 制动蹄片磨损次数
				stSaveDayStInfo.setLong(102, Types.NULL); // 制动蹄片磨损时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("37")){
				stSaveDayStInfo.setLong(103, vehicleStatus.getAlarmList().get("37").getCount()); // 空滤堵塞次数
				stSaveDayStInfo.setLong(104, vehicleStatus.getAlarmList().get("37").getTime()); // 空滤堵塞时长
			}else{
				stSaveDayStInfo.setLong(103, Types.NULL); // 空滤堵塞次数
				stSaveDayStInfo.setLong(104, Types.NULL); // 空滤堵塞时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("e5")){
				stSaveDayStInfo.setLong(105, vehicleStatus.getAlarmList().get("e5").getTime()); // 超经济区运行时长
			}else{
				stSaveDayStInfo.setLong(105, Types.NULL); // 超经济区运行时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("202")){
				stSaveDayStInfo.setLong(106, vehicleStatus.getAlarmList().get("202").getCount()); // 区域超速告警次数
				stSaveDayStInfo.setLong(107, vehicleStatus.getAlarmList().get("202").getTime()); // 区域超速告警时长
			}else{
				stSaveDayStInfo.setLong(106, Types.NULL); // 区域超速告警次数
				stSaveDayStInfo.setLong(107, Types.NULL); // 区域超速告警时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("e1")){
				stSaveDayStInfo.setLong(108, vehicleStatus.getAlarmList().get("e1").getTime()); // 加热器运行时长
			}else{
				stSaveDayStInfo.setLong(108, Types.NULL); // 加热器运行时长
			}
			
			if(vehicleStatus.getAlarmList().containsKey("e2")){
				stSaveDayStInfo.setLong(109, vehicleStatus.getAlarmList().get("e2").getTime()); // 空调开启时间
			}else{
				stSaveDayStInfo.setLong(109, Types.NULL); // 空调开启时间
			}
			
			stSaveDayStInfo.setLong(110, vehicleStatus.getDoor1()); // 门1开启次数
			stSaveDayStInfo.setLong(111, vehicleStatus.getDoor2()); // 门2开启次数
			stSaveDayStInfo.setLong(112, vehicleStatus.getDoor3()); // 门3开启次数
			stSaveDayStInfo.setLong(113, vehicleStatus.getDoor4()); // 其他门开启次数
			stSaveDayStInfo.setLong(114, (vehicleStatus.getDoor4() + vehicleStatus.getDoor3() + vehicleStatus.getDoor2() + vehicleStatus.getDoor1())); // 门开启总次数
			stSaveDayStInfo.setLong(115, vehicleStatus.getAreaOpenDoorNum()); // 区域内开关门次数
			stSaveDayStInfo.setLong(116, vehicleStatus.getAreaOpenDoorTime()); // 区域内开门时长
	
			if(vehicleStatus.getAlarmList().containsKey("40")){
				stSaveDayStInfo.setLong(117, vehicleStatus.getAlarmList().get("40").getCount()); //机虑堵塞次数
				stSaveDayStInfo.setLong(118, vehicleStatus.getAlarmList().get("40").getTime()); // 机虑堵塞时长
			}else{
				stSaveDayStInfo.setLong(117, Types.NULL); // 机虑堵塞次数
				stSaveDayStInfo.setLong(118, Types.NULL); // 机虑堵塞时长
			}
			int overLoadNum = countOverLoad();
			stSaveDayStInfo.setLong(119, overLoadNum); // 超载次数(由多媒体历史信息表中统计)
//			if(vehicleStatus.getAlarmList().containsKey("42")){
//				stSaveDayStInfo.setLong(120, vehicleStatus.getAlarmList().get("42").getCount()); //非法停靠次数
//				stSaveDayStInfo.setLong(121, vehicleStatus.getAlarmList().get("42").getTime()); // 非法停靠时长
//			}else{
				stSaveDayStInfo.setLong(120, Types.NULL); // 非法停靠次数
				stSaveDayStInfo.setLong(121, Types.NULL); // 非法停靠时长
//			}
			
			if(vehicleStatus.getAlarmList().containsKey("e9")){
				stSaveDayStInfo.setLong(122, vehicleStatus.getAlarmList().get("e9").getCount()); //档位不当次数
				stSaveDayStInfo.setLong(123, vehicleStatus.getAlarmList().get("e9").getTime()); // 档位不当持续时间
			}else{
				stSaveDayStInfo.setLong(122, Types.NULL); // 档位不当次数
				stSaveDayStInfo.setLong(123, Types.NULL); // 档位不当持续时间
			}
			
			stSaveDayStInfo.setLong(124, vehicleStatus.getIdlingTime()); // 怠速时间
			
			String vlineId = AnalysisDBAdapter.queryVechileInfo(vid).getVlineId();
			
			if(  vlineId != null && !"".equals(vlineId)){ // 线路ID
				stSaveDayStInfo.setString(125,  vlineId); 
			}else{
				stSaveDayStInfo.setNull(125,  Types.VARCHAR);
			}
			
			if( AnalysisDBAdapter.queryVechileInfo(vid).getLineName() != null){ // 线路ID
				stSaveDayStInfo.setString(126,  AnalysisDBAdapter.queryVechileInfo(vid).getLineName()); 
			}else{
				stSaveDayStInfo.setString(126, null);
			}
			
			if ("1".equals(cfgFlag)){
				stSaveDayStInfo.setDouble(127, vehicleStatus.getMetRunningOil()*0.02); // 行车油耗
			}else{
				stSaveDayStInfo.setLong(127, vehicleStatus.getEcuRunningOil()); // 行车油耗
			}
			
			stSaveDayStInfo.setLong(128, vehicleStatus.getOpening_door_ex_num()); // 开门异常
			stSaveDayStInfo.setLong(129, vehicleStatus.getPrecise_oil());
			stSaveDayStInfo.setLong(130, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程
			stSaveDayStInfo.setLong(131, vehicleStatus.getPoint_oil()); // 最后一个点减第一个点计算油耗
			stSaveDayStInfo.setLong(132, vehicleStatus.getGis_milege()); // GIS计算里程
			
			stSaveDayStInfo.setLong(133,vehicleStatus.getMetRunningOil());//当日精准行车油耗
			stSaveDayStInfo.setLong(134, (vehicleStatus.getPrecise_oil()-vehicleStatus.getMetRunningOil()));//当日精准怠速油耗
			stSaveDayStInfo.setLong(135, vehicleStatus.getEcuOil());//当日ECU油耗
			stSaveDayStInfo.setLong(136, vehicleStatus.getEcuRunningOil());//当日ECU行车油耗
			stSaveDayStInfo.setLong(137, (vehicleStatus.getEcuOil()-vehicleStatus.getEcuRunningOil()));//当日ECU怠速油耗
			stSaveDayStInfo.setString(138, cfgFlag);//是否使用精准油耗 1 使用 0不使用
			
			stSaveDayStInfo.executeUpdate();
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ",车辆日统计出错。" + vid,e);
		}finally{
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
		}
	}
	
	/**vehicleStatus
	 * 更新车辆信号状态信息
	 * 
	 */
	private void updateVehicleDayStat() throws SQLException{
		PreparedStatement ps = null;
		try {
			ps = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_updateDayStat"));
			ps.setString(1, vid);
			ps.setString(2, vid);
			ps.setString(3, vid);
			ps.setString(4, vid);
			ps.setString(5, vid);
			ps.setString(6, vid);
			ps.setString(7, vid);
			ps.setString(8, vid);
			ps.setString(9, vid);
			ps.setString(10, vid);
			ps.setString(11, vid);
			ps.setString(12, vid);
			ps.setString(13, vid);
			ps.setString(14, vid);
			ps.setString(15, vid);
			ps.setString(16, vid);
			ps.setString(17, vid);
			ps.setString(18, vid);
			ps.setString(19, vid);
			ps.setString(20, vid);
			ps.setString(21, vid);
			ps.setString(22, vid);
			ps.setString(23, vid);
			ps.setString(24, vid);
			ps.setString(25, vid);
			ps.setString(26, vid);
			ps.setString(27, vid);
			ps.setString(28, vid);
			ps.setLong(29, this.utc);
			ps.setLong(30, this.utc + 24 * 60 * 60 * 1000);
			ps.execute();
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			if(ps != null){
				ps.close();
			}
		}
	}
	
	/**
	 * 存储起步停车及行车数据
	 * @throws SQLException
	 */
	private void saveStopstartInfo() throws SQLException{
		if(stopstartlist.size() < 1){ // 无起步停车信息
			return;
		}
		PreparedStatement stopstartSt = null;
		PreparedStatement runningSt = null;
		try {
			//提取当前车的配置方案信息
			String cfgFlag = AnalysisDBAdapter.queryVechileInfo(vid).getCfgFlag();
			
			stopstartSt = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveStopstartInfo"));
			//最后一个对象冗余，因此stopstartlist.size()-2
			for (int i=0;i<stopstartlist.size();i++){
				try {
					StopstartBean ssb = stopstartlist.get(i);
					
//					long subStartMileage = -1;
//					long subEndMileage = -1;
//					long subStartOil = -1;
//					long subEndOil = -1;
					long subStartTime = -1;
					long subEndTime = -1;
					
					double ecuRunningOilSubTotal = 0;
					double metRunningOilSubTotal = 0;
					//long runningMileage = 0;
					long runningTime = 0;
					
//					long id  = CDate.getNow().getTime();
					String mainId = GeneratorPK.instance().getPKString();
					List<RunningBean> runninglist = ssb.getRunninglist();
					//排除疑似漂移的行车数据：无ECU油耗和精准油耗、无里程、有短暂车速、每个起步停车里只包含一个行车过程
					if (runninglist!=null&&runninglist.size()==1){
						RunningBean rb = runninglist.get(0);
						long ecuRunningOil = rb.getEcuRunningOil();
						long metRunningOil = rb.getMetRunningOil();
						long runningMileage = rb.getRunningMileage();

						if (ecuRunningOil<=0&&metRunningOil<=0&&runningMileage<=0){
							continue;
						}
					}
					
					if (runninglist!=null&&runninglist.size()>0){
						
						runningSt = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveRunningInfo"));
						for (int j=0;j<runninglist.size();j++){
							RunningBean rb = runninglist.get(j);
							String detailId  = GeneratorPK.instance().getPKString();
							runningSt.setString(1, detailId+""+j);
							runningSt.setString(2, mainId);
							runningSt.setString(3, vid);
							runningSt.setString(4, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
							/*runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
							runningSt.setLong(7, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
							runningSt.setString(8, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
							runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getDriverId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getDriverName());
							runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getVlineId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());*/
							long startTime = CDate.stringConvertUtc(rb.getStartTime());
							long endTime = CDate.stringConvertUtc(rb.getStopTime());
							
							runningTime +=(endTime-startTime);
							
							runningSt.setLong(5, startTime);
							runningSt.setLong(6, endTime);
							runningSt.setLong(7, rb.getBeginLon());
							runningSt.setLong(8, rb.getBeginLat());
							runningSt.setLong(9, rb.getEndLon());
							runningSt.setLong(10, rb.getEndLat());
//							long startRunningMileage = rb.getStartRunningMileage();
//							long stopRunningMileage = rb.getStopRunningMileage();
//							if (startRunningMileage<0){
//								startRunningMileage = stopRunningMileage;
//							}
//							long subTotalMileage = stopRunningMileage-startRunningMileage;
							
							runningSt.setLong(11, rb.getStartRunningMileage());
							runningSt.setLong(12, rb.getStopRunningMileage());
							runningSt.setLong(13, rb.getRunningMileage());
//							long startRunningOil = rb.getStartRunningOil();
//							long stopRunningOil = rb.getStopRunningOil();
//							if (startRunningOil<0){
//								startRunningOil = stopRunningOil;
//							}
//							long subTotalOil = stopRunningOil-startRunningOil;
							
							
							runningSt.setLong(14, rb.getStartRunningOil());
							runningSt.setLong(15, rb.getStopRunningOil());
							
							metRunningOilSubTotal +=  rb.getMetRunningOil();
							ecuRunningOilSubTotal += rb.getEcuRunningOil();
							
							if ("1".equals(cfgFlag)){
								//精准油耗时要进行单位换算 由 0.01换算为0.5  
								runningSt.setDouble(16, rb.getMetRunningOil()*0.02);
							}else{
								runningSt.setLong(16, rb.getEcuRunningOil());
							}
							
							
							runningSt.setLong(17, rb.getMaxRotateSpeed());
							runningSt.setLong(18, rb.getMaxSpeed());
							runningSt.setString(19, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
							runningSt.setString(20, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
							runningSt.setString(21, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
							runningSt.setString(22, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
							if(AnalysisDBAdapter.queryVechileInfo(vid).getVinCode() != null){
								runningSt.setString(23, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
							}else{
								runningSt.setString(23, null);
							}
							
							String vLineId = AnalysisDBAdapter.queryVechileInfo(vid).getVlineId();
							if(  vLineId != null && !"".equals(vLineId)){
								runningSt.setString(24, vLineId);
							}else{
								runningSt.setNull(24, Types.VARCHAR);
							}
							
							if(AnalysisDBAdapter.queryVechileInfo(vid).getLineName() != null){
								runningSt.setString(25, AnalysisDBAdapter.queryVechileInfo(vid).getLineName());
							}else{
								runningSt.setString(25, null);
							}
							
							runningSt.setLong(26, rb.getEcuRunningOil());
							runningSt.setLong(27, rb.getMetRunningOil());
							runningSt.setString(28, cfgFlag);
							
							if (j==0){
//								subStartMileage = startRunningMileage;
//								subStartOil = startRunningOil;
								subStartTime = startTime;
							}
							if (j==runninglist.size()-1){
//								subEndMileage = stopRunningMileage;
//								subEndOil = stopRunningOil;
								subEndTime = endTime;
							}
		
							runningSt.addBatch();
						}// Inner of end for
						runningSt.executeBatch();
						
						if(runningSt != null){
							runningSt.close();
						}
					}
					stopstartSt.setString(1, mainId);
					stopstartSt.setString(2,vid);
					stopstartSt.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
					
					long launchTime = 0;
					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
						stopstartSt.setNull(4, Types.NULL);
					}else{
						launchTime = CDate.stringConvertUtc(ssb.getLaunchTime());
						stopstartSt.setLong(4, launchTime);
					}
					
					if (subStartTime!=-1){
						stopstartSt.setLong(5, subStartTime);
					}else{
						stopstartSt.setNull(5, Types.INTEGER);
					}
					
					if (subEndTime!=-1){
						stopstartSt.setLong(6, subEndTime);
					}else{
						stopstartSt.setNull(6, Types.INTEGER);
					}
					
					long fireoffTime = 0;
					if (ssb.getFireoffTime()==null||"".equals(ssb.getFireoffTime())){
						stopstartSt.setNull(7, Types.NULL);
					}else{
						fireoffTime = CDate.stringConvertUtc(ssb.getFireoffTime());
						stopstartSt.setLong(7, fireoffTime);
					}
					
					stopstartSt.setLong(8, ssb.getBeginLon());
					stopstartSt.setLong(9, ssb.getBeginLat());
					stopstartSt.setLong(10, ssb.getEndLon());
					stopstartSt.setLong(11, ssb.getEndLat());
//					long startMileage = ssb.getStartMileage();
//					long stopMileage = ssb.getEndMileage();
//					long totalMileage = 0;
//					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
//						totalMileage = runningMileage;
//					}else{
//						totalMileage=stopMileage-startMileage;
//					}
//
//					if( totalMileage< 0){
//						totalMileage = 0;
//					}
					
					stopstartSt.setLong(12, ssb.getStartMileage());
					stopstartSt.setLong(13, ssb.getEndMileage());
					stopstartSt.setLong(14, ssb.getMileage());
//					long startOil = ssb.getStartOil();
//					long stopOil = ssb.getEndOil();
//	
//					long totalOil = 0;
//					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
//						totalOil = 0;
//					}else{
//						if(stopOil > 0 && startOil > 0)
//						totalOil = stopOil-startOil;
//					}
//
//					if( totalOil< 0){
//						totalOil = 0;
//					}
					
					
					stopstartSt.setLong(15, ssb.getStartOil());
					stopstartSt.setLong(16, ssb.getEndOil());
					
					if ("1".equals(cfgFlag)){
						stopstartSt.setDouble(17, ssb.getMetOilWear()*0.02);
					}else{
						stopstartSt.setLong(17, ssb.getEcuOilWear());
					}
					
					stopstartSt.setLong(18, ssb.getMaxRotateSpeed());
					stopstartSt.setLong(19, ssb.getMaxSpeed());
					stopstartSt.setLong(20, runninglist.size());
					if ("1".equals(cfgFlag)){
						stopstartSt.setDouble(21, (ssb.getMetOilWear()-metRunningOilSubTotal)*0.02);
						stopstartSt.setDouble(22, metRunningOilSubTotal*0.02);
					}else{
						stopstartSt.setDouble(21, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
						stopstartSt.setDouble(22, ecuRunningOilSubTotal);
					}
					
					
					//子查询语句条件
					long fromTime=launchTime;
					if (launchTime==0){
						fromTime = subStartTime;
					}
					long endTime=fireoffTime;
					if (fireoffTime==0){
						endTime=subEndTime;
					}

					stopstartSt.setString(23,vid);
					stopstartSt.setLong(24, fromTime);
					stopstartSt.setLong(25, endTime);
					stopstartSt.setLong(26, fromTime);
					stopstartSt.setLong(27, endTime);
					
					stopstartSt.setString(28,vid);
					stopstartSt.setLong(29, fromTime);
					stopstartSt.setLong(30, endTime);
					stopstartSt.setLong(31, fromTime);
					stopstartSt.setLong(32, endTime);
					
					
					stopstartSt.setString(33,vid);
					stopstartSt.setLong(34, fromTime);
					stopstartSt.setLong(35, endTime);
					stopstartSt.setLong(36, fromTime);
					stopstartSt.setLong(37, endTime);
					
					stopstartSt.setString(38,vid);
					stopstartSt.setLong(39, fromTime);
					stopstartSt.setLong(40, endTime);
					stopstartSt.setLong(41, fromTime);
					stopstartSt.setLong(42, endTime);
					
					stopstartSt.setString(43,vid);
					stopstartSt.setLong(44, fromTime);
					stopstartSt.setLong(45, endTime);
					stopstartSt.setLong(46, fromTime);
					stopstartSt.setLong(47, endTime);
					
					stopstartSt.setString(48,vid);
					stopstartSt.setLong(49, fromTime);
					stopstartSt.setLong(50, endTime);
					stopstartSt.setLong(51, fromTime);
					stopstartSt.setLong(52, endTime);
					
					
					stopstartSt.setString(53,vid);
					stopstartSt.setLong(54, fromTime);
					stopstartSt.setLong(55, endTime);
					stopstartSt.setLong(56, fromTime);
					stopstartSt.setLong(57, endTime);
					
					stopstartSt.setString(58,vid);
					stopstartSt.setLong(59, fromTime);
					stopstartSt.setLong(60, endTime);
					stopstartSt.setLong(61, fromTime);
					stopstartSt.setLong(62, endTime);
					
					stopstartSt.setLong(63,(fireoffTime-launchTime)/1000);//发动机工作时长 unit:s
					stopstartSt.setLong(64,runningTime/1000);//行车时长 unit:s
					
					stopstartSt.setString(65, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
					stopstartSt.setString(66, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
					stopstartSt.setString(67, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
					stopstartSt.setString(68, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
					if(AnalysisDBAdapter.queryVechileInfo(vid).getVinCode() != null){
						stopstartSt.setString(69, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
					}else{
						stopstartSt.setString(69, null);
					}
					
					String vLineId =AnalysisDBAdapter.queryVechileInfo(vid).getVlineId();
					if( vLineId!= null && !"".equals(vLineId)){
						stopstartSt.setString(70, vLineId);
					}else{
						stopstartSt.setNull(70, Types.VARCHAR);
					}
					
					if(AnalysisDBAdapter.queryVechileInfo(vid).getLineName() != null){
						stopstartSt.setString(71, AnalysisDBAdapter.queryVechileInfo(vid).getLineName());
					}else{
						stopstartSt.setString(71, null);
					}
					
					stopstartSt.setLong(72, this.utc + 12 * 60 * 60 * 1000);// 统计日期UTC
					
					stopstartSt.setLong(73, ssb.getEcuOilWear());
					stopstartSt.setDouble(74, ecuRunningOilSubTotal);
					stopstartSt.setDouble(75, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
					stopstartSt.setLong(76, ssb.getMetOilWear());
					stopstartSt.setDouble(77, metRunningOilSubTotal);
					stopstartSt.setDouble(78, ssb.getMetOilWear()-metRunningOilSubTotal);
					stopstartSt.setString(79, cfgFlag);
					
					stopstartSt.addBatch();
			}catch(SQLException e){
				logger.error("Thread Id : "+ this.threadId + ",存储起步停车数据出错." + vid, e);
				dbCon.rollback();
			}finally{
				if(runningSt != null){
					runningSt.close();
				}
			}
			}
			stopstartSt.executeBatch();
			
		} catch (Exception e) {
			logger.error("Thread Id : "+ this.threadId + ",存储起步停车数据出错." + vid, e);
			dbCon.rollback();
		}finally{
			/*if(runningSt != null){
				runningSt.close();
			}*/
			if(stopstartSt != null){
				stopstartSt.close();
			}
			if(stopstartlist != null){
				stopstartlist.clear();
			}
			stopstartlist = null;
		}
	}
	
	/**
	 * 读取轨迹文件
	 * @param file
	 * @throws SQLException 
	 * @throws IOException 
	 */
	private void readTrackFile(File file) throws Exception{
		long dayFirstCostOil = 0; // 日第一次累计油耗
		long dayLastCostOil = 0; // 日最后一次累计油耗
		int maxSpeed = 0;
		int maxRpm = 0;
		int mileage = 0;
		int tempMileage = 0;
		long accTime = 0;
		int accCount = 0;
		String startGpsTime = ""; // GPS时间
		String startAccGpsTime = ""; // Acc GPS时间
		String gpsTime = ""; // GPS时间
		String lastGpsTime = ""; // 上一条GPS时间
		boolean acc = false; //标记是否开始统计ACC
		long onlineTime = 0; // 车辆平台在线时间
		
		long runningTime = 0; // 行车时间 
		boolean isEngineFlag = false; //发动机运行
		String startEngineDate = ""; // 发动机运行开始时间
		long engineTime = 0; // 发动机运行时间
		boolean isflag = false;
		int count = 0;
		long intervalTime = 90 * 24 * 60 * 60 * 1000l;
		BufferedReader buf = null;
		String number = "";
		String[] column = null;
		String co = "";
		boolean accStatus = false;
		boolean isRunningNotEnoughTime = false; //行驶时间不足
		boolean isRunningLongTime = false; // 行驶时间过长
		int runCount = 0;   // 行驶时间不足次数
		int runDiffCount = 0;   // 行驶时间过长次数
		//long runningOil = 0l; // 行车油耗
		//long startRunningOil = 0; // 行车开始点累计油耗
		int opening_door_ex_num = 0; // 开门异常次数
		TreeMap<Long, String> statusMap = new TreeMap<Long, String>();
		int rowCount = 0;
		int currentRow =0;
		VehicleAlarmEvent event = null;
		long first_precise_oil = 0; //流量计油耗第一条
		long last_precise_oil = 0; //流量计油耗最后一条
		try{
			buf = new BufferedReader(new FileReader(file));
			statusMap = getTrackMap(buf);
			rowCount = statusMap.size();
			Set<Long> key = statusMap.keySet();
			Long keys = null;		
			String[] cols = null;
			
			// GIS 计算里程
			if("true".equals(SystemBaseInfoPool.getinstance().getBaseInfoMap("gis_url").getIsLoad())){
				vehicleStatus.addGis_milege(GisAccountMeg.accountMilege(statusMap,vid));
			}
			//定义速度异常对象
			thVehicleSpeedAnomalous=new ThVehicleSpeedAnomalous();
			
			for (Iterator<Long> it = key.iterator(); it.hasNext();) {
				keys = (Long) it.next();
				try{
						cols = statusMap.get(keys).split(":");
						String spdFrom = cols[24];	
						//分析轨迹文件生成起步停车数据和行车数据
						currentRow++;
						analysisStopstart(stopstartlist,cols,rowCount==currentRow,statusMap.get(keys));
						//分析生成车辆状态数据
						analysisStateEvent(cols);
						
						//计算油箱油量监控
						if(oilFlag && isHasOil){
							//checkOilMonitor(cols);
							analyseOilmassData(cols);
						}
						
						//分析GPS巡检数据
						/*if (AnalysisDBAdapter.queryVechileInfo(vid).getEntId()>0){
							generalGpsInspectionRecorder(cols);
						}*/
						
						//判断车辆是否同时上传vss和gps车速
						if (cols[3] != null && !cols[3].equals("")&&!cols[3].equals("-1")&&cols[19] != null && !cols[19].equals("")&&!cols[19].equals("-1")){
							long vss = Long.parseLong(cols[19]);
							long gps = Long.parseLong(cols[3]);
							if (vss>=50&&gps>=50){
								thVehicleSpeedAnomalous.setVssGPSSpeedTotal(vss, gps);
								//保存最后有效的车速来源
								thVehicleSpeedAnomalous.setSpeedForm(spdFrom);
							}
						}
						
						
						Long lon =  Long.parseLong(cols[7]==null ||"".equals(cols[7])?"0":cols[7]);
						Long lat = Long.parseLong(cols[8]==null ||"".equals(cols[8])?"0":cols[8]);
						gpsTime = cols[2];

						//计算在线时间
						onlineTime = onlineTime + accountOffLineTime(gpsTime,lastGpsTime);
						
						int gpsSpeed = 0; // 转换后速度
						gpsSpeed = Utils.getSpeed(spdFrom,cols)/10; // 根据来源获取车速
						String binaryStus = "";
						if(!Utils.checkEmpty(cols[14])){
							binaryStus = Long.toBinaryString(Long.parseLong(cols[14]==null ||"".equals(cols[14])?"0":cols[14]));
						}
						
						// 上传时间跟当前时间相差三个月
						if(gpsTime != null && !gpsTime.equals("") && (Math.abs(System.currentTimeMillis() - CDate.stringConvertUtc(gpsTime)) > intervalTime)){ 
							count++;
						}
						
						// 纬度范围18-54(10800000-32400000)  经度范围72-136(43200000-81600000)
						if (lon == null || lon > 43200000 || lon < 81600000 || lat == null || lat < 10800000 || lat > 32400000) {
							vehicleStatus.addCountLatLonInvalid(1);
						}
						
						//累计油耗
						if(cols[11] != null && !cols[11].equals("") && !cols[11].equals("0") && !cols[11].equals("-1")  && dayFirstCostOil == 0){ // 油耗
							dayFirstCostOil = Long.parseLong(cols[11]);
						}
						
						if(cols[11] != null && !cols[11].equals("") && !cols[11].equals("0") && !cols[11].equals("-1")){ // 油耗
							dayLastCostOil = Long.parseLong(cols[11]);
						}
						
						//流量计油耗
						if(cols[35] != null && !cols[35].equals("") && !cols[35].equals("0") && !cols[35].equals("-1") && first_precise_oil == 0){
							first_precise_oil = Long.parseLong(cols[35]);
						}
						
						if(cols[35] != null && !cols[35].equals("") && !cols[35].equals("0") && !cols[35].equals("-1")){
							last_precise_oil = Long.parseLong(cols[35]);
						}
						
						// 行车时间
						if( gpsTime != null && !gpsTime.equals("") && !isflag && gpsSpeed > 5 && binaryStus.endsWith("1")){ 
							isflag = true;
							startGpsTime = gpsTime;
							//startRunningOil = dayLastCostOil;
						}else {
							if(isflag){
								if( gpsTime != null && !gpsTime.equals("")  && gpsSpeed > 5 && binaryStus.endsWith("1")){
									long time = CDate.stringConvertUtc(gpsTime) - CDate.stringConvertUtc(startGpsTime);
									if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
										runningTime = runningTime + time;
									}
									startGpsTime = gpsTime;
//									if(startRunningOil != 0 && dayLastCostOil != 0){
//										long oil = dayLastCostOil - startRunningOil;
//										
//										//过滤突增数据
//										if(oil > 0l && oil <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue()))){
//											runningOil += oil;
//											
//										}
//									}
//									startRunningOil = dayLastCostOil; // 作为下一次开始点油耗
								}else{
									startGpsTime = "";
//									startRunningOil =0;
									isflag = false;
								}
							}
						}
						
						// 本日最大车速 转换之前速度
						int tempSpeed = Utils.getSpeed(spdFrom,cols); // 未转换后的速度
						if(tempSpeed > maxSpeed){
							maxSpeed = tempSpeed;
						}
						
						// 本日最大转速
						if(cols[13] != null && !cols[13].equals("") && !cols[13].equals("-1") && !cols[13].equals("0")){ 
							int tempRpm = Integer.parseInt(cols[13]);
							if(tempRpm > maxRpm){ //未转换后的
								maxRpm = tempRpm;
							}
						}
						
						// 记录当日开始行驶里程总数
						if(cols[10] != null && !cols[10].equals("") && !cols[10].equals("0") && !cols[10].equals("-1") && mileage == 0){ 
							mileage = Integer.parseInt(cols[10]);
						}
						
						if(cols[10] != null && !cols[10].equals("") && !cols[10].equals("0") && !cols[10].equals("-1")){ 
							tempMileage = Integer.parseInt(cols[10]);
						}
						
						if(cols[14] != null && !cols[14].equals("")  && !cols[14].equals("null")){
							number = MathUtils.getBinaryString(cols[14]);
							
							if(cols[14] != null && !cols[14].equals("")){						
								if(number.length() > 1) {							
									if(!check("1", number)){    // true定位，false为定位
										vehicleStatus.addCountGPSInvalid(1); // 定位无效数量
									} else {
										vehicleStatus.addCountGPSValid(1);   // 定位有效数量
									}							
								} else {
									vehicleStatus.addCountGPSInvalid(1); // 定位无效数量
								}
							}
						}
						
						if(!acc && cols[14] != null && !cols[14].equals("") && gpsTime != null && !gpsTime.equals("")){
							acc = true;
							accStatus = check("0", number);
							if(accStatus){
								accCount++;
								startAccGpsTime = gpsTime;
							}
						} else {
							if(acc && cols[14] != null && !cols[14].equals("")){  // 统计acc次数
								if(!accStatus && check("0", number)){      // acc开
									if(gpsTime != null && !gpsTime.equals("")){
										accCount++;
										startAccGpsTime = gpsTime;
									}
								} else if((accStatus && check("0", number)) || (accStatus && !check("0", number))){	//acc开门时长
									if(gpsTime != null && !gpsTime.equals("")){
										accTime = accTime + (CDate.stringConvertUtc(gpsTime) - CDate.stringConvertUtc(startAccGpsTime));
										startAccGpsTime = gpsTime;
									}							
								}
								accStatus = check("0", number);
							}
						}										
						
						co = cols[27];	
						column = co.split("\\|",3);	
						// 行驶时间不足和行驶时间过长
						if((!isRunningNotEnoughTime && !isRunningLongTime) && (cols[27] != null && !cols[27].equals("") && gpsTime != null && !gpsTime.equals(""))) {
							// 行驶时间不足
							if(!isRunningNotEnoughTime && column[2].equals("0")){
								event = new VehicleAlarmEvent();
								event.setAlarmCode(ExcConstants.RUNNINGNOTENONGHTIME);
								event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.RUNNINGNOTENONGHTIME)); 
								runCount++;
								isRunningNotEnoughTime = true;
							}
							
							// 行驶时间过长
							if(!isRunningLongTime && column[2].equals("1")){
								event = new VehicleAlarmEvent();
								runDiffCount++;
								event.setAlarmCode(ExcConstants.RUNNINGLONGTIME);
								event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.RUNNINGLONGTIME)); 
								isRunningLongTime = true;
							}
							
							if(event != null){
								if(event.getMaxSpeed() < Utils.getSpeed(spdFrom,cols)){
									event.setMaxSpeed((long)Utils.getSpeed(spdFrom,cols));
								}
								setAlarmEventStartInfo(cols,event);
							}
						} else if(isRunningNotEnoughTime || isRunningLongTime){
							if(event.getMaxSpeed() < Utils.getSpeed(spdFrom,cols)){
								event.setMaxSpeed((long)Utils.getSpeed(spdFrom,cols));
							}
							
							// 当不上报行驶不足附加信息或者在当天最后一条记录上报附加信息，则一次行驶不足报警结束
							if(isRunningNotEnoughTime && ((cols[27] == null || "".equals(cols[27])) || rowCount == currentRow)){ // 一次行驶不足结束
								isRunningNotEnoughTime = false;
								setAlarmEventEndInfo(cols,event);
								
								vAlarmEventList.add(event);
							}
							
							// 当不上报行驶过长附加信息或者在当天最后一条记录上报附加信息，则一次行驶过长报警结束
							if(isRunningLongTime && ((cols[27] == null || "".equals(cols[27])) || rowCount == currentRow)){ // 一次行驶时间过长
								isRunningLongTime = false;
								setAlarmEventEndInfo(cols,event);
								vAlarmEventList.add(event);
							}
						}
					 
						//发动机运行时长readTrackFile
						if(!isEngineFlag && cols[13] != null && !cols[13].equals("-1") && !cols[13].equals("") && Long.parseLong(cols[13]) * ExcConstants.RPMUNIT > 100  && gpsTime != null && !gpsTime.equals("") && binaryStus.endsWith("1")){
							startEngineDate = gpsTime;
							isEngineFlag =true;
						}else{
							if(isEngineFlag && !cols[13].equals("-1") && !cols[13].equals("")){
								if(Long.parseLong(cols[13]) * ExcConstants.RPMUNIT > 100 && gpsTime != null && !gpsTime.equals("") && binaryStus.endsWith("1")){
									long time = (CDate.stringConvertUtc(gpsTime) - CDate.stringConvertUtc(startEngineDate));
									if(time <=  ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
										engineTime = engineTime + time;
									}
									startEngineDate = gpsTime;
								}else{
									isEngineFlag = false;
									startEngineDate = "";
								}
							}
						}
						
						// 统计最近一次报警的时间
						String alarmCode = cols[6];
						long gpsUtc = CDate.stringConvertUtc(gpsTime);
						if(Utils.isContainAlarm(alarmCode, ",36,")){ // 制动蹄片磨损最近报警时间
							vehicleStatus.setLast_brake_shoe_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",34,")){ // 机油压力最近报警时间(油压报警)
							vehicleStatus.setLast_eoil_pressure_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",51,")){ // 冷却液温度最近报警时间
							vehicleStatus.setLast_e_water_temp_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",33,")){ // 制动气压最近报警时间
							vehicleStatus.setLast_trig_pressure_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",35,")){ //  水位低最近报警时间
							vehicleStatus.setLast_stage_low_alarm_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",38,")){ //  缓速器高温最近报警时间
							vehicleStatus.setLast_retarder_ht_alarm_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",39,")){ // 仓温最近报警时间
							vehicleStatus.setLast_ehouing_ht_alarm_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",52,")){ // 蓄电池电压低最近报警时间
							vehicleStatus.setLast_battery_voltage_time(gpsUtc);
						}
						
						if(Utils.isContainAlarm(alarmCode, ",41,")){ // 燃油滤清最近报警时间 (燃油堵塞信号)
							vehicleStatus.setLast_fuel_blocking_alarm_time(gpsUtc);
						}
						
						//缓存本次位置信息
						lastLocInfos = cols;
//						businessOutLine(cols);
						lastGpsTime = gpsTime;
				
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
			
//			if(outLineBean.getStartUtc() != -1){
//				long outLineTime = outLineBean.getEndUtc() - outLineBean.getStartUtc();
//				// 夜间2点到5点时间超过10分钟确定为非法营运
//				if(outLineTime > ExcConstants.ILLEGAL_OUTLINE_NIGHT_INTERVAL_TIME){
//					outLineBean.setAccountTime(outLineTime/1000);
//					outLineBean.setAlarmCode(ExcConstants.OUTLINE_CODE);
//					outLineBean.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OUTLINE_CODE));
//					vAlarmEventList.add(outLineBean);
//				}
//			}
			
			vehicleStatus.setEngineTime(engineTime/1000); // 当日发动机运行时长
			vehicleStatus.addPoint_milege(tempMileage - mileage); // 当日行驶里程数公里
			//vehicleStatus.setMileage(tempMileage - mileage); // 当日行驶里程数公里
			vehicleStatus.setAccTime(accTime/1000);
			vehicleStatus.setAccCount(accCount);
			vehicleStatus.setMaxRpm(maxRpm);
			vehicleStatus.setMaxSpeed(maxSpeed);
			vehicleStatus.addPoint_oil(dayLastCostOil - dayFirstCostOil); //计算当日油耗
			//vehicleStatus.setCostOil(dayLastCostOil - dayFirstCostOil);
			vehicleStatus.setIdlingTime((engineTime - runningTime)/1000);
			vehicleStatus.setGpsTimeInvild(count);
			vehicleStatus.setRunCount(runCount);			// 行驶时间不足次数
			vehicleStatus.setRunDiffCount(runDiffCount);	// 行驶时间过长次数
			//vehicleStatus.addRunningOil(runningOil); // 行车油耗
			vehicleStatus.setOpening_door_ex_num(opening_door_ex_num); // 开发异常次数
			vehicleStatus.setOnOffLine(onlineTime/1000);
			//vehicleStatus.setPrecise_oil(last_precise_oil - first_precise_oil);
		}catch(Exception ex){
			logger.error("Thread Id : "+ this.threadId + ",vid="+this.vid+" 分析轨迹文件过程中出错！"+ex.getMessage(),ex);
		}finally{
			if(statusMap != null && statusMap.size() > 0){
				statusMap.clear();
			}
			if(buf != null){
				buf.close();
			}
		}
	}
	
	/*****
	 * 计算车辆在线时间
	 */
	private long accountOffLineTime(String curTime,String lastTime){
		if("".equals(lastTime) || "".equals(curTime)){
			return 0;
		}
		
		long curUtc = CDate.stringConvertUtc(curTime);
		long lastUtc = CDate.stringConvertUtc(lastTime);
		if((curUtc - lastUtc) <= ExcConstants.PLATFORM_REPORT_DATA_LONGEST_INTERVAL_TIME ){
			return curUtc - lastUtc;
		}
		
		return 0;
	}
	
	/****
	 * 设置平台计算报警开始信息
	 * @param cols
	 */
	private void setAlarmEventStartInfo(String[] cols,VehicleAlarmEvent event){
		String spdFrom = cols[24];
		event.setStartUtc(CDate.stringConvertUtc(cols[2]));
		
		if(!Utils.checkEmpty(cols[9]))
			event.setStartElevation(Integer.parseInt(cols[9]));
		
		event.setStartGpsSpeed((long)Utils.getSpeed(spdFrom,cols));
		
		if(!Utils.checkEmpty(cols[4]))
			event.setStartHead(Integer.parseInt(cols[4]));
		
		event.setStartLon(Long.parseLong(cols[7]));
		event.setStartLat(Long.parseLong(cols[8]));
		event.setStartMapLat(Long.parseLong(cols[1]));
		event.setStartMapLon(Long.parseLong(cols[0]));
	}
	
	/****
	 * 设置平台计算报警结束信息
	 * @param cols
	 */
	private void setAlarmEventEndInfo(String[] cols,VehicleAlarmEvent event){
		String spdFrom = cols[24];
		event.setStartUtc(CDate.stringConvertUtc(cols[2]));
		
		if(!Utils.checkEmpty(cols[9]))
			event.setEndElevation(Integer.parseInt(cols[9]));
		
		event.setEndGpsSpeed((long)Utils.getSpeed(spdFrom,cols));
		
		if(!Utils.checkEmpty(cols[4]))
			event.setEndHead(Integer.parseInt(cols[4]));
		
		event.setEndLon(Long.parseLong(cols[7]));
		event.setEndLat(Long.parseLong(cols[8]));
		event.setEndMapLat(Long.parseLong(cols[1]));
		event.setEndMapLon(Long.parseLong(cols[0]));	
	}
	
	/**
	 * 统计报警次数，时间
	 * @param file
	 */
	private void readAlarmFile(String path){
		File file = new File(path);
		if(file.exists()){

			String tempVid = file.getName().replaceAll("\\.txt", "");
			Map<String,VehicleAlarm> alarmMap = null;
			
			/*while(true){
				try {*/
					alarmMap = StaPool.getAlarm(tempVid);
					
					/*if(alarmMap != null){
						break;
					}
					logger.info("Thread Id : "+ this.threadId + ",Waiting to alarm finished:" + tempVid);
					Thread.sleep(2 * 1000); // 等待报警处理结束
				} catch (InterruptedException e) {
					logger.error("Thread Id : "+ this.threadId + ",从报警列表中获取报警信息休眠出错.",e);
				}
			}// End while
			*/
			if(alarmMap != null && alarmMap.size() > 0){
				Set<String> s = alarmMap.keySet();
				Iterator<String> it = s.iterator();
				while(it.hasNext()){
					String alarmCode = it.next();
					vehicleStatus.getAlarmList().put(alarmCode, alarmMap.get(alarmCode));
				}// End while
				alarmMap.clear();
			}
		}
	}
	
	/**
	 * 查询车辆前一日超载次数统计信息
	 * @return
	 * @throws SQLException
	 */
	private int countOverLoad() throws SQLException{
		PreparedStatement stQueryOverLoadInfo = null;
		ResultSet rs = null;
		int count = 0;
		try{
			stQueryOverLoadInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryOverLoadInfo"));
			stQueryOverLoadInfo.setLong(1,CDate.getYesDayUTC());
			stQueryOverLoadInfo.setLong(2,CDate.getCurrentDayUTC());
			stQueryOverLoadInfo.setString(3, vid);
			rs = stQueryOverLoadInfo.executeQuery();
			if(rs.next()){
				count = rs.getInt("OVERLOADNUM");
			}
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ",超载次数统计信息出错。",e);
		}finally{
			if(rs != null){
				rs.close();
				rs = null;
			}
			
			if(stQueryOverLoadInfo != null){
				stQueryOverLoadInfo.close();
			}
		}
		return count;
	}
	
	/**
	 * 查询车辆报警总处理数统计信息
	 * @return
	 * @throws SQLException
	 */
	private int queryCountAlarmInfo() throws SQLException{
		PreparedStatement stQueryCountAlarmInfo = null;
		ResultSet rs = null;
		int count = 0;
		
		try{
			stQueryCountAlarmInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryCountAlarmInfo"));
			stQueryCountAlarmInfo.setString(1, vid);
			stQueryCountAlarmInfo.setLong(2,CDate.getYesDayUTC());
			stQueryCountAlarmInfo.setLong(3,CDate.getCurrentDayUTC());

			rs = stQueryCountAlarmInfo.executeQuery();
			if(rs.next()){
				count = rs.getInt("ALARMNUM");
			}
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ",报警总处理数统计信息",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryCountAlarmInfo != null){
				stQueryCountAlarmInfo.close();
			}
		}
		return count;
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
			logger.error("Thread Id : "+ this.threadId + ",读取轨迹文件信息出错",e);
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
	
	/**
	 * 判断二进制某位是否是1或0
	 * @param args
	 */
	private boolean check(String num, String result) {

		boolean bool = false;
		if (result.matches(".*0\\d{"+ num +"}")) { 
			bool = false;
		}
		if (result.matches(".*1\\d{"+ num +"}")) { 
			bool = true;
		}

		return bool;

	}
	
	/**
	 * 分析生成起步停车数据
	 * @param list
	 * @param cols
	 */
	//启动时间  发动机转速>100 车速<5
	//起步时间  发动机转速>100 车速>5
	//停止时间  发动机转速>100 车速<5
	//熄火时间  发动机转速<100 车速<5
	private void analysisStopstart2(List<StopstartBean> list,String cols[],boolean isLastRow) throws Exception {
		String gpsTime = cols[2]==null || "null".equals(cols[2])?"":cols[2].trim();
		String spdFrom = cols[24];	//车速来源
		int gpsSpeedStr = Utils.getSpeed(spdFrom,cols);
		Long lon =  Long.parseLong(cols[0]);
		Long lat = Long.parseLong(cols[1]);
		String mileageStr = cols[10]==null || "".equals(cols[10])?"-1":cols[10].trim();
		String oilStr = cols[11]==null || "".equals(cols[11])?"-1":cols[11].trim();
		String rorateSpeedStr = cols[13]==null || "".equals(cols[13])?"-1":cols[13].trim();
		String statusCode = MathUtils.getBinaryString(cols[14]==null || "".equals(cols[14]) || "null".equals(cols[14])?"0":cols[14].trim());
//		boolean startflag = false;
//		boolean stopflag = false;

		if (list==null) {list = new ArrayList<StopstartBean>();}
		
		if (list.size()==0){
			list.add(new StopstartBean());
		}
		int size = list.size();
		StopstartBean ssb = list.get(size-1);
		RunningBean rb = ssb.getRunninglist().get(ssb.getRunninglist().size()-1);
		
		double rorateSpeed = Double.parseDouble(rorateSpeedStr)*ExcConstants.RPMUNIT;
		double gpsSpeed = gpsSpeedStr*0.1;
		long roate = Long.valueOf(rorateSpeedStr);
		long speed = Long.valueOf(gpsSpeedStr);
		long mileage = Long.valueOf(mileageStr);
		long oil = Long.valueOf(oilStr);
		
		boolean isSerialRow = false;
		
		if (lastLocInfos!=null){
			String lastGpsTime = lastLocInfos[2]==null?"":lastLocInfos[2].trim();
			String lastStatusCode = MathUtils.getBinaryString(lastLocInfos[14]==null || "".equals(lastLocInfos[14]) || "null".equals(lastLocInfos[14])?"0":lastLocInfos[14].trim());
			long interruptedTime= (CDate.stringConvertUtc(gpsTime) - CDate.stringConvertUtc(lastGpsTime));//数据间隔时长
			if ((interruptedTime)< ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME || (!check("0",lastStatusCode)&&check("0",statusCode))){//如果两次数据上报间隔小于5分钟，或者上次记录为熄火本次为点火，则认为是连续记录
				isSerialRow = true;
			}
		}else{
			isSerialRow = true;
		}
		
		//如果汇报信息不连续，则结束本次记录的起步停车时间和运行时间，并添加新行
		if ((!isSerialRow && ssb.isLaunchFlag()) || (rorateSpeed <= 100 && rorateSpeed != -0.125 && ssb.isLaunchFlag())){
			if (!isSerialRow){
				list = funishCurrentStartstop(list,lastLocInfos);
				isSerialRow = true;
			}else{
				list = funishCurrentStartstop(list,cols);
			}
			
			list.add(new StopstartBean());
			size += 1;
			ssb = list.get(size-1);
			rb = ssb.getRunninglist().get(ssb.getRunninglist().size()-1);
		}
		
		//当gps时间有效、点火状态有效、并且gps时间和上次GPS时间差值小于20秒时进行起步停车统计
		if (!"".equals(gpsTime)&&check("0",statusCode)&&isSerialRow){
			//
			if (!ssb.isLaunchFlag()){
				if ((rorateSpeed>100&&gpsSpeed<5)||(rorateSpeed>100&&gpsSpeed>5)||(rorateSpeed<0&&gpsSpeed<5)){
					list.get(size-1).setLaunchTime(gpsTime);
					list.get(size-1).setLaunchFlag(true);
					list.get(size-1).setStartMileage(mileage);
					list.get(size-1).setStartOil(oil);
					list.get(size-1).setBeginLon(lon);
					list.get(size-1).setBeginLat(lat);
					
					ssb.setLaunchFlag(true);
				}
			}
			
			if (ssb.isLaunchFlag()&&!rb.isStartFlag()){
				//怠速下转速最大值
				if ((rorateSpeed>100&&gpsSpeed<5)||(rorateSpeed<0&&gpsSpeed<5)){
					if (ssb.getIdlingMaxRotateSpeed()<roate){
						list.get(size-1).setIdlingMaxRotateSpeed(roate);
					}
				}
				
				if ((rorateSpeed>100&&gpsSpeed>5)||(rorateSpeed<0&&gpsSpeed>5)){
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartTime(gpsTime);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartFlag(true);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartRunningMileage(mileage);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartRunningOil(oil);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setBeginLon(lon);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setBeginLat(lat);
					
					rb.setStartFlag(true);
				}
			}
			
			if (ssb.isLaunchFlag()&&rb.isStartFlag()&&!rb.isStopFlag()){
				if (gpsSpeed>5){
					if (rb.getMaxRotateSpeed()<roate){
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setMaxRotateSpeed(roate);
					}
					if (rb.getMaxSpeed()<speed){
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setMaxSpeed(speed);
					}
				}
				
				if (gpsSpeed<5){
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopTime(gpsTime);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopFlag(true);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningMileage(mileage);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningOil(oil);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLon(lon);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLat(lat);
					
					list.get(size-1).getRunninglist().add(new RunningBean());
					
					rb.setStopFlag(true);
				}
			}
			
			if (ssb.isLaunchFlag()&&rb.isStartFlag()&&rb.isStopFlag()&&!ssb.isFireoffFlag()){
				//怠速下转速最大值
				if ((rorateSpeed>100&&gpsSpeed<5)||(rorateSpeed<0&&gpsSpeed<5)){
					if (ssb.getIdlingMaxRotateSpeed()<roate){
						list.get(size-1).setIdlingMaxRotateSpeed(roate);
					}
				}

			}

		}
		
		if ((!check("0",statusCode)||isLastRow)&&ssb.isLaunchFlag()){
			list = funishCurrentStartstop(list,cols);
			if (!isLastRow){
				list.add(new StopstartBean());
			}
			
		}

	}
	
	/**
	 * 分析生成起步停车数据--根据上次记录和本次记录中车速、转速、点火状态之间的关系来判断车辆启动、起步、停止、熄火时间
	 * @param list
	 * @param cols
	 */
	//启动时间  发动机转速>100 车速<5
	//起步时间  发动机转速>100 车速>5
	//停止时间  发动机转速>100 车速<5
	//熄火时间  发动机转速<100 车速<5
	private Object formatValueByType(String str,String defaultVal,char type){
		Object obj = null;
		switch (type)	{
			case 'S': obj=((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'L': obj=Long.parseLong((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
		}
		return obj;
	}
	private void analysisStopstart(List<StopstartBean> list,String cols[],boolean isLastRow,String orgData) throws Exception {
		//当前值
		String gpsTime = (String)formatValueByType(cols[2],"",'S');
		String spdFrom = (String)formatValueByType(cols[24],"",'S');	//车速来源
		int gpsSpeed = Utils.getSpeed(spdFrom,cols);
		Long lon =  (Long)formatValueByType(cols[0],"0",'L');
		Long lat = (Long)formatValueByType(cols[1],"0",'L');
		Long mileage = (Long)formatValueByType(cols[10],"-1",'L');
		Long oil = (Long)formatValueByType(cols[11],"-1",'L');
		Long rorateSpeed = (Long)formatValueByType(cols[13],"-1",'L');
		String statusCode = MathUtils.getBinaryString((String)formatValueByType(cols[14],"0",'S'));
		Long preciseOil = (Long)formatValueByType(cols[35],"-1",'L');

		//备份值
		if (lastLocInfos==null){
			lastLocInfos = cols;
		}
		String lastGpsTime = (String)formatValueByType(lastLocInfos[2],"",'S');
		int lastGpsSpeed = Utils.getSpeed(spdFrom,lastLocInfos);
		//Long lastLon =  (Long)formatValueByType(lastLocInfos[0],"0",'L');
		//Long lastLat = (Long)formatValueByType(lastLocInfos[1],"0",'L');
		Long lastMileage = (Long)formatValueByType(lastLocInfos[10],"-1",'L');
		Long lastOil = (Long)formatValueByType(lastLocInfos[11],"-1",'L');
		Long lastRorateSpeed = (Long)formatValueByType(lastLocInfos[13],"-1",'L');
		//String lastStatusCode = MathUtils.getBinaryString((String)formatValueByType(lastLocInfos[14],"0",'S'));
		Long lastPreciseOil = (Long)formatValueByType(lastLocInfos[35],"-1",'L');
		
		long mg = 0;
		long tmpOil = 0;
		long tmpPreciseOil = 0;
		//处理里程油耗：排除里程油耗在内部为-1的情况
		if (mileage>-1){
			tmpLastMileage = mileage;
		}
		if (tmpLastMileage>-1){
			if (mileage == -1){
				mileage = tmpLastMileage;
			}
			if (lastMileage == -1){
				lastMileage = tmpLastMileage;
			}
		}
		
		if (oil>-1){
			tmpLastOil = oil;
		}
		if (tmpLastOil>-1){
			
			if (oil==-1){
				oil = tmpLastOil;
			}
			
			if (lastOil==-1){
				lastOil = tmpLastOil;
			}
			
		}
		
		if (preciseOil>-1){
			tmpLastPreciseOil = preciseOil;
		}
		if (tmpLastPreciseOil>-1){
			
			if (preciseOil==-1){
				preciseOil = tmpLastPreciseOil;
			}
			
			if (lastPreciseOil==-1){
				lastPreciseOil = tmpLastPreciseOil;
			}
			
		}
		
		//boolean startflag = false;
		//boolean stopflag = false;

		if (list==null) {list = new ArrayList<StopstartBean>();}
		
		if (list.size()==0){
			list.add(new StopstartBean());
		}
		int size = list.size();
		StopstartBean ssb = list.get(size-1);
		RunningBean rb = ssb.getRunninglist().get(ssb.getRunninglist().size()-1);
		
		//过滤突增数据
		mg = mileage - lastMileage;
		//过滤突增数据
		tmpOil = oil - lastOil;
		
		tmpPreciseOil = preciseOil - lastPreciseOil;
		
		/*****
		 * 过滤异常数据,包括里程和油耗,
		 * 里程为异常数据则本次里程负值为0,
		 */
		if(mg >= 0 && mg <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("mileage_interval").getValue()),10f)){
			// 不做处理
		}else{
			mg = 0; 
			// 记录过滤掉数据(里程突增突减)
			discardData.info("Mileage vid=" + vid + ":" + orgData);
		}
		
		if(tmpOil >= 0 && tmpOil <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue()),60f)){
			// 不做处理
		}else{
			// 油耗为异常数据则本次里程负值为0,
			tmpOil = 0;
			// 记录过滤掉数据(油耗突增突减)
			discardData.info("Oil vid=" + vid + ":" + orgData);
		}
		
		if(tmpPreciseOil >= 0 && tmpPreciseOil <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue())*50,60f)){
			// 不做处理
		}else{
			// 油耗为异常数据则本次里程负值为0,
			tmpPreciseOil = 0;
			// 记录过滤掉数据(油耗突增突减)
			discardData.info("MetOil vid=" + vid + ":" + orgData);
		}
		
		// 根据起步停车累加计算整日里程
		vehicleStatus.addMileage(mg);
		// 根据起步停车累加计算整日油耗
		//vehicleStatus.addCostOil(tmpOil);
		vehicleStatus.addEcuOil(tmpOil);
		vehicleStatus.addPrecise_oil(tmpPreciseOil);
		
		if (ssb.isLaunchFlag()){
			if (rb.isStartFlag()&&!rb.isStopFlag()){
				//ssb.getRunninglist().get(ssb.getRunninglist().size()-1).addRunningOil(tmpOil); //累加一次行车中每两点间油耗
				ssb.getRunninglist().get(ssb.getRunninglist().size()-1).addEcuRunningOil(tmpOil); //累加一次行车中每两点间油耗
				ssb.getRunninglist().get(ssb.getRunninglist().size()-1).addMetRunningOil(tmpPreciseOil); //累加一次行车中每两点间精准油耗
		    	ssb.getRunninglist().get(ssb.getRunninglist().size()-1).addRunningMileage(mg); //累加一次行车中每两点间油耗
		    	// 根据起步停车累加计算整日行车油耗
		    	//vehicleStatus.addRunningOil(tmpOil);
		    	vehicleStatus.addEcuRunningOil(tmpOil);
		    	vehicleStatus.addMetRunningOil(tmpPreciseOil);
			}
			//ssb.addUseOil(tmpOil); // 累加一次ACC开到ACC关中每两点间油耗
			ssb.addEcuOilWear(tmpOil);
			ssb.addMetOilWear(tmpPreciseOil);
			ssb.addMileage(mg); // 累加一次ACC开到ACC关中每两点间油耗
		}
		
		boolean isSerialRow = false;
		
		Long interruptedTime= (CDate.stringConvertUtc(gpsTime) - CDate.stringConvertUtc(lastGpsTime));//数据间隔时长
		if (interruptedTime < ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME ){//如果两次数据上报间隔小于5分钟，则认为是连续记录
			isSerialRow = true;
		}
		
		//如果汇报信息不连续，则结束本次记录的起步停车时间和运行时间，并添加新行
		if ((!isSerialRow && ssb.isLaunchFlag()) || (rorateSpeed <= 800 && lastRorateSpeed >800 && ssb.isLaunchFlag())|| (rorateSpeed ==-1&& gpsSpeed <= 50 && lastGpsSpeed > 50 && ssb.isLaunchFlag())){

			if (!isSerialRow){
				list = funishCurrentStartstop(list,lastLocInfos);
				isSerialRow = true;
			}else{
				list = funishCurrentStartstop(list,cols);
			}
			
			list.add(new StopstartBean());
			size += 1;
			ssb = list.get(size-1);
			rb = ssb.getRunninglist().get(ssb.getRunninglist().size()-1);
		}
		
		//当gps时间有效、点火状态有效、并且gps时间和上次GPS时间差值小于20秒时进行起步停车统计
		if (!"".equals(gpsTime)&&check("0",statusCode)&&isSerialRow){
			//如果上此发动机转速原始值大于800(实际转速大于100转/s) 
			if (!ssb.isLaunchFlag()){
				if (rorateSpeed>800){//有总线的车进入此方法设置发动机点火时信息
					list.get(size-1).setLaunchFlag(true);
					list.get(size-1).setLaunchTime(gpsTime);
					list.get(size-1).setStartMileage(mileage);
					list.get(size-1).setStartOil(oil);
					list.get(size-1).setBeginLon(lon);
					list.get(size-1).setBeginLat(lat);
					
					ssb.setLaunchFlag(true);
				}
			}
			
			if (!rb.isStartFlag()){//
				//车辆启动：本次源车速大于50(实际车速大于5Km/h) 并且 上次源车速小于等于50 时 
				//飘移排除：有总线的车同时判断发动机转速大于800,没总线的车没法进行过滤
				if (rorateSpeed>0){
					//有总线时能计算怠速下最大转速
					if (ssb.isLaunchFlag()){
						if (ssb.getIdlingMaxRotateSpeed()<rorateSpeed){
							list.get(size-1).setIdlingMaxRotateSpeed(rorateSpeed);
						}
					}
					
					if (gpsSpeed>50&&lastGpsSpeed<=50){
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartTime(gpsTime);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartFlag(true);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartRunningMileage(mileage);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartRunningOil(oil);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setBeginLon(lon);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setBeginLat(lat);
						rb.setStartFlag(true);
					}
				}else{
					if (gpsSpeed>50&&lastGpsSpeed<=50){
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartTime(gpsTime);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartFlag(true);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartRunningMileage(mileage);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStartRunningOil(oil);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setBeginLon(lon);
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setBeginLat(lat);
						rb.setStartFlag(true);
						
						//无总线车辆当车速第一次大于50时需给 发动机点火设置状态为点火
						if (!ssb.isLaunchFlag()){
							list.get(size-1).setLaunchFlag(true);
							list.get(size-1).setLaunchTime(gpsTime);
							list.get(size-1).setStartMileage(mileage);
							list.get(size-1).setStartOil(oil);
							list.get(size-1).setBeginLon(lon);
							list.get(size-1).setBeginLat(lat);
							
							ssb.setLaunchFlag(true);
						}
					}
				}
			}
			
			if (ssb.isLaunchFlag()&&rb.isStartFlag()&&!rb.isStopFlag()){
				if (gpsSpeed>50){
					if (rorateSpeed>0&&rb.getMaxRotateSpeed()<rorateSpeed){
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setMaxRotateSpeed(rorateSpeed);
					}
					if (rb.getMaxSpeed()<gpsSpeed){
						list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setMaxSpeed(gpsSpeed);
					}
				}
				
				//车辆停止：当第一次源速度由大于50变为小于50时
				if (gpsSpeed<=50&&lastGpsSpeed>50){
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopTime(gpsTime);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopFlag(true);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningMileage(mileage);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningOil(oil);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLon(lon);
					list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLat(lat);
					list.get(size-1).getRunninglist().add(new RunningBean());
					
					rb.setStopFlag(true);
				}
			}
			
			if (ssb.isLaunchFlag()&&rb.isStartFlag()&&rb.isStopFlag()&&!ssb.isFireoffFlag()){
				//怠速下转速最大值
				if (rorateSpeed>0){
					if (ssb.getIdlingMaxRotateSpeed()<rorateSpeed){
						list.get(size-1).setIdlingMaxRotateSpeed(rorateSpeed);
					}
				}
			}
		}
		
		//当车辆熄火或当前行为最后一行并且当前车辆点火未熄火时，结束起步停车本次起步停车分析
		if ((!check("0",statusCode)||isLastRow)&&ssb.isLaunchFlag()){
			list = funishCurrentStartstop(list,cols);
			//当为非最后一行时，添加起步停车对象，为下次统计做准备
			if (!isLastRow){
				list.add(new StopstartBean());
			}
		}

	}
	
	private List<StopstartBean> funishCurrentStartstop(List<StopstartBean> list,String[] endCols){
		
		String lastGpsTime = (String)formatValueByType(endCols[2],"",'S');
		//String spdFrom = (String)formatValueByType(endCols[24],"",'S');	//车速来源
		//int lastGpsSpeed = Utils.getSpeed(spdFrom,endCols);
		Long lastLon =  (Long)formatValueByType(endCols[0],"0",'L');
		Long lastLat = (Long)formatValueByType(endCols[1],"0",'L');
		Long lastMileage = (Long)formatValueByType(endCols[10],"-1",'L');
		Long lastOil = (Long)formatValueByType(endCols[11],"-1",'L');
		//Long lastRorateSpeed = (Long)formatValueByType(endCols[13],"-1",'L');
		//String lastStatusCode = MathUtils.getBinaryString((String)formatValueByType(endCols[14],"0",'S'));

		
		int size = list.size();
		StopstartBean ssb = list.get(size-1);
		RunningBean rb = ssb.getRunninglist().get(ssb.getRunninglist().size()-1);
		
		//判断车辆有无总线
		boolean isCan = true;
		if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
			isCan = false;
		}
		
		if (!rb.isStopFlag()&&rb.isStartFlag()){
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopTime(lastGpsTime);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopFlag(true);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningMileage(lastMileage);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningOil(lastOil);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLon(lastLon);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLat(lastLat);
		}else if (!rb.isStartFlag()&&!rb.isStopFlag()){
			int xx = list.get(size-1).getRunninglist().size();
			list.get(size-1).getRunninglist().remove(xx-1);
		}
		
		list.get(size-1).setFireoffFlag(true);
		if (!isCan){
			list.get(size-1).setFireoffTime(lastGpsTime);
			list.get(size-1).setEndMileage(lastMileage);
			list.get(size-1).setEndOil(lastOil);
			list.get(size-1).setBeginLon(lastLon);
			list.get(size-1).setBeginLat(lastLat);
		}else{
			list.get(size-1).setFireoffTime(lastGpsTime);
			list.get(size-1).setEndMileage(lastMileage);
			list.get(size-1).setEndOil(lastOil);
			list.get(size-1).setEndLon(lastLon);
			list.get(size-1).setEndLat(lastLat);
		}

		return list;
		
	}
	

private List<StopstartBean> funishCurrentStartstop2(List<StopstartBean> list,String[] endCols){
		
		String lastGpsTime = endCols[2]==null?"":endCols[2].trim();
		//String lastgpsSpeedStr = endCols[3]==null?"0":endCols[3].trim();
		Long lastlon =  Long.parseLong(endCols[0]);
		Long lastlat = Long.parseLong(endCols[1]);
		String lastmileageStr = endCols[10]==null || "".equals(endCols[10])?"-1":endCols[10].trim();
		String lastoilStr = endCols[11]==null || "".equals(endCols[11])?"-1":endCols[11].trim();
		String lastrorateSpeedStr = endCols[13]==null || "".equals(endCols[13])?"-1":endCols[13].trim();
		//String laststatusCode = MathUtils.getBinaryString(endCols[14]==null ||"".equals(endCols[14]) ||"null".equals(endCols[14])?"0":endCols[14].trim());
		
		double lastrorateSpeed = Double.parseDouble(lastrorateSpeedStr)*ExcConstants.RPMUNIT;
		
		int size = list.size();
		StopstartBean ssb = list.get(size-1);
		RunningBean rb = ssb.getRunninglist().get(ssb.getRunninglist().size()-1);
		
		if (!rb.isStopFlag()&&rb.isStartFlag()){
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopTime(lastGpsTime);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopFlag(true);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningMileage(Long.valueOf(lastmileageStr));
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setStopRunningOil(Long.valueOf(lastoilStr));
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLon(lastlon);
			list.get(size-1).getRunninglist().get(ssb.getRunninglist().size()-1).setEndLat(lastlat);
		}else if (!rb.isStartFlag()&&!rb.isStopFlag()){
			int xx = list.get(size-1).getRunninglist().size();
			list.get(size-1).getRunninglist().remove(xx-1);
		}
		
		list.get(size-1).setFireoffFlag(true);
		if (lastrorateSpeed<0){
			list.get(size-1).setFireoffTime("");
			//list.get(size-1).getRunninglist().remove(list.get(size-1).getRunninglist().size()-1);
			list.get(size-1).setEndMileage(Long.valueOf("0"));
			list.get(size-1).setEndOil(Long.valueOf("0"));
		}else{
			list.get(size-1).setFireoffTime(lastGpsTime);
			//list.get(size-1).getRunninglist().remove(list.get(size-1).getRunninglist().size()-1);
			list.get(size-1).setEndMileage(Long.valueOf(lastmileageStr));
			list.get(size-1).setEndOil(Long.valueOf(lastoilStr));
			list.get(size-1).setEndLon(lastlon);
			list.get(size-1).setEndLat(lastlat);
		}
		
		//ssb.setFireoffFlag(true);

		return list;
		
	}
	
	/**
	 * 分析生成车辆状态事件数据
	 * @param list
	 * @param cols
	 */
	private void analysisStateEvent(String cols[]) throws Exception{
		//MAP经度：MAP纬度：GPS时间：GPS 速度：正北方向夹角：车辆状态：报警编码：经度：纬度：海拔：里程：累计油耗：发动机运行总时长：引擎转速（发动机转速）：位置基本信息状态位：报区域/线路报警：冷却液温度：蓄电池电压：瞬时油耗：行驶记录仪速度(km/h)：机油压力：大气压力：发动机扭矩百分比：车辆信号状态：车速来源：系统时间
		//取出位置基本信息状态位和车辆信号状态
		String gpsTime = cols[2]==null?"":cols[2].trim();
		String basestatus = cols[14]== null || "".equals(cols[14])|| "null".equals(cols[14])?"0":cols[14].trim();
		String extendstatus = cols[23]==null || "".equals(cols[23])?"0":cols[23].trim();
		String basestatusCode = Utils.fillString(MathUtils.getBinaryString(basestatus),32);
		String extendstatusCode = Utils.fillString(MathUtils.getBinaryString(extendstatus),32);
	
		String spdFrom = cols[24];	//车速来源
		int speed = Utils.getSpeed(spdFrom,cols);
	
		boolean isSerialRow = false;
		
		if (lastLocInfos!=null){
			String lastGpsTime = lastLocInfos[2]==null?"":lastLocInfos[2].trim();
			long interruptedTime= (CDate.stringConvertUtc(gpsTime) - CDate.stringConvertUtc(lastGpsTime));//数据间隔时长
			if ((check("0",basestatusCode)&&(interruptedTime)< ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME) || (!check("0",basestatusCode)&&(interruptedTime)<(15*60*1000))){
				//如果点火状态下两次数据上报间隔小于5分钟，或熄火状态下两次数据上报间隔小于15分钟，则认为是连续记录
				isSerialRow = true;
			}
		}else{
			isSerialRow = true;
		}

		if (isSerialRow){
			String temp[] = cols.clone();
			for (int i=0;i<basestatusCode.length();i++){

				if (check(""+i,basestatusCode)){
					//状态开始或状态持续
					if (null == stateMap.get("BS"+String.format("%04d", i))){
						stateMap.put("BS"+String.format("%04d", i),cols);
					}
					if (maxSpeedMap.get("BS"+String.format("%04d", i))==null||(speed>(int)maxSpeedMap.get("BS"+String.format("%04d", i)))){
						maxSpeedMap.put("BS"+String.format("%04d", i),speed);
					}
					//中间点
					openningDoorStatus(temp,i,speed);
				}else{
					//状态结束
					openningDoorStatus(temp,i,speed);
					saveStateEventInfo("BS"+String.format("%04d", i),cols);
				}
			}// End while
		
			for (int i=0;i<extendstatusCode.length();i++){
				if (check(""+i,extendstatusCode)){
					//状态开始或状态持续
					if (stateMap.get("ES"+String.format("%04d", i))==null){
						stateMap.put("ES"+String.format("%04d", i),cols);
					}
					if (maxSpeedMap.get("ES"+String.format("%04d", i))==null||(speed>(int)maxSpeedMap.get("ES"+String.format("%04d", i)))){
						maxSpeedMap.put("ES"+String.format("%04d", i),speed);
					}
				}else{
					//状态结束
					saveStateEventInfo("ES"+String.format("%04d", i),cols);
				}
			}// End while
			
			
		}else{
			//时间有断点时结束全部未结束的状态
			Set<String> keycode = stateMap.keySet();
			Object[] its = keycode.toArray();
			
			for (int i=0;i<its.length;i++) {
				String key = (String)its[i];
				saveStateEventInfo(key,lastLocInfos);
			}// End while
		}
	}
	
	/******
	 * 判断带速开门、区域外开门、区域内开门
	 * 判断逻辑：
	 * 将一次的开始到结束过程中有带速开门的就标记本次为带速开门
	 * 内部协议定义：0带速开门；1区域外开门；2区域内开门；其他值保留
	 * 表字段定义：1正常开门 2带速开门 3区域内开门 4区域外开门
	 * @param cols
	 * @param i
	 */
	private void openningDoorStatus(String cols[],int i,int speed){
		// 获取开门事件
		if("BS0013".equals("BS"+String.format("%04d", i)) || "BS0014".equals("BS"+String.format("%04d", i)) || "BS0015".equals("BS"+String.format("%04d", i)) || "BS0016".equals("BS"+String.format("%04d", i))){
			//判断是否已经是带速开门、区域内开门、区域外开门
			String temp[] = stateMap.get("BS"+String.format("%04d", i));
			
			if(null != cols[33]){
				//判断0：带速开门；1区域外开门；2：区域内开门；其他值保留,带速开门：车速大于5公里/小时 时开门报警。
				if(null != temp && "0".equals(cols[33]) && speed > 50){
					stateMap.get("BS"+String.format("%04d", i))[33] = "2";
					stateMap.get("BS"+String.format("%04d", i))[33] = "2";
				}else if(null != temp && "2".equals(cols[33])){
					stateMap.get("BS"+String.format("%04d", i))[33] = "3";
				}else if(null != temp && "1".equals(cols[33])){
					
					stateMap.get("BS"+String.format("%04d", i))[33] = "4";
				}else if(null != temp && ( null == temp[33] || "".equals(temp[33]))){
					stateMap.get("BS"+String.format("%04d", i))[33] = "1";
				}
			}
		}
	}
	
	/**
	 * 存储状态事件统计信息
	 * @param alarmEventList
	 * @throws SQLException
	 */
	private void saveStateEventInfo(String key,String endCols[]) throws Exception{
			//取出状态起始点信息
			//MAP经度：MAP纬度：GPS时间：GPS 速度：正北方向夹角：车辆状态：报警编码：经度：纬度：海拔：里程：累计油耗：发动机运行总时长：
			//引擎转速（发动机转速）：位置基本信息状态位：报区域/线路报警：冷却液温度：蓄电池电压：瞬时油耗：行驶记录仪速度(km/h)：机油压力：大气压力：发动机扭矩百分比：车辆信号状态：车速来源：系统时间
			String beginCols[]=stateMap.get(key);
			
			if (beginCols!=null){
				String begin_time = beginCols[2];
				long begin_lon=Long.parseLong(beginCols[7]==null ||"".equals(beginCols[7])?"0":beginCols[7]);
				long begin_lat=Long.parseLong(beginCols[8]==null ||"".equals(beginCols[8])?"0":beginCols[8]);
				long begin_maplon=Long.parseLong(beginCols[0]==null ||"".equals(beginCols[0])?"0":beginCols[0]);
				long begin_maplat=Long.parseLong(beginCols[1]==null ||"".equals(beginCols[1])?"0":beginCols[1]);
				int begin_elevation=Integer.parseInt(beginCols[9]==null || "".equals(beginCols[9])?"0":beginCols[9]);
				int begin_direction=Integer.parseInt(beginCols[4]==null || "".equals(beginCols[4])?"0":beginCols[4]);
				String begin_spdFrom = beginCols[24];	//车速来源
				int begin_speed = Utils.getSpeed(begin_spdFrom,beginCols);
				long begin_mileage = Long.parseLong(beginCols[10]==null || "".equals(beginCols[10]) ?"-1":beginCols[10]);
				long begin_oil = Long.parseLong(beginCols[11]==null || "".equals(beginCols[11])?"-1":beginCols[11]);
				//取出状态结束点信息
				String end_time = endCols[2];
				long end_lon=Long.parseLong(endCols[7]==null ||"".equals(endCols[7])?"0":endCols[7]);
				long end_lat=Long.parseLong(endCols[8]==null ||"".equals(endCols[8])?"0":endCols[8]);
				long end_maplon=Long.parseLong(endCols[0]==null ||"".equals(endCols[0])?"0":endCols[0]);
				long end_maplat=Long.parseLong(endCols[1]==null ||"".equals(endCols[1])?"0":endCols[1]);
				int end_elevation=Integer.parseInt(endCols[9]==null || "".equals(endCols[9])?"0":endCols[9]);
				//========
				int end_direction=Integer.parseInt(endCols[4]==null || "".equals(endCols[4])?"0":endCols[4]);
				String end_spdFrom = endCols[24];	//车速来源
				int end_speed = Utils.getSpeed(end_spdFrom,endCols);
				long end_mileage = Long.parseLong(endCols[10]==null || "".equals(endCols[10])?"-1":endCols[10]);
				long end_oil = Long.parseLong(endCols[11]==null || "".equals(endCols[11])?"-1":endCols[11]);
				
				long mileage = 0; // 排除上报值为-1
				if(end_mileage > 0 && begin_mileage > 0){
					mileage = end_mileage-begin_mileage;
				}
				long oil = 0;// 排除上报值为-1
				if(end_oil > 0 && begin_oil > 0){
					oil = end_oil-begin_oil;
				}
				long time = (CDate.stringConvertUtc(end_time)-CDate.stringConvertUtc(begin_time))/1000;
				
				
				//添加成对象
				VehicleAlarmEvent event = new VehicleAlarmEvent();
				event.setAccountTime(time);
				event.setStartUtc(CDate.stringConvertUtc(begin_time));
				event.setStartLat(begin_lat);
				event.setStartLon(begin_lon);
				event.setStartMapLat(begin_maplat);
				event.setStartMapLon(begin_maplon);
				event.setStartHead(begin_direction);
				event.setStartElevation(begin_elevation);
				event.setAlarmCode(key);
				event.setStartGpsSpeed((long)begin_speed);
				event.setEndLat(end_lat);
				event.setEndLon(end_lon);
				event.setEndMapLat(end_maplat);
				event.setEndMapLon(end_maplon);
				event.setEndHead(end_direction);
				event.setEndElevation(end_elevation);
				event.setEndGpsSpeed((long)end_speed);
				event.setEndUtc(CDate.stringConvertUtc(end_time));
				event.setMaxSpeed((long)maxSpeedMap.get(key));
				if(mileage >0){
					event.setMileage((int)mileage);
				}
				
				if(oil > 0){
					event.setCostOil((int)oil);
				}
				event.setAlarmCode(key); // 临时存储门位
				event.setAlarmType(beginCols[27]); // 临时存储进出区域附加信息

				event.setOpenDoorType(beginCols[33]); 
				stateEventList.add(event);
				
				// 获取开门事件
				if("BS0013".equals(key) || "BS0014".equals(key) || "BS0015".equals(key) || "BS0016".equals(key)){
					//根据开始时间找时间差的上报图片
					String picDetail = getOpenningDoorPicture(event.getStartUtc());
					if(null != picDetail && !"".equals(picDetail)){
						String[] arr = picDetail.split(";");
						if(arr.length == 3){
							event.setMediaUrl(arr[1]);
							event.setMtypeCode(arr[2]);
						}
					}
					vAlarmEventList.add(event);
					openingDoorList.add(event);
				}
				stateMap.remove(key);
				maxSpeedMap.remove(key);
			}
	}
	
	private void saveStateEventInfo() throws SQLException{
		PreparedStatement stSaveStateEventInfo = null;
		try{
			//取出车辆当前关联的线路、驾驶员信息
			String vehicle_no = AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo();
			String corp_id = AnalysisDBAdapter.queryVechileInfo(vid).getEntId();
			String corp_name = AnalysisDBAdapter.queryVechileInfo(vid).getEntName();
			String team_id = AnalysisDBAdapter.queryVechileInfo(vid).getTeamId();
			String team_name = AnalysisDBAdapter.queryVechileInfo(vid).getTeamName();
			String vline_id = AnalysisDBAdapter.queryVechileInfo(vid).getVlineId();
			String vline_name = AnalysisDBAdapter.queryVechileInfo(vid).getLineName();
			
			stSaveStateEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveStateEventInfo"));
			Iterator<VehicleAlarmEvent> stateEventIt = stateEventList.iterator();
			while(stateEventIt.hasNext()){
				VehicleAlarmEvent stateEvent = stateEventIt.next();
				stSaveStateEventInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveStateEventInfo.setString(2, vid);
				stSaveStateEventInfo.setString(3, vehicle_no);
				stSaveStateEventInfo.setString(4, corp_id);
				stSaveStateEventInfo.setString(5, corp_name);
				
				stSaveStateEventInfo.setString(6, team_id);
				stSaveStateEventInfo.setString(7, team_name);
				
				if(vline_id != null && !"".equals(vline_id)){
					stSaveStateEventInfo.setString(8, vline_id);
				}else{
					stSaveStateEventInfo.setNull(8, Types.VARCHAR);
				}
				
				stSaveStateEventInfo.setString(9, vline_name);

				stSaveStateEventInfo.setString(10, stateEvent.getAlarmCode());
				stSaveStateEventInfo.setLong(11, stateEvent.getStartUtc());
				stSaveStateEventInfo.setLong(12,stateEvent.getStartLat());
				stSaveStateEventInfo.setLong(13, stateEvent.getStartLon());
				
				stSaveStateEventInfo.setLong(14, stateEvent.getStartMapLon());
				stSaveStateEventInfo.setLong(15, stateEvent.getStartMapLat());
				stSaveStateEventInfo.setLong(16, stateEvent.getStartElevation());
				stSaveStateEventInfo.setLong(17, stateEvent.getStartHead());
				stSaveStateEventInfo.setLong(18, stateEvent.getStartGpsSpeed());
				
				stSaveStateEventInfo.setLong(19, stateEvent.getEndUtc());
				stSaveStateEventInfo.setLong(20,stateEvent.getEndLon());
				stSaveStateEventInfo.setLong(21, stateEvent.getEndLat());
				stSaveStateEventInfo.setLong(22, stateEvent.getEndMapLon());
				stSaveStateEventInfo.setLong(23, stateEvent.getEndMapLat());
				
				stSaveStateEventInfo.setLong(24, stateEvent.getEndElevation());
				stSaveStateEventInfo.setLong(25, stateEvent.getEndHead());
				stSaveStateEventInfo.setLong(26, stateEvent.getEndGpsSpeed());
				
				stSaveStateEventInfo.setLong(27, stateEvent.getAccountTime());
				stSaveStateEventInfo.setLong(28, stateEvent.getMaxSpeed());

				stSaveStateEventInfo.setLong(29, stateEvent.getMileage());

				stSaveStateEventInfo.setLong(30, stateEvent.getCostOil());
				stSaveStateEventInfo.addBatch();
			} // End while
			stSaveStateEventInfo.executeBatch();
		}catch(Exception e){
			logger.error("Thread Id : "+ this.threadId + ",",e);
		}finally{
			if(stSaveStateEventInfo != null){
				stSaveStateEventInfo.close();
			}
			if(stateEventList.size() > 0 ){
				stateEventList.clear();
			}
		}
	}
	
	
	/**
	 * @description:车辆状态事件表 （每日凌晨统计上一日数据）
	 * @param:
	 * @modifyInformation：
	 */
	private void deleteStateEvent() throws SQLException{
		PreparedStatement stDeleteStateEventInfo = null;
		try{
			//清空原有状态数据
			stDeleteStateEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_deleteStateEventInfo"));
			stDeleteStateEventInfo.setString(1, vid);
			stDeleteStateEventInfo.executeUpdate();

		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ", 删除状态事件信息出错.",e);
		}finally{
			if(stDeleteStateEventInfo != null){
				stDeleteStateEventInfo.close();
			}
		}
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
	
	/****
	 * 统计开门次数，及区域内开门次数和时间。
	 */
	private void accountOpenningCount(){
		if(vAlarmEventList.size() > 0){
			Iterator<VehicleAlarmEvent> alarmEventIt = vAlarmEventList.iterator();
			while(alarmEventIt.hasNext()){
				VehicleAlarmEvent alarmEvent = alarmEventIt.next();
				if(alarmEvent.getAlarmCode().equals("BS0013")){
					vehicleStatus.addDoor1(1);
				}else if(alarmEvent.getAlarmCode().equals("BS0014")){
					vehicleStatus.addDoor2(1);
				}else if(alarmEvent.getAlarmCode().equals("BS0015")){
					vehicleStatus.addDoor3(1);
				}else if(alarmEvent.getAlarmCode().equals("BS0016")){
					vehicleStatus.addDoor4(1);
				}
				String enteringArea = alarmEvent.getAlarmType() == null ? "":alarmEvent.getAlarmType();
				String[] res = enteringArea.split("\\|",3);
				if(res.length == 3 && !"".equals(res[2]) && res[2].equals("0")){
					vehicleStatus.addAreaOpenDoorNum(1);
					vehicleStatus.addAreaOpenDoorTime(alarmEvent.getAccountTime());
					alarmEvent.setAlarmCode(ExcConstants.OPENINGDOOR);
				}
			}// End while
		} 
		openningDoorPicList.clear();
	}
	
	/*****
	 * 根据开门时间和照片上报时间，获取开门触发拍照UTL
	 * @param utc
	 * @return
	 */
	private String getOpenningDoorPicture(long utc){
		Iterator<String> picUrIt = openningDoorPicList.iterator();
		while(picUrIt.hasNext()){
			String str = picUrIt.next();
			String[] ky = str.split(";",3);
			Long picUtc = Long.parseLong(ky[0]);
			if(Math.abs(utc - picUtc) <= 10 * 60 * 1000){
				return str;
			}
		}// End while
		
		return "";
	}
	
	/*****
	 * 查询指定时间内开门或关门触发照片
	 * @throws SQLException
	 */
	private void selectOpenningDoorPic() throws SQLException{
		PreparedStatement stSelectOpenningDoorPic = null;
		ResultSet rs = null;
		 try {
			 stSelectOpenningDoorPic = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryOpenningDoorPicture"));
			 stSelectOpenningDoorPic.setString(1, vid);
			 stSelectOpenningDoorPic.setLong(2, this.utc);
			 stSelectOpenningDoorPic.setLong(3, this.utc + 24 * 60 * 60 * 1000);
			 rs = stSelectOpenningDoorPic.executeQuery();
			 while(rs.next()){
				 openningDoorPicList.add(rs.getLong("UTC") + ";" +  rs.getString("MEDIA_URI") + ";" + rs.getString("MTYPE_CODE"));
			 }// End while
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			if(rs != null){
				rs.close();
			}
			if(stSelectOpenningDoorPic != null){
				stSelectOpenningDoorPic.close();
			}
		}
	}
	
	/**
	 * 存储非法营运事件统计信息
	 * @param alarmEventList
	 * @throws SQLException
	 */
	private void saveOutLineEventInfo() throws SQLException{
		
		PreparedStatement stSaveVehicleOutLineEventInfo = null;
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			stSaveVehicleOutLineEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineEventInfo"));
			
			Iterator<VehicleAlarmEvent> alarmEventIt = vAlarmEventList.iterator();
			
			while(alarmEventIt.hasNext()){
				VehicleAlarmEvent event = alarmEventIt.next();
				String alarmCode =  event.getAlarmCode();
				
				if(!AnalysisDBAdapter.alarmTypeMap.containsKey(alarmCode)){
					continue;
				}
				
				stSaveVehicleOutLineEventInfo.setString(1, vid);
				stSaveVehicleOutLineEventInfo.setString(2, info.getCommaddr());
				stSaveVehicleOutLineEventInfo.setString(3, event.getAlarmCode());
				
				if(event.getAREA_ID() != null && !"".equals(event.getAREA_ID())){
					stSaveVehicleOutLineEventInfo.setString(4, event.getAREA_ID());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(4, Types.VARCHAR);
				}
				
				if(event.getMtypeCode() != null){
					stSaveVehicleOutLineEventInfo.setString(5, event.getMtypeCode());
				}else{
					stSaveVehicleOutLineEventInfo.setString(5, null);
				}
				
				if(event.getMediaUrl() != null){
					stSaveVehicleOutLineEventInfo.setString(6, event.getMediaUrl());
				}else{
					stSaveVehicleOutLineEventInfo.setString(6, null);
				}
				
				if(event.getStartUtc() != -1){
					stSaveVehicleOutLineEventInfo.setLong(7, event.getStartUtc());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(7, 0);
				}
				
				if(event.getStartLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(8,event.getStartLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(8,0);
				}
				
				if( event.getStartLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(9, event.getStartLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(9, 0);
				}
				
				if(event.getStartMapLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(10, event.getStartMapLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(10, 0);
				}
				
				if(event.getStartMapLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(11, event.getStartMapLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(11, 0);
				}
				
				if(event.getStartElevation() != -1){
					stSaveVehicleOutLineEventInfo.setInt(12, event.getStartElevation());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(12, 0);
				}
				
				if(event.getStartHead() != -1){
					stSaveVehicleOutLineEventInfo.setInt(13, event.getStartHead());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(13, 0);
				}
				
				if(event.getStartGpsSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(14, event.getStartGpsSpeed());
				}
				
				if( event.getEndUtc() != -1){
					stSaveVehicleOutLineEventInfo.setLong(15, event.getEndUtc());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(15, 0);
				}
				
				if(event.getEndLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(16,event.getEndLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(16,0);
				}
				
				if(event.getEndLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(17, event.getEndLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(17, 0);
				}
				
				if(event.getEndMapLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(18, event.getEndMapLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(18, 0);
				}
				
				if(event.getEndMapLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(19, event.getEndMapLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(19, 0);
				}
				
				if(event.getEndElevation() != -1){
					stSaveVehicleOutLineEventInfo.setInt(20, event.getEndElevation());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(20, 0);
				}
				
				if( event.getEndHead() != -1){
					stSaveVehicleOutLineEventInfo.setInt(21, event.getEndHead());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(21, 0);
				}
				
				if(event.getEndGpsSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(22, event.getEndGpsSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(22, 0);
				}
				
				if(event.getAccountTime() != -1){
					stSaveVehicleOutLineEventInfo.setDouble(23, event.getAccountTime());
				}else{
					stSaveVehicleOutLineEventInfo.setDouble(23, event.getAccountTime());
				}
				
				if(event.getMaxSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(24, event.getMaxSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(24, 0);
				}
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineEventInfo.setString(25, info.getVlineId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(25, Types.VARCHAR);
				}
				stSaveVehicleOutLineEventInfo.setString(26, info.getInnerCode());
				stSaveVehicleOutLineEventInfo.setString(27, info.getVehicleNo());
				stSaveVehicleOutLineEventInfo.setString(28, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineEventInfo.setString(29, info.getLineName());
				}else{
					stSaveVehicleOutLineEventInfo.setString(29, null);
				}
				
				stSaveVehicleOutLineEventInfo.setString(30, info.getEntId());
				stSaveVehicleOutLineEventInfo.setString(31, info.getEntName());
				stSaveVehicleOutLineEventInfo.setString(32, info.getTeamId());
				stSaveVehicleOutLineEventInfo.setString(33, info.getTeamName());
				stSaveVehicleOutLineEventInfo.executeUpdate();
			}// End while
			
			//stSaveVehicleOutLineEventInfo.executeBatch();
				
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ","+info.getVehicleNo() + " 存储非法营运事件统计信息出错.",e);
		}finally{
			vAlarmEventList.clear();
			if(stSaveVehicleOutLineEventInfo != null){
				stSaveVehicleOutLineEventInfo.close();
			}
		}
	}
	
	/**
	 * 存储日统计非法营运信息
	 * @param info
	 * @param alarmMap
	 * @param vid
	 * @throws SQLException
	 */
	/**
	 * @throws SQLException
	 */
	private void saveOutLineInfo() throws SQLException{
		PreparedStatement stSaveVehicleOutLineInfo = null;
		PreparedStatement stSelectOutlineEvent = null;
		
		ResultSet rs = null;
		
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			stSaveVehicleOutLineInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineInfo"));
			stSelectOutlineEvent = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_selectOutLineEvent"));
			stSelectOutlineEvent.setString(1, vid);
			stSelectOutlineEvent.setLong(2, this.utc);
			stSelectOutlineEvent.setLong(3, this.utc + 24*60*60*1000);
			rs = stSelectOutlineEvent.executeQuery();
			while(rs.next()){
				stSaveVehicleOutLineInfo.setLong(1, this.utc + 12 * 60 * 60 * 1000);
				stSaveVehicleOutLineInfo.setString(2, vid);
				stSaveVehicleOutLineInfo.setString(3, info.getEntId());
				stSaveVehicleOutLineInfo.setString(4, info.getEntName());
				stSaveVehicleOutLineInfo.setString(5, info.getTeamId());
				stSaveVehicleOutLineInfo.setString(6, info.getTeamName());
				stSaveVehicleOutLineInfo.setString(7, info.getVehicleNo());
				stSaveVehicleOutLineInfo.setString(8,info.getVinCode());
				String outLine_code = rs.getString("OUTLINE_CODE");
				stSaveVehicleOutLineInfo.setString(9,outLine_code);
				stSaveVehicleOutLineInfo.setInt(10, rs.getInt("NUM"));
				stSaveVehicleOutLineInfo.setString(11, AnalysisDBAdapter.alarmTypeMap.get(outLine_code));
				stSaveVehicleOutLineInfo.setLong(12, rs.getLong("TIME"));
				if(info.getVlineId() != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineInfo.setString(13, info.getVlineId());
				}else{
					stSaveVehicleOutLineInfo.setNull(13, Types.VARCHAR);
				}
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineInfo.setString(14, info.getLineName());
				}else{
					stSaveVehicleOutLineInfo.setString(14, null);
				}
				stSaveVehicleOutLineInfo.addBatch();
			}
		
			stSaveVehicleOutLineInfo.executeBatch();
			
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ","+vid + " 存储日非法营运统计信息出错",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stSelectOutlineEvent != null){
				stSelectOutlineEvent.close();
			}
			
			if(stSaveVehicleOutLineInfo != null){
				stSaveVehicleOutLineInfo.close();
			}
		}
	}
	
	/*****
	 * 汇总统计超员
	 * @throws SQLException
	 */
	private void saveVehicleIsOverLoad() throws SQLException{
		PreparedStatement stSaveVehicleOutLineEventInfo = null;
		PreparedStatement stQueryVehicleMedia = null;
		ResultSet rs = null;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stQueryVehicleMedia = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryVehicleIsOverLoad"));
			stQueryVehicleMedia.setString(1, vid);
			stQueryVehicleMedia.setLong(2, this.utc);
			stQueryVehicleMedia.setLong(3, this.utc + 24 * 60 * 60 * 1000);
			rs = stQueryVehicleMedia.executeQuery();
			stSaveVehicleOutLineEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineEventInfo"));
			while(rs.next()){

				stSaveVehicleOutLineEventInfo.setString(1, vid);
				stSaveVehicleOutLineEventInfo.setString(2, info.getCommaddr());
				stSaveVehicleOutLineEventInfo.setString(3, ExcConstants.OVERLOAD_CODE);
				
				if(rs.getString("MULT_MEDIA_ID") != null){
					stSaveVehicleOutLineEventInfo.setString(4, rs.getString("MULT_MEDIA_ID") );
				}else{
					stSaveVehicleOutLineEventInfo.setString(4, null);
				}
				
				if(rs.getString("MTYPE_CODE") != null){
					stSaveVehicleOutLineEventInfo.setString(5, rs.getString("MTYPE_CODE"));
				}else{
					stSaveVehicleOutLineEventInfo.setString(5, null);
				}
				
				if(rs.getString("MEDIA_URI") != null){
					stSaveVehicleOutLineEventInfo.setString(6, rs.getString("MEDIA_URI"));
				}else{
					stSaveVehicleOutLineEventInfo.setString(6, null);
				}
				
				stSaveVehicleOutLineEventInfo.setLong(7, rs.getLong("UTC"));
				stSaveVehicleOutLineEventInfo.setLong(8,rs.getLong("LAT"));
				stSaveVehicleOutLineEventInfo.setLong(9, rs.getLong("LON"));
				stSaveVehicleOutLineEventInfo.setLong(10, rs.getLong("MAPLAT"));
				stSaveVehicleOutLineEventInfo.setLong(11, rs.getLong("MAPLON"));
				stSaveVehicleOutLineEventInfo.setInt(12, rs.getInt("ELEVATION"));
				stSaveVehicleOutLineEventInfo.setInt(13, rs.getInt("DIRECTION"));
				stSaveVehicleOutLineEventInfo.setInt(14, rs.getInt("GPS_SPEED"));
				stSaveVehicleOutLineEventInfo.setLong(15, rs.getLong("UTC"));
				stSaveVehicleOutLineEventInfo.setLong(16,rs.getLong("LAT"));
				stSaveVehicleOutLineEventInfo.setLong(17, rs.getLong("LON"));
				stSaveVehicleOutLineEventInfo.setLong(18, rs.getLong("MAPLAT"));
				stSaveVehicleOutLineEventInfo.setLong(19, rs.getLong("MAPLON"));
				stSaveVehicleOutLineEventInfo.setInt(20, rs.getInt("ELEVATION"));
				stSaveVehicleOutLineEventInfo.setInt(21, rs.getInt("DIRECTION"));
				stSaveVehicleOutLineEventInfo.setInt(22, rs.getInt("GPS_SPEED"));
				stSaveVehicleOutLineEventInfo.setDouble(23, 0);
				stSaveVehicleOutLineEventInfo.setInt(24, rs.getInt("GPS_SPEED"));
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineEventInfo.setString(25, info.getVlineId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(25, Types.VARCHAR);
				}
				stSaveVehicleOutLineEventInfo.setString(26, info.getInnerCode());
				stSaveVehicleOutLineEventInfo.setString(27, info.getVehicleNo());
				stSaveVehicleOutLineEventInfo.setString(28, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineEventInfo.setString(29, info.getLineName());
				}else{
					stSaveVehicleOutLineEventInfo.setString(29, null);
				}
				
				stSaveVehicleOutLineEventInfo.setString(30, info.getEntId());
				stSaveVehicleOutLineEventInfo.setString(31, info.getEntName());
				stSaveVehicleOutLineEventInfo.setString(32, info.getTeamId());
				stSaveVehicleOutLineEventInfo.setString(33, info.getTeamName());
				stSaveVehicleOutLineEventInfo.addBatch();
			}
			stSaveVehicleOutLineEventInfo.executeBatch();
			
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ","+vid + " 存储日非法营运统计信息出错",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryVehicleMedia != null){
				stQueryVehicleMedia.close();
			}
			
			if(stSaveVehicleOutLineEventInfo != null){
				stSaveVehicleOutLineEventInfo.close();
			}
		}
	}
	
	/****
	 * 存储开门详细信息
	 * @throws SQLException 
	 */
	private void saveOpenningDoorDetail() throws SQLException{
		PreparedStatement stSaveOpenningDoor = null;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveOpenningDoor = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_insertOpenDoorDetails"));
			Iterator<VehicleAlarmEvent> openningDoorIt = openingDoorList.iterator();
			int count =0;
			while(openningDoorIt.hasNext()){
				VehicleAlarmEvent event = openningDoorIt.next();
				String alarmCode =  event.getAlarmCode();
				count++;
				stSaveOpenningDoor.setString(1, vid);
				stSaveOpenningDoor.setString(2, info.getVehicleNo());
				stSaveOpenningDoor.setString(3, info.getVinCode());
				stSaveOpenningDoor.setString(4, info.getInnerCode());
				stSaveOpenningDoor.setString(5, info.getEntId());
				stSaveOpenningDoor.setString(6, info.getEntName());
				stSaveOpenningDoor.setString(7, info.getTeamId());
				stSaveOpenningDoor.setString(8, info.getTeamName());
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveOpenningDoor.setString(9, info.getVlineId());
				}else{
					stSaveOpenningDoor.setNull(9, Types.VARCHAR);
				}
				if(info.getLineName() != null){
					stSaveOpenningDoor.setString(10, info.getLineName());
				}else{
					stSaveOpenningDoor.setString(10, null);
				}
	
				stSaveOpenningDoor.setNull(11, Types.INTEGER);
				stSaveOpenningDoor.setString(12, null);
				if(event.getAREA_ID() != null && !"".equals(event.getAREA_ID())){
					stSaveOpenningDoor.setString(13, event.getAREA_ID());
				}else{
					stSaveOpenningDoor.setNull(13, Types.VARCHAR);
				}
				
				if(event.getMtypeCode() != null){
					stSaveOpenningDoor.setString(14, event.getMtypeCode());
				}else{
					stSaveOpenningDoor.setString(14, null);
				}
				
				if(event.getMediaUrl() != null){
					stSaveOpenningDoor.setString(15, event.getMediaUrl());
				}else{
					stSaveOpenningDoor.setString(15, null);
				}
				
				if("BS0013".equals(alarmCode)){
					stSaveOpenningDoor.setString(16, "1");
				}else if("BS0014".equals(alarmCode)){
					stSaveOpenningDoor.setString(16, "2");
				}else if("BS0015".equals(alarmCode)){
					stSaveOpenningDoor.setString(16, "3");
				}else if("BS0016".equals(alarmCode)||"BS0017".equals(alarmCode)){
					stSaveOpenningDoor.setString(16, "4");
				}
								
				stSaveOpenningDoor.setString(17, event.getOpenDoorType());
				
				if(event.getStartUtc() != -1){
					stSaveOpenningDoor.setLong(18, event.getStartUtc());
				}else{
					stSaveOpenningDoor.setLong(18, 0);
				}
				
				if(event.getStartLat() != -1){
					stSaveOpenningDoor.setLong(19,event.getStartLat());
				}else{
					stSaveOpenningDoor.setLong(19,0);
				}
				
				if( event.getStartLon() != -1){
					stSaveOpenningDoor.setLong(20, event.getStartLon());
				}else{
					stSaveOpenningDoor.setLong(20, 0);
				}
				
				if(event.getStartMapLon() != -1){
					stSaveOpenningDoor.setLong(21, event.getStartMapLon());
				}else{
					stSaveOpenningDoor.setLong(21, 0);
				}
				
				if(event.getStartMapLat() != -1){
					stSaveOpenningDoor.setLong(22, event.getStartMapLat());
				}else{
					stSaveOpenningDoor.setLong(22, 0);
				}
				if(event.getStartElevation() != -1){
					stSaveOpenningDoor.setInt(23, event.getStartElevation());
				}else{
					stSaveOpenningDoor.setInt(23, 0);
				}
				
				if(event.getStartHead() != -1){
					stSaveOpenningDoor.setInt(24, event.getStartHead());
				}else{
					stSaveOpenningDoor.setInt(24, 0);
				}
				
				if(event.getStartGpsSpeed() != -1){
					stSaveOpenningDoor.setLong(25, event.getStartGpsSpeed());
				}else{
					stSaveOpenningDoor.setLong(25, 0);
				}
				
				if( event.getEndUtc() != -1){
					stSaveOpenningDoor.setLong(26, event.getEndUtc());
				}else{
					stSaveOpenningDoor.setLong(26, 0);
				}
				
				if(event.getEndLat() != -1){
					stSaveOpenningDoor.setLong(27,event.getEndLat());
				}else{
					stSaveOpenningDoor.setLong(27,0);
				}
				
				if(event.getEndLon() != -1){
					stSaveOpenningDoor.setLong(28, event.getEndLon());
				}else{
					stSaveOpenningDoor.setLong(28, 0);
				}

				if(event.getEndMapLon() != -1){
					stSaveOpenningDoor.setLong(29, event.getEndMapLon());
				}else{
					stSaveOpenningDoor.setLong(29, 0);
				}
				
				if(event.getEndMapLat() != -1){
					stSaveOpenningDoor.setLong(30, event.getEndMapLat());
				}else{
					stSaveOpenningDoor.setLong(30, 0);
				}
				
				if(event.getEndElevation() != -1){
					stSaveOpenningDoor.setInt(31, event.getEndElevation());
				}else{
					stSaveOpenningDoor.setInt(31, 0);
				}
				
				if( event.getEndHead() != -1){
					stSaveOpenningDoor.setInt(32, event.getEndHead());
				}else{
					stSaveOpenningDoor.setInt(32, 0);
				}
				
				if(event.getEndGpsSpeed() != -1){
					stSaveOpenningDoor.setLong(33, event.getEndGpsSpeed());
				}else{
					stSaveOpenningDoor.setLong(33, 0);
				}
				stSaveOpenningDoor.setDouble(34, event.getAccountTime());
				
				if(event.getMaxSpeed() != -1){
					stSaveOpenningDoor.setLong(35, event.getMaxSpeed());
				}else{
					stSaveOpenningDoor.setLong(35, 0);
				}
				
				stSaveOpenningDoor.setLong(36, event.getMileage());
				
				stSaveOpenningDoor.setInt(37, event.getCostOil());
				
				stSaveOpenningDoor.addBatch();
				if(count % 100 == 0){
					stSaveOpenningDoor.executeBatch();
					stSaveOpenningDoor.clearBatch();
					count = 0;
				}
			}// End while
			if(count > 0 ){
				stSaveOpenningDoor.executeBatch();
			}
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ",");
		}finally{
			if(stSaveOpenningDoor != null){
				stSaveOpenningDoor.close();
			}
			if(openingDoorList.size() > 0){
				openingDoorList.clear();
			}
		}
	}
	
	
	/****
	 * 存储车辆开门运营违规日统计
	 * @throws SQLException 
	 */
	
	private void saveOpenningDoorDay(){
		PreparedStatement stSaveOpenningDoor = null;
		PreparedStatement stSelectOpenningDoorDay = null;
		ResultSet rs = null;
		
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveOpenningDoor = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOpenningDoorDay"));
			stSelectOpenningDoorDay = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_selectOpenningDoorDay"));
			
			stSelectOpenningDoorDay.setString(1, vid);
			stSelectOpenningDoorDay.setLong(2, this.utc);
			stSelectOpenningDoorDay.setLong(3, this.utc + 24 * 60 * 60 * 1000);
			rs = stSelectOpenningDoorDay.executeQuery();
			int count = 0;
			while(rs.next()){
				stSaveOpenningDoor.setLong(1, this.utc + 12 * 60 * 60 * 1000);
				stSaveOpenningDoor.setString(2,vid);
				stSaveOpenningDoor.setString(3,info.getEntId());
				stSaveOpenningDoor.setString(4, info.getEntName());
				stSaveOpenningDoor.setString(5, info.getTeamId());
				stSaveOpenningDoor.setString(6, info.getTeamName());
				stSaveOpenningDoor.setString(7, info.getVehicleNo());
				stSaveOpenningDoor.setString(8, info.getVinCode());
				stSaveOpenningDoor.setInt(9,rs.getInt("NUM"));
				stSaveOpenningDoor.setInt(10,rs.getInt("TIME"));
				String openType = rs.getString("OPENDOOR_TYPE");
				if("2".equals(openType)){ //带速开门 
					stSaveOpenningDoor.setString(11,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.IDELINGOPENINGDOOR));
					stSaveOpenningDoor.setString(12,ExcConstants.IDELINGOPENINGDOOR);
				}else if("3".equals(openType)){ // 区域内开门
					stSaveOpenningDoor.setString(11,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OPENINGDOOR));
					stSaveOpenningDoor.setString(12,ExcConstants.OPENINGDOOR);
				}else if("4".equals(openType)){ // 区域外开门
					stSaveOpenningDoor.setString(11,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OUTOPENINGDOOR));
					stSaveOpenningDoor.setString(12,ExcConstants.OUTOPENINGDOOR);
				}else{
					stSaveOpenningDoor.setString(11,null);
					stSaveOpenningDoor.setString(12,null);
				}
				stSaveOpenningDoor.setInt(13,rs.getInt("MILEAGE"));
				stSaveOpenningDoor.setInt(14,rs.getInt("OIL_WEAR"));
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveOpenningDoor.setString(15, info.getVlineId());
				}else{
					stSaveOpenningDoor.setNull(15, Types.VARCHAR);
				}
				
				if(info.getLineName() != null){
					stSaveOpenningDoor.setString(16, info.getLineName());
				}else{
					stSaveOpenningDoor.setString(16, null);
				}
				
				stSaveOpenningDoor.addBatch();
				count++;
			}//End while
			
			if(count > 0){
				stSaveOpenningDoor.executeBatch();
			}
		} catch (SQLException e) {
			logger.error("Thread Id : "+ this.threadId + ",存储车辆开门运营违规日统计",e);
		}finally{
			try {
				if(rs != null){
					rs.close();
				}
				
				if(stSelectOpenningDoorDay != null){
					stSelectOpenningDoorDay.close();
				}
				
				if(stSaveOpenningDoor != null){
					stSaveOpenningDoor.close();
				}
			} catch (SQLException e) {
				logger.error(e.getMessage(), e);
			}
		}
	}
	
	/***
	 * 计算油箱油量监控数据
	 * @param cols
	 */
/*	private void checkOilMonitor(String[] cols){
		String charGpsTime = cols[2];
		long gpsTime = CDate.stringConvertUtc(charGpsTime);
		
		//判断偷油点 and 判断加油点
		checkAddAndStealingOil(cols);
		
		//记录正常消耗量
		if(oilFileMonitorBean.getAccOpenOilQeque().size() > 0 && !oilFileMonitorBean.isAccClose()){
			// 比较时间是否在一分钟间隔,如果再取掉最大和最小值，求平均值
			if(Math.abs(oilFileMonitorBean.getAccOpenOilQeque().getFirst().getUtc() - gpsTime)/1000 > Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("costingOil_time_interval").getValue())){
				oilmassChangedDetail = oilFileMonitorBean.getAccOpenOilPoint();
				
				if(null != oilmassChangedDetail ){ 
					if(oilmassChangedDetail.getCurr_oilmass() < lastOil){// 比较是否比前一个点油量值小，如果小则标记该点值为上一个点值，否则该点值延用上一个点油量值
						lastOil = oilmassChangedDetail.getCurr_oilmass();
					}else{
						oilmassChangedDetail.setCurr_oilmass(lastOil);
					}
					
					oilmassChangedDetail.setCurr_oilmass(oilmassChangedDetail.getCurr_oilmass() * 20); // 1L = 20 bit
					oilFileQuen.addLast(oilmassChangedDetail);
					oilFileMonitorBean.resetValue();
				}
			}
		}
	}*/
	
	/*****
	 * 判断加油和偷油
	 * @param cols
	 */
	/*private void checkAddAndStealingOil(String[] cols){
		String spdFrom = cols[24];	//车速来源
		int gpsSpeedStr = Utils.getSpeed(spdFrom,cols);
		int oilBox = Integer.parseInt(Utils.checkEmpty(cols[25]) ? "0":cols[25]);
		String charGpsTime = cols[2];
		long gpsTime = CDate.stringConvertUtc(charGpsTime);
		Long lon =  Long.parseLong(cols[7]);
		Long lat = Long.parseLong(cols[8]);
		Long mapLon =  Long.parseLong(cols[0]);
		Long mapLat = Long.parseLong(cols[1]);
		
		//判断加油点
		if(oilMonitorBean == null){
			oilMonitorBean = new OilMonitorBean();
		}

		if(oilBox >0 && oilBox != 32760){ // 记录熄火之前3分钟数据 去除特殊数字32760
			 OilmassChangedDetail oil = new OilmassChangedDetail();
			 oil.setDirection(Integer.parseInt(Utils.checkEmpty(cols[4]) ? "0":cols[4]));
			 oil.setElevation(Integer.parseInt(Utils.checkEmpty(cols[9]) ? "0":cols[9]));
			 oil.setGps_speed(gpsSpeedStr);
			 oil.setUtc(gpsTime);
			 oil.setLat(lat);
			 oil.setLon(lon);
			 oil.setMapLat(mapLat);
			 oil.setMapLon(mapLon);
			 oil.setChangeType("00");
			 oil.setCurr_oilmass(oilBox);
			 oil.setGpsTime(charGpsTime);
			 
			oilMonitorBean.getAccCloseOilQeque().addLast(oil);
			 
			 if(Math.abs(oilMonitorBean.getAccCloseOilQeque().getFirst().getUtc() - gpsTime)/1000 > Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("costingOil_time_interval").getValue())){ // check是否在连续的点并且在3分钟之内, 如果大于3分钟，移除第一个点
				 
				if(oilMonitorBean.getAccOpenOilQeque().size() > 0){
					OilmassChangedDetail accClose = oilMonitorBean.getCloseOpenOilPoint();
					 int oilMass = 0;//accClose.getCurr_oilmass() - oilmassChangedDetail.getCurr_oilmass()/20;
					 
					 String change_type = "00";
					 
					 if( oilMass >= Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("addingOil_interval").getValue()) ){ // 判断加油
					 	 change_type = "10";
					 }else if(oilMass < -Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("stealingOil_interval").getValue())){ //判断偷油
						 change_type = "01";
					 }
					
					 if(!"00".equals(change_type)){
						 boolean flag = false;
						if("01".equals(change_type) && lastOil > accClose.getCurr_oilmass()){
							flag = true;
						}else if("10".equals(change_type) && lastOil < accClose.getCurr_oilmass()){
							flag = true;
						}
						
						if(flag){
							lastOil = 0;//accClose.getCurr_oilmass();
							OilmassChangedDetail asOil = new OilmassChangedDetail();
							asOil.setChange_oilmass(Math.abs(oilMass) * 20); // 1L = 20 bit
							asOil.setCurr_oilmass(accClose.getCurr_oilmass() * 20); // 1L = 20 bit
							asOil.setChangeType(change_type);
							asOil.setGps_speed(accClose.getGps_speed());
							asOil.setDirection(accClose.getDirection());
							asOil.setElevation(accClose.getElevation());
							asOil.setLat(accClose.getLat());
							asOil.setLon(accClose.getLon());
							asOil.setMapLat(accClose.getLat());
							asOil.setMapLon(accClose.getLon());
							asOil.setGpsTime(accClose.getGpsTime());
							oilList.add(asOil);
							oilFileQuen.addLast(asOil);
							oilFileMonitorBean.setAccClose(true);
						}else{
							oilFileMonitorBean.setAccClose(false);
						}
					 }
				}
				oilMonitorBean.cloneAccOpenDeque(oilMonitorBean.getAccCloseOilQeque());
				oilFileMonitorBean.cloneAccOpenDeque(oilMonitorBean.getAccCloseOilQeque());
				oilMonitorBean.getAccCloseOilQeque().clear();
			 }
		}
	}
	*/
//	/*****
//	 * 判断加油和偷油
//	 * @param cols
//	 */
//	private void checkAddAndStealingOil(String[] cols){
//		String spdFrom = cols[24];	//车速来源
//		int gpsSpeedStr = Utils.getSpeed(spdFrom,cols);
//		String statusCode = MathUtils.getBinaryString((String)formatValueByType(cols[14],"0",'S'));
//		int oilBox = Integer.parseInt(Utils.checkEmpty(cols[25]) ? "0":cols[25]);
//		String charGpsTime = cols[2];
//		long gpsTime = CDate.stringConvertUtc(charGpsTime);
//		Long lon =  Long.parseLong(cols[7]);
//		Long lat = Long.parseLong(cols[8]);
//		Long mapLon =  Long.parseLong(cols[0]);
//		Long mapLat = Long.parseLong(cols[1]);
//		
//		//判断加油点
//		if(oilMonitorBean == null){
//			oilMonitorBean = new OilMonitorBean();
//		}
//
//		if(check("0",statusCode) && oilBox >0 && !oilMonitorBean.isAccClose()){ // 记录熄火之前3分钟数据
//			 OilmassChangedDetail oil = new OilmassChangedDetail();
//			 oil.setDirection(Integer.parseInt(Utils.checkEmpty(cols[4]) ? "0":cols[4]));
//			 oil.setElevation(Integer.parseInt(Utils.checkEmpty(cols[9]) ? "0":cols[9]));
//			 oil.setGps_speed(gpsSpeedStr);
//			 oil.setUtc(gpsTime);
//			 oil.setLat(lat);
//			 oil.setLon(lon);
//			 oil.setMapLat(mapLat);
//			 oil.setMapLon(mapLon);
//			 oil.setOil(oilBox);
//			 oil.setGpsTime(charGpsTime);
//			 oilMonitorBean.getAccCloseOilQeque().addLast(oil);
//			 
//			 if(Math.abs(oilMonitorBean.getAccCloseOilQeque().getFirst().getUtc() - gpsTime)/1000 > Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("addOrStealingOil_time_interval").getValue())){ // check是否在连续的点并且在3分钟之内, 如果大于3分钟，移除第一个点
//				 oilMonitorBean.getAccCloseOilQeque().removeFirst();
//			 }
//		}else if(!check("0",statusCode)){// 从点火持续到熄火状态时，停止记录熄火之前3分钟数据，等待记录下一次点火后三分钟数据
//			if(oilMonitorBean.getAccCloseOilQeque().size() > 0)
//			oilMonitorBean.setAccClose(true);
//		}else if(check("0",statusCode) && oilBox >0 && oilMonitorBean.isAccClose()){
//			
//			 OilmassChangedDetail oil = new OilmassChangedDetail();
//			 oil.setDirection(Integer.parseInt(Utils.checkEmpty(cols[4]) ? "0":cols[4]));
//			 oil.setElevation(Integer.parseInt(Utils.checkEmpty(cols[9]) ? "0":cols[9]));
//			 oil.setGps_speed(gpsSpeedStr);
//			 oil.setUtc(gpsTime);
//			 oil.setLat(lat);
//			 oil.setLon(lon);
//			 oil.setMapLat(mapLat);
//			 oil.setMapLon(mapLon);
//			 oil.setOil(oilBox);
//			 oil.setGpsTime(charGpsTime);
//			 oilMonitorBean.getAccOpenOilQeque().addLast(oil);
//			 if(Math.abs(oilMonitorBean.getAccOpenOilQeque().getFirst().getUtc() - gpsTime)/1000 > Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("addOrStealingOil_time_interval").getValue())){ // check是否在连续的点并且在3分钟之内,计算一次从点火到熄火再到点火，是否加油或者偷油
//				 int oilMass = (oilMonitorBean.getAccOpenAvgOil() - oilMonitorBean.getAccCloseAvgOil()) /10;
//				 String change_type = "00";
//				 
//				 if( oilMass >= Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("addingOil_interval").getValue())){ // 判断加油
//				 	 change_type = "10";
//				 }else if(oilMass < -Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("stealingOil_interval").getValue())){ //判断偷油
//					 change_type = "01";
//				 }
//				
//				 if(!"00".equals(change_type)){
//					lastOil = oilMonitorBean.getAccOpenAvgOil()/10;
//					OilmassChangedDetail asOil = new OilmassChangedDetail();
//					asOil.setChange_oilmass(Math.abs(oilMass) * 20); // 1L = 20 bit
//					asOil.setCurr_oilmass((oilMonitorBean.getAccOpenAvgOil()/10) * 20); // 1L = 20 bit
//					asOil.setChangeType(change_type);
//					asOil.setGps_speed(oilMonitorBean.getAccOpenOilQeque().getLast().getGps_speed());
//					asOil.setDirection(oilMonitorBean.getAccOpenOilQeque().getLast().getDirection());
//					asOil.setElevation(oilMonitorBean.getAccOpenOilQeque().getLast().getElevation());
//					asOil.setLat(oilMonitorBean.getAccOpenOilQeque().getLast().getLat());
//					asOil.setLon(oilMonitorBean.getAccOpenOilQeque().getLast().getLon());
//					asOil.setMapLat(oilMonitorBean.getAccOpenOilQeque().getLast().getLat());
//					asOil.setMapLon(oilMonitorBean.getAccOpenOilQeque().getLast().getLon());
//					asOil.setGpsTime(oilMonitorBean.getAccOpenOilQeque().getLast().getGpsTime());
//					oilList.add(asOil);
//					oilFileQuen.addLast(asOil);
//				 }
//				 oilMonitorBean.resetValue();
//			 }
//		}
//	}
	
	/*****
	 * 存储数据库油量变化记录
	 */
	private void saveOilChangedMass(){
		if(oilMonitorBean2!=null&&oilMonitorBean2.getOilMonitor_ls().size()>0){
			PreparedStatement stSaveOilChangedMass = null;
			try {
				stSaveOilChangedMass = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOilChanged"));
				List<OilmassChangedDetail> oilIt = oilMonitorBean2.getOilMonitor_ls();
				for (int i=0;i<oilIt.size();i++){
					OilmassChangedDetail oil = oilIt.get(i);
					if (!"00".equals(oil.getChangeType())){
						stSaveOilChangedMass.setString(1, oil.getChangeType());
						stSaveOilChangedMass.setString(2, vid);
						stSaveOilChangedMass.setLong(3, CDate.stringConvertUtc(oil.getGpsTime()));
						stSaveOilChangedMass.setLong(4, oil.getLat());
						stSaveOilChangedMass.setLong(5, oil.getLon());
						stSaveOilChangedMass.setLong(6, oil.getMapLon());
						stSaveOilChangedMass.setLong(7, oil.getMapLat());
						stSaveOilChangedMass.setInt(8, oil.getElevation());
						stSaveOilChangedMass.setInt(9, oil.getDirection());
						stSaveOilChangedMass.setInt(10, oil.getGps_speed());
						stSaveOilChangedMass.setLong(11, System.currentTimeMillis());
						stSaveOilChangedMass.setInt(12, Types.NULL);
						stSaveOilChangedMass.setDouble(13, Math.round(oil.getCurr_oilmass()*2));//进行单位换算 0.1 = 0.05 *2
						stSaveOilChangedMass.setDouble(14, Math.round(oil.getChange_oilmass()*2));//进行单位换算
						stSaveOilChangedMass.addBatch();
					}
				}// End while
				stSaveOilChangedMass.executeBatch();
			} catch (Exception e) {
				logger.error("Thread Id : "+ this.threadId + ",存储数据库油量变化记录VID=" + vid,e );
			}finally{
				if(null != stSaveOilChangedMass ){
					try {
						stSaveOilChangedMass.close();
					} catch (SQLException e) {
						logger.error(e.getMessage(), e);
					}
				}
			}
			
		}
	}
	
	/****
	 * 存储文件油量变化记录
	 */
	private void writeOilChangedMassFile(){
		if(oilMonitorBean2!=null&&oilMonitorBean2.getOilMonitor_ls().size()>0 && oilFlag){
			FileWriter fw = null;
			StringBuffer buf = new StringBuffer();
			try {
				fw = new FileWriter(oilFile);
				List<OilmassChangedDetail> ls = oilMonitorBean2.getOilMonitor_ls();
				long lastOil = 0;
				for (int i=0;i<ls.size();i++){
					OilmassChangedDetail oil = ls.get(i);
					//" + oil.getChangeType() + "
					long tmpOil = Math.round(oil.getCurr_oilmass()*2);
					//if (tmpOil!=lastOil){
					buf.append(oil.getLat() + ":" + oil.getLon() + ":" + oil.getElevation() + ":" + oil.getGps_speed() + ":" + oil.getDirection() + ":" + oil.getGpsTime().replaceAll("/", "").substring(2) + ":"+oil.getChangeType()+"::" + Math.round(oil.getChange_oilmass()*2) + ":" + tmpOil + "\r\n");
					//}
					lastOil = tmpOil;
				}// End while
				fw.write(buf.toString());
				fw.flush();
			} catch (IOException e) {
				logger.error("Thread Id : "+ this.threadId + ",存储文件油量变化记录VID=" + vid,e);
			}finally{
				if(null != fw){
					try {
						fw.close();
					} catch (IOException e) {
						logger.error(e.getMessage(), e);
					}
				}
			}
			
		}
	}
	/*private void writeOilChangedMassFile(){
		if(oilFileQuen.size() > 0 && oilFlag){
			FileWriter fw = null;
			StringBuffer buf = new StringBuffer();
			try {
				fw = new FileWriter(oilFile);
				Iterator<OilmassChangedDetail> oilIt = oilFileQuen.iterator();
				while(oilIt.hasNext()){
					OilmassChangedDetail oil = oilIt.next();
					buf.append(oil.getLat() + ":" + oil.getLon() + ":" + oil.getElevation() + ":" + oil.getGps_speed() + ":" + oil.getDirection() + ":" + oil.getGpsTime().replaceAll("/", "").substring(2) + ":" + oil.getChangeType() + "::" + oil.getChange_oilmass() + ":" + oil.getCurr_oilmass() + "\r\n");
					
				}// End while
				fw.write(buf.toString());
				fw.flush();
			} catch (IOException e) {
				logger.error("存储文件油量变化记录VID=" + vid,e);
			}finally{
				if(null != fw){
					try {
						fw.close();
					} catch (IOException e) {
						logger.error(e);
					}
				}
			}
			
		}
	}*/
	
	/*****
	 * GPS巡检记录
	 */
	private void saveGpsInspection(){
		if(inspectionRecorder!=null&&inspectionRecorder.size()>0){
			PreparedStatement stGPSInspection = null;
			try {
				stGPSInspection = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveGpsInspection"));
				
				for (int i=0;i<inspectionRecorder.size();i++){
					VehicleMessageBean po = inspectionRecorder.get(i);

					stGPSInspection.setString(1, UUID.randomUUID().toString().replace("-", ""));
					stGPSInspection.setString(2, vid);
					stGPSInspection.setLong(3, po.getUtc());
					stGPSInspection.setLong(4,po.getSpeed());
					stGPSInspection.setInt(5,check("1",Integer.toBinaryString(Integer.parseInt(po.getBaseStatus())))?1:0);
					stGPSInspection.setLong(6, po.getLat());
					stGPSInspection.setLong(7, po.getLon());
					stGPSInspection.setLong(8, po.getMaplon());
					stGPSInspection.setLong(9, po.getMaplat());
					stGPSInspection.setString(10, GetAddressUtil.getAddress((po.getMaplon()/600000.0)+"" , (po.getMaplat()/600000.0) + ""));
					stGPSInspection.addBatch();

				}// End while
				stGPSInspection.executeBatch();
			} catch (Exception e) {
				logger.error("Thread Id : "+ this.threadId + ",存储数据库油量变化记录VID=" + vid,e );
			}finally{
				if(null != stGPSInspection ){
					try {
						stGPSInspection.close();
					} catch (SQLException e) {
						logger.error(e.getMessage(), e);
					}
				}
			}
			
		}
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
	
	/**
	 * 同步车辆告警事件数据
	 * 偷油
	 * @param alarmEventList
	 * @throws SQLException
	 * @author: cuis
	 * @modifyInformation：去掉带分区名查询
	 */
	private void syncAlarmEvent(String vid) throws SQLException{
		
		PreparedStatement stSyncAlarmEventInfo = null;
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			String tmpsql = SQLPool.getinstance().getSql("sql_syncAlarmEventSql");
			//String partitionName = "TH_VEHICLE_ALARM"+CDate.utc2Str(this.utc + 24 * 60 * 60 * 1000, "yyyyMMdd");
			//tmpsql = tmpsql.replaceAll("PARTITIONNAME", partitionName);

			stSyncAlarmEventInfo = dbCon.prepareStatement(tmpsql);
			
			//stSyncAlarmEventInfo.setString(1, "TH_VEHICLE_ALARM"+CDate.utc2Str(this.utc + 24 * 60 * 60 * 1000, "yyyyMMdd"));
			logger.info("Thread Id : "+ this.threadId + ",统计数据开始日期"+CDate.utc2Str(this.utc, "yyyyMMdd")+"告警表分区名称："+"TH_VEHICLE_ALARM"+CDate.utc2Str(this.utc + 24 * 60 * 60 * 1000, "yyyyMMdd"));
			stSyncAlarmEventInfo.setString(1, vid);
			stSyncAlarmEventInfo.setLong(2, this.utc);
			stSyncAlarmEventInfo.setLong(3, this.utc + 24 * 60 * 60 * 1000);
			
			stSyncAlarmEventInfo.executeUpdate();
				
		}catch(SQLException e){
			logger.error("Thread Id : "+ this.threadId + ","+info.getVehicleNo() + " 从告警表中同步告警事件信息出错.",e);
		}finally{
			if(stSyncAlarmEventInfo != null){
				stSyncAlarmEventInfo.close();
			}
		}
	}
	
	private void generalGpsInspectionRecorder(String col[]){
		if (inspectionRecorder==null){
			inspectionRecorder = new ArrayList<VehicleMessageBean>();
		}
		if (inspectionResult==null){
			inspectionResult = new String[3];
		}
		VehicleMessageBean vmb = this.changTxtVMB(col);

		long sysTime = System.currentTimeMillis();
		
		// 判断该点所属巡检区间是否已经巡检成功
		//取出当前时段
		
		int hour = CDate.getOnedayHour(vmb.getUtc());
		//判断当前时段是否巡检成功 0--12 12--18 18--14
		boolean flag = false;
		if (hour>=0&&hour<12){
			if ("1".equals(inspectionResult[0])){
				flag = true;
			}
		}else if (hour>=12&&hour<18){
			if ("1".equals(inspectionResult[1])){
				flag = true;
			}
		}else if (hour>=18&&hour<=24){
			if ("1".equals(inspectionResult[2])){
				flag = true;
			}
		}
		
		if (!flag){
			if (vmb.getMaplat() > 0 && vmb.getMaplon() > 0){
				inspectionRecorder.add(vmb);
				
				if (hour>=0&&hour<12){
					inspectionResult[0]="1";
					
				}else if (hour>=12&&hour<18){
					inspectionResult[1]="1";
				}else if (hour>=18&&hour<=24){
					inspectionResult[2]="1";
				}
			}
		}
	}
	
	private VehicleMessageBean changTxtVMB(String[] track){
		
		VehicleMessageBean po = new VehicleMessageBean();
		
		po.setCommanddr("");
		po.setMaplon(Long.parseLong(track[0]));
		po.setMaplat(Long.parseLong(track[1]));
		String speedForm = track[24];
		Long spd = 0L;
		if ("0".equals(speedForm)){
			if(track[19] != null && !track[19].equals("")){
				spd = Long.parseLong((null == track[19] || "".equals(track[19]))?"0":track[19]);
			}
		}else{
			if(track[3] != null && !track[3].equals("")){
				spd = Long.parseLong((null == track[3] || "".equals(track[3]))?"0":track[3]); // 1：来自GPS
			}
		}
		po.setSpeed(spd);
		po.setBaseStatus(track[14]);
		po.setLon(Long.parseLong(track[7]));
		po.setLat(Long.parseLong(track[8]));
		po.setUtc(CDate.stringConvertUtc(track[2]));
		po.setAlarmcode(track[6]);
		
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
	 * 保存速度异常数据记录
	 * INSERT INTO TH_VEHICLE_SPEED_ANOMALOUS
	 * (STAT_DATE,VID,VEHICLE_NO,VSS_SPEED_AVG,GPS_SPEED_AVG,SPEED_DIFFERENCE,SPEED_FROM)
	 * VALUES ( ?, ?, ?, ?, ?, ?, ?)
	 */
	private void saveSpeedAnomalous(){
			PreparedStatement stSpeedAnomalous = null;
			try {
				//判断vss、gps差值是否符合要求
				long vss = thVehicleSpeedAnomalous.getVssSpeedAvg();
				long gps = thVehicleSpeedAnomalous.getGpsSpeedAvg();
				long diff = Math.abs(gps - vss);
				if (diff>100){
					stSpeedAnomalous = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_vehicleSpeedAnomalous"));
					stSpeedAnomalous.setLong(1, this.utc + 12 * 60 * 60 * 1000);
					stSpeedAnomalous.setString(2, ""+vid);
					if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
						stSpeedAnomalous.setString(3, ""+AnalysisDBAdapter.queryVechileInfo(vid).getEntId()); // 车牌号
					} else {
						stSpeedAnomalous.setNull(3, Types.NULL); // 车牌号
					}
					if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
						stSpeedAnomalous.setString(4, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo()); // 车牌号
					} else {
						stSpeedAnomalous.setNull(4, Types.NULL); // 车牌号
					}
					stSpeedAnomalous.setLong(5, vss);
					stSpeedAnomalous.setLong(6,gps);
					stSpeedAnomalous.setLong(7,diff);
					stSpeedAnomalous.setString(8, thVehicleSpeedAnomalous.getSpeedForm());
					stSpeedAnomalous.executeUpdate();
				}

			} catch (Exception e) {
				logger.error("Thread Id : "+ this.threadId + ",存储车辆速度异常记录VID=" + vid,e );
			}finally{
				if(null != stSpeedAnomalous ){
					try {
						stSpeedAnomalous.close();
					} catch (SQLException e) {
						logger.error(e.getMessage(), e);
					}
				}
			}
	}
	
}
