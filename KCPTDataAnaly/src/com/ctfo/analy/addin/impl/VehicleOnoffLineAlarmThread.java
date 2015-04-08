package com.ctfo.analy.addin.impl;

import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.Constant;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.util.ExceptionUtil;
import com.ctfo.analy.util.MathUtils;
import com.lingtu.xmlconf.XmlConf;

/**
 * 车辆上下线告警处理
 * 
 * @author yujch
 * 
 */
public class VehicleOnoffLineAlarmThread extends Thread implements
		PacketAnalyser {

	private static final Logger logger = Logger
			.getLogger(VehicleOnoffLineAlarmThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(
			100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
//	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 报警map 缓存 key=vId_areaId
	private Map<String, VehicleMessageBean> lastMessageMap = new ConcurrentHashMap<String, VehicleMessageBean>();
	private Map<String, AlarmCacheBean> offlineAlarmCache = new ConcurrentHashMap<String, AlarmCacheBean>();
	// 是否运行标志
	public boolean isRunning = true;

	// private int
	// count=0;//因状态不符合产生的非法运营数据计数，当连续两条数据均为因状态不符合产生的非法运营数据时，保存非法运营结束记录

	public VehicleOnoffLineAlarmThread() {
	}

	/**
	 * 初始化方法
	 */
	public void initAnalyser(int nId, XmlConf config, String nodeName)
			throws Exception {
		this.nId = nId;
		this.config = config;
		this.nodeName = nodeName;

		start();
		
		//同步启动离线告警存储线程
		OfflineAlarmSave oas = new OfflineAlarmSave();
		Thread t = new Thread(oas);
		t.start();

		oracleDBAdapter = new OracleDBAdapter();
		oracleDBAdapter.initDBAdapter(config, nodeName);
		/*mysqlDBAdapter = new MysqlDBAdapter();
		mysqlDBAdapter.initDBAdapter(config, nodeName);*/
		
		redisDBAdapter = new RedisDBAdapter();
	}

	@Override
	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void endAnalyser() {
		// TODO Auto-generated method stub

	}

	public void addPacket(VehicleMessageBean vehicleMessage) {
		try {
			vPacket.put(vehicleMessage);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	public void run() {
		logger.debug("【车辆下线报警线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				if (vehicleMessage != null) {
					// 判断并记录报警信息
					boolean isAllowAnaly = isAllowAnaly(vehicleMessage.getCommanddr());
					if (isAllowAnaly){
						checkAlarm(vehicleMessage);
					}else{
						logger.debug("企业不进行离线告警分析[" + vehicleMessage.getCommanddr()+"]");
					}
					logger.debug("VehicleOnoffLineAlarmThread主线程"
							+ vPacket.size());
				}
			} catch (Exception e) {
				e.printStackTrace();
				logger.error(e);
			}
		}
	}

	/**
	 * 车辆离线软报警分析处理
	 * 
	 * @param vehicleMessage
	 * 
	 */
	public void checkAlarm(VehicleMessageBean vehicleMessage) {
		try {
			String vid = vehicleMessage.getVid();
			String keyIsSave = vid + "_isSave";

			long currTime = System.currentTimeMillis();
			vehicleMessage.setReceiveUtc(currTime);
			String commaddr = vehicleMessage.getCommanddr();
			VehicleMessageBean lastBean = lastMessageMap.get(commaddr);
			logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],:lastBean="+lastBean);
			if (lastBean != null) {
				if ("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType())) {// 判断指令类型
					logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],:位置信息，当前时间大于缓存中实践吗"+(lastBean.getUtc() < vehicleMessage.getUtc()));
					// 缓存车辆位置信息
					if (lastBean.getUtc() < vehicleMessage.getUtc()) {
						
						lastMessageMap.put(commaddr, vehicleMessage);
					}
				} else if ("5".equals(vehicleMessage.getMsgType())) {
					logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],:收到上线离线数据；车辆在线状态："+vehicleMessage.getOnlineState());
					String binaryStr = MathUtils.getBinaryString(lastBean.getBaseStatus());
					logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],:currTime="+currTime+" ; lastBean.getReceiveUtc()="+lastBean.getReceiveUtc()+";时间跨度："+(currTime - lastBean.getReceiveUtc()));
					// 车辆离线且离线时间和最后一条指令时间相隔超过2分钟，且最后一条指令的ACC状态为开，尽可能屏蔽掉人为离线情况
					if (MathUtils.check("0", binaryStr)&&"0".equals(vehicleMessage.getOnlineState())
							&& (currTime - lastBean.getReceiveUtc()) > 2 * 60 * 1000) {
						AlarmCacheBean alarmCacheBean = offlineAlarmCache.get(keyIsSave);
						if (alarmCacheBean==null){
							String alarmId = UUID.randomUUID().toString().replace("-", "");
							lastBean.setAlarmid(alarmId);
							lastBean.setUtc(currTime);
							vehicleMessage.setAlarmid(alarmId);
							lastBean.setAlarmid(alarmId);
							//延迟15分钟后如果告警仍未结束，则对告警进行保存
							//dealSaveOfflinealarm(lastBean);
	
							AlarmCacheBean rcb = new AlarmCacheBean();
	
							rcb.setBeginVmb(lastBean);
							rcb.setAlarmId(alarmId);
							rcb.setBegintime(currTime);
							rcb.setAlarmSrc(2);
							rcb.setAlarmcode("66");
							rcb.setAlarmlevel("A005");
							rcb.setAlarmadd("2");
							rcb.setAlarmaddInfo("车辆离线报警");
	
							offlineAlarmCache.put(keyIsSave, rcb);
						}
					}
					if ("1".equals(vehicleMessage.getOnlineState())) {
						logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],:车辆上线，更新离线告警数据");
						AlarmCacheBean alarmCacheBean = offlineAlarmCache
								.get(keyIsSave);
						if (alarmCacheBean != null&&alarmCacheBean.isSaved()) {
							vehicleMessage.setReceiveUtc(currTime);
							vehicleMessage.setUtc(currTime);

							alarmCacheBean.setEndTime(currTime);
							alarmCacheBean.setEndVmb(vehicleMessage);

							dealUpdateOfflinealarm(vehicleMessage,
									alarmCacheBean);
						}
						offlineAlarmCache.remove(keyIsSave);
						lastMessageMap.remove(commaddr);
					}
				}
			} else {
				if ("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType())) {// 判断指令类型
					logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],:lastBean为空且msgtype 为1或0，添加缓存");
					lastMessageMap.put(commaddr, vehicleMessage);
				}
			}
		} catch (Exception ex) {
			logger.error("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],车辆离线告警分析过程出错：" + ex.getMessage());
		}

	}

	/**
	 * 存储车辆离线报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealSaveOfflinealarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean) {
		logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],保存实时告警信息:" + vehicleMessage.getCommanddr()+" 缓存离线告警数："+offlineAlarmCache.size());
		saveOfflineAlarm(vehicleMessage,alarmCacheBean);
	}

	/**
	 * 更新车辆离线报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealUpdateOfflinealarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) {
		logger.debug("LXGJ-离线告警日志-["+vehicleMessage.getCommanddr()+"],更新实时告警信息并保存告警事件:" + vehicleMessage.getCommanddr()
				+ " 结束时间：" + vehicleMessage.getUtc());
		updateOfflineAlarm(vehicleMessage, alarmCacheBean);
		saveOfflineAlarmEvent(vehicleMessage, alarmCacheBean);
	}

	/**
	 * 存储车辆离线报警
	 * 
	 * @param vehicleMessage
	 *            1--车载终端报警,2--平台报警,3--电子围栏限速报警,4--道路等级限速报警
	 */
	private void saveOfflineAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean) {
		try {
			//查询当前驾驶员信息
			String driverinfoStr = redisDBAdapter.getCurrentDriverInfo(vehicleMessage.getVid());
			if (driverinfoStr!=null&&driverinfoStr.length()>0){
				String driverInfo[]= driverinfoStr.split(":");
				vehicleMessage.setDriverId(driverInfo[1]);
				vehicleMessage.setDriverName(driverInfo[2]);
				vehicleMessage.setDriverSrc(driverInfo[10]);
			}
			
			oracleDBAdapter.saveVehicleAlarm(vehicleMessage,alarmCacheBean);
//			mysqlDBAdapter.saveVehicleAlarm(vehicleMessage,alarmCacheBean);
			redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage,alarmCacheBean);
			logger.debug("线程:[" + nId + "]【车辆离线软报警】【存储】成功["
					+ vehicleMessage.getCommanddr() + "]Alarmid:"
					+ vehicleMessage.getAlarmid() + ";AlarmAddInfo:"
					+ vehicleMessage.getAlarmAddInfo());
		} catch (Exception e) {
			logger.error("名称：车辆离线软报警---数据库异常", e);
		}

	}

	/**
	 * 更新非法运营软报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateOfflineAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) {
		try {
			oracleDBAdapter.updateVehicleAlarm(vehicleMessage,alarmCacheBean);
//			mysqlDBAdapter.updateVehicleAlarm(vehicleMessage,alarmCacheBean);
			redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage,alarmCacheBean);
			logger.debug("线程:[" + nId + "]【车辆离线软报警】【更新】成功["
					+ vehicleMessage.getCommanddr() + "]Alarmid:"
					+ vehicleMessage.getAlarmid()+" time:"+vehicleMessage.getUtc());
		} catch (Exception e) {
			e.printStackTrace();
			logger.error("Alarmid:" + vehicleMessage.getAlarmid()
					+ "---更新车辆离线软报警-数据库异常", e);
		}
	}

	/**
	 * 存储驾驶行为事件数据
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void saveOfflineAlarmEvent(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) {
		try {
			oracleDBAdapter.saveVehicleAlarmEvent(vehicleMessage,
					alarmCacheBean);
			logger.debug("线程:[" + nId + "]【车辆离线软报警驾驶行为事件】【添加】成功["
					+ vehicleMessage.getCommanddr() + "]Alarmid:"
					+ vehicleMessage.getAlarmid());
		} catch (Exception e) {
			logger.error("Alarmid:" + vehicleMessage.getAlarmid()
					+ "---添加车辆离线软报警驾驶行为事件-数据库异常", e);
		}
	}

	class OfflineAlarmSave implements Runnable{
		
		@Override
		public void run() {
			// TODO Auto-generated method stub
			while(true){
				try {
					 for (Map.Entry<String, AlarmCacheBean> entry : offlineAlarmCache.entrySet()) {
						   //System.out.println("key= " + entry.getKey() + " and value= " + entry.getValue());
						   AlarmCacheBean beginBean = entry.getValue();
						   VehicleMessageBean msgBean = beginBean.getBeginVmb();
						   //比对时间，和当前时间查大于15分钟则保存
						   long cutime = System.currentTimeMillis();
						   if ((cutime-msgBean.getUtc())>=15*60*1000&&!beginBean.isSaved()){
							   msgBean.setUtc(cutime);
							   //保存告警信息
							   dealSaveOfflinealarm(msgBean,beginBean);
							   //更新保存状态
							   beginBean.setBegintime(cutime);
							   beginBean.setSaved(true);
						   }
					 }
					Thread.sleep(1*60*1000);
				} catch (Exception e) {
					// TODO Auto-generated catch block
					logger.error("LXGJ-离线告警日志,车辆离线告警分析存储线程出错：" + ExceptionUtil.getErrorStack(e, 0));
				}
			}
			
		}
		
	}
	
	/**
	 * 判断该车辆所在企业是否允许进行离线相关告警分析
	 * @param commaddr
	 * @return
	 */
	private boolean isAllowAnaly(String commaddr){
		boolean flag = false;
		AlarmBaseBean bean = TempMemory.getAlarmVehicleMap(commaddr);
		if (bean!=null){
			OrgAlarmConfBean oacBean = TempMemory.getOrgAlarmConfMap(bean.getTeamId());
			if (oacBean!=null){
				String alarmCode = oacBean.getAlarmCode();
				if (alarmCode.startsWith(Constant.ALARMCODE_OFFLINE+",")||alarmCode.endsWith(","+Constant.ALARMCODE_OFFLINE)||alarmCode.indexOf(","+Constant.ALARMCODE_OFFLINE+",")>-1){
					flag = true;
				}
			}
		}
		return flag;
	}
	
}
