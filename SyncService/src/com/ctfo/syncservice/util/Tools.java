/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncServer		</li><br>
 * <li>文件名称：com.ctfo.syncserver.util Tools.java	</li><br>
 * <li>时        间：2013-8-30  下午1:57:33	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.util;



/*****************************************
 * <li>描        述：工具		
 * 
 *****************************************/
public class Tools {
	/** 初始状态	*/
	public static boolean firstStatus = true;
	/** 同步排行榜状态	*/
	public static boolean syncTopStatus = false;
	
//	/** 鉴权信息同步状态		*/
//	public static String auth = "";
//	/** 鉴权信息最近更新时间		*/
//	public static long authUtc = System.currentTimeMillis();
//	
//	/** 驾驶员信息同步状态		*/
//	public static String driver = "";
//	/** 驾驶员信息最近更新时间		*/
//	public static long driverUtc = System.currentTimeMillis();
//	
//	/** 静态鉴权车辆信息同步状态		*/
//	public static String car = "";
//	/** 静态鉴权车辆信息最近更新时间		*/
//	public static long carUtc = System.currentTimeMillis();
//	
//	/** 电子运单同步状态		*/
//	public static String eticket = "";
//	/** 电子运单最近更新时间		*/
//	public static long eticketUtc = System.currentTimeMillis();
//	
//	/** 车牌号获取手机号同步状态		*/
//	public static String phone = "";
//	/** 车牌号获取手机最近更新时间		*/
//	public static long phoneUtc = System.currentTimeMillis();
	
	/*****************************************
	 * <li>描        述：替换逗号添加＃号 		</li><br>
	 * <li>示        例：1,2,3 = #1#2#3#
	 * <li>时        间：2013-8-30  下午2:02:09	</li><br>
	 * <li>参数： @param entIdArray
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String addPound(String entIdArray){
		if(entIdArray == null || entIdArray.length() < 1){
			return Constant.SPACES;
		}
		String[] arr = entIdArray.split(",");
		StringBuffer newEntIdArray = new StringBuffer("#");
		for(int i = arr.length -1; i >=0; i-- ){
			newEntIdArray.append(arr[i]);
			newEntIdArray.append("#");
		} 
		return newEntIdArray.toString();
	}
}
