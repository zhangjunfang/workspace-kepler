package com.ctfo.analy;

import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmNotice;
import com.ctfo.analy.beans.AreaAlarmBean;
import com.ctfo.analy.beans.FatigueAlarmCfgBean;
import com.ctfo.analy.beans.GPSInspectionConfig;
import com.ctfo.analy.beans.IllegalOptionsAlarmBean;
import com.ctfo.analy.beans.LineAlarmBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.OrgParentInfo;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.RoadAlarmBean;
import com.ctfo.analy.beans.TbLineStationBean;
import com.ctfo.analy.util.ExceptionUtil;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Envelope;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;
import com.vividsolutions.jts.geom.LineString;
import com.vividsolutions.jts.index.strtree.STRtree;
import com.vividsolutions.jts.operation.buffer.BufferOp;
import com.vividsolutions.jts.operation.buffer.BufferParameters;

/**
 * 临时缓存
 * 
 * @author LiangJian
 * 2012-12-14 17:20:00
 */
public class TempMemory {

	private static final Logger logger = Logger.getLogger(TempMemory.class);
	
	/** 手机号对应报警设置Map */
	private static Map<String, AlarmBaseBean> alarmVehicleMap = new ConcurrentHashMap<String, AlarmBaseBean>();
	
	private static Map<String,GPSInspectionConfig> gpsInsConfigMap = new ConcurrentHashMap<String,GPSInspectionConfig>();
	

	/** 企业告警下发消息缓存Map */
	private static Map<String, ConcurrentHashMap<String,AlarmNotice>> orgAlarmNoticeMap = new ConcurrentHashMap<String, ConcurrentHashMap<String,AlarmNotice>>();
	
	public static ConcurrentHashMap<String,AlarmNotice> getOrgAlarmNoticeMap(String key) {
		return orgAlarmNoticeMap.get(key);
	}

	public static Map<String,ConcurrentHashMap<String,AlarmNotice>> getOrgAlarmNoticeMap(){
		return orgAlarmNoticeMap;
	}
	
	public static void setOrgAlarmNoticeMap(String entId,ConcurrentHashMap<String,AlarmNotice> alarmNoticeMap){
			orgAlarmNoticeMap.put(entId,alarmNoticeMap);
	}
	
	public static void clearOrgAlarmNoticeMap() {
 		orgAlarmNoticeMap.clear();
	}
	
	public static boolean containsOrgAlarmNoticeMap(String key) {
 		return orgAlarmNoticeMap.containsKey(key);
	}
	
	public static AlarmBaseBean getAlarmVehicleMap(String key) {
		return alarmVehicleMap.get(key);
	}

	public static void setAlarmVehicleMap(String key,AlarmBaseBean alarmvehicleBean) {
		alarmVehicleMap.put(key, alarmvehicleBean);
	}

	public static int getAlarmVehicleMapSize() {
		return alarmVehicleMap.size();
	}

	public static Map<String, AlarmBaseBean> getAlarmVehicleMapAll() {
		return alarmVehicleMap;
	}

	public static void deleteAlarmVehicleMap(String key) {
		synchronized (alarmVehicleMap) {
			if(alarmVehicleMap.containsKey(key))
				alarmVehicleMap.remove(key);
		}
		
	}

	public static Map<String,GPSInspectionConfig> getGpsInsConfigList(){
		return gpsInsConfigMap;
	}
	
	public static void addGpsInsConfig(String commaddr,GPSInspectionConfig config){
		synchronized (gpsInsConfigMap) {
			gpsInsConfigMap.put(commaddr,config);
		}
	}
	
	public static GPSInspectionConfig getGPSInspectionConfig(String commaddr){
		if(gpsInsConfigMap.containsKey(commaddr)){
			return gpsInsConfigMap.get(commaddr);
		}
		return null;
	}
	
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int roadAlarmMapNo = 1;
	/** VID对应道路等级报警设置Map */
	private static Map<String, List<RoadAlarmBean>> roadAlarmMap1 = new ConcurrentHashMap<String, List<RoadAlarmBean>>();
	private static Map<String, List<RoadAlarmBean>> roadAlarmMap2 = new ConcurrentHashMap<String, List<RoadAlarmBean>>();
	
	public static List<RoadAlarmBean> getRoadAlarmMap(String key) {
		if(roadAlarmMapNo == 1){
			return roadAlarmMap1.get(key);
		}else{
			return roadAlarmMap2.get(key);
		}
	}
	
	public static void setRoadAlarmMap(String key,List<RoadAlarmBean> roadAlarmBean) {
		if(roadAlarmMapNo == 1){
			roadAlarmMap1.put(key, roadAlarmBean);
		}else{
			roadAlarmMap2.put(key, roadAlarmBean);
		}
	}
	
	public static List<RoadAlarmBean> getRoadAlarmMap(String key,int no) {
		if(no == 1){
			return roadAlarmMap1.get(key);
		}else{
			return roadAlarmMap2.get(key);
		}
	}
	
	public static void setRoadAlarmMap(String key,List<RoadAlarmBean> roadAlarmBean,int no) {
		if(no == 1){
			roadAlarmMap1.put(key, roadAlarmBean);
		}else{
			roadAlarmMap2.put(key, roadAlarmBean);
		}
	}
	
	public static void clearRoadAlarmMap(){
		if(roadAlarmMapNo == 1){
			roadAlarmMap2.clear();
		}else{
			roadAlarmMap1.clear();
		}
	}
	
	public static int getRoadAlarmMapNo() {
		return roadAlarmMapNo;
	}

	public static void setRoadAlarmMapNo(int roadAlarmMapNo) {
		TempMemory.roadAlarmMapNo = roadAlarmMapNo;
	}


	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int areaAlarmMapNo = 1;
	/** VID对应[区域报警]设置Map */
	private static Map<String, List<AreaAlarmBean>> areaAlarmMap1 = new ConcurrentHashMap<String, List<AreaAlarmBean>>();
	private static Map<String, List<AreaAlarmBean>> areaAlarmMap2 = new ConcurrentHashMap<String, List<AreaAlarmBean>>();

	public static List<AreaAlarmBean> getAreaAlarmMap(String key) {
		if(areaAlarmMapNo == 1){
			return areaAlarmMap1.get(key);
		}else{
			return areaAlarmMap2.get(key);
		}
	}

	public static void setAreaAlarmMap(String key, List<AreaAlarmBean> value) {
		if(areaAlarmMapNo == 1){
			areaAlarmMap1.put(key, value);
		}else{
			areaAlarmMap2.put(key, value);
		}
	}
	
	public static List<AreaAlarmBean> getAreaAlarmMap(String key,int no) {
		if(no == 1){
			return areaAlarmMap1.get(key);
		}else{
			return areaAlarmMap2.get(key);
		}
	}
	
	public static void setAreaAlarmMap(String key, List<AreaAlarmBean> value,int no) {
		if(no == 1){
			areaAlarmMap1.put(key, value);
		}else{
			areaAlarmMap2.put(key, value);
		}
	}

	public static void clearAreaAlarmMap() {
		if(areaAlarmMapNo == 1){
			areaAlarmMap2.clear();
		}else{
			areaAlarmMap1.clear();
		}
	}
	
	public static void copyAreaAlarmMap() {
		if(areaAlarmMapNo == 1){
			areaAlarmMap2.putAll(areaAlarmMap1);
		}else{
			areaAlarmMap1.putAll(areaAlarmMap2);
		}
	}

	public static int getAreaAlarmMapNo() {
		return areaAlarmMapNo;
	}

	public static void setAreaAlarmMapNo(int areaAlarmMapNo) {
		TempMemory.areaAlarmMapNo = areaAlarmMapNo;
	}

	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int lineAlarmMapNo = 1;
	/** VID对应线路报警设置Map */
	private static Map<String, List<LineAlarmBean>> lineAlarmMap1 = new ConcurrentHashMap<String, List<LineAlarmBean>>();
	private static Map<String, List<LineAlarmBean>> lineAlarmMap2 = new ConcurrentHashMap<String, List<LineAlarmBean>>();

	public static List<LineAlarmBean> getLineAlarmMap(String key) {
		if(lineAlarmMapNo == 1){
			return lineAlarmMap1.get(key);
		}else{
			return lineAlarmMap2.get(key);
		}
	}

	public static void setLineAlarmMap(String key, List<LineAlarmBean> value) {
		if(lineAlarmMapNo == 1){
			lineAlarmMap1.put(key, value);
		}else{
			lineAlarmMap2.put(key, value);
		}
	}
	
	public static List<LineAlarmBean> getLineAlarmMap(String key,int no) {
		if(no == 1){
			return lineAlarmMap1.get(key);
		}else{
			return lineAlarmMap2.get(key);
		}
	}

	public static void setLineAlarmMap(String key, List<LineAlarmBean> value,int no) {
		if(no == 1){
			lineAlarmMap1.put(key, value);
		}else{
			lineAlarmMap2.put(key, value);
		}
	}
	
	public static void clearLineAlarmMap() {
		if(lineAlarmMapNo == 1){
			lineAlarmMap2.clear();
		}else{
			lineAlarmMap1.clear();
		}
	}

	public static int getLineAlarmMapNo() {
		return lineAlarmMapNo;
	}

	public static void setLineAlarmMapNo(int lineAlarmMapNo) {
		TempMemory.lineAlarmMapNo = lineAlarmMapNo;
	}

	//areaTreeNo：当前可用来查询的areaTree编号
	private static int areaTreeNo = 1;
	private static STRtree areaTree01 = new STRtree();
	private static STRtree areaTree02 = new STRtree();

	@SuppressWarnings("unchecked")
	public static List<String> getAreaTree(Envelope searchEnv) {
		if(areaTreeNo==1){
			return areaTree01.query(searchEnv);
		}else{
			return areaTree02.query(searchEnv);
		}
	}

	public static void setAreaTree() {
		if(areaTreeNo==1){
			areaTree02 = new STRtree();
			buildAreaTree(areaTree02,2);
		}else{
			areaTree01 = new STRtree();
			buildAreaTree(areaTree01,1);
		}
	}
	
	/** 构建区域Tree */
	private static void buildAreaTree(STRtree areaTree,int no){
		logger.info("正在构建区域>"+no+"号");
		Map<String, List<AreaAlarmBean>> areaAlarmMap = areaAlarmMapNo==1?areaAlarmMap1:areaAlarmMap2;//获得当前所使用的对象
		for (Map.Entry<String, List<AreaAlarmBean>> m : areaAlarmMap.entrySet()) {
			try {
				List<AreaAlarmBean> list = m.getValue();
				for (AreaAlarmBean areaAlarmBean : list) {
					List<String> lonlat = areaAlarmBean.getLonlats();
					Coordinate[] rects = new Coordinate[lonlat.size()];
					for (int i = 0; i < lonlat.size(); i++) {
						rects[i] = new Coordinate(
								Double.parseDouble(lonlat.get(i).split(",")[0]) / 600000.0,
								Double.parseDouble(lonlat.get(i).split(",")[1]) / 600000.0);
					}

					String areasharp = areaAlarmBean.getAreasharp();
					Coordinate[] coords = null;
					GeometryFactory geoFactory = new GeometryFactory();
					Geometry geometryFactory = null;
					if (areasharp.equals("5")) {// 矩形
						coords = new Coordinate[5];
						coords[0] = rects[0];// 左下角
						coords[1] = new Coordinate(rects[1].x, rects[0].y);// 右下角
						coords[2] = rects[1];// 右上角
						coords[3] = new Coordinate(rects[0].x, rects[1].y);// 左上角
						coords[4] = rects[0];// 左下角，闭合面
						geometryFactory = geoFactory.createPolygon(geoFactory.createLinearRing(coords), null);// 构建面
					} else if (areasharp.equals("2")) {// 多边形
						rects = Arrays.copyOf(rects, rects.length + 1);
						rects[rects.length - 1] = rects[0];
						geometryFactory = geoFactory.createPolygon(
								geoFactory.createLinearRing(rects), null);// 构建面
					} else {
						continue;
					}
					areaTree.insert(geometryFactory.getEnvelopeInternal(), areaAlarmBean.getVid() + "_" + areaAlarmBean.getAreaid());
				}
			} catch (Exception e) {
				logger.error("拼装围栏信息时出错！"+m+"\r\n"+ExceptionUtil.getErrorStack(e, 0));
				e.printStackTrace();
			}
		}
		areaTree.build();
		logger.info("areaTreeSize:" + areaTree.size()+";areaTreeNo:"+no);
		areaTreeNo = no;//切换
	}
	

	//lineTreeNo：当前可用来查询的lineTree编号
	private static int lineTreeNo = 1;
	private static STRtree lineTree01 = new STRtree();
	private static STRtree lineTree02 = new STRtree();

	@SuppressWarnings("unchecked")
	public static List<String> getLineTree(Envelope searchEnv) {
		if(lineTreeNo==1){
//			logger.info("正在查询lineTree1中的数据");
			return lineTree01.query(searchEnv);
		}else{
//			logger.info("正在查询lineTree2中的数据");
			return lineTree02.query(searchEnv);
		}
	}

	public static void setLineTree() {
		if(lineTreeNo==1){
			lineTree02 = new STRtree();
			buildLineTree(lineTree02,2);
//			logger.info("构建lineTree02完毕。将当前lineTree查询切换到2号");
		}else{
			lineTree01 = new STRtree();
			buildLineTree(lineTree01,1);
//			logger.info("构建lineTree01完毕。将当前lineTree查询切换到1号");
		}
	}
	/** 构建线段Tree */
	private static void buildLineTree(STRtree lineTree,int no){
		logger.info("正在构建线段："+no);
		BufferParameters buff = new BufferParameters(BufferParameters.DEFAULT_QUADRANT_SEGMENTS,BufferParameters.CAP_ROUND);// 创建缓冲区
		Map<String, List<LineAlarmBean>> lineAlarmMap = lineAlarmMapNo==1?lineAlarmMap1:lineAlarmMap2;//获取当前可使用的对象
		for (Map.Entry<String, List<LineAlarmBean>> m : lineAlarmMap.entrySet()) {
			try {
				List<LineAlarmBean> list = m.getValue();
				for (LineAlarmBean lineAlarmBean : list) {
					List<String> lonlat = lineAlarmBean.getLonlats();
					Coordinate[] coords = new Coordinate[lonlat.size()];
					for (int i = 0; i < lonlat.size(); i++) {
						String tmpLonLatStr = lonlat.get(i);
						if (tmpLonLatStr!=null){
							String tmpLonLatGroup[] = tmpLonLatStr.split(",");
							if (tmpLonLatGroup!=null&&tmpLonLatGroup.length==2){
								coords[i] = new Coordinate(
										Double.parseDouble(tmpLonLatGroup[0]) / 600000.0,
										Double.parseDouble(tmpLonLatGroup[1]) / 600000.0);
							}else{
								logger.info("线路经纬度信息不配对。");
							}
						}else{
							logger.info("线路经纬度信息为空。");
						}
					}
					
					if (coords.length>1){//多个点才能构成折线
						GeometryFactory geometryFactory = new GeometryFactory();
						LineString lineString = geometryFactory.createLineString(coords);// 构造折线
						BufferOp buffer = new BufferOp(lineString, buff);
						Geometry geometry = buffer.getResultGeometry(lineAlarmBean.getRoadwight() / 111000D);// 模糊处理
						lineTree.insert(geometry.getEnvelopeInternal(),lineAlarmBean.getVid() + "_"+ lineAlarmBean.getPid());
					}
				}
			} catch (Exception e) {
				logger.info("拼装线路数据时出错："+m+"\r\n"+ExceptionUtil.getErrorStack(e, 0));
			}
		}
		lineTree.build();
		logger.info("lineTree.size():" + lineTree.size()+";lineTreeNo:"+no);
		lineTreeNo = no;//切换
	}
	
	
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int illeOptAlarmMapNo = 1;
	/** VID对应[非法运营软报警]设置Map */
	private static Map<String, IllegalOptionsAlarmBean> illeOptAlarmMap1 = new ConcurrentHashMap<String, IllegalOptionsAlarmBean>();
	private static Map<String, IllegalOptionsAlarmBean> illeOptAlarmMap2 = new ConcurrentHashMap<String, IllegalOptionsAlarmBean>();

	public static Map<String, IllegalOptionsAlarmBean> getIlleOptAlarmMap(int no) {
		if(no == 1){
			return illeOptAlarmMap1;
		}else{
			return illeOptAlarmMap2;
		}
	}
	
	public static IllegalOptionsAlarmBean getIlleOptAlarmMap(String key) {
		if(illeOptAlarmMapNo == 1){
			return illeOptAlarmMap1.get(key);
		}else{
			return illeOptAlarmMap2.get(key);
		}
	}

	public static void setIlleOptAlarmMap(String key, IllegalOptionsAlarmBean value) {
		if(illeOptAlarmMapNo == 1){
			illeOptAlarmMap1.put(key, value);
		}else{
			illeOptAlarmMap2.put(key, value);
		}
	}
	
	public static IllegalOptionsAlarmBean getIlleOptAlarmMap(String key,int no) {
		if(no == 1){
			return illeOptAlarmMap1.get(key);
		}else{
			return illeOptAlarmMap2.get(key);
		}
	}

	public static void setIlleOptAlarmMap(String key, IllegalOptionsAlarmBean value,int no) {
		if(no == 1){
			illeOptAlarmMap1.put(key, value);
		}else{
			illeOptAlarmMap2.put(key, value);
		}
	}

	public static void clearIlleOptAlarmMap() {
		if(illeOptAlarmMapNo == 1){
			illeOptAlarmMap2.clear();
		}else{
			illeOptAlarmMap1.clear();
		}
	}
	
	public static void putAllIlleOptAlarmMap(Map map,int no) {
		if(no == 1){
			illeOptAlarmMap1.putAll(map);
		}else{
			illeOptAlarmMap2.putAll(map);
		}
	}

	public static int getIlleOptAlarmMapNo() {
		return illeOptAlarmMapNo;
	}

	public static void setIlleOptAlarmMapNo(int illeOptAlarmMapNo) {
		TempMemory.illeOptAlarmMapNo = illeOptAlarmMapNo;
	}
		
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int orgAlarmConfMapNo = 1;
	/** VID对应[非法运营软报警]设置Map */
	private static Map<String, OrgAlarmConfBean> orgAlarmConfMap1 = new ConcurrentHashMap<String, OrgAlarmConfBean>();
	private static Map<String, OrgAlarmConfBean> orgAlarmConfMap2 = new ConcurrentHashMap<String, OrgAlarmConfBean>();

	public static OrgAlarmConfBean getOrgAlarmConfMap(String key) {
		if(orgAlarmConfMapNo == 1){
			return orgAlarmConfMap1.get(key);
		}else{
			return orgAlarmConfMap2.get(key);
		}
	}

	public static void setOrgAlarmConfMap(String key, OrgAlarmConfBean value) {
		if(orgAlarmConfMapNo == 1){
			orgAlarmConfMap1.put(key, value);
		}else{
			orgAlarmConfMap2.put(key, value);
		}
	}
	
	public static OrgAlarmConfBean getOrgAlarmConfMap(String key,int no) {
		if(no == 1){
			return orgAlarmConfMap1.get(key);
		}else{
			return orgAlarmConfMap2.get(key);
		}
	}

	public static void setOrgAlarmConfMap(String key, OrgAlarmConfBean value,int no) {
		if(no == 1){
			orgAlarmConfMap1.put(key, value);
		}else{
			orgAlarmConfMap2.put(key, value);
		}
	}

	public static void clearOrgAlarmConfMap() {
		if(orgAlarmConfMapNo == 1){
			orgAlarmConfMap2.clear();
		}else{
			orgAlarmConfMap1.clear();
		}
	}

	public static int getOrgAlarmConfMapNo() {
		return orgAlarmConfMapNo;
	}

	public static void setOrgAlarmConfMapNo(int orgAlarmConfMapNo) {
		TempMemory.orgAlarmConfMapNo = orgAlarmConfMapNo;
	}
	public static void main(String[] args) {
		STRtree lineTree = new STRtree();
		Geometry point = new GeometryFactory().createPoint(new Coordinate(69811584 / 600000.0, 23989290 / 600000.0));
		Geometry point2 = new GeometryFactory().createPoint(new Coordinate(79811584 / 600000.0, 33989290 / 600000.0));
		lineTree.insert(point.getEnvelopeInternal(), 1);
		lineTree.insert(point2.getEnvelopeInternal(), 2);
		lineTree.build();
		logger.info(lineTree.size());
		
		Geometry po = new GeometryFactory().createPoint(new Coordinate(69811585 / 600000.0,	23989290 / 600000.0));
		logger.info(lineTree.query(po.getEnvelopeInternal()));
	}

	/*****************************************
	 * <li>描       述：删除vid对应非法运营软报警缓存	</li><br>
	 *  
	 * <li>参        数：@param key		vid </li><br>
	 *****************************************/
	public static void deleteIlleOptAlarmMapByVid(String vid) {
		synchronized (illeOptAlarmMap1) {
			if(illeOptAlarmMap1.containsKey(vid)){
				illeOptAlarmMap1.remove(vid);
			}
		}
		synchronized (illeOptAlarmMap2) {
			if(illeOptAlarmMap2.containsKey(vid)){
				illeOptAlarmMap2.remove(vid);
			}
		}
	}
	
	/** 线路对应的设置过告警参数的路段*/
	private static Map<String, String> lineSectionWithAlarmMap = new ConcurrentHashMap<String, String>();

	public static String getLineSectionWithAlarmMap(String key) {
		return lineSectionWithAlarmMap.get(key);
	}

	public static void setLineSectionWithAlarmMap(String key, String value) {
		lineSectionWithAlarmMap.put(key, value);
	}
	
	public static boolean containsKeyOfLineSectionWithAlarmMap(String key) {
		return lineSectionWithAlarmMap.containsKey(key);
	}

	public static void clearLineSectionWithAlarmMap() {
		lineSectionWithAlarmMap.clear();
	}
	
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int overspeedAlarmCfgMapNo = 1;
	/** VID对应[非法运营软报警]设置Map */
	private static Map<String, OverspeedAlarmCfgBean> overspeedAlarmCfgMap1 = new ConcurrentHashMap<String, OverspeedAlarmCfgBean>();
	private static Map<String, OverspeedAlarmCfgBean> overspeedAlarmCfgMap2 = new ConcurrentHashMap<String, OverspeedAlarmCfgBean>();

	public static Map<String, OverspeedAlarmCfgBean> getOverspeedAlarmCfgMap(int no) {
		if(no == 1){
			return overspeedAlarmCfgMap1;
		}else{
			return overspeedAlarmCfgMap2;
		}
	}
	
	public static OverspeedAlarmCfgBean getOverspeedAlarmCfgMap(String key) {
		if(overspeedAlarmCfgMapNo == 1){
			return overspeedAlarmCfgMap1.get(key);
		}else{
			return overspeedAlarmCfgMap2.get(key);
		}
	}

	public static void setOverspeedAlarmCfgMap(String key, OverspeedAlarmCfgBean value) {
		if(overspeedAlarmCfgMapNo == 1){
			overspeedAlarmCfgMap1.put(key, value);
		}else{
			overspeedAlarmCfgMap2.put(key, value);
		}
	}
	
	public static OverspeedAlarmCfgBean getOverspeedAlarmCfgMap(String key,int no) {
		if(no == 1){
			return overspeedAlarmCfgMap1.get(key);
		}else{
			return overspeedAlarmCfgMap2.get(key);
		}
	}

	public static void setOverspeedAlarmCfgMap(String key, OverspeedAlarmCfgBean value,int no) {
		if(no == 1){
			overspeedAlarmCfgMap1.put(key, value);
		}else{
			overspeedAlarmCfgMap2.put(key, value);
		}
	}

	public static void clearOverspeedAlarmCfgMap() {
		if(overspeedAlarmCfgMapNo == 1){
			overspeedAlarmCfgMap2.clear();
		}else{
			overspeedAlarmCfgMap1.clear();
		}
	}
	
	public static void putAllOverspeedAlarmCfgMap(Map map,int no) {
		if(no == 1){
			overspeedAlarmCfgMap1.putAll(map);
		}else{
			overspeedAlarmCfgMap2.putAll(map);
		}
	}

	public static int getOverspeedAlarmCfgMapNo() {
		return overspeedAlarmCfgMapNo;
	}

	public static void setOverspeedAlarmCfgMapNo(int overspeedAlarmCfgMapNo) {
		TempMemory.overspeedAlarmCfgMapNo = overspeedAlarmCfgMapNo;
	}
	
	
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int fatigueAlarmCfgMapNo = 1;
	/** VID对应[非法运营软报警]设置Map */
	private static Map<String, FatigueAlarmCfgBean> fatigueAlarmCfgMap1 = new ConcurrentHashMap<String, FatigueAlarmCfgBean>();
	private static Map<String, FatigueAlarmCfgBean> fatigueAlarmCfgMap2 = new ConcurrentHashMap<String, FatigueAlarmCfgBean>();

	public static Map<String, FatigueAlarmCfgBean> getFatigueAlarmCfgMap(int no) {
		if(no == 1){
			return fatigueAlarmCfgMap1;
		}else{
			return fatigueAlarmCfgMap2;
		}
	}
	
	public static FatigueAlarmCfgBean getFatigueAlarmCfgMap(String key) {
		if(fatigueAlarmCfgMapNo == 1){
			return fatigueAlarmCfgMap1.get(key);
		}else{
			return fatigueAlarmCfgMap2.get(key);
		}
	}

	public static void setFatigueAlarmCfgMap(String key, FatigueAlarmCfgBean value) {
		if(fatigueAlarmCfgMapNo == 1){
			fatigueAlarmCfgMap1.put(key, value);
		}else{
			fatigueAlarmCfgMap2.put(key, value);
		}
	}
	
	public static FatigueAlarmCfgBean getFatigueAlarmCfgMap(String key,int no) {
		if(no == 1){
			return fatigueAlarmCfgMap1.get(key);
		}else{
			return fatigueAlarmCfgMap2.get(key);
		}
	}

	public static void setFatigueAlarmCfgMap(String key, FatigueAlarmCfgBean value,int no) {
		if(no == 1){
			fatigueAlarmCfgMap1.put(key, value);
		}else{
			fatigueAlarmCfgMap2.put(key, value);
		}
	}

	public static void clearFatigueAlarmCfgMap() {
		if(fatigueAlarmCfgMapNo == 1){
			fatigueAlarmCfgMap2.clear();
		}else{
			fatigueAlarmCfgMap1.clear();
		}
	}
	
	public static void putAllFatigueAlarmCfgMap(Map map,int no) {
		if(no == 1){
			fatigueAlarmCfgMap1.putAll(map);
		}else{
			fatigueAlarmCfgMap2.putAll(map);
		}
	}

	public static int getFatigueAlarmCfgMapNo() {
		return fatigueAlarmCfgMapNo;
	}

	public static void setFatigueAlarmCfgMapNo(int fatigueAlarmCfgMapNo) {
		TempMemory.fatigueAlarmCfgMapNo = fatigueAlarmCfgMapNo;
	}
	
	
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int stationMapNo = 1;
	/** VID对应[站点]设置Map */
	private static Map<String, List<TbLineStationBean>> stationMap1 = new ConcurrentHashMap<String, List<TbLineStationBean>>();
	private static Map<String, List<TbLineStationBean>> stationMap2 = new ConcurrentHashMap<String, List<TbLineStationBean>>();

	public static List<TbLineStationBean> getStationMap(String key) {
		if(stationMapNo == 1){
			return stationMap1.get(key);
		}else{
			return stationMap2.get(key);
		}
	}

	public static void setStationMap(String key, List<TbLineStationBean> value) {
		if(stationMapNo == 1){
			stationMap1.put(key, value);
		}else{
			stationMap2.put(key, value);
		}
	}
	
	public static List<TbLineStationBean> getStationMap(String key,int no) {
		if(no == 1){
			return stationMap1.get(key);
		}else{
			return stationMap2.get(key);
		}
	}
	
	public static void setStationMap(String key, List<TbLineStationBean> value,int no) {
		if(no == 1){
			stationMap1.put(key, value);
		}else{
			stationMap2.put(key, value);
		}
	}

	public static void clearStationMap() {
		if(stationMapNo == 1){
			stationMap2.clear();
		}else{
			stationMap1.clear();
		}
	}
	
	public static void copyStationMap() {
		if(stationMapNo == 1){
			stationMap2.putAll(stationMap1);
		}else{
			stationMap1.putAll(stationMap2);
		}
	}

	public static int getStationMapNo() {
		return stationMapNo;
	}

	public static void setStationMapNo(int stationMapNo) {
		TempMemory.stationMapNo = stationMapNo;
	}
	
	//企业父ID缓存
	/** 当前所使用的缓存编号-用来切换同步配置 */
	private static int orgParentMapNo = 1;
	/** VID对应[站点]设置Map */
	private static Map<String, OrgParentInfo> orgParentMap1 = new ConcurrentHashMap<String, OrgParentInfo>();
	private static Map<String, OrgParentInfo> orgParentMap2 = new ConcurrentHashMap<String, OrgParentInfo>();

	public static OrgParentInfo getOrgParentMap(String key) {
		if(orgParentMapNo == 1){
			return orgParentMap1.get(key);
		}else{
			return orgParentMap2.get(key);
		}
	}

	public static void setOrgParentMap(String key, OrgParentInfo value) {
		if(orgParentMapNo == 1){
			orgParentMap1.put(key, value);
		}else{
			orgParentMap2.put(key, value);
		}
	}
	
	public static OrgParentInfo getOrgParentMap(String key,int no) {
		if(no == 1){
			return orgParentMap1.get(key);
		}else{
			return orgParentMap2.get(key);
		}
	}
	
	public static void setOrgParentMap(String key, OrgParentInfo value,int no) {
		if(no == 1){
			orgParentMap1.put(key, value);
		}else{
			orgParentMap2.put(key, value);
		}
	}

	public static void clearOrgParentMap() {
		if(orgParentMapNo == 1){
			orgParentMap2.clear();
		}else{
			orgParentMap1.clear();
		}
	}
	
	public static void copyOrgParentMap() {
		if(orgParentMapNo == 1){
			orgParentMap2.putAll(orgParentMap1);
		}else{
			orgParentMap1.putAll(orgParentMap2);
		}
	}

	public static int getOrgParentMapNo() {
		return orgParentMapNo;
	}

	public static void setOrgParentMapNo(int orgParentMapNo) {
		TempMemory.orgParentMapNo = orgParentMapNo;
	}
}
