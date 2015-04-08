package com.ctfo.statistics.alarm.common;

import java.io.File;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.Map;

import com.ctfo.statistics.alarm.model.OracleProperties;
import com.ctfo.statistics.alarm.model.RedisProperties;



public class Utils {
	public static final String[] EMPTY_STRING_ARRAY = new String[0];
	 /**
     * The System property key for the user directory.
     */
    private static final String USER_DIR_KEY = "user.dir";
	/**
	 * 获取用户当前目录
	 * @return
	 */
	public static File getUserDir() {
        return new File(System.getProperty(USER_DIR_KEY));
    }
    /**
     * <p>Checks if a CharSequence is not empty (""), not null and not whitespace only.</p>
     *
     * <pre>
     * StringUtils.isNotBlank(null)      = false
     * StringUtils.isNotBlank("")        = false
     * StringUtils.isNotBlank(" ")       = false
     * StringUtils.isNotBlank("bob")     = true
     * StringUtils.isNotBlank("  bob  ") = true
     * </pre>
     *
     * @param cs  the CharSequence to check, may be null
     * @return {@code true} if the CharSequence is
     *  not empty and not null and not whitespace
     * @since 2.0
     * @since 3.0 Changed signature from isNotBlank(String) to isNotBlank(CharSequence)
     */
    public static boolean isNotBlank(CharSequence cs) {
        return !Utils.isBlank(cs);
    }
    /**
     * <p>Checks if a CharSequence is whitespace, empty ("") or null.</p>
     *
     * <pre>
     * StringUtils.isBlank(null)      = true
     * StringUtils.isBlank("")        = true
     * StringUtils.isBlank(" ")       = true
     * StringUtils.isBlank("bob")     = false
     * StringUtils.isBlank("  bob  ") = false
     * </pre>
     *
     * @param cs  the CharSequence to check, may be null
     * @return {@code true} if the CharSequence is null, empty or whitespace
     * @since 2.0
     * @since 3.0 Changed signature from isBlank(String) to isBlank(CharSequence)
     */
    public static boolean isBlank(CharSequence cs) {
        int strLen;
        if (cs == null || (strLen = cs.length()) == 0) {
            return true;
        }
        for (int i = 0; i < strLen; i++) {
            if (Character.isWhitespace(cs.charAt(i)) == false) {
                return false;
            }
        }
        return true;
    }
	/**
	 * 判断是否包含数字
	 * @param cs
	 * @return
	 * TODO
	 */
	public static boolean hasAlarm(String cs) {
		if (cs == null || cs.length() == 0) {
            return false;
        }
        int sz = cs.length();
        for (int i = 0; i < sz; i++) {
            if (Character.isDigit(cs.charAt(i)) == true) {
                return true;
            }
        }
        return false;
	}
	public static String[] split(String str, String separatorChars) {
        return splitWorker(str, separatorChars, -1, false);
    }
	/**
	 * 字符分解器
	 * @param str
	 * @param separatorChars
	 * @param max
	 * @param preserveAllTokens
	 * @return
	 */
    private static String[] splitWorker(String str, String separatorChars, int max, boolean preserveAllTokens) {
        // Performance tuned for 2.0 (JDK1.4)
        // Direct code is quicker than StringTokenizer.
        // Also, StringTokenizer uses isSpace() not isWhitespace()

        if (str == null) {
            return null;
        }
        int len = str.length();
        if (len == 0) {
            return EMPTY_STRING_ARRAY;
        }
	    if(!hasNumber(str)){
	    	return null;   
	    }
        
        List<String> list = new ArrayList<String>();
        int sizePlus1 = 1;
        int i = 0, start = 0;
        boolean match = false;
        boolean lastMatch = false;
        if (separatorChars == null) {
            // Null separator means use whitespace
            while (i < len) {
                if (Character.isWhitespace(str.charAt(i))) {
                    if (match || preserveAllTokens) {
                        lastMatch = true;
                        if (sizePlus1++ == max) {
                            i = len;
                            lastMatch = false;
                        }
                        list.add(str.substring(start, i));
                        match = false;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
        } else if (separatorChars.length() == 1) {
            // Optimise 1 character case
            char sep = separatorChars.charAt(0);
            while (i < len) {
                if (str.charAt(i) == sep) {
                    if (match || preserveAllTokens) {
                        lastMatch = true;
                        if (sizePlus1++ == max) {
                            i = len;
                            lastMatch = false;
                        }
                        list.add(str.substring(start, i));
                        match = false;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
        } else {
            // standard case
            while (i < len) {
                if (separatorChars.indexOf(str.charAt(i)) >= 0) {
                    if (match || preserveAllTokens) {
                        lastMatch = true;
                        if (sizePlus1++ == max) {
                            i = len;
                            lastMatch = false;
                        }
                        list.add(str.substring(start, i));
                        match = false;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
        }
        if (match || preserveAllTokens && lastMatch) {
            list.add(str.substring(start, i));
        }
        return list.toArray(new String[list.size()]);
    }
    /**
     * <p>Splits the provided text into an array, separators specified,
     * preserving all tokens, including empty tokens created by adjacent
     * separators. This is an alternative to using StringTokenizer.</p>
     *
     * <p>The separator is not included in the returned String array.
     * Adjacent separators are treated as separators for empty tokens.
     * For more control over the split use the StrTokenizer class.</p>
     *
     * <p>A {@code null} input String returns {@code null}.
     * A {@code null} separatorChars splits on whitespace.</p>
     *
     * <pre>
     * StringUtils.splitPreserveAllTokens(null, *)           = null
     * StringUtils.splitPreserveAllTokens("", *)             = []
     * StringUtils.splitPreserveAllTokens("abc def", null)   = ["abc", "def"]
     * StringUtils.splitPreserveAllTokens("abc def", " ")    = ["abc", "def"]
     * StringUtils.splitPreserveAllTokens("abc  def", " ")   = ["abc", "", def"]
     * StringUtils.splitPreserveAllTokens("ab:cd:ef", ":")   = ["ab", "cd", "ef"]
     * StringUtils.splitPreserveAllTokens("ab:cd:ef:", ":")  = ["ab", "cd", "ef", ""]
     * StringUtils.splitPreserveAllTokens("ab:cd:ef::", ":") = ["ab", "cd", "ef", "", ""]
     * StringUtils.splitPreserveAllTokens("ab::cd:ef", ":")  = ["ab", "", cd", "ef"]
     * StringUtils.splitPreserveAllTokens(":cd:ef", ":")     = ["", cd", "ef"]
     * StringUtils.splitPreserveAllTokens("::cd:ef", ":")    = ["", "", cd", "ef"]
     * StringUtils.splitPreserveAllTokens(":cd:ef:", ":")    = ["", cd", "ef", ""]
     * </pre>
     *
     * @param str  the String to parse, may be {@code null}
     * @param separatorChars  the characters used as the delimiters,
     *  {@code null} splits on whitespace
     * @return an array of parsed Strings, {@code null} if null String input
     * @since 2.1
     */
    public static String[] splitPreserveAllTokens(String str, String separatorChars) {
        return splitWorker(str, separatorChars, -1, true);
    }
    /**
     * <p>Checks if the CharSequence contains only Unicode digits.
     * A decimal point is not a Unicode digit and returns false.</p>
     *
     * <p>{@code null} will return {@code false}.
     * An empty CharSequence (length()=0) will return {@code false}.</p>
     *
     * <pre>
     * StringUtils.isNumeric(null)   = false
     * StringUtils.isNumeric("")     = false
     * StringUtils.isNumeric("  ")   = false
     * StringUtils.isNumeric("123")  = true
     * StringUtils.isNumeric("12 3") = false
     * StringUtils.isNumeric("ab2c") = false
     * StringUtils.isNumeric("12-3") = false
     * StringUtils.isNumeric("12.3") = false
     * </pre>
     *
     * @param cs  the CharSequence to check, may be null
     * @return {@code true} if only contains digits, and is non-null
     * @since 3.0 Changed signature from isNumeric(String) to isNumeric(CharSequence)
     * @since 3.0 Changed "" to return false and not true
     */
    public static boolean isNumeric(CharSequence cs) {
        if (cs == null || cs.length() == 0) {
            return false;
        }
        int sz = cs.length();
        for (int i = 0; i < sz; i++) {
            if (Character.isDigit(cs.charAt(i)) == false) {
                return false;
            }
        }
        return true;
    }
    /**
     * 包含数字
     * @param cs
     * @return
     */
    private static boolean hasNumber(String cs) {
		if (cs == null || cs.length() == 0) {
            return false;
        }
        int sz = cs.length();
        for (int i = 0; i < sz; i++) {
            if (Character.isDigit(cs.charAt(i)) == true) {
                return true;
            }
        }
        return false;
	}
    /**
     * 获取Oracle连接参数
     * @param config
     * @return
     */
	public static OracleProperties getOracleProperties(Map<String, String> config) {
		OracleProperties oracleProperties = new OracleProperties();
		oracleProperties.setUrl(config.get("oracleUrl"));
		oracleProperties.setUsername(config.get("oracleUser"));
		oracleProperties.setPassword(config.get("oraclePass"));
		oracleProperties.setInitialSize(Integer.parseInt(config.get("oracleInitialSize")));
		oracleProperties.setMaxActive(Integer.parseInt(config.get("oracleMaxActive")));
		oracleProperties.setMinIdle(Integer.parseInt(config.get("oracleMinIdle")));
		oracleProperties.setMaxWait(Long.parseLong(config.get("oracleMaxWait")));
		oracleProperties.setTimeBetweenEvictionRunsMillis(Long.parseLong(config.get("oracleTimeBetweenEvictionRunsMillis")));
		oracleProperties.setMinEvictableIdleTimeMillis(Long.parseLong(config.get("oracleMinEvictableIdleTimeMillis")));
		oracleProperties.setTestWhileIdle(Boolean.parseBoolean(config.get("oracleTestWhileIdle")));
		oracleProperties.setTestOnBorrow(Boolean.parseBoolean(config.get("oracleTestOnBorrow")));
		oracleProperties.setTestOnReturn(Boolean.parseBoolean(config.get("oracleTestOnReturn")));
		oracleProperties.setMaxPoolPreparedStatementPerConnectionSize(Integer.parseInt(config.get("oracleMaxPoolPreparedStatementPerConnectionSize")));
		return oracleProperties;
	}
	/**
	 * 获取Redis缓存参数
	 * @param config
	 * @return
	 */
	public static RedisProperties getRedisProperties(Map<String, String> config) {
		RedisProperties redisProperties = new RedisProperties();
		redisProperties.setHost(config.get("redisHost"));
		redisProperties.setPort(Integer.parseInt(config.get("redisPort")));
		redisProperties.setPwd(config.get("redisPass"));
		redisProperties.setMaxActive(Integer.parseInt(config.get("redisMaxActive"))); 
		redisProperties.setMaxIdle(Integer.parseInt(config.get("redisMaxIdle")));
		redisProperties.setMaxWait(Long.parseLong(config.get("redisMaxWait")));
		redisProperties.setRedisTimeout(Integer.parseInt(config.get("redisTimeOut"))); 
		return redisProperties;
	}
	/**
	 * 获取时间描述
	 * @param time
	 * @return
	 */
	public static String getTimeDesc(long time) {
		StringBuffer sb = new StringBuffer();
		if(time >= 3600000){
			sb.append(time /3600000).append("小时");
			long mod = time % 3600000;
			if(mod != 0){
				if(mod >= 60000){
					sb.append(mod /60000).append("分钟");
					long sec = mod % 60000;
					if(sec >= 1000){
						sb.append(sec /1000).append("秒");
					}
				} else {
					if(mod >= 1000){
						sb.append(mod /1000).append("秒");
					}
				}
			}
		} else if(time >= 60000){
			sb.append(time /60000).append("分钟");
			long sec = time % 60000;
			if(sec >= 1000){
				sb.append(sec /1000).append("秒");
			}
		} else if(time >= 1000){
			sb.append(time /1000).append("秒");
		} else {
			sb.append(time).append("毫秒");
		}
		return sb.toString();
	}
	/**
	 * 获取昨天日期目录
	 * @return 
	 */
	public static String getYesterdayDir() {
		SimpleDateFormat sdf = new SimpleDateFormat("/yyyy/MM/dd/");
		Calendar cal = Calendar.getInstance();
		cal.add(Calendar.DAY_OF_MONTH, -1); 
		return sdf.format(cal.getTime());
	}
	/**
	 * 获取昨天日期目录时间-格式:yyyyMMdd
	 * @return 
	 */
	public static String getYesterdayStr() {
		SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd");
		Calendar cal = Calendar.getInstance();
		cal.add(Calendar.DAY_OF_MONTH, -1); 
		return sdf.format(cal.getTime());
	}
	/**
	 * 获取当月15号正午12点整毫秒数
	 * @param dateStr
	 * @return
	 */
	public static long getLastMonthUtc(String dateStr) {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.YEAR, Integer.parseInt(dateStr.substring(0, 4)));
		cal.set(Calendar.MONTH, Integer.parseInt(dateStr.substring(4, 6)) - 1); // Calendar月份从0开始
		cal.set(Calendar.DAY_OF_MONTH, 15); // 当前月15号
		cal.set(Calendar.HOUR_OF_DAY, 12);  // 正午12点
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	/**
	 * 获取当月最后一天23:59:59的毫秒数
	 * @param dateStr
	 * @return
	 */
	public static long getLastMonthEndUtc(String dateStr) {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.YEAR, Integer.parseInt(dateStr.substring(0, 4)));
		cal.set(Calendar.MONTH, Integer.parseInt(dateStr.substring(4, 6)) - 1); // Calendar月份从0开始
		cal.set(Calendar.HOUR_OF_DAY, 23);
		cal.set(Calendar.MINUTE, 59);
		cal.set(Calendar.SECOND, 59);
		cal.set(Calendar.MILLISECOND, 0);
		cal.set(Calendar.DAY_OF_MONTH, 1);
        cal.roll(Calendar.DAY_OF_MONTH, -1);
		return cal.getTimeInMillis();
	}
	/**
	 * 获取当月第一天00:00:00的毫秒数
	 * @param dateStr
	 * @return
	 */
	public static long getLastMonthStartUtc(String dateStr) {
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.YEAR, Integer.parseInt(dateStr.substring(0, 4)));
		cal.set(Calendar.MONTH, Integer.parseInt(dateStr.substring(4, 6)) - 1); // Calendar月份从0开始
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		cal.set(Calendar.DAY_OF_MONTH, 1);
		return cal.getTimeInMillis();
	}
	/**
	 * 获取上周统计UTC时间（上周三正午12点整）
	 * @param yesterdayUtc 周一正午12点UTC
	 * @return
	 */
	public static long getLastWeekUtc(long yesterdayUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(yesterdayUtc); // 时间设置为昨天
		cal.add(Calendar.DAY_OF_MONTH, - 4); // 每周一统计时回退到上周三
		return cal.getTimeInMillis();
	}
	/**
	 * 获取上周统计结束时间（周日晚上23:59:59）
	 * @param yesterdayUtc
	 * @return 
	 */
	public static long getLastWeekEndUtc(long yesterdayUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(yesterdayUtc); // 时间设置为昨天
		cal.set(Calendar.HOUR_OF_DAY, 23);
		cal.set(Calendar.MINUTE, 59);
		cal.set(Calendar.SECOND, 59);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	/**
	 * 获取上周统计开始时间（周一凌晨00:00:00）
	 * @param yesterdayUtc
	 * @return
	 */
	public static long getLastWeekStartUtc(long yesterdayUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(yesterdayUtc); // 时间设置为昨天
		cal.add(Calendar.DAY_OF_MONTH, -6); // 周一凌晨00:00:00
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	/**
	 * 获取周字符串
	 * @param lastWeekUtc
	 * @return
	 */
	public static int getWeek(long lastWeekUtc) {
		Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(lastWeekUtc); // 设置时间 
		return cal.get(Calendar.WEEK_OF_YEAR);
	}
	/**
	 * 获取运行当天时间字符串
	 * @param startDateStr
	 * @param day
	 * @return
	 */
	public static String getRunningDay(String startDateStr, int day) {
		SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd");
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.YEAR, Integer.parseInt(startDateStr.substring(0, 4)));
		cal.set(Calendar.MONTH, Integer.parseInt(startDateStr.substring(4, 6)) - 1); // Calendar月份从0开始
		cal.set(Calendar.DAY_OF_MONTH, Integer.parseInt(startDateStr.substring(6, 8)));
		cal.add(Calendar.DAY_OF_MONTH, +day); 
		return sdf.format(cal.getTime());
	}
	public static void main(String[] args) throws ParseException {
//		Calendar cal = Calendar.getInstance();
//		cal.set(Calendar.MONTH, 10);
//		cal.set(Calendar.DAY_OF_MONTH, 30);
////		cal.add(Calendar.DAY_OF_MONTH, -1);
//		cal.set(Calendar.HOUR_OF_DAY, 12);
//		cal.set(Calendar.MINUTE, 0);
//		cal.set(Calendar.SECOND, 0);
//		cal.set(Calendar.MILLISECOND, 0);
//		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS").format(new Date(getWeek(cal.getTimeInMillis()))));
		String startDate = "20141201";
		int runningDays = 35;
		for(int i =0 ; i < runningDays; i++){
			System.out.println(getRunningDay(startDate, i));
		}
	}


}
