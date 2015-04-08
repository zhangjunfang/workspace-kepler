package com.ctfo.mcc.service;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.atomic.AtomicInteger;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.mcc.dao.RedisConnectionPool;
import com.ctfo.mcc.model.Dispatch;
import com.ctfo.mcc.model.DispatchRules;
import com.ctfo.mcc.model.DispatchSend;
import com.ctfo.mcc.model.Vehicle;

/**
 *	发送调度信息服务
 *
 */
public class SendDispatchService  extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(SendDispatchService.class);
	/** 数据队列 */
	private static ArrayBlockingQueue<DispatchRules> dataQueue = new ArrayBlockingQueue<DispatchRules>(1000000);
	/** 计数器 */ 
	private int index;
	/** 上次时间 */
	private long lastTime = System.currentTimeMillis();
	
	private AtomicInteger integer = new AtomicInteger(1);
	
	public SendDispatchService(){
		setName("SendDispatchService");
	}
	
	public void init() {
		start();
	}
	
	public void run() {
		logger.info("发送调度信息服务-[SendDispatchService]启动");
		while (true) {
			try {
				DispatchRules dispatchRules = dataQueue.poll();// 获取并移除此队列的头，如果此队列为空，则返回
				if (dispatchRules != null) {
					index++;
					process(dispatchRules); 
				} else {
					Thread.sleep(1);
				}
				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastTime) > 30000) {
					int size = dataQueue.size();
					logger.info("发送调度信息服务 - 30秒接收数据:[{}]条, 排队:[{}]条", index,  size);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error("发送调度信息服务异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 处理调度信息
	 * @param dispatchRules
	 * 
	 **/
	private void process(DispatchRules dispatchRules) {
		dispatchRules.generageBase64();
		long sendUtc = System.currentTimeMillis();
//		1. 查询规则对应的车辆列表
		List<Vehicle> sendVehicleList = OracleService.queryRulesVehicle(dispatchRules.getId());
		
		logger.debug("处理规则[{}], 关联车辆[{}]台", dispatchRules.getName(), sendVehicleList.size());
		List<Dispatch> dispatchList = new ArrayList<Dispatch>();
		for(Vehicle vehicle : sendVehicleList){
			Dispatch dispatch = new Dispatch();
			dispatch.setId(GeneratorPK.instance().getPKString());
			dispatch.setVid(vehicle.getVid());
			dispatch.setPlate(vehicle.getPlate());
			dispatch.setPlateColor(vehicle.getPlateColor());
			dispatch.setSendUtc(sendUtc);
			dispatch.setCreateBy(dispatchRules.getCreateBy());
			dispatch.setCreateUtc(dispatchRules.getCreateUtc());
			dispatch.setType(dispatchRules.getType()); 
			dispatch.setTypeFlag(getTypeFlag(dispatchRules.getType()));
			String utc = String.valueOf(sendUtc).substring(0, 10);
			String seq = dispatchRules.getCreateBy() + "_" + utc + "_" + getAtomicInteger();
			dispatch.setSeq(seq);
			dispatch.setContent(dispatchRules.getContent());
			
			dispatchList.add(dispatch);
		}
		logger.debug("处理规则:[{}], 关联调度消息:[{}]", dispatchRules.getName(), dispatchList.size());
//		2. 先存储调度信息到数据库
		if(dispatchList.size() > 0){
			OracleService.insertTimingDispatch(dispatchList);
			
//		3. 后发送指令
			for(Dispatch dispatch : dispatchList){
//			获取发送信息指令、判断车辆是否在线
				DispatchSend send = getDispatchSend(dispatchRules, dispatch);
				logger.debug("获取发送指令对象[{}]", send); 
				if(send != null){
					if(send.isOnline()){ // 在线
						SendQueue.put(send.getCommand());
						logger.debug("处理调度规则完成,发送指令:[{}]", send.getCommand()); 
					} else {
						if (dispatchRules.getIsOffline() == 1 && dispatchRules.getOfflineCycle() > 0) { // 离线发送
							// 缓存定时调度信息
							PushCommandRedisCache.pushCommandToRedisCache(dispatchRules.getOfflineCycle() + send.getCommand());
						}
						logger.debug("车辆不在线,缓存车辆:[{}](1:是; 0:否), 离线时间:[{}]", dispatchRules.getIsOffline(), dispatchRules.getOfflineCycle()); 
					}
				}
			}
		}
	}



	/**
	 * 获取定时发送调度指令信息
	 * @param rules
	 * @param vehicle.
	 * @return
	 */
	private DispatchSend getDispatchSend(DispatchRules rules, Dispatch dispatch) {
		Jedis jedis = null;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6);
			String value = jedis.get(dispatch.getVid());
			if(StringUtils.isNotBlank(value)){ 
				String[] array = StringUtils.splitPreserveAllTokens(value, ":"); 
				if(array != null && array.length > 41){
					DispatchSend ds = new DispatchSend();
					if(array[41].equals("1")){ 
						ds.setOnline(true);
					} else {
						ds.setOnline(false);
					}
					String command = getSendCommand(dispatch.getSeq(), dispatch.getTypeFlag(), rules.getContentBase64(), array[39], array[32], dispatch.getSendUtc());
					ds.setCommand(command); 
					return ds;
				}
			}
			return null;
		} catch (Exception e) {
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			logger.error("获取定时发送调度指令信息异常:" + e.getMessage(), e);
			return null;
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}

	/**
	 * 获得发送指令
	 * @param dispatchRules
	 * @param macid
	 * @return
	 */
//	CAITS 169517_1407810191_279071 E001_15249679472 0 D_SNDM {TYPE:1,1:31,2:udnXr0d}
	private String getSendCommand(String seq, int typeFlag, String content,String oemCode, String phoneNumber, long sendUtc) {
		StringBuffer send = new StringBuffer(512);
		send.append("CAITS ");
		send.append(seq).append(" ");
		send.append(oemCode).append("_");
		send.append(phoneNumber).append(" 0 D_SNDM {TYPE:1,1:");
		send.append(typeFlag).append(",2:"); 
		send.append(content).append("}\r\n");
		return send.toString(); 
	}
	/**
	 * 获取发送调度消息标志位
	 * @param type
	 * @return
	 */
	private int getTypeFlag(String type) {
		int sendType = 0;
		if(type != null && type.length() > 0){
			String[] array = StringUtils.split(type, ",");
			for(String str : array){
				if(StringUtils.isNumeric(str)){
					sendType+=Integer.parseInt(str);
				}
			}
		}
		return sendType;
	}
	/**
	 * 写入队列
	 * @param rule
	 */
	public static void put(DispatchRules rule) {
		try {
			dataQueue.put(rule);
		} catch (InterruptedException e) {
			logger.error("写入队列异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 获取序列号
	 * @return
	 */
	private int getAtomicInteger(){
		int result = integer.incrementAndGet();
		if(result == Integer.MAX_VALUE){
			return integer.getAndSet(1);
		}
		return result;
	}


}
