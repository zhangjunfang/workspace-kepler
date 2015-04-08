/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncServer		</li><br>
 * <li>文件名称：com.ctfo.syncserver.core ApplicationCache.java	</li><br>
 * <li>时        间：2013-8-30  上午9:44:35	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/*****************************************
 * <li>描        述：应用缓存		
 * 
 *****************************************/
public class Cache {
	/** 车辆对应驾驶员关系表   */
	private static Map<String, String> vehicleDriverMap = new ConcurrentHashMap<String, String>();
	
	/*****************************************
	 * <li>描        述：更新车辆、驾驶员对应关系 		</li><br>
	 * <li>时        间：2013-8-30  上午9:57:30	</li><br>
	 * <li>参数： @param vid
	 * <li>参数： @param driverName			</li><br>
	 * 
	 *****************************************/
	public static void saveVehicleDriverMap(String vid, String driverName){
		if(vid !=null && driverName != null){
			vehicleDriverMap.put(vid, driverName);
		}
	}
	/*****************************************
	 * <li>描        述：根据车辆编号获取驾驶员姓名 		</li><br>
	 * <li>时        间：2013-8-30  上午9:59:40	</li><br>
	 * <li>参数： @param vid
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String getDriverNameByVid(String vid){
		return vehicleDriverMap.get(vid);
	}
	/*****************************************
	 * <li>描        述：是否存在对应的车辆编号		</li><br>
	 * <li>时        间：2013-8-30  上午9:58:00	</li><br>
	 * <li>参数： @param vid
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static boolean containsKey(String vid){
		return vehicleDriverMap.containsKey(vid);
	}
	/*****************************************
	 * <li>描        述：是否存在对应驾驶员信息 		</li><br>
	 * <li>时        间：2013-8-30  上午9:58:26	</li><br>
	 * <li>参数： @param driverName
	 * <li>参数： @return			</li><br>
	 * 
	 **************************************** */
	public static boolean containsValue(String driverName){
		return vehicleDriverMap.containsValue(driverName);
	}
	/*****************************************
	 * <li>描        述：根据车辆编号删除缓存信息 		</li><br>
	 * <li>时        间：2013-8-30  上午10:10:27	</li><br>
	 * <li>参数： @param vid			</li><br>
	 * 
	 *****************************************/
	public static void  removeVehicleDriverMap(String vid){
		vehicleDriverMap.remove(vid);
	}
	/*****************************************
	 * <li>描        述：清空车辆对应驾驶员缓存 		</li><br>
	 * <li>时        间：2013-8-30  上午10:13:45	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public static void clearVehicleDriverMap(){
		vehicleDriverMap.clear();
	}
	
}
