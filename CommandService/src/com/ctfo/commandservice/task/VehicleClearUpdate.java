/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.task VehicleClearUpdate.java	</li><br>
 * <li>时        间：2013-9-22  上午9:42:56	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.task;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.service.OracleJdbcService;

/*****************************************
 * <li>描        述：车辆清除更新任务		
 * 
 *****************************************/
public class VehicleClearUpdate extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(VehicleClearUpdate.class);
	/**	oracle连接管理	*/
	private OracleJdbcService oracleJdbcService;
	/**	更新时间(单位:小时 01-23)	*/
	private String clearUpdateTime;
	
	public VehicleClearUpdate() {
	}


	public VehicleClearUpdate(OracleProperties oracleProperties, String clearUpdateTime) {
		super("VehicleClearUpdate");
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.clearUpdateTime = clearUpdateTime;
	}

	/*****************************************
	 * <li>描        述：车辆清除更新任务 		</li><br>
	 * <pre>
	 * 每天凌晨3点对缓存的车辆信息进行重新更新，删除所有缓存，然后更新最新数据。
	 * 解决部分车辆删除后还存在缓存中的问题
	 * </pre>
	 * <li>时        间：2013-9-22  上午9:44:15	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void run(){
		while(true){
			try{
				long currentTime = System.currentTimeMillis();
				SimpleDateFormat sdf = new SimpleDateFormat("HH");
				String hoursStr = sdf.format(new Date());
				if(hoursStr.equals(clearUpdateTime.trim())){ 
					oracleJdbcService.clearUpdateVehicle();
					logger.info("车辆清除更新任务结束,耗时:(" + (System.currentTimeMillis() -currentTime) + ")ms" );
//					任务完成后休眠23个小时
					Thread.sleep(82800000);
				}else{
//					休眠10分钟
					Thread.sleep(600000);
				}
			}catch(Exception e){
				logger.info("车辆清除更新任务异常:" + e.getMessage(), e);
				try {
					Thread.sleep(600000);
				} catch (InterruptedException e1) {
					logger.error("车辆清除更新任务--线程休眠异常:" + e1.getMessage(), e1);
				}
			}
		}
	}
}
