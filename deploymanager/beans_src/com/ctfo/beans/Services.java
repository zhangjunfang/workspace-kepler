package com.ctfo.beans;

public class Services {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5207267911718976666L;

	private Long serviceId;
	
	private String serviceName;
	
	private String serviceType;
	
	private String launchType;
	
	private String launchShell;

	public Long getServiceId() {
		return serviceId;
	}

	public void setServiceId(Long serviceId) {
		this.serviceId = serviceId;
	}

	public String getServiceName() {
		return serviceName;
	}

	public void setServiceName(String serviceName) {
		this.serviceName = serviceName;
	}

	public String getServiceType() {
		return serviceType;
	}

	public void setServiceType(String serviceType) {
		this.serviceType = serviceType;
	}

	public String getLaunchType() {
		return launchType;
	}

	public void setLaunchType(String launchType) {
		this.launchType = launchType;
	}

	public String getLaunchShell() {
		return launchShell;
	}

	public void setLaunchShell(String launchShell) {
		this.launchShell = launchShell;
	}
}