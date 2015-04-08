/*****************************************
 * <ul>
 * <li>创  建  者：yinshuaiwei 		</li><br>
 * <li>工程名称： KCPTDataAnaly		</li><br>
 * <li>文件名称：com.ctfo.analy.util Cache.java	</li><br>
 * <li>时        间：2014-12-4  下午4:41:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.analy.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.analy.beans.Coordinates;


/*****************************************
 * <li>描        述：缓存		
 * 
 *****************************************/
public class Cache {
	/**
	 * 位置坐标缓存
	 */
	private static Map<String, Coordinates> trackCoordinatesMap = new ConcurrentHashMap<String, Coordinates>();

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
}
