package com.ctfo.dataanalysisservice.beans;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： DataAnalysisService <br>
 * 功能：定义平台报警类型，根据报警类型返回相应的报警描述 <br>
 * 描述： 报警类型公用类 <br>
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
 * <td>2012-2-16</td>
 * <td>wuqj</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wuqj
 * @since JDK1.6
 */
public class PlatAlarmTypeUtil {
	/**
	 * 服务的前缀
	 */
	public static final String KEY_WORD = "DAS";
	/**
	 * 围栏报警
	 */
	public static final int PLAT_AREA_ALARM = 0;
	/**
	 * 偏移路线报警
	 */
	public static final int PLAT_DEVIATE_LINE_ALARM = 1;
	/**
	 * 分段限速报警
	 */
	public static final int PLAT_SECTION_ALARM = 2;
	/**
	 * 关键点报警
	 */
	public static final int PLAT_KEY_POINT_ALARM = 3;
	/**
	 * 关键点的关键字
	 */
	public static final String KEY_POINT_WORD = "KEY_WORD";

	/**
	 * 根据报警类型获取车辆报警描述
	 * 
	 * @param type
	 *            报警类型
	 * @return 报警描述
	 */
	public static String getAlarmDescr(int type) {
		String descr = "未知";
		switch (type) {
		case 0:
			descr = "围栏报警";
			break;
		case 1:
			descr = "偏移路线报警";
			break;
		case 2:
			descr = "分段限速报警";
			break;
		case 3:
			descr = "关键点报警";
			break;
		default:
			break;
		}
		return descr;
	}
}
