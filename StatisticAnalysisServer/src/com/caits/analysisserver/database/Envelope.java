package com.caits.analysisserver.database;

public class Envelope {
	private static long btlon = 73;
	
	private static long btlat = 15;
	
	private static long  toplon= 136;
	
	private static long  toplat= 55;
	
	
	/*****
	 * 判断该点是否在中国范围之内，经纬度为偏移之后的
	 * @param lat
	 * @param lon
	 * @return
	 */
	public static boolean checkEnvelope(double lat,double lon){
		if (lon < btlon || lon > toplon) {// 经度范围72-136(43200000-81600000)
			return false;
		} else if (lat < btlat || lat > toplat) {// 纬度范围18-54(10800000-32400000)
			return false;
		}
		return true;
	}
	
}
