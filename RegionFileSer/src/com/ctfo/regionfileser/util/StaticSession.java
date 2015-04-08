package com.ctfo.regionfileser.util;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： monitorser
 * <br>
 * 功能：项目公用静态常量
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交兴路信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2011-9-26</td><td>yangjian</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author yangjian
 * @since JDK1.6
 */
public class StaticSession {
	
	
	public static Map<String,String> perMap = new HashMap<String,String>();
	
	public static List<String> sessionNames = new ArrayList<String>();
	
	
	/**
	 * 文本消息
	 */
	public final static String DISPLAY_MESSAGE="消息内容:";
	
	
	/**
	 * 抓拍指令
	 */
	public final static String DISPLAY_PHOTO="抓拍指令";
	
	/**
	 * 监听
	 */
	public final static String DISPLAY_MONITOR="监听号码:";
	
	
	
	/**
	 * 抓拍指令
	 */
	public final static String DISPLAY_EMPHASIS="重点追踪上报时间间隔:";
	
	
	/**
	 * 报警处理状态 正在处理 1
	 */
	public final static String ALARMHANDLE_ING="1";
	
	
	/**
	 * 报警处理状态 处理成功 2
	 */
	public final static String ALARMHANDLE_SUCCESS="2";
	
	/**
	 * id分隔符
	 */
	public final static String SPLITSTR=",";
	/**
	 * 指令来源 CoFrom 0本平台 1监管平台
	 */
	public final static Integer COFROMSELF=0;
	
	  /**
	   * 发送状态 -1等待回应 
	   */
	  public final static Integer SENDSTATUS_WAIT=-1;
	  
	  /**
	   * 发送状态 -1等待回应  文字
	   */
	  public final static String SENDSTATUS_MESSAGE_WAIT="等待回应 ";
	
	  /**
	   * 发送状态 0:成功
	   */
	  public final static Integer SENDSTATUS_SUCCESS=0;
	
	  /**
	   * 发送状态 0:成功  文字
	   */
	  public final static String SENDSTATUS_MESSAGE_SUCCESS="发送成功 ";
	  
	  /**
	   * 发送状态 1:设备返回失败
	   */
	  public final static Integer SENDSTATUS_RETURNERROR=1;
	  
	  /**
	   * 发送状态 1:设备返回失败 文字
	   */
	  public final static String SENDSTATUS_MESSAGE_RETURNERROR="设备返回失败 ";
	  /**
	   * 发送状态 2:发送失败
	   */
	  public final static Integer SENDSTATUS_SENDERROR=2;
	  
	  /**
	   * 发送状态 2:发送失败 文字
	   */
	  public final static String SENDSTATUS_MESSAGE_ENDERROR="发送失败 ";
	  /**
	   * 发送状态  3:设备不支持此功能
	   */
	  public final static Integer SENDSTATUS_NONSUPPORT=3;
	  
	  /**
	   * 发送状态  3:设备不支持此功能  文字
	   */
	  public final static String SENDSTATUS_MESSAGE_NONSUPPORT="设备不支持此功能 ";
	  /**
	   * 发送状态  4:设备不在线
	   */
	  public final static Integer SENDSTATUS_NOTONLINE=4;
	  
	  /**
	   * 发送状态  4:设备不在线  文字
	   */
	  public final static String SENDSTATUS_MESSAGE_NOTONLINE="设备不在线 ";
	  
	  
	  
	  /**
	   * 重点跟踪 默认时间 
	   */
	  public final static String EMPHASIS_DEFAULT="3";   //设置默认值 3秒
	  
	public final static String LOGDIR = "/log/";
	public final static String DATALOGDIR = "/log/bak/datalog/";
	public final static String ZIPDOWNLOADDIR = "/log/temp/databaklog.zip";
	public final static String ZIPHUIGUNDIR = "/log/bak/";
	public final static String SYSLOGDIR = "/log/bak/syslog/";
	public final static String USERLOGDIR = "/log/bak/userlog/";
	public final static String LINELOGDIR = "/log/bak/linelog/";
	public final static String SERVERDIR = "/log/server/";
	public final static String DELETETEMPDIR = "/log/temp/";
	public final static String APPDIR = "/log/app/";

	//在线
	public final static int ISONLINE_TRUE=1;
	
	//不在线
	public final static int ISONLINE_FALSE=0;
	
	
	
	/**
	 * IO操作异常号 0 正常
	 */
	public final static int IO_STAT_OK = 0;

	/**
	 * IO操作异常号 -1 存储对象为空
	 */
	public final static int IO_OBJ_NULL = -1;

	/**
	 * IO操作异常号 -2 创建文件失败
	 */
	public final static int IO_CREATE_FAIL = -2;

	/**
	 * IO操作异常号 -3 写文件失败
	 */
	public final static int IO_WRITE_FAIL = -3;

	/**
	 * IO操作异常号 -4 读文件失败
	 */
	public final static int IO_READ_FAIL = -4;

	/**
	 * IO操作异常号 5 删除文件失败
	 */
	public final static int IO_DEL_FAIL = -5;

	/**
	 * IO操作异常号 5 创建文件夹失败
	 */
	public final static int IO_MKDIR_FAIL = -6;
	
	/**
	 * 实体类型  0为，1为企业，2为车队
	 */

	/**
	 * 实体类型  运营商 0
	 */
	public final static String ENTTYPE_OPE="0";
	/**
	 * 实体类型  企业  1
	 */
	public final static String ENTTYPE_CORP="1";
	
	/**
	 * 实体类型  车队  2
	 */
	public final static String ENTTYPE_TEAM="2";

	
	public final static String COMMAND_SPLIT="|";
	
	
	// ////////////////////////////////////////////////////////////////////////////////
	// 定义日志类型Code，用于Information工程记录日志，对应的日志对象的logTypeid属性
	/**
	 * 用户登录
	 */
	public final static String USERLOGIN = "USERLOGIN";
	/**
	 * 系统操作
	 */
	public final static String SYSOPERATE = "SYSOPERATE";
	/**
	 * 终端控制
	 */
	public final static String TERMINALCONTROL = "TERMINALCONTROL";
	/**
	 * 系统巡检
	 */
	public final static String SYSINSPECTION = "SYSINSPECTION";
	//////////////////////////////////////////////////////////////////////////////////
}
