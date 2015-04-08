package com.ctfo.monitoring.beans;

import com.ctfo.local.obj.DynamicSqlParameter;

public class MonitoringDataParameter {

	/** ID */
	private String id;

	/** 服务名称 */
	private String serviceName;

	/** 方法名称 */
	private String methodName;

	/** 动态参数对象 */
	private DynamicSqlParameter parameter;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
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

	public DynamicSqlParameter getParameter() {
		return parameter;
	}

	public void setParameter(DynamicSqlParameter parameter) {
		this.parameter = parameter;
	}
}
