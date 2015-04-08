package com.ctfo.analy.activemq;


import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.JMSException;

import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.apache.log4j.Logger;

import com.ctfo.analy.DataAnalyMain;




public abstract class CreateActiveMQ extends Thread {
	
	private static final Logger logger = Logger.getLogger(CreateActiveMQ.class);
	private ConnectionFactory connectionFactory = null;
	public Connection connection = null;
	public String mqUrl = null;

	
	/***
	 * 创建 ACTIVEMQ Connection
	 * @throws JMSException 
	 */
	public void createConnectActiveMQ() throws JMSException{
		connectionFactory = new ActiveMQConnectionFactory(ActiveMQConnection.DEFAULT_USER, ActiveMQConnection.DEFAULT_PASSWORD,mqUrl );
		connection = connectionFactory.createConnection();
	}
	
	public void reconnectActiveMQ(){
		try{
			sleep(10000);//每10秒重连一次
			createConnectActiveMQ();
		}catch(Exception ex){
			logger.debug("重连activeMQ服务器失败！");
			reconnectActiveMQ();
		}
	}
	public abstract void createQueueMQ();
	
	public abstract void interrupt();
}
