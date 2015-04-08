package com.ctfo.trackservice.model;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.common.GeometryUtil;


public class StationNo{
	private static final Logger log = LoggerFactory.getLogger(StationNo.class);
	/**	车辆编号	*/
	private String vid;
	/**	线路方向（1:起点发车；2:终点发车）	*/
	private List<LineList> list;
	/**	站点线路列表	*/
	private Map<String, List<StationLine>> map;
	/**	线路中站点距离列表	*/
	private Map<String, Integer> stationDistance;
	/**
	 * 初始化
	 * @param vid 车辆编号
	 * @param stationMap 站点结果集
	 */
	public StationNo(String vid, Map<String, List<StationLine>> stationMap) {
		this.vid = vid;
		this.map = stationMap;
		this.list = new ArrayList<LineList>();
		this.stationDistance = new ConcurrentHashMap<String, Integer>();
	}

	/**
	 * @return the 车辆编号
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * @param 车辆编号 the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * @return the 站点序号
	 */
	public Map<String, List<StationLine>> getMap() {
		return map;
	}

	/**
	 * @param 站点序号 the map to set
	 */
	public void setMap(Map<String, List<StationLine>> map) {
		this.map = map;
	}

	/**
	 * @return the 站点序号privatein
	 */
	public List<LineList> getList() {
		return list;
	}

	/**
	 * @param 站点序号privatein the list to set
	 */
	public void setList(List<LineList> list) {
		this.list = list;
	}
	/**
	 * @return the 线路中站点距离列表
	 */
	public Map<String, Integer> getStationDistance() {
		return stationDistance;
	}

	/**
	 * @param 线路中站点距离列表 the stationDistance to set
	 */
	public void setStationDistance(Map<String, Integer> stationDistance) {
		this.stationDistance = stationDistance;
	}

	/**
	 * 合并线路信息
	 */
	public void mergeSites() {
		long start = System.currentTimeMillis();
		int lineIndex = 0;
		int distanceIndex = 0;
		if(map != null && map.size() > 0){
			Map<String, Integer> stationDistance = new ConcurrentHashMap<String, Integer>();
			Station last = new Station();
			for(Map.Entry<String, List<StationLine>> m : map.entrySet()){
				List<StationLine> l = m.getValue(); // 站点信息列表
				if(l != null && l.size() > 0){
					Collections.sort(l); // 站点信息列表排序
					StringBuffer sb = new StringBuffer();
					StringBuffer sbNo = new StringBuffer();
					for(StationLine sl : l){
						sb.append(sl.getStationId()).append("_");
						sbNo.append(sl.getStationId()).append(",");
						if(sl.getStationNo() != 1){
//							double distance = Utils.getLength(last.getMapLon()*1.0/600000, sl.getMapLon()*1.0/600000 ,last.getMapLat()*1.0/600000, sl.getMapLat()*1.0/600000);
							double distance = GeometryUtil.getDistance(sl.getMapLon()*1.0/600000,sl.getMapLat()*1.0/600000,last.getMapLon()*1.0/600000,last.getMapLat()*1.0/600000);
							int dis = Double.valueOf(distance).intValue();
							stationDistance.put(m.getKey() + "_" + sl.getStationId(), dis);
							log.debug("站点[" + m.getKey() + "_" + sl.getStationId() + "]的距离是:[" + dis + "]米, lon1["  + last.getMapLon()+"]lat1["+last.getMapLat()+"]lon2["+sl.getMapLon()+"]lat2["+sl.getMapLat()+"]");
							distanceIndex++;
						}
						last.setMapLon(sl.getMapLon());
						last.setMapLat(sl.getMapLat()); 
					}
					log.debug("线路编号[" + m.getKey() + "], 站点集["+sb.toString()+"], 线路方向["+l.get(0).getLineDirection()+"], 线路序号["+sbNo.toString()+"]");
					setStationDistance(stationDistance);
					list.add(new LineList(m.getKey(),sb.toString(),l.get(0).getLineDirection()));
					lineIndex++;
				} 
			}
			map.clear(); 
		}
		long end = System.currentTimeMillis();
		log.info("车辆[{}]-线路站点集合[{}]条、站点距离[{}]条, 解析耗时[{}]ms",vid, lineIndex, distanceIndex, end-start);
	}
}
