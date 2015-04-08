package com.ctfo.storage.process.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.model.Location;
import com.ctfo.storage.process.model.RealTimeLocation;
/**
 *	实时数据存储
 */
public class RealTimeProcess extends Thread{
	private static Logger log = LoggerFactory.getLogger(RealTimeProcess.class);
	/**	多媒体文件队列	*/
	private static ArrayBlockingQueue<Location> queue = new ArrayBlockingQueue<Location>(50000);
	/**	Redis接口	*/
	private RealTimeStorage realTimeStorage;
	/**	计数器	*/
	private int index = 0;
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	
	public RealTimeProcess(){
		setName("RealTimeProcess");
		realTimeStorage = new RealTimeStorage();
		realTimeStorage.start(); 
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
					log.info("RealTimeProcess-10秒处理[{}]条, 排队[{}]条", index, queueSize);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("处理实时数据存储队列数据异常:" + e.getMessage());
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
	 * 处理实时数据
	 * @param Location
	 * @throws  InterruptedException 
	 */
	private void process(Location location)  {
		RealTimeLocation realTimeLocation = new RealTimeLocation();
		realTimeLocation.setVid(location.getVid());
		realTimeLocation.setPlate(location.getPlate());
		realTimeLocation.setPlateColor(location.getPlateColor());
		realTimeLocation.setPhoneNumber(location.getPhoneNumber());
		realTimeLocation.setTid(location.getTid());
		realTimeLocation.setTerminalType(location.getTerminalType());
		realTimeLocation.setStaffName(location.getStaffName());
		realTimeLocation.setEntName(location.getEntName());
		realTimeLocation.setEntId(location.getEntId());
		realTimeLocation.setTeamId(location.getTeamId());
		realTimeLocation.setTeamName(location.getTeamName());
		realTimeLocation.setStatus(location.getStatus());
		realTimeLocation.setOnline(location.getOnline());
		realTimeLocation.setMaplon(location.getMaplon());
		realTimeLocation.setMaplat(location.getMaplat());
		realTimeLocation.setSpeedSource(location.getSpeedSource());
		realTimeLocation.setSpeed(location.getSpeed());
		realTimeLocation.setVssSpeed(location.getVssSpeed());
		realTimeLocation.setElevation(location.getElevation());
		realTimeLocation.setDirection(location.getDirection());
		realTimeLocation.setGspTime(location.getUtcTime());
		realTimeLocation.setSysTime(System.currentTimeMillis());
		realTimeLocation.setMileage(location.getMileage());
		realTimeLocation.setOemCode(location.getOemCode());
		
		realTimeStorage.put(realTimeLocation);
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
