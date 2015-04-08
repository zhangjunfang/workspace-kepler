package com.ctfo.trackservice.service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import org.junit.Test;

import com.alibaba.fastjson.JSON;
import com.ctfo.trackservice.common.ConfigLoader;
import com.ctfo.trackservice.common.Utils;
import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.RedisConnectionPool;
import com.ctfo.trackservice.model.StationJson;

public class RedisServiceTest {
//	RedisService redisService;
	public RedisServiceTest() {
		try {
//			ConfigLoader.init(new String[]{"-d" , "E:/WorkSpace/trank/TrackService/src/config.xml", "E:/WorkSpace/trank/TrackService/src/system.properties"});
			ConfigLoader.init(new String[]{"-d" , "E:/WorkSpace2/TrackService/src/config.xml", "E:/WorkSpace2/TrackService/src/system.properties"});
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
		} catch (Exception e) {
			e.printStackTrace();
		} 
	}
	@Test
	public void testSaveVehicleStationInfo() {
		System.out.println(RedisService.getVehicleStationInfo("123").getLineId());
	}

	@Test
	public void testGetVehicleStationInfo() {
		StationJson sj = new StationJson();
		sj.setVid("123");// 车辆编号
		sj.setPlate("");// 车牌号
		sj.setSpeed(1000);// 车速
		sj.setLineId("123");// 线路编号
		sj.setStationId("123");// 上一站站点编号
		sj.setDistance(0); // 与上一站距离（米）
		sj.setPercentage(0);// 与上一站距离百分比（0~99）
		sj.setMapLon(123); // 上一站中心点经度
		sj.setMapLat(123); // 上一站中心点纬度
		
		StationJson sj2 = new StationJson();
		sj2.setVid("123");// 车辆编号
		sj2.setPlate("");// 车牌号
		sj2.setSpeed(1000);// 车速
		sj2.setLineId("1234");// 线路编号
		sj2.setStationId("1234");// 上一站站点编号
		sj2.setDistance(0); // 与上一站距离（米）
		sj2.setPercentage(0);// 与上一站距离百分比（0~99）
		sj2.setMapLon(1234); // 上一站中心点经度
		sj2.setMapLat(1234); // 上一站中心点纬度
		Map<String, String> stationMap = new HashMap<String, String>();
		List<StationJson> sjList = new ArrayList<StationJson>();
		sjList.add(sj);
		sjList.add(sj2);
		stationMap.put("123", JSON.toJSONString(sjList));
		RedisService.saveVehicleStationInfo(stationMap);
	}
	@Test
	public void testSaveLineVehicleMap() {
		try {
			Map<String, Set<String>> lineVehicleMap = new ConcurrentHashMap<String, Set<String>>();
//			for(int i = 0; i < 5; i++){
//				Set<String> vSet = new HashSet<String>();
//				for(int j =0;j<10;j++){
//					vSet.add("" + (j+1));
//				}
//				lineVehicleMap.put(""+(i+1), vSet);
//			} 
			Set<String> vSet = new HashSet<String>();
			vSet.add("123");
			lineVehicleMap.put("1234", vSet);
			Map<String, String> lineVehicle = new ConcurrentHashMap<String, String>();
			for(Map.Entry<String, Set<String>> lv : lineVehicleMap.entrySet()){
				lineVehicle.put(lv.getKey(), JSON.toJSONString(lv.getValue()));
			}
			RedisService.saveLineVehicleMap(lineVehicle);
			
			
		} catch (Exception e) { 
			e.printStackTrace(); 
		}
	}
}
