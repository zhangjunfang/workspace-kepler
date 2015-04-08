/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task DriverAuthenticationTask.java	</li><br>
 * <li>时        间：2013-8-21  下午7:16:43	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.model.StatisticsVehicle;
import com.ctfo.syncservice.util.Constant;
import com.ctfo.syncservice.util.TaskAdapter;

/*****************************************
 * <li>描        述：入网统计信息同步任务		
 * 
 *****************************************/
public class NetWorkStatisticsTask extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(NetWorkStatisticsTask.class);

	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("NetWorkStatisticsTask");
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		try {
			initMemVehicleStatistics(Constant.STATISTICS_TYPE_NETWORK, Constant.NETWORK_VEHICLE, "入网车辆");
			initMemVehicleStatistics(Constant.STATISTICS_TYPE_OPER, Constant.OPERATION_VEHICLE, "营运车辆");
			initMemVehicleStatistics(Constant.STATISTICS_TYPE_ONLINE, Constant.ONLINE_VEHICLE, "在线车辆 ");
			initMemVehicleStatistics(Constant.STATISTICS_TYPE_DRIVING, Constant.DRIVING_VEHICLE, "行驶车辆");
			long end = System.currentTimeMillis();
			logger.info("--syncNetWork--车辆统计结束 ,总耗时:[{}]ms", end -start);
		} catch (Exception e) {
			logger.error("入网统计信息异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 初始化同步服务
	 */
	private void initMemVehicleStatistics(String type, String staType, String desc) {
		long start = System.currentTimeMillis();
		List<StatisticsVehicle> trServiceunitList = new ArrayList<StatisticsVehicle>(); 
		Jedis  jedis = null;
		Map<String, String> statisticsMap = new HashMap<String, String>();
		try {
			trServiceunitList = getVehicleStatistics(type);
			long s1 = System.currentTimeMillis();
			if(trServiceunitList != null && trServiceunitList.size() > 0){
				for(StatisticsVehicle sv : trServiceunitList){
					statisticsMap.put(String.valueOf(sv.getEntId()), String.valueOf(sv.getStatisticVehicle()));
				}
				jedis = this.redis.getJedisConnection();
				jedis.select(5);
				jedis.hmset(staType, statisticsMap);
			}
			long s2 = System.currentTimeMillis();
			logger.info("--syncNetWork--统计{}结束, 查询数据耗时:({})ms, 存入redis耗时:({})ms, 处理数据({})条, 总耗时:({})ms", desc, s1 - start, s2 - s1, trServiceunitList.size(), s2 - start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error(e.getMessage());
		} finally {
			if(jedis != null){
				this.redis.returnJedisConnection(jedis);
			}
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
			conn = this.oracle.getConnection();
			if(conn == null){
				logger.error("无法获取数据库连接,请检查(1)数据库连接(2)数据库配置");
				return null;
			}
			if(type != null && !type.equals("")) {
				if(type.equals(Constant.STATISTICS_TYPE_NETWORK)){
					pstmt = conn.prepareStatement(conf.get("sql_network"));
				}
				if(type.equals(Constant.STATISTICS_TYPE_OPER)){
					pstmt = conn.prepareStatement(conf.get("sql_rates"));
				}
				if(type.equals(Constant.STATISTICS_TYPE_ONLINE)){
					pstmt = conn.prepareStatement(conf.get("sql_online"));
				}
				if(type.equals(Constant.STATISTICS_TYPE_DRIVING)){
					pstmt = conn.prepareStatement(conf.get("sql_driving"));
				}
			}
			rs = pstmt.executeQuery();
			while (rs.next()) {
				StatisticsVehicle sta = new StatisticsVehicle();
				sta.setEntId(rs.getString("entId"));
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
}
