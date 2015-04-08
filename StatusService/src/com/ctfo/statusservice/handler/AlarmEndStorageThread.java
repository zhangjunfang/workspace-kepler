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

import com.ctfo.statusservice.model.AlarmEnd;
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
public class AlarmEndStorageThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(AlarmEndStorageThread.class);
	/** 报警结束队列 	*/
	private ArrayBlockingQueue<AlarmEnd> alarmEndQueue = new ArrayBlockingQueue<AlarmEnd>(100000);
	/** oracle数据库操作类 	 */
	private OracleJdbcService oracleJdbcService;
	/** redis数据库操作类	 */
	private RedisService redisService;
	/** 批量提交间隔时间	 */
	private int batchSize;
	/** 报警结束批量提交数量	 */
	private long endBatchTime;
	/** 处理报警结束最近时间	 */
	private long endLastTime = System.currentTimeMillis();
	/** 状态最近时间	 */
	private long statusLastTime = System.currentTimeMillis();

	/*****************************************
	 * <li>描 述：初始化方法</li><br>
	 * <li>参 数：@param oracleJdbcService 
	 * <li>参 数：@param threadId
	 *****************************************/
	public AlarmEndStorageThread(OracleProperties oracleProperties, long alarmBatchTime, int alarmBatchSize) {
		super("AlarmEndStorageThread");
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.redisService = new RedisService(false);
		this.endBatchTime = alarmBatchTime * 1000;
		this.batchSize = alarmBatchSize;
	}
	
	@Override
	public void run() {
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				int queueSize = alarmEndQueue.size();
//				判断是否符合时间、批量提交条件
				if(queueSize > 0 && ((currentTime - endLastTime) >= endBatchTime )){
					List<AlarmEnd> list = new ArrayList<AlarmEnd>();
					for (int i = 0; i < queueSize; i++) {
						list.add(alarmEndQueue.poll());
					}
					long current = System.currentTimeMillis();
//					报警结束必须在报警开始提交后处理：当前时间 > （报警结束系统时间 + 批量提交间隔时间 + 5秒延迟）说明可以存储否则放回队列 
					List<AlarmEnd> updateList = new ArrayList<AlarmEnd>();
					for(AlarmEnd alarmEnd : list){
						if(current > (alarmEnd.getSysUtc() + endBatchTime + 5000)){
							updateList.add(alarmEnd);// 只更新存储了报警开始的数据
						} else {
							alarmEndQueue.put(alarmEnd);
						}
					}
					long st = System.currentTimeMillis();
					long s2 = st;
					int updateSize = updateList.size();
					try {
						if(updateSize > 0){
							oracleJdbcService.saveAlarmEnd(updateList, batchSize);
							s2 = System.currentTimeMillis();
							redisService.saveAlarmEndList(updateList);
//							发送实时报警
//							String sendAlarmStr = 
							sendReadTimeAlarm(list);
//							if(sendAlarmStr != null){
//								SendMsgManage.getSendMsgThread().putDataMap(sendAlarmStr);
//							}
						}
					} catch (Exception e) {
						logger.error("批量存储报警开始数据异常:" + e.getMessage(), e);
					}
					list.clear();
					updateList.clear();
					long et = System.currentTimeMillis();
					endLastTime = et;
					long ctime = et - st;
					long otime = s2 -st;
					long rtime = et -s2;
					logger.info("--saveBatchAlarmEnd----更新报警:队列数量["+queueSize+"]条, 可处理数["+updateSize+"]条, 总耗时:["+ctime+"]ms, oracle存储耗时:["+otime+"]ms , redis存储耗时:["+rtime+"]ms");
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
					long eTime = endBatchTime/1000;
					int size = alarmEndQueue.size();
					String status = "报警结束队列:[" + size + "]条";
					logger.info("---AlarmEndStorageThread---报警结束批量提交数量[{}]条, 间隔时间[{}]秒, {}", batchSize, eTime, status);
					statusLastTime = System.currentTimeMillis();
				}

			} catch (Exception e) {
				logger.error(e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取报警结束中的实时报警信息
	 * @param list
	 * @return
	 */
	private void sendReadTimeAlarm(List<AlarmEnd> list) {
		if (list != null && list.size() > 0) {
//			StringBuffer sb = new StringBuffer(1024);
			for (AlarmEnd end : list) {
				if (end.getRealTimeAlarmCommand() != null) {
					SendMsgManage.getSendMsgThread().putDataMap(end.getRealTimeAlarmCommand());
//					sb.append();
					logger.debug("实时告警结束[{}]", end.getRealTimeAlarmCommand());
				}
			}
//			if (sb.length() > 1) {
//				return sb.toString();
//			} else {
//				return null;
//			}
		}
//		return null;
	}

	/****************************************
	 * <li>描 述：缓存报警结束</li><br>
	 * <li>时 间：2013-7-2 下午4:33:59</li><br>
	 * <li>参数： @param packet</li><br>
	 * 
	 *****************************************/
	public void putDataMap(AlarmEnd alarmEnd) {
		try {
			alarmEndQueue.put(alarmEnd);
		} catch (InterruptedException e) {
			logger.error("向报警队列插入元素异常:", e);
		}
	}
	
	/*****************************************
	 * <li>描 述：获得队列大小</li><br>
	 * <li>时 间：2013-7-2 下午4:36:57</li><br>
	 * <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public String getStatus() {
		int size = alarmEndQueue.size();
		return "报警结束队列:[" + size + "]条";
	}
}