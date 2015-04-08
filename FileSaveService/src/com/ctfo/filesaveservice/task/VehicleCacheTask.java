/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.dao.OracleConnectionPool;
import com.ctfo.filesaveservice.model.ServiceUnit;
import com.ctfo.filesaveservice.util.Cache;
import com.ctfo.filesaveservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：车辆缓存同步任务
 * 
 *****************************************/
public class VehicleCacheTask extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(VehicleCacheTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
	/** 全量同步标记		*/
	private static boolean allFlag = true;
	/** 车辆缓存最近更新时间		*/
	private static long authUtc = System.currentTimeMillis();
	/** 全量查询语句		*/
	private static String sql_syncAll;
	/** 增量查询语句		*/
	private static String sql_syncIncrements;
	/** 3g全量查询语句		*/
	private static String sql_syncAll_3g;
	/** 3g增量查询语句		*/
	private static String sql_syncIncrements_3g;
	
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		try {
			setName("VehicleCacheTask");
			sql_syncAll = config.get("sql_syncAll");
			sql_syncIncrements = config.get("sql_syncIncrements");
			sql_syncAll_3g = config.get("sql_syncAll_3g");
			sql_syncIncrements_3g = config.get("sql_syncIncrements_3g");
			long interval = Long.parseLong(config.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			execute();
		} catch (Exception e) {
			logger.error("车辆缓存任务启动异常:" + e.getMessage(), e); 
		}
	}
	
	/*****************************************
	 * <li>描 述：同步车辆缓存 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		long lastUtc = authUtc;
		int index = 0;
		int error = 0;
		String syncType = "";
		Connection conn = null;
		Connection conn_3g = null;
		PreparedStatement ps = null;
		PreparedStatement ps_3g = null;
		ResultSet rs = null;
		ResultSet rs_3g = null;
		try {
//			判断清理时间间隔
			if((start - lastTime) > clearInterval){
				allFlag = true;
				lastTime = System.currentTimeMillis();
			}
			conn = OracleConnectionPool.getConnection();
			conn_3g = OracleConnectionPool.getConnection();
			if(allFlag){
				ps = conn.prepareStatement(sql_syncAll);
				ps_3g = conn_3g.prepareStatement(sql_syncAll_3g);
				syncType = "全量同步";
			} else {
				ps = conn.prepareStatement(sql_syncIncrements);
				ps_3g = conn_3g.prepareStatement(sql_syncIncrements_3g);
				for(int i = 1; i <= 8; i++){
					ps.setLong(i, lastUtc);
					ps_3g.setLong(i, lastUtc);
				}
				ps_3g.setLong(9, lastUtc);
				ps_3g.setLong(10, lastUtc);
				syncType = "增量同步";
			}
			long s0 = System.currentTimeMillis();
			rs = ps.executeQuery();
			long s1 = System.currentTimeMillis();
			Map<String, ServiceUnit> map = new ConcurrentHashMap<String, ServiceUnit>();
			while (rs.next()) {
				try {
					ServiceUnit su = new ServiceUnit();
					String oemcode = rs.getString("oemcode");// 车机类型码
					String t_identifyno = rs.getString("t_identifyno");// 终端标识号
					su.setMacid(oemcode + "_" + t_identifyno);
					su.setSuid(rs.getString("suid"));
					su.setPlatecolorid(rs.getString("plate_color_id"));
					su.setVid(rs.getString("vid"));
					su.setTeminalCode(rs.getString("tmodel_code"));
					su.setTid(rs.getString("tid"));
					su.setCommaddr(t_identifyno);
					su.setOemcode(oemcode);
					su.setVehicleno(rs.getString("vehicle_no"));
					su.setVinCode(rs.getString("VIN_CODE"));
					map.put(oemcode + "_" + t_identifyno, su);
					index++;
				} catch (Exception e) {
					logger.error("查询结果写入缓存异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			
			long s2 = System.currentTimeMillis();
			rs_3g = ps_3g.executeQuery();
			long s3 = System.currentTimeMillis();
			while (rs_3g.next()) {
				try {
					ServiceUnit su = new ServiceUnit();
					String oemcode = rs_3g.getString("oemcode");// 车机类型码
					String t_identifyno = rs_3g.getString("dvr_simnum");// 终端标识号
					String mac_id = oemcode + "_" + t_identifyno;
					su.setMacid(mac_id);
					su.setSuid(rs_3g.getString("suid"));
					su.setPlatecolorid(rs_3g.getString("plate_color_id"));
					su.setVid(rs_3g.getString("vid"));
					su.setTeminalCode(rs_3g.getString("tmodel_code"));
					su.setTid(rs_3g.getString("tid"));
					su.setCommaddr(rs_3g.getString("t_identifyno"));
					su.setOemcode(oemcode);
					su.setVehicleno(rs_3g.getString("vehicle_no"));
					su.setVinCode(rs_3g.getString("VIN_CODE"));
					map.put(mac_id, su);
					index++;
				} catch (Exception e) {
					logger.error("查询结果写入缓存异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			long s4 = System.currentTimeMillis();
			if(map.size() > 0){
				if(allFlag){ // 清理已经删除或者解绑的车辆信息（垃圾信息清理）
//					1. 获取历史缓存所有KEYS，存储到SET集合中
					Set<String> oldKeys = Cache.getVehicleKeySet();
					if(oldKeys != null && oldKeys.size() > 0){
						int oldSize = oldKeys.size();
//						2. 获取当前缓存所有KEYS，对比出差集
						Set<String> newKeys = map.keySet();
						if(newKeys != null && newKeys.size() > 0){
							oldKeys.removeAll(newKeys);
							logger.info("缓存中当前数量:[{}], 当前查询到的数量:[{}], 要删除的车辆数量:[{}]", oldSize, newKeys.size(), oldKeys.size());
							if(oldKeys != null && oldKeys.size() > 0){
								Cache.removeKeys(oldKeys); 
							}
						}
					}
					allFlag = false;
				}
				Cache.putAllVehicleMap(map);
				map.clear();
			}
			long end = System.currentTimeMillis();
			logger.info("{}同步车辆缓存结束,处理数据:[{}]条, 正常:[{}]条, 异常:[{}]条 , 获取连接耗时:{}ms, 查询车辆耗时:[{}]ms, 处理车辆耗时:[{}]ms, 查询3g车辆耗时:[{}]ms, 3g处理耗时:[{}]ms, 缓存耗时:[{}]ms, 总耗时[{}]ms", syncType, (index + error), index, error, (s0 - start), (s1 -s0),(s2 -s1), s3 - s2, s4-s3, end - s4, end -start);
		} catch (Exception e) {
			logger.error("同步车辆缓存异常:" + e.getMessage(), e);
		} finally {
			try {
				if(rs != null){ rs.close(); }
				if(ps != null){ ps.close(); }
				if(conn != null){ conn.close(); }
				if(rs_3g != null){ rs_3g.close(); }
				if(ps_3g != null){ ps_3g.close(); }
				if(conn_3g != null){ conn_3g.close(); }
			} catch (SQLException e) {
				logger.error("同步车辆缓存关闭资源异常:" + e.getMessage(), e);
			}
			authUtc = start - 60000;
		}
	}
}
