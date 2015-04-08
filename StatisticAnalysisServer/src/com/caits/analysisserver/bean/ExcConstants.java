package com.caits.analysisserver.bean;

public class ExcConstants {
	
	//机油压力 单位：1bit=4Kpa 0=0Kpa
	public static final int OILPRESSURE = 4;
	
	//大气压力 单位：1bit=0.5Kpa 0=0Kpa
	public static final float  GSPRESSURE = 0.5f;
	
	//冷却液温度 单位：1bit=1℃ 0=-40℃
	public static final int COOLLIQUID = -40;
	
	//上报时间间隔
	public static final int REPORTTIME = 5 * 60;
	
	//发动机转速单位转换
	public static final float RPMUNIT = 0.125f;
	
	// 连接池前缀
	public static final String POOL_SUFFIX= "jdbc:jdc:jdcpool";
	
	//非法营运编码
	public static final String OUTLINE_CODE = "230";
	
	//超员编码
	public static final String OVERLOAD_CODE = "231";
	
	//路段行驶时间不足
	public static final String RUNNINGNOTENONGHTIME = "232";
	
	//路段行驶时间过长
	public static final String RUNNINGLONGTIME = "233";
	
	//区域内开门
	public static final String OPENINGDOOR = "234";
	
	//带速开门
	public static final String IDELINGOPENINGDOOR = "235";
	
	//区域外开门
	public static final String OUTOPENINGDOOR = "236";
	
	//平台数据最大间隔
	public static final long PLATFORM_REPORT_DATA_LONGEST_INTERVAL_TIME = 18 * 60 * 1000;
	
	//终端上报数据最大间隔
	public static final long TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME = 5 * 60 * 1000;
	
	// 夜间2点到5点非法营运最大时间间隔
	public static final long ILLEGAL_OUTLINE_NIGHT_INTERVAL_TIME = 10 * 60 * 1000;
}
