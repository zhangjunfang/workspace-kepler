/**
 * 
 */
package com.ctfo.storage.command.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;



/**
 * @author zjhl
 *
 */
public class Cache {
	private static Map<String, String> orgTreeMap = new ConcurrentHashMap<String, String>();
	
	public static String getValue(String orgID){
		return orgTreeMap.get(orgID);
	}
	public static void setAllValue(Map<String, String> map){
		orgTreeMap.putAll(map); 
	}
	

}
