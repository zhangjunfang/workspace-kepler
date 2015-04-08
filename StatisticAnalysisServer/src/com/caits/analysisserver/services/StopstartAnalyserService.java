package com.caits.analysisserver.services;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.RunningBean;
import com.caits.analysisserver.bean.StopstartBean;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.MathUtils;
import com.caits.analysisserver.utils.Utils;

public class StopstartAnalyserService {
	private static final Logger logger = LoggerFactory.getLogger(StopstartAnalyserService.class);
	// 记录丢弃数据
	private static final Logger discardData = LoggerFactory.getLogger("discardData");

	private long utc;
	private String vid;

	private List<StopstartBean> stopstartlist = null;

	private VehicleMessageBean lastLocBean = null;

	private Long tmpLastMileage = -1L;

	private Long tmpLastOil = -1L;

	private Long tmpLastPreciseOil = -1L;

	public StopstartAnalyserService(String vid, long utc) {
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
		// 起步停车分析服务初始化
		stopstartlist = new ArrayList<StopstartBean>();

	}

	public void executeAnalyser(VehicleMessageBean trackBean, boolean isLastRow) {
		try {
			analysisStopstart(trackBean, isLastRow);

			lastLocBean = trackBean;
		} catch (Exception ex) {
			logger.debug("起步停车分析过程中出错！", ex);
		}
	}

	/**
	 * 分析生成起步停车数据
	 * 
	 * @param list
	 * @param cols
	 */
	// 启动时间 发动机转速>100 车速<5
	// 起步时间 发动机转速>100 车速>5
	// 停止时间 发动机转速>100 车速<5
	// 熄火时间 发动机转速<100 车速<5
	private void analysisStopstart(VehicleMessageBean trackBean,
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

		long mg = 0;
		long tmpOil = 0;
		long tmpPreciseOil = 0;
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

		// boolean startflag = false;
		// boolean stopflag = false;

		if (stopstartlist == null) {
			stopstartlist = new ArrayList<StopstartBean>();
		}

		if (stopstartlist.size() == 0) {
			stopstartlist.add(new StopstartBean());
		}

		int size = stopstartlist.size();
		StopstartBean ssb = stopstartlist.get(size - 1);
		ssb.setStatDate(this.utc + 12 * 60 * 60 * 1000);

		RunningBean rb = ssb.getRunninglist().get(
				ssb.getRunninglist().size() - 1);

		// 过滤突增数据
		mg = mileage - lastMileage;
		// 过滤突增数据
		tmpOil = oil - lastOil;

		tmpPreciseOil = preciseOil - lastPreciseOil;

		/*****
		 * 过滤异常数据,包括里程和油耗, 里程为异常数据则本次里程负值为0,
		 */
		if (mg >= 0
				&& mg <= CDate
						.accountTimeIntervalVale(
								gpsTime,
								lastGpsTime,
								Integer.parseInt(SystemBaseInfoPool
										.getinstance()
										.getBaseInfoMap("mileage_interval")
										.getValue()), 10f)) {
			// 不做处理
		} else {
			mg = 0;
			// 记录过滤掉数据(里程突增突减)
			discardData.info("Mileage vid=" + vid + ":"
					+ trackBean.getTrackStr());
		}

		if (tmpOil >= 0
				&& tmpOil <= CDate.accountTimeIntervalVale(
						gpsTime,
						lastGpsTime,
						Integer.parseInt(SystemBaseInfoPool.getinstance()
								.getBaseInfoMap("oil_interval").getValue()),
						60f)) {
			// 不做处理
		} else {
			// 油耗为异常数据则本次里程负值为0,
			tmpOil = 0;
			// 记录过滤掉数据(油耗突增突减)
			discardData.info("Oil vid=" + vid + ":" + trackBean.getTrackStr());
		}

		if (tmpPreciseOil >= 0
				&& tmpPreciseOil <= CDate
						.accountTimeIntervalVale(
								gpsTime,
								lastGpsTime,
								Integer.parseInt(SystemBaseInfoPool
										.getinstance()
										.getBaseInfoMap("oil_interval")
										.getValue()) * 50, 60f)) {
			// 不做处理
		} else {
			// 油耗为异常数据则本次里程负值为0,
			tmpPreciseOil = 0;
			// 记录过滤掉数据(油耗突增突减)
			discardData.info("MetOil vid=" + vid + ":"
					+ trackBean.getTrackStr());
		}

		if (ssb.isLaunchFlag()) {
			if (rb.isStartFlag() && !rb.isStopFlag()) {
				// ssb.getRunninglist().get(ssb.getRunninglist().size()-1).addRunningOil(tmpOil);
				// //累加一次行车中每两点间油耗
				ssb.getRunninglist().get(ssb.getRunninglist().size() - 1)
						.addEcuRunningOil(tmpOil); // 累加一次行车中每两点间油耗
				ssb.getRunninglist().get(ssb.getRunninglist().size() - 1)
						.addMetRunningOil(tmpPreciseOil); // 累加一次行车中每两点间精准油耗
				ssb.getRunninglist().get(ssb.getRunninglist().size() - 1)
						.addRunningMileage(mg); // 累加一次行车中每两点间油耗
			}
			// ssb.addUseOil(tmpOil); // 累加一次ACC开到ACC关中每两点间油耗
			ssb.addEcuOilWear(tmpOil);
			ssb.addMetOilWear(tmpPreciseOil);
			ssb.addMileage(mg); // 累加一次ACC开到ACC关中每两点间油耗
		}

		boolean isSerialRow = false;

		Long interruptedTime = (CDate.stringConvertUtc(gpsTime) - CDate
				.stringConvertUtc(lastGpsTime));// 数据间隔时长
		if (interruptedTime < ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME) {// 如果两次数据上报间隔小于5分钟，则认为是连续记录
			isSerialRow = true;
		}

		// 如果汇报信息不连续，则结束本次记录的起步停车时间和运行时间，并添加新行
		if ((!isSerialRow && ssb.isLaunchFlag())
				|| (rorateSpeed <= 800 && lastRorateSpeed > 800 && ssb
						.isLaunchFlag())
				|| (rorateSpeed == -1 && gpsSpeed <= 50 && lastGpsSpeed > 50 && ssb
						.isLaunchFlag())) {

			if (!isSerialRow) {
				stopstartlist = funishCurrentStartstop(stopstartlist,
						lastLocBean);
				isSerialRow = true;
			} else {
				stopstartlist = funishCurrentStartstop(stopstartlist, trackBean);
			}

			stopstartlist.add(new StopstartBean());
			size += 1;
			ssb = stopstartlist.get(size - 1);
			rb = ssb.getRunninglist().get(ssb.getRunninglist().size() - 1);
		}
		
		String number = MathUtils.getBinaryString(statusCode);

		// 当gps时间有效、点火状态有效、并且gps时间和上次GPS时间差值小于20秒时进行起步停车统计
		if (!"".equals(gpsTime) && Utils.check("0", number) && isSerialRow) {
			// 如果上此发动机转速原始值大于800(实际转速大于100转/s)
			if (!ssb.isLaunchFlag()) {
				if (rorateSpeed > 800) {// 有总线的车进入此方法设置发动机点火时信息
					stopstartlist.get(size - 1).setLaunchFlag(true);
					stopstartlist.get(size - 1).setLaunchTime(gpsTime);
					stopstartlist.get(size - 1).setStartMileage(mileage);
					stopstartlist.get(size - 1).setStartOil(oil);
					stopstartlist.get(size - 1).setBeginLon(lon);
					stopstartlist.get(size - 1).setBeginLat(lat);
					stopstartlist.get(size - 1).setStatDate(
							this.utc + 12 * 60 * 60 * 1000);
					stopstartlist.get(size - 1).setDriverId(trackBean.getDriverId());
					stopstartlist.get(size - 1).setDriverName(trackBean.getDriverName());
					stopstartlist.get(size - 1).setDriverSrc(trackBean.getDriverSrc());

					ssb.setLaunchFlag(true);
				}
			}

			if (!rb.isStartFlag()) {//
				// 车辆启动：本次源车速大于50(实际车速大于5Km/h) 并且 上次源车速小于等于50 时
				// 飘移排除：有总线的车同时判断发动机转速大于800,没总线的车没法进行过滤
				if (rorateSpeed > 0) {
					// 有总线时能计算怠速下最大转速
					if (ssb.isLaunchFlag()) {
						if (ssb.getIdlingMaxRotateSpeed() < rorateSpeed) {
							stopstartlist.get(size - 1)
									.setIdlingMaxRotateSpeed(rorateSpeed);
						}
					}

					if (gpsSpeed > 50 && lastGpsSpeed <= 50) {
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartTime(gpsTime);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartFlag(true);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartRunningMileage(mileage);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartRunningOil(oil);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setBeginLon(lon);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setBeginLat(lat);
						rb.setStartFlag(true);
					}
				} else {
					if (gpsSpeed > 50 && lastGpsSpeed <= 50) {
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartTime(gpsTime);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartFlag(true);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartRunningMileage(mileage);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setStartRunningOil(oil);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setBeginLon(lon);
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setBeginLat(lat);
						rb.setStartFlag(true);

						// 无总线车辆当车速第一次大于50时需给 发动机点火设置状态为点火
						if (!ssb.isLaunchFlag()) {
							stopstartlist.get(size - 1).setLaunchFlag(true);
							stopstartlist.get(size - 1).setLaunchTime(gpsTime);
							stopstartlist.get(size - 1)
									.setStartMileage(mileage);
							stopstartlist.get(size - 1).setStartOil(oil);
							stopstartlist.get(size - 1).setBeginLon(lon);
							stopstartlist.get(size - 1).setBeginLat(lat);
							stopstartlist.get(size - 1).setStatDate(
									this.utc + 12 * 60 * 60 * 1000);
							stopstartlist.get(size - 1).setDriverId(trackBean.getDriverId());
							stopstartlist.get(size - 1).setDriverName(trackBean.getDriverName());
							stopstartlist.get(size - 1).setDriverSrc(trackBean.getDriverSrc());

							ssb.setLaunchFlag(true);
						}
					}
				}
			}

			if (ssb.isLaunchFlag() && rb.isStartFlag() && !rb.isStopFlag()) {
				if (gpsSpeed > 50) {
					if (rorateSpeed > 0 && rb.getMaxRotateSpeed() < rorateSpeed) {
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setMaxRotateSpeed(rorateSpeed);
					}
					if (rb.getMaxSpeed() < gpsSpeed) {
						stopstartlist.get(size - 1).getRunninglist()
								.get(ssb.getRunninglist().size() - 1)
								.setMaxSpeed(gpsSpeed);
					}
				}

				// 车辆停止：当第一次源速度由大于50变为小于50时
				if (gpsSpeed <= 50 && lastGpsSpeed > 50) {
					stopstartlist.get(size - 1).getRunninglist()
							.get(ssb.getRunninglist().size() - 1)
							.setStopTime(gpsTime);
					stopstartlist.get(size - 1).getRunninglist()
							.get(ssb.getRunninglist().size() - 1)
							.setStopFlag(true);
					stopstartlist.get(size - 1).getRunninglist()
							.get(ssb.getRunninglist().size() - 1)
							.setStopRunningMileage(mileage);
					stopstartlist.get(size - 1).getRunninglist()
							.get(ssb.getRunninglist().size() - 1)
							.setStopRunningOil(oil);
					stopstartlist.get(size - 1).getRunninglist()
							.get(ssb.getRunninglist().size() - 1)
							.setEndLon(lon);
					stopstartlist.get(size - 1).getRunninglist()
							.get(ssb.getRunninglist().size() - 1)
							.setEndLat(lat);
					stopstartlist.get(size - 1).getRunninglist()
							.add(new RunningBean());

					rb.setStopFlag(true);
				}
			}

			if (ssb.isLaunchFlag() && rb.isStartFlag() && rb.isStopFlag()
					&& !ssb.isFireoffFlag()) {
				// 怠速下转速最大值
				if (rorateSpeed > 0) {
					if (ssb.getIdlingMaxRotateSpeed() < rorateSpeed) {
						stopstartlist.get(size - 1).setIdlingMaxRotateSpeed(
								rorateSpeed);
					}
				}
			}
		}

		// 当车辆熄火或当前行为最后一行并且当前车辆点火未熄火时，结束起步停车本次起步停车分析
		if ((!Utils.check("0", number) || isLastRow) && ssb.isLaunchFlag()) {
			stopstartlist = funishCurrentStartstop(stopstartlist, trackBean);
			// 当为非最后一行时，添加起步停车对象，为下次统计做准备
			if (!isLastRow) {
				stopstartlist.add(new StopstartBean());
			}
		}

	}

	private List<StopstartBean> funishCurrentStartstop(
			List<StopstartBean> list, VehicleMessageBean trackBean) {
		String lastGpsTime = trackBean.getDateString();
		Long lastLon = trackBean.getLon();
		Long lastLat = trackBean.getLat();
		Long lastMileage = trackBean.getMileage();
		Long lastOil = trackBean.getOil();

		int size = list.size();
		StopstartBean ssb = list.get(size - 1);
		RunningBean rb = ssb.getRunninglist().get(
				ssb.getRunninglist().size() - 1);

		// 判断车辆有无总线
		boolean isCan = true;
		if (ssb.getLaunchTime() == null || "".equals(ssb.getLaunchTime())) {
			isCan = false;
		}

		if (!rb.isStopFlag() && rb.isStartFlag()) {
			list.get(size - 1).getRunninglist()
					.get(ssb.getRunninglist().size() - 1)
					.setStopTime(lastGpsTime);
			list.get(size - 1).getRunninglist()
					.get(ssb.getRunninglist().size() - 1).setStopFlag(true);
			list.get(size - 1).getRunninglist()
					.get(ssb.getRunninglist().size() - 1)
					.setStopRunningMileage(lastMileage);
			list.get(size - 1).getRunninglist()
					.get(ssb.getRunninglist().size() - 1)
					.setStopRunningOil(lastOil);
			list.get(size - 1).getRunninglist()
					.get(ssb.getRunninglist().size() - 1).setEndLon(lastLon);
			list.get(size - 1).getRunninglist()
					.get(ssb.getRunninglist().size() - 1).setEndLat(lastLat);
		} else if (!rb.isStartFlag() && !rb.isStopFlag()) {
			int xx = list.get(size - 1).getRunninglist().size();
			list.get(size - 1).getRunninglist().remove(xx - 1);
		}

		list.get(size - 1).setFireoffFlag(true);
		if (!isCan) {
			list.get(size - 1).setFireoffTime(lastGpsTime);
			list.get(size - 1).setEndMileage(lastMileage);
			list.get(size - 1).setEndOil(lastOil);
			list.get(size - 1).setBeginLon(lastLon);
			list.get(size - 1).setBeginLat(lastLat);
		} else {
			list.get(size - 1).setFireoffTime(lastGpsTime);
			list.get(size - 1).setEndMileage(lastMileage);
			list.get(size - 1).setEndOil(lastOil);
			list.get(size - 1).setEndLon(lastLon);
			list.get(size - 1).setEndLat(lastLat);
		}

		return list;
	}

	public void fillStopstartList(Vector<AlarmCacheBean> stateList) {
		if (stopstartlist.size() < 1) { // 无起步停车信息
			return;
		}

		HashMap<String, String> hm = new HashMap<String, String>();
		for (int i = 0; i < stopstartlist.size(); i++) {
			StopstartBean ssb = stopstartlist.get(i);
			long beginUtc = CDate.stringConvertUtc(ssb.getLaunchTime());
			long endUtc = CDate.stringConvertUtc(ssb.getFireoffTime());

			hm.put(beginUtc + "," + endUtc, "" + i);
		}

		Iterator<AlarmCacheBean> driverEventList = stateList.iterator();
		while (driverEventList.hasNext()) {
			AlarmCacheBean bean = driverEventList.next();
			if ("EV0001".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addHeaterWorkingTime((utc1 - utc0)/1000);
				}
			} else if ("EV0002".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addAircWorkingTime((utc1 - utc0)/1000);
				}
			} else if ("BS0013".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addDoor1OpenNum(1);
				}
			} else if ("BS0014".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addDoor2OpenNum(1);
				}
			} else if ("ES0004".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addBrakingNum(1);
				}
			} else if ("ES0008".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addHornWorkingNum(1);
				}
			} else if ("ES0011".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addRetarderWorkNum(1);
				}
			} else if ("ES0012".equals(bean.getAlarmcode())) {
				long utc0 = bean.getBeginVmb().getUtc();
				long utc1 = bean.getEndVmb().getUtc();
				int ii = panduan(utc0, utc1, hm);
				if (ii >= 0) {
					stopstartlist.get(ii).addAbsWorkingNum(1);
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

	public List<StopstartBean> getStopstartlist() {
		return stopstartlist;
	}
	
	
}
