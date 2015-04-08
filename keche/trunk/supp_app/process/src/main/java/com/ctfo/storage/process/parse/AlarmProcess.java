/**
 * 
 */
package com.ctfo.storage.process.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.model.AlarmCache;
import com.ctfo.storage.process.model.AlarmEnd;
import com.ctfo.storage.process.model.AlarmStart;
import com.ctfo.storage.process.model.Location;
import com.ctfo.storage.process.util.Cache;

/**
 * 报警处理
 *
 */
public class AlarmProcess extends Thread {
	private static Logger log = LoggerFactory.getLogger(AlarmProcess.class);
	/**	基础数据队列	*/
	private static ArrayBlockingQueue<Location> queue = new ArrayBlockingQueue<Location>(500000);
	/**	计数器	*/
	private int index = 0;
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	报警结束存储线程	*/
	private AlarmEndStorage alarmEndStorage;
	/**	报警开始存储线程	*/
	private AlarmStartStorage alarmStartStorage;
	
	public AlarmProcess() throws Exception{
		setName("AlarmProcess");
		alarmEndStorage = new AlarmEndStorage();
		alarmEndStorage.start();
		
		alarmStartStorage = new AlarmStartStorage();
		alarmStartStorage.start();
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
					log.info("AlarmProcess-10秒处理[{}]条, 排队[{}]条", index, queueSize);
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
	 * 处理报警
	 * @param location
	 */
	private void process(Location location)  {
		// 获取上一次报警
		List<AlarmCache> alarmMap = Cache.alarmCacheMap.get(location.getVid());
		// 本次报警类型数组
		String[] alarmCodeArray = null;
		// 上一次报警代码
		List<String> lastAlarmCodeList = null; 
		String alarmId = null;
		String currentAlarmCode = location.getAlarmFlag();
		alarmCodeArray = StringUtils.split(currentAlarmCode, ",");
		
		if(alarmMap!=null){
			lastAlarmCodeList = getAlarmCodeList(alarmMap, ",");
		}
		//		处理速度
		parseVehicleSpeed(location);
		//		 (1)开始报警 --- 1.如果缓存中没有mac_id对应的报警表，2.有报警缓存，无对应报警编号
		if (alarmMap == null || alarmMap.size() < 1) {
			alarmMap = new ArrayList<AlarmCache>();
			if(alarmCodeArray != null && alarmCodeArray.length > 0){
				for (String alarmCode : alarmCodeArray) {
					alarmId =  location.getUtcTime() + ":" +  UUID.randomUUID().toString().replaceAll("-", ""); 
					AlarmCache alarmCache = new AlarmCache();
					alarmCache.setStartTime(location.getUtcTime());
					alarmCache.setAlarmId(alarmId);
					alarmCache.setAlarmCode(alarmCode); 
					saveAlarmStart(location, alarmCache); 
					alarmMap.add(alarmCache);
				}
			}
			Cache.alarmCacheMap.put(location.getVid(), alarmMap);
		} else {
//			有报警缓存，无对应报警编号 -- 存储报警
			if(alarmCodeArray != null && alarmCodeArray.length > 0){
				for (String alarmCode : alarmCodeArray) {
					if (containsCode(alarmMap, alarmCode)) {
//						(2)持续报警 --- 如果报警列表中有相同报警，就从上一次报警代码中删除，更新报警结束时不处理 
						lastAlarmCodeList.remove(alarmCode);
					} else {
						// 如果是新上传的报警，报警缓存中没有对应报警，就存储报警
						alarmId =  location.getUtcTime() + ":" +  UUID.randomUUID().toString().replaceAll("-", ""); 
						AlarmCache alarmCache = new AlarmCache();
						alarmCache.setStartTime(location.getUtcTime());
						alarmCache.setAlarmId(alarmId);
						alarmCache.setAlarmCode(alarmCode); 
						saveAlarmStart(location, alarmCache); 
						alarmMap.add(alarmCache);
					}
				}
			}
//		(3)结束报警 --- 1.将剔除持续报警后的历史报警中的报警信息进行更新
//			alarmCodeArray = StringUtils.split(lastAlarmCodeStr, Constant.COMMA);
			for (String lastCode : lastAlarmCodeList) {
				AlarmCache alarmCache = getAlarmCache(alarmMap,lastCode);
				
				if (alarmCache != null && (location.getUtcTime() > alarmCache.getStartTime())) {
					saveAlarmEnd(location, alarmCache);
					alarmMap.remove(alarmCache);
				} 
			}
		}
	
	}

	/**
	 * 保存告警开始
	 * @param location
	 * @param alarmCache
	 */
	private void saveAlarmStart(Location location, AlarmCache alarmCache) {
		AlarmStart alarm = new AlarmStart();
		
		alarm.setVid(location.getVid());
		alarm.setAlarmCode(alarmCache.getAlarmCode()); 
		alarm.setAlarmId(alarmCache.getAlarmId());
		alarm.setAlarmStartUtc(alarmCache.getStartTime());
		alarm.setVid(location.getVid());
		alarm.setLon(location.getLon());
		alarm.setLat(location.getLat());
		
		alarm.setMaplon(location.getMaplon());
		alarm.setMaplat(location.getMaplat());
		alarm.setElevation(location.getElevation());
		alarm.setDirection(location.getDirection());
		alarm.setGpsSpeed(location.getGpsSpeed());
		alarm.setSysUtc(System.currentTimeMillis());
		alarm.setAlarmStatus(0);
		alarm.setMileage(location.getMileage());
		alarm.setOilTotal(location.getCumulativeFuel());
		alarm.setPlate(location.getPlate());
		alarm.setAlarmSource(1);
		alarm.setBaseStatus(location.getStatusFlag());
		alarm.setExtendStatus(location.getExpandStatusFlag());
		alarm.setTeamId(location.getTeamId());
		alarm.setTeamName(location.getTeamName());
		alarm.setEntId(location.getEntId());
		alarm.setEntName(location.getEntName());
		
		alarmStartStorage.put(alarm);
		
	}
	/**
	 * 保存告警结束
	 * @param location
	 * @param alarmCache
	 */
	private void saveAlarmEnd(Location location, AlarmCache alarmCache) {
		AlarmEnd alarm = new AlarmEnd();
		alarm.setVid(location.getVid());
		alarm.setAlarmId(alarmCache.getAlarmId());
		alarm.setLon(location.getLon());
		alarm.setLat(location.getLat());
		alarm.setMaplon(location.getMaplon()) ;
		alarm.setMaplat(location.getMaplat());
		alarm.setElevation(location.getElevation());
		alarm.setDirection(location.getDirection());
		alarm.setGpsSpeed(location.getGpsSpeed());
		alarm.setMileage(location.getMileage());
		alarm.setOilTotal(location.getCumulativeFuel());
		alarm.setEndUtc(location.getUtcTime());
		alarmEndStorage.put(alarm);
		
	}

	/**
	 * @param alarmList
	 * @param lastCode
	 * @return
	 */
	private AlarmCache getAlarmCache(List<AlarmCache> alarmList, String lastCode) {
		for(AlarmCache alarmCache : alarmList){
			if(alarmCache.getAlarmCode().equals(alarmCache)){ 
				return alarmCache;
			}
		}
		return null;
	}

	/*****************************************
	 * <li>描        述：添加逗号 		</li><br>
	 * <li>  addComma("ABC") = ,ABC,
	 * <li>时        间：2013-7-10  下午1:54:41	</li><br>
	 * <li>参数： @param alarmCode
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
//	private String addComma(String alarmCode) {
//		return "," + alarmCode + ",";
//	}

	/**
	 * 判断列表中是否包含当前报警
	 * @param alarmMap
	 * @param alarmCode
	 * @return
	 */
	private boolean containsCode(List<AlarmCache> alarmList, String alarmCode) {
		for(AlarmCache alarmCache : alarmList){
			if(alarmCache.getAlarmCode().equals(alarmCache)){ 
				return true;
			}
		}
		return false;
	}


	/*****************************************
	 * <li>描        述：拼接字符串 		</li><br>
	 * <li> join([a,b,c] , ",")  = ",a,b,c,"
	 * <li> 
	 * <li>时        间：2013-7-4  下午4:23:47	</li><br>
	 * <li>参数： @param set
	 * <li>参数： @param separator
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static List<String> getAlarmCodeList(List<AlarmCache> list, String separator) {
		List<String> l = new ArrayList<String>(); 
		for(AlarmCache alarm : list){
			l.add(alarm.getAlarmCode());
		}
		return l;
	}
	/*****************************************
	 * <li>描        述：解析车速 		</li><br>
	 * <li>时        间：2013-9-27  下午7:17:46	</li><br>
	 * <li>参数： @param app
	 * <li>参数： @return			</li><br>
	 * 优先去GPS速度  速度来源(VSS:0; GPS:1)
	 *****************************************/
	public void parseVehicleSpeed(Location location){
		int source = location.getSpeedSource();
		int vss = location.getVssSpeed();
		int gps = location.getGpsSpeed();
		if(source == 0){// 速度来源是VSS
			location.setSpeed(vss);
		} else {
			location.setSpeed(gps);
		}
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
