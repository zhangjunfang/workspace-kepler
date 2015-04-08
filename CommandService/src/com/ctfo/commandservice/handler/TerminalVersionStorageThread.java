package com.ctfo.commandservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.TerminalVersion;
import com.ctfo.commandservice.service.OracleJdbcService;
/**
 *	终端版本处理线程
 */
public class TerminalVersionStorageThread extends Thread {
	/**	日志	*/
	private static final Logger logger = LoggerFactory.getLogger(TerminalVersionStorageThread.class);
	/**	终端版本信息集合		*/
	private Map<String, TerminalVersion> terminalMap = new ConcurrentHashMap<String, TerminalVersion>();
	/** 批量提交数量	 */
	private int batchSize;
	/** 处理标识数量	 */
	private int processSize;
	/** 终端版本批量提交间隔时间  */
	private long terminalBatchTime;
	/** 终端版本信息更新最近处理时间	 */
	private long terminalLastTime = System.currentTimeMillis();
	/** 最近处理时间	 */
	private long lastTime = System.currentTimeMillis();
	/**	数据库接口	*/
	private OracleJdbcService oracleJdbcService;
	
	public TerminalVersionStorageThread(OracleJdbcService oracleJdbcService, int batchSize,int processSize, long terminalBatchTime) {
		setName("TerminalVersionStorageThread"); 
		this.oracleJdbcService = oracleJdbcService;
		this.batchSize = batchSize; 
		this.processSize = processSize; 
		this.terminalBatchTime = terminalBatchTime;
	}

	/**
	 * 业务处理方法
	 */
	public void run(){
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				int terminalSize = terminalMap.size();
//				数据量达到处理标识数后 、 处理时间符合后进行处理
				if(terminalSize > processSize || ((currentTime - terminalLastTime) >= terminalBatchTime)){ //	终端版本信息处理
					List<TerminalVersion> list = new ArrayList<TerminalVersion>();
					Set<String> set = terminalMap.keySet();
					for (String key : set) { 
						list.add(terminalMap.remove(key));
					}
					long st = System.currentTimeMillis();
					long s2 = 0;
					try {
						oracleJdbcService.saveOrUpdateTerminalVersion(list, batchSize); 
						s2 = System.currentTimeMillis();
						oracleJdbcService.updateTernimalVersion(list, batchSize);
					} catch (Exception e) {
						logger.error("批量存储终端版本数据异常:" + e.getMessage(), e);
					}
					list.clear();
					long et = System.currentTimeMillis();
					terminalLastTime = et;
					logger.info("--saveBatchTerminalVersion--批量处理[{}]条终端版本数据, 总耗时:[{}]ms, 存储最新版本耗时:[{}]ms , 更新终端版本耗时:[{}]ms", terminalSize, et - st, s2 -st, et -s2);
				}
			} catch (Exception e) {
				logger.error("终端版本存储线程错误:" + e.getMessage(), e);
			}

			try {
//				10秒输出一次线程状态
				if(currentTime - lastTime >= 10000){
					logger.info("---TerminalVersionStorageThread---终端版本存储线程当前状态:终端版本队列[{}]条", terminalMap.size());
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
	 * 加入终端版本队列
	 * @param terminalVersion
	 */
	public void putTerminalVersion(String vid, TerminalVersion terminalVersion) {
		try {
			TerminalVersion tv = terminalMap.get(vid);
			if(tv != null){
				tv.update(terminalVersion);
			} else {
				terminalMap.put(vid, terminalVersion);
			}
		} catch (Exception e) {
			logger.error(e.getMessage());
		}
	}
}

