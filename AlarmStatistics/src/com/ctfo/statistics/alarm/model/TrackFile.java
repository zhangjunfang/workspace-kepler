package com.ctfo.statistics.alarm.model;

import java.util.List;
/**
 * 轨迹文件
 * @author zjhl
 *
 */
public class TrackFile {
	/**	vid	*/
	private String name;
	/**	内容列表	*/
	private List<String> list;
	/**
	 * @return the vid
	 */
	public String getName() {
		return name;
	}
	/**
	 * @param vid the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}
	/**
	 * @return the 内容列表
	 */
	public List<String> getList() {
		return list;
	}
	/**
	 * @param 内容列表 the list to set
	 */
	public void setList(List<String> list) {
		this.list = list;
	}
	
}
