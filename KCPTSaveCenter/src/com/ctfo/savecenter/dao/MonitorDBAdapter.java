package com.ctfo.savecenter.dao;



import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleResultSet;


import com.ctfo.savecenter.beans.AlarmTypeBean;
import com.ctfo.savecenter.beans.ServiceUnit;
import com.ctfo.savecenter.beans.StatusBean;
import com.ctfo.savecenter.beans.StatusCode;
import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.lingtu.xmlconf.XmlConf;

/**
 * 数据库访问类
 */
public class MonitorDBAdapter extends DBAdapter {

	private static final Logger logger = LoggerFactory.getLogger(MonitorDBAdapter.class);
	
	public static long statuscount=0;

	private static String queryVehicleStatusCode;

	/** 根据macid查询车辆基本信息 **/
	private static String queryVehicleByMacid = null;

	private static String queryAlarmType = null;
	private static String queryDeleteVehicle = null;

	private static String sql_corpHasAlarmLevel = null;
	
	private static String sql_corpToListVid = null;
	
	private static String sql_corpToListAlarm = null;
	/** 更新3g号对应车辆信息SQL */
	private static String update3GPhotoVehicleInfoSql = null;
	
	/**
	 * 
	 * 
	 * @param dbDriver
	 *            数据库驱动
	 * @param dbConString
	 *            数据库连接字
	 * @param dbUserName
	 *            数据库用户名
	 * @param dbPassword
	 *            数据库密码
	 * @param reconnectWait
	 *            数据库断线重新连接时间(秒)
	 */
	public void initDBAdapter(XmlConf config) throws Exception {
		this.config = config;
		// 根据macid查询车辆基本信息
		queryVehicleByMacid = config.getStringValue("database|sqlstatement|sql_queryVehicleByMacid");

		// 查询实体ID所属企业，车队
		String queryAllVehicle = config
				.getStringValue("database|sqlstatement|sql_queryAllVehicle");
		if (TempMemory.getVehicleMapSize() == 0)
			queryAllVehicle(queryAllVehicle);

		// 查询车辆状态编码
		queryVehicleStatusCode = config.getStringValue("database|sqlstatement|sql_queryVehicleStatusCode");

		// 查询报警编码
		queryAlarmType = config.getStringValue("database|sqlstatement|sql_queryAlarmType");
		queryAlarmType(queryAlarmType);
		
		
		update3GPhotoVehicleInfoSql = config.getStringValue("database|sqlstatement|update3GPhotoVehicleInfoSql");
		update3GPhotoVehicleInfo();
		
		// 查询已删除车辆
		queryDeleteVehicle = config
				.getStringValue("database|sqlstatement|sql_queryDeleteVehicle");
		
		// 查询有告警设置企业列表
		sql_corpHasAlarmLevel = config.getStringValue("database|sqlstatement|sql_corpHasAlarmLevel");
		
		// 查询设置告警等级企业对应车辆LIST
		sql_corpToListVid = config.getStringValue("database|sqlstatement|sql_corpToListVid");
		
		// 查询设置企业对应报警列表
		sql_corpToListAlarm = config.getStringValue("database|sqlstatement|sql_corpToListAlarm");
		
//		initAlarmSetting();
		updateVehicleAlarmSetting();
	}

	/**
	 * 查询已删除车辆
	 * 
	 * @return
	 * @throws SQLException
	 */
	public static Map<Long, Long> queryDeleteVehicle(long time) {

		OracleConnection conn = null;
		OracleResultSet rs = null;
		OraclePreparedStatement stQueryDeleteVehicle = null;
		Map<Long, Long> map = new HashMap<Long, Long>();
		try {

			// 从连接池获得连接
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryDeleteVehicle = (OraclePreparedStatement)conn.prepareStatement(queryDeleteVehicle);

			stQueryDeleteVehicle.setLong(1, time);
			rs = (OracleResultSet)stQueryDeleteVehicle.executeQuery();
			while (rs.next()) {
				map.put(rs.getLong("vid"), 0L);
			}
			return map;
		} catch (Exception e) {
			logger.error("查询已删除车辆出错."+ e.getMessage());
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryDeleteVehicle != null) {
					stQueryDeleteVehicle.close();

				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询车机出错."+ e.getMessage());
			}
		}

		return null;
	}

	/**
	 * 根据一个车牌号码和车牌颜色查询车辆编号
	 * 
	 * @param macid
	 * @return
	 * @throws SQLException
	 */
	public static ServiceUnit queryVehicleByMacid(String macid) {

		OracleConnection conn = null;
		OracleResultSet rs = null;
		OraclePreparedStatement stQueryVehicleByMacid = null;
		try {
			String[] tempmacid = macid.split("_", 2);
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryVehicleByMacid = (OraclePreparedStatement)conn.prepareStatement(queryVehicleByMacid);

			stQueryVehicleByMacid.setString(1, tempmacid[1]);
			stQueryVehicleByMacid.setString(2, tempmacid[0]);
			stQueryVehicleByMacid.setExecuteBatch(1);
			rs = (OracleResultSet)stQueryVehicleByMacid.executeQuery();
			if (rs.next()) {
				ServiceUnit su = new ServiceUnit();
				String oemcode = rs.getString("oemcode");// 车机类型码
				String t_identifyno = rs.getString("t_identifyno");// 终端标识号
				su.setMacid(oemcode + "_" + t_identifyno);
				su.setSuid(rs.getLong("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getLong("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getLong("tid"));
				su.setCommaddr(t_identifyno);
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setTyrer(rs.getDouble("TYRE_R"));
				su.setRearaxlerate(rs.getDouble("REAR_AXLE_RATE"));
				su.setVinCode(rs.getString("VIN_CODE"));
				TempMemory.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				return su;
			}

		} catch (Exception e) {
			logger.error("查询车机出错."+ e.getMessage());
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryVehicleByMacid != null) {
					stQueryVehicleByMacid.close();

				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询车机出错."+ e.getMessage());
			}
		}

		return null;

	}

	/**
	 * 查询所有车辆
	 * 
	 * @param con
	 *            --使用的连接
	 * @param sql
	 *            --查询的sql
	 * @param map
	 *            --存放的map
	 * @throws SQLException
	 */
	private static void queryAllVehicle(String sql) throws SQLException {
		logger.info("--vehicleService--车辆服务信息更新开始...");
		long start = System.currentTimeMillis();
		OraclePreparedStatement stQueryAllVehicle = null;
		OracleResultSet rs = null;
		String oemcode;
		String t_identifyno;
		ServiceUnit su;
		// 从连接池获得连接
		OracleConnection conn = null;
		int index = 0 ;
		try {
			conn = (OracleConnection)OracleConnectionPool.getConnection();
			stQueryAllVehicle = (OraclePreparedStatement) conn.prepareStatement(sql);
			stQueryAllVehicle.setExecuteBatch(500);
			rs = (OracleResultSet)stQueryAllVehicle.executeQuery();

			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("t_identifyno");// 终端标识号
				su.setMacid(oemcode + "_" + t_identifyno);
				su.setSuid(rs.getLong("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getLong("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getLong("tid"));
				su.setCommaddr(t_identifyno);
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setTyrer(rs.getDouble("TYRE_R"));
				su.setRearaxlerate(rs.getDouble("REAR_AXLE_RATE"));
				su.setVinCode(rs.getString("VIN_CODE"));
				TempMemory.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				index++;
			}// End while

			//logger.info("查询到车辆总数:" + TempMemory.getVehicleMapSize());
		} catch (SQLException e) {
			logger.error("查询所有车辆"+ e.getMessage());
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryAllVehicle != null) {
				stQueryAllVehicle.close();
			}
			if (conn != null) {
				conn.close();
			}
		}
		long end = System.currentTimeMillis();
		logger.info("--vehicleService--车辆服务信息更新结束,更新车辆数:"+index+",耗时:"+(end - start));
	}
	
	/*****************************************
	 * <li>描        述：更新3g手机号对应的车辆缓存信息 		</li><br>
	 * <li>时        间：2013-7-24  上午9:57:13	</li><br>
	 * <li>参数： @param sql
	 * <li>参数： @throws SQLException			</li><br>
	 * 
	 *****************************************/
	public static void update3GPhotoVehicleInfo() throws SQLException {
		logger.info("更新3g手机号对应的车辆缓存信息...");
		long start = System.currentTimeMillis();
		OraclePreparedStatement update3GPhotoVehicleInfoStatement= null;
		OracleResultSet rs = null;
		String oemcode;
		String t_identifyno;
		String mac_id;
		ServiceUnit su;
		int index  = 0;
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			conn = (OracleConnection)OracleConnectionPool.getConnection();
			update3GPhotoVehicleInfoStatement = (OraclePreparedStatement) conn.prepareStatement(update3GPhotoVehicleInfoSql);
			update3GPhotoVehicleInfoStatement.setExecuteBatch(500);
			rs = (OracleResultSet)update3GPhotoVehicleInfoStatement.executeQuery();

			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("dvr_simnum");// 终端标识号
				mac_id = oemcode + "_" + t_identifyno;
				su.setMacid(mac_id);
				su.setSuid(rs.getLong("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getLong("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getLong("tid"));
				su.setCommaddr(rs.getString("t_identifyno"));
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setTyrer(rs.getDouble("TYRE_R"));
				su.setRearaxlerate(rs.getDouble("REAR_AXLE_RATE"));
				su.setVinCode(rs.getString("VIN_CODE"));
				TempMemory.setVehicleMapValue(mac_id, su);
				index++;
			}// End while

			//logger.info("查询到车辆总数:" + TempMemory.getVehicleMapSize());
		} catch (SQLException e) {
			logger.error("查询所有车辆"+ e.getMessage());
		} finally {
			if (rs != null) {
				rs.close();
			}
			if (update3GPhotoVehicleInfoStatement != null) {
				update3GPhotoVehicleInfoStatement.close();
			}
			if (conn != null) {
				conn.close();
			}
		}
		long end = System.currentTimeMillis();
		logger.info("更新3g手机号对应的车辆缓存信息结束,更新数量:"+index+",耗时:"+(end - start));
	}
	
	
	/**
	 * 根据车辆ID查询车辆状态编码
	 * 
	 * @param con
	 * @param sql
	 * @param statusCode
	 * @throws SQLException
	 */
	public static StatusCode queryStatusCode(long vid) throws SQLException {
		StatusBean statusBean = null;
		
		OracleResultSet rs = null;
		OraclePreparedStatement stQueryVehicleStatusCode = null;

		StatusCode statusCode = TempMemory.getStatusMapValue(vid);

		if (statusCode != null) {
			return statusCode;
		}
		statuscount++;
		logger.info("车辆状态查询次数"+statuscount);
		
		OracleConnection conn = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryVehicleStatusCode = (OraclePreparedStatement)conn
					.prepareStatement(queryVehicleStatusCode);
			stQueryVehicleStatusCode.setExecuteBatch(1);
			stQueryVehicleStatusCode.setLong(1, vid);
			rs = (OracleResultSet)stQueryVehicleStatusCode.executeQuery();

			if (rs.next()) {
				statusCode = new StatusCode();
				statusBean = new StatusBean();// 终端状态
				statusBean.setType(rs.getInt("TERMINAL_STATUS_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_TERMINAL_STATUS"));
				statusBean.setMax(rs.getDouble("MAX_TERMINAL_STATUS"));
				statusCode.setTerminalStatus(statusBean);
				statusBean = new StatusBean(); // GPS状态
				statusBean.setType(rs.getInt("GPS_STATUS_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_GPS_STATUS"));
				statusBean.setMax(rs.getDouble("MAX_GPS_STATUS"));
				statusCode.setGpsStatus(statusBean);
				statusBean = new StatusBean(); // 冷却液温度
				statusBean.setType(rs.getInt("E_WATER_TEMP_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_E_WATER_TEMP"));
				statusBean.setMax(rs.getDouble("MAX_E_WATER_TEMP"));
				statusCode.seteWater(statusBean);
				statusBean = new StatusBean(); // 蓄电池电压
				statusBean.setType(rs.getInt("EXT_VOLTAGE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EXT_VOLTAGE"));
				statusBean.setMax(rs.getDouble("MAX_EXT_VOLTAGE"));
				statusCode.setExtVoltage(statusBean);
				statusBean = new StatusBean(); // 油压状态
				statusBean.setType(rs.getInt("OIL_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_OIL_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_OIL_PRESSURE"));
				statusCode.setOilPressure(statusBean);
				statusBean = new StatusBean(); // 制动气压值
				statusBean.setType(rs.getInt("BRAKE_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_BRAKE_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_BRAKE_PRESSURE"));
				statusCode.setBrakePressure(statusBean);
				statusBean = new StatusBean(); // 制动蹄片磨损
				statusBean.setType(rs.getInt("BRAKEPAD_FRAY_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_BRAKEPAD_FRAY"));
				statusBean.setMax(rs.getDouble("MAX_BRAKEPAD_FRAY"));
				statusCode.setBrakepadFray(statusBean);
				statusBean = new StatusBean(); // 燃油告警
				statusBean.setType(rs.getInt("OIL_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_OIL_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_OIL_ALARM"));
				statusCode.setOilAram(statusBean);
				statusBean = new StatusBean(); // ABS故障状态
				statusBean.setType(rs.getInt("ABS_BUG_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_ABS_BUG"));
				statusBean.setMax(rs.getDouble("MAX_ABS_BUG"));
				statusCode.setAbsBug(statusBean);
				statusBean = new StatusBean(); // 水位低状态
				statusBean.setType(rs.getInt("COOLANT_LEVEL_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_COOLANT_LEVEL"));
				statusBean.setMax(rs.getDouble("MAX_COOLANT_LEVEL"));
				statusCode.setCoolantLevel(statusBean);
				statusBean = new StatusBean(); // 空滤堵塞
				statusBean.setType(rs.getInt("AIR_FILTER_CLOG_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_AIR_FILTER_CLOG"));
				statusBean.setMax(rs.getDouble("MAX_AIR_FILTER_CLOG"));
				statusCode.setAirFilter(statusBean);
				statusBean = new StatusBean(); // 机虑堵塞
				statusBean.setType(rs.getInt("MWERE_BLOCKING_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_MWERE_BLOCKING"));
				statusBean.setMax(rs.getDouble("MAX_MWERE_BLOCKING"));
				statusCode.setMwereBlocking(statusBean);
				statusBean = new StatusBean(); // 燃油堵塞
				statusBean.setType(rs.getInt("FUEL_BLOCKING_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_FUEL_BLOCKING"));
				statusBean.setMax(rs.getDouble("MAX_FUEL_BLOCKING"));
				statusCode.setFuelBlocking(statusBean);
				statusBean = new StatusBean(); // 机油温度
				statusBean.setType(rs.getInt("EOIL_TEMPERATURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EOIL_TEMPERATURE_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_EOIL_TEMPERATURE_ALARM"));
				statusCode.setEoilTemperature(statusBean);
				statusBean = new StatusBean(); // 缓速器高温
				statusBean.setType(rs.getInt("RETARDER_HT_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_RETARDER_HT_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_RETARDER_HT_ALARM"));
				statusCode.setRetarerHt(statusBean);
				statusBean = new StatusBean(); // 仓温高状态
				statusBean.setType(rs.getInt("EHOUSING_HT_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EHOUSING_HT_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_EHOUSING_HT_ALARM"));
				statusCode.setEhousing(statusBean);
				statusBean = new StatusBean(); // 大气压力
				statusBean.setType(rs.getInt("AIR_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_AIR_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_AIR_PRESSURE"));
				statusCode.setAirPressure(statusBean);
				TempMemory.setStatusMapValue(vid, statusCode);
			}// End while
			return statusCode;
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryVehicleStatusCode != null) {
				stQueryVehicleStatusCode.close();
			}

			if (conn != null) {
				conn.close();
			}
		}
	}

//	/**
//	 * 根据车辆ID查询车辆状态编码
//	 * 
//	 * @param con
//	 * @param sql
//	 * @param statusCode
//	 * @throws SQLException
//	 */
//	public synchronized static StatusCode queryStatusCode(long vid) throws SQLException {
//		StatusBean statusBean = null;
//		
//		OracleResultSet rs = null;
//		OraclePreparedStatement stQueryVehicleStatusCode = null;
//
//		StatusCode statusCode = TempMemory.getStatusMapValue(vid);
//
//		if (statusCode != null) {
//			return statusCode;
//		}
//		statuscount++;
//		logger.info("车辆状态查询次数"+statuscount);
//		
//		OracleConnection conn = (OracleConnection)ConnectionManager.getInstance().getConnection();
//		try {
//
//			stQueryVehicleStatusCode = (OraclePreparedStatement)conn
//					.prepareStatement(queryVehicleStatusCode);
//			stQueryVehicleStatusCode.setExecuteBatch(1);
//			stQueryVehicleStatusCode.setLong(1, vid);
//			rs = (OracleResultSet)stQueryVehicleStatusCode.executeQuery();
//
//			if (rs.next()) {
//				statusCode = new StatusCode();
//				statusBean = new StatusBean();// 终端状态
//				statusBean.setType(rs.getInt("TERMINAL_STATUS_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_TERMINAL_STATUS"));
//				statusBean.setMax(rs.getDouble("MAX_TERMINAL_STATUS"));
//				statusCode.setTerminalStatus(statusBean);
//				statusBean = new StatusBean(); // GPS状态
//				statusBean.setType(rs.getInt("GPS_STATUS_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_GPS_STATUS"));
//				statusBean.setMax(rs.getDouble("MAX_GPS_STATUS"));
//				statusCode.setGpsStatus(statusBean);
//				statusBean = new StatusBean(); // 冷却液温度
//				statusBean.setType(rs.getInt("E_WATER_TEMP_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_E_WATER_TEMP"));
//				statusBean.setMax(rs.getDouble("MAX_E_WATER_TEMP"));
//				statusCode.seteWater(statusBean);
//				statusBean = new StatusBean(); // 蓄电池电压
//				statusBean.setType(rs.getInt("EXT_VOLTAGE_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_EXT_VOLTAGE"));
//				statusBean.setMax(rs.getDouble("MAX_EXT_VOLTAGE"));
//				statusCode.setExtVoltage(statusBean);
//				statusBean = new StatusBean(); // 油压状态
//				statusBean.setType(rs.getInt("OIL_PRESSURE_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_OIL_PRESSURE"));
//				statusBean.setMax(rs.getDouble("MAX_OIL_PRESSURE"));
//				statusCode.setOilPressure(statusBean);
//				statusBean = new StatusBean(); // 制动气压值
//				statusBean.setType(rs.getInt("BRAKE_PRESSURE_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_BRAKE_PRESSURE"));
//				statusBean.setMax(rs.getDouble("MAX_BRAKE_PRESSURE"));
//				statusCode.setBrakePressure(statusBean);
//				statusBean = new StatusBean(); // 制动蹄片磨损
//				statusBean.setType(rs.getInt("BRAKEPAD_FRAY_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_BRAKEPAD_FRAY"));
//				statusBean.setMax(rs.getDouble("MAX_BRAKEPAD_FRAY"));
//				statusCode.setBrakepadFray(statusBean);
//				statusBean = new StatusBean(); // 燃油告警
//				statusBean.setType(rs.getInt("OIL_ALARM_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_OIL_ALARM"));
//				statusBean.setMax(rs.getDouble("MAX_OIL_ALARM"));
//				statusCode.setOilAram(statusBean);
//				statusBean = new StatusBean(); // ABS故障状态
//				statusBean.setType(rs.getInt("ABS_BUG_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_ABS_BUG"));
//				statusBean.setMax(rs.getDouble("MAX_ABS_BUG"));
//				statusCode.setAbsBug(statusBean);
//				statusBean = new StatusBean(); // 水位低状态
//				statusBean.setType(rs.getInt("COOLANT_LEVEL_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_COOLANT_LEVEL"));
//				statusBean.setMax(rs.getDouble("MAX_COOLANT_LEVEL"));
//				statusCode.setCoolantLevel(statusBean);
//				statusBean = new StatusBean(); // 空滤堵塞
//				statusBean.setType(rs.getInt("AIR_FILTER_CLOG_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_AIR_FILTER_CLOG"));
//				statusBean.setMax(rs.getDouble("MAX_AIR_FILTER_CLOG"));
//				statusCode.setAirFilter(statusBean);
//				statusBean = new StatusBean(); // 机虑堵塞
//				statusBean.setType(rs.getInt("MWERE_BLOCKING_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_MWERE_BLOCKING"));
//				statusBean.setMax(rs.getDouble("MAX_MWERE_BLOCKING"));
//				statusCode.setMwereBlocking(statusBean);
//				statusBean = new StatusBean(); // 燃油堵塞
//				statusBean.setType(rs.getInt("FUEL_BLOCKING_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_FUEL_BLOCKING"));
//				statusBean.setMax(rs.getDouble("MAX_FUEL_BLOCKING"));
//				statusCode.setFuelBlocking(statusBean);
//				statusBean = new StatusBean(); // 机油温度
//				statusBean.setType(rs.getInt("EOIL_TEMPERATURE_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_EOIL_TEMPERATURE_ALARM"));
//				statusBean.setMax(rs.getDouble("MAX_EOIL_TEMPERATURE_ALARM"));
//				statusCode.setEoilTemperature(statusBean);
//				statusBean = new StatusBean(); // 缓速器高温
//				statusBean.setType(rs.getInt("RETARDER_HT_ALARM_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_RETARDER_HT_ALARM"));
//				statusBean.setMax(rs.getDouble("MAX_RETARDER_HT_ALARM"));
//				statusCode.setRetarerHt(statusBean);
//				statusBean = new StatusBean(); // 仓温高状态
//				statusBean.setType(rs.getInt("EHOUSING_HT_ALARM_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_EHOUSING_HT_ALARM"));
//				statusBean.setMax(rs.getDouble("MAX_EHOUSING_HT_ALARM"));
//				statusCode.setEhousing(statusBean);
//				statusBean = new StatusBean(); // 大气压力
//				statusBean.setType(rs.getInt("AIR_PRESSURE_VALTYPE"));
//				statusBean.setMin(rs.getDouble("MIN_AIR_PRESSURE"));
//				statusBean.setMax(rs.getDouble("MAX_AIR_PRESSURE"));
//				statusCode.setAirPressure(statusBean);
//				TempMemory.setStatusMapValue(vid, statusCode);
//			}// End while
//			return statusCode;
//		} finally {
//			if (rs != null) {
//				rs.close();
//			}
//
//			if (stQueryVehicleStatusCode != null) {
//				stQueryVehicleStatusCode.close();
//			}
//
//			if (conn != null) {
//				conn.close();
//			}
//		}
//	}

	/**
	 * 查询所有报警类型
	 * 
	 * @param con
	 *            --使用的连接
	 * @param sql
	 *            --查询的sql
	 * @param map
	 *            --存放的map
	 * @throws SQLException
	 */
	private static void queryAlarmType(String sql) throws SQLException {
		
		OraclePreparedStatement stQueryAlarmType = null;
		OracleResultSet rs = null;
		OracleConnection conn = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAlarmType = (OraclePreparedStatement)conn.prepareStatement(sql);
			stQueryAlarmType.setExecuteBatch(100);
			rs = (OracleResultSet)stQueryAlarmType.executeQuery();

			while (rs.next()) {
				TempMemory
						.setAlarmtypeMapValue(
								rs.getInt("ALARM_CODE"),
								new AlarmTypeBean(rs.getString("ALARM_NAME"),
										rs.getInt("A_TYPE"), rs
												.getString("PARENT_CODE")));
			}

		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryAlarmType != null) {
				stQueryAlarmType.close();
			}
			if(conn != null){
				conn.close();
			}
		}
	}

//	/******
//	 * 初始化设置报警设置    2013-7-22  上午9:37:19 修改为updateVehicleAlarmSetting，去查询物化视图内容
//	 */
//	private void initAlarmSetting(){
//		OraclePreparedStatement stQueryCorpHasAlarm = null;
//		OraclePreparedStatement stQueryCorpListVid = null;
//		OraclePreparedStatement stQueryCorpToList = null;
//		OracleResultSet rs = null;
//		OracleResultSet rsVid = null;
//		OracleResultSet rsCorpToList = null;
//		OracleConnection conn = null;
//		
//		try {
//			conn = (OracleConnection)OracleConnectionPool.getConnection();
//			stQueryCorpHasAlarm = (OraclePreparedStatement) conn.prepareStatement(sql_corpHasAlarmLevel);
//			rs = (OracleResultSet) stQueryCorpHasAlarm.executeQuery();
//		
//			while(rs.next()){
//				long ent_id = rs.getLong(1);
//				try{
//					if(1 != ent_id && 0 != ent_id){
//						// 获取车辆对应企业设置列表
//						stQueryCorpListVid = (OraclePreparedStatement) conn.prepareStatement(sql_corpToListVid);
//						stQueryCorpListVid.setLong(1, ent_id);
//						rsVid = (OracleResultSet) stQueryCorpListVid.executeQuery();
//						
//						while(rsVid.next()){
//							TempMemory.vidEntMap.put(rsVid.getLong(2),ent_id);
//						}// End while
//					}
//					
//					// 查询设置企业对应报警列表
//					List<String> alarmList = new ArrayList<String>();
//					stQueryCorpToList = (OraclePreparedStatement) conn.prepareStatement(sql_corpToListAlarm);
//					stQueryCorpToList.setLong(1, ent_id);
//					rsCorpToList = (OracleResultSet) stQueryCorpToList.executeQuery();
//					while(rsCorpToList.next()){
//						alarmList.add(rsCorpToList.getString(1));
//					}// End while
//					
//					if(alarmList.size() > 0){
//						TempMemory.entAlarmMap.put(ent_id, alarmList);
//					}
//				}finally{
//					if(null != rsVid){
//						rsVid.close();
//					}
//					
//					if(null != rsCorpToList){
//						rsCorpToList.close();
//					}
//					
//					if(null != stQueryCorpListVid){
//						stQueryCorpListVid.close();
//					}
//					
//					if(null != stQueryCorpToList){
//						stQueryCorpToList.close();
//					}
//				}
//			}// End while
//		} catch (SQLException e) {
//			logger.error("初始化报警设置信息ERROR"+ e.getMessage());
//		}finally{
//			try{
//				if(null != rs){
//					rs.close();
//				}
//					
//				if(null != stQueryCorpHasAlarm){
//					stQueryCorpHasAlarm.close();
//				}
//				
//				if(null != conn){
//					conn.close();
//				}
//				
//			}catch(Exception ex){
//				logger.error(ex.getMessage());
//			}
//		}
//	}
	/*****************************************
	 * <li>描        述：更新车辆报警设置 		</li><br>
	 * <li>时        间：2013-7-22  上午9:37:19	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public static void updateVehicleAlarmSetting(){
		long start = System.currentTimeMillis();
		logger.info("--orgAlarmSetting--beginning...");
		OraclePreparedStatement stQueryCorpHasAlarm = null;
		OraclePreparedStatement stQueryCorpListVid = null;
		OraclePreparedStatement stQueryCorpToList = null;
		OracleResultSet rs = null;
		OracleResultSet rsVid = null;
		OracleResultSet rsCorpToList = null;
		OracleConnection conn = null;
		try {
//			查询车辆对应企业列表
			conn = (OracleConnection)OracleConnectionPool.getConnection();
			stQueryCorpHasAlarm = (OraclePreparedStatement) conn.prepareStatement(sql_corpHasAlarmLevel);
			rs = (OracleResultSet) stQueryCorpHasAlarm.executeQuery();
			while(rs.next()){
				long ent_id = rs.getLong(1);
				try{
//					查询非根企业下对应的车辆列表
					if(1 != ent_id){
						stQueryCorpListVid = (OraclePreparedStatement) conn.prepareStatement(sql_corpToListVid);
						stQueryCorpListVid.setLong(1, ent_id);
						rsVid = (OracleResultSet) stQueryCorpListVid.executeQuery();
						while(rsVid.next()){
							TempMemory.vidEntMap.put(rsVid.getLong(2),ent_id);
						}// End while
					}
					// 查询设置企业对应报警列表
					List<String> alarmList = new ArrayList<String>();
					stQueryCorpToList = (OraclePreparedStatement) conn.prepareStatement(sql_corpToListAlarm);
					stQueryCorpToList.setLong(1, ent_id);
					rsCorpToList = (OracleResultSet) stQueryCorpToList.executeQuery();
					while(rsCorpToList.next()){
						alarmList.add(rsCorpToList.getString(1));
					}// End while
					
					if(alarmList.size() > 0){
						TempMemory.entAlarmMap.put(ent_id, alarmList);
					}
				}finally{
					if(null != rsVid){
						rsVid.close();
					}
					
					if(null != rsCorpToList){
						rsCorpToList.close();
					}
					
					if(null != stQueryCorpListVid){
						stQueryCorpListVid.close();
					}
					
					if(null != stQueryCorpToList){
						stQueryCorpToList.close();
					}
				}
			}// End while
		} catch (SQLException e) {
			logger.error("---orgAlarmSetting---ERROR"+ e.getMessage());
		}finally{
			try{
				if(null != rs){
					rs.close();
				}
				if(null != stQueryCorpHasAlarm){
					stQueryCorpHasAlarm.close();
				}
				
				if(null != conn){
					conn.close();
				}
			}catch(Exception ex){
				logger.error("--orgAlarmSetting--colse rs query conn ERROR:" + ex.getMessage(),ex);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("---orgAlarmSetting---end--,Processed(ms):"+(end - start));
		
	}
	/*****
	 * 通过企业ID查找车辆
	 * @param ent_id
	 */
	public static void getListByCorp(long ent_id){
		OraclePreparedStatement stListByCorp = null;
		OracleResultSet rs = null;
		OracleConnection conn = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			// 获取车辆对应企业设置列表
			stListByCorp = (OraclePreparedStatement) conn.prepareStatement(sql_corpToListVid);
			stListByCorp.setLong(1, ent_id);
			rs = (OracleResultSet) stListByCorp.executeQuery();
			while(rs.next()){
				TempMemory.vidEntMap.put( rs.getLong(2),ent_id);
			}// End while
		} catch (SQLException e) {
			logger.error(e.getMessage());
		}finally{
			try{
				if(null != rs){
					rs.close();
				}
					
				if(null != stListByCorp){
					stListByCorp.close();
				}
				
				if(null != conn){
					conn.close();
				}
			}catch(Exception ex){
				logger.error(ex.getMessage());
			}
		}
	}
	
}
