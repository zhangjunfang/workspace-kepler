package com.ctfo.mgdb.util;

import java.util.HashMap;

import org.apache.log4j.Logger;


public class AnalyUtil {
	
	private static final Logger log = Logger.getLogger(AnalyUtil.class);
	public static HashMap<String,String> getJson(String command){
		//CAITS 0_1384824304628_6674 E013_13617395958 0 U_REPT {TYPE:0,1:66868924,2:16333838,3:0,4:20131119/092549,5:196,6:2,8:2,20:0}
		//CAITS 0_1384824304628_6674 E013_13617395958 0 D_CALL {TYPE:2}
		HashMap<String,String> json = new HashMap<String,String>();
		String[] command_ng = command.split(" ");
		if(command_ng[5].startsWith("{") && command_ng[5].endsWith("}")){
			String[] com = command_ng[5].substring(1,command_ng[5].length()-1).split(",");
			for(int i = 0;i<com.length;i++){
				String[] command_type = com[i].split(":");
				json.put(command_type[0], command_type[1] == null? "" : command_type[1]);
			}
		}else 	log.debug("erro:上传数据不完整！");
		return json;
	}
    
	 private static String getValue(String key, HashMap<String, String> json)
	  {
	    String value = (String)json.get(key);
	    if (value != null) {
	      if (value.indexOf("!") > -1) {
	        value = value.replace("!", ":");
	      }
	      if (value.indexOf("?") > -1) {
	        value = value.replace("?", ",");
	      }
	    }
	    return value != null ? value : "";
	  }
	 
	 public static int judgeData(String command){
		int flag = -1;
		 try {
			if(command.startsWith("CAITS") || command.startsWith("CAITR")){
					String[] command_arr = command.split(" ");
					String key = command_arr[4];	
					if("U_REPT".equals(key)){
						HashMap<String, String> p = AnalyUtil.getJson(command);
						String type = getValue("TYPE", p);
						if("5".equals(type) || "36".equals(type) || "38".equals(type)){
							flag =  0;
						}
					}
			 }
		} catch (Exception e) {
			log.error("解析数据错误:上传数据不完整！"+":"+command);
			flag = 1;
		}
		 return flag;
	 }
	
	public static void main(String[] args) {
	
	}
}
