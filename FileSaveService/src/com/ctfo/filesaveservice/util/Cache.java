/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.util Cache.java	</li><br>
 * <li>时        间：2013-9-9  下午4:41:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.util;

import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.filesaveservice.model.Coordinates;
import com.ctfo.filesaveservice.model.DriftLatLon;
import com.ctfo.filesaveservice.model.ServiceUnit;

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
	/**
	 * 位置坐标缓存
	 */
	private static Map<String, Coordinates> trackCoordinatesMap = new ConcurrentHashMap<String, Coordinates>();
	/**	油量数据时间缓存		*/
	private static Map<String, Long> oilupdateTimeCache = new ConcurrentHashMap<String, Long>();
	
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
	/**
	 * 将映射表中数据加入缓存
	 * @param m
	 */
	public static void putAllVehicleMap(Map<String, ServiceUnit> m){
		registerVehicleMap.putAll(m); 
	}
	/**
	 * 清空车辆缓存后加入新缓存
	 */
	public static void clearAndPutAllVehicleMap(Map<String, ServiceUnit> m){
		registerVehicleMap.clear(); 
		registerVehicleMap.putAll(m); 
	}
	/**
	 * 获取车辆缓存主键集合
	 * @return
	 */
	public static Set<String> getVehicleKeySet(){
		return registerVehicleMap.keySet();
	}
	/**
	 * 删除多个主键数据
	 * @param keys
	 */
	public static void removeKeys(Set<String> keys){
		for(String key : keys){
			registerVehicleMap.remove(key);
		}
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

	public static void removeDriftLatLon(Long vid) {
		if (driftLatLonMap.containsKey(vid)) {
			driftLatLonMap.remove(vid);
		}
	}
	/**
	 * 缓存轨迹经纬度坐标
	 * @param vid
	 * @param coordinates
	 */
	public static void setTrackCoordinates(String vid, Coordinates coordinates){
		trackCoordinatesMap.put(vid, coordinates);
	}
	/**
	 * 根据车辆编号获取轨迹经纬度坐标
	 * @param vid
	 * @return
	 */
	public static Coordinates getTrackCoordinates(String vid){
		return trackCoordinatesMap.get(vid); 
	}
	/**
	 * 根据车辆编号生产轨迹经纬度缓存
	 * @param vid
	 */
	public static void removeTrackCoordinates(String vid){
		trackCoordinatesMap.remove(vid);
	}
	/**
	 * 获取轨迹坐标缓存数
	 * @return
	 */
	public static int getTrackCoordinatesSize(){
		return trackCoordinatesMap.size();
	}
	/**
	 * 设置油量上报时间
	 * @param vid
	 * @param updateTime
	 */
	public static void putOilUpdateTime(String vid, Long updateTime){
		oilupdateTimeCache.put(vid, updateTime);
	}
	/**
	 * 获取油量上报时间
	 * @param vid
	 */
	public static Long getOilUpdateTime(String vid){
		return oilupdateTimeCache.get(vid);
	}
	/**
	 * 删除油量上报时间
	 * @param vid
	 */
	public static void delOilUpdateTime(String vid){
		oilupdateTimeCache.remove(vid);
	}

}
