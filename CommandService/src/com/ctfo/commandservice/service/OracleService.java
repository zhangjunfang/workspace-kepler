package com.ctfo.commandservice.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Types;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.dao.OracleConnectionPool;
import com.ctfo.commandservice.model.CustomIssued;
import com.ctfo.commandservice.model.CustomUpload;
import com.ctfo.commandservice.model.OilInfo;
import com.ctfo.commandservice.model.OilSpill;
import com.ctfo.commandservice.util.ConfigLoader;
import com.ctfo.commandservice.util.DateTools;
import com.ctfo.commandservice.util.Utils;
import com.ctfo.generator.pk.GeneratorPK;
import com.encryptionalgorithm.Point;

public class OracleService {
	private static final Logger logger = LoggerFactory.getLogger(OracleService.class);
	/** 存储油量变化SQL	 */
	private static String sql_saveOilChanged;
	/** 存储油量信息SQL	 */
	private static String sql_saveOilInfo;
	/** 存储偷漏油报警SQL	 */
	private static String sql_saveStealingOilAlarm;
	/** 更新自定义指令状态SQL	 */
	private static String sql_saveCustomCommand;
	/** 存储自定义上报指令SQL	 */
	private static String sql_saveCustomCommandDetail;
	
	public static void init(){
		sql_saveOilChanged = ConfigLoader.config.get("sql_saveOilChanged");
		sql_saveOilInfo = ConfigLoader.config.get("sql_saveOilInfo");
		sql_saveStealingOilAlarm = ConfigLoader.config.get("sql_saveStealingOilAlarm");
		
		sql_saveCustomCommand = ConfigLoader.config.get("sql_saveCustomCommand");
		sql_saveCustomCommandDetail = ConfigLoader.config.get("sql_saveCustomCommandDetail");
	}
	/**
	 * 更新或者插入油箱油量标定信息
	 * @param info
	 * TODO
	 */
	public static void saveOilInfo(OilInfo info) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveOilInfo);
			ps.setInt(1, info.getOilCalibration());
			ps.setInt(2, info.getGap());
			ps.setInt(3, info.getRefuelThreshold());
			ps.setInt(4, info.getStealThreshold());
			ps.setString(5, info.getSeq());
			ps.setString(6, info.getVid());
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("更新或者插入油箱油量标定信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新或者插入油箱油量标定信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储油量变化记录 当为偷油告警和加油提示作为变化记录。
	 * 
	 * @param app
	 * @throws SQLException
	 * TODO
	 */
	public static void saveOilChanged(OilInfo oilInfo) throws SQLException {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String changeId = GeneratorPK.instance().getPKString();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveOilChanged);
			ps.setString(1, oilInfo.getStatus()); // CHANGE_TYPE
			ps.setString(2, oilInfo.getVid()); // VID
			ps.setLong(3, DateTools.shortDateConvertUtc(oilInfo.getTime()));// UTC
			ps.setLong(4, oilInfo.getLat()); // LAT
			ps.setLong(5, oilInfo.getLon()); // LON
			Point point = Utils.convertLatLon(oilInfo.getLon(), oilInfo.getLat());
			if (point != null) {
				ps.setLong(6, Math.round(point.getX() * 600000)); // MAPLON
				ps.setLong(7, Math.round(point.getY() * 600000)); // MAPLAT
			} else {
				ps.setNull(6, Types.INTEGER);
				ps.setNull(7, Types.INTEGER);
			}
			ps.setInt(8, oilInfo.getElevation()); 			// ELEVATION 海拔
			ps.setInt(9, oilInfo.getDirection()); 			// DIRECTION 方向
			ps.setLong(10, oilInfo.getSpeed()); 			// GPS_SPEED 速度
			ps.setLong(11, DateTools.getCurrentUtcMsDate());// SYSUTC 系统时间
			ps.setLong(12, oilInfo.getFuelLevel()); 		// CURR_OILLEVEL 燃油液位
			ps.setLong(13, oilInfo.getOilAllowance()); 		// CURR_OILMAS 当前油量
			ps.setLong(14, oilInfo.getOilChange()); 		// CHANGE_OILMASS 变化油量
			ps.setString(15, changeId); 					// CHANGE_ID 变化油量编号
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储油量变化记录异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储油量变化记录关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储偷漏油报警
	 * @param oilInfo
	 * @param sql_saveStealingOilAlarm
	 * TODO
	 */
	public static void saveStealingOilAlarm(OilSpill oilSpill,long endUtc) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String uuid = GeneratorPK.instance().getPKString();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveStealingOilAlarm);
			long utc = DateTools.getStringToDate(oilSpill.getTime());
			ps.setString(1, uuid);			//	ALARM_ID
			ps.setString(2, oilSpill.getVid());			//	VID
			ps.setLong(3, utc);//	UTC
			ps.setLong(4, oilSpill.getLat());//	LAT
			ps.setLong(5, oilSpill.getLon());//	LON
			Point point = Utils.convertLatLon(oilSpill.getLon(), oilSpill.getLat());//	
			long maplon = 0;
			long maplat = 0;
			if (point != null) {
				maplon = Math.round(point.getX() * 600000);
				maplat = Math.round(point.getY() * 600000);
				ps.setLong(6, maplon);//	MAPLON
				ps.setLong(7, maplat);//	MAPLAT
			} else {
				ps.setNull(6, Types.INTEGER);
				ps.setNull(7, Types.INTEGER);
			}
			ps.setLong(8, oilSpill.getElevation());//	ELEVATION
			ps.setLong(9, oilSpill.getDirection());//	DIRECTION
			ps.setLong(10, oilSpill.getSpeed());//	GPS_SPEED
			ps.setString(11, "111"); // 报警CODE ALARM_CODE
			ps.setLong(12, utc);//	SYSUTC
			ps.setLong(13, utc);//	ALARM_START_UTC
			ps.setString(14, "A005"); // BGLEVEL	所属类型
			ps.setLong(15, endUtc);//	ALARM_END_UTC	
			ps.setLong(16, oilSpill.getLat());//	END_LAT
			ps.setLong(17, oilSpill.getLon());//	END_LON
			if (point != null) {
				ps.setLong(18, maplat);	//	END_MAPLAT
				ps.setLong(19, maplon);//	END_MAPLON
			} else {
				ps.setNull(18, Types.INTEGER);
				ps.setNull(19, Types.INTEGER);
			}
			ps.setLong(20, oilSpill.getElevation());	//	END_ELEVATION
			ps.setLong(21, oilSpill.getDirection());	//	END_DIRECTION
			ps.setLong(22, oilSpill.getSpeed());	//	END_GPS_SPEED
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储偷油告警异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储偷油告警关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	
	/**
	 * 更新自定义指令状态
	 * @param issuedList
	 * @param batchSize 
	 * TODO
	 */
	public static int saveCustomCommand(List<CustomIssued> issuedList, int batchSize) { 
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			int result = 0;
			int index = 0;
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveCustomCommand);
			for(CustomIssued issued : issuedList){
				index++;
				result++;
				ps.setString(1, issued.getId()); //自定义指令下发ID
				ps.setString(2, issued.getSeq()); //报文流水号
				ps.setString(3, issued.getVid()); //车辆ID
				ps.setInt(4, issued.getStatus()); //发送状态(-1:等待回应，0:发送成功，2:发送失败，4:车辆不在线)
				ps.setString(5, issued.getCreateId()); //创建人ID
				ps.setLong(6, issued.getCreateUtc()); //创建时间
				ps.addBatch();
				if(index == batchSize){
					ps.executeBatch();
					index = 0;
				}
			}
			if(index > 0){
				ps.executeBatch();
			}
			return result;
		} catch (SQLException e) {
			logger.error("更新自定义指令状态异常:" + e.getMessage(), e);
			return 0;
		} finally {
			close(conn, ps);
		}
	}
	/**
	 * 存储自定义上报指令
	 * @param uploadList
	 * @param batchSize 
	 * TODO
	 */
	public static int saveCustomCommandDetail(List<CustomUpload> uploadList, int batchSize) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			int result = 0;

			int index = 0;
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveCustomCommandDetail);
			for(CustomUpload upload : uploadList){
				index++;
				result++;
				ps.setString(1, upload.getSeq()); 		//指令序列号
				ps.setLong(2, upload.getUtc()); 		//时间
				ps.setString(3, upload.getType()); 		//报文类型
				ps.setString(4, upload.getContent()); 	//报文内容
				ps.setInt(5, upload.getCommandType()); 	//指令类型
				ps.addBatch();
				if(index == batchSize){
					ps.executeBatch();
					index = 0;
				}
			}
			if(index > 0){
				ps.executeBatch();
			}
			return result;
		} catch (SQLException e) {
			logger.error("存储自定义上报指令异常:" + e.getMessage(), e);
			return 0;
		} finally {
			close(conn, ps);
		}
	}
	/**
	 * 关闭连接资源
	 * @param conn
	 * @param ps
	 */
	private static void close(Connection conn, PreparedStatement ps){
		try {
			if(ps != null){
				ps.close();
			}
			if(conn != null){
				conn.close();
			}
		} catch (Exception e) {
			logger.error("关闭连接失败:" + e.getMessage(), e);
		}
	}

}
