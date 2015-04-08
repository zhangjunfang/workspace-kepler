package com.ctfo.trackservice.parse;

import java.io.File;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.model.OilWearBean;
import com.ctfo.trackservice.service.OilWearService;

/**
 * 文件名：OilWearAnalyThread.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-23上午10:27:18
 * 
 */
public class OilWearAnalyThread extends AbstractThread{
	private final static Logger logger = LoggerFactory.getLogger(OilWearAnalyThread.class);
	
	private ArrayBlockingQueue<File> queue = new ArrayBlockingQueue<File>(5000);
	//private VehicleRunningUpdateThread vehicleRunningUpdateThread;
	//private static VehicleRunningAnalyserService vehicleRunningAnalyserService;
	private long utc;
	//private static String vid;
	private long threadId ;
	
	private long endTime = System.currentTimeMillis();
	
	private final long time = 20 * 1000;
	private Boolean flag = true;
	public OilWearAnalyThread(int threadId,int threadNum,long u){
		setName("OilWearAnaly-thread-"+threadId);
		this.threadId = threadId;
		utc = u;		
	
		
	}
	@Override
	public void init() {
		// TODO Auto-generated method stub
		
	}
	
	@Override
	public void run() {
		while(flag){
			try {
				File file = queue.poll();
				if(file != null){
					long  startTime=System.currentTimeMillis();
					//logger.info("----车辆运行状态分析线程Thread Id : "+ threadId + ", 处理文件名称:" + file.getName());
					statisticStatus(file); //开始统计
					endTime=System.currentTimeMillis();
					logger.info("----车辆油耗油量分析线程Thread Id : {},处理文件名称:{},处理时长：【{}】ms",this.threadId,file.getName(),(endTime-startTime));
				}else{
					if((System.currentTimeMillis()-endTime)>time){
						//20秒没文件读取，自动关闭分析线程
						close();
					}
				}		
				
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		logger.info("----车辆油耗油量分析线程Thread Id : {} 已关闭！",this.threadId);
	}

	/**
	 * 文件分析方法
	 * @param file
	 * @throws Exception 
	 */
	private void statisticStatus(File file) throws Exception {	
		OilWearService oilWearService = new OilWearService(utc, file.getName().replaceAll("\\.txt", ""));
		OilWearBean oilWear = oilWearService.analysisOilRecords(file);
		ThreadPool.getOilWearUpdatePool().get(1).addPacket(oilWear);
	}
	
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */

	
	public void addPacket(Object o) {
		try {
			queue.put((File)o);
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
