/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.savecenter.addin.kcpt.trackmanager AlarmHandlerThread.java	</li><br>
 * <li>时        间：2013-7-2  下午4:21:48	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.model.Region;
import com.ctfo.statusservice.service.OracleJdbcService;


/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： statusservice		</li><br>
 * <li>时        间：2013-7-2  下午4:21:48	</li><br>
 * </ul>
 *****************************************/
public class RegionStorageThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(RegionStorageThread.class);
	/** 区域队列 	*/
	private ArrayBlockingQueue<Region> regionQueue = new ArrayBlockingQueue<Region>(100000);
	/** oracle数据库操作类 	 */
	private OracleJdbcService oracleJdbcService;
	/** 批量提交间隔时间	 */
	private long batchTime;
	/** 批量提交数量	 */
	private int batchSize;
	/** 处理报警开始最近时间	 */
	private long startLastTime = System.currentTimeMillis();
	/** 状态最近时间	 */
	private long statusLastTime = System.currentTimeMillis();

	/*****************************************
	 * <li>描 述：初始化方法</li><br>
	 * <li>参 数：@param oracleJdbcService 
	 * <li>参 数：@param threadId
	 *****************************************/
	public RegionStorageThread(OracleProperties oracleProperties, Integer batchSize, Long batchTime) {
		super("RegionStorageThread");
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.batchSize = batchSize;
		this.batchTime = batchTime * 1000;
	}
	
	
	@Override
	public void run() {
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				int queueSize = regionQueue.size();
//				判断是否符合时间、批量提交条件
				if(queueSize > 0 && ((currentTime - startLastTime) >= batchTime)){
					List<Region> list = new ArrayList<Region>();
					for (int i = 0; i < queueSize; i++) {
						list.add(regionQueue.poll());
					}
					long st = System.currentTimeMillis();
					try {
						oracleJdbcService.saveRegion(list, batchSize);
					} catch (Exception e) {
						logger.error("跨域数据异常:" + e.getMessage(), e);
					}
					list.clear();
					long et = System.currentTimeMillis();
					startLastTime = et;
					long ctime = et -st;
					logger.info("--saveBatchRegion--批量存储["+queueSize+"]条跨域数据, 总耗时:["+ctime+"]ms");
				} else {
//					暂停1毫秒
					Thread.sleep(1);
				} 
			} catch (Exception e) {
				logger.error("跨域处理线程错误:" + e.getMessage(), e);
			}
			
			try {
//				10秒输出一次线程状态
				if(currentTime - statusLastTime > 60000){
					logger.info("RegionStorageThread 跨域存储线程当前状态:批量提交数量[{}]条, 批量提交时间[{}]秒, {}",  batchSize, batchTime/1000, getStatus());
					statusLastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error(e.getMessage(), e);
			}
		}
	}

	/****************************************
	 * <li>描 述：向跨域队列插入元素</li><br>
	 * <li>时 间：2013-7-2 下午4:33:59</li><br>
	 * <li>参数： @param packet</li><br>
	 * 
	 *****************************************/
	public void putDataMap(Region region) {
		try {
			regionQueue.put(region);
		} catch (InterruptedException e) {
			logger.error("向跨域队列插入元素异常:", e);
		}
	}

	/*****************************************
	 * <li>描 述：获得队列大小</li><br>
	 * <li>时 间：2013-7-2 下午4:36:57</li><br>
	 * <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public String getStatus() {
		StringBuilder sb = new StringBuilder();
		sb.append("跨域队列排队[").append(regionQueue.size()).append("]条");
		return sb.toString();
	}
}