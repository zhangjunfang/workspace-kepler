package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.dao.RedisDBAdapter;

public class TrackRedisManagerThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackRedisManagerThread.class);
	//计数器
	private int index = 0 ;
	//计时器
	private long tempTime = System.currentTimeMillis();
	// 线程id
	private int nId = 0 ;
	// 异步数据报向量
	private ArrayBlockingQueue<Map<String,String>> vPacket = new ArrayBlockingQueue<Map<String,String>>(100000);
	
	private RedisDBAdapter redisDB = null;
	
	public TrackRedisManagerThread(RedisDBAdapter redisDB, int nId){
		this.redisDB = redisDB;
		this.nId = nId;
	}
	
	public void addPacket(Map<String,String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error("",e);
		}
	}
	
	public int getPacketsSize() {
		return vPacket.size();
	}
	public void run(){
		while(TrackManagerKcptMainThread.isRunning){
			try{
				Map<String,String> app = vPacket.take();
				String isPvalid = app.get(Constant.ISPVALID);
//				if(isPvalid == null){
//					continue;
//				}
//				正常轨迹处理
				if(isPvalid.equals("0")){ 
					redisDB.setTrackInfo(app);
				}else{
//					非法轨迹处理
//					int isPValid = Integer.parseInt(isPvalid);
//					long vid = Long.parseLong(app.get(Constant.VID));
					String msgid=app.get(Constant.MSGID);
					redisDB.setTrackisPValidInfo(isPvalid, app.get(Constant.VID) ,msgid);
//					JedisConnectionServer.updateTrackValidStatus(isPvalid, app.get(Constant.VID), msgid, app.get("8"));
				}
				long currentTime = System.currentTimeMillis();
				if(currentTime - tempTime > 3000){
					logger.warn("trackredis:" + nId + ",size:" + vPacket.size() + ",3秒处理数据:"+index+"条");
					tempTime = currentTime;
					index = 0 ;
				}
				index++;
			}catch(Exception ex){
//				ex.printStackTrace(); //TODO
				logger.error("Redis位置更新线程出错."  + ex.getMessage(),ex);
			}
			
			
		}// End while
	}

}
