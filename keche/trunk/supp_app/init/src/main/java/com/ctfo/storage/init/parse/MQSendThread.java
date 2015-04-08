package com.ctfo.storage.init.parse;

import java.util.Date;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.init.model.TbDvr3G;
import com.ctfo.storage.init.model.TbDvrSer;
import com.ctfo.storage.init.model.TbOrg;
import com.ctfo.storage.init.model.TbOrgInfo;
import com.ctfo.storage.init.model.TbPredefinedMsg;
import com.ctfo.storage.init.model.TbProductType;
import com.ctfo.storage.init.model.TbSim;
import com.ctfo.storage.init.model.TbSpOperator;
import com.ctfo.storage.init.model.TbSpRole;
import com.ctfo.storage.init.model.TbTerminal;
import com.ctfo.storage.init.model.TbTerminalOem;
import com.ctfo.storage.init.model.TbTerminalParam;
import com.ctfo.storage.init.model.TbTerminalProtocol;
import com.ctfo.storage.init.model.TbVehicle;
import com.ctfo.storage.init.model.ThTransferHistory;
import com.ctfo.storage.init.model.TrOperatorRole;
import com.ctfo.storage.init.model.TrRoleFunction;
import com.ctfo.storage.init.model.TrServiceunit;
import com.ctfo.storage.init.service.MQService;
import com.ctfo.storage.init.util.Tools;


/**
 * MQSendThread
 * 
 * 
 * @author huangjincheng
 * 2014-5-27下午03:41:49
 * 
 */
public class MQSendThread{
	private static Logger log = LoggerFactory.getLogger(MQSendThread.class);
	private long startTime = 0l; //初始化服务开始时间
	private int threadCount = 0; //需要执行的线程数
	public MQSendThread(long startTime,int threadCount){
		this.startTime = startTime;
		this.threadCount = threadCount;
	}
	
	public class MQSendTbOrgThread extends Thread{
		private  ArrayBlockingQueue<TbOrg> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_ORGANIZATION";
		private int count = 0;
		private int size = 0;
		public MQSendTbOrgThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbOrg>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbOrg o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					process(o); 						
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbOrg o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbOrg data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	
	public class MQSendTbDvr3GThread extends Thread{
		private  ArrayBlockingQueue<TbDvr3G> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_DVR";
		private int count = 0;
		private int size = 0;
		public MQSendTbDvr3GThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbDvr3G>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbDvr3G o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;		
					}
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
/*					if(queue.size() == 0) {
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
									
					}*/
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbDvr3G o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbDvr3G data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	public class MQSendTbDvrSerThread extends Thread{
		private  ArrayBlockingQueue<TbDvrSer> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_DVRSER";
		private int count = 0;
		private int size = 0;
		public MQSendTbDvrSerThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbDvrSer>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbDvrSer o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
										
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbDvrSer o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public  void put(TbDvrSer data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbOrgInfoThread extends Thread{
		private  ArrayBlockingQueue<TbOrgInfo> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_ORG_INFO";
		private int count = 0;
		private int size = 0;
		public MQSendTbOrgInfoThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbOrgInfo>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbOrgInfo o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbOrgInfo o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public  void put(TbOrgInfo data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbPredefinedMsgThread extends Thread{
		private  ArrayBlockingQueue<TbPredefinedMsg> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_PREDEFINED_MSG";
		private int count = 0;
		private int size = 0;
		public MQSendTbPredefinedMsgThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbPredefinedMsg>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbPredefinedMsg o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
										
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbPredefinedMsg o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public  void put(TbPredefinedMsg data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	public class MQSendTbProductTypeThread extends Thread{
		private  ArrayBlockingQueue<TbProductType> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "SYS_PRODUCT_TYPE";
		private int count = 0;
		private int size = 0;
		public MQSendTbProductTypeThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbProductType>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbProductType o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
										
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbProductType o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbProductType data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbSimThread extends Thread{
		private  ArrayBlockingQueue<TbSim> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_SIM";
		private int count = 0;
		private int size = 0;
		public MQSendTbSimThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbSim>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbSim o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbSim o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbSim data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbSpOperatorThread extends Thread{
		private  ArrayBlockingQueue<TbSpOperator> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "SYS_SP_OPERATOR";
		private int count = 0;
		private int size = 0;
		public MQSendTbSpOperatorThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbSpOperator>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbSpOperator o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
										
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbSpOperator o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbSpOperator data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	public class MQSendTbSpRoleThread extends Thread{
		private  ArrayBlockingQueue<TbSpRole> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "SYS_SP_ROLE";
		private int count = 0;
		private int size = 0;
		public MQSendTbSpRoleThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbSpRole>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbSpRole o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbSpRole o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbSpRole data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQTbTerminalThread extends Thread{
		private  ArrayBlockingQueue<TbTerminal> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_TERMINAL";
		private int count = 0;
		private int size = 0;
		public MQTbTerminalThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbTerminal>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbTerminal o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
								
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbTerminal o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbTerminal data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	public class MQSendTbTerminalOemThread extends Thread{
		private  ArrayBlockingQueue<TbTerminalOem> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "SYS_TERMINAL_OEM";
		private int count = 0;
		private int size = 0;
		public MQSendTbTerminalOemThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbTerminalOem>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbTerminalOem o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
								
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbTerminalOem o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbTerminalOem data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbTerminalParamThread extends Thread{
		private  ArrayBlockingQueue<TbTerminalParam> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_TERMINAL_PARAM";
		private int count = 0;
		private int size = 0;
		public MQSendTbTerminalParamThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbTerminalParam>(1000000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbTerminalParam o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
					
					
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbTerminalParam o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbTerminalParam data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbTerminalProtocolThread extends Thread{
		private  ArrayBlockingQueue<TbTerminalProtocol> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_TERMINAL_PROTOCOL";
		private int count = 0;
		private int size = 0;
		public MQSendTbTerminalProtocolThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbTerminalProtocol>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbTerminalProtocol o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
						
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbTerminalProtocol o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbTerminalProtocol data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTbVehicleThread extends Thread{
		private  ArrayBlockingQueue<TbVehicle> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TB_VEHICLE";
		private int count = 0;
		private int size = 0;
		public MQSendTbVehicleThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TbVehicle>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TbVehicle o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TbVehicle o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TbVehicle data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendThTransferHistoryThread extends Thread{
		private  ArrayBlockingQueue<ThTransferHistory> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TH_TRANSFER_HISTORY";
		private int count = 0;
		private int size = 0;
		public MQSendThTransferHistoryThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<ThTransferHistory>(100000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					ThTransferHistory o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
								
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(ThTransferHistory o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(ThTransferHistory data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTrOperatorRoleThread extends Thread{
		private  ArrayBlockingQueue<TrOperatorRole> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TR_OPERATOR_ROLE";
		private int count = 0;
		private int size = 0;
		public MQSendTrOperatorRoleThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TrOperatorRole>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TrOperatorRole o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o);	
					
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage()+buffer.length());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TrOperatorRole o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TrOperatorRole data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	public class MQSendTrRoleFunctionThread extends Thread{
		private  ArrayBlockingQueue<TrRoleFunction> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TR_ROLE_FUNCTION";
		private int count = 0;
		private int size = 0;
		public MQSendTrRoleFunctionThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TrRoleFunction>(500000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TrRoleFunction o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
					
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" + mqName+e.getMessage());
				}
			}
			
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TrRoleFunction o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TrRoleFunction data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	public class MQSendTrServiceunitThread extends Thread{
		private  ArrayBlockingQueue<TrServiceunit> queue ;
		private  StringBuffer buffer = new StringBuffer();
		
		private MQService mqService;
		private boolean flag = true;
		private String mqName = "TR_SERVICEUNIT";
		private int count = 0;
		private int size = 0;
		public MQSendTrServiceunitThread(){
			setName(mqName);
			mqService = new MQService();
			queue = new ArrayBlockingQueue<TrServiceunit>(50000);
		}
		
		public void run(){
			
			Date sd = new Date();
			while(flag){
				try {
					TrServiceunit o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
					if(o.getQueneName().equals("END")){
						if(buffer.length() != 0){
							mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						}
						flag = false;
						break;
					}
					
					process(o); 
									
					if(count == 1000){
						mqService.send(buffer.substring(0, buffer.length()-1).toString(),mqName);
						buffer.delete(0, buffer.length());
						count =0;
					}
//					Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
			
					
				} catch (Exception e) {
					log.error("处理队列数据异常:" ,mqName+e.getMessage());
				}
			}
			Date ed = new Date();
			threadCount--;	
			log.info(mqName+": 发送MQ消息线程执行结束, 大小: [{}]条, 耗时: [{}]秒", size , (ed.getTime()-sd.getTime())/1000.0);
			if(threadCount == 0) log.info("--------------init初始化服务同步结束！总耗时：[{}]秒--------------",(ed.getTime()-startTime)/1000.0);
		}
		
		private void process(TrServiceunit o) {
			String jsonStr = JSON.toJSONString(o);
			jsonStr = Tools.strToBase64(jsonStr);
			count++;
			size++;
			buffer.append("1:"+jsonStr+";");		
		}
		public void put(TrServiceunit data){
			try {
				queue.put(data);
			} catch (InterruptedException e) {
				log.error("插入数据到队列异常!"); 
			}
		}
		
	}
	
	
	
	
}
