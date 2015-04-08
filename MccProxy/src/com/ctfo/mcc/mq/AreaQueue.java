package com.ctfo.mcc.mq;

import java.util.Calendar;
import java.util.Timer;

import javax.jms.Connection;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageConsumer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.mcc.dao.RedisConnectionPool;
import com.ctfo.mcc.service.LinkService;
import com.ctfo.mcc.service.OracleService;
import com.ctfo.mcc.service.RedisAreaUpdateStatus;

//import com.ctfo.manager.redis.ConnectRedis;


public class AreaQueue extends CreateActiveMQ {
	
	protected static Logger logger = LoggerFactory.getLogger(AreaQueue.class);
	
	private Session session = null;
	
	public String mqName = null;

//	public RedisAreaUpdateStatus areaUpdateRedisStatus = null;
	
	// oracle DAO接口
//	public OracleAreaUpdateStatus areaOracleUpdateStatus = null;
	
	// 定时任务
	private TimerMananger timerTask = null;
	
	private Timer timer = null;
	
//	public LinkService linkService = null;
	
//	public ConnectRedis connectRedis = null;
	
	// 标记当前是否有任务执行，false无任务，true有任务在执行
	public static boolean isExecuteTask = false;
	
	public void init(){
		setName("AreaQueue");
		
		// 初始化TIMER
		timer = new Timer();
		startTaskTimer();
		start();
	}
	
	public void run(){
		createQueueMQ();
	}
	
	@Override
	public void createQueueMQ() {
		try {
			// Destination ：消息的目的地;消息发送给谁.
			Destination destination;
			// 消费者，消息接收者
			MessageConsumer consumer;
//			循环连接ActiveMQ,直到成功为止
			for(;;) {
				try {
					super.createConnectActiveMQ();
					super.connection.start();
					session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);// 获取操作连接
					destination = session.createQueue(this.mqName);// 获取session
					consumer = session.createConsumer(destination);
					break;
				} catch (Exception e) {
					logger.error("连接ActiveMQ失败:{}", e.getMessage()); 
					Thread.sleep(10000);
				}
			}
			while(true){
				try{
					logger.info("消息监听启动 - ActiveMQ Listener Starting ...");
					// 设置接收者接收消息的时间.
					TextMessage message = (TextMessage) consumer.receive(60000);
					if(null != message){
						logger.info("收到电子围栏信息 - Received Area：" + message.getText());
						processMsg(message.getText());
					}else{
						// 检测当前是否有TIMER在执行，并且是否有新的任务需要执行.
						if(isExecuteTask){
							logger.info("当前有TIMER在执行。");
						}else{
							logger.info("当前无TIMER在执行。");
							startTaskTimer();
						}
					}
				}catch(Exception ex){
					logger.error("MCC接收队列异常:Process message to error." + ex.getMessage());
//					循环连接ActiveMQ,直到成功为止
					for(;;) {
						try {
							destination = null;
							closeResource(consumer, session, connection);
							super.createConnectActiveMQ();
							super.connection.start();
							session = super.connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
							destination = session.createQueue(this.mqName);
							consumer = session.createConsumer(destination);
							break;
						} catch (Exception e) {
							logger.error("连接ActiveMQ失败:{}", e.getMessage()); 
							Thread.sleep(10000);
						}
					}
				}
			}// End while
		} catch (Exception e) {
			logger.error("ActiveMQ启动异常:Connect ActiveMQ Error!", e);
		}
	}
	
	private void closeResource(MessageConsumer consumer, Session session2, Connection connection) {
		try {
			if (consumer != null) {
				consumer.close();
			}
			if (session != null) {
				session.close();
			}
			if (connection != null) {
				connection.close();
			}
		} catch (JMSException e) {
			logger.error("ActiveMQ关闭资源异常:ActiveMQ Close Resource Error!", e);
		}
	}

	/*****
	 * 处理接收信息
	 * @param msg
	 */
	private void processMsg(String msg){
		String[] infos = msg.split("&");
		if(infos.length == 4){
			String seq = infos[0].split("@")[1];
			String time = infos[1].split("@")[1];
			String command = infos[2].split("@")[1];
			String areaId = infos[3].split("@")[1];
			command = command + "@" + areaId; // 添加电子围栏ID
			RedisAreaUpdateStatus.setAreaCache(seq, time, command);
			startTaskTimer(); // 启动TIMER
		}
	}
	
	/******
	 * 启动TIMER
	 */
	private void startTaskTimer(){
		String[] list = RedisAreaUpdateStatus.getAreaLastInvalidTime();
		
		if(null != list && list.length == 2){
			if(null != timerTask){
				// 停止一项任务
				try{
					timerTask.cancel();
					timerTask = null;
				}catch(IllegalStateException e){
					logger.error("Stop timer");
				}
			}
		
			AreaUpdateServie areaUpdateServie = new AreaUpdateServie(list[1]);
			if(Long.parseLong(list[0]) < System.currentTimeMillis()){ // 如果该任务时间已经过期，则直接执行任务，解除围栏绑定,否则启动任务
				areaUpdateServie.removeAreaBunding();
				areaUpdateServie = null;
			}else{
				timerTask = new TimerMananger(areaUpdateServie);
				//启动新任务
				Calendar c = Calendar.getInstance();
				c.setTimeInMillis(Long.parseLong(list[0]));
				logger.info("Start to task " + list[1] + ";executing time : " + c.getTime().toString());
				timer.schedule(timerTask, c.getTime());
				isExecuteTask = true; // 标记启动一个任务
			}
		}
	}

	@Override
	public void interrupt() {
		// TODO Auto-generated method stub
		
	}

	public String getMqName() {
		return mqName;
	}

	public void setMqName(String mqName) {
		this.mqName = mqName;
	}

}

/******
 * 业务处理解除电子围栏绑定，发送指令
 * @author robin
 *
 */
class AreaUpdateServie{
	private static Logger logger = LoggerFactory.getLogger(AreaUpdateServie.class);
//	private LinkService linkService = null;
	
//	private OracleAreaUpdateStatus areaUpdateStatus = null;
	
//	private ConnectRedis connectRedis = null;
	
	private String seq = null;
	
//	private RedisAreaUpdateStatus areaUpdateRedisStatus = null;
	
	public AreaUpdateServie(){
		
	}
	
	public AreaUpdateServie(String seq){
//		this.areaUpdateStatus = areaUpdateStatus;
//		this.linkService = linkService;
		this.seq = seq;
//		this.connectRedis = connectRedis;
//		this.areaUpdateRedisStatus = areaUpdateRedisStatus;
	}
	
	public void removeAreaBunding(){
		
		try {
			String areaId = null;
			// 下发车机解绑
			Jedis jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);// 库3，存储电子围栏信息
			if(jedis.exists(seq)){
				String[] arr = jedis.get(seq).split("@");
				if(arr.length == 2){
					LinkService.sendMessage(arr[0]);
					areaId = arr[1];
				}
			}
			RedisConnectionPool.returnJedisConnection(jedis);
			// 任务执行完成，清除REDIS缓存
			RedisAreaUpdateStatus.delAreaSeq(seq);
			
			// ORACLE数据库解除绑定电子围栏
//			HashMap<String,String> params = new HashMap<String,String>();
//			params.put("SEQ", this.seq);
//			params.put("SYSUTC", System.currentTimeMillis() + "");
			OracleService.updateBindAreaStatus(seq, System.currentTimeMillis());
			
			// 更新电子围栏表
			if(null != areaId){
//				HashMap<String,String> param = new HashMap<String,String>();
//				param.put("AREAID", areaId);
				OracleService.updateAreaStatus(areaId);
			}
			
			AreaQueue.isExecuteTask = false; //标记一个任务结束
		} catch (Exception e) {
			logger.error("" + e.getMessage(), e);
		}
		
	}
}
