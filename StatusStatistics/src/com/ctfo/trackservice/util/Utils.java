package com.ctfo.trackservice.util;


import java.util.Map;

import org.apache.commons.lang3.StringUtils;


import com.ctfo.trackservice.model.OracleProperties;
import com.ctfo.trackservice.model.RedisProperties;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

public class Utils {
	
	public static final int RATIO_BASE = 10; 
	
	public static String checkLength(String param,int len){
		if(param.length() > len){
			return param.substring(0,len);
		}else{
			return param;
		}
	}
	
	/***
	 * 
	 * @param org
	 * @param key
	 * @return
	 */
	public static boolean checkAdditionalStatus(String org,String key){
		String[] array = org.split("\\|");
		for(String ar : array){
			if(ar.equals(key)){
				return true;
			}
		}//End for
		
		return false;
	}
	
	/***
	 * 平移经纬度
	 * @param lon
	 * @param lat
	 * @return
	 */
	public static Point convertLatLon(long lon,long lat){
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
		return point;
	}
	
	
	/***
	 *  获取发动机最大转速
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static Long getMaxRPM(String maxRPM){
		if(StringUtils.isNumeric(maxRPM)){
			return Long.parseLong(maxRPM);
		}else{
			return 0l;
		}
	}
	/*****
	 * 判断字符串是否是为null或者空，是则返回true，否则为false
	 * @param ck
	 * @return
	 */
	public static boolean checkEmpty(String str){
		if(str == null || "".equals(str) || "null".equals(str)){
			return true;
		}
		return false;
	}
	/***
	 *  根据速度来源，获取是GPS速度或VSS速度
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static int getSpeed(String spdFrom,String[] cols){
		int spd = 0;
		if(spdFrom.equals("0")){// 0：来自VSS
			if(cols[19] != null && !cols[19].equals("")){
				spd = Integer.parseInt((null == cols[19] || "".equals(cols[19]))?"0":cols[19]);
			}
		}else{
			if(cols[3] != null && !cols[3].equals("")){
				spd = Integer.parseInt((null == cols[3] || "".equals(cols[3]))?"0":cols[3]); // 1：来自GPS
			}
		}
		return spd;
	}
	/**
	 * 获取Redis配置信息
	 * @param config
	 * @return
	 */
	public static RedisProperties getRedisProperties(Map<String, String> config) {
//		初始化redis
		RedisProperties redisProperties = new RedisProperties();
		redisProperties.setHost(config.get("redisHost"));
		redisProperties.setPort(Integer.parseInt(config.get("redisPort")));
		redisProperties.setPwd(config.get("redisPass"));
		redisProperties.setMaxActive(Integer.parseInt(config.get("redisMaxActive"))); 
		redisProperties.setMaxIdle(Integer.parseInt(config.get("redisMaxIdle")));
		redisProperties.setMaxWait(Long.parseLong(config.get("redisMaxWait")));
		redisProperties.setRedisTimeout(Integer.parseInt(config.get("redisTimeOut"))); 
		return redisProperties;
	}
	/**
	 * 获取Oracle配置信息
	 * @param config
	 * @return
	 */
	public static OracleProperties getOracleProperties(Map<String, String> config) {
		OracleProperties oracleProperties = new OracleProperties();
		oracleProperties.setUrl(config.get("oracleUrl"));
		oracleProperties.setUsername(config.get("oracleUsername"));
		oracleProperties.setPassword(config.get("oraclePassword"));
		oracleProperties.setInitialSize(Integer.parseInt(config.get("oracleInitialSize")));
		oracleProperties.setMaxActive(Integer.parseInt(config.get("oracleMaxActive")));
		oracleProperties.setMinIdle(Integer.parseInt(config.get("oracleMinIdle")));
		oracleProperties.setMaxWait(Integer.parseInt(config.get("oracleMaxWait")));
		oracleProperties.setTimeBetweenEvictionRunsMillis(Integer.parseInt(config.get("oracleTimeBetweenEvictionRunsMillis")));
		oracleProperties.setMinEvictableIdleTimeMillis(Integer.parseInt(config.get("oracleMinEvictableIdleTimeMillis")));
		oracleProperties.setTestWhileIdle(Boolean.parseBoolean(config.get("oracleTestWhileIdle")));
		oracleProperties.setTestOnBorrow(Boolean.parseBoolean(config.get("oracleTestOnBorrow")));
		oracleProperties.setTestOnReturn(Boolean.parseBoolean(config.get("oracleTestOnReturn")));
		oracleProperties.setMaxOpenPreparedStatements(Integer.parseInt(config.get("oracleSetMaxOpenPreparedStatements")));
		
		return oracleProperties;
	}
	
	public static void main(String args[]){
		/*
		 7e 09 00 00 1e 01 38 03 84 98 41 00 ef 
			82 01 
			01 a9 61 33 ---27877683	
			06 d1 57 3b ---114382651
			00 69 
			02 54 
			01 4f 
			12 11 21 09 21 21 
			00 ce 00 00 00 00 ed 0c 28 7e 
		 */
		int lon = 114382651;
		int lat = 27877683;
		
		double tmpLat = (lat*6)/10;
		double tmpLon = (lon*6)/10;
		
		Point point = Utils.convertLatLon(Math.round(tmpLon),Math.round(tmpLat));
		
		System.out.println(point.getX()+"  "+Math.round(point.getX()*600000));
		System.out.println(point.getY()+"  "+Math.round(point.getY()*600000));
	}


}
