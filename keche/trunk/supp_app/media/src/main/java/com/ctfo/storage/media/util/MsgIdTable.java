package com.ctfo.storage.media.util;

public class MsgIdTable {
	public static String SERVER_RESPONSE_ID = "C001";//服务端应答ID
	
	public static String CLIENT_RESPONSE_ID = "1001";//客户端应答ID
	
	public static String LINK_MANAGE_ID = "1100";//链路管理ID
	public static String LINK_MANAGE_LOGIN_ID = "1101";//登录id
	public static String LINK_MANAGE_NOOP_ID = "1102";//心跳id
	
	public static String TERMINAL_UP_ID = "1200";//终端主动上传id
	public static String TERMINAL_UP_UPDOWN_ID = "1201";//车辆上下线通知
	public static String TERMINAL_UP_REGISTER_ID = "1202";//终端注册
	public static String TERMINAL_UP_LOGOUT_ID = "1203";//终端注销
	public static String TERMINAL_UP_AUTH_ID = "1204";//终端鉴权
	public static String TERMINAL_UP_REALLOC_ID = "1205";//车辆实时位置数据上传
	public static String TERMINAL_UP_HISTORYLOC_ID = "1206";//车辆历史位置数据补传
	public static String TERMINAL_UP_MEDIAEVENT_ID = "1207";//多媒体事件上传
	public static String TERMINAL_UP_MEDIADATA_ID = "1208";//多媒体数据上传
	public static String TERMINAL_UP_DRIVE_ID = "1209";//驾驶行为事件上传
	public static String TERMINAL_UP_VERSION_ID = "120A";//终端版本信息上传
	public static String TERMINAL_UP_DISPATCH_ID = "120B";//终端透传数据上传
	public static String TERMINAL_UP_ALARM_ID = "120C";//车辆报警数据上传
	
	
	public static String TERMINAL_CONTROL_ID = "C300";//终端控制交互id
	public static String TERMINAL_CONTROL_LISTEN_ID = "C301";//车辆监听请求
	public static String TERMINAL_CONTROL_PHOTO_ID = "C302";//车辆拍照请求
	public static String TERMINAL_CONTROL_TEXT_ID = "C303";//车辆文本消息请求
	public static String TERMINAL_CONTROL_DISPATCH_ID = "C304";//终端转移调度请求
	
	public static String MCENTER_DISPATCH_ID = "C400";//主中心转发id
	public static String MCENTER_DISPATCH_REALLOC_ID = "C401";//车辆实时位置数据转发

	
	public static String MCENTER_PUSH_ID = "C500";//主中心推送基础信息id
	public static String MCENTER_PUSH_BASEINFO_ID = "C501";//车辆基础资料配置推送
	public static String MCENTER_PUSH_ALARMLEVEL_ID = "C502";//告警等级
	public static String MCENTER_PUSH_ALARMTYPE_ID = "C503";//告警类型
	public static String MCENTER_PUSH_REGIONTABLE_ID = "C504";//全国行政区划表
	public static String MCENTER_PUSH_SCENTERTABLE_ID = "C505";//系统分中心表

	
	public static String SCENTER_UP_ID = "1600";//分中心主动上传基础信息id
	public static String SCENTER_UP_COMPANY_CONF_ID = "1601";//客户判断规则配置表
	public static String SCENTER_UP_CORP_LEVEL_ID = "1602";//企业经营等级表
	public static String SCENTER_UP_FUNCTION_ID = "1603";//系统功能表
	public static String SCENTER_UP_GENERAL_CODE_ID = "1604";//通用编码表
	public static String SCENTER_UP_OUTLINE_TYPE_ID = "1605";//运营违规统计类型
	public static String SCENTER_UP_STAFF_WORK_TYPE_ID = "1606";//从业人员资格信息表
	public static String SCENTER_UP_TERMINAL_CONFIGUER_CODE_ID = "1607";//车终端配置参数码表
	public static String SCENTER_UP_ALARM_ENT_CONF_ID = "1608";//报警企业配置表
	public static String SCENTER_UP_ALARM_ENT_INFO_ID = "1609";//报警企业配置信息表
	public static String SCENTER_UP_ALARM_NOTICE_ID = "160A";//企业告警提示语音设置信息
	public static String SCENTER_UP_ALARM_TACTICS_SET_ID = "160B";//夜间非法运营时间设置
	public static String SCENTER_UP_AREA_ID = "160C";//电子围栏表
	public static String SCENTER_UP_ASSESSOIL_SET_ID = "160D";//系统车辆考核表
	public static String SCENTER_UP_CHECKMONTH_SET_ID = "160E";//考核月设置
	public static String SCENTER_UP_CLASS_LINE_ID = "160F";//线路信息表
	public static String SCENTER_UP_CUSTOM_REPORT_COLUMNS_ID = "1610";//用户自定义报表列信息表
	public static String SCENTER_UP_DVR_ID = "1611";//3G视频终端信息表
	public static String SCENTER_UP_DVRSER_ID = "1612";//3G视频服务器信息表
	public static String SCENTER_UP_EMPLOYEE_ID = "1613";//驾驶员信息表
	public static String SCENTER_UP_ENG_DIAGNOSIS_CONF_ID = "1614";//发动机远程诊断配置信息表
	public static String SCENTER_UP_ENG_VERSION_ID = "1615";//发动机版本信息表
	public static String SCENTER_UP_FEEDBACK_ID = "1616";//反馈信息表
	public static String SCENTER_UP_IC_CARD_ID = "1617";//IC卡信息
	public static String SCENTER_UP_LINE_PROP_ID = "1618";//线段属性表
	public static String SCENTER_UP_LINE_STATION_ID = "1619";//线路站点信息表
	public static String SCENTER_UP_LOCK_VEHICLE_DETAIL_ID = "161A";//远程锁车信息表
	public static String SCENTER_UP_MAINTAIN_CLASS_ID = "161B";//智能维保项目表
	public static String SCENTER_UP_MAINTAIN_PLAN_ID = "161C";//智能维保计划表
	public static String SCENTER_UP_ORG_INFO_ORGANIZATION_ID = "161D";//企业组织表
	public static String SCENTER_UP_PHONEEXAMINE_CONF_ID = "161E";//照片互检配置表
	public static String SCENTER_UP_PHOTO_SETTINGS_ID = "161F";//触发拍照设置基本设置表
	public static String SCENTER_UP_PHOTO_SETTINGS_DETAIL_ID = "1620";//终端拍照设置明细表
		
		
}
