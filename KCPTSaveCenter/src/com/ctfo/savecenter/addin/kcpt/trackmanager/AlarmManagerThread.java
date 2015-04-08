package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.sql.SQLException;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.TimeoutException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.Message;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;
import com.ctfo.savecenter.dao.TrackManagerKcptMysqlDBAdapter;
import com.ctfo.savecenter.io.DataPool;
import com.ctfo.savecenter.util.CDate;
import com.ctfo.savecenter.util.Utils;

public class AlarmManagerThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(AlarmManagerThread.class);
	// 异步数据报向量
	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	private TrackManagerKcptDBAdapter oracleDB = null;

	private TrackManagerKcptMysqlDBAdapter mysqlDB = null;
	
	private int startIndex = 0;
	private int endIndex = 0;
	
	// 线程id
	private int nId = 0;
	//计数器
	private int index = 0 ;
	//计时器
	private long tempTime = System.currentTimeMillis();
	
	// 判断 数字
	public AlarmManagerThread(TrackManagerKcptDBAdapter oracleDB, TrackManagerKcptMysqlDBAdapter mysqlDB, int nId) {
		this.oracleDB = oracleDB;
		this.mysqlDB = mysqlDB;
		this.nId = nId;
	}

	public void addPacket(Map<String, String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) { 
			logger.error(""+e.getStackTrace());
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}
	long time = System.currentTimeMillis();
	public void run() {
		while (TrackManagerKcptMainThread.isRunning) {
			try {
				Map<String, String> app = vPacket.take();
				//判断非法里程数据
				String mileage = app.get("9");
				if(mileage == null){
					app.put("9", "-1");
				}else{
					if(mileage.indexOf("|") > -1){
						continue;
					} 
				}
				analysisAlarm(app);
				long currentTime = System.currentTimeMillis();
				if(currentTime - tempTime > 3000){
					logger.warn("alarm-----:" + nId + ",size:" + vPacket.size() + ",3秒处理数据:"+index+"条,存储报警开始:("+startIndex+")条,存储结束报警:("+endIndex+")条");
					tempTime = currentTime;
					index = 0 ;
					startIndex = 0;
					endIndex = 0;
				}
				index++;
			} catch (Exception ex) {
				logger.error("AlarmManagerThread报警存储线程出错:"+ex.getMessage() ,ex);
			}
		}// End while
	}

	/**
	 * 设置缓存
	 * 
	 * @param key
	 * @param value
	 */
	private void setTempMemory(String key, String value) {
		TempMemory.setVehicleAlarmMapValue(key, value);
	}

	/**
	 * 获取缓存
	 * 
	 * @param key
	 * @param value
	 */
	private String getTempMemory(String key) {
		return TempMemory.getVehicleAlarmMapValue(key);
	}
	
	public boolean tempMemoryContain(String key){
		return TempMemory.vehicleAlarmMapContain(key);
	}

	private void analysisAlarm(Map<String, String> app) {
		//车速
		Long spd = Utils.getSpeed(app);
		Map<String, String> map = null;
		String alarmCode=null;
//		logger.debug("-------------alarmcode----"+app.get(Constant.COMMDR)+"-----收到报警:"+app.get(Constant.COMMAND)); 
		String mac_id = app.get(Constant.MACID);
		if(null != mac_id){
			map = TempMemory.vehicleStatusMap.get(mac_id);
		}else{
			return ;
		}
		if(map==null){
			map=new ConcurrentHashMap<String,String>();
			if(null != app.get(Constant.MACID))
			TempMemory.vehicleStatusMap.put(mac_id, map);
			
		}else{
			alarmCode = map.get(Constant.ALARMCODE);
		}
//		logger.debug("-------------alarmcode----"+app.get(Constant.COMMDR)+"-----历史报警:"+alarmCode); 
//		logger.debug("-------------alarmcode----"+app.get(Constant.COMMDR)+"-----当前报警,基础报警:"+app.get("20") +",扩展报警"+app.get("21")); 
		try {
			
			//yujch 2013-05-30  缓存车辆最大转速
//			logger.info("[vid="+app.get(Constant.VID)+"] maxrpm--缓存更新最大转速[vid="+app.get(Constant.VID)+"]：告警编码："+app.get("20")+" 转速："+app.get("210"));
			this.cacheMaxRpm(app.get(Constant.VID), "47", app.get("210"));
			if (alarmCode == null) { // 第一次收到该车数据
				String standAlarmCode = app.get("20"); // 标准报警标志位

				if (standAlarmCode != null && !standAlarmCode.equals(",")) {
					String[] standAlarmCode_arr = standAlarmCode.split(",");
					// 存储报警标志位
					for (int i = 0; i < standAlarmCode_arr.length; i++) {
						if (!standAlarmCode_arr[i].trim().equals("")) {
							String key = app.get(Constant.VID) + standAlarmCode_arr[i] + CDate.getCurrentUtcMsDate();
							mysqlDB.mysqlSaveAlarmTrack(app,standAlarmCode_arr[i],key,spd);
							oracleDB.saveAlarmTrack(app, standAlarmCode_arr[i],key,spd);
							// 设置报警缓存,VALUE为报警主键和GPS UTC时间
							setTempMemory(app.get(Constant.VID) + "_" + standAlarmCode_arr[i].trim(),key + "_" + app.get(Constant.UTC)); 
							startIndex++;
						}
					}// End for
				}

				String additionalAlarmCode = app.get("21"); // 附加报警标志
				if (additionalAlarmCode != null && !additionalAlarmCode.equals(",")) {
					String[] additionalAlarmCode_arr = additionalAlarmCode.split(",");
					// 存储附加报警标志位
					for (int i = 0; i < additionalAlarmCode_arr.length; i++) {
						if (!additionalAlarmCode_arr[i].trim().equals("")) {
							String key = app.get(Constant.VID) + additionalAlarmCode_arr[i] + CDate.getCurrentUtcMsDate();
							mysqlDB.mysqlSaveAlarmTrack(app,additionalAlarmCode_arr[i],key,spd);
							oracleDB.saveAlarmTrack(app,additionalAlarmCode_arr[i],key,spd);
							// 设置报警缓存,VALUE为报警主键和GPS UTC时间
							setTempMemory(app.get(Constant.VID) + "_" + additionalAlarmCode_arr[i],key + "_" + app.get(Constant.UTC));
							startIndex++;
							//yujch 2013-05-30 车辆发生超转告警时需要开始缓存车辆最大转速
							if ("47".equals(additionalAlarmCode_arr[i])){
//								logger.info("[vid="+app.get(Constant.VID)+"] maxrpm--开始缓存更新最大转速1："+app.get("210"));
								this.startCacheMaxRpm(app.get(Constant.VID), additionalAlarmCode_arr[i].trim(), app.get("210"));
							}
						}
					}// End for
				}

				if (additionalAlarmCode != null) {
					standAlarmCode = standAlarmCode + additionalAlarmCode;
					standAlarmCode = standAlarmCode.replaceAll("\\,\\,", ",");
				}
				if(standAlarmCode!=null)
				map.put(Constant.ALARMCODE, standAlarmCode.trim());
			} else {
//				logger.info("-------------alarmcode---------:"+alarmCode);
				dealAlarm(app, map, alarmCode,spd);
			}
		} catch (SQLException e) {
			logger.error("处理报警错误." + e.getMessage(),e);
		}
	}

	/**
	 * 报警处理
	 * 
	 * @param trackPacketBean
	 * @throws SQLException
	 * @throws TimeoutException
	 * @throws InterruptedException
	 * @throws SQLException
	 */
	private void dealAlarm(Map<String, String> app, Map<String, String> mem, String alarmCode,Long spd) throws SQLException {
		String standAlarmCode = app.get("20"); // 标准报警标志位

		if (standAlarmCode != null && !standAlarmCode.equals(",")) {

			String[] standAlarmCode_arr = standAlarmCode.split(",");

			// 存储报警标志位
			for (int i = 0; i < standAlarmCode_arr.length; i++) {
				if (!standAlarmCode_arr[i].trim().equals("")) {
					if (!alarmCode.contains("," + standAlarmCode_arr[i].trim() + ",")) { // 如果不包含，说明这一次新报警上报，则存储
						String key = app.get(Constant.VID) + standAlarmCode_arr[i].trim() + CDate.getCurrentUtcMsDate();
						mysqlDB.mysqlSaveAlarmTrack(app, standAlarmCode_arr[i],key,spd);
						oracleDB.saveAlarmTrack(app, standAlarmCode_arr[i],key,spd);
						// 设置报警缓存,VALUE为报警主键和GPS UTC时间
						setTempMemory(app.get(Constant.VID) + "_" + standAlarmCode_arr[i],key + "_" + app.get(Constant.UTC)); 
						//发送报警数据
						sendAlarm(app,key,standAlarmCode_arr[i],"0");
						startIndex++;
					} else { // 如果存在，则替换保证变量只有这次上报中没有的报警
						alarmCode = alarmCode.replaceAll(","+ standAlarmCode_arr[i].trim() + ",", ",");
					}
				}
			}// End for
		}
		
		String additionalAlarmCode = app.get("21"); // 附加报警标志
		if (additionalAlarmCode != null && !additionalAlarmCode.equals(",")) {

			String[] additionalAlarmCode_arr = additionalAlarmCode.split(",");
			// 存储附加报警标志位
			for (int i = 0; i < additionalAlarmCode_arr.length; i++) {
				if (!additionalAlarmCode_arr[i].trim().equals("")) {
					if (!alarmCode.contains("," + additionalAlarmCode_arr[i].trim() + ",")) {
						String key = app.get(Constant.VID) + additionalAlarmCode_arr[i] + CDate.getCurrentUtcMsDate();
						mysqlDB.mysqlSaveAlarmTrack(app,additionalAlarmCode_arr[i],key,spd); 
						oracleDB.saveAlarmTrack(app, additionalAlarmCode_arr[i],key,spd);
						// 设置报警缓存,VALUE为报警主键和GPS UTC时间
						setTempMemory(app.get(Constant.VID) + "_" + additionalAlarmCode_arr[i].trim(),key + "_" + app.get(Constant.UTC));
						//发送报警数据
						sendAlarm(app,key,additionalAlarmCode_arr[i],"0");
						startIndex++;
						//yujch 2013-05-30 车辆发生超转告警时需要开始缓存车辆最大转速
						if ("47".equals(additionalAlarmCode_arr[i])){
//							logger.info("[vid="+app.get(Constant.VID)+"] maxrpm--开始缓存最大转速："+app.get("210"));
							this.startCacheMaxRpm(app.get(Constant.VID), additionalAlarmCode_arr[i], app.get("210"));
						}
					} else {
						alarmCode = alarmCode.replaceAll("," + additionalAlarmCode_arr[i].trim() + ",", ",");
					}
				}
			}// End for
		}
		
		// 更新附加结束报警标志位
		String[] meadditionalAlarmCode_arr = alarmCode.split(",");
		for (int i = 0; i < meadditionalAlarmCode_arr.length; i++) {
			if (!meadditionalAlarmCode_arr[i].trim().equals("")) {
				String ky = app.get(Constant.VID) + "_" + meadditionalAlarmCode_arr[i].trim();
				if(tempMemoryContain(ky)){
					String value = getTempMemory(ky);
					String gpsUtc = value.split("_",2)[1];
					// 判断当前GPS时间是否大于上一次报警上报时间
					if(Long.parseLong(app.get(Constant.UTC)) > Long.parseLong(gpsUtc)){
						String key = value.split("_",2)[0];
						
						//yujch 2013-05-30 当超转告警结束进行保存时，移除缓存的告警期间车辆最大转速
						if ("47".equals(meadditionalAlarmCode_arr[i].trim())){
							try{
								String maxrpm_key = app.get(Constant.VID)+"_"+meadditionalAlarmCode_arr[i].trim();
								Long maxRpm = TempMemory.getVehicleMaxRpmMapValue(maxrpm_key); 
								if(maxRpm != null){
//									logger.info("[vid="+app.get(Constant.VID)+"] maxrpm--最大转速为1："+maxRpm);
									this.removeCacheMaxRpm(maxrpm_key);
//									logger.info("[vid="+app.get(Constant.VID)+"] maxrpm--最大转速为2："+maxRpm);
									app.put(Constant.MAXRPM, ""+maxRpm);
								}
							}catch(Exception e){
								app.put(Constant.MAXRPM, "0");
							}
						}
						mysqlDB.updateAlarmEndTime(app, meadditionalAlarmCode_arr[i],key,spd);
						oracleDB.updateAlarmEndTime(app, meadditionalAlarmCode_arr[i],key,spd);
						//发送报警数据
						sendAlarm(app,getTempMemory(app.get(Constant.VID) + "_" + meadditionalAlarmCode_arr[i]),meadditionalAlarmCode_arr[i],"1");
						endIndex++;
					}
					else{ // 小于则缓存上一次上报警编码，等待下次处理
						additionalAlarmCode = additionalAlarmCode + meadditionalAlarmCode_arr[i].trim() + ","; 
					}
				}
			}
		}// End for
		if (additionalAlarmCode != null) {
			standAlarmCode = standAlarmCode + additionalAlarmCode;
			standAlarmCode = standAlarmCode.replaceAll("\\,\\,", ",");
		}
		if(standAlarmCode!=null){
			mem.put(Constant.ALARMCODE, standAlarmCode.trim());
		}
//		logger.debug("-------------alarmcode----"+app.get(Constant.COMMDR)+"-----报警处理结束,历史报警:"+standAlarmCode); 
		
		
	}
	
	/**
	 * 发送报警处理协议
	 * @param app
	 * @param alarmid
	 * @param alarmcode
	 * @param status
	 */
	private void sendAlarm(Map<String, String> app,String alarmid,String alarmcode,String status){
		if(alarmid!=null&&alarmcode!=null){
			Message message=new Message();
			message.setCommand(message.returnCommand(app.get(Constant.COMMAND), alarmid, alarmcode, status));
			message.setMsgid(app.get(Constant.MSGID));
			DataPool.addSendPacket(message);
		}
	}
	
	/**
	 * 开始缓存告警时车辆最大转速
	 * @param vid
	 * @param alarmCode
	 * @param rpm
	 */
	private void startCacheMaxRpm(String vid,String alarmCode,String rpm){
		String key = vid+"_"+alarmCode;
		long tmp = 0L;
		if (rpm!=null&&!"".equals(rpm)){
			tmp = Long.parseLong(rpm);
		}
		
		TempMemory.setVehicleMaxRpmValue(key, tmp);
	}
	
	/**
	 * 计算车辆告警期间内最大转速，并更新缓存
	 * @param vid
	 * @param alarmCode
	 * @param rpm
	 */
	private void cacheMaxRpm(String vid,String alarmCode,String rpm){
		String key = vid+"_"+alarmCode;
		long tmp = 0L;
		if (rpm!=null&&!"".equals(rpm)){
			tmp = Long.parseLong(rpm);
		}
		if (TempMemory.vehicleMaxRpmMapContain(key)){
			long maxrpm = TempMemory.getVehicleMaxRpmMapValue(key);
			
			if (maxrpm<tmp){
				TempMemory.setVehicleMaxRpmValue(key, tmp);
			}
		}
	}
	
	/**
	 * 移除告警期间内车辆最大转速缓存
	 * @param vid
	 * @param alarmCode
	 */
	private void removeCacheMaxRpm(String key){
		TempMemory.removeVehicleMaxRpmValue(key);
	}
}
