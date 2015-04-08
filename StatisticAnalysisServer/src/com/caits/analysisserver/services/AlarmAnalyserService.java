package com.caits.analysisserver.services;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.Vector;
import java.util.concurrent.ConcurrentHashMap;

import oracle.jdbc.OracleConnection;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.DataBean;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.VehicleAlarm;
import com.caits.analysisserver.bean.VehicleAlarmEvent;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleDBAdapter;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.MathUtils;
import com.caits.analysisserver.utils.Utils;
import com.ctfo.generator.pk.GeneratorPK;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AlarmAnalyserService <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-10-18</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6 @ Description: 用于统计车辆报警信息
 */
@SuppressWarnings("unused")
public class AlarmAnalyserService {
	private static final Logger logger = LoggerFactory.getLogger(AlarmAnalyserService.class);

	private final String keyWord = "track";

	private final String keyWords = "alarm";

	private long serverRunTime = 0l;

	private Vector<VehicleAlarmEvent> alarmEventList = new Vector<VehicleAlarmEvent>(); // 报警事件

	private Map<String, Long> maxSpeedMap = new HashMap<String, Long>(); // 存储持续报警周中最大速度

	private Vector<AlarmCacheBean> alarmList = new Vector<AlarmCacheBean>();

	private Vector<AlarmCacheBean> driverEventList = new Vector<AlarmCacheBean>();// 存储驾驶行为事件列表(除告警外)

	// 报警map 缓存 key=vId_areaId
	private Map<String, AlarmCacheBean> alarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();

	// private Map<String,VehicleAlarm> alarmCountMap = new
	// ConcurrentHashMap<String, VehicleAlarm>();

	private Map<String, VehicleAlarm> stateCountMap = new ConcurrentHashMap<String, VehicleAlarm>();

	private Map<String, Long> lastAlarmTimeMap = new ConcurrentHashMap<String, Long>();

	// 报警分析文件目录
	private String alarmFileUrl = null;

	// 驾驶行为事件目录
	private String eventFileUrl = null;

	private String saveVehicleAlarmInfo = null;

	private String procSaveVehicleAlarmInfo = null;

	private String sql_selectAlarmEvent = null; // 查询车报警事件表

	private String saveDriverEventInfo = null;

	private OracleConnection dbCon = null;

	private Map<String, VehicleAlarm> alarmSatDayMap = null; // 报警日统计存储列表

	private String vid = ""; // 当前车辆编号

	private String eventFilePath = "";
	private long utc = 0;

	private VehicleMessageBean lastLocBean = null;

	private Map<String, VehicleMessageBean> stateMap = new HashMap<String, VehicleMessageBean>();
	private Vector<AlarmCacheBean> stateEventList = new Vector<AlarmCacheBean>();

	private List<String> openningDoorPicList = new ArrayList<String>();

	private Vector<AlarmCacheBean> vAlarmEventList = new Vector<AlarmCacheBean>();

	private Vector<AlarmCacheBean> openingDoorList = new Vector<AlarmCacheBean>(); // 存储开门信息

	private VehicleStatus vehicleStatus = new VehicleStatus();// 此处指记录开门次数信息
	
	private List<String> driverClockinList = new ArrayList<String>();

	public AlarmAnalyserService(OracleConnection dbCon, String path,
			String vid, long utc,List<String> driverClockinList) {
		this.vid = vid;
		this.eventFilePath = path;
		this.utc = utc;
		this.dbCon = dbCon;
		this.driverClockinList = driverClockinList;
		initAnalyser();
	}

	/**
	 * 初始化报警统计线程
	 * 
	 * @param nodeName
	 * @throws Exception
	 */
	public void initAnalyser() {
		alarmFileUrl = FilePool.getinstance().getFile(this.utc, "alarmfileurl");
		eventFileUrl = FilePool.getinstance().getFile(this.utc, "eventfileurl");
		// 存储车辆报警日统计信息
		// saveVehicleAlarmInfo =
		// SQLPool.getinstance().getSql("sql_saveVehicleAlarmInfo");

		procSaveVehicleAlarmInfo = SQLPool.getinstance().getSql(
				"sql_procSaveVehicleAlarmInfo");

		// 查询车辆报警明细表
		sql_selectAlarmEvent = SQLPool.getinstance().getSql(
				"sql_selectAlarmEvent");

		// 查询车辆当日拍照信息
		selectOpenningDoorPic();
	}

	public void executeAnalyser(VehicleMessageBean trackBean, boolean isLastRow) {
		try {

			analyserRealTimeAlarm(trackBean, isLastRow);

			analysisStateEvent(trackBean);

			if (isLastRow) {
				readEventFile(eventFilePath, vid);

				accountOpenningCount();// 统计开门次数及区域内开门时长

				openningDoorPicList.clear();// 清除缓存数据

			}

			lastLocBean = trackBean;
		} catch (Exception ex) {
			logger.debug("VID:" + vid + " 告警、状态分析过程中出错！", ex);
		}
	}

	/**
	 * 实时告警分析
	 * 
	 * @param cols
	 * @throws IllegalAccessException
	 * @throws InstantiationException
	 * @throws InvocationTargetException
	 * @throws NoSuchMethodException
	 */
	@SuppressWarnings("rawtypes")
	public void analyserRealTimeAlarm(VehicleMessageBean trackBean,
			boolean isLastRow) throws IllegalAccessException,
			InstantiationException, InvocationTargetException,
			NoSuchMethodException {

		Long speed = trackBean.getSpeed();
		String alarmCodes = trackBean.getAlarmcode();

		if (alarmCodes != null && alarmCodes.length() > 0) {
			String alarmCode[] = alarmCodes.split(",");

			// 获取当前未完成告警数据
			if (alarmMap.size() > 0) {

				HashMap<String, String> remocedHM = new HashMap<String, String>();// 缓存已结束的报警编码

				Set<String> keys = alarmMap.keySet();
				for (Iterator it = keys.iterator(); it.hasNext();) {
					String key = (String) it.next();
					boolean flag = false;
					// 查询报警是否结束
					for (int i = 0; i < alarmCode.length; i++) {
						String code = alarmCode[i];
						if (key.equals(code)) {
							flag = true;
							alarmCode[i] = "";
							break;
						}
					}
					if (!flag) {
						// 报警结束
						AlarmCacheBean alarmBean = alarmMap.get(key);

						alarmBean.setEndVmb(trackBean);

						alarmList.add(alarmBean);
						// this.countAlarm(alarmBean);

						remocedHM.put(key, key);

					} else {
						// 报警未结束
						if (speed >= 50 && speed < 3000) {
							if (alarmMap.get(key).getMaxSpeed() < speed) {
								alarmMap.get(key).setMaxSpeed(speed);
							}

							alarmMap.get(key).setAvgSpeed(speed);

						}

						alarmMap.get(key).setEndVmb(trackBean);

						parseAlarmAdditional(key, alarmMap.get(key), trackBean);
					}
				}
				// 移除已结束的报警
				Set<String> removedkeys = remocedHM.keySet();
				for (Iterator it = removedkeys.iterator(); it.hasNext();) {
					String key = (String) it.next();
					alarmMap.remove(key);
				}
				remocedHM.clear();

				// 向缓存中添加告警信息
				for (String code : alarmCode) {
					if (code != null && !"".equals(code)) {
						// 向缓存中添加告警信息
						AlarmCacheBean tmpcacheBean = new AlarmCacheBean();

						tmpcacheBean.setVid(vid);
						tmpcacheBean.setAlarmId(GeneratorPK.instance()
								.getPKString());
						tmpcacheBean.setAlarmcode(code);
						tmpcacheBean.setBeginVmb(trackBean);
						tmpcacheBean.setEndVmb(trackBean);
						tmpcacheBean.setAlarmSrc(1);
						if (speed >= 50 && speed < 3000) {
							tmpcacheBean.setMaxSpeed(speed);
							tmpcacheBean.setAvgSpeed(speed);
						}
						tmpcacheBean.setSaved(true);

						parseAlarmAdditional(code, tmpcacheBean, trackBean);

						alarmMap.put(code, tmpcacheBean);
					}
				}
			} else {
				// 当前缓存中无报警
				for (String code : alarmCode) {
					if (code != null && !"".equals(code)) {
						// 向缓存中添加告警信息
						AlarmCacheBean tmpcacheBean = new AlarmCacheBean();
						tmpcacheBean.setVid(vid);
						tmpcacheBean.setAlarmId(GeneratorPK.instance()
								.getPKString());
						tmpcacheBean.setAlarmcode(code);
						tmpcacheBean.setBeginVmb(trackBean);
						tmpcacheBean.setEndVmb(trackBean);
						tmpcacheBean.setAlarmSrc(1);
						tmpcacheBean.setMaxSpeed(speed);
						tmpcacheBean.setAvgSpeed(speed);
						tmpcacheBean.setSaved(true);

						parseAlarmAdditional(code, tmpcacheBean, trackBean);

						alarmMap.put(code, tmpcacheBean);
					}
				}
			}
		}

		if (isLastRow) {
			intterputRealTimeAlarm();
		}
	}

	private void parseAlarmAdditional(String alarmCode, AlarmCacheBean bean,
			VehicleMessageBean trackBean) {
		if (alarmCode != "") {
			if ("1".equals(alarmCode)) {// 超速报警附加信息
				if (!"".equals(trackBean.getOverspeedAlarmAdditional())) {
					String[] column = trackBean.getOverspeedAlarmAdditional()
							.split("\\|");
					if (column.length >= 2) {
						String locType = column[0];
						if (!"0".equals(locType)) {
							bean.setAreaId(column[1]);

							if ("4".equals(locType)) {
								// 线路超速告警时附加超速告警阀值
								Long hold = DBAdapter.lineSpeedThresholdMap
										.get(this.vid + "_" + column[1]);
								if (hold != null && hold > 0) {
									bean.setSpeedThreshold(hold);
								}
							} else {
								// 区域超速告警时附加超速告警阀值
								Long hold = DBAdapter.areaSpeedThresholdMap
										.get(this.vid + "_" + column[1]);
								if (hold != null && hold > 0) {
									bean.setSpeedThreshold(hold);
								}
							}
						}
					}
				} else {
					// 如果超速告警无附加信息，则认为此超速为普通超速，附加终端设置的超速阀值
					VehicleInfo vehicleMessage = AnalysisDBAdapter
							.queryVechileInfo(this.vid);
					if (vehicleMessage != null) {
						Long hold = vehicleMessage.getMaxSpeed();
						if (hold != null && hold > 0) {
							bean.setSpeedThreshold(hold);
						}

					}
				}
			}
			if ("21".equals(alarmCode)) {// 进出区域/线路报警附加信息
				if (!"".equals(trackBean.getAreaAlarmAdditional())) {
					String[] column = trackBean.getAreaAlarmAdditional().split(
							"\\|", 3);
					// logger.debug("---overspeed----"+trackBean.getAreaAlarmAdditional());
					if (column.length >= 3) {
						if (!"0".equals(column[0])) {
							bean.setAreaId(column[1]);
						}
						bean.setInoutAreaAlarmDir(column[2]);
					}
				}
			}
			if ("22".equals(alarmCode)) {// 路段行驶时间不足或过长告警
				if (!"".equals(trackBean.getAlarmAdditional())) {
					String[] column = trackBean.getAlarmAdditional().split(
							"\\|", 3);
					if (column.length == 3) {
						bean.setAreaId(column[0]);
						bean.setRunningEnoughTime(Long.parseLong(column[1]));
						bean.setIsRunningenough(column[2]);
					}
				}
			}
		}
	}

	/**
	 * 结束所有未完成的实时报警
	 */
	private void intterputRealTimeAlarm() {
		alarmList.addAll(alarmMap.values());
		alarmMap.clear();
	}

	private void readAlarmEventFile(String eventFile) {
		try {
			// 读取分析告警事件信息
			readEventFile(eventFile, vid);
		} catch (Exception ex) {
			logger.error("日报警统计，统计报警事件出错." + vid, ex);
		}
	}

	private void countState(AlarmCacheBean alarmBean) {
		String alarmCode = alarmBean.getAlarmcode();
		VehicleAlarm va = null;
		if (stateCountMap.containsKey(alarmCode)) {
			va = stateCountMap.get(alarmCode);
		} else {
			va = new VehicleAlarm();
		}
		va.setAlarmCode(alarmCode);
		long diffTime = alarmBean.getEndVmb().getUtc()
				- alarmBean.getBeginVmb().getUtc();
		if (diffTime >= 0) {
			va.addCount(1);
			va.addSpeedingMileage(alarmBean.getEndVmb().getMileage()
					- alarmBean.getBeginVmb().getMileage());
			va.addSpeedingOil(alarmBean.getEndVmb().getOil()
					- alarmBean.getBeginVmb().getOil());
			va.addTime(diffTime);

			stateCountMap.put(alarmCode, va);
		}
	}

	/**
	 * 强制结束未结束的持续报警
	 * 
	 * @param alarmSatDayMap
	 * @param info
	 * @param vid
	 */
	private void intterputAlarm(Map<String, VehicleAlarm> alarmSatDayMap,
			Map<String, String[]> chMap, VehicleInfo info, String vid,
			String[] col) {

		// 最后一行与chMap比较，如果存在的需要添加累计里程等数据。
		Set<String> key = chMap.keySet();
		// 最后一行
		String lastCode = "";
		String[] tempCode = null;
		String[] mapCode = null;

		// 如果有上报的报警信息
		if (col[0].length() > 1) {
			lastCode = col[0].substring(1, col[0].lastIndexOf(","));

			tempCode = lastCode.split(",");

			// 最后一行都要结束的报警
			for (String code : tempCode) {
				if (code == null || "".equals(code)) {
					continue;
				}
				int count = 0;
				code = vid + "_" + code;
				if (chMap.containsKey(code)) {
					count++;
				}
				if (count == 0) {
					// 报警事件统计
					VehicleAlarmEvent event = new VehicleAlarmEvent();

					String alarmCd = code.substring(code.indexOf("_") + 1);
					event.setAlarmCode(alarmCd);
					event.setVid(vid);
					event.setPhoneNumber(info.getCommaddr());
					event.setAREA_ID("");// 电子围栏编号
					event.setAlarmType(AnalysisDBAdapter.alarmTypeMap
							.get(alarmCd));
					event.setMtypeCode("");
					event.setMediaUrl("");
					event.setStartUtc(CDate.stringConvertUtc(col[5]));
					event.setStartLat((Long) formatValueByType(col[4], "-1",
							'L'));
					event.setStartLon((Long) formatValueByType(col[3], "-1",
							'L'));
					event.setStartMapLat((Long) formatValueByType(col[2], "-1",
							'L'));
					event.setStartMapLon((Long) formatValueByType(col[1], "-1",
							'L'));
					if (col[11] != null && !col[11].equals("")
							&& !"null".equals(col[11])) {
						event.setStartElevation(Integer.parseInt(col[11]));
					}
					event.setStartHead((Integer) formatValueByType(col[7], "0",
							'I'));
					event.setStartGpsSpeed((Long) formatValueByType(col[6],
							"0", 'L'));
					event.setEndUtc(CDate.stringConvertUtc(col[5]));
					event.setEndLat((Long) formatValueByType(col[4], "-1", 'L'));
					event.setEndLon((Long) formatValueByType(col[3], "-1", 'L'));
					event.setEndMapLat((Long) formatValueByType(col[2], "-1",
							'L'));
					event.setEndMapLon((Long) formatValueByType(col[1], "-1",
							'L'));
					if (col[11] != null && !col[11].equals("")
							&& !"null".equals(col[11])) {
						event.setEndElevation(Integer.parseInt(col[11]));
					}
					event.setEndHead((Integer) formatValueByType(col[7], "0",
							'I'));
					event.setEndGpsSpeed((Long) formatValueByType(col[6], "0",
							'L'));
					long alarmTime = (event.getEndUtc() - event.getStartUtc()) / 1000;

					// 报警事件时长
					event.setAccountTime(alarmTime);

					// 核查结束时是否最大速度
					event.setMaxSpeed(Long.parseLong(col[6]));
					// 计算里程和油耗
					accountOilAndMelige(col, col, event);

					alarmEventList.add(event);

				} else {
					// 报警事件统计
					VehicleAlarmEvent event = new VehicleAlarmEvent();

					mapCode = chMap.get(code);

					String alarmCd = code.substring(code.indexOf("_") + 1);
					event.setAlarmCode(alarmCd);
					event.setVid(vid);
					event.setPhoneNumber(info.getCommaddr());
					event.setAREA_ID("");// 电子围栏编号
					event.setAlarmType(AnalysisDBAdapter.alarmTypeMap
							.get(alarmCd));
					event.setMtypeCode("");
					event.setMediaUrl("");
					event.setStartUtc(CDate.stringConvertUtc(mapCode[5]));
					event.setStartLat(Long.parseLong(mapCode[4]));
					event.setStartLon(Long.parseLong(mapCode[3]));
					event.setStartMapLat(Long.parseLong(mapCode[2]));
					event.setStartMapLon(Long.parseLong(mapCode[1]));
					if (mapCode[11] != null && !mapCode[11].equals("")
							&& !"null".equals(mapCode[11])) {
						event.setStartElevation(Integer.parseInt(mapCode[11]));
					}
					event.setStartHead((Integer) formatValueByType(mapCode[7],
							"0", 'I'));
					event.setStartGpsSpeed((Long) formatValueByType(mapCode[6],
							"0", 'L'));
					event.setEndUtc(CDate.stringConvertUtc(col[5]));
					event.setEndLat((Long) formatValueByType(col[4], "-1", 'L'));
					event.setEndLon((Long) formatValueByType(col[3], "-1", 'L'));
					event.setEndMapLat((Long) formatValueByType(col[2], "-1",
							'L'));
					event.setEndMapLon((Long) formatValueByType(col[1], "-1",
							'L'));
					if (col[11] != null && !col[11].equals("")
							&& !"null".equals(col[11])) {
						event.setEndElevation(Integer.parseInt(col[11]));
					}
					event.setEndHead((Integer) formatValueByType(col[7], "0",
							'I'));
					event.setEndGpsSpeed((Long) formatValueByType(col[6], "0",
							'L'));
					long alarmTime = (event.getEndUtc() - event.getStartUtc()) / 1000;

					// 报警事件时长
					if (alarmTime > 0) {
						event.setAccountTime(alarmTime);
					}
					// 核查结束时是否最大速度
					if (maxSpeedMap.get(code) < (Long) formatValueByType(
							col[6], "0", 'L')) {
						event.setMaxSpeed((Long) formatValueByType(col[6], "0",
								'L'));
					} else {
						event.setMaxSpeed(maxSpeedMap.get(code));
					}
					// 计算里程和油耗
					accountOilAndMelige(col, mapCode, event);
					alarmEventList.add(event);
				}

			} // for end
		}

		// chMap必须要结束的报警
		String endCode = null;
		String[] endMapCode = null;
		int num = 0;

		for (Iterator<String> it = key.iterator(); it.hasNext();) {
			endCode = it.next();
			endMapCode = chMap.get(endCode);

			if (tempCode != null) {
				for (String code : tempCode) {
					code = vid + "_" + code;
					if (endCode.equals(code)) {
						num++;
					}
				}// End for
			}

			if (num == 0) {

				// 报警事件统计
				VehicleAlarmEvent event = new VehicleAlarmEvent();
				String alarmcode = endCode.substring(endCode.indexOf("_") + 1);
				event.setAlarmCode(alarmcode);
				event.setVid(vid);
				event.setPhoneNumber(info.getCommaddr());
				event.setAREA_ID("");// 电子围栏编号
				event.setAlarmType(AnalysisDBAdapter.alarmTypeMap
						.get(alarmcode));
				event.setMtypeCode("");
				event.setMediaUrl("");
				event.setStartUtc(CDate.stringConvertUtc(endMapCode[5]));
				event.setStartLat((Long) formatValueByType(endMapCode[4], "-1",
						'L'));
				event.setStartLon((Long) formatValueByType(endMapCode[3], "-1",
						'L'));
				event.setStartMapLat((Long) formatValueByType(endMapCode[2],
						"-1", 'L'));
				event.setStartMapLon((Long) formatValueByType(endMapCode[1],
						"-1", 'L'));
				if (endMapCode[11] != null && !endMapCode[11].equals("")
						&& !"null".equals(endMapCode[11])) {
					event.setStartElevation(Integer.parseInt(endMapCode[11]));
				}
				event.setStartHead((Integer) formatValueByType(endMapCode[7],
						"0", 'I'));
				event.setStartGpsSpeed((Long) formatValueByType(endMapCode[6],
						"0", 'L'));
				event.setEndUtc(CDate.stringConvertUtc(col[5]));
				event.setEndLat((Long) formatValueByType(col[4], "-1", 'L'));
				event.setEndLon((Long) formatValueByType(col[3], "-1", 'L'));
				event.setEndMapLat((Long) formatValueByType(col[2], "-1", 'L'));
				event.setEndMapLon((Long) formatValueByType(col[1], "-1", 'L'));
				if (col[11] != null && !col[11].equals("")
						&& !"null".equals(col[11])) {
					event.setEndElevation(Integer.parseInt(col[11]));
				}
				event.setEndHead((Integer) formatValueByType(col[7], "0", 'I'));
				event.setEndGpsSpeed((Long) formatValueByType(col[6], "0", 'L'));
				long alarmTime = (event.getEndUtc() - event.getStartUtc()) / 1000;

				// 报警事件时长
				if (alarmTime > 0) {
					event.setAccountTime(alarmTime);
				}
				// 核查结束时是否最大速度
				if (maxSpeedMap.get(endCode) < (Long) formatValueByType(col[6],
						"0", 'L')) {
					event.setMaxSpeed((Long) formatValueByType(col[6], "0", 'L'));
				} else {
					event.setMaxSpeed(maxSpeedMap.get(endCode));
				}
				// 计算里程和油耗
				accountOilAndMelige(col, endMapCode, event);
				alarmEventList.add(event);
			}
		}// End for
	}

	/**
	 * 根据gps时间将读取的报警文件数据进行排序
	 */
	private TreeMap<Long, String> getAlarmMap(BufferedReader buf) {
		TreeMap<Long, String> returnAlarmMap = new TreeMap<Long, String>();
		String readLine = null;
		String gpsdate = null;
		String[] alarm = null;
		List<DataBean> list = new ArrayList<DataBean>();
		try {
			while ((readLine = buf.readLine()) != null) {

				// 报警文件每行的数据分割
				alarm = readLine.split(":");

				if (alarm.length == 14) {

					gpsdate = alarm[5];

					addList(readLine, gpsdate, list); // 按GPS时间排序

				}
			}// End while

			sortList(list); // 集合按GPS UTC时间排序
			Utils.clearDuplicateRecord(list, returnAlarmMap); // 滤过GPS 开始时间重复记录
		} catch (Exception e) {
			logger.error("读取报警文件信息出错", e);
		} finally {
			if (list.size() > 0) {
				list.clear();
			}
		}

		return returnAlarmMap;
	}

	/**
	 * 读取驾驶行为事件 1-加热器工作 2-空调工作 3-发动机超转 4-过长怠速 5-超经济区运行 6-空档滑行 7-怠速空调 8-二档起步
	 * 9-档位不当 10-超速 11-疲劳驾驶
	 * 
	 * 集合Map列表对应键 e1-加热器工作； e2-空调工作 e3-发动机超转 e4-过长怠速 e5-超经济区运行 e6-空档滑行 e7-怠速空调
	 * e8-二档起步 e9-档位不当
	 * 
	 * 车辆日统计表中存储加热器运行时间、空调工作、二档起步、档位不当 报警事件明细表中存储超速、疲劳驾驶、空档滑行、过长怠速、怠速空调、发动机超转
	 * 驾驶行为事件分为两部分，告警类的存入告警事件表中，其他存入状态事件 表中
	 * 
	 * @param path
	 * @throws SQLException
	 * @throws IOException
	 */
	private void readEventFile(String path, String vid) throws SQLException {
		long serverStartTime = System.currentTimeMillis();
		try {
			TreeMap<String, String> dataMap = new TreeMap<String, String>();
			readEvent(path, dataMap); // 读驾驶行为事件文件，并排序
			if (dataMap.size() > 0) {// 为了支持8080B车机，如果有驾驶行为事件文件，则清除事实上报超速、疲劳驾驶、空档滑行、过长怠速、怠速空调、发动机超转，否则则使用实时上报
				// 清除在日报警文件统计的超速、疲劳驾驶、空档滑行、过长怠速数据
				clearOtherAlarm("1"); // 超速
				clearOtherAlarm("2"); // 疲劳驾驶
				clearOtherAlarm("44"); // 空档滑行
				clearOtherAlarm("45"); // 过长怠速
				clearOtherAlarm("46"); // 怠速空调
				clearOtherAlarm("47"); // 发动机超转

				Set<String> set = dataMap.keySet();
				Iterator<String> it = set.iterator();
				while (it.hasNext()) {
					String key = it.next();
					String data = dataMap.get(key);
					try {
						String[] event = data.split("\\|");

						if (event.length == 3) {

							String[] startPos = event[1].split("\\]", 6);
							String[] endPos = event[2].split("\\]", 6);

							if (startPos.length == 6 && endPos.length == 6) {

								String ky = event[0];
								long beginLon = (Long) formatValueByType(
										startPos[1].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "-1",
										'L');
								long beginLat = (Long) formatValueByType(
										startPos[0].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "-1",
										'L');
								int beginElevation = (Integer) formatValueByType(
										startPos[2].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "0",
										'I');
								Long beginSpeed = (Long) formatValueByType(
										startPos[3].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "0",
										'L');
								int beginDirection = (Integer) formatValueByType(
										startPos[4].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "0",
										'I');
								String startTime = startPos[5].replaceAll(
										"\\[", "").replaceAll("\\]", "");
								long beginUtc = CDate
										.stringConvertUtc(startTime);

								VehicleMessageBean beginVmb = new VehicleMessageBean();
								beginVmb.setLat(beginLat);
								beginVmb.setLon(beginLon);
								beginVmb.setElevation(beginElevation);
								beginVmb.setSpeed(beginSpeed);
								beginVmb.setDir(beginDirection);
								beginVmb.setUtc(beginUtc);
								long[] beginMapPoint = convertLatLonToMap(""
										+ beginLat, "" + beginLon); // 偏移经纬度
								beginVmb.setMaplat(beginMapPoint[0]);
								beginVmb.setMaplon(beginMapPoint[1]);
								
								//匹配当前驾驶员
								String driverinfo = getCurrectDriver(beginUtc);
								if (driverinfo!=null&&!"".equals(driverinfo)){
									String driver[] = driverinfo.split(";");
									beginVmb.setDriverId(driver[0]);
									beginVmb.setDriverName(driver[1]);
									beginVmb.setDriverSrc(driver[2]);
								}
								

								long endLon = (Long) formatValueByType(
										endPos[1].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "-1",
										'L');
								long endLat = (Long) formatValueByType(
										endPos[0].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "-1",
										'L');
								int endElevation = (Integer) formatValueByType(
										endPos[2].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "0",
										'I');
								Long endSpeed = (Long) formatValueByType(
										endPos[3].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "0",
										'L');
								int endDirection = (Integer) formatValueByType(
										endPos[4].replaceAll("\\[", "")
												.replaceAll("\\]", ""), "0",
										'I');
								String endTime = endPos[5]
										.replaceAll("\\[", "").replaceAll(
												"\\]", "");
								long endUtc = CDate.stringConvertUtc(endTime);

								VehicleMessageBean endVmb = new VehicleMessageBean();
								endVmb.setLat(endLat);
								endVmb.setLon(endLon);
								endVmb.setElevation(endElevation);
								endVmb.setSpeed(endSpeed);
								endVmb.setDir(endDirection);
								endVmb.setUtc(endUtc);
								long[] endMapPoint = convertLatLonToMap(""
										+ endLat, "" + endLon); // 偏移经纬度
								endVmb.setMaplat(endMapPoint[0]);
								endVmb.setMaplon(endMapPoint[1]);

								AlarmCacheBean alarmEventBean = new AlarmCacheBean();

								alarmEventBean.setAlarmId(GeneratorPK
										.instance().getPKString());
								alarmEventBean.setAlarmType("EVENT");
								alarmEventBean.setAlarmSrc(1);
								alarmEventBean.setBeginVmb(beginVmb);
								alarmEventBean.setEndVmb(endVmb);
								alarmEventBean.setMaxSpeed(beginSpeed);

								// 状态事件 1加热器工作、2空调工作、5超经济区运行、8二档起步、9档位不当
								if (ky.equals("1") || ky.equals("2")
										|| ky.equals("5") || ky.equals("8")
										|| ky.equals("9")) {
									if (ky.equals("1")) { // 加热器工作
										alarmEventBean.setAlarmcode("EV0001");
									} else if (ky.equals("2")) { // 空调工作
										alarmEventBean.setAlarmcode("EV0002");
									} else if (ky.equals("5")) { // 超经济区运行
										alarmEventBean.setAlarmcode("EV0005");
									} else if (ky.equals("8")) { // 二档起步
										alarmEventBean.setAlarmcode("EV0008");
									} else if (ky.equals("9")) { // 档位不当
										alarmEventBean.setAlarmcode("EV0009");
									}

									stateEventList.add(alarmEventBean);

									this.countState(alarmEventBean);
								}

								// 报警事件明细表中存储10超速、11疲劳驾驶、6空档滑行、4过长怠速、7怠速空调、3发动机超转
								if (ky.equals("3") || ky.equals("4")
										|| ky.equals("6") || ky.equals("7")
										|| ky.equals("10") || ky.equals("11")) {
									// 封装报警事件统计bean
									if (ky.equals("10")) { // 超速
										alarmEventBean.setAlarmcode("1");
									} else if (ky.equals("11")) { // 疲劳驾驶
										alarmEventBean.setAlarmcode("2");
									} else if (ky.equals("6")) { // 空档滑行
										alarmEventBean.setAlarmcode("44");
									} else if (ky.equals("4")) { // 过长怠速
										alarmEventBean.setAlarmcode("45");
									} else if (ky.equals("7")) { // 怠速空调
										alarmEventBean.setAlarmcode("46");
									} else if (ky.equals("3")) { // 发动机超转
										alarmEventBean.setAlarmcode("47");
									}
									alarmList.add(alarmEventBean);
								}
							}
						}

					} catch (Exception ex) {
						logger.error(
								"VID:" + vid + " 解析时间文件过程中出错，出错数据：" + data, ex);
					}

				}// End while

			}
			long serverEndTime = System.currentTimeMillis();
			serverRunTime = serverRunTime + (serverEndTime - serverStartTime)
					/ 1000;
			String message = "----------------------解析驾驶行为事件："
					+ (serverEndTime - serverEndTime) / 1000 + "s";
			logger.info(message);
		} catch (Exception e) {
			logger.error(vid + " 读取驾驶行为事件信息出错.", e);
		} finally {

		}
	}

	/****
	 * 清除在日报警文件统计的超速、疲劳驾驶数据
	 * 
	 * @param alarmCode
	 */
	private void clearOtherAlarm(String alarmCode) {
		LinkedList<Integer> idxList = new LinkedList<Integer>();
		for (int i = 0; i < alarmList.size(); i++) {
			AlarmCacheBean event = alarmList.get(i);
			if (event.getAlarmcode().equals(alarmCode)) { // 获取列表下标
				idxList.addFirst(i);
			}
		}// End for

		if (idxList.size() > 0) {
			for (int i = idxList.size() - 1; i >= 0; i--) {
				Integer idx = idxList.removeFirst();
				alarmList.removeElementAt(idx); // 清除
			}// End for
		}
		idxList.clear();
	}

	/****
	 * 获取报警类型
	 * 
	 * @param v
	 * @param alarmCode
	 */
	private String getAlarmType(String alarmCode) {
		String alarmType = "";
		if (AnalysisDBAdapter.alarmTypeMap.containsKey(alarmCode)) {
			alarmType = AnalysisDBAdapter.alarmTypeMap.get(alarmCode);
		}

		logger.info("alarm code : " + alarmCode + "; alarm type:" + alarmType);

		return alarmType;
	}

	private void readEvent(String path, TreeMap<String, String> map) {
		File file = new File(path);
		List<DataBean> list = new ArrayList<DataBean>();
		if (file.exists()) {
			String readLine = null;
			BufferedReader buf = null;
			try {
				buf = new BufferedReader(new FileReader(file));
				while ((readLine = buf.readLine()) != null) {
					String[] event = readLine.split("\\|");
					if (event.length == 3) {
						String[] startPos = event[1].split("\\]");
						if (startPos.length == 6) {
							String startTime = startPos[5]
									.replaceAll("\\[", "")
									.replaceAll("\\]", "");
							map.put(event[0] + "_"
									+ CDate.stringConvertUtc(startTime),
									readLine); // 滤过GPS 开始时间重复记录,
						}
					}
				}// End while
			} catch (IOException e) {
				if (buf != null) {
					try {
						buf.close();
					} catch (IOException ex) {
						logger.error("关闭文件 " + path + " 出错", ex);
					}
				}

				if (list != null && list.size() > 0) {
					list.clear();
				}
			}
		}
	}

	/***
	 * 添加数据到集合文件
	 * 
	 * @param data
	 * @param time
	 */
	private void addList(String data, String time, List<DataBean> a) {
		long gpsTime = 0;
		gpsTime = CDate.stringConvertUtc(time);
		DataBean db = new DataBean();
		db.setData(data);
		db.setGpsTime(gpsTime);
		a.add(db);
	}

	/****
	 * 对List集合按日期时间排序
	 * 
	 * @param list
	 */
	private void sortList(List<DataBean> list) {
		Collections.sort(list, new LongComparator());
	}

	class LongComparator implements Comparator<Object> {
		public int compare(Object o1, Object o2) {
			DataBean d1 = (DataBean) o1;
			DataBean d2 = (DataBean) o2;
			Long g1 = d1.getGpsTime();
			Long g2 = d2.getGpsTime();
			// 如果有空值，直接返回0
			if (g1 == null || g2 == null)
				return 0;

			return g1.compareTo(g2);
		}
	}

	/**
	 * 封装报警事件统计bean
	 * 
	 * @param 起始位置信息
	 *            ，结束位置信息，事件类型
	 */
	private void addAlarmEventList(String[] startPos, String[] endPos,
			String[] event, String vid, VehicleInfo info,
			TreeMap<Long, String> returnAlarmMap) {
		String beginLat = startPos[0].replaceAll("\\[", "").replaceAll("\\]",
				"");
		String beginLon = startPos[1].replaceAll("\\[", "").replaceAll("\\]",
				"");
		String beginElevation = startPos[2].replaceAll("\\[", "").replaceAll(
				"\\]", "");
		String beginGpsSpeed = startPos[3].replaceAll("\\[", "").replaceAll(
				"\\]", "");
		String beginDirection = startPos[4].replaceAll("\\[", "").replaceAll(
				"\\]", "");
		String beginTime = startPos[5].replaceAll("\\[", "").replaceAll("\\]",
				"");
		String endLat = endPos[0].replaceAll("\\[", "").replaceAll("\\]", "");
		String endLon = endPos[1].replaceAll("\\[", "").replaceAll("\\]", "");
		String endElevation = endPos[2].replaceAll("\\[", "").replaceAll("\\]",
				"");
		String endGpsSpeed = endPos[3].replaceAll("\\[", "").replaceAll("\\]",
				"");
		String endDirection = endPos[4].replaceAll("\\[", "").replaceAll("\\]",
				"");
		String endTime = endPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
		String ky = event[0];
		// 报警事件统计
		VehicleAlarmEvent events = new VehicleAlarmEvent();
		if (ky.equals("10")) { // 超速
			events.setAlarmType(getAlarmType("1"));
			events.setAlarmCode("1");
		} else if (ky.equals("11")) { // 疲劳驾驶
			events.setAlarmType(getAlarmType("2"));
			events.setAlarmCode("2");
		} else if (ky.equals("6")) { // 空档滑行
			events.setAlarmType(getAlarmType("44"));
			events.setAlarmCode("44");
		} else if (ky.equals("4")) { // 过长怠速
			events.setAlarmType(getAlarmType("45"));
			events.setAlarmCode("45");
		} else if (ky.equals("7")) { // 怠速空调
			events.setAlarmType(getAlarmType("46"));
			events.setAlarmCode("46");
		} else if (ky.equals("3")) { // 发动机超转
			events.setAlarmType(getAlarmType("47"));
			events.setAlarmCode("47");
		}

		events.setVid(vid);
		events.setPhoneNumber(info.getCommaddr());
		events.setAREA_ID("");// 电子围栏编号
		events.setMtypeCode("");
		events.setMediaUrl("");
		events.setStartUtc(CDate.stringConvertUtc(beginTime));
		events.setStartLat((Long) formatValueByType(beginLat, "-1", 'L'));
		events.setStartLon((Long) formatValueByType(beginLon, "-1", 'L'));
		long[] beginMapPoint = convertLatLonToMap(beginLat, beginLon); // 偏移经纬度
		events.setStartMapLat(beginMapPoint[0]);
		events.setStartMapLon(beginMapPoint[1]);
		events.setStartElevation((Integer) formatValueByType(beginElevation,
				"0", 'I'));
		events.setStartHead((Integer) formatValueByType(beginDirection, "0",
				'I'));
		events.setStartGpsSpeed((Long) formatValueByType(beginGpsSpeed, "0",
				'L'));

		events.setEndUtc(CDate.stringConvertUtc(endTime));
		events.setEndLat((Long) formatValueByType(endLat, "-1", 'L'));
		events.setEndLon((Long) formatValueByType(endLon, "-1", 'L'));

		long[] endMapPoint = convertLatLonToMap(endLat, endLon); // 偏移经纬度
		events.setEndMapLat(endMapPoint[0]);
		events.setEndMapLon(endMapPoint[1]);
		events.setEndElevation((Integer) formatValueByType(endElevation, "0",
				'I'));
		events.setEndHead((Integer) formatValueByType(endDirection, "0", 'I'));
		events.setEndGpsSpeed((Long) formatValueByType(endGpsSpeed, "0", 'L'));
		long alarmTime = (events.getEndUtc() - events.getStartUtc()) / 1000;

		// 报警事件时长
		if (alarmTime > 0) {
			events.setAccountTime(alarmTime);
		}

		// 最大速度
		if (events.getStartGpsSpeed() > events.getEndGpsSpeed()) {
			events.setMaxSpeed(events.getStartGpsSpeed());
		} else {
			events.setMaxSpeed(events.getEndGpsSpeed());
		}

		events.setAlarmSrc(1);

		// 计算累计里程和累计油耗
		staOilAndMil(events, returnAlarmMap, beginTime, endTime);

		alarmEventList.add(events);
	}

	/****
	 * 偏移经纬度
	 * 
	 * @param lt
	 * @param ln
	 */
	private long[] convertLatLonToMap(String lt, String ln) {
		long pointArr[] = new long[2];
		long lon = Long.parseLong(ln);
		long lat = Long.parseLong(lt);
		long maplon = -100;
		long maplat = -100;
		// 偏移
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		pointArr[0] = maplat;
		pointArr[1] = maplon;
		return pointArr;
	}

	/**
	 * 统计超速报警的累计油耗和里程
	 */
	private void staOilAndMil(VehicleAlarmEvent v,
			TreeMap<Long, String> returnAlarmMap, String startTime,
			String endTime) {
		int startOil = 0;
		int endOil = 0;
		int startMileage = 0;
		int endMileage = 0;
		Long k = null;
		String[] col = null;

		Set<Long> key = returnAlarmMap.keySet();
		for (Iterator<Long> it = key.iterator(); it.hasNext();) {
			k = it.next();
			col = returnAlarmMap.get(k).split(":");
			long tmpStartTime = CDate.stringConvertUtc(startTime);
			long tmpEndTime = CDate.stringConvertUtc(endTime);
			if (k >= tmpStartTime) {
				// 超速累计油耗
				if (!Utils.checkEmpty(col[8])) {
					if (tmpStartTime == k) {
						startOil = Integer.parseInt(col[8]);
					}

					if (tmpEndTime == k) {
						endOil = Integer.parseInt(col[8]);
					}
				}

				// 超速行驶里程
				if (!Utils.checkEmpty(col[9])) {
					if (tmpStartTime == k) {
						startMileage = Integer.parseInt(col[9]);
					}

					if (tmpEndTime == k) {
						endMileage = Integer.parseInt(col[9]);
					}
				}
			} else {
				continue;
			}

			if (startOil > 0 && endOil > 0 && startMileage > 0
					&& endMileage > 0) {
				break;
			}

			if (tmpEndTime <= k) {
				break;
			}
		}// End for
		if (endOil != 0 && startOil != 0) {
			if (endOil > startOil && endOil != 4294967295l
					&& startOil != 4294967295l && endOil != -1
					&& startOil != -1) {
				v.setCostOil(endOil - startOil);
			}
		}
		if (endMileage != 0 && startMileage != 0 && startMileage != 4294967295l
				&& endMileage != 4294967295l && endMileage != -1
				&& startMileage != -1) {
			if (endMileage > startMileage) {
				v.setMileage(endMileage - startMileage);
			}
		}
	}

	/****
	 * 计算报警下行驶里程和油耗
	 * 
	 * @param cols
	 *            结束点
	 * @param startCol
	 *            开始点
	 * @param event
	 *            事件bean
	 */
	private void accountOilAndMelige(String[] cols, String[] startCol,
			VehicleAlarmEvent event) {
		// 计算报警下行驶里程
		if (cols[9] != null && !cols[9].equals("") && startCol[9] != null
				&& !startCol[9].equals("") && !startCol[9].equals("-1")
				&& !cols[9].equals("-1")) {
			int value = (Integer.parseInt(cols[9]) - Integer
					.parseInt(startCol[9]));
			if (value > 0) {
				event.setMileage(value);
			}
		}

		// 计算报警下耗油
		if (cols[8] != null && !cols[8].equals("") && startCol[8] != null
				&& !startCol[8].equals("") && !startCol[8].equals("-1")
				&& !cols[8].equals("-1")) {
			int value = (Integer.parseInt(cols[8]) - Integer
					.parseInt(startCol[8]));
			if (value > 0) {
				event.setCostOil(value);
			}
		}
	}

	private Object formatValueByType(String str, String defaultVal, char type) {
		Object obj = null;
		switch (type) {
		case 'S':
			obj = ((str == null || "".equals(str) || "null".equals(str)) ? defaultVal
					: str.trim());
			break;
		case 'L':
			obj = Long.parseLong((str == null || "".equals(str) || "null"
					.equals(str)) ? defaultVal : str.trim());
			break;
		case 'I':
			obj = Integer.parseInt((str == null || "".equals(str) || "null"
					.equals(str)) ? defaultVal : str.trim());
			break;
		}
		return obj;
	}

	public Vector<AlarmCacheBean> getAlarmList() {
		return alarmList;
	}

	private void lastAlarmTime(String alarmCode, long gpsTime) {
		// 统计最近一次报警的时间
		lastAlarmTimeMap.put(alarmCode, gpsTime);
	}

	public Map<String, Long> getLastAlarmMap() {
		return lastAlarmTimeMap;
	}

	/**
	 * 返回驾驶行为事件中其他事件
	 * 
	 * @return
	 */
	public Vector<AlarmCacheBean> getDriverEventList() {
		return driverEventList;
	}

	/**
	 * 返回状态事件中信息
	 * 
	 * @return
	 */
	public Vector<AlarmCacheBean> getStateEventList() {
		return stateEventList;
	}

	/**
	 * 分析生成车辆状态事件数据
	 * 
	 * @param list
	 * @param cols
	 */
	private void analysisStateEvent(VehicleMessageBean trackBean)
			throws Exception {
		// MAP经度：MAP纬度：GPS时间：GPS
		// 速度：正北方向夹角：车辆状态：报警编码：经度：纬度：海拔：里程：累计油耗：发动机运行总时长：引擎转速（发动机转速）：位置基本信息状态位：报区域/线路报警：冷却液温度：蓄电池电压：瞬时油耗：行驶记录仪速度(km/h)：机油压力：大气压力：发动机扭矩百分比：车辆信号状态：车速来源：系统时间
		// 取出位置基本信息状态位和车辆信号状态
		long gpsTime = trackBean.getUtc();
		String basestatus = trackBean.getBaseStatus();
		String extendstatus = trackBean.getExtendStatus();
		String basestatusCode = Utils.fillString(
				MathUtils.getBinaryString(basestatus), 32);
		String extendstatusCode = Utils.fillString(
				MathUtils.getBinaryString(extendstatus), 32);

		long speed = trackBean.getSpeed();

		boolean isSerialRow = false;

		if (lastLocBean != null) {
			long lastGpsTime = lastLocBean.getUtc();
			long interruptedTime = (gpsTime - lastGpsTime);// 数据间隔时长
			if ((Utils.check("0", basestatusCode) && (interruptedTime) < ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME)
					|| (!Utils.check("0", basestatusCode) && (interruptedTime) < (15 * 60 * 1000))) {
				// 如果点火状态下两次数据上报间隔小于5分钟，或熄火状态下两次数据上报间隔小于15分钟，则认为是连续记录
				isSerialRow = true;
			}
		} else {
			isSerialRow = true;
		}

		if (isSerialRow) {
			VehicleMessageBean tmpBean = trackBean;
			for (int i = 0; i < basestatusCode.length(); i++) {

				if (Utils.check("" + i, basestatusCode)) {
					// 状态开始或状态持续
					if (null == stateMap.get("BS" + String.format("%04d", i))) {
						stateMap.put("BS" + String.format("%04d", i), tmpBean);
					}
					if (maxSpeedMap.get("BS" + String.format("%04d", i)) == null
							|| (speed > (Long) maxSpeedMap.get("BS"
									+ String.format("%04d", i)))) {
						maxSpeedMap.put("BS" + String.format("%04d", i), speed);
					}
					// 中间点
					openningDoorStatus(tmpBean, i, speed);
				} else {
					// 状态结束
					openningDoorStatus(tmpBean, i, speed);
					saveStateEventInfo("BS" + String.format("%04d", i), tmpBean);
				}
			}// End while

			for (int i = 0; i < extendstatusCode.length(); i++) {
				if (Utils.check("" + i, extendstatusCode)) {
					// 状态开始或状态持续
					if (stateMap.get("ES" + String.format("%04d", i)) == null) {
						stateMap.put("ES" + String.format("%04d", i), tmpBean);
					}
					if (maxSpeedMap.get("ES" + String.format("%04d", i)) == null
							|| (speed > (Long) maxSpeedMap.get("ES"
									+ String.format("%04d", i)))) {
						maxSpeedMap.put("ES" + String.format("%04d", i), speed);
					}
				} else {
					// 状态结束
					saveStateEventInfo("ES" + String.format("%04d", i), tmpBean);
				}
			}// End while

		} else {
			// 时间有断点时结束全部未结束的状态
			Set<String> keycode = stateMap.keySet();
			Object[] its = keycode.toArray();

			for (int i = 0; i < its.length; i++) {
				String key = (String) its[i];
				saveStateEventInfo(key, lastLocBean);
			}// End while
		}
	}

	/**
	 * 存储状态事件统计信息
	 * 
	 * @param alarmEventList
	 * @throws SQLException
	 */
	private void saveStateEventInfo(String key, VehicleMessageBean endTrackBean)
			throws Exception {
		// 取出状态起始点信息
		// MAP经度：MAP纬度：GPS时间：GPS 速度：正北方向夹角：车辆状态：报警编码：经度：纬度：海拔：里程：累计油耗：发动机运行总时长：
		// 引擎转速（发动机转速）：位置基本信息状态位：报区域/线路报警：冷却液温度：蓄电池电压：瞬时油耗：行驶记录仪速度(km/h)：机油压力：大气压力：发动机扭矩百分比：车辆信号状态：车速来源：系统时间
		VehicleMessageBean beginTrackBean = stateMap.get(key);

		if (beginTrackBean != null) {

			long mileage = 0; // 排除上报值为-1
			if (endTrackBean.getMileage() > 0
					&& beginTrackBean.getMileage() > 0) {
				mileage = endTrackBean.getMileage()
						- beginTrackBean.getMileage();
			}
			long oil = 0;// 排除上报值为-1
			if (endTrackBean.getOil() > 0 && beginTrackBean.getOil() > 0) {
				oil = endTrackBean.getOil() - beginTrackBean.getOil();
			}
			long time = (endTrackBean.getUtc() - beginTrackBean.getUtc()) / 1000;

			// 添加成对象
			AlarmCacheBean cacheBean = new AlarmCacheBean();
			cacheBean.setAlarmcode(key);
			cacheBean.setBeginVmb(beginTrackBean);
			cacheBean.setEndVmb(endTrackBean);
			cacheBean.setMaxSpeed(maxSpeedMap.get(key));
			if (mileage > 0) {
				cacheBean.setMileage(mileage);
			}

			if (oil > 0) {
				cacheBean.setOil(oil);
			}

			/*
			 * event.setAlarmCode(key); // 临时存储门位
			 * event.setAlarmType(beginCols[27]); // 临时存储进出区域附加信息
			 * 
			 * event.setOpenDoorType(beginCols[33]);
			 */

			stateEventList.add(cacheBean);

			this.countState(cacheBean);

			// 获取开门事件
			if ("BS0013".equals(key) || "BS0014".equals(key)
					|| "BS0015".equals(key) || "BS0016".equals(key)) {
				// 根据开始时间找时间差的上报图片
				String picDetail = getOpenningDoorPicture(beginTrackBean
						.getUtc());
				if (null != picDetail && !"".equals(picDetail)) {
					String[] arr = picDetail.split(";");
					if (arr.length == 3) {
						cacheBean.setMediaUrl(arr[1]);
						cacheBean.setMtypeCode(arr[2]);
					}
				}

				vAlarmEventList.add(cacheBean);
				openingDoorList.add(cacheBean);
			}
			stateMap.remove(key);
			maxSpeedMap.remove(key);
		}
	}

	/*****
	 * 根据开门时间和照片上报时间，获取开门触发拍照UTL
	 * 
	 * @param utc
	 * @return
	 */
	private String getOpenningDoorPicture(long utc) {
		Iterator<String> picUrIt = openningDoorPicList.iterator();
		while (picUrIt.hasNext()) {
			String str = picUrIt.next();
			String[] ky = str.split(";", 3);
			Long picUtc = Long.parseLong(ky[0]);
			if (Math.abs(utc - picUtc) <= 10 * 60 * 1000) {
				return str;
			}
		}// End while

		return "";
	}

	/*****
	 * 查询指定时间内开门或关门触发照片
	 * 
	 * @throws SQLException
	 */
	private void selectOpenningDoorPic() {
		try {
			openningDoorPicList = OracleDBAdapter.selectOpenningDoorPic(dbCon,
					vid, this.utc, this.utc + 24 * 60 * 60 * 1000);
		} catch (Exception e) {
			logger.error("查询指定时段内多媒体信息过程中出错！", e);
		}
	}

	/******
	 * 判断带速开门、区域外开门、区域内开门 判断逻辑： 将一次的开始到结束过程中有带速开门的就标记本次为带速开门
	 * 内部协议定义：0带速开门；1区域外开门；2区域内开门；其他值保留 表字段定义：1正常开门 2带速开门 3区域内开门 4区域外开门
	 * 
	 * @param cols
	 * @param i
	 */
	private void openningDoorStatus(VehicleMessageBean vehicleMessageBean,
			int i, long speed) {
		String stateCode = "BS" + String.format("%04d", i);
		// 获取开门事件
		if ("BS0013".equals(stateCode) || "BS0014".equals(stateCode)
				|| "BS0015".equals(stateCode) || "BS0016".equals(stateCode)) {
			// 判断是否已经是带速开门、区域内开门、区域外开门
			VehicleMessageBean temp = stateMap.get(stateCode);

			if (temp != null) {
				if (null != temp.getOpendoorState()
						&& !"".equals(temp.getOpendoorState())) {
					// 判断0：带速开门；1区域外开门；2：区域内开门；其他值保留,带速开门：车速大于5公里/小时 时开门报警。
					if (null != temp && "0".equals(temp.getOpendoorState())
							&& speed > 50) {
						stateMap.get(stateCode).setOpendoorState("2");
					} else if (null != temp
							&& "2".equals(temp.getOpendoorState())) {
						stateMap.get(stateCode).setOpendoorState("3");
					} else if (null != temp
							&& "1".equals(temp.getOpendoorState())) {

						stateMap.get(stateCode).setOpendoorState("4");
					} else if (null != temp
							&& (null == temp.getOpendoorState() || ""
									.equals(temp.getOpendoorState()))) {
						stateMap.get(stateCode).setOpendoorState("1");
					}
				}
			}
		}
	}

	/****
	 * 统计开门次数，及区域内开门次数和时间。
	 */
	private void accountOpenningCount() {
		if (vAlarmEventList.size() > 0) {
			Iterator<AlarmCacheBean> alarmEventIt = vAlarmEventList.iterator();
			while (alarmEventIt.hasNext()) {
				AlarmCacheBean alarmEvent = alarmEventIt.next();
				if (alarmEvent.getAlarmcode().equals("BS0013")) {
					vehicleStatus.addDoor1(1);
				} else if (alarmEvent.getAlarmcode().equals("BS0014")) {
					vehicleStatus.addDoor2(1);
				} else if (alarmEvent.getAlarmcode().equals("BS0015")) {
					vehicleStatus.addDoor3(1);
				} else if (alarmEvent.getAlarmcode().equals("BS0016")) {
					vehicleStatus.addDoor4(1);
				}
				String enteringArea = alarmEvent.getAlarmType() == null ? ""
						: alarmEvent.getAlarmType();
				String[] res = enteringArea.split("\\|", 3);
				if (res.length == 3 && !"".equals(res[2]) && res[2].equals("0")) {
					long accountTime = (alarmEvent.getEndVmb().getUtc() - alarmEvent
							.getBeginVmb().getUtc()) / 1000;
					if (accountTime > 0) {
						vehicleStatus.addAreaOpenDoorNum(1);
						vehicleStatus.addAreaOpenDoorTime(accountTime);
						alarmEvent.setAlarmcode(ExcConstants.OPENINGDOOR);
					}
				}
			}// End while
		}
	}

	public Vector<AlarmCacheBean> getvAlarmEventList() {
		return vAlarmEventList;
	}

	public Vector<AlarmCacheBean> getOpeningDoorList() {
		return openingDoorList;
	}

	public VehicleStatus getVehicleStatus() {
		return vehicleStatus;
	}

	public Map<String, VehicleAlarm> getStateCountMap() {
		return stateCountMap;
	}
	
	/*****
	 * 根据GPS时间对应当前车辆驾驶员信息
	 * 
	 * @param utc
	 * @return
	 */
	private String getCurrectDriver(long utc) {
		Iterator<String> picUrIt = driverClockinList.iterator();
		String tmpstr = "";
		while (picUrIt.hasNext()) {
			String str = picUrIt.next();
			String[] ky = str.split(";");
			Long beginUtc = Long.parseLong(ky[3]);
			Long endUtc = Long.parseLong(ky[4]);
			if ("0".equals(ky[2])){
				tmpstr = ky[0]+";"+ky[1]+";"+ky[2];
				continue;
			}
			if (utc>=beginUtc && utc <=endUtc) {
				return ky[0]+";"+ky[1]+";"+ky[2];
			}
		}// End while

		return tmpstr;
	}
	
	private String getCurrectDriverName(String driverId) {
		Iterator<String> picUrIt = driverClockinList.iterator();
		while (picUrIt.hasNext()) {
			String str = picUrIt.next();
			String[] ky = str.split(";");
			String id = ky[0];
			if (driverId.equals(id)){
				return ky[1];
			}
		}// End while
		return "";
	}

}
