package com.ctfo.commandservice.handler;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.commandservice.dao.RedisConnectionPool;
import com.ctfo.commandservice.model.Driver;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.util.Constant;
/**
 *	驾驶员信息处理线程
 */
public class DriverProcess extends Thread {
	/**	日志	*/
	private static final Logger logger = LoggerFactory.getLogger(DriverProcess.class);
	/**	Driver队列	*/
	private ArrayBlockingQueue<Map<String, String>> driverQueue = new ArrayBlockingQueue<Map<String, String>>(10000);
	/** 批量提交数量	 */
	private int index;
	/** 日志间隔 （默认10秒）	 */
	private int interval = 10000;
	/** 最近处理时间	 */
	private long lastTime = System.currentTimeMillis();
	/** 驾驶员上下班信息存储	 */
	private DriverOnlineStorage driverOnlineStorage;
	/** 驾驶员上下班信息存储	 */
	private DriverOfflineStorage driverOfflineStorage;
	/** 驾驶员历史信息存储	 */
	private DriverHistoryStorage driverHistoryStorage;
	/** 从业证过期时间解析	 */
	private SimpleDateFormat valid = new SimpleDateFormat("yyyyMMdd");
	/** 插拔卡时间验证	 */
	private SimpleDateFormat mating = new SimpleDateFormat("yyMMddHHmmss");
	/** 车辆缓存接口	 */
	private Jedis vehicleRedis = null;
	/** 驾驶员缓存接口	 */
	private Jedis driverRedis = null;
	
	public DriverProcess(OracleProperties oracleProperties) throws Exception { 
		setName("DriverProcess"); 
		driverOnlineStorage = new DriverOnlineStorage(oracleProperties);
		driverOnlineStorage.start();
		
		driverOfflineStorage = new DriverOfflineStorage(oracleProperties);
		driverOfflineStorage.start();
		
		driverHistoryStorage = new DriverHistoryStorage(oracleProperties);
		driverHistoryStorage.start();
		
		vehicleRedis = RedisConnectionPool.getJedisConnection();
		vehicleRedis.select(6);
		
		driverRedis =  RedisConnectionPool.getJedisConnection();
		driverRedis.select(4);
	}

	/**
	 * 业务处理方法
	 */
	public void run(){
		while (true) {
			try {
				Map<String, String> map = driverQueue.take();
				index++;
//				处理驾驶员信息
				parseDriver(map);
				
				long currentTime = System.currentTimeMillis();
				if(currentTime - lastTime > interval){
					int size = getQueueSize();
					int intervalTime = interval/1000;
					logger.info("DriverProcess---{}s处理[{}]条, 排队:[{}]条", intervalTime, index, size);
					lastTime = System.currentTimeMillis();
					index = 0;
				}
			} catch (Exception e) {
				logger.error("Driver信息存储线程错误:" + e.getMessage(), e);
			}
		}
	}
	
	/**	获得队列长度	*/
	private int getQueueSize() {
		return driverQueue.size();
	}
	/**
	 * 解析驾驶员信息
	 * @param map
	 */
	private void parseDriver(Map<String, String> map) {
		Driver driver = new Driver();
		String matingStatus = map.get("107");// 从业资格证IC卡插拔状态(1: 上班，2:下班)
		if (StringUtils.isNumeric(matingStatus)) { // 只处理有上下班状态的驾驶员信息
			try {
				String vid = map.get(Constant.VID);
				String uuid = map.get(Constant.UUID);
				driver.setVid(vid);
				driver.setMatingStatus(Integer.parseInt(matingStatus));
				String time = map.get("108");
				if(StringUtils.isNumeric(time) && time.length() == 12){
					driver.setMatingTime(mating.parse(time).getTime()); 
				} else {
					driver.setMatingTime(System.currentTimeMillis()); 
				}
				String phoneNumber = map.get(Constant.COMMDR); 
				String onlineId = getOnileId(vid);
//				处理上线
				if(matingStatus.equals("1")){ // 上班
					if(onlineId != null && onlineId.length() > 10){ 
						driver.setUuid(onlineId); // 结束上一次上班记录
						driver.setPhoneNumber(phoneNumber);
						driverOfflineStorage.put(driver);
						deleteOffline(vid);
						deleteDriverMatingCache(vid);
					}
					driver.setUuid(uuid); 
					processDriverOnline(driver, map);
					saveOnile(vid, uuid);
//				处理下线
				} else if (matingStatus.equals("2")){ // 下班
					if(onlineId != null && onlineId.length() > 10){
						driver.setUuid(onlineId); 
						driver.setPhoneNumber(phoneNumber);
						driverOfflineStorage.put(driver);
						deleteOffline(vid);
						deleteDriverMatingCache(vid);
					}
				}
			}catch(Exception e){
				logger.error("解析驾驶员信息异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 删除驾驶员刷卡信息缓存
	 * @param vid
	 */
	private void deleteDriverMatingCache(String vid) {
		try {
			driverRedis.hdel("HD_DRIVER_CARD", vid);
		} catch (Exception e) {
			if(driverRedis != null){
				RedisConnectionPool.returnBrokenResource(driverRedis);
			}
			driverRedis =  RedisConnectionPool.getJedisConnection();
			driverRedis.select(4);
		}
	}

	/**
	 * 处理驾驶员上线
	 * @param driver
	 * @param map
	 * @throws ParseException   
	 */ 
	private void processDriverOnline(Driver driver, Map<String, String> map) {
		try {
			// 从业资格证IC卡读取结果 (0:成功；1:卡片密钥认证未通过,2:卡片已被锁定,3:卡片被拔出,4:数据校验错; 5:平台解析异常)
			String readStatus = map.get("109");
			if (StringUtils.isNumeric(readStatus)) {
				if (readStatus.equals("0")) {
					driver.setStaffName(map.get("110")); // 驾驶员姓名
					driver.setIdNumber(map.get("111")); // 驾驶员身份证号码
					String qualificationId = map.get("112");// 从业资格证编码
					driver.setQualificationId(qualificationId); 
					driver.setQualificationName(map.get("113"));// 从业资格证发证机构名称
					String validTimee = map.get("114");// 解析资格证有效时间
					if(StringUtils.isNumeric(validTimee) && validTimee.length() == 8){
						driver.setQualificationValid(valid.parse(validTimee).getTime()); 
					} else {
						driver.setQualificationValid(-1); 
					}
//					缓存驾驶员上报刷卡信息
					cacheDriverMatingCard(driver.getVid(), qualificationId);
				}
				driver.setReadStatus(Integer.parseInt(readStatus));
			} else {
				driver.setReadStatus(5);
			}
			String vehicleInfo = getVehicle(driver.getVid());
			String[] array = null;
			if (vehicleInfo != null) {
				array = StringUtils.splitPreserveAllTokens(vehicleInfo, ":", 45);
				if (array != null && array.length == 45) {
					if (StringUtils.isNumeric(array[37])) {
						driver.setTeamId(array[37]);
					}
					driver.setTeamName(array[44]);
					if (StringUtils.isNumeric(array[38])) {
						driver.setEntId(array[38]);
					}
					driver.setEntName(array[36]);
				}
			}
		} catch (ParseException e) {
			logger.error("解析证件有效期异常:" + map.get("114"), e);
		}
		driver.setPhoneNumber(map.get(Constant.COMMDR));
		driver.setPlate(map.get(Constant.VEHICLENO));
		driver.setPlateColor(map.get(Constant.PLATECOLORID));
		driver.setSysUtc(System.currentTimeMillis());
		driverOnlineStorage.put(driver); 
		driverHistoryStorage.put(driver);

	}
	/**
	 * <pre>
	 * 缓存驾驶员刷卡信息
	 * <li>根据驾驶员从业证号查询到驾驶员信息，将驾驶员信息存入驾驶员刷卡信息缓存中
	 * @param vid
	 * @param qualificationId
	 * </pre>
	 */
	private void cacheDriverMatingCard(String vid, String qualificationId) {
		try {
			String info = driverRedis.hget("HD_DRIVER_LIST", qualificationId);
			if(info != null && info.length() > 10){
				String cardInfo = info.substring(0, info.length() -1) + "1";
				driverRedis.hset("HD_DRIVER_CARD", vid, vid + cardInfo);
			}
		} catch (Exception e) {
			if(driverRedis != null){
				RedisConnectionPool.returnBrokenResource(driverRedis);
			}
			driverRedis =  RedisConnectionPool.getJedisConnection();
			driverRedis.select(4);
		}
	}

	/**
	 * 获取车辆缓存信息
	 * @param vid
	 * @return
	 */
	private String getVehicle(String vid) {
		try {
			return vehicleRedis.get(vid);
		} catch (Exception e) {
			if(vehicleRedis != null){
				RedisConnectionPool.returnBrokenResource(vehicleRedis);
			}
			vehicleRedis =  RedisConnectionPool.getJedisConnection();
			vehicleRedis.select(6);
			return null;
		}
		
	}
	/**
	 * 获取上线编号
	 * @param field
	 * @return
	 */
	private String getOnileId(String field) {
		try {
			return driverRedis.hget("H_KCPT_DRIVER_ONOFF_STATUS", field); 
		} catch (Exception e) {
			if(driverRedis != null){
				RedisConnectionPool.returnBrokenResource(driverRedis);
			}
			driverRedis =  RedisConnectionPool.getJedisConnection();
			driverRedis.select(4);
			return null;
		}
	}
	/**
	 * 保存上线信息
	 * @param field 车辆编号
	 * @param value	上线记录编号
	 */
	private void saveOnile(String field, String value) {
		try {
			driverRedis.hset("H_KCPT_DRIVER_ONOFF_STATUS", field, value); 
		} catch (Exception e) {
			if(driverRedis != null){
				RedisConnectionPool.returnBrokenResource(driverRedis);
			}
			driverRedis =  RedisConnectionPool.getJedisConnection();
			driverRedis.select(4);
		}
	}
	/**
	 * 删除驾驶信息
	 * @param fields
	 */
	private void deleteOffline(String fields) {
		try {
			driverRedis.hdel("H_KCPT_DRIVER_ONOFF_STATUS", fields);
		} catch (Exception e) {
			if(driverRedis != null){
				RedisConnectionPool.returnBrokenResource(driverRedis);
			}
			driverRedis =  RedisConnectionPool.getJedisConnection();
			driverRedis.select(4);
		}
		
	}

	/**
	 * 加入Driver队列
	 * @param driver
	 */
	public void putData(Map<String, String> app) {
		try {
			driverQueue.put(app);
		} catch (InterruptedException e) {
			logger.error("加入Driver队列异常:" + e.getMessage(), e);
		}
	}
	
}

