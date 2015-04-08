/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.util Cache.java	</li><br>
 * <li>时        间：2013-9-9  下午4:41:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.util;

import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.commandservice.model.Custom;
import com.ctfo.commandservice.model.DriftLatLon;
import com.ctfo.commandservice.model.ServiceUnit;


/*****************************************
 * <li>描        述：缓存		
 * 
 *****************************************/
public class Cache {
	/** 注册车辆缓存表   */
	public static Map<String, ServiceUnit> registerVehicleMap = new ConcurrentHashMap<String, ServiceUnit>(); 
	/**	偏移量	*/
	private static Map<Long, DriftLatLon> driftLatLonMap = new ConcurrentHashMap<Long, DriftLatLon>();
	/** 车辆对应报警设置企业列表(key:vid,value:ent_id)  */
	public static Map<Long,Long> vidEntMap = new ConcurrentHashMap<Long,Long>();
	/** 企业对应报警设置告警列表(key:vid,value:报警code列表)  */
	public static Map<Long,List<String>> entAlarmMap = new ConcurrentHashMap<Long,List<String>>();
	/**	组织父级缓存		*/
	private static Map<String, String> orgParentCache = new ConcurrentHashMap<String, String>();
	/**	自定义指令级缓存		*/
	private static Map<String, Custom> customCache = new ConcurrentHashMap<String, Custom>();
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
	/**
	 * 清空车辆缓存后加入新缓存
	 */
	public static void clearAndPutAllVehicleMap(Map<String, ServiceUnit> m){
		registerVehicleMap.clear(); 
		registerVehicleMap.putAll(m); 
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

	public static void putDriftLatLon(Long vid, DriftLatLon driftLatLon) {
		driftLatLonMap.put(vid, driftLatLon);
	}
	/*****************************************
	 * <li>描        述：获得偏移数据 		</li><br>
	 * <li>时        间：2013-9-10  下午3:07:30	</li><br>
	 * <li>参数： @param parseLong
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static DriftLatLon getDriftLatLon(Long vid) {
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
	
	/*****************************************
	 * <li>描        述：缓存所有组织父级表		</li><br>
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
	/**
	 * 清空父级组织编号
	 */
	public static void clearOrgParent(){
		orgParentCache.clear(); 
	}
	/**
	 * 判断key是否存在
	 * @param key
	 * @return
	 */
	public static Custom getCustom(String key){
		boolean contains = customCache.containsKey(key);
		if(contains){
			Custom c = customCache.get(key);
			long utc = c.getOutTime();
			long cur = System.currentTimeMillis();
			if(cur > utc){
				customCache.remove(key);
				return null;
			} 
			return c;
		}
		return null;
	}
	/**
	 * 缓存自定义指令下发时间
	 * @param key
	 * @param value
	 */
	public static void putCustom(String key, Custom custom){
		customCache.put(key, custom);
	}
	/**
	 * 清理自定义指令缓存
	 */
	public static void clearCustomCommandCache() {
		for(Map.Entry<String, Custom> map : customCache.entrySet()){
			Custom c = map.getValue();
			long utc = c.getOutTime();
			long cur = System.currentTimeMillis();
			if(cur > utc){
				customCache.remove(map.getKey());
			} 
		}
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
