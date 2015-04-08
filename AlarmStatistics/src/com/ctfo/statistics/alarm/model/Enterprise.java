package com.ctfo.statistics.alarm.model;

import java.util.ArrayList;
import java.util.List;

public class Enterprise {
	/**	组织编号	*/
	private String entId;
	/**	父组织编号	*/
	private String parentId;
	/**	子企业	*/
	private List<Enterprise> child;
	/** 告警配置列表	*/
	private List<AlarmConf> alarmConf;
	
	public Enterprise(String _entId, String _parentId) {
		this.entId = _entId;
		this.parentId = _parentId;
		this.child = new ArrayList<Enterprise>();
	}
	/**
	 * 获取[组织编号]值
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置[组织编号] 值
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * 获取[父组织编号]值
	 */
	public String getParentId() {
		return parentId;
	}
	/**
	 * 设置[父组织编号] 值
	 */
	public void setParentId(String parentId) {
		this.parentId = parentId;
	}
	/**
	 * 获取[子企业]值
	 */
	public List<Enterprise> getChild() {
		return child;
	}
	/**
	 * 设置[子企业] 值
	 */
	public void setChild(List<Enterprise> child) {
		this.child = child;
	}
	/**
	 * 获取[告警配置列表]值
	 */
	public List<AlarmConf> getAlarmConf() {
		return alarmConf;
	}
	/**
	 * 设置[告警配置列表] 值
	 */
	public void setAlarmConf(List<AlarmConf> alarmConf) {
		this.alarmConf = alarmConf;
	}
	
}
