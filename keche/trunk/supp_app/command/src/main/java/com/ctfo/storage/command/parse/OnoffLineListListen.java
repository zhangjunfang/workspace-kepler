/**
 * 2014-6-4OnoffLineListListen.java
 */
package com.ctfo.storage.command.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.service.HBaseService;

/**
 * OnoffLineListListen
 * 
 * 
 * @author huangjincheng
 * 2014-6-4下午02:16:34
 * 
 */
public class OnoffLineListListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(OnoffLineListListen.class);
	
	private long batchTime = 20000; // 默认每30秒提交一次
	private HBaseService hbaseService;
	
	public OnoffLineListListen(){
		setName("OnoffLineListListen");
		hbaseService = new HBaseService();
	}
	public void run(){
		while(true){
			long curTime1 = System.currentTimeMillis();
			log.info("上下线集合的大小和等待时间:"+ThVehicleOnoffLineLoading.getList().size()+"/"+(curTime1-ThVehicleOnoffLineLoading.getLastTime()));
		//	log.info(TbDvr3GLoading.getList().size() != 0);
			if(ThVehicleOnoffLineLoading.getList().size() != 0 &&(curTime1-ThVehicleOnoffLineLoading.getLastTime()) > batchTime){
				ThVehicleOnoffLineLoading.updateList(hbaseService);
			} 
			try {
				Thread.sleep(20000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

}
