package com.ctfo.commandservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.IccId;
import com.ctfo.commandservice.service.OracleJdbcService;
/**
 *	鉴权存储线程
 */
public class IccIdUpdateThread extends Thread {
	/**	日志	*/
	private static final Logger logger = LoggerFactory.getLogger(IccIdUpdateThread.class);
	/**	IccId队列	*/
	private ArrayBlockingQueue<IccId> iccIdQueue = new ArrayBlockingQueue<IccId>(10000);
	/** 批量提交数量	 */
	private int batchSize;
	/** 终端版本批量提交间隔时间  */
	private long terminalBatchTime;
	/** IccId最近处理时间	 */
	private long iccIdLastTime = System.currentTimeMillis();
	/** 最近处理时间	 */
	private long lastTime = System.currentTimeMillis();
	
	
	/**	数据库接口	*/
	private OracleJdbcService oracleJdbcService;
	
	public IccIdUpdateThread(OracleJdbcService oracleJdbcService, int batchSize, long terminalBatchTime) {
		setName("IccIdUpdateThread"); 
		this.oracleJdbcService = oracleJdbcService;
		this.batchSize = batchSize; 
		this.terminalBatchTime = terminalBatchTime;
	}

	/**
	 * 业务处理方法
	 */
	public void run(){
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				int authSize = iccIdQueue.size();
				if(authSize > 0 && ((currentTime - iccIdLastTime) >= terminalBatchTime )){	// 存储IccId信息
					List<IccId> list = new ArrayList<IccId>();
					for (int i = 0; i < authSize; i++) {
						list.add(iccIdQueue.poll());
					}
					long st = System.currentTimeMillis();
					try {
						oracleJdbcService.saveIccId(list, batchSize);
					} catch (Exception e) {
						logger.error("批量存储IccId信息数据异常:" + e.getMessage(), e);
					}
					list.clear();
					long et = System.currentTimeMillis();
					iccIdLastTime = et;
					logger.info("--saveBatchIccId----批量存储[{}]条IccId信息数据, 总耗时:[{}]ms", authSize, et - st);
				}
			} catch (Exception e) {
				logger.error("IccId信息存储线程错误:" + e.getMessage(), e);
			}

			try {
//				10秒输出一次线程状态
				if(currentTime - lastTime >= 10000){
					logger.info("---IccIdUpdateThread---IccId信息存储线程当前状态: IccId队列[{}]条", iccIdQueue.size());
					lastTime = System.currentTimeMillis();
				}
//				暂停1秒
				Thread.sleep(1000);
			} catch (Exception e) {
				logger.error(e.getMessage(), e);
			}
		}
	}
	
	
	/**
	 * 加入IccId队列
	 * @param iccIdInfo
	 */
	public void putIccId(IccId iccIdInfo) {
		try {
			iccIdQueue.put(iccIdInfo);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
}

