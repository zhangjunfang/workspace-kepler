package com.ctfo.trackservice.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.model.OilPressureBean;
import com.ctfo.trackservice.service.OracleService;

public class VehicleReportOilThread extends AbstractThread{
	private final static Logger logger = LoggerFactory.getLogger(VehicleReportOilThread.class);
	
	private ArrayBlockingQueue<OilPressureBean> queue = new ArrayBlockingQueue<OilPressureBean>(5000);
	private long threadId ;
	private long currentTime = 0l;
	private long lastTime = System.currentTimeMillis();
	private long time = 30*1000;
	private int count;
	private Boolean flag = true;
	private OracleService oracleService = new OracleService();
	//private static OracleConnectionPool oracleConnectionPool = new OracleConnectionPool();
	private List<OilPressureBean> list= new ArrayList<OilPressureBean>();
	public VehicleReportOilThread(int threadId,long time,int count){
		setName("VehicleReportOil-thread-"+threadId);
		this.threadId = threadId;
		this.time = time * 1000;
		this.count = count;
	}
	@Override
	public void init() {
		// TODO Auto-generated method stub
		
	}
	
	@Override
	public void run() {
		while(flag){
			try {
				currentTime = System.currentTimeMillis();
				if(list.size() >0){
					if(list.size() >= count || (currentTime-lastTime)>time){
						oracleService.saveOilPressureDay(OracleConnectionPool.getConnection(), list);
						lastTime= System.currentTimeMillis();
						logger.info("----燃油压力提交成功Thread Id : {},数量:【{}】,耗时:【{}】ms",this.threadId,list.size(),(lastTime-currentTime));
						ThreadPool.addCurrentReportsize(list.size());
						list.clear();
						
					}
				}
				OilPressureBean oilWear = queue.poll();
				if(null != oilWear){
					list.add(oilWear);
				}
				//logger.info("------------------"+vehicleStatus.getMileage());
			} catch (Exception e) {
				logger.debug("燃油压力批量提交异常！",e);
				
			}
		}
		logger.info("----燃油压力提交线程Thread Id : {} 已关闭！",this.threadId);
	}
	
	
	
	
	public void addPacket(Object o) {
		try {
			queue.put((OilPressureBean)o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!"); 
		}
		
		
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

