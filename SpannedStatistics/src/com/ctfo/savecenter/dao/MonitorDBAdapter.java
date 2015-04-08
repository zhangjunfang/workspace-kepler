package com.ctfo.savecenter.dao;

import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleResultSet;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.beans.ServiceUnit;
import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.lingtu.xmlconf.XmlConf;

/**
 * 数据库访问类
 */
public class MonitorDBAdapter extends DBAdapter {

	private static final Logger logger = LoggerFactory.getLogger(MonitorDBAdapter.class);
	/** 查询所有车辆 */
	private String queryAllVehicle = null;
	/** 查询已删除的车辆 */
	private static String queryDeleteVehicle = null;
	/** 根据macid查询车辆基本信息 **/
	private static String queryVehicleByMacid = null;
	/**
	 * 查询语句初始化
	 * 
	 * @param dbDriver 数据库驱动
	 * @param dbConString  数据库连接字
	 * @param dbUserName  数据库用户名
	 * @param dbPassword  数据库密码
	 * @param reconnectWait 数据库断线重新连接时间(秒)
	 */
	public void initDBAdapter(XmlConf config) throws Exception {
		this.config = config;
		// 查询实体ID所属企业，车队
		queryAllVehicle = config.getStringValue("database|sqlstatement|sql_queryAllVehicle");
		if (TempMemory.getVehicleMapSize() == 0) {
			queryAllVehicle(queryAllVehicle);
		}
		// 查询已删除车辆
		queryDeleteVehicle = config.getStringValue("database|sqlstatement|sql_queryDeleteVehicle");
		// 根据macid查询车辆基本信息
		queryVehicleByMacid = config.getStringValue("database|sqlstatement|sql_queryVehicleByMacid");

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
		OraclePreparedStatement stQueryAllVehicle = null;
		OracleResultSet rs = null;
		String oemcode;
		String t_identifyno;
		ServiceUnit su;
		// 从连接池获得连接
		OracleConnection conn = null;

		try {

			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAllVehicle = (OraclePreparedStatement) conn.prepareStatement(sql);
			stQueryAllVehicle.setExecuteBatch(100);
			rs = (OracleResultSet) stQueryAllVehicle.executeQuery();

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
				su.setAreacode(rs.getLong("CITY_ID"));
				TempMemory.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				TempMemory.setAreaLastMap(String.valueOf(su.getVid()), String.valueOf(su.getAreacode()));
			}// End while

			// logger.info("查询到车辆总数:" + TempMemory.getVehicleMapSize());
		} catch (SQLException e) {
			logger.error("查询所有车辆" + e.getMessage());
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
			stQueryDeleteVehicle = (OraclePreparedStatement) conn.prepareStatement(queryDeleteVehicle);

			stQueryDeleteVehicle.setLong(1, time);
			rs = (OracleResultSet) stQueryDeleteVehicle.executeQuery();
			while (rs.next()) {
				map.put(rs.getLong("vid"), 0L);
			}
			return map;
		} catch (Exception e) {
			logger.error("查询已删除车辆出错." + e.getMessage());
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryDeleteVehicle != null) {
					stQueryDeleteVehicle.close();

				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询车机出错." + e.getMessage());
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
			conn = (OracleConnection)OracleConnectionPool.getConnection();
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
}
