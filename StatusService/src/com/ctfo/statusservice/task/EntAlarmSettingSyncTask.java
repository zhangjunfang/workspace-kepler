/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService		</li><br>
 * <li>文件名称：com.ctfo.statusservice.task EntAlarmSettingSyncTask.java	</li><br>
 * <li>时        间：2013-9-27  下午2:30:46	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.task;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.service.OracleJdbcService;

/*****************************************
 * <li>描        述:企业告警设置同步任务		
 * 
 *****************************************/
public class EntAlarmSettingSyncTask extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(EntAlarmSettingSyncTask.class);
	/**	oracle连接管理	*/
	private OracleJdbcService oracleJdbcService;
	/**	间隔时间	*/
	private long intervalTime;
	/**	最近时间	*/
	private long lastTime = System.currentTimeMillis();


	
	public EntAlarmSettingSyncTask() {
	}

	public EntAlarmSettingSyncTask(OracleProperties oracleProperties, Long intervalTime){
		super("EntAlarmSettingSyncTask");
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.intervalTime = intervalTime; 
	}
	
	public void run(){
		while(true){
			try {
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > intervalTime){
					this.oracleJdbcService.updateVehicleAlarmSetting();
//					logger.info("企业告警设置同步任务结束,耗时:(" + (System.currentTimeMillis() -currentTime) + ")ms" );
					lastTime = System.currentTimeMillis();
				} else {
					Thread.sleep(10000);
				}
			} catch (Exception e) {
				logger.error("企业告警设置同步任务异常:" + e.getMessage(), e);
				try {
					Thread.sleep(10000);
				} catch (InterruptedException e1) {
					logger.error("企业告警设置同步任务--线程休眠异常:" + e1.getMessage(), e1);
				}
			}
		}
	}
	
	
	public OracleJdbcService getOracleJdbcService() {
		return oracleJdbcService;
	}
	public void setOracleJdbcService(OracleJdbcService oracleJdbcService) {
		this.oracleJdbcService = oracleJdbcService;
	}
	public long getIntervalTime() {
		return intervalTime;
	}
	public void setIntervalTime(long intervalTime) {
		this.intervalTime = intervalTime;
	}
	
}
