package com.caits.analysisserver.services;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.database.Envelope;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.GisAccountMeg;

public class VehicleStatusAnalyserService {
	
	private static final Logger logger = LoggerFactory.getLogger(VehicleStatusAnalyserService.class);
	
/**
	 * 完成以下指标计算
	 * STAT_DATE	NUMBER(15)	N			日期utc，填写中午12点
VID	NUMBER(15)	N			车辆编号
ONLINE_TIMES	NUMBER(10)	Y			车辆上线次数（终端成功鉴权次数）
ONLINE_TIME	NUMBER(10)	Y			在线时长（通过车辆上下线记录表计算）单位：秒
ENGINE_ROTATE_TIME	NUMBER(10)	Y			发动机运行时长（终端上报）单位：1bit=0.05h 0=0h
MILEAGE	NUMBER(10)	Y			行驶里程数 单位：1/10Km
OIL_WEAR	NUMBER(17,2)	Y			当日油耗 单位：1bit=0.5L 0=0L
SPEEDING_OIL	NUMBER(10)	Y			本日超速下油耗   单位：1bit=0.5L 0=0L
SPEEDING_MILEAGE	NUMBER(10)	Y			本日超速下行驶里程  单位：1/10Km
SPEED_MAX	NUMBER(10)	Y			本日最高车速  单位：1/10Km/h
RPM_MAX	NUMBER(10)	Y				本日最高转速  单位：1bit=0.125Rpm  0=0Rpm
VCL_GPS_AMOUNT	NUMBER(10)	Y			定位量
VCL_GPS_INVALID_AMOUNT	NUMBER(10)	Y			定位无效数量
VCL_GPS_TIMEINVALID_AMOUNT	NUMBER(10)	Y			GPS时间无效数量(上传时间跟当前时间相差三个月)
VCL_GPS_LONINVALID_AMOUNT	NUMBER(10)	Y			经纬度无效数量
IDLING_TIME	NUMBER(10)	Y			怠速时间    单位：秒
PRECISE_OIL	NUMBER(15)	Y			 精准总油耗 单位为：1bit=0.01L 0=0L
POINT_MILEAGE	NUMBER(15)	Y			最后一点减第一个点计算里程
POINT_OIL	NUMBER(15)	Y			最后一点减第一个点计算油耗
GIS_MILEAGE	NUMBER(15)	Y			GIS计算里程
MET_IDLING_OIL_WEAR	NUMBER(15)	Y			精准怠速下油耗(unit：0.01L)
MET_RUNNING_OIL_WEAR	NUMBER(15)	Y			精准行车油耗(unit：0.01L)
ECU_OIL_WEAR	NUMBER(15)	Y			ECU总油耗(unit：0.5L)
ECU_IDLING_OIL_WEAR	NUMBER(15)	Y			ECU怠速下油耗(unit：0.5L)
ECU_RUNNING_OIL_WEAR	NUMBER(15)	Y			ECU行车油耗(unit：0.5L)
* 
	 */
	
	private VehicleStatus vehicleStatus = new VehicleStatus();
	
	private int count = 0;
	
	private static long intervalTime = 90 * 24 * 60 * 60 * 1000l;
	
	long dayFirstCostOil = 0; // 日第一次累计油耗
	long dayLastCostOil = 0; // 日最后一次累计油耗
	Long maxSpeed = 0L;
	Long maxRpm = 0L;
	Long dayFirstMileage = 0L;
	Long dayLastMileage = 0L;
	long accTime = 0;
	int accCount = 0;
	Long startGpsTime = -1L; // GPS时间
	long startAccGpsTime = 0; // Acc GPS时间
	Long gpsTime = -1L; // GPS时间
	Long lastGpsTime = -1L; // 上一条GPS时间
	boolean acc = false; //标记是否开始统计ACC
	long onlineTime = 0; // 车辆平台在线时间
	
	long runningTime = 0; // 行车时间 
	boolean isEngineFlag = false; //发动机运行
	long startEngineDate = 0; // 发动机运行开始时间
	long engineTime = 0; // 发动机运行时间
	
	long first_precise_oil = 0; //流量计油耗第一条
	long last_precise_oil = 0; //流量计油耗最后一条
	
	boolean isflag = false;
	
	int runCount = 0;   // 行驶时间不足次数
	int runDiffCount = 0;   // 行驶时间过长次数
	//long runningOil = 0l; // 行车油耗
	//long startRunningOil = 0; // 行车开始点累计油耗
	
	int opening_door_ex_num = 0; // 开门异常次数
	
	boolean accStatus = false;
	boolean isRunningNotEnoughTime = false; //行驶时间不足
	boolean isRunningLongTime = false; // 行驶时间过长
	
	private long utc;
	private String vid;
	
	private VehicleMessageBean lastLocBean=null;
	
	StringBuffer latLons = new StringBuffer("latons=");
	
	private Long tmpLastMileage=-1L;
	
	private Long tmpLastOil = -1L;
	
	private Long tmpLastPreciseOil= -1L;
	
	public VehicleStatusAnalyserService(long utc,String vid){
		this.utc = utc;
		this.vid = vid;
		vehicleStatus.setStatDate(this.utc+12*60*60*1000);
		vehicleStatus.setVid(vid);
	}
	
	public void executeAnalyser(VehicleMessageBean trackBean,boolean isLastRow){
		
		Long gpsTime = trackBean.getUtc();
		Long mileage = trackBean.getMileage();
		Long oil = trackBean.getOil();
		Long metOil = trackBean.getMetOil();
		
		//备份值
		if (lastLocBean==null){
			lastLocBean = trackBean;
		}
		
		Long lastGpsTime = lastLocBean.getUtc();
		Long lastMileage = lastLocBean.getMileage();
		Long lastOil = lastLocBean.getOil();
		Long lastMetOil = lastLocBean.getMetOil();
		
		//处理里程油耗：排除里程油耗在内部为-1的情况
		if (mileage>-1){
			tmpLastMileage = mileage;
		}
		if (tmpLastMileage>-1){
			if (mileage == -1){
				mileage = tmpLastMileage;
			}
			if (lastMileage == -1){
				lastMileage = tmpLastMileage;
			}
		}
		
		if (oil>-1){
			tmpLastOil = oil;
		}
		if (tmpLastOil>-1){
			
			if (oil==-1){
				oil = tmpLastOil;
			}
			
			if (lastOil==-1){
				lastOil = tmpLastOil;
			}
			
		}
		
		if (metOil>-1){
			tmpLastPreciseOil = metOil;
		}
		if (tmpLastPreciseOil>-1){
			if (metOil==-1){
				metOil = tmpLastPreciseOil;
			}
			
			if (lastMetOil==-1){
				lastMetOil = tmpLastPreciseOil;
			}
		}
		//存放里程油耗差值
		long mg = 0;
		long tmpOil = 0;
		long tmpPreciseOil = 0;
		
		long timediff = accountOffLineTime(gpsTime,lastGpsTime);//本条记录和上条记录的时间差 ms
		
		//计算在线时长
		onlineTime = onlineTime + timediff;
		
		// 上传时间跟当前时间相差三个月
		if(gpsTime > 0 && (Math.abs(System.currentTimeMillis() - gpsTime) > intervalTime)){ 
			count++;
		}
		
		//分析生成车辆日统计相关数据
		Long lon =  trackBean.getLon();
		Long lat = trackBean.getLat();
		
		double maplon = trackBean.getMaplon()/600000D;
		double maplat = trackBean.getMaplat()/600000D;
		
		
		Long speed = trackBean.getSpeed(); //根据车速来源获取的车速
		
		//根据经纬度计算里程
		if (maplon!=0&&maplat!=0&&Envelope.checkEnvelope(maplat, maplon)){
			latLons.append(maplon);
			latLons.append(",");
			latLons.append(maplat);
			latLons.append(",");
			latLons.append(speed);
			latLons.append(",");
			latLons.append(trackBean.getDateString());
			latLons.append(";");
		}
		
		// 纬度范围18-54(10800000-32400000)  经度范围72-136(43200000-81600000)
		if (lon == null || lon > 43200000 || lon < 81600000 || lat == null || lat < 10800000 || lat > 32400000) {
			vehicleStatus.addCountLatLonInvalid(1);
		}
		
		//累计油耗
		if( oil>0  && dayFirstCostOil == 0){ // 油耗
			dayFirstCostOil = oil;
		}
		
		if(oil>0){ // 油耗
			dayLastCostOil = oil;
		}
		
		//流量计油耗
		if( metOil>0 && first_precise_oil == 0){
			first_precise_oil = metOil;
		}
		
		if(metOil>0 ){
			last_precise_oil = metOil;
		}
		
		// 记录当日开始行驶里程总数
		if(mileage>0 && dayFirstMileage == 0){ 
			dayFirstMileage = mileage;
		}
		
		if(mileage>0){ 
			dayLastMileage = mileage;
		}
		
		//过滤突增数据
		mg = mileage - lastMileage;
		//过滤突增数据
		tmpOil = oil - lastOil;
		
		tmpPreciseOil = metOil - lastMetOil;
		
		/*****
		 * 过滤异常数据,包括里程和油耗,
		 * 里程为异常数据则本次里程负值为0,
		 */
		if(mg >= 0 && mg <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("mileage_interval").getValue()),10f)){
			// 不做处理
		}else{
			mg = 0; 
		}
		
		if(tmpOil >= 0 && tmpOil <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue()),60f)){
			// 不做处理
		}else{
			// 油耗为异常数据则本次里程负值为0,
			tmpOil = 0;
		}
		
		if(tmpPreciseOil >= 0 && tmpPreciseOil <= CDate.accountTimeIntervalVale(gpsTime,lastGpsTime,Integer.parseInt(SystemBaseInfoPool.getinstance().getBaseInfoMap("oil_interval").getValue())*50,60f)){
			// 不做处理
		}else{
			// 油耗为异常数据则本次里程负值为0,
			tmpPreciseOil = 0;
		}
		
		// 根据起步停车累加计算整日里程
		vehicleStatus.addMileage(mg);
		// 根据起步停车累加计算整日油耗
		//vehicleStatus.addCostOil(tmpOil);
		vehicleStatus.addEcuOil(tmpOil);
		vehicleStatus.addPrecise_oil(tmpPreciseOil);
		
		if (trackBean.isGpsState()){
			vehicleStatus.addCountGPSValid(1);   // 定位有效数量
		}else{
			vehicleStatus.addCountGPSInvalid(1); // 定位无效数量
		}
		
		//计算ACC开次数及时长
		if (trackBean.isAccState()){
			if (accStatus){
				accTime = accTime + timediff;
			}
			accStatus = true;
		}else{
			if (accStatus){
				accTime = accTime + timediff;
				accStatus = false;
				accCount++;
			}
		}				

		//发动机运行时长readTrackFile
		//车辆点火且发动机转数大于100转时，表示发动机运行
		//??发动机工作 应采用点火状态 发动机转速 车速 结合来判断
		if ((trackBean.getRpm()*ExcConstants.RPMUNIT>100||speed >= 50)&&trackBean.isAccState()){
			if (isEngineFlag){
				if(timediff <=  ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
					engineTime = engineTime + timediff;
				}
			}
			isEngineFlag =true;
		}else{
			if (isEngineFlag){
				if(timediff <=  ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
					engineTime = engineTime + timediff;
				}
				isEngineFlag = false;
			}
		}
		
		//行车时间
		if(speed >= 50 && trackBean.isAccState()){
			if (isflag){
				if(timediff <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
					runningTime = runningTime + timediff;
				}
				//累加计算整日行车油耗
		    	vehicleStatus.addEcuRunningOil(tmpOil);
		    	vehicleStatus.addMetRunningOil(tmpPreciseOil);
			}
			isflag = true;
		}else {
			if(isflag){
				if(timediff <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
					runningTime = runningTime + timediff;
				}
				//累加计算整日行车油耗
		    	vehicleStatus.addEcuRunningOil(tmpOil);
		    	vehicleStatus.addMetRunningOil(tmpPreciseOil);
				isflag = false;
			}
		}
		
		// 当日最大车速 转换之前速度,排除漂移车速 车速大于300Km/h认为是漂移
		if(trackBean.getSpeed() > maxSpeed && trackBean.getSpeed() < 3000){
			maxSpeed = trackBean.getSpeed();
		}
		
		// 当日最大转速
		if(trackBean.getRpm() > maxRpm){ //未转换后的
			maxRpm = trackBean.getRpm();
		}
		
		lastGpsTime = gpsTime;
		
		if (isLastRow){//当到达最后一行时
			// GIS 计算里程
			if("true".equals(SystemBaseInfoPool.getinstance().getBaseInfoMap("gis_url").getIsLoad())){
				try {
					vehicleStatus.addGis_milege(GisAccountMeg.accountMilege(latLons.toString(),vid));
				} catch (Exception e) {
					// TODO Auto-generated catch block
					logger.error("GIS计算里程出错！",e);
				}
			}
			
			vehicleStatus.setGpsTimeInvild(count);
			vehicleStatus.setEngineTime(engineTime/1000); // 当日发动机运行时长
			vehicleStatus.setAccTime(accTime/1000);
			vehicleStatus.setAccCount(accCount);
			vehicleStatus.setMaxRpm(maxRpm);
			vehicleStatus.setMaxSpeed(maxSpeed);
			vehicleStatus.setOnOffLine(onlineTime/1000);
			vehicleStatus.setIdlingTime(runningTime/1000);
			vehicleStatus.addPoint_milege(dayLastMileage - dayFirstMileage); // 当日行驶里程数公里
			vehicleStatus.addPoint_oil(dayLastCostOil - dayFirstCostOil); //计算当日油耗
		}
		
		lastLocBean = trackBean;
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

	public VehicleStatus getVehicleStatus() {
		return vehicleStatus;
	}
	
	

}
