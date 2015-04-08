package com.ctfo.statement;

/***
 * 告警标志
 * @author LiangJian
 * 2012-12-4 15:14:05
 */
public class AlarmMark {
	
	/** 围栏进标志 */
	public final static String WLJC = "100001"; 
	/** 围栏出标志 */
	public final static String WLSC = "100010"; 
	/** 围栏超速标志 */
	public final static String WLCS = "100020";
	/** 围栏低速标志 */
	public final static String WLDS = "100021";
	
	/** 围栏开门标志 */
	public final static String WLKM = "100030";
	
	/** 围栏停车标志 */
	public final static String WLTC = "100040";
	
	/** 线路进出标志 */
	public final static String XLJC = "200001";
	/** 线路进出标志 */
	public final static String XLSC = "200010";
	/** 线路超速标志 */
	public final static String XLCS = "200020"; 

	/** 道路等级-高速限速 */
	public final static String DLDJ_GAOSU = "400001";
	/** 道路等级-国道限速 */
	public final static String DLDJ_GUODAO = "400002";
	/** 道路等级-省道限速 */
	public final static String DLDJ_SHENGDAO = "400003";
	/** 道路等级-城区限速 */
	public final static String DLDJ_CHENGQU = "400004";
	/** 道路等级-其他限速 */
	public final static String DLDJ_QITA = "400005";
	
	/** 非法运营 */
	public final static String FFYY = "500000";
	
	/** 离线报警 */
	public final static String CLLX = "660000";
	
	/** 疲劳驾驶报警 */
	public final static String PLJS = "200000";
	
	/** 站点进标志 */
	public final static String ZDJR = "700001"; 
	/** 站点出标志 */
	public final static String ZDSC = "700010"; 
}
