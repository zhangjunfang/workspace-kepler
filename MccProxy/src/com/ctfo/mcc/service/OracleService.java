package com.ctfo.mcc.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mcc.dao.OracleConnectionPool;
import com.ctfo.mcc.model.Dispatch;
import com.ctfo.mcc.model.DispatchRules;
import com.ctfo.mcc.model.Vehicle;
import com.ctfo.mcc.utils.ConfigLoader;

public class OracleService{
	private static Logger logger = LoggerFactory.getLogger(OracleService.class);
	/**	解绑电子围栏SQL	*/
	private static String sql_unBindArea;
	/**	更新围栏编号SQL	*/
	private static String sql_updateAreaId; 
	/**	查询调度信息执行SQL	*/
	private static String sql_queryDispatchRules;
	/**	查询调度信息执行SQL	*/
	private static String sql_updateDispatchRules;
	/**	查询调度规则关联车辆SQL	*/
	private static String sql_queryRulesVehicle;
	/**	存储定时调度信息SQL	*/
	private static String sql_insertTimingDispatch;
	public OracleService(){
	}
	
	public static void init(){
		sql_unBindArea = ConfigLoader.appParam.get("sql_unBindArea");
		sql_updateAreaId = ConfigLoader.appParam.get("sql_updateAreaId");
		sql_queryDispatchRules = ConfigLoader.appParam.get("sql_queryDispatchRules");
		sql_updateDispatchRules = ConfigLoader.appParam.get("sql_updateDispatchRules");
		sql_queryRulesVehicle = ConfigLoader.appParam.get("sql_queryRulesVehicle");
		sql_insertTimingDispatch = ConfigLoader.appParam.get("sql_insertTimingDispatch");
	}
	
	/**
	 * 解绑电子围栏
	 * @param map
	 * @return
	 */
	public static void updateBindAreaStatus(String seq, long sysUtc){
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_unBindArea);
			ps.setLong(1, sysUtc);
			ps.setString(2, seq);
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("解绑电子围栏异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("解绑电子围栏关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 更新电子围栏信息
	 * @param map
	 * @return
	 */
	public static void updateAreaStatus(String areaId){
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateAreaId);
			ps.setString(1, areaId);
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("更新电子围栏信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新电子围栏信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 查询调度规则信息
	 * @return
	 */
	public static List<DispatchRules> queryDispatchRules() {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		List<DispatchRules> list = new ArrayList<DispatchRules>();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryDispatchRules);
			rs = ps.executeQuery();
			while(rs.next()){
				DispatchRules dr = new DispatchRules();
				dr.setId(rs.getString("ID"));
				dr.setName(rs.getString("NAME"));
				dr.setContent(rs.getString("CONTENT"));
				dr.setCreateBy(rs.getString("CREATEBY"));
				dr.setCreateUtc(rs.getLong("CREATEUTC"));
				dr.setStartUtc(rs.getLong("STARTUTC"));
				dr.setEndUtc(rs.getLong("ENDUTC"));
				dr.setSendTime(rs.getString("SENDTIME"));
				dr.setSendCycle(rs.getString("SENDCYCLE"));
				dr.setType(rs.getString("TYPE")); 
				dr.setIsOffline(rs.getInt("ISOFFLINE"));
				dr.setOfflineCycle(rs.getInt("OFFLINECYCLE"));
				dr.setOracleSysUtc(rs.getLong("SYSUTC"));
				
				list.add(dr);
			}
			return list;
		} catch (Exception ex) {
			logger.error("查询调度规则信息异常:" + ex.getMessage(), ex);
			return null;
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询调度规则信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 更新定时调度规则状态
	 * @param expiredId
	 * @return
	 */
	public static int updateDispatchRules(List<String> expiredId) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateDispatchRules);
			for(String expired : expiredId){
				ps.setString(1, expired);
				ps.addBatch();
				index++;
			}
			ps.executeBatch();
			return index;
		} catch (Exception ex) {
			logger.error("更新定时调度规则状态异常:" + ex.getMessage(), ex);
			return 0;
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新定时调度规则状态关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	
	/**
	 * 查询调度规则对应车辆列表
	 * @param id
	 * @return
	 */
	public static List<Vehicle> queryRulesVehicle(String id) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		List<Vehicle> list = new ArrayList<Vehicle>();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryRulesVehicle);
			ps.setString(1, id); 
			rs = ps.executeQuery();
			while(rs.next()){
				Vehicle v = new Vehicle();
				v.setVid(rs.getString("VID"));
				v.setPlate(rs.getString("PLATE"));
				v.setPlateColor(rs.getString("PLATECOLOR")); 
				
				list.add(v);
			}
			return list;
		} catch (Exception ex) {
			logger.error("查询调度规则对应车辆列表异常:" + ex.getMessage(), ex);
			return null;
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询调度规则对应车辆列表关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 存储定时调度信息
	 * @param dispatchList
	 */
	public static void insertTimingDispatch(List<Dispatch> dispatchList) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_insertTimingDispatch);
			for(Dispatch dispatch : dispatchList){
				ps.setString(1, dispatch.getId());
				ps.setString(2, dispatch.getVid());
				ps.setString(3, dispatch.getPlate());
				ps.setString(4, dispatch.getPlateColor());
				ps.setLong(5, dispatch.getCreateUtc());
				ps.setLong(6, dispatch.getSendUtc());
				ps.setInt(7, dispatch.getTypeFlag());
				ps.setString(8, dispatch.getContent());
				ps.setString(9, dispatch.getSeq());
				ps.setString(10, dispatch.getCreateBy());
				ps.addBatch();
			}
			ps.executeBatch(); 
			conn.commit(); 
		} catch (Exception ex) {
			logger.error("存储定时调度信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储定时调度信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	
	
}
