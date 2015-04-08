package com.ctfo.trackservice.util;




import java.text.DateFormat;
import java.text.ParseException;
import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class DateTools {
	private static Logger log = LoggerFactory.getLogger(DateTools.class);
	private static Calendar localTime = Calendar.getInstance(); // 当前日期
	
	/**
	 * 获取当前年份
	 * @param d
	 * @return
	 */
	public static int getCurrentYear(){
		Calendar gc = Calendar.getInstance();
		return gc.get(Calendar.YEAR);
	}
	
	/**
	 * 获取当前月份
	 * @param d
	 * @return
	 */
	public static int getCurrentMonth(){
		Calendar gc = Calendar.getInstance();
		//gc.add(Calendar.MONTH, 1);
		return gc.get(Calendar.MONTH)+1;
	}
	
	/**
	 * 获取当前日期
	 * @param d
	 * @return
	 */
	public static int getCurrentDay(){
		Calendar gc = Calendar.getInstance();
		return gc.get(Calendar.DAY_OF_MONTH);
	}
	
	/**
	 * 获取现在时间
	 * 
	 * @return 返回时间类型 yyyy-MM-dd HH:mm:ss
	 */
	public static Date getNowDate() {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		String dateString = formatter.format(currentTime);
		ParsePosition pos = new ParsePosition(8);
		Date currentTime_2 = formatter.parse(dateString, pos);
		return currentTime_2;
	}

	/**
	 * 将时间格式由srcformat 转化成 destformat
	 * 
	 * @return返回返回转化后的字符串
	 */
	public static String changeDateFormat(String srcformat, String destformat,
			String srcdatastr) {
		// yyyymmdd/hhmmss
		DateFormat format1 = new SimpleDateFormat(srcformat);
		DateFormat format2 = new SimpleDateFormat(destformat);
		Date date = null;
		String str = null;
		try {
			date = format1.parse(srcdatastr);
			str = format2.format(date);
		} catch (ParseException e) {
			log.error(e.getMessage());
		}
		return str;
	}

	public static Date changeDateFormat(String srcformat, String srcdatastr) {
		// yyyymmdd/hhmmss
		DateFormat format1 = new SimpleDateFormat(srcformat);
		Date date = new Date();
		try {			
			date = format1.parse(srcdatastr);
		} catch (ParseException e) {
			log.error(e.getMessage());
			e.printStackTrace();
		}
		return date;
	}
	/**
	 * 获取现在时间
	 * 
	 * @return返回字符串格式 yyyy-MM-dd HH:mm:ss
	 */
	public static String getStringDate(long utc) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
		String dateString = formatter.format(utc);
		return dateString;
	}
	/**
	 * 获取现在时间
	 * 
	 * @return返回字符串格式 yyyy-MM-dd HH:mm:ss
	 */
	public static String getStringDate() {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		String dateString = formatter.format(currentTime);
		return dateString;
	}
	
	/**
	 * 获取现在时间
	 * 
	 * @return 返回短时间字符串格式yyyy-MM-dd
	 */
	public static String getStringDateShort() {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
		String dateString = formatter.format(currentTime);
		return dateString;
	}

	/**
	 * 获取时间 小时:分;秒 HH:mm:ss
	 * 
	 * @return
	 */
	public static String getTimeShort() {
		SimpleDateFormat formatter = new SimpleDateFormat("HH:mm:ss");
		Date currentTime = new Date();
		String dateString = formatter.format(currentTime);
		return dateString;
	}
	
	/**
	 * 将长时间格式字符串转换为时间
	 * 
	 * @param strDate
	 * @return
	 */
	public static Date strToDateByFormat(String strDate,String formatstr) {
		SimpleDateFormat formatter = new SimpleDateFormat(formatstr);
		ParsePosition pos = new ParsePosition(0);
		Date strtodate = formatter.parse(strDate, pos);
		return strtodate;
	}

	/**
	 * 将长时间格式字符串转换为时间 yyyy-MM-dd HH:mm:ss
	 * 
	 * @param strDate
	 * @return
	 */
	public static Date strToDateLong(String strDate) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		ParsePosition pos = new ParsePosition(0);
		Date strtodate = formatter.parse(strDate, pos);
		return strtodate;
	}

	/**
	 * 将长时间格式时间转换为字符串 yyyy-MM-dd HH:mm:ss
	 * 
	 * @param dateDate
	 * @return
	 */
	public static String dateToStrLong(java.util.Date dateDate) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		String dateString = formatter.format(dateDate);
		return dateString;
	}

	/**
	 * 将短时间格式时间转换为字符串 yyyy-MM-dd
	 * 
	 * @param dateDate
	 * @param k
	 * @return
	 */
	public static String dateToStr(java.util.Date dateDate) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
		String dateString = formatter.format(dateDate);
		return dateString;
	}
	
	/**
	 * 将短时间格式时间转换为字符串 yyyy-MM-dd
	 * 
	 * @param dateDate
	 * @param k
	 * @return
	 */
	public static String dateToStr2(java.util.Date dateDate) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMdd");
		String dateString = formatter.format(dateDate);
		return dateString;
	}


	/**
	 * 将短时间格式字符串转换为时间 yyyy-MM-dd
	 * 
	 * @param strDate
	 * @return
	 */
	public static Date strToDate(String strDate) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
		ParsePosition pos = new ParsePosition(0);
		Date strtodate = formatter.parse(strDate, pos);
		return strtodate;
	}

	/**
	 * 得到现在时间
	 * 
	 * @return
	 */
	public static Date getNow() {
		Date currentTime = new Date();
		return currentTime;
	}

	/**
	 * 提取一个月中的最后一天
	 * 
	 * @param day
	 * @return
	 */
	public static Date getLastDate(long day) {
		Date date = new Date();
		long date_3_hm = date.getTime() - 3600000 * 34 * day;
		Date date_3_hm_date = new Date(date_3_hm);
		return date_3_hm_date;
	}

	/**
	 * 得到现在时间
	 * 
	 * @return 字符串 yyyyMMdd HHmmss
	 */
	public static String getStringToday() {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMdd HHmmss");
		String dateString = formatter.format(currentTime);
		return dateString;
	}

	/**
	 * 得到现在小时
	 */
	public static String getHour() {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		String dateString = formatter.format(currentTime);
		String hour;
		hour = dateString.substring(11, 13);
		return hour;
	}
	
	/**
	 * 字符串转utc
	 * @param gpsdate
	 * @return
	 */
	public static long getTime(String gpsdate){
		long ret = 0;
		try {
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
			Date d = sdf.parse(gpsdate);
			ret = d.getTime();
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ret;
	}
	/**
	 * 得到现在分钟
	 * 
	 * @return
	 */
	public static String getTime() {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		String dateString = formatter.format(currentTime);
		String min;
		min = dateString.substring(14, 16);
		return min;
	}

	/**
	 * 根据用户传入的时间表示格式，返回当前时间的格式 如果是yyyyMMdd，注意字母y不能大写。
	 * 
	 * @param sformat
	 *            yyyyMMddhhmmss
	 * @return
	 */
	public static String getUserDate(String sformat) {
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat(sformat);
		String dateString = formatter.format(currentTime);
		return dateString;
	}

	public static String getNextNDate(int i) {
		Calendar cal = Calendar.getInstance();
		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy_MM_dd");
		String r = "";
		try {
			cal.setTime(date);
			cal.add(Calendar.DATE, i);
			r = sdf.format(cal.getTime());
		} catch (Exception e) {
			log.error(e.getMessage());
		}
		return r;
	}

	/**
	 * 时间(yyyymmdd/hhmmss) 转换成长整型utc格式
	 * 
	 * @return
	 */
	public static long stringConvertUtc(String time) {
		if (time == null || "0".equals(time) || !time.matches("\\d{8}/\\d{6}")) {
			return 0;
		}
		Calendar cal = Calendar.getInstance();
		cal.set(Integer.parseInt(time.substring(0, 4)), Integer.parseInt(time
				.substring(4, 6)) - 1, Integer.parseInt(time.substring(6, 8)),
				Integer.parseInt(time.substring(9, 11)), Integer.parseInt(time
						.substring(11, 13)), Integer.parseInt(time.substring(
						13, 15)));
		cal.set(Calendar.MILLISECOND, 0);
		long utc = cal.getTimeInMillis();
		return utc;
	}
	
	/**
	 * 时间(YYYY/MM/DD) 转换成长整型utc格式
	 * 
	 * @return
	 */
	public static long yearMonthDayConvertUtc(String time) {
		if (time == null) {
			return 0;
		}
		String[] date = time.split("/");
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.YEAR, Integer.parseInt(date[0]));
		cal.set(Calendar.MONTH, Integer.parseInt(date[1]) -1);
		cal.set(Calendar.DAY_OF_MONTH, Integer.parseInt(date[2]));
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		long utc = cal.getTimeInMillis();
		return utc;
	}

	/**
	 * 得到前一天的年月日Long类型
	 * 用于存储
	 * @return
	 */
	public static long getYesDayYearMonthDay() {
		return getCurrentDayYearMonthDay() - 24 * 60 * 60 * 1000;
	}
	
	/*****
	 * 获取指定日期中午12 UTC时间
	 * @param utc
	 * @return
	 */
	public static long getMiddyUtc(long utc){
		return utc + 12 * 60 * 60 * 1000;
	}
	
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
	public static long getYesDayUTC() {
		return getCurrentDayUTC() - 24 * 60 * 60 * 1000;
	}

	/**
	 * 得到当天的年月日Long类型
	 * 零点
	 * 用于查询
	 * @return
	 */
	public static long getCurrentDayUTC() {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	
	

	/**
	 * 获取上一年份
	 * 
	 * @return
	 */
	public static int getPreviousYear() {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(getCurrentDayYearMonthDay());
		return cal.get(Calendar.YEAR) - 1;
	}
	
	/**
	 * 根据long整型时间获取年份
	 * 
	 * 
	 * @return
	 */
	public static int getYear(long time) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(time);
		return cal.get(Calendar.YEAR);
	}

	/**
	 * 获取上一周
	 * 
	 * @return
	 */
	public static int getPreviousWeek() {
		Calendar cal = Calendar.getInstance();
		cal.setFirstDayOfWeek(Calendar.MONDAY);
		cal.setTimeInMillis(getCurrentDayYearMonthDay() - 7 * 24 * 60 * 60 * 1000);
		return cal.get(Calendar.WEEK_OF_YEAR);
	}
	
	/**
	 * 获取指定日期所对应的周
	 * @param d
	 * @return
	 */
	public static int getDaysWeek(Date d) {
		Calendar cal = Calendar.getInstance();
		cal.setFirstDayOfWeek(Calendar.MONDAY);
		cal.setTime(d);
		return cal.get(Calendar.WEEK_OF_YEAR);
	}

	/**
	 * 获取上一月
	 * 
	 * @return
	 */
	public static int getPreviousMonth() {
		Calendar lastDate = Calendar.getInstance();
		//lastDate.add(Calendar.MONTH);// 减一个月
		return lastDate.get(Calendar.MONTH);
	}

	/**
	 * 获取上一月的UTC格式
	 * 
	 * @return
	 */
	public static long getPreviousMonthUtc() {
		Calendar lastDate = Calendar.getInstance();
		
		lastDate.add(Calendar.MONTH, -1);// 减一个月
		return lastDate.getTimeInMillis();
	}

	/**
	 * 根据参数获取一周周一的UTC格式时间
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

	/**
	 * 获取下一个月的UTC格式
	 * 
	 * @return
	 */
	public static long getNextMonthUtc() {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.DAY_OF_MONTH, 1);
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		cal.add(Calendar.MONTH, 1);
		return cal.getTimeInMillis();
	}
	
	/**
	 * 以当前年月为基准，获取某年、某月、某日、某时、某分、某秒的UTC格式
	 * 
	 * @return
	 */
	public static Date getDate(int year,int month,int day,int hour,int minute,int second,int millisecond) {
		Calendar cal = Calendar.getInstance();
		cal.add(Calendar.YEAR, year);
		cal.add(Calendar.MONTH, month);
		cal.set(Calendar.DAY_OF_MONTH, day);
		cal.set(Calendar.HOUR_OF_DAY, hour);
		cal.set(Calendar.MINUTE, minute);
		cal.set(Calendar.SECOND, second);
		cal.set(Calendar.MILLISECOND, millisecond);
		
		return cal.getTime();
	}
	
	/**
	 * 以指定日期为基准，获取某年、某月、某日、某时、某分、某秒的UTC格式
	 * 
	 * @return
	 */
	public static Date getDateFromParam(Date srcDate,int year,int month,int day,int hour,int minute,int second,int millisecond) {
		Calendar cal = Calendar.getInstance();
		cal.setTime(srcDate);
		cal.add(Calendar.YEAR, year);
		cal.add(Calendar.MONTH, month);
		cal.set(Calendar.DAY_OF_MONTH, day);
		cal.set(Calendar.HOUR_OF_DAY, hour);
		cal.set(Calendar.MINUTE, minute);
		cal.set(Calendar.SECOND, second);
		cal.set(Calendar.MILLISECOND, millisecond);
		
		return cal.getTime();
	}
	
	/**
	 * 获得指定日期指定分钟数后的时间
	 * @param srcDate
	 * @param hour
	 * @return
	 */
	public static Date getNextHourDateFromParam(Date srcDate,int minute){
		Calendar cal = Calendar.getInstance();
		cal.setTime(srcDate);
		cal.add(Calendar.MINUTE,minute);
		return cal.getTime();
	}

	/**
	 * 获取下一年的UTC格式
	 * 
	 * @return
	 */
	public static long getNextYearUtc() {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.MONTH, 0);
		cal.set(Calendar.DAY_OF_MONTH, 1);
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		cal.add(Calendar.YEAR, 1);
		return cal.getTimeInMillis();
	}

	/**
	 * 根据参数获取年月的UTC格式
	 * 
	 * @return
	 */
	public static long getYearMonthUtc(int num) {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.DAY_OF_MONTH, 1);
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		cal.add(Calendar.MONTH, num);
		return cal.getTimeInMillis();
	}
	/**
	 * 获取当前系统日期的UTC格式 Long
	 * 
	 * @return
	 */
	public static long getYearMonthDayUtc() {
		Calendar cal = Calendar.getInstance();
		return cal.getTimeInMillis();
	}

	/**
	 * 功能：得到当前月份 格式为：xxxx-yy (eg: 2007-01-01)<br>
	 * 
	 * @return String
	 * @author pure
	 */
	public static String thisMonth() {
		String strY = null;
		int x = localTime.get(Calendar.YEAR);
		int y = localTime.get(Calendar.MONTH) + 1;
		strY = y >= 10 ? String.valueOf(y) : ("0" + y);
		return x + "-" + strY;

	}

	/**
	 * 功能：获得上一月<br>
	 * 
	 * @return String
	 * @author pure
	 */
	public static String lastMonth() {
		String strY = null;
		Calendar curTime = Calendar.getInstance(); 
		int x = curTime.get(Calendar.YEAR);
		int y = curTime.get(Calendar.MONTH);
		if (y == 0) {
			x = x - 1;
			y = 12;
		}
		strY = y >= 10 ? String.valueOf(y) : ("0" + y);
		return x + "-" + strY;
	}
	/**
	 * 功能：智能维保日期计算<br>
	 * 
	 * @return 上次维保日期,间隔天数,提醒天数
	 * @author pure
	 */
	public static String  getMainTainDate(String mainTainDate,int intervalDays,int warnDays){
		String r = "";
		try {
			Date date=DateTools.changeDateFormat("yyyy-MM-dd", mainTainDate);
			Calendar cal = Calendar.getInstance();
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
			cal.setTime(date);	
			cal.add(Calendar.DAY_OF_MONTH, intervalDays-warnDays);		
			r = sdf.format(cal.getTime());
		} catch (Exception e) {
			log.error(e.getMessage());
		}
		return r;
	}
	

	
	/**
	 * 功能：智能维保比较两个日期大小<br>
	 * 
	 * @return 当前日期 ,基准日期
	 * @author pure
	 */
	public static boolean compareDoubleDate(String currentDate,String compareDate ){
		long currentD=0,compareD=0;
		Calendar cal = Calendar.getInstance();
		cal.setTime(changeDateFormat("yyyy-MM-dd",currentDate));	
		currentD=cal.getTimeInMillis();		
		cal.setTime(changeDateFormat("yyyy-MM-dd",compareDate));	
		compareD=cal.getTimeInMillis();			
		return (currentD>=compareD);
	}
	
	/****
	 *  比较上报GPS时间是否在凌晨2点到5点之间
	 * @param secTime
	 * @param gpsTimeUtc
	 * @return
	 */
	public static boolean checkSecTime(long startTime,long endTime,long gpsTimeUtc){
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(gpsTimeUtc);
		cal.set(Calendar.MILLISECOND, 0);
		long hour = cal.get(Calendar.HOUR_OF_DAY) * 60 * 60 * 1000;
		
		if(startTime <= hour && endTime > hour){
			return true;
		}
		return false;
	}
	
	/**
	 * 获得指定日期的小时
	 * @param timeUtc
	 * @return
	 */
	public static int getOnedayHour(long timeUtc){
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(timeUtc);
		int hour = cal.get(Calendar.HOUR_OF_DAY);
		return hour;
	}
	
	/**
	 * 设定某天的时分秒
	 * @param timeUtc
	 * @param hourofday
	 * @param minute
	 * @param second
	 * @return
	 */
	public static long setOnedayHMS(long timeUtc,int hourofday,int minute,int second){
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(timeUtc);
		cal.set(Calendar.HOUR_OF_DAY, hourofday);
		cal.set(Calendar.MINUTE, minute);
		cal.set(Calendar.SECOND, second);
		cal.set(Calendar.MILLISECOND, 0);
		
		return cal.getTimeInMillis();
	}
	
	/****
	 * 判断当前日期是否为当前月1号
	 * @param utc
	 * @return
	 */
	public static boolean checkIsFirstDayMonth(long utc){
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(utc);
		if(c.get(Calendar.DAY_OF_MONTH) == 1 ){
			return true;
		}
		return false;
	}
	
	/****
	 * 判断当前日期是否为当年的1月1号
	 * @param utc
	 * @return
	 */
	public static boolean checkIsFirstDayMonthYear(long utc){
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(utc);
		if(c.get(Calendar.DAY_OF_MONTH) == 1 && (c.get(Calendar.MONTH) + 1) == 1){
			return true;
		}
		return false;
	}
	
	/*
	 * 功能：根据传入日期和间隔天数，计算间隔后的时间
	 */
	public static String addDays(String addDate,int days){
		
		String r = "";
		
		try {
			Date date=DateTools.changeDateFormat("yyyy-MM-dd", addDate);
			Calendar cal = Calendar.getInstance();
			cal.setTime(date);	
			cal.add(Calendar.DAY_OF_MONTH, days);
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
			r = sdf.format(cal.getTime());
		} catch (Exception e) {
			log.error(e.getMessage());
		}
		
		return r;
		
	}
	
	/**
	 * 将 yyMMddHHmmss格式字符串转换为long
	 * 
	 * @param strDate
	 * @return
	 */
	public static Long strToLong(String strDate) {
		SimpleDateFormat formatter = new SimpleDateFormat("yyMMddHHmmss");
		ParsePosition pos = new ParsePosition(0);
		Date strtodate = formatter.parse(strDate, pos);
		Calendar cal = Calendar.getInstance();
		cal.setTime(strtodate);
		return cal.getTimeInMillis();
	}

	/***
	 * 时间段内间隔最大值
	 * @param endTime
	 * @param startTime
	 * @param unitInterval
	 * @return
	 */
	public static int accountTimeIntervalVale(String endTime,String startTime,int unitInterval,float unitTime){
		int intervalCount = Math.round(Float.parseFloat(Math.abs(stringConvertUtc(endTime) - stringConvertUtc(startTime)) + "") / 1000.0f / unitTime) ;
		// 判断间隔是否在一分钟以上，防止盲区数据
		if(intervalCount * unitInterval > unitInterval){
			return intervalCount * unitInterval;
		}else{
			return unitInterval;
		}
	}
	
	public static int accountTimeIntervalVale(Long endTime,Long startTime,int unitInterval,float unitTime){
		int intervalCount = Math.round(Float.parseFloat(Math.abs(endTime - startTime) + "") / 1000.0f / unitTime) ;
		// 判断间隔是否在一分钟以上，防止盲区数据
		if(intervalCount * unitInterval > unitInterval){
			return intervalCount * unitInterval;
		}else{
			return unitInterval;
		}
	}
	
	public static String date2StrByFormat(Date dt,String format){
		SimpleDateFormat formatter = new SimpleDateFormat(format);
		String dateString = formatter.format(dt);
		return dateString;
	}
	
	/**
	 * 获取下月第一天格式化字符串
	 * @param format
	 * @return
	 */
	public static String getNextMonthFirstDayByFormat(String format){
		return date2StrByFormat(getNextMonthFirstDay(),format);
	}
	
	public static Date getNextMonthFirstDay(){
		return getDate(0, 1, 1, 0, 0, 0, 0);
	}
	
	/**
	 * 获取某月天数
	 * @param dt
	 * @return
	 */
	public static int getMonthDaynum(Date dt){
		Calendar gc = Calendar.getInstance();
		gc.setTime(dt);
		int daynum = 0;
		switch ( gc.get( Calendar.MONTH ) )
		  {
		   case 0:
			   daynum = 31;
		    break;
		   case 1:
			   daynum = 28;
		    break;
		   case 2:
			   daynum = 31;
		    break;
		   case 3:
			   daynum = 30;
		    break;
		   case 4:
			   daynum = 31;
		    break;
		   case 5:
			   daynum = 30;
		    break;
		   case 6:
			   daynum = 31;
		    break;
		   case 7:
			   daynum = 31;
		    break;
		   case 8:
			   daynum = 30;
		    break;
		   case 9:
			   daynum = 31;
		    break;
		   case 10:
			   daynum = 30;
		    break;
		   case 11:
			   daynum = 31;
		    break;
		  }
		  //检查闰年
		  if ( ( gc.get( Calendar.MONTH ) == Calendar.FEBRUARY )
		   && ( isLeapYear( gc.get( Calendar.YEAR ) ) ) )
		  {
			  daynum = 29;
		  }
		  
		  return daynum;
	}

	/**
	 * 判断某年是否为闰年
	 * @param year
	 * @return
	 */
	public static boolean isLeapYear( int year )
	 {
	  /** *//**
	   * 详细设计： 1.被400整除是闰年，否则： 2.不能被4整除则不是闰年 3.能被4整除同时不能被100整除则是闰年
	   * 3.能被4整除同时能被100整除则不是闰年
	   */
	  if ( ( year % 400 ) == 0 )
	   return true;
	  else if ( ( year % 4 ) == 0 )
	  {
	   if ( ( year % 100 ) == 0 )
	    return false;
	   else return true;
	  }
	  else return false;
	 }
	
	/**
	 * 获得当前日期为本年的第几周，周1为每周第一天
	 * @return
	 */
	public static int getWeekOfYearByCurrentDate(){
		return getWeekOfYearByDate(new Date());
	}
	
	/**
	 * 获取指定日期为当年第几周，周1为每周第一天
	 * @param d
	 * @return
	 */
	public static int getWeekOfYearByDate(Date d){
		Calendar gc = Calendar.getInstance();
		gc.setFirstDayOfWeek(Calendar.MONDAY);
		gc.setTime(d);
		return gc.get(Calendar.WEEK_OF_YEAR);
	}
	
	/**
	 * 取某周的第一天 ,周1为每周第一天
	 * @param year
	 * @param weekOfYear
	 * @return
	 */
	public static Date getFirstDayOfWeek(int year,int weekOfYear){
		Calendar gc = Calendar.getInstance();
		gc.set(Calendar.YEAR, year);
		gc.setFirstDayOfWeek(Calendar.MONDAY);
		gc.set(Calendar.WEEK_OF_YEAR, weekOfYear);
		gc.set(Calendar.DAY_OF_WEEK, gc.getFirstDayOfWeek());
		gc.set(Calendar.HOUR_OF_DAY, 0);
		gc.set(Calendar.MINUTE, 0);
		gc.set(Calendar.SECOND, 0);
		gc.set(Calendar.MILLISECOND, 0);
		return gc.getTime();
	}
	
	/**
	 * 取某周的最后一天 ,周1为每周第一天
	 * @param year
	 * @param weekOfYear
	 * @return
	 */
	public static Date getLastDayOfWeek(int year,int weekOfYear){
		Calendar gc = Calendar.getInstance();
		gc.set(Calendar.YEAR, year);
		gc.setFirstDayOfWeek(Calendar.MONDAY);
		gc.set(Calendar.WEEK_OF_YEAR, weekOfYear);
		gc.set(Calendar.DAY_OF_WEEK, gc.getFirstDayOfWeek()+6);
		gc.set(Calendar.HOUR_OF_DAY, 0);
		gc.set(Calendar.MINUTE, 0);
		gc.set(Calendar.SECOND, 0);
		gc.set(Calendar.MILLISECOND, 0);
		return gc.getTime();
	}
	
	/**
	 * 获得当前日期处在本年的第几月
	 * @return
	 */
	public static int getMonthOfYearByCurrentDate(){
		return getMonthOfYearByDate(new Date());
	}
	
	/**
	 * 获取指定日期处在当年第几月
	 * @param d
	 * @return
	 */
	public static int getMonthOfYearByDate(Date d){
		Calendar gc = Calendar.getInstance();
		gc.setTime(d);
		return gc.get(Calendar.MONTH);
	}
	
	/**
	 * 取某月的第一天
	 * @param year
	 * @param weekOfYear
	 * @return
	 */
	public static Date getFirstDayOfMonth(int year,int month){
		Calendar gc = Calendar.getInstance();
		gc.set(Calendar.YEAR, year);
		gc.set(Calendar.MONTH, month-1);
		gc.set(Calendar.DAY_OF_MONTH, 1);
		gc.set(Calendar.HOUR_OF_DAY, 0);
		gc.set(Calendar.MINUTE,0);
		gc.set(Calendar.SECOND, 0);
		gc.set(Calendar.MILLISECOND,0);
		return gc.getTime();
	}
	
	
	
	/**
	 * 根据long整型时间转换成字符串yyyyMMdd
	 * time 欲转换的整型日期
	 * formatStr 转换后的日期格式
	 * @return
	 */
	public static String utc2Str(long time,String formatStr) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(time);
		SimpleDateFormat sdf = new SimpleDateFormat(formatStr);
		return sdf.format(cal.getTime());
	}
	
    public static long TimeToUTC(String time){
    	String[] s=time.split(":");
    	long utc=0;
    	if (s.length==1){
    		utc=Integer.parseInt(s[0])*3600;
    	}else if (s.length==2){
    		utc=Integer.parseInt(s[0])*3600+Integer.parseInt(s[1])*60;
    	}else if (s.length==3){
    		utc=Integer.parseInt(s[0])*3600+Integer.parseInt(s[1])*60+Integer.parseInt(s[2]);
    	}

    	return utc;
    }
    
    /**
     * 长整形毫秒数转换为日期
     * @param time
     * @return
     */
    public static Date convertUtc2Date(long time) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(time);
		return cal.getTime();
	}
    
    /**
     * 获取上个月时间 如“2014/9”
     * @return
     */
    public static String getPreYearMonth(){
    	int year = getCurrentYear();
    	int month = getCurrentMonth();
    	if(month == 1){
    		year = year - 1;
    		month = 12;
    	}else {
    		month = month - 1;
    	}
    	return year+"/"+month;
    }
    
    /**
     * 获取下个月时间 如“2014/10”
     * @return
     */
    public static String getNextYearMonth(String yearMonth){
    	int year = Integer.parseInt(yearMonth.split("/")[0]);
    	int month = Integer.parseInt(yearMonth.split("/")[1]);
    	if(month == 12){
    		year = year + 1;
    		month = 1;
    	}else {
    		month = month + 1;
    	}
    	return year+"/"+month;
    }

	
	public static void main(String argv[]) throws Exception {
		System.out.println(DateTools.getPreYearMonth().replace("/", "-"));
		System.out.println(DateTools.lastMonth());
		//Calendar c = Calendar.getInstance();
		//c.setTimeInMillis(1321000052083l);
		System.out.println(DateTools.getCurrentMonth());
		//System.out.println(c.get(Calendar.DAY_OF_MONTH) + "," + (c.get(Calendar.MONTH)+1) + "," + c.get(Calendar.HOUR_OF_DAY) + "," + c.get(Calendar.MINUTE));
		//System.out.println( CDate.getPreviousWeek());
		System.out.println( "---------;;;"+getFirstDayOfMonth(2013,4).getTime());
		Long a = stringConvertUtc("20120220/000000");
//		Long a1 = stringConvertUtc("20120220/235939");
		System.out.println(Integer.toBinaryString(300));
		System.out.println(Integer.parseInt("1001011000001111100011111", 2));
		Date dt = DateTools.strToDateByFormat("20121229", "yyyyMMdd");//转换成当天中午12点时间
		int week = DateTools.getDaysWeek(dt);
		System.out.println(week + "--" +DateTools.getPreviousWeek());
		System.out.println(getFirstDayOfWeek(2012,53)+"----"+getLastDayOfWeek(2012,53));
		
//		System.out.println(CDate.checkSecTime(2*60*60*1000, 5*60*60*1000, 1332359003000l));
		System.out.println("统计数据开始日期"+DateTools.utc2Str(a, "yyyyMMdd")+"告警表分区名称："+"TH_VEHICLE_ALARM"+DateTools.utc2Str(a + 24 * 60 * 60 * 1000, "yyyyMMdd"));
	}
	
}
