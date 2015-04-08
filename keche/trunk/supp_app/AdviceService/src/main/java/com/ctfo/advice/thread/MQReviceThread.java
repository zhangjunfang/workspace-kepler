package com.ctfo.advice.thread;

import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageConsumer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.dao.MQDataSource;


/**
 * 文件名：MQSendThread.java
 * 功能：mq消息接收线程
 *
 * @author hjc
 * 2014-8-12下午3:46:34
 * 
 */
public class MQReviceThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(MQReviceThread.class);
	/**	频道名称	*/
	private String topicName;
	/**	连接通道	*/
	private Session session = null;
	/**	连接工厂	*/
	private ConnectionFactory connectionFactory = null;
	/**	连接	*/
	public Connection connection = null;
	/**	监听路径	*/
	public String brokerURL;

	/**	消息来源	*/
	private Destination destination;
	/**	消息接收对象	*/
	private MessageConsumer consumer;
	
	private MQSendThread mqSendThread;
	
	public MQReviceThread() {
		setName("MQReviceThread");
		topicName = MQDataSource.getInstance().getMqName();
		mqSendThread = new MQSendThread();
		mqSendThread.start();
	}

	public void run(){
		logger.info("--MQReviceThread-- MQ消息接收线程启动！");
		try {
			logger.info("["+topicName+"服务] - [消息总线] - 启动 ......");
			//消息的目的地;消息发送给谁.
			// 消费者，消息接收者
//			创建连接工厂和连接
			//createConnectActiveMQ();
			connection = MQDataSource.getInstance().getConnection();
//			启动连接
			connection.start();
			// 初始化连接对象
			session = this.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
			destination = session.createTopic(topicName);
			consumer = session.createConsumer(destination);
//			ActiveMQ.Advisory.Consumer.Topic.t_org
			while (true) {
				TextMessage message = null;
				try {
					// 设置接收者接收消息的等待时间./
					message = (TextMessage) consumer.receive(300000);
					if (null != message) {
						logger.info("ActiveMQ RECEIVED：" + message.getText());
						processMsg(message.getText());
					}
				} catch (Exception ex) {
					logger.error("ActiveMQ Process Error (消息处理异常)! " + ex.getMessage(), ex);
					closeResources(consumer, destination, session);
					// 重连
					reconnectActiveMQ();
					connection.start();
					session = this.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
					// 获取session
					destination = session.createTopic(this.topicName);
					consumer = session.createConsumer(destination);
					logger.info("ActiveMQ Reconnect Success(重连成功)!"); 
				} finally {
					if (null != message) {
						message.clearBody();
						message = null;
					}
				}
			}
		} catch (JMSException e) {
			logger.error("ActiveMQ Thread Running ERROR (线程运行异常)! ：" + e.getMessage(), e); 
		}
	}
	/**
	 * 关闭连接资源
	 * @param consumer_
	 * @param destination_
	 * @param session_
	 */
	private void closeResources(MessageConsumer consumer_, Destination destination_, Session session_) {
		try {
			if (consumer_ != null) {
				consumer_.close();
			}
			if (destination_ != null) {
				destination_ = null;
			}
			if (session_ != null) {
				session_.close();
			}
		} catch (JMSException e) {
			logger.error("ActiveMQ 关闭连接异常:" + e.getMessage(), e);
		}
	}

	
	/**
	 * 处理消息
	 * @param msg
	 */
	private void processMsg(String msg) {
		mqSendThread.put(msg);

	}


	public void interrupt() {
		try {
			if (null != session) {
				session.close();
			}
			this.connection.stop();
			this.connection.close();
		} catch (JMSException e) {
			logger.error("关闭组织信息总线线程异常:", e);
		}
	}
	
	/***
	 * 创建 ACTIVEMQ Connection
	 * @throws JMSException 
	 */
	public void createConnectActiveMQ() throws JMSException{
		if(connection != null){
			connection.close();
		}
		if(connectionFactory != null){
			connectionFactory = null;
		}
		connectionFactory = new ActiveMQConnectionFactory(ActiveMQConnection.DEFAULT_USER, ActiveMQConnection.DEFAULT_PASSWORD,brokerURL);
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
	public String getBrokerURL() {
		return brokerURL;
	}
	public void setBrokerURL(String brokerURL) {
		this.brokerURL = brokerURL;
	}

	public String getTopicName() {
		return topicName;
	}
	public void setTopicName(String topicName) {
		this.topicName = topicName;
	}
	
	public static void main(String[] args) {

	}
	
	
}
