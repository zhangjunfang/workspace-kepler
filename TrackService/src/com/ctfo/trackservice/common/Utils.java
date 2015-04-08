package com.ctfo.trackservice.common;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang3.ArrayUtils;

public class Utils {
	/**
	 * 判断是否包含数字
	 * 
	 * @param cs
	 * @return TODO
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
	 * 
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
			return ArrayUtils.EMPTY_STRING_ARRAY;
		}
		if (!hasNumber(str)) {
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
	 * 包含数字
	 * 
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
	 * 
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
	 * 
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
	 * 
	 * @param time
	 * @return
	 */
	public static String getTimeDesc(long time) {
		StringBuffer sb = new StringBuffer();
		if (time >= 3600000) {
			sb.append(time / 3600000).append("小时");
			long mod = time % 3600000;
			if (mod != 0) {
				if (mod >= 60000) {
					sb.append(mod / 60000).append("分钟");
					long sec = mod % 60000;
					if (sec >= 1000) {
						sb.append(sec / 1000).append("秒");
					}
				} else {
					if (mod >= 1000) {
						sb.append(mod / 1000).append("秒");
					}
				}
			}
		} else if (time >= 60000) {
			sb.append(time / 60000).append("分钟");
			long sec = time % 60000;
			if (sec >= 1000) {
				sb.append(sec / 1000).append("秒");
			}
		} else if (time >= 1000) {
			sb.append(time / 1000).append("秒");
		} else {
			sb.append(time).append("毫秒");
		}
		return sb.toString();
	}

	/**
	 * 获取昨天日期目录
	 * 
	 * @return
	 */
	public static String getYesterdayDir() {
		SimpleDateFormat sdf = new SimpleDateFormat("/yyyy/MM/dd/");
		Calendar cal = Calendar.getInstance();
		cal.add(Calendar.DAY_OF_MONTH, -1);
		return sdf.format(cal.getTime());
	}

	/**
	 * 获取秒数后的执行时间字符串
	 * 
	 * @param seconds
	 * @return
	 */
	public static String getAfterSecondsExecution(int seconds) {
		Calendar cl = Calendar.getInstance();
		cl.add(Calendar.SECOND, seconds);
		StringBuffer sb = new StringBuffer();
		sb.append(cl.get(Calendar.SECOND)).append(" ");
		sb.append(cl.get(Calendar.MINUTE)).append(" ");
		sb.append(cl.get(Calendar.HOUR_OF_DAY));
		sb.append(" * * ?");
		return sb.toString();
	}

	/**
	 * 获取昨天日期目录时间-格式:yyyyMMdd
	 * 
	 * @return
	 */
	public static String getYesterdayStr() {
		SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd");
		Calendar cal = Calendar.getInstance();
		cal.add(Calendar.DAY_OF_MONTH, -1);
		return sdf.format(cal.getTime());
	}
	/**
	 * 获取通讯服务器配置信息
	 * @param config
	 * @return
	 */
	public static MsgProperties getMsgProperties(Map<String, String> config) {
		MsgProperties msgProperties = new MsgProperties();
		msgProperties.setMsgEncoding(config.get("msgEncoding"));
		msgProperties.setLoginType(config.get("msgLoginType"));
		msgProperties.setMsgGroup(config.get("msgGroup"));
		msgProperties.setMsgGroupId(config.get("msgGroupId"));
		msgProperties.setMsgHost(config.get("msgHost"));
		msgProperties.setMsgPassword(config.get("msgPassword"));
		msgProperties.setMsgUserName(config.get("msgUserName"));
		msgProperties.setMsgPort(Integer.parseInt(config.get("msgPort")));
		msgProperties.setReConnectTime(Long.parseLong(config.get("msgReConnectTime")));
		msgProperties.setReaderIdle(Integer.parseInt(config.get("msgReaderIdle")));
		msgProperties.setWriterIdle(Integer.parseInt(config.get("msgWriterIdle")));
		msgProperties.setReceiveBufferSize(Integer.parseInt(config.get("msgReceiveBufferSize")));
		msgProperties.setSendBufferSize(Integer.parseInt(config.get("msgSendBufferSize")));
		
		return msgProperties;
	}
	
	/**
	 * 获取当天的年月日毫秒值(long类型)
	 * @return
	 */
	public static long getCurrentDayYearMonthDayMillis(){
		Calendar cal = Calendar.getInstance();
		cal.set(Calendar.HOUR_OF_DAY , 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	
	/**
	 * 计算两个经纬度之间距离
	 * @param fromx 起点经度
	 * @param tox	终点经度
	 * @param fromy 起点纬度
	 * @param toy	终点纬度
	 * @return
	 */
	public static Double getLength(double fromx, double tox, double fromy, double toy) {
		double b = (tox - fromx) * Math.PI / 180;
		double c = Math.PI / 2 - toy * Math.PI / 180;
		double a = Math.PI / 2 - fromy * Math.PI / 180;
		double s = Math.cos(a) * Math.cos(c) + Math.sin(a) * Math.sin(c) * Math.cos(b);
		return 6378137 * Math.acos(s);
	}
	
	
	public static void main(String[] args) {
		System.out.println(getYesterdayStr());
	}

	/***
	 * 将HH:MM:SS 转成秒格式
	 * 
	 * @param time
	 * @return
	 */
	public static long timeConvertSec(String time) {
		if (time == null || time.length() == 0) {
			return 0;
		}
		String[] arrays = time.split(":");
		if (arrays.length == 3) {
			return Integer.parseInt(arrays[0]) * 60 * 60 + Integer.parseInt(arrays[1]) * 60 + Integer.parseInt(arrays[2]);
		}
		return 0;
	}
	

	
}
