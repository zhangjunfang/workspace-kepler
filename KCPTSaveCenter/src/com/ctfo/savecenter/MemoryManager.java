package com.ctfo.savecenter;

import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.beans.ServiceUnit;
import com.ctfo.savecenter.dao.MonitorDBAdapter;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.util.CDate;

/**
 * 缓存维护线程
 *  10分钟运行一次
 * 
 * @author Administrator
 * 
 */
public class MemoryManager extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(MemoryManager.class);

	public boolean isRunning = true;// 线程运行标志

	public MemoryManager() {

	}

	@Override
	public void run() {
		long time = CDate.getCurrentUtcMsDate();
		while (isRunning) {
			try {
				time = CDate.getCurrentUtcMsDate();
				sleep(1 * 60 * 1000);
//				缓存、数据库车辆服务已删除信息同步
				Map<Long, Long> deleteMap = MonitorDBAdapter.queryDeleteVehicle(time);
				if (deleteMap != null && deleteMap.size() > 0) {
					Map<String, ServiceUnit> map = TempMemory.getVehicleMap();
					Set<Map.Entry<String, ServiceUnit>> set = map.entrySet();
					String key = null;
					ServiceUnit serviceUnit = null;
					for (Iterator<Map.Entry<String, ServiceUnit>> it = set.iterator(); it.hasNext();) {
						Map.Entry<String, ServiceUnit> entry = (Map.Entry<String, ServiceUnit>) it.next();
						key = entry.getKey();
						serviceUnit = entry.getValue();
						if (deleteMap.get(serviceUnit.getVid()) != null) {
							TempMemory.deleteVehicleMapValue(key);
							logger.info("同步删除车辆:" + key + ";vid:" + serviceUnit.getVid() + " 成功。");
						}
					}// End for
				}
//				更新缓存中3G手机号对应的车辆信息
				MonitorDBAdapter.update3GPhotoVehicleInfo();
//				logger.error("-----vehicleInfo----"+TempMemory.getVehicleMap().get("E001_15290424036").getVid());

			} catch (Exception e) {
				logger.error("缓存维护线程错误" + e.getMessage());
			}
		}
	}

}
