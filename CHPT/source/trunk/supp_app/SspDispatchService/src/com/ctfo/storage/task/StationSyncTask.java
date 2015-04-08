package com.ctfo.storage.task;

import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.io.SendMsgThread;
import com.ctfo.storage.service.RedisService;
import com.ctfo.storage.util.TaskAdapter;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 车厂数据同步任务<br>
 * 描述： 车厂数据同步任务<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-12-2</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class StationSyncTask extends TaskAdapter {

	/** 日志 */
	private static Logger logger = LoggerFactory.getLogger(StationSyncTask.class);

	private RedisService redisService = new RedisService();

	public StationSyncTask() {
		setName("StationSyncTask");
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.storage.util.TaskAdapter#init()
	 */
	@Override
	public void init() {
		this.setName("StationSyncTask");
		logger.info("车厂数据同步程序启动初始化！");
		this.execute();
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.storage.util.TaskAdapter#execute()
	 */
	@Override
	public void execute() {
		long start = System.currentTimeMillis();
		int processSize = 0;
		try {
			Set<String> keys = redisService.getkeysLike("CF_");
			for (String key : keys) {
				String stationId = key.substring(3);
				logger.info("车厂数据同步任务读取服务站ID：" + stationId);
				if (!SendMsgThread.getMsgMap().containsKey(stationId)) { // 判断客户端是否连接
					logger.info("车厂数据同步任务读取服务站ID：" + stationId + " 没有登录");
					continue;
				}
				Set<String> data = redisService.getStationList(key);
				for (String message : data) {
					SendMsgThread.put(message);
					processSize++;
				}
				redisService.delKey(key); // 同步完删除redis数据
			}
			logger.info("--StationSyncTask--车厂数据同步任务执行完成, ---同步数据:[{}]条, ---总耗时:[{}]秒 ", processSize, (System.currentTimeMillis() - start) / 1000.0);
		} catch (Exception e) {
			logger.debug("--StationSyncTask--车厂数据同步任务执行异常 ", e);
		}
	}
}
