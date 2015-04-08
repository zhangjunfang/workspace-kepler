package com.ctfo.trackservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.trackservice.common.GeometryUtil;
import com.ctfo.trackservice.common.StationCache;
import com.ctfo.trackservice.model.Station;
import com.ctfo.trackservice.model.StationHistory;
import com.ctfo.trackservice.model.StationJson;
import com.ctfo.trackservice.service.RedisService;
import com.ctfo.trackservice.util.Constant;
/**
 * 	站点信息处理器
 *	TODO
 */
public class StationHandler extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(StationHandler.class);
	/** 数据队列 */
	private ArrayBlockingQueue<Map<String, String>> queue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号 */
	private String threadName = "StationHandler-";
	/** 计数器 */
	private int index;
	/** 计数器 */
	private int inStation;
	/** 计数器 */
	private int hasStation;
	/** 上次时间 */
	private long last = System.currentTimeMillis();
	
	public StationHandler(int id) {
		try {
			threadName += id;
			setName(threadName);
			logger.info("站点处理线程[{}]启动", threadName);
		} catch (Exception e) {
			logger.error("站点处理线程启动异常:" + e.getMessage(), e); 
		}
	}

	/**
	 * 线程处理器
	 * TODO
	 */
	public void run(){
		while(true){
			try {
				Map<String, String> map = queue.take();
				
				stationHandler(map); 
				
			} catch (Exception e) {
				logger.error("站点处理线程处理异常:" + e.getMessage(), e);
			}
		}
	}
	
	/**
	 * 站点处理
	 * @param map
	 * TODO
	 */
//	private void stationHandler(Map<String, String> map) {
//		index++;
//		String vid = map.get(Constant.VID);
////		只处理有绑定站点的车辆
//		if(StationCache.vehicleBindStation(vid)){
//			String plate = map.get(Constant.VEHICLENO);
//			String speed = map.get("speed");
//			long lon = Long.parseLong(map.get("maplon"));
//			long lat = Long.parseLong(map.get("maplat"));
//			List<Station> stationList = getStationList(vid, lon, lat); //获取当前位置所在站点
//			//在站点中
//			if(stationList != null && stationList.size() > 0) {
//				logger.debug("车辆[{}]根据当前位置[{}]匹配到[{}]个站点!", vid+"-"+plate+"-"+speed, lon+"-"+lat,stationList != null ? stationList.size() : 0);
//				StationJson sj = RedisService.getVehicleStationInfo(vid);
//				//首次进站
//				if(sj == null) {
//					inStation++;
//					// 起点站  - 查询线路编号
//					Station station = StationCache.queryStationLine(vid, stationList, null, null);
//					if(station != null){
//						 sj = new StationJson();
//						 sj.setVid(vid);// 车辆编号
//						 sj.setPlate(plate);// 车牌号
//						 sj.setSpeed(Integer.parseInt(speed));// 车速
//						 sj.setLineId(station.getLineId());// 线路编号 
//						 sj.setStationId(station.getStationId());// 当前站点编号 
//						 sj.setDistance(0);	// 与上一站距离（米）
//						 sj.setPercentage(0);// 与上一站距离百分比（0~99）
//						 sj.setMapLon(station.getMapLon()); // 当前站点中心点经度
//						 sj.setMapLat(station.getMapLat()); // 当前站点中心点纬度
//						 Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
//						 stationMap.put(vid, JSON.toJSONString(sj)); 
//						 RedisService.saveVehicleStationInfo(stationMap);
//						 logger.debug("车辆[{}]根据当前起始站点[{}]查询线路[{}]成功!", vid, station.getStationId(),station.getLineId());
//					 }
//				} else {
//					//非首次进站
////					 根据上上站与上一站查询线路
//					Station station = StationCache.queryStationLine(vid, stationList, sj.getStationId(), sj.getLineId()); //TODO
//					 if(station != null){
//						 sj.setVid(vid);// 车辆编号
//						 sj.setPlate(plate);// 车牌号
//						 sj.setSpeed(Integer.parseInt(speed));// 车速
//						 sj.setLineId(station.getLineId());// 线路编号 
//						 sj.setAfterStationId(sj.getStationId());// 上上站站点编号 
//						 sj.setStationId(station.getStationId());// 上一站站点编号 
//						 sj.setDistance(0);	// 与上一站距离（米）
//						 sj.setPercentage(0);// 与上一站距离百分比（0~99）
//						 sj.setMapLon(station.getMapLon()); // 上一站中心点经度
//						 sj.setMapLat(station.getMapLat()); // 上一站中心点纬度
//						 Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
//						 stationMap.put(vid, JSON.toJSONString(sj)); 
//						 RedisService.saveVehicleStationInfo(stationMap);
//						 logger.debug("车辆[{}]根据当前站点[{}]和上一站点[{}]查询线路[{}]成功!", vid, station.getStationId(), sj.getStationId(), sj.getLineId());
//					 } 
//				}
//			} else {
//				// 出站 - (计算与上一站点距离)
//					StationJson sj = RedisService.getVehicleStationInfo(vid);
//					if(sj != null){
//						outStation++; 
//						double dis = GeometryUtil.getDistance(sj.getMapLon()*1.0/600000,sj.getMapLat()*1.0/600000,lon*1.0/600000,lat*1.0/600000);
//						int distance = Double.valueOf(dis).intValue();
//						int stationDistance = 0;
//						int percentage = 0;
//						if(distance > 0 && sj.getAfterStationId() != null){
//							stationDistance = StationCache.getStationDistance(vid, sj.getLineId(), sj.getStationId()); // 获取站点距离
//							logger.debug("车辆[{}]线路[{}]站点距离[{}]:", vid, sj.getLineId()+"_"+sj.getStationId(),stationDistance);
//							if(stationDistance > 0){
//								percentage = distance  * 100/ stationDistance; 
//							}
//						}
//						sj.setDistance(distance); // 与上一站距离（米）
//						sj.setPercentage(percentage > 100 ? 99 : percentage);// 与上一站距离百分比（0~99）
//						sj.setSpeed(Integer.parseInt(speed));// 车速
//						Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
//						stationMap.put(vid, JSON.toJSONString(sj)); 
//						RedisService.saveVehicleStationInfo(stationMap);
//						logger.debug("车辆[{}]出站, 与上一站距离[{}]米, 比率[{}], 当前经度[{}]当前纬度[{}]上一站经度[{}]上一站纬度[{}]上上站经度[{}]上上站纬度[{}]", plate,distance,percentage,lon,lat,sj.getMapLon(),sj.getMapLat(),sj.getAfterMapLon(),sj.getAfterMapLat());
//					} 
//			} 
//		}
//
//		long cur = System.currentTimeMillis();
//		if(cur - last > 10000){
//			logger.info("站点处理线程10秒接收[{}]条记录, 队列[{}]条, 进站[{}]次, 出站[{}]次", index, queue.size(), inStation, outStation); 
//			index = 0;
//			inStation = 0;
//			outStation = 0;
//			last = System.currentTimeMillis();
//		}
//	}
	/**
	 * 站点处理
	 * @param map
	 * TODO
	 */
	public void stationHandler(Map<String, String> map) {
		index++;
		String vid = map.get(Constant.VID);
//		只处理有绑定站点的车辆
		if(StationCache.vehicleBindStation(vid)){
			hasStation++;
			String plate = map.get(Constant.VEHICLENO);
			String speed = map.get("speed");
			long lon = Long.parseLong(map.get("maplon"));
			long lat = Long.parseLong(map.get("maplat"));
//		查询缓存是否有车辆信息
			StationJson sj = RedisService.getVehicleStationInfo(vid);
			if (sj == null) { // 无缓存 - 建立缓存，将车辆放到起点发车（上行）起点站
				Station station = StationCache.getStartPointStation(vid);
				if(station != null){
					sj = new StationJson();
					sj.setVid(vid);// 车辆编号
					sj.setPlate(plate);// 车牌号
					sj.setSpeed(Integer.parseInt(speed));// 车速
					sj.setLineId(station.getLineId());// 线路编号
					sj.setStationId(station.getStationId());// 当前站点编号
					sj.setDistance(0); // 与上一站距离（米）
					sj.setPercentage(0);// 与上一站距离百分比（0~99）
					sj.setMapLon(station.getMapLon()); // 当前站点中心点经度
					sj.setMapLat(station.getMapLat()); // 当前站点中心点纬度
					sj.setAnalyzingLine(true); // 需要分析线路
					Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
					stationMap.put(vid, JSON.toJSONString(sj));
					RedisService.saveVehicleStationInfo(stationMap);
					logger.debug("lineAnalysis - 车辆[{} {}]存储起始站点[{}]线路[{}]成功!", plate, vid, station.getStationId(), station.getLineId());
				}else{
					logger.error("lineAnalysis - 车辆[{} {}] 获取线路起点站失败", plate, vid);
				}
			}else{ // 有缓存
//			判断当前位置是否进站
				List<Station> stationList = getStationList(vid, lon, lat); //获取当前位置所在站点
				if(sj.isAnalyzingLine()){ // 判断线路
//				判断线路时必须保证车辆有进站记录
					if(stationList != null && stationList.size() > 0){ 
						inStation++;
						StationHistory sh = RedisService.getStationHistory(vid);
						if(sh != null){ // 存储站点历史记录
							sh.saveStationList(stationList);
//						分析站点在那条线路上
							List<String> stationStr = sh.getStationStr();
							String lineId = StationCache.getLineIdByStationHistory(vid, stationStr);
							if(lineId != null && lineId.length() > 0){
								Station station = getStationIdByLineId(lineId, stationList);
								if(station != null){
									sj.setSpeed(Integer.parseInt(speed));// 车速
									sj.setLineId(lineId);// 线路编号
									sj.setStationId(station.getStationId());// 当前站点编号
									sj.setDistance(0); // 与上一站距离（米）
									sj.setPercentage(0);// 与上一站距离百分比（0~99）
									sj.setMapLon(station.getMapLon()); // 当前站点中心点经度
									sj.setMapLat(station.getMapLat()); // 当前站点中心点纬度
									sj.setAnalyzingLine(false); // 需要分析线路
									Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
									stationMap.put(vid, JSON.toJSONString(sj));
									RedisService.saveVehicleStationInfo(stationMap);
									RedisService.removeStationHistory(vid);
									String str = getStationIdStr(stationList);
									logger.debug("lineAnalysis - 线路[{}]匹配站点[{}]成功", lineId, str);
								}else{
									String str = getStationIdStr(stationList);
									logger.debug("lineAnalysis - 通过线路编号[{}]获取匹配站点[{}]异常", lineId, str);
								}
							}else{ 
								sh.clearList();
								RedisService.saveStationHistory(vid, sh);
								printSaveStationHistory(speed, speed, stationList);
							}
						}else{ // 第一次进站
							sh = new StationHistory();
							sh.setFirstStation(stationList);
							RedisService.saveStationHistory(vid, sh);
							printSaveStationHistory(speed, speed, stationList);
						}
					}
				}else{ // 不用判断线路
//				进站
					if(stationList != null && stationList.size() > 0){ 
						inStation++;
						Station station = isStartingPoint(stationList); // 判断是否起始站点 - 如果当前站点是是终点
//					如果在起点 - 更新线路编号
						if(station != null){
							sj.setSpeed(Integer.parseInt(speed));// 车速
							sj.setLineId(station.getLineId());// 线路编号
							sj.setStationId(station.getStationId());// 当前站点编号
							sj.setDistance(0); // 与上一站距离（米）
							sj.setPercentage(0);// 与上一站距离百分比（0~99）
							sj.setMapLon(station.getMapLon()); // 当前站点中心点经度
							sj.setMapLat(station.getMapLat()); // 当前站点中心点纬度
							Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
							stationMap.put(vid, JSON.toJSONString(sj));
							RedisService.saveVehicleStationInfo(stationMap);
							logger.debug("lineAnalysis - 车辆[{}-{}]进入起点[{}]成功, 线路编号[{}]", plate, vid, station.getStationId(), station.getLineId());
						}else{
//					如果在线路上 - 判断当前站点列表是否属于此站点
							Station s = StationCache.getStationInLine(vid, sj.getLineId(), stationList);
							if(s != null){
								sj.setSpeed(Integer.parseInt(speed));// 车速
								sj.setStationId(s.getStationId());// 当前站点编号
								sj.setDistance(0); // 与上一站距离（米）
								sj.setPercentage(0);// 与上一站距离百分比（0~99）
								sj.setMapLon(s.getMapLon()); // 当前站点中心点经度
								sj.setMapLat(s.getMapLat()); // 当前站点中心点纬度
								Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
								stationMap.put(vid, JSON.toJSONString(sj));
								RedisService.saveVehicleStationInfo(stationMap);
								logger.debug("lineAnalysis - 车辆[{}-{}]进站成功, 站点[{}]匹配线路[{}]成功", plate, vid, s.getStationId(), sj.getLineId());
							}else{
								String stationIds = getStationIdStr(stationList);
								logger.debug("lineAnalysis - 车辆[{}-{}]进站失败, 站点[{}]匹配线路[{}]失败", plate, vid, stationIds, sj.getLineId());
							}
						}
//				出站	
					}else{
//					计算当前位置与过站之间距离
						double dis = GeometryUtil.getDistance(sj.getMapLon() * 1.0 / 600000, sj.getMapLat() * 1.0 / 600000, lon * 1.0 / 600000, lat * 1.0 / 600000);
						int distance = Double.valueOf(dis).intValue();
						int stationDistance = 0;
						int percentage = 0;
//					计算当前位置在站点间的比例
						if (distance > 0) {
							stationDistance = StationCache.getStationDistance(vid, sj.getLineId(), sj.getStationId()); // 获取站点距离
							logger.debug("lineAnalysis - 车辆[{}]线路[{}]站点距离[{}]:", vid, sj.getLineId() + "_" + sj.getStationId(), stationDistance);
							if (stationDistance > 0) {
								percentage = distance * 100 / stationDistance;
							}
						}
						sj.setDistance(distance); // 与上一站距离（米）
						sj.setPercentage(percentage > 100 ? 99 : percentage);// 与上一站距离百分比（0~99）
						sj.setSpeed(Integer.parseInt(speed));// 车速
						Map<String, String> stationMap = new ConcurrentHashMap<String, String>();
						stationMap.put(vid, JSON.toJSONString(sj));
						RedisService.saveVehicleStationInfo(stationMap);
						logger.debug("lineAnalysis - 车辆[{}]出站, 与上一站距离[{}]米,上站与下站距离[{}], 比率[{}], 当前经度[{}]当前纬度[{}]上一站经度[{}]上一站纬度[{}]上上站经度[{}]上上站纬度[{}]", plate, distance,stationDistance, percentage, lon, lat, sj.getMapLon(),sj.getMapLat(), sj.getAfterMapLon(), sj.getAfterMapLat());
					}
				}
			}
		}
		long cur = System.currentTimeMillis();
		if(cur - last > 10000){
			logger.info("lineAnalysis - 站点处理线程10秒接收[{}]条记录, 有效[{}]次, 队列[{}]条, 进站[{}]次", index, hasStation, queue.size(), inStation); 
			index = 0;
			inStation = 0;
			hasStation = 0;
			last = System.currentTimeMillis();
		}
	}
	/**
	 * 根据线路编号获取站点信息
	 * @param lineId
	 * @param stationList
	 * @return
	 * TODO
	 */
	private Station getStationIdByLineId(String lineId, List<Station> stationList) {
		for(Station s : stationList){
			if(s.getLineId().equals(lineId)){
				return s;
			}
		}
		return null;
	}

	/**
	 * 日志输出站点历史存储记录
	 * @param plate
	 * @param vid
	 * @param list
	 * TODO
	 */
	private void printSaveStationHistory(String plate, String vid, List<Station> list) {
		if(logger.isDebugEnabled()){
			String str = getStationIdStr(list);
			logger.debug("lineAnalysis - 存储车辆[{} {}]站点[{}]历史记录成功!", plate, vid, str);
		}
	}
	/**
	 * 获取站点编号
	 * @param list
	 * @return
	 * TODO
	 */
	private String getStationIdStr(List<Station> list){
		StringBuffer sb = new StringBuffer(" ");
		for(Station s : list){
			sb.append(s.getStationId()).append(",");
		}
		return sb.substring(0, sb.length() - 1);
	}
	/**
	 * 获取起始站点
	 * @param stationList
	 * @return
	 * TODO
	 */
	private Station isStartingPoint(List<Station> stationList) {
		for(Station s : stationList){
			if(s.getStationNo() == 1){
				return s;
			}
		}
		return null;
	}

	/**
	 * 获取当前位置所在站点
	 * @param vid
	 * @param lon
	 * @param lat
	 * @return
	 * TODO
	 */
	private List<Station> getStationList(String vid, long lon, long lat) {
		List<Station> sl = new ArrayList<Station>();
		List<Station> stationList = StationCache.getStationList(vid); // 获取车辆站点列表
		if (stationList != null && stationList.size() > 0) { 
			for(Station station : stationList){ 
//				计算当前位置与站点中心距离 
				double distance = GeometryUtil.getDistance(station.getMapLon()/600000.0,station.getMapLat()/600000.0,lon/600000.0,lat/600000.0);
				logger.debug("车辆[{}]当前位置[{}][{}]与站点[{}]范围[{}]经度[{}]纬度[{}]距离为[{}]米", vid,lon,lat,station.getStationId(),station.getStationRadius(),station.getMapLon(),station.getMapLat(),Double.valueOf(distance).intValue());
				if (distance >= 0 && distance <= station.getStationRadius()){
					sl.add(station);
				}
			}
		}
		return sl;
	}

	/**
	 * 队列写入
	 * @param dataMap
	 * TODO
	 */
	public void put(Map<String, String> dataMap) {
		try {
			queue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error("队列写入失败:" + e.getMessage(), e); 
		}
	}
}
