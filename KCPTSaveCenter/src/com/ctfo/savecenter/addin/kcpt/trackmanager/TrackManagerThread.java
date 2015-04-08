package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;

/****
 * ORACLE 处理轨迹位置
 * @author robin
 *
 */
public class TrackManagerThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackManagerThread.class);
	private int nId = 0;
	// 异步数据报向量
	private ArrayBlockingQueue<Map<String,String>> vPacket = new ArrayBlockingQueue<Map<String,String>>(100000);
	
	private TrackManagerKcptDBAdapter oracleDB = null;
	
	public TrackManagerThread(TrackManagerKcptDBAdapter oracleDB, int nId){
		this.oracleDB = oracleDB;
		this.nId = nId;
	}
	
	public void addPacket(Map<String,String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error("更新最后位置信息异常:",e);
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}
	int index = 0;
	int index2 = 0;
	long time = System.currentTimeMillis();
	public void run(){
	
		while(TrackManagerKcptMainThread.isRunning){
			try{
				Map<String,String> app = vPacket.take();
				String mileage = app.get("9");
				if(!StringUtils.isNumeric(mileage)){
//					logger.warn("里程数据为空:"+app.get("9"));
					app.put("9", "-1");
				}
				String isPvalidStr = app.get(Constant.ISPVALID);
				if(!StringUtils.isNumeric(isPvalidStr)){
					continue; 
				}
				String vidStr = app.get(Constant.VID);
				if(!StringUtils.isNumeric(vidStr)){
					continue; 
				}
				if(isPvalidStr.equals("0")){
					if(app.get("509") != null){ // 判断冷却液温度为准，是否上报总线数据
						oracleDB.updateLastTrackLine(app);
					}else{
						oracleDB.updateLastTrack(app); 
					}
					index++;
				}else{
					int isPValid = Integer.parseInt(isPvalidStr);
					long vid = Long.parseLong(vidStr);
					String msgid=app.get(Constant.MSGID);
					oracleDB.saveDumpTrack(app, isPValid);
					oracleDB.updateLastTrackISonLine(isPValid,vid,msgid); 
					index2++;
				} 
				long temp = System.currentTimeMillis();
				if((temp - time) >= 3000){
					logger.warn("trackupdat:" + nId + ",size:" + vPacket.size()+",3s更新合法轨迹数量:"+index+",非法轨迹数量:"+index2);
					time = temp;
					index = 0;
					index2 = 0;
				}
			}catch(Exception ex){
//				ex.printStackTrace(); //TODO
				logger.error("位置更新线程出错."+ex.getMessage(),ex);
			}
			
		}// End while
	}
	
}
