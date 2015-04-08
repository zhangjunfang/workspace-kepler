package com.ctfo.regionfileser.util;

public class GridUtil {
	
	public static long startxLong;
	public static long startyLong;
	public static long lenLong; 

	private String startx;
	
	private String starty;
	
	private String len;
	
	
	public void setStartx(String startx) {
		startxLong=Long.parseLong(startx);
		this.startx = startx;
	}


	public void setStarty(String starty) {
		startyLong=Long.parseLong(starty);
		this.starty = starty;
	}


	public void setLen(String len) {
		lenLong=Long.parseLong(len);
		this.len = len;
	}


	/**
	 * 根据经纬度取得格子名
	 */
	public static String queryKey(long lon, long lat) {
		
		
		String key = null;
		long x = (long) (lon - startxLong) / lenLong;
		long y = (long) (startyLong-lat) / lenLong;
		key = x + "_" + y;
		return key;
	}
	
}
