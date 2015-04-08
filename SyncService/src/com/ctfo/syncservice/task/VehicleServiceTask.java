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
import java.sql.SQLException;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.dao.OracleDataSource;
import com.ctfo.syncservice.util.TaskAdapter;

/*****************************************
 * <li>描        述：车辆服务信息同步任务		
 * 
 *****************************************/ 
public class VehicleServiceTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(VehicleServiceTask.class);
	/**	全量同步最近处理时间	*/
	private static long fullSyncLastTime = 0;
	/**	最近处理时间	*/
	private static long lastTime = System.currentTimeMillis();
	/**	时间误差限制	*/
	private static long limit = 3 * 60 * 1000;
	/**	全量同步间隔(默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	全量同步SQL	*/
	private static String sql_syncAll;
	/**	增量同步SQL	*/
	private static String sql_syncIncrements;
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		try {
			setName("VehicleServiceTask");
			sql_syncAll = conf.get("sql_syncAll");
			sql_syncIncrements = conf.get("sql_syncIncrements");
			String clearStr = conf.get("clearInterval");
			String limitStr = conf.get("limit");
			clearInterval = Long.parseLong(clearStr) * 60 * 1000;
			limit = Long.parseLong(limitStr) * 60 * 1000;
			logger.info("车辆信息缓存初始化完成, 全量同步间隔:[{}]分钟, 时间允许误差[{}]分钟", clearStr, limitStr);
		} catch (Exception e) {
			logger.error("车辆信息缓存初始化异常:" + e.getMessage(), e);
		} 
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 *  需要修改的时间为数据库记录最后一条时间
	 *****************************************/
	public void execute() {
		long currentTime = System.currentTimeMillis();
		try{
			long fullSyncInterval = currentTime - fullSyncLastTime; // 全量同步间隔
			if(fullSyncInterval > clearInterval){
				logger.info("----------------------全量同步一次----------------------");
				syncVehicleService(true, 0);
				fullSyncLastTime = currentTime;
				logger.debug(OracleDataSource.getPoolStackTrace());
			}else {
				long interval = currentTime - lastTime;
				syncVehicleService(false, currentTime - interval - limit);
			}
		}catch(Exception e){
			logger.error("车辆同步异常:" + e.getMessage(), e); 
		}finally{
			lastTime = currentTime;
		}
	}
	/**
	 * 同步车辆缓存到redis
	 * @param fullFlag 全量同步标识
	 * @param l 
	 */
	private void syncVehicleService(boolean fullFlag, long lastUpdateTime) {
		long start = System.currentTimeMillis();
		long s1 = start;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Jedis jedis = null;
		String sync = "增量";
		try {
			conn = this.oracle.getConnection();
			if(fullFlag){
				ps = conn.prepareStatement(sql_syncAll);
				sync = "全量";
			}else{
				ps = conn.prepareStatement(sql_syncIncrements);
				for(int i=1;i<=10;i++){
					ps.setLong(i, lastUpdateTime);
				}
			}
			rs = ps.executeQuery();
			Map<String, String> map = new ConcurrentHashMap<String, String>();
			int valid = 0;
			int invalid = 0;
			while(rs.next()){
				try{
					map.put(rs.getString("VID"), rs.getString("STR"));
					valid++;
				}catch(Exception e){
					logger.error("查询车辆缓存信息:vid:"+rs.getString("VID")+", str:"+rs.getString("STR")+" 异常:"+ e.getMessage(), e);
					invalid++;
				}
			}
			s1 = System.currentTimeMillis();
			jedis = this.redis.getJedisConnection();
			jedis.select(6);
			if(fullFlag){
				Set<String> newKeys = map.keySet();
				if(newKeys != null && newKeys.size() > 0){
					Set<String> oldKeys = jedis.keys("*");
					if(oldKeys != null && oldKeys.size() > 0){
						oldKeys.removeAll(newKeys);
						if(oldKeys.size() > 0){
							String[] keys = oldKeys.toArray(new String[oldKeys.size()]);
							if(keys != null && keys.length > 0){
								jedis.del(keys);
							}
						}
					}
				}
			}
			int cache = 0;
			for(Map.Entry<String, String> m : map.entrySet()){
				jedis.set(m.getKey(), m.getValue());
				cache++;
			}
			long end = System.currentTimeMillis();
			logger.info("{}查询车辆[{}]条, 缓存[{}]条, 总耗时[{}]ms, 正常[{}]条, 异常[{}]条, oracle查询[{}]ms, redis存储[{}]ms", sync, (valid+invalid), cache,(end-start), valid, invalid, s1-start, end-s1);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("查询车辆缓存信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(jedis != null){
					this.redis.returnJedisConnection(jedis);
				}
				if(rs != null){
					rs.close();	
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询车辆缓存信息异常:" + e.getMessage(), e);
			}
		}
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 *  需要修改的时间为数据库记录最后一条时间
	 *****************************************/
//	public void execute2() {
//		try{
//	//		1. 同步驾驶员信息
//			syncDriver();
//	//		2。 全量同步  或者 增量同步
//			if(status){
//				logger.info("----------------------全量同步一次----------------------");
//				syncFullVehicleService();
//				status = false;
//			}else {
//				logger.info("----------------------增量同步一次----------------------");
//				long start = System.currentTimeMillis() - 60000; //  向前60秒，解决服务器时间误差
//	//			3. 同步车辆服务信息
//				syncIncrementsVehicleService(flagTime);
//	//			4. 删除redis中oracle已经删除车辆
//				deleteRedisVehicleByOracle(flagTime);
//				flagTime = start;
//			}
//			logger.debug(OracleDataSource.getPoolStackTrace());
//	//		4. 同步结束后删除缓存驾驶员信息
//			Cache.clearVehicleDriverMap();
//		}catch(Exception e){
//			logger.error("车辆同步异常:" + e.getMessage(), e); 
//		}
//	}


	/*****************************************
	 * <li>描        述：删除redis中oracle已经删除车辆 		</li><br>
	 * <li>时        间：2013-8-30  下午4:27:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
//	private void deleteRedisVehicleByOracle(Long time) {
////		logger.info("删除redis中oracle已经删除车辆开始");
//		long start = System.currentTimeMillis();
//		int index = 0;
//		int error = 0;
//		Connection oracleConn = null;
//		PreparedStatement oraclePs = null;
//		ResultSet oracleRs = null;
//		Jedis jedis = null;
//		try {
//			jedis = this.redis.getJedisConnection();
//			jedis.select(6);
//			
//			oracleConn = this.oracle.getConnection();
//			oraclePs = oracleConn.prepareStatement("SELECT V.VID VID FROM TB_VEHICLE V WHERE (V.ENABLE_FLAG = 0 OR V.VEHICLE_STATE = 3) AND V.UPDATE_TIME > ?");
//			oraclePs.setLong(1, time);
//			
//			oracleRs = oraclePs.executeQuery();
//			while(oracleRs.next()){
//				try{
//				String vid = oracleRs.getString("VID");
//					if(jedis.exists(vid)){
//						jedis.del(vid); 
//						index++;
//					}
//					logger.info("删除车辆VID:" + vid);
//				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
//					logger.error("删除redis中oracle已经删除车辆 - 连接redis异常:" + ex.getMessage(), ex);
//					break;
//				}catch(Exception e){
//					logger.error("删除redis中oracle已经删除车辆异常:" + e.getMessage(), e);
//					error++;
//					continue;
//				}
//			}
//			long end = System.currentTimeMillis();
//			logger.info("删除redis中oracle已经删除车辆结束, 处理数据:({})条, 正常处理:({})条, 异常:({})条, 总耗时[{}]ms", (index + error), index, error, (end -start));
//		} catch (Exception e) {
//			if(jedis != null){
//				this.redis.returnBrokenResource(jedis);
//			}
//			logger.error("删除redis中oracle已经删除车辆异常:" + e.getMessage(), e);
//		} finally {
//			if(jedis != null){
//				this.redis.returnJedisConnection(jedis);
//			}
//			try {
//				oracleConn.close();
//				oraclePs.close();
//				oracleConn.close();
//			} catch (SQLException e) {
//				logger.error("删除redis中oracle已经删除车辆异常:" + e.getMessage(), e);
//			}
//		}
//	}


	/*****************************************
	 * <li>描        述：全量更新车辆服务信息 		</li><br>
	 * <li>时        间：2013-8-30  下午3:11:49	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
//	private void syncFullVehicleService() {
//		long start = System.currentTimeMillis();
//		int index = 0;
//		int error = 0;
//		Connection oracleConn = null;
//		PreparedStatement oraclePs = null;
//		ResultSet oracleRs = null;
//		Jedis jedis = null;
//		try {
//			jedis = this.redis.getJedisConnection();
//			jedis.select(6);
//			long s0 = System.currentTimeMillis();
//			oracleConn = this.oracle.getConnection();
//			oraclePs = oracleConn.prepareStatement(conf.get("sql_syncAll"));
//			oracleRs = oraclePs.executeQuery();
//			long s1 = System.currentTimeMillis();
//			while (oracleRs.next()) {
//				try {
//					String vid = oracleRs.getString("VID");
//					String vehicleTrack = preseTrack(oracleRs, vid);
//					jedis.set(vid,vehicleTrack);
//					index++;
//				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
//					logger.error("全量更新车辆服务信息 - 连接redis异常:" + ex.getMessage(), ex);
//					break;
//				} catch (Exception e) {
//					logger.error("全量更新车辆服务信息 - 写入redis异常:" + e.getMessage(), e);
//					error++;
//					continue;
//				}
//			}
//			long end = System.currentTimeMillis();
//			logger.info("--syncVehicleFull--全量更新车辆服务信息结束, 处理数据:({})条, 正常:({})条, 异常:({})条, 总耗时[{}]ms, oracle查询耗时:({})ms, redis存储耗时:({})ms", (index + error), index, error, (end -start), s1-s0, end - s1); 
//		} catch (Exception e) {
//			if(jedis != null){
//				this.redis.returnBrokenResource(jedis);
//			}
//			logger.error("全量更新车辆服务信息异常:" + e.getMessage(), e);
//		} finally {
//			if(jedis != null){
//				this.redis.returnJedisConnection(jedis);
//			}
//			try {
//				if(oracleRs != null){
//					oracleRs.close();
//				}
//				if(oraclePs != null){
//					oraclePs.close();
//				}
//				if(oracleConn != null){
//					oracleConn.close();
//				}
//			} catch (SQLException e) {
//				logger.error("全量更新车辆服务信息异常:" + e.getMessage(), e);
//			}
//		}
//		
//	}

	/*****************************************
	 * <li>描        述：同步驾驶员信息 		</li><br>
	 * <li>时        间：2013-8-30  上午10:51:33	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
//	private void syncDriver() {
////		logger.info("同步车辆服务信息--驾驶员--数据开始");
//		long start = System.currentTimeMillis();
//		Connection conn = null;
//		PreparedStatement ps = null;
//		ResultSet rs = null;
//		int index = 0;
//		int error = 0;
//		try {
//			conn = this.oracle.getConnection();
//			ps = conn.prepareStatement(conf.get("sql_syncDriver"));
//			rs = ps.executeQuery();
//			while (rs.next()) {
//				try {
//					Cache.saveVehicleDriverMap(rs.getString("vid"), rs.getString("drivername"));
//					index++;
//				} catch (Exception e) {
//					logger.error("同步车辆服务信息数据 --缓存驾驶员--异常:" + e.getMessage(), e);
//					error++;
//					continue;
//				}
//			}
//			long end = System.currentTimeMillis();
//			logger.info("车辆服务信息--缓存驾驶员--结束, 处理数据:({})条, 正常处理:({})条, 异常:({})条, 总耗时:[{}]ms", (index + error), index, error, (end -start));
//		} catch (Exception e) {
//			logger.error("缓存车辆服务信息--缓存驾驶员--异常:" + e.getMessage(), e);
//		} finally {
//			try {
//				rs.close();
//				ps.close();
//				conn.close();
//			} catch (SQLException e) {
//				logger.error("同步车辆服务信息--驾驶员--关闭资源异常:" + e.getMessage(), e);
//			}
//		}
//	}
	/*****************************************
	 * <li>描 述：增量更新车辆服务信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
//	private void syncIncrementsVehicleService(Long lastUpdateTime) {
//		int index = 0;
//		int error = 0;
//		long start = System.currentTimeMillis();
//		
//		Connection oracleConn = null;
//		PreparedStatement oraclePs = null;
//		ResultSet oracleRs = null;
//		Jedis jedis = null;
//		try {
//			jedis = this.redis.getJedisConnection();
//			jedis.select(6);
//			oracleConn = this.oracle.getConnection();
//			String sqls = conf.get("sql_syncIncrements");
//			logger.debug(sqls); 
//			oraclePs = oracleConn.prepareStatement(sqls);
//			oraclePs.setLong(1, lastUpdateTime);
//			oraclePs.setLong(2, lastUpdateTime);
//			oraclePs.setLong(3, lastUpdateTime);
//			oraclePs.setLong(4, lastUpdateTime);
//			oraclePs.setLong(5, lastUpdateTime);
//			oraclePs.setLong(6, lastUpdateTime);
//			oraclePs.setLong(7, lastUpdateTime);
//			oraclePs.setLong(8, lastUpdateTime);
//			oraclePs.setLong(9, lastUpdateTime);
//			oraclePs.setLong(10, lastUpdateTime);
//			oracleRs = oraclePs.executeQuery();
//			while (oracleRs.next()) {
//				try {
//					String 	vid 		= oracleRs.getString("VID");
//					String 	entId 		= oracleRs.getString("CORP_ID");
//					String 	entName 	= oracleRs.getString("CORP_NAME");
//					if(StringUtils.isBlank(entName)){
//						entName = "";
//					}
//					String 	teamId 		= oracleRs.getString("TEAM_ID");
//					String 	teamName 	= oracleRs.getString("TEAM_NAME");
//					if(StringUtils.isBlank(teamName)){
//						teamName = "";
//					}
//					String 	oemCode 	= oracleRs.getString("OEMCODE");
//					String 	simNum 		= oracleRs.getString("COMMADDR");
//					String 	tId 		= oracleRs.getString("TID");
//					String 	tMac 		= oracleRs.getString("T_MAC");
//					String 	employeeName 		= oracleRs.getString("CNAME");
//					if(StringUtils.isBlank(employeeName)){
//						employeeName = "";
//					}
//					String 	vehicleNo 	= oracleRs.getString("VEHICLENO");
//					int 	plateColorId= oracleRs.getInt("PLATE_COLOR_ID");
////					String 	areaCode	= oracleRs.getString("AREA_CODE");
////					更新或者插入轨迹信息
//					String vehicleTrack = null;
//					if(jedis.exists(vid)){
//						String trackInfo = jedis.get(vid);
//						String[] trackArray = trackInfo.split(":", 45);;
//						vehicleTrack = updateTrack(trackArray, vid, entId, entName, teamId, teamName, oemCode, simNum, tId, tMac, employeeName, vehicleNo, plateColorId);
//					}else {
//						vehicleTrack = saveTrack(vid, entId, entName, teamId, teamName, oemCode, simNum, tId, tMac, employeeName, vehicleNo, plateColorId);
//					} 
//					if(vehicleTrack.split(":", 45).length != 45){
//						vehicleTrack = getNullTrack();
//					}
//					jedis.set(vid, vehicleTrack);
//					index++;
//				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
//					logger.error("增量更新车辆服务信息 - 连接redis异常:" + ex.getMessage(), ex);
//					break;
//				} catch (Exception e) {
//					logger.error("增量更新车辆服务信息 - 写入redis异常:" + e.getMessage(), e);
//					error++;
//					continue;
//				}
//			}
//			
//			long end = System.currentTimeMillis();
//			logger.info("--syncVehicleIncrements--增量更新车辆服务信息结束, 处理数据:({})条, 正常处理:({})条, 异常:({})条, 总耗时:[{}]ms", (index + error), index, error, (end -start));
//		} catch (Exception e) {
//			if(jedis != null){
//				this.redis.returnBrokenResource(jedis);
//			}
//			logger.error("增量更新车辆服务信息异常:" + e.getMessage(), e);
//		} finally {
//			if(jedis != null){
//				this.redis.returnJedisConnection(jedis);
//			}
//			try {
//				oracleRs.close();
//				oraclePs.close();
//				oracleConn.close();
//			} catch (SQLException e) {
//				logger.error("增量更新车辆服务信息异常:" + e.getMessage(), e);
//			}
//		}
//	}
	


	/*****************************************
	 * <li>描        述：处理车辆服务信息 		</li><br>
	 * <li>时        间：2013-8-30  下午3:46:18	</li><br>
	 * <li>参数： @param rs
	 * <li>参数： @return			</li><br>
	 * @throws SQLException 
	 * 
	 *****************************************/
//	public String preseTrack(ResultSet resultSet, String vid) throws SQLException {
//		StringBuffer strBuf = new StringBuffer();
//		strBuf.append(resultSet.getLong("LAT")); // 0
//		strBuf.append(":"); 
//		strBuf.append(resultSet.getLong("LON")); // 1
//		strBuf.append(":");
//		strBuf.append(resultSet.getLong("MAPLON")); // 2
//		strBuf.append(":");
//		strBuf.append(resultSet.getLong("MAPLAT")); // 3
//		strBuf.append(":");
//		strBuf.append(resultSet.getLong("GPS_SPEED")); // 4
//		strBuf.append(":");
//		strBuf.append(resultSet.getLong("DIRECTION")); // 5
//		strBuf.append(":");
//		strBuf.append(resultSet.getLong("UTC")); // 6
//		strBuf.append(":");
//		if(null != resultSet.getString("ALARM_CODE")){
//			strBuf.append(resultSet.getString("ALARM_CODE")); // 7
//		}
//		
//		strBuf.append(":");
//		if (resultSet.getLong("ENGINE_ROTATE_SPEED") >= -1) { // 引擎转速（发动机转速） // 8
//			strBuf.append(resultSet.getLong("ENGINE_ROTATE_SPEED"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("OIL_INSTANT") >= -1) { // 瞬时油耗 // 9
//			strBuf.append(resultSet.getLong("OIL_INSTANT"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("OIL_PRESSURE") >= -1) { // 机油压力 // 10
//			strBuf.append(resultSet.getLong("OIL_PRESSURE"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("OIL_TEMPERATURE") >= -1) { // 机油温度（随位置汇报上传） 11
//			strBuf.append(resultSet.getLong("OIL_TEMPERATURE"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("EEC_APP") >= -1) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传） 12
//			strBuf.append(resultSet.getLong("EEC_APP"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("OIL_TOTAL") >= -1) { // 累计油耗 13
//			strBuf.append(resultSet.getLong("OIL_TOTAL"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("ENGINE_WORKING_LONG") >= -1) { // 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）14
//			strBuf.append(resultSet.getLong("ENGINE_WORKING_LONG"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("AIR_INFLOW_TPR") >= -1) { // 进气温度（随位置汇报上传）15
//			strBuf.append(resultSet.getLong("AIR_INFLOW_TPR"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("AIR_PRESSURE") >= -1) { // 大气压力（随位置汇报上传）16
//			strBuf.append(resultSet.getLong("AIR_PRESSURE"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("VEHICLE_SPEED") > 0) { // 脉冲车速（随位置汇报上传）17
//			strBuf.append(resultSet.getLong("VEHICLE_SPEED"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("BATTERY_VOLTAGE") >= -1) { // 终端内置电池电压（随位置汇报上传）18
//			strBuf.append(resultSet.getLong("BATTERY_VOLTAGE"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("E_WATER_TEMP") >= -1) { // 冷却液温度（随位置汇报上传）19
//			strBuf.append(resultSet.getLong("E_WATER_TEMP"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("EXT_VOLTAGE") >= -1) { // 车辆蓄电池电压（随位置汇报上传）20
//			strBuf.append(resultSet.getLong("EXT_VOLTAGE"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("E_TORQUE") >= -1) { // 发动机扭矩（随位置汇报上传）21
//			strBuf.append(resultSet.getLong("E_TORQUE"));
//		}
//		strBuf.append(":");
//		
//		if (resultSet.getLong("MILEAGE") >= -1) { // 累计里程 22
//			strBuf.append(resultSet.getLong("MILEAGE"));
//		}
//		strBuf.append(":");
//		
//		if(null != resultSet.getString("BASESTATUS")){ //基本状态位 23
//			strBuf.append(resultSet.getString("BASESTATUS"));
//		}
//		strBuf.append(":");
//		
//		if(null != resultSet.getString("EXTENDSTATUS")){ //扩展状态位 24
//			strBuf.append(resultSet.getString("EXTENDSTATUS"));
//		}
//
//		strBuf.append(":");
//		if(null != resultSet.getString("SPEED_FROM")){
//			strBuf.append(resultSet.getString("SPEED_FROM")); // 25车速来源
//		}
//		strBuf.append(":");
//	
//		if(resultSet.getLong("PRECISE_OIL") >= -1){ //计量仪油耗 // 26
//			strBuf.append(resultSet.getLong("PRECISE_OIL"));
//		}
//		strBuf.append(":");
//		
//		if(resultSet.getLong("ELEVATION") >= -1){
//			strBuf.append(resultSet.getLong("ELEVATION")); // 海拔 27
//		}
//		
//		strBuf.append(":");
//		
//		if(resultSet.getLong("OIL_MEASURE") >= -1){
//			strBuf.append(resultSet.getLong("OIL_MEASURE")); // 油箱存油量(升) 28
//		}
//		
//		strBuf.append(":");
//		
//		strBuf.append(vid); // vid 29
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("PLATE_COLOR_ID")); // 车牌颜色 30
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("VEHICLENO")); // 车牌号 31
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("COMMADDR")); // 手机号 32
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("TID")); // 终端ID 33
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("T_MAC")); // 终端型号 34
//		
//		strBuf.append(":");
//		
//		if(Cache.containsKey(vid)){
//			strBuf.append(Cache.getDriverNameByVid(vid)); // 驾驶员姓名 35
//		}
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("CORP_NAME"));  //所属组织 36
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("TEAM_ID")); // 车队id 37
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("CORP_ID")); // 企业id 38
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("OEM_CODE")); // OEMCODE 39
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("SYSUTC")); // 系统时间40
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("ISONLINE")); //  是否在线41
//		
//		strBuf.append(":");
//		
//		strBuf.append(resultSet.getString("ISVALID")); //  是否有效 42
//		
//		strBuf.append(":");
//		
//		if(null != resultSet.getString("MSGID")){
//			strBuf.append(resultSet.getString("MSGID")); //  MSGID 43
//		}
//		
//		strBuf.append(":");
//		
//		if(null != resultSet.getString("TEAM_NAME")){
//			strBuf.append(resultSet.getString("TEAM_NAME")); //  TEAM_NAME 车队名称  44
//		}
//		
//		return strBuf.toString();
//	}
	/*****************************************
	 * <li>描        述：更新轨迹信息 		</li><br>
	 * <li>时        间：2013-8-26  下午1:01:23	</li><br>
	 * <li>参数： @param tracktrackArrayay	原轨迹数组
	 * <li>参数： @param vid		车辆编号
	 * <li>参数： @param entId		组织编号	
	 * <li>参数： @param entName	组织名称
	 * <li>参数： @param teamId	车队编号
	 * <li>参数： @param teamName	车队名称
	 * <li>参数： @param oemCode	企业编码
	 * <li>参数： @param simNum	手机号
	 * <li>参数： @param tid		设备编号
	 * <li>参数： @param tMac		设备Mac
	 * <li>参数： @param employeeName	驾驶员名称
	 * <li>参数： @param vehicleNo		车牌号
	 * <li>参数： @param plateColorId	车牌颜色
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
//	public String updateTrack(String[] trackArray, String vid, String entId, String entName, String teamId, String teamName, String oemCode, String simNum, String tId, String tMac, String employeeName, String vehicleNo, int plateColorId) {
//		StringBuffer strBuf = new StringBuffer(512);
//		strBuf.append(trackArray[0]);
//		strBuf.append(":"); 
//		strBuf.append(trackArray[1]);
//		strBuf.append(":");
//		strBuf.append(trackArray[2]);
//		strBuf.append(":");
//		strBuf.append(trackArray[3]);
//		strBuf.append(":");
//		strBuf.append(trackArray[4]);
//		strBuf.append(":");
//		strBuf.append(trackArray[5]);
//		strBuf.append(":");
//		strBuf.append(trackArray[6]);
//		strBuf.append(":");
//		strBuf.append(trackArray[7]);
//		strBuf.append(":");
//		strBuf.append(trackArray[8]);
//		strBuf.append(":");
//		strBuf.append(trackArray[9]);
//		strBuf.append(":"); 
//		strBuf.append(trackArray[10]);
//		strBuf.append(":");
//		strBuf.append(trackArray[11]);
//		strBuf.append(":");
//		strBuf.append(trackArray[12]);
//		strBuf.append(":");
//		strBuf.append(trackArray[13]);
//		strBuf.append(":");
//		strBuf.append(trackArray[14]);
//		strBuf.append(":");
//		strBuf.append(trackArray[15]);
//		strBuf.append(":");
//		strBuf.append(trackArray[16]);
//		strBuf.append(":"); 
//		strBuf.append(trackArray[17]);
//		strBuf.append(":");
//		strBuf.append(trackArray[18]);
//		strBuf.append(":");
//		strBuf.append(trackArray[19]);
//		strBuf.append(":");
//		strBuf.append(trackArray[20]);
//		strBuf.append(":");
//		strBuf.append(trackArray[21]);
//		strBuf.append(":");
//		strBuf.append(trackArray[22]);
//		strBuf.append(":");
//		strBuf.append(trackArray[23]);
//		strBuf.append(":");
//		strBuf.append(trackArray[24]);
//		strBuf.append(":");
//		strBuf.append(trackArray[25]);
//		strBuf.append(":");
//		strBuf.append(trackArray[26]);
//		strBuf.append(":");
//		strBuf.append(trackArray[27]);
//		strBuf.append(":");
//		strBuf.append(trackArray[28]);
//		strBuf.append(":");
//		strBuf.append(trackArray[29]);
//		strBuf.append(":");
//		strBuf.append(plateColorId); // 车牌颜色
//		strBuf.append(":");
//		strBuf.append(vehicleNo); // 车牌号码
//		strBuf.append(":");
//		strBuf.append(simNum); // 手机号
//		strBuf.append(":");
//		strBuf.append(tId); // 终端ID
//		strBuf.append(":");
//		strBuf.append(tMac); // 终端型号
//		strBuf.append(":");
//		strBuf.append(employeeName); //驾驶员姓名
//		strBuf.append(":");
//		strBuf.append(entName); // 所属组织
//		strBuf.append(":");
//		strBuf.append(teamId); // 车队id
//		strBuf.append(":");
//		strBuf.append(entId); // 企业id 38
//		strBuf.append(":");
//		strBuf.append(oemCode); // OEMCODE 39
//		strBuf.append(":");
//		strBuf.append(trackArray[40]);
//		strBuf.append(":");
//		strBuf.append(trackArray[41]);
//		strBuf.append(":");
//		strBuf.append(trackArray[42]);
//		strBuf.append(":");
//		strBuf.append(trackArray[43]);
//		strBuf.append(":");
//		strBuf.append(trackArray[44]); //  TEAM_NAME 车队名称  44 
//		return strBuf.toString();
//	}
	/*****************************************
	 * <li>描        述：存储轨迹信息 		</li><br>
	 * <li>时        间：2013-8-26  下午1:07:08	</li><br>
	 * <li>参数： @param vid		车辆编号
	 * <li>参数： @param entId		组织编号	
	 * <li>参数： @param entName	组织名称
	 * <li>参数： @param teamId	车队编号
	 * <li>参数： @param teamName	车队名称
	 * <li>参数： @param oemCode	企业编码
	 * <li>参数： @param simNum	手机号
	 * <li>参数： @param tid		设备编号
	 * <li>参数： @param tMac		设备Mac
	 * <li>参数： @param employeeName	驾驶员名称
	 * <li>参数： @param vehicleNo		车牌号
	 * <li>参数： @param plateColorId	车牌颜色
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
//	public String saveTrack(String vid, String entId, String entName, String teamId, String teamName, String oemCode, String simNum, String tId, String tMac, String employeeName, String vehicleNo, int plateColorId) {
//		StringBuffer strBuf = new StringBuffer(512);
//		strBuf.append(0); // 0
//		strBuf.append(":"); 
//		strBuf.append(0); // 1
//		strBuf.append(":");
//		strBuf.append(0); // 2
//		strBuf.append(":");
//		strBuf.append(0); // 3
//		strBuf.append(":");
//		strBuf.append(0); // 4
//		strBuf.append(":");
//		strBuf.append(0); // 5
//		strBuf.append(":");
//		strBuf.append(System.currentTimeMillis()); // 6
//		strBuf.append(":");
//		strBuf.append(":");
//		// 引擎转速（发动机转速） // 8
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 瞬时油耗 // 9
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 机油压力 // 10
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 机油温度（随位置汇报上传） 11
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传） 12
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 累计油耗 13
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）14
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 进气温度（随位置汇报上传）15
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 大气压力（随位置汇报上传）16
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 脉冲车速（随位置汇报上传）17
//		strBuf.append("0");
//		strBuf.append(":");
//		// 终端内置电池电压（随位置汇报上传）18
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 冷却液温度（随位置汇报上传）19
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 车辆蓄电池电压（随位置汇报上传）20
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 发动机扭矩（随位置汇报上传）21
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 累计里程 22
//		strBuf.append("-1");
//		strBuf.append(":");
//		//基本状态位 23
//		strBuf.append("0");
//		strBuf.append(":");
//		//扩展状态位 24
//		strBuf.append("0");
//		strBuf.append(":");
//		// 25车速来源
//		strBuf.append("1"); 
//		strBuf.append(":");
//		//计量仪油耗 // 26
//		strBuf.append("-1");
//		strBuf.append(":");
//		// 海拔 27
//		strBuf.append("0"); 
//		strBuf.append(":");
//		// 油箱存油量(升) 28
//		strBuf.append("0"); 
//		strBuf.append(":");
//		 // vid 29
//		strBuf.append(vid);
//		strBuf.append(":");
//		 // 车牌颜色 30
//		strBuf.append(plateColorId);
//		strBuf.append(":");
//		 // 车牌号 31
//		strBuf.append(vehicleNo);
//		strBuf.append(":");
//		// 手机号 32
//		strBuf.append(simNum); 
//		strBuf.append(":");
//		 // 终端ID 33
//		strBuf.append(tId);
//		strBuf.append(":");
//		 // 终端型号 34
//		strBuf.append(tMac);
//		strBuf.append(":");
//		// 驾驶员姓名 35
//		if(null != employeeName){
//			strBuf.append(employeeName); 
//		}
//		strBuf.append(":");
//		//所属组织 36
//		strBuf.append(entName);  
//		strBuf.append(":");
//		// 车队id 37
//		strBuf.append(teamId); 
//		strBuf.append(":");
//		// 企业id 38
//		strBuf.append(entId); 
//		strBuf.append(":");
//		// OEMCODE 39
//		strBuf.append(oemCode); 
//		strBuf.append(":");
//		// 系统时间40
//		strBuf.append(System.currentTimeMillis()); 
//		strBuf.append(":");
//	//  是否在线41
//		strBuf.append("0"); 
//		strBuf.append(":");
//	//  是否有效 42
//		strBuf.append("0"); 
//		strBuf.append(":");
//	//  MSGID 43
//		strBuf.append("0"); 
//		strBuf.append(":");
//		strBuf.append("0"); 
//		strBuf.append(":");//  TEAM_NAME 车队名称  44
//		return strBuf.toString();
//	}
	/*****************************************
	 * <li>描        述：拼接空字符串 		</li><br>
	 * <li>时        间：2013-11-1  上午9:10:05	</li><br>
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
//	private static String getNullTrack(){
//		StringBuffer sb = new StringBuffer();
//		for(int i = 0; i < 45; i++){
//			sb.append("0").append(":");
//		}
//		return sb.toString();
//	}
//	public static boolean isStatus() {
//		return status;
//	}
//	public static void setStatus(boolean status) {
//		VehicleServiceTask.status = status;
//	}
	
}
