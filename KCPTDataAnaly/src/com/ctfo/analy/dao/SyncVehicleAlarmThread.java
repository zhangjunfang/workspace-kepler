package com.ctfo.analy.dao;

import java.util.Date;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.lingtu.xmlconf.XmlConf;

/**
 * 同步数据库报警设置
 * 
 * @author LiangJian 2012-12-12 13:28:03
 */
// public class SyncVehicleAlarmThread extends Thread {
public class SyncVehicleAlarmThread  extends Thread {
	private static final Logger logger = Logger
			.getLogger(SyncVehicleAlarmThread.class);
	// //第一次同步数据库报警配置，是否完成。
	 public static ArrayBlockingQueue<Boolean> complete = new
	 ArrayBlockingQueue<Boolean>(1);
	private int completeNum = 0;// 同步完成次数

	public int getCompleteNum() {
		return completeNum;
	}

	public void setCompleteNum(int completeNum) {
		this.completeNum = completeNum;
	}

	XmlConf config;

	// 是否运行标志
	public boolean isRunning = true;
	int synctime = 0;
	MonitorDBAdapter monitorDBAdapter;

	// 将配置文件中的SQL装入内存
	public SyncVehicleAlarmThread(XmlConf config) throws Exception {
		this.config = config;
		synctime = config.getIntValue("SyncTime");

		monitorDBAdapter = new MonitorDBAdapter();
		monitorDBAdapter.initDBAdapter(config);
	}

	public void run() {
		logger.info("TB-同步数据库报警设置启动");
		long dateTime = System.currentTimeMillis();
		// while (isRunning) {
		try {

			long fullStart = new Date().getTime();

			long vehicleStart = new Date().getTime();
			// 初始化手机号对应报警设置
			logger.debug("TB-开始初始化手机号对应报警设置");
			monitorDBAdapter.synNewVehicle(dateTime);
			monitorDBAdapter.synDelVehicle(dateTime);
			dateTime = System.currentTimeMillis();
			logger.debug("TB-初始化手机号对应报警设置完成，耗时："
					+ (new Date().getTime() - vehicleStart) + "ms");

			long areaStart = new Date().getTime();
			// 初始化VID对应区域报警设置Map
			logger.debug("TB-开始初始化VID对应区域报警设置");
			monitorDBAdapter.queryAreaAlarm();
			logger.debug("TB-初始化VID对应区域报警设置完成，耗时："
					+ (new Date().getTime() - areaStart) + "ms");

			long roadStart = new Date().getTime();
			logger.debug("TB-开始初始化VID对应道路等级报警设置");
			// 初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryRoadAlarm();
			logger.debug("TB-初始化VID对应道路等级报警设置完成，耗时："
					+ (new Date().getTime() - roadStart) + "ms");

			long illeOptStart = new Date().getTime();
			logger.debug("TB-开始初始化VID对应非法运营报警设置");
			// 初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryIllegalOptionsAlarm();
			logger.debug("TB-初始化VID对应非法运营报警设置完成，耗时："
					+ (new Date().getTime() - illeOptStart) + "ms");

			//logger.debug("TB-同步数据库线路报警配置次数：" + completeNum + 1);

			// if(completeNum==0){
			// complete.add(true);//同步成功！
			// }

			long lineStart = new Date().getTime();
			logger.debug("TB-开始初始化VID对应线路报警设置");
			monitorDBAdapter.queryLineAlarm();// 初始化VID对应线路报警设置
			logger.debug("TB-初始化VID对应线路报警设置完成，耗时："
					+ (new Date().getTime() - lineStart) + "ms");
			
			//logger.debug("TB-同步数据库线路报警配置-完成" + completeNum);
			//completeNum++;

			/*long orgAlarmConfStart = new Date().getTime();
			logger.debug("TB-开始初始化企业告警等级设置");
			//初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryOrgAlarmConf();
			logger.debug("TB-初始化企业告警等级设置完成，耗时："+(new Date().getTime()-orgAlarmConfStart)+"ms");*/
			
			long alarmNoticeStart = new Date().getTime();
			logger.debug("TB-开始初始化查询企业车辆告警下发消息内容设置");
			monitorDBAdapter.queryOrgAlarmNotice();
			logger.debug("TB-初始化查询企业车辆告警下发消息内容设置完成，耗时："+(new Date().getTime()-alarmNoticeStart)+"ms");
			
			long overspeedStart = new Date().getTime();
			logger.debug("TB-开始初始化VID对应超速报警设置");
			// 初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryOverspeedAlarmCfg();
			logger.debug("TB-初始化VID对应超速报警设置完成，耗时："
					+ (new Date().getTime() - overspeedStart) + "ms");
			
			long fatigueStart = new Date().getTime();
			logger.debug("TB-开始初始化VID对应疲劳驾驶报警设置");
			// 初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryFatigueAlarmCfg();
			logger.debug("TB-初始化VID对应疲劳驾驶报警设置完成，耗时："
					+ (new Date().getTime() - fatigueStart) + "ms");
			
			long lineStationStart = new Date().getTime();
			logger.debug("TB-开始初始化VID对应站点信息");
			// 初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryLineStationCfg();
			logger.debug("TB-初始化VID对应站点信息完成，耗时："
					+ (new Date().getTime() - lineStationStart) + "ms");
			
			long orgParentStart = new Date().getTime();
			logger.debug("TB-开始初始化企业对应父ID信息");
			// 初始化VID对应道路等级报警设置Map
			monitorDBAdapter.queryOrgParentInfo();
			logger.debug("TB-初始化企业对应父ID信息完成，耗时："
					+ (new Date().getTime() - orgParentStart) + "ms");
			
			//加载需要定时更新的缓存
			while (isRunning) {
				if (completeNum>0){
					fullStart = new Date().getTime();
				}
				long orgAlarmConfStart = new Date().getTime();
				logger.debug("TB-开始初始化企业告警等级设置");
				//初始化VID对应道路等级报警设置Map
				monitorDBAdapter.queryOrgAlarmConf();
				logger.debug("TB-初始化企业告警等级设置完成，耗时："+(new Date().getTime()-orgAlarmConfStart)+"ms");
				
				/*long updateIllegalOptionsStart = new Date().getTime();
				logger.debug("TB-定时更新车辆非法运营设置");
				monitorDBAdapter.updateIllegalOptionsAlarm();
				logger.debug("TB-定时更新车辆非法运营完成，耗时："+(new Date().getTime()-updateIllegalOptionsStart)+"ms");
				
				long overspeedAlarmCfgStart = new Date().getTime();
				logger.debug("TB-定时更新车辆超速设置");
				monitorDBAdapter.updateOverspeedAlarmCfg();
				logger.debug("TB-定时更新车辆超速设置完成，耗时："+(new Date().getTime()-overspeedAlarmCfgStart)+"ms");
				
				long fatigueAlarmCfgStart = new Date().getTime();
				logger.debug("TB-定时更新车辆疲劳驾驶设置");
				monitorDBAdapter.updateFatigueAlarmCfg();
				logger.debug("TB-定时更新车辆疲劳驾驶设置完成，耗时："+(new Date().getTime()-fatigueAlarmCfgStart)+"ms");
				*/
				
				 if(completeNum==0){
					 complete.add(true);//同步成功！
				 }
				
				completeNum++;
				
				logger.debug("TB-本次同步报警配置信息共耗时："
						+ (new Date().getTime() - fullStart) + "ms");
				Thread.sleep(synctime * 1000);
			}

		} catch (Exception e) {
			logger.error("TB-定时同步数据错误" + e);
			e.printStackTrace();
		}

		// }

	}
}
