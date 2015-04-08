package com.ctfo.statistics.alarm.model;

import java.util.ArrayList;
import java.util.List;

import com.ctfo.statistics.alarm.common.Utils;

public class AlarmConf {
	/** 告警级别	*/
	private String level;
	/** 告警列表	*/
	private List<String> alarmCode = new ArrayList<String>();
	
	public AlarmConf(String level, String codeStr){
		String[] codes = Utils.split(codeStr, ",");
		if(codes != null && codes.length >0){
			for(String code : codes){
				alarmCode.add(code);
			}
		}
		this.level = level;
	}
	/**
	 * 获取[告警级别]值
	 */
	public String getLevel() {
		return level;
	}
	/**
	 * 设置[告警级别] 值
	 */
	public void setLevel(String level) {
		this.level = level;
	}
	/**
	 * 获取[告警列表]值
	 */
	public List<String> getAlarmCode() {
		return alarmCode;
	}
	/**
	 * 设置[告警列表] 值
	 */
	public void setAlarmCode(List<String> alarmCode) {
		this.alarmCode = alarmCode;
	}
	
}
