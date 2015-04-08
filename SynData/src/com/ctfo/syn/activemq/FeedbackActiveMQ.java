package com.ctfo.syn.activemq;

import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageConsumer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.log4j.Logger;

import com.ctfo.syn.dao.CacheUpdateDBAdapter;

/*****************************************
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： com.ctfo.analy.activemq		</li><br>
 * <li>文件名称：VehicleAreaActiveMQ.java </li><br>
 * <li>时        间：2013-5-9  下午12:53:31	</li><br>
 * <li>描        述：车辆区域频道监听			</li><br>
 *****************************************/
public class FeedbackActiveMQ extends CreateActiveMQ {
	private static final Logger logger = Logger.getLogger(FeedbackActiveMQ.class);

	private String topicName = null;

	private Session session = null;

	private CacheUpdateDBAdapter cacheUpdateDBAdapter;
			
	public FeedbackActiveMQ(String mqUrl, String topicName) {
		super.mqUrl = mqUrl;
		this.topicName = topicName;
		cacheUpdateDBAdapter = new CacheUpdateDBAdapter();
	}

	@Override
	/**
	 * 
	 */
	public void createQueueMQ() {
		try {
			logger.info("["+topicName+"服务] - [消息总线] - 启动 ...");
			//消息的目的地;消息发送给谁.
			Destination destination;
			// 消费者，消息接收者
			MessageConsumer consumer;
			super.createConnectActiveMQ();
			super.connection.start();
			// 获取操作连接
			session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
			// 获取session
			destination = session.createTopic(this.topicName);
			consumer = session.createConsumer(destination);
			
			while (true) {
				TextMessage message = null;
				try {
					// 设置接收者接收消息的时间./
					message = (TextMessage) consumer.receive(100000);
					if (null != message) {
						logger.debug("activemq收到基础数据更新通知消息：" + message.getText());
						processMsg(message.getText());
					}
				} catch (Exception ex) {
					logger.error("activemq接收、处理基础数据更新通知消息异常:" + ex);
					destination = null;
					consumer.close();
					session.close();
					connection.close();

					// 从新获取操作连接
					super.createConnectActiveMQ();
					super.connection.start();
					session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

					// 获取session
					destination = session.createQueue(this.topicName);
					consumer = session.createConsumer(destination);
				} finally {
					if (null != message) {
						message.clearBody();
						message = null;
					}
				}
			}
		} catch (JMSException e) {
			logger.error("activemq连接消息总线异常:", e);
		}
	}

	/*****************************************
	 * <li>描       述： 处理接收到的基础数据通知消息		</li><br>
	 * <li>参        数：@param msg	 		消息体	</li><br>
	 *****************************************/
	private void processMsg(String msg) {
		String[] msgStr = msg.split(";");
		if(msgStr.length == 3){
			String type = (msgStr[0].split(":"))[1];
			String name = "TR.ID";
			String value = (msgStr[2].split(":"))[1];
			//通知与车辆区域相关的缓存更新
			if(type.equals("ADD")){
				//车辆区域报警设置
				cacheUpdateDBAdapter.feedbackUpdate(name, value);		
			//批量导入  全量导入通知与车辆区域相关的缓存更新 
			}else if (type.equals("BULKADD")){
				cacheUpdateDBAdapter.feedbackUpdate(name, value);		
			//通知与车辆区域相关的缓存更新 
			}else if (type.equals("UPDATA")){
				cacheUpdateDBAdapter.feedbackUpdate(name, value);		
			//单条删除
			}else if (type.equals("DELETE")){
//				cacheUpdateDBAdapter.areaAlarmBlukAdd();
			}else{
				logger.debug("activemq基础信息变更消息格式不合法");
			}
		}else{
			logger.debug("activemq基础信息变更消息格式不合法");
		}

	}

	@Override
	public void run() {
		createQueueMQ();
	}

	@Override
	public void interrupt() {
		try {
			if (null != session) {
				session.close();
			}
			super.connection.stop();
			super.connection.close();
		} catch (JMSException e) {
			logger.error("关闭车辆区域信息总线线程异常:", e);
		}
	}
}
