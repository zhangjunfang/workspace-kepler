/**
 * 2014-6-4CheckdListListen.java
 */
package com.ctfo.storage.command.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.service.HBaseService;

/**
 * CheckdListListen
 * 
 * 
 * @author huangjincheng
 * 2014-6-4下午02:15:48
 * 
 */
public class CheckdListListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(CheckdListListen.class);
	
	private long batchTime = 20000; // 默认每30秒提交一次
	private HBaseService hbaseService;
	
	public CheckdListListen(){
		setName("CheckdListListen");
		hbaseService = new HBaseService();
	}
	public void run(){
		while(true){
			long curTime1 = System.currentTimeMillis();
			log.info("鉴权集合的大小和等待时间:"+ThVehicleCheckdLoading.getList().size()+"/"+(curTime1-ThVehicleCheckdLoading.getLastTime()));
			if(ThVehicleCheckdLoading.getList().size() != 0 &&(curTime1-ThVehicleCheckdLoading.getLastTime()) > batchTime){
				ThVehicleCheckdLoading.updateList(hbaseService);
			} 
			try {
				Thread.sleep(5000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

}
