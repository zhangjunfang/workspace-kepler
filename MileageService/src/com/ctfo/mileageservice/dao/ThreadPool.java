package com.ctfo.mileageservice.dao;

import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;
import com.ctfo.mileageservice.model.VehicleInfo;
import com.ctfo.mileageservice.parse.AbstractThread;


/**
 * 文件名：ThreadPool.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-15上午9:42:13
 * 
 */
public class ThreadPool {
	//private final static Logger logger = LoggerFactory.getLogger(ThreadPool.class);
	/**vid—车辆信息*/
	public static HashMap<String, VehicleInfo> vehicleInfoMap = new HashMap<String, VehicleInfo>();
	/**分析线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> vehicleStatusAnalyPool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**提交数据库线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> vehicleStatusUpdatePool = new ConcurrentHashMap<Integer, AbstractThread>();
	

	/**轨迹文件日统计总文件数目*/
	private static long totalSize;
	/**轨迹文件日统计数目*/
	private static long analySize;
	/**轨迹文件日统计当前文件数目*/
	private static long fileSize;
	
	
	
	
	public static void init(){
		totalSize = 0;
		fileSize = 0;
		analySize = 0;

	}

	/**
	 * @return the totalSize
	 */
	public static long getTotalSize() {
		return totalSize;
	}


	/**
	 * @param totalSize the totalSize to set
	 */
	public static void setTotalSize(long size) {
		totalSize = size;
	}


	/**
	 * @return the fileSize
	 */
	public static long getFileSize() {
		return fileSize;
	}


	/**
	 * @param fileSize the fileSize to set
	 */
	public static void setFileSize(long size) {
		fileSize = size;
	}


	public static void addVehicleStatusAnalyPool(Integer key,AbstractThread thread){
		vehicleStatusAnalyPool.put(key, thread);
	}
	
	public static void addVehicleStatusUpdatePool(Integer key,AbstractThread thread){
		vehicleStatusUpdatePool.put(key, thread);
	}
	
	
	public static void addFileSize(long size){
		fileSize = fileSize + size;
	}


	/**
	 * @return the analyPool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getAnalyPool() {
		return vehicleStatusAnalyPool;
	}


	/**
	 * @param analyPool the analyPool to set
	 */
	public static void setAnalyPool(
			ConcurrentHashMap<Integer, AbstractThread> analyPool) {
		ThreadPool.vehicleStatusAnalyPool = analyPool;
	}


	/**
	 * @return the updatePool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getUpdatePool() {
		return vehicleStatusUpdatePool;
	}


	/**
	 * @param updatePool the updatePool to set
	 */
	public static void setUpdatePool(
			ConcurrentHashMap<Integer, AbstractThread> updatePool) {
		ThreadPool.vehicleStatusUpdatePool = updatePool;
	}

	/**
	 * @return the analySize
	 */
	public static long getAnalySize() {
		return analySize;
	}

	/**
	 * @param analySize the analySize to set
	 */
	public static void setAnalySize(long analySize) {
		ThreadPool.analySize = analySize;
	}
	
	public static void addAnalySize(long a) {
		analySize = analySize + a;
	}
	

	
}
