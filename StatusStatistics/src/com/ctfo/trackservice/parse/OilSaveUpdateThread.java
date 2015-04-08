package com.ctfo.trackservice.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.model.OilSaveBean;
import com.ctfo.trackservice.service.OracleService;

public class OilSaveUpdateThread extends AbstractThread{

private final static Logger logger = LoggerFactory.getLogger(OilSaveUpdateThread.class);
	
	private ArrayBlockingQueue<OilSaveBean> queue = new ArrayBlockingQueue<OilSaveBean>(5000);
	private long threadId ;
	private long currentTime = 0l;
	private long lastTime = System.currentTimeMillis();
	private long time = 30*1000;
	private int count;
	private Boolean flag = true;
	private OracleService oracleService = new OracleService();
	//private static OracleConnectionPool oracleConnectionPool = new OracleConnectionPool();
	private List<OilSaveBean> list= new ArrayList<OilSaveBean>();
	public OilSaveUpdateThread(int threadId,long time,int count){
		setName("OilSaveUpdate-thread-"+threadId);
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
						oracleService.saveOilDayInfo(OracleConnectionPool.getConnection(), list);
						lastTime= System.currentTimeMillis();
						logger.info("----节油驾驶提交成功Thread Id : {},数量:【{}】,耗时:【{}】ms",this.threadId,list.size(),(lastTime-currentTime));
						ThreadPool.addFileSize(list.size());//提交文件数与总文件数对比，判断是否结束线程
						list.clear();			
					}
				}
				OilSaveBean oilSaveBean = queue.poll();
				if(null != oilSaveBean){
					list.add(oilSaveBean);
				}
				//logger.info("------------------"+vehicleStatus.getMileage());
			} catch (Exception e) {
				logger.debug("节油驾驶统计信息批量提交异常！",e);
				
			}
		}
		logger.info("----节油驾驶提交线程Thread Id : {} 已关闭！",this.threadId);
	}
	
	
	
	
	public void addPacket(Object o) {
		try {
			queue.put((OilSaveBean)o);
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
