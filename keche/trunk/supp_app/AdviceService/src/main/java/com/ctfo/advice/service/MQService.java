package com.ctfo.advice.service;

import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.activemq.command.ActiveMQQueue;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.dao.MQDataSource;

/**
 * 文件名：MQService.java
 * 功能：
 *
 * @author hjc
 * 2014-8-12下午3:54:42
 * 
 */
public class MQService {
	private static Logger log = LoggerFactory.getLogger(MQService.class);
	private Session session ;
	private OracleService oracleService;
	
	public MQService(){
		session = MQDataSource.getSession();
		oracleService = new OracleService();
	}
	/**
	 * 
	 * 发送消息至MQ
	 * @param message
	 */
	public void send(String message,String qName){
		MessageProducer producer =null;
		 try {
			 Destination destination = new ActiveMQQueue(qName); 
			 producer = session.createProducer(destination); 
			 TextMessage msg = session.createTextMessage();
			 if(!message.equals(null)){
				 log.info("从mq中获取到的消息 : " + message);
				 String processMessage = oracleService.processMessage(message);
				 log.info("服务处理的结果 : " + processMessage);
				 msg.setText(processMessage); 
				 producer.send(msg); 
			 }		
		 } catch (JMSException ex) {
			 log.debug("MQ发送消息异常："+ex.getMessage());
		 } catch (Exception e) {
			 log.error(e.getMessage(),e);
		} finally{
			 try {
				if(producer!=null){
					producer.close();
				}
				
			} catch (JMSException e) {
				log.debug("MQ消息器关闭异常："+e.getMessage());
			}
		 }
	}
	/**
	 * 获取session的值
	 * @return session  
	 */
	public Session getSession() {
		return session;
	}
	/**
	 * 设置session的值
	 * @param session
	 */
	public void setSession(Session session) {
		this.session = session;
	}
	
	
	
}
