package com.ctfo.common.util;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 项目公用静态常量<br>
 * 描述： 项目公用静态常量<br>
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
 * <td>2014-5-23</td>
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
public class StaticSession {

	// 接口操作
	/** 参数为空 */
	public static final String DISMESSAGE_PARAMETERS = "参数为空";

	/** 角色被用 */
	public static final String MESSAGE_SPROLE_FAIL = "角色被用";

	/** 系统异常 */
	public static final String MESSAGE_ERROR = "系统异常";

	/** 判断删除组织 */
	public static final String MESSAGE_REMOVE_ORG = "请先删除下级企业";

	/** 操作成功 */
	public static final String MESSAGE_SUCCESS = "success";

	/** 旧密码错误 */
	public static final String MESSAGE_OLDPASS = "旧密码错误";

	// 账号登录
	/** 账号不存在 */
	public static final String OP_EXIST = "账号不存在";

	/** 账号存在多个 */
	public static final String OP_EXISTS = "账号存在多个";

	/** 账号已过期 */
	public static final String OP_EXPIRED = "账号已过期 ";

	/** 账号已注销 */
	public static final String OP_LOGOUT = "账号已注销";

	/** 账号需重新登录 */
	public final static String OP_LOGIN = "请重新登录";

	/** 验证码输入错误 */
	public static final String OP_CHECKCODE = "验证码输入错误！";

	/** 用户cookie key */
	public final static String COOKIE_USERID = "COOKIE_USERID";

	/** 系统标识前缀 */
	public final static String SYS_MARKING_PREFIX_CENTER = "CENTER";

	// 常用变量
	/** 分隔符 */
	public final static String SPLIT = ",";

	/** 常用数字1 */
	public final static String ONE = "1";

	/** 常用数字2 */
	public final static String TWO = "2";

}