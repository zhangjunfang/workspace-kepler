/**
 * 
 */
package com.ctfo.storage.process.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.model.Location;
import com.ctfo.storage.process.model.ThAlarm;
import com.ctfo.storage.process.model.TrackFile;

/**
 * 轨迹、报警文件处理程序
 *
 */
public class TrackFileProcess extends Thread {
	private static Logger log = LoggerFactory.getLogger(TrackFileProcess.class);
	/**	多媒体文件队列	*/
	private static ArrayBlockingQueue<Location> queue = new ArrayBlockingQueue<Location>(100000);
	/**	计数器	*/
	private int index = 0;
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	轨迹存储线程	*/
	private TrackFileStorage trackFileStorage;
	/**	报警文件存储线程	*/
	private AlarmFileStorage alarmFileStorage;
	
	public TrackFileProcess() throws Exception{ 
		setName("TrackFileProcess");
		trackFileStorage = new TrackFileStorage();
		trackFileStorage.start();
		
		alarmFileStorage = new AlarmFileStorage();
		alarmFileStorage.start();
	}
	
	public void run(){
		while(true){
			try {
				Location location = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。						
				index++;
				process(location); 
				long current = System.currentTimeMillis();
				if ((current - lastTime) > 10000) {
					int queueSize = getQueueSize();
					log.info("TrackFileProcess-10秒处理[{}]条, 排队[{}]条", index, queueSize);
					index = 0;
					lastTime = System.currentTimeMillis();
				}	
			} catch (Exception e) {
				log.error("处理报警队列数据异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取队列大小
	 * @return
	 */
	private int getQueueSize() {
		return queue.size();
	}
	/**
	 * 处理轨迹位置文件
	 * @param MediaEvent
	 * @throws  InterruptedException 
	 */
	private void process(Location location)  {
		TrackFile track = new TrackFile();
		track.setVid(location.getVid());
		track.setEntId(location.getEntId()); 
		track.setMapLon(location.getMaplon()); 		// 经度0
		track.setMapLat(location.getMaplat()); 		// 纬度1
		track.setGpsTime(location.getUtcTime());	// GPS时间2
		track.setGpsSpeed(location.getSpeed()); 	// GPS 速度3
		track.setDirection(location.getDirection());// 正北方向夹角4
		track.setStatus(location.getStatusFlag()); 	// 车辆状态5
		track.setAlarmCode(location.getAlarmFlag());// 报警编码6
		track.setLon(location.getLon()); 			// 经度7
		track.setLat(location.getLat()); 			// 纬度8
		track.setElevation(location.getElevation());// 海拔9
		track.setMileage(location.getMileage()); 	// 里程10
		track.setOilWear(location.getCumulativeFuel()); 			// 累计油耗11
		track.setEngineWorkinglong(location.getEngineRunTotal()); 	// 发动机运行总时长12
		track.setEngineRotateSpeed(location.getEngineRpm()); 		// 引擎转速（发动机转速）13
		track.setStatus(location.getStatusFlag()); 					// 位置基本信息状态位14
		track.setAddStatus(location.getAreaAndLineAlarmAdded()); 	// 区域/线路报警附加信息15
		track.seteWater(location.getCoolantTemperature());			// 冷却液温度16
		track.setBatteryVoltage(location.getBatteryVoltage());		// 蓄电池电压17
		track.setOilInstant(location.getInstantFuel());				// 瞬时油耗18
		track.setVssSpeed(location.getVssSpeed());					// 行驶记录仪速度(km/h)19
		track.setOilPressure(location.getOilTemperature());			// 机油压力 (20 COL)
		track.setAirPressure(location.getAtmosphericPressure());	// 大气压力21
		track.seteTorque(location.getEngineTorque()); 				// 发动机扭矩百分比，1bit=1%，0=-125%    22
		track.setExpandStatusFlag(location.getExpandStatusFlag());	// 车辆信号状态 23
		track.setSpeedSource(location.getSpeedSource()); 			// 车速来源 24
		track.setOilMass(location.getOil()); 						// 油量（对应仪表盘读数） 25 
		track.setAddOverSpeed(location.getOverspeedAlarmAdded()); 	// 超速报警附加信息 26
		track.setOutRouteST(location.getRoadTimeAlarmAdded()); 		// 路线行驶时间不足/过长 27
		track.setEecApp(location.getThrottlePedalPosition()); 		// 油门踏板位置，(1bit=0.4%，0=0%) 28 
		track.setInnerBatteryVoltage(location.getTerminalBatteryVoltage()); // 终端内置电池电压 29
		track.setEngineTemperature(location.getCoolantTemperature()); // 发动机水温 30
		track.setOilTemperature(location.getOilTemperature()); 		// 机油温度 31
		track.setAirInflowTpr(location.getInletTemperature()); 		// 进气温度 32
		track.setOpenStatus(location.getDoorAlarmAdded()); 			// 开门报警附加信息 33
		track.setAlarmConfirmId(location.getAcknowledgeAlarmId());  // 需要人工确认报警事件的ID 34
		track.setFuelMeter(location.getMeterOil()); 	// 计量仪油耗，1bit=0.01L,0=0L 35
		track.setSysUtc(System.currentTimeMillis()); 	//系统时间 36
		
		trackFileStorage.put(track); 
		
		ThAlarm alarm = new ThAlarm();
		// 报警存储
		alarm.setVid(location.getVid()); 
		alarm.setAlarmCode(location.getAlarmFlag()); 	// 报警编码0
		alarm.setMapLon(location.getMaplon()); 			// MAP经度1
		alarm.setMapLat(location.getMaplat()); 			// MAP纬度2
		alarm.setLon(location.getLon());				// 经度3
		alarm.setLat(location.getLat()); 				// 纬度4
		alarm.setGpsTime(location.getUtcTime()); 		// GPS时间5
		alarm.setGpsSpeed(location.getSpeed()); 		// 根据车速来源获取车速6
		alarm.setDirection(location.getDirection()); 	//正北方向夹角7
		alarm.setOilWear(location.getCumulativeFuel()); // 累计油耗8
		alarm.setMileage(location.getMileage()); 		// 里程9
		alarm.setLineAlarm(location.getAreaAndLineAlarmAdded()); //报区域/线路报警10
		alarm.setElevation(location.getElevation()); 	// 海拔11
		alarm.setSpeedSource(location.getSpeedSource());// 车速来源 12
		alarm.setSysUtc(System.currentTimeMillis()); 	//系统时间13
		
		alarmFileStorage.put(alarm);
	}
	
	

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(Location location){
		return queue.offer(location);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(Location location){
		try {
			queue.put(location);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(Location location){
		return queue.add(location);
	}
}
