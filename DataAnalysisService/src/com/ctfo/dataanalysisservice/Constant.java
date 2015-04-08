package com.ctfo.dataanalysisservice;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 静态常量类
 * 
 * @author yangyi
 * 
 */

public class Constant {

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

	public final static long FIVEINTVALDATA = 10 * 60 * 1000; // CHECK五分钟数据间隔时间

	// ORACLE连接池前缀
	public static final String ORACLE_POOL_SUFFIX = "jdbc:jdc:oracle:jdcpool";

	// MYSQL连接池前缀
	public static final String MYSQL_POOL_SUFFIX = "jdbc:jdc:mysql:jdcpool";

	
	

	/**
	 * 系统版本号 service version no.
	 */
	static String ver = "DataAnalysisService-1.0.0-2012.02.07";
	
	
	/**
	 * 系统命令提示字符串
	 */
	public static final String SYSTEM_HELPER="Usage: DataAnalysisService <-start|stop|status|version> [-f configfile]"; 
	
	/**
	 * 加载系统配置文件失败
	 */
	public static final String LOAD_CONFIG_ERROR="Can not find the config file SaveCenter.xml in all classpath!";
	
	/**
	 * 系统配置文件
	 */
	public static final String CONFIG_XML="Service_Config.xml"; 
	
	/**
	 * 服务停止命令
	 */
	public static final String  SERVICE_STOP="-stop";

	/**
	 * 服务查询状态命令
	 */
	public static final String  SERVICE_STATUS="-status";
	

	/**
	 * socket停止服务命令
	 */
	public static final String COMMAND_SHUTDOWN = "SaveCenter shutdown";

	/**
	 * socket 状态查询指令
	 */
	public static final String COMMAND_STATUS = "SaveCenter status";
	
	
	/**
	 * 消息服务配置管理测试开关  1启动消息服务直连，0通过节点管理处理
	 */
	public static final String MANAGEFLAG = "1";
	
	
	/**
	 * 主服务SOCKET 读取超时值  单位毫秒
	 */
	public static final int MAIN_SOCKET_TIMEOUT=5000;
	
    public static Map<String,String> perMap = new HashMap<String,String>();
	
	public static List<String> sessionNames = new ArrayList<String>();
	
	
	/**
	 * 命令头 （软报警所数据）
	 */
	public static final String COMMAND_HEAD_ISALARM="CAITS";
	
	/**
	 * 原始命令拆分字符串
	 */
	public static final String COMMAND_SPLIT="\\s+";
	
	/**
	 * 指令字 位置上传
	 */
	public static final String COMMAND_MTYPE_POSITION="U_REPT";
	public static void main(String[] args) {
		int i = 100;
		while (i < 120) {
			System.out.println(i % 1);
			i++;
		}
	}
}
