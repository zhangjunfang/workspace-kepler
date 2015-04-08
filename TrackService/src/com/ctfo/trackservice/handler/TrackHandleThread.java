/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service TrackHandleThread.java	</li><br>
 * <li>时        间：2013-9-16  下午1:41:02	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.handler;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.Constant;

/*****************************************
 * <li>描        述：轨迹存储线程		
 * 
 *****************************************/
public class TrackHandleThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackHandleThread.class);
	/** 数据缓冲队列	  */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号	  */
	private int threadId;
	/** 计数器	  */
	private int index;
	/** 计数器	2  */
	private int index2;
	/** 计数器3	  */
	private int index3;
	/** 计数器4	  */
	private int index4;
	/** 计数器5	  */
	private int index5;
	
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** 数据访问接口	  */
	private OracleService oracleService;
	
	public TrackHandleThread(int threadId, OracleService oracle) {
		super("TrackHandleThread-" + threadId);
		this.threadId = threadId;
		this.oracleService = oracle;
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
				Map<String, String> dataMap = dataQueue.take();
				long start = System.currentTimeMillis();
				String mileage = dataMap.get("9");
				if(!StringUtils.isNumeric(mileage)){
//					logger.warn("里程数据为空:"+app.get("9"));
					dataMap.put("9", "-1");
				}
				String isPvalidStr = dataMap.get(Constant.ISPVALID);
				String vidStr = dataMap.get(Constant.VID);
				if(isPvalidStr.equals("0")){
					if(dataMap.get("509") != null){ // 判断冷却液温度为准，是否上报总线数据
						index3++;
						oracleService.updateLastTrackLine(dataMap);
						long end = System.currentTimeMillis() - start;
						if(end > 1){
							logger.debug("trackDesc - 处理带总线数据[{}]条, 耗时:[{}]ms", index3, end);
							index3 = 0;
						}
					}else{
						oracleService.updateLastTrack(dataMap);
						index4++;
						long end = System.currentTimeMillis() - start;
						if(end > 1){
							logger.debug("trackDesc - 处理不带总线数据[{}]条, 耗时:[{}]ms", index4, end);
							index4 = 0;
						}
					}
					index++;
				} else {
					String baseStatus = dataMap.get("8");
					if(baseStatus == null){
						baseStatus = "";
					}
					oracleService.updateLastTrackISonLine(vidStr, baseStatus); 
					index2++;
					index5++;
					long end = System.currentTimeMillis() - start;
					if(end > 1){
						logger.debug("trackDesc - 处理非法数据[{}]条, 耗时:[{}]ms", index5, end);
						index5 = 0;
					}
				} 
				long currentTime = System.currentTimeMillis();
				if(currentTime - lastTime > 10000){
					logger.info("trackHandler:{},排队:[{}], 10s处理正常轨迹:[{}]条, 非法轨迹[{}]条", threadId, getQueueSize() , index, index2);
					lastTime = currentTime;
					index = 0 ;
					index2 = 0;
				}
			}catch(Exception ex){
				logger.error("轨迹存储线程异常:" + ex.getMessage(), ex); 
			}
		}
	}
	
}
