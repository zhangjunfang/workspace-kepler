package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.ctfo.generator.pk.GeneratorPK;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AnalysisStatisticCrossDaysThread <br>
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
 * <td>2011-10-18</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6 @ Description: 用于统计车辆相关信息周统计月统计年统计
 */
public class AnalysisStatisticCrossDaysThread {

	private static final Logger logger = LoggerFactory.getLogger(AnalysisStatisticCrossDaysThread.class);

	// 是否统计上一个月车辆信息
	private boolean isMonthFlag = false;

	// 是否统计上一年车辆信息
	// private boolean isYearFlag = false;

	private Map<String, Long> lastMonthOilMap = new HashMap<String, Long>();

	private String saveStatMonthInfo = null;

	private String queryOilMonthNum = null;

	private String queryStatDayInfo = null;

	private String saveVehicleAlarmMonthInfo = null;

	private String queryVehicleAlarmDayInfo = null;

	private String sql_queryVehicleSta = null;

	private String sql_queryAsseessoil = null;

	private String sql_saveOilWear = null;

	private String sql_queryVehicleOutLineDayInfo = null;

	private String sql_saveOutLineMonthInfo = null;

	private Connection dbCon = null;

	private String date = null;

	public void initAnalyser() throws Exception {
		// 存储车辆月统计信息
		saveStatMonthInfo = SQLPool.getinstance().getSql("sql_saveStatMonthInfo");

		// 根据车辆ID，加油时间统计上一月累计加油量
		queryOilMonthNum = SQLPool.getinstance().getSql("sql_queryOilMonthNum");

		// 查询车辆日统计信息
		queryStatDayInfo = SQLPool.getinstance().getSql("sql_queryStatDayInfo");

		// 存储车辆报警月统计信息
		saveVehicleAlarmMonthInfo = SQLPool.getinstance().getSql(
				"sql_saveVehicleAlarmMonthInfo");

		// 查询车辆报警日统计信息
		queryVehicleAlarmDayInfo = SQLPool.getinstance().getSql(
				"sql_queryVehicleAlarmDayInfo");

		// 根据车辆ID查询车辆总累计油耗值和里程
		sql_queryVehicleSta = SQLPool.getinstance().getSql(
				"sql_queryVehicleSta");

		// 根据车辆ID查询车辆考核油耗值
		sql_queryAsseessoil = SQLPool.getinstance().getSql(
				"sql_queryAsseessoil");

		// 统计车辆燃油分析月报
		sql_saveOilWear = SQLPool.getinstance().getSql("sql_saveOilWear");

		// 查询非法营运日表
		sql_queryVehicleOutLineDayInfo = SQLPool.getinstance().getSql(
				"sql_queryVehicleOutLineDayInfo");

		// 存储非法营运月表
		sql_saveOutLineMonthInfo = SQLPool.getinstance().getSql(
				"sql_saveOutLineMonthInfo");

	}

	public void setMonthFlag(boolean isMonthFlag) {
		this.isMonthFlag = isMonthFlag;
	}

	public String getDate() {
		return date;
	}

	public void setDate(String date) {
		this.date = date;
	}

	public void statistic() {

		while (true) {
			try {
				// 从连接池获得连接
				dbCon = OracleConnectionPool.getConnection();

				try {
					if (isMonthFlag) {
						String month = CDate.getPreviousMonth() + "";
						String year = CDate.getYear(CDate.getYearMonthUtc(-1))
								+ "";
						if (date != null) {
							month = date.split("/")[1];
							year = date.split("/")[0];
						}

						long startMonthUtc = CDate.getYearMonthUtc(-1);
						long endMonthUtc = CDate.getYearMonthUtc(0);
						logger.debug("开始统计月:" + month);
						// 汇总车辆月统计信息
						saveStaMonthInfo(startMonthUtc, endMonthUtc, month, year); 
						saveVehicleAlarmMonthDayInfo(startMonthUtc,
								endMonthUtc, month, year); // 统计车辆月报警
						stSaveOilWear(startMonthUtc, endMonthUtc, month, year); // 汇总车辆燃油分析月报
						saveVehicleOutLineMonthDayInfo(startMonthUtc,
								endMonthUtc, month, year); // 汇总非法营运月统计
					}
				} catch (Exception e) {
					logger.error("汇总月表出错.", e);
				} finally {
					isMonthFlag = false;
				}
				break;
			} catch (Exception e) {
				logger.error("车辆汇总月表出错.", e);
			} finally {
				if (dbCon != null) {
					try {
						dbCon.close();
					} catch (SQLException e) {
						logger.error("连接放回连接池出错。", e);
					}
				}
			}
		}// End while
	}

	public int statistic(long startMonthUtc, long endMonthUtc, String month,
			String year) {
		int flag = 0;
		try {
			// 从连接池获得连接
			dbCon = OracleConnectionPool.getConnection();
			try {
				logger.debug("开始统计车辆月数据:" + month);
				// 汇总车辆月统计信息
				saveStaMonthInfo(startMonthUtc, endMonthUtc, month, year); 
				// 统计车辆月报警
				saveVehicleAlarmMonthDayInfo(startMonthUtc, endMonthUtc, month, year); 
				// 汇总车辆燃油分析月报
				stSaveOilWear(startMonthUtc, endMonthUtc, month, year); 
				// 汇总非法营运月统计
				saveVehicleOutLineMonthDayInfo(startMonthUtc, endMonthUtc, month, year); 
				flag = 1;
			} catch (Exception e) {
				logger.error("汇总月表出错.", e);
				flag = 0;
			}
		} catch (Exception e) {
			logger.error("车辆汇总月表出错.", e);
			flag = 0;
		} finally {
			if (dbCon != null) {
				try {
					dbCon.close();
				} catch (SQLException e) {
					logger.error("连接放回连接池出错。", e);
				}
			}
		}
		return flag;
	}

	/**
	 * 存储月统计信息
	 * 
	 * @throws SQLException
	 * 
	 */
	private void saveStaMonthInfo(long startMonthUtc, long endMonthUc,
			String month, String year) throws SQLException {
		PreparedStatement stSaveStatMonthInfo = null;
		PreparedStatement stQueryStatDayInfo = null;
		ResultSet rs = null;
		String vid = "";
		try {
			stSaveStatMonthInfo = dbCon.prepareStatement(saveStatMonthInfo);
			stQueryStatDayInfo = dbCon.prepareStatement(queryStatDayInfo);
			stQueryStatDayInfo.setLong(1, startMonthUtc);// 获取上一个月1号凌晨时间
			stQueryStatDayInfo.setLong(2, endMonthUc);// 获取当前月1号凌晨时间
			rs = stQueryStatDayInfo.executeQuery();
			int count = 0;
			while (rs.next()) {
				try {
					stSaveStatMonthInfo.setLong(1, Integer.parseInt(month)); // 统计月
					vid = rs.getString(1);
					stSaveStatMonthInfo.setString(2, vid); // 车辆编号
					stSaveStatMonthInfo.setString(3, rs.getString(2)); // 车牌号码
					stSaveStatMonthInfo.setString(4, rs.getString(3)); // 车架号(vin)
					stSaveStatMonthInfo.setString(5, rs.getString(4)); // 企业ID
					stSaveStatMonthInfo.setString(6, rs.getString(5)); // 企业名称
					stSaveStatMonthInfo.setString(7, rs.getString(6)); // 车队ID
					stSaveStatMonthInfo.setString(8, rs.getString(7)); // 车队名称
					stSaveStatMonthInfo.setLong(9, rs.getLong(8)); // 车辆上线次数（终端成功鉴权次数）
					stSaveStatMonthInfo.setLong(10, rs.getLong(9)); // 车辆在线时长
					stSaveStatMonthInfo.setLong(11, rs.getLong(10)); // 上一月发动机运行时长
					stSaveStatMonthInfo.setLong(12, rs.getLong(11)); // 上一月行驶里程
					long lastMonthAssessOil = rs.getLong(12);
					lastMonthOilMap.put(vid, lastMonthAssessOil);
					stSaveStatMonthInfo.setLong(13, lastMonthAssessOil); // 上一月油耗
					stSaveStatMonthInfo.setLong(14, rs.getLong(13)); // 上一月超速下油耗
					stSaveStatMonthInfo.setLong(15, rs.getLong(14)); // 上一月超速下行驶里程

					stSaveStatMonthInfo.setLong(16, rs.getLong(15)); // 上一月最高车速
					stSaveStatMonthInfo.setLong(17, rs.getLong(16)); // 上一月最高发动机转速
					stSaveStatMonthInfo.setLong(18, rs.getLong(17)); // 定位量
																		// 定位有效数量
					stSaveStatMonthInfo.setLong(19, rs.getLong(18)); // 定位无效数量
					stSaveStatMonthInfo.setLong(20, rs.getLong(19)); // GPS时间无效数量
					stSaveStatMonthInfo.setLong(21, rs.getLong(20)); // 经纬度无效数量
					stSaveStatMonthInfo.setLong(22, rs.getLong(21)); // 报警总数
					stSaveStatMonthInfo.setLong(23, rs.getLong(22)); // 报警总处理数
					stSaveStatMonthInfo.setLong(24, rs.getLong(23)); // 紧急报警总数

					stSaveStatMonthInfo.setLong(25, rs.getLong(24)); // ACC开次数
					stSaveStatMonthInfo.setLong(26, rs.getLong(25)); // ACC开时长
					stSaveStatMonthInfo.setLong(27, rs.getLong(26)); // 超速报警总数
					stSaveStatMonthInfo.setLong(28, rs.getLong(27)); // 超速持续时间

					stSaveStatMonthInfo.setLong(29, rs.getLong(28)); // 疲劳驾驶次数
					stSaveStatMonthInfo.setLong(30, rs.getLong(29)); // 疲劳驾驶时间

					stSaveStatMonthInfo.setLong(31, rs.getLong(30)); // GNSS模块故障次数
					stSaveStatMonthInfo.setLong(32, rs.getLong(31)); // GNSS模块故障时长

					stSaveStatMonthInfo.setLong(33, rs.getLong(32)); // GNSS模块天线未接或被剪断次数
					stSaveStatMonthInfo.setLong(34, rs.getLong(33)); // GNSS模块天线未接或被剪断时长

					stSaveStatMonthInfo.setLong(35, rs.getLong(34)); // GNSS模块天线短路次数
					stSaveStatMonthInfo.setLong(36, rs.getLong(35)); // GNSS模块天线短路时长

					stSaveStatMonthInfo.setLong(37, rs.getLong(36)); // 终端主电源欠压次数
					stSaveStatMonthInfo.setLong(38, rs.getLong(37)); // 终端主电源欠压时长

					stSaveStatMonthInfo.setLong(39, rs.getLong(38)); // 终端主电源掉电次数
					stSaveStatMonthInfo.setLong(40, rs.getLong(39)); // 终端主电源掉电时长

					stSaveStatMonthInfo.setLong(41, rs.getLong(40)); // 终端LCD或显示器故障次数
					stSaveStatMonthInfo.setLong(42, rs.getLong(41)); // 终端LCD或显示器故障时长

					stSaveStatMonthInfo.setLong(43, rs.getLong(42)); // TIS模块故障次数
					stSaveStatMonthInfo.setLong(44, rs.getLong(43)); // TIS模块故障时长

					stSaveStatMonthInfo.setLong(45, rs.getLong(44)); // 摄像头故障次数
					stSaveStatMonthInfo.setLong(46, rs.getLong(45)); // 摄像头故障时长

					stSaveStatMonthInfo.setLong(47, rs.getLong(46)); // 当天累计驾驶超时时长

					stSaveStatMonthInfo.setLong(48, rs.getLong(47)); // 超时停车次数
					stSaveStatMonthInfo.setLong(49, rs.getLong(48)); // 超时停车时长

					stSaveStatMonthInfo.setLong(50, rs.getLong(49)); // 进区告警次数

					stSaveStatMonthInfo.setLong(51, rs.getLong(50)); // 出区告警次数

					stSaveStatMonthInfo.setLong(52, rs.getLong(51)); // 进线路次数

					stSaveStatMonthInfo.setLong(53, rs.getLong(52)); // 出线路次数

					stSaveStatMonthInfo.setLong(54, rs.getLong(53)); // 路段行驶时间不足次数

					stSaveStatMonthInfo.setLong(55, rs.getLong(54)); // 路段行驶时间过长次数

					stSaveStatMonthInfo.setLong(56, rs.getLong(55)); // 偏航告警次数
					stSaveStatMonthInfo.setLong(57, rs.getLong(56)); // 偏航告警时长

					stSaveStatMonthInfo.setLong(58, rs.getLong(57)); // 车辆VSS故障告警次数
					stSaveStatMonthInfo.setLong(59, rs.getLong(58)); // 车辆VSS故障告警时长

					stSaveStatMonthInfo.setLong(60, rs.getLong(59)); // 车辆油量异常告警次数
					stSaveStatMonthInfo.setLong(61, rs.getLong(60)); // 车辆油量异常告警时长

					stSaveStatMonthInfo.setLong(62, rs.getLong(61)); // 车辆被盗时长

					stSaveStatMonthInfo.setLong(63, rs.getLong(62)); // 车辆非法点火次数

					stSaveStatMonthInfo.setLong(64, rs.getLong(63)); // 车辆非法移位次数

					stSaveStatMonthInfo.setLong(65, rs.getLong(64)); // 冷却液温度告警次数
					stSaveStatMonthInfo.setLong(66, rs.getLong(65)); // 冷却液温度告警时长

					stSaveStatMonthInfo.setLong(67, rs.getLong(66)); // 机油压力告警次数
					stSaveStatMonthInfo.setLong(68, rs.getLong(67)); // 机油压力告警时长

					stSaveStatMonthInfo.setLong(69, rs.getLong(68)); // 蓄电池电压告警次数
					stSaveStatMonthInfo.setLong(70, rs.getLong(69)); // 蓄电池电压告警时长

					stSaveStatMonthInfo.setLong(71, rs.getLong(70)); // 制动气压告警次数
					stSaveStatMonthInfo.setLong(72, rs.getLong(71)); // 制动气压告警时长

					stSaveStatMonthInfo.setLong(73, rs.getLong(72)); // 燃油告警次数
					stSaveStatMonthInfo.setLong(74, rs.getLong(73)); // 燃油告警时长

					stSaveStatMonthInfo.setLong(75, rs.getLong(74)); // 水位低告警次数
					stSaveStatMonthInfo.setLong(76, rs.getLong(75)); // 水位低告警时长

					stSaveStatMonthInfo.setLong(77, rs.getLong(76)); // 燃油堵塞次数
					stSaveStatMonthInfo.setLong(78, rs.getLong(77)); // 燃油堵塞时长

					stSaveStatMonthInfo.setLong(79, rs.getLong(78)); // 机油温度次数
					stSaveStatMonthInfo.setLong(80, rs.getLong(79)); // 机油温度时长

					stSaveStatMonthInfo.setLong(81, rs.getLong(80)); // 缓速器高温次数
					stSaveStatMonthInfo.setLong(82, rs.getLong(81)); // 缓速器高温时长

					stSaveStatMonthInfo.setLong(83, rs.getLong(82)); // 仓温高告警次数
					stSaveStatMonthInfo.setLong(84, rs.getLong(83)); // 仓温高告警时长

					stSaveStatMonthInfo.setLong(85, rs.getLong(84)); // 发动机超转次数
					stSaveStatMonthInfo.setLong(86, rs.getLong(85)); // 发动机超转时长

					stSaveStatMonthInfo.setLong(87, rs.getLong(86)); // 二档起步次数
					stSaveStatMonthInfo.setLong(88, rs.getLong(87)); // 二档起步时长

					stSaveStatMonthInfo.setLong(89, rs.getLong(88)); // 空档滑行次数
					stSaveStatMonthInfo.setLong(90, rs.getLong(89)); // 空档滑行时长

					stSaveStatMonthInfo.setLong(91, rs.getLong(90)); // 急加速次数
					stSaveStatMonthInfo.setLong(92, rs.getLong(91)); // 急加速时长

					stSaveStatMonthInfo.setLong(93, rs.getLong(92)); // 急减速次数
					stSaveStatMonthInfo.setLong(94, rs.getLong(93)); // 急减速时长

					stSaveStatMonthInfo.setLong(95, rs.getLong(94)); // 超长怠速次数
					stSaveStatMonthInfo.setLong(96, rs.getLong(95)); // 超长怠速时长

					stSaveStatMonthInfo.setLong(97, rs.getLong(96)); // 怠速空调次数
					stSaveStatMonthInfo.setLong(98, rs.getLong(97)); // 怠速空调时长

					stSaveStatMonthInfo.setLong(99, rs.getLong(98)); // 制动蹄片磨损次数
					stSaveStatMonthInfo.setLong(100, rs.getLong(99)); // 制动蹄片磨损时长

					stSaveStatMonthInfo.setLong(101, rs.getLong(100)); // 空滤堵塞次数
					stSaveStatMonthInfo.setLong(102, rs.getLong(101)); // 空滤堵塞时长

					stSaveStatMonthInfo.setLong(103, rs.getLong(102)); // 超经济区运行时长

					stSaveStatMonthInfo.setLong(104, rs.getLong(103)); // 区域超速告警次数
					stSaveStatMonthInfo.setLong(105, rs.getLong(104)); // 区域超速告警时长

					stSaveStatMonthInfo.setLong(106, rs.getLong(105)); // 加热器运行时长

					stSaveStatMonthInfo.setLong(107, rs.getLong(106)); // 空调开启时间

					stSaveStatMonthInfo.setLong(108, rs.getLong(107)); // 门1开启次数
					stSaveStatMonthInfo.setLong(109, rs.getLong(108)); // 门2开启次数
					stSaveStatMonthInfo.setLong(110, rs.getLong(109)); // 门3开启次数
					stSaveStatMonthInfo.setLong(111, rs.getLong(110)); // 其他门开启次数
					stSaveStatMonthInfo.setLong(112, rs.getLong(111)); // 门开启总次数
					stSaveStatMonthInfo.setLong(113, rs.getLong(112)); // 区域内开关门次数
					stSaveStatMonthInfo.setLong(114, rs.getLong(113)); // 区域内开门时长

					stSaveStatMonthInfo.setLong(115, rs.getLong(114)); // 机虑堵塞次数
					stSaveStatMonthInfo.setLong(116, rs.getLong(115)); // 机虑堵塞时长

					stSaveStatMonthInfo.setLong(117, rs.getLong(116)); // 超载次数(由多媒体历史信息表中统计)
					stSaveStatMonthInfo.setLong(118, rs.getLong(117)); // 非法停靠次数
					stSaveStatMonthInfo.setLong(119, rs.getLong(118)); // 非法停靠时长

					stSaveStatMonthInfo.setLong(120, rs.getLong(119)); // 非法停靠次数
					stSaveStatMonthInfo.setLong(121, rs.getLong(120)); // 非法停靠时长

					stSaveStatMonthInfo.setLong(122, rs.getLong(121)); // 档位不当次数
					stSaveStatMonthInfo.setLong(123, rs.getLong(122)); // 档位不当持续时间

					stSaveStatMonthInfo.setLong(124, rs.getLong(123)); // //
																		// 怠速时间
					stSaveStatMonthInfo.setLong(125, Integer.parseInt(year)); // 统计年度
					if (rs.getString(124) != null && !"".equals(rs.getString(124))) {
						stSaveStatMonthInfo.setString(126, rs.getString(124)); // 线路ID
					} else {
						stSaveStatMonthInfo.setNull(126, Types.VARCHAR); // 线路ID
					}
					if (rs.getString(125) != null) {
						stSaveStatMonthInfo.setString(127, rs.getString(125)); // 线路名称
					} else {
						stSaveStatMonthInfo.setNull(127, Types.INTEGER); // 线路名称
					}
					stSaveStatMonthInfo.setLong(128, rs.getLong(126)); // 行车油耗

					stSaveStatMonthInfo.setLong(129, rs.getLong(127)); // 缓速器工作时间
					stSaveStatMonthInfo.setLong(130, rs.getLong(128)); // 缓速器工作次数
					stSaveStatMonthInfo.setLong(131, rs.getLong(129)); // 制动信号次数
					stSaveStatMonthInfo.setLong(132, rs.getLong(130)); // 制动信号时间
					stSaveStatMonthInfo.setLong(133, rs.getLong(131)); // 倒档信号次数
					stSaveStatMonthInfo.setLong(134, rs.getLong(132)); // 倒档信号时间
					stSaveStatMonthInfo.setLong(135, rs.getLong(133)); // 近光灯信号次数
					stSaveStatMonthInfo.setLong(136, rs.getLong(134)); // 近光灯信号时间
					stSaveStatMonthInfo.setLong(137, rs.getLong(135)); // 远光灯信号次数
					stSaveStatMonthInfo.setLong(138, rs.getLong(136)); // 远光灯信号时间
					stSaveStatMonthInfo.setLong(139, rs.getLong(137)); // 左转向灯信号时间
					stSaveStatMonthInfo.setLong(140, rs.getLong(138)); // 左转向灯信号次数
					stSaveStatMonthInfo.setLong(141, rs.getLong(139)); // 右转向灯信号次数
					stSaveStatMonthInfo.setLong(142, rs.getLong(140)); // 右转向灯信号时间
					stSaveStatMonthInfo.setLong(143, rs.getLong(141)); // 示廊灯时间
					stSaveStatMonthInfo.setLong(144, rs.getLong(142)); // 示廊灯次数
					stSaveStatMonthInfo.setLong(145, rs.getLong(143)); // 喇叭信号次数
					stSaveStatMonthInfo.setLong(146, rs.getLong(144)); // 喇叭信号时间
					stSaveStatMonthInfo.setLong(147, rs.getLong(145)); // 空调工作次数
					stSaveStatMonthInfo.setLong(148, rs.getLong(146)); // 空档信号次数
					stSaveStatMonthInfo.setLong(149, rs.getLong(147)); // 空档信号时间
					stSaveStatMonthInfo.setLong(150, rs.getLong(148)); // abs工作次数
					stSaveStatMonthInfo.setLong(151, rs.getLong(149)); // abs工作时间
					stSaveStatMonthInfo.setLong(152, rs.getLong(150)); // 加热器工作次数
					stSaveStatMonthInfo.setLong(153, rs.getLong(151)); // 加热器工作时间
					stSaveStatMonthInfo.setLong(154, rs.getLong(152)); // 离合器工作状态次数
					stSaveStatMonthInfo.setLong(155, rs.getLong(153)); // 离合器工作状态时间
					stSaveStatMonthInfo.setLong(156, rs.getLong(154)); // 开门异常
					stSaveStatMonthInfo.setLong(157, rs.getLong(155)); // 精准油耗
					stSaveStatMonthInfo.setLong(158, rs.getLong(156)); // 精准怠速下油耗
					stSaveStatMonthInfo.setLong(159, rs.getLong(157)); // 精准行车油耗
					stSaveStatMonthInfo.setLong(160, rs.getLong(158)); // ECU总油耗
					stSaveStatMonthInfo.setLong(161, rs.getLong(159)); // ECU怠速下油耗
					stSaveStatMonthInfo.setLong(162, rs.getLong(160)); // ECU行车油耗
					stSaveStatMonthInfo.setLong(163, rs.getLong(161)); // 前向碰撞
					stSaveStatMonthInfo.setLong(164, rs.getLong(162)); // 车道偏离
					stSaveStatMonthInfo.setLong(165, rs.getLong(163)); // 油耗标志（1、精准油耗；0、ECU油耗）
					
					stSaveStatMonthInfo.addBatch();
					count++;
					if (count % 5 == 0) {
						stSaveStatMonthInfo.executeBatch();
						stSaveStatMonthInfo.clearBatch();
						count = 0;
					}
				} catch (SQLException ex) {
					logger.error("车辆月统计出错." + vid, ex);
				}
			}

			if (count > 0) {
				stSaveStatMonthInfo.executeBatch();
				stSaveStatMonthInfo.clearBatch();
				count = 0;
			}
		} catch (SQLException e) {
			logger.error("车辆月统计出错." + vid, e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stSaveStatMonthInfo != null) {
				stSaveStatMonthInfo.close();
			}

			if (stQueryStatDayInfo != null) {
				stQueryStatDayInfo.close();
			}
		}
	}

	/**
	 * 统计车辆月报警统计表
	 * 
	 * @throws SQLException
	 */
	private void saveVehicleAlarmMonthDayInfo(long startMonthUtc,
			long endMonthUc, String month, String year) throws SQLException {
		PreparedStatement stSaveVehicleAlarmMonthInfo = null;
		PreparedStatement stQueryVehicleAlarmDayInfo = null;
		ResultSet rs = null;
		try {
			stQueryVehicleAlarmDayInfo = dbCon
					.prepareStatement(queryVehicleAlarmDayInfo);
			stSaveVehicleAlarmMonthInfo = dbCon
					.prepareStatement(saveVehicleAlarmMonthInfo);
			stQueryVehicleAlarmDayInfo.setLong(1, startMonthUtc); // 月报警开始时间
			stQueryVehicleAlarmDayInfo.setLong(2, endMonthUc); // 月报警结束时间
			rs = stQueryVehicleAlarmDayInfo.executeQuery();
			int count = 0;
			while (rs.next()) {
				String vid = rs.getString("vid");
				try {
					String alarmCode = rs.getString("ALARM_CODE");
					stSaveVehicleAlarmMonthInfo.setString(1,
							GeneratorPK.instance().getPKString());
					stSaveVehicleAlarmMonthInfo.setInt(2,
							Integer.parseInt(month)); // 统计月度，如1月写1,2月写2
					stSaveVehicleAlarmMonthInfo.setInt(3,
							Integer.parseInt(year)); // 统计年度，如2011年写2011
					stSaveVehicleAlarmMonthInfo.setString(4, vid);
					stSaveVehicleAlarmMonthInfo.setString(5,
							rs.getString("CORP_ID"));
					stSaveVehicleAlarmMonthInfo.setString(6,
							rs.getString("CORP_NAME"));
					stSaveVehicleAlarmMonthInfo.setString(7,
							rs.getString("TEAM_ID"));
					stSaveVehicleAlarmMonthInfo.setString(8,
							rs.getString("TEAM_NAME"));
					stSaveVehicleAlarmMonthInfo.setString(9,
							rs.getString("VEHICLE_NO"));
					stSaveVehicleAlarmMonthInfo.setString(10,
							rs.getString("VIN_CODE"));
					stSaveVehicleAlarmMonthInfo.setString(11, alarmCode);
					stSaveVehicleAlarmMonthInfo.setInt(12,
							rs.getInt("ALARM_NUM"));
					stSaveVehicleAlarmMonthInfo.setString(13,
							AnalysisDBAdapter.alarmTypeMap.get(alarmCode));
					stSaveVehicleAlarmMonthInfo.setDouble(14,
							rs.getLong("ALARM_TIME"));
					stSaveVehicleAlarmMonthInfo.setLong(15,
							rs.getLong("MILEAGE"));
					stSaveVehicleAlarmMonthInfo.setLong(16,
							rs.getLong("OIL_WEAR"));
					
					if (rs.getString("VLINE_ID") != null && "".equals(rs.getString("VLINE_ID"))) {
						stSaveVehicleAlarmMonthInfo.setString(17,
								rs.getString("VLINE_ID"));
					} else {
						stSaveVehicleAlarmMonthInfo.setNull(17, Types.VARCHAR);
					}

					if (rs.getString("LINE_NAME") != null) {
						stSaveVehicleAlarmMonthInfo.setString(18,
								rs.getString("LINE_NAME"));
					} else {
						stSaveVehicleAlarmMonthInfo.setString(18, null);
					}
					
					stSaveVehicleAlarmMonthInfo.setString(19, rs.getString("DRIVER_ID"));

					stSaveVehicleAlarmMonthInfo.addBatch();
					count++;
					if (count % 3 == 0) {
						stSaveVehicleAlarmMonthInfo.executeBatch();
						stSaveVehicleAlarmMonthInfo.clearBatch();
						count = 0;
					}
				} catch (SQLException e) {
					logger.error("报警月统计出错。" + vid, e);
				}
			}// End while

			if (count > 0) {
				stSaveVehicleAlarmMonthInfo.executeBatch();
				stSaveVehicleAlarmMonthInfo.clearBatch();
				count = 0;
			}
		} catch (SQLException e) {
			logger.error("报警月统计出错。", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stSaveVehicleAlarmMonthInfo != null) {
				stSaveVehicleAlarmMonthInfo.close();
			}

			if (stQueryVehicleAlarmDayInfo != null) {
				stQueryVehicleAlarmDayInfo.close();
			}
		}
	}

	/**
	 * 统计非法营运月表
	 * 
	 * @throws SQLException
	 */
	private void saveVehicleOutLineMonthDayInfo(long startMonthUtc,
			long endMonthUc, String month, String year) throws SQLException {
		PreparedStatement stSaveVehicleOutLineMonthInfo = null;
		PreparedStatement stQueryVehicleOutLineDayInfo = null;
		ResultSet rs = null;
		try {
			stQueryVehicleOutLineDayInfo = dbCon
					.prepareStatement(sql_queryVehicleOutLineDayInfo);
			stSaveVehicleOutLineMonthInfo = dbCon
					.prepareStatement(sql_saveOutLineMonthInfo);
			stQueryVehicleOutLineDayInfo.setLong(1, startMonthUtc); // 月非法营运开始时间
			stQueryVehicleOutLineDayInfo.setLong(2, endMonthUc); // 月非法营运结束时间
			rs = stQueryVehicleOutLineDayInfo.executeQuery();
			int count = 0;
			while (rs.next()) {
				String vid = rs.getString("vid");
				try {
					String alarmCode = rs.getString("OUTLINE_CODE");
					stSaveVehicleOutLineMonthInfo.setString(1,
							GeneratorPK.instance().getPKString());
					stSaveVehicleOutLineMonthInfo.setInt(2,
							Integer.parseInt(month)); // 统计月度，如1月写1,2月写2
					stSaveVehicleOutLineMonthInfo.setInt(3,
							Integer.parseInt(year)); // 统计年度，如2011年写2011
					stSaveVehicleOutLineMonthInfo.setString(4, vid);
					stSaveVehicleOutLineMonthInfo.setString(5,
							rs.getString("CORP_ID"));
					stSaveVehicleOutLineMonthInfo.setString(6,
							rs.getString("CORP_NAME"));
					stSaveVehicleOutLineMonthInfo.setString(7,
							rs.getString("TEAM_ID"));
					stSaveVehicleOutLineMonthInfo.setString(8,
							rs.getString("TEAM_NAME"));
					stSaveVehicleOutLineMonthInfo.setString(9,
							rs.getString("VEHICLE_NO"));
					stSaveVehicleOutLineMonthInfo.setString(10,
							rs.getString("VIN_CODE"));
					stSaveVehicleOutLineMonthInfo.setString(11, alarmCode);
					stSaveVehicleOutLineMonthInfo.setInt(12,
							rs.getInt("OUTLINE_NUM"));
					stSaveVehicleOutLineMonthInfo.setString(13,
							AnalysisDBAdapter.alarmTypeMap.get(alarmCode));
					stSaveVehicleOutLineMonthInfo.setDouble(14,
							rs.getLong("OUTLINE_TIME"));
					if (rs.getString("VLINE_ID") != null && !"".equals(rs.getString("VLINE_ID"))) {
						stSaveVehicleOutLineMonthInfo.setString(15,
								rs.getString("VLINE_ID"));
					} else {
						stSaveVehicleOutLineMonthInfo
								.setNull(15, Types.VARCHAR);
					}

					if (rs.getString("LINE_NAME") != null) {
						stSaveVehicleOutLineMonthInfo.setString(16,
								rs.getString("LINE_NAME"));
					} else {
						stSaveVehicleOutLineMonthInfo.setString(16, null);
					}

					stSaveVehicleOutLineMonthInfo.addBatch();
					count++;
					if (count % 3 == 0) {
						stSaveVehicleOutLineMonthInfo.executeBatch();
						stSaveVehicleOutLineMonthInfo.clearBatch();
						count = 0;
					}
				} catch (SQLException e) {
					logger.error("非法营运月统计出错。" + vid, e);
				}
			}// End while

			if (count > 0) {
				stSaveVehicleOutLineMonthInfo.executeBatch();
				stSaveVehicleOutLineMonthInfo.clearBatch();
				count = 0;
			}
		} catch (SQLException e) {
			logger.error("非法营运统计出错。", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stSaveVehicleOutLineMonthInfo != null) {
				stSaveVehicleOutLineMonthInfo.close();
			}

			if (stQueryVehicleOutLineDayInfo != null) {
				stQueryVehicleOutLineDayInfo.close();
			}
		}
	}

	/***
	 * 存储车辆燃油分析月报
	 * 
	 * @throws SQLException
	 */
	private void stSaveOilWear(long startMonthUtc, long endMonthUc,
			String month, String year) throws SQLException {
		PreparedStatement stSaveOilWear = null;
		try {
			stSaveOilWear = dbCon.prepareStatement(sql_saveOilWear);
			Set<String> set = lastMonthOilMap.keySet();
			Iterator<String> it = set.iterator();
			int count = 0;
			while (it.hasNext()) {
				String vid = it.next();
				try {
					VehicleInfo info = DBAdapter.vehicleInfoMap.get(vid);
					if (info == null) {
						continue;
					}
				} catch (Exception e) {
					logger.error("Sreach VID = " + vid, e);
					continue;
				}
				try {
					stSaveOilWear.setString(1, GeneratorPK.instance().getPKString());
					stSaveOilWear.setInt(2, Integer.parseInt(year));
					stSaveOilWear.setInt(3, Integer.parseInt(month));
					stSaveOilWear.setString(4, vid);
					stSaveOilWear.setString(5, AnalysisDBAdapter.vehicleInfoMap
							.get(vid).getVehicleNo());
					stSaveOilWear.setString(6, AnalysisDBAdapter.vehicleInfoMap
							.get(vid).getEntId());
					stSaveOilWear.setString(7, AnalysisDBAdapter.vehicleInfoMap
							.get(vid).getEntName());
					int assessOil = queryAsseessoil(vid);
					stSaveOilWear.setInt(8, assessOil);
					stSaveOilWear.setLong(9, lastMonthOilMap.get(vid));
					stSaveOilWear.setLong(10,
							assessOil - (lastMonthOilMap.get(vid) / 2)); // 油耗差距(考核油耗
																			// -
																			// 实际油耗)
					int addUpOil = queryOilMonthNum(vid, startMonthUtc,
							endMonthUc);
					stSaveOilWear.setLong(11, addUpOil);
					Long sum[] = queryAccountOilAndMileage(vid);
					stSaveOilWear.setLong(12, sum[0]);
					stSaveOilWear.setLong(13, addUpOil - sum[0] / 2); // 耗油量差距（累计加油
																		// -
																		// 累计耗油量）
					stSaveOilWear.setLong(14, sum[1]);
					stSaveOilWear.setString(15,
							AnalysisDBAdapter.vehicleInfoMap.get(vid)
									.getVrBrandCode());
					stSaveOilWear.setLong(16, System.currentTimeMillis());
					stSaveOilWear.addBatch();
					count++;

					if (count % 3 == 0) {
						stSaveOilWear.executeBatch();
						count = 0;
					}
				} catch (SQLException e) {
					logger.error("车辆燃油分析月统计出错." + vid, e);
				}
			} // End while

			if (count > 0) {
				stSaveOilWear.executeBatch();
				stSaveOilWear.clearBatch();
				count = 0;
			}
		} catch (SQLException e) {
			logger.error("车辆燃油分析月统计出错.", e);
		} finally {
			if (stSaveOilWear != null) {
				stSaveOilWear.close();
			}
			lastMonthOilMap.clear();
		}
	}

	/**
	 * 根据车辆ID查询车辆考核油耗
	 * 
	 * @return
	 * @throws SQLException
	 */
	private int queryAsseessoil(String vid) throws SQLException {
		PreparedStatement stQueryAsseessoil = null;
		ResultSet rs = null;
		try {
			stQueryAsseessoil = dbCon.prepareStatement(sql_queryAsseessoil);
			stQueryAsseessoil.setString(1, vid);
			rs = stQueryAsseessoil.executeQuery();
			if (rs.next()) {
				return rs.getInt("ASSESS_VALUE");
			}
		} catch (SQLException e) {
			logger.error("车辆ID查询车辆考核油耗出错", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryAsseessoil != null) {
				stQueryAsseessoil.close();
			}
		}
		return 0;
	}

	/**
	 * 根据车辆ID，加油时间统计上一月累计加油量
	 * 
	 * @return
	 * @throws SQLException
	 */
	private int queryOilMonthNum(String vid, long startMonthUtc, long endMonthUc)
			throws SQLException {
		PreparedStatement stQueryOilMonthNum = null;
		ResultSet rs = null;

		try {
			stQueryOilMonthNum = dbCon.prepareStatement(queryOilMonthNum);
			stQueryOilMonthNum.setString(1, vid);
			stQueryOilMonthNum.setLong(2, startMonthUtc);
			stQueryOilMonthNum.setLong(3, endMonthUc);
			rs = stQueryOilMonthNum.executeQuery();
			if (rs.next()) {
				return rs.getInt("NUM");
			}
		} catch (SQLException e) {
			logger.error("统计上一月累计加油量", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryOilMonthNum != null) {
				stQueryOilMonthNum.close();
			}
		}
		return 0;
	}

	/**
	 * 根据车辆ID查询车辆总累计油耗和里程
	 * 
	 * @param vid
	 * @return
	 * @throws SQLException
	 */
	private Long[] queryAccountOilAndMileage(String vid) throws SQLException {
		PreparedStatement stQueryVehicleSta = null;
		ResultSet rs = null;
		Long[] sum = new Long[2];
		// 初始化为0
		sum[0] = 0l;
		sum[1] = 0l;
		try {
			stQueryVehicleSta = dbCon.prepareStatement(sql_queryVehicleSta);
			stQueryVehicleSta.setString(1, vid);
			rs = stQueryVehicleSta.executeQuery();

			if (rs.next()) {
				sum[0] = rs.getLong("OIL_WEAR"); // 总累计加油量
				sum[1] = rs.getLong("MILEAGE"); // 总累计里程
			}
		} catch (SQLException e) {
			logger.error("查询车辆总累计油耗和里程", e);
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQueryVehicleSta != null) {
				stQueryVehicleSta.close();
			}
		}
		return sum;
	}
}
