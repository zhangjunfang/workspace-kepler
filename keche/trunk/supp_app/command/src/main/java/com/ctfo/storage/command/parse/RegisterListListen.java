/**
 * 2014-6-4ListListen.java
 */
package com.ctfo.storage.command.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.service.HBaseService;

/**
 * ListListen
 * 
 * 
 * @author huangjincheng
 * 2014-6-4下午01:29:47
 * 
 */
public class RegisterListListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(RegisterListListen.class);
	
	private long batchTime = 20000; // 默认每30秒提交一次
	private HBaseService hbaseService;
	
	public RegisterListListen(){
		setName("RegisterListListen");
		hbaseService = new HBaseService();
	}
	public void run(){
		while(true){
			long curTime1 = System.currentTimeMillis();
			log.info("注册集合的大小和等待时间:"+ThTerminalRegisterLoading.getList().size()+"/"+(curTime1-ThTerminalRegisterLoading.getLastTime()));
			if(ThTerminalRegisterLoading.getList().size() != 0 &&(curTime1-ThTerminalRegisterLoading.getLastTime()) > batchTime){
				ThTerminalRegisterLoading.updateList(hbaseService);
			} 
			try {
				Thread.sleep(20000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
}
