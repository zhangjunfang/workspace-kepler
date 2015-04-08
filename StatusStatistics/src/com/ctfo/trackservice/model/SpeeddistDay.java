package com.ctfo.trackservice.model;

import com.ctfo.trackservice.util.ExcConstants;

public class SpeeddistDay {

	private String autoId;

	private String vid;
	
	private long statDate;

	private String vehicleNo;

	private String cVin;

	private Long statTime;

	private Long sumSpeed = new Long(0); // 总次数

	private Long speed0 = new Long(0);

	private Long speed0Time = new Long(0);

	private Long speed010 = new Long(0);

	private Long speed010Time = new Long(0);

	private Long speed1020 = new Long(0);

	private Long speed1020Time = new Long(0);

	private Long speed2030 = new Long(0);

	private Long speed2030Time = new Long(0);

	private Long speed3040 = new Long(0);

	private Long speed3040Time = new Long(0);

	private Long speed4050 = new Long(0);

	private Long speed4050Time = new Long(0);

	private Long speed5060 = new Long(0);

	private Long speed5060Time = new Long(0);

	private Long speed6070 = new Long(0);

	private Long speed6070Time = new Long(0);

	private Long speed7080 = new Long(0);

	private Long speed7080Time = new Long(0);

	private Long speed8090 = new Long(0);

	private Long speed8090Time = new Long(0);

	private Long speed90100 = new Long(0);

	private Long speed90100Time = new Long(0);

	private Long speed100110 = new Long(0);

	private Long speed100110Time = new Long(0);

	private Long speed110120 = new Long(0);

	private Long speed110120Time = new Long(0);

	private Long speed120130 = new Long(0);

	private Long speed120130Time = new Long(0);

	private Long speed130140 = new Long(0);

	private Long speed130140Time = new Long(0);

	private Long speed140150 = new Long(0);

	private Long speed140150Time = new Long(0);

	private Long speed150160 = new Long(0);

	private Long speed150160Time = new Long(0);

	private Long speed160170 = new Long(0);

	private Long speed160170Time = new Long(0);

	private Long speed170180 = new Long(0);

	private Long speed170180Time = new Long(0);

	private Long speed180190 = new Long(0);

	private Long speed180190Time = new Long(0);

	private Long speed190200 = new Long(0);

	private Long speed190200Time = new Long(0);

	private Long speedMax = new Long(0);

	private Long speedMaxTime = new Long(0);

	private Long maxSpeed = new Long(0);

	private Long minSpeed = new Long(0);
	
	private Long lastGpsTime = 0l;
	
	public SpeeddistDay(){

	}
	
	public SpeeddistDay(long utc,String vid){
		this.statDate = utc;
		this.vid = vid;
	}
	
	public String getAutoId() {
		return autoId;
	}

	public void setAutoId(String autoId) {
		this.autoId = autoId;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getcVin() {
		return cVin;
	}

	public void setcVin(String cVin) {
		this.cVin = cVin;
	}

	public Long getStatTime() {
		return statTime;
	}

	public void setStatTime(Long statTime) {
		this.statTime = statTime;
	}

	public Long getSumSpeed() {
		return sumSpeed;
	}

	public void setSumSpeed(Long sumSpeed) {
		this.sumSpeed = sumSpeed;
	}

	public Long getSpeed0() {
		return speed0;
	}

	public void setSpeed0(Long speed0) {
		this.speed0 = speed0;
	}

	public Long getSpeed0Time() {
		return speed0Time;
	}

	public void setSpeed0Time(Long speed0Time) {
		this.speed0Time = speed0Time;
	}

	public Long getSpeed010() {
		return speed010;
	}

	public void setSpeed010(Long speed010) {
		this.speed010 = speed010;
	}

	public Long getSpeed010Time() {
		return speed010Time;
	}

	public void setSpeed010Time(Long speed010Time) {
		this.speed010Time = speed010Time;
	}

	public Long getSpeed1020() {
		return speed1020;
	}

	public void setSpeed1020(Long speed1020) {
		this.speed1020 = speed1020;
	}

	public Long getSpeed1020Time() {
		return speed1020Time;
	}

	public void setSpeed1020Time(Long speed1020Time) {
		this.speed1020Time = speed1020Time;
	}

	public Long getSpeed2030() {
		return speed2030;
	}

	public void setSpeed2030(Long speed2030) {
		this.speed2030 = speed2030;
	}

	public Long getSpeed2030Time() {
		return speed2030Time;
	}

	public void setSpeed2030Time(Long speed2030Time) {
		this.speed2030Time = speed2030Time;
	}

	public Long getSpeed3040() {
		return speed3040;
	}

	public void setSpeed3040(Long speed3040) {
		this.speed3040 = speed3040;
	}

	public Long getSpeed3040Time() {
		return speed3040Time;
	}

	public void setSpeed3040Time(Long speed3040Time) {
		this.speed3040Time = speed3040Time;
	}

	public Long getSpeed4050() {
		return speed4050;
	}

	public void setSpeed4050(Long speed4050) {
		this.speed4050 = speed4050;
	}

	public Long getSpeed4050Time() {
		return speed4050Time;
	}

	public void setSpeed4050Time(Long speed4050Time) {
		this.speed4050Time = speed4050Time;
	}

	public Long getSpeed5060() {
		return speed5060;
	}

	public void setSpeed5060(Long speed5060) {
		this.speed5060 = speed5060;
	}

	public Long getSpeed5060Time() {
		return speed5060Time;
	}

	public void setSpeed5060Time(Long speed5060Time) {
		this.speed5060Time = speed5060Time;
	}

	public Long getSpeed6070() {
		return speed6070;
	}

	public void setSpeed6070(Long speed6070) {
		this.speed6070 = speed6070;
	}

	public Long getSpeed6070Time() {
		return speed6070Time;
	}

	public void setSpeed6070Time(Long speed6070Time) {
		this.speed6070Time = speed6070Time;
	}

	public Long getSpeed7080() {
		return speed7080;
	}

	public void setSpeed7080(Long speed7080) {
		this.speed7080 = speed7080;
	}

	public Long getSpeed7080Time() {
		return speed7080Time;
	}

	public void setSpeed7080Time(Long speed7080Time) {
		this.speed7080Time = speed7080Time;
	}

	public Long getSpeed8090() {
		return speed8090;
	}

	public void setSpeed8090(Long speed8090) {
		this.speed8090 = speed8090;
	}

	public Long getSpeed8090Time() {
		return speed8090Time;
	}

	public void setSpeed8090Time(Long speed8090Time) {
		this.speed8090Time = speed8090Time;
	}

	public Long getSpeed90100() {
		return speed90100;
	}

	public void setSpeed90100(Long speed90100) {
		this.speed90100 = speed90100;
	}

	public Long getSpeed90100Time() {
		return speed90100Time;
	}

	public void setSpeed90100Time(Long speed90100Time) {
		this.speed90100Time = speed90100Time;
	}

	public Long getSpeed100110() {
		return speed100110;
	}

	public void setSpeed100110(Long speed100110) {
		this.speed100110 = speed100110;
	}

	public Long getSpeed100110Time() {
		return speed100110Time;
	}

	public void setSpeed100110Time(Long speed100110Time) {
		this.speed100110Time = speed100110Time;
	}

	public Long getSpeed110120() {
		return speed110120;
	}

	public void setSpeed110120(Long speed110120) {
		this.speed110120 = speed110120;
	}

	public Long getSpeed110120Time() {
		return speed110120Time;
	}

	public void setSpeed110120Time(Long speed110120Time) {
		this.speed110120Time = speed110120Time;
	}

	public Long getSpeed120130() {
		return speed120130;
	}

	public void setSpeed120130(Long speed120130) {
		this.speed120130 = speed120130;
	}

	public Long getSpeed120130Time() {
		return speed120130Time;
	}

	public void setSpeed120130Time(Long speed120130Time) {
		this.speed120130Time = speed120130Time;
	}

	public Long getSpeed130140() {
		return speed130140;
	}

	public void setSpeed130140(Long speed130140) {
		this.speed130140 = speed130140;
	}

	public Long getSpeed130140Time() {
		return speed130140Time;
	}

	public void setSpeed130140Time(Long speed130140Time) {
		this.speed130140Time = speed130140Time;
	}

	public Long getSpeed140150() {
		return speed140150;
	}

	public void setSpeed140150(Long speed140150) {
		this.speed140150 = speed140150;
	}

	public Long getSpeed140150Time() {
		return speed140150Time;
	}

	public void setSpeed140150Time(Long speed140150Time) {
		this.speed140150Time = speed140150Time;
	}

	public Long getSpeed150160() {
		return speed150160;
	}

	public void setSpeed150160(Long speed150160) {
		this.speed150160 = speed150160;
	}

	public Long getSpeed150160Time() {
		return speed150160Time;
	}

	public void setSpeed150160Time(Long speed150160Time) {
		this.speed150160Time = speed150160Time;
	}

	public Long getSpeed160170() {
		return speed160170;
	}

	public void setSpeed160170(Long speed160170) {
		this.speed160170 = speed160170;
	}

	public Long getSpeed160170Time() {
		return speed160170Time;
	}

	public void setSpeed160170Time(Long speed160170Time) {
		this.speed160170Time = speed160170Time;
	}

	public Long getSpeed170180() {
		return speed170180;
	}

	public void setSpeed170180(Long speed170180) {
		this.speed170180 = speed170180;
	}

	public Long getSpeed170180Time() {
		return speed170180Time;
	}

	public void setSpeed170180Time(Long speed170180Time) {
		this.speed170180Time = speed170180Time;
	}

	public Long getSpeed180190() {
		return speed180190;
	}

	public void setSpeed180190(Long speed180190) {
		this.speed180190 = speed180190;
	}

	public Long getSpeed180190Time() {
		return speed180190Time;
	}

	public void setSpeed180190Time(Long speed180190Time) {
		this.speed180190Time = speed180190Time;
	}

	public Long getSpeed190200() {
		return speed190200;
	}

	public void setSpeed190200(Long speed190200) {
		this.speed190200 = speed190200;
	}

	public Long getSpeed190200Time() {
		return speed190200Time;
	}

	public void setSpeed190200Time(Long speed190200Time) {
		this.speed190200Time = speed190200Time;
	}

	public Long getSpeedMax() {
		return speedMax;
	}

	public void setSpeedMax(Long speedMax) {
		this.speedMax = speedMax;
	}

	public Long getSpeedMaxTime() {
		return speedMaxTime;
	}

	public void setSpeedMaxTime(Long speedMaxTime) {
		this.speedMaxTime = speedMaxTime;
	}

	public Long getMaxSpeed() {
		return maxSpeed;
	}

	public void setMaxSpeed(Long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

	public Long getMinSpeed() {
		return minSpeed;
	}

	public void setMinSpeed(Long minSpeed) {
		this.minSpeed = minSpeed;
	}

	// 统计车速次数
	public void analyseSpeed(SpeeddistDay sd, Long speed,Long gpsTime) {		
		// 最大车速
		if (sd.getMaxSpeed()>0){		
			if (speed > sd.getMaxSpeed()) sd.setMaxSpeed(speed);
		}else{
			sd.setMaxSpeed(speed);
		}		
		//最小车速
		if (sd.getMinSpeed()>0){
		   if (speed < sd.getMinSpeed()&& speed>=50) sd.setMinSpeed(speed);
		}else{
			sd.setMinSpeed(speed);
		}
		
		//根据转换标准  Gps速度  单位：1/10 Km/h
		double speeds = speed*0.1;	

		// 总次数
		sd.setSumSpeed(sd.getSumSpeed() + 1);
		long time = 0;
		// 计算时间差，上一次gps时间减当前时间
		if(this.lastGpsTime > 0){ // 当天第一条不做时间处理
			long temp = gpsTime - lastGpsTime;
			if(temp > 0 && temp <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = temp/1000; //存储以秒为单位
			}else{
				time = 0;
			}
		}
		
		if (speed>=5 && speeds < 10) {
			sd.setSpeed010(sd.getSpeed010() + 1);
			sd.setSpeed010Time(sd.getSpeed010Time() + time);
		}else if (10 <= speeds && speeds < 20) {
			sd.setSpeed1020(sd.getSpeed1020() + 1);
			sd.setSpeed1020Time(sd.getSpeed1020Time() + time);
		}else if (20 <= speeds && speeds < 30) {
			sd.setSpeed2030(sd.getSpeed2030() + 1);
			sd.setSpeed2030Time(sd.getSpeed2030Time() + time);
		}else if (30 <= speeds && speeds < 40) {
			sd.setSpeed3040(sd.getSpeed3040() + 1);
			sd.setSpeed3040Time(sd.getSpeed3040Time() + time);
		}else if (40 <= speeds && speeds < 50) {
			sd.setSpeed4050(sd.getSpeed4050() + 1);
			sd.setSpeed4050Time(sd.getSpeed4050Time() + time);
		}else if (50 <= speeds && speeds < 60) {
			sd.setSpeed5060(sd.getSpeed5060() + 1);
			sd.setSpeed5060Time(sd.getSpeed5060Time() + time);
		}else if (60 <= speeds && speeds < 70) {
			sd.setSpeed6070(sd.getSpeed6070() + 1);
			sd.setSpeed6070Time(sd.getSpeed6070Time() + time);
		}else if (70 <= speeds && speeds < 80) {
			sd.setSpeed7080(sd.getSpeed7080() + 1);
			sd.setSpeed7080Time(sd.getSpeed7080Time() + time);
		}else if (80 <= speeds && speeds < 90) {
			sd.setSpeed8090(sd.getSpeed8090() + 1);
			sd.setSpeed8090Time(sd.getSpeed8090Time() + time);
		}else if (90 <= speeds && speeds < 100) {
			sd.setSpeed90100(sd.getSpeed90100() + 1);
			sd.setSpeed90100Time(sd.getSpeed90100Time() + time);
		}else if (100 <= speeds && speeds < 110) {
			sd.setSpeed100110(sd.getSpeed100110() + 1);
			sd.setSpeed100110Time(sd.getSpeed100110Time() + time);
		}else if (110 <= speeds && speeds < 120) {
			sd.setSpeed110120(sd.getSpeed110120() + 1);
			sd.setSpeed110120Time(sd.getSpeed110120Time() + time);
		}else if (120 <= speeds && speeds < 130) {
			sd.setSpeed120130(sd.getSpeed120130() + 1);
			sd.setSpeed120130Time(sd.getSpeed120130Time() + time);
		}else if (130 <= speeds && speeds < 140) {
			sd.setSpeed130140(sd.getSpeed130140() + 1);
			sd.setSpeed130140Time(sd.getSpeed130140Time() + time);
		}else if (140 <= speeds && speeds < 150) {
			sd.setSpeed140150(sd.getSpeed140150() + 1);
			sd.setSpeed140150Time(sd.getSpeed140150Time() + time);
		}else if (150 <= speeds && speeds < 160) {
			sd.setSpeed150160(sd.getSpeed150160() + 1);
			sd.setSpeed150160Time(sd.getSpeed150160Time() + time);
		}else if (160 <= speeds && speeds < 170) {
			sd.setSpeed160170(sd.getSpeed160170() + 1);
			sd.setSpeed160170Time(sd.getSpeed160170Time() + time);
		}else if (170 <= speeds && speeds < 180) {
			sd.setSpeed170180(sd.getSpeed170180() + 1);
			sd.setSpeed170180Time(sd.getSpeed170180Time() + time);
		}else if (180 <= speeds && speeds < 190) {
			sd.setSpeed180190(sd.getSpeed180190() + 1);
			sd.setSpeed180190Time(sd.getSpeed180190Time() + time);
		}else if (190 <= speeds && speeds < 200) {
			sd.setSpeed190200(sd.getSpeed190200() + 1);
			sd.setSpeed190200Time(sd.getSpeed190200Time() + time);
		}else if (200 <= speeds) {
			sd.setSpeedMax(sd.getSpeedMax() + 1);
			sd.setSpeedMaxTime(sd.getSpeedMaxTime() + time);
		}
		
		this.lastGpsTime = gpsTime;
	}

	public Long getLastGpsTime() {
		return lastGpsTime;
	}

	public void setLastGpsTime(Long lastGpsTime) {
		this.lastGpsTime = lastGpsTime;
	}

	public long getStatDate() {
		return statDate;
	}

	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}
}
