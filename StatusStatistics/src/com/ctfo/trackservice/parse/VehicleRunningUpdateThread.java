package com.ctfo.trackservice.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.model.VehicleStatus;
import com.ctfo.trackservice.service.OracleService;

/**
 * 文件名：VehicleRunningUpdateThread.java
 * 功能：车辆状态提交线程
 *
 * @author huangjincheng
 * 2014-9-15下午5:22:11
 * 
 */
public class VehicleRunningUpdateThread extends AbstractThread{
	private final static Logger logger = LoggerFactory.getLogger(VehicleRunningUpdateThread.class);
	
	private ArrayBlockingQueue<VehicleStatus> queue = new ArrayBlockingQueue<VehicleStatus>(30000);
	private long threadId ;
	private long currentTime = 0l;
	private long lastTime = System.currentTimeMillis();
	private long time = 30*1000;
	private int count;
	private Boolean flag = true;
	private static OracleService oracleService = new OracleService();
	//private static OracleConnectionPool oracleConnectionPool = new OracleConnectionPool();
	private List<VehicleStatus> list= new ArrayList<VehicleStatus>();
	public VehicleRunningUpdateThread(int threadId,long time,int count){
		setName("VehicleStatusUpdate-thread-"+threadId);
		this.threadId = threadId;
		this.time = time * 1000;
		this.count = count;
	}
    
	@Override
	public void run() {
		while(flag){
			try {
				currentTime = System.currentTimeMillis();
				if(list.size() >0){
					if(list.size() >= count || (currentTime-lastTime)>time){
						oracleService.saveStaDayInfo(OracleConnectionPool.getConnection(), list);
						lastTime= System.currentTimeMillis();
						logger.info("----车辆运行状态提交成功Thread Id : {},数量:【{}】,耗时:【{}】ms",this.threadId,list.size(),(lastTime-currentTime));
						ThreadPool.addFileSize(list.size());
						list.clear();
						
					}
				}
				VehicleStatus vehicleStatus = queue.poll();
				if(null != vehicleStatus){
					list.add(vehicleStatus);
				}
				//logger.info("------------------"+vehicleStatus.getMileage());
			} catch (Exception e) {
				logger.debug("车辆运行统计信息批量提交异常！",e);
				
			}
		}
		logger.info("----车辆运行状态提交线程Thread Id : {} 已关闭！",this.threadId);
	}
	
	
	
	
	public void addPacket(Object o) {
		try {
			queue.put((VehicleStatus)o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!"); 
		}
		
		
	}

	
	@Override
	public void init() {
		// TODO Auto-generated method stub
		
	}
	
	@Override
	public void close() {
		this.flag = false;
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
		
	}

	@Override
	public void setTime(long utc) {
		// TODO Auto-generated method stub
		
	}

}
