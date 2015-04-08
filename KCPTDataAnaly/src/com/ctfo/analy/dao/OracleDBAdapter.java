package com.ctfo.analy.dao;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Map;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.apache.log4j.Logger;

import com.ctfo.analy.TempMemory;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.TbLineStationBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.connpool.OracleConnectionPool;
import com.ctfo.analy.io.DataPool;
import com.ctfo.analy.util.ExceptionUtil;
import com.ctfo.redis.util.Tools;
import com.lingtu.xmlconf.XmlConf;

/**
 * oracle数据库访问
 * 
 * @author yangyi
 * 
 */
public class OracleDBAdapter {

	private static final Logger logger = Logger
			.getLogger(OracleDBAdapter.class);

	/** 存储报警信息 */
	private OraclePreparedStatement stSaveVehicleAlarm;
	private String saveVehicleAlarmSQL;

	/** 更新报警信息 */
	private OraclePreparedStatement stUpdateVehicleAlarm;
	private String updateVehicleAlarmSQL;

	/** 存储报警事件信息 */
	private OraclePreparedStatement stSaveVehicleAlarmEvent;
	private String saveVehicleAlarmEventSQL;
	
	/** 存储车辆过站信息 */
	private OraclePreparedStatement stSaveVehicleOverStation;
	private String saveVehicleOverStationSQL;

	/***
	 * 初始化SQL参数
	 */
	public void initDBAdapter(XmlConf config, String nodeName) throws Exception {

		// 存储报警信息
		saveVehicleAlarmSQL = config
				.getStringValue("database|sqlstatement|sql_saveVehicleAlarm");

		// 更新报警信息
		updateVehicleAlarmSQL = config
				.getStringValue("database|sqlstatement|sql_updateVehicleAlarm");

		// 存储报警事件信息
		saveVehicleAlarmEventSQL = config
				.getStringValue("database|sqlstatement|sql_saveVehicleAlarmEventInfo");
		
		// 存储车辆过站信息
		saveVehicleOverStationSQL = config
				.getStringValue("database|sqlstatement|sql_saveVehicleOverStation");
	}

	/**
	 * 存储报警信息
	 * 
	 * TH_VEHICLE_ALARM( ALARM_ID,VID,UTC,LAT,LON,
	 * MAPLON,MAPLAT,ELEVATION,DIRECTION,GPS_SPEED,
	 * MILEAGE,OIL_TOTAL,ALARM_CODE,SYSUTC,ALARM_STATUS,
	 * ALARM_START_UTC,ALARM_DRIVER,VEHICLE_NO,BGLEVEL,BASESTATUS,
	 * EXTENDSTATUS,ALARM_ADD_INFO_START,ALARM_SRC,ALARM_ADD_INFO,AREA_ID,
	 * SPEED_THRESHOLD
	 * VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)
	 * 
	 * @throws SQLException
	 * 
	 */
	public void saveVehicleAlarm(VehicleMessageBean vehicleMessage)
			throws SQLException {

		ResultSet rs = null;
		OracleConnection dbCon = null;
		try {
			AlarmBaseBean alarmVehicleBean = TempMemory
			.getAlarmVehicleMap(vehicleMessage.getCommanddr());
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleAlarm = (OraclePreparedStatement) dbCon
					.prepareStatement(saveVehicleAlarmSQL);
			stSaveVehicleAlarm.setExecuteBatch(1);
			stSaveVehicleAlarm.setString(1, vehicleMessage.getAlarmid());
			stSaveVehicleAlarm.setString(2, vehicleMessage.getVid());
			stSaveVehicleAlarm.setLong(3, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setLong(4, vehicleMessage.getLat());
			stSaveVehicleAlarm.setLong(5, vehicleMessage.getLon());
			stSaveVehicleAlarm.setLong(6, vehicleMessage.getMaplon());
			stSaveVehicleAlarm.setLong(7, vehicleMessage.getMaplat());
			stSaveVehicleAlarm.setLong(8, vehicleMessage.getElevation());
			stSaveVehicleAlarm.setInt(9, vehicleMessage.getDir());
			stSaveVehicleAlarm.setInt(10, vehicleMessage.getSpeed());
			if (vehicleMessage.getMileage() != null
					&& vehicleMessage.getMileage() >= 0) {
				stSaveVehicleAlarm.setLong(11, vehicleMessage.getMileage());
			} else {
				stSaveVehicleAlarm.setNull(11, Types.INTEGER);
			}

			if (vehicleMessage.getOil() != null && vehicleMessage.getOil() >= 0) {
				stSaveVehicleAlarm.setLong(12, vehicleMessage.getOil());
			} else {
				stSaveVehicleAlarm.setNull(12, Types.INTEGER);
			}

			stSaveVehicleAlarm.setString(13, vehicleMessage.getAlarmcode());
			stSaveVehicleAlarm.setLong(14, System.currentTimeMillis());
			stSaveVehicleAlarm.setInt(15, 1);
			stSaveVehicleAlarm.setLong(16, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setString(17, "");
			stSaveVehicleAlarm.setString(18, vehicleMessage.getVehicleno());
			stSaveVehicleAlarm.setString(19, vehicleMessage.getBglevel());
			stSaveVehicleAlarm.setString(20, "");
			stSaveVehicleAlarm.setString(21, "");
			stSaveVehicleAlarm.setString(22, vehicleMessage.getAlarmadd());
			stSaveVehicleAlarm.setInt(23, vehicleMessage.getAlarmSrc());
			stSaveVehicleAlarm.setString(24, vehicleMessage.getAlarmAddInfo());// 告警具体描述
			String areaId = vehicleMessage.getAreaId();
			if (areaId == null || "".equals(areaId)) {
				stSaveVehicleAlarm.setNull(25, Types.VARCHAR);
			} else {
				stSaveVehicleAlarm.setString(25, areaId);
			}

			if (vehicleMessage.getSpeedThreshold() > 0) {
				stSaveVehicleAlarm.setDouble(26,
						vehicleMessage.getSpeedThreshold());
			} else {
				stSaveVehicleAlarm.setNull(26, Types.INTEGER);
			}
			
			stSaveVehicleAlarm.setString(27, alarmVehicleBean.getCorpId());
			stSaveVehicleAlarm.setString(28, alarmVehicleBean.getCorpName());
			stSaveVehicleAlarm.setString(29, alarmVehicleBean.getTeamId());
			stSaveVehicleAlarm.setString(30, alarmVehicleBean.getTeamName());
			
			stSaveVehicleAlarm.setString(31, vehicleMessage.getDriverId());
			stSaveVehicleAlarm.setString(32, vehicleMessage.getDriverName());
			stSaveVehicleAlarm.setString(33, vehicleMessage.getDriverSrc());

			stSaveVehicleAlarm.executeUpdate();
		} catch (Exception ex) {
			ex.printStackTrace();
			logger.error("保存告警日志出错（ORA）" + ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stSaveVehicleAlarm != null) {
				stSaveVehicleAlarm.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 更新报警信息
	 * 
	 * UPDATE TH_VEHICLE_ALARM SET ALARM_END_UTC =
	 * ?,END_LAT=?,END_LON=?,END_MAPLAT
	 * =?,END_MAPLON=?,END_ELEVATION=?,END_DIRECTION
	 * =?,END_GPS_SPEED=?,END_MILEAGE=?,END_OIL_TOTAL=?,
	 * ALARM_STATUS=0,ALARM_ADD_INFO_END =? WHERE ALARM_ID = ?
	 * 
	 * @throws SQLException
	 */
	public void updateVehicleAlarm(VehicleMessageBean vehicleMessage)
			throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateVehicleAlarm = (OraclePreparedStatement) dbCon
					.prepareStatement(updateVehicleAlarmSQL);
			stUpdateVehicleAlarm.setExecuteBatch(1);

			stUpdateVehicleAlarm.setLong(1, vehicleMessage.getUtc());

			if (vehicleMessage.getLat() != null) {
				stUpdateVehicleAlarm.setLong(2, vehicleMessage.getLat());
			} else {
				stUpdateVehicleAlarm.setNull(2, Types.INTEGER);
			}

			if (vehicleMessage.getLon() != null) {
				stUpdateVehicleAlarm.setLong(3, vehicleMessage.getLon());
			} else {
				stUpdateVehicleAlarm.setNull(3, Types.INTEGER);
			}

			stUpdateVehicleAlarm.setLong(4, vehicleMessage.getMaplat());
			stUpdateVehicleAlarm.setLong(5, vehicleMessage.getMaplon());
			stUpdateVehicleAlarm.setLong(6, vehicleMessage.getElevation());

			if (vehicleMessage.getDir() != null) {
				stUpdateVehicleAlarm.setInt(7, vehicleMessage.getDir());
			} else {
				stUpdateVehicleAlarm.setNull(7, Types.INTEGER);
			}
			if (vehicleMessage.getSpeed() != null) {
				stUpdateVehicleAlarm.setInt(8, vehicleMessage.getSpeed());
			} else {
				stUpdateVehicleAlarm.setNull(8, Types.INTEGER);
			}

			if (vehicleMessage.getMileage() != null
					&& vehicleMessage.getMileage() >= 0) {
				stUpdateVehicleAlarm.setLong(9, vehicleMessage.getMileage());
			} else {
				stUpdateVehicleAlarm.setNull(9, Types.INTEGER);
			}

			if (vehicleMessage.getOil() != null && vehicleMessage.getOil() >= 0) {
				stUpdateVehicleAlarm.setLong(10, vehicleMessage.getOil());
			} else {
				stUpdateVehicleAlarm.setNull(10, Types.INTEGER);
			}

			if (vehicleMessage.getAlarmadd() != null
					&& !"".equals(vehicleMessage.getAlarmadd())) {
				stUpdateVehicleAlarm
						.setString(11, vehicleMessage.getAlarmadd());
			} else {
				stUpdateVehicleAlarm.setNull(11, Types.VARCHAR);
			}
			
			
			stUpdateVehicleAlarm.setNull(12, Types.INTEGER);

			stUpdateVehicleAlarm.setNull(13, Types.INTEGER);


			stUpdateVehicleAlarm.setString(14, vehicleMessage.getAlarmid());
			stUpdateVehicleAlarm.executeUpdate();

		} catch (Exception ex) {
			ex.printStackTrace();
			logger.error("更新告警信息出错（ORA）：" + ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (stUpdateVehicleAlarm != null) {
				stUpdateVehicleAlarm.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	public void saveVehicleAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) throws SQLException {

		ResultSet rs = null;
		OracleConnection dbCon = null;
		try {
			AlarmBaseBean alarmVehicleBean = TempMemory
			.getAlarmVehicleMap(vehicleMessage.getCommanddr());
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleAlarm = (OraclePreparedStatement) dbCon
					.prepareStatement(saveVehicleAlarmSQL);
			stSaveVehicleAlarm.setExecuteBatch(1);
			stSaveVehicleAlarm.setString(1, alarmCacheBean.getAlarmId());
			stSaveVehicleAlarm.setString(2, vehicleMessage.getVid());
			stSaveVehicleAlarm.setLong(3, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setLong(4, vehicleMessage.getLat());
			stSaveVehicleAlarm.setLong(5, vehicleMessage.getLon());
			stSaveVehicleAlarm.setLong(6, vehicleMessage.getMaplon());
			stSaveVehicleAlarm.setLong(7, vehicleMessage.getMaplat());
			stSaveVehicleAlarm.setLong(8, vehicleMessage.getElevation());
			stSaveVehicleAlarm.setInt(9, vehicleMessage.getDir());
			stSaveVehicleAlarm.setInt(10, vehicleMessage.getSpeed());
			if (vehicleMessage.getMileage() != null
					&& vehicleMessage.getMileage() >= 0) {
				stSaveVehicleAlarm.setLong(11, vehicleMessage.getMileage());
			} else {
				stSaveVehicleAlarm.setNull(11, Types.INTEGER);
			}

			if (vehicleMessage.getOil() != null && vehicleMessage.getOil() >= 0) {
				stSaveVehicleAlarm.setLong(12, vehicleMessage.getOil());
			} else {
				stSaveVehicleAlarm.setNull(12, Types.INTEGER);
			}

			stSaveVehicleAlarm.setString(13, alarmCacheBean.getAlarmcode());
			stSaveVehicleAlarm.setLong(14, System.currentTimeMillis());
			stSaveVehicleAlarm.setInt(15, 1);
			stSaveVehicleAlarm.setLong(16, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setString(17, "");
			stSaveVehicleAlarm.setString(18, vehicleMessage.getVehicleno());
			stSaveVehicleAlarm.setString(19, alarmCacheBean.getAlarmlevel());
			stSaveVehicleAlarm.setString(20, "");
			stSaveVehicleAlarm.setString(21, "");
			stSaveVehicleAlarm.setString(22, alarmCacheBean.getAlarmadd());
			stSaveVehicleAlarm.setInt(23, alarmCacheBean.getAlarmSrc());
			stSaveVehicleAlarm.setString(24, alarmCacheBean.getAlarmaddInfo());// 告警具体描述
			String areaId = alarmCacheBean.getAreaId();
			if (areaId == null || "".equals(areaId)) {
				stSaveVehicleAlarm.setNull(25, Types.VARCHAR);
			} else {
				stSaveVehicleAlarm.setString(25, areaId);
			}

			if (alarmCacheBean.getSpeedThreshold() > 0) {
				stSaveVehicleAlarm.setDouble(26,
						alarmCacheBean.getSpeedThreshold());
			} else {
				stSaveVehicleAlarm.setNull(26, Types.INTEGER);
			}
			
			stSaveVehicleAlarm.setString(27, alarmVehicleBean.getCorpId());
			stSaveVehicleAlarm.setString(28, alarmVehicleBean.getCorpName());
			stSaveVehicleAlarm.setString(29, alarmVehicleBean.getTeamId());
			stSaveVehicleAlarm.setString(30, alarmVehicleBean.getTeamName());
			
			stSaveVehicleAlarm.setString(31, vehicleMessage.getDriverId());
			stSaveVehicleAlarm.setString(32, vehicleMessage.getDriverName());
			stSaveVehicleAlarm.setString(33, vehicleMessage.getDriverSrc());

			stSaveVehicleAlarm.executeUpdate();
			
			//保存成功后发送告警开始通知消息给CS客户端
			sendAlarm(vehicleMessage, alarmCacheBean.getAlarmId(),alarmCacheBean.getAlarmcode(), "0");
		} catch (Exception ex) {
			ex.printStackTrace();
			logger.error("保存告警日志出错（ORA）" + ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stSaveVehicleAlarm != null) {
				stSaveVehicleAlarm.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 更新报警信息
	 * 
	 * UPDATE TH_VEHICLE_ALARM SET ALARM_END_UTC =
	 * ?,END_LAT=?,END_LON=?,END_MAPLAT
	 * =?,END_MAPLON=?,END_ELEVATION=?,END_DIRECTION
	 * =?,END_GPS_SPEED=?,END_MILEAGE=?,END_OIL_TOTAL=?,
	 * ALARM_STATUS=0,ALARM_ADD_INFO_END =? WHERE ALARM_ID = ?
	 * 
	 * @throws SQLException
	 */
	public void updateVehicleAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateVehicleAlarm = (OraclePreparedStatement) dbCon
					.prepareStatement(updateVehicleAlarmSQL);
			stUpdateVehicleAlarm.setExecuteBatch(1);

			stUpdateVehicleAlarm.setLong(1, vehicleMessage.getUtc());

			if (vehicleMessage.getLat() != null) {
				stUpdateVehicleAlarm.setLong(2, vehicleMessage.getLat());
			} else {
				stUpdateVehicleAlarm.setNull(2, Types.INTEGER);
			}

			if (vehicleMessage.getLon() != null) {
				stUpdateVehicleAlarm.setLong(3, vehicleMessage.getLon());
			} else {
				stUpdateVehicleAlarm.setNull(3, Types.INTEGER);
			}

			stUpdateVehicleAlarm.setLong(4, vehicleMessage.getMaplat());
			stUpdateVehicleAlarm.setLong(5, vehicleMessage.getMaplon());
			stUpdateVehicleAlarm.setLong(6, vehicleMessage.getElevation());

			if (vehicleMessage.getDir() != null) {
				stUpdateVehicleAlarm.setInt(7, vehicleMessage.getDir());
			} else {
				stUpdateVehicleAlarm.setNull(7, Types.INTEGER);
			}
			if (vehicleMessage.getSpeed() != null) {
				stUpdateVehicleAlarm.setInt(8, vehicleMessage.getSpeed());
			} else {
				stUpdateVehicleAlarm.setNull(8, Types.INTEGER);
			}

			if (vehicleMessage.getMileage() != null
					&& vehicleMessage.getMileage() >= 0) {
				stUpdateVehicleAlarm.setLong(9, vehicleMessage.getMileage());
			} else {
				stUpdateVehicleAlarm.setNull(9, Types.INTEGER);
			}

			if (vehicleMessage.getOil() != null && vehicleMessage.getOil() >= 0) {
				stUpdateVehicleAlarm.setLong(10, vehicleMessage.getOil());
			} else {
				stUpdateVehicleAlarm.setNull(10, Types.INTEGER);
			}

			if (alarmCacheBean.getAlarmadd() != null
					&& !"".equals(alarmCacheBean.getAlarmadd())) {
				stUpdateVehicleAlarm
						.setString(11, alarmCacheBean.getAlarmadd());
			} else {
				stUpdateVehicleAlarm.setNull(11, Types.VARCHAR);
			}
			
			if (alarmCacheBean.getMaxSpeed() != -1){
				stUpdateVehicleAlarm.setLong(12, alarmCacheBean.getMaxSpeed());
			}else{
				stUpdateVehicleAlarm.setNull(12, Types.INTEGER);
			}
			
			if (alarmCacheBean.getAvgSpeed()> 0){
				stUpdateVehicleAlarm.setLong(13, alarmCacheBean.getAvgSpeed());
			}else{
				stUpdateVehicleAlarm.setNull(13, Types.INTEGER);
			}

			stUpdateVehicleAlarm.setString(14, alarmCacheBean.getAlarmId());
			stUpdateVehicleAlarm.executeUpdate();
			
			//保存成功后发送告警结束通知消息给CS客户端
			sendAlarm(vehicleMessage, alarmCacheBean.getAlarmId(),alarmCacheBean.getAlarmcode(), "1");

		} catch (Exception ex) {
			ex.printStackTrace();
			logger.error("更新告警信息出错（ORA）：" + ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (stUpdateVehicleAlarm != null) {
				stUpdateVehicleAlarm.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 存储告警事件信息
	 * 
	 * INSERT INTO TH_VEHICLE_ALARM_EVENT (
	 * AUTO_ID,VID,DEVICE_NO,ALARM_CODE,AREA_ID,
	 * MTYPE_CODE,MEDIA_URI,BEGIN_UTC,BEGIN_LAT,BEGIN_LON,
	 * BEGIN_MAPLAT,BEGIN_MAPLON
	 * ,BEGIN_ELEVATION,BEGIN_DIRECTION,BEGIN_GPS_SPEED,
	 * END_UTC,END_LAT,END_LON,END_MAPLAT,END_MAPLON,
	 * END_ELEVATION,END_DIRECTION
	 * ,END_GPS_SPEED,ALARM_EVENT_TIME,KEYPOINT_GPS_SPEED,
	 * Vline_Id,Inner_Code,Vehicleno,MILEAGE,OIL_WEAR,
	 * C_VIN,LINE_NAME,CORP_ID,CORP_NAME,TEAM_ID, TEAM_NAME,ALARM_SRC,
	 * SPEED_THRESHOLD) VALUES(SEQ_ALARMID_EVENT_ID
	 * .NEXTVAL,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
	 * ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)
	 * 
	 * @param vehicleMessage
	 * @param illeOptAlarmCache
	 * @throws SQLException
	 */
	public void saveVehicleAlarmEvent(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) throws SQLException {
		OracleConnection dbCon = null;
		try {
			AlarmBaseBean alarmVehicleBean = TempMemory
					.getAlarmVehicleMap(vehicleMessage.getCommanddr());
			if (alarmVehicleBean != null) {

				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveVehicleAlarmEvent = (OraclePreparedStatement) dbCon
						.prepareStatement(saveVehicleAlarmEventSQL);
				stSaveVehicleAlarmEvent.setExecuteBatch(1);

				stSaveVehicleAlarmEvent.setString(1, alarmCacheBean.getAlarmId());
				stSaveVehicleAlarmEvent.setString(2, vehicleMessage.getVid());
				stSaveVehicleAlarmEvent.setString(3,
						vehicleMessage.getCommanddr());
				stSaveVehicleAlarmEvent.setString(4,
						alarmCacheBean.getAlarmcode());
				
				String areaId = alarmCacheBean.getAreaId();

				if ( areaId!= null&&!"".equals(areaId)) {
					stSaveVehicleAlarmEvent.setString(5,
							areaId);
				} else {
					stSaveVehicleAlarmEvent.setNull(5, Types.VARCHAR);
				}

				if (alarmCacheBean.getMtypeCode() != null) {
					stSaveVehicleAlarmEvent.setString(6,
							alarmCacheBean.getMtypeCode());
				} else {
					stSaveVehicleAlarmEvent.setString(6, null);
				}

				if (alarmCacheBean.getMediaUrl() != null) {
					stSaveVehicleAlarmEvent.setString(7,
							alarmCacheBean.getMediaUrl());
				} else {
					stSaveVehicleAlarmEvent.setString(7, null);
				}

				VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();

				stSaveVehicleAlarmEvent.setLong(8, beginVmb.getUtc());
				stSaveVehicleAlarmEvent.setLong(9, beginVmb.getLat());
				stSaveVehicleAlarmEvent.setLong(10, beginVmb.getLon());
				stSaveVehicleAlarmEvent.setLong(11, beginVmb.getMaplat());
				stSaveVehicleAlarmEvent.setLong(12, beginVmb.getMaplon());
				stSaveVehicleAlarmEvent.setInt(13, beginVmb.getElevation());
				stSaveVehicleAlarmEvent.setInt(14, beginVmb.getDir());
				stSaveVehicleAlarmEvent.setInt(15, beginVmb.getSpeed());

				VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();

				stSaveVehicleAlarmEvent.setLong(16, endVmb.getUtc());

				if (endVmb.getLat() != null) {
					stSaveVehicleAlarmEvent.setLong(17, endVmb.getLat());
				} else {
					stSaveVehicleAlarmEvent.setNull(17, Types.INTEGER);
				}

				if (endVmb.getLon() != null) {
					stSaveVehicleAlarmEvent.setLong(18, endVmb.getLon());
				} else {
					stSaveVehicleAlarmEvent.setNull(18, Types.INTEGER);
				}
				stSaveVehicleAlarmEvent.setLong(19, endVmb.getMaplat());
				stSaveVehicleAlarmEvent.setLong(20, endVmb.getMaplon());
				stSaveVehicleAlarmEvent.setInt(21, endVmb.getElevation());
				if (endVmb.getDir() != null) {
					stSaveVehicleAlarmEvent.setInt(22, endVmb.getDir());
				} else {
					stSaveVehicleAlarmEvent.setNull(22, Types.INTEGER);
				}
				if (endVmb.getSpeed() != null) {
					stSaveVehicleAlarmEvent.setInt(23, endVmb.getSpeed());
				} else {
					stSaveVehicleAlarmEvent.setNull(23, Types.INTEGER);
				}

				stSaveVehicleAlarmEvent.setDouble(24,
						(endVmb.getUtc() - beginVmb.getUtc()) / 1000);

				if (alarmCacheBean.getMaxSpeed() != -1) {
					stSaveVehicleAlarmEvent.setLong(25,
							alarmCacheBean.getMaxSpeed());
				} else {
					stSaveVehicleAlarmEvent.setNull(25, Types.INTEGER);
				}

				if (alarmCacheBean.getVlineId() != null && !"".equals(alarmCacheBean.getVlineId())) {
					stSaveVehicleAlarmEvent.setString(26,
							alarmCacheBean.getVlineId());
				} else {
					stSaveVehicleAlarmEvent.setNull(26, Types.VARCHAR);
				}
				stSaveVehicleAlarmEvent.setString(27,
						alarmVehicleBean.getInnerCode());
				stSaveVehicleAlarmEvent.setString(28,
						alarmVehicleBean.getVehicleno());

				if (beginVmb.getMileage() >= 0
						&& endVmb.getMileage() >= beginVmb.getMileage()) {
					stSaveVehicleAlarmEvent.setLong(29, endVmb.getMileage()
							- beginVmb.getMileage());
				} else {
					stSaveVehicleAlarmEvent.setNull(29, Types.INTEGER);
				}

				if (endVmb.getOil() >= 0
						&& beginVmb.getOil() >= endVmb.getOil()) {
					stSaveVehicleAlarmEvent.setLong(30, beginVmb.getOil()
							- endVmb.getOil());
				} else {
					stSaveVehicleAlarmEvent.setNull(30, Types.INTEGER);
				}

				stSaveVehicleAlarmEvent.setString(31,
						alarmVehicleBean.getVinCode());

				if (alarmCacheBean.getLineName() != null) {
					stSaveVehicleAlarmEvent.setString(32,
							alarmCacheBean.getLineName());
				} else {
					stSaveVehicleAlarmEvent.setString(32, null);
				}

				stSaveVehicleAlarmEvent.setString(33,
						alarmVehicleBean.getCorpId());
				stSaveVehicleAlarmEvent.setString(34,
						alarmVehicleBean.getCorpName());
				stSaveVehicleAlarmEvent.setString(35,
						alarmVehicleBean.getTeamId());
				stSaveVehicleAlarmEvent.setString(36,
						alarmVehicleBean.getTeamName());
				stSaveVehicleAlarmEvent
						.setInt(37, alarmCacheBean.getAlarmSrc());

				if (alarmCacheBean.getSpeedThreshold() > 0) {
					stSaveVehicleAlarmEvent.setDouble(38,
							alarmCacheBean.getSpeedThreshold());
				} else {
					stSaveVehicleAlarmEvent.setNull(38, Types.INTEGER);
				}
				
				if (alarmCacheBean.getAvgSpeed()> 0){
					stSaveVehicleAlarmEvent.setLong(39, alarmCacheBean.getAvgSpeed());
				}else{
					stSaveVehicleAlarmEvent.setNull(39, Types.INTEGER);
				}
				
				stSaveVehicleAlarmEvent.setString(40, beginVmb.getDriverId());
				stSaveVehicleAlarmEvent.setString(41, beginVmb.getDriverName());
				stSaveVehicleAlarmEvent.setString(42, beginVmb.getDriverSrc());

				logger.debug("1, "
						+ vehicleMessage.getVid()
						+ "2,"
						+ vehicleMessage.getCommanddr()
						+ "3,"
						+ "7,"
						+ alarmCacheBean.getBegintime()
						+ "8,"
						+ beginVmb.getLat()
						+ "9,"
						+ beginVmb.getLon()
						+ "10,"
						+ beginVmb.getMaplat()
						+ "11,"
						+ beginVmb.getMaplon()
						+ "12,"
						+ beginVmb.getElevation()
						+ "13,"
						+ beginVmb.getDir()
						+ "14,"
						+ beginVmb.getSpeed()
						+ "15,"
						+ alarmCacheBean.getEndTime()
						+ "16,"
						+ endVmb.getLat()
						+ "17,"
						+ endVmb.getLon()
						+ "18,"
						+ endVmb.getMaplat()
						+ "19,"
						+ endVmb.getMaplon()
						+ "20,"
						+ endVmb.getElevation()
						+ "21,"
						+ endVmb.getDir()
						+ "22,"
						+ endVmb.getSpeed()
						+ "23,"
						+ (alarmCacheBean.getEndTime() - alarmCacheBean
								.getBegintime()) + "24,"
						+ alarmCacheBean.getMaxSpeed() + "26,"
						+ alarmVehicleBean.getInnerCode() + "27,"
						+ alarmVehicleBean.getVehicleno() + "28,"
						+ alarmCacheBean.getMileage() + "29,"
						+ alarmCacheBean.getOil() + "30,"
						+ alarmVehicleBean.getVinCode() + "32,"
						+ alarmVehicleBean.getCorpId() + "33,"
						+ alarmVehicleBean.getCorpName() + "34,"
						+ alarmVehicleBean.getTeamId() + "35,"
						+ alarmVehicleBean.getTeamName() + "36,"
						+ alarmCacheBean.getAlarmSrc() + "37,"
						+ alarmCacheBean.getSpeedThreshold());
				stSaveVehicleAlarmEvent.executeUpdate();
			}
		} catch (Exception ex) {
			ex.printStackTrace();
			logger.error("保存告警事件信息出错（ORA）："
					+ ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (stSaveVehicleAlarmEvent != null) {
				stSaveVehicleAlarmEvent.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	private void sendAlarm(VehicleMessageBean vehicleMessage, String alarmid,
			String alarmcode, String status) {
		if ((alarmid != null) && (alarmcode != null)&&vehicleMessage.getCommand()!=null) {
			String sendcommand = replaceCommandFlag(
					vehicleMessage.getCommand(), alarmid, alarmcode, status);
			MessageBean message = new MessageBean();
			message.setMsgid(vehicleMessage.getMsgid());
			message.setCommand(sendcommand);
			DataPool.setReceivePacket(message);
		}
	}

	private String replaceCommandFlag(String command, String alarmId,
			String alarmCode, String status) {
		command = command.replace("CAITS", "CAITR");
		command = command.replace("}", ",130:" + alarmId + ",131:" + alarmCode
				+ ",132:" + status + "} \r\n");
		return command;
	}
	
	
	/**
	 * 保存车辆过站信息
	 * @param vehicleMessage
	 * @param alarmCacheBean
	 * @throws SQLException
	 */
	public void saveVehicleOverStationInfo(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean,TbLineStationBean tblineStationBean) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleOverStation = (OraclePreparedStatement) dbCon
					.prepareStatement(saveVehicleOverStationSQL);
			stSaveVehicleOverStation.setExecuteBatch(1);
			stSaveVehicleOverStation.setString(1, alarmCacheBean.getAlarmId());
			stSaveVehicleOverStation.setString(2, ""+vehicleMessage.getVid());
			stSaveVehicleOverStation.setString(3, tblineStationBean.getLineId());
			stSaveVehicleOverStation.setLong(4, tblineStationBean.getStationNum());
			stSaveVehicleOverStation.setString(5, tblineStationBean.getStationId());
			stSaveVehicleOverStation.setLong(6, tblineStationBean.getStationNumber());
			stSaveVehicleOverStation.setString(7, tblineStationBean.getStationName());
			stSaveVehicleOverStation.setLong(8, vehicleMessage.getUtc());
			stSaveVehicleOverStation.setString(9, alarmCacheBean.getAlarmcode());
			stSaveVehicleOverStation.setLong(10, vehicleMessage.getMileage());
			
			stSaveVehicleOverStation.executeUpdate();
	
		} catch (Exception ex) {
			ex.printStackTrace();
			logger.error("保存车辆过站信息出错（ORA）" + ExceptionUtil.getErrorStack(ex, 0));
		} finally {

			if (stSaveVehicleOverStation != null) {
				stSaveVehicleOverStation.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

}
