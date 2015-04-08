package com.ctfo.syn.membeans;

/**
 * memcache静态常量
 * @author xuehui
 *
 */
public class StaticMemcache {

	/**
	 * 入网车辆数、在线车辆数、接入企业数
	 */
	public final static String MEMCACHE_VEHICLENUM = "mVehicleNum";

	/**
	 * 企业接入车辆数、企业在线车辆数、企业在线行驶车辆数
	 */
	public final static String MEMCACHE_VEHICLECORPNUM = "mVehicleCorpNum";

	/**
	 * 前端标示字符
	 */
	public final static String MEMCACHE_ENTID = "entId";

	/**
	 * 信息反馈
	 */
	public final static String MEMCACHE_TBFEEDBACK = "mTbFeedback";

	/**
	 * 路况
	 */
	public final static String MEMCACHE_ROADCONDITION = "mRoadCondition";

	/**
	 * 公告
	 */
	public final static String MEMCACHE_TBPUBLISHINFO = "mTbPublishInfos";

	/**
	 * 车辆排行榜
	 */
	public final static String MEMCACHE_VEHICLETOP = "mVehicleTop";
	/**
	 * 车队排行榜
	 */
	public final static String MEMCACHE_VEHICLETEAMTOP = "mVehicleTeamTop";

	/**
	 * 平台公告
	 */
	public final static String TBPUBLISHINFO_SYSTEM = "-1";

	/**
	 * 参数：在线
	 */
	public final static String TBSERVICEVIEW_ISONLINE_1 = "1";

	/**
	 * 参数：车辆速度
	 */
	public final static String TBSERVICEVIEW_SPEED_5 = "5";

	/**
	 * 参数：定位标志
	 */
	public final static String TBSERVICEVIEW_ISVALID_0 = "0";

	/**
	 * 参数：路况默认条数
	 */
	public final static Integer ROADCONDITION_NUM = 10;

	/**
	 * 参数：排行榜默认条数
	 */
	public final static Integer VEHICLETOP_NUM = 15;

	/**
	 * 参数：北京110000,天津120000,河北130000,河南410000,湖南430000,广西450000,海南460000,
	 * 贵州520000,陕西610000,甘肃620000,青海630000,新疆650000
	 */
	// TODO 临时，记得建立相关编码表
	public final static Long[] PROVINCECODES = { new Long(110000),
			new Long(120000), new Long(130000), new Long(410000),
			new Long(430000), new Long(450000), new Long(460000),
			new Long(520000), new Long(610000), new Long(620000),
			new Long(630000), new Long(650000) };

	/**
	 * 参数：系统公告
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_001 = "001";

	/**
	 * 参数：企业公告
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_002 = "002";

	/**
	 * 参数：政策法规
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_003 = "003";

	/**
	 * 参数：行业快讯
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_004 = "004";

	/**
	 * 参数：企业资讯
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_005 = "005";

	/**
	 * 参数：平台快报
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_006 = "006";

	/**
	 * 参数：重点推荐
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_008 = "008";

	/**
	 * 参数：安全讲堂
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_007 = "007";

	/**
	 * 参数：企业资讯和企业公告
	 */
	public final static String TBPUBLISHINFO_ORG = "02";

	/**
	 * 参数：公告的类型，系统公告
	 */
	public final static String TBPUBLISHINFO_SYS = "01";

	/**
	 * 友情链接
	 */
	public final static String MEMCACHE_FAVOURITE = "favourite";

	/**
	 * 参数：友情链接
	 */
	public final static String TBFAVOURITE_TYPE_1 = "1";

	/**
	 * 参数：合作伙伴
	 */
	public final static String TBFAVOURITE_TYPE_2 = "2";

	/**
	 * 入网车辆
	 */
	public final static String NETWORK_VEHICLE = "networkVehicle";

	/**
	 * 按运营状态统计入网车辆
	 */
	public final static String STATISTICS_VEHICLE_OPERATION_STATE = "statisticsVehicleOperationState";

	/**
	 * 按运营状态统计在线车辆
	 */
	public final static String STATISTICS_VEHICLE_OPERATION_ONLINE_STATE = "statisticsVehicleOperationOnlineState";

	/**
	 * 按运营状态统计行驶车辆
	 */
	public final static String STATISTICS_VEHICLE_OPERATION_DRIVING_STATE = "statisticsVehicleOperationDrivingState";
	
	/**
	 * 查询条件状态 入网车辆
	 */
	public final static String STATISTICS_TYPE_NETWORK = "01";
	
	/**
	 * 查询条件状态营状车辆
	 */
	public final static String STATISTICS_TYPE_OPER = "02";
	
	/**
	 * 查询条件状态 运营在线车辆
	 */
	public final static String STATISTICS_TYPE_ONLINE = "03";
	
	/**
	 * 查询条件状态 运营行驶车辆
	 */
	public final static String STATISTICS_TYPE_DRIVING = "04";
	
	/**
	 * 查询条件 车辆
	 */
	public final static String VEHICLE_TOP = "01";
	
	/**
	 * 查询条件 车队
	 */
	public final static String TEAM_TOP = "02";
}
