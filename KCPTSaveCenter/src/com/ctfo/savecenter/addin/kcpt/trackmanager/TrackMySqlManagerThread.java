package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.dao.TrackManagerKcptMysqlDBAdapter;


/****
 * MYSQL 处理轨迹数据
 * @author robin
 *
 */
public class TrackMySqlManagerThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackMySqlManagerThread.class);
	private TrackManagerKcptMysqlDBAdapter mysqlDB = null;
	
	// 异步数据报向量
	private ArrayBlockingQueue<Map<String,String>> vPacket = new ArrayBlockingQueue<Map<String,String>>(100000);
	
	public TrackMySqlManagerThread(TrackManagerKcptMysqlDBAdapter mysqlDB){
		this.mysqlDB = mysqlDB;
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
				if(app.get(Constant.ISPVALID) == null){
					if(app.get("509") != null){ // 判断冷却液温度为准，是否上报总线数据
						mysqlDB.mysqlUpdateLastTrackLine(app);
					}else{
						mysqlDB.mysqlUpdateLastTrack(app);
					}
				}else{
					int isPValid = Integer.parseInt(app.get(Constant.ISPVALID));
					long vid = Long.parseLong(app.get(Constant.VID));
					String msgid=app.get(Constant.MSGID);
					mysqlDB.updateLastTrackISonLine(isPValid,vid,msgid);
				}
			}catch(Exception ex){
				logger.error("Mysql位置更新线程出错." + ex.getMessage());
			}
		}// End while
	}
}
