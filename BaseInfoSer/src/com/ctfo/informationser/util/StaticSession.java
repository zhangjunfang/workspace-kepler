package com.ctfo.informationser.util;

import java.util.ArrayList;
import java.util.List;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能：项目公用静态常量 <br>
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
 * <td>2011-9-26</td>
 * <td>yangjian</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author yangjian
 * @since JDK1.6
 */
public class StaticSession {

	public static List<String> sessionNames = new ArrayList<String>();

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
	 * 上线标识 1：上线
	 */
	public static Integer ONLINE = 1;

	/**
	 * 上线标识 1：上线 显示文本
	 */
	public static String ONLINE_MESSAGE = "刚刚上线";

	/**
	 * 下线标识 0：下线
	 */
	public static Integer OFFLINE = 0;

	/**
	 * 下线标识 0：下线 显示文本
	 */
	public static String OFFLINE_MESSAGE = "刚刚下线";
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
	
	/**
	 * 威森厂商编号
	 */
	public final static String VIDEO_OEMCODE_WS="1";
	
	/**
	 * 海康厂商编号
	 */
    public final static String VIDEO_OEMCODE_HK="2";
    
    
    
    /**
	 * 多媒体类型  0：图像 1：音频 2：视频
	 */
	public static String MTYPECODE2NAME(String code){
		
		String name=null;
		if(code!=null){
			if(code.equals("0")){
				name="图像";
			}else if(code.equals("1")){
				name="音频";
			}else if(code.equals("2")){
				name="视频";
			}
		}
		return name;
	}
	
	/**
	 * 多媒体类型  0：图像
	 */
	public final static String MTYPECODE_IMAGE="0";
	
	/**
	 * 多媒体类型   1：音频 
	 */
	public final static String MTYPECODE_VOICE="1";
	
	/**
	 * 多媒体类型   2：视频
	 */
	public final static String MTYPECODE_VEDIO="2";
	
	
	
	/** 
	 * 多媒体格式 0：JPEG 1：TIF 2：MP3 3：WAV 4：WMV
	 */
	public static String MFORMATCODE2NAME(String code){
		
		String name=null;
		if(code!=null){
			if(code.equals("0")){
				name="JPEG";
			}else if(code.equals("1")){
				name="TIF";
			}else if(code.equals("2")){
				name="MP3";
			}else if(code.equals("3")){
				name="WAV";
			}else if(code.equals("4")){
				name="WMV";
			}
		}
		return name;
	}
	
	
	/**
	 * 多媒体格式 0：JPEG 
	 */
	public final static String MFORMATCODE_JPEG="0";
	
	
	/**
	 * 多媒体格式 1：TIF 
	 */
	public final static String MFORMATCODE_TIF="1";
	
	/**
	 * 多媒体格式 2：MP3 
	 */
	public final static String MFORMATCODE_MP3="2";
	
	/**
	 * 多媒体格式3：WAV 
	 */
	public final static String MFORMATCODE_WAV="3";
	
	/**
	 * 多媒体格式 4：WMV
	 */
	public final static String MFORMATCODE_WMV="4";
	
	
	
	/** 
	 * 照片尺寸   0：320*240  1 : 640*480 2: 800*600 3:1024*768
	 */
	public static String PHOTOSENSECODE2NAME(String code){
		
		String name=null;
		if(code!=null){
			if(code.equals("0")){
				name="320*240";
			}else if(code.equals("1")){
				name="640*480";
			}else if(code.equals("2")){
				name="800*600";
			}else if(code.equals("3")){
				name="1024*768";
			}
		}
		return name;
	}
	
	
	/**
	 * 照片尺寸  0：320*240  1 : 640*480 2: 800*600 3:1024*768
	 */
	public final static String PHOTOSENSE_320="0";
	
	public final static String PHOTOSENSE_640="1";
	
	public final static String PHOTOSENSE_800="2";
	
	public final static String PHOTOSENSE_1024="3";
	
	/**
     * 数据库坐标转换为正常地图坐标
     */
    public final static Integer MAP_CONVERT=600000;
}
