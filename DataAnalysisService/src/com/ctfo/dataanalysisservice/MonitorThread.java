package com.ctfo.dataanalysisservice;

import org.apache.log4j.Logger;

/**
 * 监控线程
 * @author yangjian
 *
 */
public class MonitorThread extends Thread {
	
	
	private static final Logger logger = Logger.getLogger(DataAnalysisServiceMain.class);
	
	public void run(){
	
		while(true){
			/* logger.info("接收的数据量"+DataPool.i);
			 logger.info("处理的数据量"+DataPool.j);
			 logger.info("接收原始命令队列数："+DataPool.getReceivePacketSize());
			 logger.info("存储队列数："+DataPool.getSaveDataPacketSize());*/
			 logger.info("业务缓存池大小"+BussinesDistributeThreadPool.getPoolSize());
			 logger.info("~~~~~~~~~~~~~~~~~~~~业务缓存池数据大小"+BussinesDistributeThreadPool.getBussinesDistributeThreadArray()[0].getPacketsSize());
			
			/*int i=0;
			for(BussinesDistributeThread bu:BussinesDistributeThreadPool.getBussinesDistributeThreadArray()){
				logger.info("业务线程"+i+++"队列数："+bu.getPacketsSize());
			}*/
			
		}
		
		
	}
	
	public static void main(String args[]){
		System.out.println("123456".substring(0,"123456".length() -3));
	}

}
