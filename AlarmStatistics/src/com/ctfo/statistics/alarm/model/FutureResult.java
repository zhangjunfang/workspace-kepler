package com.ctfo.statistics.alarm.model;

public class FutureResult {
	/**	线程名称	 */
	private String name;
	/**	文件数量	 */
	private int fileSize;
	/**	耗时-单位:毫秒	 */
	private long consuming;
	/**
	 * 获取[线程名称]值
	 */
	public String getName() {
		return name;
	}
	/**
	 * 设置[线程名称] 值
	 */
	public void setName(String name) {
		this.name = name;
	}
	/**
	 * 获取[文件数量]值
	 */
	public int getFileSize() {
		return fileSize;
	}
	/**
	 * 设置[文件数量] 值
	 */
	public void setFileSize(int fileSize) {
		this.fileSize = fileSize;
	}
	/**
	 * 获取[耗时-单位:毫秒]值
	 */
	public long getConsuming() {
		return consuming;
	}
	/**
	 * 设置[耗时-单位:毫秒] 值
	 */
	public void setConsuming(long consuming) {
		this.consuming = consuming;
	}
	
	
}
