package com.ctfo.savecenter;

/**
 * 静态常量类
 * 
 * @author yangyi
 * 
 */

public class Constant {
	/**空格 */
	public static final String SPACES = "";
	/**
	 * 内部协议常量
	 */
	public final static String COMMAND = "command";// 原始指令
	public final static String HEAD = "head";// 包头
	public final static String SEQ = "seq";// 业务序列号
	public final static String MACID = "macid";// 车辆标识
	public final static String CHANNEL = "channel";// 通道
	public final static String MTYPE = "mtype";// 类型
	public final static String CONTENT = "content";// 具体内容
	public final static String MSGID = "msgid";// 消息服务器id
	public final static String UUID = "uuid";// 指令唯一标识uuid
	public final static String VID = "vid";// 车辆id
	public final static String PTYPE = "ptype";// 插件类型

	public final static String OEMCODE = "oecode"; // OEMCODE
	public final static String PLATECOLORID = "platecolorid"; // 车牌颜色ID
	public final static String TID = "tid"; // 终端ID
	public final static String VEHICLENO = "vehicleno"; // 车牌号

	public final static String REARAXLERATE = "rearaxlerate";// 后桥速比
	public final static String TYRER = "tyrer";// 轮胎滚动半径

	/***
	 * 解析后常量
	 */
	public final static String MAPLON = "maplon"; // 偏移经度
	public final static String MAPLAT = "maplat"; // 偏移纬度
	public final static String COMMDR = "commdr"; // 手机号
	public static final String UTC = "utc";//gps上报UTC时间
	public static final String ALARMCODE = "alarmcode";//报警编码
	public final static String MAXRPM = "maxrpm";

	
	public static final String ORACLE_POOL_SUFFIX = "jdbc:jdc:oracle:jdcpool";// ORACLE连接池前缀

	public static final String MYSQL_POOL_SUFFIX = "jdbc:jdc:mysql:jdcpool";// MYSQL连接池前缀

	public static final String RATIO = "ratio";

	public static final String GEARS = "gears";
	/**非法轨迹类型   1经度错误 2纬度错误 3定位时间错误  4车辆速度错误 5行驶方向错误 6车辆状态错误 9其他错误*/
	public static final String ISPVALID = "isPValid"; //非法轨迹类型   1经度错误 2纬度错误 3定位时间错误  4车辆速度错误 5行驶方向错误 6车辆状态错误 9其他错误

	public static final String SHUTDOWNCOMMAND = "SaveCenter shutdown";// 停止服务命令

	public static final String STATUSCOMMAND = "SaveCenter status";// 状态查询指令
	/** 队列状态查询指令 */
	public static final String QUEUECOMMAND = "SaveCenter queue";// 
	/** 处理状态查询指令 */
	public static final String PROCESSCOMMAND = "SaveCenter process";// 
	
	public static final String VER = "SaveCenter-1.0.0.0-2013.02.18";// 版本
	
	public final static String SPEEDFROM = "speedfrom"; // 车速来源
	
	public final static int EXPIRETIME = 24 * 60 * 60; //失效时间 (单位：秒)

	public static final String FILEALARMCODE = "filealarmcode";//文件报警编码
	
	/** 多媒体上传设备类型 */
	public static final String DEV_TYPE = "CHANNEL_TYPE";
	/** 终端参数设置 */
	public static final String D_SETP = "D_SETP";
	/** 终端控制指令 */
	public static final String D_CTLM = "D_CTLM";
	/** 车架号 */
	public static final String VIN_CODE = "VIN_CODE";
	
}
