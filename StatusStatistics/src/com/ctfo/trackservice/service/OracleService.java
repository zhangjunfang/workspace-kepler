package com.ctfo.trackservice.service;

import java.io.File;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.model.AirTempertureBean;
import com.ctfo.trackservice.model.CoolLiquidtemBean;
import com.ctfo.trackservice.model.DriverDetailBean;
import com.ctfo.trackservice.model.GasPressureBean;
import com.ctfo.trackservice.model.OilPressureBean;
import com.ctfo.trackservice.model.OilSaveBean;
import com.ctfo.trackservice.model.OilWearBean;
import com.ctfo.trackservice.model.OilmassChangedDetail;
import com.ctfo.trackservice.model.RotateSpeedDay;
import com.ctfo.trackservice.model.SpeeddistDay;
import com.ctfo.trackservice.model.VehicleInfo;
import com.ctfo.trackservice.model.VehicleMessageBean;
import com.ctfo.trackservice.model.VehicleStatus;
import com.ctfo.trackservice.model.VoltagedistDay;
import com.ctfo.trackservice.util.ConfigLoader;

/**
 * 文件名：OracleService.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-25上午9:45:20
 * 
 */
public class OracleService {
	private  final Logger logger = LoggerFactory.getLogger(OracleService.class);
	
	/**	查询车辆信息SQL	*/
	private static  String sql_queryVehicleInfo;
	/**	更新车辆状态SQL	*/
	private static  String sql_saveDayServiceStInfo;
	/**	更新车辆运行起步停车SQL	*/
	private static String sql_saveStopstartInfo;	
	/**	更新油耗油量SQL	*/
	private static String sql_saveOilDayStat;
	/**	初始化配置油耗油量SQL	*/
	private static String sql_vehicleConfigOilMonitor;	
	/**更新节油驾驶SQL	*/
	private static String sql_saveOilDriver;
	/**查询节油驾驶SQL*/
	private static String sql_queryOilDayInfo;
	/**节油驾驶月统计*/
	private static String sql_saveOilMonthInfo;
	/**更新油量监控变化详情表SQL	*/
	private static String sql_saveOilChanged;
	/**车辆状态月统计*/
	private static String sql_saveMonthServiceStInfo;
	/**车辆状态月统计*/
	private static String sql_queryStatDayInfo;
	/**删除车辆状态月统计SQL*/
	private static String sql_delRestoreMonthInfo;
	/**删除节能驾驶月统计SQL*/
	private static String sql_delOilMonthInfo;
	/**查询驾驶员信息SQL*/
	private static String sql_queryDriverClockinDetail;
	
	/** 补录数据删除SQL*/
	private static String sql_deleteVehicleInfo;
	/** 补录数据删除SQL*/
	private static String sql_deleteDriverInfo;
	/** 保存驾驶员月统计删除SQL*/
	private static String sql_saveMonthDriverDetailInfo;
	/** 查询驾驶员日统计信息SQL*/
	private static String sql_queryDriverDetailInfo;
	/** 删除驾驶员月统计信息SQL*/
	private static String sql_delRestoreDriverMonthInfo;
	/** 获取每天上报里程*/
	private static String sql_queryTotalMileageInfo;
	/** 更新总里程 */
	private static String sql_updateStaInfo;	
	/**	保存驾驶员明细SQL*/
	private static String sql_saveDriverDetailInfo;
	/** 保存行驶里程SQL*/
	private static String sql_saveDayMileageStInfo;
	
	private static String sql_saveGasPressureDayStat;
	private static String sql_saveOilPressureDayStat;
	private static String sql_saveCoolLiquidtemDayStat;
	private static String sql_saveSpeeddistDayStat;
	private static String sql_saveRotateDayStat;
	private static String sql_saveVoltageDayStat;
	private static String sql_saveAirTemperture;
	/** 车辆运行统计数据删除SQL*/
	private static String sql_deleteOilSaveInfo;
	private static String sql_deleteStopStartInfo;
	private static String sql_deleteRunningInfo;
	/** 油箱油量监控删除SQL*/
	private static String sql_deleteOilWearInfo;
	//private static String sql_deleteOilChangedInfo;
	/** 单车分析报告删除SQL*/
	private static String sql_deleteReportInfo;
	
	/** 加载传感器油耗SQL*/
	private static String sql_loadingOilMap;
	
	

	/**
	 * 初始化SQL脚本
	 */
	public static void init(){
		sql_saveDayServiceStInfo = ConfigLoader.config.get("sql_saveDayServiceStInfo");
		sql_queryVehicleInfo = ConfigLoader.config.get("sql_queryVehicleInfo");
		sql_saveOilDayStat = ConfigLoader.config.get("sql_saveOilDayStat");
		sql_vehicleConfigOilMonitor = ConfigLoader.config.get("sql_vehicleConfigOilMonitor");
		sql_saveOilDriver= ConfigLoader.config.get("sql_saveOilDriver");
		sql_queryOilDayInfo = ConfigLoader.config.get("sql_queryOilDayInfo");
		sql_saveOilMonthInfo = ConfigLoader.config.get("sql_saveOilMonthInfo");
		sql_saveOilChanged = ConfigLoader.config.get("sql_saveOilChanged");
		sql_saveStopstartInfo = ConfigLoader.config.get("sql_saveStopstartInfo");
		sql_queryDriverClockinDetail = ConfigLoader.config.get("sql_queryDriverClockinDetail");
		
		sql_saveMonthServiceStInfo = ConfigLoader.config.get("sql_saveMonthServiceStInfo");
		sql_queryStatDayInfo = ConfigLoader.config.get("sql_queryStatDayInfo");
		sql_delRestoreMonthInfo = ConfigLoader.config.get("sql_delRestoreMonthInfo");
		sql_delOilMonthInfo = ConfigLoader.config.get("sql_delOilMonthInfo");
		
		sql_saveGasPressureDayStat = ConfigLoader.config.get("sql_saveGasPressureDayStat");
		sql_saveOilPressureDayStat = ConfigLoader.config.get("sql_saveOilPressureDayStat");
		sql_saveCoolLiquidtemDayStat = ConfigLoader.config.get("sql_saveCoolLiquidtemDayStat");
		sql_saveSpeeddistDayStat = ConfigLoader.config.get("sql_saveSpeeddistDayStat");
		sql_saveRotateDayStat = ConfigLoader.config.get("sql_saveRotateDayStat");
		sql_saveVoltageDayStat = ConfigLoader.config.get("sql_saveVoltageDayStat");
		sql_saveAirTemperture = ConfigLoader.config.get("sql_saveAirTemperture");
		
		sql_saveDayMileageStInfo = ConfigLoader.config.get("sql_saveDayMileageStInfo");
		sql_saveDriverDetailInfo = ConfigLoader.config.get("sql_saveDriverDetailInfo");
		sql_queryTotalMileageInfo = ConfigLoader.config.get("sql_queryTotalMileageInfo");
		sql_updateStaInfo = ConfigLoader.config.get("sql_updateStaInfo");
		sql_deleteVehicleInfo = ConfigLoader.config.get("sql_deleteVehicleInfo");
		sql_deleteDriverInfo = ConfigLoader.config.get("sql_deleteDriverInfo");
		sql_saveMonthDriverDetailInfo = ConfigLoader.config.get("sql_saveMonthDriverDetailInfo");
		sql_queryDriverDetailInfo = ConfigLoader.config.get("sql_queryDriverDetailInfo");
		sql_delRestoreDriverMonthInfo = ConfigLoader.config.get("sql_delRestoreDriverMonthInfo");
		
		sql_deleteOilSaveInfo = ConfigLoader.config.get("sql_deleteOilSaveInfo");
		sql_deleteStopStartInfo = ConfigLoader.config.get("sql_deleteStopStartInfo");
		sql_deleteRunningInfo = ConfigLoader.config.get("sql_deleteRunningInfo");
		
		sql_deleteOilWearInfo = ConfigLoader.config.get("sql_deleteOilWearInfo");
		//sql_deleteOilChangedInfo = ConfigLoader.config.get("sql_deleteOilChangedInfo");
		
		sql_deleteReportInfo = ConfigLoader.config.get("sql_deleteReportInfo");
		
		sql_loadingOilMap = ConfigLoader.config.get("sql_loadingOilMap");
		
	}
	
	/***
	 *  存储车辆运行状态信息日统计
	 * @throws SQLException
	 */
	public void saveStaDayInfo(Connection dbCon,List<VehicleStatus> list) throws Exception{
		String vid = "";
		PreparedStatement stSaveDayStInfo = null;
		stSaveDayStInfo = dbCon.prepareStatement(sql_saveDayServiceStInfo);
		try{
			for(VehicleStatus vehicleStatus : list){
				vid = vehicleStatus.getVid();
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(vehicleStatus.getVid());
				String cfgFlag = "0";
				if (info!=null){
					cfgFlag = info.getCfgFlag();
				}
				//String cfgFlag = info.getCfgFlag();
				
				stSaveDayStInfo.setLong(1, vehicleStatus.getStatDate()); // 日期UTC
				stSaveDayStInfo.setString(2, vehicleStatus.getVid()); // 车辆编号
				if(info != null){
					stSaveDayStInfo.setString(3, info.getVehicleNo()); // 车牌号码
					stSaveDayStInfo.setString(4, info.getVinCode()); // 车架号(VIN)
					stSaveDayStInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDayStInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDayStInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDayStInfo.setString(8, info.getTeamName()); // 车队名称
			
					//stSaveDayStInfo.setLong(9, info.getCheckNum()); // 车辆上线次数(终端成功鉴权次数)
					stSaveDayStInfo.setLong(9, vehicleStatus.getOnOffLine()); // 车辆在线时长
					stSaveDayStInfo.setLong(10, vehicleStatus.getEngineTime()); // 当日发动机运行时间
					
					stSaveDayStInfo.setLong(11, vehicleStatus.getMaxSpeed()); // 本日最大车速
					stSaveDayStInfo.setLong(12, vehicleStatus.getMaxRpm()); // 本日最大发动机转速
					stSaveDayStInfo.setLong(13, vehicleStatus.getCountGPSValid()); // 定位有效数量
					stSaveDayStInfo.setLong(14, vehicleStatus.getCountGPSInvalid()); // 定位无效数量
					stSaveDayStInfo.setLong(15, vehicleStatus.getGpsTimeInvild()); // GPS时间无效数量
					stSaveDayStInfo.setLong(16, vehicleStatus.getCountLatLonInvalid()); // 经纬度无效数量
				
					
					stSaveDayStInfo.setLong(17, vehicleStatus.getAccCount()); // ACC开次数
					stSaveDayStInfo.setLong(18, vehicleStatus.getAccTime()); // ACC开时长				
					stSaveDayStInfo.setString(19,  info.getVlineId()); //线路ID	
					stSaveDayStInfo.setString(20, info.getLineName());//线路名称
					stSaveDayStInfo.setLong(21, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程
					stSaveDayStInfo.setLong(22, vehicleStatus.getPoint_oil()); // 最后一个点减第一个点计算油耗
					stSaveDayStInfo.setLong(23, vehicleStatus.getGis_milege()); // GIS计算里程
					
					stSaveDayStInfo.setLong(24,vehicleStatus.getRunningTime());//当日行车时长
					stSaveDayStInfo.setString(25, cfgFlag);//是否使用精准油耗 1 使用 0不使用,2宇通传感器,3金旅透传
					
					stSaveDayStInfo.setLong(26, vehicleStatus.getMileage());//里程
				
					if ("0".equals(cfgFlag) || "3".equals(cfgFlag)){ //ECU油耗
						stSaveDayStInfo.setDouble(27, vehicleStatus.getEcuOil());
						stSaveDayStInfo.setDouble(28, vehicleStatus.getEcuRunningOil());			
					}else if("2".equals(cfgFlag)){ //传感器油耗
						String value = ThreadPool.oilWearMap.get(vehicleStatus.getVid());
						if(!"".equals(value) &&  value!= null){
							stSaveDayStInfo.setString(27, value.split(":")[0]);
							stSaveDayStInfo.setString(28, value.split(":")[1]);
						}else{
							stSaveDayStInfo.setString(27, null);
							stSaveDayStInfo.setString(28, null);
						}
					}else{
						stSaveDayStInfo.setDouble(27, vehicleStatus.getPrecise_oil()*0.02);//油耗
						stSaveDayStInfo.setDouble(28, vehicleStatus.getMetRunningOil()*0.02);//行车油耗
					}
					
					stSaveDayStInfo.setDouble(29, vehicleStatus.getPrecise_oil()*0.02);//精准油耗
					stSaveDayStInfo.setDouble(30, vehicleStatus.getMetRunningOil()*0.02);//精准行车油耗
					stSaveDayStInfo.setDouble(31, (vehicleStatus.getPrecise_oil() - vehicleStatus.getMetRunningOil())*0.02);//精准怠速油耗
					
					stSaveDayStInfo.setLong(32, vehicleStatus.getEcuOil());//ecu油耗
					stSaveDayStInfo.setLong(33, vehicleStatus.getEcuRunningOil());//ecu行车油耗
					stSaveDayStInfo.setLong(34, (vehicleStatus.getEcuOil() - vehicleStatus.getEcuRunningOil()));//ecu怠速油耗
					//----新增八个字段
					stSaveDayStInfo.setLong(35, vehicleStatus.getHeatUpTime());
					stSaveDayStInfo.setLong(36, vehicleStatus.getAirconditionTime());
					stSaveDayStInfo.setLong(37, vehicleStatus.getBrake());
					stSaveDayStInfo.setLong(38, vehicleStatus.getRetarder());
					stSaveDayStInfo.setLong(39, vehicleStatus.getTrumpet());
					stSaveDayStInfo.setLong(40, vehicleStatus.getAbs());
					stSaveDayStInfo.setLong(41, vehicleStatus.getDoor1());
					stSaveDayStInfo.setLong(42, vehicleStatus.getDoor2());
					stSaveDayStInfo.addBatch();
				}
				
			}
			stSaveDayStInfo.executeBatch();
				
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}catch(SQLException e){
			logger.debug("存储车辆运行状态出错：VID="+vid+" ",e);
		}
	
		
	}
	
	/**
	 * 车辆状态存储月统计信息
	 * 
	 * @throws SQLException
	 * 
	 */
	public void saveStaMonthInfo(long startMonthUtc, long endMonthUc,
			String month, String year) throws SQLException {
		Connection dbCon = OracleConnectionPool.getConnection();
		PreparedStatement stSaveStatMonthInfo = null;
		PreparedStatement stQueryStatDayInfo = null;
		ResultSet rs = null;
		String vid = "";
		try {
			stSaveStatMonthInfo = dbCon.prepareStatement(sql_saveMonthServiceStInfo);
			stQueryStatDayInfo = dbCon.prepareStatement(sql_queryStatDayInfo);
			stQueryStatDayInfo.setLong(1, startMonthUtc);// 获取上一个月1号凌晨时间
			stQueryStatDayInfo.setLong(2, endMonthUc);// 获取当前月1号凌晨时间
			rs = stQueryStatDayInfo.executeQuery();
			int count = 0;
			while (rs.next()) {
				try {
					stSaveStatMonthInfo.setLong(1, Integer.parseInt(year)); // 统计月
					stSaveStatMonthInfo.setLong(2, Integer.parseInt(month)); // 统计月
					stSaveStatMonthInfo.setString(3, rs.getString("VID")); // 车辆编号
					stSaveStatMonthInfo.setString(4, rs.getString("VEHICLE_NO")); // 车牌号码
					stSaveStatMonthInfo.setString(5,rs.getString("C_VIN")); // 车架号(VIN)
					stSaveStatMonthInfo.setString(6,rs.getString("CORP_ID")); // 企业ID
					stSaveStatMonthInfo.setString(7, rs.getString("CORP_NAME")); // 企业名称
					stSaveStatMonthInfo.setString(8, rs.getString("TEAM_ID")); // 车队ID
					stSaveStatMonthInfo.setString(9, rs.getString("TEAM_NAME")); // 车队名称
			
					//stSaveDayStInfo.setLong(9, info.getCheckNum()); // 车辆上线次数(终端成功鉴权次数)
					stSaveStatMonthInfo.setLong(10, rs.getLong(8)); // 车辆在线时长
					stSaveStatMonthInfo.setLong(11, rs.getLong(9)); // 当日发动机运行时间
		
					stSaveStatMonthInfo.setLong(12, rs.getLong(10)); // 本日最大车速
					stSaveStatMonthInfo.setLong(13, rs.getLong(11)); // 本日最大发动机转速
					stSaveStatMonthInfo.setLong(14, rs.getLong(12)); // 定位有效数量
					stSaveStatMonthInfo.setLong(15, rs.getLong(13)); // 定位无效数量
					stSaveStatMonthInfo.setLong(16, rs.getLong(14)); // GPS时间无效数量
					stSaveStatMonthInfo.setLong(17, rs.getLong(15)); // 经纬度无效数量
				
					
					stSaveStatMonthInfo.setLong(18, rs.getLong(16)); // ACC开次数
					stSaveStatMonthInfo.setLong(19, rs.getLong(17)); // ACC开时长				
					stSaveStatMonthInfo.setString(20,  rs.getString(18)); //线路ID	
					stSaveStatMonthInfo.setString(21, rs.getString(19));//线路名称
					stSaveStatMonthInfo.setLong(22, rs.getLong(20)); // 最后一个点减第一个点计算里程
					stSaveStatMonthInfo.setLong(23, rs.getLong(21)); // 最后一个点减第一个点计算油耗
					stSaveStatMonthInfo.setLong(24, rs.getLong(22)); // GIS计算里程
					
					stSaveStatMonthInfo.setLong(25,rs.getLong(23));//当日行车时长
					stSaveStatMonthInfo.setString(26, rs.getString(24));//是否使用精准油耗 1 使用 0不使用
					
					stSaveStatMonthInfo.setLong(27, rs.getLong(25));//里程
				
		
					stSaveStatMonthInfo.setDouble(28, rs.getLong(26));//油耗
					stSaveStatMonthInfo.setDouble(29, rs.getLong(27));//行车油耗
					
					stSaveStatMonthInfo.setLong(30, rs.getLong(28));//精准油耗
					stSaveStatMonthInfo.setLong(31, rs.getLong(29));//精准行车油耗
					stSaveStatMonthInfo.setLong(32, rs.getLong(30));//精准怠速油耗
					
					stSaveStatMonthInfo.setLong(33, rs.getLong(31));//ecu油耗
					stSaveStatMonthInfo.setLong(34, rs.getLong(32));//ecu行车油耗
					stSaveStatMonthInfo.setLong(35, rs.getLong(33));//ecu怠速油耗
					//----新增八个字段
					stSaveStatMonthInfo.setLong(36, rs.getLong(34));
					stSaveStatMonthInfo.setLong(37, rs.getLong(35));
					stSaveStatMonthInfo.setLong(38, rs.getLong(36));
					stSaveStatMonthInfo.setLong(39, rs.getLong(37));
					stSaveStatMonthInfo.setLong(40, rs.getLong(38));
					stSaveStatMonthInfo.setLong(41, rs.getLong(39));
					stSaveStatMonthInfo.setLong(42, rs.getLong(40));
					stSaveStatMonthInfo.setLong(43, rs.getLong(41));
					stSaveStatMonthInfo.addBatch();
					
					count++;
					
					if(count % 1000 == 0){
						stSaveStatMonthInfo.executeBatch();
						stSaveStatMonthInfo.clearBatch();
						count = 0;
					}

				} catch (SQLException ex) {
					logger.error("车辆月统计出错." + vid, ex);
				}
			}

			if (count > 0) {
				stSaveStatMonthInfo.executeBatch();
				stSaveStatMonthInfo.clearBatch();
				count = 0;
			}
		} catch (SQLException e) {
			logger.error("车辆月统计出错." + vid, e);
		} finally {
			if (rs != null) {
				rs.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
			if (stSaveStatMonthInfo != null) {
				stSaveStatMonthInfo.close();
			}
			if (stQueryStatDayInfo != null) {
				stQueryStatDayInfo.close();
			}
		}
	}
	/**
	 * 
	 * 停车起步信息存储
	 * @param dbCon
	 * @param list
	 * @throws SQLException
	 */
	public void saveStopStartInfo(Connection dbCon,List<VehicleStatus> list) throws SQLException{
		PreparedStatement stSaveDayStInfo = null;
		stSaveDayStInfo = dbCon.prepareStatement(sql_saveStopstartInfo);
		String vid ="";
		try{
			for(VehicleStatus vehicleStatus : list){
				vid = vehicleStatus.getVid();
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(vehicleStatus.getVid());
				String cfgFlag = "0";
				if (info!=null){
					cfgFlag = info.getCfgFlag();
				}
				//String cfgFlag = info.getCfgFlag();
				
				stSaveDayStInfo.setLong(1, vehicleStatus.getStatDate()); // 日期UTC
				stSaveDayStInfo.setString(2, vehicleStatus.getVid()); // 车辆编号
				if(info != null){
					stSaveDayStInfo.setString(3, info.getVehicleNo()); // 车牌号码
					stSaveDayStInfo.setString(4, info.getVinCode()); // 车架号(VIN)
					stSaveDayStInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDayStInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDayStInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDayStInfo.setString(8, info.getTeamName()); // 车队名称
			
					stSaveDayStInfo.setLong(9, vehicleStatus.getEngineOpenTime()); // 发动机点火
					stSaveDayStInfo.setLong(10, vehicleStatus.getStartTime()); // 起步
					stSaveDayStInfo.setLong(11, vehicleStatus.getStopTime()); // 停车
					stSaveDayStInfo.setLong(12, vehicleStatus.getEngineCloseTime()); // 发动机熄火
					stSaveDayStInfo.setLong(13, vehicleStatus.getOnOffLine()); // 车辆在线时长
					stSaveDayStInfo.setLong(14, vehicleStatus.getEngineTime()); // 当日发动机运行时间
					
					stSaveDayStInfo.setLong(15, vehicleStatus.getMaxSpeed()); // 本日最大车速
					stSaveDayStInfo.setLong(16, vehicleStatus.getMaxRpm()); // 本日最大发动机转速
					stSaveDayStInfo.setLong(17, vehicleStatus.getCountGPSValid()); // 定位有效数量
					stSaveDayStInfo.setLong(18, vehicleStatus.getCountGPSInvalid()); // 定位无效数量
					stSaveDayStInfo.setLong(19, vehicleStatus.getGpsTimeInvild()); // GPS时间无效数量
					stSaveDayStInfo.setLong(20, vehicleStatus.getCountLatLonInvalid()); // 经纬度无效数量
				
					
					stSaveDayStInfo.setLong(21, vehicleStatus.getAccCount()); // ACC开次数
					stSaveDayStInfo.setLong(22, vehicleStatus.getAccTime()); // ACC开时长					
					stSaveDayStInfo.setString(23,  info.getVlineId()); //线路ID	
					stSaveDayStInfo.setString(24, info.getLineName());//线路名称
					stSaveDayStInfo.setLong(25, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程
					stSaveDayStInfo.setLong(26, vehicleStatus.getPoint_oil()); // 最后一个点减第一个点计算油耗
					stSaveDayStInfo.setLong(27, vehicleStatus.getGis_milege()); // GIS计算里程
					
					stSaveDayStInfo.setLong(28,vehicleStatus.getRunningTime());//当日行车时长
					stSaveDayStInfo.setString(29, cfgFlag);//是否使用精准油耗 1 使用 0不使用
					
					stSaveDayStInfo.setLong(30, vehicleStatus.getMileage());//里程
				
					if ("0".equals(cfgFlag) || "3".equals(cfgFlag) ){ //对于宇通车辆传感器油耗无法计算停车起步问题，后续。
						stSaveDayStInfo.setDouble(31, vehicleStatus.getEcuOil());
						stSaveDayStInfo.setDouble(32, vehicleStatus.getEcuRunningOil());				
					}else if("2".equals(cfgFlag)){
						stSaveDayStInfo.setDouble(31, vehicleStatus.getUseOil());
						stSaveDayStInfo.setDouble(32, vehicleStatus.getRunningOil());
					}else{
						stSaveDayStInfo.setDouble(31, vehicleStatus.getPrecise_oil()*0.02);//油耗
						stSaveDayStInfo.setDouble(32, vehicleStatus.getMetRunningOil()*0.02);//行车油耗
					}
					
					stSaveDayStInfo.setDouble(33, vehicleStatus.getPrecise_oil()*0.02);//精准油耗
					stSaveDayStInfo.setDouble(34, vehicleStatus.getMetRunningOil()*0.02);//精准行车油耗
					stSaveDayStInfo.setDouble(35, (vehicleStatus.getPrecise_oil() - vehicleStatus.getMetRunningOil())*0.02);//精准怠速油耗
					
					stSaveDayStInfo.setLong(36, vehicleStatus.getEcuOil());//ecu油耗
					stSaveDayStInfo.setLong(37, vehicleStatus.getEcuRunningOil());//ecu行车油耗
					stSaveDayStInfo.setLong(38, (vehicleStatus.getEcuOil() - vehicleStatus.getEcuRunningOil()));//ecu怠速油耗
					//----新增八个字段
					stSaveDayStInfo.setLong(39, vehicleStatus.getHeatUpTime());
					stSaveDayStInfo.setLong(40, vehicleStatus.getAirconditionTime());
					stSaveDayStInfo.setLong(41, vehicleStatus.getBrake());
					stSaveDayStInfo.setLong(42, vehicleStatus.getRetarder());
					stSaveDayStInfo.setLong(43, vehicleStatus.getTrumpet());
					stSaveDayStInfo.setLong(44, vehicleStatus.getAbs());
					stSaveDayStInfo.setLong(45, vehicleStatus.getDoor1());
					stSaveDayStInfo.setLong(46, vehicleStatus.getDoor2());
					stSaveDayStInfo.addBatch();
				}
			
			}
			stSaveDayStInfo.executeBatch();
		}catch(SQLException e){
			logger.debug("存储起步停车出错：VID="+vid,e);
		}finally{
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}	
	
	} 
	
	
	/**
	 * 查询车辆编号，车架号，企业编号，企业名称，车队编号，车队名称,车牌号、车辆内部编码、线路、司机
	 * 
	 * @param vid
	 * @return
	 * @throws SQLException
	 */
	public synchronized VehicleInfo queryVechileInfo(String vid){
		VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(vid);
		//logger.info("----------------------------------静态数据vehicleInfoMap大小："+ThreadPool.vehicleInfoMap.size());
/*		if (info == null) {
			ResultSet rs = null;
			Connection dbCon = null;

			PreparedStatement stQueryVehicleInfo = null;
			try {
				dbCon = OracleConnectionPool.getConnection();
				stQueryVehicleInfo = dbCon.prepareStatement(sql_queryVehicleInfo);
				stQueryVehicleInfo.setString(1, vid);
				rs = stQueryVehicleInfo.executeQuery();
				if (rs.next()) {
					info = new VehicleInfo();
					info.setVid(rs.getString("VID"));
					info.setVehicleNo(rs.getString("VEHICLE_NO"));
					info.setVinCode(rs.getString("VIN_CODE"));
					info.setEntId(rs.getString("EID"));
					info.setEntName(rs.getString("ENAME"));
					info.setTeamId(rs.getString("ENT_ID"));
					info.setTeamName(rs.getString("ENT_NAME"));
					info.setInnerCode(rs.getString("INNER_CODE"));
					info.setVehicleType(rs.getString("VEHICLE_TYPE"));
					info.setCfgFlag(rs.getString("CFG_FLAG"));
					info.setVlineId(rs.getString("CLASS_LINE_ID"));
					info.setLineName(rs.getString("LINE_NAME"));
				}
			} catch (SQLException e) {
				logger.debug("查询基本信息出错", e);
			} finally {
				try{
				if (rs != null) {
					rs.close();
				}
				if (stQueryVehicleInfo != null) {
					stQueryVehicleInfo.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
				}catch(Exception ex){
					logger.error("释放数据库连接出错.",ex);
				}
			}
			ThreadPool.vehicleInfoMap.put(vid, info);
		}*/
		
		return info;
	}
	
	/****
	 * 初始化加载车辆列表
	 */
	public  void loadVehicleInfo() {
		long startTime = System.currentTimeMillis();
		Connection dbCon = null;
		PreparedStatement stQueryVehicleInfo = null;
		ResultSet rs = null;
		String vid = "";
		try {
			ThreadPool.vehicleInfoMap.clear();
			dbCon = OracleConnectionPool.getConnection();
			stQueryVehicleInfo = dbCon.prepareStatement(sql_queryVehicleInfo);
			rs = stQueryVehicleInfo.executeQuery();
			while (rs.next()) {
				VehicleInfo info = new VehicleInfo();
				vid = rs.getString("VID");
				info.setVid(vid);
				info.setVehicleNo(rs.getString("VEHICLE_NO"));
				info.setVinCode(rs.getString("VIN_CODE"));
				info.setEntId(rs.getString("EID"));
				info.setEntName(rs.getString("ENAME"));
				info.setTeamId(rs.getString("ENT_ID"));
				info.setTeamName(rs.getString("ENT_NAME"));
				info.setInnerCode(rs.getString("INNER_CODE"));
				info.setVehicleType(rs.getString("VEHICLE_TYPE"));
				info.setCfgFlag(rs.getString("CFG_FLAG"));
				info.setVlineId(rs.getString("CLASS_LINE_ID"));
				info.setLineName(rs.getString("LINE_NAME"));
				ThreadPool.vehicleInfoMap.put(vid, info);
			}
			logger.info("车辆静态信息列表加载完成，配置数：[{}],耗时：[{}]S",ThreadPool.vehicleInfoMap.size(),(System.currentTimeMillis()-startTime)/1000);
		} catch (SQLException e) {
			logger.error("初始化车辆静态信息列表出错.", e);
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryVehicleInfo != null) {
					stQueryVehicleInfo.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception ex) {
				logger.error(ex.getMessage(), ex);
			}
		}
	}
	
	/****
	 * 初始化加载油箱油量监控车辆列表
	 */
	public  void loadOilMonitorVehicleListing() {
		Connection dbCon = null;
		PreparedStatement oilStat = null;
		ResultSet rs = null;
		try {
			ThreadPool.oilMonitorMap.clear();
			dbCon = OracleConnectionPool.getConnection();
			oilStat = dbCon.prepareStatement(sql_vehicleConfigOilMonitor);
			rs = oilStat.executeQuery();
			while (rs.next()) {
				ThreadPool.oilMonitorMap.put(rs.getString("VID"), rs.getString("CODE_ID"));
			}// End while
			logger.info("油量监控配置列表加载完成，配置数：[{}]",ThreadPool.oilMonitorMap.size());
		} catch (SQLException e) {
			logger.error("初始化加载油箱油量配置车辆列表出错.", e);
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (oilStat != null) {
					oilStat.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception ex) {
				logger.error(ex.getMessage(), ex);
			}
		}
	}
	/******
	 * 存储油量日统计
	 * @throws SQLException
	 */
	public void saveOilDayStat(Connection dbCon,List<OilWearBean> list) throws Exception{
		PreparedStatement stSaveOilDayStat = null;
		String vid = "";
		try{
			stSaveOilDayStat = dbCon.prepareStatement(sql_saveOilDayStat);
			for(OilWearBean oilWear : list){
				vid = oilWear.getVid();
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(oilWear.getVid());
	/*			String cfgFlag = "0";
				if (info!=null){
					cfgFlag = info.getCfgFlag();
				}*/
				if(info != null){
					stSaveOilDayStat.setLong(1, oilWear.getStatDate());
					stSaveOilDayStat.setString(2, oilWear.getVid());
					stSaveOilDayStat.setString(3, info.getVehicleNo()); // 车牌号码
					stSaveOilDayStat.setString(4, info.getInnerCode()); // 内部编号
					if(null != info.getVehicleType()){
						stSaveOilDayStat.setString(5, info.getVehicleType()); // 车型
					}else{
						stSaveOilDayStat.setString(5, null); // 车型
					}
					stSaveOilDayStat.setString(6, info.getEntId()); // 企业ID
					stSaveOilDayStat.setString(7, info.getEntName()); // 企业名称
					stSaveOilDayStat.setString(8, info.getTeamId()); // 车队ID	
					stSaveOilDayStat.setString(9, info.getTeamName()); // 车队名称
					//stSaveOilDayStat.setString(10, null); // 驾驶员ID  注：以后考虑
					//stSaveOilDayStat.setString(11, info.getDriverName()); // 驾驶员名称
					stSaveOilDayStat.setString(10, oilWear.getChanged_type());
					stSaveOilDayStat.setInt(11, oilWear.getAddOil());
					stSaveOilDayStat.setInt(12, oilWear.getDecreaseOil());
					stSaveOilDayStat.setInt(13, oilWear.getUsedOil()); //总油耗
					stSaveOilDayStat.setInt(14, oilWear.getRunningOil()); //行车油耗
					stSaveOilDayStat.setInt(15, oilWear.getUsedOil()-oilWear.getRunningOil()); //怠速油耗
					stSaveOilDayStat.addBatch();
					//logger.info("[VID="+vid+"] 油量监控日统计保存成功！");
				}
				
			}
			stSaveOilDayStat.executeBatch();
		}catch(Exception ex){
			logger.error("存储日油量监控出错." +vid,ex);
		}finally{	
			if(null != stSaveOilDayStat){
				stSaveOilDayStat.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
	
	}
	/**
	 * 节油驾驶日统计
	 * @param dbCon
	 * @param list
	 * @throws Exception
	 */
	public  void saveOilDayInfo(Connection dbCon,List<OilSaveBean> list) throws Exception{
		PreparedStatement stSaveOilDayStat = null;
		String vid = "";
		try{
			stSaveOilDayStat = dbCon.prepareStatement(sql_saveOilDriver);
			for(OilSaveBean oilSaveBean : list){
				vid = oilSaveBean.getVid();
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(oilSaveBean.getVid());
				String cfgFlag = "0";
				if (info!=null){
					cfgFlag = info.getCfgFlag();
				}
				if(info != null){
					stSaveOilDayStat.setLong(1, oilSaveBean.getStatDate());
					stSaveOilDayStat.setString(2, oilSaveBean.getVid());
					stSaveOilDayStat.setString(3, info.getVehicleNo()); // 车牌号码
					stSaveOilDayStat.setString(4, info.getInnerCode()); // 内部编号
					if(null != info.getVehicleType()){
						stSaveOilDayStat.setString(5, info.getVehicleType()); // 车型
					}else{
						stSaveOilDayStat.setString(5, null); // 车型
					}
					stSaveOilDayStat.setString(6, info.getEntId()); // 企业ID
					stSaveOilDayStat.setString(7, info.getEntName()); // 企业名称
					stSaveOilDayStat.setString(8, info.getTeamId()); // 车队ID	
					stSaveOilDayStat.setString(9, info.getTeamName()); // 车队名称
					stSaveOilDayStat.setString(10, oilSaveBean.getDriverId()); // 驾驶员ID  注：以后考虑
					stSaveOilDayStat.setString(11, oilSaveBean.getDriverName()); // 驾驶员名称
					
					stSaveOilDayStat.setLong(12,oilSaveBean.getMileage());
					if("0".equals(cfgFlag) || "3".equals(cfgFlag)){
						stSaveOilDayStat.setLong(13,oilSaveBean.getEcuOil());
						stSaveOilDayStat.setLong(14,oilSaveBean.getEcuRunningOil());
					}else if("2".equals(cfgFlag)){ //传感器油耗
						String value = ThreadPool.oilWearMap.get(oilSaveBean.getVid());
						if(!"".equals(value) &&  value!= null){
							stSaveOilDayStat.setString(13, value.split(":")[0]);
							stSaveOilDayStat.setString(14, value.split(":")[1]);
						}else{
							stSaveOilDayStat.setString(13, null);
							stSaveOilDayStat.setString(14, null);
						}
					}else {
						stSaveOilDayStat.setDouble(13,oilSaveBean.getPrecise_oil()*0.02);
						stSaveOilDayStat.setDouble(14,oilSaveBean.getMetRunningOil()*0.02);
					}					
					stSaveOilDayStat.setDouble(15,oilSaveBean.getPrecise_oil()*0.02);
					stSaveOilDayStat.setDouble(16,oilSaveBean.getMetRunningOil()*0.02);
					stSaveOilDayStat.setLong(17,oilSaveBean.getOverSpeedNum());
					stSaveOilDayStat.setLong(18,oilSaveBean.getOverSpeedTime());
					stSaveOilDayStat.setLong(19,oilSaveBean.getOverRpmNum());
					stSaveOilDayStat.setLong(20,oilSaveBean.getOverRpmTime());
					stSaveOilDayStat.setLong(21,oilSaveBean.getUrgentSpeedNuM());
					stSaveOilDayStat.setLong(22,oilSaveBean.getUrgentSpeedTime());
					stSaveOilDayStat.setLong(23,oilSaveBean.getUrgentLowdownNum());
					stSaveOilDayStat.setLong(24,oilSaveBean.getUrgentLowdownTime());
					stSaveOilDayStat.setLong(25,oilSaveBean.getLongIdleNum());
					stSaveOilDayStat.setLong(26,oilSaveBean.getLongIdleTime());
					stSaveOilDayStat.setLong(27,oilSaveBean.getIdleAirConditionNum());
					stSaveOilDayStat.setLong(28,oilSaveBean.getIdleAirConditionTime());
					stSaveOilDayStat.setLong(29,oilSaveBean.getGearGlideNum());
					stSaveOilDayStat.setLong(30,oilSaveBean.getGearGlideTime());
					
					stSaveOilDayStat.setLong(31,oilSaveBean.getAirConditionTime());
					stSaveOilDayStat.setLong(32,oilSaveBean.getAirConditionNum());
					stSaveOilDayStat.setLong(33,oilSaveBean.getWarmWindTime()); //暖风工作时长
					stSaveOilDayStat.setLong(34,oilSaveBean.getEconomicRunTime()); //超经济区运时长
					stSaveOilDayStat.setLong(35,oilSaveBean.getEngineTime()); //发动机运行时长
					stSaveOilDayStat.setString(36,cfgFlag); //油耗来源
					
					stSaveOilDayStat.addBatch();
					//logger.info("[VID="+oilSaveBean.getVid()+"] 节油驾驶日统计保存成功！");
				}
				
			}
			stSaveOilDayStat.executeBatch();
		}catch(Exception ex){
			logger.error("存储节油驾驶出错." + vid,ex);
		}finally{
			if(null != stSaveOilDayStat){
				stSaveOilDayStat.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
	}
	
	/**
	 * 节油驾驶月统计
	 * @param dbCon
	 * @param list
	 * @throws Exception
	 */
	public  void saveOilMonthInfo(long startMonthUtc, long endMonthUc,
			String month, String year) throws Exception{
		PreparedStatement stQueryOilDayInfo = null;
		PreparedStatement stSaveOilDayStat = null;
		Connection dbCon = null;
		ResultSet rs = null;
		String vid = "";
		try {
			dbCon = OracleConnectionPool.getConnection();
			stQueryOilDayInfo = dbCon.prepareStatement(sql_queryOilDayInfo);
			stSaveOilDayStat = dbCon.prepareStatement(sql_saveOilMonthInfo);
			
			stQueryOilDayInfo.setLong(1, startMonthUtc);// 获取上一个月1号凌晨时间
			stQueryOilDayInfo.setLong(2, endMonthUc);// 获取当前月1号凌晨时间
			rs = stQueryOilDayInfo.executeQuery();
			int count = 0;
			
			while(rs.next()){
				vid = rs.getString("VID");
				stSaveOilDayStat.setLong(1, Integer.parseInt(year)); // 统计月
				stSaveOilDayStat.setLong(2, Integer.parseInt(month)); // 统计月
				stSaveOilDayStat.setString(3, rs.getString("VID"));
				stSaveOilDayStat.setString(4, rs.getString("VEHICLE_NO")); // 车牌号码
				stSaveOilDayStat.setString(5, rs.getString("INNER_CODE")); // 内部编号
			
				stSaveOilDayStat.setString(6, rs.getString("PROD_TYPE")); // 车型
				
				stSaveOilDayStat.setString(7, rs.getString("CORP_ID")); // 企业ID
				stSaveOilDayStat.setString(8, rs.getString("CORP_NAME")); // 企业名称
				stSaveOilDayStat.setString(9, rs.getString("TEAM_ID")); // 车队ID	
				stSaveOilDayStat.setString(10, rs.getString("TEAM_NAME")); // 车队名称
				stSaveOilDayStat.setString(11, rs.getString("DRIVER_ID")); // 驾驶员ID  注：以后考虑
				stSaveOilDayStat.setString(12, rs.getString("DRIVER_NAME")); // 驾驶员名称
				
				stSaveOilDayStat.setLong(13,rs.getLong(11));
				
				stSaveOilDayStat.setLong(14,rs.getLong(12));							
				stSaveOilDayStat.setLong(15,rs.getLong(13));
				stSaveOilDayStat.setLong(16,rs.getLong(14));
				stSaveOilDayStat.setLong(17,rs.getLong(15));
				stSaveOilDayStat.setLong(18,rs.getLong(16));
				stSaveOilDayStat.setLong(19,rs.getLong(17));
				stSaveOilDayStat.setLong(20,rs.getLong(18));
				stSaveOilDayStat.setLong(21,rs.getLong(19));
				stSaveOilDayStat.setLong(22,rs.getLong(20));
				stSaveOilDayStat.setLong(23,rs.getLong(21));
				stSaveOilDayStat.setLong(24,rs.getLong(22));
				stSaveOilDayStat.setLong(25,rs.getLong(23));
				stSaveOilDayStat.setLong(26,rs.getLong(24));
				stSaveOilDayStat.setLong(27,rs.getLong(25));
				stSaveOilDayStat.setLong(28,rs.getLong(26));
				stSaveOilDayStat.setLong(29,rs.getLong(27));
				stSaveOilDayStat.setLong(30,rs.getLong(28));
				stSaveOilDayStat.setLong(31,rs.getLong(29));
				stSaveOilDayStat.setLong(32,rs.getLong(30));
				stSaveOilDayStat.setLong(33,rs.getLong(31));
				stSaveOilDayStat.setLong(34,rs.getLong(32)); //暖风工作时长
				stSaveOilDayStat.setLong(35,rs.getLong(33)); //超经济区运行时长
				stSaveOilDayStat.setLong(36,rs.getLong(34)); //发动机运行时长
				stSaveOilDayStat.setString(37,rs.getString(35)); //油耗来源
				
				stSaveOilDayStat.addBatch();
				count++;
				
				if(count % 1000 == 0){
					stSaveOilDayStat.executeBatch();
					stSaveOilDayStat.clearBatch();
					count = 0;
				}
				
		}

		if (count > 0) {
			stSaveOilDayStat.executeBatch();
			stSaveOilDayStat.clearBatch();
			count = 0;
		}
		}catch (Exception e) {
			logger.error("节油驾驶月统计出错." + vid, e);
		}finally{
			if(null != rs){
				rs.close();
			}
			if(null != stQueryOilDayInfo){
				stQueryOilDayInfo.close();
			}
			if(null != stSaveOilDayStat){
				stSaveOilDayStat.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		
		
	
	}
	

	/*****
	 * 存储数据库油量变化记录
	 */
	public  void saveOilChangedMass(Connection dbCon,File file,List<OilmassChangedDetail> oilIt){
		//OracleConnection dbCon = null;
		String vid = file.getName().replaceAll("\\.txt", "");
		PreparedStatement stSaveOilChangedMass = null;
		try {
			//dbCon = (OracleConnection)OracleConnectionPool.getConnection();
			stSaveOilChangedMass = dbCon.prepareStatement(sql_saveOilChanged);

			for (int i=0;i<oilIt.size();i++){
				OilmassChangedDetail oil = oilIt.get(i);
				stSaveOilChangedMass.setString(1, GeneratorPK.instance().getPKString());
				stSaveOilChangedMass.setString(2, oil.getChangeType());
				stSaveOilChangedMass.setString(3, vid);
				
				stSaveOilChangedMass.setString(4, oil.getGpsTime());
				stSaveOilChangedMass.setLong(5, oil.getLat());
				stSaveOilChangedMass.setLong(6, oil.getLon());
				stSaveOilChangedMass.setLong(7, oil.getMapLon());
				stSaveOilChangedMass.setLong(8, oil.getMapLat());
				stSaveOilChangedMass.setInt(9, oil.getElevation());
				stSaveOilChangedMass.setInt(10, oil.getDirection());
				stSaveOilChangedMass.setInt(11, oil.getGps_speed());
				stSaveOilChangedMass.setLong(12, System.currentTimeMillis());
				stSaveOilChangedMass.setInt(13, Types.NULL);
				stSaveOilChangedMass.setDouble(14, Math.round(oil.getCurr_oilmass()*2));//进行单位换算 0.1 = 0.05 *2
				stSaveOilChangedMass.setDouble(15, Math.round(oil.getChange_oilmass()*2));//进行单位换算
				stSaveOilChangedMass.addBatch();
				
			}// End while
			stSaveOilChangedMass.executeBatch();
			logger.info("存储数据库油量变化记录成功！VID= {}",vid);
		} catch (Exception e) {
			logger.error("存储数据库油量变化记录失败！VID=" + vid,e );
		}finally{
			try {
				if(null != stSaveOilChangedMass ){
						stSaveOilChangedMass.close();
				}
				if (dbCon!=null){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error(e.getMessage(),e);
			}
		}

	}
	/**
	 * 删除驾驶员月统计
	 * @param utc
	 * @throws SQLException 
	 */
	public  void deleteRestoreDriverMonthInfo(String utc) throws SQLException{
		Connection dbCon = null;
		PreparedStatement stDeleteStatMonthInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			stDeleteStatMonthInfo = dbCon.prepareStatement(sql_delRestoreDriverMonthInfo);
			int year  = 0;
			int month = 0;
			if("".equals(utc) || utc != null){
				year = Integer.parseInt(utc.split("/")[0]);
				month = Integer.parseInt(utc.split("/")[1]);
			}
			stDeleteStatMonthInfo.setInt(1, year);
			stDeleteStatMonthInfo.setInt(2, month);
			stDeleteStatMonthInfo.execute();
		}catch (Exception e) {
			logger.error("删除驾驶员月统计删除出错.", e);
		}finally {
			if(dbCon != null){
				dbCon.close();
			}
			if (stDeleteStatMonthInfo != null) {
				stDeleteStatMonthInfo.close();
			}
		}
		
	}
	
	
	/**
	 * 存储机油压力
	 * 
	 * @throws SQLException
	 * @throws SQLException
	 */
	public  void saveOilPressureDay(Connection dbCon,List<OilPressureBean> list) throws Exception {
		PreparedStatement stSaveOilPressureDayStat = null;
		stSaveOilPressureDayStat = dbCon
				.prepareStatement(sql_saveOilPressureDayStat);
		try {
			for(OilPressureBean oilPressureBean : list){	
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(oilPressureBean.getVid());
				//VehicleInfo info = null;
				stSaveOilPressureDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveOilPressureDayStat.setString(2, oilPressureBean.getVid());
				if (info!= null) {
					stSaveOilPressureDayStat.setString(3, info.getVehicleNo());
					stSaveOilPressureDayStat.setString(4, info.getVinCode());
				} else {
					stSaveOilPressureDayStat.setString(3, null);
					stSaveOilPressureDayStat.setString(4, null);
				}
				stSaveOilPressureDayStat.setLong(5, oilPressureBean.getStatDate());
	
				stSaveOilPressureDayStat.setInt(6, oilPressureBean.getPressure_0()
						.getNum());
				stSaveOilPressureDayStat.setInt(7, oilPressureBean.getPressure_0()
						.getTime());
				stSaveOilPressureDayStat.setInt(8, oilPressureBean
						.getPressure_0_50().getNum());
				stSaveOilPressureDayStat.setInt(9, oilPressureBean
						.getPressure_0_50().getTime());
	
				stSaveOilPressureDayStat.setInt(10, oilPressureBean
						.getPressure_50_100().getNum());
				stSaveOilPressureDayStat.setInt(11, oilPressureBean
						.getPressure_50_100().getTime());
	
				stSaveOilPressureDayStat.setInt(12, oilPressureBean
						.getPressure_100_150().getNum());
				stSaveOilPressureDayStat.setInt(13, oilPressureBean
						.getPressure_100_150().getTime());
	
				stSaveOilPressureDayStat.setInt(14, oilPressureBean
						.getPressure_150_200().getNum());
				stSaveOilPressureDayStat.setInt(15, oilPressureBean
						.getPressure_150_200().getTime());
	
				stSaveOilPressureDayStat.setInt(16, oilPressureBean
						.getPressure_200_250().getNum());
				stSaveOilPressureDayStat.setInt(17, oilPressureBean
						.getPressure_200_250().getTime());
				stSaveOilPressureDayStat.setInt(18, oilPressureBean
						.getPressure_250_300().getNum());
				stSaveOilPressureDayStat.setInt(19, oilPressureBean
						.getPressure_250_300().getTime());
	
				stSaveOilPressureDayStat.setInt(20, oilPressureBean
						.getPressure_300_350().getNum());
				stSaveOilPressureDayStat.setInt(21, oilPressureBean
						.getPressure_300_350().getTime());
	
				stSaveOilPressureDayStat.setInt(22, oilPressureBean
						.getPressure_350_400().getNum());
				stSaveOilPressureDayStat.setInt(23, oilPressureBean
						.getPressure_350_400().getTime());
	
				stSaveOilPressureDayStat.setInt(24, oilPressureBean
						.getPressure_400_450().getNum());
				stSaveOilPressureDayStat.setInt(25, oilPressureBean
						.getPressure_400_450().getTime());
	
				stSaveOilPressureDayStat.setInt(26, oilPressureBean
						.getPressure_450_500().getNum());
				stSaveOilPressureDayStat.setInt(27, oilPressureBean
						.getPressure_450_500().getTime());
	
				stSaveOilPressureDayStat.setInt(28, oilPressureBean
						.getPressure_500_550().getNum());
				stSaveOilPressureDayStat.setInt(29, oilPressureBean
						.getPressure_500_550().getTime());
	
				stSaveOilPressureDayStat.setInt(30, oilPressureBean
						.getPressure_550_600().getNum());
				stSaveOilPressureDayStat.setInt(31, oilPressureBean
						.getPressure_550_600().getTime());
	
				stSaveOilPressureDayStat.setInt(32, oilPressureBean
						.getPressure_600_650().getNum());
				stSaveOilPressureDayStat.setInt(33, oilPressureBean
						.getPressure_600_650().getTime());
	
				stSaveOilPressureDayStat.setInt(34, oilPressureBean
						.getPressure_650_700().getNum());
				stSaveOilPressureDayStat.setInt(35, oilPressureBean
						.getPressure_650_700().getTime());
	
				stSaveOilPressureDayStat.setInt(36, oilPressureBean
						.getPressure_700_750().getNum());
				stSaveOilPressureDayStat.setInt(37, oilPressureBean
						.getPressure_700_750().getTime());
	
				stSaveOilPressureDayStat.setInt(38, oilPressureBean
						.getPressure_750_800().getNum());
				stSaveOilPressureDayStat.setInt(39, oilPressureBean
						.getPressure_750_800().getTime());
	
				stSaveOilPressureDayStat.setInt(40, oilPressureBean
						.getPressure_800().getNum());
				stSaveOilPressureDayStat.setInt(41, oilPressureBean
						.getPressure_800().getTime());
	
				stSaveOilPressureDayStat.setLong(42, oilPressureBean.getMax());
				stSaveOilPressureDayStat.setLong(43, oilPressureBean.getMin());
	
				stSaveOilPressureDayStat.addBatch();
			}
			
			stSaveOilPressureDayStat.executeBatch();
		} catch (SQLException e) {
		logger.error("统计车辆机油压力出错", e);
		
		} finally {
			if(null != stSaveOilPressureDayStat ){
				stSaveOilPressureDayStat.close();
			}
			if(null != dbCon){
				dbCon.close();
			}
		}

	}
	
	
	/***
	 * 存储进气压力
	 * 
	 * @param vid
	 * @throws SQLException
	 */
	public  void saveGasPressure(Connection dbCon,List<GasPressureBean> list) throws SQLException {
		PreparedStatement stSaveGasPressureDayStat = null;
		stSaveGasPressureDayStat = dbCon
				.prepareStatement(sql_saveGasPressureDayStat);
		try {
			for(GasPressureBean gsPressureBean : list){
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(gsPressureBean.getVid());
				//VehicleInfo info = null;
				stSaveGasPressureDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveGasPressureDayStat.setString(2, gsPressureBean.getVid());
				if ( info!= null) {
					stSaveGasPressureDayStat.setString(3, info.getVehicleNo());
					stSaveGasPressureDayStat.setString(4, info.getVinCode());
				} else {
					stSaveGasPressureDayStat.setString(3, null);
					stSaveGasPressureDayStat.setString(4, null);
				}
				stSaveGasPressureDayStat.setLong(5, gsPressureBean.getStatDate());
	
				stSaveGasPressureDayStat.setInt(6, gsPressureBean.getGsPressure_0()
						.getNum());
				stSaveGasPressureDayStat.setInt(7, gsPressureBean.getGsPressure_0()
						.getTime());
	
				stSaveGasPressureDayStat.setInt(8, gsPressureBean
						.getGsPressure_0_50().getNum());
				stSaveGasPressureDayStat.setInt(9, gsPressureBean
						.getGsPressure_0_50().getTime());
	
				stSaveGasPressureDayStat.setInt(10, gsPressureBean
						.getGsPressure_50_55().getNum());
				stSaveGasPressureDayStat.setInt(11, gsPressureBean
						.getGsPressure_50_55().getTime());
	
				stSaveGasPressureDayStat.setInt(12, gsPressureBean
						.getGsPressure_55_60().getNum());
				stSaveGasPressureDayStat.setInt(13, gsPressureBean
						.getGsPressure_55_60().getTime());
	
				stSaveGasPressureDayStat.setInt(14, gsPressureBean
						.getGsPressure_60_65().getNum());
				stSaveGasPressureDayStat.setInt(15, gsPressureBean
						.getGsPressure_60_65().getTime());
	
				stSaveGasPressureDayStat.setInt(16, gsPressureBean
						.getGsPressure_65_70().getNum());
				stSaveGasPressureDayStat.setInt(17, gsPressureBean
						.getGsPressure_65_70().getTime());
	
				stSaveGasPressureDayStat.setInt(18, gsPressureBean
						.getGsPressure_70_75().getNum());
				stSaveGasPressureDayStat.setInt(19, gsPressureBean
						.getGsPressure_70_75().getTime());
	
				stSaveGasPressureDayStat.setInt(20, gsPressureBean
						.getGsPressure_75_80().getNum());
				stSaveGasPressureDayStat.setInt(21, gsPressureBean
						.getGsPressure_75_80().getTime());
	
				stSaveGasPressureDayStat.setInt(22, gsPressureBean
						.getGsPressure_80_85().getNum());
				stSaveGasPressureDayStat.setInt(23, gsPressureBean
						.getGsPressure_80_85().getTime());
	
				stSaveGasPressureDayStat.setInt(24, gsPressureBean
						.getGsPressure_85_90().getNum());
				stSaveGasPressureDayStat.setInt(25, gsPressureBean
						.getGsPressure_85_90().getTime());
	
				stSaveGasPressureDayStat.setInt(26, gsPressureBean
						.getGsPressure_90_95().getNum());
				stSaveGasPressureDayStat.setInt(27, gsPressureBean
						.getGsPressure_90_95().getTime());
	
				stSaveGasPressureDayStat.setInt(28, gsPressureBean
						.getGsPressure_95_100().getNum());
				stSaveGasPressureDayStat.setInt(29, gsPressureBean
						.getGsPressure_95_100().getTime());
	
				stSaveGasPressureDayStat.setInt(30, gsPressureBean
						.getGsPressure_100_105().getNum());
				stSaveGasPressureDayStat.setInt(31, gsPressureBean
						.getGsPressure_100_105().getTime());
	
				stSaveGasPressureDayStat.setInt(32, gsPressureBean
						.getGsPressure_105_110().getNum());
				stSaveGasPressureDayStat.setInt(33, gsPressureBean
						.getGsPressure_105_110().getTime());
	
				stSaveGasPressureDayStat.setInt(34, gsPressureBean
						.getGsPressure_110_115().getNum());
				stSaveGasPressureDayStat.setInt(35, gsPressureBean
						.getGsPressure_110_115().getTime());
	
				stSaveGasPressureDayStat.setInt(36, gsPressureBean
						.getGsPressure_115_120().getNum());
				stSaveGasPressureDayStat.setInt(37, gsPressureBean
						.getGsPressure_115_120().getTime());
	
				stSaveGasPressureDayStat.setInt(38, gsPressureBean
						.getGsPressure_120().getNum());
				stSaveGasPressureDayStat.setInt(39, gsPressureBean
						.getGsPressure_120().getTime());
	
				stSaveGasPressureDayStat.setDouble(40, gsPressureBean.getMax());
				stSaveGasPressureDayStat.setDouble(41, gsPressureBean.getMin());
				stSaveGasPressureDayStat.addBatch();
			}

			stSaveGasPressureDayStat.executeBatch();
		} catch (SQLException e) {
			logger.error("统计进气压力出错.", e);
		} finally {
			if(null != stSaveGasPressureDayStat){
				stSaveGasPressureDayStat.close();
			}
			if(null != dbCon){
				dbCon.close();
			}
		}
	}
	
	/**
	 * 存储冷却液温度
	 * 
	 * @param vid
	 * @throws SQLException
	 */
	public  void saveCoolLiquidtemDayStat(Connection dbCon,List<CoolLiquidtemBean> list) throws Exception {
		PreparedStatement stSaveCoolLiquidtemDayStat = null;
		stSaveCoolLiquidtemDayStat = dbCon
				.prepareStatement(sql_saveCoolLiquidtemDayStat);
		try {
			for(CoolLiquidtemBean coolLiquidtemBean : list){
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(coolLiquidtemBean.getVid());
				//VehicleInfo info = null;
				stSaveCoolLiquidtemDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveCoolLiquidtemDayStat.setString(2, coolLiquidtemBean.getVid());
				if (info != null) {
					stSaveCoolLiquidtemDayStat.setString(3, info.getVehicleNo());
					stSaveCoolLiquidtemDayStat.setString(4, info.getVinCode());
				} else {
					stSaveCoolLiquidtemDayStat.setString(3, null);
					stSaveCoolLiquidtemDayStat.setString(4, null);
				}
				stSaveCoolLiquidtemDayStat.setLong(5, coolLiquidtemBean.getStatDate());
				stSaveCoolLiquidtemDayStat.setInt(6, coolLiquidtemBean
						.getCollLiquidtem_0().getNum());
				stSaveCoolLiquidtemDayStat.setInt(7, coolLiquidtemBean
						.getCollLiquidtem_0().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(8, coolLiquidtemBean
						.getCollLiquidtem_0_5().getNum());
				stSaveCoolLiquidtemDayStat.setInt(9, coolLiquidtemBean
						.getCollLiquidtem_0_5().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(10, coolLiquidtemBean
						.getCollLiquidtem_5_10().getNum());
				stSaveCoolLiquidtemDayStat.setInt(11, coolLiquidtemBean
						.getCollLiquidtem_5_10().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(12, coolLiquidtemBean
						.getCollLiquidtem_10_15().getNum());
				stSaveCoolLiquidtemDayStat.setInt(13, coolLiquidtemBean
						.getCollLiquidtem_10_15().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(14, coolLiquidtemBean
						.getCollLiquidtem_15_20().getNum());
				stSaveCoolLiquidtemDayStat.setInt(15, coolLiquidtemBean
						.getCollLiquidtem_15_20().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(16, coolLiquidtemBean
						.getCollLiquidtem_20_25().getNum());
				stSaveCoolLiquidtemDayStat.setInt(17, coolLiquidtemBean
						.getCollLiquidtem_20_25().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(18, coolLiquidtemBean
						.getCollLiquidtem_25_30().getNum());
				stSaveCoolLiquidtemDayStat.setInt(19, coolLiquidtemBean
						.getCollLiquidtem_25_30().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(20, coolLiquidtemBean
						.getCollLiquidtem_30_35().getNum());
				stSaveCoolLiquidtemDayStat.setInt(21, coolLiquidtemBean
						.getCollLiquidtem_30_35().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(22, coolLiquidtemBean
						.getCollLiquidtem_35_40().getNum());
				stSaveCoolLiquidtemDayStat.setInt(23, coolLiquidtemBean
						.getCollLiquidtem_35_40().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(24, coolLiquidtemBean
						.getCollLiquidtem_40_45().getNum());
				stSaveCoolLiquidtemDayStat.setInt(25, coolLiquidtemBean
						.getCollLiquidtem_40_45().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(26, coolLiquidtemBean
						.getCollLiquidtem_45_50().getNum());
				stSaveCoolLiquidtemDayStat.setInt(27, coolLiquidtemBean
						.getCollLiquidtem_45_50().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(28, coolLiquidtemBean
						.getCollLiquidtem_50_55().getNum());
				stSaveCoolLiquidtemDayStat.setInt(29, coolLiquidtemBean
						.getCollLiquidtem_50_55().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(30, coolLiquidtemBean
						.getCollLiquidtem_55_60().getNum());
				stSaveCoolLiquidtemDayStat.setInt(31, coolLiquidtemBean
						.getCollLiquidtem_55_60().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(32, coolLiquidtemBean
						.getCollLiquidtem_60_65().getNum());
				stSaveCoolLiquidtemDayStat.setInt(33, coolLiquidtemBean
						.getCollLiquidtem_60_65().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(34, coolLiquidtemBean
						.getCollLiquidtem_65_70().getNum());
				stSaveCoolLiquidtemDayStat.setInt(35, coolLiquidtemBean
						.getCollLiquidtem_65_70().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(36, coolLiquidtemBean
						.getCollLiquidtem_70_75().getNum());
				stSaveCoolLiquidtemDayStat.setInt(37, coolLiquidtemBean
						.getCollLiquidtem_70_75().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(38, coolLiquidtemBean
						.getCollLiquidtem_75_80().getNum());
				stSaveCoolLiquidtemDayStat.setInt(39, coolLiquidtemBean
						.getCollLiquidtem_75_80().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(40, coolLiquidtemBean
						.getCollLiquidtem_80_85().getNum());
				stSaveCoolLiquidtemDayStat.setInt(41, coolLiquidtemBean
						.getCollLiquidtem_80_85().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(42, coolLiquidtemBean
						.getCollLiquidtem_85_90().getNum());
				stSaveCoolLiquidtemDayStat.setInt(43, coolLiquidtemBean
						.getCollLiquidtem_85_90().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(44, coolLiquidtemBean
						.getCollLiquidtem_90_95().getNum());
				stSaveCoolLiquidtemDayStat.setInt(45, coolLiquidtemBean
						.getCollLiquidtem_90_95().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(46, coolLiquidtemBean
						.getCollLiquidtem_95_100().getNum());
				stSaveCoolLiquidtemDayStat.setInt(47, coolLiquidtemBean
						.getCollLiquidtem_95_100().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(48, coolLiquidtemBean
						.getCollLiquidtem_100_105().getNum());
				stSaveCoolLiquidtemDayStat.setInt(49, coolLiquidtemBean
						.getCollLiquidtem_100_105().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(50, coolLiquidtemBean
						.getCollLiquidtem_105_110().getNum());
				stSaveCoolLiquidtemDayStat.setInt(51, coolLiquidtemBean
						.getCollLiquidtem_105_110().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(52, coolLiquidtemBean
						.getCollLiquidtem_110_115().getNum());
				stSaveCoolLiquidtemDayStat.setInt(53, coolLiquidtemBean
						.getCollLiquidtem_110_115().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(54, coolLiquidtemBean
						.getCollLiquidtem_115_120().getNum());
				stSaveCoolLiquidtemDayStat.setInt(55, coolLiquidtemBean
						.getCollLiquidtem_115_120().getTime());
	
				stSaveCoolLiquidtemDayStat.setInt(56, coolLiquidtemBean
						.getCollLiquidtem_120().getNum());
				stSaveCoolLiquidtemDayStat.setInt(57, coolLiquidtemBean
						.getCollLiquidtem_120().getTime());
	
				stSaveCoolLiquidtemDayStat.setLong(58, coolLiquidtemBean.getMax());
				stSaveCoolLiquidtemDayStat.setLong(59, coolLiquidtemBean.getMin());
				stSaveCoolLiquidtemDayStat.addBatch();
			}

			stSaveCoolLiquidtemDayStat.executeBatch();
		} catch (SQLException e) {
			logger.error("统计冷却液温度出错.", e);
		} finally {
			if(null != stSaveCoolLiquidtemDayStat ){
				stSaveCoolLiquidtemDayStat.close();
			}
			if(null != dbCon ){
				dbCon.close();
			}
		}
	}
	
	
	/**
	 * 存储车速分析表
	 * 
	 * @throws SQLException
	 */
	public  void saveSpeeddistDay(Connection dbCon,List<SpeeddistDay> list) throws Exception {
		PreparedStatement stSaveSpeeddistDayStat = null;
		stSaveSpeeddistDayStat = dbCon
				.prepareStatement(sql_saveSpeeddistDayStat);
		try {
			for(SpeeddistDay speeddistDay : list){
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(speeddistDay.getVid());
				//VehicleInfo info = null;
				stSaveSpeeddistDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveSpeeddistDayStat.setString(2, speeddistDay.getVid());
				if (info != null) {
					stSaveSpeeddistDayStat.setString(3, info.getVehicleNo());
					stSaveSpeeddistDayStat.setString(4, info.getVinCode());
				} else {
					stSaveSpeeddistDayStat.setString(3, null);
					stSaveSpeeddistDayStat.setString(4, null);
				}
				stSaveSpeeddistDayStat.setLong(5, speeddistDay.getStatDate());
	
				stSaveSpeeddistDayStat.setLong(6, speeddistDay.getSpeed0());
				stSaveSpeeddistDayStat.setLong(7, speeddistDay.getSpeed0Time());
	
				stSaveSpeeddistDayStat.setLong(8, speeddistDay.getSpeed010());
				stSaveSpeeddistDayStat.setLong(9, speeddistDay.getSpeed010Time());
	
				stSaveSpeeddistDayStat.setLong(10, speeddistDay.getSpeed1020());
				stSaveSpeeddistDayStat.setLong(11, speeddistDay.getSpeed1020Time());
	
				stSaveSpeeddistDayStat.setLong(12, speeddistDay.getSpeed2030());
				stSaveSpeeddistDayStat.setLong(13, speeddistDay.getSpeed2030Time());
	
				stSaveSpeeddistDayStat.setLong(14, speeddistDay.getSpeed3040());
				stSaveSpeeddistDayStat.setLong(15, speeddistDay.getSpeed3040Time());
	
				stSaveSpeeddistDayStat.setLong(16, speeddistDay.getSpeed4050());
				stSaveSpeeddistDayStat.setLong(17, speeddistDay.getSpeed4050Time());
	
				stSaveSpeeddistDayStat.setLong(18, speeddistDay.getSpeed5060());
				stSaveSpeeddistDayStat.setLong(19, speeddistDay.getSpeed5060Time());
	
				stSaveSpeeddistDayStat.setLong(20, speeddistDay.getSpeed6070());
				stSaveSpeeddistDayStat.setLong(21, speeddistDay.getSpeed6070Time());
	
				stSaveSpeeddistDayStat.setLong(22, speeddistDay.getSpeed7080());
				stSaveSpeeddistDayStat.setLong(23, speeddistDay.getSpeed7080Time());
	
				stSaveSpeeddistDayStat.setLong(24, speeddistDay.getSpeed8090());
				stSaveSpeeddistDayStat.setLong(25, speeddistDay.getSpeed8090Time());
	
				stSaveSpeeddistDayStat.setLong(26, speeddistDay.getSpeed90100());
				stSaveSpeeddistDayStat
						.setLong(27, speeddistDay.getSpeed90100Time());
	
				stSaveSpeeddistDayStat.setLong(28, speeddistDay.getSpeed100110());
				stSaveSpeeddistDayStat.setLong(29,
						speeddistDay.getSpeed100110Time());
	
				stSaveSpeeddistDayStat.setLong(30, speeddistDay.getSpeed110120());
				stSaveSpeeddistDayStat.setLong(31,
						speeddistDay.getSpeed110120Time());
	
				stSaveSpeeddistDayStat.setLong(32, speeddistDay.getSpeed120130());
				stSaveSpeeddistDayStat.setLong(33,
						speeddistDay.getSpeed120130Time());
	
				stSaveSpeeddistDayStat.setLong(34, speeddistDay.getSpeed130140());
				stSaveSpeeddistDayStat.setLong(35,
						speeddistDay.getSpeed130140Time());
	
				stSaveSpeeddistDayStat.setLong(36, speeddistDay.getSpeed140150());
				stSaveSpeeddistDayStat.setLong(37,
						speeddistDay.getSpeed140150Time());
	
				stSaveSpeeddistDayStat.setLong(38, speeddistDay.getSpeed150160());
				stSaveSpeeddistDayStat.setLong(39,
						speeddistDay.getSpeed150160Time());
	
				stSaveSpeeddistDayStat.setLong(40, speeddistDay.getSpeed160170());
				stSaveSpeeddistDayStat.setLong(41,
						speeddistDay.getSpeed160170Time());
	
				stSaveSpeeddistDayStat.setLong(42, speeddistDay.getSpeed170180());
				stSaveSpeeddistDayStat.setLong(43,
						speeddistDay.getSpeed170180Time());
	
				stSaveSpeeddistDayStat.setLong(44, speeddistDay.getSpeed180190());
				stSaveSpeeddistDayStat.setLong(45,
						speeddistDay.getSpeed180190Time());
	
				stSaveSpeeddistDayStat.setLong(46, speeddistDay.getSpeed190200());
				stSaveSpeeddistDayStat.setLong(47,
						speeddistDay.getSpeed190200Time());
	
				stSaveSpeeddistDayStat.setLong(48, speeddistDay.getSpeedMax());
				stSaveSpeeddistDayStat.setLong(49, speeddistDay.getSpeedMaxTime());
	
				stSaveSpeeddistDayStat.setLong(50, speeddistDay.getMaxSpeed());
				stSaveSpeeddistDayStat.setLong(51, speeddistDay.getMinSpeed());
				stSaveSpeeddistDayStat.addBatch();
			}
			stSaveSpeeddistDayStat.executeBatch();
		} catch (SQLException e) {
			logger.error("统计车速分析表出错.", e);
		} finally {
			if(null != stSaveSpeeddistDayStat ){
				stSaveSpeeddistDayStat.close();
			}
			if(null != dbCon ){
				dbCon.close();
			}
		}
	}
	
	/**
	 * 存储转速分析表
	 * 
	 * @throws SQLException
	 */
	public  void saveRotatedistDay(Connection dbCon,List<RotateSpeedDay> list) throws Exception {
		PreparedStatement stSaveRoatedistDayStat = null;
		stSaveRoatedistDayStat = dbCon
				.prepareStatement(sql_saveRotateDayStat);
		try {
			for(RotateSpeedDay rotateSpeedDay : list){
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(rotateSpeedDay.getVid());
				//VehicleInfo info = null;
				stSaveRoatedistDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveRoatedistDayStat.setString(2, rotateSpeedDay.getVid());
				if (info != null) {
					stSaveRoatedistDayStat.setString(3, info.getVehicleNo());
					stSaveRoatedistDayStat.setString(4, info.getVinCode());
				} else {
					stSaveRoatedistDayStat.setString(3, null);
					stSaveRoatedistDayStat.setString(4, null);
				}
				stSaveRoatedistDayStat.setLong(5, rotateSpeedDay.getStatDate());
				stSaveRoatedistDayStat.setLong(6, rotateSpeedDay.getStatDate());
	
				stSaveRoatedistDayStat.setLong(7, rotateSpeedDay.getRotateSpeed0());
				stSaveRoatedistDayStat.setLong(8,
						rotateSpeedDay.getRotateSpeed0Time());
	
				stSaveRoatedistDayStat.setLong(9,
						rotateSpeedDay.getRotateSpeed0100());
				stSaveRoatedistDayStat.setLong(10,
						rotateSpeedDay.getRotateSpeed0100Time());
	
				stSaveRoatedistDayStat.setLong(11,
						rotateSpeedDay.getRotateSpeed100200());
				stSaveRoatedistDayStat.setLong(12,
						rotateSpeedDay.getRotateSpeed100200Time());
	
				stSaveRoatedistDayStat.setLong(13,
						rotateSpeedDay.getRotateSpeed200300());
				stSaveRoatedistDayStat.setLong(14,
						rotateSpeedDay.getRotateSpeed200300Time());
	
				stSaveRoatedistDayStat.setLong(15,
						rotateSpeedDay.getRotateSpeed300400());
				stSaveRoatedistDayStat.setLong(16,
						rotateSpeedDay.getRotateSpeed300400Time());
	
				stSaveRoatedistDayStat.setLong(17,
						rotateSpeedDay.getRotateSpeed400500());
				stSaveRoatedistDayStat.setLong(18,
						rotateSpeedDay.getRotateSpeed400500Time());
	
				stSaveRoatedistDayStat.setLong(19,
						rotateSpeedDay.getRotateSpeed500600());
				stSaveRoatedistDayStat.setLong(20,
						rotateSpeedDay.getRotateSpeed500600Time());
	
				stSaveRoatedistDayStat.setLong(21,
						rotateSpeedDay.getRotateSpeed600700());
				stSaveRoatedistDayStat.setLong(22,
						rotateSpeedDay.getRotateSpeed600700Time());
	
				stSaveRoatedistDayStat.setLong(23,
						rotateSpeedDay.getRotateSpeed700800());
				stSaveRoatedistDayStat.setLong(24,
						rotateSpeedDay.getRotateSpeed700800Time());
	
				stSaveRoatedistDayStat.setLong(25,
						rotateSpeedDay.getRotateSpeed800900());
				stSaveRoatedistDayStat.setLong(26,
						rotateSpeedDay.getRotateSpeed800900Time());
	
				stSaveRoatedistDayStat.setLong(27,
						rotateSpeedDay.getRotateSpeed9001000());
				stSaveRoatedistDayStat.setLong(28,
						rotateSpeedDay.getRotateSpeed9001000Time());
	
				stSaveRoatedistDayStat.setLong(29,
						rotateSpeedDay.getRotateSpeed10001100());
				stSaveRoatedistDayStat.setLong(30,
						rotateSpeedDay.getRotateSpeed10001100Time());
	
				stSaveRoatedistDayStat.setLong(31,
						rotateSpeedDay.getRotateSpeed11001200());
				stSaveRoatedistDayStat.setLong(32,
						rotateSpeedDay.getRotateSpeed11001200Time());
	
				stSaveRoatedistDayStat.setLong(33,
						rotateSpeedDay.getRotateSpeed12001300());
				stSaveRoatedistDayStat.setLong(34,
						rotateSpeedDay.getRotateSpeed12001300Time());
	
				stSaveRoatedistDayStat.setLong(35,
						rotateSpeedDay.getRotateSpeed13001400());
				stSaveRoatedistDayStat.setLong(36,
						rotateSpeedDay.getRotateSpeed13001400Time());
	
				stSaveRoatedistDayStat.setLong(37,
						rotateSpeedDay.getRotateSpeed14001500());
				stSaveRoatedistDayStat.setLong(38,
						rotateSpeedDay.getRotateSpeed14001500Time());
	
				stSaveRoatedistDayStat.setLong(39,
						rotateSpeedDay.getRotateSpeed15001600());
				stSaveRoatedistDayStat.setLong(40,
						rotateSpeedDay.getRotateSpeed15001600Time());
	
				stSaveRoatedistDayStat.setLong(41,
						rotateSpeedDay.getRotateSpeed16001700());
				stSaveRoatedistDayStat.setLong(42,
						rotateSpeedDay.getRotateSpeed16001700Time());
	
				stSaveRoatedistDayStat.setLong(43,
						rotateSpeedDay.getRotateSpeed17001800());
				stSaveRoatedistDayStat.setLong(44,
						rotateSpeedDay.getRotateSpeed17001800Time());
	
				stSaveRoatedistDayStat.setLong(45,
						rotateSpeedDay.getRotateSpeed18001900());
				stSaveRoatedistDayStat.setLong(46,
						rotateSpeedDay.getRotateSpeed18001900Time());
	
				stSaveRoatedistDayStat.setLong(47,
						rotateSpeedDay.getRotateSpeed19002000());
				stSaveRoatedistDayStat.setLong(48,
						rotateSpeedDay.getRotateSpeed19002000Time());
	
				stSaveRoatedistDayStat.setLong(49,
						rotateSpeedDay.getRotateSpeed20002100());
				stSaveRoatedistDayStat.setLong(50,
						rotateSpeedDay.getRotateSpeed20002100Time());
	
				stSaveRoatedistDayStat.setLong(51,
						rotateSpeedDay.getRotateSpeed21002200());
				stSaveRoatedistDayStat.setLong(52,
						rotateSpeedDay.getRotateSpeed21002200Time());
	
				stSaveRoatedistDayStat.setLong(53,
						rotateSpeedDay.getRotateSpeed22002300());
				stSaveRoatedistDayStat.setLong(54,
						rotateSpeedDay.getRotateSpeed22002300Time());
	
				stSaveRoatedistDayStat.setLong(55,
						rotateSpeedDay.getRotateSpeed23002400());
				stSaveRoatedistDayStat.setLong(56,
						rotateSpeedDay.getRotateSpeed23002400Time());
	
				stSaveRoatedistDayStat.setLong(57,
						rotateSpeedDay.getRotateSpeed24002500());
				stSaveRoatedistDayStat.setLong(58,
						rotateSpeedDay.getRotateSpeed24002500Time());
	
				stSaveRoatedistDayStat.setLong(59,
						rotateSpeedDay.getRotateSpeed25002600());
				stSaveRoatedistDayStat.setLong(60,
						rotateSpeedDay.getRotateSpeed25002600Time());
	
				stSaveRoatedistDayStat.setLong(61,
						rotateSpeedDay.getRotateSpeed26002700());
				stSaveRoatedistDayStat.setLong(62,
						rotateSpeedDay.getRotateSpeed26002700Time());
	
				stSaveRoatedistDayStat.setLong(63,
						rotateSpeedDay.getRotateSpeed27002800());
				stSaveRoatedistDayStat.setLong(64,
						rotateSpeedDay.getRotateSpeed27002800Time());
	
				stSaveRoatedistDayStat.setLong(65,
						rotateSpeedDay.getRotateSpeed28002900());
				stSaveRoatedistDayStat.setLong(66,
						rotateSpeedDay.getRotateSpeed28002900Time());
	
				stSaveRoatedistDayStat.setLong(67,
						rotateSpeedDay.getRotateSpeed29003000());
				stSaveRoatedistDayStat.setLong(68,
						rotateSpeedDay.getRotateSpeed29003000Time());
	
				stSaveRoatedistDayStat.setLong(69,
						rotateSpeedDay.getRotateSpeedMax());
				stSaveRoatedistDayStat.setLong(70,
						rotateSpeedDay.getRotateSpeedMaxTime());
	
				stSaveRoatedistDayStat.setLong(71,
						rotateSpeedDay.getPercent6080Fuhelv());
	
				stSaveRoatedistDayStat.setLong(72,
						rotateSpeedDay.getMinRotateSpeed());
				stSaveRoatedistDayStat.setLong(73,
						rotateSpeedDay.getMaxRotateSpeed());
				stSaveRoatedistDayStat.addBatch();
			}
			stSaveRoatedistDayStat.executeBatch();
		} catch (SQLException e) {
			logger.error("统计转速分析表出错。", e);
		} finally {
			if(null != stSaveRoatedistDayStat ){
				stSaveRoatedistDayStat.close();
			}
			if(null != dbCon){
				dbCon.close();
			}
		}
	}
	
	
	/**
	 * //蓄电池电压分布存储
	 * 
	 * @throws SQLException
	 */
	public  void saveVoltageDayStat(Connection dbCon,List<VoltagedistDay> list) throws SQLException {
		PreparedStatement stSaveVoltageDayStat = null;
		stSaveVoltageDayStat = dbCon
				.prepareStatement(sql_saveVoltageDayStat);
		try {
			for(VoltagedistDay voltagedistDay : list){
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(voltagedistDay.getVid());
				//VehicleInfo info = null;
				stSaveVoltageDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveVoltageDayStat.setString(2, voltagedistDay.getVid());
				if (info != null) {
					stSaveVoltageDayStat.setString(3, info.getVehicleNo());
					stSaveVoltageDayStat.setString(4, info.getVinCode());
				} else {
					stSaveVoltageDayStat.setString(3, null);
					stSaveVoltageDayStat.setString(4, null);
				}
				stSaveVoltageDayStat.setLong(5, voltagedistDay.getStatDate());
	
				stSaveVoltageDayStat.setLong(6, voltagedistDay.getVoltage0());
				stSaveVoltageDayStat.setLong(7, voltagedistDay.getVoltage0Time());
	
				stSaveVoltageDayStat.setLong(8, voltagedistDay.getVoltage020());
				stSaveVoltageDayStat.setLong(9, voltagedistDay.getVoltage020Time());
	
				stSaveVoltageDayStat.setLong(10, voltagedistDay.getVoltage20211());
				stSaveVoltageDayStat.setLong(11,
						voltagedistDay.getVoltage20211Time());
	
				stSaveVoltageDayStat.setLong(12, voltagedistDay.getVoltage20212());
				stSaveVoltageDayStat.setLong(13,
						voltagedistDay.getVoltage20212Time());
	
				stSaveVoltageDayStat.setLong(14, voltagedistDay.getVoltage21221());
				stSaveVoltageDayStat.setLong(15,
						voltagedistDay.getVoltage21221Time());
	
				stSaveVoltageDayStat.setLong(16, voltagedistDay.getVoltage21222());
				stSaveVoltageDayStat.setLong(17,
						voltagedistDay.getVoltage21222Time());
	
				stSaveVoltageDayStat.setLong(18, voltagedistDay.getVoltage22231());
				stSaveVoltageDayStat.setLong(19,
						voltagedistDay.getVoltage22231Time());
	
				stSaveVoltageDayStat.setLong(20, voltagedistDay.getVoltage22232());
				stSaveVoltageDayStat.setLong(21,
						voltagedistDay.getVoltage22232Time());
	
				stSaveVoltageDayStat.setLong(22, voltagedistDay.getVoltage23241());
				stSaveVoltageDayStat.setLong(23,
						voltagedistDay.getVoltage23241Time());
	
				stSaveVoltageDayStat.setLong(24, voltagedistDay.getVoltage23242());
				stSaveVoltageDayStat.setLong(25,
						voltagedistDay.getVoltage23242Time());
	
				stSaveVoltageDayStat.setLong(26, voltagedistDay.getVoltage24251());
				stSaveVoltageDayStat.setLong(27,
						voltagedistDay.getVoltage24251Time());
	
				stSaveVoltageDayStat.setLong(28, voltagedistDay.getVoltage24252());
				stSaveVoltageDayStat.setLong(29,
						voltagedistDay.getVoltage24252Time());
	
				stSaveVoltageDayStat.setLong(30, voltagedistDay.getVoltage25261());
				stSaveVoltageDayStat.setLong(31,
						voltagedistDay.getVoltage25261Time());
	
				stSaveVoltageDayStat.setLong(32, voltagedistDay.getVoltage25262());
				stSaveVoltageDayStat.setLong(33,
						voltagedistDay.getVoltage25262Time());
	
				stSaveVoltageDayStat.setLong(34, voltagedistDay.getVoltage26271());
				stSaveVoltageDayStat.setLong(35,
						voltagedistDay.getVoltage26271Time());
	
				stSaveVoltageDayStat.setLong(36, voltagedistDay.getVoltage26272());
				stSaveVoltageDayStat.setLong(37,
						voltagedistDay.getVoltage26272Time());
	
				stSaveVoltageDayStat.setLong(38, voltagedistDay.getVoltage27281());
				stSaveVoltageDayStat.setLong(39,
						voltagedistDay.getVoltage27281Time());
	
				stSaveVoltageDayStat.setLong(40, voltagedistDay.getVoltage27282());
				stSaveVoltageDayStat.setLong(41,
						voltagedistDay.getVoltage27282Time());
	
				stSaveVoltageDayStat.setLong(42, voltagedistDay.getVoltage28291());
				stSaveVoltageDayStat.setLong(43,
						voltagedistDay.getVoltage28291Time());
	
				stSaveVoltageDayStat.setLong(44, voltagedistDay.getVoltage28292());
				stSaveVoltageDayStat.setLong(45,
						voltagedistDay.getVoltage28292Time());
	
				stSaveVoltageDayStat.setLong(46, voltagedistDay.getVoltage29Max());
				stSaveVoltageDayStat.setLong(47,
						voltagedistDay.getVoltage29MaxTime());
	
				stSaveVoltageDayStat.setLong(48, voltagedistDay.getMax());
				stSaveVoltageDayStat.setLong(49, voltagedistDay.getMin());
				stSaveVoltageDayStat.setLong(50, voltagedistDay.getSumtime());
				stSaveVoltageDayStat.setLong(51, voltagedistDay.getSumcount());
	
				stSaveVoltageDayStat.setLong(52, voltagedistDay.getVoltage0121());
				stSaveVoltageDayStat.setLong(53,
						voltagedistDay.getVoltage0121Time());
	
				stSaveVoltageDayStat.setLong(54, voltagedistDay.getVoltage0122());
				stSaveVoltageDayStat.setLong(55,
						voltagedistDay.getVoltage0122Time());
	
				stSaveVoltageDayStat.setLong(56, voltagedistDay.getVoltage12131());
				stSaveVoltageDayStat.setLong(57,
						voltagedistDay.getVoltage12131Time());
	
				stSaveVoltageDayStat.setLong(58, voltagedistDay.getVoltage12132());
				stSaveVoltageDayStat.setLong(59,
						voltagedistDay.getVoltage12132Time());
	
				stSaveVoltageDayStat.setLong(60, voltagedistDay.getVoltage13141());
				stSaveVoltageDayStat.setLong(61,
						voltagedistDay.getVoltage13141Time());
	
				stSaveVoltageDayStat.setLong(62, voltagedistDay.getVoltage13142());
				stSaveVoltageDayStat.setLong(63,
						voltagedistDay.getVoltage13142Time());
	
				stSaveVoltageDayStat.setLong(64, voltagedistDay.getVoltage141());
				stSaveVoltageDayStat
						.setLong(65, voltagedistDay.getVoltage141Time());
	
				stSaveVoltageDayStat.setLong(66, voltagedistDay.getVoltage14Max());
				stSaveVoltageDayStat.setLong(67,
						voltagedistDay.getVoltage14MaxTime());
				stSaveVoltageDayStat.addBatch();
			}
			stSaveVoltageDayStat.executeBatch();
		} catch (SQLException e) {
			logger.error("统计蓄电池电压出错.", e);
		} finally {
			if(null != stSaveVoltageDayStat ){
				stSaveVoltageDayStat.close();
			}
			if(null != dbCon ){
				dbCon.close();
			}
		}
	}
	
	/*****
	 * 存储进气温度
	 * 
	 * @param vid
	 */
	public  void saveAirTemperture(Connection dbCon,List<AirTempertureBean> list) throws Exception{
		PreparedStatement stSaveAirTempertureStat = null;
		stSaveAirTempertureStat = dbCon
				.prepareStatement(sql_saveAirTemperture);
		try {
			for(AirTempertureBean airTempertureBean : list){
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(airTempertureBean.getVid());
				//VehicleInfo info = null;
				stSaveAirTempertureStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveAirTempertureStat.setString(2, airTempertureBean.getVid());
				if (info != null) {
					stSaveAirTempertureStat.setString(3, info.getVehicleNo());
					stSaveAirTempertureStat.setString(4, info.getVinCode());
				} else {
					stSaveAirTempertureStat.setString(3, null);
					stSaveAirTempertureStat.setString(4, null);
				}
				stSaveAirTempertureStat.setLong(5, airTempertureBean.getStatDate());
				stSaveAirTempertureStat.setLong(6, airTempertureBean
						.getTemperature_0().getNum());
				stSaveAirTempertureStat.setLong(7, airTempertureBean
						.getTemperature_0().getTime());
				stSaveAirTempertureStat.setLong(8, airTempertureBean
						.getTemperature_0_10().getNum());
				stSaveAirTempertureStat.setLong(9, airTempertureBean
						.getTemperature_0_10().getTime());
				stSaveAirTempertureStat.setLong(10, airTempertureBean
						.getTemperature_10_20().getNum());
				stSaveAirTempertureStat.setLong(11, airTempertureBean
						.getTemperature_10_20().getTime());
				stSaveAirTempertureStat.setLong(12, airTempertureBean
						.getTemperature_20_25().getNum());
				stSaveAirTempertureStat.setLong(13, airTempertureBean
						.getTemperature_20_25().getTime());
				stSaveAirTempertureStat.setLong(14, airTempertureBean
						.getTemperature_25_30().getNum());
				stSaveAirTempertureStat.setLong(15, airTempertureBean
						.getTemperature_25_30().getTime());
				stSaveAirTempertureStat.setLong(16, airTempertureBean
						.getTemperature_30_35().getNum());
				stSaveAirTempertureStat.setLong(17, airTempertureBean
						.getTemperature_30_35().getTime());
				stSaveAirTempertureStat.setLong(18, airTempertureBean
						.getTemperature_35_40().getNum());
				stSaveAirTempertureStat.setLong(19, airTempertureBean
						.getTemperature_35_40().getTime());
				stSaveAirTempertureStat.setLong(20, airTempertureBean
						.getTemperature_40_45().getNum());
				stSaveAirTempertureStat.setLong(21, airTempertureBean
						.getTemperature_40_45().getTime());
				stSaveAirTempertureStat.setLong(22, airTempertureBean
						.getTemperature_45_50().getNum());
				stSaveAirTempertureStat.setLong(23, airTempertureBean
						.getTemperature_45_50().getTime());
				stSaveAirTempertureStat.setLong(24, airTempertureBean
						.getTemperature_50_60().getNum());
				stSaveAirTempertureStat.setLong(25, airTempertureBean
						.getTemperature_50_60().getTime());
				stSaveAirTempertureStat.setLong(26, airTempertureBean
						.getTemperature_60_70().getNum());
				stSaveAirTempertureStat.setLong(27, airTempertureBean
						.getTemperature_60_70().getTime());
				stSaveAirTempertureStat.setLong(28, airTempertureBean
						.getTemperature_70().getNum());
				stSaveAirTempertureStat.setLong(29, airTempertureBean
						.getTemperature_70().getTime());
				stSaveAirTempertureStat.setLong(30, airTempertureBean.getMax());
				stSaveAirTempertureStat.setLong(31, airTempertureBean.getMin());
				stSaveAirTempertureStat.addBatch();
			}
			stSaveAirTempertureStat.executeBatch();
		} catch (SQLException e) {
			logger.error("统计进气温度出错", e);
		} finally {
			if(null != stSaveAirTempertureStat ){
				stSaveAirTempertureStat.close();
			}
			if(null != dbCon ){
				dbCon.close();
			}
		}
	}
	
	

	/**
	 * 查询驾驶员在给定时段内的打卡明细
	 * @param dbCon
	 * @param vid
	 * @param beginUtc
	 * @param endUtc
	 * @return
	 * @throws Exception
	 */
	public List<String> selectDriverClockinDetail(String vid,long beginUtc,long endUtc) throws Exception{
		PreparedStatement stSelectClockin = null;
		Connection dbCon = null;
		ResultSet rs = null;
		List<String> ls = new ArrayList<String>();
		 try {
			 dbCon= OracleConnectionPool.getConnection();
			 stSelectClockin = dbCon.prepareStatement(sql_queryDriverClockinDetail);
			 stSelectClockin.setString(1, vid);
			 stSelectClockin.setLong(2, beginUtc);
			 stSelectClockin.setLong(3, endUtc);
			 stSelectClockin.setLong(4, beginUtc);
			 stSelectClockin.setLong(5, endUtc);
			 stSelectClockin.setString(6, vid);
			 
			 rs = stSelectClockin.executeQuery();
			 while(rs.next()){
				 ls.add(rs.getString("DEVICE_ID") + ";" + rs.getString("DRIVER_NAME") + ";"+ rs.getString("DRIVER_SRC") + ";" + rs.getLong("ON_LINE_TIME") + ";" +  rs.getLong("OFF_LINE_TIME"));
			 }
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			if(rs != null){
				rs.close();
			}
			if(stSelectClockin != null){
				stSelectClockin.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		return ls;
	}
	

	/**
	 * 更新里程累计表
	 * @throws SQLException
	 */
	public  void updateVehicleStat(Long utc) throws SQLException{
		PreparedStatement stUpdateStaInfo = null;
		PreparedStatement stSelectStatInfo = null;
		ResultSet rs = null;
		Connection dbCon = null;
		try{
			dbCon = OracleConnectionPool.getConnection();
			stSelectStatInfo = dbCon.prepareStatement(sql_queryTotalMileageInfo);
			stUpdateStaInfo = dbCon.prepareStatement(sql_updateStaInfo);
			stSelectStatInfo.setLong(1, utc);
			int count = 0;
			rs = stSelectStatInfo.executeQuery();
			while(rs.next()){
				stUpdateStaInfo.setLong(1, rs.getLong("MILEAGE"));
				stUpdateStaInfo.setString(2, rs.getString("VID"));
				stUpdateStaInfo.addBatch();
				count++;
				if(count % 100 == 0){
					stUpdateStaInfo.executeBatch();
					count = 0;
					stUpdateStaInfo.clearBatch();
				}
			}
			
			if(count >0){
				stUpdateStaInfo.executeBatch(); // 更新车辆总累计表
			}
		}catch (Exception e) {
			logger.debug("更新总里程出错！",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stSelectStatInfo != null){
				stSelectStatInfo.close();
			}
			
			if(stUpdateStaInfo != null){
				stUpdateStaInfo.close();
			}
			
			if(dbCon != null){
				dbCon.close();
			}
		}
	}
	/**
	 * 
	 * 行驶里程数据删除
	 * @param utc
	 */
	public  void deleteRestoreInfo(long utc){
		Connection dbCon = null;
		PreparedStatement stdeleteVehicleInfo = null;
		PreparedStatement stdeleteDriverInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
		
			stdeleteVehicleInfo = dbCon.prepareStatement(sql_deleteVehicleInfo);
			stdeleteVehicleInfo.setLong(1, utc);
			stdeleteVehicleInfo.execute();
			
			stdeleteDriverInfo = dbCon.prepareStatement(sql_deleteDriverInfo);
			stdeleteDriverInfo.setLong(1, utc);
			stdeleteDriverInfo.execute();
			logger.info("--------------------------------行驶里程补录数据删除成功");
		} catch (SQLException e) {
			logger.debug("里程统计补录删除数据异常！",e);
		}finally{
			try {
				if(stdeleteVehicleInfo != null){	
					stdeleteVehicleInfo.close();			
				}
				if(stdeleteDriverInfo != null){	
					stdeleteDriverInfo.close();			
				}
				if(dbCon != null){
					dbCon.close();
				}
			} catch (SQLException ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
		
	}
	
	/**
	 * 
	 * 车辆运行统计数据删除
	 * @param utc
	 */
	public  void deleteRunningInfo(long utc){
		Connection dbCon = null;
		PreparedStatement stdeleteVehicleInfo = null;
		PreparedStatement stdeleteDriverInfo = null;
		PreparedStatement stdeleteOilInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
		
			stdeleteVehicleInfo = dbCon.prepareStatement(sql_deleteRunningInfo);
			stdeleteVehicleInfo.setLong(1, utc);
			stdeleteVehicleInfo.execute();
			
			stdeleteDriverInfo = dbCon.prepareStatement(sql_deleteStopStartInfo);
			stdeleteDriverInfo.setLong(1, utc-12*60*1000);
			stdeleteDriverInfo.setLong(2, utc+12*60*1000);
			stdeleteDriverInfo.execute();
			
			stdeleteOilInfo = dbCon.prepareStatement(sql_deleteOilSaveInfo);
			stdeleteOilInfo.setLong(1, utc);
			stdeleteOilInfo.execute();
			logger.info("--------------------------------车辆运行状态统计补录数据删除成功");
		} catch (SQLException e) {
			logger.debug("车辆运行状态统计补录删除数据异常！",e);
		}finally{
			try {
				if(stdeleteVehicleInfo != null){	
					stdeleteVehicleInfo.close();			
				}
				if(stdeleteDriverInfo != null){	
					stdeleteDriverInfo.close();			
				}
				if(stdeleteOilInfo != null){	
					stdeleteOilInfo.close();			
				}
				if(dbCon != null){
					dbCon.close();
				}
			} catch (SQLException ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
		
	}
	
	/**
	 * 
	 * 油箱油量监控统计数据删除
	 * @param utc
	 */
	public  void deleteOilInfo(long utc){
		Connection dbCon = null;
		PreparedStatement stdeleteVehicleInfo = null;
		PreparedStatement stdeleteDriverInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
		
			stdeleteVehicleInfo = dbCon.prepareStatement(sql_deleteOilWearInfo);
			stdeleteVehicleInfo.setLong(1, utc-12*60*1000);
			stdeleteVehicleInfo.setLong(2, utc+12*60*1000);
			stdeleteVehicleInfo.execute();
			
		/*	stdeleteDriverInfo = dbCon.prepareStatement(sql_deleteOilChangedInfo);
			stdeleteDriverInfo.setLong(1, utc-12*60*1000);
			stdeleteDriverInfo.setLong(2, utc+12*60*1000);
			stdeleteDriverInfo.execute();*/
			logger.info("------------------油箱油量监控补录数据删除成功");
		} catch (SQLException e) {
			logger.debug("油箱油量监控补录删除数据异常！",e);
		}finally{
			try {
				if(stdeleteVehicleInfo != null){	
					stdeleteVehicleInfo.close();			
				}
				if(stdeleteDriverInfo != null){	
					stdeleteDriverInfo.close();			
				}
				if(dbCon != null){
					dbCon.close();
				}
			} catch (SQLException ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
		
	}
	
	/**
	 * 
	 * 单车分析报告统计数据删除
	 * @param utc
	 */
	public  void deleteReportInfo(long utc){
		Connection dbCon = null;
		PreparedStatement stdeleteVehicleInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
		
			stdeleteVehicleInfo = dbCon.prepareStatement(sql_deleteReportInfo);
			stdeleteVehicleInfo.setLong(1, utc);
			stdeleteVehicleInfo.setLong(2, utc);
			stdeleteVehicleInfo.setLong(3, utc);
			stdeleteVehicleInfo.setLong(4, utc);
			stdeleteVehicleInfo.setLong(5, utc);
			stdeleteVehicleInfo.setLong(6, utc);
			stdeleteVehicleInfo.setLong(7, utc);		
			stdeleteVehicleInfo.execute();

			logger.info("--------------------------------单车分析报告补录数据删除成功");
		} catch (SQLException e) {
			logger.debug("单车分析报告补录删除数据异常！",e);
		}finally{
			try {
				if(stdeleteVehicleInfo != null){	
					stdeleteVehicleInfo.close();			
				}
				if(dbCon != null){
					dbCon.close();
				}
			} catch (SQLException ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
		
	}
	
	/***
	 *  存储驾驶员驾驶明细数据
	 * @throws SQLException
	 */
	public  void saveDriverDetail(Connection connection,List<DriverDetailBean> driverDetaillist) throws Exception{
		PreparedStatement stSaveDriverDetailInfo = null;
		try{
			
			stSaveDriverDetailInfo = connection.prepareStatement(sql_saveDriverDetailInfo);
			
			for (int i = 0;i < driverDetaillist.size(); i++){
				DriverDetailBean ddb = driverDetaillist.get(i);
				stSaveDriverDetailInfo.setString(1, ddb.getDetailId());
				stSaveDriverDetailInfo.setLong(2, ddb.getStatDate()); // 日期UTC
				stSaveDriverDetailInfo.setString(3, ddb.getVid()); // 车辆编号
				
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(ddb.getVid());
				String cfgFlag = "0";
				if (info!=null){
					cfgFlag = info.getCfgFlag();
				}
				if(info != null){
					stSaveDriverDetailInfo.setString(4, info.getVehicleNo()); // 车牌号码
					stSaveDriverDetailInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDriverDetailInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDriverDetailInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDriverDetailInfo.setString(8, info.getTeamName()); // 车队名称
				} else {
					stSaveDriverDetailInfo.setString(4, null); // 车牌号码
					stSaveDriverDetailInfo.setString(5, null); // 企业ID
					stSaveDriverDetailInfo.setString(6, null); // 企业名称
					stSaveDriverDetailInfo.setString(7, null); // 车队ID
					stSaveDriverDetailInfo.setString(8, null); // 车队名称
				}

				VehicleMessageBean beginVmb = ddb.getBeginVmb();
				
				stSaveDriverDetailInfo.setString(9, beginVmb.getDriverId()); // 驾驶员编号
				stSaveDriverDetailInfo.setString(10, beginVmb.getDriverName()); // 驾驶员名称
				stSaveDriverDetailInfo.setString(11, beginVmb.getDriverSrc()); // 驾驶员信息来源
				
				stSaveDriverDetailInfo.setLong(12, beginVmb.getUtc()); // 开始时间
				stSaveDriverDetailInfo.setLong(13, beginVmb.getLon()); // 经度
				stSaveDriverDetailInfo.setLong(14, beginVmb.getLat()); // 维度
				stSaveDriverDetailInfo.setLong(15, beginVmb.getMaplon()); // 地图经度
				stSaveDriverDetailInfo.setLong(16, beginVmb.getMaplat()); // 地图维度
				stSaveDriverDetailInfo.setLong(17, beginVmb.getMileage()); //里程
				
				VehicleMessageBean endVmb = ddb.getEndVmb();
				stSaveDriverDetailInfo.setLong(18, endVmb.getUtc()); // 开始时间
				stSaveDriverDetailInfo.setLong(19, endVmb.getLon()); // 经度
				stSaveDriverDetailInfo.setLong(20, endVmb.getLat()); // 维度
				stSaveDriverDetailInfo.setLong(21, endVmb.getMaplon()); // 地图经度
				stSaveDriverDetailInfo.setLong(22, endVmb.getMaplat()); // 地图维度
				stSaveDriverDetailInfo.setLong(23, endVmb.getMileage()); //里程
				
				stSaveDriverDetailInfo.setLong(24, ddb.getMileage()); //驾驶员驾驶里程
				
				if("0".equals(cfgFlag)||"3".equals(cfgFlag)){
					stSaveDriverDetailInfo.setLong(25, ddb.getEcuOilWear()); //ecu油耗
					stSaveDriverDetailInfo.setLong(26, ddb.getEcuRunningOilWear()); //行车
				}else if("2".equals(cfgFlag)){ //传感器油耗
					String value = ThreadPool.oilWearMap.get(ddb.getVid());
					if(!"".equals(value) &&  value!= null){
						stSaveDriverDetailInfo.setString(25, value.split(":")[0]);
						stSaveDriverDetailInfo.setString(26, value.split(":")[1]);
					}else{
						stSaveDriverDetailInfo.setString(25, null);
						stSaveDriverDetailInfo.setString(26, null);
					}
				}else{
					stSaveDriverDetailInfo.setDouble(25, ddb.getMetOilWear()*0.02); //精准油耗
					stSaveDriverDetailInfo.setDouble(26, ddb.getMetRunningOilWear()*0.02); //行车
				}			
				stSaveDriverDetailInfo.setDouble(27, ddb.getMetOilWear()*0.02); //精准油耗
				stSaveDriverDetailInfo.setDouble(28, ddb.getMetRunningOilWear()*0.02); //行车
				
				stSaveDriverDetailInfo.setLong(29, ddb.getEngineRotateTime()); //发动机时长
				stSaveDriverDetailInfo.setLong(30, ddb.getRunningTime()); //行车时长
				stSaveDriverDetailInfo.setLong(31, ddb.getEngineRotateTime()-ddb.getRunningTime()); //怠速时长
				
				stSaveDriverDetailInfo.setString(32, cfgFlag); //油耗来源
				
				stSaveDriverDetailInfo.setLong(33, ddb.getOverSpeedNum()); //超速次数
				stSaveDriverDetailInfo.setLong(34, ddb.getOverSpeedTime()); //超速时长
				stSaveDriverDetailInfo.setLong(35, ddb.getOverRpmNum()); //超转次数
				stSaveDriverDetailInfo.setLong(36, ddb.getOverRpmTime()); //超转时长
				stSaveDriverDetailInfo.setLong(37, ddb.getUrgentSpeedNuM()); //急加速次数
				stSaveDriverDetailInfo.setLong(38, ddb.getUrgentSpeedTime()); //急加速时长
				stSaveDriverDetailInfo.setLong(39, ddb.getUrgentLowdownNum()); //急减速次数
				stSaveDriverDetailInfo.setLong(40, ddb.getUrgentLowdownTime()); //急减速时长
				stSaveDriverDetailInfo.setLong(41, ddb.getLongIdleNum()); //超长怠速次数
				stSaveDriverDetailInfo.setLong(42, ddb.getLongIdleTime()); //超长怠速时长
				stSaveDriverDetailInfo.setLong(43, ddb.getIdleAirConditionNum()); //怠速空调次数
				stSaveDriverDetailInfo.setLong(44, ddb.getIdleAirConditionTime()); //怠速空调时长
				stSaveDriverDetailInfo.setLong(45, ddb.getGearGlideNum()); //空挡滑行次数
				stSaveDriverDetailInfo.setLong(46, ddb.getGearGlideTime()); //空挡滑行时长
				stSaveDriverDetailInfo.setLong(47, ddb.getAirConditionNum()); //空调总次数
				stSaveDriverDetailInfo.setLong(48, ddb.getAirConditionTime()); //空调总时长
				stSaveDriverDetailInfo.setLong(49, ddb.getHeatUpTime()); //暖风时长
				stSaveDriverDetailInfo.setLong(50, ddb.getEconomicRunTime()); //超经济区运行时长			
				
				stSaveDriverDetailInfo.addBatch();
			}
			
			stSaveDriverDetailInfo.executeBatch();
		}catch (Exception e) {
			logger.debug("驾驶员明细录入出错", e);
		}finally{
			try{
				if (stSaveDriverDetailInfo != null) {
					stSaveDriverDetailInfo.close();
				}
				if (connection != null) {
					connection.close();
				}
				}catch(Exception ex){
					logger.error("释放数据库连接出错.",ex);
				}
		}
	}



			
	/**
	 * 驾驶员存储月统计信息
	 * 
	 * @throws SQLException
	 * 
	 */
	public void saveDriverMonthInfo(long startMonthUtc, long endMonthUc,
			String month, String year) throws SQLException {
		Connection dbCon = OracleConnectionPool.getConnection();
		PreparedStatement stSaveStatMonthInfo = null;
		PreparedStatement stQueryStatDayInfo = null;
		ResultSet rs = null;
		String vid = "";
		try {
			stSaveStatMonthInfo = dbCon.prepareStatement(sql_saveMonthDriverDetailInfo);
			stQueryStatDayInfo = dbCon.prepareStatement(sql_queryDriverDetailInfo);
			stQueryStatDayInfo.setLong(1, startMonthUtc);// 获取上一个月1号凌晨时间
			stQueryStatDayInfo.setLong(2, endMonthUc);// 获取当前月1号凌晨时间
			rs = stQueryStatDayInfo.executeQuery();
			int count = 0;
			while (rs.next()) {
				try {
					stSaveStatMonthInfo.setLong(1, Integer.parseInt(year)); // 统计月
					stSaveStatMonthInfo.setLong(2, Integer.parseInt(month)); // 统计月
					stSaveStatMonthInfo.setString(3, rs.getString("VID")); // 车辆编号
					stSaveStatMonthInfo.setString(4, rs.getString("VEHICLE_NO")); // 车牌号码					
					stSaveStatMonthInfo.setString(5,rs.getString("CORP_ID")); // 企业ID
					stSaveStatMonthInfo.setString(6, rs.getString("CORP_NAME")); // 企业名称
					stSaveStatMonthInfo.setString(7, rs.getString("TEAM_ID")); // 车队ID
					stSaveStatMonthInfo.setString(8, rs.getString("TEAM_NAME")); // 车队名称
					stSaveStatMonthInfo.setString(9, rs.getString("DRIVER_ID")); // 驾驶员ID
					stSaveStatMonthInfo.setString(10, rs.getString("DRIVER_NAME")); // 驾驶员姓名
					stSaveStatMonthInfo.setString(11, rs.getString("DRIVER_SRC")); // 驾驶员来源

					stSaveStatMonthInfo.setLong(12, rs.getLong(10)); // 总里程
					stSaveStatMonthInfo.setLong(13, rs.getLong(11)); // ECU总油耗
					stSaveStatMonthInfo.setLong(14, rs.getLong(12)); // ECU行车油耗
					stSaveStatMonthInfo.setLong(15, rs.getLong(13)); // 精准总油耗
					stSaveStatMonthInfo.setLong(16, rs.getLong(14)); //  精准行车油耗
					stSaveStatMonthInfo.setLong(17, rs.getLong(15)); //  发动机总时间
					stSaveStatMonthInfo.setLong(18, rs.getLong(16)); // 行车时间
					stSaveStatMonthInfo.setLong(19, rs.getLong(17)); // 怠速时间
					
					stSaveStatMonthInfo.setLong(20, rs.getLong(18)); // 油耗来源
					
					stSaveStatMonthInfo.setLong(21, rs.getLong(19)); 
					stSaveStatMonthInfo.setLong(22, rs.getLong(20)); 
					stSaveStatMonthInfo.setLong(23, rs.getLong(21)); 
					stSaveStatMonthInfo.setLong(24, rs.getLong(22)); 
					stSaveStatMonthInfo.setLong(25, rs.getLong(23)); 
					stSaveStatMonthInfo.setLong(26, rs.getLong(24)); 
					stSaveStatMonthInfo.setLong(27, rs.getLong(25)); 
					stSaveStatMonthInfo.setLong(28, rs.getLong(26)); 
					stSaveStatMonthInfo.setLong(29, rs.getLong(27)); 
					stSaveStatMonthInfo.setLong(30, rs.getLong(28)); 
					stSaveStatMonthInfo.setLong(31, rs.getLong(29)); 
					stSaveStatMonthInfo.setLong(32, rs.getLong(30)); 
					stSaveStatMonthInfo.setLong(33, rs.getLong(31)); 
					stSaveStatMonthInfo.setLong(34, rs.getLong(32)); 
					stSaveStatMonthInfo.setLong(35, rs.getLong(33)); 
					stSaveStatMonthInfo.setLong(36, rs.getLong(34)); 
					stSaveStatMonthInfo.setLong(37, rs.getLong(35)); 
					stSaveStatMonthInfo.setLong(38, rs.getLong(36)); 
					
					stSaveStatMonthInfo.addBatch();
					
					count++;
					
					if(count % 1000 == 0){
						stSaveStatMonthInfo.executeBatch();
						stSaveStatMonthInfo.clearBatch();
						count = 0;
					}

				} catch (SQLException ex) {
					logger.error("驾驶员月统计出错." + vid, ex);
				}
			}

			if (count > 0) {
				stSaveStatMonthInfo.executeBatch();
				stSaveStatMonthInfo.clearBatch();
				count = 0;
			}
		} catch (SQLException e) {
			logger.error("驾驶员月统计出错." + vid, e);
		} finally {
			if (rs != null) {
				rs.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
			if (stSaveStatMonthInfo != null) {
				stSaveStatMonthInfo.close();
			}

			if (stQueryStatDayInfo != null) {
				stQueryStatDayInfo.close();
			}
		}
	}
	
	/**
	 * 删除驾驶员月统计
	 * @param utc
	 * @throws SQLException 
	 */
	public  void deleteRestoreMonthInfo(String utc) throws SQLException{
		Connection dbCon = null;
		PreparedStatement stDeleteStatMonthInfo = null;
		PreparedStatement stDeleteOilMonthInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			stDeleteStatMonthInfo = dbCon.prepareStatement(sql_delRestoreMonthInfo);
			stDeleteOilMonthInfo = dbCon.prepareStatement(sql_delOilMonthInfo);
			int year  = 0;
			int month = 0;
			if("".equals(utc) || utc != null){
				year = Integer.parseInt(utc.split("/")[0]);
				month = Integer.parseInt(utc.split("/")[1]);
			}
			stDeleteStatMonthInfo.setInt(1, year);
			stDeleteStatMonthInfo.setInt(2, month);
			stDeleteOilMonthInfo.setInt(1, year);
			stDeleteOilMonthInfo.setInt(2, month);
			
			stDeleteStatMonthInfo.execute();
			stDeleteOilMonthInfo.execute();
		}catch (Exception e) {
			logger.error("车辆运行月统计删除出错.", e);
		}finally {
			if(dbCon != null){
				dbCon.close();
			}
			if (stDeleteStatMonthInfo != null) {
				stDeleteStatMonthInfo.close();
			}
			if (stDeleteOilMonthInfo != null) {
				stDeleteOilMonthInfo.close();
			}
		}
		
	}
	
	/***
	 *  存储车辆行驶里程信息
	 * @throws SQLException
	 */
	public  void saveStaMileageInfo(Connection dbCon,List<VehicleStatus> list) throws Exception{
		PreparedStatement stSaveDayStInfo = null;
		stSaveDayStInfo = dbCon.prepareStatement(sql_saveDayMileageStInfo);
		String vid = "";
		try{	
			for(VehicleStatus vehicleStatus : list){
				vid = vehicleStatus.getVid();
				VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(vid);	
				stSaveDayStInfo.setLong(1, vehicleStatus.getStatDate()); // 日期UTC
				stSaveDayStInfo.setString(2, vehicleStatus.getVid()); // 车辆编号
				if(info != null){
					stSaveDayStInfo.setString(3, info.getVehicleNo()); // 车牌号码
					stSaveDayStInfo.setString(4, info.getVinCode()); // 车架号(VIN)
					stSaveDayStInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDayStInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDayStInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDayStInfo.setString(8, info.getTeamName()); // 车队名称		
					stSaveDayStInfo.setLong(9, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程			
					stSaveDayStInfo.setLong(10, vehicleStatus.getGis_milege()); // GIS计算里程		
					stSaveDayStInfo.setLong(11, vehicleStatus.getMileage());//里程
					stSaveDayStInfo.setLong(12, System.currentTimeMillis());//utc
					stSaveDayStInfo.addBatch();
				}
			}
			stSaveDayStInfo.executeBatch(); //车辆上报里程
		}catch(SQLException e){
			logger.debug("存储车辆行驶里程统计出错：VID="+vid+" ",e);
		}finally{
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		
		
	}
	
	
	/***
	 *  更新车辆总里程信息
	 * @throws SQLException
	 */
	public  void updateTotalMileageInfo(Connection dbCon,List<VehicleStatus> list) throws Exception{
		PreparedStatement stUpdateMileagetInfo = null;
		stUpdateMileagetInfo = dbCon.prepareStatement(sql_updateStaInfo);
		String vid = "";
		try{	
			for(VehicleStatus vehicleStatus : list){
				vid = vehicleStatus.getVid();
				stUpdateMileagetInfo.setLong(1,vehicleStatus.getTotalMileage());//总里程
				stUpdateMileagetInfo.setString(2,vehicleStatus.getVid());//vid
				stUpdateMileagetInfo.addBatch();

			}
			stUpdateMileagetInfo.executeBatch();//总里程
		}catch(SQLException e){
			logger.debug("更新车辆总里程统计出错：VID="+vid+" ",e);
		}finally{
			if(stUpdateMileagetInfo != null){
				stUpdateMileagetInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		
		
	}
	
	
	/***
	 *  加载传感器油耗信息
	 * @throws SQLException
	 */
	public  void loadingOilMap(long utc) throws Exception{
		ThreadPool.oilWearMap.clear(); //先清空以前数据
		int count =0;
		ResultSet rs = null;
		Connection dbCon = OracleConnectionPool.getConnection();
		PreparedStatement stUpdateMileagetInfo = null;
		stUpdateMileagetInfo = dbCon.prepareStatement(sql_loadingOilMap);
		stUpdateMileagetInfo.setLong(1, utc+12*60*60*1000);
		rs = stUpdateMileagetInfo.executeQuery();	
		try{
			while(rs.next()){
				String key = rs.getString("VID");
				String value = Double.parseDouble(rs.getString("USEOIL_VOLUME"))*0.1+":"+Double.parseDouble(rs.getString("RUNNINGOIL_VOLUME"))*0.1;
				ThreadPool.oilWearMap.put(key, value);
				count ++;
			}
			
			logger.info("--------------------------------传感器油耗缓存加载成功！数目：[{}]",count);
		}catch(SQLException e){
			logger.debug("加载传感器油耗缓存出错！",e);
		}finally{
			if(stUpdateMileagetInfo != null){
				stUpdateMileagetInfo.close();
			}
			if(rs != null){
				rs.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		
		
	}
	

	

}
