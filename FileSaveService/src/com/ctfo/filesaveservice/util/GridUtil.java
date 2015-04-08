package com.ctfo.filesaveservice.util;
/*****************************************
 * <li>描        述：经纬度格子工具		
 * 
 *****************************************/
public class GridUtil {
	public static long startxLong;
	public static long startyLong;
	public static long lenLong; 
	private String startx;
	private String starty;
	private String len;
	/**
	 * 根据经纬度取得格子名
	 */
	public String queryKey(long lon, long lat) {
		String key = null;
		long x = (long) (lon - startxLong) / lenLong;
		long y = (long) (startyLong-lat) / lenLong;
		key = x + "_" + y;
		return key;
	}
	
	//---------------------GET & SET ----------------
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
	public String getStartx() {
		return startx;
	}
	public String getStarty() {
		return starty;
	}
	public String getLen() {
		return len;
	}
}
