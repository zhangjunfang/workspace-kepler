/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.task VehicleSyncCacheTask.java	</li><br>
 * <li>时        间：2013-9-10  上午11:13:45	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.task;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.service.OracleJdbcService;


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
		//			更新缓存间隔 (默认更新比正常时间多一分钟)
					Long interval = currentTime - intervalTime - 60000;
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
	
	public OracleJdbcService getOracleJdbcService() {
		return oracleJdbcService;
	}
	public void setOracleJdbcService(OracleJdbcService oracleJdbcService) {
		this.oracleJdbcService = oracleJdbcService;
	}
	public int getIntervalTime() {
		return intervalTime;
	}
	public void setIntervalTime(int intervalTime) {
		this.intervalTime = intervalTime;
	}
}
