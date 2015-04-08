package com.ctfo.analy.util;

/**
 * 计算经纬度偏移
 * @author LiangJian
 * 2012-12-12 20:08:00
 */
public class GpsConverterUtil {

	/**
	 * 获偏移后的经纬度
	 * @param lon 偏移前经度
	 * @param lat 偏移前纬度
	 * @return 经度，纬度 
	 */
	public static Long[] getMigrationLonLat(long lon,long lat){
		long maplon = -100;
		long maplat = -100;
		// 偏移
		com.encryptionalgorithm.Converter conver = new com.encryptionalgorithm.Converter();
		com.encryptionalgorithm.Point point = conver.getEncryPoint(lon / 600000.0,lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		Long[] maplonlat = new Long[2];
		maplonlat[0] = maplon;
		maplonlat[1] = maplat;
		return maplonlat;
	}
}
