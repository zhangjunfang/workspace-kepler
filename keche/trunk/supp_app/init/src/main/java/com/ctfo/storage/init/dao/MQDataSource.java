/**
 * 2014-5-22MQDataSource.java
 */
package com.ctfo.storage.init.dao;

import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.JMSException;
import javax.jms.Session;

import org.apache.activemq.ActiveMQConnectionFactory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


/**
 * MQDataSource
 * 
 * 
 * @author huangjincheng
 * 2014-5-22下午04:32:06
 * 
 */
public class MQDataSource {
	private static final Logger logger = LoggerFactory.getLogger(MQDataSource.class);

	private static MQDataSource mqDataSource = new MQDataSource();
	private static String url ;
	private static String mqName;
	private static ConnectionFactory cf;
	
	public static MQDataSource getInstance(){
		if(mqDataSource == null){
			mqDataSource = new MQDataSource();
		}
		return mqDataSource;
	}
	public void init(){
		 cf = new ActiveMQConnectionFactory(url);
	}
	
	public Connection getConnection(){
		if(cf != null){
			try {
				return cf.createConnection();
			} catch (JMSException e) {
				logger.debug("创建MQ Connection连接失败"+e.getMessage());
			}
		}
		return null;
	}
	
	public static Session getSession(){
		if(cf != null){
			try {
				return mqDataSource.getConnection().createSession(false, Session.AUTO_ACKNOWLEDGE);
			} catch (JMSException e) {
				logger.debug("创建MQ Session连接失败"+e.getMessage());
			}
		}
		return null;
	}
	/**
	 * 获取mqName的值
	 * @return mqName  
	 */
	public static String getMqName() {
		return mqName;
	}
	/**
	 * 设置mqName的值
	 * @param mqName
	 */
	public void setMqName(String mqName) {
		MQDataSource.mqName = mqName;
	}
	/**
	 * 获取url的值
	 * @return url  
	 */
	public static String getUrl() {
		return url;
	}
	/**
	 * 设置url的值
	 * @param url
	 */
	public void setUrl(String url) {
		MQDataSource.url = url;
	}
	
	
}
