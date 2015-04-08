package com.ctfo.informationser.portalmng.daoImpl;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.portalmng.dao.RoadConditionURL;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.portalmng.beans.RoadCondition;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-11-8</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@AnnotationName(name = "路况管理")
public class RoadConditionURLImpl implements RoadConditionURL {

	private final static String CODE = "GBK";

	private String url;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.portalmng.dao.RoadConditionURL#findRoadCondition(java.lang.Long[])
	 */
	@Override
	public Map<Long, List<RoadCondition>> findRoadCondition(Long[] provinceCodes) throws CtfoAppException {
		Map<Long, List<RoadCondition>> map = null;
		if (null == provinceCodes || 0 >= provinceCodes.length) {
			return map;
		}
		InputStream is = null;
		BufferedReader br = null;
		map = new HashMap<Long, List<RoadCondition>>();
		try {
			synchronized (this) {
				for (Long provinceCode : provinceCodes) {
					boolean b = true;
					// 发送
					URL url = new URL(getUrl().replace("{0}", String.valueOf(provinceCode)));
					URLConnection connection = url.openConnection();
					connection.setDoOutput(true);
					// 接收
					is = connection.getInputStream();
					br = new BufferedReader(new InputStreamReader(is, CODE));
					StringBuffer sb = new StringBuffer();
					while (b) {
						if (br.ready()) {
							sb.append(br.readLine()).append("\r\n");
						} else {
							b = false;
						}
					}
					List<RoadCondition> roadConditionsList = new ArrayList<RoadCondition>();
					String[] strs = sb.toString().split("\r\n");
					if (null != strs && 0 < strs.length) {
						String roadConditionTime = getTime(strs[0]);
						if (null != roadConditionTime) {
							for (int i = 0; i < strs.length; i++) {
								RoadCondition roadCondition = new RoadCondition();
								if (i == 0) {
									continue;
								}
								roadCondition.setProvinceCode(provinceCode);
								roadCondition.setRoadConditionTime(roadConditionTime);
								roadCondition.setDescriptions(strs[i]);
								roadConditionsList.add(roadCondition);
							}
						}
					}
					map.put(provinceCode, roadConditionsList);
				}
			}
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		} finally {
			try {
				if (null != is) {
					is.close();
				}
				if (null != br) {
					br.close();
				}
			} catch (IOException e) {
				throw new CtfoAppException(e.fillInStackTrace());
			}
		}
		return map;
	}

	/**
	 * 破解时间 1~288，五分钟
	 * 
	 * @param time
	 * @return
	 */
	private static String getTime(String time) {
		String timeStr = null;
		if (null != time) {
			Long ltime = new Long(0);
			Long utc = getZeroUtc(0);
			Long l = Long.parseLong(time);
			ltime = l * 5 * 60 * 1000 + utc;

			timeStr = dateFormat(new Date(ltime));
		}
		return timeStr;
	}

	/**
	 * 获取UTC时间
	 * 
	 * @param dayNum
	 *            天数,以当天零点为基数，指定天数UTC时间(如传递-1获取前一天零点UTC时间)
	 * @return
	 */
	private static long getZeroUtc(int dayNum) {
		Calendar calendar = Calendar.getInstance();
		calendar.add(Calendar.DAY_OF_MONTH, dayNum);
		calendar.set(Calendar.HOUR_OF_DAY, 0);
		calendar.set(Calendar.MINUTE, 0);
		calendar.set(Calendar.SECOND, 0);
		calendar.set(Calendar.MILLISECOND, 0);
		return calendar.getTimeInMillis();
	}

	/**
	 * 时间格式化 yyyy-MM-dd HH:mm:ss
	 * 
	 * @param date
	 *            java.util.Date
	 * @return
	 */
	private static String dateFormat(Date date) {
		String str = "";
		if (null != date) {
			str = new SimpleDateFormat("yyyy-MM-dd HH:mm").format(date);
		}
		return str;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}
}
