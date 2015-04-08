package com.ctfo.savecenter.analy;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 * 兼容老协议数据
 * 
 * @author yangyi
 * 
 */
public class OldAgreementData {
	public static Map<String, Integer> alarmMap = new ConcurrentHashMap<String, Integer>();// 报警代码
	public static Map<String, Integer> baseStatusMap = new ConcurrentHashMap<String, Integer>();// 基本状态代码
	public static Map<String, Integer> extendStatusMap = new ConcurrentHashMap<String, Integer>();// 扩展状态代码

	static {
		alarmMap.put("0", 0);// 紧急报警
		alarmMap.put("4", 3);// 预警
		alarmMap.put("5", 4);// 导航模块故障
		alarmMap.put("6", 5);// 导航系统天线未接
		alarmMap.put("7", 7);// 终端主电源欠压
		alarmMap.put("8", 8);// 终端主电源掉电
		alarmMap.put("9", 9);// 终端显示屏故障
		alarmMap.put("10", 2);// 疲劳驾驶
		alarmMap.put("14", 6);// 导航天线短路
		alarmMap.put("16", 10);// 语音模块故障
		alarmMap.put("17", 11);// 摄像头故障
		alarmMap.put("18", 18);// 当天累计驾驶超时
		alarmMap.put("19", 21);// 进出路线
		alarmMap.put("23", 29);//
		alarmMap.put("24", 24);//
		alarmMap.put("25", 25);//
		alarmMap.put("26", 26);//
		alarmMap.put("27", 22);//
		alarmMap.put("28", 28);//
		alarmMap.put("31", 27);//
		alarmMap.put("32", 50);//
		alarmMap.put("34", 51);//
		alarmMap.put("35", 52);//
		alarmMap.put("36", 53);//
		alarmMap.put("41", 1);//
		alarmMap.put("42", 19);//
		alarmMap.put("44", 32);// k
		alarmMap.put("45", 33);//
		alarmMap.put("46", 34);//
		alarmMap.put("47", 35);//
		alarmMap.put("48", 36);//
		alarmMap.put("49", 37);//
		alarmMap.put("50", 38);//
		alarmMap.put("51", 39);//
		alarmMap.put("52", 40);//
		alarmMap.put("53", 41);//
		alarmMap.put("54", 42);//
		alarmMap.put("55", 43);//
		alarmMap.put("56", 44);//
		alarmMap.put("57", 45);//
		alarmMap.put("58", 46);//
		alarmMap.put("59", 47);//
		alarmMap.put("60", 48);//
		alarmMap.put("61", 49);//
		alarmMap.put("200", 20);//
		alarmMap.put("210", 23);//

		baseStatusMap.put("2", 0);
		baseStatusMap.put("4", 1);
		baseStatusMap.put("6", 2);
		baseStatusMap.put("8", 3);
		baseStatusMap.put("10", 4);
		baseStatusMap.put("12", 5);
		baseStatusMap.put("14", 10);
		baseStatusMap.put("16", 11);
		baseStatusMap.put("18", 12);

		baseStatusMap.put("20", 13);
		baseStatusMap.put("22", 14);
		baseStatusMap.put("24", 15);
		baseStatusMap.put("26", 16);

		extendStatusMap.put("1", 0);
		extendStatusMap.put("3", 1);
		extendStatusMap.put("5", 2);
		extendStatusMap.put("7", 3);
		extendStatusMap.put("9", 4);
		extendStatusMap.put("11", 5);
		extendStatusMap.put("13", 6);
		extendStatusMap.put("15", 7);
		extendStatusMap.put("17", 8);
		extendStatusMap.put("19", 9);
		extendStatusMap.put("21", 10);
		extendStatusMap.put("23", 11);
		extendStatusMap.put("25", 12);
		extendStatusMap.put("27", 13);
		extendStatusMap.put("29", 14);

	}
}
