package com.ctfo.storage.dispatch.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.model.ResponseModel;
import com.ctfo.storage.dispatch.util.SerialUtil;
import com.ctfo.storage.dispatch.util.Tools;


/**
 * ResponseListen
 * 
 * 
 * @author huangjincheng
 * 2014-6-12下午03:01:56
 * 
 */
public class ResponseListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(ResponseListen.class);
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(50000);
	private boolean flag = true;
	private static int count=0;
	private static IoSession session;
	public ResponseListen(){
		setName("ResponseListen");
	}
	
	public void run(){
		log.info("--ResponseListen-- 应答监听线程启动！");
		while(flag){
			try {
				String response = queue.take();
				process(response);			
			} catch (InterruptedException e) {
				log.error("应答监听线程异常:"+e.getMessage());
			}
		}
	}
	
	public void process(String response){
		//IoSession session = response.getSession();
		//String sourceStr = response.getSourceStr();
		StringBuffer buff = new StringBuffer();
		buff.append(response.substring(10,18))
		.append(response.substring(2,10)).append("C001")
		.append(Tools.fillNBitBefore(Integer.toHexString(SerialUtil.getInt()), 8, "0"))
		.append("00000005").append(response.substring(22,30)).append("00");
		buff.append(Tools.getCheckCode(buff.toString())).append("5D");
		String res = Tools.getReverTransferContent("5B"+buff.toString());      		
		session.write(res);
		count++;
		//log.info("--应答--[{}]",res);
	}
	
	public static void put(String response){
		try {
			queue.put(response);
		} catch (InterruptedException e) {
			log.error("应答监听queue异常！"+e.getMessage());
		}
	}

	public static int getCount() {
		return count;
	}

	public void setCount(int count) {
		this.count = count;
	}

	public static ArrayBlockingQueue<String> getQueue() {
		return queue;
	}

	public static void setQueue(ArrayBlockingQueue<String> queue) {
		ResponseListen.queue = queue;
	}

	public IoSession getSession() {
		return session;
	}

	public static void setSession(IoSession session) {
		ResponseListen.session = session;
	}
	
	
}
