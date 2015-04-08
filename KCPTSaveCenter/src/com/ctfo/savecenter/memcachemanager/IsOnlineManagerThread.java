package com.ctfo.savecenter.memcachemanager;


import java.sql.SQLException;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.addin.kcpt.trackmanager.TrackManagerKcptMainThread;
import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.util.CDate;
import com.lingtu.xmlconf.XmlConf;

/**
 * 文件存储线程
 * 
 * @author 刘志伟
 * 
 */
public class IsOnlineManagerThread extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(IsOnlineManagerThread.class);
	//计数器
	private int index = 0 ;
	//计时器
	private long tempTime = System.currentTimeMillis();
	// 线程id
	private int nId = 0;
	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(100000);
//
//	/** 更新MYSQL最新位置表 */
//	private PreparedStatement stUpdateTrackIsonline = null;
//
//	/** 更新ORACLE最新位置表 */
//	private OraclePreparedStatement updateOracleLastTrack = null;

	/** 更新ORACLE上下线位置表 */
	private OraclePreparedStatement stUpdateIsonlineTbl = null;

	/** 存储ORACLE上下线位置表 */
	private OraclePreparedStatement stSaveIsonlineTbl = null;
//
//	private String updateVehicleisonline = null;
//
//	private String sql_updateOracleLastTrack = null;

	private String updateVehicleisonlineTbl = null;

	private String saveOracleLastTrackTbl = null;

	public IsOnlineManagerThread(int id, XmlConf config, String nodeName)
			throws SQLException, ClassNotFoundException {

		this.nId = id;

//		// 更新MYSQL最新位置表
//		updateVehicleisonline = config.getStringValue(nodeName
//				+ "|mysql_sql_updateVehicleisonline");
//
//		// 更新ORACLE最新位置表
//		sql_updateOracleLastTrack = config.getStringValue(nodeName
//				+ "|sql_updateTrackisonline");

		// 更新ORACLE最新位置表
		updateVehicleisonlineTbl = config.getStringValue(nodeName
				+ "|sql_updateVehicleisonlineTbl");

		// 存储ORACLE最新位置表
		saveOracleLastTrackTbl = config.getStringValue(nodeName
				+ "|sql_saveOracleLastTrackTbl");
	}

	public void addPacket(Map<String, String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(Constant.SPACES,e);
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}
	@Override
	public void run() {
		logger.info("更新车辆上下线线程" + nId + "启动");

		while (TrackManagerKcptMainThread.isRunning) {
			try {
				Map<String, String> packet = vPacket.take();

				String parm = packet.get("18");
				String parms[] = parm.split("/");
				Long vid = Long.parseLong(packet.get(Constant.VID));
				//String msgid = packet.get(Constant.MSGID);
				if ("0".equals(parms[0]) || "1".equals(parms[0])) {

					String macid = packet.get(Constant.MACID);
					String isonline = TempMemory
							.getvehicleIsonlineMapValue(macid);
					if (isonline != null) {
						if ("1".equals(parms[0]) && "1".equals(isonline)) {
							updateOffOnLineTbl(parms[0], vid); // 更新上一次上线
							saveOffOnLineTbl(parms[0], vid,
									packet.get(Constant.VEHICLENO)); // 插入新的上线
						}

						if ("0".equals(parms[0]) && "1".equals(isonline)) {
							updateOffOnLineTbl(parms[0], vid);
							TempMemory.setvehicleIsonlineMapValue(macid,
									parms[0]);

						}

						if ("1".equals(parms[0]) && "0".equals(isonline)) {
							saveOffOnLineTbl(parms[0], vid,
									packet.get(Constant.VEHICLENO)); // 插入新的上线
							TempMemory.setvehicleIsonlineMapValue(macid,
									parms[0]);

						}
					} else {
						if ("1".equals(parms[0])) {
							saveOffOnLineTbl(parms[0], vid,
									packet.get(Constant.VEHICLENO)); // 插入新的上线
						}
						TempMemory.setvehicleIsonlineMapValue(macid,
								parms[0]);

					}

					// 更新线程
//					mysqlUpdateIsonline(parms[0], vid, msgid);
//
//					oracleUpdateIsonline(parms[0], vid, msgid);

					logger.debug("保存车辆上下线:" + macid);
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime- tempTime) > 3000){
					logger.warn("isonline--:" + nId + ",保存车辆上下线更新剩余:" + vPacket.size() +",3秒处理数据:"+index+"条");
					index = 0;
					tempTime = currentTime;
				}
				index++;
			} catch (Exception e)// 报文格式错误
			{
				logger.error("车辆上下线线程" + nId + " 报文解析错误 "+ e.getMessage());
			}
		}// End while
	}

	/***
	 * 存储上下线表
	 * 
	 * @throws SQLException
	 */
	private void saveOffOnLineTbl(String isOffOnLine, long vid, String vechileNo)
			throws SQLException {
		// 从连接池获得连接
		OracleConnection dbCon = null;
		try {
			
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveIsonlineTbl = (OraclePreparedStatement)dbCon.prepareStatement(saveOracleLastTrackTbl);
			stSaveIsonlineTbl.setExecuteBatch(1);
			stSaveIsonlineTbl.setLong(1, vid);
			stSaveIsonlineTbl.setLong(2, CDate.getCurrentUtcMsDate());
			stSaveIsonlineTbl.setInt(3, 1);
			stSaveIsonlineTbl.setString(4, vechileNo);
			stSaveIsonlineTbl.executeUpdate();
		} finally {
			if (stSaveIsonlineTbl != null) {
				stSaveIsonlineTbl.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 更新上下线表
	 * 
	 * @throws SQLException
	 */
	private void updateOffOnLineTbl(String isOffOnLine, long vid)
			throws SQLException {
		// 从连接池获得连接
		OracleConnection dbCon = null;
		try {
			dbCon = (OracleConnection)OracleConnectionPool.getConnection();
			stUpdateIsonlineTbl = (OraclePreparedStatement)dbCon.prepareStatement(updateVehicleisonlineTbl);
			stUpdateIsonlineTbl.setExecuteBatch(1);
			stUpdateIsonlineTbl.setInt(1, 0);
			stUpdateIsonlineTbl.setLong(2, CDate.getCurrentUtcMsDate());
			stUpdateIsonlineTbl.setLong(3, vid); // 待定修改
			stUpdateIsonlineTbl.executeUpdate();
		} finally {
			if (stUpdateIsonlineTbl != null) {
				stUpdateIsonlineTbl.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}
//
//	/****
//	 * 更新车辆上下线
//	 * 
//	 * @param isOnline
//	 * @param vid
//	 * @throws SQLException 
//	 * @throws SQLException
//	 */
//	private void mysqlUpdateIsonline(String isOnline, Long vid, String msgid) throws SQLException{
//		Connection mysqlConn = null;
//		try {
//			mysqlConn = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//			stUpdateTrackIsonline = mysqlConn.prepareStatement(updateVehicleisonline);
//			stUpdateTrackIsonline.setInt(1, Integer.parseInt(isOnline));
//			stUpdateTrackIsonline.setLong(2, CDate.getCurrentUtcMsDate());
//			stUpdateTrackIsonline.setString(3, msgid);
//			stUpdateTrackIsonline.setLong(4, vid);
//			stUpdateTrackIsonline.executeUpdate();
//		}catch(Exception ex){
//			logger.error(ex);
//		} finally {
//			if (stUpdateTrackIsonline != null) {
//				stUpdateTrackIsonline.close();
//			}
//
//			if (mysqlConn != null) {
//				mysqlConn.close();
//			}
//		}
//	}
//
//	/***
//	 * 更新车辆上下线
//	 * 
//	 * @param isOnline
//	 * @param vid
//	 * @throws SQLException
//	 */
//
//	private void oracleUpdateIsonline(String isOnline, Long vid, String msgid)
//			throws SQLException {
//		OracleConnection dbCon = null;
//		try {
//			// 从连接池获得连接
//			dbCon = (OracleConnection)ConnectionManager.getInstance().getConnection();
//			updateOracleLastTrack = (OraclePreparedStatement)dbCon.prepareStatement(sql_updateOracleLastTrack);
//			updateOracleLastTrack.setExecuteBatch(1);
//			updateOracleLastTrack.setInt(1, Integer.parseInt(isOnline));
//			updateOracleLastTrack.setLong(2, CDate.getCurrentUtcMsDate());
//			updateOracleLastTrack.setString(3, msgid);
//			updateOracleLastTrack.setLong(4, vid);
//			updateOracleLastTrack.executeUpdate();
//		} finally {
//			if (updateOracleLastTrack != null) {
//				updateOracleLastTrack.close();
//			}
//
//			if (dbCon != null) {
//				dbCon.close();
//			}
//		}
//	}
}
