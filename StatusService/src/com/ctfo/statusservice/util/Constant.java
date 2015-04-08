package com.ctfo.statusservice.util;

/*****************************************
 * <li>描        述：常量		
 * 
 *****************************************/
public class Constant {
	/*-------------------------------数字-------------------------------------*/
	/**	字符串0	*/
	public final static String N0 = "0";
	/**	字符串1	*/
	public final static String N1 = "1";
	/**	字符串2	*/
	public final static String N2 = "2";
	/**	字符串3	*/
	public final static String N3 = "3";
	/**	字符串4	*/
	public final static String N4 = "4";
	/**	字符串5	*/
	public final static String N5 = "5";
	/**	字符串6	*/
	public final static String N6 = "6";
	/**	字符串7	*/
	public final static String N7 = "7";
	/**	字符串8	*/
	public final static String N8 = "8";
	/**	字符串9	*/
	public final static String N9 = "9";
	/**	字符串10	*/
	public final static String N10 = "10";
	/** 上线补传   &  '11'  */
	public static final String N11 = "11";
	/** 告警确认包   &  '12'  */
	public static final String N12 = "12"; 
	/** 第三方数据包   &  '13'  */
	public static final String N13 = "13";
	/** 数据压缩上传   &  '14'  */
	public static final String N14 = "14";
	/** 发动机故障告警   & GPS是否有效  &  '15'  */
	public static final String N15 = "15";
	/** 故障诊断处理   & 触发抓拍事件  &  '16'  */
	public static final String N16 = "16";
	/** 普通上行短信   &  '17'  */
	public static final String N17 = "17";
	/** 设备上下线   &  '18'  */
	public static final String N18 = "18";
	/** 车机状态   &  '19'  */
	public static final String N19 = "19";
	/** 基础报警位   &  '20'  */
	public static final String N20 = "20";
	/** 扩展报警位  &  '21'  */
	public static final String N21 = "21";
	/** 通讯流量(KB)   &  '22'  */
	public static final String N22 = "22";
	/** 温度(.C)   &  '23'  */
	public static final String N23 = "23";
	/** 油箱存油量(升)   &  '24'  */
	public static final String N24 = "24";
	/** 水泥罐车状态   &  '25'  */
	public static final String N25 = "25";
	/** 车辆外设状态   &  '26'  */
	public static final String N26 = "26";
	/** 求助   &  '27'  */
	public static final String N27 = "27";
	/** 身份识别（0成功 1失败）   &  '28'  */
	public static final String N28 = "28";
	/** 认证类型（0中心 1本地）  &  '29'  */
	public static final String N29 = "29";
	/** 黑匣子数据   &  '30'  */
	public static final String N30 = "30";
	/** 事件报告(预警)   & 超速报警  &  '31'  */
	public static final String N31 = "31";	
	/** 提问应答  & 进出区域/路段报警附加信息类型 & '32'  */
	public static final String N32 = "32";	
	/** 信息点播/取消  &  短消息内容 & '33'  */
	public static final String N33 = "33";	
	/** 周边信息查询   & 告警确认包  & '34'  */
	public static final String N34 = "34";	
	/** 电子路单   &  路线行驶时间不足/过长  & '35'  */
	public static final String N35 = "35";	
	/** 终端注册  & 第三方数据包 &  '36'  */
	public static final String N36 = "36";	
	/** 终端注销   &  '37'  */
	public static final String N37 = "37";	
	/** 终端鉴权   &  '38'  */
	public static final String N38 = "38";	
	/** 多媒体事件上传   &  '39'  */
	public static final String N39 = "39";	
	/** 多媒体进度通知   & 省域ID & '40'  */
	public static final String N40 = "40";
	/**  发动机超转告警  & '47'  */
	public static final String N47 = "47";
	/**	字符串50	*/
	public final static String N50 = "50";
	/**历史数据上传   &  起点时间  & '51'  */
	public static final String N51 = "51";	
	/**	字符串52	*/
	public final static String N52 = "52";
	/** 终端版本信息上传   &  乘车时间  & '53'  */
	public static final String N53 = "53";	
	/** 怠速信息上传   & 上车时间  &  '54'  */
	public static final String N54 = "54";	
	/** 下车时间  &  '55'  */
	public static final String N55 = "55";	
	/** CAN 总线数据上传   &  '30'  */
	public static final String N60 = "60";	
	/** 90  */
	public static final String N90 = "90";
	/** 123  */
	public static final String N123 = "123";
	/** 126  */
	public static final String N126 = "126";
	/** 127  */
	public static final String N127 = "127";
	
	
	/** 发动机最大转速   &  '210'  */
	public static final String N210 = "210";
	/** 213  */
	public static final String N213 = "213";
	
	/** 218 & 速度来源  */
	public static final String N218 = "218";
	/** 500 & 扩展状态  */
	public static final String N500 = "500";
	/** 516  */
	public static final String N516 = "516";
	/** 514  */
	public static final String N514 = "514";
	
	
	
	/*-------------------------------标点符号-------------------------------------*/
	/**	空格 	*/
	public static final String SPACES = " ";
	/**	空字符串	 */
	public static final String EMPTY = "";
	/**  逗号      */
	public static final String COMMA = ",";
	/**  句号      */
	public static final String PERIOD = ".";
	/** 下划线      */
	public static final String UNDERLINE = "_";
	/** 中划线 减号      */
	public static final String MINUS = "-";
	/** 冒号      */
	public static final String COLON = ":";
	/** 换行符      */
	public static final String NEWLINE = " \r\n";
	/** 反斜杠     */
	public static final String BACKSLASH = "/";
	/** 左大括号 	*/
	public static final String LEFT_BRACE = "{";
	/** 右大括号     */
	public static final String RIGHT_BRACE = "}";

	
	/** 时间路径格式     */
	public static final String FILE_PATH_DATE = "/yyyy/MM/dd/";
	
	
	/** 文件格式     */
	public static final String TXT = ".txt";
	
	
	/** 读写状态    */
	public static final String RW = "rw";
	
	/** 疲劳驾驶报警    */
	public static final String ALARM_2 = ",2,"; 
	
	
	/*-------------------------------内部协议常量-------------------------------------*/
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
	/**	位置汇报	*/
	public final static String U_REPT = "U_REPT";
	/**	请求终端数据指令	*/
	public final static String D_REQD = "D_REQD";
	
	/**	内部协议包	*/
	public final static String CAITS = "CAITS";
	/**	内部协议包	*/
	public final static String CAITR = "CAITR";
	/**	车辆速度	*/
	public static final String SPEED = "SPEED";
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

	public static final String RATIO = "ratio";

	public static final String GEARS = "gears";
	/**非法轨迹类型   1经度错误 2纬度错误 3定位时间错误  4车辆速度错误 5行驶方向错误 6车辆状态错误 9其他错误*/
	public static final String ISPVALID = "isPValid"; //非法轨迹类型   1经度错误 2纬度错误 3定位时间错误  4车辆速度错误 5行驶方向错误 6车辆状态错误 9其他错误

	public static final String SHUTDOWNCOMMAND = "statusservice shutdown";// 停止服务命令

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
	
	/** MSG心跳 */
	public static final String NOOP = "NOOP";
	/** 报警信息 */
	public static final String ALARM_CODE = "ALARM_CODE";
	/** 消息服务器key */
	public final static String MSG_KEY = "MSG_KEY";// 插件类型
	
	
	/*-------------------------------指令-------------------------------------*/
	/** 指令类型		 */
	public static final String TYPE = "TYPE";










}
