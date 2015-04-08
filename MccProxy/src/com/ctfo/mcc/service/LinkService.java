package com.ctfo.mcc.service;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.mcc.dao.RedisConnectionPool;

public class LinkService {
	private static Logger logger = LoggerFactory.getLogger(LinkService.class);
	/**
	 * 缓存地址
	 */
//	public String cacheAddress;
	
	
//	public ConnectManager conm = null;
	
//	private ConnectionService connectionService;
	
//	public PushCommandRedisCache pushCommandRedisCache = null;

	public LinkService() {
		
	}
	
	public void init(){
		
	}
	/**
	 * @param msg
	 *            例如:CAITS 50000001_1316614968_2 4C54_13800138001 0 D_CTLM
	 *            {TYPE:10,RETRY:0,VALUE:1|1|10|1|320*240|1|148|65|126|65} vid
	 * @return
	 */
	public static Map<String, String> sendMessage(String msg) {
//		logger.info("收到消息:["+ msg+"]");
		Map<String, String> map = new HashMap<String, String>();
		if(StringUtils.isBlank(msg.trim())){ 
			map.put("online", "2");
			map.put("msg", "非法数据!");
			return map;
		} else {
			try {
				boolean  ismsg = true;
				String[] sstr = null;
				String vid = "";
				String   subtype ="";
				int idex = msg.indexOf("DISCON ");//pcc 
				int clrc = msg.indexOf("CLRC");//pcc 
				
				boolean isnum = false;//是否计数器
				
				if(idex>=0||clrc>=0){
					ismsg = false;
				}else{
					int si = msg.indexOf("START");
					int st = msg.indexOf("STOP");
					if(si==0||st==0){
						isnum = true;//计数
					}else{
						sstr = msg.split("\\ ");
						if(sstr == null || sstr.length < 5){
							logger.error("消息格式错误，缺少参数:[{}]", msg); 
							map.put("online", "-1");
							map.put("msg", "消息发送失败[格式错误,缺少参数" + msg + "]");
							return map;
						}
						subtype = sstr[4];
						//如果是监管信息、查岗信息就不获取VID
						if("L_PROV".equals(subtype)){
							ismsg = false;
						} else if ("L_PLAT".equals(subtype)){
							ismsg = false;
					    }else{
							if(sstr.length == 7){ // 获取VID值
								vid = sstr[6];	
							} else {
								logger.error("消息格式错误，缺少参数:[{}]", msg);
								map.put("online", "-1");
								map.put("msg", "消息发送失败[格式错误,缺少参数" + msg + "]");
								return map;
							}
						}
					}
				}
				// 判断有无中文 如果有中文表示监管协议、 无中文表示监控协议
				if (ismsg) {
					
					if(isnum){
//						conm.sendMsg(msg);
						if(SendQueue.offer(msg)){
							map.put("online", "1");
							map.put("msg", "消息已发送("+msg+"),请等待...");
						} else {
//							//缓存指令
							PushCommandRedisCache.pushCommandToRedisCache(sstr[0] + " " + sstr[1] + " " + sstr[2] + " " + sstr[3] + " " + sstr[4] + " " + sstr[5] + "\r\n");
							map.put("online", "0");
							map.put("msg", "消息发送失败,请检查与消息服务的连接!");
						}
					}else{
						Jedis jedis = null;
						jedis = RedisConnectionPool.getJedisConnection();
						jedis.select(6);
						String value = jedis.get(vid);
						
						if(null != value){
							String[] vehicleArray = StringUtils.splitPreserveAllTokens(value, ":", 45);
							String msgid = vehicleArray[43];	 // 获取MSG
							String isonline = vehicleArray[41]; 	// 获取是否在线
							
							if(null != isonline && null != msgid && "1".equals(isonline)){
								// 组合消息，移除VID值。
								msg = sstr[0] + " " + sstr[1] + " " + sstr[2] + " " + sstr[3] + " " + sstr[4] + " " + sstr[5] + "\r\n";
//								boolean sendEnd = connectionService.sendMessageToMSG(msg);
								boolean sendEnd = SendQueue.offer(msg);
								if (sendEnd) {
									map.put("online", "1");
									map.put("msg", "消息已发送(" + msgid + "),请等待...");
								}else{
//									//缓存指令
									PushCommandRedisCache.pushCommandToRedisCache(sstr[0] + " " + sstr[1] + " " + sstr[2] + " " + sstr[3] + " " + sstr[4] + " " + sstr[5] + "\r\n");
									map.put("online", "0");
									map.put("msg", "消息发送失败,请检查与消息服务的连接!");
								}
							} else {
								//缓存指令
								PushCommandRedisCache.pushCommandToRedisCache(sstr[0] + " " + sstr[1] + " " + sstr[2] + " " + sstr[3] + " " + sstr[4] + " " + sstr[5] + "\r\n");
								map.put("online", "0");
								map.put("msg", "消息发送失败,车辆不在线!");
							}
						} else {
							//缓存指令
							PushCommandRedisCache.pushCommandToRedisCache(sstr[0] + " " + sstr[1] + " " + sstr[2] + " " + sstr[3] + " " + sstr[4] + " " + sstr[5] + "\r\n");
							map.put("online", "0");
							map.put("msg", "消息发送失败,车辆不在线!");
						}
						
						if(null != jedis){
							RedisConnectionPool.returnJedisConnection(jedis);
						}
					
					}
				} else {
					if (SendQueue.offer(msg)) {
						map.put("online", "1");
						map.put("msg", "消息已发送监管服务,请等待...");
					} else {
						map.put("online", "0");
						map.put("msg", "消息发送失败,请检查与监管服务的连接!");
					}
				}
			} catch (Exception e) {
				logger.error("消息发送失败", e);
				map.put("online", "0");
				map.put("msg", "消息发送失败," + e.getMessage());
			}
		} 
//		logger.info("send info:" + msg + ",result:" + map.get("online") + ",desc:" + map.get("msg"));
		return map;
	}


//	public ConnectManager getConm() {
//	return conm;
//}
//public void setConm(ConnectManager conm) {
//	this.conm = conm;
//}
}
