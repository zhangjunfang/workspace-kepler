/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse AllocationInstruction.java	</li><br>
 * <li>时        间：2013-9-9  下午2:06:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.parse;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.dao.OracleConnectionPool;
import com.ctfo.filesaveservice.handler.BlindFileHandleThread;
import com.ctfo.filesaveservice.handler.EloadFileHandleThread;
import com.ctfo.filesaveservice.handler.EventFileHandleThread;
import com.ctfo.filesaveservice.handler.OilFileHandleThread;
import com.ctfo.filesaveservice.handler.TrackProcessThread;
import com.ctfo.filesaveservice.model.Coordinates;
import com.ctfo.filesaveservice.util.Cache;
import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;

/*****************************************
 * <li>描        述：指令分配线程		
 * 
 *****************************************/
public class AllocationInstructionThread  extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(AllocationInstructionThread.class);
	/** 数据缓冲队列  */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程数量	  */
	private int threadSize = 5;
	/** 负载均衡MD5 */
	private MessageDigest md5;
	/** 负载均衡显示(默认不显示) */
	private boolean loadDisplay = false;
	/** 负载列表  */
	private Map<String, Integer> map = new HashMap<String, Integer>();
	/** 负载打印最后时间 */
	private long  modListTime = System.currentTimeMillis();
	/** 盲区补传处理线程组	  */
	private BlindFileHandleThread[] blindFileHandleThreads;
//	/** 轨迹处理线程组	  */
//	private TrackFileHandleThread[] trackFileHandleThreads;
	/** 轨迹处理线程组	  */
	private TrackProcessThread[] trackProcessThreads;
	/** 驾驶行为事件线程组  */
	private EventFileHandleThread[] eventFileHandleThreads;
	/** 油量监控处理线程组	  */
	private OilFileHandleThread[] oilFileHandleThreads;
	/** 发动机负荷数据处理线程组	  */
	private EloadFileHandleThread[] eloadFileHandleThreads;
	/*****************************************
	 * <li>描        述：初始化指令分配线程			</li><br>
	 * <li>参        数：@param threadSize 		线程数量
	 * <li>参        数：@param trackfileurl	轨迹数据目录
	 * <li>参        数：@param alarmFileUrl	报警数据目录
	 * <li>参        数：@param trackSubmitFrequency	轨迹提交间隔
	 * <li>参        数：@param blindTrackfileurl		盲区补传轨迹目录
	 * <li>参        数：@param blindAlarmFileUrl		盲区补传报警目录
	 * <li>参        数：@param blindSubmitFrequency	盲区补传数据提交间隔
	 * <li>参        数：@param eventFilePath			驾驶行为事件文件目录
	 * <li>参        数：@param eloadFilePath			发动机负荷率文件目录
	 * <li>参        数：@param oilFilePath				油量监控文件目录
	 * <li>参        数：@throws Exception
	 *****************************************/
	public AllocationInstructionThread() throws Exception{
		super("AllocationInstructionThread");
		this.threadSize = Integer.parseInt(ConfigLoader.fileParamMap.get("threadSize"));
		this.loadDisplay = Boolean.parseBoolean(ConfigLoader.fileParamMap.get("loadDisplay"));
		try {
			if (threadSize == 0) {
				throw new Exception("指令分配启动失败,线程启动数为0");
			}
			try {
				md5 = MessageDigest.getInstance("MD5");
			} catch (NoSuchAlgorithmException e) {
				throw new RuntimeException("MD5 not supported", e);
			}

			// 创建线程数组和线程
			trackProcessThreads = new TrackProcessThread[threadSize];
			// trackFileHandleThreads = new TrackFileHandleThread[threadSize];
			blindFileHandleThreads = new BlindFileHandleThread[threadSize];
			eventFileHandleThreads = new EventFileHandleThread[threadSize];
			oilFileHandleThreads = new OilFileHandleThread[threadSize];
			eloadFileHandleThreads = new EloadFileHandleThread[threadSize];
			for (int i = 0; i < this.threadSize; i++) {
				trackProcessThreads[i] = new TrackProcessThread(i);
				trackProcessThreads[i].start();
				// trackFileHandleThreads[i] = new TrackFileHandleThread(i);
				// trackFileHandleThreads[i].start();
				blindFileHandleThreads[i] = new BlindFileHandleThread(i);
				blindFileHandleThreads[i].start();
				eventFileHandleThreads[i] = new EventFileHandleThread(i);
				eventFileHandleThreads[i].start();
				oilFileHandleThreads[i] = new OilFileHandleThread(i);
				oilFileHandleThreads[i].start();
				eloadFileHandleThreads[i] = new EloadFileHandleThread(i);
				eloadFileHandleThreads[i].start();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public void addData(Map<String, String> data) {
		try {
			dataQueue.put(data); 
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
	public int getQueueSize() {
		return dataQueue.size();
	}
	public void run(){
		String commandType = null;
		String vid = null;
		Map<String, String> dataMap = null;
		int threadId = 0;
		while (true) {
			try{
				dataMap = dataQueue.take();
				commandType = dataMap.get("TYPE");
				vid = dataMap.get("vid");
				threadId = hashMod(vid, threadSize); // 线程负载均衡算法
//				轨迹文件
				if(commandType.equals("0")){
					processCoordinatesByACC(dataMap, vid, commandType);
					trackProcessThreads[threadId].putDataMap(dataMap);
					continue;
				}
//				盲区补传
				if(commandType.equals("7")){
					blindFileHandleThreads[threadId].putDataMap(dataMap);
					trackProcessThreads[threadId].putDataMap(dataMap);
					continue;
				} 
//				驾驶行为事件
				if(commandType.equals("52") && dataMap.containsKey("516")){
					eventFileHandleThreads[threadId].putDataMap(dataMap);
					continue;
				}
//				发动机负荷率分布表数据
				if(commandType.equals("50") && dataMap.get("513").equals("1") && dataMap.containsKey("514")){ 
					eloadFileHandleThreads[threadId].putDataMap(dataMap);
					continue;
				} 
//				油量监控
				if(commandType.equals("9") && "130".equals(dataMap.get("91")) && dataMap.containsKey("90")){ 
					oilFileHandleThreads[threadId].putDataMap(dataMap);
					continue;
				}
				// 显示负载分布状态
				if(loadDisplay){
					String node = String.valueOf(threadId);
					Integer i = map.get(node);
					if(i == null){
						map.put(node, 1);
					} else {
						map.put(node, i + 1);
					}
					
					long current = System.currentTimeMillis();
					if((current - modListTime) > 60000){
						StringBuffer sb = new StringBuffer();
						for(Map.Entry<String, Integer> m : map.entrySet()){
							sb.append("----modstatus----Thread-"+ m.getKey() + "---size:"+ m.getValue() + "\r\n");
						}
						logger.info("\r\n----modstatus----1分钟内各线程命中状态-------\r\n" + sb.toString());
						logger.info(OracleConnectionPool.getPoolStackTrace()); 
						map.clear();
						modListTime = System.currentTimeMillis();
					}
				}
			}catch(Exception e){
				logger.error("指令分配线程异常:"+e.getMessage(), e);
			}
		}
	}
	/**
	 * 根据ACC状态处理经纬度坐标, 防止停车坐标偏移
	 * @param dataMap
	 * @param vid 
	 * @param commandType 
	 * 缓存车辆经纬度坐标，当ACC总电源关闭时，就取上一次的经纬度, 防止停车坐标偏移。
	 */
	private void processCoordinatesByACC(Map<String, String> dataMap, String vid, String commandType) {
		String tempStatus = dataMap.get("8"); // 获取基础状态位
		Coordinates coordinates = null;
		if (tempStatus != null) {
			coordinates = Cache.getTrackCoordinates(vid); // 获取轨迹坐标缓存
			String binarystatus = Long.toBinaryString(Long.parseLong(tempStatus));
			if (binarystatus.endsWith("0")) { // 判断ACC关
			// 取上一次车辆经纬度 - 缓存为空就缓存当前经纬度，不为空就替换数据包中的经纬度
				if (coordinates == null) {
					String lon = dataMap.get("1");
					String lat = dataMap.get("2");
					String maplon = dataMap.get(Constant.MAPLON);
					String maplat = dataMap.get(Constant.MAPLAT);
					coordinates = new Coordinates();
					coordinates.setLon(lon);
					coordinates.setLat(lat);
					coordinates.setMaplon(maplon);
					coordinates.setMaplat(maplat);
					Cache.setTrackCoordinates(vid, coordinates);
				} else {
					dataMap.put("1", coordinates.getLon());
					dataMap.put("2", coordinates.getLat());
					dataMap.put(Constant.MAPLON, coordinates.getMaplon());
					dataMap.put(Constant.MAPLAT, coordinates.getMaplat());
				}
			} else {
				// 缓存车辆经纬度 - 缓存为空就创建新坐标对象缓存，不为空就替换原经纬度
				if (coordinates != null) {
					Cache.removeTrackCoordinates(vid);
				}
			}
			// logger.info("当前轨迹坐标缓存数[{}], 当前盲区坐标缓存数[{}]" ,
		}
	}
	
	/**
	 * 负载均衡算法 - 统一哈希算法
	 * @param vId
	 * @return
	 */
	private int hashMod(String vId,int threadSize) {
		byte[] b = computeMd5(vId);
		int hashNum = (int) (hash(b, 1) %  threadSize);
		return Math.abs(hashNum); 
	}
	/**
	 * 获取MD5密钥
	 */
	public byte[] computeMd5(String key) {
		md5.reset();
		byte[] keyBytes = null;
		try {
			keyBytes = key.getBytes("UTF-8");
		} catch (UnsupportedEncodingException e) {
			//发送异常时默认使用0线程进行处理
			keyBytes = new byte[]{-124, -119, -108, -56, 93, -64, -103, -19, -128, 70, -20, 40, 86, 40, 84, -60};
		}
		md5.update(keyBytes);
		return md5.digest();
	}
	/**
	 * 获取MD5的HASH域值
	 * @param digest
	 * @param nTime
	 * @return
	 */
	public long hash(byte[] digest, int nTime) {
		long rv = ((long) (digest[3+nTime*4] & 0xFF) << 24)
				| ((long) (digest[2+nTime*4] & 0xFF) << 16)
				| ((long) (digest[1+nTime*4] & 0xFF) << 8)
				| (digest[0+nTime*4] & 0xFF);
		
		return rv & 0xffffffffL; /* Truncate to 32-bits */
	}

}
