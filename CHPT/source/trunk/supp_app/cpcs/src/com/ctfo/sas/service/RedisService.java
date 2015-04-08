package com.ctfo.sas.service;

/**
 * redis 读取服务
 * 
 * @author 蒋东卿
 * @date 2014年12月4日下午3:37:15
 * @since JDK1.6
 */
public interface RedisService {
	
	/**
	 * 根据车厂id获取服务站id
	 * 
	 * @param sapId
	 * @return
	 * @throws Exception
	 */
	public String getStationId(String sapId);
	
	/**
	 * @description:把报文塞到redis 里
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年12月4日下午6:18:30
	 * @modifyInformation：
	 */
	public void storageRedis(String key,String members);
	
}
