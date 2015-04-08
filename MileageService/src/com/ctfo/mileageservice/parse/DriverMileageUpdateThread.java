package com.ctfo.mileageservice.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mileageservice.dao.OracleConnectionPool;
import com.ctfo.mileageservice.model.DriverDetailBean;
import com.ctfo.mileageservice.service.OracleService;


/**
 * 文件名：DriverMileageUpdateThread.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-10-14上午10:27:04
 * 
 */
public class DriverMileageUpdateThread extends AbstractThread{
	private final static Logger logger = LoggerFactory.getLogger(DriverMileageUpdateThread.class);
	
	private ArrayBlockingQueue<DriverDetailBean> queue = new ArrayBlockingQueue<DriverDetailBean>(5000);
	private long threadId ;
	private long currentTime = 0l;
	private long lastTime = System.currentTimeMillis();
	private long time = 30*1000;
	private int count;
	private Boolean flag = true;
	//private static OracleService oracleService = new OracleService();
	//private static OracleConnectionPool oracleConnectionPool = new OracleConnectionPool();
	private List<DriverDetailBean> list= new ArrayList<DriverDetailBean>();
	public DriverMileageUpdateThread(int threadId,long time,int count){
		setName("DriverDetailBeanUpdate-thread-"+threadId);
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
						OracleService.saveDriverDetail(OracleConnectionPool.getConnection(), list);
						lastTime= System.currentTimeMillis();
						logger.info("----驾驶员行驶里程统计提交成功Thread Id : {},数量:【{}】,耗时:【{}】ms",this.threadId,list.size(),(lastTime-currentTime));
						list.clear();					
					}
				}else{
					if((System.currentTimeMillis()-lastTime)>5*60*1000){
						//5分钟没文件读取，自动关闭提交线程
						close();
					}
				}
				DriverDetailBean DriverDetailBean = queue.poll();
				if(null != DriverDetailBean){
					list.add(DriverDetailBean);
				}
				//logger.info("------------------"+DriverDetailBean.getMileage());
			} catch (Exception e) {
				logger.debug("驾驶员行驶里程统计批量提交异常！",e);
				
			}
		}
		logger.info("----驾驶员行驶里程统计提交线程Thread Id : {} 已关闭！",this.threadId);
	}
	
	
	
	
	public void addPacket(Object o) {
		try {
			queue.put((DriverDetailBean)o);
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
