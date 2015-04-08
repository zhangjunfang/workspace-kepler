package com.ctfo.storage.process.parse;

import java.io.IOException;

import org.apache.hadoop.hbase.client.HTableInterface;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.util.Cache;

/**
 * HbaseStorage
 * 
 * 
 * @author huangjincheng
 * 2014-7-11下午4:53:31
 * 
 */
public class HbaseStorageTask implements Runnable{
	private static Logger log = LoggerFactory.getLogger(HbaseStorageTask.class);
	
	private HTableInterface table = null;
	/** 任务名等于表名*/
	private String taskName ;
	/** 当前缓冲区数目*/
	private int currentCount;
	/** 上一次提交时缓冲区数目*/
	private int lastCount;
	/** 最后提交时间*/
	private long lastTime = System.currentTimeMillis();
	
	public HbaseStorageTask(HTableInterface table,String taskName){
		this.table = table;
		this.taskName = taskName;
	}
	
	@Override
	public void run() {
		try {
			if(table != null){
				lastTime = System.currentTimeMillis();
				currentCount = Cache.getTableMap().get(taskName);
				table.flushCommits();	
				log.info("--------[{}]定时提交缓存区成功！提交：[{}]条，耗时：[{}]ms，总提交：[{}]条-----------",taskName,(currentCount-lastCount),(System.currentTimeMillis()-lastTime),currentCount);
				lastCount = currentCount;
			}
		} catch (IOException e) {
			log.info("hbase定时提交缓存区异常！"+e.getMessage());
		}
		
	}

	/**
	 * 获取currentCount的值
	 * @return currentCount  
	 */
	public int getCurrentCount() {
		return currentCount;
	}

	/**
	 * 设置currentCount的值
	 * @param currentCount
	 */
	public void setCurrentCount(int currentCount) {
		this.currentCount = currentCount;
	}

	/**
	 * 获取lastCount的值
	 * @return lastCount  
	 */
	public int getLastCount() {
		return lastCount;
	}

	/**
	 * 设置lastCount的值
	 * @param lastCount
	 */
	public void setLastCount(int lastCount) {
		this.lastCount = lastCount;
	}

	
	
	
}
