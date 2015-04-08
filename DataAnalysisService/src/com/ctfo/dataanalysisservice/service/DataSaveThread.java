package com.ctfo.dataanalysisservice.service;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;
import com.ctfo.dataanalysisservice.io.DataPool;
import com.ctfo.dataanalysisservice.save.dao.ThVehicleAlarmDao;
import com.ctfo.dataanalysisservice.save.dao.impl.MysqlThVehicleAlarmDaoImpl;
import com.ctfo.dataanalysisservice.save.dao.impl.ThvehicleAlarmDaoImpl;


/**
 * 数据存储
 * @author yangjian
 *
 */
public class DataSaveThread extends Thread {

	private static final Logger logger = Logger.getLogger(DataSaveThread.class);
	private ThVehicleAlarmDao alarmDao = new ThvehicleAlarmDaoImpl();
	private ThVehicleAlarmDao mysqlalarmDao = new MysqlThVehicleAlarmDaoImpl();
	
	private String name;
	
	public DataSaveThread(){
		name=UUID.randomUUID().toString();
	}


	/**
	 * 线程执行
	 */
	public void run() {

	 
		   ThVehicleAlarm alarm=null;
		   int saveCount = 0;
		   int updateCount = 0;
		   List<ThVehicleAlarm> saveData = new ArrayList<ThVehicleAlarm>();
		   List<ThVehicleAlarm> updateData = new ArrayList<ThVehicleAlarm>();
		   //提交数据最长间隔（毫秒）
		   int intervalTime=5000; 
		   //提交判断时间
		   Long time=System.currentTimeMillis();
		   //当前时间
		   Long curentTime=null;
		   //批次提交记录最大数
		   int count=100;
		   
		   Long checkTime=null;
		   
		   while(true){
			     try{
			   logger.info("@--------------------------DataSaveThread");
				//取得原始报文命令
//			    try{
			    alarm = DataPool.getSaveDataPacketValue();
			    logger.info("@@-----------------------alarm");
//				}catch(InterruptedException e){
//					logger.error(e);
//					Thread.currentThread().interrupt();
//				}
			   // logger.info(name+" SaveDataPacketSize="+DataPool.getSaveDataPacketSize());
				checkTime=time+intervalTime;
				curentTime=System.currentTimeMillis();
				if(alarm!=null){

					// 插入数据
					if(!alarm.getIsUpdate()){
					    saveData.add(alarm);
					}else{
						//修改数据	
					    updateData.add(alarm);
					}
					
					
					//满足记录提交间隔时间条件 则提交数据库操作
					if( checkTime<=curentTime){
						if(saveData.size()>0){
						alarmDao.save(saveData);
						mysqlalarmDao.save(saveData);
						saveData= new ArrayList<ThVehicleAlarm>();
						saveCount = 0;}
						//time=System.currentTimeMillis();
						if(updateData.size()>0){
						alarmDao.update(updateData);
						mysqlalarmDao.update(updateData);
						updateData= new ArrayList<ThVehicleAlarm>();
						updateCount = 0;
						}
						time=System.currentTimeMillis();
						continue;
					}
					
					 //满足批次记录提交数条件,则提交数据库操作
					if (saveCount >= count ) {
						saveCount = 0;
						alarmDao.save(saveData);
						mysqlalarmDao.save(saveData);
						saveData= new ArrayList<ThVehicleAlarm>();	
					}else{
					saveCount++;
					}
					
					
					 //满足批次记录提交数条件,则提交数据库操作
					if (updateCount >= count ) {
						updateCount = 0;
						alarmDao.update(updateData);
						mysqlalarmDao.update(updateData);
						updateData= new ArrayList<ThVehicleAlarm>();
					}else{
					updateCount++;
					}
					
					//----------------
			    } 
						} catch (Exception e) {
             e.printStackTrace();
			logger.error(e);
		}
		   }



	}
	
	
	
	 

}