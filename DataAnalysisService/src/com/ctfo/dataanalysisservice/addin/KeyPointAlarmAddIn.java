package com.ctfo.dataanalysisservice.addin;

import java.util.Calendar;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.ctfo.dataanalysisservice.PermeterInit;
import com.ctfo.dataanalysisservice.beans.KeyPointDataObject;
import com.ctfo.dataanalysisservice.beans.PlatAlarmTypeUtil;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.gis.PoiUtil;
import com.ctfo.dataanalysisservice.io.DataPool;
import com.ctfo.dataanalysisservice.mem.MemManager;

/**
 * 关键点报警处理
 * 
 * @author yangjian
 * 
 */
public class KeyPointAlarmAddIn extends Thread implements IaddIn {

	private static final Logger log = Logger.getLogger(KeyPointAlarmAddIn.class);

	/**
	 * 待处理数据队列, 队列数据由上级分发数据线程自动分发数据。
	 */
	private ArrayBlockingQueue<VehicleMessage> vPacket = new ArrayBlockingQueue<VehicleMessage>(
			100000);

	public KeyPointAlarmAddIn() {
		// 记录线程数
		DataAnalysisServiceMain.threadCount++;
	}

	/**
	 * 关键点报警是指在指定的时间内未到达或未离开的报警,当天只报一次警
	 */
	public void run() {

		try {
			while (true) {
				// 获得要处理的位置信息数据
				VehicleMessage vehicleMessage = vPacket.take();
				log.debug("关键点报警数据：" + vehicleMessage.getCommanddr());
				checkKeyPoint(vehicleMessage);
			}
		} catch (Exception e) {
			log.error("关键点业务类-主线程处理数据异常，异常信息如下：");
			log.error(e.getMessage());
		}
	}

	public void addPacket(VehicleMessage vehicleMessage)
			throws InterruptedException {
		if (vehicleMessage != null) {
			vPacket.put(vehicleMessage);
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	public VehicleMessage getPacket() throws InterruptedException {
		return vPacket.take();
	}
	
	/***
	 * 判断当前点是否是关键点报警。
	 * 1.判断该关键点是否结束，结束则不做任何处理。
	 * 2.判断关键点到开始点时间，如果到关键点时间则判断关键点结束时间，及判断是否在正点时间内到达这个区域。
	 * 3.如果在关键点的时间及区域范围内未到达关键点，则等待TIMERTASK任务处理.
	 * @param msg
	 * @return
	 */
	private void checkKeyPoint(VehicleMessage msg){
		long vid = msg.getVid();
		String key = PlatAlarmTypeUtil.KEY_WORD + "_" + vid + "_" + PlatAlarmTypeUtil.PLAT_KEY_POINT_ALARM;
		List<KeyPointDataObject> list = MemManager.getKeyPointMap(key);
		if (list != null && list.size() >0) {
			synchronized (list) {
				for (KeyPointDataObject keyPoint : list) {
					VehicleMessage vm = DataPool.getKeyPointPacket(key);				
					if(vm != null ){
						// 数据是否为当天
						boolean isCuDay = false;
						boolean isReach = false;
						boolean isLeave = false;
						isReach = vm.isReach();
						isLeave = vm.isLeave();
						// 获取数据的最新事件和缓存中的就时间进行对比
						Calendar cStart = Calendar.getInstance();
						
						cStart.setTimeInMillis(msg.getUtc());
						cStart.set(Calendar.HOUR , 0);
						cStart.set(Calendar.MINUTE , 0);
						cStart.set(Calendar.SECOND , 0);
						cStart.set(Calendar.MILLISECOND , 0);
						Calendar cEnd = Calendar.getInstance();
	
						cEnd.setTimeInMillis(vm.getUtc());
						cEnd.set(Calendar.HOUR , 0);
						cEnd.set(Calendar.MINUTE , 0);
						cEnd.set(Calendar.SECOND , 0);
						cEnd.set(Calendar.MILLISECOND , 0);
						if (cStart.getTimeInMillis() == cEnd.getTimeInMillis()) {
							isCuDay = true;
						}
	
						if(isReach && isLeave && isCuDay){ // 如果集合中有该关键点数据，则该关键点已经处理过
							continue;
						}else if(isReach && isLeave && !isCuDay){
							vm.setReach(false);
							vm.setLeave(false);
						}
						
						//不为当天的，则是第二天报警开始
						vm = msg;
						vm.setReach(isReach);
						vm.setLeave(isLeave);
						
						// 判断是否正点到达关键点
						if(!vm.isReach()){
							isKeyPoint(vm,keyPoint,1);
						}
						// 判断是否正点离开关键点
						if(!vm.isLeave()){
							isKeyPoint(vm,keyPoint,2);
						}
					}else{
						// 缓存没有该车记录，则添加
						DataPool.putKeyPointPacket(key, msg);
						// 判断是否正点到达关键点
						isKeyPoint(msg,keyPoint,1);
						// 判断是否正点离开关键点
						isKeyPoint(msg,keyPoint,2);
					}
					
				}// End for
			}
		}
	}

	/**
	 * 业务逻辑判断，根据关键点的设置信息判断车辆是否正点到达和离开关键点
	 * 
	 * @param lon
	 *            纬度
	 * @param lat
	 *            经度
	 * @param vid
	 *            车辆标识
	 * @param flag 1. 正点到达,2. 正点离开
	 * 			  标记判断类型 
	 * @return true(正点到达)/false(未到达)
	 */
	private void isKeyPoint(VehicleMessage msg,KeyPointDataObject keyPoint,int flag) {
		long vid = msg.getVid();
		String key = PlatAlarmTypeUtil.KEY_WORD + "_" + vid + "_" + PlatAlarmTypeUtil.PLAT_KEY_POINT_ALARM;
		
		String time = "";
		if(flag == 1){
			time = keyPoint.getReachTime();
		}else if(flag == 2){
			time = keyPoint.getLeaveTime();
		}
		
		if(time.equals("")){
			return;
		}
		
		// 判断该关键点开始时间
		if(!checkKeyPointStartTime(msg.getUtc(),time)){
			return ;
		}
		
		// 判断关键点结束时间
		if (checkKeyPointEndTime(msg.getUtc(),time)) {
			// 数据的POI点
			String poi = msg.getLon() + ","+ msg.getLat();
			// 关键点的基本配置
			String confPoi = keyPoint.getLon() + ","+ keyPoint.getLat();
			String road = keyPoint.getKeyPointArea();
			// 判断是否在正点的时间内到达这个区域
			boolean isTrue = PoiUtil.PoiInPoiBuffer(confPoi, road, poi);
			if (isTrue) {
				if(flag == 1){
					msg.setReach(true);
				}else if(flag == 2){
					msg.setLeave(true);
				}
				//msg.setAlarm(true); // 标记今天该关键点判断结束
				DataPool.putKeyPointPacket(key, msg);
			}
		}
	}
	
	/***
	 * 
	 * @param reportTime 上报GPS 时间 (年月日时分秒)
	 * @param keyPointTime 关键点时间 (时分)
	 * @return
	 */
	private boolean checkKeyPointStartTime(long reportTime,String keyPointTime){
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(reportTime);
		c.set(Calendar.YEAR, 0);
		c.set(Calendar.MONTH, 0);
		c.set(Calendar.DAY_OF_MONTH, 0);
		c.set(Calendar.MILLISECOND, 0);
		int time = c.get(Calendar.HOUR_OF_DAY) * 60 * 60 + c.get(Calendar.MINUTE) * 60 + c.get(Calendar.SECOND);
		String[] t = keyPointTime.split(":");
		if(t.length == 3){
//			c.set(Calendar.HOUR_OF_DAY, Integer.parseInt(t[0]));	
//			c.set(Calendar.MINUTE, Integer.parseInt(t[1]));
//			c.set(Calendar.SECOND, Integer.parseInt(t[2]));
			long kTime = Integer.parseInt(t[0]) * 60 * 60 + Integer.parseInt(t[1]) * 60 + Integer.parseInt(t[2]);
			// 判断上报时间是否在关键点的时间容差内
			if(time >= (kTime - PermeterInit.getKeyPointTimeTolerance()) ){
				return true;
			}
		}else{
			log.error("设置关键点时间格式不正确." + keyPointTime);
		}
		return false;
	}
	
	/***
	 * 
	 * @param reportTime 上报GPS 时间 (年月日时分秒)
	 * @param keyPointTime 关键点时间 (时分)
	 * @return
	 */
	private boolean checkKeyPointEndTime(long reportTime,String keyPointTime){
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(reportTime);
		c.set(Calendar.YEAR, 0);
		c.set(Calendar.MONTH, 0);
		c.set(Calendar.DAY_OF_MONTH, 0);
		c.set(Calendar.MILLISECOND, 0);
		int time = c.get(Calendar.HOUR_OF_DAY) * 60 * 60 + c.get(Calendar.MINUTE) * 60 + c.get(Calendar.SECOND);
		String[] t = keyPointTime.split(":");
		if(t.length == 3){
			long kTime = Integer.parseInt(t[0]) * 60 * 60 + Integer.parseInt(t[1]) * 60 + Integer.parseInt(t[2]);
			// 判断上报时间是否在关键点的时间容差内
			if(time <= (kTime + PermeterInit.getKeyPointTimeTolerance())){
				return true;
			}
		}else{
			log.error("设置关键点时间格式不正确." + keyPointTime);
		}
		return false;
	}
}
