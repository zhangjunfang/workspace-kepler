package com.ctfo.savecenter.dao;


import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.savecenter.beans.ServiceUnit;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;
import com.vividsolutions.jts.index.strtree.STRtree;
import com.vividsolutions.jts.io.WKTReader;

public class TempMemory {


	// 车辆缓存
	private static Map<String, ServiceUnit> vehicleMap = new ConcurrentHashMap<String, ServiceUnit>();

//	/** 报警代码对应报警描述Map */
//	private static Map<Integer, AlarmTypeBean> alarmtypeMap = new ConcurrentHashMap<Integer, AlarmTypeBean>();
//
//	// 存储车辆状态对应参考值
//	private static Map<Long, StatusCode> statusMap = new HashMap<Long, StatusCode>();
//
//	// 存储车辆状态对应参考值
//	private static Map<String, String> vehicleIsonlineMap = new ConcurrentHashMap<String, String>();
//
//	/** 车辆报警缓存 */  // 存储车辆报警id
//	private static Map<String, String> vehicleAlarmMap = new ConcurrentHashMap<String, String>();
//	
//	//缓存车辆最大转速
//	private static Map<String, Long> vehicleMaxRpmMap = new ConcurrentHashMap<String, Long>();
//	
//	// 缓存五分钟数据
//	private static Map<Long,FiveMinDataBean> fiveMap = new HashMap<Long,FiveMinDataBean>();
//	
//	// 车辆对应报警设置企业列表(key:vid,value:ent_id)
//	public static Map<Long,Long> vidEntMap = new ConcurrentHashMap<Long,Long>();
//	
//	// 企业对应报警设置告警列表(key:vid,value:报警code列表)
//	public static Map<Long,List<String>> entAlarmMap = new ConcurrentHashMap<Long,List<String>>();
//	/* 全国区域拓扑树 */
	private static STRtree areaTree = new STRtree();
//	//缓存车辆上一次区域位置信息 ，初始化时为所属地区域位置信息
	private static Map<String, String> areaLastMap = new ConcurrentHashMap<String, String>();
//	/**
//	 * 获取接收队列值
//	 * 
//	 * @return
//	 */
//	public static String getVehicleAlarmMapValue(String key) {
//		return vehicleAlarmMap.get(key);
//	}
//
//	/**
//	 * 添加接收队列值
//	 * 
//	 * @param value
//	 */
//	public static void setVehicleAlarmMapValue(String key, String value) {
//		vehicleAlarmMap.put(key, value);
//	}
//	
//	/***
//	 * 判断是否包含当前key
//	 * @param key
//	 * @return
//	 */
//	public static boolean vehicleAlarmMapContain(String key){
//		return vehicleAlarmMap.containsKey(key);
//	}
//
//	/**
//	 * 获取接收队列值
//	 * 
//	 * @return
//	 */
//	public static String getvehicleIsonlineMapValue(String key) {
//		return vehicleIsonlineMap.get(key);
//	}
//
//	/**
//	 * 添加接收队列值
//	 * 
//	 * @param value
//	 */
//	public static void setvehicleIsonlineMapValue(String key, String value) {
//		vehicleIsonlineMap.put(key, value);
//	}
//
//	/**
//	 * 获取接收队列大小
//	 */
//	public static long getAlarmtypeMapSize() {
//		return alarmtypeMap.size();
//	}
//
//	/**
//	 * 获取接收队列值
//	 * 
//	 * @return
//	 */
//	public static AlarmTypeBean getAlarmtypeMapValue(int key) {
//		return alarmtypeMap.get(key);
//	}
//
//	/**
//	 * 添加接收队列值
//	 * 
//	 * @param value
//	 */
//	public static void setAlarmtypeMapValue(int key, AlarmTypeBean alarmTypeBean) {
//		alarmtypeMap.put(key, alarmTypeBean);
//	}
//
//	/**
//	 * 获取接收队列大小
//	 */
//	public static long getStatusMapSize() {
//		return statusMap.size();
//	}
//
//	/**
//	 * 获取接收队列值
//	 * 
//	 * @return
//	 */
//	public static StatusCode getStatusMapValue(long key) {
//		return statusMap.get(key);
//	}
//
//	/**
//	 * 添加接收队列值
//	 * 
//	 * @param value
//	 */
//	public static void setStatusMapValue(long key, StatusCode statusCode) {
//		statusMap.put(key, statusCode);
//	}

	/**
	 * 获取接收队列大小
	 */
	public static long getVehicleMapSize() {
		return vehicleMap.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static ServiceUnit getVehicleMapValue(String macid) {
		return vehicleMap.get(macid);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setVehicleMapValue(String macid, ServiceUnit serviceUnit) {
		vehicleMap.put(macid, serviceUnit);
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static Map<String, ServiceUnit> getVehicleMap() {
		return vehicleMap;
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static void deleteVehicleMapValue(String macid) {
		vehicleMap.remove(macid);
	}

	public static void initTempMemory() {
	}

//	// 存储大气压力缓存状态
//	public static void addFiveAirPressureStatus(long vid,int status){
//		FiveMinDataBean fiveMinData = fiveMap.get(vid);
//		if(fiveMinData != null){
//			fiveMinData.setAirPressureStatus(status);
//		}else{
//			fiveMinData = new FiveMinDataBean();
//			fiveMinData.setAirPressureStatus(status);
//			fiveMap.put(vid, fiveMinData);
//		}
//	}
//	
//	// 获取大气压力缓存状态,无任何缓存，返回无效状态2
//	public static int getFiveAirPressureStatus(long vid){
//		FiveMinDataBean fiveMinData = fiveMap.get(vid);
//		if(fiveMinData != null){
//			return fiveMinData.getAirPressureStatus();
//		}else{
//			return 2;
//		}
//	}
//	
//	// 存储冷却液温度缓存状态
//	public static void addFiveEWaterStatus(long vid,int status){
//		FiveMinDataBean fiveMinData = fiveMap.get(vid);
//		if(fiveMinData != null){
//			fiveMinData.seteWaterStatus(status);
//		}else{
//			fiveMinData = new FiveMinDataBean();
//			fiveMinData.seteWaterStatus(status);
//			fiveMap.put(vid, fiveMinData);
//		}
//	}
//	
//	// 获取冷却液温度缓存状态,无任何缓存，返回无效状态2
//	public static int getFiveEWaterStatus(long vid){
//		FiveMinDataBean fiveMinData = fiveMap.get(vid);
//		if(fiveMinData != null){
//			return fiveMinData.geteWaterStatus();
//		}else{
//			return 2;
//		}
//	}
//	
//	// 存储蓄电池电压缓存状态
//	public static void addFiveExtVoltageStatus(long vid,int status){
//		FiveMinDataBean fiveMinData = fiveMap.get(vid);
//		if(fiveMinData != null){
//			fiveMinData.setExtVoltageStatus(status);
//		}else{
//			fiveMinData = new FiveMinDataBean();
//			fiveMinData.setExtVoltageStatus(status);
//			fiveMap.put(vid, fiveMinData);
//		}
//	}
//	
//	// 获取蓄电池电压缓存状态 ,无任何缓存，返回无效状态2
//	public static int getFiveExtVoltageStatus(long vid){
//		FiveMinDataBean fiveMinData = fiveMap.get(vid);
//		if(fiveMinData != null){
//			return fiveMinData.getExtVoltageStatus();
//		}else{
//			return 2;
//		}
//	}
//
//	/**
//	 * 获取缓存的车辆最大转速
//	 * 
//	 * @return
//	 */
//	public static Long getVehicleMaxRpmMapValue(String key) {
//		if(vehicleMaxRpmMap== null){
//			return null;
//		}
//		return vehicleMaxRpmMap.get(key);
//	}
//
//	/**
//	 * 添加车量最大转速缓存
//	 * 
//	 * @param value
//	 */
//	public static void setVehicleMaxRpmValue(String key, Long value) {
//		vehicleMaxRpmMap.put(key, value);
//	}
	
//	/**
//	 * 移除车辆最大转速缓存
//	 * 
//	 * @param value
//	 */
//	public static void removeVehicleMaxRpmValue(String key) {
//		vehicleMaxRpmMap.remove(key);
//	}
//	
//	/***
//	 * 判断是否包含当前key
//	 * @param key
//	 * @return
//	 */
//	public static boolean vehicleMaxRpmMapContain(String key){
//		return vehicleMaxRpmMap.containsKey(key);
//	}
//	
//	
	// 获得最近区域编码集合
	public static Map<String, String> getAreaLastMap() {
		return areaLastMap;
	}
	//添加队列值
	public static void setAreaLastMap(String key, String value) {
		areaLastMap.put(key, value);
	}
	//获取队列key对应的值
	public static String getAreaLastMapValue(String key){
		return areaLastMap.get(key);
	}
	//获取AreaLastMap的大小
	public static int getAreaLastMapSize(){
		return areaLastMap.size();
	}
	/**
	 * areaTree全国区域拓扑树初始化
	 * 
	 * @param fileName
	 * @throws Exception
	 * void
	 *
	 */
	public static void buildTree(String fileName) throws Exception{
		FileInputStream fis = new FileInputStream(new File(fileName)); 
		BufferedReader br = new BufferedReader(new InputStreamReader(fis, "utf-8"));
		String line = null;
		AreaNode node = null;
		while ((line = br.readLine()) != null) {// 读取区域文件
			String[] lines = line.split(";");
			Geometry polygon = new WKTReader().read(lines[lines.length - 1]);
			node = new AreaNode();// 创建节点
			node.setAreaCode(Long.parseLong(lines[0]));
			node.setAreaName(lines[1]);
			node.setPolygon(polygon);
			areaTree.insert(polygon.getEnvelopeInternal(), node);// 向树上添加节点
		}
		fis.close();
		br.close();
		areaTree.build();// 构建树
	}
	
	/**
	 * 根据经度、纬度获取区域编码
	 * 
	 * @param lon 经度	
	 * @param lat 纬度
	 * @return
	 * @throws Exception
	 * long
	 *
	 */
	public static long getAreaAnalyzer(double lon, double lat) throws Exception{
		long areacode = -1; 
		Geometry geo = new GeometryFactory().createPoint(new Coordinate(lon, lat));
		List<?> areas = areaTree.query(geo.getEnvelopeInternal()); 
		for (Object obj : areas) {
			AreaNode anode = (AreaNode)obj;
			if(anode.getPolygon().contains(geo)){
				areacode = anode.getAreaCode();
				break;
			}
		}
		return areacode; 
	}
	/**
	 * getAreaAnalyzer方法中使用的静态类
	 */
	static class AreaNode {
		private long areaCode = -1;// 区域编码
		private String areaName = "";// 区域名称
		private Geometry polygon = null;// 区域几何图形
		
		public long getAreaCode() {
			return areaCode;
		}
		public void setAreaCode(long areaCode) {
			this.areaCode = areaCode;
		}
		public String getAreaName() {
			return areaName;
		}
		public void setAreaName(String areaName) {
			this.areaName = areaName;
		}
		public Geometry getPolygon() {
			return polygon;
		}
		public void setPolygon(Geometry polygon) {
			this.polygon = polygon;
		}
	}
}
