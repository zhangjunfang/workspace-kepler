package com.ctfo.trackservice.dao;

import java.io.File;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.trackservice.model.StatusCode;
import com.ctfo.trackservice.model.VehicleInfo;
import com.ctfo.trackservice.parse.AbstractThread;

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
	public static boolean isOilOver =false;
	
	public static boolean isReportOver =false;
	
	/**vid—车辆信息*/
	public static HashMap<String, VehicleInfo> vehicleInfoMap = new HashMap<String, VehicleInfo>();
	/**分析线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> vehicleStatusAnalyPool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**分析线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> mileageAnalyPool = new ConcurrentHashMap<Integer, AbstractThread>();	
	
	/**分析线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> oilWearAnalyPool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**分析线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> vehicleReportAnalyPool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**提交数据库线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> vehicleStatusUpdatePool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**提交数据库线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> oilWearUpdatePool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**提交数据库线程池*/
	private static ConcurrentHashMap<Integer, AbstractThread> vehicleReportUpdatePool = new ConcurrentHashMap<Integer, AbstractThread>();
	
	/**油耗油量配置map*/
	public static ConcurrentHashMap<String,String> oilMonitorMap = new ConcurrentHashMap<String,String>();
	
	/**油耗油量配置map*/
	public static ConcurrentHashMap<String,String> oilWearMap = new ConcurrentHashMap<String,String>();
	
	/**油耗文件缓存*/
	public static ConcurrentHashMap<String,File> oilFileMap = new ConcurrentHashMap<String,File>();
	
	/**事件文件缓存*/
	public static ConcurrentHashMap<String,File> eventFileMap = new ConcurrentHashMap<String,File>();
	
	
	//存储车辆状态对应参考值
	public static Map<String, StatusCode> statusMap = new HashMap<String,StatusCode>();
	
	/**车辆运行日统计总文件数目*/
	private static long totalSize;
	/**车辆运行日统计当前文件数目*/
	private static long fileSize;
	
	/**行驶里程日统计总文件数目*/
	private static long mileageSize;
	/**行驶里程日统计当前文件数目*/
	private static long currentMileageSize;
	
	/**油量文件日统计总文件数目*/
	private static long oilSize;
	/**油量文件日统计当前文件数目*/
	private static long currentOilsize;
	
	/**单车报告日统计总文件数目*/
	private static long reportSize;
	/**单车报告日统计当前文件数目*/
	private static long currentReportsize;
	
	private static int taskCount = 0; //执行任务数 
	private static long size = 0;  //总文件数，计算进度百分比
	
	
	public static void init(){
		
		totalSize = 0;
		fileSize = 0;
		oilSize= 0;
		currentOilsize= 0;
		reportSize= 0;
		currentReportsize= 0;
		mileageSize = 0;
		currentMileageSize = 0;
		
		size = 0;
	}
	
	
	/**
	 * @return the oilSize
	 */
	public static long getOilSize() {
		return oilSize;
	}


	/**
	 * @param oilSize the oilSize to set
	 */
	public static void setOilSize(long oilSize) {
		ThreadPool.oilSize = oilSize;
	}


	/**
	 * @return the currentOilsize
	 */
	public static long getCurrentOilsize() {
		return currentOilsize;
	}


	/**
	 * @param currentOilsize the currentOilsize to set
	 */
	public static void setCurrentOilsize(long currentOilsize) {
		ThreadPool.currentOilsize = currentOilsize;
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

	public static void addMileageAnalyPool(Integer key,AbstractThread thread){
		mileageAnalyPool.put(key, thread);
	}
	
	public static void addVehicleStatusAnalyPool(Integer key,AbstractThread thread){
		vehicleStatusAnalyPool.put(key, thread);
	}
	
	public static void addVehicleStatusUpdatePool(Integer key,AbstractThread thread){
		vehicleStatusUpdatePool.put(key, thread);
	}
	
	public static void addOilWearAnalyPool(Integer key,AbstractThread thread){
		oilWearAnalyPool.put(key, thread);
	}
	
	public static void addOilWearUpdatePool(Integer key,AbstractThread thread){
		oilWearUpdatePool.put(key, thread);
	}
	
	public static void addVehicleReportAnalyPool(Integer key,AbstractThread thread){
		vehicleReportAnalyPool.put(key, thread);
	}
	
	public static void addVehicleReportUpdatePool(Integer key,AbstractThread thread){
		vehicleReportUpdatePool.put(key, thread);
	}
	
	
	public static void addFileSize(long size){
		fileSize = fileSize + size;
	}
	
	public static void addMileageSize(long size){
		currentMileageSize = currentMileageSize + size;
	}
	
	
	public static void addCurrentOilsize(long size){
		currentOilsize = currentOilsize + size;
	}
	
	public static void addCurrentReportsize(long size){
		currentReportsize = currentReportsize + size;
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
	 * @return the oilWearAnalyPool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getOilWearAnalyPool() {
		return oilWearAnalyPool;
	}


	/**
	 * @param oilWearAnalyPool the oilWearAnalyPool to set
	 */
	public static void setOilWearAnalyPool(
			ConcurrentHashMap<Integer, AbstractThread> oilWearAnalyPool) {
		ThreadPool.oilWearAnalyPool = oilWearAnalyPool;
	}


	/**
	 * @return the oilWearUpdatePool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getOilWearUpdatePool() {
		return oilWearUpdatePool;
	}


	/**
	 * @param oilWearUpdatePool the oilWearUpdatePool to set
	 */
	public static void setOilWearUpdatePool(
			ConcurrentHashMap<Integer, AbstractThread> oilWearUpdatePool) {
		ThreadPool.oilWearUpdatePool = oilWearUpdatePool;
	}


	/**
	 * @return the oilMonitorMap
	 */
	public static ConcurrentHashMap<String, String> getOilMonitorMap() {
		return oilMonitorMap;
	}


	/**
	 * @param oilMonitorMap the oilMonitorMap to set
	 */
	public static void setOilMonitorMap(
			ConcurrentHashMap<String, String> oilMonitorMap) {
		ThreadPool.oilMonitorMap = oilMonitorMap;
	}


	/**
	 * @return the vehicleReportUpdatePool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getVehicleReportUpdatePool() {
		return vehicleReportUpdatePool;
	}


	/**
	 * @param vehicleReportUpdatePool the vehicleReportUpdatePool to set
	 */
	public static void setVehicleReportUpdatePool(
			ConcurrentHashMap<Integer, AbstractThread> vehicleReportUpdatePool) {
		ThreadPool.vehicleReportUpdatePool = vehicleReportUpdatePool;
	}


	/**
	 * @return the vehicleReportAnalyPool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getVehicleReportAnalyPool() {
		return vehicleReportAnalyPool;
	}


	/**
	 * @param vehicleReportAnalyPool the vehicleReportAnalyPool to set
	 */
	public static void setVehicleReportAnalyPool(
			ConcurrentHashMap<Integer, AbstractThread> vehicleReportAnalyPool) {
		ThreadPool.vehicleReportAnalyPool = vehicleReportAnalyPool;
	}


	/**
	 * @return the reportSize
	 */
	public static long getReportSize() {
		return reportSize;
	}


	/**
	 * @param reportSize the reportSize to set
	 */
	public static void setReportSize(long reportSize) {
		ThreadPool.reportSize = reportSize;
	}


	/**
	 * @return the currentReportsize
	 */
	public static long getCurrentReportsize() {
		return currentReportsize;
	}


	/**
	 * @param currentReportsize the currentReportsize to set
	 */
	public static void setCurrentReportsize(long currentReportsize) {
		ThreadPool.currentReportsize = currentReportsize;
	}


	/**
	 * @return the taskCount
	 */
	public static int getTaskCount() {
		return taskCount;
	}


	/**
	 * @param taskCount the taskCount to set
	 */
	public static void setTaskCount(int taskCount) {
		ThreadPool.taskCount = taskCount;
	}


	/**
	 * @return the size
	 */
	public static long getSize() {
		return size;
	}


	/**
	 * @param size the size to set
	 */
	public static void setSize(long size) {
		ThreadPool.size = size;
	}


	/**
	 * @return the mileageSize
	 */
	public static long getMileageSize() {
		return mileageSize;
	}


	/**
	 * @param mileageSize the mileageSize to set
	 */
	public static void setMileageSize(long mileageSize) {
		ThreadPool.mileageSize = mileageSize;
	}


	/**
	 * @return the currentMileageSize
	 */
	public static long getCurrentMileageSize() {
		return currentMileageSize;
	}


	/**
	 * @param currentMileageSize the currentMileageSize to set
	 */
	public static void setCurrentMileageSize(long currentMileageSize) {
		ThreadPool.currentMileageSize = currentMileageSize;
	}


	/**
	 * @return the mileageAnalyPool
	 */
	public static ConcurrentHashMap<Integer, AbstractThread> getMileageAnalyPool() {
		return mileageAnalyPool;
	}


	/**
	 * @param mileageAnalyPool the mileageAnalyPool to set
	 */
	public static void setMileageAnalyPool(
			ConcurrentHashMap<Integer, AbstractThread> mileageAnalyPool) {
		ThreadPool.mileageAnalyPool = mileageAnalyPool;
	}


	/**
	 * @return the oilFileMap
	 */
	public static ConcurrentHashMap<String,File> getOilFileMap() {
		return oilFileMap;
	}


	/**
	 * @param oilFileMap the oilFileMap to set
	 */
	public static void setOilFileMap(ConcurrentHashMap<String,File> oilFileMap) {
		ThreadPool.oilFileMap = oilFileMap;
	}
	
	

	
}
