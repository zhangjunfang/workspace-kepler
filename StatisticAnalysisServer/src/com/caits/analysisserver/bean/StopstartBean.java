package com.caits.analysisserver.bean;

import java.util.ArrayList;
import java.util.List;

import com.caits.analysisserver.utils.CDate;
@SuppressWarnings("unused")
public class StopstartBean {
	private long statDate;
	private String vid;
	private String launchTime;//启动时间
	private String startTime;//起步时间
	private String stopTime;//停止时间
	private String fireoffTime;//熄火时间
	private long mileage;//行驶里程
	private long useOil;//油耗

	private long maxSpeed;//最大车速
	private long maxRotateSpeed;//最高转速
	private long idlingMaxRotateSpeed;//最高转速
	private long beginLon;
	private long beginLat;
	private long endLon;
	private long endLat;
	
	private boolean launchFlag = false;
	private boolean startFlag = false;
	private boolean stopFlag = false;
	private boolean fireoffFlag = false;
	
	private long startMileage;//开始行驶里程
	private long startOil;//开始油耗
	private long endMileage;//结束行驶里程
	private long endOil;//结束油耗
	
	private long ecuOilWear;
	private long ecuRunningOilWear;
	private long ecuIdlingOilWear;
	
	private long metOilWear;
	private long metRunningOilWear;
	private long metIdlingOilWear;
	
	private long oilFlag;
	
	private long heaterWorkingTime=0L;//EV0001
	private long aircWorkingTime=0L;//EV0002
	private long door1OpenNum=0L;//BS0013
	private long door2OpenNum=0L;//BS0014
	private long brakingNum=0L;//ES0004
	private long hornWorkingNum=0L;//ES0008
	private long retarderWorkNum=0L;//ES0011
	private long absWorkingNum=0L;//ES0012
	
	private String driverId;
	private String driverName;
	private String driverSrc;
	
	private List<RunningBean> runninglist = new ArrayList<RunningBean>();
	
	public StopstartBean(){
		runninglist.add(new RunningBean());
	}
	

	public String getLaunchTime() {
		return launchTime;
	}
	public void setLaunchTime(String launchTime) {
		this.launchTime = launchTime;
	}
	public String getStartTime() {
		return startTime;
	}
	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}
	public String getStopTime() {
		return stopTime;
	}
	public void setStopTime(String stopTime) {
		this.stopTime = stopTime;
	}
	public String getFireoffTime() {
		return fireoffTime;
	}
	public void setFireoffTime(String fireoffTime) {
		this.fireoffTime = fireoffTime;
	}
	
	public long getMaxSpeed() {
		long speed = 0;
		if (runninglist.size()>0){
			speed = runninglist.get(0).getMaxSpeed();
			for (int i=1;i<runninglist.size();i++){
				RunningBean rb = runninglist.get(i);
				if (speed<rb.getMaxSpeed()){
					speed = rb.getMaxSpeed();
				}
			}
		}
		return speed;
	}
	
	public void setMaxSpeed(long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}
	public long getMaxRotateSpeed() {
		if (runninglist.size()>0){
			for (int i=0;i<runninglist.size();i++){
				RunningBean rb = runninglist.get(i);
				if (this.idlingMaxRotateSpeed<rb.getMaxRotateSpeed()){
					idlingMaxRotateSpeed = rb.getMaxRotateSpeed();
				}
			}
		}
		return idlingMaxRotateSpeed;
	}
	public void setMaxRotateSpeed(long maxRotateSpeed) {
		this.maxRotateSpeed = maxRotateSpeed;
	}
	public long getMileage() {
		return mileage;
	}
	public void addMileage(long mileage) {
		this.mileage = mileage + this.mileage;
	}
	public long getUseOil() {
		return useOil;
	}
	public void addUseOil(long useOil) {
		this.useOil = useOil + this.useOil;
	}
	public boolean isLaunchFlag() {
		return launchFlag;
	}
	public void setLaunchFlag(boolean launchFlag) {
		this.launchFlag = launchFlag;
	}
	public boolean isStartFlag() {
		return startFlag;
	}
	public void setStartFlag(boolean startFlag) {
		this.startFlag = startFlag;
	}
	public boolean isStopFlag() {
		return stopFlag;
	}
	public void setStopFlag(boolean stopFlag) {
		this.stopFlag = stopFlag;
	}
	public boolean isFireoffFlag() {
		return fireoffFlag;
	}
	public void setFireoffFlag(boolean fireoffFlag) {
		this.fireoffFlag = fireoffFlag;
	}
	public long getStartMileage() {
		return startMileage;
	}
	public void setStartMileage(long startMileage) {
		this.startMileage = startMileage;
	}
	public long getStartOil() {
		return startOil;
	}
	public void setStartOil(long startOil) {
		this.startOil = startOil;
	}
	public long getEndMileage() {
		return endMileage;
	}
	public void setEndMileage(long endMileage) {
		this.endMileage = endMileage;
	}
	public long getEndOil() {
		return endOil;
	}
	public void setEndOil(long endOil) {
		this.endOil = endOil;
	}


	public String getVid() {
		return vid;
	}


	public void setVid(String vid) {
		this.vid = vid;
	}


	public List<RunningBean> getRunninglist() {
		return runninglist;
	}


	public void setRunninglist(List<RunningBean> runninglist) {
		this.runninglist = runninglist;
	}


	public long getIdlingMaxRotateSpeed() {
		return idlingMaxRotateSpeed;
	}


	public void setIdlingMaxRotateSpeed(long idlingMaxRotateSpeed) {
		this.idlingMaxRotateSpeed = idlingMaxRotateSpeed;
	}


	public long getBeginLon() {
		return beginLon;
	}


	public void setBeginLon(long beginLon) {
		this.beginLon = beginLon;
	}


	public long getBeginLat() {
		return beginLat;
	}


	public void setBeginLat(long beginLat) {
		this.beginLat = beginLat;
	}


	public long getEndLon() {
		return endLon;
	}


	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}


	public long getEndLat() {
		return endLat;
	}


	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}
	
	public String toString(){
		String str="";
		if (this.getRunninglist().size()>0){
			for (int i=0;i<this.runninglist.size();i++){
				RunningBean rb = runninglist.get(i);
				str += rb.toString();
			}
		}
		return "time:"+this.getLaunchTime()+","+this.getFireoffTime()+","+(CDate.stringConvertUtc(this.getFireoffTime())-CDate.stringConvertUtc(this.getLaunchTime()))/1000+
		",loc:"+this.getBeginLon()+","+this.getBeginLat()+","+this.getEndLon()+","+this.getEndLat()+
		",mileage:"+this.getStartMileage()+","+this.getEndMileage()+
		",oil:"+this.getStartOil()+","+this.getEndOil()+
		",metoil:"+this.getMetOilWear()+","+this.getMetRunningOilWear()+
		",ecuoil:"+this.getEcuOilWear()+","+this.getEcuRunningOilWear()+
		",max:"+this.getIdlingMaxRotateSpeed()+","+this.getMaxRotateSpeed()+","+this.getMaxSpeed()+
		"\r\n"+str;
	}


	public long getEcuOilWear() {
		return ecuOilWear;
	}


	public void addEcuOilWear(long ecuOilWear) {
		this.ecuOilWear = ecuOilWear + this.ecuOilWear;
	}


	public long getEcuRunningOilWear() {
		return ecuRunningOilWear;
	}


	public void setEcuRunningOilWear(long ecuRunningOilWear) {
		this.ecuRunningOilWear = ecuRunningOilWear;
	}


	public long getEcuIdlingOilWear() {
		return ecuIdlingOilWear;
	}


	public void setEcuIdlingOilWear(long ecuIdlingOilWear) {
		this.ecuIdlingOilWear = ecuIdlingOilWear;
	}


	public long getMetOilWear() {
		return metOilWear;
	}


	public void addMetOilWear(long metOilWear) {
		this.metOilWear = metOilWear + this.metOilWear;
	}


	public long getMetRunningOilWear() {
		return metRunningOilWear;
	}


	public void setMetRunningOilWear(long metRunningOilWear) {
		this.metRunningOilWear = metRunningOilWear;
	}


	public long getMetIdlingOilWear() {
		return metIdlingOilWear;
	}


	public void setMetIdlingOilWear(long metIdlingOilWear) {
		this.metIdlingOilWear = metIdlingOilWear;
	}


	public long getOilFlag() {
		return oilFlag;
	}


	public void setOilFlag(long oilFlag) {
		this.oilFlag = oilFlag;
	}


	public long getStatDate() {
		return statDate;
	}


	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}


	public long getHeaterWorkingTime() {
		return heaterWorkingTime;
	}


	public void setHeaterWorkingTime(long heaterWorkingTime) {
		this.heaterWorkingTime = heaterWorkingTime;
	}
	
	public void addHeaterWorkingTime(long heaterWorkingTime) {
		if (heaterWorkingTime>0){
			this.heaterWorkingTime += heaterWorkingTime;
		}
	}


	public long getAircWorkingTime() {
		return aircWorkingTime;
	}


	public void setAircWorkingTime(long aircWorkingTime) {
		this.aircWorkingTime = aircWorkingTime;
	}
	
	public void addAircWorkingTime(long aircWorkingTime) {
		if (aircWorkingTime>0){
			this.aircWorkingTime += aircWorkingTime;
		}
	}


	public long getDoor1OpenNum() {
		return door1OpenNum;
	}


	public void setDoor1OpenNum(long door1OpenNum) {
		this.door1OpenNum = door1OpenNum;
	}
	
	public void addDoor1OpenNum(long door1OpenNum) {
		this.door1OpenNum += door1OpenNum;
	}


	public long getDoor2OpenNum() {
		return door2OpenNum;
	}


	public void setDoor2OpenNum(long door2OpenNum) {
		this.door2OpenNum = door2OpenNum;
	}
	
	public void addDoor2OpenNum(long door2OpenNum) {
		this.door2OpenNum += door2OpenNum;
	}


	public long getBrakingNum() {
		return brakingNum;
	}


	public void setBrakingNum(long brakingNum) {
		this.brakingNum = brakingNum;
	}
	
	public void addBrakingNum(long brakingNum) {
		this.brakingNum += brakingNum;
	}


	public long getHornWorkingNum() {
		return hornWorkingNum;
	}


	public void setHornWorkingNum(long hornWorkingNum) {
		this.hornWorkingNum = hornWorkingNum;
	}
	
	public void addHornWorkingNum(long hornWorkingNum) {
		this.hornWorkingNum += hornWorkingNum;
	}


	public long getRetarderWorkNum() {
		return retarderWorkNum;
	}


	public void setRetarderWorkNum(long retarderWorkNum) {
		this.retarderWorkNum = retarderWorkNum;
	}
	
	public void addRetarderWorkNum(long retarderWorkNum) {
		this.retarderWorkNum += retarderWorkNum;
	}


	public long getAbsWorkingNum() {
		return absWorkingNum;
	}


	public void setAbsWorkingNum(long absWorkingNum) {
		this.absWorkingNum = absWorkingNum;
	}
	
	public void addAbsWorkingNum(long absWorkingNum) {
		this.absWorkingNum += absWorkingNum;
	}


	public String getDriverId() {
		return driverId;
	}


	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}


	public String getDriverName() {
		return driverName;
	}


	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}


	public String getDriverSrc() {
		return driverSrc;
	}


	public void setDriverSrc(String driverSrc) {
		this.driverSrc = driverSrc;
	}


}