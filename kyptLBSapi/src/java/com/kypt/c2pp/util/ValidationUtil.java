package com.kypt.c2pp.util;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Properties;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kypt.configuration.C2ppTerminalParamCfg;

public class ValidationUtil {
	
	/**
	 * 终端参数设置结果状态 success 成功，failure 终端返回失败,sendfailure 发送失败，nonsupport 终端不支持,timeout 超时
	 */
	public static enum GENERAL_STATUS {success,failure,sendfailure,nonsupport,notonline,timeout}
	
	/**
	 * 事件项编码 0平台下发 1定时动作 2抢劫报警触发 3碰撞侧翻报警触发
	 */
	public static enum EVENT_ENTRY{platsend,timingaction,robalarm,collisionalarm}
	
	public static boolean validateTerminalParamId(HashMap hm){
		boolean flag = true;
		if (hm!=null&&hm.size()>0){
			Properties prop = C2ppTerminalParamCfg.props;
			for (Object obj:hm.keySet()){
				if (!C2ppTerminalParamCfg.props.containsValue(obj)){
					flag = false;
					break;
				}
			}
		}
		
		return flag;
	}
	
	public static HashMap validateTerminalParamIdHm(HashMap hm){
		HashMap hm0 = new HashMap();
		if (hm!=null&&hm.size()>0){
			Properties prop = C2ppTerminalParamCfg.props;
			for (Object obj:hm.keySet()){
				if (!C2ppTerminalParamCfg.props.containsValue(obj)){
					hm0.put(obj, hm.get(obj));
				}
			}
		}
		return hm0;
	}
	
	/**
	 * 拆分字符串 传入[key:value][key:value][key:value][key:value]形式的字符串，分割成HashMap<key,value>
	 */
	public static HashMap splitString_(String str){
		HashMap hm=new HashMap();
		
		Pattern pattern = Pattern.compile("\\[\\w+\\]");

		Matcher matcher = pattern.matcher(str);
		
		ArrayList ls = new ArrayList();
		
		//[1:118,2:base64(GBK)][1:119,2:base64(GBK)]
		while (matcher.find()){
			ls.add(matcher.group());
		}
		
		for (int x=0;x<ls.size();x++){
			String unit = (String)ls.get(x);
			String aa[]=unit.substring(1,unit.length()-1).split(",");
			//[1:23,2:oi]
			String id="";
			String mem="";
			for (int k=0;k<aa.length;k++){
				String bb[]=aa[k].split(":");
				if (bb[0].equals("1")){
					id=bb[1];
				}
				if (bb[0].equals("2")){
					mem=bb[1];
				}
			}
			hm.put(id, mem);
		}
		
		return hm;
		
	}

}
