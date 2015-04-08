package com.ctfo.analy.activemq;

import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageConsumer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.log4j.Logger;

import com.ctfo.analy.dao.CacheUpdateDBAdapter;
import com.lingtu.xmlconf.XmlConf;

/*****************************************
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： com.ctfo.analy.activemq		</li><br>
 * <li>文件名称：TerminalActiveMQ.java </li><br>
 * <li>时        间：2013-5-9  上午10:07:26	</li><br>
 * <li>描        述：终端频道			</li><br>
 *****************************************/
public class TerminalActiveMQ extends CreateActiveMQ {
	private static final Logger logger = Logger.getLogger(TerminalActiveMQ.class);

	private String queueName = null;

	private Session session = null;

	private CacheUpdateDBAdapter cacheUpdateDBAdapter;
			
	public TerminalActiveMQ(String mqUrl, String queueName, XmlConf config) {
		super.mqUrl = mqUrl;
		this.queueName = queueName;
		cacheUpdateDBAdapter = new CacheUpdateDBAdapter();
		try {
			cacheUpdateDBAdapter.initCacheUpdateDBAdapter(config);
		} catch (Exception e) {
			logger.error("消息总线启动 - 创建数据库连接池异常",e);
		}
	}

	@Override
	/**
	 * 
	 */
	public void createQueueMQ() {
		try {
			logger.info("["+queueName+"服务] - [消息总线] - 启动 ...");
			//消息的目的地;消息发送给谁.
			Destination destination;
			// 消费者，消息接收者
			MessageConsumer consumer;
			super.createConnectActiveMQ();
			super.connection.start();
			// 获取操作连接
			session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
			// 获取session
			destination = session.createTopic(this.queueName);
			consumer = session.createConsumer(destination);
			
			while (true) {
				TextMessage message = null;
				try {
					// 设置接收者接收消息的时间./
					message = (TextMessage) consumer.receive(100000);
					if (null != message) {
						logger.info("activemq收到基础数据更新通知消息：" + message.getText());
						processMsg(message.getText());
					}
				} catch (Exception ex) {
					logger.error("activemq接收、处理基础数据更新通知消息异常:" + ex);
					destination = null;
					consumer.close();
					if (session!=null){
						session = null;
					}
					//session.close();
					//connection.close();

					// 从新获取操作连接
					super.reconnectActiveMQ();
					super.connection.start();
					session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

					// 获取session
					destination = session.createTopic(this.queueName);
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
			try {
				this.sleep(3*60*1000);
			} catch (InterruptedException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
			this.createQueueMQ();
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
			String name = "TT.TID";
			String value = (msgStr[2].split(":"))[1];
			//通知与终端相关的缓存更新
			if(type.equals("ADD")){
				//终端报警设置
				cacheUpdateDBAdapter.vehicleAlarmUpdate(name, value);
			//批量导入  全量导入通知与终端相关的缓存更新 
			}else if (type.equals("BULKADD")){
				cacheUpdateDBAdapter.vehicleAlarmBlukAdd();		
			//通知与终端相关的缓存更新 
			}else if (type.equals("UPDATA")){
				cacheUpdateDBAdapter.vehicleAlarmUpdate(name, value);
			//单条删除
			}else if (type.equals("DELETE")){
				cacheUpdateDBAdapter.vehicleAlarmBlukAdd();		
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

//	public static void main(String[] args) {
//		AlarmActiveMQ alarm = new AlarmActiveMQ("tcp://192.168.100.50:61616",
//				"alarmSettingQueue");
//		alarm.createQueueMQ();
//		System.out.println("TYPE:a;NAME:b;VALUE:c".split(";")[1]);
//	}

	@Override
	public void interrupt() {
		try {
			if (null != session) {
				session.close();
			}
			super.connection.stop();
			super.connection.close();
		} catch (JMSException e) {
			logger.error("关闭终端信息总线线程异常:", e);
		}
	}

}
