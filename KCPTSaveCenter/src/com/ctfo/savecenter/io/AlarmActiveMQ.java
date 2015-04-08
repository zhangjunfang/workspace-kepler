//package com.ctfo.savecenter.io;
//
//import java.util.ArrayList;
//import java.util.List;
//import java.util.regex.Matcher;
//import java.util.regex.Pattern;
//
//import javax.jms.Destination;
//import javax.jms.JMSException;
//import javax.jms.MessageConsumer;
//import javax.jms.Session;
//import javax.jms.TextMessage;
//
//import org.slf4j.Logger;
//import org.slf4j.LoggerFactory;
//
//import com.ctfo.savecenter.dao.MonitorDBAdapter;
//import com.ctfo.savecenter.dao.TempMemory;
//
//
//public class AlarmActiveMQ extends CreateActiveMQ {
//	private static final Logger logger = LoggerFactory.getLogger(AlarmActiveMQ.class);
//	
//	private String queueName = null;
//	
//	// 判断企业正则表达式
//	private final String corpPattern = "TYPE:1;ENT_ID:(\\d+);ACODE:(.*)";
//	
//	// 判断车辆正则表达式
//	private final String vidPattern = "TYPE:2;VID:(\\d+);ENT_ID:(\\d+)";
//	
//	private Session session = null;
//	
//	public AlarmActiveMQ(String mqUrl,String queueName){
//		super.mqUrl = mqUrl;
//		this.queueName = queueName;
//	}
//	
//	@Override
//	public void createQueueMQ() {
//		try {
//			// Destination ：消息的目的地;消息发送给谁.
//			Destination destination;
//			// 消费者，消息接收者
//			MessageConsumer consumer;
//			super.createConnectActiveMQ();
//			super.connection.start();
//			// 获取操作连接
//			session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
//			
//			// 获取session
//			destination = session.createTopic(this.queueName);
//			consumer = session.createConsumer(destination);
//			
//			while(true){
//				TextMessage message = null;
//				try{
//					logger.info("At the beginning for getting msg");
//					// 设置接收者接收消息的时间.
//					message = (TextMessage) consumer.receive(100000);
//					if(null != message){
//						logger.info("接受到告警设置处理信息：" + message.getText());
//						processMsg(message.getText());
//					}
//				}catch(Exception ex){
//					logger.error("Parsing message to error." + ex.getMessage());
//					destination = null;
//					consumer.close();
//					session.close();
//					connection.close();
//					
//					// 从新获取操作连接
//					super.createConnectActiveMQ();
//					super.connection.start();
//					session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
//					
//					// 获取session
//					destination = session.createQueue(this.queueName);
//					consumer = session.createConsumer(destination);
//				}finally{
//					if(null != message){
//						message.clearBody();
//						message = null;
//					}
//				}
//			}// End while
//			
//		} catch (JMSException e) {
//			logger.error("Connect Active MQ to error.",e);
//		}
//	}
//	
//	/*****
//	 * 处理报警设置信息
//	 * @param msg
//	 */
//	private void processMsg(String msg){
//		if(msg.matches(corpPattern)){ //企业修改报警设置信息时
//			Pattern pt = Pattern.compile(corpPattern);
//			Matcher mt = pt.matcher(msg);
//			if(mt.find()){
//				
//				String ent_id = mt.group(1);
//				String alarmCode = mt.group(2);
//				
//				String[] alarms = alarmCode.split(",");
//				List<String> alarmList = new ArrayList<String>();
//				for(String a : alarms){
//					if(a.matches("\\d+")){
//						alarmList.add(a);
//					}
//				}// End for
//				
//				// 企业报警设置列表
//				if(alarmList.size() > 0){
//					TempMemory.entAlarmMap.put(Long.parseLong(ent_id), alarmList);
//					// 企业对应车辆列表
//					MonitorDBAdapter.getListByCorp(Long.parseLong(ent_id));
//				}
//				mt = null;
//				pt = null;
//			}
//		}else if(msg.matches(vidPattern)){ // 添加新车辆
//			Pattern pt = Pattern.compile(vidPattern);
//			Matcher mt = pt.matcher(msg);
//			if(mt.find()){
//				String vid = mt.group(1);
//				String ent_id = mt.group(2);
//				if(TempMemory.entAlarmMap.containsKey(Long.parseLong(ent_id))) // 该企业对应有报警设置，则增加企业对应该车辆，否则使用默认告警设置
//				TempMemory.vidEntMap.put(Long.parseLong(vid), Long.parseLong(ent_id));
//			}
//		}
//	}
//	
//	@Override
//	public void run() {
//		createQueueMQ();
//	}
//	
//	public static void main(String[] args){
//		AlarmActiveMQ alarm = new AlarmActiveMQ("tcp://192.168.100.50:61616","alarmSettingQueue");
//		alarm.createQueueMQ();
//	}
//
//	@Override
//	public void interrupt() {
//		try {
//			if(null != session){
//				session.close();
//			}
//			super.connection.stop();
//			super.connection.close();
//		} catch (JMSException e) {
//			logger.error("Close active MQ connection.",e);
//		}
//	}
//
//}
