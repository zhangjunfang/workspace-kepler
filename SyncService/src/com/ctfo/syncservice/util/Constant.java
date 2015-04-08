/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncServer		</li><br>
 * <li>文件名称：com.ctfo.syncserver.util Costant.java	</li><br>
 * <li>时        间：2013-8-29  下午3:51:04	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.util;

/*****************************************
 * <li>描        述：常量		
 * 
 *****************************************/
public class Constant {
	
	/* -----------------------时间------------------------  */
	/** 10秒    */
	public final static long T_10S = 10 * 1000;
	/** 5分钟    */
	public final static long T_5M = 5 * 60 * 1000;
	/** 10分钟    */
	public final static long T_10M = 10 * 60 * 1000;
	/** 15分钟    */
	public final static long T_15M = 15 * 60 * 1000;
	/** 20分钟   */
	public final static long T_20M = 20 * 60 * 1000;
	/** 30分钟    */
	public final static long T_30M = 30 * 60 * 1000;
	/** 45分钟    */
	public final static long T_45M = 45 * 1000;
	/** 1小时    */
	public final static long T_1H = 60 * 60 * 1000;
	
	/* -----------------------符号------------------------  */
	/**  空格      */
	public static final String SPACES = "";
	/**  逗号      */
	public static final String COMMA = ",";
	/**  句号      */
	public static final String PERIOD = ",";
	/** 下划线      */
	public static final String UNDERLINE = "_";
	/** 冒号      */
	public static final String COLON = ":";
	
	/* -----------------------入网车辆查询相关------------------------  */
	/** 入网车辆        */
	public final static String NETWORK_VEHICLE = "NETWORK_VEHICLE";
	/** 运营车辆  	*/
	public static final String OPERATION_VEHICLE = "OPERATION_VEHICLE";
	/** 在线车辆 	 */
	public final static String ONLINE_VEHICLE = "ONLINE_VEHICLE";
	/** 行驶车辆	 */
	public final static String DRIVING_VEHICLE = "DRIVING_VEHICLE";
	/**  查询条件状态 入网车辆  */
	public final static String STATISTICS_TYPE_NETWORK = "01";
	/** 查询条件状态营状车辆 */
	public final static String STATISTICS_TYPE_OPER = "02";
	/** 查询条件状态 运营在线车辆  */
	public final static String STATISTICS_TYPE_ONLINE = "03";
	/**  查询条件状态 运营行驶车辆  */
	public final static String STATISTICS_TYPE_DRIVING = "04";
	/**  查询条件 车辆 */
	public final static String VEHICLE_TOP = "01";
	/** 查询条件 车队 */
	public final static String TEAM_TOP = "02";
	/** 车辆排行榜		*/
	public final static String M_VEHICLETOP = "mVehicleTop";
	/** 车队排行榜 		*/
	public final static String M_VEHICLETEAMTOP = "mVehicleTeamTop";
	
	public static final String SHUTDOWNCOMMAND = "syncservice shutdown";// 停止服务命令

}
