package com.ctfo.trackservice.common;

import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.trackservice.model.LineList;
import com.ctfo.trackservice.model.Station;
import com.ctfo.trackservice.model.StationNo;

public class StationCache {
	/**	站点缓存	*/
	private static Map<String, List<Station>> vehicleStatiionMap = new ConcurrentHashMap<String, List<Station>>();
	/**	最后站点缓存	*/
//	private static Map<String, String> lastStationMap = new ConcurrentHashMap<String, String>();
	/**	站点详细缓存	*/
	private static Map<String, StationNo> stations = new ConcurrentHashMap<String, StationNo>();
	
	/**	多条线路已过站点(上一站)缓存	*/
	private static Map<String, Set<String>> beforeStationsMap = new ConcurrentHashMap<String, Set<String>>();
	
	/**
	 * 获取当前缓存中所有车辆编号
	 * @return
	 */
	public static Set<String> getVehicleStationKeys() {
		return vehicleStatiionMap.keySet();
	}
	/**
	 * 删除失效站点
	 * @param oldSets
	 */
	public static void removeVehicleStationKeys(Set<String> oldSets) {
		for(String key : oldSets){
			vehicleStatiionMap.remove(key);
		}
	}
	/**
	 * 缓存站点
	 * @param m
	 */
	public static void putVehicleStation(Map<String, List<Station>> m) {
		vehicleStatiionMap.putAll(m);
	}
	/**
	 * 获取车辆关联的站点列表
	 * @param vid
	 * @return
	 */
	public static List<Station> getStationList(String vid){
		return vehicleStatiionMap.get(vid);
	}
	/**
	 * 车辆是否绑定站点
	 * @param vid
	 * @return
	 */
	public static boolean vehicleBindStation(String vid){
		return vehicleStatiionMap.containsKey(vid);
	}
//	/**
//	 * 获取上一站点
//	 * @param vid
//	 * @return
//	 */
//	public static String getLastStation(String vid) {
//		return lastStationMap.get(vid);
//	}
//	/**
//	 * 缓存最近站点
//	 * @param vid
//	 * @param station
//	 */
//	public static void setLastStation(String vid, String station) {
//		lastStationMap.put(vid, station);
//	}
//	/**
//	 * 删除最后一站记录
//	 * @param vid
//	 */
//	public static void removeLastStation(String vid) {
//		lastStationMap.remove(vid);
//	}
	
	/**
	 * 获取多条线路已过站点(上一站)
	 * @param vid
	 * @return
	 */
	public static Set<String> getBeforeStations(String vid) {
		return beforeStationsMap.get(vid);
	}
	/**
	 * 缓存多条线路已过站点(上一站)
	 * @param vid
	 * @param stations
	 */
	public static void setBeforeStations(String vid, Set<String> stations) {
		beforeStationsMap.put(vid, stations);
	}
	/**
	 * 删除多条线路已过站点(上一站)
	 * @param vid
	 */
	public static void removeBeforeStations(String vid) {
		beforeStationsMap.remove(vid);
	}
	
	/**
	 * 查询站点线路编号 - (根据上一站站点获取当前线路编号; 从启动开始，默认是起点始发)
	 * @param vid 车辆编号
	 * @param currentStationList  当前站站点编号
	 * @param lastStationId 	上一站站点编号
	 * @param lineId 			上一站线路编号
	 * @return
	 */
	public static Station queryStationLine(String vid, List<Station> currentStationList, String lastStationId, String lineId) {
		if(vid == null || currentStationList == null){ // 车辆编号、当前站点不能为空
			return null;
		}
		if(lastStationId != null && lineId != null){ // 上一站站点编号不为空 就查询线路编号
			StationNo stationNo = stations.get(vid);
			if(stationNo != null && stationNo.getList() != null && stationNo.getList().size() > 0){
				//首次匹配线路
				for(LineList ll : stationNo.getList()){
					for(Station currentStation : currentStationList) {
						if(ll.getLines().startsWith(currentStation.getStationId() + "_")){
							currentStation.setLineId(ll.getLineId());
							return currentStation;
						}
						if(ll.getLines().contains(lastStationId + "_" + currentStation.getStationId())){
							currentStation.setLineId(ll.getLineId());
							return currentStation;
						}
					}
				} 
				//重新定位线路、站点
				for(LineList ll : stationNo.getList()){
					if(!ll.getLineId().equals(lineId)) {
						for(Station currentStation : currentStationList) {
							if(ll.getLines().contains(currentStation.getStationId())) {
								currentStation.setLineId(ll.getLineId());
								return currentStation;
							}
						}
					}
				}
			} 
			return null;
		} else {
			StationNo stationNo = stations.get(vid);
			if(stationNo != null && stationNo.getList() != null && stationNo.getList().size() > 0){
				for(LineList ll : stationNo.getList()){
					for(Station currentStation : currentStationList) {
						if(ll.getLines().startsWith(currentStation.getStationId() + "_")){
							currentStation.setLineId(ll.getLineId());
							return currentStation;
						}else if(ll.getLines().contains(currentStation.getStationId() + "_")) {
							currentStation.setLineId(ll.getLineId());
							return currentStation;
						}
					}
				} 
			} 
			return null;
		}
	}

	/**
	 * 缓存站点集合
	 * @param m
	 */
	public static void putStationMap(Map<String, StationNo> m){
		stations.putAll(m);
	}
	
	public static StationNo getStationNo(String vid) {
		return stations.get(vid);
	}
	/**
	 * 获取线路当前站点与下一站距离（米）
	 * @param vid
	 * @param line_stationId
	 * @return
	 */
	public static int getStationDistance(String vid, String lineId, String cStationId) {
		String nextStationId = null;
		StationNo station = stations.get(vid);
		if(station != null){
			List<LineList> lineList = station.getList();
			if(lineList == null) 
				return 0;
			for(LineList line : lineList) {
				if(line.getLineId().equals(lineId)) {
					String[] stas = line.getLines().split("_");
					if(stas != null && stas.length > 0) {
						for(int i = 0; i < stas.length; i++) {
							if(cStationId.equals(stas[i])) {
								if(i != stas.length-1) {
									nextStationId = stas[i+1];
								}else {
									nextStationId = stas[i];
								}
								break;
							}
						}
					}
					break;
				}
			}
			if(station.getStationDistance() != null && station.getStationDistance().size() > 0){
				if(nextStationId != null) {
					Integer distance = station.getStationDistance().get(lineId + "_"+ nextStationId);
					if(distance != null){
						return distance;
					}
				}
			}
		}
		return 0;
	}
	/**
	 * 获取上行（起点发车）起点站
	 * @param vid
	 * @return
	 */
	public static Station getStartPointStation(String vid) {
		StationNo sn = stations.get(vid);
		if(sn == null){
			return null;
		}
		
		List<Station> list = vehicleStatiionMap.get(vid);
		for(Station s : list){
			// 取上行线路起点站
			if(s.getLineDirection() == 1 && s.getStationNo() == 1){ 
				return s;
			}
		}
		return null;
	}
	/**
	 * 根据站点历史记录获取线路编号
	 * @param vid
	 * @param stationStr
	 * @return
	 */
	public static String getLineIdByStationHistory(String vid, List<String> stationStr) {
		StationNo sn = stations.get(vid);
		if(sn == null){
			return null;
		}
		List<LineList> list = sn.getList();
		if(list != null && list.size() > 0){
			for(LineList line : list){
				for(String str : stationStr){
					if(line.getLines().contains(str)){
						return line.getLineId();
					}
				}
			}
		}
		return null;
	}
	/**
	 * 获取线路上的站点
	 * @param lineId 
	 * @param stationList
	 * @return
	 */
	public static Station getStationInLine(String vid, String lineId, List<Station> stationList) { 
		StationNo sn = stations.get(vid);
		if(sn == null){
			return null;
		}
		List<LineList> list = sn.getList(); // 获取线路列表
		if(list != null && list.size() > 0){
			for(LineList line : list){
				if(line.getLineId().equals(lineId)){ // 比对线路编号
					for(Station station : stationList){
						if(line.getLines().contains(station.getStationId())){
							return station;
						}
					}
				}
			}
		}
		return null;
	}

}
