package com.ctfo.mgdb.beans;

import org.apache.log4j.Logger;
import com.ctfo.mgdb.util.XmlConfUtil;



/**
 * @author huangjincheng
 *
 */
public class Mongodb {
	private static final Logger log = Logger.getLogger(Mongodb.class);
	public  String ip ;
	public  int port;
	public  String dbname;
	public  String collection;
	public  XmlConfUtil config ;
	public Mongodb(){
		this.config = new XmlConfUtil();
		try {
			this.ip   = this.config.getStringValue("mongoServiceManage|MongoIp");
			this.port = Integer.parseInt(this.config.getStringValue("mongoServiceManage|MongoPort"));
			this.dbname = this.config.getStringValue("mongoServiceManage|MongoDbname");
			this.collection = this.config.getStringValue("mongoServiceManage|MongoCollection");
		} catch (Exception e) {
			log.debug("解析mongoDB配置文件错误！"+e.getMessage());
		
		}
	}
	public String getIp() {
		return ip;
	}
	public void setIp(String ip) {
		this.ip = ip;
	}
	public int getPort() {
		return port;
	}
	public void setPort(int port) {
		this.port = port;
	}
	public String getDbname() {
		return dbname;
	}
	public void setDbname(String dbname) {
		this.dbname = dbname;
	}
	public String getCollection() {
		return collection;
	}
	public void setCollection(String collection) {
		this.collection = collection;
	}
	public XmlConfUtil getConfig() {
		return config;
	}
	public void setConfig(XmlConfUtil config) {
		this.config = config;
	}
	
	

}
