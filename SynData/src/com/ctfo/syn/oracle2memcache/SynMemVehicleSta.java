package com.ctfo.syn.oracle2memcache;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeoutException;

import org.apache.log4j.Logger;

import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.syn.membeans.StaticMemcache;
import com.ctfo.syn.membeans.StatisticsVehicle;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;


/**
 * 首页车辆信息统计同步到memcache服务 (45秒)
 * @author xuehui
 *
 */
public class SynMemVehicleSta implements Runnable{

	public static Logger logger = Logger.getLogger(SynMemVehicleSta.class);
	
	public SynMemVehicleSta() {
	}
	
	/**
	 * 初始化同步服务
	 */
	public void run() {
		logger.info("开始执行车辆统计查询");
		initMemVehicleStatistics(StaticMemcache.STATISTICS_TYPE_NETWORK, StaticMemcache.NETWORK_VEHICLE);
		initMemVehicleStatistics(StaticMemcache.STATISTICS_TYPE_OPER, StaticMemcache.STATISTICS_VEHICLE_OPERATION_STATE);
		initMemVehicleStatistics(StaticMemcache.STATISTICS_TYPE_ONLINE, StaticMemcache.STATISTICS_VEHICLE_OPERATION_ONLINE_STATE);
		initMemVehicleStatistics(StaticMemcache.STATISTICS_TYPE_DRIVING, StaticMemcache.STATISTICS_VEHICLE_OPERATION_DRIVING_STATE);
		System.gc();
	}
	
	/**
	 * 初始化同步服务
	 */
	private void initMemVehicleStatistics(String type, String staType) {
		List<StatisticsVehicle> trServiceunitList = new ArrayList<StatisticsVehicle>();
		JedisUnifiedStorageService jedis = null;
		try {
			jedis = RedisServer.getJedisServiceInstance();
			
			trServiceunitList = getVehicleStatistics(type);
			
			for (StatisticsVehicle trServiceunit : trServiceunitList) {
				StringBuffer key = new StringBuffer().append(staType).append(String.valueOf(trServiceunit.getEntId()));
				jedis.saveVehicleStatistics(key.toString(), String.valueOf(trServiceunit.getStatisticVehicle())); 
//				jedis.set(0, key.toString(), RedisJsonUtil.objectToJson(trServiceunit.getStatisticVehicle()));//TODO
//				ConnectMemcachePool.getSqlMap(SynPool.getinstance().getSql("memcacheMainAddr")).set(key.toString(), 0, trServiceunit.getStatisticVehicle());
			}// End for
		} catch (TimeoutException e) {
			logger.error(e);
		} catch (InterruptedException e) {
			logger.error(e);
		} catch (Exception e) {
			logger.error(e);
		}finally {
			if(trServiceunitList != null && trServiceunitList.size() > 0){
				trServiceunitList.clear();
			}
		}	
	}
	
	/**
	 * 查询车辆统计
	 */
	private List<StatisticsVehicle> getVehicleStatistics(String type) {	
		List<StatisticsVehicle> trServiceunitList = new ArrayList<StatisticsVehicle>();
		
		Connection conn = null;
		
		PreparedStatement pstmt = null;
		
		ResultSet rs = null;	
		
		try {		
			conn = OracleConnectionPool.getConnection();
			logger.debug("oracle数据库连接成功");
			
			if(type != null && !type.equals("")) {
				
				if(type.equals(StaticMemcache.STATISTICS_TYPE_NETWORK)){
					pstmt = conn.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_network_vehicle"));
				}
				
				if(type.equals(StaticMemcache.STATISTICS_TYPE_OPER)){
					pstmt = conn.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_operation_vehicle"));
				}
				
				if(type.equals(StaticMemcache.STATISTICS_TYPE_ONLINE)){
					pstmt = conn.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_online_vehicle"));
				}
				
				if(type.equals(StaticMemcache.STATISTICS_TYPE_DRIVING)){
					pstmt = conn.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_driving_vehicle"));
				}
				
			}
			
			rs = pstmt.executeQuery();
			
			while (rs.next()) {
				StatisticsVehicle sta = new StatisticsVehicle();
				sta.setEntId(rs.getLong("entId"));
				sta.setStatisticVehicle(rs.getLong("statisticVehicle"));
				trServiceunitList.add(sta);
			}
			
		} catch (Exception e) {
			logger.error("获取车辆统计数据失败：",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(pstmt != null) {
					pstmt.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				logger.error("CLOSED PROPERTIES,ORACLE TO FAIL.",ex);
			}
		}
		
		return trServiceunitList;
	}

	/**
	 * 获取数据库链接
	 */
//	private DataBaseConnection getDBC() {
//		
//		return DataBaseConnection.getInstance(dbdriver, dburl, username, password);
//		
//	}
	
}
