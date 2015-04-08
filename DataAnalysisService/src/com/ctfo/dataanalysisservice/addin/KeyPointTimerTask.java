package com.ctfo.dataanalysisservice.addin;

import java.util.Calendar;
import java.util.List;
import java.util.TimerTask;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.dataanalysisservice.PermeterInit;
import com.ctfo.dataanalysisservice.beans.KeyPointDataObject;
import com.ctfo.dataanalysisservice.beans.PlatAlarmTypeUtil;
import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.io.DataPool;
import com.ctfo.dataanalysisservice.mem.MemManager;
import com.ctfo.dataanalysisservice.util.Base64_URl;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

public class KeyPointTimerTask extends TimerTask {
	
	
	public static boolean isFinished = false;
	
	// 关键点的在某个时间点内应该到达的车辆列表
	private List<String> vids;
	// 日志记录
	private final static Log log = LogFactory.getLog(KeyPointTimerTask.class);
	// 获取关键点的在某个时间点内应该到达的车辆列表
	public KeyPointTimerTask(List<String> vids) {
		this.vids = vids;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see java.util.TimerTask#run()
	 */
	@Override
	public void run() {
		for (String vid : vids) {
			log.debug("Key point timer is run...." + vid);
			// 配置关键点的key.
			String key = PlatAlarmTypeUtil.KEY_WORD + "_" + vid + "_" + PlatAlarmTypeUtil.PLAT_KEY_POINT_ALARM;
			List<KeyPointDataObject> list = MemManager.getKeyPointMap(key);
			ThVehicleAlarm thVehicleAlarm = null;
			if (list != null) {
				VehicleMessage mess = DataPool.getKeyPointPacket(key);
				for (KeyPointDataObject keyPoint : list) {
					long reachTime = timeConvertSec(keyPoint.getReachTime());
					long leaveTime = timeConvertSec(keyPoint.getLeaveTime());
					long currentTime = getCurTime();
					if (mess == null) {
						thVehicleAlarm = new ThVehicleAlarm();
						long crruentTime = System.currentTimeMillis();
						thVehicleAlarm.setVid(Long.parseLong(vid));
						thVehicleAlarm.setAlarmCode("220");
						thVehicleAlarm.setAlarmId( vid+ "220" + crruentTime );
						thVehicleAlarm.setUtc(crruentTime);
						thVehicleAlarm.setAlarmAddInfoStart("||0|"); // 0 标记进入点
						thVehicleAlarm.setBgLevel("A002");
						thVehicleAlarm.setLat(0l);
						thVehicleAlarm.setLon(0l);
						thVehicleAlarm.setMaplat(0l);
						thVehicleAlarm.setMaplon(0l);
 
						
						try {
							DataPool.setSaveDataPacket(thVehicleAlarm);
						} catch (InterruptedException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						thVehicleAlarm = new ThVehicleAlarm();
						long crruentLeaveTime = System.currentTimeMillis();
						thVehicleAlarm.setAlarmId( vid+ "220" + crruentLeaveTime );
						thVehicleAlarm.setVid(Long.parseLong(vid));
						thVehicleAlarm.setAlarmCode("220");
						thVehicleAlarm.setUtc(crruentLeaveTime);
						thVehicleAlarm.setAlarmAddInfoStart("||1|"); // 1标记离开
						thVehicleAlarm.setBgLevel("A002");
						thVehicleAlarm.setLat(0l);
						thVehicleAlarm.setLon(0l);
						thVehicleAlarm.setMaplat(0l);
						thVehicleAlarm.setMaplon(0l);
						try {
							DataPool.setSaveDataPacket(thVehicleAlarm);
						} catch (InterruptedException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
					} else {
						if(!mess.isReach() && currentTime > reachTime){ // 标记该关键点为正点到达关键点报警结束
							thVehicleAlarm = new ThVehicleAlarm();
							mess.setLeave(true);
							// 组装报警信息
							thVehicleAlarm.setAlarmId(mess.getVid() + "220" + System.currentTimeMillis());
							thVehicleAlarm.setVid(mess.getVid());
							thVehicleAlarm.setAlarmCode("220");
							thVehicleAlarm.setUtc(mess.getUtc());
							thVehicleAlarm.setLat(mess.getLat());
							thVehicleAlarm.setLon(mess.getLon());
							thVehicleAlarm.setMaplat(0l);
							thVehicleAlarm.setMaplon(0l);
							thVehicleAlarm.setEndGpsSpeed(Long.valueOf(mess.getSpeed()));
							thVehicleAlarm.setElevation(null);
							thVehicleAlarm.setMileage(0l);
							thVehicleAlarm.setSysutc(System.currentTimeMillis());
							thVehicleAlarm.setOilTotal(0l);
							thVehicleAlarm.setBgLevel("A002");
							thVehicleAlarm.setAlarmAddInfoStart("||0|"); // 0 标记进入点
 
							thVehicleAlarm.setMaplat(mess.getMaplat());
							thVehicleAlarm.setMaplon(mess.getMaplon());
							
							String s=Base64_URl.base64Encode("未正点进入关键点报警");
							String sendcommand="CAITS 0_0_0 "+mess.getOemCode()+"_"+mess.getCommanddr()+" 0 D_SNDM {TYPE:1,1:9,2:"+s+"} \r\n";
							DataPool.setSendPacketValue(sendcommand);
							
							try {
								DataPool.setSaveDataPacket(thVehicleAlarm);
							} catch (InterruptedException e) {
								log.error("关键点报警分发到DB队列出错.",e);
							}
						}
						
						if (!mess.isLeave() && currentTime > leaveTime) {// 标记该关键点为正点离开关键点报警结束
							thVehicleAlarm = new ThVehicleAlarm();
							mess.setLeave(true);
							// 组装报警信息
							thVehicleAlarm.setAlarmId(mess.getVid() + "220" + System.currentTimeMillis());
							thVehicleAlarm.setVid(mess.getVid());
							thVehicleAlarm.setAlarmCode("220");
							thVehicleAlarm.setUtc(mess.getUtc());
							thVehicleAlarm.setLat(mess.getLat());
							thVehicleAlarm.setLon(mess.getLon());
							thVehicleAlarm.setEndGpsSpeed(Long.valueOf(mess.getSpeed()));
							thVehicleAlarm.setElevation(null);
							thVehicleAlarm.setMileage(0l);
							thVehicleAlarm.setSysutc(System.currentTimeMillis());
							thVehicleAlarm.setOilTotal(0l);
							thVehicleAlarm.setBgLevel("A002");
 
							thVehicleAlarm.setMaplat(mess.getMaplat());
							thVehicleAlarm.setMaplon(mess.getMaplon());
							thVehicleAlarm.setAlarmAddInfoStart("||1|"); // 1标记离开
							
							String s=Base64_URl.base64Encode("未正点离开关键点报警");
							String sendcommand="CAITS 0_0_0 "+mess.getOemCode()+"_"+mess.getCommanddr()+" 0 D_SNDM {TYPE:1,1:9,2:"+s+"} \r\n";
							DataPool.setSendPacketValue(sendcommand);
							
							try {
								DataPool.setSaveDataPacket(thVehicleAlarm);
							} catch (InterruptedException e) {
								log.error("关键点报警分发到DB队列出错.",e);
							}
						}
					}
				}// End for
			}
		}// End for
		isFinished = true;
	}

	/**
	 * @return the vids
	 */
	public List<String> getVids() {
		return vids;
	}

	/**
	 * @param vids
	 *            the vids to set
	 */
	public void setVids(List<String> vids) {
		this.vids = vids;
	}

	/***
	 * 获取当前时间以秒形式获取
	 * @return
	 */
	private long getCurTime(){
		Calendar c = Calendar.getInstance();
		int hour = c.get(Calendar.HOUR_OF_DAY);
		int min = c.get(Calendar.MINUTE);
		int sec = c.get(Calendar.SECOND);
		long second = hour * 60 * 60 + min * 60 + sec;
		return second;
	}
	
	/***
	 * 将HH:MM 转成秒格式
	 * @param time
	 * @return
	 */
	private long timeConvertSec(String time){
		String[] arrays = time.split(":");
		if(arrays.length == 3){
			return Integer.parseInt(arrays[0]) * 60 * 60 + Integer.parseInt(arrays[1]) * 60 + PermeterInit.getKeyPointTimeTolerance(); 
		}else{
			log.error("设置时间格式不正确。" + time);
		}
		return 0;
	}
}
