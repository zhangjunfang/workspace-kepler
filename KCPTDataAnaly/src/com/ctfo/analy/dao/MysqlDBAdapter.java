package com.ctfo.analy.dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;

import org.apache.log4j.Logger;

import com.ctfo.analy.Constant;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.lingtu.xmlconf.XmlConf;

/**
 * mysql数据访问
 * @author yangyi
 *
 */
public class MysqlDBAdapter {

	private static final Logger logger = Logger
	.getLogger(MysqlDBAdapter.class);

	/** 存储报警信息 */
	private PreparedStatement stSaveVehicleAlarm;
	private String saveVehicleAlarmSQL;
	
	/** 更新报警信息 */
	private PreparedStatement stUpdateVehicleAlarm;
	private String updateVehicleAlarmSQL;

	/***
	 * 初始化SQL参数
	 */
	public void initDBAdapter(XmlConf config, String nodeName) throws Exception {

		// 存储报警信息
		saveVehicleAlarmSQL = config.getStringValue("database|sqlstatement|mysql_saveVehicleAlarm");
		
		// 更新报警信息
		updateVehicleAlarmSQL = config.getStringValue("database|sqlstatement|mysql_updateVehicleAlarm");
	}

	/**
	 * 存储报警信息
	 * 
	 * INSERT INTO MEM_TH_VEHICLE_ALARM(
	 * ALARM_ID,VID,UTC,LAT,LON,MAPLON,MAPLAT,ELEVATION,DIRECTION,
	 * GPS_SPEED,MILEAGE,OIL_TOTAL,ALARM_CODE,SYSUTC,ALARM_STATUS,
	 * ALARM_START_UTC,ALARM_DRIVER,VEHICLE_NO,BGLEVEL,BASESTATUS,
	 * EXTENDSTATUS,ALARM_ADD_INFO_START,ALARM_ADD_INFO) 
	 * VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)
	 * @throws SQLException 
	 * 
	 */
	public void saveVehicleAlarm(VehicleMessageBean vehicleMessage) throws SQLException {

		ResultSet rs = null;
		Connection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			stSaveVehicleAlarm =  dbCon.prepareStatement(saveVehicleAlarmSQL);
 
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
			stSaveVehicleAlarm.setLong(11, vehicleMessage.getMileage());
			stSaveVehicleAlarm.setLong(12, vehicleMessage.getOil());
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
			stSaveVehicleAlarm.setString(23, vehicleMessage.getAlarmAddInfo());//告警具体描述
			stSaveVehicleAlarm.executeUpdate();
		}catch(Exception ex){
			logger.error("保存告警日志出错（MySql）"+ex);
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
	 * UPDATE MEM_TH_VEHICLE_ALARM SET ALARM_END_UTC =
	 * ?,END_LAT=?,END_LON=?,END_MAPLAT
	 * =?,END_MAPLON=?,END_ELEVATION=?,END_DIRECTION
	 * =?,END_GPS_SPEED=?,END_MILEAGE=?,END_OIL_TOTAL=?,
	 * ALARM_STATUS=0,ALARM_ADD_INFO_END =? WHERE ALARM_ID = ?
	 * 
	 * @throws SQLException
	 */
	public void updateVehicleAlarm(VehicleMessageBean vehicleMessage) throws SQLException {
		Connection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			stUpdateVehicleAlarm =dbCon.prepareStatement(updateVehicleAlarmSQL);
			
			stUpdateVehicleAlarm.setLong(1, vehicleMessage.getUtc());
			
			if (vehicleMessage.getLat()!=null){
				stUpdateVehicleAlarm.setLong(2, vehicleMessage.getLat());
			}else{
				stUpdateVehicleAlarm.setNull(2, Types.INTEGER);
			}
			
			if (vehicleMessage.getLon()!=null){
				stUpdateVehicleAlarm.setLong(3, vehicleMessage.getLon());
			}else{
				stUpdateVehicleAlarm.setNull(3, Types.INTEGER);
			}
			
			stUpdateVehicleAlarm.setLong(4,vehicleMessage.getMaplat());
			stUpdateVehicleAlarm.setLong(5,vehicleMessage.getMaplon());
			stUpdateVehicleAlarm.setLong(6, vehicleMessage.getElevation());
			if (vehicleMessage.getDir()!=null){
				stUpdateVehicleAlarm.setInt(7, vehicleMessage.getDir());
			}else{
				stUpdateVehicleAlarm.setNull(7, Types.INTEGER);
			}
			if (vehicleMessage.getSpeed()!=null){
				stUpdateVehicleAlarm.setInt(8, vehicleMessage.getSpeed());
			}else{
				stUpdateVehicleAlarm.setNull(8, Types.INTEGER);
			}
			
			if (vehicleMessage.getMileage()!=null&&vehicleMessage.getMileage()>=0){
				stUpdateVehicleAlarm.setLong(9, vehicleMessage.getMileage());
			}else{
				stUpdateVehicleAlarm.setNull(9,  Types.INTEGER);
			}
			
			if (vehicleMessage.getOil()!=null&&vehicleMessage.getOil()>=0){
				stUpdateVehicleAlarm.setLong(10, vehicleMessage.getOil());
			}else{
				stUpdateVehicleAlarm.setNull(10,  Types.INTEGER);
			}
			

			stUpdateVehicleAlarm.setString(11, vehicleMessage.getAlarmadd());
			stUpdateVehicleAlarm.setString(12, vehicleMessage.getAlarmid());
			stUpdateVehicleAlarm.executeUpdate();

		} finally {
			if (stUpdateVehicleAlarm != null) {
				stUpdateVehicleAlarm.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}
	
	public void saveVehicleAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCache) throws SQLException {

		ResultSet rs = null;
		Connection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			stSaveVehicleAlarm =  dbCon.prepareStatement(saveVehicleAlarmSQL);
 
			stSaveVehicleAlarm.setString(1, alarmCache.getAlarmId());
			stSaveVehicleAlarm.setString(2, vehicleMessage.getVid());
			stSaveVehicleAlarm.setLong(3, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setLong(4, vehicleMessage.getLat());
			stSaveVehicleAlarm.setLong(5, vehicleMessage.getLon());
			stSaveVehicleAlarm.setLong(6, vehicleMessage.getMaplon());
			stSaveVehicleAlarm.setLong(7, vehicleMessage.getMaplat());
			stSaveVehicleAlarm.setLong(8, vehicleMessage.getElevation());
			stSaveVehicleAlarm.setInt(9, vehicleMessage.getDir());
			stSaveVehicleAlarm.setInt(10, vehicleMessage.getSpeed());
			stSaveVehicleAlarm.setLong(11, vehicleMessage.getMileage());
			stSaveVehicleAlarm.setLong(12, vehicleMessage.getOil());
			stSaveVehicleAlarm.setString(13, alarmCache.getAlarmcode());
			stSaveVehicleAlarm.setLong(14, System.currentTimeMillis());
			stSaveVehicleAlarm.setInt(15, 1);
			stSaveVehicleAlarm.setLong(16, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setString(17, "");
			stSaveVehicleAlarm.setString(18, vehicleMessage.getVehicleno());
			stSaveVehicleAlarm.setString(19, vehicleMessage.getBglevel());
			stSaveVehicleAlarm.setString(20, "");
			stSaveVehicleAlarm.setString(21, "");			
			stSaveVehicleAlarm.setString(22, alarmCache.getAlarmadd());
			stSaveVehicleAlarm.setString(23, alarmCache.getAlarmaddInfo());//告警具体描述
			stSaveVehicleAlarm.executeUpdate();
		}catch(Exception ex){
			logger.error("保存告警日志出错（MySql）"+ex);
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
	 * UPDATE MEM_TH_VEHICLE_ALARM SET ALARM_END_UTC =
	 * ?,END_LAT=?,END_LON=?,END_MAPLAT
	 * =?,END_MAPLON=?,END_ELEVATION=?,END_DIRECTION
	 * =?,END_GPS_SPEED=?,END_MILEAGE=?,END_OIL_TOTAL=?,
	 * ALARM_STATUS=0,ALARM_ADD_INFO_END =? WHERE ALARM_ID = ?
	 * 
	 * @throws SQLException
	 */
	public void updateVehicleAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCache) throws SQLException {
		Connection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			stUpdateVehicleAlarm =dbCon.prepareStatement(updateVehicleAlarmSQL);
			
			stUpdateVehicleAlarm.setLong(1, vehicleMessage.getUtc());
			
			if (vehicleMessage.getLat()!=null){
				stUpdateVehicleAlarm.setLong(2, vehicleMessage.getLat());
			}else{
				stUpdateVehicleAlarm.setNull(2, Types.INTEGER);
			}
			
			if (vehicleMessage.getLon()!=null){
				stUpdateVehicleAlarm.setLong(3, vehicleMessage.getLon());
			}else{
				stUpdateVehicleAlarm.setNull(3, Types.INTEGER);
			}
			
			stUpdateVehicleAlarm.setLong(4,vehicleMessage.getMaplat());
			stUpdateVehicleAlarm.setLong(5,vehicleMessage.getMaplon());
			stUpdateVehicleAlarm.setLong(6, vehicleMessage.getElevation());
			if (vehicleMessage.getDir()!=null){
				stUpdateVehicleAlarm.setInt(7, vehicleMessage.getDir());
			}else{
				stUpdateVehicleAlarm.setNull(7, Types.INTEGER);
			}
			if (vehicleMessage.getSpeed()!=null){
				stUpdateVehicleAlarm.setInt(8, vehicleMessage.getSpeed());
			}else{
				stUpdateVehicleAlarm.setNull(8, Types.INTEGER);
			}
			
			if (vehicleMessage.getMileage()!=null&&vehicleMessage.getMileage()>=0){
				stUpdateVehicleAlarm.setLong(9, vehicleMessage.getMileage());
			}else{
				stUpdateVehicleAlarm.setNull(9,  Types.INTEGER);
			}
			
			if (vehicleMessage.getOil()!=null&&vehicleMessage.getOil()>=0){
				stUpdateVehicleAlarm.setLong(10, vehicleMessage.getOil());
			}else{
				stUpdateVehicleAlarm.setNull(10,  Types.INTEGER);
			}
			

			stUpdateVehicleAlarm.setString(11, alarmCache.getAlarmadd());
			stUpdateVehicleAlarm.setString(12, alarmCache.getAlarmId());
			stUpdateVehicleAlarm.executeUpdate();

		} finally {
			if (stUpdateVehicleAlarm != null) {
				stUpdateVehicleAlarm.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}
	
	
}
