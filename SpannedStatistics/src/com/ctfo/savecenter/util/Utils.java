package com.ctfo.savecenter.util;


import java.util.Map;

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
	 *  根据速度来源，获取是GPS速度或VSS速度
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static int getSpeed(String spdFrom,Map<String, String> app){
		try{
			if(spdFrom.equals("0")){// 0：来自VSS
				return Integer.parseInt(app.get("7"));
				
			}else{
				return Integer.parseInt(app.get("3"));
			}
		}catch(Exception e){
			return 0;
		}
	}
	
	
//	/*****
//	 * 偏移经纬度
//	 * @param lat
//	 * @param lon
//	 * @return
//	 */
//	public static Long[] convertLatLon(long lat,long lon){
//		Long[] latLon = new Long[2];
//		// 偏移
//		Converter conver = new Converter();
//		Point point = conver.getEncryPoint(lon / 600000.0,
//				lat / 600000.0);
//		if (point != null) {
//			latLon[0] = Math.round(point.getX() * 600000); // MapLon
//			latLon[1] = Math.round(point.getY() * 600000); // MapLat
//		} else {
//			latLon[0] = 0l;
//			latLon[1] = 0l;
//		}
//		return latLon;
//	}
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
