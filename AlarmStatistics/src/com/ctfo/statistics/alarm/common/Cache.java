package com.ctfo.statistics.alarm.common;

import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.statistics.alarm.model.DriverOnoffline;
import com.ctfo.statistics.alarm.model.FatigueRules;
import com.ctfo.statistics.alarm.model.NightRules;
import com.ctfo.statistics.alarm.model.OverspeedRules;
import com.ctfo.statistics.alarm.model.VehicleInfo;

public class Cache {
	/** 超速告警规则		*/
	private static Map<String, List<OverspeedRules>> overspeedRulesMap = new ConcurrentHashMap<String, List<OverspeedRules>>();
	
	/** 疲劳驾驶告警规则		*/
	private static Map<String, List<FatigueRules>> fatigueRulesMap = new ConcurrentHashMap<String, List<FatigueRules>>();
	
	/** 夜间非法运营告警规则		*/
	private static Map<String, List<NightRules>> nightRulesMap = new ConcurrentHashMap<String, List<NightRules>>();
	
	/** 车辆详情		*/
	private static Map<String, VehicleInfo> vehicleInfoMap = new ConcurrentHashMap<String, VehicleInfo>();
	
	/** 告警父级类型		*/
	private static Map<String, String> alarmParentCodeMap = new ConcurrentHashMap<String, String>();
	
	/** 车辆限速阀值		*/
	private static Map<String, Integer> speedLimitSettingsMap = new ConcurrentHashMap<String, Integer>();
	
	/** 车队告警级别配置		*/
	private static Map<String, String> teamAlarmLevelConfMap = new ConcurrentHashMap<String, String>();
	
	/** 驾驶员上下线记录（vid list上下线记录）		*/
	private static Map<String, List<DriverOnoffline>> driverOnofflineMap = new ConcurrentHashMap<String, List<DriverOnoffline>>();
	
	
	/**
	 * 清空所有缓存
	 */
	public static void clearAll() {
		overspeedRulesMap.clear(); 	// 超速规则
		fatigueRulesMap.clear();	// 疲劳驾驶规则
		nightRulesMap.clear();		// 夜间非法运营规则
		vehicleInfoMap.clear();		// 车辆信息缓存
		alarmParentCodeMap.clear();	// 组织父级编号缓存
		speedLimitSettingsMap.clear(); // 车辆限速阀值缓存
		teamAlarmLevelConfMap.clear();  // 车队告警级别配置
		driverOnofflineMap.clear(); // 驾驶员上下线记录
	}
	
	/**	获取车辆详情	*/
	public static VehicleInfo getVehicleInfo(String vid) {
		return vehicleInfoMap.get(vid);
	}
	
	/**
	 * 缓存车辆详情
	 * @param map
	 */
	public static void putVehicleInfo(Map<String, VehicleInfo> map){
		vehicleInfoMap.putAll(map);
	}
	/**
	 * 缓存车辆详情
	 * @param map
	 */
	public static void putVehicleInfo(String vid, VehicleInfo v){
		vehicleInfoMap.put(vid, v);
	}
	
	/**
	 * 获取超速告警规则
	 * @param vid
	 * @return
	 */
	public static List<OverspeedRules> getOverspeedRules(String vid) {
		return overspeedRulesMap.get(vid);
	}
	
	/**
	 * 缓存超速告警规则
	 * @param map
	 */
	public static void putAllOverspeedRules(Map<String, List<OverspeedRules>> map){
		overspeedRulesMap.putAll(map);
	}

	/**
	 * 获取疲劳告警规则
	 * @param vid
	 * @return
	 */
	public static List<FatigueRules> getFatigueRules(String vid) {
		return fatigueRulesMap.get(vid);
	}
	
	/**
	 * 缓存疲劳告警规则
	 * @param map
	 */
	public static void putAllFatigueRules(Map<String, List<FatigueRules>> map){
		fatigueRulesMap.putAll(map);
	}
	
	/**
	 * 缓存报警父级编码
	 * @param code
	 * @param parentCode
	 */
	public static void putAlarmParentCode(String code, String parentCode) {
		alarmParentCodeMap.put(code, parentCode);
	}
	
	/**
	 * 获取报警父级编码
	 * @param code
	 * @return
	 */
	public static String getAlarmParentCode(String code){
		return alarmParentCodeMap.get(code);
	}

	/**
	 * 缓存车辆限速设置
	 * @param code
	 * @param parentCode
	 */
	public static void putSpeedLimitSettings(String vid, Integer limit) {
		speedLimitSettingsMap.put(vid, limit);
	}
	
	/**
	 * 获取车辆限速设置
	 * @param code
	 * @return
	 */
	public static Integer getSpeedLimitSettings(String vid){
		return speedLimitSettingsMap.get(vid);
	}
	/**
	 * 获取夜间非法运营规则列表
	 * @param vid
	 * @return
	 */
	public static List<NightRules> getNightRules(String vid) {
		return nightRulesMap.get(vid);
	}
	/**
	 * 缓存夜间非法运营规则集合
	 * @param m
	 */
	public static void putAllNightRules(Map<String, List<NightRules>> m){
		nightRulesMap.putAll(m); 
	}
	/**
	 * 获取车队告警级别
	 * @param teamId_alarmCode 车队编号_报警编号
	 * @return
	 */
	public static String getTeamAlarmLevel(String teamId_alarmCode) {
		return teamAlarmLevelConfMap.get(teamId_alarmCode);
	}
	/**
	 * 缓存车队告警级别配置集合
	 * @param m
	 */
	public static void putAllTeamAlarmLevelConf(Map<String, String> m){
		teamAlarmLevelConfMap.putAll(m); 
	}
//	/**
//	 * 获取车队告警级别配置所有KEY
//	 * @return
//	 */
//	public static Set<String> getTeamAlarmLevelConfKeys() {
//		return teamAlarmLevelConfMap.keySet();
//	}
//	/**
//	 * 删除车队告警级别配置
//	 * @param oldKeys
//	 */
//	public static void removeTeamAlarmLevelConf(Set<String> oldKeys) {
//		for(String key : oldKeys){
//			teamAlarmLevelConfMap.remove(key);
//		}
//	}
	/**
	 * 缓存车辆的驾驶员上下线记录
	 * @param m
	 */
	public static void putAllDriverOnoffline(Map<String, List<DriverOnoffline>> m){
		driverOnofflineMap.putAll(m);
	}
	/**
	 * 获取驾驶员编号
	 * @param vid	车辆编号
	 * @param alarmStartUtc	告警开始时间
	 * @return
	 */
	public static String getDriverIdByAlarmStartUtc(String vid, long alarmStartUtc){
		if(driverOnofflineMap.containsKey(vid)){
			List<DriverOnoffline> list = driverOnofflineMap.get(vid);
			for(DriverOnoffline dof : list){
				if(alarmStartUtc >= dof.getOnlineUtc() && alarmStartUtc <= dof.getOfflineUtc()){
					return dof.getDriverId();
				}
			}
		}
		return null;
	}
}
