package com.ctfo.monitoring.beans;

import java.util.Map;

public class MonitoringData {

	/** 唯一标识 */
	private String key;

	/** 服务名称 */
	private String serviceName;

	/** 方法名称 */
	private String methodName;

	/** 输入参数 */
	private Map<String, String> inputMap;

	/** 输出参数 */
	private Map<String, String> outputMap;

	public String getKey() {
		return key;
	}

	public void setKey(String key) {
		this.key = key;
	}

	public String getServiceName() {
		return serviceName;
	}

	public void setServiceName(String serviceName) {
		this.serviceName = serviceName;
	}

	public String getMethodName() {
		return methodName;
	}

	public void setMethodName(String methodName) {
		this.methodName = methodName;
	}

	public Map<String, String> getInputMap() {
		return inputMap;
	}

	public void setInputMap(Map<String, String> inputMap) {
		this.inputMap = inputMap;
	}

	public Map<String, String> getOutputMap() {
		return outputMap;
	}

	public void setOutputMap(Map<String, String> outputMap) {
		this.outputMap = outputMap;
	}
}
