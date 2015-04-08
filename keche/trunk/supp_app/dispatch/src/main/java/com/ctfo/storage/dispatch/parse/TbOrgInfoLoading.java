package com.ctfo.storage.dispatch.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.model.TbOrgInfo;
import com.ctfo.storage.dispatch.service.MySqlService;
import com.ctfo.storage.dispatch.util.ConfigLoader;

public class TbOrgInfoLoading extends Thread{
	private static Logger log = LoggerFactory.getLogger(TbOrgInfoLoading.class);
	
	private static ArrayBlockingQueue<TbOrgInfo> queue = new ArrayBlockingQueue<TbOrgInfo>(50000);
	
	private static MySqlService mySqlService;
	
	private int batchSize = 1000; // 默认每次批量提交100条
	private static int processSize = 0; //提交数据库总条数
	private static int size = 0;
	private static long lastTime = System.currentTimeMillis();
	
	private static List<TbOrgInfo> list = new ArrayList<TbOrgInfo>();
	
	public TbOrgInfoLoading(){
		mySqlService = new MySqlService();
		mySqlService.setSqlMap(ConfigLoader.sqlMap); 
	}
	
	public void run(){
		while(true){
			try {
//				TbDvr3G o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 		
				TbOrgInfo o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。

				size++;
				process(o); 
				
			} catch (Exception e) {
				log.error("Loading处理队列数据异常:" + e.getMessage());
			}
		}
	}
	
	private void process(TbOrgInfo o) {
		
		if(o != null){
			list.add(o);
		}
		if(size == batchSize){			
			long start = System.currentTimeMillis();	
			mySqlService.tbOrgInfoSave(list);
			processSize += list.size();
			log.info("【TB_ORG_INFO】 1000条批量提交:{}, 耗时:{}ms, 共提交总数:{}"  , list.size(), (System.currentTimeMillis() - start), processSize);
			list.clear();
			size = 0;
			lastTime = System.currentTimeMillis();
			
		}
		
		
	}
	
	static void updateList(MySqlService mySqlService) {
		long start = System.currentTimeMillis();
		List<TbOrgInfo> listCopy = new ArrayList<TbOrgInfo>();
		listCopy.addAll(list);
		list.clear();
		mySqlService.tbOrgInfoSave(listCopy);
		processSize += listCopy.size();
		log.info("【TB_ORG_INFO】 自动批量提交:{}, 耗时:{}ms, 共提交总数:{}"  , listCopy.size(), (System.currentTimeMillis() - start), processSize);
		size = 0;
		lastTime = System.currentTimeMillis();
			
		}


	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(TbOrgInfo data){
		return queue.offer(data);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(TbOrgInfo data){
		try {
			queue.put(data);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(TbOrgInfo data){
		return queue.add(data);
	}

	/**
	 * 获取list的值
	 * @return list  
	 */
	public static List<TbOrgInfo> getList() {
		return list;
	}

	/**
	 * 设置list的值
	 * @param list
	 */
	public static void setList(List<TbOrgInfo> list) {
		TbOrgInfoLoading.list = list;
	}

	/**
	 * 获取lastTime的值
	 * @return lastTime  
	 */
	public static long getLastTime() {
		return lastTime;
	}

	/**
	 * 设置lastTime的值
	 * @param lastTime
	 */
	public static void setLastTime(long lastTime) {
		TbOrgInfoLoading.lastTime = lastTime;
	}
	
	
}
