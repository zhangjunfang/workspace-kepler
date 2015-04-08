package com.ctfo.datatransferserver.util;

import java.util.Calendar;

/**
 * 时间工具类
 * 
 * @author yangyi
 * 
 */
public class DateConvert {

	/**
	 * 时间(yyyymmdd/hhmmss) 转换成长整型utc格式
	 * 
	 * @return
	 */
	public static long stringConvertUtc(String time) {
		if (time == null) {
			return 0;
		}
		try {
			Calendar cal = Calendar.getInstance();
			cal.set(Calendar.YEAR, Integer.parseInt(time.substring(0, 4)));
			cal.set(Calendar.MONTH, Integer.parseInt(time.substring(4, 6)) - 1);
			cal.set(Calendar.DAY_OF_MONTH, Integer.parseInt(time.substring(6, 8)));
			cal.set(Calendar.HOUR_OF_DAY, Integer.parseInt(time.substring(9, 11)));
			cal.set(Calendar.MINUTE, Integer.parseInt(time.substring(11, 13)));
			cal.set(Calendar.SECOND, Integer.parseInt(time.substring(13, 15)));
			cal.set(Calendar.MILLISECOND, 0);
			long utc = cal.getTimeInMillis();
			return utc;
		} catch (Exception e) {
			return 0;
		}
	}

}
