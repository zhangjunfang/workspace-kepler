package com.ctfo.mileageservice.service;

import com.ctfo.mileageservice.model.VehicleMessageBean;
import com.ctfo.mileageservice.model.VehicleStatus;
import com.ctfo.mileageservice.util.DateTools;


/**
 * 文件名：VehicleStatusService.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-25上午9:45:27
 * 
 */
public class VehicleMileageService {
	
	//private static final Logger logger = LoggerFactory.getLogger(VehicleMileageService.class);
	
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

	Long dayFirstMileage = 0L;
	Long dayLastMileage = 0L;

	
	boolean isflag = false;
	
	private long utc;
	private String vid;
	
	private VehicleMessageBean lastLocBean=null;
	
	private Long tmpLastMileage=-1L;
	
	
	
	
	public VehicleMileageService(long utc,String vid){
		this.utc = utc;
		this.vid = vid;
		vehicleStatus.setStatDate(this.utc+12*60*60*1000);
		vehicleStatus.setVid(this.vid);
	}
	
	public void executeAnalyser(VehicleMessageBean trackBean,boolean isLastRow){
		
		Long gpsTime = trackBean.getUtc();
		Long mileage = trackBean.getMileage();
		//备份值
		if (lastLocBean==null){
			lastLocBean = trackBean;
		}
		Long lastGpsTime = lastLocBean.getUtc();
		Long lastMileage = lastLocBean.getMileage();
		

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
		
	
		//存放里程油耗差值
		long mg = 0;
		


		// 记录当日开始行驶里程总数
		if(mileage>0 && dayFirstMileage == 0){ 
			dayFirstMileage = mileage;
		}
		
		if(mileage>0){ 
			dayLastMileage = mileage;
		}
		
		//过滤突增数据
		mg = mileage - lastMileage;
		
		/*****
		 * 过滤异常数据,包括里程和油耗,
		 * 里程为异常数据则本次里程负值为0,
		 */
		if(mg >= 0 && mg <= DateTools.accountTimeIntervalVale(gpsTime,lastGpsTime,5,10f)){
			// 不做处理
		}else{
			mg = 0; 
		}
		
		// 根据起步停车累加计算整日里程
		vehicleStatus.addMileage(mg);

	
		
		if (isLastRow){//当到达最后一行时
			//System.out.println("日统计mile:"+vehicleStatus.getMileage());
			vehicleStatus.addPoint_milege(dayLastMileage - dayFirstMileage); // 当日行驶里程数公里
		}
		
		lastLocBean = trackBean;
	}
	

	public VehicleStatus getVehicleStatus() {
		return vehicleStatus;
	}
	
	

}
