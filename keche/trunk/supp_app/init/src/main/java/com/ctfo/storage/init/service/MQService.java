package com.ctfo.storage.init.service;

import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.activemq.command.ActiveMQQueue;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.init.dao.MQDataSource;

/**
 * MqService
 * 
 * 
 * @author huangjincheng
 * 2014-5-22上午10:43:16
 * 
 */
public class MQService {
	private static Logger log = LoggerFactory.getLogger(MQService.class);
	private Session session ;
	
	public MQService(){
		session = MQDataSource.getSession();
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
				 msg.setText(message); 
				 producer.send(msg); 
			 }		
		 } catch (JMSException ex) {
			 log.debug("MQ发送消息异常："+ex.getMessage());
		 } finally{
			 try {
				if(producer!=null){
					producer.close();
				}
				
			} catch (JMSException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
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
