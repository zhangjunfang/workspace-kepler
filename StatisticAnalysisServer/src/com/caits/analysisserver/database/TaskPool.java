package com.caits.analysisserver.database;

import java.util.Vector;
import java.util.concurrent.ConcurrentHashMap;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.bean.Task;

public class TaskPool {
	
	private ConcurrentHashMap<String,Task> mainTaskMap = new ConcurrentHashMap<String,Task>();
	
	// 子任务集合
	private ConcurrentHashMap<String,ConcurrentHashMap<String,Vector<UnifiedFileDispatch>>> taskMap = new ConcurrentHashMap<String,ConcurrentHashMap<String,Vector<UnifiedFileDispatch>>>();	
	
	private final static TaskPool taskPool = new TaskPool();
	
	public static TaskPool getinstance(){
		return taskPool;
	}
	
	/****
	 * 
	 * @param key 任务名称
	 * @param value 任务线程集合
	 */
	public void putChildTask(String key,ConcurrentHashMap<String,Vector<UnifiedFileDispatch>> value){
		taskMap.put(key, value);
	}
	
	public ConcurrentHashMap<String,Vector<UnifiedFileDispatch>> getTask(String key){
		if(taskMap.containsKey(key)){
			return taskMap.get(key);
		}
		return null;
	}
	
	/****
	 * 将主任务接口添加到集合
	 * @param taskName
	 * @param task
	 */
	public void putMainTask(String taskName,Task task){
		mainTaskMap.put(taskName, task);
	}
	
	/***
	 * 获取主任务
	 * @param taskName
	 * @return
	 */
	public ConcurrentHashMap<String,Task> getMainTask(){
		return mainTaskMap;
	}
}
