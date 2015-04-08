package com.ctfo.analy.util;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import org.apache.log4j.Logger;

/**
 * 时间工具类
 * @author yangyi
 *
 */
public class CDate
{
	private static Logger log = Logger.getLogger(CDate.class);

	/**
	 * 获取现在时间
	 * 
	 * @return 返回时间类型 yyyy-MM-dd HH:mm:ss
	 */
	public static Date getNowDate()
	{
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
	@SuppressWarnings("finally")
	public static String changeDateFormat(String srcformat, String destformat, String srcdatastr)
	{
		// yyyymmdd/hhmmss
		DateFormat format1 = new SimpleDateFormat(srcformat);
		DateFormat format2 = new SimpleDateFormat(destformat);
		Date date = null;
		String str = null;
		try
		{
			date = format1.parse(srcdatastr);
			str = format2.format(date);
		}
		catch (ParseException e)
		{
			log.error(e.getMessage());
			e.printStackTrace();
		}
		finally
		{
			return str;
		}
	}
	@SuppressWarnings("finally")
	public static Date changeDateFormat(String srcformat,  String srcdatastr)
	{
		// yyyymmdd/hhmmss
		DateFormat format1 = new SimpleDateFormat(srcformat);
		 
		Date date = null;
		 
		try
		{
			date = format1.parse(srcdatastr);
			 
		}
		catch (ParseException e)
		{
			log.error(e.getMessage());
			e.printStackTrace();
		}
		finally
		{
			return date;
		}
	}
	/**
	 * 获取现在时间
	 * 
	 * @return返回字符串格式 yyyy-MM-dd HH:mm:ss
	 */
	public static String getStringDate()
	{
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
	public static String getStringDateShort()
	{
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
	public static String getTimeShort()
	{
		SimpleDateFormat formatter = new SimpleDateFormat("HH:mm:ss");
		Date currentTime = new Date();
		String dateString = formatter.format(currentTime);
		return dateString;
	}

	/**
	 * 将长时间格式字符串转换为时间 yyyy-MM-dd HH:mm:ss
	 * 
	 * @param strDate
	 * @return
	 */
	public static Date strToDateLong(String strDate)
	{
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
	public static String dateToStrLong(java.util.Date dateDate)
	{
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
	public static String dateToStr(java.util.Date dateDate)
	{
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
		String dateString = formatter.format(dateDate);
		return dateString;
	}

	/**
	 * 将短时间格式字符串转换为时间 yyyy-MM-dd
	 * 
	 * @param strDate
	 * @return
	 */
	public static Date strToDate(String strDate)
	{
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
	public static Date getNow()
	{
		Date currentTime = new Date();
		return currentTime;
	}

	/**
	 * 提取一个月中的最后一天
	 * 
	 * @param day
	 * @return
	 */
	public static Date getLastDate(long day)
	{
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
	public static String getStringToday()
	{
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMdd HHmmss");
		String dateString = formatter.format(currentTime);
		return dateString;
	}

	/**
	 * 得到现在小时
	 */
	public static String getHour()
	{
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		String dateString = formatter.format(currentTime);
		String hour;
		hour = dateString.substring(11, 13);
		return hour;
	}

	/**
	 * 得到现在分钟
	 * 
	 * @return
	 */
	public static String getTime()
	{
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
	public static String getUserDate(String sformat)
	{
		Date currentTime = new Date();
		SimpleDateFormat formatter = new SimpleDateFormat(sformat);
		String dateString = formatter.format(currentTime);
		return dateString;
	}

	@SuppressWarnings("finally")
	public static String getNextNDate(int i)
	{
		Calendar cal = Calendar.getInstance();
		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy_MM_dd");
		String r = "";
		try
		{
			cal.setTime(date);
			cal.add(Calendar.DATE, i);
			r = sdf.format(cal.getTime());
			// System.out.println("下一天的时间是：" + sdf.format(cal.getTime()));
		}
		catch (Exception e)
		{
			log.error(e.getMessage());
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		finally
		{
			return r;
		}
	}
	
	/** /KCPTDataAnaly
	 * 时间(yyyymmdd/hhmmss) 转换成长整型utc格式
	 * @return
	 */
	public static long stringConvertUtc(String time){
		if(time == null || !time.matches("\\d{8}/\\d{6}")){
			return 0;
		}
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.YEAR, Integer.parseInt(time.substring(0, 4)));
		cal.set(Calendar.MONTH, Integer.parseInt(time.substring(4, 6)) -1);
		cal.set(Calendar.DAY_OF_MONTH, Integer.parseInt(time.substring(6, 8)));
		cal.set(Calendar.HOUR_OF_DAY, Integer.parseInt(time.substring(9, 11)));
		cal.set(Calendar.MINUTE, Integer.parseInt(time.substring(11, 13)));
		cal.set(Calendar.SECOND, Integer.parseInt(time.substring(13, 15)));
		cal.set(Calendar.MILLISECOND, 0);
		long utc = cal.getTimeInMillis();
		return utc;
	}
	
	/**
	 * 得到前一天的年月日Long类型
	 * @return
	 */
	public static long getYesDayYearMonthDay(){
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.HOUR_OF_DAY , 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		cal.setTimeInMillis(cal.getTimeInMillis() - 24 * 60 * 60 * 1000);
		return cal.getTimeInMillis();
	}
	
	/**
	 * 得到当天的年月日Long类型
	 * @return
	 */
	public static long getCurrentDayYearMonthDay(){
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.HOUR_OF_DAY , 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	
	/**
	 * 获取上一年份
	 * @return
	 */
	public static int getPreviousYear(){
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(getPreviousMonthUtc());
		return cal.get(Calendar.YEAR);
	}
	
	/**
	 * 获取上一周
	 * @return
	 */
	public static int getPreviousWeek(){
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(getPreviousMonthUtc());
		return cal.get(Calendar.WEEK_OF_YEAR);
	}
	
	/**
	 * 获取上一月
	 * @return
	 */
    public static int getPreviousMonth(){    
       Calendar lastDate = Calendar.getInstance(); 
       lastDate.add(Calendar.MONTH,-1);//减一个月 
       return (lastDate.get(Calendar.MONTH)+ 1);    
    } 
    
    /**
	 * 获取上一月的UTC格式
	 * @return
	 */
    public static long getPreviousMonthUtc(){    
       Calendar lastDate = Calendar.getInstance(); 
       lastDate.add(Calendar.MONTH,-1);//减一个月 
       return lastDate.getTimeInMillis();    
    }
    
    /**
	 * 根据参数获取一周中周一的UTC格式时间
	 * @return
	 */
    public static long getWeekUtc(int num){    
       Calendar cal = Calendar.getInstance();
       cal.set(Calendar.DAY_OF_MONTH, 1);
	   cal.set(Calendar.HOUR_OF_DAY , 0);
	   cal.set(Calendar.MINUTE, 0);
	   cal.set(Calendar.SECOND, 0);
	   cal.set(Calendar.MILLISECOND, 0);
	   cal.add(Calendar.WEEK_OF_YEAR, num);
	   cal.add(Calendar.DAY_OF_YEAR, 1); // 获取是下一周周一00:00:00的时间
       return cal.getTimeInMillis();    
    }
    
    /**
	 * 获取下一个天的UTC格式
	 * @return
	 */
    public static long getNextNDayUtc(int n){
       Calendar cal = Calendar.getInstance();
	   cal.set(Calendar.HOUR_OF_DAY , 0);
	   cal.set(Calendar.MINUTE, 0);
	   cal.set(Calendar.SECOND, 0);
	   cal.set(Calendar.MILLISECOND, 0);
	   cal.add(Calendar.DATE, n);
       return cal.getTimeInMillis();    
    }
    
    /**
	 * 获取下一个天的UTC格式
	 * @return
	 */
    public static long getNextNDayUtc(long dateTime,int n){
       Calendar cal = Calendar.getInstance();
       cal.setTimeInMillis(dateTime);
	   cal.add(Calendar.DATE, n);
       return cal.getTimeInMillis();    
    }
    
    /**
	 * 获取下一个月的UTC格式
	 * @return
	 */
    public static long getNextMonthUtc(){    
       Calendar cal = Calendar.getInstance();
       cal.set(Calendar.DAY_OF_MONTH, 1);
	   cal.set(Calendar.HOUR_OF_DAY , 0);
	   cal.set(Calendar.MINUTE, 0);
	   cal.set(Calendar.SECOND, 0);
	   cal.set(Calendar.MILLISECOND, 0);
	   cal.add(Calendar.MONTH, 1);
       return cal.getTimeInMillis();    
    }
    
    /**
	 * 获取下一年的UTC格式
	 * @return
	 */
    public static long getNextYearUtc(){    
       Calendar cal = Calendar.getInstance();
       cal.set(Calendar.MONTH, 1);
       cal.set(Calendar.DAY_OF_MONTH, 1);
	   cal.set(Calendar.HOUR_OF_DAY , 0);
	   cal.set(Calendar.MINUTE, 0);
	   cal.set(Calendar.SECOND, 0);
	   cal.set(Calendar.MILLISECOND, 0);
	   cal.add(Calendar.YEAR, 1);
       return cal.getTimeInMillis();    
    }
    
    /**
	 * 根据参数获取年月的UTC格式
	 * @return
	 */
    public static long getYearMonthUtc(int num){    
       Calendar cal = Calendar.getInstance();
       cal.set(Calendar.DAY_OF_MONTH, 0);
	   cal.set(Calendar.HOUR_OF_DAY , 0);
	   cal.set(Calendar.MINUTE, 0);
	   cal.set(Calendar.SECOND, 0);
	   cal.set(Calendar.MILLISECOND, 0);
	   cal.add(Calendar.MONTH, num);
       return cal.getTimeInMillis();    
    }
    
    public static Long getCurrentUtcMsDate(){
 		Long dateLong = System.currentTimeMillis();
       return dateLong;
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
    
    private static boolean checkTime(long currentTime,String beginTime,String endTime){
		boolean flag = false;
		String currDay = CDate.getStringDateShort();
		
		long fromTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(beginTime)*1000;
		long toTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(endTime)*1000;
		if (fromTime>toTime){
			if (currentTime>fromTime||(currentTime<fromTime&&currentTime>toTime)){
				toTime = CDate.getNextNDayUtc(1)+CDate.TimeToUTC(endTime)*1000;
			}else
			if (currentTime<toTime){
				fromTime = CDate.getNextNDayUtc(-1)+CDate.TimeToUTC(beginTime)*1000;
				if ((toTime-fromTime)>12*60*60*1000){
					fromTime = CDate.getNextNDayUtc(1)+CDate.TimeToUTC(beginTime)*1000;
				}
			}
		}
		
		if (fromTime<currentTime&&currentTime<=toTime){
			flag=true;
		}

		return flag;
	}
    
    /**
	 * 根据输入时间（HH:mm）获取整型时间
	 * 
	 * @return
	 */
	public static int getTimeShort(String time)
	{
		String[] arr = time.split(":");
		return Integer.parseInt(arr[0]) * 60 * 60 * 1000 + Integer.parseInt(arr[1])* 60 * 1000 ;
	}
	
	 public static Date getschedule(String time){
	 	String[] arrTime = time.split(":");
	    Calendar tomorrow = new GregorianCalendar();
	    tomorrow.add(Calendar.DATE, 1);
	    Calendar result = new GregorianCalendar(
	      tomorrow.get(Calendar.YEAR),
	      tomorrow.get(Calendar.MONTH),
	      tomorrow.get(Calendar.DATE),
	      Integer.parseInt(arrTime[0]),
	      Integer.parseInt(arrTime[1])
	    );
	    return result.getTime();
	 }
    
	 public static void main(String argv[]) throws Exception
	 {
//			 Calendar cal = Calendar.getInstance();
//			 cal.setTimeInMillis(1335354737000L);
//			 String yearString=cal.get
		 
			Date currentTime = new Date();
			SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMdd/HHmmss");
			String dateString = formatter.format(currentTime); 
			System.out.println(currentTime.getTime()+"::"+dateString);
			long lon1=stringConvertUtc("20121218/155623");
			long lon2 = CDate.getNextNDayUtc(-1)+CDate.TimeToUTC("15:56:23")*1000;
			long fromTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC("20:20:32")*1000;
			long toTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC("01:30:00")*1000;
			
			System.out.println(lon1+"---"+lon2+"---"+fromTime+"--"+toTime);
			
			String date1String = formatter.format(fromTime); 
			System.out.println(currentTime.getTime()+":111:"+date1String);
			
			long lon0=CDate.stringConvertUtc("20121220/212023");
			String a0="20:20:32";
			String a1="01:30:00";
			System.out.println(checkTime(lon0,a0,a1));
		
	 }
		/***
		 * 将HH:MM:SS 转成秒格式
		 * 
		 * @param time
		 * @return
		 */
		public static long timeConvertSec(String time) {
			if("".equals(time)||null==time){
				return 0;
			}
			String[] arrays = time.split(":");
			if (arrays.length == 3) {
				return Integer.parseInt(arrays[0]) * 60 * 60
						+ Integer.parseInt(arrays[1]) * 60
						+ Integer.parseInt(arrays[2]);
			} else {
				log.error("设置时间格式不正确。" + time);
			}
			return 0;
		}
}
