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

import com.ctfo.statusservice.model.AlarmStart;
import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.service.OracleJdbcService;
import com.ctfo.statusservice.service.RedisService;


/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： statusservice		</li><br>
 * <li>时        间：2013-7-2  下午4:21:48	</li><br>
 * </ul>
 *****************************************/
public class AlarmStartStorageThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(AlarmStartStorageThread.class);
	/** 报警开始队列 	*/
	private ArrayBlockingQueue<AlarmStart> alarmStartQueue = new ArrayBlockingQueue<AlarmStart>(100000);
	/** oracle数据库操作类 	 */
	private OracleJdbcService oracleJdbcService;
	/** redis数据库操作类	 */
	private RedisService redisService;
	/** 批量提交间隔时间	 */
	private int batchSize;
	/** 报警开始批量提交数量	 */
	private long startBatchTime;
	/** redis报警存放过期秒数	 */
	private int expiredSeconds;
	
	/** 处理报警开始最近时间	 */
	private long startLastTime = System.currentTimeMillis();
	/** 状态最近时间	 */
	private long statusLastTime = System.currentTimeMillis();

	/*****************************************
	 * <li>描 述：初始化方法</li><br>
	 * <li>参 数：@param oracleJdbcService 
	 * <li>参 数：@param expiredSeconds 
	 * <li>参 数：@param threadId
	 *****************************************/
	public AlarmStartStorageThread(OracleProperties oracleProperties, int expiredSeconds, long alarmBatchTime, int alarmBatchSize) {
		super("AlarmStartStorageThread");
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.redisService = new RedisService(true);
		this.expiredSeconds = expiredSeconds;
		this.startBatchTime = alarmBatchTime * 1000;
		this.batchSize = alarmBatchSize;
	}
	
	
	@Override
	public void run() {
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				int queueSize = alarmStartQueue.size();
//				判断是否符合时间、批量提交条件
				if(queueSize > 0 && ((currentTime - startLastTime) > startBatchTime)){
					List<AlarmStart> list = new ArrayList<AlarmStart>();
					for (int i = 0; i < queueSize; i++) {
						list.add(alarmStartQueue.poll());
					}
					long st = System.currentTimeMillis();
					long s2 = 0;
					try {
						oracleJdbcService.saveAlarmStart(list, batchSize);
						s2 = System.currentTimeMillis();
						redisService.saveAlarmStartList(list, expiredSeconds);
//						发送实时报警,监管报警到实时服务
//						String sendAlarmStr = 
						sendReadTimeAlarmAndPccAlarm(list);
//						if(sendAlarmStr != null){
//							SendMsgManage.getSendMsgThread().putDataMap(sendAlarmStr);
//						}
					} catch (Exception e) {
						logger.error("批量存储报警开始数据异常:" + e.getMessage(), e);
					}
					list.clear();
					long et = System.currentTimeMillis();
					startLastTime = et;
					
					long ctime = et - st;
					long otime = s2 -st;
					long rtime = et -s2;
					logger.info("--saveBatchAlarmStart--批量存储["+queueSize+"]条报警开始数据, 总耗时:["+ctime+"]ms, oracle存储耗时:["+otime+"]ms , redis存储耗时:["+rtime+"]ms");
				} else {
//					暂停1毫秒
					Thread.sleep(1);
				}
			} catch (Exception e) {
				logger.error("报警处理线程错误:" + e.getMessage(), e);
			}

			try {
//				60秒输出一次线程状态
				if(currentTime - statusLastTime > 60000){
					int size = alarmStartQueue.size();
					String status =  "报警开始队列[" + size + "]条";
					long sTime = startBatchTime/1000;
					logger.info("---AlarmStartStorageThread---报警开始批量提交数量[{}]条, 报警批量提交开始[{}]秒, {}", batchSize, sTime, status);
					statusLastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error(e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取报警开始中的实时报警和监管报警信息
	 * @param list
	 * @return
	 */
	private void sendReadTimeAlarmAndPccAlarm(List<AlarmStart> list) {
		if(list != null && list.size() > 0){
//			StringBuffer sb = new StringBuffer(1024);
			for(AlarmStart start : list){
				if(start.getRealTimeAlarmCommand() != null){
					SendMsgManage.getSendMsgThread().putDataMap(start.getRealTimeAlarmCommand());
					logger.debug("实时告警开始[{}]", start.getRealTimeAlarmCommand());
				}
				if(start.getPccAlarmCommand() != null){
					SendMsgManage.getSendMsgThread().putDataMap(start.getPccAlarmCommand());
					logger.debug("PCC告警开始[{}]", start.getPccAlarmCommand());
				}
			}
//			if(sb.length() > 1){
//				return sb.toString();
//			} else {
//				return null;
//			}
		}
//		return null;
	}


	/****************************************
	 * <li>描 述：缓存报警开始</li><br>
	 * <li>时 间：2013-7-2 下午4:33:59</li><br>
	 * <li>参数： @param packet</li><br>
	 * 
	 *****************************************/
	public void putDataMap(AlarmStart alarmStart) {
		try {
			alarmStartQueue.put(alarmStart);
		} catch (InterruptedException e) {
			logger.error("向报警队列插入元素异常:", e);
		}
	}

}