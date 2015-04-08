/**
 * 2014-6-4LogoffListListen.java
 */
package com.ctfo.storage.command.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.service.HBaseService;

/**
 * LogoffListListen
 * 
 * 
 * @author huangjincheng
 * 2014-6-4下午02:16:04
 * 
 */
public class LogoffListListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(LogoffListListen.class);
	
	private long batchTime = 20000; // 默认每30秒提交一次
	private HBaseService hbaseService;
	
	public LogoffListListen(){
		setName("LogoffListListen");
		hbaseService = new HBaseService();
	}
	public void run(){
		while(true){
			long curTime1 = System.currentTimeMillis();
			log.info("注销集合的大小和等待时间:"+ThVehicleLogoffLoading.getList().size()+"/"+(curTime1-ThVehicleLogoffLoading.getLastTime()));
		//	log.info(TbDvr3GLoading.getList().size() != 0);
			if(ThVehicleLogoffLoading.getList().size() != 0 &&(curTime1-ThVehicleLogoffLoading.getLastTime()) > batchTime){
				ThVehicleLogoffLoading.updateList(hbaseService);
			} 
			try {
				Thread.sleep(20000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

}
