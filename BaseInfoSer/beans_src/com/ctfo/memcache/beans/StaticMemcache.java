package com.ctfo.memcache.beans;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
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
 * <td>2011-11-16</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
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
	public final static String TBSERVICEVIEW_SPEED_2 = "2";
	/**
	 * 参数：路况默认条数
	 */
	public final static Integer ROADCONDITION_NUM = 10;
	
	/**
	 * 参数：排行榜默认条数
	 */
	public final static Integer VEHICLETOP_NUM = 15;
	
	/**
	 * 参数：北京110000,天津120000,河北130000,河南410000,湖南430000,广西450000,海南460000,贵州520000,陕西610000,甘肃620000,青海630000,新疆650000
	 */
	// TODO 临时，记得建立相关编码表
	public final static Long[] PROVINCECODES = { new Long(110000), new Long(120000), new Long(130000), new Long(410000), new Long(430000), new Long(450000), new Long(460000), new Long(520000), new Long(610000), new Long(620000), new Long(630000), new Long(650000) };
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
	 * 参数：企业资讯和企业公告
	 */
	public final static String TBPUBLISHINFO_INFOTYPE_SYS = "002,005";
	
	/**
	 * 参数：公告的类型，代表企业级的公告（企业资讯和企业公告）
	 */
	public final static String TBPUBLISHINFO_SYS = "01";
}
