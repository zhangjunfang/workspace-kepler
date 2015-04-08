//package com.ctfo.savecenter.io;
//
//
//import javax.jms.Connection;
//import javax.jms.ConnectionFactory;
//import javax.jms.JMSException;
//
//import org.apache.activemq.ActiveMQConnection;
//import org.apache.activemq.ActiveMQConnectionFactory;
//
//
//
//
//public abstract class CreateActiveMQ extends Thread {
//	
//	private ConnectionFactory connectionFactory = null;
//	public Connection connection = null;
//	public String mqUrl = null;
//
//	
//	/***
//	 * 创建 ACTIVEMQ Connection
//	 * @throws JMSException 
//	 */
//	public void createConnectActiveMQ() throws JMSException{
//		connectionFactory = new ActiveMQConnectionFactory(ActiveMQConnection.DEFAULT_USER, ActiveMQConnection.DEFAULT_PASSWORD,mqUrl );
//		connection = connectionFactory.createConnection();
//	}
//	
//	public abstract void createQueueMQ();
//	
//	public abstract void interrupt();
//}
