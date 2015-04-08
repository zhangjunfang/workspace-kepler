package com.caits.analysisserver.database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.StatusBean;
import com.caits.analysisserver.bean.StatusCode;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;

public class AnalysisDBAdapter extends DBAdapter {

	private static final Logger logger = LoggerFactory.getLogger(AnalysisDBAdapter.class);

	private static String queryVehicleStatusCode;

	private static String queryVehicleInfo;

	private static String queryStatisticalAllAlarmType;

	private static String sql_queryStatisticalAllOutLineType;

	private static String queryVehicleAreaSpeedthresholdSql;

	private static String queryVehicleLineSpeedthresholdSql;
	
	private static String querySoftAlarmDetailSql;

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
	public void initDBAdapter() throws Exception {

		// 查询车辆状态编码
		queryVehicleStatusCode = SQLPool.getinstance().getSql(
				"sql_queryVehicleStatusCode");
		// 查询车辆编号，车架号，企业编号，企业名称，车队编号，车队名称
		queryVehicleInfo = SQLPool.getinstance().getSql("sql_queryVehicleInfo");

		// 查询车辆围栏超速阀值
		queryVehicleAreaSpeedthresholdSql = SQLPool.getinstance().getSql(
				"sql_queryVehicleAreaSpeedthreshold");
		// 查询车辆线路超速阀值
		queryVehicleLineSpeedthresholdSql = SQLPool.getinstance().getSql(
				"sql_queryVehicleLineSpeedthreshold");

		// 初始化存储所有报警编码与报警类型和等级对应关系
		queryStatisticalAllAlarmType = SQLPool.getinstance().getSql(
				"sql_queryStatisticalAllAlarmType");
		selectAllAlarmTypeCode(queryStatisticalAllAlarmType, "ALARM_CODE");

		// 加载非法营运码表
		sql_queryStatisticalAllOutLineType = SQLPool.getinstance().getSql(
				"sql_queryStatisticalAllOutLineType");
		
		//加载软报警明细数据
		querySoftAlarmDetailSql = SQLPool.getinstance().getSql("sql_querySoftAlarmDetail");
		
		selectAllAlarmTypeCode(sql_queryStatisticalAllOutLineType,
				"OUTLINE_CODE");
		
		queryStatusCode();

		// 初始化加载油箱油量监控车辆列表
		loadOilMonitorVehicleListing();

		// 查询车辆围栏线路超速阀值
		queryVehicleAreaSpeedthreshold();
		queryVehicleLineSpeedthreshold();

		System.out.println("车辆状态编码,缓存大小：" + queryVehicleStatusCode.length());
		System.out.println("车辆编号，车架号，企业编号，企业名称，车队编号，车队名称,缓存大小："
				+ queryVehicleInfo.length());
		System.out.println("初始化存储所有报警编码与报警类型和等级对应关系,缓存大小："
				+ alarmTypeMap.size());
		System.out.println("油箱油量监控车辆列表,缓存大小：" + oilMonitorMap.size());
	}

	/****
	 * 初始化加载油箱油量监控车辆列表
	 */
	private void loadOilMonitorVehicleListing() {
		Connection dbCon = null;
		PreparedStatement oilStat = null;
		ResultSet rs = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			oilStat = dbCon.prepareStatement(SQLPool.getinstance().getSql(
					"sql_vehicleConfigOilMonitor"));
			rs = oilStat.executeQuery();
			while (rs.next()) {
				oilMonitorMap.put(rs.getString("VID"), rs.getString("CODE_ID"));
			}// End while
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

	/**
	 * 初始化加载车辆状态编码
	 * 
	 * @param con
	 * @param sql
	 * @param statusCode
	 * @throws SQLException
	 */
	public static synchronized void queryStatusCode() throws SQLException {
		StatusBean statusBean = null;
		ResultSet rs = null;
		StatusCode statusCode = null;
		Connection dbCon = null;
		/** 根据车辆ID查询车辆状态参考编码表 ***/
		PreparedStatement stQueryVehicleStatusCode = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			if (dbCon != null) {
				stQueryVehicleStatusCode = dbCon
						.prepareStatement(queryVehicleStatusCode);
				rs = stQueryVehicleStatusCode.executeQuery();

				while (rs.next()) {
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
					statusBean.setVoltageType(rs.getString("VOLTAGE"));
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
					statusBean.setMin(rs
							.getDouble("MIN_EOIL_TEMPERATURE_ALARM"));
					statusBean.setMax(rs
							.getDouble("MAX_EOIL_TEMPERATURE_ALARM"));
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
					statusMap.put(rs.getString("VID"), statusCode);

				}
			}
		} catch (SQLException e) {
			logger.error("加载车辆状态编码出错", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryVehicleStatusCode != null) {
				stQueryVehicleStatusCode.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 查询所有报警编码与报警类型和等级对应关系
	 * 
	 * @return
	 * @throws SQLException
	 */
	private void selectAllAlarmTypeCode(String sql, String colName)
			throws SQLException {
		ResultSet rs = null;
		Connection dbCon = null;
		PreparedStatement stQueryStatisticalAllAlarmType = null;
		try {

			dbCon = OracleConnectionPool.getConnection();
			stQueryStatisticalAllAlarmType = dbCon.prepareStatement(sql);

			rs = stQueryStatisticalAllAlarmType.executeQuery();
			while (rs.next()) {
				alarmTypeMap.put(rs.getString(colName),
						rs.getString("PARENT_CODE"));
			}// End while
		} catch (SQLException e) {
			logger.error("查询所有报警编码与报警类型和等级对应关系出错.", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryStatisticalAllAlarmType != null) {
				stQueryStatisticalAllAlarmType.close();
			}
			if (dbCon != null) {
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
	public static synchronized VehicleInfo queryVechileInfo(String vid)
			 {
		VehicleInfo info = (VehicleInfo) vehicleInfoMap.get(vid);
		if (info == null) {
			ResultSet rs = null;
			Connection dbCon = null;

			PreparedStatement stQueryVehicleInfo = null;
			try {
				dbCon = OracleConnectionPool.getConnection();
				stQueryVehicleInfo = dbCon.prepareStatement(queryVehicleInfo);
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

					if (rs.getString("INNER_CODE") != null) {
						info.setInnerCode(rs.getString("INNER_CODE"));
					}

					if (rs.getString("STAFF_NAME") != null) {
						info.setDriverName(rs.getString("STAFF_NAME"));
					}

					if (rs.getString("STAFF_ID") != null) {
						info.setDriverId(rs.getString("STAFF_ID"));
					}

					if (rs.getString("CLASS_LINE_ID") != null
							&& !"".equals(rs.getString("CLASS_LINE_ID"))) {
						info.setVlineId(rs.getString("CLASS_LINE_ID"));
					}

					if (rs.getString("VBRAND_CODE") != null) {
						info.setVrBrandCode(rs.getString("VBRAND_CODE"));
					}

					if (rs.getString("COMMADDR") != null) {
						info.setCommaddr(rs.getString("COMMADDR"));
					}

					/*
					 * if(rs.getInt("CHECKED") != 0){
					 * info.setCheckNum(rs.getInt("CHECKED")); }
					 */

					if (rs.getString("LINE_NAME") != null) {
						info.setLineName(rs.getString("LINE_NAME"));
					}

					if (rs.getString("VEHICLE_TYPE") != null) {
						info.setVehicleType(rs.getString("VEHICLE_TYPE"));
					}

					if (rs.getString("OEM_CODE") != null) {
						info.setOemCode(rs.getString("OEM_CODE"));
					}

					if (rs.getString("CFG_FLAG") != null) {
						info.setCfgFlag(rs.getString("CFG_FLAG"));
					}

					if (rs.getLong("MAX_SPEED") > 0) {
						info.setMaxSpeed(rs.getLong("MAX_SPEED"));
					}
				}
			} catch (SQLException e) {
				logger.error("查询基本信息出错", e);
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
			vehicleInfoMap.put(vid, info);
		}

		return info;
	}

	/**
	 * 查询围栏超速阀值信息
	 * 
	 * @throws SQLException
	 */
	private void queryVehicleAreaSpeedthreshold() throws SQLException {
		Connection dbCon = null;
		PreparedStatement areaStatement = null;
		ResultSet rs = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			areaStatement = dbCon
					.prepareStatement(queryVehicleAreaSpeedthresholdSql);
			rs = areaStatement.executeQuery();
			while (rs.next()) {
				Long hold = rs.getLong("AREAMAXSPEED");
				if (hold > 0) {
					areaSpeedThresholdMap.put(
							rs.getString("VID") + "_" + rs.getString("AREAID"),
							hold);
				}
			}// End while
		} catch (SQLException e) {
			logger.error("查询围栏超速阀值信息出错.", e);
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (areaStatement != null) {
					areaStatement.close();
				}

				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
	}

	/**
	 * 查询线路超速阀值信息
	 * 
	 * @throws SQLException
	 */
	private void queryVehicleLineSpeedthreshold() throws SQLException {
		Connection dbCon = null;
		PreparedStatement lineStatement = null;
		ResultSet rs = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			lineStatement = dbCon
					.prepareStatement(queryVehicleLineSpeedthresholdSql);
			rs = lineStatement.executeQuery();
			while (rs.next()) {
				Long hold = rs.getLong("SPEEDTHRESHOLD");
				if (hold > 0) {
					lineSpeedThresholdMap.put(
							rs.getString("VID") + "_" + rs.getString("PID"),
							hold);
				}
			}// End while
		} catch (SQLException e) {
			logger.error("查询线路超速阀值信息出错.", e);
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (lineStatement != null) {
					lineStatement.close();
				}

				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
	}
	
	
	public void querySoftAlarmDetail(long utc) throws SQLException {
		Connection dbCon = null;
		PreparedStatement softAlarmStmt = null;
		ResultSet rs = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
			softAlarmStmt = dbCon.prepareStatement(querySoftAlarmDetailSql);
			softAlarmStmt.setLong(1, utc);
			softAlarmStmt.setLong(2, utc + 24 * 60 *60 *1000);
			rs = softAlarmStmt.executeQuery();
			while (rs.next()) {
				String vid = rs.getString("VID");
				String alarmCode = rs.getString("ALARM_CODE");
				int alarmSrc = rs.getInt("ALARM_SRC");
				Long beginUtc = rs.getLong("BEGIN_UTC");
				Long endUtc = rs.getLong("END_UTC");
				String areaId = rs.getString("AREA_ID");
				
				AlarmCacheBean cacheBean = new AlarmCacheBean();
				cacheBean.setVid(vid);
				cacheBean.setAlarmcode(alarmCode);
				cacheBean.setAlarmSrc(alarmSrc);
				cacheBean.setBegintime(beginUtc);
				cacheBean.setEndTime(endUtc);
				cacheBean.setAreaId(areaId);
				
				VehicleMessageBean beginVmb = new VehicleMessageBean();
				beginVmb.setUtc(beginUtc);
				
				VehicleMessageBean endVmb = new VehicleMessageBean();
				endVmb.setUtc(endUtc);
				
				cacheBean.setBeginVmb(beginVmb);
				cacheBean.setEndVmb(endVmb);
				
				if (softAlarmDetailMap.containsKey(vid)) {
					softAlarmDetailMap.get(vid).add(cacheBean);
				}else{
					Vector<AlarmCacheBean> ls = new Vector<AlarmCacheBean>();
					ls.add(cacheBean);
					softAlarmDetailMap.put(vid, ls);
				}
			}// End while
		} catch (SQLException e) {
			logger.error("查询线路超速阀值信息出错.", e);
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (softAlarmStmt != null) {
					softAlarmStmt.close();
				}

				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
	}

}
