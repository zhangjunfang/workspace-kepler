package com.ctfo.trackservice.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.FutureTask;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.trackservice.common.ConfigLoader;
import com.ctfo.trackservice.common.StationCache;
import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.model.EquipmentStatus;
import com.ctfo.trackservice.model.FutureMapResult;
import com.ctfo.trackservice.model.ServiceUnit;
import com.ctfo.trackservice.model.Station;
import com.ctfo.trackservice.model.StationLine;
import com.ctfo.trackservice.model.StationNo;
import com.ctfo.trackservice.model.StatusBean;
import com.ctfo.trackservice.model.StatusCode;
import com.ctfo.trackservice.task.AlarmSettingsTask;
import com.ctfo.trackservice.task.InitCacheTask;
import com.ctfo.trackservice.task.VehicleCacheTask;
import com.ctfo.trackservice.util.Cache;
import com.ctfo.trackservice.util.Constant;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.Tools;


public class OracleService {
	private static final Logger logger = LoggerFactory.getLogger(OracleService.class);
	
	public OracleService(){
	}
	
	//---------------------------数据库连接------------------------------------
	/** 更新车辆总线状态连接 */
	private OracleConnection updateStatusConn;
	/** 数据库连接对象 */
	private OracleConnection trackConnection;
	/** 存储车辆上线连接 */
	private OracleConnection saveOnlineConn;
	/** 存储车辆下线连接 */
	private OracleConnection saveOfflineConn;
//	/** 更新最后位置表车辆上下线状态连接 */
//	 private OracleConnection updateOnOfflineStatusConn;
	//---------------------------预处理声明-----------------------------------
	/** 更新车辆总线状态 */
	private OraclePreparedStatement stUpdateVehicleLineStatus;
	/** 更新轨迹最后位置 */
	private OraclePreparedStatement stUpdateLastTrack;
	/** 更新轨迹最后位置报警 */
	private OraclePreparedStatement stUpdateLastTrackA;
	/** 更新轨迹带总线数据最后位置 */
	private OraclePreparedStatement stUpdateLastTrackLine;
	/** 更新轨迹带总线数据最后位置报警 */
	private OraclePreparedStatement stUpdateLastTrackALine;
	/*** 更新车辆是否在线状态及数据是否有效值 ***/
	private OraclePreparedStatement stUpdateLastTrackISonLine;
	/** 更新最后位置表车辆上下线状态预处理声明 */
	private OraclePreparedStatement updateOnOfflineStatusPS;	
	/** 存储车辆上线预处理声明 */
	private OraclePreparedStatement saveOnlineOPS;
	/** 存储车辆下线预处理声明 */
	private OraclePreparedStatement saveOfflineOPS;
	//---------------------------SQL------------------------------------
	/** 更新轨迹在线状态 */
	private static String sql_cacheAllVehicleStatus;
	/** 全量车辆信息查询SQL */
	private static String sql_allVehicleInfo;
	/** 增量车辆信息查询SQL */
	private static String sql_incrementalVehicleInfo;
	/** 全量车辆3G信息查询SQL */
	private static String sql_allVehicle3GInfo;
	/** 增量车辆3G信息查询SQL */
	private static String sql_incrementalVehicle3GInfo;
	/** 查询车辆对应企业SQL */
	private static String sql_queryVehicleOrgMap;
	/** 查询企业对应报警设置SQL */
	private static String sql_queryOrgAlarmCodeMap;
	/** 同步所有上级企业编号SQL */
	private static String sql_orgParentSync;
	/** 根据车辆ID查询车辆状态编码SQL */
	private static String sql_queryStatusCode;
	/** 轨迹包更新最后位置到数据库 */
	private static String sql_updateLastTrack;
	/** 轨迹包更新最后位置到数据库 */
	private static String sql_updateLastTrackA;
	/** 轨迹包带总线数据更新最后位置到数据库 */
	private static String sql_updateLastTrackLine;
	/** 轨迹包带总线数据更新最后位置到数据库 */
	private static String sql_updateLastTrackALine;
	/** 更新车辆总线状态信息 */
	private static String sql_updateVehicleLineStatus;
	/** 更新轨迹在线状态 */
	private static String sql_UpdateLastTrackISonLine;
	/** 更新最后位置表车辆上下线状态SQL */
	private static String sql_updateOnOfflineStatus;
	/** 存储上线SQL */
	private static String sql_saveOnline;
	/** 存储下线SQL */
	private static String sql_saveOffline;
	/** 查询线路站点绑定列表SQL */
	private static String sql_queryLineStationBind;
	//---------------------------批量提交数------------------------------------
	/** 轨迹批量提交数 */
	private static int trackSubmit;
	/** 非法轨迹提批量交数 */
	private static int trackValidSubmit;
	/** 设备状态更新批量提交数 */
	private static int equipmentSubmit;
	/** 上线提交数 */
	private static int onlineSubmit;
	/** 下线提交数 */
	private static int offlineSubmit;
	/** 更新最后位置表车辆上下线状态提交数 */
	private static int updateOfflineStatusSubmit;

	/**
	 * 业务信息初始化
	 * 
	 * TODO
	 */
	public static void init() {
//		初始化SQL
		sql_allVehicleInfo = ConfigLoader.config.get("sql_allVehicleInfo");
		sql_incrementalVehicleInfo = ConfigLoader.config.get("sql_incrementalVehicleInfo");
		sql_cacheAllVehicleStatus = ConfigLoader.config.get("sql_cacheAllVehicleStatus");
		sql_allVehicle3GInfo = ConfigLoader.config.get("sql_allVehicle3GInfo");
		sql_incrementalVehicle3GInfo = ConfigLoader.config.get("sql_incrementalVehicle3GInfo");
		sql_queryVehicleOrgMap = ConfigLoader.config.get("sql_queryVehicleOrgMap");
		sql_queryOrgAlarmCodeMap = ConfigLoader.config.get("sql_queryOrgAlarmCodeMap");
		sql_orgParentSync = ConfigLoader.config.get("sql_orgParentSync");
		sql_queryLineStationBind = ConfigLoader.config.get("sql_queryLineStationBind"); 
		
		/** 任务列表	  */
		List<FutureTask<Integer>> futureList = new ArrayList<FutureTask<Integer>>();
		int threadSize = 5;
		/** 线程池	  */
		ExecutorService exec =  Executors.newFixedThreadPool(threadSize);;
		// 将文件列表加入对应统计线程
		for (int i = 0; i < threadSize; i++) {
			// 创建对象
			FutureTask<Integer> task = new FutureTask<Integer>(new InitCacheTask(i));
			// 添加到list,方便后面取得结果
			futureList.add(task);
			// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
			exec.submit(task);
		}
		Set<Integer> set = new HashSet<Integer>();
		boolean THREAD_STATE = true;
		while (THREAD_STATE) {
			try {
				for (FutureTask<Integer> result : futureList) {
					if (result.isDone()) {
						Integer id = result.get();
						if (!set.contains(id)) {
							logger.debug("业务线程[{}]执行完成!", id);
							set.add(id);
						}
					}
					if (set.size() == threadSize) {
						THREAD_STATE = false;
						break;
					}
				}
				Thread.sleep(3);
			} catch (Exception e) {
				logger.error("报警统计-查询状态异常::" + e.getMessage(), e);
			}
		}
		exec.shutdown(); 
	}

	
	/*****************************************
	 * <li>描 述：缓存所有车辆状态</li><br>
	 * <li>时 间：2013-9-18 下午2:47:36</li><br>
	 * <li>参数：</li><br>
	 * TODO
	 *****************************************/
	public static void cacheAllVehicleStatus() {
		int vehicleIndex = 0;
		long start = System.currentTimeMillis();
		StatusBean statusBean = null;
		StatusCode statusCode = null;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_cacheAllVehicleStatus);
			rs = ps.executeQuery();
			while (rs.next()) {
				statusCode = new StatusCode();
				statusBean = new StatusBean();// 终端状态
				statusBean.setType(rs.getInt("TERMINAL_STATUS_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_TERMINAL_STATUS"));
				statusBean.setMax(rs.getDouble("MAX_TERMINAL_STATUS"));
				statusCode.setTerminalStatus(statusBean);
				statusBean = new StatusBean(); // GPS状态
				statusBean.setType(rs.getInt("GPS_STATUS_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_GPS_STATUS"));
				statusBean.setMax(rs.getDouble("MAX_GPS_STATUS"));
				statusCode.setGpsStatus(statusBean);
				statusBean = new StatusBean(); // 冷却液温度
				statusBean.setType(rs.getInt("E_WATER_TEMP_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_E_WATER_TEMP"));
				statusBean.setMax(rs.getDouble("MAX_E_WATER_TEMP"));
				statusCode.seteWater(statusBean);
				statusBean = new StatusBean(); // 蓄电池电压
				statusBean.setType(rs.getInt("EXT_VOLTAGE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EXT_VOLTAGE"));
				statusBean.setMax(rs.getDouble("MAX_EXT_VOLTAGE"));
				statusCode.setExtVoltage(statusBean);
				statusBean = new StatusBean(); // 油压状态
				statusBean.setType(rs.getInt("OIL_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_OIL_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_OIL_PRESSURE"));
				statusCode.setOilPressure(statusBean);
				statusBean = new StatusBean(); // 制动气压值
				statusBean.setType(rs.getInt("BRAKE_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_BRAKE_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_BRAKE_PRESSURE"));
				statusCode.setBrakePressure(statusBean);
				statusBean = new StatusBean(); // 制动蹄片磨损
				statusBean.setType(rs.getInt("BRAKEPAD_FRAY_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_BRAKEPAD_FRAY"));
				statusBean.setMax(rs.getDouble("MAX_BRAKEPAD_FRAY"));
				statusCode.setBrakepadFray(statusBean);
				statusBean = new StatusBean(); // 燃油告警
				statusBean.setType(rs.getInt("OIL_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_OIL_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_OIL_ALARM"));
				statusCode.setOilAram(statusBean);
				statusBean = new StatusBean(); // ABS故障状态
				statusBean.setType(rs.getInt("ABS_BUG_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_ABS_BUG"));
				statusBean.setMax(rs.getDouble("MAX_ABS_BUG"));
				statusCode.setAbsBug(statusBean);
				statusBean = new StatusBean(); // 水位低状态
				statusBean.setType(rs.getInt("COOLANT_LEVEL_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_COOLANT_LEVEL"));
				statusBean.setMax(rs.getDouble("MAX_COOLANT_LEVEL"));
				statusCode.setCoolantLevel(statusBean);
				statusBean = new StatusBean(); // 空滤堵塞
				statusBean.setType(rs.getInt("AIR_FILTER_CLOG_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_AIR_FILTER_CLOG"));
				statusBean.setMax(rs.getDouble("MAX_AIR_FILTER_CLOG"));
				statusCode.setAirFilter(statusBean);
				statusBean = new StatusBean(); // 机虑堵塞
				statusBean.setType(rs.getInt("MWERE_BLOCKING_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_MWERE_BLOCKING"));
				statusBean.setMax(rs.getDouble("MAX_MWERE_BLOCKING"));
				statusCode.setMwereBlocking(statusBean);
				statusBean = new StatusBean(); // 燃油堵塞
				statusBean.setType(rs.getInt("FUEL_BLOCKING_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_FUEL_BLOCKING"));
				statusBean.setMax(rs.getDouble("MAX_FUEL_BLOCKING"));
				statusCode.setFuelBlocking(statusBean);
				statusBean = new StatusBean(); // 机油温度
				statusBean.setType(rs.getInt("EOIL_TEMPERATURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EOIL_TEMPERATURE_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_EOIL_TEMPERATURE_ALARM"));
				statusCode.setEoilTemperature(statusBean);
				statusBean = new StatusBean(); // 缓速器高温
				statusBean.setType(rs.getInt("RETARDER_HT_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_RETARDER_HT_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_RETARDER_HT_ALARM"));
				statusCode.setRetarerHt(statusBean);
				statusBean = new StatusBean(); // 仓温高状态
				statusBean.setType(rs.getInt("EHOUSING_HT_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EHOUSING_HT_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_EHOUSING_HT_ALARM"));
				statusCode.setEhousing(statusBean);
				statusBean = new StatusBean(); // 大气压力
				statusBean.setType(rs.getInt("AIR_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_AIR_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_AIR_PRESSURE"));
				statusCode.setAirPressure(statusBean);
				Cache.setStatusMapValue(rs.getString("VID"), statusCode);
				vehicleIndex++;
			}
		} catch (Exception e) {
			logger.error("缓存所有车辆状态异常:" + e.getMessage(), e);
		} finally {
			close(rs, ps, conn);
		}
		long end = System.currentTimeMillis();
		logger.info("S[1/1]缓存所有车辆状态结束,更新数量:[" + vehicleIndex + "]条,耗时[" + (end - start)+"]ms");
	}
	/**
	 * 车辆信息更新
	 */
	@SuppressWarnings("unchecked")
	public static void vehicleInfoUpdate(long lastTime, boolean allFlag){
		try{
			long start = System.currentTimeMillis();
			/** 任务列表	  */
			List<FutureTask<FutureMapResult>> futureList = new ArrayList<FutureTask<FutureMapResult>>();
			int threadSize = 2;
			/** 线程池	  */
			ExecutorService exec =  Executors.newFixedThreadPool(threadSize);;
			// 将文件列表加入对应统计线程
			for (int i = 0; i < threadSize; i++) {
				// 创建对象
				FutureTask<FutureMapResult> task = new FutureTask<FutureMapResult>(new VehicleCacheTask(i, lastTime, allFlag));
				// 添加到list,方便后面取得结果
				futureList.add(task);
				// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
				exec.submit(task);
			}
			Set<String> set = new HashSet<String>();
			boolean THREAD_STATE = true;
			Map<String, ServiceUnit> map = new ConcurrentHashMap<String, ServiceUnit>();
			while (THREAD_STATE) {
				try {
					for (FutureTask<FutureMapResult> result : futureList) {
						if (result.isDone()) {
							FutureMapResult fmr = result.get();
							String taskName = fmr.getName();
							if (!set.contains(taskName)) {
								Map<String, ServiceUnit> m = (Map<String, ServiceUnit>) fmr.getValue();
								if(m != null && m.size() > 0){
									map.putAll(m); 
								}
								logger.info("业务线程[{}]执行完成!", taskName);
								set.add(taskName);
							}
						}
						if (set.size() == threadSize) {
							THREAD_STATE = false;
							break;
						}
					}
					Thread.sleep(500);
				} catch (Exception e) {
					logger.error("车辆信息更新-查询状态异常::" + e.getMessage(), e);
				}
			}
			int mapSize = map.size();
			if(mapSize > 0){
				if(allFlag){
//					1. 获取历史缓存所有KEYS，存储到SET集合中
					Set<String> oldKeys = Cache.getVehicleInfoKey();
					if(oldKeys != null && oldKeys.size() > 0){
						int oldSize = oldKeys.size();
//						2. 获取当前缓存所有KEYS，对比出差集
						Set<String> newKeys = map.keySet();
						if(newKeys != null && newKeys.size() > 0){
							oldKeys.removeAll(newKeys);
							logger.debug("缓存中当前数量:[{}], 当前查询到的数量:[{}], 要删除的车辆数量:[{}]", oldSize, newKeys.size(), oldKeys.size());
							if(oldKeys != null && oldKeys.size() > 0){
								Cache.removeVehicleInfoKeys(oldKeys);// 清除过期车辆缓存  
							}
						}
					}
				} 
				Cache.putAllVehicleInfo(map);
			}
			
			exec.shutdown(); 
			futureList.clear();
			long end = System.currentTimeMillis();
			logger.info("V[3/3]{}更新所有车辆信息[{}]条, 耗时[{}]ms", allFlag ? "全量" : "增量", mapSize, end - start);
		} catch (Exception e) {
			logger.error("车辆信息更新异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 增量更新车辆信息
	 * @param lastTime 	最近更新时间
	 * @param allFlag 	全量更新标记
	 * TODO
	 */
	public static Map<String, ServiceUnit> updateVehicleInfo(long lastTime, boolean allFlag) {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			conn = OracleConnectionPool.getConnection();// 从连接池获得连接
			if(allFlag){ // 全量更新
				ps = conn.prepareStatement(sql_allVehicleInfo);
			} else {	 // 增量更新
				ps = conn.prepareStatement(sql_incrementalVehicleInfo);
				for(int i = 1 ; i <= 8; i++){
					ps.setLong(i, lastTime);
				}
			}
			rs = ps.executeQuery();
			Map<String, ServiceUnit> map = new ConcurrentHashMap<String, ServiceUnit>();
			while (rs.next()) {
				ServiceUnit su = getServiceUnit(rs);
				if(su != null){
					map.put(su.getMacid(), su);
				}
			}
			int mapSize = map.size();
			long end = System.currentTimeMillis();
			logger.info("V[2/3]{}更新车辆信息[{}]条, 耗时[{}]ms", allFlag ? "全量" : "增量", mapSize, end - start);
			return map;
		} catch (SQLException e) {
			logger.error("更新车辆信息异常:" + e.getMessage(), e);
			return null;
		} finally {
			close(rs, ps, conn);
		}
	}
	/*****************************************
	 * <li>描 述：更新3g手机号对应的车辆缓存信息</li><br>
	 * <li>时 间：2013-7-24 上午9:57:13</li><br>
	 * <li>参数： @param sql <li>参数： @throws SQLException</li><br>
	 * TODO
	 *****************************************/
	public static Map<String, ServiceUnit> vehicle3GInfoUpdate(long lastTime, boolean allFlag) {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			conn = OracleConnectionPool.getConnection();// 从连接池获得连接
			if(allFlag){ // 全量更新
				ps = conn.prepareStatement(sql_allVehicle3GInfo);
			} else {	 // 增量更新
				ps = conn.prepareStatement(sql_incrementalVehicle3GInfo);
				for(int i = 1 ; i <= 10; i++){
					ps.setLong(i, lastTime);
				}
			}
			rs = ps.executeQuery();
			Map<String, ServiceUnit> map = new ConcurrentHashMap<String, ServiceUnit>();
			while (rs.next()) {
				ServiceUnit su = get3GServiceUnit(rs);
				if(su != null){
					map.put(su.getMacid(), su);
				}
			}
			int mapSize = map.size();
			long end = System.currentTimeMillis();
			logger.info("V[1/3]{}更新3G车辆信息[{}]条, 耗时[{}]ms", allFlag ? "全量" : "增量", mapSize, end - start);
			return map;
		} catch (SQLException e) {
			logger.error("更新3G车辆信息异常:" + e.getMessage(), e);
			return null;
		} finally {
			close(rs, ps, conn);
		}
	}



	
	
	/*****************************************
	 * <li>描 述：告警设置企业查询</li><br>
	 * <li>时 间：2013-7-22 上午9:37:19</li><br>
	 * <li>参数：</li><br>
	 * TODO
	 *****************************************/
	public static Map<String, String> alarmSettingEntQuery() {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int index = 0;
		try {
			// 查询车辆对应企业列表
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryOrgAlarmCodeMap);
			rs = ps.executeQuery();
			Map<String, String> map = new ConcurrentHashMap<String, String>();
			while (rs.next()) {
				map.put(rs.getString("ENT_ID"), rs.getString("ALARM_CODE"));
				index++;
			}
			long end = System.currentTimeMillis();
			logger.info("G[2/3]告警设置企业查询 , 结果[{}]条, 实际[{}]条, 耗时[{}]ms", map.size(), index, end-start);
			return map;
		} catch (SQLException e) {
			logger.error("告警设置企业查询异常:" + e.getMessage(), e);
			return null;
		} finally {
			close(rs, ps, conn);
		}
	}
	
	/*****************************************
	 * <li>描 述：告警设置车辆查询</li><br>
	 * <li>时 间：2013-7-22 上午9:37:19</li><br>
	 * <li>参数：</li><br>
	 * TODO
	 *****************************************/
	public static Map<String, String> alarmSettingVehicleQuery() {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int index = 0;
		try {
			// 查询车辆对应企业列表
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryVehicleOrgMap);
			rs = ps.executeQuery();
			Map<String, String> map = new ConcurrentHashMap<String, String>();
			while (rs.next()) {
				map.put(rs.getString("VID"), rs.getString("ENT_ID"));
				index++;
			}
			long end = System.currentTimeMillis();
			logger.info("G[1/3]告警设置车辆查询 , 结果[{}]条, 实际[{}]条, 耗时[{}]ms", map.size(), index, end-start);
			return map;
		} catch (SQLException e) {
			logger.error("告警设置车辆查询异常:" + e.getMessage(), e);
			return null;
		} finally {
			close(rs, ps, conn);
		}
	}
	/**
	 * 车辆告警设置信息更新
	 * TODO
	 */
	@SuppressWarnings("unchecked")
	public static void updateVehicleAlarmSetting(){
		try{
			long start = System.currentTimeMillis();
			/** 任务列表	  */
			List<FutureTask<FutureMapResult>> futureList = new ArrayList<FutureTask<FutureMapResult>>();
			int threadSize = 2;
			/** 线程池	  */
			ExecutorService exec =  Executors.newFixedThreadPool(threadSize);;
			// 将文件列表加入对应统计线程
			for (int i = 0; i < threadSize; i++) {
				// 创建对象
				FutureTask<FutureMapResult> task = new FutureTask<FutureMapResult>(new AlarmSettingsTask(i));
				// 添加到list,方便后面取得结果
				futureList.add(task);
				// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
				exec.submit(task);
			}
			Set<String> set = new HashSet<String>();
			boolean THREAD_STATE = true;
			Map<String, String> vehicheMap = new ConcurrentHashMap<String, String>();
			Map<String, String> alarmMap = new ConcurrentHashMap<String, String>();
			while (THREAD_STATE) {
				try {
					for (FutureTask<FutureMapResult> result : futureList) {
						if (result.isDone()) {
							FutureMapResult asr = result.get();
							String name = asr.getName();
							if (!set.contains(name)) {
								if(name.equals("AlarmSettingVehicle")){
									Map<String, String> m = (Map<String, String>) asr.getValue();
									if(m != null && m.size() > 0){
										vehicheMap.putAll(m); 
									}
								} else if(name.equals("AlarmSettingEnt")){
									Map<String, String> m = (Map<String, String>) asr.getValue();
									if(m != null && m.size() > 0){
										alarmMap.putAll(m); 
									}
								}
								logger.info("业务线程[{}]执行完成!", name);
								set.add(name);
							}
						}
						if (set.size() == threadSize) {
							THREAD_STATE = false;
							break;
						}
					}
					Thread.sleep(500);
				} catch (Exception e) {
					logger.error("车辆信息更新-查询状态异常::" + e.getMessage(), e);
				}
			}
			int vehicleSize = vehicheMap.size();
			int alarmSize = alarmMap.size();
			if(vehicleSize > 0 && alarmSize > 0){
				boolean N1Exist = alarmMap.containsKey(Constant.N1);
				for (Map.Entry<String, String> map : vehicheMap.entrySet()) {
					// 将车辆企业对应表中的企业编号进行替换
					if (alarmMap.containsKey(map.getValue())) {
						vehicheMap.put(map.getKey(), alarmMap.get(map.getValue()));
					} else {
						if (N1Exist) {
							vehicheMap.put(map.getKey(), alarmMap.get(Constant.N1));
						}
					}
				}
				Cache.vidEntMap.putAll(vehicheMap);
				Cache.entAlarmMap.putAll(alarmMap); 
			}
			
			exec.shutdown(); 
			futureList.clear();
			long end = System.currentTimeMillis();
			logger.info("G[3/3]报警设置企业[{}]个, 车辆[{}]条, 总 耗时[{}]ms", alarmSize, vehicleSize, end - start);
		} catch (Exception e) {
			logger.error("车辆信息更新异常:" + e.getMessage(), e);
		}
	}
	/*****************************************
	 * <li>描 述：组织父级同步任务</li><br>
	 * <li>时 间：2013-10-28 下午5:51:37</li><br>
	 * <li>参数： 组织父级表 key:车队编号 ,value:父级组织编号 以‘,’逗号隔开</li><br>
	 * TODO
	 *****************************************/
	public static void orgParentSync() {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int index = 0;
		int error = 0;
		Map<String, String> orgParent = new ConcurrentHashMap<String, String>();
		// 从连接池获得连接
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_orgParentSync);
			rs = ps.executeQuery();
			while (rs.next()) {
				try {
					// 组织父级表 key:车队编号 ,value:父级组织编号 以‘,’逗号隔开
					orgParent.put(rs.getString("MOTORCADE"), "," + rs.getString("PARENT_ID") + ",");
					index++;
				} catch (Exception e) {
					error++;
					continue;
				}
			}
			if (orgParent.size() > 0) {
				Cache.putAllOrgParent(orgParent);
			}
		} catch (SQLException e) {
			logger.error("组织父级同步任务异常:" + e.getMessage(), e);
		} finally {
			close(rs, ps, conn);
		}
		long end = System.currentTimeMillis();
		logger.info("E[1/1]组织父级同步任务结束,有效数据[{}]条, 异常数据[{}]条, 总数据:[{}]条, 耗时:[{}]ms", index, error, (index + error), (end - start));
	}
	

	
	/*****************************************
	 * <li>描 述：更新最后位置在线信息</li><br>
	 * <li>时 间：2013-9-16 下午4:53:29</li><br>
	 * <li>参数： @param eqStatus</li><br>
	 * TODO
	 *****************************************/
	public void updateLastTrackLine(Map<String, String> eqStatus) {
		int gpsSpeed = Integer.parseInt(eqStatus.get("3"));
		long lon = Long.parseLong(eqStatus.get("1"));
		long lat = Long.parseLong(eqStatus.get("2"));
		String vid = eqStatus.get(Constant.VID);
		int head = Integer.parseInt(eqStatus.get("5"));

		// String gpsTime = eqStatus.get("4");
		long utc = Long.parseLong(eqStatus.get(Constant.UTC));
		long mapLon = Long.parseLong(eqStatus.get(Constant.MAPLON));
		long mapLat = Long.parseLong(eqStatus.get(Constant.MAPLAT));
		String alarmCode = eqStatus.get("20");
		if (eqStatus.containsKey("21")) {
			alarmCode = alarmCode.substring(0, alarmCode.length() - 1) + eqStatus.get("21");
		}
		alarmCode = alarmCode.replaceAll("\\,\\,", ",");
		String basestatus = eqStatus.get("8");// 基本状态
		String extendstatus = eqStatus.get("500");// 扩展状态

		String msgid = eqStatus.get(Constant.MSGID);

		if (alarmCode != null && alarmCode.length() > 1) {
			try {
				stUpdateLastTrackALine.setLong(1, lon); // LON
				stUpdateLastTrackALine.setLong(2, lat); // LAT
				stUpdateLastTrackALine.setInt(3, gpsSpeed); // GPS_SPEED
				String mileage = eqStatus.get("9");
				if (mileage != null) {
					stUpdateLastTrackALine.setLong(4, Long.parseLong(mileage)); // MILEAGE
					stUpdateLastTrackALine.setLong(5, Long.parseLong(mileage)); // MILEAGE
				} else {
					stUpdateLastTrackALine.setLong(4, -1); // MILEAGE
					stUpdateLastTrackALine.setNull(5, Types.INTEGER); // MILEAGE
				}
				stUpdateLastTrackALine.setInt(6, head); // DIRECTION
				stUpdateLastTrackALine.setLong(7, utc); // UTC
				stUpdateLastTrackALine.setLong(8, DateTools.getCurrentUtcMsDate()); // SYSUTC
				stUpdateLastTrackALine.setLong(9, mapLon); // MAPLON
				stUpdateLastTrackALine.setLong(10, mapLat); // MAPLAT
				stUpdateLastTrackALine.setLong(11, utc); // ALARM_UTC
				if (eqStatus.get("6") != null) { //
					stUpdateLastTrackALine.setLong(12, Long.parseLong(eqStatus.get("6")));
				} else {
					stUpdateLastTrackALine.setNull(12, Types.INTEGER); //
				}

				if (eqStatus.get("24") != null) { // OIL_MEASURE 判断数据
					stUpdateLastTrackALine.setLong(13, Long.parseLong(eqStatus.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrackALine.setLong(13, -1); // 油量（单位：L）
				}
				if (eqStatus.get("24") != null) { // OIL_MEASURE
					stUpdateLastTrackALine.setLong(14, Long.parseLong(eqStatus.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrackALine.setNull(14, Types.INTEGER); // 油量（单位：L）
				}

				if (eqStatus.get("210") != null) {// ENGINE_ROTATE_SPEED
					stUpdateLastTrackALine.setLong(15, Long.parseLong(eqStatus.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrackALine.setNull(15, Types.INTEGER);
				}

				if (eqStatus.get("503") != null && !eqStatus.get("503").equals("")) { // E_TORQUE
					stUpdateLastTrackALine.setLong(16, Long.parseLong(eqStatus.get("503"))); // 发动机扭矩
				} else {
					stUpdateLastTrackALine.setNull(16, Types.INTEGER);
				}

				if (eqStatus.get("216") != null) { // OIL_INSTANT
					stUpdateLastTrackALine.setLong(17, Long.parseLong(eqStatus.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrackALine.setNull(17, Types.INTEGER);
				}

				if (eqStatus.get("507") != null) { // BATTERY_VOLTAGE
					stUpdateLastTrackALine.setLong(18, Long.parseLong(eqStatus.get("507"))); // 电池电压
				} else {
					stUpdateLastTrackALine.setNull(18, Types.INTEGER);
				}

				if (eqStatus.get("506") != null) { // EXT_VOLTAGE
					stUpdateLastTrackALine.setLong(19, Long.parseLong(eqStatus.get("506"))); // 外部电压
				} else {
					stUpdateLastTrackALine.setNull(19, Types.INTEGER);
				}

				if (eqStatus.get("509") != null && !eqStatus.get("509").equals("")) { // E_WATER_TEMP
					stUpdateLastTrackALine.setLong(20, Long.parseLong(eqStatus.get("509"))); // 冷却液温度
				} else {
					stUpdateLastTrackALine.setNull(20, Types.INTEGER);
				}
				String ratio = eqStatus.get(Constant.RATIO);
				if (!StringUtils.equals(ratio, "-100")) {// 速比
					stUpdateLastTrackALine.setDouble(21, Double.parseDouble(ratio));
				} else {
					stUpdateLastTrackALine.setNull(21, Types.DOUBLE);
				}
				String gears = eqStatus.get(Constant.GEARS);
				if (!StringUtils.equals(gears, "-100")) {// 档位rob
					stUpdateLastTrackALine.setInt(22, Integer.parseInt(gears));
				} else {
					stUpdateLastTrackALine.setNull(22, Types.INTEGER);
				}

				if (eqStatus.get("7") != null) {// VEHICLE_SPEED
					stUpdateLastTrackALine.setInt(23, Integer.parseInt(eqStatus.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrackALine.setNull(23, Types.INTEGER); // 脉冲车速
				}

				// 大气压力
				if (eqStatus.get("511") != null && !eqStatus.get("511").equals("")) {
					stUpdateLastTrackALine.setLong(24, Long.parseLong(eqStatus.get("511")));
				} else {
					stUpdateLastTrackALine.setNull(24, Types.INTEGER);
				}

				// 进气温度
				if (eqStatus.get("510") != null) {
					stUpdateLastTrackALine.setLong(25, Long.parseLong(eqStatus.get("510")));
				} else {
					stUpdateLastTrackALine.setNull(25, Types.INTEGER);
				}

				if (eqStatus.get("505") != null) {
					stUpdateLastTrackALine.setLong(26, Long.parseLong(eqStatus.get("505")));
				} else {
					stUpdateLastTrackALine.setLong(26, -1);
				}
				// 发动机运行总时长
				if (eqStatus.get("505") != null) {
					stUpdateLastTrackALine.setLong(27, Long.parseLong(eqStatus.get("505")));
				} else {
					stUpdateLastTrackALine.setNull(27, Types.INTEGER);
				}

				if (eqStatus.get("213") != null) {
					stUpdateLastTrackALine.setLong(28, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrackALine.setLong(28, -1);
				}

				// 累计油耗
				if (eqStatus.get("213") != null) {
					stUpdateLastTrackALine.setLong(29, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrackALine.setNull(29, Types.INTEGER);
				}

				// 油门踏板位置
				if (eqStatus.get("504") != null && !eqStatus.get("504").equals("")) {
					stUpdateLastTrackALine.setLong(30, Long.parseLong(eqStatus.get("504")));
				} else {
					stUpdateLastTrackALine.setNull(30, Types.INTEGER);
				}

				// 机油温度
				if (eqStatus.get("508") != null) {
					stUpdateLastTrackALine.setLong(31, Long.parseLong(eqStatus.get("508")));
				} else {
					stUpdateLastTrackALine.setNull(31, Types.INTEGER);
				}

				// 机油压力
				if (eqStatus.get("215") != null && !eqStatus.get("215").equals("")) {
					stUpdateLastTrackALine.setLong(32, Long.parseLong(eqStatus.get("215")));
				} else {
					stUpdateLastTrackALine.setNull(32, Types.INTEGER);
				}

				stUpdateLastTrackALine.setString(33, alarmCode);

				stUpdateLastTrackALine.setString(34, basestatus);
				stUpdateLastTrackALine.setString(35, extendstatus);
				stUpdateLastTrackALine.setString(36, msgid);
				stUpdateLastTrackALine.setString(37, eqStatus.get(Constant.SPEEDFROM)); // 车速来源

				// 精准油耗
				if (null != eqStatus.get("219")) {
					stUpdateLastTrackALine.setLong(38, Long.parseLong(eqStatus.get("219")));
				} else {
					stUpdateLastTrackALine.setNull(38, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrackALine.setInt(39, status);
				stUpdateLastTrackALine.setInt(40, acc);
				// 判断锁车状态
				String lockStatus = eqStatus.get("570");
				if (StringUtils.isNumeric(lockStatus)) {
					stUpdateLastTrackALine.setString(41, Tools.parseLockStatus(lockStatus));
				} else {
					stUpdateLastTrackALine.setNull(41, Types.NULL);
				}

				stUpdateLastTrackALine.setString(42, vid);// VID
				stUpdateLastTrackALine.executeUpdate();

			} catch (Exception e) {
				logger.error("更新轨迹包带总线数据并带有报警异常:" + e.getMessage(), e);
				try {
					trackConnection.getMetaData();
					if (stUpdateLastTrackALine == null) {
						stUpdateLastTrackALine = createStatement(trackConnection, trackSubmit, sql_updateLastTrack);
					}
				} catch (Exception ex) {
					logger.error("更新轨迹包带总线数据并带有报警重连异常:", ex);
					stUpdateLastTrackALine = recreateStatement(trackConnection, trackSubmit, sql_updateLastTrack);
				}
			}
		} else {
			try {
				stUpdateLastTrackLine.setLong(1, lon);
				stUpdateLastTrackLine.setLong(2, lat);
				stUpdateLastTrackLine.setInt(3, gpsSpeed);
				if (eqStatus.get("9") != null) {
					stUpdateLastTrackLine.setLong(4, Long.parseLong(eqStatus.get("9")));
				} else {
					stUpdateLastTrackLine.setLong(4, -1);
				}
				if (eqStatus.get("9") != null) {
					stUpdateLastTrackLine.setLong(5, Long.parseLong(eqStatus.get("9")));
				} else {
					stUpdateLastTrackLine.setNull(5, Types.INTEGER);
				}
				stUpdateLastTrackLine.setInt(6, head);
				stUpdateLastTrackLine.setLong(7, utc);
				stUpdateLastTrackLine.setLong(8, DateTools.getCurrentUtcMsDate());
				stUpdateLastTrackLine.setLong(9, mapLon);
				stUpdateLastTrackLine.setLong(10, mapLat);
				if (eqStatus.get("6") != null) {
					stUpdateLastTrackLine.setLong(11, Long.parseLong(eqStatus.get("6"))); //
				} else {
					stUpdateLastTrackLine.setNull(11, Types.INTEGER);
				}

				if (eqStatus.get("24") != null) { // 判断数据
					stUpdateLastTrackLine.setLong(12, Long.parseLong(eqStatus.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrackLine.setLong(12, -1);
				}
				if (eqStatus.get("24") != null) {
					stUpdateLastTrackLine.setLong(13, Long.parseLong(eqStatus.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrackLine.setNull(13, Types.INTEGER);
				}
				if (eqStatus.get("210") != null) {
					stUpdateLastTrackLine.setLong(14, Long.parseLong(eqStatus.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrackLine.setNull(14, Types.INTEGER);
				}
				if (eqStatus.get("503") != null && !eqStatus.get("503").equals("")) {
					stUpdateLastTrackLine.setLong(15, Long.parseLong(eqStatus.get("503"))); // 发动机扭矩
				} else {
					stUpdateLastTrackLine.setNull(15, Types.INTEGER);
				}
				if (eqStatus.get("216") != null) {
					stUpdateLastTrackLine.setLong(16, Long.parseLong(eqStatus.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrackLine.setNull(16, Types.INTEGER);
				}
				if (eqStatus.get("507") != null) {
					stUpdateLastTrackLine.setLong(17, Long.parseLong(eqStatus.get("507"))); // 电池电压
				} else {
					stUpdateLastTrackLine.setNull(17, Types.INTEGER);
				}
				if (eqStatus.get("506") != null) {
					stUpdateLastTrackLine.setLong(18, Long.parseLong(eqStatus.get("506"))); // 外部电压
				} else {
					stUpdateLastTrackLine.setNull(18, Types.INTEGER);
				}
				if (eqStatus.get("509") != null && !eqStatus.get("509").equals("")) {
					stUpdateLastTrackLine.setLong(19, Long.parseLong(eqStatus.get("509"))); // 冷却液温度
				} else {
					stUpdateLastTrackLine.setNull(19, Types.INTEGER);
				}
				if (!eqStatus.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrackLine.setDouble(20, Double.parseDouble(eqStatus.get(Constant.RATIO)));
				} else {
					stUpdateLastTrackLine.setNull(20, Types.DOUBLE);
				}

				if (!eqStatus.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrackLine.setInt(21, Integer.parseInt(eqStatus.get(Constant.GEARS)));
				} else {
					stUpdateLastTrackLine.setNull(21, Types.INTEGER);
				}

				if (eqStatus.get("7") != null) {
					stUpdateLastTrackLine.setInt(22, Integer.parseInt(eqStatus.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrackLine.setNull(22, Types.INTEGER);
				}

				// 大气压力
				if (eqStatus.get("511") != null && !eqStatus.get("511").equals("")) {
					stUpdateLastTrackLine.setLong(23, Long.parseLong(eqStatus.get("511")));
				} else {
					stUpdateLastTrackLine.setNull(23, Types.INTEGER);
				}

				// 进气温度
				if (eqStatus.get("510") != null) {
					stUpdateLastTrackLine.setLong(24, Long.parseLong(eqStatus.get("510")));
				} else {
					stUpdateLastTrackLine.setNull(24, Types.INTEGER);
				}

				if (eqStatus.get("505") != null) {
					stUpdateLastTrackLine.setLong(25, Long.parseLong(eqStatus.get("505")));
				} else {
					stUpdateLastTrackLine.setLong(25, -1);
				}

				// 发动机运行总时长
				if (eqStatus.get("505") != null) {
					stUpdateLastTrackLine.setLong(26, Long.parseLong(eqStatus.get("505")));
				} else {
					stUpdateLastTrackLine.setNull(26, Types.INTEGER);
				}

				if (eqStatus.get("213") != null) {
					stUpdateLastTrackLine.setLong(27, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrackLine.setLong(27, -1);
				}

				// 累计油耗
				if (eqStatus.get("213") != null) {
					stUpdateLastTrackLine.setLong(28, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrackLine.setNull(28, Types.INTEGER);
				}

				// 油门踏板位置
				if (eqStatus.get("504") != null && !eqStatus.get("504").equals("")) {
					stUpdateLastTrackLine.setLong(29, Long.parseLong(eqStatus.get("504")));
				} else {
					stUpdateLastTrackLine.setNull(29, Types.INTEGER);
				}

				// 机油温度
				if (eqStatus.get("508") != null) {
					stUpdateLastTrackLine.setLong(30, Long.parseLong(eqStatus.get("508")));
				} else {
					stUpdateLastTrackLine.setNull(30, Types.INTEGER);
				}

				// 机油压力
				if (eqStatus.get("215") != null && !eqStatus.get("215").equals("")) {
					stUpdateLastTrackLine.setLong(31, Long.parseLong(eqStatus.get("215")));
				} else {
					stUpdateLastTrackLine.setNull(31, Types.INTEGER);
				}
				stUpdateLastTrackLine.setString(32, basestatus);
				stUpdateLastTrackLine.setString(33, extendstatus);
				stUpdateLastTrackLine.setString(34, msgid);
				stUpdateLastTrackLine.setString(35, eqStatus.get(Constant.SPEEDFROM)); // 车速来源
				// 精准油耗
				if (null != eqStatus.get("219")) {
					stUpdateLastTrackLine.setLong(36, Long.parseLong(eqStatus.get("219")));
				} else {
					stUpdateLastTrackLine.setNull(36, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrackLine.setInt(37, status);
				stUpdateLastTrackLine.setInt(38, acc);
				// 判断锁车状态 
				if (StringUtils.isNotBlank(eqStatus.get("570"))) {
					stUpdateLastTrackLine.setString(39, eqStatus.get("570"));
				} else {
					stUpdateLastTrackLine.setNull(39, Types.NULL);
				}
				stUpdateLastTrackLine.setString(40, vid);
				stUpdateLastTrackLine.executeUpdate();

			} catch (Exception e) {
				logger.error("更新轨迹包带总线数据异常:" + e.getMessage(), e);
				try {
					trackConnection.getMetaData();
					if (stUpdateLastTrackLine == null) {
						stUpdateLastTrackLine = createStatement(trackConnection, trackSubmit, sql_updateLastTrackLine);
					}
				} catch (Exception ex) {
					logger.error("更新轨迹包带总线数据重连异常:", ex);
					stUpdateLastTrackLine = recreateStatement(trackConnection, trackSubmit, sql_updateLastTrackLine);
				}
			}
		}
	}

	/*****************************************
	 * <li>描 述：更新最后位置信息</li><br>
	 * <li>时 间：2013-9-16 下午4:53:23</li><br>
	 * <li>参数： @param eqStatus</li><br>
	 * TODO
	 *****************************************/
	public void updateLastTrack(Map<String, String> eqStatus) {
		Integer gpsSpeed = 0;
		String speed = eqStatus.get("3");
		if (speed != null) {
			gpsSpeed = Integer.parseInt(speed);
		}
		long lon = 0l;
		long lat = 0;
		String lonStr = eqStatus.get("1");
		String latStr = eqStatus.get("2");
		if (lonStr != null) {
			lon = Long.parseLong(lonStr);
		}
		if (latStr != null) {
			lat = Long.parseLong(latStr);
		}
		String vid = eqStatus.get(Constant.VID);
		if (eqStatus.get("5") == null) {
			logger.error("updateLastTrack---eqStatus.get(5)==null:" + eqStatus.get(Constant.COMMAND));
			return;
		}
		String utcStr = eqStatus.get(Constant.UTC);
		if (utcStr == null) {
			logger.error("updateLastTrack---UTC==null:" + eqStatus.get(Constant.COMMAND));
			return;
		}
		int head = Integer.parseInt(eqStatus.get("5"));
		long utc = Long.parseLong(utcStr);
		long mapLon = Long.parseLong(eqStatus.get(Constant.MAPLON) == null ? "0" : eqStatus.get(Constant.MAPLON));
		long mapLat = Long.parseLong(eqStatus.get(Constant.MAPLAT) == null ? "0" : eqStatus.get(Constant.MAPLAT));
		String alarmCode = eqStatus.get("20");
		if (alarmCode != null) {
			if (eqStatus.get("21") != null) {
				alarmCode = alarmCode.substring(0, alarmCode.length() - 1) + eqStatus.get("21");
			} else {
				alarmCode = alarmCode.replaceAll("\\,\\,", ",");

			}
		}
		String basestatus = eqStatus.get("8");// 基本状态
		String extendstatus = eqStatus.get("500");// 扩展状态
		String msgid = eqStatus.get(Constant.MSGID);

		// int i = alarmCode.length();
		if (Tools.onAlarm(alarmCode)) {
			try {
				stUpdateLastTrackA.setLong(1, lon); // LON
				stUpdateLastTrackA.setLong(2, lat); // LAT
				stUpdateLastTrackA.setInt(3, gpsSpeed); // GPS_SPEED
				String mileage = eqStatus.get("9");
				if (StringUtils.isNumeric(mileage)) {
					stUpdateLastTrackA.setLong(4, Long.parseLong(eqStatus.get("9"))); // MILEAGE
					stUpdateLastTrackA.setLong(5, Long.parseLong(eqStatus.get("9"))); // MILEAGE
				} else {
					stUpdateLastTrackA.setLong(4, -1); // MILEAGE
					stUpdateLastTrackA.setNull(5, Types.INTEGER); // MILEAGE
				}
				stUpdateLastTrackA.setInt(6, head); // DIRECTION
				stUpdateLastTrackA.setLong(7, utc); // UTC
				stUpdateLastTrackA.setLong(8, DateTools.getCurrentUtcMsDate()); // SYSUTC
				stUpdateLastTrackA.setLong(9, mapLon); // MAPLON
				stUpdateLastTrackA.setLong(10, mapLat); // MAPLAT
				stUpdateLastTrackA.setLong(11, utc); // ALARM_UTC
				String elevation = eqStatus.get("6");
				if (StringUtils.isNumeric(elevation)) { // ELEVATION
					stUpdateLastTrackA.setLong(12, Long.parseLong(elevation)); //
				} else {
					stUpdateLastTrackA.setNull(12, Types.INTEGER); //
				}
				String oil_measure = eqStatus.get("24");
				if (StringUtils.isNumeric(oil_measure)) { // OIL_MEASURE 判断数据
					stUpdateLastTrackA.setLong(13, Long.parseLong(oil_measure)); // 油量（单位：L）
					stUpdateLastTrackA.setLong(14, Long.parseLong(oil_measure)); // 油量（单位：L）
				} else {
					stUpdateLastTrackA.setLong(13, -1); // 油量（单位：L）
					stUpdateLastTrackA.setNull(14, Types.INTEGER); // 油量（单位：L）
				}
				if (!eqStatus.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrackA.setDouble(15, Double.parseDouble(eqStatus.get(Constant.RATIO)));
				} else {
					stUpdateLastTrackA.setNull(15, Types.DOUBLE);
				}

				if (!eqStatus.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrackA.setInt(16, Integer.parseInt(eqStatus.get(Constant.GEARS)));
				} else {
					stUpdateLastTrackA.setNull(16, Types.INTEGER);
				}
				if (eqStatus.get("7") != null) {// VEHICLE_SPEED
					stUpdateLastTrackA.setInt(17, Integer.parseInt(eqStatus.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrackA.setNull(17, Types.INTEGER); // 脉冲车速
				}
				if (!alarmCode.equals(",")) {
					stUpdateLastTrackA.setString(18, alarmCode);
				} else {
					stUpdateLastTrackA.setNull(18, Types.NULL);
				}
				stUpdateLastTrackA.setString(19, basestatus);
				stUpdateLastTrackA.setString(20, extendstatus);
				if (eqStatus.get("213") != null) {
					stUpdateLastTrackA.setLong(21, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrackA.setLong(21, -1);
				}
				// 累计油耗
				if (eqStatus.get("213") != null) {
					stUpdateLastTrackA.setLong(22, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrackA.setNull(22, Types.INTEGER);
				}
				// 油门踏板位置
				if (StringUtils.isNumeric(eqStatus.get("504"))) {
					stUpdateLastTrackA.setLong(23, Long.parseLong(eqStatus.get("504")));
				} else {
					stUpdateLastTrackA.setNull(23, Types.INTEGER);
				}

				if (eqStatus.get("210") != null) {// ENGINE_ROTATE_SPEED
					stUpdateLastTrackA.setLong(24, Long.parseLong(eqStatus.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrackA.setNull(24, Types.INTEGER);
				}

				if (eqStatus.get("216") != null && !eqStatus.get("216").equals("")) { // OIL_INSTANT
					stUpdateLastTrackA.setLong(25, Long.parseLong(eqStatus.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrackA.setNull(25, Types.INTEGER);
				}
				String e_torque = eqStatus.get("503");
				if (StringUtils.isNumeric(e_torque)) { // E_TORQUE
					stUpdateLastTrackA.setLong(26, Long.parseLong(e_torque)); // 发动机扭矩
				} else {
					stUpdateLastTrackA.setNull(26, Types.INTEGER);
				}
				stUpdateLastTrackA.setString(27, msgid);// msid
				stUpdateLastTrackA.setString(28, eqStatus.get(Constant.SPEEDFROM));// 车速来源
				// 精准油耗
				if (null != eqStatus.get("219")) {
					stUpdateLastTrackA.setLong(29, Long.parseLong(eqStatus.get("219")));
				} else {
					stUpdateLastTrackA.setNull(29, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrackA.setInt(30, status);
				stUpdateLastTrackA.setInt(31, acc);
				// 判断锁车状态  
				String lockStatus = eqStatus.get("570");
				if (StringUtils.isNumeric(lockStatus)) {
					stUpdateLastTrackA.setString(32, Tools.parseLockStatus(lockStatus));
				} else {
					stUpdateLastTrackA.setNull(32, Types.NULL);
				}

				stUpdateLastTrackA.setString(33, vid);// VID
				stUpdateLastTrackA.executeUpdate();

			} catch (Exception e) {
				logger.error("更新轨迹包带报警异常:" + e.getMessage(), e);
				try {
					trackConnection.getMetaData();
					if (stUpdateLastTrackA == null) {
						stUpdateLastTrackA = createStatement(trackConnection, trackSubmit, sql_updateLastTrackA);
					}
				} catch (Exception ex) {
					logger.error("更新轨迹表状态重连", ex);
					stUpdateLastTrackA = recreateStatement(trackConnection, trackSubmit, sql_updateLastTrackA);
				}
			}
		} else {
			try {

				stUpdateLastTrack.setLong(1, lon);
				stUpdateLastTrack.setLong(2, lat);
				stUpdateLastTrack.setInt(3, gpsSpeed);
				if (eqStatus.get("9") != null) {
					stUpdateLastTrack.setLong(4, Long.parseLong(eqStatus.get("9")));
				} else {
					stUpdateLastTrack.setLong(4, -1);
				}
				if (eqStatus.get("9") != null) {
					stUpdateLastTrack.setLong(5, Long.parseLong(eqStatus.get("9")));
				} else {
					stUpdateLastTrack.setNull(5, Types.INTEGER);
				}
				stUpdateLastTrack.setInt(6, head);
				stUpdateLastTrack.setLong(7, utc);
				stUpdateLastTrack.setLong(8, DateTools.getCurrentUtcMsDate()); // SYSUTC
				stUpdateLastTrack.setLong(9, mapLon);
				stUpdateLastTrack.setLong(10, mapLat);
				if (eqStatus.get("6") != null) {
					stUpdateLastTrack.setLong(11, Long.parseLong(eqStatus.get("6"))); //
				} else {
					stUpdateLastTrack.setNull(11, Types.INTEGER);
				}

				if (eqStatus.get("24") != null) { // 判断数据
					stUpdateLastTrack.setLong(12, Long.parseLong(eqStatus.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrack.setLong(12, -1);
				}
				if (eqStatus.get("24") != null) {
					stUpdateLastTrack.setLong(13, Long.parseLong(eqStatus.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrack.setNull(13, Types.INTEGER);
				}

				if (!eqStatus.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrack.setDouble(14, Double.parseDouble(eqStatus.get(Constant.RATIO)));
				} else {
					stUpdateLastTrack.setNull(14, Types.DOUBLE);
				}

				if (!eqStatus.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrack.setInt(15, Integer.parseInt(eqStatus.get(Constant.GEARS)));
				} else {
					stUpdateLastTrack.setNull(15, Types.INTEGER);
				}

				if (eqStatus.get("7") != null) {
					stUpdateLastTrack.setInt(16, Integer.parseInt(eqStatus.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrack.setNull(16, Types.INTEGER);
				}

				stUpdateLastTrack.setString(17, basestatus);
				stUpdateLastTrack.setString(18, extendstatus);

				if (eqStatus.get("213") != null) {
					stUpdateLastTrack.setLong(19, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrack.setLong(19, -1);
				}

				// 累计油耗
				if (eqStatus.get("213") != null) {
					stUpdateLastTrack.setLong(20, Long.parseLong(eqStatus.get("213")));
				} else {
					stUpdateLastTrack.setNull(20, Types.INTEGER);
				}
				// 油门踏板位置
				if (eqStatus.get("504") != null && !eqStatus.get("504").equals("")) {
					stUpdateLastTrack.setLong(21, Long.parseLong(eqStatus.get("504")));
				} else {
					stUpdateLastTrack.setNull(21, Types.INTEGER);
				}

				if (eqStatus.get("210") != null) {// ENGINE_ROTATE_SPEED
					stUpdateLastTrack.setLong(22, Long.parseLong(eqStatus.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrack.setNull(22, Types.INTEGER);
				}

				if (eqStatus.get("216") != null) { // OIL_INSTANT
					stUpdateLastTrack.setLong(23, Long.parseLong(eqStatus.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrack.setNull(23, Types.INTEGER);
				}
				if (eqStatus.get("503") != null && !eqStatus.get("503").equals("")) { // E_TORQUE
					stUpdateLastTrack.setLong(24, Long.parseLong(eqStatus.get("503"))); // 发动机扭矩
				} else {
					stUpdateLastTrack.setNull(24, Types.INTEGER);
				}
				stUpdateLastTrack.setString(25, msgid);
				stUpdateLastTrack.setString(26, eqStatus.get(Constant.SPEEDFROM)); // 车速来源
				// 精准油耗
				if (null != eqStatus.get("219")) {
					stUpdateLastTrack.setLong(27, Long.parseLong(eqStatus.get("219")));
				} else {
					stUpdateLastTrack.setNull(27, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrack.setInt(28, status);
				stUpdateLastTrack.setInt(29, acc);
				// 判断锁车状态  
				if (StringUtils.isNumeric(eqStatus.get("570"))) {
					stUpdateLastTrack.setString(30, eqStatus.get("570"));
				} else {
					stUpdateLastTrack.setNull(30, Types.NULL);
				}

				stUpdateLastTrack.setString(31, vid);
				stUpdateLastTrack.executeUpdate();

			} catch (Exception e) {
				logger.error("更新轨迹包带报警异常:" + e.getMessage(), e);
				try {
					trackConnection.getMetaData();
					if (stUpdateLastTrack == null) {
						stUpdateLastTrack = createStatement(trackConnection, trackSubmit, sql_updateLastTrack);
					}
				} catch (Exception ex) {
					logger.error("更新轨迹包带报警异常:", ex);
					stUpdateLastTrack = recreateStatement(trackConnection, trackSubmit, sql_updateLastTrack);
				}
			}
		}
	}
	/*****************************************
	 * <li>描 述：更新非法轨迹信息状态</li><br>
	 * <li>时 间：2013-9-16 下午4:53:08</li><br>
	 * <li>参数： @param isPValid <li>参数： @param vid <li>参数： @param msgid</li><br>
	 * TODO
	 *****************************************/
	public void updateLastTrackISonLine(String vid, String baseStatus) {
		try {
			stUpdateLastTrackISonLine.setLong(1, System.currentTimeMillis());
			stUpdateLastTrackISonLine.setString(2, baseStatus);
			stUpdateLastTrackISonLine.setString(3, vid);
			stUpdateLastTrackISonLine.executeUpdate();

		} catch (SQLException e) {
			try {
				trackConnection.getMetaData();
				if (stUpdateLastTrackISonLine == null) {
					stUpdateLastTrackISonLine = createStatement(trackConnection, trackValidSubmit, sql_UpdateLastTrackISonLine);
				}
			} catch (Exception ex) {
				logger.error("提交安全设备状态重建连接异常:", ex);
				stUpdateLastTrackISonLine = recreateStatement(trackConnection, trackValidSubmit, sql_UpdateLastTrackISonLine);
			}
		}
	}
	
	/*****************************************
	 * <li>描 述：更新车辆状态</li><br>
	 * <li>时 间：2013-9-16 下午4:52:44</li><br>
	 * <li>参数： @param eqStatus</li><br>
	 * TODO
	 * 
	 * @throws Exception
	 *****************************************/
	public void updateVehicleLineStatus(EquipmentStatus eqStatus) throws Exception {
		try {
			stUpdateVehicleLineStatus.setLong(1, System.currentTimeMillis());

			stUpdateVehicleLineStatus.setInt(2, eqStatus.getTerminalStatus());
			if (eqStatus.getTerminalValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(3, eqStatus.getTerminalValue());
			} else {
				stUpdateVehicleLineStatus.setNull(3, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(4, eqStatus.getGpsStatusStatus());
			if (eqStatus.getGpsValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(5, eqStatus.getGpsValue());
			} else {
				stUpdateVehicleLineStatus.setNull(5, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(6, eqStatus.geteWaterStatus()); // 冷却液温度
																				// 比较值
			stUpdateVehicleLineStatus.setInt(7, eqStatus.geteWaterStatus()); // 冷却液温度
																				// 更新值

			stUpdateVehicleLineStatus.setDouble(8, eqStatus.geteWaterValue());

			if (eqStatus.geteWaterValue() == -2) {
				stUpdateVehicleLineStatus.setDouble(9, -1); // 将值置为无效
			} else {
				stUpdateVehicleLineStatus.setDouble(9, eqStatus.geteWaterValue());
			}

			// 蓄电池电压比较值
			stUpdateVehicleLineStatus.setInt(10, eqStatus.getExtVoltageStatus());
			stUpdateVehicleLineStatus.setInt(11, eqStatus.getExtVoltageStatus()); // 蓄电池电压更新值

			stUpdateVehicleLineStatus.setDouble(12, eqStatus.getExtVoltageValue());

			if (eqStatus.getExtVoltageValue() == -2) {
				stUpdateVehicleLineStatus.setDouble(13, -1);
			} else {
				stUpdateVehicleLineStatus.setDouble(13, eqStatus.getExtVoltageValue());
			}

			stUpdateVehicleLineStatus.setInt(14, eqStatus.getOilPressureStatus());

			if (eqStatus.getOilPressureValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(15, eqStatus.getOilPressureValue());
			} else {
				stUpdateVehicleLineStatus.setNull(15, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(16, eqStatus.getBrakePressureStatus());
			if (eqStatus.getBrakePressureValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(17, eqStatus.getBrakePressureValue());
			} else {
				stUpdateVehicleLineStatus.setNull(17, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(18, eqStatus.getBrakepadFrayStatus());
			if (eqStatus.getBrakepadFrayValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(19, eqStatus.getBrakepadFrayValue());
			} else {
				stUpdateVehicleLineStatus.setNull(19, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(20, eqStatus.getOilAramStatus());
			if (eqStatus.getOilAramValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(21, eqStatus.getOilAramValue());
			} else {
				stUpdateVehicleLineStatus.setNull(21, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(22, eqStatus.getAbsBugStatus());
			if (eqStatus.getAbsBugValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(23, eqStatus.getAbsBugValue());
			} else {
				stUpdateVehicleLineStatus.setNull(23, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(24, eqStatus.getCoolantLevelStatus());
			if (eqStatus.getCoolantLevelValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(25, eqStatus.getCoolantLevelValue());
			} else {
				stUpdateVehicleLineStatus.setNull(25, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(26, eqStatus.getAirFilterStatus());
			if (eqStatus.getAirFilterValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(27, eqStatus.getAirFilterValue());
			} else {
				stUpdateVehicleLineStatus.setNull(27, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(28, eqStatus.getMwereBlockingStatus());
			if (eqStatus.getMwereBlockingValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(29, eqStatus.getMwereBlockingValue());
			} else {
				stUpdateVehicleLineStatus.setNull(29, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(30, eqStatus.getFuelBlockingStatus());
			if (eqStatus.getFuelBlockingValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(31, eqStatus.getFuelBlockingValue());
			} else {
				stUpdateVehicleLineStatus.setNull(31, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(32, eqStatus.getEoilTemperatureStatus()); // 机油温度
			if (eqStatus.getEoilTemperatureValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(33, eqStatus.getEoilTemperatureValue());
			} else {
				stUpdateVehicleLineStatus.setNull(33, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(34, eqStatus.getRetarerHtStatus());
			if (eqStatus.getRetarerHtValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(35, eqStatus.getRetarerHtValue());
			} else {
				stUpdateVehicleLineStatus.setNull(35, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(36, eqStatus.getEhousingStatus());
			if (eqStatus.getEhousingValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(37, eqStatus.getEhousingValue());
			} else {
				stUpdateVehicleLineStatus.setNull(37, Types.DOUBLE);
			}

			// 整车状态（根据前边所有状态进行判断，只要有一个状态为红色，此处就标记红色，全部为绿色时此处标记为绿色，其他情况标记为灰色）(0
			// 绿灯 1红灯 2 灰灯)
			if (eqStatus.getTerminalStatus() == 0 && eqStatus.getGpsStatusStatus() == 0 && eqStatus.geteWaterStatus() == 0 && eqStatus.getExtVoltageStatus() == 0
					&& eqStatus.getOilPressureStatus() == 0 && eqStatus.getBrakePressureStatus() == 0 && eqStatus.getBrakepadFrayStatus() == 0 && eqStatus.getOilAramStatus() == 0
					&& eqStatus.getAbsBugStatus() == 0 && eqStatus.getCoolantLevelStatus() == 0 && eqStatus.getAirFilterStatus() == 0 && eqStatus.getMwereBlockingStatus() == 0
					&& eqStatus.getFuelBlockingStatus() == 0 && eqStatus.getEoilTemperatureStatus() == 0 && eqStatus.getRetarerHtStatus() == 0 && eqStatus.getEhousingStatus() == 0
					&& eqStatus.getAirPressureStatus() == 0

					&& eqStatus.getGpsFaultStatus() == 0 && eqStatus.getGpsOpenciruitStatus() == 0 && eqStatus.getGpsShortciruitStatus() == 0
					&& eqStatus.getTerminalUnderVoltageStatus() == 0 && eqStatus.getTerminalPowerDownStatus() == 0 && eqStatus.getTerminalScreenfalutStatus() == 0
					&& eqStatus.getTtsFaultStatus() == 0 && eqStatus.getCameraFaultStatus() == 0) {
				stUpdateVehicleLineStatus.setInt(38, 0);
			} else if (eqStatus.getTerminalStatus() == 2 && eqStatus.getGpsStatusStatus() == 2 && eqStatus.geteWaterStatus() == 2 && eqStatus.getExtVoltageStatus() == 2
					&& eqStatus.getOilPressureStatus() == 2 && eqStatus.getBrakePressureStatus() == 2 && eqStatus.getBrakepadFrayStatus() == 2 && eqStatus.getOilAramStatus() == 2
					&& eqStatus.getAbsBugStatus() == 2 && eqStatus.getCoolantLevelStatus() == 2 && eqStatus.getAirFilterStatus() == 2 && eqStatus.getMwereBlockingStatus() == 2
					&& eqStatus.getFuelBlockingStatus() == 2 && eqStatus.getEoilTemperatureStatus() == 2 && eqStatus.getRetarerHtStatus() == 2 && eqStatus.getEhousingStatus() == 2
					&& eqStatus.getAirPressureStatus() == 2

					&& eqStatus.getGpsFaultStatus() == 2 && eqStatus.getGpsOpenciruitStatus() == 2 && eqStatus.getGpsShortciruitStatus() == 2
					&& eqStatus.getTerminalUnderVoltageStatus() == 2 && eqStatus.getTerminalPowerDownStatus() == 2 && eqStatus.getTerminalScreenfalutStatus() == 2
					&& eqStatus.getTtsFaultStatus() == 2 && eqStatus.getCameraFaultStatus() == 2) {
				stUpdateVehicleLineStatus.setInt(38, 2);
			} else {
				stUpdateVehicleLineStatus.setInt(38, 1);
			}

			stUpdateVehicleLineStatus.setInt(39, eqStatus.getAirPressureStatus()); // 大气压力比较值
			stUpdateVehicleLineStatus.setInt(40, eqStatus.getAirPressureStatus()); // 大气压力更新值

			stUpdateVehicleLineStatus.setDouble(41, eqStatus.getAirPressureValue());

			if (eqStatus.getAirPressureValue() == -2) {
				stUpdateVehicleLineStatus.setDouble(42, -1);
			} else {
				stUpdateVehicleLineStatus.setDouble(42, eqStatus.getAirPressureValue());
			}

			stUpdateVehicleLineStatus.setInt(43, eqStatus.getGpsFaultStatus());
			if (eqStatus.getGpsFaultValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(44, eqStatus.getGpsFaultValue());
			} else {
				stUpdateVehicleLineStatus.setNull(44, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(45, eqStatus.getGpsOpenciruitStatus());
			if (eqStatus.getGpsOpenciruitValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(46, eqStatus.getGpsOpenciruitValue());
			} else {
				stUpdateVehicleLineStatus.setNull(46, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(47, eqStatus.getGpsShortciruitStatus());
			if (eqStatus.getGpsShortciruitValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(48, eqStatus.getGpsShortciruitValue());
			} else {
				stUpdateVehicleLineStatus.setNull(48, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(49, eqStatus.getTerminalUnderVoltageStatus());
			if (eqStatus.getTerminalUnderVoltageValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(50, eqStatus.getTerminalUnderVoltageValue());
			} else {
				stUpdateVehicleLineStatus.setNull(50, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(51, eqStatus.getTerminalPowerDownStatus());
			if (eqStatus.getTerminalPowerDownValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(52, eqStatus.getTerminalPowerDownValue());
			} else {
				stUpdateVehicleLineStatus.setNull(52, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(53, eqStatus.getTerminalScreenfalutStatus());
			if (eqStatus.getTerminalScreenfalutValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(54, eqStatus.getTerminalScreenfalutValue());
			} else {
				stUpdateVehicleLineStatus.setNull(54, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(55, eqStatus.getTtsFaultStatus());
			if (eqStatus.getTtsFaultValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(56, eqStatus.getTtsFaultValue());
			} else {
				stUpdateVehicleLineStatus.setNull(56, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(57, eqStatus.getCameraFaultStatus());
			if (eqStatus.getCameraFaultValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(58, eqStatus.getCameraFaultValue());
			} else {
				stUpdateVehicleLineStatus.setNull(58, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setString(59, eqStatus.getVid());
			stUpdateVehicleLineStatus.executeUpdate();

		} catch (Exception e) {
			logger.error("提交安全设备状态异常:" + e.getMessage(), e);
			try {
				updateStatusConn.getMetaData();
				if (stUpdateVehicleLineStatus == null) {
					stUpdateVehicleLineStatus = createStatement(updateStatusConn, equipmentSubmit, sql_updateVehicleLineStatus);
				}
			} catch (Exception ex) {
				logger.error("提交安全设备状态重建连接异常:", ex);
				stUpdateVehicleLineStatus = recreateStatement(updateStatusConn, equipmentSubmit, sql_updateVehicleLineStatus);
			}
		}
	}

	
	/*****************************************
	 * <li>描 述：根据车辆ID查询车辆状态编码</li><br>
	 * <li>时 间：2013-9-16 下午4:01:30</li><br>
	 * <li>参数： @param vid <li>参数： @return</li><br>
	 * TODO
	 *****************************************/
	public StatusCode queryStatusCode(String vid) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			StatusBean statusBean = null;
			StatusCode statusCode = Cache.getStatusMapValue(vid);
			if (statusCode != null) {
				return statusCode;
			}
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryStatusCode);
			ps.setString(1, vid);
			rs = ps.executeQuery();

			if (rs.next()) {
				statusCode = new StatusCode();
				statusBean = new StatusBean();// 终端状态
				statusBean.setType(rs.getInt("TERMINAL_STATUS_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_TERMINAL_STATUS"));
				statusBean.setMax(rs.getDouble("MAX_TERMINAL_STATUS"));
				statusCode.setTerminalStatus(statusBean);
				statusBean = new StatusBean(); // GPS状态
				statusBean.setType(rs.getInt("GPS_STATUS_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_GPS_STATUS"));
				statusBean.setMax(rs.getDouble("MAX_GPS_STATUS"));
				statusCode.setGpsStatus(statusBean);
				statusBean = new StatusBean(); // 冷却液温度
				statusBean.setType(rs.getInt("E_WATER_TEMP_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_E_WATER_TEMP"));
				statusBean.setMax(rs.getDouble("MAX_E_WATER_TEMP"));
				statusCode.seteWater(statusBean);
				statusBean = new StatusBean(); // 蓄电池电压
				statusBean.setType(rs.getInt("EXT_VOLTAGE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EXT_VOLTAGE"));
				statusBean.setMax(rs.getDouble("MAX_EXT_VOLTAGE"));
				statusCode.setExtVoltage(statusBean);
				statusBean = new StatusBean(); // 油压状态
				statusBean.setType(rs.getInt("OIL_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_OIL_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_OIL_PRESSURE"));
				statusCode.setOilPressure(statusBean);
				statusBean = new StatusBean(); // 制动气压值
				statusBean.setType(rs.getInt("BRAKE_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_BRAKE_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_BRAKE_PRESSURE"));
				statusCode.setBrakePressure(statusBean);
				statusBean = new StatusBean(); // 制动蹄片磨损
				statusBean.setType(rs.getInt("BRAKEPAD_FRAY_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_BRAKEPAD_FRAY"));
				statusBean.setMax(rs.getDouble("MAX_BRAKEPAD_FRAY"));
				statusCode.setBrakepadFray(statusBean);
				statusBean = new StatusBean(); // 燃油告警
				statusBean.setType(rs.getInt("OIL_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_OIL_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_OIL_ALARM"));
				statusCode.setOilAram(statusBean);
				statusBean = new StatusBean(); // ABS故障状态
				statusBean.setType(rs.getInt("ABS_BUG_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_ABS_BUG"));
				statusBean.setMax(rs.getDouble("MAX_ABS_BUG"));
				statusCode.setAbsBug(statusBean);
				statusBean = new StatusBean(); // 水位低状态
				statusBean.setType(rs.getInt("COOLANT_LEVEL_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_COOLANT_LEVEL"));
				statusBean.setMax(rs.getDouble("MAX_COOLANT_LEVEL"));
				statusCode.setCoolantLevel(statusBean);
				statusBean = new StatusBean(); // 空滤堵塞
				statusBean.setType(rs.getInt("AIR_FILTER_CLOG_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_AIR_FILTER_CLOG"));
				statusBean.setMax(rs.getDouble("MAX_AIR_FILTER_CLOG"));
				statusCode.setAirFilter(statusBean);
				statusBean = new StatusBean(); // 机虑堵塞
				statusBean.setType(rs.getInt("MWERE_BLOCKING_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_MWERE_BLOCKING"));
				statusBean.setMax(rs.getDouble("MAX_MWERE_BLOCKING"));
				statusCode.setMwereBlocking(statusBean);
				statusBean = new StatusBean(); // 燃油堵塞
				statusBean.setType(rs.getInt("FUEL_BLOCKING_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_FUEL_BLOCKING"));
				statusBean.setMax(rs.getDouble("MAX_FUEL_BLOCKING"));
				statusCode.setFuelBlocking(statusBean);
				statusBean = new StatusBean(); // 机油温度
				statusBean.setType(rs.getInt("EOIL_TEMPERATURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EOIL_TEMPERATURE_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_EOIL_TEMPERATURE_ALARM"));
				statusCode.setEoilTemperature(statusBean);
				statusBean = new StatusBean(); // 缓速器高温
				statusBean.setType(rs.getInt("RETARDER_HT_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_RETARDER_HT_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_RETARDER_HT_ALARM"));
				statusCode.setRetarerHt(statusBean);
				statusBean = new StatusBean(); // 仓温高状态
				statusBean.setType(rs.getInt("EHOUSING_HT_ALARM_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_EHOUSING_HT_ALARM"));
				statusBean.setMax(rs.getDouble("MAX_EHOUSING_HT_ALARM"));
				statusCode.setEhousing(statusBean);
				statusBean = new StatusBean(); // 大气压力
				statusBean.setType(rs.getInt("AIR_PRESSURE_VALTYPE"));
				statusBean.setMin(rs.getDouble("MIN_AIR_PRESSURE"));
				statusBean.setMax(rs.getDouble("MAX_AIR_PRESSURE"));
				statusCode.setAirPressure(statusBean);
				Cache.setStatusMapValue(vid, statusCode);
			}
			return statusCode;
		} catch (Exception e) {
			logger.error("查询车辆状态编码异常:" + e.getMessage(), e);
			return null;
		} finally {
			close(rs, ps, conn);
		}
	}
	
	/*****************************************
	 * <li>描 述：存储上线信息</li><br>
	 * <li>时 间：2013-9-25 下午6:22:22</li><br>
	 * <li>参数： @param online TODO
	 *****************************************/
	public void saveOnline(String vid, String plate, String uuid) {
		try {
			saveOnlineOPS.setString(1, uuid);
			saveOnlineOPS.setString(2, vid);
			saveOnlineOPS.setLong(3, System.currentTimeMillis());
			saveOnlineOPS.setString(4, plate);
			saveOnlineOPS.executeUpdate();

		} catch (Exception e) {
			logger.error("存储上线信息异常:" + e.getMessage() + ",vehicleNo:" + plate + ",VID:" + vid, e);
			try {
				saveOnlineConn.getMetaData();
				if (saveOnlineOPS == null) {
					saveOnlineOPS = createStatement(saveOnlineConn, onlineSubmit, sql_saveOnline);
				}
			} catch (Exception ex) {
				logger.error("重置存储上线信息异常:", ex);
				saveOnlineOPS = recreateStatement(saveOnlineConn, onlineSubmit, sql_saveOnline);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储下线</li><br>
	 * <li>时 间：2013-10-8 下午6:24:54</li><br>
	 * <li>参数： @param uuid</li><br>
	 * TODO
	 *****************************************/
	public void saveOffline(String uuid) {
		try {
			saveOfflineOPS.setString(1, uuid);
			saveOfflineOPS.setLong(2, System.currentTimeMillis());
			saveOfflineOPS.setInt(3, 1);
			saveOfflineOPS.executeUpdate();

		} catch (Exception ee) {
			logger.error("存储下线状态异常:" + ee.getMessage(), ee);
			try {
				saveOnlineConn.getMetaData();
				if (saveOfflineOPS == null) {
					saveOfflineOPS = createStatement(saveOfflineConn, offlineSubmit, sql_saveOffline);
				}
			} catch (Exception ex) {
				logger.error("重置存储下线状态异常:", ex);
				saveOfflineOPS = recreateStatement(saveOfflineConn, offlineSubmit, sql_saveOffline);
			}
		}
	}
	
	/*****************************************
	 * <li>描 述：更新最后位置表的上下线状态</li><br>
	 * <li>时 间：2013-9-25 下午6:45:35</li><br>
	 * <li>参数： @param packet</li><br>
	 * TODO
	 *****************************************/
	public void updateOnOfflineStatus(int status, String msgId, String vid) {
		try {
//			int positioning = -1;
//			String basestatus = "-1";
//			if(status == 0){ // 离线
//				positioning = 0;
//				basestatus = "0";	
//			}
			updateOnOfflineStatusPS.setInt(1, status);
			updateOnOfflineStatusPS.setLong(2, System.currentTimeMillis());
			updateOnOfflineStatusPS.setString(3, msgId);
//			updateOnOfflineStatusPS.setInt(4, positioning);		// 定位状态
//			updateOnOfflineStatusPS.setInt(5, positioning);		// 定位状态
			updateOnOfflineStatusPS.setInt(4, status);  		// ACC
//			updateOnOfflineStatusPS.setString(7, basestatus);	// 基础状态位
//			updateOnOfflineStatusPS.setString(8, basestatus);	// 基础状态位
			updateOnOfflineStatusPS.setString(5, vid);			// 车辆编号
			updateOnOfflineStatusPS.executeUpdate();				
		} catch (Exception ee) {
			logger.error("更新最后位置表的上下线状态异常:" + ee.getMessage(), ee);
			try {
				saveOnlineConn.getMetaData();
				if (updateOnOfflineStatusPS == null) {
					updateOnOfflineStatusPS = createStatement(trackConnection, updateOfflineStatusSubmit, sql_updateOnOfflineStatus);
				}
			} catch (Exception ex) {
				logger.error("重置更新最后位置表异常:", ex);
				updateOnOfflineStatusPS = recreateStatement(trackConnection, updateOfflineStatusSubmit, sql_updateOnOfflineStatus);
			}
		}
	}
	
	
//	/**
//	 * 更新线路站点缓存
//	 * TODO
//	 */
//	public void updateLineStation() {
//		PreparedStatement ps = null;
//		Connection conn = null;
//		ResultSet rs = null;
//		try {
//			int index = 0, error = 0;
//			long start = System.currentTimeMillis();
//			conn = 	OracleConnectionPool.getConnection();// 从连接池获得连接
//			ps   = 	conn.prepareStatement(sql_getLineStation);
//			rs   =	ps.executeQuery();
//			Map<String, List<Station>> map = new ConcurrentHashMap<String, List<Station>>();
//			while (rs.next()) {
//				try {
//					Station station = new Station();
//					String vid = rs.getString("VID");
//					station.setStationId(rs.getString("STATION_ID"));//围栏ID
//					station.setStationCode(rs.getString("STATION_CODE"));
//					station.setStationName(rs.getString("STATION_NAME"));//围栏名称
//					station.setStationRadius(rs.getLong("STATION_RADIUS"));
//					station.setStationNumber(rs.getLong("STATION_NUMBER"));
//					station.setLineStationCount(rs.getLong("STATION_NUM"));
//					station.setLineId(rs.getString("LINE_ID"));
//					station.setMapLat(rs.getLong("MAPLAT"));
//					station.setMapLon(rs.getLong("MAPLON"));
//
//					//key：车vid,value:该车所在的各个围栏集合
//					if(map.containsKey(vid)){
//						map.get(vid).add(station);
//					} else {
//						List<Station> list = new ArrayList<Station>();
//						list.add(station);
//						map.put(vid, list);
//					}
//					index++;
//				} catch (Exception e) {
//					logger.error("更新线路站点缓存异常:" + e.getMessage(), e);
//					error++;
//				}
//			}
//			int vehicleSize = map.size();
//			if(vehicleSize > 0){ // 删除失效围栏信息
//				Set<String> newSets = map.keySet();
//				Set<String> oldSets = StationCache.getStationKeys();
//				if(oldSets != null && oldSets.size() > 0){
//					oldSets.removeAll(newSets);
//					StationCache.removeKeys(oldSets);
//				}
//				StationCache.putStation(map);
//			}
//			long end = System.currentTimeMillis();
//			logger.info("更新线路站点[{}]条, 车[{}]辆, 耗时[{}]ms, 正常[{}]条, 异常[{}]条", index+error,vehicleSize,end-start,index,error);
//		} catch (Exception e) {
//			logger.error("更新线路站点缓存异常" + e.getMessage(),e);
//		} finally {
//			close(rs, ps, conn);
//		}
//	}
	
	/**
	 * 更新线路缓存
	 * TODO
	 */
//	public void updateLine() {
//		// 从连接池获得连接
//		Connection conn = null;
//		PreparedStatement ps = null;
//		ResultSet rs = null;
//		try {
//			int index = 0, error = 0;
//			long start = System.currentTimeMillis();
//			conn = OracleConnectionPool.getConnection();
//			ps = conn.prepareStatement(sql_updateLine);
//			rs = ps.executeQuery();
//			Map<String, List<Line>> map = new ConcurrentHashMap<String, List<Line>>();
//			long currentdayMillis = Utils.getCurrentDayYearMonthDayMillis();
//			while (rs.next()) {
//				try {
//					Line line = new Line();
//					String vid = rs.getString("vid");
//					line.setLineid(rs.getString("lineid"));
//					String lonlat = rs.getString("lonlat");
//					String[] ll = lonlat.split(",");
//					List<String> points = new ArrayList<String>();
//					for (int i = 0; i < ll.length; i += 2) {
//						points.add(ll[i] + "," + ll[i + 1]);
//					}
//					line.setPid(rs.getString("pid"));
//					line.setSpeedthreshold(rs.getLong("speedthreshold"));// 最大速度
//					line.setSpeedtimethreshold(rs.getLong("speedtimethreshold"));// 最大速度持续时间
//					line.setLineName(rs.getString("linename"));// 线路名称
//					line.setVid(vid);
//					line.setRoadwight(rs.getLong("roadwight"));
//					line.setBeginTime(currentdayMillis + (Utils.timeConvertSec(rs.getString("periodbegintime")) * 1000));
//					line.setEndTime(currentdayMillis + (Utils.timeConvertSec(rs.getString("periodendtime")) * 1000));
//
//					line.setLonlats(points);
//					line.setUsetype(rs.getString("usetype").split(","));
//					// key：车vid,value:该车所在的各个线路集合
//					if (map.containsKey(vid)) {
//						map.get(vid).add(line);
//					} else {
//						List<Line> list = new ArrayList<Line>();
//						list.add(line);
//						map.put(vid, list);
//					}
//					index++;
//				} catch (Exception e) {
//					logger.error("更新线路缓存异常:" + e.getMessage(), e);
//					error++;
//				}
//			}
//
//			int lineSize = map.size();
//			if (lineSize > 0) { // 删除失效围栏信息
//				Set<String> newSets = map.keySet();
//				Set<String> oldSets = LineCache.getLineKeys();
//				if (oldSets != null && oldSets.size() > 0) {
//					oldSets.removeAll(newSets);
//					LineCache.removeKeys(oldSets);
//				}
//				LineCache.putLine(map);
//			}
//			long end = System.currentTimeMillis();
//			logger.info("更新线路[{}]条, 车[{}]辆, 耗时[{}]ms, 正常[{}]条, 异常[{}]条", index + error, lineSize, end - start, index, error);
//		} catch (SQLException e) {
//			logger.error("更新线路缓存异常" + e.getMessage(), e);
//		} finally {
//			close(rs, ps, conn);
//		}
//	}
	
	/**
	 * 查询线路站点绑定列表
	 * TODO
	 */
	public static void queryLineStationBind() {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			int index = 0, error = 0;
			conn = OracleConnectionPool.getConnection();// 从连接池获得连接
			long start = System.currentTimeMillis();
			ps = conn.prepareStatement(sql_queryLineStationBind);
			rs = ps.executeQuery();
			Map<String, List<Station>> vehicleStationMap = new ConcurrentHashMap<String, List<Station>>();
			Map<String, StationNo> stations = new ConcurrentHashMap<String, StationNo>();
			Map<String, Set<String>> lineVehicleMap = new ConcurrentHashMap<String, Set<String>>();
			while (rs.next()) {
				try {
					String vid = rs.getString("VID");
					String lineId = rs.getString("LINE_ID");
					String stationId = rs.getString("STATION_ID");
					int    stationNo = rs.getInt("STATION_NUMBER");
					int lineDirection = rs.getInt("LINE_DIRECTION"); // 线路方向（1:起点发车；2:终点发车）
					long mapLon = rs.getLong("MAPLON");
					long mapLat = rs.getLong("MAPLAT"); 
					int stationRadius = rs.getInt("STATION_RADIUS"); //站点半径(米)
//					缓存线路对应车辆列表
					if(lineVehicleMap.containsKey(lineId)){
						lineVehicleMap.get(lineId).add(vid);
					} else {
						Set<String> vSet = new HashSet<String>();
						vSet.add(vid);
						lineVehicleMap.put(lineId, vSet);
					}
//					处理车辆对应站点信息列表
					Station station = new Station();
					station.setStationId(stationId);
					station.setStationRadius(stationRadius);
					station.setStationNo(stationNo); 
					station.setMapLon(mapLon);
					station.setMapLat(mapLat);
					station.setLineId(lineId); 
					station.setLineDirection(lineDirection);
					if(vehicleStationMap.containsKey(vid)){
						vehicleStationMap.get(vid).add(station);
					} else {
						List<Station> list = new ArrayList<Station>();
						list.add(station);
						vehicleStationMap.put(vid, list);
					}
//					处理车辆对应线路中的站点列表 
					StationLine sl = new StationLine(stationId, stationNo,lineDirection, mapLon, mapLat);
					if(stations.containsKey(vid)){
						if(stations.get(vid).getMap().containsKey(lineId)){
							stations.get(vid).getMap().get(lineId).add(sl);
						}else{
							List<StationLine> list = new ArrayList<StationLine>();
							list.add(sl);
							stations.get(vid).getMap().put(lineId, list);
						}
					}else{
						List<StationLine> list = new ArrayList<StationLine>();
						list.add(sl);
						Map<String, List<StationLine>> stationMap = new ConcurrentHashMap<String, List<StationLine>>();
						stationMap.put(lineId, list);
						StationNo sn = new StationNo(vid, stationMap);
						stations.put(vid, sn);
					}
					index++;
				} catch (Exception e) {
					logger.error("更新线路缓存异常:" + e.getMessage(), e);
					error++;
				}
			}
			long s1 = System.currentTimeMillis();
			if(lineVehicleMap.size() > 0){
				Set<String> newSets = lineVehicleMap.keySet();
				Set<String> oldSets = RedisService.getLineVehicleMapKeys();
				if(oldSets != null && oldSets.size() > 0){
					oldSets.removeAll(newSets);
					if(oldSets.size() > 0){
						RedisService.removeLineVehicleKeys(oldSets);
					}
				}
				Map<String, String> lineVehicle = new ConcurrentHashMap<String, String>();
				for(Map.Entry<String, Set<String>> lv : lineVehicleMap.entrySet()){
					lineVehicle.put(lv.getKey(), JSON.toJSONString(lv.getValue()));
				}
				RedisService.saveLineVehicleMap(lineVehicle);
			}
			int stationSize = stations.size();
			if(stationSize > 0){
				for(Map.Entry<String, StationNo> m : stations.entrySet()){
					m.getValue().mergeSites();
				}
				StationCache.putStationMap(stations); 
			}
			int vehicleSize = vehicleStationMap.size();
			if(vehicleSize > 0){ // 删除失效围栏信息 
				Set<String> newSets = vehicleStationMap.keySet();
				Set<String> oldSets = StationCache.getVehicleStationKeys();
				if(oldSets != null && oldSets.size() > 0){
					oldSets.removeAll(newSets);
					if(oldSets.size() > 0){
						StationCache.removeVehicleStationKeys(oldSets);
					}
				}
				StationCache.putVehicleStation(vehicleStationMap);
			}
			long end = System.currentTimeMillis();
//			System.out.println("查询线路站点绑定列表[{"+(index + error)+"}]条, 车[{"+stationSize+"}]辆, 总耗时[{"+(end-start)+"}]ms, 查询[{"+(s1-start)+"}]ms, 处理[{"+(end-s1)+"}]ms, 正常[{"+(index)+"}]条, 异常[{"+(error)+"}]条");
			logger.info("查询线路站点绑定列表[{}]条, 车[{}]辆, 总耗时[{}]ms, 查询[{}]ms, 处理[{}]ms, 正常[{}]条, 异常[{}]条", index + error, stationSize, end-start,s1-start,end-s1, index, error);
		} catch (Exception e) {
			logger.error("查询线路站点绑定列表异常" + e.getMessage(), e);
		} finally {
			close(rs, ps, conn);
		}
	}
	
	
	/**
	 * 初始化轨迹更新服务
	 */
	public void initService() {
		try {
			// 创建连接
			trackConnection = (OracleConnection) OracleConnectionPool.getConnection();
			
			// 轨迹包带总线数据更新最后位置到数据库
			sql_updateLastTrackLine = ConfigLoader.config.get("sql_updateLastTrackLine");
			trackSubmit = Integer.parseInt(ConfigLoader.config.get("trackSubmit"));
			stUpdateLastTrackLine = (OraclePreparedStatement) trackConnection.prepareStatement(sql_updateLastTrackLine);
			stUpdateLastTrackLine.setExecuteBatch(trackSubmit);

			// 轨迹包带总线数据更新最后位置到数据库
			sql_updateLastTrackALine = ConfigLoader.config.get("sql_updateLastTrackALine");
			stUpdateLastTrackALine = (OraclePreparedStatement) trackConnection.prepareStatement(sql_updateLastTrackALine);
			stUpdateLastTrackALine.setExecuteBatch(trackSubmit);
			
			// 轨迹包更新最后位置到数据库
			sql_updateLastTrack = ConfigLoader.config.get("sql_updateLastTrack");
			stUpdateLastTrack = (OraclePreparedStatement) trackConnection.prepareStatement(sql_updateLastTrack);
			stUpdateLastTrack.setExecuteBatch(trackSubmit);

			// 轨迹包更新最后位置到数据库
			sql_updateLastTrackA = ConfigLoader.config.get("sql_updateLastTrackA");
			stUpdateLastTrackA = (OraclePreparedStatement) trackConnection.prepareStatement(sql_updateLastTrackA);
			stUpdateLastTrackA.setExecuteBatch(trackSubmit);

			// 非法轨迹位置更新
			sql_UpdateLastTrackISonLine = ConfigLoader.config.get("sql_UpdateLastTrackISonLine");
			trackValidSubmit = Integer.parseInt(ConfigLoader.config.get("trackValidSubmit"));
			stUpdateLastTrackISonLine = (OraclePreparedStatement) trackConnection.prepareStatement(sql_UpdateLastTrackISonLine);
			stUpdateLastTrackISonLine.setExecuteBatch(trackValidSubmit);

			// 状态更新
			updateStatusConn = (OracleConnection) OracleConnectionPool.getConnection();
			equipmentSubmit = Integer.parseInt(ConfigLoader.config.get("equipmentSubmit"));
			sql_updateVehicleLineStatus = ConfigLoader.config.get("sql_updateVehicleLineStatus");
			stUpdateVehicleLineStatus = (OraclePreparedStatement) updateStatusConn.prepareStatement(sql_updateVehicleLineStatus);
			stUpdateVehicleLineStatus.setExecuteBatch(equipmentSubmit);
			
			// 更新最后位置表车辆上下线状态
//			updateOnOfflineStatusConn = (OracleConnection) OracleConnectionPool.getConnection();
			updateOfflineStatusSubmit = Integer.parseInt(ConfigLoader.config.get("updateOfflineStatusSubmit"));
			sql_updateOnOfflineStatus = ConfigLoader.config.get("sql_updateOnOfflineStatus");
			updateOnOfflineStatusPS = (OraclePreparedStatement) trackConnection.prepareStatement(sql_updateOnOfflineStatus);
			updateOnOfflineStatusPS.setExecuteBatch(updateOfflineStatusSubmit);
		} catch (SQLException e) {
			logger.error("初始化预编译声明失败:" + e.getMessage(), e);
		}
	}
	
	/**
	 * 初始化上下线预处理声明
	 */
	public void initOnlineStatement() {
		try {
			// 存储车辆上线
			saveOnlineConn = (OracleConnection) OracleConnectionPool.getConnection();
			onlineSubmit = Integer.parseInt(ConfigLoader.config.get("onlineSubmit"));
			sql_saveOnline = ConfigLoader.config.get("sql_saveOnline");
			saveOnlineOPS = (OraclePreparedStatement) saveOnlineConn.prepareStatement(sql_saveOnline);
			saveOnlineOPS.setExecuteBatch(onlineSubmit);

			// 存储车辆下线
			saveOfflineConn = (OracleConnection) OracleConnectionPool.getConnection();
			offlineSubmit = Integer.parseInt(ConfigLoader.config.get("offlineSubmit"));
			sql_saveOffline = ConfigLoader.config.get("sql_saveOffline");
			saveOfflineOPS = (OraclePreparedStatement) saveOfflineConn.prepareStatement(sql_saveOffline);
			saveOfflineOPS.setExecuteBatch(offlineSubmit);
			
		} catch (Exception e) {
			logger.error("初始化oracle更新车辆总线状态statement异常:", e);
		}
	}
	
	/**
	 * 创建设备更新服务
	 */
	public void initEquipmentPreparedStatement() {
		try {
			// 3. 更新车辆总线状态连接
			updateStatusConn = (OracleConnection) OracleConnectionPool.getConnection();
			sql_queryStatusCode = ConfigLoader.config.get("sql_queryStatusCode");
			sql_updateVehicleLineStatus = ConfigLoader.config.get("sql_updateVehicleLineStatus");
			stUpdateVehicleLineStatus = (OraclePreparedStatement) updateStatusConn.prepareStatement(sql_updateVehicleLineStatus);
			stUpdateVehicleLineStatus.setExecuteBatch(equipmentSubmit);
		} catch (SQLException e) {
			logger.error("初始化oracle更新车辆总线状态statement异常:", e);
		}
	}
	
	/*****************************************
	 * <li>描 述：创建预处理声明</li><br>
	 * <li>时 间：2013-10-22 下午3:10:24</li><br>
	 * <li>参数： @param conn <li>参数： @param count <li>参数： @param sql <li>参数： @return
	 * </li><br>
	 * TODO
	 *****************************************/
	private OraclePreparedStatement createStatement(OracleConnection conn, int count, String sql) {
		// 轨迹包更新最后位置到数据库
		OraclePreparedStatement stat = null;
		try {
			stat = (OraclePreparedStatement) conn.prepareStatement(sql);
			stat.setExecuteBatch(count);
		} catch (SQLException e) {
			logger.error("创建预处理声明异常", e);
		}
		return stat;
	}

	/****************************************
	 * <li>描 述：重置预处理声明</li><br>
	 * <li>时 间：2013-10-22 下午3:10:07</li><br>
	 * <li>参数： @param conn <li>参数： @param count <li>参数： @param sql <li>参数： @return
	 * </li><br>
	 * TODO
	 *****************************************/
	private OraclePreparedStatement recreateStatement(OracleConnection conn, int count, String sql) {
		OraclePreparedStatement stat = null;
		try {
			if (conn != null) {
				conn.close();
				conn = null;
			}
			// 从连接池获得连接
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			// 创建预处理声明
			stat = (OraclePreparedStatement) conn.prepareStatement(sql);
			stat.setExecuteBatch(count);
			logger.info("重置预处理声明!");
		} catch (SQLException e) {
			logger.error("重置预处理声明异常:", e);
		}
		return stat;
	}
	
	/**
	 * 关闭连接资源
	 * @param rs
	 * @param ps
	 * @param conn
	 * TODO
	 */
	private static void close(ResultSet rs, PreparedStatement ps, Connection conn) {
		try {
			if (rs != null) {
				rs.close();
			}
			if (ps != null) {
				ps.close();
			}
			if (conn != null) {
				conn.close();
			}
		} catch (SQLException e) {
			logger.error("关闭连接异常:" + e.getMessage(), e);
		}
	}


	
	/**
	 * 从结果集中获取车辆信息
	 * @param rs
	 * @return
	 */
	private static ServiceUnit getServiceUnit(ResultSet rs) {
		try {
			ServiceUnit su = new ServiceUnit(); 
			String oemcode = rs.getString("oemcode");
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
			su.setMotorcade(rs.getString("ENT_ID"));
			return su;
		} catch (SQLException e) {
			logger.error("从结果集中获取车辆信息异常:" + e.getMessage(), e);
			return null;
		}
	}
	/**
	 * 从结果集中获取车辆3G信息
	 * @param rs
	 * @return
	 * TODO
	 */
	private static ServiceUnit get3GServiceUnit(ResultSet rs) {
		try {
			ServiceUnit su = new ServiceUnit();
			String oemcode = rs.getString("oemcode");// 车机类型码
			String t_identifyno = rs.getString("dvr_simnum");// 终端标识号
			su.setMacid(oemcode + "_" + t_identifyno);
			su.setSuid(rs.getString("suid"));
			su.setPlatecolorid(rs.getString("plate_color_id"));
			su.setVid(rs.getString("vid"));
			su.setTeminalCode(rs.getString("tmodel_code"));
			su.setTid(rs.getString("tid"));
			su.setCommaddr(rs.getString("t_identifyno"));
			su.setOemcode(oemcode);
			su.setVehicleno(rs.getString("vehicle_no"));
			su.setVinCode(rs.getString("VIN_CODE"));
			su.setMotorcade(rs.getString("ENT_ID"));
			return su;
		} catch (SQLException e) {
			logger.error("从结果集中获取车辆3G信息异常:" + e.getMessage(), e);
			return null;
		}
	}
	/**
	 * 测试用 - 提交数据
	 * @throws SQLException
	 */
	public void commit() { 
		try {
			if(updateStatusConn!=null){  updateStatusConn.commit();}
			if(trackConnection!=null){  trackConnection.commit();}
			if(saveOnlineConn!=null){  saveOnlineConn.commit();}
			if(saveOfflineConn!=null){  saveOfflineConn.commit();}
//			if(updateOnOfflineStatusConn!=null){  updateOnOfflineStatusConn.commit();}
		} catch (Exception e) {
			logger.error("异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 站点设置
	 */
	public static void statiionSetting() {
//		1. 查询站点信息
		
//		2. 生成站点树
		
//		3. 缓存每个站点之间距离
		
	}
}
