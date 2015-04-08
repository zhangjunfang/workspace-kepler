package com.ctfo.statistics.alarm.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.statistics.alarm.common.Cache;
import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.common.Utils;
import com.ctfo.statistics.alarm.dao.OracleConnectionPool;
import com.ctfo.statistics.alarm.dao.RedisConnectionPool;
import com.ctfo.statistics.alarm.model.Alarm;
import com.ctfo.statistics.alarm.model.AlarmTrend;
import com.ctfo.statistics.alarm.model.AlarmTrendBean;
import com.ctfo.statistics.alarm.model.DriverOnoffline;
import com.ctfo.statistics.alarm.model.FatigueRules;
import com.ctfo.statistics.alarm.model.NightRules;
import com.ctfo.statistics.alarm.model.OverspeedRules;
import com.ctfo.statistics.alarm.model.StatisticsParma;
import com.ctfo.statistics.alarm.model.VehicleInfo;

public class OracleService {
	private static Logger log = LoggerFactory.getLogger(OracleService.class);
	/** 删除告警元数据信息SQL */
	private static String sql_deleteAlarmDetail;
	/** 删除超速日统计信息SQL */
	private static String sql_deleteOverspeedStatisticsDay;
	/** 删除超速月统计信息SQL */
	private static String sql_deleteOverspeedStatisticsMonth;
	/** 删除日告警级别明细SQL */
	private static String sql_deleteAlarmLevelStatisticsDay;
	/** 删除周告警级别明细SQL */
	private static String sql_deleteAlarmLevelStatisticsWeek;
	/** 删除月告警级别明细SQL */
	private static String sql_deleteAlarmLevelStatisticsMonth;
	/** 删除日告警类型明细SQL */
	private static String sql_deleteAlarmTypeStatisticsDay;
	/** 删除月告警类型明细SQL */
	private static String sql_deleteAlarmTypeStatisticsMonth;
	/** 删除日危险驾驶分析SQL */
	private static String sql_deleteDangerousDrivingStatisticsDay;
	/** 删除月危险驾驶分析SQL */
	private static String sql_deleteDangerousDrivingStatisticsMonth;
	/** 删除驾驶员日危险驾驶分析SQL */
	private static String sql_deleteDangerousDrivingStatisticsDriverDay;
	/** 删除驾驶员月危险驾驶分析SQL */
	private static String sql_deleteDangerousDrivingStatisticsDriverMonth;
	/** 删除日违规统计SQL */
	private static String sql_deleteViolationStatisticsDay;
	/** 删除月违规统计SQL */
	private static String sql_deleteViolationStatisticsMonth;
	/** 删除驾驶员日违规统计SQL */
	private static String sql_deleteViolationStatisticsDriverDay;
	/** 删除驾驶员月违规统计SQL */
	private static String sql_deleteViolationStatisticsDriverMonth;
	/** 导入偷漏油告警SQL */
	private static String sql_importOilAlarm;
	/** 导入离线告警SQL */
	private static String sql_importOfflineAlarm;
	/** 导入围栏告警SQL */
	private static String sql_importAreaAlarm;
	/** 导入超速告警SQL */
	private static String sql_importOverspeedAlarm;
//	/** 存储车辆当天最大总里程SQL */
//	private static String sql_saveLastTotalMileage;
	/** 删除车辆当天总里程SQL */
//	private static String sql_deleteLastTotalMileage;
	/** 存储报警列表SQL */
	private static String sql_saveAlarmList;
	/** 报警父级编码初始化SQL */
	private static String sql_initAlarmParentCode;
	/** 加载车辆信息SQL */
	private static String sql_initVehicleInfo;
	/** 统计日超速SQL */
	private static String sql_statisticsOverspeedDay;
	/** 统计月超速SQL */
	private static String sql_statisticsOverspeedMonth;
	/** 初始化限速设置SQL */
	private static String sql_initSpeedLimitSettings;
	/** 查询平台超速设置SQL */
	private static String sql_queryOverspeedSetting;
	/** 查询平台疲劳驾驶设置SQL */
	private static String sql_queryFatigueSetting;
	/** 查询平台夜间非法运营设置SQL */
	private static String sql_queryNightSetting;
	/** 查询告警级别设置SQL */
	private static String sql_queryAlarmLevel;
//	/** 查询组织列表SQL */
//	private static String sql_queryEntIdList;
//	/** 查询车队列表SQL */
//	private static String sql_queryTeamIdList;
	/** 告警类型日统计SQL */
	private static String sql_statisticsAlarmTypeDay;
	/** 告警级别日统计SQL */
	private static String sql_statisticsAlarmLevelDay;
	/** 告警级别周统计SQL */
	private static String sql_statisticsAlarmLevelWeek;
	/** 告警级别月统计SQL */
	private static String sql_statisticsAlarmLevelMonth;
	/** 查询日告警级别总数SQL */
	private static String sql_queryAlarmLevelTotalDay;
	/** 查询周告警级别总数SQL */
	private static String sql_queryAlarmLevelTotalWeek;
	/** 查询月告警级别总数SQL */
	private static String sql_queryAlarmLevelTotalMonth;
	/** 告警类型月统计SQL */
	private static String sql_statisticsAlarmTypeMonth;
	/** 危险驾驶日统计SQL */
	private static String sql_statisticsDangerousDrivingDay;
	/** 危险驾驶月统计SQL */
	private static String sql_statisticsDangerousDrivingMonth;
	/** 驾驶员危险驾驶日统计SQL */
	private static String sql_statisticsDangerousDrivingDriverDay;
	/** 驾驶员危险驾驶月统计SQL */
	private static String sql_statisticsDangerousDrivingDriverMonth;
	/** 运营违规日统计SQL */
	private static String sql_statisticsOperationsViolationDay;
	/** 运营违规月统计SQL */
	private static String sql_statisticsOperationsViolationMonth;
	/** 驾驶员运营违规日统计SQL */
	private static String sql_statisticsOperationsViolationDriverDay;
	/** 驾驶员运营违规月统计SQL */
	private static String sql_statisticsOperationsViolationDriverMonth;
	/** 查询驾驶员上下线记录SQL */
	private static String sql_queryDriverOnoffline;
	/**
	 * 告警存储数据接口实例
	 * TODO
	 * @param type
	 */
	public OracleService(String type) {
		try {
			if (type.equals("alarm")) {
				if (sql_saveAlarmList == null) {
					sql_saveAlarmList = ConfigLoader.config.get("sql_saveAlarmList");
				}
			}
		} catch (Exception e) {
			log.error("Oracle服务启动异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 初始化
	 * TODO
	 */
	public static void init() {
		
		sql_deleteAlarmDetail = ConfigLoader.config.get("sql_deleteAlarmDetail");
		sql_deleteOverspeedStatisticsDay = ConfigLoader.config.get("sql_deleteOverspeedStatisticsDay");
		sql_deleteOverspeedStatisticsMonth = ConfigLoader.config.get("sql_deleteOverspeedStatisticsMonth");
		sql_deleteAlarmLevelStatisticsDay = ConfigLoader.config.get("sql_deleteAlarmLevelStatisticsDay");
		sql_deleteAlarmLevelStatisticsWeek = ConfigLoader.config.get("sql_deleteAlarmLevelStatisticsWeek");
		sql_deleteAlarmLevelStatisticsMonth = ConfigLoader.config.get("sql_deleteAlarmLevelStatisticsMonth");
		sql_deleteAlarmTypeStatisticsDay = ConfigLoader.config.get("sql_deleteAlarmTypeStatisticsDay");
		sql_deleteAlarmTypeStatisticsMonth = ConfigLoader.config.get("sql_deleteAlarmTypeStatisticsMonth");
		sql_deleteDangerousDrivingStatisticsDay = ConfigLoader.config.get("sql_deleteDangerousDrivingStatisticsDay");
		sql_deleteDangerousDrivingStatisticsMonth = ConfigLoader.config.get("sql_deleteDangerousDrivingStatisticsMonth");
		sql_deleteDangerousDrivingStatisticsDriverDay = ConfigLoader.config.get("sql_deleteDangerousDrivingStatisticsDriverDay");
		sql_deleteDangerousDrivingStatisticsDriverMonth = ConfigLoader.config.get("sql_deleteDangerousDrivingStatisticsDriverMonth");
		sql_deleteViolationStatisticsDay = ConfigLoader.config.get("sql_deleteViolationStatisticsDay");
		sql_deleteViolationStatisticsMonth = ConfigLoader.config.get("sql_deleteViolationStatisticsMonth");
		sql_deleteViolationStatisticsDriverDay = ConfigLoader.config.get("sql_deleteViolationStatisticsDriverDay");
		sql_deleteViolationStatisticsDriverMonth = ConfigLoader.config.get("sql_deleteViolationStatisticsDriverMonth");
		
		sql_importOilAlarm = ConfigLoader.config.get("sql_importOilAlarm");
		sql_importOfflineAlarm = ConfigLoader.config.get("sql_importOfflineAlarm");
		sql_importAreaAlarm = ConfigLoader.config.get("sql_importAreaAlarm");
		sql_importOverspeedAlarm = ConfigLoader.config.get("sql_importOverspeedAlarm");
		
		sql_saveAlarmList = ConfigLoader.config.get("sql_saveAlarmList");
		sql_initAlarmParentCode = ConfigLoader.config.get("sql_initAlarmParentCode");
		sql_initVehicleInfo = ConfigLoader.config.get("sql_initVehicleInfo");
		sql_statisticsOverspeedDay = ConfigLoader.config.get("sql_statisticsOverspeedDay");
		sql_statisticsOverspeedMonth = ConfigLoader.config.get("sql_statisticsOverspeedMonth");
		sql_initSpeedLimitSettings = ConfigLoader.config.get("sql_initSpeedLimitSettings");
		sql_queryOverspeedSetting = ConfigLoader.config.get("sql_queryOverspeedSetting");
		sql_queryFatigueSetting = ConfigLoader.config.get("sql_queryFatigueSetting");
		sql_queryNightSetting = ConfigLoader.config.get("sql_queryNightSetting");
		sql_queryAlarmLevel = ConfigLoader.config.get("sql_queryAlarmLevel");
//		sql_queryEntIdList = ConfigLoader.config.get("sql_queryEntIdList");
//		sql_queryTeamIdList = ConfigLoader.config.get("sql_queryTeamIdList");
		sql_statisticsAlarmLevelDay = ConfigLoader.config.get("sql_statisticsAlarmLevelDay");
		sql_statisticsAlarmLevelWeek = ConfigLoader.config.get("sql_statisticsAlarmLevelWeek");
		sql_statisticsAlarmLevelMonth = ConfigLoader.config.get("sql_statisticsAlarmLevelMonth");
		sql_queryAlarmLevelTotalDay = ConfigLoader.config.get("sql_queryAlarmLevelTotalDay");
		sql_queryAlarmLevelTotalWeek = ConfigLoader.config.get("sql_queryAlarmLevelTotalWeek");
		sql_queryAlarmLevelTotalMonth = ConfigLoader.config.get("sql_queryAlarmLevelTotalMonth");
		
		sql_statisticsAlarmTypeDay = ConfigLoader.config.get("sql_statisticsAlarmTypeDay");
		sql_statisticsAlarmTypeMonth = ConfigLoader.config.get("sql_statisticsAlarmTypeMonth");
		
//		sql_saveLastTotalMileage = ConfigLoader.config.get("sql_saveLastTotalMileage");
//		sql_deleteLastTotalMileage = ConfigLoader.config.get("sql_deleteLastTotalMileage");
		sql_statisticsDangerousDrivingDay = ConfigLoader.config.get("sql_statisticsDangerousDrivingDay");
		sql_statisticsDangerousDrivingMonth = ConfigLoader.config.get("sql_statisticsDangerousDrivingMonth");
		sql_statisticsDangerousDrivingDriverDay = ConfigLoader.config.get("sql_statisticsDangerousDrivingDriverDay");
		sql_statisticsDangerousDrivingDriverMonth = ConfigLoader.config.get("sql_statisticsDangerousDrivingDriverMonth");
		
		sql_statisticsOperationsViolationDay = ConfigLoader.config.get("sql_statisticsOperationsViolationDay");
		sql_statisticsOperationsViolationMonth = ConfigLoader.config.get("sql_statisticsOperationsViolationMonth");
		sql_statisticsOperationsViolationDriverDay = ConfigLoader.config.get("sql_statisticsOperationsViolationDriverDay");
		sql_statisticsOperationsViolationDriverMonth = ConfigLoader.config.get("sql_statisticsOperationsViolationDriverMonth");
	
		sql_queryDriverOnoffline = ConfigLoader.config.get("sql_queryDriverOnoffline");
	}

	/**
	 * 初始化缓存 TODO
	 */
//	public static void initCache() {
//		try {
//			long start = System.currentTimeMillis();
////			SimpleDateFormat dateFull = new SimpleDateFormat("yyyyMMddHHmmss");
////			boolean manualOperation = Boolean.parseBoolean(ConfigLoader.config.get("manualOperation"));
////			// 手动处理设置日期
////			if (manualOperation) {
////				dateStr = ConfigLoader.config.get("manualOperationDate");
////				startUtc = dateFull.parse(dateStr + "020000").getTime();
////				endUtc = dateFull.parse(dateStr + "050000").getTime();
////			} else { // 自动处理上一天数据
////				dateStr = Utils.getYesterdayStr();
////				startUtc = dateFull.parse(dateStr + "020000").getTime();
////				endUtc = dateFull.parse(dateStr + "050000").getTime();
////			}
//
////			Cache.clearAll();
//			long s1 = System.currentTimeMillis();
////			initAlarmParentCode(); // 1 报警父级编码初始化
//			long s2 = System.currentTimeMillis();
////			initVehicleInfo(); // 2 加载车辆信息
//			long s3 = System.currentTimeMillis();
////			initSpeedLimitSettings(); // 3 初始化限速设置
//			long s4 = System.currentTimeMillis();
////			initFatigueRules(); // 4 加载平台设置疲劳驾驶规则
//			long s5 = System.currentTimeMillis();
////			initNightIllegalOperationsRules(); // 5 加载夜间非法运营规则
//			long s6 = System.currentTimeMillis();
////			initOverspeedRules(); // 6 加载平台设置超速规则
//			long s7 = System.currentTimeMillis();
////			queryAlarmLevelSetting(); // 7 查询告警级别设置
//			long end = System.currentTimeMillis();
//			log.info("<initCache> - 总耗时[{}]ms, 清空缓存[{}]ms, 报警父编码[{}]ms, 车辆[{}]ms, 限速设置[{}]ms, 疲劳规则[{}]ms, 夜间非法运营规则[{}]ms, 超速规则[{}]ms, 告警级别[{}]ms", end - start, s1 - start,
//					s2 - s1, s3 - s2, s4 - s3, s5 - s4, s6 - s5, s7 - s6, end - s7);
//		} catch (Exception e) {
//			log.error("初始化缓存异常:" + e.getMessage(), e);
//		}
//	}
	/**
	 * 导入超速告警
	 * @param parma
	 * TODO
	 */
	public static void importOverspeedAlarm(StatisticsParma parma) {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_importOverspeedAlarm);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("导入超速告警[{}]条, 耗时[{}]", count, Utils.getTimeDesc(end - start));
		} catch (Exception ex) {
			log.error("导入超速告警异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
		
	}
	/**
	 * 加载夜间非法运营规则 
	 * @param parma 
	 * TODO
	 */
	public static void initNightIllegalOperationsRules(StatisticsParma parma) {
		long start = System.currentTimeMillis();
		int index = 0;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			SimpleDateFormat format = new SimpleDateFormat("yyyyMMddHH:mm:ss");
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryNightSetting);
			rs = ps.executeQuery();
			Map<String, List<NightRules>> map = new HashMap<String, List<NightRules>>();
			long startUtc = parma.getIllegalDrivingStartUtc();
			long endUtc = parma.getIllegalDrivingEndUtc();
			String dateStr = parma.getDateStr();
			while (rs.next()) {
				index++;
				try {
					NightRules nr = new NightRules();
					String vid = rs.getString("VID");
					nr.setVid(vid);
					String startTime = rs.getString("START_TIME");
					long setStartUtc = startUtc;
					if (!startTime.equals("02:00:00")) {
						setStartUtc = format.parse(dateStr + startTime).getTime();
					}
					long setEndUtc = endUtc;
					String endTime = rs.getString("END_TIME");
					if (!endTime.equals("05:00:00")) {
						setEndUtc = format.parse(dateStr + endTime).getTime();
					}
					nr.setStartUtc(setStartUtc);
					nr.setEndUtc(setEndUtc);
					nr.setRunTotal(rs.getInt("DEFERRED") * 60 * 1000); // 夜间非法运营时间默认单位：分钟
					if (map.containsKey(vid)) {
						map.get(vid).add(nr);
					} else {
						List<NightRules> list = new ArrayList<NightRules>();
						list.add(nr);
						map.put(vid, list);
					}
				} catch (Exception e) {
					log.error("处理夜间非法运营异常" + e.getMessage(), e);
				}
			}
			if (map.size() > 0) {
				Cache.putAllNightRules(map);
			}
			long end = System.currentTimeMillis();
			log.info("加载夜间非法运营规则[{}]条, 耗时[{}]", index, Utils.getTimeDesc(end - start));
		} catch (Exception ex) {
			log.error("加载夜间非法运营规则异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}

	/**
	 * 加载疲劳驾驶规则 TODO
	 * @param parma 
	 */
	public static void initFatigueRules(StatisticsParma parma) { 
		long start = System.currentTimeMillis();
		int index = 0;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			SimpleDateFormat format = new SimpleDateFormat("yyyyMMddHH:mm:ss");
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryFatigueSetting);
			rs = ps.executeQuery();
			Map<String, List<FatigueRules>> map = new HashMap<String, List<FatigueRules>>();
			long startUtc = parma.getIllegalDrivingStartUtc();
			long endUtc = parma.getIllegalDrivingEndUtc();
			String dateStr = parma.getDateStr();
			while (rs.next()) {
				index++;
				try {
					FatigueRules rule = new FatigueRules();
					String vid = rs.getString("VID");
					rule.setVid(vid);
					String startTime = rs.getString("START_TIME");
					long setStartUtc = startUtc;
					if (!startTime.equals("02:00:00")) {
						setStartUtc = format.parse(dateStr + startTime).getTime();
					}
					long setEndUtc = endUtc;
					String endTime = rs.getString("END_TIME");
					if (!endTime.equals("05:00:00")) {
						setEndUtc = format.parse(dateStr + endTime).getTime();
					}
					rule.setStartUtc(setStartUtc);
					rule.setEndUtc(setEndUtc);
					if (map.containsKey(vid)) {
						map.get(vid).add(rule);
					} else {
						List<FatigueRules> list = new ArrayList<FatigueRules>();
						list.add(rule);
						map.put(vid, list);
					}
				} catch (Exception e) {
					log.error("处理疲劳驾驶规则异常" + e.getMessage(), e);
				}
			}
			if (map.size() > 0) {
				Cache.putAllFatigueRules(map);
			}
			long end = System.currentTimeMillis();
			log.info("加载疲劳驾驶规则[{}]条, 耗时[{}]", index, Utils.getTimeDesc(end - start));
		} catch (Exception ex) {
			log.error("加载疲劳驾驶规则异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}

	/**
	 * 加载超速规则 TODO
	 * @param parma 
	 */
	public static void initOverspeedRules(StatisticsParma parma) {
		long start = System.currentTimeMillis();
		int index = 0;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			SimpleDateFormat format = new SimpleDateFormat("yyyyMMddHH:mm:ss");
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryOverspeedSetting);
			rs = ps.executeQuery();
			Map<String, List<OverspeedRules>> map = new HashMap<String, List<OverspeedRules>>();
			long startUtc = parma.getIllegalDrivingStartUtc();
			long endUtc = parma.getIllegalDrivingEndUtc();
			String dateStr = parma.getDateStr();
			while (rs.next()) {
				index++;
				try {
					OverspeedRules rule = new OverspeedRules();
					String vid = rs.getString("VID");
					rule.setVid(vid);
					String startTime = rs.getString("START_TIME");
					long setStartUtc = startUtc;
					if (!startTime.equals("02:00:00")) {
						setStartUtc = format.parse(dateStr + startTime).getTime();
					}
					long setEndUtc = endUtc;
					String endTime = rs.getString("END_TIME");
					if (!endTime.equals("05:00:00")) {
						setEndUtc = format.parse(dateStr + endTime).getTime();
					}
					rule.setStartUtc(setStartUtc);
					rule.setEndUtc(setEndUtc);
					int scale = rs.getInt("SPEED_SCALE");
					Integer speedLimit = Cache.getSpeedLimitSettings(vid);
					int speed = 950;
					if (speedLimit != null && speedLimit > 0) {
						speed = (speedLimit * scale) / 100;
					}
					rule.setSpeedLimit(speed);
					if (map.containsKey(vid)) {
						map.get(vid).add(rule);
					} else {
						List<OverspeedRules> list = new ArrayList<OverspeedRules>();
						list.add(rule);
						map.put(vid, list);
					}
				} catch (Exception e) {
					log.error("处理超速规则异常" + e.getMessage(), e);
				}
			}
			if (map.size() > 0) {
				Cache.putAllOverspeedRules(map);
			}
			long end = System.currentTimeMillis();
			log.info("加载超速规则[{}]条, 耗时[{}]", index, Utils.getTimeDesc(end - start));
		} catch (Exception ex) {
			log.error("加载超速规则异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}

	/**
	 * 加载车辆信息 
	 * TODO
	 */
	public static void initVehicleInfo() {
		long start = System.currentTimeMillis();
		int index = 0;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_initVehicleInfo);
			rs = ps.executeQuery();
			while (rs.next()) {
				index++;
				VehicleInfo v = new VehicleInfo();
				v.setVid(rs.getString("VID"));
				v.setPlate(rs.getString("PLATE"));
				v.setTeamId(checkNull(rs.getString("TEAM_ID")));
				v.setTeamName(checkNull(rs.getString("TEAM_NAME")));
				v.setEntId(checkNull(rs.getString("ENT_ID")));
				v.setEntName(checkNull(rs.getString("ENT_NAME")));
				v.setSpeedThreshold(rs.getInt("SPEED_LIMIT"));
				// log.debug("车辆[{}]vid[{}]缓存完成", v.getVid(), v.getPlate());
				Cache.putVehicleInfo(v.getVid(), v);
			}
			long end = System.currentTimeMillis();
			log.info("加载车辆信息[{}]条, 耗时[{}]", index, Utils.getTimeDesc(end - start));
		} catch (Exception ex) {
			log.error("加载车辆信息异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}
	/**
	 * 检查数据是否为空
	 * @param str
	 * @return
	 */
	private static String checkNull(String str) {
		if (str != null && str.length() > 0) {
			return str;
		}
		return "";
	}

	/**
	 * 初始化报警父级编码 TODO
	 */
	public static void initAlarmParentCode() { 
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_initAlarmParentCode);
			rs = ps.executeQuery();
			while (rs.next()) {
				index++;
				Cache.putAlarmParentCode(rs.getString("CODE"), rs.getString("PARENT_CODE"));
			}
			long end = System.currentTimeMillis();
			log.info("加载父级编码[{}]条, 耗时[{}]", index, Utils.getTimeDesc(end - start));
		} catch (Exception ex) {
			log.error("初始化报警父级编码异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}

	/**
	 * 保存报警列表
	 * @param alarmList
	 * TODO
	 */
	public static void saveAlarmList(List<Alarm> alarmList) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveAlarmList);
			conn.setAutoCommit(false);
			for (Alarm alarm : alarmList) {
				index++;
				ps.setString(1, alarm.getAlarmId());
				ps.setString(2, alarm.getVid());
				ps.setString(3, alarm.getPlate());
				ps.setString(4, alarm.getTeamId());
				ps.setString(5, alarm.getTeamName());
				ps.setString(6, alarm.getEntId());
				ps.setString(7, alarm.getEntName());
				ps.setString(8, alarm.getAlarmCode());
				ps.setInt(9, alarm.getAlarmSource());
				ps.setString(10, alarm.getParentType());
				ps.setInt(11, alarm.getDuration());
				ps.setInt(12, alarm.getMaxSpeed());
				ps.setInt(13, alarm.getAvgSpeed());
				ps.setInt(14, alarm.getSpeedThreshold());
				ps.setInt(15, alarm.getAlarmTotalMileage());
				ps.setLong(16, alarm.getStartUtc());
				ps.setLong(17, alarm.getStartLon());
				ps.setLong(18, alarm.getStartLat());
				ps.setLong(19, alarm.getEndUtc());
				ps.setLong(20, alarm.getEndLon());
				ps.setLong(21, alarm.getEndLat());
				ps.setLong(22, alarm.getSysUtc());
				ps.addBatch();
			}
			ps.executeBatch();
			conn.commit();
			long end = System.currentTimeMillis();
			String vid = "";
			String plate = "";
			if(alarmList != null && alarmList.size() > 0){
				Alarm alarm = alarmList.get(0);
				vid = alarm.getVid();
				plate = alarm.getPlate();
			}
			log.info("车辆[{}-{}]存储[{}]条告警耗时[{}]", plate, vid, index, Utils.getTimeDesc(end-start));
		} catch (Exception ex) {
			log.error("保存报警列表异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}

	/**
	 * 保存报警列表
	 * @param alarmList
	 * TODO
	 */
	public void insertAlarmList(List<Alarm> alarmList) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveAlarmList);
			conn.setAutoCommit(false);
			for (Alarm alarm : alarmList) {
				index++;
				ps.setString(1, alarm.getAlarmId());
				ps.setString(2, alarm.getVid());
				ps.setString(3, alarm.getPlate());
				ps.setString(4, alarm.getTeamId());
				ps.setString(5, alarm.getTeamName());
				ps.setString(6, alarm.getEntId());
				ps.setString(7, alarm.getEntName());
				ps.setString(8, alarm.getAlarmCode());
				ps.setInt(9, alarm.getAlarmSource());
				ps.setString(10, alarm.getParentType());
				ps.setInt(11, alarm.getDuration());
				ps.setInt(12, alarm.getMaxSpeed());
				ps.setInt(13, alarm.getAvgSpeed());
				ps.setInt(14, alarm.getSpeedThreshold());
				ps.setInt(15, alarm.getAlarmTotalMileage());
				ps.setLong(16, alarm.getStartUtc());
				ps.setLong(17, alarm.getStartLon());
				ps.setLong(18, alarm.getStartLat());
				ps.setLong(19, alarm.getEndUtc());
				ps.setLong(20, alarm.getEndLon());
				ps.setLong(21, alarm.getEndLat());
				ps.setLong(22, alarm.getSysUtc());
				ps.setInt(23, alarm.getAlarmLevel());
				ps.setString(24, alarm.getDriverId());
				ps.setInt(25, alarm.getDriverSource());
				ps.setInt(26, alarm.getStartSpeed());
				ps.setInt(27, alarm.getEndSpeed());
				ps.addBatch();
			}
			ps.executeBatch();
			conn.commit();
			String vid = "";
			String plate = "";
			if(alarmList != null && alarmList.size() > 0){
				Alarm alarm = alarmList.get(0);
				vid = alarm.getVid();
				plate = alarm.getPlate();
			}
			long end = System.currentTimeMillis();
			log.info("车辆[{}-{}]存储[{}]条告警耗时[{}]", plate, vid, index, Utils.getTimeDesc(end-start));
		} catch (Exception ex) {
			log.error("保存报警列表异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}

	/**
	 * 导入偷漏油告警（平台软告警）
	 * @param parma
	 * TODO
	 */
	public static void importOilAlarm(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_importOilAlarm);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("导入偷漏油告警[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("导入偷漏油告警[" + parma.getDateStr() + "]异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 导入离线告警
	 * @param parma
	 * TODO
	 */
	public static void importOfflineAlarm(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_importOfflineAlarm);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("导入离线告警[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("导入离线告警[" + parma.getDateStr() + "]异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 导入围栏告警
	 * @param parma
	 * TODO
	 */
	public static void importAreaAlarm(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_importAreaAlarm);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("导入围栏告警[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("导入围栏告警[" + parma.getDateStr() + "]异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}

	/**
	 * 删除告警明细
	 * @param parma
	 * TODO
	 */
	public static void deleteAlarmDetail(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteAlarmDetail);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除告警日明细[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]告警日明细数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 删除超速日统计
	 * @param parma
	 * TODO
	 */
	public static void deleteOverspeedStatisticsDay(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteOverspeedStatisticsDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); // 删除告警元数据
			long end = System.currentTimeMillis();
			log.info("删除超速日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteOverspeedStatisticsMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate(); // 删除告警元数据
				long monthEnd = System.currentTimeMillis();
				log.info("删除超速月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]超速统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 删除告警级别日统计
	 * @param parma
	 * TODO
	 */
	public static void deleteAlarmLevelStatisticsDay(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteAlarmLevelStatisticsDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除告警级别日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isWeekStatistics()){
				ps = conn.prepareStatement(sql_deleteAlarmLevelStatisticsWeek);
				ps.setLong(1, parma.getLastWeekStartUtc());
				ps.setLong(2, parma.getLastWeekEndUtc());
				int weekCount = ps.executeUpdate(); // 删除告警元数据
				long weekEnd = System.currentTimeMillis();
				log.info("删除告警级别周统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", weekCount, Utils.getTimeDesc(weekEnd-end), parma.getLastWeekStartUtc(), parma.getLastWeekEndUtc());
			}
			end = System.currentTimeMillis();
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteAlarmLevelStatisticsMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate(); // 删除告警元数据
				long monthEnd = System.currentTimeMillis();
				log.info("删除告警级别月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "告警级别日统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 删除告警类型日统计
	 * @param parma
	 * TODO
	 */
	public static void deleteAlarmTypeStatisticsDay(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteAlarmTypeStatisticsDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除告警类型日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteAlarmTypeStatisticsMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate();
				long monthEnd = System.currentTimeMillis();
				log.info("删除告警类型月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]告警类型日统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 删除危险驾驶日统计
	 * @param parma
	 * TODO
	 */
	public static void deleteDangerousDrivingStatisticsDay(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteDangerousDrivingStatisticsDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除危险驾驶日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteDangerousDrivingStatisticsMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate(); 
				long monthEnd = System.currentTimeMillis();
				log.info("删除危险驾驶月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]危险驾驶日统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 *  删除驾驶员危险驾驶日统计
	 * @param parma
	 * TODO
	 */
	public static void deleteDangerousDrivingStatisticsDriver(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteDangerousDrivingStatisticsDriverDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除驾驶员危险驾驶日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteDangerousDrivingStatisticsDriverMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate(); 
				long monthEnd = System.currentTimeMillis();
				log.info("删除驾驶员危险驾驶月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]驾驶员危险驾驶统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}


	/**
	 * 删除违规日统计
	 * @param parma
	 * TODO
	 */
	public static void deleteViolationStatisticsDay(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteViolationStatisticsDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除运营违规日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteViolationStatisticsMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate(); 
				long monthEnd = System.currentTimeMillis();
				log.info("删除运营违规月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]运营违规日统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 删除驾驶员运营违规统计
	 * @param parma
	 * TODO
	 */
	public static void deleteViolationStatisticsDriver(StatisticsParma parma) {
		Connection conn = null; 
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_deleteViolationStatisticsDriverDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			int count = ps.executeUpdate(); 
			long end = System.currentTimeMillis();
			log.info("删除驾驶员运营违规日统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			if(parma.isMonthStatistics()){
				ps = conn.prepareStatement(sql_deleteViolationStatisticsDriverMonth);
				ps.setLong(1, parma.getLastMonthStartUtc());
				ps.setLong(2, parma.getLastMonthEndUtc());
				int monthCount = ps.executeUpdate(); 
				long monthEnd = System.currentTimeMillis();
				log.info("删除驾驶员运营违规月统计[{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]", monthCount, Utils.getTimeDesc(monthEnd-end), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
			}
		} catch (Exception e) {
			log.error("删除[" + parma.getDateStr() + "]运营驾驶员违规统计数据异常:" + e.getMessage(), e);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 初始化限速设置 
	 * TODO
	 */
	public static void initSpeedLimitSettings() {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int index = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_initSpeedLimitSettings);
			rs = ps.executeQuery();
			while (rs.next()) {
				Cache.putSpeedLimitSettings(rs.getString("VID"), rs.getInt("LIMIT"));
				index++;
			}
			long end = System.currentTimeMillis();
			log.info("初始化限速设置[{}]条, 耗时[{}]", index, Utils.getTimeDesc(end-start));
		} catch (Exception ex) {
			log.error("初始化限速设置异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}

	/**
	 * 查询告警级别设置 
	 * @param parma 
	 * TODO
	 */
	public static void queryAlarmLevelSetting(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int teamSize = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryAlarmLevel);
			// 查询所有企业对应的告警等级设置编码
			rs = ps.executeQuery();
			Map<String, String> teamMap = new ConcurrentHashMap<String, String>();
			while (rs.next()) {
				String teamId = rs.getString("TEAM_ID"); // 车队编号
				String alarmLevel = rs.getString("LEVEL_CODE"); // 级别编号
				teamMap.put(teamId, alarmLevel);
			}
			teamSize = teamMap.size();
			if (teamSize > 0) {
				Cache.putAllTeamAlarmLevelConf(teamMap);
			}
			long end = System.currentTimeMillis();
			log.info("查询告警级别设置: 车队[{}]个, 总耗时[{}]", teamSize, Utils.getTimeDesc(end-start));
		} catch (Exception ex) {
			log.error("查询告警级别设置异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}

	/**
	 * 告警类型日统计【告警统计】模块
	 * @param parma
	 * TODO
	 */
	public static void statisticsAlarmTypeDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsAlarmTypeDay);
			ps.setLong(1, parma.getCurrentUtc());
			ps.setLong(2, parma.getStartUtc());
			ps.setLong(3, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("日统计[{}]告警大类 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计告警大类异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 告警类型月统计【告警统计】模块
	 * @param parma
	 * TODO
	 */
	public static void statisticsAlarmTypeMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsAlarmTypeMonth);
			ps.setLong(1, parma.getLastMonthUtc());
			ps.setInt(2, parma.getYear());
			ps.setInt(3, parma.getMonth());
			ps.setLong(4, parma.getLastMonthStartUtc());
			ps.setLong(5, parma.getLastMonthEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
//			log.info("月统计[{}]告警大类 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			log.info("月统计[{}年{}月]告警大类 - [{}]条完成 - 耗时[{}], 月中[{}], 开始[{}], 结束[{}]",parma.getYear(), parma.getMonth(), count, Utils.getTimeDesc(end-start), parma.getLastMonthUtc(), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计告警大类异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 危险驾驶日统计【危险驾驶分析】
	 * @param parma
	 * TODO
	 */
	public static void statisticsDangerousDrivingDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsDangerousDrivingDay);
			ps.setLong(1, parma.getCurrentUtc());
			ps.setLong(2, parma.getStartUtc());
			ps.setLong(3, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("日统计[{}]危险驾驶 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计危险驾驶异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 危险驾驶月统计【危险驾驶分析】
	 * @param parma
	 * TODO
	 */
	public static void statisticsDangerousDrivingMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsDangerousDrivingMonth);
			ps.setLong(1, parma.getLastMonthUtc());
			ps.setInt( 2, parma.getYear());
			ps.setInt( 3, parma.getMonth());
			ps.setLong(4, parma.getLastMonthStartUtc());
			ps.setLong(5, parma.getLastMonthEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("月统计[{}年{}月]危险驾驶 - [{}]条完成 - 耗时[{}], 月中[{}], 开始[{}], 结束[{}]",parma.getYear(), parma.getMonth(), count, Utils.getTimeDesc(end-start), parma.getLastMonthUtc(), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计危险驾驶异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 驾驶员危险驾驶日统计【驾驶员危险驾驶分析】
	 * @param parma
	 * TODO
	 */
	public static void statisticsDangerousDrivingDriverDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsDangerousDrivingDriverDay);
			ps.setLong(1, parma.getCurrentUtc());
			ps.setLong(2, parma.getStartUtc());
			ps.setLong(3, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("日统计[{}]驾驶员危险驾驶 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]驾驶员日统计危险驾驶异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 驾驶员危险驾驶月统计【驾驶员危险驾驶分析】
	 * @param parma
	 * TODO
	 */
	public static void statisticsDangerousDrivingDriverMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsDangerousDrivingDriverMonth);
			ps.setLong(1, parma.getLastMonthUtc());
			ps.setInt( 2, parma.getYear());
			ps.setInt( 3, parma.getMonth());
			ps.setLong(4, parma.getLastMonthStartUtc());
			ps.setLong(5, parma.getLastMonthEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("月统计[{}年{}月]驾驶员危险驾驶 - [{}]条完成 - 耗时[{}], 月中[{}], 开始[{}], 结束[{}]",parma.getYear(), parma.getMonth(), count, Utils.getTimeDesc(end-start), parma.getLastMonthUtc(), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]驾驶员日统计危险驾驶异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}

	/**
	 * 运营违规日统计
	 * @param parma
	 * TODO
	 */
	public static void statisticsOperationsViolationDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsOperationsViolationDay);
			ps.setLong(1, parma.getCurrentUtc());
			ps.setLong(2, parma.getStartUtc());
			ps.setLong(3, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("日统计[{}]运营违规 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计运营违规异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 运营违规月统计
	 * @param parma
	 * TODO
	 */
	public static void statisticsOperationsViolationMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsOperationsViolationMonth);
			ps.setLong(1, parma.getLastMonthUtc());
			ps.setInt(2, parma.getYear());
			ps.setInt(3, parma.getMonth());
			ps.setLong(4, parma.getLastMonthStartUtc());
			ps.setLong(5, parma.getLastMonthEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
//			log.info("月统计[{}]运营违规 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			log.info("月统计[{}年{}月]运营违规 - [{}]条完成 - 耗时[{}], 月中[{}], 开始[{}], 结束[{}]",parma.getYear(), parma.getMonth(), count, Utils.getTimeDesc(end-start), parma.getLastMonthUtc(), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计运营违规异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 驾驶员运营违规日统计
	 * @param parma
	 * TODO
	 */
	public static void statisticsOperationsViolationDriverDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsOperationsViolationDriverDay);
			ps.setLong(1, parma.getCurrentUtc());
			ps.setLong(2, parma.getStartUtc());
			ps.setLong(3, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("日统计[{}]驾驶员运营违规 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计驾驶员运营违规日统计异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 驾驶员运营违规月统计
	 * @param parma
	 * TODO
	 */
	public static void statisticsOperationsViolationDriverMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsOperationsViolationDriverMonth);
			ps.setLong(1, parma.getLastMonthUtc());
			ps.setInt(2, parma.getYear());
			ps.setInt(3, parma.getMonth());
			ps.setLong(4, parma.getLastMonthStartUtc());
			ps.setLong(5, parma.getLastMonthEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("月统计[{}年{}月]驾驶员运营违规 - [{}]条完成 - 耗时[{}], 月中[{}], 开始[{}], 结束[{}]",parma.getYear(), parma.getMonth(), count, Utils.getTimeDesc(end-start), parma.getLastMonthUtc(), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计驾驶员运营违规月统计异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}


	/**
	 * 统计车辆超速
	 * @param currentDate
	 * @param vid
	 * @param startUtc
	 * @param endUtc
	 * TODO
	 */
	public static void statisticsOverspeedDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsOverspeedDay);
			ps.setLong(1, parma.getCurrentUtc());
			ps.setLong(2, parma.getStartUtc());
			ps.setLong(3, parma.getEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
			log.info("日统计[{}]车辆超速 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计单车超速异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 超速月统计
	 * @param parma
	 * TODO
	 */
	public static void statisticsOverspeedMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsOverspeedMonth);
			ps.setLong(1, parma.getLastMonthUtc());
			ps.setInt(2, parma.getYear());
			ps.setInt(3, parma.getMonth());
			ps.setLong(4, parma.getLastMonthStartUtc());
			ps.setLong(5, parma.getLastMonthEndUtc());
			int count = ps.executeUpdate();
			long end = System.currentTimeMillis();
//			log.info("月统计[{}]车辆超速 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
			log.info("月统计[{}年{}月]车辆超速 - [{}]条完成 - 耗时[{}], 月中[{}], 开始[{}], 结束[{}]",parma.getYear(), parma.getMonth(), count, Utils.getTimeDesc(end-start), parma.getLastMonthUtc(), parma.getLastMonthStartUtc(), parma.getLastMonthEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计单车月超速异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 删除车辆当天最大总里程
	 * TODO
	 */
//	public static void deleteLastTotalMileage() {
//		Connection conn = null;
//		PreparedStatement ps = null;
//		try {
//			long start = System.currentTimeMillis();
//			conn = OracleConnectionPool.getConnection();
//			ps = conn.prepareStatement(sql_deleteLastTotalMileage);
//			int count = ps.executeUpdate();
//			long end = System.currentTimeMillis();
//			log.info("删除车辆当天最大总里程[{}]条, 耗时[{}]",count, Utils.getTimeDesc(end-start));
//		} catch (Exception e) {
//			log.error("删除车辆当天最大总里程异常:" + e.getMessage(), e);
//		}finally {
//			close(null, ps, conn);
//		}
//	}
	/**
	 * 存储车辆当天最大总里程
	 * @param mileageMap 车辆里程集合
	 * TODO
	 */
//	public void saveLastTotalMileage(Map<String, Integer> mileageMap) {
//		Connection conn = null;
//		PreparedStatement ps = null;
//		try {
//			long start = System.currentTimeMillis();
//			conn = OracleConnectionPool.getConnection();
//			ps = conn.prepareStatement(sql_saveLastTotalMileage);
//			conn.setAutoCommit(false);
//			int count = 0;
//			for(Map.Entry<String, Integer> map : mileageMap.entrySet()){
//				ps.setString(1, map.getKey());
//				ps.setInt(2, map.getValue());
//				ps.addBatch();
//				count++;
//			}
//			ps.executeBatch();
//			conn.commit(); 
//			long end = System.currentTimeMillis();
//			log.info("存储车[{}]辆当天最大总里程耗时[{}]",count, Utils.getTimeDesc(end-start));
//		} catch (Exception e) {
//			log.error("存储车辆当天最大总里程异常:" + e.getMessage(), e);
//		}finally {
//			close(null, ps, conn);
//		}
//	}

	/**
	 * 告警级别日统计【首页告警趋势图】
	 * @param currentDayStr 当前日期字符串[yyyy-MM-dd]
	 * @param currentUtc 当前日期UTC时间[当天正午12:00:00]
	 * @param startUtc 当天开始时间[当天00:00:00]
	 * @param endUtc 当天结束时间[当天23:59:59] TODO
	 */
	public static void statisticsAlarmLevelDay(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Jedis jedis = null;
		int index = 0, entIndex = 0;
		int serious = 0, urgent = 0, general = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			// 统计当天车辆告警级别
			ps = conn.prepareStatement(sql_statisticsAlarmLevelDay);
			for (int i = 1; i < 4; i++) {
				ps.setLong(1, parma.getCurrentUtc());
				ps.setInt(2, i);
				ps.setInt(3, i);
				ps.setLong(4, parma.getStartUtc());
				ps.setLong(5, parma.getEndUtc());
				int size = ps.executeUpdate();
				if (i == 1) {
					serious = size;
				} else if (i == 2) {
					urgent = size;
				} else {
					general = size;
				}
				conn.commit();
			}
			long s1 = System.currentTimeMillis();
			// 查询当天告警级别
			ps = conn.prepareStatement(sql_queryAlarmLevelTotalDay);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			rs = ps.executeQuery();
			Map<String, AlarmTrend> levelMap = new ConcurrentHashMap<String, AlarmTrend>();
			while (rs.next()) {
				index++;
				String entId = rs.getString("ENT_ID");
				int total = rs.getInt("TOTAL");
				int level = rs.getInt("ALARM_LEVEL");
				if (levelMap.containsKey(entId)) {
					levelMap.get(entId).setAlarmLevel(level, total);
				} else {
					AlarmTrend alarmTrend = new AlarmTrend(parma.getCurrentUtc());
					alarmTrend.setAlarmLevel(level, total);
					levelMap.put(entId, alarmTrend);
				}
			}
			long s2 = System.currentTimeMillis();
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(1);
			for (Map.Entry<String, AlarmTrend> map : levelMap.entrySet()) {
				String trendKey = "TREND_DAY_" + map.getKey();
				String trend = jedis.get(trendKey);
				String newTrend = processDayTrend(map.getKey(), map.getValue(), trend);
				if(newTrend != null){
					jedis.set(trendKey, newTrend);
					entIndex++;
				}
			}
			long end = System.currentTimeMillis();
			log.info("日统计[{}]告警级别 - 总耗时[{}], 严重[{}]条, 中度[{}]条, 一般[{}]条, 当前统计[{}]条, 组织[{}]个, 统计导入[{}], 查询数据[{}], 更新缓存[{}]", parma.getStatisticsDayStr(), Utils.getTimeDesc(end - start),
					 serious, urgent, general, index, entIndex, Utils.getTimeDesc(s1-start), Utils.getTimeDesc(s2-start), Utils.getTimeDesc(end-s2));
		} catch (Exception ex) {
			if (jedis != null) {
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			log.error("统计日告警级别异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
			if (jedis != null) {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 告警级别周统计【首页告警趋势图】
	 * @param parma
	 * TODO
	 */
	public static void statisticsAlarmLevelWeek(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Jedis jedis = null;
		int index = 0, entIndex = 0;
		int serious = 0, urgent = 0, general = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			// 统计当天车辆告警级别
			ps = conn.prepareStatement(sql_statisticsAlarmLevelWeek);
			for (int i = 1; i < 4; i++) {
				ps.setLong(1, parma.getLastWeekUtc());
				ps.setInt(2, parma.getYear());
				ps.setInt(3, parma.getWeek());
				ps.setInt(4, i);
				ps.setInt(5, i);
				ps.setLong(6, parma.getLastWeekStartUtc());
				ps.setLong(7, parma.getLastWeekEndUtc());
				int size = ps.executeUpdate();
				if (i == 1) {
					serious = size;
				} else if (i == 2) {
					urgent = size;
				} else {
					general = size;
				}
				conn.commit();
			}
			long s1 = System.currentTimeMillis();
			// 查询当周告警级别
			ps = conn.prepareStatement(sql_queryAlarmLevelTotalWeek);
			ps.setLong(1, parma.getLastWeekStartUtc());
			ps.setLong(2, parma.getLastWeekEndUtc());
			rs = ps.executeQuery();
			Map<String, AlarmTrend> levelMap = new ConcurrentHashMap<String, AlarmTrend>();
			while (rs.next()) {
				index++;
				String entId = rs.getString("ENT_ID");
				int total = rs.getInt("TOTAL");
				int level = rs.getInt("ALARM_LEVEL");
				if (levelMap.containsKey(entId)) {
					levelMap.get(entId).setAlarmLevel(level, total);
				} else {
					AlarmTrend alarmTrend = new AlarmTrend(parma.getLastWeekUtc());
					alarmTrend.setAlarmLevel(level, total);
					levelMap.put(entId, alarmTrend);
				}
			}
			long s2 = System.currentTimeMillis();
//			向Redis缓存中存储告警趋势
			if(levelMap != null && levelMap.size() > 0){
				jedis = RedisConnectionPool.getJedisConnection();
				jedis.select(1);
				for (Map.Entry<String, AlarmTrend> map : levelMap.entrySet()) {
					String trendKey = "TREND_WEEK_" + map.getKey();
					String trend = jedis.get(trendKey);
					String newTrend = processWeekTrend(map.getKey(), map.getValue(), trend);
					if(newTrend != null){
						jedis.set(trendKey, newTrend);
						entIndex++;
					}
				}
			}
			long end = System.currentTimeMillis();
			log.info("周统计[{}]告警级别 - 总耗时[{}], 严重[{}]条, 中度[{}]条, 一般[{}]条, 当前统计[{}]条, 组织[{}]个, 统计导入[{}], 查询数据[{}], 更新缓存[{}]", parma.getStatisticsDayStr(), Utils.getTimeDesc(end - start),serious, urgent, general, index, entIndex, Utils.getTimeDesc(s1-start), Utils.getTimeDesc(s2 - start), Utils.getTimeDesc(end - s2));
		} catch (Exception ex) {
			if (jedis != null) {
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			log.error("统计周告警级别异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
			if (jedis != null) {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}

	/**
	 * 告警级别月统计【首页告警趋势图】
	 * @param parma
	 * TODO
	 */
	public static void statisticsAlarmLevelMonth(StatisticsParma parma) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Jedis jedis = null;
		int index = 0, entIndex = 0;
		int serious = 0, urgent = 0, general = 0;
		long start = System.currentTimeMillis();
		try {
			conn = OracleConnectionPool.getConnection();
			// 统计当月车辆告警级别
			ps = conn.prepareStatement(sql_statisticsAlarmLevelMonth);
			for (int i = 1; i < 4; i++) {
				ps.setLong(1, parma.getLastMonthUtc());
				ps.setInt(2, parma.getYear());
				ps.setInt(3, parma.getMonth());
				ps.setInt(4, i);
				ps.setInt(5, i);
				ps.setLong(6, parma.getLastMonthStartUtc());
				ps.setLong(7, parma.getLastMonthEndUtc());
				int size = ps.executeUpdate();
				if (i == 1) {
					serious = size;
				} else if (i == 2) {
					urgent = size;
				} else {
					general = size;
				}
				conn.commit();
			}
			long s1 = System.currentTimeMillis();
			// 查询当月告警级别
			ps = conn.prepareStatement(sql_queryAlarmLevelTotalMonth);
			ps.setLong(1, parma.getLastMonthStartUtc());
			ps.setLong(2, parma.getLastMonthEndUtc());
			rs = ps.executeQuery();
			Map<String, AlarmTrend> levelMap = new ConcurrentHashMap<String, AlarmTrend>();
			while (rs.next()) {
				index++;
				String entId = rs.getString("ENT_ID");
				int total = rs.getInt("TOTAL");
				int level = rs.getInt("ALARM_LEVEL");
				if (levelMap.containsKey(entId)) {
					levelMap.get(entId).setAlarmLevel(level, total);
				} else {
					AlarmTrend alarmTrend = new AlarmTrend(parma.getLastMonthUtc());
					alarmTrend.setAlarmLevel(level, total);
					levelMap.put(entId, alarmTrend);
				}
			}
			long s2 = System.currentTimeMillis();
//			向Redis缓存中存储告警趋势
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(1);
			for (Map.Entry<String, AlarmTrend> map : levelMap.entrySet()) {
				String trendKey = "TREND_MONTH_" + map.getKey();
				String trend = jedis.get(trendKey);
				String newTrend = processMonthTrend(map.getKey(), map.getValue(), trend);
				if(newTrend != null){
					jedis.set(trendKey, newTrend);
					entIndex++;
				}
			}
			long end = System.currentTimeMillis();
			log.info("月统计[{}年{}月]告警级别 - 总耗时[{}], 严重[{}]条, 中度[{}]条, 一般[{}]条, 当前统计[{}]条, 组织[{}]个, 统计导入[{}], 查询数据[{}], 更新缓存[{}]", parma.getYear(), parma.getMonth(), Utils.getTimeDesc(end - start),serious, urgent, general, index, entIndex, Utils.getTimeDesc(s1 - start), Utils.getTimeDesc(s2 - start), Utils.getTimeDesc(end - s2));
			
		} catch (Exception ex) {
			if (jedis != null) {
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			log.error("统计月告警级别异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
			if (jedis != null) {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}

	/**
	 * 处理告警趋势数据
	 * @param value
	 * @param trend
	 * @return TODO
	 * @throws Exception 
	 */
	private static String processDayTrend(String entId, AlarmTrend alarmTrend, String trend) throws Exception {
		String trendStr = null; 
		if(alarmTrend == null){
			log.error("处理[{}]告警日统计趋势异常:趋势内容为空", entId);
			return null;
		}
		int dayNum = getDayNumber(alarmTrend.getDate()); 
		alarmTrend.setDateStr(getDayStr(dayNum)); 
		if (trend != null) {
			int length = trend.length();
			if (length > 30) {
				// 有趋势数据就添加到趋势数据中
				trend = trend.substring(2, length - 2);
				List<AlarmTrendBean> trendList = new ArrayList<AlarmTrendBean>();
				String[] dayTrend = trend.split("\\},\\{");
				int min = 0;
				boolean error = false;
				try {
					if (dayTrend != null && dayTrend.length > 0) {
						for (String dayStr : dayTrend) {
							String dateStr = dayStr.substring(2, 12).replaceAll("-", "");
							int dateInt = Integer.parseInt(dateStr);
							if (min == 0) {
								min = dateInt;
							} else if (dateInt < min) {
								min = dateInt;
							}
							AlarmTrendBean trendBean = new AlarmTrendBean();
							trendBean.setDate(dateInt);
							trendBean.setTrend("{" + dayStr + "}");
							trendList.add(trendBean);
						}
					}
				} catch (Exception e) {
					log.error("[" + alarmTrend.getDate() + "]处理企业[" + entId + "]告警趋势数据异常:" + e.getMessage(), e);
					error = true;
				}
				if (error) { //发生异常后就不处理
					trendStr = trend;
				} else {
					// 将当前统计日期的数据写入列表
					if (dayNum > 0) {
							String value = getTrendValue(alarmTrend);
							boolean append = true; // 是否追加数据
							Collections.sort(trendList);
							AlarmTrendBean oldestTrend = trendList.get(0);
							boolean add = dayNum >= oldestTrend.getDate(); // 是否将统计结果加入趋势集 当前时间大于等于最久结果集才能处理
							// 如果统计数据超过6条，就删除历史最久一天的数据
							if (trendList.size() > 6) {
								for (AlarmTrendBean bean : trendList) {
									if (bean.getDate() == dayNum) {
										bean.setTrend(value);
										append = false;
									}
								}
								if (append) {
									if (add) { // 删除最历史最久那天的趋势
										trendList.remove(0);
									}
								}
							}
							if (append) {
								if (add) { // 添加当前趋势结果到集合中
									AlarmTrendBean trendBean = new AlarmTrendBean();
									trendBean.setDate(dayNum);
									trendBean.setTrend(value);
									trendList.add(trendBean);
									Collections.sort(trendList);
								}
							}
							trendStr = getTrendListStr(trendList);
					} else {
						log.error("[" + alarmTrend.getDate() + "]处理企业[" + entId + "]告警趋势当前日期异常:" + alarmTrend.getDate());
					}
				}
			} else {
				// 没有趋势数据就将前几天的数据置空，再加上当前趋势
				trendStr = getAlarmDayTrendStr(alarmTrend);
			}
		} else {
			trendStr = getAlarmDayTrendStr(alarmTrend);
		}
		return trendStr;
	}

	/**
	 * 处理周告警趋势
	 * @param weekEntId
	 * @param alarmWeekTrend
	 * @param trendWeekStr
	 * @return
	 * TODO
	 */
	private static String processWeekTrend(String weekEntId, AlarmTrend alarmWeekTrend, String trendWeekStr) {
		String trendStr = null;
		if(alarmWeekTrend == null){
			log.error("处理[{}]告警周统计趋势异常:趋势内容为空", weekEntId);
			return null;
		}
		int weekNum = getWeekNumber(alarmWeekTrend.getDate());
		alarmWeekTrend.setDateStr(getWeekOrMonthStr(weekNum));  
		if (trendWeekStr != null) {
			int length = trendWeekStr.length();
			if (length > 30) {
				// 有趋势数据就添加到趋势数据中
				trendWeekStr = trendWeekStr.substring(2, length - 2);
				List<AlarmTrendBean> trendList = new ArrayList<AlarmTrendBean>();
				String[] dayTrend = trendWeekStr.split("\\},\\{");
				boolean error = false;
				try {
					if (dayTrend != null && dayTrend.length > 0) {
						for (String dayStr : dayTrend) {
							String dateStr = dayStr.substring(2, 9).replaceAll("-", "");
							int minWeekInt = Integer.parseInt(dateStr);
							AlarmTrendBean trendBean = new AlarmTrendBean();
							trendBean.setDate(minWeekInt);
							trendBean.setTrend("{" + dayStr + "}");
							trendList.add(trendBean);
						}
					}
				} catch (Exception e) {
					log.error("[" + alarmWeekTrend.getDate() + "]处理企业[" + trendWeekStr + "]告警趋势数据异常:" + e.getMessage(), e);
					error = true;
				}
				if (error) {
					trendStr = trendWeekStr;
				} else {
					// 将当前统计日期的数据写入列表
					if (weekNum > 0) {
						String value = getTrendValue(alarmWeekTrend);
						boolean append = true; // 是否追加数据
						Collections.sort(trendList);
						AlarmTrendBean oldestTrend = trendList.get(0);
						boolean add = weekNum >= oldestTrend.getDate(); // 是否将统计结果加入趋势集  当前时间大于等于最久结果集才能处理
						if (trendList.size() > 6) {
							// 如果统计数据中时间有相同就替换-并设置状态为不追加
							for (AlarmTrendBean bean : trendList) {
								if (bean.getDate() == weekNum) {
									bean.setTrend(value);
									append = false;
								}
							}
							if (append) {// 不追加时判断是否删除趋势
								if (add) { // 删除最历史最久那天的趋势
									trendList.remove(0);
								}
							}
						}
						if (append) {
							if (add) { // 添加当前趋势结果到集合中
								AlarmTrendBean trendBean = new AlarmTrendBean();
								trendBean.setDate(weekNum);
								trendBean.setTrend(value);
								trendList.add(trendBean);
								Collections.sort(trendList);
							}
						}
						trendStr = getTrendListStr(trendList);
					} else {
						log.error("[" + alarmWeekTrend.getDate() + "]处理企业[" + trendWeekStr + "]告警趋势当前日期异常:" + alarmWeekTrend.getDate());
					}
				}
			} else {
				// 没有趋势数据就将前几天的数据置空，再加上当前趋势
				trendStr = getAlarmWeekTrendStr(alarmWeekTrend);
			}
		} else {
			trendStr = getAlarmWeekTrendStr(alarmWeekTrend);
		}
		return trendStr;
	}
	/**
	 * 处理月告警趋势
	 * @param entId				组织编号
	 * @param alarmMonthTrend	月告警趋势 
	 * @param trendMonthStr		缓存中趋势列表字符串
	 * @return
	 * TODO
	 */
	private static String processMonthTrend(String entId, AlarmTrend alarmMonthTrend, String trendMonthStr) {
		String trendStr = null;
		if(alarmMonthTrend == null){
			log.error("处理[{}]告警月统计趋势异常:趋势内容为空", entId);
			return null;
		}
		int monthNum = getMonthNumber(alarmMonthTrend.getDate()); 
		alarmMonthTrend.setDateStr(getWeekOrMonthStr(monthNum)); 
		if (trendMonthStr != null) {
			int length = trendMonthStr.length();
			if (length > 30) {
				// 有趋势数据就添加到趋势数据中
				trendMonthStr = trendMonthStr.substring(2, length - 2);
				List<AlarmTrendBean> trendList = new ArrayList<AlarmTrendBean>();
				String[] monthTrend = trendMonthStr.split("\\},\\{");
				boolean error = false;
				try {
					if (monthTrend != null && monthTrend.length > 0) {
						for (String dayStr : monthTrend) {
							String dateStr = dayStr.substring(2, 9).replaceAll("-", "");
							int minMonthInt = Integer.parseInt(dateStr);
							AlarmTrendBean trendBean = new AlarmTrendBean();
							trendBean.setDate(minMonthInt);
							trendBean.setTrend("{" + dayStr + "}");
							trendList.add(trendBean);
						}
					}
				} catch (Exception e) {
					log.error("[" + alarmMonthTrend.getDateStr() + "]处理企业[" + trendMonthStr + "]告警趋势数据异常:" + e.getMessage(), e);
					error = true;
				}
				if (error) {
					trendStr = trendMonthStr;
				} else {
					// 将当前统计日期的数据写入列表
					if (monthNum > 0) {
						String value = getTrendValue(alarmMonthTrend);
						boolean append = true; // 是否追加数据
						Collections.sort(trendList);
						AlarmTrendBean oldestTrend = trendList.get(0);
						boolean add = monthNum >= oldestTrend.getDate(); // 是否将统计结果加入趋势集  当前时间大于等于最久结果集才能处理
						if (trendList.size() > 6) {
							// 如果统计数据中时间有相同就替换-并设置状态为不追加
							for (AlarmTrendBean bean : trendList) {
								if (bean.getDate() == monthNum) {
									bean.setTrend(value);
									append = false;
								}
							}
							if (append) {// 不追加时判断是否删除趋势
								if (add) { // 删除最历史最久那天的趋势
									trendList.remove(0);
								}
							}
						}
						if (append) {
							if (add) { // 添加当前趋势结果到集合中
								AlarmTrendBean trendBean = new AlarmTrendBean();
								trendBean.setDate(monthNum);
								trendBean.setTrend(value);
								trendList.add(trendBean);
								Collections.sort(trendList);
							}
						}
						trendStr = getTrendListStr(trendList);
					} else {
						log.error("[" + alarmMonthTrend.getDate() + "]处理企业[" + trendMonthStr + "]告警趋势当前日期异常:" + alarmMonthTrend.getDate());
					}
				}
			} else {
				// 没有趋势数据就将前几天的数据置空，再加上当前趋势
				trendStr = getAlarmMonthTrendStr(alarmMonthTrend);
			}
		} else {
			trendStr = getAlarmMonthTrendStr(alarmMonthTrend);
		}
		return trendStr;
	}

	/**
	 * 获取日期数
	 * @param date
	 * @return
	 * TODO
	 */
	private static int getDayNumber(long dateUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(dateUtc);
		int year = cal.get(Calendar.YEAR);
		int month = cal.get(Calendar.MONTH) + 1;
		int day = cal.get(Calendar.DAY_OF_MONTH);
		StringBuffer sb = new StringBuffer();
		sb.append(year);
		int dayNumber = 0;
		if(month < 10){
			sb.append("0").append(month);
		}else{
			sb.append(month);
		}
		if(day < 10){
			sb.append("0").append(day);
		}else{
			sb.append(day);
		}
		if(Utils.isNumeric(sb.toString())){
			dayNumber = Integer.parseInt(sb.toString());
		}
		return dayNumber;
	}
	/**
	 * 获取年度周数字（yyyyww）
	 * @param dateUtc
	 * @return
	 * TODO
	 */
	private static int getWeekNumber(long dateUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(dateUtc);
		cal.set(Calendar.DAY_OF_WEEK, 7); 
		int year = cal.get(Calendar.YEAR);
		int week = cal.get(Calendar.WEEK_OF_YEAR);
		StringBuffer sb = new StringBuffer();
		int weekNumber = 0;
		if(week < 10){
			sb.append(year).append("0").append(week);
			
		}else{
			sb.append(year).append(week);
		}
		if(Utils.isNumeric(sb.toString())){
			weekNumber = Integer.parseInt(sb.toString());
		}
		return weekNumber;
	}
	/**
	 * 获取年度周数字（yyyyww）
	 * @param dateUtc
	 * @return
	 * TODO
	 */
	private static int getMonthNumber(long dateUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(dateUtc);
		int year = cal.get(Calendar.YEAR);
		int month = cal.get(Calendar.MONTH) + 1; //  月份:0~11
		StringBuffer sb = new StringBuffer();
		int monthNumber = 0;
		if(month < 10){
			sb.append(year).append("0").append(month);
		}else{
			sb.append(year).append(month);
		}
		if(Utils.isNumeric(sb.toString())){
			monthNumber = Integer.parseInt(sb.toString());
		}
		return monthNumber;
	}
	/**
	 * 获取趋势集合字符串
	 * 
	 * @param trendMap
	 * @return TODO
	 */
	private static String getTrendListStr(List<AlarmTrendBean> trendList) {
		Collections.sort(trendList);
		StringBuffer sb = new StringBuffer(512);
		sb.append("[");
		for (AlarmTrendBean bean : trendList) {
			sb.append(bean.getTrend()).append(",");
		}
		String result = sb.substring(0, sb.length() - 1);
		return result + "]";
	}


	/**
	 * 获取日趋势值
	 * 
	 * @param alarmTrend
	 * @return TODO
	 */
	private static String getTrendValue(AlarmTrend alarmTrend) {
		StringBuffer sb = new StringBuffer(64);
		sb.append("{D:"); // 日期关键字
		sb.append(alarmTrend.getDateStr());
		sb.append(",G:").append(alarmTrend.getGeneral());// 普通告警关键字
		sb.append(",S:").append(alarmTrend.getSerious());// 严重告警关键字
		sb.append(",F:0,U:");
		sb.append(alarmTrend.getUrgent()).append("}");// 中度告警关键字
		return sb.toString();
	}
	/**
	 * 获取日告警趋势字符串
	 * @param alarmTrend
	 * @return TODO
	 */
	private static String getAlarmDayTrendStr(AlarmTrend alarmTrend) {
		StringBuffer sb = new StringBuffer(512);
		sb.append("[");
		for (int i = 1; i < 7; i++) {
			sb.append("{D:"); // 日期关键字
			sb.append(getDayDateStr(7 - i, alarmTrend.getDate()));
			sb.append(",G:0,S:0,F:0,U:0},");
		}
		sb.append(getTrendValue(alarmTrend)).append("]");
		return sb.toString();
	}

	/**
	 * 获取周告警趋势字符串
	 * @param alarmTrend
	 * @return TODO
	 */
	private static String getAlarmWeekTrendStr(AlarmTrend alarmTrend) {
		StringBuffer sb = new StringBuffer(512);
		sb.append("[");
		for (int i = 1; i < 7; i++) {
			sb.append("{D:"); // 日期关键字
			sb.append(getWeekDateStr(7 - i, alarmTrend.getDate()));
			sb.append(",G:0,S:0,F:0,U:0},");
		}
		sb.append(getTrendValue(alarmTrend)).append("]");
		return sb.toString();
	}
	/**
	 * 获取月告警趋势字符串
	 * @param alarmTrend
	 * @return TODO
	 */
	private static String getAlarmMonthTrendStr(AlarmTrend alarmTrend) {
		StringBuffer sb = new StringBuffer(512);
		sb.append("[");
		for (int i = 1; i < 7; i++) {
			sb.append("{D:"); // 日期关键字
			sb.append(getMonthDateStr(7 - i, alarmTrend.getDate()));
			sb.append(",G:0,S:0,F:0,U:0},");
		}
		sb.append(getTrendValue(alarmTrend)).append("]");
		return sb.toString();
	}

//	/**
//	 * 获取周趋势值
//	 * 
//	 * @param alarmTrend
//	 * @return TODO
//	 */
//	private static String getWeekTrendValue(AlarmTrend alarmTrend) {
//		StringBuffer sb = new StringBuffer(64);
//		sb.append("{D:"); // 日期关键字
//		sb.append(getWeekOfYear(alarmTrend.getDateStr()));
//		sb.append(",G:").append(alarmTrend.getGeneral());// 普通告警关键字
//		sb.append(",S:").append(alarmTrend.getSerious());// 严重告警关键字
//		sb.append(",F:0,U:");
//		sb.append(alarmTrend.getUrgent()).append("}");// 中度告警关键字
//		return sb.toString();
//	}
//	/**
//	 * 获取当前时间周字符串（YYYY-本年第几周）
//	 * @param dateUtc
//	 * @return
//	 */
//	private static String getWeekOfYear(long dateUtc) {
//		Calendar cal = Calendar.getInstance();
//		cal.setTimeInMillis(dateUtc);
//		cal.set(Calendar.DAY_OF_WEEK, 7); 
//		int year = cal.get(Calendar.YEAR);
//		int week = cal.get(Calendar.WEEK_OF_YEAR);
//		StringBuffer sb = new StringBuffer();
//		if(week < 10){
//			sb.append(year).append("-0").append(week);
//		}else{
//			sb.append(year).append("-").append(week);
//		}
//		return sb.toString();
//	}
	/**
	 * 获取偏移日期字符串
	 * @param i 偏移天数
	 * @param dateStr  日期字符串
	 * @return TODO
	 */
	private static String getDayDateStr(int i, long dateUtc) {
		SimpleDateFormat alarmLevelFormat = new SimpleDateFormat("yyyy-MM-dd");
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(dateUtc); 
		cal.add(Calendar.DAY_OF_MONTH, -i);
		return alarmLevelFormat.format(cal.getTime());
	}
	/**
	 * 获取周趋势偏移日期字符串
	 * @param i 偏移天数
	 * @param dateStr 日期字符串
	 * @return TODO
	 */
	private static String getWeekDateStr(int i, long dateStr) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(dateStr);
		cal.add(Calendar.WEEK_OF_YEAR, -i);/**cal.add(Calendar.DAY_OF_YEAR, -(i*7));**/
		cal.set(Calendar.DAY_OF_WEEK, 7); 
		int year = cal.get(Calendar.YEAR);
		int week = cal.get(Calendar.WEEK_OF_YEAR);
		StringBuffer sb = new StringBuffer();
		if(week < 10){
			sb.append(year).append("-0").append(week);
		}else{
			sb.append(year).append("-").append(week);
		}
		return sb.toString();
	}
	/**
	 * 获取周趋势偏移日期字符串
	 * @param i 偏移天数
	 * @param dateStr 日期字符串
	 * @return TODO
	 */
	private static String getMonthDateStr(int i, long dateUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(dateUtc);
		cal.add(Calendar.MONTH, -i);
		int year = cal.get(Calendar.YEAR);
		int month = cal.get(Calendar.MONTH) + 1;
		StringBuffer sb = new StringBuffer();
		if(month < 10){
			sb.append(year).append("-0").append(month);
		}else{
			sb.append(year).append("-").append(month);
		}
		return sb.toString();
	}

	/**
	 * 存储轨迹当天最大里程数据
	 * @param mileageMap
	 * TODO
	 */
	public void saveTrackMileage(Map<String, Integer> mileageMap) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_statisticsAlarmTypeDay);
			conn.setAutoCommit(false);
			int count = 0;
			for(Map.Entry<String, Integer> map : mileageMap.entrySet()){
				ps.setString(1, map.getKey());
				ps.setInt(2, map.getValue());
				ps.addBatch(); 
				count++;
			} 
			ps.executeBatch();
			long end = System.currentTimeMillis();
			log.info("存储[{}]条轨迹当天最大里程数据完成 - 耗时[{}]", count, Utils.getTimeDesc(end-start));
		} catch (Exception ex) {
			log.error("存储轨迹当天最大里程数据异常:" + ex.getMessage(), ex);
		} finally {
			close(null, ps, conn);
		}
	}
	/**
	 * 获取日字符串
	 * @param dayNum
	 * @return
	 */
	private static String getDayStr(int dayNum) {
		StringBuffer sb = new StringBuffer();
		sb.append(dayNum);
		sb.insert(4, '-');
		sb.insert(7, '-');
		return sb.toString();
	}
	/**
	 * 获取周或者月字符串
	 * @param Num
	 * @return
	 */
	private static String getWeekOrMonthStr(int Num) {
		StringBuffer sb = new StringBuffer();
		sb.append(Num);
		sb.insert(4, '-');
		return sb.toString();
	}
	/**
	 * 缓存驾驶员上下线记录
	 * @param parma
	 * TODO
	 */
	public static void queryDriverOnoffline(StatisticsParma parma) {
		ResultSet rs = null;
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			long start = System.currentTimeMillis();
			long startUtc = parma.getStartUtc();
			long endUtc = parma.getEndUtc();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_queryDriverOnoffline);
			ps.setLong(1, parma.getStartUtc());
			ps.setLong(2, parma.getEndUtc());
			rs = ps.executeQuery();
			int count = 0;
			Map<String, List<DriverOnoffline>> map = new ConcurrentHashMap<String, List<DriverOnoffline>>();
			while(rs.next()){
				count++;
				try {
					String vid = rs.getString("VID");
					String driverId = rs.getString("STAFF_ID");
					long onlineUtc = rs.getLong("ON_LINE_TIME");
					long offlineUtc = rs.getLong("OFF_LINE_TIME");
					DriverOnoffline driverOnoffline = new DriverOnoffline(driverId,onlineUtc,offlineUtc);
					if(map.containsKey(vid)){
						map.get(vid).add(driverOnoffline);
					}else{
						List<DriverOnoffline> list = new ArrayList<DriverOnoffline>();
						list.add(driverOnoffline);
						map.put(vid, list);
					}
				} catch (Exception e) {
					log.error("缓存驾驶员上下线记录异常:" + e.getMessage(), e);
				} 
			} 
			if(map.size() > 0){
				Map<String, List<DriverOnoffline>> driverOnofflineMap = new ConcurrentHashMap<String, List<DriverOnoffline>>();
				for(Map.Entry<String, List<DriverOnoffline>> m : map.entrySet()){
					driverOnofflineMap.put(m.getKey(), processDriverOfflineTime(m.getValue(), startUtc, endUtc));
				}
				Cache.putAllDriverOnoffline(driverOnofflineMap);
			}
			long end = System.currentTimeMillis();
			log.info("日统计[{}]缓存驾驶员上下线记录 - [{}]条完成 - 耗时[{}], 时间范围[{}]~[{}]",parma.getStatisticsDayStr(), count, Utils.getTimeDesc(end-start), parma.getStartUtc(), parma.getEndUtc());
		} catch (Exception ex) {
			log.error("current[" + parma.getCurrentUtc() + "]start[" + parma.getStartUtc() + "] end[" + parma.getEndUtc() + "]统计单车超速异常:" + ex.getMessage(), ex);
		} finally {
			close(rs, ps, conn);
		}
	}
	/**
	 * 处理离线时间
	 * @param endUtc 
	 * @param startUtc 
	 * @param value
	 * @return
	 */
	private static List<DriverOnoffline> processDriverOfflineTime(List<DriverOnoffline> onofflineList, long startUtc, long endUtc) {
			Collections.sort(onofflineList); // 排序 - 倒序排列  最后一条在最前面
			long lastUtc = 0;
			int index = 1;
			int size = onofflineList.size();
			for(DriverOnoffline onoffline : onofflineList){
				lastUtc = onoffline.getOnlineUtc();
				if(index == 1){ // 第一进入 处理最后一条上下线记录
					if(onoffline.getOnlineUtc() > onoffline.getOfflineUtc()){
						onoffline.setOfflineUtc(endUtc); // 使用当天最后时间
					}
					lastUtc = onoffline.getOnlineUtc();
				}else if(index == size ){ // 最后一条  处理第一条上下线记录
					if(onoffline.getOnlineUtc() > onoffline.getOfflineUtc()){
						onoffline.setOfflineUtc(startUtc);  // 使用当天开始时间
					}
				} else{
					if(onoffline.getOnlineUtc() > onoffline.getOfflineUtc()){
						onoffline.setOfflineUtc(lastUtc); // 使用上条报警开始时间作为结束
					}
					lastUtc = onoffline.getOnlineUtc();
				}
				index++;
			}
		return onofflineList;
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
			log.error("关闭连接异常:" + e.getMessage(), e);
		}
	}

	public static void main(String[] args) {
		// AlarmTrend alarmTrend = new AlarmTrend("2014-10-15");
		// alarmTrend.setGeneral(1);
		// alarmTrend.setSerious(2);
		// alarmTrend.setUrgent(3);
		// System.out.println(getTrendValue(alarmTrend));
		String str = "D:2014-12-04,G:0,S:0,F:0,U:0},{D:2014-12-05,G:0,S:0,F:0,U:0},{D:2014-12-06,G:0,S:0,F:0,U:0},{D:2014-12-07,G:0,S:0,F:0,U:0},{D:2014-12-08,G:0,S:0,F:0,U:0},{D:2014-12-09,G:0,S:0,F:0,U:0},{D:2014-11-10,G:0,S:0,F:0,U:0";
		String[] dayTrend = str.split("\\},\\{");
//		for (String s : a) {
//			System.out.println(s.substring(2, 12));
//		}
		List<AlarmTrendBean> trendList = new ArrayList<AlarmTrendBean>();
		for (String dayStr : dayTrend) {
			String dateStr = dayStr.substring(2, 12).replaceAll("-", "");
			int dateInt = Integer.parseInt(dateStr);
			AlarmTrendBean trendBean = new AlarmTrendBean();
			trendBean.setDate(dateInt);
			trendBean.setTrend("{" + dayStr + "}");
			trendList.add(trendBean);
		}
		System.out.println(getTrendListStr(trendList));
		System.out.println(getWeekOrMonthStr(201401));
		//---------------------- 获取日趋势--------------------------------
		Calendar cd = Calendar.getInstance();
		cd.set(Calendar.YEAR, 2014);
		cd.set(Calendar.MONTH, 0);
		cd.set(Calendar.DAY_OF_MONTH, 1);
		AlarmTrend dayAT = new AlarmTrend(cd.getTimeInMillis());
		dayAT.setDateStr("2014-01-01");
		dayAT.setGeneral(0);
		dayAT.setSerious(1);
		dayAT.setUrgent(2);
		System.out.println("获取日趋势:" + getAlarmDayTrendStr(dayAT));
		//---------------------- 获取周趋势--------------------------------
		Calendar cw = Calendar.getInstance();
		cw.set(Calendar.YEAR, 2014);
		cw.set(Calendar.WEEK_OF_YEAR, 1);
		AlarmTrend weekAT = new AlarmTrend(cw.getTimeInMillis());
		weekAT.setDateStr("2014-01");
		weekAT.setGeneral(0);
		weekAT.setSerious(1);
		weekAT.setUrgent(2);
		System.out.println("获取周趋势:" + getAlarmWeekTrendStr(weekAT));
		//---------------------- 获取月份趋势--------------------------------
		Calendar cm = Calendar.getInstance();
		cm.set(Calendar.YEAR, 2014);
		cm.set(Calendar.MONTH, 0);
		cm.set(Calendar.DAY_OF_MONTH, 14);
		AlarmTrend at = new AlarmTrend(cm.getTimeInMillis());
		at.setDateStr("2014-01");
		at.setGeneral(0);
		at.setSerious(1);
		at.setUrgent(2);
		System.out.println("获取月份趋势:" + getAlarmMonthTrendStr(at));
//		for (int i = 1; i < 7; i++) {
//			System.out.println(getWeekDateStr(7 - i, c.getTimeInMillis()));
//			System.out.println("-----------");
//		}
//		for(int i = 0 ; i < 17; i++){
//			Calendar c = Calendar.getInstance();
//			c.add(Calendar.DAY_OF_MONTH, +i);
//			int wy = c.get(Calendar.WEEK_OF_YEAR);
//			int wm = c.get(Calendar.WEEK_OF_MONTH);
//			int dm = c.get(Calendar.DAY_OF_MONTH);
//			int dw = c.get(Calendar.DAY_OF_WEEK);
//			int dwm = c.get(Calendar.DAY_OF_WEEK_IN_MONTH);
//			int dy = c.get(Calendar.DAY_OF_YEAR);
//			
//			
//			System.out.println("今天是["+new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(c.getTime())+"]\r\nWEEK_OF_YEAR["+wy+
//					"]\r\nWEEK_OF_MONTH["+wm+
//					"]\r\nDAY_OF_MONTH["+dm+
//					"]\r\nDAY_OF_WEEK["+dw+
//					"]\r\nDAY_OF_WEEK_IN_MONTH["+dwm+
//					"]\r\nDAY_OF_YEAR["+dy+"]\r\n--------------------------");
//		}
	}
	
}
