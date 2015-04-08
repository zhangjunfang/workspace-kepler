/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse AllocationInstruction.java	</li><br>
 * <li>时        间：2013-9-9  下午2:06:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.parse;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.dao.OracleConnectionPool;
import com.ctfo.commandservice.handler.CommandHandleThread;
import com.ctfo.commandservice.handler.CustomCommandProcess;
import com.ctfo.commandservice.handler.DriverProcess;
import com.ctfo.commandservice.handler.IccIdUpdateThread;
import com.ctfo.commandservice.handler.TerminalVersionStorageThread;
import com.ctfo.commandservice.handler.TravellingRecorderParseThread;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.service.OracleJdbcService;
import com.ctfo.commandservice.service.OracleService;
import com.ctfo.commandservice.util.Constant;

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
	/** 盲区补传处理线程组 */
	private CommandHandleThread[] commandHandleThreads;
	/** 计数器 */
	private int allIndex;
	/** 上次时间 */
	private long lastTime = System.currentTimeMillis();
	/** 负载均衡MD5 */
	private MessageDigest md5;
	/** 负载均衡显示(默认不显示) */
	private boolean load = false;
	/** 负载列表 */
	private Map<String, Integer> map = new HashMap<String, Integer>();
	/** 负载打印最后时间 */
	private long modListTime = System.currentTimeMillis();

	/*****************************************
	 * <li>描 述：初始化指令分配线程</li><br>
	 * <li>参 数：@param threadSize 线程数量 <li>参 数：@param submitFrequency 提交间隔 <li>参
	 * 数：@throws Exception
	 *****************************************/
	public AllocationInstructionThread(int threadSize, OracleProperties oracleProperties, int batchSize, long authBatchTime, long terminalBatchTime, int processSize, boolean load,
			boolean init) throws Exception {
		super("AllocationInstructionThread");
		try {
			if (threadSize == 0) {
				throw new Exception("指令分配启动失败,线程启动数为0");
			}
			this.threadSize = threadSize;
			this.load = load;
			try {
				md5 = MessageDigest.getInstance("MD5");
			} catch (NoSuchAlgorithmException e) {
				throw new RuntimeException("MD5 not supported", e);
			}
			OracleJdbcService oracleJdbcService = new OracleJdbcService(oracleProperties);
			if (init) {
				// 初始化缓存所有车辆信息
				oracleJdbcService.initAllVehilceCache();
				// 初始化缓存所有3G视频终端服务信息
				oracleJdbcService.update3GPhotoVehicleInfo();
			}

			/** 终端版本存储线程 */
			TerminalVersionStorageThread terminalVersionStorageThread = new TerminalVersionStorageThread(oracleJdbcService, batchSize, processSize, terminalBatchTime);
			terminalVersionStorageThread.start();

			/** ICC存储线程 */
			IccIdUpdateThread iccIdUpdateThread = new IccIdUpdateThread(oracleJdbcService, batchSize, authBatchTime);
			iccIdUpdateThread.start();
			/** 事故疑点数据处理线程 */
			TravellingRecorderParseThread travellingRecorderParseThread = new TravellingRecorderParseThread(oracleJdbcService, batchSize, processSize);
			travellingRecorderParseThread.start();
			/** 驾驶员信息处理线程 */
			DriverProcess driverProcess = new DriverProcess(oracleProperties);
			driverProcess.start();
			/** 自定义指令处理线程 */
			CustomCommandProcess customCommandProcess = CustomCommandProcess.getInstance();
			customCommandProcess.start();

			OracleService.init();
			
			// 创建线程数组和线程
			commandHandleThreads = new CommandHandleThread[threadSize];
			for (int i = 0; i < this.threadSize; i++) {
				commandHandleThreads[i] = new CommandHandleThread(i, oracleProperties, terminalVersionStorageThread, iccIdUpdateThread, travellingRecorderParseThread);
				commandHandleThreads[i].setDriverProcess(driverProcess);
//				commandHandleThreads[i].setCustomCommandProcess(customCommandProcess);
				commandHandleThreads[i].start(); 
			}
		} catch (Exception e) {
			System.out.println(e.getMessage()); 
			throw new Exception(e);
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
		String vid = null;
		Map<String, String> dataMap = null;
		int threadId = 0;
		int index = 0;
		while (true) {
			try {
				dataMap = dataQueue.take();
				vid = dataMap.get("vid");
				if (vid == null) {
					// 平台数据通过轮询方式让线程处理
					String mtype = dataMap.get(Constant.MTYPE);
					if (mtype != null && mtype.equals("L_PLAT")) {
						index = index + 1 >= threadSize ? 0 : index + 1;
						commandHandleThreads[index].putDataMap(dataMap);
					} else {
						logger.error("指令分配线程异常,错误数据:" + dataMap.get(Constant.COMMAND));
					}
				} else {
					threadId = hashMod(vid, threadSize); // 线程负载均衡算法
					// 业务数据通过VID求模定点处理
					// threadId = Math.abs((int) (Long.parseLong(vid) %
					// threadSize));
					// 轨迹文件
					commandHandleThreads[threadId].putDataMap(dataMap);
				}

				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastTime) > 10000) {
					lastTime = currentTime;
					logger.info("allocation---:{}, 排队:[{}] , 10s秒处理:[{}]条", threadId, getQueueSize(), allIndex);
					allIndex = 0;
				}
				// 显示负载分布状态
				if (load) {
					String node = String.valueOf(threadId);
					Integer i = map.get(node);
					if (i == null) {
						map.put(node, 1);
					} else {
						map.put(node, i + 1);
					}
					long current = System.currentTimeMillis();
					if ((current - modListTime) > 60000) {
						StringBuffer sb = new StringBuffer();
						for (Map.Entry<String, Integer> m : map.entrySet()) {
							sb.append("----modstatus----Thread-" + m.getKey() + "---size:" + m.getValue() + "\r\n");
						}
						logger.info("\r\n----modstatus----1分钟内各线程命中状态-------\r\n" + sb.toString());
						logger.info(OracleConnectionPool.getPoolStackTrace()); 
						map.clear();
						modListTime = System.currentTimeMillis();
					}
				}
				allIndex++;
			} catch (Exception e) {
				logger.error("指令分配线程异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 负载均衡算法 - 统一哈希算法
	 * 
	 * @param vId
	 * @return
	 */
	private int hashMod(String vId, int threadSize) {
		byte[] b = computeMd5(vId);
		int hashNum = (int) (hash(b, 1) % threadSize);
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
			// 发送异常时默认使用0线程进行处理
			keyBytes = new byte[] { -124, -119, -108, -56, 93, -64, -103, -19, -128, 70, -20, 40, 86, 40, 84, -60 };
		}
		md5.update(keyBytes);
		return md5.digest();
	}

	/**
	 * 获取MD5的HASH域值
	 * 
	 * @param digest
	 * @param nTime
	 * @return
	 */
	public long hash(byte[] digest, int nTime) {
		long rv = ((long) (digest[3 + nTime * 4] & 0xFF) << 24) | ((long) (digest[2 + nTime * 4] & 0xFF) << 16) | ((long) (digest[1 + nTime * 4] & 0xFF) << 8)
				| (digest[0 + nTime * 4] & 0xFF);

		return rv & 0xffffffffL; /* Truncate to 32-bits */
	}
}
