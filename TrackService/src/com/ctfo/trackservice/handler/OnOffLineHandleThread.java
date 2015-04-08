/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service TrackHandleThread.java	</li><br>
 * <li>时        间：2013-9-16  下午1:40:48	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.handler;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.model.ServiceUnit;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.service.RedisService;
import com.ctfo.trackservice.util.Cache;
import com.ctfo.trackservice.util.Constant;
import com.ctfo.trackservice.util.DateTools;

/*****************************************
 * <li>描 述：轨迹位置处理线程
 * 
 *****************************************/
public class OnOffLineHandleThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(OnOffLineHandleThread.class);
	/** 数据缓冲队列 */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号 */
	private int threadId;
	/** 计数器 */
	private int index;
	/** 上线计数器 */
	private int onlineIndex;
	/** 下线计数器 */
	private int offlineIndex;
	/** 上次时间 */
	private long lastTime = System.currentTimeMillis();
	/** Onoffline Oracle服务接口	  */
	private OracleService onofflineoracle;
	/** Onoffline Oracle服务接口	  */
	private OracleService onofflineInsert;
	/** redis服务接口	  */
	private RedisService redisService;
	
	public OnOffLineHandleThread(int id, OracleService oracle) throws Exception {
		setName("OnOffLineHandleThread-" + id);
		threadId = id;
		redisService = new RedisService("OnOffLine"); 
		onofflineoracle = oracle;
		onofflineInsert = new OracleService();
		onofflineInsert.initOnlineStatement();  
	}

	/*****************************************
	 * <li>描 述：将数据插入队列顶部</li><br>
	 * <li>时 间：2013-9-16 下午4:42:17</li><br>
	 * <li>参数： @param dataMap</li><br>
	 * 
	 *****************************************/
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	/*****************************************
	 * <li>描 述：获得队列大小</li><br>
	 * <li>时 间：2013-9-16 下午4:42:47</li><br>
	 * <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public int getQueueSize() {
		return dataQueue.size();
	}


	public void run() {
		logger.info("更新车辆上下线线程" + threadId + "启动");
		Map<String, String> packet = null;
		String[] statusArray = new String[2]; 
		String isonline = null;
		
		while (true) {
			try {
				packet = dataQueue.take();
				String parm = packet.get(Constant.N18);
				String parms[] = parm.split(Constant.BACKSLASH);
				String status = "";
				String prompt = " 刚刚上线";
				
				if(parms.length == 4){
					status = parms[0];
				}else{
					logger.error("非法上下线数据,车牌号:{"+ packet.get(Constant.VEHICLENO) +"},数据:" + packet.get(Constant.COMMAND));
					continue;
				}
				String vid = packet.get(Constant.VID);
				String msgid = "0";
				String macid = packet.get(Constant.MACID);
				String vehicleNo = packet.get(Constant.VEHICLENO);
				Long sysTime = DateTools.getCurrentUtcMsDate();
				String sysuuid = packet.get(Constant.UUID);
				

				
//				只处理有效状态
				if (Constant.N0.equals(status) || Constant.N1.equals(status)) {
					String onlineStatus = Cache.getOnOfflineStatus(macid); 
//					车辆上过线
					if (onlineStatus != null ) {
						statusArray = StringUtils.splitPreserveAllTokens(onlineStatus, "_", 2);
						isonline = statusArray[0];
						String uuid = statusArray[1];
//						车辆状态是上线,发送上线包就存储一次下线,再存储上线
						if (Constant.N1.equals(status) && Constant.N1.equals(isonline)) {
							onofflineInsert.saveOffline(uuid);
							onofflineInsert.saveOnline(vid, vehicleNo, sysuuid);
							Cache.setOnOfflineStatus(macid, status + "_" + sysuuid);
							prompt = " 刚刚上线";
							onlineIndex++;
							offlineIndex++;
						}
//						状态是上线,发送下线包就存储下线
						if (Constant.N0.equals(status) && Constant.N1.equals(isonline)) {
							onofflineInsert.saveOffline(uuid);
							Cache.setOnOfflineStatus(macid, status + "_");
							prompt = " 刚刚下线";
							offlineIndex++;
						}
//						状态是下线，发送上线包就存储上线
						if (Constant.N1.equals(status) && Constant.N0.equals(isonline)) {
							onofflineInsert.saveOnline(vid, vehicleNo, sysuuid);
							Cache.setOnOfflineStatus(macid, status + "_" + sysuuid);
							prompt = " 刚刚上线";
							onlineIndex++;
						}
					} else {
//						车辆未上线过,发送上线包,存储上线
						if (Constant.N1.equals(status)) {
							onofflineInsert.saveOnline(vid, vehicleNo, sysuuid);
							Cache.setOnOfflineStatus(macid, status + "_" + sysuuid);
							prompt = " 刚刚上线";
							onlineIndex++;
						}
					}
				}
//				packet.put("status", status);
//				packet.put("msgId", msgid);
//				更新最后位置表的上下线状态
				onofflineoracle.updateOnOfflineStatus(Integer.parseInt(status), msgid,vid);
//				更新redis上下线状态
				redisService.setOffLine(status,sysTime, msgid, vid);
				
				ServiceUnit serviceUnit = Cache.getVehicleMapValue(macid);
//				获取车队编号
				String motorcadeKey = serviceUnit.getMotorcade();
				if(motorcadeKey != null && motorcadeKey.length() > 0){
					String promptKey = Cache.getOrgParent(motorcadeKey);// 根据车队编号获取车辆队列
					if(promptKey != null){
						prompt = vehicleNo + prompt;
						motorcadeKey = promptKey + vehicleNo;
						this.redisService.saveOnOffStatusPrompt(motorcadeKey, prompt);
					}
				}
				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastTime) > 10000) {
					logger.info("onoffline--:{} , 排队:[{}], 10s秒处理数据:[{}]条, 上线[{}]条,下线[{}]条", threadId, getQueueSize(), index, onlineIndex ,offlineIndex);
					index = 0;
					onlineIndex = 0;
					offlineIndex = 0;
					lastTime = System.currentTimeMillis();
				}
				index++;
			} catch (Exception e){
				logger.error("车辆上下线线程" + threadId + " 报文解析错误 " + e.getMessage(), e);
			}
		}
	}
}
