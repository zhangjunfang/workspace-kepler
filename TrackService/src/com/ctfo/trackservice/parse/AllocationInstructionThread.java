/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse AllocationInstruction.java	</li><br>
 * <li>时        间：2013-9-9  下午2:06:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.parse;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.common.ConfigLoader;
import com.ctfo.trackservice.handler.TrackAnalysisThread;
import com.ctfo.trackservice.util.Constant;

/*****************************************
 * <li>描 述：指令分配线程
 * 
 *****************************************/
public class AllocationInstructionThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(AllocationInstructionThread.class);
	/** 数据缓冲队列 */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程数量 */
	private int threadSize;
	/** 计数器	  */
	private int index;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** 轨迹分析线程组 */
	private TrackAnalysisThread[] trackAnalysisThreads;
	/** 负载列表  */
	private Map<String, Integer> map = new HashMap<String, Integer>();
	/** 负载打印最后时间 */
	private long  modListTime = System.currentTimeMillis();
//	/** 数据库接口 */
//	private OracleJdbcService oracleJdbcService;
	/** 负载均衡MD5 */
	private MessageDigest md5;
	/** 负载均衡显示(默认不显示) */
	private boolean load = false;
	
	
	/*****************************************
	 * <li>描 述：初始化指令分配线程</li><br>
	 * <li>参 数：@param threadSize 线程数量 
	 * <li>参 数：@param submitFrequency 提交间隔 
	 * <li>参数：@throws Exception
	 *****************************************/
	public AllocationInstructionThread() throws Exception {
		try {
			setName("AllocationInstructionThread");
			threadSize = Integer.parseInt(ConfigLoader.config.get("threadSize")); 	// 线程数
			load = Boolean.parseBoolean(ConfigLoader.config.get("loadView"));;	// 显示负载
			int speedLimit = Integer.parseInt(ConfigLoader.config.get("speedLimit"));	// 限速阀值
			int timeLimit = Integer.parseInt(ConfigLoader.config.get("timeLimit"));		// 时间阀值
			md5 = MessageDigest.getInstance("MD5");

			// 创建线程数组和线程
			trackAnalysisThreads = new TrackAnalysisThread[threadSize];
			for (int i = 0; i < this.threadSize; i++) {
				trackAnalysisThreads[i] = new TrackAnalysisThread(i, speedLimit, timeLimit);
				trackAnalysisThreads[i].start();
			}
			
			logger.info("数据分发线程启动 - 速度阀值[{}]km/h, 时间阀值[{}]分钟" , speedLimit/10, timeLimit); 
		} catch (Exception e) {
			logger.error("数据分发线程启动异常:" + e.getMessage(), e);
			throw new Exception("数据分发线程启动异常:" + e.getMessage(), e);
		}
	}

	public void addData(Map<String, String> data) {
		try {
			dataQueue.put(data);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	public int getQueueSize() {
		return dataQueue.size();
	}

	public void run() {
		String vId = null;
		Map<String, String> dataMap = null;
		
		while (true) {
			try {
				dataMap = dataQueue.take();
				vId = dataMap.get(Constant.VID);
				int threadId = hashMod(vId, threadSize); // 线程负载均衡算法
				
				trackAnalysisThreads[threadId].putDataMap(dataMap);
				
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					logger.info("--allocation--数据分配10s处理:[" + index +"]条, 缓存区:["+ getQueueSize() +"]条");
					index = 0;
					lastTime = currentTime;
				}
				
				// 显示负载分布状态
				if(load){
					String node = String.valueOf(threadId);
					Integer i = map.get(node);
					if(i == null){
						map.put(node, 1);
					} else {
						map.put(node, i + 1);
					}
					
					long current = System.currentTimeMillis();
					if((current - modListTime) > 60000){
						StringBuffer sb = new StringBuffer();
						for(Map.Entry<String, Integer> m : map.entrySet()){
							sb.append("----modstatus----Thread-"+ m.getKey() + "---size:"+ m.getValue() + "\r\n");
						}
						logger.info("\r\n----modstatus----1分钟内各线程命中状态-------\r\n" + sb.toString());
						map.clear();
						modListTime = System.currentTimeMillis();
					}
				}
				index++;
			} catch (Exception e) {
				logger.error("指令分配线程异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 负载均衡算法 - 统一哈希算法
	 * @param vId
	 * @return
	 */
	public int hashMod(String vId,int threadSize) {
		byte[] b = computeMd5(vId);
		int hashNum = (int) (hash(b, 1) %  threadSize);
		return Math.abs(hashNum); 
	}
	/**
	 * 获取MD5密钥
	 */
	public byte[] computeMd5(String key) {
		md5.reset();
		byte[] keyBytes = null;
		try {
			keyBytes = key.getBytes("UTF-8");
		} catch (UnsupportedEncodingException e) {
			//发送异常时默认使用0线程进行处理
			keyBytes = new byte[]{-124, -119, -108, -56, 93, -64, -103, -19, -128, 70, -20, 40, 86, 40, 84, -60};
		}
		md5.update(keyBytes);
		return md5.digest();
	}
	/**
	 * 获取MD5的HASH域值
	 * @param digest
	 * @param nTime
	 * @return
	 */
	public long hash(byte[] digest, int nTime) {
		long rv = ((long) (digest[3+nTime*4] & 0xFF) << 24)
				| ((long) (digest[2+nTime*4] & 0xFF) << 16)
				| ((long) (digest[1+nTime*4] & 0xFF) << 8)
				| (digest[0+nTime*4] & 0xFF);
		
		return rv & 0xffffffffL; /* Truncate to 32-bits */
	}

//	public static void main(String[] args) throws Exception { 
//		AllocationInstructionThread a = new AllocationInstructionThread(0,new OracleProperties(), true, true, 140, 1 );
//		System.out.println(""+a.hashMod("258936",5));
//	}
}
