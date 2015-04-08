/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service RedisTrackHandleThread.java	</li><br>
 * <li>时        间：2013-9-16  下午1:41:02	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.handler;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.RedisService;
import com.ctfo.trackservice.util.Constant;

/*****************************************
 * <li>描        述：轨迹redis存储线程		
 * 
 *****************************************/
public class RedisTrackHandleThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(RedisTrackHandleThread.class);
	/** 数据缓冲队列	  */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号	  */
	private int threadId;
	/** 计数器	  */
	private int index;

	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** redis数据访问接口	  */
	private RedisService redisService;
	
	public RedisTrackHandleThread(int threadId) throws Exception {
		super("RedisTrackHandleThread" + threadId);
		this.threadId = threadId;
		this.redisService = new RedisService("track"); 
	}
	/*****************************************
	 * <li>描        述：将数据插入队列顶部 		</li><br>
	 * <li>时        间：2013-9-16  下午4:42:17	</li><br>
	 * <li>参数： @param dataMap			</li><br>
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
	 * <li>描        述：获得队列大小 		</li><br>
	 * <li>时        间：2013-9-16  下午4:42:47	</li><br>
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public int getQueueSize() {
		return dataQueue.size();
	}
	
	/*****************************************
	 * <li>描        述：处理轨迹逻辑 		</li><br>
	 * <li>时        间：2013-9-16  下午4:43:13	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void run(){
		while(true){
			try{
				Map<String,String> app = dataQueue.take();
				String isPvalid = app.get("isPValid");
//				正常轨迹处理
				String vid = app.get(Constant.VID);
				if(isPvalid.equals("0")){ 
					redisService.setTrackInfo(app,vid);
				}else{
//					非法轨迹处理
					redisService.setTrackisPValidInfo(app, isPvalid, vid);
				}
				long currentTime = System.currentTimeMillis();
				if(currentTime - lastTime > 10000){
					logger.info("redistrack:[{}], 排队:[{}], 10S秒处理数据:[{}]条", threadId, getQueueSize(), index);
					lastTime = currentTime;
					index = 0 ;
				}
				index++;
			}catch(Exception ex){
				logger.error("轨迹redis存储线程异常:"  + ex.getMessage(), ex); 
				continue;
			}
		}
	}
	
}
