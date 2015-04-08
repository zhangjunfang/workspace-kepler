package com.ctfo.syn.kcpt.utils;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

public class CDate {
	private final static int fONE_DAY = 1;
	/**
	 * 得到当天零点UTC
	 * 用于存储
	 * @return
	 */
	public static long getCurrentDayYearMonthDay() {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	
	/**
	 * 得到前一天的年月日Long类型
	 * 零点
	 * 用于查询
	 * @return
	 */
	public static long getBeforeDayUTC(long utc) {
		return utc - 24 * 60 * 60 * 1000;
	}
	
	/*****
	 * 将长整型UTC转换成YYYY-MM-DD
	 * @param utc
	 * @return
	 */
	public static String utcToString(long utc){
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(utc);
		StringBuffer buf = new StringBuffer();
		buf.append(c.get(Calendar.YEAR));
		buf.append("-");
		buf.append(c.get(Calendar.MONTH) + 1);
		buf.append("-");
		buf.append(c.get(Calendar.DAY_OF_MONTH));
		return buf.toString();
	}
	
	 public static Date getschedule(String time){
		 	String[] arrTime = time.split(":");
		    Calendar tomorrow = new GregorianCalendar();
		    tomorrow.add(Calendar.DATE, fONE_DAY);
		    Calendar result = new GregorianCalendar(
		      tomorrow.get(Calendar.YEAR),
		      tomorrow.get(Calendar.MONTH),
		      tomorrow.get(Calendar.DATE),
		      Integer.parseInt(arrTime[0]),
		      Integer.parseInt(arrTime[1])
		    );
		    return result.getTime();
		 }
	
		/**
		 * 获取当前周一时间
		 * 
		 * @return
		 */
		public static long getWeekUtc() {
			Calendar cal = Calendar.getInstance();
			cal.setFirstDayOfWeek(Calendar.MONDAY);
			cal.set(Calendar.HOUR_OF_DAY, 0);
			cal.set(Calendar.MINUTE, 0);
			cal.set(Calendar.SECOND, 0);
			cal.set(Calendar.MILLISECOND, 0);
			cal.set(Calendar.DAY_OF_WEEK, Calendar.MONDAY);// 获取是下一周周一00:00:00的时间
			return cal.getTimeInMillis();
		}
		
		/*****
		 * 获取上一周周一UTC时间
		 * @param utc
		 * @return
		 */
		public static long getPreviousWeekUtc(long utc){
			return utc - 7 * 24 * 60 * 60 * 1000;
		}
		
		/**
		 * 获取上一周
		 * 
		 * @return
		 */
		public static int getPreviousWeek(long utc) {
			Calendar cal = Calendar.getInstance();
			cal.setTimeInMillis(utc);
			return cal.get(Calendar.WEEK_OF_YEAR) ;
		}
		
		/*****
		 * 根据指定长整型日期，获取月份
		 * @param utc
		 * @return
		 */
		public static int getMonth(long utc ){
			Calendar cal = Calendar.getInstance();
			cal.setTimeInMillis(utc);
			return cal.get(Calendar.MONTH) + 1;
		}
		
		/*****
		 * 根据指定长整型日期，获取年份
		 * @param utc
		 * @return
		 */
		public static int getYear(long utc){
			Calendar cal = Calendar.getInstance();
			cal.setTimeInMillis(utc);
			return cal.get(Calendar.YEAR);
		}
		
		/******
		 * 获取上一月最后长整型日期数
		 * @return
		 */
		public static long getPreviousMonth(long utc){
			Calendar cal = Calendar.getInstance();
			cal.setTimeInMillis(utc);
			cal.set(Calendar.DAY_OF_MONTH, -1);
			cal.set(Calendar.HOUR_OF_DAY, 0);
			cal.set(Calendar.MINUTE, 0);
			cal.set(Calendar.SECOND, 0);
			cal.set(Calendar.MILLISECOND, 0);
			return cal.getTimeInMillis();
		}
		
		
	 
	public static void main(String args[]){
		String today = "2012-06-18";
		SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
		Date date;
		try {
			date = format.parse(today);
			Calendar calendar = Calendar.getInstance();
			calendar.setFirstDayOfWeek(Calendar.MONDAY);
			calendar.setTime(date);
			System.out.println(calendar.get(Calendar.WEEK_OF_YEAR) + ";"+ calendar.getTimeInMillis());
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	
		
		System.out.print( CDate.getWeekUtc() + ";" + CDate.getPreviousWeek(CDate.getPreviousWeekUtc(CDate.getWeekUtc())));
	}
}
