/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse AllocationInstruction.java	</li><br>
 * <li>时        间：2013-9-9  下午2:06:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.parse;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.handler.AlarmEndStorageThread;
import com.ctfo.statusservice.handler.AlarmHandleThread;
import com.ctfo.statusservice.handler.AlarmStartStorageThread;
import com.ctfo.statusservice.handler.RegionHandleThread;
import com.ctfo.statusservice.handler.RegionStorageThread;
import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.model.Pack;
import com.ctfo.statusservice.service.OracleJdbcService;
import com.ctfo.statusservice.util.Cache;

/*****************************************
 * <li>描 述：指令分配线程
 * 
 *****************************************/
public class AllocationInstructionThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(AllocationInstructionThread.class);
	/** 数据缓冲队列 */
	private ArrayBlockingQueue<Pack> dataQueue = new ArrayBlockingQueue<Pack>(100000);
	/** 线程数量 */
	private int threadSize;
	/** 计数器	  */
	private int index;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** 报警处理线程组 */
	private AlarmHandleThread[] alarmHandleThreads;
	/** 跨域处理线程组 */
	private RegionHandleThread[] regionHandleThreads;
	/** 负载均衡MD5 */
	private MessageDigest md5;
	/** 负载均衡显示(默认不显示) */
	private boolean load = false;
	/** 负载列表  */
	private Map<String, Integer> map = new HashMap<String, Integer>();
	/** 负载打印最后时间 */
	private long  modListTime = System.currentTimeMillis();
	
	/*****************************************
	 * <li>描 述：初始化指令分配线程</li><br>
	 * <li>参 数：@param threadSize 线程数量 
	 * <li>参 数：@param submitFrequency 提交间隔 
	 * <li>参数：@throws Exception
	 *****************************************/
	public AllocationInstructionThread(int threadSize, OracleProperties oracleProperties, String treeName,  long alarmBatchTime, int alarmBatchSize, int regionBatchSize, long regionBatchTime, int expiredSeconds, boolean load) throws Exception {
		super("AllocationInstructionThread");
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
		
//		更新车辆报警设置
		oracleJdbcService.updateVehicleAlarmSetting();
//		初始化缓存所有车辆信息
		oracleJdbcService.initAllVehilceCache();
//		初始化缓存所有3G视频终端服务信息
		oracleJdbcService.update3GPhotoVehicleInfo();
//		初始化车队对应父组织信息
		oracleJdbcService.orgParentSync();
		
//		构建全国拓扑树
		String filePath = getTreeFilePath(treeName);
		Cache.buildTree(filePath);
		
		
		// 报警结束存储线程
		AlarmStartStorageThread alarmStartStorageThread = new AlarmStartStorageThread(oracleProperties, expiredSeconds, alarmBatchTime, alarmBatchSize);
		alarmStartStorageThread.start();
	
		// 报警开始存储线程
		AlarmEndStorageThread alarmEndStorageThread = new AlarmEndStorageThread(oracleProperties, alarmBatchTime, alarmBatchSize);
		alarmEndStorageThread.start();
		
		
		RegionStorageThread regionStorageThread = new RegionStorageThread(oracleProperties, regionBatchSize, regionBatchTime);
		regionStorageThread.start();
		alarmHandleThreads = new AlarmHandleThread[threadSize];
		regionHandleThreads = new RegionHandleThread[threadSize];
		for (int i = 0; i < this.threadSize; i++) {
			alarmHandleThreads[i] = new AlarmHandleThread(i, alarmStartStorageThread, alarmEndStorageThread);
			alarmHandleThreads[i].start();
			regionHandleThreads[i] = new RegionHandleThread(i, regionStorageThread);
			regionHandleThreads[i].start(); 
		}
	}

	/*****************************************
	 * <li>描        述：获得树文件全路径 		</li><br>
	 * <li>时        间：2013-9-23  下午5:16:49	</li><br>
	 * <li>参数： @param treeName
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private String getTreeFilePath(String treeName) {
		String currentDir = System.getProperty("user.dir");
		String separator = System.getProperty("file.separator");
		return currentDir + separator + treeName;
	}

	public void addData(Pack pack) {
		try {
			dataQueue.put(pack);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	public int getQueueSize() {
		return dataQueue.size();
	}

	public void run() {
		String vId = null;
		Pack pack = null;
		while (true) {
			try {
				pack = dataQueue.take();
//				vId = dataMap.get(Constant.VID);
				vId = String.valueOf(pack.getVid()); 
//				hash负载均衡
				int threadId = hashMod(vId, threadSize);
//				threadId = Math.abs((int) (Long.parseLong(vId) % threadSize));
//				处理报警
				alarmHandleThreads[threadId].putDataMap(pack);
//				处理跨域统计
				regionHandleThreads[threadId].putDataMap(pack);
				
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					int size = getQueueSize();
					logger.info("--allocation--数据解析10s处理:[" + index +"]条, 缓存区:["+ size +"]条");
					index = 0;
					lastTime = currentTime;
				}
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
	private int hashMod(String vId,int threadSize) {
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

}
