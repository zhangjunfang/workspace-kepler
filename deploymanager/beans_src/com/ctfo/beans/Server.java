package com.ctfo.beans;

public class Server {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5207267911718976666L;

	private long sid;
	
	private String serverName;
	
	private String sshIp;
	
	private String sshPort;
	
	private String sshUsername;
	
	private String sshUserpwd;
	
	private String remark;
	
	private String pid;
	
	private String platName;

	public long getSid() {
		return sid;
	}

	public void setSid(long sid) {
		this.sid = sid;
	}

	public String getServerName() {
		return serverName;
	}

	public void setServerName(String serverName) {
		this.serverName = serverName;
	}

	public String getSshIp() {
		return sshIp;
	}

	public void setSshIp(String sshIp) {
		this.sshIp = sshIp;
	}

	public String getSshPort() {
		return sshPort;
	}

	public void setSshPort(String sshPort) {
		this.sshPort = sshPort;
	}

	public String getSshUsername() {
		return sshUsername;
	}

	public void setSshUsername(String sshUsername) {
		this.sshUsername = sshUsername;
	}

	public String getSshUserpwd() {
		return sshUserpwd;
	}

	public void setSshUserpwd(String sshUserpwd) {
		this.sshUserpwd = sshUserpwd;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public String getPid() {
		return pid;
	}

	public void setPid(String pid) {
		this.pid = pid;
	}

	public String getPlatName() {
		return platName;
	}

	public void setPlatName(String platName) {
		this.platName = platName;
	}
}