/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.util Cache.java	</li><br>
 * <li>时        间：2013-9-9  下午4:41:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.util;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.statusservice.model.Alarm;
import com.ctfo.statusservice.model.AlarmTypeBean;
import com.ctfo.statusservice.model.ParentInfo;
import com.ctfo.statusservice.model.ServiceUnit;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;
import com.vividsolutions.jts.index.strtree.STRtree;
import com.vividsolutions.jts.io.WKTReader;


/*****************************************
 * <li>描        述：缓存		
 * 
 *****************************************/
public class Cache {
	/** 注册车辆缓存表   */
	public static Map<String, ServiceUnit> registerVehicleMap = new ConcurrentHashMap<String, ServiceUnit>(); 
	/** 车辆上下线状态缓存   */
	private static Map<String, String> vehicleOnOfflineMap = new ConcurrentHashMap<String, String>();
	/** 车辆对应报警设置企业列表(key:vid,value:ent_id)  */
	public static Map<String, String> vidEntMap = new ConcurrentHashMap<String, String>();
	/** 企业对应报警设置告警列表(key:ent_id,value:报警code)  */
	public static Map<String,String> entAlarmMap = new ConcurrentHashMap<String,String>();
	/** 车辆状态Map */
	public static Map<String, Map<String, Alarm>> vehicleStatusMap = new ConcurrentHashMap<String, Map<String, Alarm>>();
	/** 最大转速缓存 */
	public static  Map<String, Long> vehicleMaxRpmMap = new ConcurrentHashMap<String, Long>();;
	/** 报警代码对应报警描述Map */
	private static Map<Integer, AlarmTypeBean> alarmtypeMap = new ConcurrentHashMap<Integer, AlarmTypeBean>();
	/** 全国区域拓扑树	 */
	private static STRtree areaTree = new STRtree();
	/** 缓存车辆上一次区域位置信息 ，初始化时为所属地区域位置信息	*/
	private static Map<String, String> areaLastMap = new ConcurrentHashMap<String, String>();
	/**	组织父级缓存		*/
	private static Map<String, ParentInfo> orgParentCache = new ConcurrentHashMap<String, ParentInfo>();
	
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
	 * 获取接收队列大小
	 */
	public static long getAlarmtypeMapSize() {
		return alarmtypeMap.size();
	}
	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static AlarmTypeBean getAlarmtypeMapValue(int key) {
		return alarmtypeMap.get(key);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setAlarmtypeMapValue(int key, AlarmTypeBean alarmTypeBean) {
		alarmtypeMap.put(key, alarmTypeBean);
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
	 * <li>描        述：缓存所有组织父级表		</li><br>
	 * <li>时        间：2013-10-28  下午7:36:57	</li><br>
	 * <li>参数： @param orgParent			</li><br>
	 * 
	 *****************************************/
	public static void putAllOrgParent(Map<String, ParentInfo> orgParent) {
		orgParentCache.putAll(orgParent);
	}
	/*****************************************
	 * <li>描        述：根据车队编号获取父级组织编号 		</li><br>
	 * <li>时        间：2013-10-28  下午7:39:41	</li><br>
	 * <li>参数： @param motorcade
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static ParentInfo getOrgParent(String motorcade){
		return orgParentCache.get(motorcade);
	}
	/**
	 * 清空父级组织编号
	 */
	public static void orgParentClear(){
		orgParentCache.clear(); 
	}
	
}
