/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.savecenter AlarmBufferThread.java	</li><br>
 * <li>时        间：2013-7-22  上午10:54:05	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.savecenter;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.dao.MonitorDBAdapter;

/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.savecenter AlarmBufferThread.java	</li><br>
 * <li>时        间：2013-7-22  上午10:54:05	</li><br>
 * </ul>
 *****************************************/
public class OrgAlarmSettingThread implements Runnable{
	private static final Logger logger = LoggerFactory.getLogger(OrgAlarmSettingThread.class);
	/**
	 * 
	 */
	public void run() {
		try{
			MonitorDBAdapter.updateVehicleAlarmSetting();
		}catch(Exception e){
			logger.error("更新企业告警设置线程异常:" + e.getMessage(),e);
		}
	}
	
	

}
