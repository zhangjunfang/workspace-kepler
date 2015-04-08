package com.ctfo.util;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import com.ctfo.baseinfo.service.impl.SysGeneralCodeServiceImpl;
import com.ctfo.local.exception.CtfoAppException;



public class DateUtil
{
	private static String VIEW_WHEN_NULL = "--";
    static SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
    /**
	 * 获取时间段的时分秒格式
	 * 
	 * @param seconds
	 *            时间段的秒数
	 * @return 如果时间段不可用，为VIEW_WHEN_NULL常量，否则为时间段的时分秒格式（可以处理负数时间）。
	 */
	public static String getHMSColonFormateOfTimePeriodBySeconds(Long seconds) {
		if (seconds == null) {
			return VIEW_WHEN_NULL;
		} else if (seconds == 0) {
			return "00:00:00";
		}

		boolean isMinueNum = false;

		if (seconds == null || seconds <= 0) {
			isMinueNum = true;
			seconds = Math.abs(seconds);
		}

		long h = seconds / 3600;
		long m = (seconds % 3600) / 60;
		long s = (seconds % 3600) % 60;
		StringBuffer sb = new StringBuffer();

		if (h > 0) {
			if (h < 10) {
				sb.append("0");
			}
			sb.append(h);
		} else {
			sb.append("00");
		}
		sb.append(":");

		if (m > 0) {
			if (m < 10) {
				sb.append("0");
			}
			sb.append(m);
		} else {
			sb.append("00");
		}
		sb.append(":");

		if (s > 0) {
			if (s < 10) {
				sb.append("0");
			}
			sb.append(s);
		} else {
			sb.append("00");
		}

		if (isMinueNum) {
			sb = sb.insert(0, "-");
		}

		return sb.toString();

	}
   
	 /**
		 * @description: 根据时间获取(utcMs)时间毫秒
		 */
	public static Long dateToUtcTime(Date date) {

		// 将本地当前时间转换成UTC国际标准时间的毫秒形式
		if (date != null) {
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(date);
			return calendar.getTimeInMillis();
		} else {
			return null;
		}

	}
	/**
	 * 将utc时间数转换为date时间类型
	 * 
	 * @param utcTime
	 *            utc时间数(s)
	 * @return date时间
	 */
	public static Date utcTimeToDate(Long utcTime) {
		Date resultDate = null;
		if (utcTime != null) {
			// 将UTC国际标准时间的毫秒形式转换成本地的时间
			Calendar calendar = Calendar.getInstance();
			calendar.setTimeInMillis(utcTime);
			resultDate = calendar.getTime();
		}
		return resultDate;
	}
	
	/**
	 * UTC时间转换-Long转String
	 * 
	 * @param utc
	 *            毫秒
	 * @param format
	 *            格式化
	 * @return String
	 */
	public static String utcToStr(Long utc, String format) {
		String result = "";
		if (null == utc || null == format) {
			return result;
		}
		SimpleDateFormat sdf = new SimpleDateFormat(format);
		try {
			result = sdf.format(new Date(utc));
		} catch (Exception e) {
			e.printStackTrace();
		}
		return result;
	}
    
    /**
     * 开发人： 张波
     * 开发时间： 2011-7-9 上午10:30:06
     * 功能描述：按指定格式对日期进行格式化
     * 方法的参数和返回值
     * @param date
     * @param format
     * @return
     * String 
     */
    public static String format(Date date,String format){
    	if(date==null)return null;
    	sdf.applyPattern(format);
    	return sdf.format(date);
    }
    
    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午10:30:41
     * 功能描述：按指定格式对字符串进行日期转换
     * 方法的参数和返回值
     * @param dateStr
     * @param format
     * @return
     * @throws ParseException
     * Date 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static Date parse(String dateStr,String format) throws ParseException{
    	if(dateStr==null)return null;
    	sdf.applyPattern(format);
    	return sdf.parse(dateStr);
    }

    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午10:31:03
     * 功能描述：按yyyy-MM-dd格式对指定日期进行格式化
     * 方法的参数和返回值
     * @param date
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static String format(Date date){
    	return format(date,"yyyy-MM-dd");
    }
    
    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午10:31:22
     * 功能描述：按“年 月 日 小时 分钟 秒”的格式对字符串进行日期转换，
     * 以非数字字符对字符串进行分隔，如不足6个数字，则后边进行补0。
     * 可匹配以下任意格式：2011-3-28，2011-03-28，2011－03－28，2011年03月28日，
     * 	2011-3-28 14:29:8，2011-03-28 14:29:08，2011－03－28 14：29：08，2011年03月28日14时29分8秒，
     * 	2011-3-28 14:2，2011-03-28 14:02，2011－03－28 14：02，2011年03月28日14时2分，等
     * 方法的参数和返回值
     * @param dateStr
     * @return
     * @throws ParseException
     * Date 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static Date parse(String dateStr) throws ParseException{
		if(StringUtil.isBlank((String)dateStr)){
			return null;
		}
		Date result = null;
		try {
			String[] ds = ((String)dateStr).split("\\D");
			int[] dds = new int[7];
			for(int i=0;i<dds.length;i++){
				dds[i] = 0;
				try {
					dds[i] = Integer.parseInt(ds[i]);
				} catch (Exception e) {
				}
			}
			Calendar c = Calendar.getInstance();
			c.set(dds[0],dds[1]-1,dds[2],dds[3],dds[4],dds[5]);
			result = c.getTime();
		} catch (Exception e) {
			System.out.println("dateconverter is failed!");
		}
    	return result;
    }
    /**
     * 
    * 开发人：张波
    * 开发日期：2007-3-23
    * 开发时间：8:44:00
    * 功能描述：返回两个日期之间的天数，只计日期，不计时分秒，
    * 如2011-02-04 00:00:00与2011-02-05 23:59:59相差1天
    * 方法的参数和返回值：
     * @throws CtfoAppException 
     */
    public static int getDiffDay(Date dateStart,Date dateEnd) throws CtfoAppException
    {
    	if(dateStart==null||dateEnd==null){
    		throw new CtfoAppException("日期参数不能为空");
    	}
    	Calendar c1 = Calendar.getInstance();
    	Calendar c2 = Calendar.getInstance();
    	c1.setTime(dateStart);
    	c2.setTime(dateEnd);
    	c1.clear(Calendar.MILLISECOND);
    	c1.clear(Calendar.SECOND);
    	c1.clear(Calendar.MINUTE);
    	c1.set(Calendar.HOUR_OF_DAY,12);
    	c2.clear(Calendar.MILLISECOND);
    	c2.clear(Calendar.SECOND);
    	c2.clear(Calendar.MINUTE);
    	c2.set(Calendar.HOUR_OF_DAY,12);
    	System.out.println(dateEnd.getTime()/24/60/60000);
    	System.out.println(dateStart.getTime()/24/60/60000);
    	long n=(c2.getTime().getTime()/24/60/60000)-(c1.getTime().getTime()/24/60/60000);
		return (int) n;
    }
    /**
     * 
    * 开发人：张波
    * 开发日期：2007-4-1
    * 开发时间：16:20
    * 功能描述：返回指定日期后指定天数的日期，参数为负值时返回当前日期前的日期
    * 方法的参数和返回值：
     */ 
    public static Date addDay(Date date, int rd)
    {
        if(date == null)
            return null;
        try
        {
            Calendar calendar = Calendar.getInstance();
            calendar.setTime(date);
            calendar.add(Calendar.DAY_OF_MONTH, rd);
            date = calendar.getTime();
            return date;
        }
        catch(Exception exception)
        {
            return null;
        }
    }
    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午10:10:32
     * 功能描述：返回当前日期后指定天数的日期，参数为负值时返回当前日期前的日期
     * 方法的参数和返回值
     * @param rd
     * @return
     * Date 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static Date addDay(int rd){
        return addDay(new Date(),rd);
    }

    /**
     * 
    * 开发人：张波
    * 开发日期：2007-05-31
    * 开发时间：16:20
    * 功能描述：返回当前日期是周几，分别返回日,一,二,三,四,五,六
    * 方法的参数和返回值：
     */ 
	public static String getWeek() {
		return getWeek(new Date());
	}

	/**
	 * 开发人： 张波
	 * 开发时间： 2011-6-23 上午10:14:07
	 * 功能描述：返回指定日期是周几，分别返回日,一,二,三,四,五,六
	 * 方法的参数和返回值
	 * @param d
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String getWeek(Date d) {
		Calendar cal = Calendar.getInstance();
		cal.setTime(d);
		int posOfWeek = cal.get(Calendar.DAY_OF_WEEK);
		posOfWeek--;
		String[] ary = "日,一,二,三,四,五,六".split(",");
		return ary[posOfWeek];
	}
    
    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午10:15:04
	 * 功能描述：返回指定日期是周几，分别返回日,一,二,三,四,五,六
     * 方法的参数和返回值
     * @param strdate
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static  String getWeek(String  strdate){
    	Date d = null;
		try {
			d = parse(strdate);
		} catch (ParseException e) {
		}
    	if(d==null){
    		return null;
    	}
    	return getWeek(d);
    }
    
    public static String getYear(){
    	 Calendar ca = Calendar.getInstance();
         ca.setTime(new java.util.Date());
         String year = ""+ca.get(Calendar.YEAR);
         return year;
    }
    
    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午11:06:05
     * 功能描述：获取指定月份的天数
     * 方法的参数和返回值
     * @param year
     * @param mon
     * @return
     * int 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
	public static int getDays(int year, int mon) {
		Calendar ca = Calendar.getInstance();
		ca.set(Calendar.YEAR, year);
		ca.set(Calendar.MONTH, mon);
		return ca.getActualMaximum(Calendar.DAY_OF_MONTH);
	}
    
    /**
     * 
     * 开发人：张波
     * 开发日期：2007-6-13
     * 功能描述：返回当前月的第一天
     * @param
     * @return
     */
	public static String getMonthFirstDay() {
		return DateUtil.getYear() + "-" + DateUtil.getMonth() + "-01";
	}
    
	public static String getMonthLastDay() {
		return DateUtil.getYear() + "-" + DateUtil.getMonth() + "-" + String.valueOf(DateUtil.getDays(Integer.parseInt(DateUtil.getYear()), Integer.parseInt(DateUtil.getMonth())));
	}

    /**
     * 
     * 开发人：张波
     * 开发日期：2007-11-14
     * 功能描述：得到当前月
     * @param
     * @return
     */
	public static String getMonth() {
		return getMonth(null);
	}

    /**
     * 
     * 开发人：张波
     * 开发日期：2007-11-14
     * 功能描述：得到当前月
     * @param
     * @return
     */
	public static String getMonth(Date d) {
		Calendar tempDate = Calendar.getInstance();
		if(d!=null){
			tempDate.setTime(d);
		}
		String month = (tempDate.get(Calendar.MONTH) + 1) < 10 ? "0"
				+ (tempDate.get(Calendar.MONTH) + 1) : ""
				+ (tempDate.get(Calendar.MONTH) + 1);
		return month;
	}

	/**
	 * 得到格式化后的当月第一天，格式为yyyy-MM-dd，如2006-02-01
	 * @param currDate 要格式化的日期
	 * @see java.util.Calendar#getMinimum(int)
	 * 
	 * @return String 返回格式化后的当月第一天，格式为yyyy-MM-dd，如2006-02-01
	 */
	public static String getFirstDayOfMonth(Date currDate){
		Calendar cal = Calendar.getInstance();
		cal.setTime(currDate);
		int firstDay = cal.getMinimum(Calendar.DAY_OF_MONTH);
		cal.set(Calendar.DAY_OF_MONTH, firstDay);
		return format(cal.getTime(), "yyyy-MM-dd");
	}
	
	/**
	 * 得到格式化后的当月第一天，格式为yyyy-MM-dd，如2006-02-01
	 * @param currDate 要格式化的日期
	 * @see java.util.Calendar#getMinimum(int)
	 * 
	 * @return String 返回格式化后的当月第一天，格式为yyyy-MM-dd，如2006-02-01
	 */
	public static String getLastDayOfMonth(Date currDate){
		Calendar cal = Calendar.getInstance();
		cal.setTime(currDate);
		int lastDay = cal.getActualMaximum(Calendar.DAY_OF_MONTH);
		cal.set(Calendar.DAY_OF_MONTH, lastDay);
		return format(cal.getTime(), "yyyy-MM-dd");
	}
	
	/**
	 * 得到格式化后的中文日期，格式为****年**朋**日，如二〇一二年五月二日
	 * @param currDate 要格式化的日期，格式yyyy-MM-dd
	 * @see java.util.Calendar#getMinimum(int)
	 * 
	 * @return String 得到格式化后的中文日期，格式为****年**朋**日，如二〇一二年五月二日
	 */
	
	public static String getChinaDate(String strDate){
		String numberTochar[] = {"〇", "一", "二", "三", "四", "五", "六","七","八","九"};
		String numberTomonth[] = {"十","一", "二", "三", "四", "五", "六","七","八","九"};
		String tdate="";
		String[] dateArray = strDate.split("-");
		//年
		String nowy = dateArray[0];
		String nowy1=nowy.substring(0,1);
		String nowy2=nowy.substring(1,2);
		String nowy3=nowy.substring(2,3);
		String nowy4=nowy.substring(3,4);
		nowy = numberTochar[Integer.parseInt(nowy1)]+numberTochar[Integer.parseInt(nowy2)]+numberTochar[Integer.parseInt(nowy3)]+numberTochar[Integer.parseInt(nowy4)];
		
		//月
		String nowm = dateArray[1];
		String nowm1 = nowm.substring(0,1);
		String nowm2 = nowm.substring(1,2);
		if("0".equals(nowm1)){
			nowm = numberTomonth[Integer.parseInt(nowm2)];
		}else{
			if("0".equals(nowm2)){
				nowm = numberTomonth[0];
			}else{
				nowm = numberTomonth[0]+numberTomonth[Integer.parseInt(nowm2)];
			}
		}
		
		//日
		String nowd = dateArray[2];
		String nowd1 = nowd.substring(0,1);
		String nowd2 = nowd.substring(1,2);
		if("0".equals(nowd1)){
			nowd = numberTomonth[Integer.parseInt(nowd2)];//1-9号
		}else if("1".equals(nowd1)){
			if("0".equals(nowd2)){
				nowd = numberTomonth[0];//10号
			}else{
				nowd = numberTomonth[0]+numberTomonth[Integer.parseInt(nowd2)];//11-19号
			}
		}else{
			if("0".equals(nowd2)){
				nowd = numberTomonth[Integer.parseInt(nowd2)]+numberTomonth[0];//20、30号
			}else{
				nowd = numberTomonth[Integer.parseInt(nowd1)]+numberTomonth[0]+numberTomonth[Integer.parseInt(nowd2)];//21-29号及31号
			}
		}
		
		tdate=nowy+"年"+nowm+"月"+nowd+"日";
		return tdate;
	}
	
	
	public static String getUTCdate(String utctime)
	  {
	    if ((utctime == null) || (utctime.equals("")))
	      return "";

	    String rtnResult = "";
	    long itime = -5600643796434944000L;
	    try {
	      itime = Long.parseLong(utctime);
	    } catch (Exception localException1) {
	    }
	    try {
	      Date date = new Date();
	      date.setTime(itime);

	      SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd");
	      rtnResult = sdf1.format(date);
	    } catch (Exception e) {
	      e.printStackTrace();
	    }
	    return rtnResult;
	  }

	  public static String getUTCtime(String utctime) {
	    if ((utctime == null) || (utctime.equals("")))
	      return "";

	    String rtnResult = "";
	    long itime = -5600643796434944000L;
	    try {
	      itime = Long.parseLong(utctime);
	    } catch (Exception localException1) {
	    }
	    try {
	      Date date = new Date();
	      date.setTime(itime);
	      SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
	      rtnResult = sdf1.format(date);
	    } catch (Exception e) {
	      e.printStackTrace();
	    }
	    return rtnResult;
	  }
	  /**
	   * 
	   * @author ： 陈园
	   * @since： 2013-9-2 下午6:10:52
	   * 功能描述： 从本月向前推i个月  显示当时月的一号 如今天5月08日，订单创建日期起始时间为3月1日
	   * 方法的参数和返回值 为"yyyy-MM-dd"字符串
	   * @param date
	   * @param i
	   * @return
	   * Date 
	   * ==================================
	   * 修改历史
	   * 修改人        修改时间      修改原因及内容
	   *
	   * ==================================
	   */
	  public static String getPastNumMoth(Date date,int i){
			 SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd"); 
			 String time = sdf.format(date); 
			 String[] item = time.split("-"); 
			 int year = Integer.parseInt(item[0]); 
			 int month = Integer.parseInt(item[1]); 
			 if((month - i+1) <= 0){ 
			 month = month + 12 - i+1; 
			 year = year -1; 
			 }else { 
			 month = month - i+1; 
			 } 
			return year + "-" + month + "-" + 1;
			
	  }

	/**
	 * 导出数据时通过区域编码转换name
	 * 
	 * @param code
	 * @return
	 */
	public static String getAreaInfoByCodeToName(String code) {
		if (code == null || code.equals("")) {
			return "";
		}
		String rtnResult = "";
		try {
			rtnResult = SysGeneralCodeServiceImpl.sysAreaInfoExpMap.get(code);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return rtnResult;
	}
	  
}