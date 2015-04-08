package com.caits.analysisserver.services;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.UUID;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.DriverDetailBean;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.utils.CDate;

/**
 * 驾驶员驾驶明细分析服务
 * @author yujch
 *
 */
public class DriverAnalyserService {
	private static final Logger logger = LoggerFactory.getLogger(DriverAnalyserService.class);

	private long utc;
	private String vid;

	private List<DriverDetailBean> driverDetaillist = new ArrayList<DriverDetailBean>();

	private VehicleMessageBean lastLocBean = null;

	private Long tmpLastMileage = -1L;

	private Long tmpLastOil = -1L;

	private Long tmpLastPreciseOil = -1L;
	
	private DriverDetailBean driverDetail = null;
	
	private boolean accStatus = false;
	
	private boolean isEngineFlag = false;
	
	private boolean isRunningFlag = false;

	public DriverAnalyserService(String vid, long utc) {
		this.vid = vid;
		this.utc = utc;
		initAnalyser();
	}

	/**
	 * 初始化报警统计线程
	 * 
	 * @param nodeName
	 * @throws Exception
	 */
	public void initAnalyser() {

	}

	public void executeAnalyser(VehicleMessageBean trackBean, boolean isLastRow) {
		try {
			analysisDriverDetail(trackBean, isLastRow);

			lastLocBean = trackBean;
		} catch (Exception ex) {
			logger.debug("驾驶明细分析过程中出错！", ex);
		}
	}

	/**
	 * 分析驾驶员驾驶明细
	 * 如果驾驶行为跨天则会截断
	 * 
	 * @param list
	 * @param cols
	 */
	@SuppressWarnings("unused")
	private void analysisDriverDetail(VehicleMessageBean trackBean,
 boolean isLastRow) throws Exception {
		// 当前值
		String gpsTime = trackBean.getDateString();
		Long gpsSpeed = trackBean.getSpeed();
		Long lon = trackBean.getLon();
		Long lat = trackBean.getLat();
		Long mileage = trackBean.getMileage();
		Long oil = trackBean.getOil();
		Long rorateSpeed = trackBean.getRpm();
		String statusCode = trackBean.getBaseStatus();
		Long preciseOil = trackBean.getMetOil();
		String driverId = trackBean.getDriverId();

		// 备份值
		if (lastLocBean == null) {
			lastLocBean = trackBean;
		}

		String lastGpsTime = lastLocBean.getDateString();
		Long lastGpsSpeed = lastLocBean.getSpeed();
		Long lastMileage = lastLocBean.getMileage();
		Long lastOil = lastLocBean.getOil();
		Long lastRorateSpeed = lastLocBean.getRpm();
		Long lastPreciseOil = lastLocBean.getMetOil();
		String lastDriverId = lastLocBean.getDriverId();

		long mg = 0;
		long tmpOil = 0;
		long tmpPreciseOil = 0;

		long timediff = accountOffLineTime(CDate.stringConvertUtc(gpsTime), CDate.stringConvertUtc(lastGpsTime));//本条记录和上条记录的时间差ms

		// 处理里程油耗：排除里程油耗在内部为-1的情况
		if (mileage > -1) {
			tmpLastMileage = mileage;
		}
		if (tmpLastMileage > -1) {
			if (mileage == -1) {
				mileage = tmpLastMileage;
			}
			if (lastMileage == -1) {
				lastMileage = tmpLastMileage;
			}
		}

		if (oil > -1) {
			tmpLastOil = oil;
		}
		if (tmpLastOil > -1) {

			if (oil == -1) {
				oil = tmpLastOil;
			}

			if (lastOil == -1) {
				lastOil = tmpLastOil;
			}

		}

		if (preciseOil > -1) {
			tmpLastPreciseOil = preciseOil;
		}
		if (tmpLastPreciseOil > -1) {

			if (preciseOil == -1) {
				preciseOil = tmpLastPreciseOil;
			}

			if (lastPreciseOil == -1) {
				lastPreciseOil = tmpLastPreciseOil;
			}
		}

		// 过滤突增数据
		mg = mileage - lastMileage;
		// 过滤突增数据
		tmpOil = oil - lastOil;

		tmpPreciseOil = preciseOil - lastPreciseOil;

		/*****
		 * 过滤异常数据,包括里程和油耗, 里程为异常数据则本次里程负值为0,
		 */
		if (mg >= 0 && mg <= CDate.accountTimeIntervalVale(gpsTime, lastGpsTime, Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("mileage_interval").getValue()), 10f)) {
			// 不做处理
		} else {
			mg = 0;
		}

		if (tmpOil >= 0 && tmpOil <= CDate.accountTimeIntervalVale(gpsTime, lastGpsTime, Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue()), 60f)) {
			// 不做处理
		} else {
			// 油耗为异常数据则本次里程负值为0,
			tmpOil = 0;
		}

		if (tmpPreciseOil >= 0 && tmpPreciseOil <= CDate.accountTimeIntervalVale(gpsTime, lastGpsTime, Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue()) * 50, 60f)) {
			// 不做处理
		} else {
			// 油耗为异常数据则本次里程负值为0,
			tmpPreciseOil = 0;
		}

		// 驾驶明细分析
		if (driverDetail == null) {
			if (driverId != null && !"".equals(driverId)) {
				driverDetail = new DriverDetailBean();
				driverDetail.setDetailId(UUID.randomUUID().toString().replace("-", ""));
				driverDetail.setVid(vid);
				driverDetail.setStatDate(this.utc + 12 * 60 * 60 * 1000);
				driverDetail.setBeginVmb(trackBean);
			}
		} else {

			// 计算ACC开次数及时长
			if (trackBean.isAccState()) {
				if (accStatus) {
					driverDetail.addAccTime(timediff / 1000);
				}
				accStatus = true;
			} else {
				if (accStatus) {
					driverDetail.addAccTime(timediff / 1000);
					accStatus = false;
					driverDetail.addAccNum(1);
				}
			}

			// 发动机运行时长readTrackFile
			// 车辆点火且发动机转数大于100转时，表示发动机运行
			// ??发动机工作 应采用点火状态 发动机转速 车速 结合来判断
			if ((trackBean.getRpm() * ExcConstants.RPMUNIT > 100 || gpsSpeed >= 50) && trackBean.isAccState()) {
				if (isEngineFlag) {
					if (timediff <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME) {
						driverDetail.addEngineTime(timediff / 1000);
					}
				}
				isEngineFlag = true;
			} else {
				if (isEngineFlag) {
					if (timediff <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME) {
						driverDetail.addEngineTime(timediff / 1000);
					}
					isEngineFlag = false;
				}
			}

			// 行车时间
			if (gpsSpeed >= 50 && trackBean.isAccState()) {
				if (isRunningFlag) {
					if (timediff <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME) {
						driverDetail.addRunningTime(timediff / 1000);
					}
					// 累加计算整日行车油耗
					driverDetail.addEcuRunningOilWear(tmpOil);
					driverDetail.addMetRunningOilWear(tmpPreciseOil);
				}
				isRunningFlag = true;
			} else {
				if (isRunningFlag) {
					if (timediff <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME) {
						driverDetail.addRunningTime(timediff / 1000);
					}
					// 累加计算整日行车油耗
					driverDetail.addEcuRunningOilWear(tmpOil);
					driverDetail.addMetRunningOilWear(tmpPreciseOil);
					isRunningFlag = false;
				}
			}

			// 累加油耗、里程值
			driverDetail.addEcuOilWear(tmpOil);
			driverDetail.addMetOilWear(tmpPreciseOil);
			driverDetail.addMileage(mg);

			// 判断本次驾驶是否结束
			if (!driverId.equals(lastDriverId) || isLastRow) {
				// 驾驶员切换时需要结束上次驾驶记录
				driverDetail.setEndVmb(trackBean);
				driverDetaillist.add(driverDetail);
				driverDetail = null;
			}

			if (driverDetail == null && driverId != null && !"".equals(driverId) && !isLastRow) {
				driverDetail = new DriverDetailBean();
				driverDetail.setDetailId(UUID.randomUUID().toString().replace("-", ""));
				driverDetail.setVid(vid);
				driverDetail.setStatDate(this.utc + 12 * 60 * 60 * 1000);
				driverDetail.setBeginVmb(trackBean);
			}
		}
	}
	
	public List<DriverDetailBean> getDriverDetaillist() {
		return driverDetaillist;
	}

	/*****
	 * 计算车辆在线时间
	 */
	private long accountOffLineTime(Long curUtc,Long lastUtc){
		if(lastUtc<=0||curUtc<=0){
			return 0;
		}

		if((curUtc - lastUtc) <= ExcConstants.PLATFORM_REPORT_DATA_LONGEST_INTERVAL_TIME ){
			return curUtc - lastUtc;
		}
		
		return 0;
	}
	
	/**
	 * 向驾驶明细数据中填充告警及状态统计结果
	 * @param stateList
	 */
	public void fillDriverDetailList(Vector<AlarmCacheBean> stateList,Vector<AlarmCacheBean> alarmList) {
		if (driverDetaillist.size() < 1) { // 无驾驶明细信息
			return;
		}

		HashMap<String, String> hm = new HashMap<String, String>();
		for (int i = 0; i < driverDetaillist.size(); i++) {
			DriverDetailBean ssb = driverDetaillist.get(i);
			long beginUtc = ssb.getBeginVmb().getUtc();
			long endUtc = ssb.getEndVmb().getUtc();
			hm.put(beginUtc + "," + endUtc, "" + i);
		}

		Iterator<AlarmCacheBean> stateEventList = stateList.iterator();
		while (stateEventList.hasNext()) {
			AlarmCacheBean bean = stateEventList.next();
			if ("BS0012".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDoorLock((utc1 - utc0)/1000);
				}
			} else if ("BS0013".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDoor1OpenNum(1);
				}
			} else if ("BS0014".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDoor2OpenNum(1);
				}
			} else if ("BS0015".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDoor3OpenNum(1);
				}
			} else if ("BS0016".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDoor4OpenNum(1);
				}
			} else if ("BS0017".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDoor4OpenNum(1);
				}
			} else if ("ES0000".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addLowerBeam((utc1 - utc0)/1000);
				}
			} else if ("ES0001".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addHighBeam((utc1 - utc0)/1000);
				}
			} else if ("ES0002".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addRightTurningSignal((utc1 - utc0)/1000);
				}
			} else if ("ES0003".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addLeftTurningSignal((utc1 - utc0)/1000);
				}
			} else if ("ES0004".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addBrake((utc1 - utc0)/1000);
				}
			} else if ("ES0005".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addReverseGear((utc1 - utc0)/1000);
				}
			} else if ("ES0006".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addFoglight((utc1 - utc0)/1000);
				}
			} else if ("ES0007".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addOutlineLamp((utc1 - utc0)/1000);
				}
			} else if ("ES0008".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addTrumpet((utc1 - utc0)/1000);
				}
			} else if ("ES0009".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addAircondition((utc1 - utc0)/1000);
				}
			} else if ("ES0010".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addFreePosition((utc1 - utc0)/1000);
				}
			} else if ("ES0011".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addRetarderWork((utc1 - utc0)/1000);
				}
			} else if ("ES0012".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addAbsWork(1);
				}
			} else if ("ES0013".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addHeatUp((utc1 - utc0)/1000);
				}
			} else if ("ES0014".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addClutch((utc1 - utc0)/1000);
				}
			} else if ("EV0008".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addGearWrong((utc1 - utc0)/1000);
				}
			} else if ("EV0004".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addEconomicRunTime((utc1 - utc0)/1000);
				}
			}
		}
		
		//文件分析告警和软报警进行合并
		Vector<AlarmCacheBean> softAlarm = DBAdapter.softAlarmDetailMap.get(vid);
		if (softAlarm!=null){
			alarmList.addAll(softAlarm);
		}
		
		
		Iterator<AlarmCacheBean> alarmEventList = alarmList.iterator();
		while (alarmEventList.hasNext()) {
			AlarmCacheBean bean = alarmEventList.next();
			if ("1".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addOverspeed((utc1 - utc0)/1000);
					//区域内超速判断
					if (bean.getAreaId()!=null){
						driverDetaillist.get(ii).addAreaOverspeed((utc1 - utc0)/1000);
					}
				}
			} else if ("2".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addFatigue((utc1 - utc0)/1000);
				}
			} else if ("18".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDriverTimeoutTime((utc1 - utc0)/1000);
				}
			} else if ("19".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addStopTimout((utc1 - utc0)/1000);
				}
			} else if ("20".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addIntoarea(1);
				}
			} else if ("68".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addOutarea(1);
				}
			} else if ("21".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addIntoRoute(1);
				}
			} else if ("69".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addOutRoute(1);
				}
			} else if ("22".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addRouteRunDiffNum(1);
				}
			} else if ("23".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addDeviateRoute((utc1 - utc0)/1000);
				}
			} else if ("27".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addIllegalFireNum(1);
				}
			} else if ("28".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addIllegalMoveNum(1);
				}
			} else if ("29".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addCashAlarm((utc1 - utc0)/1000);
				}
			} else if ("47".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addOverrpm((utc1 - utc0)/1000);
				}
			} else if ("44".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addGearGlide((utc1 - utc0)/1000);
				}
			} else if ("48".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addUrgentSpeed((utc1 - utc0)/1000);
				}
			} else if ("49".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addUrgentLowdown((utc1 - utc0)/1000);
				}
			} else if ("45".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addLongIdle((utc1 - utc0)/1000);
				}
			} else if ("46".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addIdlingAir((utc1 - utc0)/1000);
				}
			} else if ("60".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addAreaOpendoor((utc1 - utc0)/1000);
				}
			} else if ("55".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					driverDetaillist.get(ii).addHeadCollideNum(1);
				}
			} else if ("56".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) { 
					driverDetaillist.get(ii).addVehicleDeviateNum(1);
				}
			}
			
		}

	}

	// 判断当前事件在哪条起步停车数据上
	@SuppressWarnings({ "rawtypes", "unchecked" })
	private int panduan(long beginUtc, long endUtc, HashMap hm) {
		Set<String> key = hm.keySet();
		for (Iterator it = key.iterator(); it.hasNext();) {
			String s = (String) it.next();
			String[] ss = s.split(",");
			if ((Long.parseLong(ss[0]) > beginUtc && endUtc > Long
					.parseLong(ss[1]))
					|| (Long.parseLong(ss[0]) <= beginUtc && endUtc <= Long
							.parseLong(ss[1]))) {
				return Integer.parseInt((String) hm.get(s));
			}
		}
		return -1;
	}
	
}
