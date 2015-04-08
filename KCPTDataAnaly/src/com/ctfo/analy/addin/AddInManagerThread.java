package com.ctfo.analy.addin;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.Vector;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.beanutils.BeanUtils;
import org.apache.log4j.Logger;

import com.ctfo.analy.beans.Coordinates;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.util.Cache;

/**
 * Title: 插件管理类 Description: 管理轨迹分析线程，进行深入的轨迹分析
 */
public class AddInManagerThread extends Thread {
	private static final Logger logger = Logger.getLogger(AddInManagerThread.class);

	// 异步数据报向量
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);

	private Map<String, Long> tmpCache = new ConcurrentHashMap<String, Long>();
	
	// 是否运行标志
	public boolean isRunning = true;

	public Hashtable<String, Vector<PacketAnalyser>> taAnalyser = null;
	int threadId = 0;

	public AddInManagerThread(int threadId,Hashtable<String, Vector<PacketAnalyser>> taAnalyser) {
		this.taAnalyser = taAnalyser;
		this.threadId = threadId;
	}

	public void addPacket(VehicleMessageBean vehicleMessage) {
		try {
			vPacket.put(vehicleMessage);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	/**
	 * 线程执行体
	 */
	public void run() {
		logger.info("插件管理应用启动");
		// 启动所有接收分析管理线程
		VehicleMessageBean vehicleMessage = null;
		Vector<PacketAnalyser> vA;
		int thnum = 0;
		
		//获取插件KEY
		List<String> kyList = new ArrayList<String>();
		Set<String> st = taAnalyser.keySet();
		Iterator<String> it = st.iterator();
		while(it.hasNext()){
			kyList.add(it.next());
		}// End while
		
		while (isRunning) {
			try {
				vehicleMessage = vPacket.take();
//				String alarmType = vehicleMessage.getAlarmtype();
//				logger.info("插件管理器收到数据，准备分发给业务类。-----"+alarmType);
				//String[] typeArray = alarmType.split(",");
				//按类型进行分配
				if (isSort(vehicleMessage)){
				for (String type : kyList) {
					vA = taAnalyser.get(type);
					if (vA == null || vA.size() == 0)
						continue;
					thnum = (int) (Math.abs(vehicleMessage.getVid().hashCode()) % vA.size());
					//logger.info("插件管理器将收到的数据----分发给业务类。-----"+type+"下的子线程>>"+thnum);
					//此处需要克隆一下，然后将消息分发到不同的队列 add liangjian by 2012-11-15 15:44:29
					VehicleMessageBean vehicleMessageNew = (VehicleMessageBean)BeanUtils.cloneBean(vehicleMessage);
					processCoordinatesByACC(vehicleMessageNew,vehicleMessageNew.getVid());
					vA.elementAt(thnum).addPacket(vehicleMessageNew);
				}
				}
				// logger.debug("插件管理线程" + threadId + "," + vPacket.size());
			} catch (Exception e) {
				logger.error("插件管理线程出错.", e);
			}
		} // End While
		logger.info("插件管理服务停止");
	}
	
	/**
	 * 根据ACC状态处理经纬度坐标, 防止停车坐标偏移
	 * @param dataMap
	 * @param vid 
	 * @param commandType 
	 * 缓存车辆经纬度坐标，当ACC总电源关闭时，就取上一次的经纬度, 防止停车坐标偏移。
	 */
	private void processCoordinatesByACC(VehicleMessageBean vehicleMessage, String vid) {
		String tempStatus = vehicleMessage.getBaseStatus(); // 获取基础状态位
		Coordinates coordinates = null;
		if (tempStatus != null) {
			coordinates = Cache.getTrackCoordinates(vid); // 获取轨迹坐标缓存
			String binarystatus = Long.toBinaryString(Long.parseLong(tempStatus));
			if (binarystatus.endsWith("0")) { // 判断ACC关
			// 取上一次车辆经纬度 - 缓存为空就缓存当前经纬度，不为空就替换数据包中的经纬度
				if (coordinates == null) {
					long lon = vehicleMessage.getLon();
					long lat = vehicleMessage.getLat();
					long maplon = vehicleMessage.getMaplon();
					long maplat = vehicleMessage.getMaplat();
					coordinates = new Coordinates();
					coordinates.setLon(lon);
					coordinates.setLat(lat);
					coordinates.setMaplon(maplon);
					coordinates.setMaplat(maplat);
					Cache.setTrackCoordinates(vid, coordinates);
				} else {
					vehicleMessage.setLat(coordinates.getLat());
					vehicleMessage.setLon(coordinates.getLon());
					vehicleMessage.setMaplat(coordinates.getMaplat());
					vehicleMessage.setMaplon(coordinates.getMaplon());
				}
			} else {
				// 缓存车辆经纬度 - 缓存为空就创建新坐标对象缓存，不为空就替换原经纬度
				if (coordinates != null) {
					Cache.removeTrackCoordinates(vid);
				}
			}
		}
	}
	
	/**
	 * 缓存位置信息汇报时间点,判断位置汇报数据是否顺序报送，排除补报数据干扰
	 * @return
	 */
	private boolean isSort(VehicleMessageBean vehicleMessage){
		if (vehicleMessage==null){
			return false;
		}
		
		String msgType = vehicleMessage.getMsgType();
		if ("0".equals(msgType)||"1".equals(msgType)){
			
			String vid = vehicleMessage.getVid();
			Long utc = vehicleMessage.getUtc();
			
			if (utc==null){
				return false;
			}
			
			String key = vid + "_utc";
			if (!tmpCache.containsKey(key)){
				tmpCache.put(key, utc);
				return true;
			}
			
			if (utc>=(tmpCache.get(key))){
				tmpCache.put(key, utc);
				return true;
			}else{
				//补传位置信息汇报数据返回false
				return false;
			}
		}
		
		return true;
	}
}
