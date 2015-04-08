package com.ctfo.statistics.alarm.task;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.concurrent.Callable;
import java.util.concurrent.atomic.AtomicInteger;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statistics.alarm.common.Cache;
import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.common.FileUtils;
import com.ctfo.statistics.alarm.common.Utils;
import com.ctfo.statistics.alarm.model.Alarm;
import com.ctfo.statistics.alarm.model.AlarmTemp;
import com.ctfo.statistics.alarm.model.FatigueRules;
import com.ctfo.statistics.alarm.model.FutureResult;
import com.ctfo.statistics.alarm.model.NightRules;
import com.ctfo.statistics.alarm.model.OverspeedRules;
import com.ctfo.statistics.alarm.model.StatisticsParma;
import com.ctfo.statistics.alarm.model.Track;
import com.ctfo.statistics.alarm.model.TrackFile;
import com.ctfo.statistics.alarm.model.VehicleInfo;
import com.ctfo.statistics.alarm.service.OracleService;
/**
 * 告警分析任务
 *
 */
public class AlarmAnalysisTask implements Callable<FutureResult> {
	private static Logger log = LoggerFactory.getLogger(AlarmAnalysisTask.class);
	/** 数据列表	  */
	private List<File> list = new ArrayList<File>();
	/** 工作计数器	  */
	private AtomicInteger workIndex = null;
	/** 计数器	  */
	private int index;
	/** 线程名称	  */
	private String threadName = "AlarmStatistics-";
	/** 日期解析器	(yyyyMMddHHmmss)  */
	private SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
	/** 告警批量提交数量	 */
	private int alarmBatchSize;
	/** 最近处理时间	  */
	private long last = System.currentTimeMillis();
	/** 平台最后告警	  */
	private Alarm platformOverspeedAlarm;
	/** 平台最后临时告警	  */
	private AlarmTemp platformOverspeedAlarmTemp;
	/** 平台最后告警	  */
	private Alarm platformNightAlarm;
	/** 平台最后临时告警	  */
	private AlarmTemp platformNightAlarmTemp;
	/** 夜间非法运营开始时间	  */
	private long nightOperationsStartUtc;
	/** 夜间非法运营结束时间	  */
	private long nightOperationsEndUtc;
	/** Oracle接口	  */
	private OracleService oracleService;
	
	public AlarmAnalysisTask(int i, List<File> fileList, StatisticsParma parma){ 
		try {
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
			threadName += "["+i+"]";
			list = fileList; 
			nightOperationsStartUtc = parma.getStartUtc() + 7200000; 	// 当天凌晨加2小时
			nightOperationsEndUtc = parma.getStartUtc() + 18000000;	// 当天凌晨加5小时
			alarmBatchSize = Integer.parseInt(ConfigLoader.config.get("alarmBatchSize"));
			workIndex = new AtomicInteger(0);
			oracleService = new OracleService("alarm");
			log.info("{} - 线程启动, 夜间非法运营处理时间[{}]~[{}]", threadName, sdf.format(new Date(nightOperationsStartUtc)), sdf.format(new Date(nightOperationsEndUtc)));
		} catch (Exception e) { 
			log.error("读取轨迹文件线程异常:" + e.getMessage(), e);
		}
	}
	
	@Override
	public FutureResult call() throws Exception {
		FutureResult fr = new FutureResult();
		fr.setName(threadName); 
		long begin = System.currentTimeMillis();
		if (list != null && list.size() > 0) {
			int size = list.size();
			fr.setFileSize(size);
			for (File file : list) {
				try {
					if (file != null && file.isFile()) {
						List<String> fileList = FileUtils.readLines(file);
						if (fileList != null && fileList.size() > 0) {
							long start = System.currentTimeMillis();
							TrackFile trackFile = new TrackFile();
							trackFile.setList(fileList);
							String vid = file.getName().replace(".txt", "");
							trackFile.setName(vid);
							log.debug("文件[{}]读取完成, 数据量[{}]行", file.getPath(), fileList.size());
							//  解析告警
							process(trackFile);
							workIndex.incrementAndGet();
							long end = System.currentTimeMillis();
							log.debug("车辆[{}]统计耗时[{}]ms", vid, end - start);
						} else {
							log.error("读取轨迹文件内容[{}]异常, 内容为空或者文件不存在", file.getPath());
						}
					} else {
						log.error("读取轨迹文件异常:", file);
					}
				} catch (Exception e) {
					log.error("读取轨迹文件线程异常:" + e.getMessage(), e);
				}

				long cur = System.currentTimeMillis();
				if (cur - last > 10000) {
					int curIndex = workIndex.get();
					int sucess = size - curIndex;
					int ratio = curIndex * 100 / size;
					log.info("{} - progress-完成[{}%] 未处理[{}]条, 已处理[{}]条, 文件总数[{}]", threadName, ratio, sucess, curIndex, size);
					last = System.currentTimeMillis();
				}
			}
		}
		long over = System.currentTimeMillis();
		fr.setConsuming(over-begin); 
		return fr;
	}
	/**
	 * 处理报警
	 * @param trackFile
	 * TODO
	 */
	public void process(TrackFile trackFile) {
		long start = System.currentTimeMillis();
		List<Alarm> alarmList = new ArrayList<Alarm>();
		int alarmSize = 0;
		int alarmSaveIndex = 0; 
//		获取车辆信息
		VehicleInfo vehicleInfo = Cache.getVehicleInfo(trackFile.getName()); 
		if(vehicleInfo == null){
			log.debug("车辆[{}]在数据库没有对应记录，未处理!",trackFile.getName()); 
			return;
		}
//		获取对象列表
		List<Track> list = getTrackList(trackFile.getList(), trackFile.getName());
		long s1 = System.currentTimeMillis();
//		对象排序 
		Collections.sort(list); 
		long s2 = System.currentTimeMillis();
		Map<String, AlarmTemp> alarmMap = new HashMap<String, AlarmTemp>();
		Map<String, Alarm> alarms = new HashMap<String, Alarm>();
		int trackSize = list.size();
		Track lastTrack = list.get(trackSize - 1);
		Track endTrack = getEndTrack(lastTrack);
		list.add(trackSize, endTrack);
		
		String vid = endTrack.getVid();
		Integer speedThreshold = Cache.getSpeedLimitSettings(vid);
		if(speedThreshold == null){
			speedThreshold = 950;
		}
	
//		报警分析
		for(Track track : list){
			int alarmSource = 1;
			int speed = getSpeed(track); // 获取车速
//			平台超速告警处理
			processPlatformOverspeed(track, vid, speed, vehicleInfo, alarmList);
//			平台夜间非法运营处理
			processPlatformNightOperations(track, vid, speed, vehicleInfo, alarmList);
			
			String lastAlarm = join(alarmMap.keySet());
			String[] alarmArray = Utils.split(track.getAlarmCode(), ",") ;
			StringBuffer startAlarm = new StringBuffer(",");

			if(alarmArray != null && alarmArray.length > 0){
				for(String alarmCode : alarmArray){
//				开始报警
					if(!alarmMap.containsKey(alarmCode)){
						int codeNumber = Integer.parseInt(alarmCode);
						
						if(alarmCode.equals("20") // 过滤进区域告警
								|| alarmCode.equals("23") // 过滤路线偏离告警 
								|| alarmCode.equals("12") // 过滤道路运输证IC卡故障
								|| alarmCode.equals("13") // 过滤超速预警
								|| alarmCode.equals("14") // 过滤疲劳预警
								|| alarmCode.equals("15") // 过滤保留字段
								|| alarmCode.equals("16") // 过滤保留字段
								|| alarmCode.equals("17") // 过滤保留字段
								|| alarmCode.equals("30") // 过滤侧翻预警
								|| alarmCode.equals("31") // 过滤非法开门告警  
								|| codeNumber > 56){ 
							continue;
						} else if(alarmCode.equals("2")){//  疲劳驾驶以平台设置优先 TODO -- 过滤疲劳驾驶报警
							if(filterFatigueAlarm(vid, track)){
//								不在疲劳驾驶规则时间内的不记录疲劳驾驶告警
								continue;
							} else {
								alarmSource = track.getAlarmSource();
							} 
						} else if(alarmCode.equals("1")){ // 超速过滤
//							如果没有在平台上设置超速告警、或者设置了超速告警，但是时间在设置时间内的，就不处理
							if(filterOverspeedAlarm(vid, track)){
//								企业在平台设置了超速告警且超速时间在设置时间内的不处理
								continue;
							} else {
								alarmSource = track.getAlarmSource();
							}
						}
						startAlarm.append(alarmCode).append(",");
						AlarmTemp temp = new AlarmTemp();
//						缓存里程
						if(track.getMileage() > 0){
							temp.setLastMileage(track.getMileage()); 
						}
//						缓存车速
						temp.setMaxSpeed(speed);
						temp.setTotalSpeed(speed);
						temp.setAlarmCode(alarmCode);
						temp.setStartUtc(track.getGpsUtc()); 
						temp.setAlarmSource(alarmSource); 
						alarmMap.put(alarmCode, temp);
//						新建报警
						Alarm alarm = new Alarm(track, vehicleInfo); 
						alarm.setAlarmId(temp.getAlarmId());
						alarm.setStartSpeed(speed); 
						alarm.setAlarmCode(alarmCode); 
						alarm.setAlarmSource(alarmSource); 
						String parentCode = getAlarmParentType(alarmCode);
						if(parentCode == null){
							log.error("----报警编码[{}]没有父级编号", alarmCode); 
						} else {
							alarm.setParentType(parentCode);
						}
						alarm.setSpeedThreshold(vehicleInfo.getSpeedThreshold());
//						设置驾驶员信息
						setDriverInfo(track, alarm);
						
						alarms.put(alarm.getAlarmId(), alarm);
					} else {
//				持续报警
						lastAlarm = lastAlarm.replace("," + alarmCode + "," , ",");
						AlarmTemp temp = alarmMap.get(alarmCode);
						if(temp != null){
//							计算告警总里程
							if(temp.getLastMileage() > 0 && track.getMileage() > temp.getLastMileage()){
								int mileage = track.getMileage() - temp.getLastMileage();
								temp.setLastMileage(track.getMileage()); 
								temp.setAlarmTotalMileage(mileage); 
							}
//							计算车速
							temp.setTotalSpeed(speed);
							if(speed > temp.getMaxSpeed()){
								temp.setMaxSpeed(speed);
							}
						} else {
							log.error("车辆[{}]持续告警[{}]缓存无数据", vid, track.getAlarmCode()); 
						}
					}
				}
			}
			String[] result = Utils.split(lastAlarm, ",");
			StringBuffer endAlarm = new StringBuffer(",");
//				结束报警
			if(result != null){
				for(String alarmCode : result){
					endAlarm.append(alarmCode).append(",");
					AlarmTemp temp = alarmMap.get(alarmCode);
					if(track.getGpsUtc() > temp.getStartUtc()){
						if(temp.getLastMileage() > 0 && track.getMileage() > temp.getLastMileage()){
							int mileage = track.getMileage() - temp.getLastMileage();
							temp.setAlarmTotalMileage(mileage); 
						}
						Alarm alarm = alarms.get(temp.getAlarmId());
						String alarmLevelStr = alarm.getTeamId() +"_"+ alarm.getAlarmCode();
						String level = Cache.getTeamAlarmLevel(alarmLevelStr);
						if(Utils.isNumeric(level)){
							alarm.setAlarmLevel(Integer.parseInt(level)); 
						}else{
							alarm.setAlarmLevel(5);
						} 
						alarm.setEndUtc(track.getGpsUtc());
						alarm.setEndLon(track.getLon());
						alarm.setEndLat(track.getLat());
						alarm.setAlarmTotalMileage(temp.getAlarmTotalMileage());
						alarm.setAvgSpeed(temp.getAvgSpeed());//获取[平均车速]值
						alarm.setMaxSpeed(temp.getMaxSpeed());
						alarm.setEndSpeed(speed);// 告警结束速度
						alarm.setAlarmSource(temp.getAlarmSource());
						alarm.setSpeedThreshold(speedThreshold);
						alarmList.add(alarm);
						alarmSaveIndex++;
						if(alarmSaveIndex == alarmBatchSize){
							alarmSize += alarmList.size();
							long saveStart = System.currentTimeMillis();
							oracleService.insertAlarmList(alarmList);
							alarmList.clear();
							long saveEnd = System.currentTimeMillis();
							log.info("{} - 存储[{}]条报警耗时[{}]ms", threadName, index, saveEnd - saveStart);
							alarmSaveIndex = 0;
						}
						alarmMap.remove(alarmCode);
					}
				}
			}
			log.debug("车辆[{}]-时间[{}]-最近报警[{}], 当前报警[{}], 开始告警[{}], 结束告警[{}]", vid,track.getGpsUtc(),lastAlarm,track.getAlarmCode(),startAlarm,endAlarm);
		}
		if(alarmList.size() > 0){
			alarmSize += alarmList.size();
			oracleService.insertAlarmList(alarmList);
		}
		long end = System.currentTimeMillis();
		log.info("车辆[{}], 记录[{}]条, 处理数量[{}], 有效告警[{}]条,  总耗时[{}]ms, 对象转换耗时[{}]ms, 排序耗时[{}]ms, 处理耗时[{}]",trackFile.getName(),trackFile.getList().size(), list.size(),alarmSize, end-start, s1-start,s2-s1,end-s2);
	} 
	/**
	 * 设置驾驶员信息
	 * @param track
	 * @param alarm
	 */
	private void setDriverInfo(Track track, Alarm alarm) {
		if(track.getDriverId() != null){
			alarm.setDriverId(track.getDriverId());
			alarm.setDriverSource(track.getDriverSource());
		}else{
			String driverId = Cache.getDriverIdByAlarmStartUtc(track.getVid(), track.getGpsUtc());
			if(driverId != null){
				alarm.setDriverId(driverId);
				alarm.setDriverSource(2);
			}else{
				alarm.setDriverId("-1");
				alarm.setDriverSource(-1);
			}
		}
	}

	/**
	 * 平台设置夜间非法运营处理
	 * @param track
	 * @param speed 
	 * @param vehicleInfo 
	 * @param alarmList 
	 * TODO
	 */
	private void processPlatformNightOperations(Track track, String vid, int speed, VehicleInfo vehicleInfo, List<Alarm> alarmList) {
		List<NightRules> rules = Cache.getNightRules(vid);
		long gpsUtc = track.getGpsUtc();
		if (rules != null && rules.size() > 0) {
			long runTatol = getPlatformNightOperations(rules, gpsUtc, vid);
			if (runTatol > -1 && speed > 50) {
//				处理告警开始
				processNightAlarmStart(track, speed, vehicleInfo, 2, runTatol);
				log.debug("车辆[{}]当前时间[{}]满足夜间非法运营速度规则[{}]", vid, gpsUtc, speed);
			} else {
//				处理告警结束
				processNightAlarmEnd(track, speed, vehicleInfo, alarmList); 
			}
		} else {
			// 没有平台设置的车辆 - 凌晨2点致5点之间默认处理
			if (gpsUtc > nightOperationsStartUtc && gpsUtc < nightOperationsEndUtc && speed > 50) {
//				处理报警开始
				processNightAlarmStart(track, speed, vehicleInfo, 1, 0); 
				log.debug("车辆[{}]当前时间[{}]满足夜间非法运营速度规则[{}]时间规则[{}]~[{}]", vid, gpsUtc, speed, nightOperationsStartUtc, nightOperationsEndUtc);
			} else {
//				处理告警结束
				processNightAlarmEnd(track, speed, vehicleInfo, alarmList); 
			}
		}
	}
	/**
	 * 处理夜间非法运营告警结束
	 * @param track
	 * @param speed
	 * @param vehicleInfo
	 * @param alarmList
	 */
	private void processNightAlarmEnd(Track track, int speed, VehicleInfo vehicleInfo, List<Alarm> alarmList) {
		// 结束告警
		if (platformNightAlarm != null) { 
			if (track.getGpsUtc() >= platformNightAlarmTemp.getStartUtc()) {
				if (platformNightAlarmTemp.getLastMileage() > 0 && track.getMileage() > platformNightAlarmTemp.getLastMileage()) {
					int mileage = track.getMileage() - platformNightAlarmTemp.getLastMileage();
					platformNightAlarmTemp.setAlarmTotalMileage(mileage);
				}
				platformNightAlarm.setEndUtc(track.getGpsUtc());
				platformNightAlarm.setEndLon(track.getLon());
				platformNightAlarm.setEndLat(track.getLat());
				platformNightAlarm.setAlarmTotalMileage(platformNightAlarmTemp.getAlarmTotalMileage());
				platformNightAlarm.setAvgSpeed(platformNightAlarmTemp.getAvgSpeed());// 获取[平均车速]值
				platformNightAlarm.setMaxSpeed(platformNightAlarmTemp.getMaxSpeed());
				platformNightAlarm.setAlarmSource(platformNightAlarmTemp.getAlarmSource());
				platformNightAlarm.setEndSpeed(speed); 
//				设置告警级别
				String alarmLevelStr = platformNightAlarm.getTeamId() +"_110";
				String level = Cache.getTeamAlarmLevel(alarmLevelStr);
				if(Utils.isNumeric(level)){
					platformNightAlarm.setAlarmLevel(Integer.parseInt(level)); 
				}else{
					platformNightAlarm.setAlarmLevel(5);
				} 
//				设置驾驶员
				setDriverInfo(track, platformNightAlarm);
//				存储告警
				if(platformNightAlarmTemp.getAlarmSource() == 2 && platformNightAlarm.getDuration() < platformNightAlarmTemp.getRunTatol()){ 
					log.debug("车辆[{}]告警时长[{}]小于规则时长[{}]", track.getVid(), platformNightAlarm.getDuration(),platformNightAlarmTemp.getRunTatol());
				} else {
					alarmList.add(platformNightAlarm);
				}
			}
			platformNightAlarm = null;
			platformNightAlarmTemp = null;
		}
	}
	/**
	 * 处理夜间非法运营告警开始
	 * @param track
	 * @param speed
	 * @param vehicleInfo
	 * @param alarmSource
	 * @param runTatol 
	 */
	private void processNightAlarmStart(Track track, int speed, VehicleInfo vehicleInfo, int alarmSource, long runTatol) {
		// 告警开始 
		if (platformNightAlarm == null) {
			platformNightAlarmTemp = new AlarmTemp();
			// 缓存里程
			if (track.getMileage() > 0) {
				platformNightAlarmTemp.setLastMileage(track.getMileage());
			}
			// 缓存车速
			platformNightAlarmTemp.setMaxSpeed(speed);
			platformNightAlarmTemp.setTotalSpeed(speed);
			platformNightAlarmTemp.setAlarmCode("110");
			platformNightAlarmTemp.setStartUtc(track.getGpsUtc());
			platformNightAlarmTemp.setAlarmSource(alarmSource);
			platformNightAlarmTemp.setRunTatol(runTatol); 
			
			platformNightAlarm = new Alarm(track, vehicleInfo);
			platformNightAlarm.setAlarmId(UUID.randomUUID().toString().replace("-", ""));
			platformNightAlarm.setAlarmCode("110");
			platformNightAlarm.setAlarmSource(alarmSource);
			platformNightAlarm.setParentType("A001");
			platformNightAlarm.setSpeedThreshold(track.getPlatformSpeedThreshold());
			platformNightAlarm.setStartSpeed(speed); 
		} else {
			// 告警持续
			if (platformNightAlarmTemp != null) {
				// 计算告警总里程
				if (platformNightAlarmTemp.getLastMileage() > 0 && track.getMileage() > platformNightAlarmTemp.getLastMileage()) {
					int mileage = track.getMileage() - platformNightAlarmTemp.getLastMileage();
					platformNightAlarmTemp.setLastMileage(track.getMileage());
					platformNightAlarmTemp.setAlarmTotalMileage(mileage);
				}
				// 计算车速
				platformNightAlarmTemp.setTotalSpeed(speed);
				if (speed > platformNightAlarmTemp.getMaxSpeed()) {
					platformNightAlarmTemp.setMaxSpeed(speed);
				}
			} else {
				log.error("车辆[{}]持续告警[{}]缓存无数据", track.getVid(), track.getAlarmCode());
			}
		}
	}

	/**
	 * 获得平台设置夜间非法营运时长设置
	 * @param rules
	 * @param gpsUtc
	 * @return
	 */
	private long getPlatformNightOperations(List<NightRules> rules, long gpsUtc, String vid) {
		for(NightRules rule : rules){
			if(gpsUtc > rule.getStartUtc() && gpsUtc < rule.getEndUtc()){
				log.debug("车辆[{}]当前时间[{}]满足夜间非法运营时间规则[{}]~[{}]", vid, gpsUtc, rule.getStartUtc(), rule.getEndUtc());
				return rule.getRunTotal();
			}
		}
		return -1;
	}

	/**
	 * 平台设置超速告警处理
	 * @param track
	 * @param vid 
	 * @param speed 
	 * TODO
	 * @param vehicleInfo 
	 * @param alarmList 
	 */
	private void processPlatformOverspeed(Track track, String vid, int speed, VehicleInfo vehicleInfo, List<Alarm> alarmList) {
//		满足平台超速告警规则
		if(filterPlatformOverspeedAlarm(vid,track, speed)){
			if(platformOverspeedAlarm == null){
//			告警开始
				platformOverspeedAlarmTemp = new AlarmTemp();
//				缓存里程
				if(track.getMileage() > 0){
					platformOverspeedAlarmTemp.setLastMileage(track.getMileage()); 
				}
//				缓存车速
				platformOverspeedAlarmTemp.setMaxSpeed(speed);
				platformOverspeedAlarmTemp.setTotalSpeed(speed);
				platformOverspeedAlarmTemp.setAlarmCode("1");
				platformOverspeedAlarmTemp.setStartUtc(track.getGpsUtc()); 
				platformOverspeedAlarmTemp.setAlarmSource(2); 
				
				platformOverspeedAlarm = new Alarm(track, vehicleInfo); 
				platformOverspeedAlarm.setAlarmId(UUID.randomUUID().toString().replace("-", ""));
				platformOverspeedAlarm.setAlarmCode("1"); 
				platformOverspeedAlarm.setAlarmSource(2); 
				platformOverspeedAlarm.setStartSpeed(speed);
				platformOverspeedAlarm.setParentType("A001");
				platformOverspeedAlarm.setSpeedThreshold(track.getPlatformSpeedThreshold()); 
				platformOverspeedAlarm.setAlarmLevel(1); // TODO
			} else {
//			告警持续
				if(platformOverspeedAlarmTemp != null){
//					计算告警总里程
					if(platformOverspeedAlarmTemp.getLastMileage() > 0 && track.getMileage() > platformOverspeedAlarmTemp.getLastMileage()){
						int mileage = track.getMileage() - platformOverspeedAlarmTemp.getLastMileage();
						platformOverspeedAlarmTemp.setLastMileage(track.getMileage()); 
						platformOverspeedAlarmTemp.setAlarmTotalMileage(mileage); 
					}
//					计算车速
					platformOverspeedAlarmTemp.setTotalSpeed(speed);
					if(speed > platformOverspeedAlarmTemp.getMaxSpeed()){
						platformOverspeedAlarmTemp.setMaxSpeed(speed);
					}
				} else {
					log.error("车辆[{}]持续告警[{}]缓存无数据", vid, track.getAlarmCode()); 
				}
			}
		} else {
//		结束告警
			if(platformOverspeedAlarm != null){
				if(track.getGpsUtc() >= platformOverspeedAlarmTemp.getStartUtc()){
					if(platformOverspeedAlarmTemp.getLastMileage() > 0 && track.getMileage() > platformOverspeedAlarmTemp.getLastMileage()){
						int mileage = track.getMileage() - platformOverspeedAlarmTemp.getLastMileage();
						platformOverspeedAlarmTemp.setAlarmTotalMileage(mileage); 
					}
					platformOverspeedAlarm.setEndUtc(track.getGpsUtc());
					platformOverspeedAlarm.setEndLon(track.getLon());
					platformOverspeedAlarm.setEndLat(track.getLat());
					platformOverspeedAlarm.setAlarmTotalMileage(platformOverspeedAlarmTemp.getAlarmTotalMileage());
					platformOverspeedAlarm.setAvgSpeed(platformOverspeedAlarmTemp.getAvgSpeed());//获取[平均车速]值
					platformOverspeedAlarm.setMaxSpeed(platformOverspeedAlarmTemp.getMaxSpeed());
					platformOverspeedAlarm.setAlarmSource(platformOverspeedAlarmTemp.getAlarmSource());
					platformOverspeedAlarm.setEndSpeed(speed); 
//					设置告警级别
					String alarmLevelStr = platformOverspeedAlarm.getTeamId() +"_1";
					String level = Cache.getTeamAlarmLevel(alarmLevelStr);
					if(Utils.isNumeric(level)){ 
						platformOverspeedAlarm.setAlarmLevel(Integer.parseInt(level)); 
					}else{
						platformOverspeedAlarm.setAlarmLevel(5);
					} 
//					设置驾驶员
					setDriverInfo(track, platformOverspeedAlarm);
					
					alarmList.add(platformOverspeedAlarm);
				}
				platformOverspeedAlarm = null;
				platformOverspeedAlarmTemp = null;
			}
		}
	}

	/**
	 * 获取车速
	 * @param track
	 * @return
	 */
	private int getSpeed(Track track) {
		if(track.getSpeedSource() == 0){
			return track.getVssSpeed();
		}
		return track.getGpsSpeed(); 
	}

	/**
	 * 获取报警父类型
	 * @param alarmCode
	 * @return
	 *  TODO
	 */
	private String getAlarmParentType(String alarmCode) {
		return Cache.getAlarmParentCode(alarmCode); 
	}
	/**
	 * 平台超速告警过滤规则 
	 * <br>（平台上设置超速告警，且告警时间在设置时间内的，速度大于设置值  true ,<br> 没有报警设置、时间不在设置时间内、速度未到设置值的 false）
	 * @param vid
	 * @param track
	 * @return 
	 */
	private boolean filterPlatformOverspeedAlarm(String vid, Track track, int speed) {
		List<OverspeedRules> rules = Cache.getOverspeedRules(vid);
		long alarmUtc = track.getGpsUtc();
		if(rules != null && rules.size() > 0){
			for(OverspeedRules rule : rules){
				if(alarmUtc > rule.getStartUtc() && alarmUtc < rule.getEndUtc() && speed > rule.getSpeedLimit()){
					track.setPlatformSpeedThreshold(rule.getSpeedLimit()); 
					log.debug("车辆[{}]当前时间[{}]速度[{}]满足平台设置告警规则[{}]~[{}]速度[{}]", vid, alarmUtc,speed, rule.getStartUtc(),rule.getEndUtc(), rule.getSpeedLimit()); 
					return true;
				}
			}
		} 
		return false;
	}
	/**
	 * 终端超速告警过滤规则 <br>
	 * @param vid
	 * @param track
	 * @return （如果平台上设置超速告警，且告警时间在设置时间内的，true , 没有报警设置或者时间不在设置时间内的 false）
	 * TODO
	 */
	private boolean filterOverspeedAlarm(String vid, Track track) {
		List<OverspeedRules> rules = Cache.getOverspeedRules(vid);
		long alarmUtc = track.getGpsUtc();
		if(rules != null && rules.size() > 0){
			track.setAlarmSource(2);
			for(OverspeedRules rule : rules){
				if(alarmUtc > rule.getStartUtc() && alarmUtc < rule.getEndUtc()){
//					log.debug("车辆[{}]终端超速告警时间[{}]在告警规则内[{}]~[{}]", vid, alarmUtc, rule.getStartUtc(),rule.getEndUtc()); 
					return true;
				}
			}
		} 
		track.setAlarmSource(1);
		return false;
	}
	/**
	 * 疲劳告警设置规则过滤
	 * @param track
	 * @return 告警时间在规则内 false 告警时间在规则外 true
	 * TODO
	 */
	private boolean filterFatigueAlarm(String vid, Track track) {
		List<FatigueRules> rules = Cache.getFatigueRules(vid);
		long alarmUtc = track.getGpsUtc();
		if(rules != null && rules.size() > 0){
			track.setAlarmSource(2);
			for(FatigueRules rule : rules){ // 告警开始时间在规则时间内的告警
				if(alarmUtc > rule.getStartUtc() && alarmUtc < rule.getEndUtc()){
					log.debug("车辆[{}]疲劳驾驶告警时间[{}]在告警规则内[{}]~[{}]", vid, alarmUtc, rule.getStartUtc(),rule.getEndUtc()); 
					return false;
				}
			}
			return true;
		} else {
			track.setAlarmSource(1);
			return false;
		}
	}


	/**
	 * 增加一个没有报警的最后轨迹
	 * @param lastTrack
	 * @return
	 */
	private Track getEndTrack(Track lastTrack) {
		Track t = new Track();
		t.setAlarmCode(""); // 将最后一条记录告警设置为空
		t.setVid(lastTrack.getVid()); 
		t.setLon(lastTrack.getLon());
		t.setLat(lastTrack.getLat()); 
		t.setGpsUtc(lastTrack.getGpsUtc()+1000);
		t.setDirection(lastTrack.getDirection());
		t.setElevation(lastTrack.getElevation());
		t.setGpsSpeed(0);
		t.setSpeedSource(1);
		t.setVssSpeed(lastTrack.getVssSpeed());
		t.setMileage(lastTrack.getMileage()); 
		return t;
	}

	/**
	 * 获取轨迹列表
	 * @param list
	 * @return
	 * TODO
	 */
	private List<Track> getTrackList(List<String> list, String name) {
		List<Track> trackList = new ArrayList<Track>();
		for(String str : list){
			String[] array = Utils.splitPreserveAllTokens(str, ":");
			if(array != null && array.length > 38){
				try{
					Track track = getTrack(array, name);
					if(track != null){
						trackList.add(track);
					} else {
						log.error("车辆[{}]解析轨迹异常[{}]", name, str);
					}
				}catch(Exception e){
					log.error("车辆["+name+"]解析轨迹["+str+"]异常:" + e.getMessage(), e);
				}
			} else {
				log.error("车辆[{}]轨迹中有异常数据[{}]", name, str);
 			}
		}
		return trackList;
	}

	/**
	 * 获取轨迹对象
	 * @param array
	 * @param vid 
	 * @return
	 * TODO
	 */
	private Track getTrack(String[] array, String vid) {
			Track track = new Track();
			track.setVid(vid);
	//		偏移经度0
			String mapLon = array[0];
			if(Utils.isNumeric(mapLon)){
				track.setMapLon(Long.parseLong(mapLon)); 
			}
	//		偏移纬度1
			String mapLat = array[1];
			if(Utils.isNumeric(mapLat)){
				track.setMapLat(Long.parseLong(mapLat)); 
			}
	//		GPS时间2
			String gpsTime = array[2].replace("/", "");
			if(Utils.isNumeric(gpsTime) && gpsTime.length() == 14){
				try {
					Date date = sdf.parse(gpsTime);
					track.setGpsUtc(date.getTime()); 
					track.setGpsTime(gpsTime);
					track.setGpsDateUtc(Integer.parseInt(gpsTime.substring(8))); 
				} catch (Exception e) {
					return null;
				}
			} 
	//		GPS速度3
			String gpsSpeed = array[3];
			if(Utils.isNumeric(gpsSpeed)){
				track.setGpsSpeed(Integer.parseInt(gpsSpeed)); 
			}
	//		方向(度)4
			String direction = array[4];
			if(Utils.isNumeric(direction)){
				track.setDirection(Integer.parseInt(direction)); 
			}
	//		基本状态(经度、纬度异常:1; 速度异常:2; 时间异常:4; 方向异常:8;  ) 5
			String status = array[5];
			if(Utils.isNumeric(status)){
				track.setStatus(Integer.parseInt(status)); 
			}
	//		报警编码6
			String alarmCode = array[6];
			track.setAlarmCode(alarmCode); 
	//		原始经度7
			String lon = array[7];
			if(Utils.isNumeric(lon)){
				track.setLon(Long.parseLong(lon)); 
			}
	//		原始纬度8
			String lat = array[8];
			if(Utils.isNumeric(lat)){
				track.setLat(Long.parseLong(lat)); 
			}
	//		海拔(米)9
			String elevation = array[9];
			if(Utils.isNumeric(elevation)){
				track.setElevation(Integer.parseInt(elevation)); 
			}
	//		里程10
			String mileage = array[10];
			if(Utils.isNumeric(mileage)){
				long m = Long.parseLong(mileage);
				if(m < Integer.MAX_VALUE){
					track.setMileage(Integer.parseInt(mileage));  
				}
			}
	//		累计油耗11
			String cumulativeFuel = array[11];
			if(Utils.isNumeric(cumulativeFuel)){
				track.setCumulativeFuel(Long.parseLong(cumulativeFuel)); 
			}
	//		发动机运行总时长12
			String engineRunTotal = array[12];
			if(Utils.isNumeric(engineRunTotal)){
				track.setEngineRunTotal(Long.parseLong(engineRunTotal)); 
			}
	//		引擎转速（发动机转速）13
			String engineSpeed = array[13];
			if(Utils.isNumeric(engineSpeed)){
				track.setEngineSpeed(Integer.parseInt(engineSpeed)); 
			}
	//		位置基本信息状态位14
			String baseStatus = array[14];
			if(Utils.isNotBlank(baseStatus)){ 
				track.setBaseStatus(baseStatus);
			}
	//		区域/线路报警附加信息15
			String alarmAdded = array[15];
			if(Utils.isNotBlank(alarmAdded)){
				track.setAlarmAdded(alarmAdded); 
			}
	//		冷却液温度16
			String eWaterTemp = array[16];
			if(Utils.isNumeric(eWaterTemp)){
				track.seteWaterTemp(Long.parseLong(eWaterTemp));
			}
	//		蓄电池电压17
			String batteryVoltage = array[17];
			if(Utils.isNumeric(batteryVoltage)){
				track.setBatteryVoltage(Long.parseLong(batteryVoltage)); 
			}
	//		瞬时油耗18
			String oilInstant = array[18];
			if(Utils.isNumeric(oilInstant)){
				track.setOilInstant(Long.parseLong(oilInstant)); 
			}
	//		行驶记录仪速度(km/h)19
			String vssSpeed = array[19];
			if(Utils.isNumeric(vssSpeed)){
				track.setVssSpeed(Integer.parseInt(vssSpeed)); 
			}
	//		机油压力 (20 COL)
			String oilPressure = array[20];
			if(Utils.isNumeric(oilPressure)){
				track.setOilPressure(Long.parseLong(oilPressure)); 
			}
	//		大气压力21
			String atmosphericPressure = array[21];
			if(Utils.isNumeric(atmosphericPressure)){
				track.setAtmosphericPressure(Long.parseLong(atmosphericPressure)); 
			}
	//		 发动机扭矩百分比，1bit=1%，0=-125%    22
			String engineTorque = array[22];
			if(Utils.isNumeric(engineTorque)){
				track.setEngineTorque(Long.parseLong(engineTorque)); 
			}
	//		车辆信号状态 23
			String signalStatus = array[23];
			if(Utils.isNotBlank(signalStatus)){
				track.setSignalStatus(signalStatus);
			}
	//		车速来源 24
			String speedSource = array[24];
			if(Utils.isNumeric(speedSource)){
				track.setSpeedSource(Integer.parseInt(speedSource)); 
			} else {
				track.setSpeedSource(1); // 默认取GPS速度
			}
	//		油量（对应仪表盘读数） 25 
			String oilMeasure = array[25];
			if(Utils.isNumeric(oilMeasure)){
				track.setOilMeasure(Long.parseLong(oilMeasure)); 
			}
	//		超速报警附加信息 26
			String overspeedAlarmAdded = array[26];
			if(Utils.isNotBlank(overspeedAlarmAdded)){
				track.setOverspeedAlarmAdded(overspeedAlarmAdded); 
			}
	//		路线行驶时间不足/过长 27
			String lineError = array[27];
			if(Utils.isNotBlank(lineError)){
				track.setLineError(lineError);
			}
	//		油门踏板位置，(1bit=0.4%，0=0%) 28 
			String throttlePedalPosition = array[28];
			if(Utils.isNumeric(throttlePedalPosition)){
				track.setThrottlePedalPosition(Long.parseLong(throttlePedalPosition)); 
			}
	//		终端内置电池电压 29
			String builtBatteryVoltage = array[29];
			if(Utils.isNumeric(builtBatteryVoltage)){
				track.setBuiltBatteryVoltage(Long.parseLong(builtBatteryVoltage)); 
			}
	//		发动机水温 30
			String engineWaterTemperature = array[30];
			if(Utils.isNumeric(engineWaterTemperature)){
				track.setEngineWaterTemperature(Long.parseLong(engineWaterTemperature)); 
			}
	//		机油温度 31
			String oilTemperature = array[31];
			if(Utils.isNumeric(oilTemperature)){
				track.setOilTemperature(Long.parseLong(oilTemperature)); 
			}
	//		进气温度 32
			String inletTemperature = array[32];
			if(Utils.isNumeric(inletTemperature)){
				track.setInletTemperature(Long.parseLong(inletTemperature)); 
			}
	//		开门状态 33
			String openState = array[33];
			if(Utils.isNotBlank(openState)){
				track.setOpenState(openState);
			}
	//		需要人工确认报警事件的ID 34
			String manualAlarmId = array[34];
			if(Utils.isNotBlank(manualAlarmId)){
				track.setManualAlarmId(manualAlarmId);
			}
	//		计量仪油耗，1bit=0.01L,0=0L 35
			String preciseFuel = array[35];
			if(Utils.isNumeric(preciseFuel)){
				track.setPreciseFuel(Long.parseLong(preciseFuel)); 
			}
	//		驾驶员编号36
			String driverId = array[36];
			if(Utils.isNotBlank(lat)){
				track.setDriverId(driverId);
			}
	//		驾驶员信息来源37 
			String driverSource = array[37];
			if(Utils.isNumeric(driverSource)){
				track.setDriverSource(Integer.parseInt(driverSource));
			}else{
				track.setDriverSource(-1); 
			}
	//		系统时间 38
			String sysUtc = array[38];
			if(Utils.isNumeric(sysUtc)){
				track.setSysUtc(Long.parseLong(sysUtc)); 
			}
			
			return track;
	}
	/**
	 * 连接key字符串
	 * @param keySet
	 * @return
	 * TODO
	 */
	private String join(Set<String> keySet) {
		if(keySet == null || keySet.size() == 0){
			return "";
		}
		StringBuffer sb = new StringBuffer(",");
		for(String str : keySet){
			sb.append(str).append(",");
		}
		return sb.toString(); 
	}
	
}
