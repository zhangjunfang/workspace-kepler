/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.util Cache.java	</li><br>
 * <li>时        间：2013-9-9  下午4:41:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.util;

import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.trackservice.model.DriftLatLon;
import com.ctfo.trackservice.model.FiveMinDataBean;
import com.ctfo.trackservice.model.LastTrack;
import com.ctfo.trackservice.model.ServiceUnit;
import com.ctfo.trackservice.model.StatusCode;


/*****************************************
 * <li>描        述：缓存		
 * 
 *****************************************/
public class Cache {
	/** 注册车辆缓存表   */
	public static Map<String, ServiceUnit> registerVehicleMap = new ConcurrentHashMap<String, ServiceUnit>(); 
	/**	偏移量	*/
	private static Map<String, DriftLatLon> driftLatLonMap = new ConcurrentHashMap<String, DriftLatLon>();
	/** 车辆对应报警设置企业列表(key:vid,value:ent_id)  */
	public static Map<String, String> vidEntMap = new ConcurrentHashMap<String, String>();
	/** 企业对应报警设置告警列表(key:ent_id,value:报警code)  */
	public static Map<String,String> entAlarmMap = new ConcurrentHashMap<String,String>();
	// 缓存五分钟数据
	private static Map<String,FiveMinDataBean> fiveMap = new ConcurrentHashMap<String,FiveMinDataBean>();
	// 存储车辆状态对应参考值
	private static Map<String, StatusCode> statusMap = new ConcurrentHashMap<String, StatusCode>();
	/**	存储redis缓存数据		*/
	public static Map<String, String> redisStorageCache = new ConcurrentHashMap<String, String>();
	
	/** 车辆上下线状态缓存   */
	private static Map<String, String> vehicleOnOfflineMap = new ConcurrentHashMap<String, String>();
	/**	最后位置缓存		*/
	public static Map<String, LastTrack> lastTrackCache = new ConcurrentHashMap<String, LastTrack>();
	/**	组织父级缓存		*/
	private static Map<String, String> orgParentCache = new ConcurrentHashMap<String, String>();
	/*****************************************
	 * <li>描        述：获取车辆对象 		</li><br>
	 * <li>时        间：2013-9-9  下午6:05:38	</li><br>
	 * <li>参数： @param macId
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static ServiceUnit getVehicleMapValue(String macId) {
		return registerVehicleMap.get(macId);
	}
	/*****************************************
	 * <li>描        述：存储车辆对象 		</li><br>
	 * <li>时        间：2013-9-10  上午10:48:53	</li><br>
	 * <li>参数： @param macId
	 * <li>参数： @param serviceUnit			</li><br>
	 * 
	 *****************************************/
	public static void setVehicleMapValue(String macId, ServiceUnit serviceUnit) {
		registerVehicleMap.put(macId, serviceUnit);
	}
	/**
	 * 缓存所有车辆信息
	 * @param m
	 */
	public static void putAllVehicleInfo(Map<String, ServiceUnit> m) {
		registerVehicleMap.putAll(m);
	}
	/**
	 * 获取车辆信息主键集合
	 * @return
	 */
	public static Set<String> getVehicleInfoKey() {
		return registerVehicleMap.keySet();
	}
	/**
	 * 清除过期车辆缓存
	 * @param oldKeys
	 */
	public static void removeVehicleInfoKeys(Set<String> oldKeys) {
		for(String key : oldKeys){
			registerVehicleMap.remove(key);
		}
	}
	public static void putDriftLatLon(String vid, DriftLatLon driftLatLon) {
		driftLatLonMap.put(vid, driftLatLon);
	}
	/*****************************************
	 * <li>描        述：获得偏移数据 		</li><br>
	 * <li>时        间：2013-9-10  下午3:07:30	</li><br>
	 * <li>参数： @param parseLong
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static DriftLatLon getDriftLatLon(String vid) {
		DriftLatLon driftLatLon = null;
		if (driftLatLonMap.containsKey(vid)) {
			driftLatLon = driftLatLonMap.get(vid);
		} else {
			driftLatLon = new DriftLatLon();
			driftLatLonMap.put(vid, driftLatLon);
		}
		return driftLatLon;
	}

	public static void removeDriftLatLon(String vid) {
		if (driftLatLonMap.containsKey(vid)) {
			driftLatLonMap.remove(vid);
		}
	}
	
	// 存储大气压力缓存状态
	public static void addFiveAirPressureStatus(String vid,int status){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			fiveMinData.setAirPressureStatus(status);
		}else{
			fiveMinData = new FiveMinDataBean();
			fiveMinData.setAirPressureStatus(status);
			fiveMap.put(vid, fiveMinData);
		}
	}
	
	// 获取大气压力缓存状态,无任何缓存，返回无效状态2
	public static int getFiveAirPressureStatus(String vid){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			return fiveMinData.getAirPressureStatus();
		}else{
			return 2;
		}
	}
	
	// 存储冷却液温度缓存状态
	public static void addFiveEWaterStatus(String vid,int status){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			fiveMinData.seteWaterStatus(status);
		}else{
			fiveMinData = new FiveMinDataBean();
			fiveMinData.seteWaterStatus(status);
			fiveMap.put(vid, fiveMinData);
		}
	}
	
	// 获取冷却液温度缓存状态,无任何缓存，返回无效状态2
	public static int getFiveEWaterStatus(String vid){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			return fiveMinData.geteWaterStatus();
		}else{
			return 2;
		}
	}
	
	// 存储蓄电池电压缓存状态
	public static void addFiveExtVoltageStatus(String vid,int status){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			fiveMinData.setExtVoltageStatus(status);
		}else{
			fiveMinData = new FiveMinDataBean();
			fiveMinData.setExtVoltageStatus(status);
			fiveMap.put(vid, fiveMinData);
		}
	}
	// 获取蓄电池电压缓存状态 ,无任何缓存，返回无效状态2
	public static int getFiveExtVoltageStatus(String vid){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			return fiveMinData.getExtVoltageStatus();
		}else{
			return 2;
		}
	}
	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setStatusMapValue(String key, StatusCode statusCode) {
		statusMap.put(key, statusCode);
	}

	/**
	 * 获取接收队列大小
	 */
	public static long getStatusMapSize() {
		return statusMap.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static StatusCode getStatusMapValue(String key) {
		return statusMap.get(key);
	}
	/*****************************************
	 * <li>描        述：生成缓存数组 		</li><br>
	 * <li>时        间：2013-9-18  下午4:50:50	</li><br>
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static synchronized String[] getRedisVehicleArray() {
		String[] vehicleArray = new String[Cache.redisStorageCache.size()];
		int i = 0;
		for(Map.Entry<String, String> m : Cache.redisStorageCache.entrySet()){ 
			vehicleArray[i] = m.getKey();
			vehicleArray[i+1] = m.getValue();
			i = i+2;
		}
		Cache.redisStorageCache.clear();
		return vehicleArray;
	}
	/*****************************************
	 * <li>描        述：缓存redis车辆信息 		</li><br>
	 * <li>时        间：2013-9-18  下午5:15:09	</li><br>
	 * <li>参数： @param vid
	 * <li>参数： @param vehicleValue			</li><br>
	 * 
	 *****************************************/
	public static void cacheRedisVechile(String vid, String vehicleValue) {
		redisStorageCache.put(vid, vehicleValue);
	}
	/*****************************************
	 * <li>描        述：缓存最后位置 		</li><br>
	 * <li>时        间：2013-10-19  下午7:21:57	</li><br>
	 * <li>参数： @param valueOf
	 * <li>参数： @param lastTrack			</li><br>
	 * 
	 *****************************************/
	public static void setLastTrack(String vid, LastTrack lastTrack) {
		lastTrackCache.put(vid, lastTrack);
	}
	/*****************************************
	 * <li>描        述：获得最后位置 		</li><br>
	 * <li>时        间：2013-10-19  下午7:24:23	</li><br>
	 * <li>参数： @param vid
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static LastTrack getLastTrack(String vid) {
		return lastTrackCache.get(vid);
	}
	/*****************************************
	 * <li>描        述：获得上下线状态 		</li><br>
	 * <li>时        间：2013-9-23  下午7:33:38	</li><br>
	 * <li>参数： @param macid
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String getOnOfflineStatus(String macid) {
		return vehicleOnOfflineMap.get(macid);
	}
	/*****************************************
	 * <li>描        述：保存上下线状态 		</li><br>
	 * <li>时        间：2013-9-23  下午7:33:43	</li><br>
	 * <li>参数： @param macid
	 * <li>参数： @param value			</li><br>
	 * 
	 *****************************************/
	public static void setOnOfflineStatus(String macid, String value) {
		vehicleOnOfflineMap.put(macid, value);
	}
	/*****************************************
	 * <li>描        述：缓存所有组织父级表 		</li><br>
	 * <li>时        间：2013-10-28  下午7:36:57	</li><br>
	 * <li>参数： @param orgParent			</li><br>
	 * 
	 *****************************************/
	public static void putAllOrgParent(Map<String, String> orgParent) {
		orgParentCache.putAll(orgParent);
	}
	/*****************************************
	 * <li>描        述：根据车队编号获取父级组织编号 		</li><br>
	 * <li>时        间：2013-10-28  下午7:39:41	</li><br>
	 * <li>参数： @param motorcade
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String getOrgParent(String motorcade){
		return orgParentCache.get(motorcade);
	}
	

}
