package com.ctfo.trackservice.service;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import com.ctfo.trackservice.model.DriverDetailBean;
import com.ctfo.trackservice.model.VehicleMessageBean;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.ExcConstants;
import com.ctfo.trackservice.util.Tools;






/**
 * 文件名：VehicleStatusService.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-25上午9:45:27
 * 
 */
public class DriverMileageService {
	
	///private static final Logger logger = LoggerFactory.getLogger(DriverMileageService.class);
	
	private List<DriverDetailBean> driverDetaillist = new ArrayList<DriverDetailBean>();
	private DriverDetailBean driverDetail = null;

	Long dayFirstMileage = 0L;
	Long dayLastMileage = 0L;

	
	boolean isflag = false;
	
	private long utc;
	private String vid;
	
	private VehicleMessageBean lastLocBean=null;
	private Long tmpLastOil = -1L;

	private Long tmpLastPreciseOil = -1L;
	
	private Long tmpLastMileage=-1L;
	private boolean isEngineFlag;
	private boolean isRunningFlag;
	
	
	
	
	public DriverMileageService(long utc,String vid){
		this.utc = utc+12*60*60*1000;
		this.vid = vid;
	}
	
	public void executeAnalyser(VehicleMessageBean trackBean,boolean isLastRow) throws Exception{
		// 当前值
		Long gpsTime = trackBean.getUtc();
		Long gpsSpeed = trackBean.getSpeed();
		Long mileage = trackBean.getMileage();
		String driverId = trackBean.getDriverId();
		Long oil = trackBean.getOil();
		Long preciseOil = trackBean.getMetOil();
		
		String[] alarmCode = trackBean.getAlarmcode().split(",");
		
		String extendStatus = (String)Tools.formatValueByType(trackBean.getExtendStatus(),"0",'S');//扩展状态位
		String extendStatusBinary = Tools.fillString(Tools.getBinaryString(extendStatus), 32);
		
		String heatUpStatus = extendStatusBinary.substring(18,19);//加热器
		String airconditionStatus = extendStatusBinary.substring(22,23);//空调
		// 备份值
		if (lastLocBean == null) {
			lastLocBean = trackBean;
		}
		Long lastGpsTime = lastLocBean.getUtc();
		Long lastMileage = lastLocBean.getMileage();
		Long lastOil = lastLocBean.getOil();
		Long lastPreciseOil = lastLocBean.getMetOil();
		String lastDriverId = lastLocBean.getDriverId();

		long mg = 0;
		long tmpOil = 0;
		long tmpPreciseOil = 0;
	
		long timediff = accountOffLineTime(gpsTime, lastGpsTime);//本条记录和上条记录的时间差ms
	
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
		
		tmpOil = oil - lastOil;

		tmpPreciseOil = preciseOil - lastPreciseOil;
		/*****
		 * 过滤异常数据,包括里程和油耗, 里程为异常数据则本次里程负值为0,
		 */
		if(mg >= 0 && mg <= DateTools.accountTimeIntervalVale(gpsTime,lastGpsTime,5,10f)){
			// 不做处理
		} else {
			mg = 0;
		}
		if (tmpOil >= 0 && tmpOil <= DateTools.accountTimeIntervalVale(gpsTime,lastGpsTime,3,60f)) {
			// 不做处理
		} else {
			// 油耗为异常数据则本次里程负值为0,
			tmpOil = 0;
		}

		if (tmpPreciseOil >= 0 && tmpPreciseOil <= DateTools.accountTimeIntervalVale(gpsTime,lastGpsTime,3,60f)) {
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
				driverDetail.setStatDate(this.utc);
				driverDetail.setBeginVmb(trackBean);
			}
		}else {
			if("1".equals(heatUpStatus)){
				trackBean.setHeatUp(true);
				if(lastLocBean.getHeatUp()){
					driverDetail.addHeatUpTime(timediff/1000);
				}
			}else {
				trackBean.setHeatUp(false);
				if(lastLocBean.getHeatUp()){
					driverDetail.addHeatUpTime(timediff/1000);
				}
			}
			//------------------	
			if("1".equals(airconditionStatus)){
				trackBean.setAircondition(true);
				if(lastLocBean.getAircondition()){
					driverDetail.addAirConditionTime(timediff/1000);
				}else{
					driverDetail.addAirConditionNum(1);
				}
			}else {
				trackBean.setAircondition(false);
				if(lastLocBean.getAircondition()){
					driverDetail.addAirConditionTime(timediff/1000);
				}
			}
			
			if(null != alarmCode && isContainCode(alarmCode, "48")){//急加速
				trackBean.setUrgentSpeed(true);
				if(!lastLocBean.getUrgentSpeed()){
					driverDetail.addUrgentSpeedNuM(1);
				}else{
					driverDetail.addUrgentSpeedTime(timediff/1000);
				}
			}else {
				trackBean.setUrgentSpeed(false);
				if(lastLocBean.getUrgentSpeed()){
					driverDetail.addUrgentSpeedTime(timediff/1000);
				}
			}
			if(null != alarmCode && isContainCode(alarmCode, "49")){//急减速
				trackBean.setUrgentLowdown(true);
				if(!lastLocBean.getUrgentLowdown()){
					driverDetail.addUrgentLowdownNum(1);
				}else{
					driverDetail.addUrgentLowdownTime(timediff/1000);
				}
			}else {
				trackBean.setUrgentLowdown(false);
				if(lastLocBean.getUrgentLowdown()){
					driverDetail.addUrgentLowdownTime(timediff/1000);
				}
			}
			if(null != alarmCode && isContainCode(alarmCode, "1")){//超速
				trackBean.setOverSpeed(true);
				if(!lastLocBean.getOverSpeed()){
					driverDetail.addOverSpeedNum(1);
				}else{
					driverDetail.addOverSpeedTime(timediff/1000);
				}
			}else {
				trackBean.setOverSpeed(false);
				if(lastLocBean.getOverSpeed()){
					driverDetail.addOverSpeedTime(timediff/1000);
				}
			}
			if(null != alarmCode && isContainCode(alarmCode, "47")){//超转
				trackBean.setOverRpm(true);
				if(!lastLocBean.getOverRpm()){
					driverDetail.addOverRpmNum(1);
				}else{
					driverDetail.addOverRpmTime(timediff/1000);
				}
			}else {
				trackBean.setOverRpm(false);
				if(lastLocBean.getOverRpm()){
					driverDetail.addOverRpmTime(timediff/1000);
				}
			}
			if(null != alarmCode && isContainCode(alarmCode, "45")){//超长怠速
				trackBean.setLongIdle(true);
				if(!lastLocBean.getLongIdle()){
					driverDetail.addLongIdleNum(1);
				}else{
					driverDetail.addLongIdleTime(timediff/1000);
				}
			}else {
				trackBean.setLongIdle(false);
				if(lastLocBean.getLongIdle()){
					driverDetail.addLongIdleTime(timediff/1000);
				}
			}
			if(null != alarmCode && isContainCode(alarmCode, "46")){//怠速空调
				trackBean.setIdleAirCondition(true);
				if(!lastLocBean.getIdleAirCondition()){
					driverDetail.addIdleAirConditionNum(1);
				}else{
					driverDetail.addIdleAirConditionTime(timediff/1000);
				}
			}else {
				trackBean.setIdleAirCondition(false);
				if(lastLocBean.getIdleAirCondition()){
					driverDetail.addIdleAirConditionTime(timediff/1000);
				}
			}
			if(null != alarmCode && isContainCode(alarmCode, "44")){//空挡滑行
				trackBean.setGearGlide(true);
				if(!lastLocBean.getGearGlide()){
					driverDetail.addGearGlideNum(1);
				}else{
					driverDetail.addGearGlideTime(timediff/1000);
				}
			}else {
				trackBean.setGearGlide(false);
				if(lastLocBean.getGearGlide()){
					driverDetail.addGearGlideTime(timediff/1000);
				}
			}
			
			
			
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
			driverDetail.addMileage(mg);
			driverDetail.addEcuOilWear(tmpOil);
			driverDetail.addMetOilWear(tmpPreciseOil);
			
			//System.out.println("MILEAGE:"+driverDetail.getMileage()+":"+trackBean.getTrackStr());
			// 判断本次驾驶是否结束
			if (!driverId.equals(lastDriverId) || isLastRow) {
				// 驾驶员切换时需要结束上次驾驶记录
				driverDetail.setEndVmb(trackBean);
				//System.out.println("MILEAGE:"+driverDetail.getMileage()+":"+trackBean.getTrackStr());
				driverDetaillist.add(driverDetail);
				driverDetail = null;
			}
	
			if (driverDetail == null && driverId != null && !"".equals(driverId) && !isLastRow) {
				driverDetail = new DriverDetailBean();
				driverDetail.setDetailId(UUID.randomUUID().toString().replace("-", ""));
				driverDetail.setVid(vid);
				driverDetail.setStatDate(this.utc);
				driverDetail.setBeginVmb(trackBean);
			}
		}
		
		lastLocBean = trackBean;
	}

	/**
	 * @return the driverDetaillist
	 */
	public List<DriverDetailBean> getDriverDetaillist() {
		return driverDetaillist;
	}

	/**
	 * @param driverDetaillist the driverDetaillist to set
	 */
	public void setDriverDetaillist(List<DriverDetailBean> driverDetaillist) {
		this.driverDetaillist = driverDetaillist;
	}

	/**
	 * @return the driverDetail
	 */
	public DriverDetailBean getDriverDetail() {
		return driverDetail;
	}

	/**
	 * @param driverDetail the driverDetail to set
	 */
	public void setDriverDetail(DriverDetailBean driverDetail) {
		this.driverDetail = driverDetail;
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
	 * 判断报警编码是否含有
	 * @param arr
	 * @param code
	 * @return
	 */
	private Boolean isContainCode(String[] arr,String code){
		for(int i = 0;i<arr.length;i++){
			if(code.equals(arr[i])){
				return true ;
			}
		}
		return false;
	}
}
	

