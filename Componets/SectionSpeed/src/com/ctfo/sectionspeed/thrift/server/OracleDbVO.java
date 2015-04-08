package com.ctfo.sectionspeed.thrift.server;

import java.util.Properties;

/**
 * 数据库配置VO对象，每项配置见system.properties
 * @author jiangzhongming
 * @version 1.1 2012-6-26
 */
public class OracleDbVO {
	private String ip;
	private String uid;
	private String user;
	private String pwd;
	private int port;

	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}

	public String getUid() {
		return uid;
	}

	public void setUid(String uid) {
		this.uid = uid;
	}

	public String getUser() {
		return user;
	}

	public void setUser(String user) {
		this.user = user;
	}

	public String getPwd() {
		return pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}

	public int getPort() {
		return port;
	}

	public void setPort(int port) {
		this.port = port;
	}

	@Override
	public String toString() {
		return "OracleDbVO [ip=" + ip + ", port=" + port + ", pwd=" + pwd + ", uid=" + uid + ", user=" + user + "]";
	}
	
	public OracleDbVO makeSelf(final Properties p) {
		this.ip = p.getProperty("DB_ip");
		this.user = p.getProperty("DB_user");
		this.pwd = p.getProperty("DB_pwd");
		this.uid = p.getProperty("DB_uid");
		this.port = Integer.valueOf(p.getProperty("DB_port", "1521").trim());
		
		return this;
	}

}
