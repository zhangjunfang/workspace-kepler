package com.ctfo.statusservice.task;

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

import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.service.OracleJdbcService;



/*****************************************
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： com.ctfo.analy		</li><br>
 * <li>文件名称：OrgActiveMQ.java </li><br>
 * <li>时        间：2013-5-9  上午9:59:34	</li><br>
 * <li>描        述：组织信息总线			</li><br>
 *****************************************/
public class OrgActiveMQ extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(OrgActiveMQ.class);
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
	/**	数据库接口	*/
	private OracleJdbcService oracleJdbcService;
	/**	数据库连接配置	*/
	private OracleProperties oracleProperties;
	/**	消息来源	*/
	private Destination destination;
	/**	消息接收对象	*/
	private MessageConsumer consumer;
	
	public OrgActiveMQ() {
		setName("OrgActiveMQ");
	}

	public void run(){
		logger.info(topicName + "启动数据库连接接口");
		oracleJdbcService = new OracleJdbcService(oracleProperties);
		try {
			logger.info("["+topicName+"服务] - [消息总线] - 启动 ...");
			//消息的目的地;消息发送给谁.
			// 消费者，消息接收者
//			创建连接工厂和连接
			createConnectActiveMQ();
//			启动连接
			connection.start();
			// 初始化连接对象
			session = this.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
			destination = session.createTopic(this.topicName);
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

	/*****************************************
	 * <li>描       述： 处理接收到的基础数据通知消息		</li><br>
	 * <li>参        数：@param msg	 		消息体	</li><br>
	 *****************************************/
	private void processMsg(String msg) {
		String[] msgStr = msg.split(";");
		if(msgStr.length == 3){
			oracleJdbcService.orgParentSync();
			logger.info("ActiveMQ Process Message[{}] Success (车队父级列表更新完成)!", msg);
		}else{
			logger.error("ActiveMQ Data Format Error(数据格式错误):{}", msg);
		}

	}


	@Override
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
		connectionFactory = new ActiveMQConnectionFactory(ActiveMQConnection.DEFAULT_USER, ActiveMQConnection.DEFAULT_PASSWORD,brokerURL );
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
	public OracleProperties getOracleProperties() {
		return oracleProperties;
	}
	public void setOracleProperties(OracleProperties oracleProperties) {
		this.oracleProperties = oracleProperties;
	}
	public String getTopicName() {
		return topicName;
	}
	public void setTopicName(String topicName) {
		this.topicName = topicName;
	}
	
	public static void main(String[] args) {
		OrgActiveMQ oa = new OrgActiveMQ();
		oa.setBrokerURL("tcp://192.168.100.52:61616");
		OracleProperties oracleProperties = new OracleProperties();
		oracleProperties.setSql_orgParentSync("SELECT ORG.ENT_ID AS MOTORCADE,PAR.ENT_NAME,  ',' || (SELECT WM_CONCAT(T.ENT_ID) FROM KCPT.TB_ORGANIZATION T WHERE T.ENABLE_FLAG = 1 AND T.ENT_TYPE = 1  START WITH T.ENT_ID = ORG.PARENT_ID CONNECT BY PRIOR T.PARENT_ID = T.ENT_ID) || ',' PARENT_ID  FROM KCPT.TB_ORGANIZATION ORG,  KCPT.TB_ORGANIZATION PAR WHERE ORG.PARENT_ID=PAR.ENT_ID  AND ORG.ENABLE_FLAG = 1 AND ORG.ENT_TYPE = 2"); 
		oa.setOracleProperties(oracleProperties);
		oa.setTopicName("t_org");
		oa.start();
	}
}
