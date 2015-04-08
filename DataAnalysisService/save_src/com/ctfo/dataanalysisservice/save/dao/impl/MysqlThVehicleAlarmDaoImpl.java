package com.ctfo.dataanalysisservice.save.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.List;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;
import com.ctfo.dataanalysisservice.database.DBConnectionManager;
import com.ctfo.dataanalysisservice.save.dao.ThVehicleAlarmDao;

/**
 * MYSQL存储
 * 
 * @author yangyi
 * 
 */
public class MysqlThVehicleAlarmDaoImpl implements ThVehicleAlarmDao {

	private static final Logger logger = Logger
			.getLogger(MysqlThVehicleAlarmDaoImpl.class);

	/*
	 * (non-Javadoc)
	 * 
	 * @see
	 * com.ctfo.dataanalysisservice.save.dao.ThVehicleAlarmDao#save(com.ctfo
	 * .dataanalysisservice.beans.ThVehicleAlarm)
	 */
	@Override
	public boolean save(List<ThVehicleAlarm> alarm) {

		//logger.info("####alarm" + alarm.get(0).getVid());
		Connection conn = DBConnectionManager.getMysqlConnection();
		//logger.info("1111alarm" + alarm.get(0).getVid());

		String sql = "insert into MEM_TH_VEHICLE_ALARM(" + "ALARM_ID," + // 1
																			// 报警ID
				"VID," + // 2 车辆ID
				"UTC," + // 3 GPS报警时间UTC
				"LAT," + // 4 报警位置纬度（单位：1/600000度）
				"LON," + // 5 报警位置经度（单位：1/600000度）
				"ELEVATION," + // 6 海拔(m)
				"DIRECTION," + // 7 GPS方向单位：0--359度,正北为0 顺时针
				"GPS_SPEED," + // 8 报警时刻速度 (单位：1/10千米/小时)
				"ALARM_CODE," + // 9 报警代码
				"SYSUTC," + // 10 系统时间utc
				"ALARM_STATUS," + // 11 报警处理状态0：未处理
				"ALARM_START_UTC," + // 12 报警开始时间UTC
				"ALARM_HANDLER_STATUS," + // 13 报警处理状态0：未处理
				"ALARM_DRIVER," + // 14 当班司机
				"MILEAGE," + // 15 里程(单位：千米)
				"OIL_TOTAL," + // 16 累计油耗 单位：1bit=0.5L 0=0L
				"VEHICLE_NO," + // 17 车牌号
				"ALARM_ADD_INFO_START,MAPLAT,MAPLON)" + // 18 报警附加信息
				" values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";
		try {

			/*
			 * conn.setAutoCommit(false); Statement st=conn.createStatement();
			 * for(String sql :sqls){ st.addBatch(sql); } st.executeBatch();
			 * con.commit();
			 */
			conn.setAutoCommit(false);

			PreparedStatement ps = conn.prepareStatement(sql);
			for (ThVehicleAlarm bo : alarm) {
				ps.setObject(1, bo.getAlarmId());
				ps.setObject(2, bo.getVid());
				ps.setObject(3, bo.getUtc());
				ps.setObject(4, bo.getLat());
				ps.setObject(5, bo.getLon());
				ps.setObject(6, bo.getElevation());
				ps.setObject(7, bo.getDirection());
				ps.setObject(8, bo.getGpsSpeed());
				ps.setObject(9, bo.getAlarmCode());
				ps.setObject(10, bo.getSysutc());
				ps.setObject(11, 0);// 报警状态0：未处理 默认
				ps.setObject(12, bo.getAlarmStartUtc());
				ps.setObject(13, 0);// 报警处理状态0：未处理 默认
				ps.setObject(14, bo.getAlarmDriver());
				ps.setObject(15, bo.getMileage());
				ps.setObject(16, bo.getOilTotal());
				ps.setObject(17, bo.getVehicleNo());
				ps.setObject(18, bo.getAlarmAddInfoStart());
				ps.setObject(19, bo.getMaplat());
				ps.setObject(20, bo.getMaplon());
				logger.info(bo.getAlarmId()+"mysql alarmInsert save");
				ps.addBatch();
			}

			/*
			 * for(ThVehicleAlarm bo : alarm){ ps.setObject(,); .............
			 * ps.addBatch(); }
			 */
			ps.executeBatch();
			// ps.execute();
			conn.commit();
			logger.info("####alarmInsert执行");
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			logger.error("####alarmERROR=" + e.fillInStackTrace());
		} finally {
			try {
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}

		return false;
	}

	@Override
	public boolean update(List<ThVehicleAlarm> alarm) {

	//	logger.info("####alarm" + alarm.get(0).getVid());
		Connection conn = DBConnectionManager.getMysqlConnection();
		//logger.info("1111alarm" + alarm.get(0).getVid());

		String sql = " UPDATE MEM_TH_VEHICLE_ALARM " + "set "
				+ "ALARM_END_UTC = ?, " + // 1 报警结束时间UTC
				"END_LAT = ? , " + // 2 报警位置纬度
				"END_LON = ? , " + // 3 报警位置经度
				"END_ELEVATION = ? , " + // 4 海拔(m)
				"END_DIRECTION = ? , " + // 5 GPS方向单位：度,正北为0
				"END_GPS_SPEED = ? , " + // 6 报警时刻速度cm/s->km/h*36/1000
				"END_MILEAGE = ? , " + // 7 里程(单位：千米)
				"END_OIL_TOTAL= ? , END_MAPLAT=?,END_MAPLON=? " + // 8 累计油耗 单位：1bit=0.5L 0=0L*/
				"where ALARM_ID=? "; // 9 更新条件， 报警ID

		try {
			/*
			 * conn.setAutoCommit(false); Statement st=conn.createStatement();
			 * for(String sql :sqls){ st.addBatch(sql); } st.executeBatch();
			 * con.commit();
			 */

			conn.setAutoCommit(false);
			PreparedStatement ps = conn.prepareStatement(sql);
			for (ThVehicleAlarm bo : alarm) {
				ps.setObject(1, bo.getAlarmEndUtc());
				ps.setObject(2, bo.getEndLat());
				ps.setObject(3, bo.getEndLon());
				ps.setObject(4, bo.getEndElevation());
				ps.setObject(5, bo.getEndDirection());
				ps.setObject(6, bo.getEndGpsSpeed());
				ps.setObject(7, bo.getEndMileage());
				ps.setObject(8, bo.getEndOilTotal());
				 ps.setObject(9, bo.getEndMaplat());
				 ps.setObject(10, bo.getEndMaplon());
				ps.setObject(11, bo.getAlarmId());
				logger.info(bo.getAlarmId()+" mysql alarmupdate  update");
				ps.addBatch();
			}

			/*
			 * for(ThVehicleAlarm bo : alarm) { ps.setObject(,); ......
			 * ps.addBatch(); }
			 */

			ps.executeBatch();
			// ps.execute();
			conn.commit();
			logger.info("####mysql alarmupdate执行");
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			logger.error("####alarmERROR=" + e.fillInStackTrace());
		} finally {
			try {
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
		return false;
	}

}
