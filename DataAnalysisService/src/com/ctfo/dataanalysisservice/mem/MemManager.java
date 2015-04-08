package com.ctfo.dataanalysisservice.mem;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.dataanalysisservice.beans.AlarmVehicleBean;
import com.ctfo.dataanalysisservice.beans.AreaDataObject;
import com.ctfo.dataanalysisservice.beans.KeyPointDataObject;
import com.ctfo.dataanalysisservice.beans.LineAlarmBean;
import com.ctfo.dataanalysisservice.beans.SectionsDataObject;

/**
 * 缓存数据
 * 
 * @author yangyi
 * 
 */
public class MemManager {

	// 车辆报警信息缓存
	private static Map<String, AlarmVehicleBean> alarmVehicleMap = new ConcurrentHashMap<String, AlarmVehicleBean>();

	// 车辆关键点信息缓存
	public static Map<String, List<KeyPointDataObject>> keyPointMap = new ConcurrentHashMap<String, List<KeyPointDataObject>>();

	// 车辆关键点信息缓存（时间判断）
	private static Map<String, List<Map.Entry<String, List<String>>>> stationMap = new ConcurrentHashMap<String, List<Map.Entry<String, List<String>>>>();
	// 车辆关键点信息缓存（时间）
	private static Map<String, List<String>> stationTimeMap = new ConcurrentHashMap<String, List<String>>();

	// 车辆区域信息缓存
	private static Map<String, List<AreaDataObject>> areaMap = new ConcurrentHashMap<String, List<AreaDataObject>>();

	// 车辆线路信息缓存
	private static Map<String, List<SectionsDataObject>> lineMap = new ConcurrentHashMap<String, List<SectionsDataObject>>();

	// 车辆线路信息缓存(上个点信息)
	private static Map<String, LineAlarmBean> tempLineMap = new ConcurrentHashMap<String, LineAlarmBean>();

	/**
	 * 根据key获取车辆报警信息缓存对象
	 * 
	 * @param key
	 * @return
	 */
	public static AlarmVehicleBean getAlarmVehicleMap(String key) {
		return alarmVehicleMap.get(key);
	}

	/**
	 * 设置车辆报警信息缓存
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setAlarmVehicleMap(String key,
			AlarmVehicleBean alarmVehicleBean) {
		alarmVehicleMap.put(key, alarmVehicleBean);
	}

	/**
	 * 根据key获取车辆关键点信息缓存对象
	 * 
	 * @param key
	 * @return
	 */
	public static List<KeyPointDataObject> getKeyPointMap(String key) {
		
		if(keyPointMap.containsKey(key)){
			List<KeyPointDataObject> list = new ArrayList<KeyPointDataObject>();
			list.addAll(keyPointMap.get(key));
			return list;
		}
		return null;
	}

	/**
	 * 设置车辆关键点信息缓存
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setKeyPointMap(String key, List<KeyPointDataObject> list) {
		keyPointMap.put(key, list);
	}
	
	/**
	 * 根据key获取车辆关键点信息缓存对象（时间判断）
	 * 
	 * @param key
	 * @return
	 */
	public static List<Map.Entry<String, List<String>>> getStationMap(String key) {
		return stationMap.get(key);
	}
	
	public static Map<String, List<KeyPointDataObject>>  getKeyPointDataObject(){
		return keyPointMap;
	}

	/**
	 * 设置车辆关键点信息缓存（时间判断）
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setStationMap(String key, List<Map.Entry<String, List<String>>> map) {
		stationMap.put(key, map);
	}

	/**
	 * 根据key获取车辆关键点信息缓存对象（时间）
	 * 
	 * @param key
	 * @return
	 */
	public static List<String> getStationTimeMap(String key) {
		return stationTimeMap.get(key);
	}

	/**
	 * 设置车辆关键点信息缓存（时间）
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setStationTimeMap(String key, List<String> list) {
		stationTimeMap.put(key, list);
	}

	/**
	 * 根据key获取车辆线路信息缓存
	 * 
	 * @param key
	 * @return
	 */
	public static List<SectionsDataObject> getLineMap(String key) {
		return lineMap.get(key);
	}

	/**
	 * 设置车辆线路信息缓存
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setLineMap(String key, List<SectionsDataObject> list) {
		lineMap.put(key, list);
	}

	/**
	 * 根据key获取车辆线路信息缓存(上个点信息)
	 * 
	 * @param key
	 * @return
	 */
	public static LineAlarmBean getTempLineMap(String key) {
		return tempLineMap.get(key);
	}

	/**
	 * 设置车辆线路信息缓存(上个点信息)
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setTempLineMap(String key, LineAlarmBean lineAlarmBean) {
		tempLineMap.put(key, lineAlarmBean);
	}

	/**
	 * 根据key获取车辆区域信息缓存
	 * 
	 * @param key
	 * @return
	 */
	public static List<AreaDataObject> getAreaMap(String key) {
 
		return areaMap.get(key);
	}

	/**
	 * 设置车辆区域信息缓存
	 * 
	 * @param key
	 * @param alarmVehicleBean
	 */
	public static void setAreaMap(String key, List<AreaDataObject> list) {
 
		areaMap.put(key, list);
	}
	/**
	 * 获取MEM客户端
	 * 
	 * @return MEM客户端
	 * 
	 *         2012-2-29 杨毅注释，去掉memcache
	 * 
	 *         public static MemcachedClient getMemcachedClient() {
	 *         MemcachedClientBuilder bulider = new XMemcachedClientBuilder(
	 *         AddrUtil.getAddresses(DataAnalysisServiceMain.config
	 *         .getStringValue("MemCached|MEM_SERVER")));
	 *         bulider.setConnectionPoolSize(2); try { return bulider.build(); }
	 *         catch (IOException e) { e.printStackTrace(); } return null; }
	 */
}
