package com.caits.analysisserver.database;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleResultSet;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.IllegalOptionsAlarmBean;
import com.caits.analysisserver.bean.IllegalOptionsCacheBean;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;

public class TmpOracleDBAdapter{
	
	private static final Logger logger = LoggerFactory.getLogger(TmpOracleDBAdapter.class);

	private static String saveVehicleAlarmSQL;
	private static String updateVehicleAlarmSQL;
	private static String saveVehicleAlarmEventSQL;
	private static String queryIllegalOptionsSQL;
	
	public static Map<String, IllegalOptionsAlarmBean> illeOptAlarmMap = new ConcurrentHashMap<String, IllegalOptionsAlarmBean>();
	
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
	public static void initDBAdapter()
			throws Exception {
		
		saveVehicleAlarmSQL= SQLPool.getinstance().getSql("sql_saveVehicleAlarm");
		updateVehicleAlarmSQL= SQLPool.getinstance().getSql("sql_updateVehicleAlarm");
		saveVehicleAlarmEventSQL= SQLPool.getinstance().getSql("sql_saveVehicleAlarmEvent");
		queryIllegalOptionsSQL = SQLPool.getinstance().getSql("sql_queryIllegealOperationsAlarm");
		
	}
	
	/**
	 * 存储报警信息
	 * 
	 * INSERT INTO TH_VEHICLE_ALARM (ALARM_ID,VID,UTC,
	 * LAT,LON,MAPLON,MAPLAT,ELEVATION,DIRECTION,
	 * GPS_SPEED,MILEAGE,OIL_TOTAL,ALARM_CODE,
	 * SYSUTC,ALARM_STATUS,ALARM_START_UTC,ALARM_DRIVER,
	 * VEHICLE_NO,BGLEVEL,BASESTATUS
	 * ,EXTENDSTATUS,ALARM_ADD_INFO_START,ALARM_SRC,ALARM_ADD_INFO) VALUES (?,
	 * ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 2, ?)
	 * 
	 * @throws SQLException
	 * 
	 */
	public static void saveVehicleAlarm(VehicleMessageBean vehicleMessage)
			throws SQLException {

		ResultSet rs = null;
		OracleConnection dbCon = null;
		OraclePreparedStatement stSaveVehicleAlarm =null;
		try {
			// 从连接池获得连接
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vehicleMessage.getVid());
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
			stSaveVehicleAlarm.setLong(10, vehicleMessage.getSpeed());
			stSaveVehicleAlarm.setLong(11, vehicleMessage.getMileage());
			stSaveVehicleAlarm.setLong(12, vehicleMessage.getOil());
			stSaveVehicleAlarm.setString(13, vehicleMessage.getAlarmcode());
			stSaveVehicleAlarm.setLong(14, System.currentTimeMillis());
			stSaveVehicleAlarm.setInt(15, 1);
			stSaveVehicleAlarm.setLong(16, vehicleMessage.getUtc());
			stSaveVehicleAlarm.setString(17, "");
			stSaveVehicleAlarm.setString(18, info.getVehicleNo());
			stSaveVehicleAlarm.setString(19, vehicleMessage.getBglevel());
			stSaveVehicleAlarm.setString(20, "");
			stSaveVehicleAlarm.setString(21, "");
			stSaveVehicleAlarm.setString(22, vehicleMessage.getAlarmadd());
			stSaveVehicleAlarm.setString(23, vehicleMessage.getAlarmAddInfo());// 告警具体描述
			String areaId = vehicleMessage.getAreaId();
			if (areaId==null||"".equals(areaId)){
				stSaveVehicleAlarm.setNull(24, Types.VARCHAR);
			}else{
				stSaveVehicleAlarm.setString(24, areaId);
			}
			
			stSaveVehicleAlarm.executeUpdate();
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
	public static void updateVehicleAlarm(VehicleMessageBean vehicleMessage)
			throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stUpdateVehicleAlarm =null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateVehicleAlarm = (OraclePreparedStatement) dbCon
					.prepareStatement(updateVehicleAlarmSQL);
			stUpdateVehicleAlarm.setExecuteBatch(1);

			stUpdateVehicleAlarm.setLong(1, vehicleMessage.getUtc());
			stUpdateVehicleAlarm.setLong(2, vehicleMessage.getLat());
			stUpdateVehicleAlarm.setLong(3, vehicleMessage.getLon());
			stUpdateVehicleAlarm.setLong(4, vehicleMessage.getMaplat());
			stUpdateVehicleAlarm.setLong(5, vehicleMessage.getMaplon());
			stUpdateVehicleAlarm.setLong(6, vehicleMessage.getElevation());
			stUpdateVehicleAlarm.setInt(7, vehicleMessage.getDir());
			stUpdateVehicleAlarm.setLong(8, vehicleMessage.getSpeed());
			stUpdateVehicleAlarm.setLong(9, vehicleMessage.getMileage());
			stUpdateVehicleAlarm.setLong(10, vehicleMessage.getOil());

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
	 * C_VIN,LINE_NAME,CORP_ID,CORP_NAME,TEAM_ID, TEAM_NAME,ALARM_SRC)
	 * VALUES(SEQ_ALARMID_EVENT_ID
	 * .NEXTVAL,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
	 * ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)
	 * 
	 * @param vehicleMessage
	 * @param illeOptAlarmCache
	 * @throws SQLException
	 * 
	 */
	public static void saveVehicleAlarmEvent(VehicleMessageBean vehicleMessage,
			IllegalOptionsCacheBean illeOptAlarmCache) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stSaveVehicleAlarmEvent =null;
		try {
			VehicleInfo alarmVehicleBean = AnalysisDBAdapter.queryVechileInfo(vehicleMessage.getVid());
			if (alarmVehicleBean != null) {

				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveVehicleAlarmEvent = (OraclePreparedStatement) dbCon
						.prepareStatement(saveVehicleAlarmEventSQL);
				stSaveVehicleAlarmEvent.setExecuteBatch(1);
				
				stSaveVehicleAlarmEvent.setString(1, vehicleMessage.getVid());
				stSaveVehicleAlarmEvent.setString(2,
						vehicleMessage.getCommanddr());
				stSaveVehicleAlarmEvent.setString(3,
						illeOptAlarmCache.getAlarmcode());

				if (illeOptAlarmCache.getAREA_ID() != null && !"".equals(illeOptAlarmCache.getAREA_ID())) {
					stSaveVehicleAlarmEvent.setString(4,
							illeOptAlarmCache.getAREA_ID());
				} else {
					stSaveVehicleAlarmEvent.setNull(4, Types.VARCHAR);
				}

				if (illeOptAlarmCache.getMtypeCode() != null) {
					stSaveVehicleAlarmEvent.setString(5,
							illeOptAlarmCache.getMtypeCode());
				} else {
					stSaveVehicleAlarmEvent.setString(5, null);
				}

				if (illeOptAlarmCache.getMediaUrl() != null) {
					stSaveVehicleAlarmEvent.setString(6,
							illeOptAlarmCache.getMediaUrl());
				} else {
					stSaveVehicleAlarmEvent.setString(6, null);
				}

				VehicleMessageBean beginVmb = illeOptAlarmCache.getBeginVmb();

				stSaveVehicleAlarmEvent.setLong(7,
						illeOptAlarmCache.getBegintime());
				stSaveVehicleAlarmEvent.setLong(8, beginVmb.getLat());
				stSaveVehicleAlarmEvent.setLong(9, beginVmb.getLon());
				stSaveVehicleAlarmEvent.setLong(10, beginVmb.getMaplat());
				stSaveVehicleAlarmEvent.setLong(11, beginVmb.getMaplon());
				stSaveVehicleAlarmEvent.setInt(12, beginVmb.getElevation());
				stSaveVehicleAlarmEvent.setInt(13, beginVmb.getDir());
				stSaveVehicleAlarmEvent.setLong(14, beginVmb.getSpeed());

				VehicleMessageBean endVmb = illeOptAlarmCache.getEndVmb();

				stSaveVehicleAlarmEvent.setLong(15,
						illeOptAlarmCache.getEndTime());
				stSaveVehicleAlarmEvent.setLong(16, endVmb.getLat());
				stSaveVehicleAlarmEvent.setLong(17, endVmb.getLon());
				stSaveVehicleAlarmEvent.setLong(18, endVmb.getMaplat());
				stSaveVehicleAlarmEvent.setLong(19, endVmb.getMaplon());
				stSaveVehicleAlarmEvent.setInt(20, endVmb.getElevation());
				stSaveVehicleAlarmEvent.setInt(21, endVmb.getDir());
				stSaveVehicleAlarmEvent.setLong(22, endVmb.getSpeed());
				stSaveVehicleAlarmEvent.setDouble(23, (illeOptAlarmCache
						.getEndTime() - illeOptAlarmCache.getBegintime())/1000);
				stSaveVehicleAlarmEvent.setLong(24,
						illeOptAlarmCache.getMaxSpeed());

				if (illeOptAlarmCache.getVlineId() != null && !"".equals(illeOptAlarmCache.getVlineId())) {
					stSaveVehicleAlarmEvent.setString(25,
							illeOptAlarmCache.getVlineId());
				} else {
					stSaveVehicleAlarmEvent.setNull(25, Types.VARCHAR);
				}
				stSaveVehicleAlarmEvent.setString(26,
						alarmVehicleBean.getInnerCode());
				stSaveVehicleAlarmEvent.setString(27,
						alarmVehicleBean.getVehicleNo());
				stSaveVehicleAlarmEvent.setLong(28,
						illeOptAlarmCache.getMileage());
				stSaveVehicleAlarmEvent.setLong(29, illeOptAlarmCache.getOil());
				stSaveVehicleAlarmEvent.setString(30,
						alarmVehicleBean.getVinCode());

				if (illeOptAlarmCache.getLineName() != null) {
					stSaveVehicleAlarmEvent.setString(31,
							illeOptAlarmCache.getLineName());
				} else {
					stSaveVehicleAlarmEvent.setString(31, null);
				}

				stSaveVehicleAlarmEvent.setString(32,
						alarmVehicleBean.getEntId());
				stSaveVehicleAlarmEvent.setString(33,
						alarmVehicleBean.getEntName());
				stSaveVehicleAlarmEvent.setString(34,
						alarmVehicleBean.getTeamId());
				stSaveVehicleAlarmEvent.setString(35,
						alarmVehicleBean.getTeamName());
				stSaveVehicleAlarmEvent.setInt(36,
						illeOptAlarmCache.getAlarmSrc()==0?1:illeOptAlarmCache.getAlarmSrc());
				
				logger.debug("1, "+vehicleMessage.getVid()+
						"2,"+
						vehicleMessage.getCommanddr()+
						"3,"+
						"7,"+illeOptAlarmCache.getBegintime()+
						"8,"+ beginVmb.getLat()
						+"9,"+ beginVmb.getLon()+
						"10,"+ beginVmb.getMaplat()+
						"11,"+ beginVmb.getMaplon()+
						"12,"+ beginVmb.getElevation()+
						"13,"+ beginVmb.getDir()+
						"14,"+ beginVmb.getSpeed()+
						"15,"+illeOptAlarmCache.getEndTime()+
						"16,"+ endVmb.getLat()+
						"17,"+ endVmb.getLon()+
						"18,"+ endVmb.getMaplat()+
						"19,"+ endVmb.getMaplon()+
						"20,"+ endVmb.getElevation()+
						"21,"+ endVmb.getDir()+
						"22,"+ endVmb.getSpeed()+
						"23,"+ (illeOptAlarmCache
						.getEndTime() - illeOptAlarmCache.getBegintime())+
						"24,"+
						illeOptAlarmCache.getMaxSpeed()+"26,"+
						alarmVehicleBean.getInnerCode()+"27,"+
						alarmVehicleBean.getVehicleNo()+"28,"+
						illeOptAlarmCache.getMileage()+
						"29,"+ illeOptAlarmCache.getOil()+
						"30,"+
						alarmVehicleBean.getVinCode()+"32,"+
						alarmVehicleBean.getEntId()+"33,"+
						alarmVehicleBean.getEntName()+"34,"+
						alarmVehicleBean.getTeamId()+"35,"+
						alarmVehicleBean.getTeamName()+"36,"+
						illeOptAlarmCache.getAlarmSrc());
				stSaveVehicleAlarmEvent.executeUpdate();
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			if (stSaveVehicleAlarmEvent != null) {
				stSaveVehicleAlarmEvent.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}
	
	public static void queryIllegalOptionsAlarm() {
		OraclePreparedStatement stQueryIllegalOptionsAlarm= null;
		OracleResultSet rs = null;
 
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryIllegalOptionsAlarm = (OraclePreparedStatement) conn.prepareStatement(queryIllegalOptionsSQL);
			rs = (OracleResultSet) stQueryIllegalOptionsAlarm.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("VID");
				IllegalOptionsAlarmBean illegalOptAlarmBean = new IllegalOptionsAlarmBean();
				illegalOptAlarmBean.setVid(vid);//车辆ID
				illegalOptAlarmBean.setEntId(rs.getString("ENT_ID"));//所属企业
				illegalOptAlarmBean.setStartTime(rs.getString("START_TIME"));//开始时间
				illegalOptAlarmBean.setEndTime(rs.getString("END_TIME"));//结束时间
				illegalOptAlarmBean.setDeferred(rs.getLong("DEFERRED"));//持续时间
				illegalOptAlarmBean.setIsDefault(rs.getString("ISDEFAULT"));//是否默认配置（1、是）
				
				//key：车vid,value:该车对应的非法运营软报警配置
				illeOptAlarmMap.put(vid, illegalOptAlarmBean);
				
			}// End while
		} catch (SQLException e) {
			logger.error("查询到非法运营软报警配置车辆总数-ERROR-数据库异常"+e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (stQueryIllegalOptionsAlarm != null) {
					stQueryIllegalOptionsAlarm.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到非法运营软报警配置车辆出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
}
