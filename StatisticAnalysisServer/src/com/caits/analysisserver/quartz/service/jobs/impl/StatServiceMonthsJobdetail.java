package com.caits.analysisserver.quartz.service.jobs.impl;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.statisticanalysis.AnalysisStatisticCrossDaysThread;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.utils.CDate;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
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
 * <td>2013-01-16</td>
 * <td>yujch</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author yujch
 * @since JDK1.6
 */
public class StatServiceMonthsJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(StatServiceMonthsJobdetail.class);

	// ------获得xml拼接的Sql语句

//	private int count = 0;// 计数器

//	private long statDate;
	private long beginTime;
	private long endTime;
	
	private int year;
	private int month;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public StatServiceMonthsJobdetail(int year,int month) {
		this.year = year;
		this.month = month;
		this.beginTime = CDate.getFirstDayOfMonth(year,month).getTime();//当月第一天零点
		this.endTime = CDate.getDateFromParam(CDate.getFirstDayOfMonth(year,month),0,1,1,0,0,0,0).getTime();//下月第一天零点

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			AnalysisDBAdapter dba = new AnalysisDBAdapter();
			dba.initDBAdapter();
			//跨多天统计
			AnalysisStatisticCrossDaysThread croDaysThread = new AnalysisStatisticCrossDaysThread();
			croDaysThread.initAnalyser();
			flag = croDaysThread.statistic(beginTime,endTime,""+(month+1),""+year);
		} catch (Exception e) {
			logger.error("跨天统计信息出错：", e);
			AnalysisDBAdapter.clearCollections();
			flag = 0;
		}
		AnalysisDBAdapter.clearCollections();
		return flag;
	}

	/**
	 * 将空值转换为空字符串
	 * 
	 * @param str
	 *            字符串
	 * @return String 返回处理后的字符串
	 */
	public static String nullToStr(String str) {
		return str == null || str.equals("null") ? "" : str.trim();
	}

}
