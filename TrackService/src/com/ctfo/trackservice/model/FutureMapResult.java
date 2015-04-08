package com.ctfo.trackservice.model;

import java.util.Map;

public class FutureMapResult {
	/**	名称	*/
	private String name;
	/**	结果值	*/
	private Map<String, ?> value;
	/**
	 * @return the 名称
	 */
	public String getName() {
		return name;
	}
	/**
	 * @param 名称 the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}
	/**
	 * @return the 结果值
	 */
	public Map<String, ?> getValue() {
		return value;
	}
	/**
	 * @param 结果值 the value to set
	 */
	public void setValue(Map<String, ?> value) {
		this.value = value;
	}
	
}
