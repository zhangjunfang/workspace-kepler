/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.task VehicleSyncCacheTask.java	</li><br>
 * <li>时        间：2013-9-10  上午11:13:45	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.task;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.service.OracleJdbcService;


/*****************************************
 * <li>描        述：同步更新或者新增车辆缓存任务		
 * 
 *****************************************/
public class SyncUpdateOrAddVehicheCacheTask  extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(SyncUpdateOrAddVehicheCacheTask.class);
	/**	oracle连接管理	*/
	private OracleJdbcService oracleJdbcService;
	/**	间隔时间(单位:ms毫秒)	*/
	private int intervalTime;
	/**	最近时间	*/
	private long lastTime = System.currentTimeMillis();
	
	public SyncUpdateOrAddVehicheCacheTask() {
	}

	public SyncUpdateOrAddVehicheCacheTask(OracleProperties oracleProperties, Integer intervalTime) {
		super("SyncUpdateOrAddVehicheCacheTask");
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.intervalTime = intervalTime;
	}

	/****************************************
	 * <li>描        述：同步不存在车辆缓存 		</li><br>
	 * <li>时        间：2013-9-10  上午11:17:21	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void run() {
		while(true){
			try{
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > intervalTime){
		//			更新缓存间隔 (允许10秒误差)
					Long interval = currentTime - intervalTime - 10000;
					oracleJdbcService.updateVehilceCache(interval);
//					logger.info("同步更新或者新增车辆缓存结束,耗时:(" + (System.currentTimeMillis() -currentTime) + ")ms" );
					lastTime = System.currentTimeMillis();
				}else{
					Thread.sleep(10000);
				}
			}catch(Exception e){
				logger.error("同步更新或者新增车辆缓存异常:" + e.getMessage(), e);
				try {
					Thread.sleep(10000);
				} catch (InterruptedException e1) {
					logger.error("同步更新或者新增车辆--线程休眠异常:" + e1.getMessage(), e1);
				}
			}
		}
	}

	/*------------------------getter & setter--------------------------*/
}
