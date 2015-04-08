package com.ctfo.util;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 常量<br>
 * 描述： 常量<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
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
 * <td>2014-10-27</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class Constant {

	/* ---------------------标点符号-------------------- */
	/** $符号 */
	public static final String DOLLAR = "$";

	/** 左中括号 */
	public static final String LEFT_BRACKET = "[";

	/** 右中括号 */
	public static final String RIGHT_BRACKET = "]";

	/** 下划线 */
	public static final String UNDERLINE = "_";

	/** 中划线 减号 */
	public static final String MINUS = "-";

	/** 句号 */
	public static final String PERIOD = ".";

	/* ---------------------消息主类型-------------------- */

	/** 服务站上传数据类 */
	public static final String TYPE_U = "$U$";

	/** 服务站上传附件类 */
	public static final String TYPE_F = "$F$";

	/** 服务站上传客户端状态类 */
	public static final String TYPE_T = "$T$";

	/** 客户端上传车厂数据类 */
	public static final String TYPE_Y = "$Y$";

	/** 支撑系统数据上传类 */
	public static final String TYPE_S = "$S$";

	/** 支撑系统控制指令传类 */
	public static final String TYPE_C = "$C$";

	/** 云平台同步车厂数据类 */
	public static final String TYPE_YD = "$YD$";

	/** 云平台同步支撑数据类 */
	public static final String TYPE_SD = "$SD$";

	/** 云平台同步支撑系统控制指令传类 */
	public static final String TYPE_CD = "$CD$";

	/** 服务端应答类 */
	public static final String TYPE_A = "$A$";

	/** 客户端链路管理类 */
	public static final String TYPE_L = "$L$";

	/** 通用应答类 */
	public static final String TYPE_R = "$R$";

	/* ---------------------消息子类型-------------------- */

	/** 公告 */
	public static final String TYPE_SD1 = "SD1";

	/** 登录验证应答 */
	public static final String TYPE_A1 = "A1";

	/** 心跳应答 */
	public static final String TYPE_A2 = "A2";

	/** 登录 */
	public static final String TYPE_L1 = "L1";

	/** 心跳 */
	public static final String TYPE_L2 = "L2";

	/** 维修 */
	public static final String TYPE_U1 = "$U1_";

	/** 权限管理 */
	public static final String TYPE_U2 = "$U2_";

	/** 库存管理 */
	public static final String TYPE_U3 = "$U3_";

	/** 财务管理 */
	public static final String TYPE_U4 = "$U4_";

	/** 会员管理 */
	public static final String TYPE_U5 = "$U5_";

	/** 基础数据 */
	public static final String TYPE_U6 = "$U6_";

	/** 系统设置 */
	public static final String TYPE_U7 = "$U7_";

	/* ---------------------数字-------------------- */

	/** 字符串0 */
	public final static String N0 = "0";

	/** 字符串1 */
	public final static String N1 = "1";

	/** 字符串2 */
	public final static String N2 = "2";

	/** 字符串3 */
	public final static String N3 = "3";

	/** 字符串4 */
	public final static String N4 = "4";

	/* ---------------------文件类型-------------------- */

	/** TXT */
	public final static String TXT = "txt";

	/** XML */
	public final static String XML = "xml";

	/** JPG */
	public final static String JPG = "jpg";
}
