package com.caits.analysisserver.bean;


public class RotateSpeedDay {
	private String rotateId;

	private String vid;
	
	private long statDate;

	private String vehicleNo;

	private String cVin;

	private Long terminalUtc=new Long(0);

	private Long createTime=new Long(0);

	private Long sumRotateSpeed=new Long(0);

	private Long rotateSpeed0=new Long(0);

	private Long rotateSpeed0Time=new Long(0);

	private Long rotateSpeed0100=new Long(0);

	private Long rotateSpeed0100Time=new Long(0);

	private Long rotateSpeed100200=new Long(0);

	private Long rotateSpeed100200Time=new Long(0);

	private Long rotateSpeed200300=new Long(0);

	private Long rotateSpeed200300Time=new Long(0);

	private Long rotateSpeed300400=new Long(0);

	private Long rotateSpeed300400Time=new Long(0);

	private Long rotateSpeed400500=new Long(0);

	private Long rotateSpeed400500Time=new Long(0);

	private Long rotateSpeed500600=new Long(0);

	private Long rotateSpeed500600Time=new Long(0);

	private Long rotateSpeed600700=new Long(0);

	private Long rotateSpeed600700Time=new Long(0);

	private Long rotateSpeed700800=new Long(0);

	private Long rotateSpeed700800Time=new Long(0);

	private Long rotateSpeed800900=new Long(0);

	private Long rotateSpeed800900Time=new Long(0);

	private Long rotateSpeed9001000=new Long(0);

	private Long rotateSpeed9001000Time=new Long(0);

	private Long rotateSpeed10001100=new Long(0);

	private Long rotateSpeed10001100Time=new Long(0);

	private Long rotateSpeed11001200=new Long(0);

	private Long rotateSpeed11001200Time=new Long(0);

	private Long rotateSpeed12001300=new Long(0);

	private Long rotateSpeed12001300Time=new Long(0);

	private Long rotateSpeed13001400=new Long(0);

	private Long rotateSpeed13001400Time=new Long(0);

	private Long rotateSpeed14001500=new Long(0);

	private Long rotateSpeed14001500Time=new Long(0);

	private Long rotateSpeed15001600=new Long(0);

	private Long rotateSpeed15001600Time=new Long(0);

	private Long rotateSpeed16001700=new Long(0);

	private Long rotateSpeed16001700Time=new Long(0);

	private Long rotateSpeed17001800=new Long(0);

	private Long rotateSpeed17001800Time=new Long(0);

	private Long rotateSpeed18001900=new Long(0);

	private Long rotateSpeed18001900Time=new Long(0);

	private Long rotateSpeed19002000=new Long(0);

	private Long rotateSpeed19002000Time=new Long(0);

	private Long rotateSpeed20002100=new Long(0);

	private Long rotateSpeed20002100Time=new Long(0);

	private Long rotateSpeed21002200=new Long(0);

	private Long rotateSpeed21002200Time=new Long(0);

	private Long rotateSpeed22002300=new Long(0);

	private Long rotateSpeed22002300Time=new Long(0);

	private Long rotateSpeed23002400=new Long(0);

	private Long rotateSpeed23002400Time=new Long(0);

	private Long rotateSpeed24002500=new Long(0);

	private Long rotateSpeed24002500Time=new Long(0);

	private Long rotateSpeed25002600=new Long(0);

	private Long rotateSpeed25002600Time=new Long(0);

	private Long rotateSpeed26002700=new Long(0);

	private Long rotateSpeed26002700Time=new Long(0);

	private Long rotateSpeed27002800=new Long(0);

	private Long rotateSpeed27002800Time=new Long(0);

	private Long rotateSpeed28002900=new Long(0);

	private Long rotateSpeed28002900Time=new Long(0);

	private Long rotateSpeed29003000=new Long(0);

	private Long rotateSpeed29003000Time=new Long(0);

	private Long rotateSpeedMax=new Long(0);

	private Long rotateSpeedMaxTime=new Long(0);

	private Long percent6080Fuhelv=new Long(0);

	private Long minRotateSpeed=new Long(0);

	private Long maxRotateSpeed=new Long(0);
	
	String rpmSection = "";//上次转速区间
	long periorGpsTime = -1; //上次Gps时间
	
	public RotateSpeedDay(){
		
	}
	
	public RotateSpeedDay(long utc,String vid){
		this.statDate = utc;
		this.vid = vid;
	}

	public String getRotateId() {
		return rotateId;
	}

	public void setRotateId(String rotateId) {
		this.rotateId = rotateId;
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

	public Long getTerminalUtc() {
		return terminalUtc;
	}

	public void setTerminalUtc(Long terminalUtc) {
		this.terminalUtc = terminalUtc;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public Long getRotateSpeed0() {
		return rotateSpeed0;
	}

	public void setRotateSpeed0(Long rotateSpeed0) {
		this.rotateSpeed0 = rotateSpeed0;
	}

	public Long getRotateSpeed0Time() {
		return rotateSpeed0Time;
	}

	public void setRotateSpeed0Time(Long rotateSpeed0Time) {
		this.rotateSpeed0Time = rotateSpeed0Time;
	}

	public Long getRotateSpeed0100() {
		return rotateSpeed0100;
	}

	public void setRotateSpeed0100(Long rotateSpeed0100) {
		this.rotateSpeed0100 = rotateSpeed0100;
	}

	public Long getRotateSpeed0100Time() {
		return rotateSpeed0100Time;
	}

	public void setRotateSpeed0100Time(Long rotateSpeed0100Time) {
		this.rotateSpeed0100Time = rotateSpeed0100Time;
	}

	public Long getRotateSpeed100200() {
		return rotateSpeed100200;
	}

	public void setRotateSpeed100200(Long rotateSpeed100200) {
		this.rotateSpeed100200 = rotateSpeed100200;
	}

	public Long getRotateSpeed100200Time() {
		return rotateSpeed100200Time;
	}

	public void setRotateSpeed100200Time(Long rotateSpeed100200Time) {
		this.rotateSpeed100200Time = rotateSpeed100200Time;
	}

	public Long getRotateSpeed200300() {
		return rotateSpeed200300;
	}

	public void setRotateSpeed200300(Long rotateSpeed200300) {
		this.rotateSpeed200300 = rotateSpeed200300;
	}

	public Long getRotateSpeed200300Time() {
		return rotateSpeed200300Time;
	}

	public void setRotateSpeed200300Time(Long rotateSpeed200300Time) {
		this.rotateSpeed200300Time = rotateSpeed200300Time;
	}

	public Long getRotateSpeed300400() {
		return rotateSpeed300400;
	}

	public void setRotateSpeed300400(Long rotateSpeed300400) {
		this.rotateSpeed300400 = rotateSpeed300400;
	}

	public Long getRotateSpeed300400Time() {
		return rotateSpeed300400Time;
	}

	public void setRotateSpeed300400Time(Long rotateSpeed300400Time) {
		this.rotateSpeed300400Time = rotateSpeed300400Time;
	}

	public Long getRotateSpeed400500() {
		return rotateSpeed400500;
	}

	public void setRotateSpeed400500(Long rotateSpeed400500) {
		this.rotateSpeed400500 = rotateSpeed400500;
	}

	public Long getRotateSpeed400500Time() {
		return rotateSpeed400500Time;
	}

	public void setRotateSpeed400500Time(Long rotateSpeed400500Time) {
		this.rotateSpeed400500Time = rotateSpeed400500Time;
	}

	public Long getRotateSpeed500600() {
		return rotateSpeed500600;
	}

	public void setRotateSpeed500600(Long rotateSpeed500600) {
		this.rotateSpeed500600 = rotateSpeed500600;
	}

	public Long getRotateSpeed500600Time() {
		return rotateSpeed500600Time;
	}

	public void setRotateSpeed500600Time(Long rotateSpeed500600Time) {
		this.rotateSpeed500600Time = rotateSpeed500600Time;
	}

	public Long getRotateSpeed600700() {
		return rotateSpeed600700;
	}

	public void setRotateSpeed600700(Long rotateSpeed600700) {
		this.rotateSpeed600700 = rotateSpeed600700;
	}

	public Long getRotateSpeed600700Time() {
		return rotateSpeed600700Time;
	}

	public void setRotateSpeed600700Time(Long rotateSpeed600700Time) {
		this.rotateSpeed600700Time = rotateSpeed600700Time;
	}

	public Long getRotateSpeed700800() {
		return rotateSpeed700800;
	}

	public void setRotateSpeed700800(Long rotateSpeed700800) {
		this.rotateSpeed700800 = rotateSpeed700800;
	}

	public Long getRotateSpeed700800Time() {
		return rotateSpeed700800Time;
	}

	public void setRotateSpeed700800Time(Long rotateSpeed700800Time) {
		this.rotateSpeed700800Time = rotateSpeed700800Time;
	}

	public Long getRotateSpeed800900() {
		return rotateSpeed800900;
	}

	public void setRotateSpeed800900(Long rotateSpeed800900) {
		this.rotateSpeed800900 = rotateSpeed800900;
	}

	public Long getRotateSpeed800900Time() {
		return rotateSpeed800900Time;
	}

	public void setRotateSpeed800900Time(Long rotateSpeed800900Time) {
		this.rotateSpeed800900Time = rotateSpeed800900Time;
	}

	public Long getRotateSpeed9001000() {
		return rotateSpeed9001000;
	}

	public void setRotateSpeed9001000(Long rotateSpeed9001000) {
		this.rotateSpeed9001000 = rotateSpeed9001000;
	}

	public Long getRotateSpeed9001000Time() {
		return rotateSpeed9001000Time;
	}

	public void setRotateSpeed9001000Time(Long rotateSpeed9001000Time) {
		this.rotateSpeed9001000Time = rotateSpeed9001000Time;
	}

	public Long getRotateSpeed10001100() {
		return rotateSpeed10001100;
	}

	public void setRotateSpeed10001100(Long rotateSpeed10001100) {
		this.rotateSpeed10001100 = rotateSpeed10001100;
	}

	public Long getRotateSpeed10001100Time() {
		return rotateSpeed10001100Time;
	}

	public void setRotateSpeed10001100Time(Long rotateSpeed10001100Time) {
		this.rotateSpeed10001100Time = rotateSpeed10001100Time;
	}

	public Long getRotateSpeed11001200() {
		return rotateSpeed11001200;
	}

	public void setRotateSpeed11001200(Long rotateSpeed11001200) {
		this.rotateSpeed11001200 = rotateSpeed11001200;
	}

	public Long getRotateSpeed11001200Time() {
		return rotateSpeed11001200Time;
	}

	public void setRotateSpeed11001200Time(Long rotateSpeed11001200Time) {
		this.rotateSpeed11001200Time = rotateSpeed11001200Time;
	}

	public Long getRotateSpeed12001300() {
		return rotateSpeed12001300;
	}

	public void setRotateSpeed12001300(Long rotateSpeed12001300) {
		this.rotateSpeed12001300 = rotateSpeed12001300;
	}

	public Long getRotateSpeed12001300Time() {
		return rotateSpeed12001300Time;
	}

	public void setRotateSpeed12001300Time(Long rotateSpeed12001300Time) {
		this.rotateSpeed12001300Time = rotateSpeed12001300Time;
	}

	public Long getRotateSpeed13001400() {
		return rotateSpeed13001400;
	}

	public void setRotateSpeed13001400(Long rotateSpeed13001400) {
		this.rotateSpeed13001400 = rotateSpeed13001400;
	}

	public Long getRotateSpeed13001400Time() {
		return rotateSpeed13001400Time;
	}

	public void setRotateSpeed13001400Time(Long rotateSpeed13001400Time) {
		this.rotateSpeed13001400Time = rotateSpeed13001400Time;
	}

	public Long getRotateSpeed14001500() {
		return rotateSpeed14001500;
	}

	public void setRotateSpeed14001500(Long rotateSpeed14001500) {
		this.rotateSpeed14001500 = rotateSpeed14001500;
	}

	public Long getRotateSpeed14001500Time() {
		return rotateSpeed14001500Time;
	}

	public void setRotateSpeed14001500Time(Long rotateSpeed14001500Time) {
		this.rotateSpeed14001500Time = rotateSpeed14001500Time;
	}

	public Long getRotateSpeed15001600() {
		return rotateSpeed15001600;
	}

	public void setRotateSpeed15001600(Long rotateSpeed15001600) {
		this.rotateSpeed15001600 = rotateSpeed15001600;
	}

	public Long getRotateSpeed15001600Time() {
		return rotateSpeed15001600Time;
	}

	public void setRotateSpeed15001600Time(Long rotateSpeed15001600Time) {
		this.rotateSpeed15001600Time = rotateSpeed15001600Time;
	}

	public Long getRotateSpeed16001700() {
		return rotateSpeed16001700;
	}

	public void setRotateSpeed16001700(Long rotateSpeed16001700) {
		this.rotateSpeed16001700 = rotateSpeed16001700;
	}

	public Long getRotateSpeed16001700Time() {
		return rotateSpeed16001700Time;
	}

	public void setRotateSpeed16001700Time(Long rotateSpeed16001700Time) {
		this.rotateSpeed16001700Time = rotateSpeed16001700Time;
	}

	public Long getRotateSpeed17001800() {
		return rotateSpeed17001800;
	}

	public void setRotateSpeed17001800(Long rotateSpeed17001800) {
		this.rotateSpeed17001800 = rotateSpeed17001800;
	}

	public Long getRotateSpeed17001800Time() {
		return rotateSpeed17001800Time;
	}

	public void setRotateSpeed17001800Time(Long rotateSpeed17001800Time) {
		this.rotateSpeed17001800Time = rotateSpeed17001800Time;
	}

	public Long getRotateSpeed18001900() {
		return rotateSpeed18001900;
	}

	public void setRotateSpeed18001900(Long rotateSpeed18001900) {
		this.rotateSpeed18001900 = rotateSpeed18001900;
	}

	public Long getRotateSpeed18001900Time() {
		return rotateSpeed18001900Time;
	}

	public void setRotateSpeed18001900Time(Long rotateSpeed18001900Time) {
		this.rotateSpeed18001900Time = rotateSpeed18001900Time;
	}

	public Long getRotateSpeed19002000() {
		return rotateSpeed19002000;
	}

	public void setRotateSpeed19002000(Long rotateSpeed19002000) {
		this.rotateSpeed19002000 = rotateSpeed19002000;
	}

	public Long getRotateSpeed19002000Time() {
		return rotateSpeed19002000Time;
	}

	public void setRotateSpeed19002000Time(Long rotateSpeed19002000Time) {
		this.rotateSpeed19002000Time = rotateSpeed19002000Time;
	}

	public Long getRotateSpeed20002100() {
		return rotateSpeed20002100;
	}

	public void setRotateSpeed20002100(Long rotateSpeed20002100) {
		this.rotateSpeed20002100 = rotateSpeed20002100;
	}

	public Long getRotateSpeed20002100Time() {
		return rotateSpeed20002100Time;
	}

	public void setRotateSpeed20002100Time(Long rotateSpeed20002100Time) {
		this.rotateSpeed20002100Time = rotateSpeed20002100Time;
	}

	public Long getRotateSpeed21002200() {
		return rotateSpeed21002200;
	}

	public void setRotateSpeed21002200(Long rotateSpeed21002200) {
		this.rotateSpeed21002200 = rotateSpeed21002200;
	}

	public Long getRotateSpeed21002200Time() {
		return rotateSpeed21002200Time;
	}

	public void setRotateSpeed21002200Time(Long rotateSpeed21002200Time) {
		this.rotateSpeed21002200Time = rotateSpeed21002200Time;
	}

	public Long getRotateSpeed22002300() {
		return rotateSpeed22002300;
	}

	public void setRotateSpeed22002300(Long rotateSpeed22002300) {
		this.rotateSpeed22002300 = rotateSpeed22002300;
	}

	public Long getRotateSpeed22002300Time() {
		return rotateSpeed22002300Time;
	}

	public void setRotateSpeed22002300Time(Long rotateSpeed22002300Time) {
		this.rotateSpeed22002300Time = rotateSpeed22002300Time;
	}

	public Long getRotateSpeed23002400() {
		return rotateSpeed23002400;
	}

	public void setRotateSpeed23002400(Long rotateSpeed23002400) {
		this.rotateSpeed23002400 = rotateSpeed23002400;
	}

	public Long getRotateSpeed23002400Time() {
		return rotateSpeed23002400Time;
	}

	public void setRotateSpeed23002400Time(Long rotateSpeed23002400Time) {
		this.rotateSpeed23002400Time = rotateSpeed23002400Time;
	}

	public Long getRotateSpeed24002500() {
		return rotateSpeed24002500;
	}

	public void setRotateSpeed24002500(Long rotateSpeed24002500) {
		this.rotateSpeed24002500 = rotateSpeed24002500;
	}

	public Long getRotateSpeed24002500Time() {
		return rotateSpeed24002500Time;
	}

	public void setRotateSpeed24002500Time(Long rotateSpeed24002500Time) {
		this.rotateSpeed24002500Time = rotateSpeed24002500Time;
	}

	public Long getRotateSpeed25002600() {
		return rotateSpeed25002600;
	}

	public void setRotateSpeed25002600(Long rotateSpeed25002600) {
		this.rotateSpeed25002600 = rotateSpeed25002600;
	}

	public Long getRotateSpeed25002600Time() {
		return rotateSpeed25002600Time;
	}

	public void setRotateSpeed25002600Time(Long rotateSpeed25002600Time) {
		this.rotateSpeed25002600Time = rotateSpeed25002600Time;
	}

	public Long getRotateSpeed26002700() {
		return rotateSpeed26002700;
	}

	public void setRotateSpeed26002700(Long rotateSpeed26002700) {
		this.rotateSpeed26002700 = rotateSpeed26002700;
	}

	public Long getRotateSpeed26002700Time() {
		return rotateSpeed26002700Time;
	}

	public void setRotateSpeed26002700Time(Long rotateSpeed26002700Time) {
		this.rotateSpeed26002700Time = rotateSpeed26002700Time;
	}

	public Long getRotateSpeed27002800() {
		return rotateSpeed27002800;
	}

	public void setRotateSpeed27002800(Long rotateSpeed27002800) {
		this.rotateSpeed27002800 = rotateSpeed27002800;
	}

	public Long getRotateSpeed27002800Time() {
		return rotateSpeed27002800Time;
	}

	public void setRotateSpeed27002800Time(Long rotateSpeed27002800Time) {
		this.rotateSpeed27002800Time = rotateSpeed27002800Time;
	}

	public Long getRotateSpeed28002900() {
		return rotateSpeed28002900;
	}

	public void setRotateSpeed28002900(Long rotateSpeed28002900) {
		this.rotateSpeed28002900 = rotateSpeed28002900;
	}

	public Long getRotateSpeed28002900Time() {
		return rotateSpeed28002900Time;
	}

	public void setRotateSpeed28002900Time(Long rotateSpeed28002900Time) {
		this.rotateSpeed28002900Time = rotateSpeed28002900Time;
	}

	public Long getRotateSpeed29003000() {
		return rotateSpeed29003000;
	}

	public void setRotateSpeed29003000(Long rotateSpeed29003000) {
		this.rotateSpeed29003000 = rotateSpeed29003000;
	}

	public Long getRotateSpeed29003000Time() {
		return rotateSpeed29003000Time;
	}

	public void setRotateSpeed29003000Time(Long rotateSpeed29003000Time) {
		this.rotateSpeed29003000Time = rotateSpeed29003000Time;
	}

	public Long getRotateSpeedMax() {
		return rotateSpeedMax;
	}

	public void setRotateSpeedMax(Long rotateSpeedMax) {
		this.rotateSpeedMax = rotateSpeedMax;
	}

	public Long getRotateSpeedMaxTime() {
		return rotateSpeedMaxTime;
	}

	public void setRotateSpeedMaxTime(Long rotateSpeedMaxTime) {
		this.rotateSpeedMaxTime = rotateSpeedMaxTime;
	}

	public Long getPercent6080Fuhelv() {
		return percent6080Fuhelv;
	}

	public void setPercent6080Fuhelv(Long percent6080Fuhelv) {
		this.percent6080Fuhelv = percent6080Fuhelv;
	}

	public Long getMinRotateSpeed() {
		return minRotateSpeed;
	}

	public void setMinRotateSpeed(Long minRotateSpeed) {
		this.minRotateSpeed = minRotateSpeed;
	}

	public Long getMaxRotateSpeed() {
		return maxRotateSpeed;
	}

	public void setMaxRotateSpeed(Long maxRotateSpeed) {
		this.maxRotateSpeed = maxRotateSpeed;
	}

	public Long getSumRotateSpeed() {
		return sumRotateSpeed;
	}

	public void setSumRotateSpeed(Long sumRotateSpeed) {
		this.sumRotateSpeed = sumRotateSpeed;
	}
	
	// 逐行统计发动机转速次数及时间，从日志文件中每取出一个转速，首先确定转速分布区间，连续多次上报转速命中同一区间则这段区间命中次数加1，时间进行逐条累加计算。  
	public RotateSpeedDay analyseRotateLineByLine(RotateSpeedDay rs, long roate,
			String torque,long gpsTime) {
		if (roate<=0){
			rs.setRpmSection("");
			rs.setPeriorGpsTime(-1);
		}else{
			double standRoate = roate*0.125;
			
			if (standRoate<100){
				rs.setRpmSection("");
				rs.setPeriorGpsTime(gpsTime);
			}else {

					//最大转速
				if(rs.getMaxRotateSpeed() > 0){
					if (roate > rs.getMaxRotateSpeed()){
						rs.setMaxRotateSpeed(roate);
					}
				}else{
					rs.setMaxRotateSpeed(roate);
				}
				//最小转速
				if(rs.getMinRotateSpeed() > 0){
					if (roate < rs.getMinRotateSpeed()){
						rs.setMinRotateSpeed(roate);
					}
				}else{
					rs.setMinRotateSpeed(roate);
				}
				
				//获取本次转速命中区间
				String currRpmSection = rotateSection(standRoate);
				
				//计算时间差值单位秒
				long time = 0L;
				if (rs.getPeriorGpsTime()>0){
					time = gpsTime - rs.getPeriorGpsTime();
					if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
						time = time/1000;
					}else{
						//记录本次命中区间
						rs.setRpmSection(currRpmSection);
						rs.setPeriorGpsTime(gpsTime);
						time = 0;
						return rs;
					}
				}
				
				if ("100_200".equals(rs.getRpmSection())) {

					rs.setRotateSpeed100200Time(rs.getRotateSpeed100200Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed100200(rs.getRotateSpeed100200() + 1);
					}			
				}else if ("200_300".equals(rs.getRpmSection())) {

					rs.setRotateSpeed200300Time(rs.getRotateSpeed200300Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed200300(rs.getRotateSpeed200300() + 1);
					}			
				}else if ("300_400".equals(rs.getRpmSection())) {

					rs.setRotateSpeed300400Time(rs.getRotateSpeed300400Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed300400(rs.getRotateSpeed300400() + 1);
					}			
				}else if ("400_500".equals(rs.getRpmSection())) {

					rs.setRotateSpeed400500Time(rs.getRotateSpeed400500Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed400500(rs.getRotateSpeed400500() + 1);
					}			
				}else if ("500_600".equals(rs.getRpmSection())) {

					rs.setRotateSpeed500600Time(rs.getRotateSpeed500600Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed500600(rs.getRotateSpeed500600() + 1);
					}			
				}else if ("600_700".equals(rs.getRpmSection())) {
					
					rs.setRotateSpeed600700Time(rs.getRotateSpeed600700Time()+time);
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed600700(rs.getRotateSpeed600700() + 1);
					}
				}else if ("700_800".equals(rs.getRpmSection())) {

					rs.setRotateSpeed700800Time(rs.getRotateSpeed700800Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed700800(rs.getRotateSpeed700800() + 1);
					}			
				}else if ("800_900".equals(rs.getRpmSection())) {

					rs.setRotateSpeed800900Time(rs.getRotateSpeed800900Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed800900(rs.getRotateSpeed800900() + 1);
					}			
				}else if ("900_1000".equals(rs.getRpmSection())) {

					rs.setRotateSpeed9001000Time(rs.getRotateSpeed9001000Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed9001000(rs.getRotateSpeed9001000() + 1);
					}			
				}else if ("1000_1100".equals(rs.getRpmSection())) {

					rs.setRotateSpeed10001100Time(rs.getRotateSpeed10001100Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed10001100(rs.getRotateSpeed10001100() + 1);
					}			
				}else if ("1100_1200".equals(rs.getRpmSection())) {

					rs.setRotateSpeed11001200Time(rs.getRotateSpeed11001200Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed11001200(rs.getRotateSpeed11001200() + 1);
					}			
				}else if ("1200_1300".equals(rs.getRpmSection())) {

					rs.setRotateSpeed12001300Time(rs.getRotateSpeed12001300Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed12001300(rs.getRotateSpeed12001300() + 1);
					}			
				}else if ("1300_1400".equals(rs.getRpmSection())) {

					rs.setRotateSpeed13001400Time(rs.getRotateSpeed13001400Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed13001400(rs.getRotateSpeed13001400() + 1);
					}			
				}else if ("1400_1500".equals(rs.getRpmSection())) {

					rs.setRotateSpeed14001500Time(rs.getRotateSpeed14001500Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed14001500(rs.getRotateSpeed14001500() + 1);
					}			
				}else if ("1500_1600".equals(rs.getRpmSection())) {

					rs.setRotateSpeed15001600Time(rs.getRotateSpeed15001600Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed15001600(rs.getRotateSpeed15001600() + 1);
					}			
				}else if ("1600_1700".equals(rs.getRpmSection())) {

					rs.setRotateSpeed16001700Time(rs.getRotateSpeed16001700Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed16001700(rs.getRotateSpeed16001700() + 1);
					}			
				}else if ("1700_1800".equals(rs.getRpmSection())) {

					rs.setRotateSpeed17001800Time(rs.getRotateSpeed17001800Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed17001800(rs.getRotateSpeed17001800() + 1);
					}			
				}else if ("1800_1900".equals(rs.getRpmSection())) {

					rs.setRotateSpeed18001900Time(rs.getRotateSpeed18001900Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed18001900(rs.getRotateSpeed18001900() + 1);
					}			
				}else if ("1900_2000".equals(rs.getRpmSection())) {

					rs.setRotateSpeed19002000Time(rs.getRotateSpeed19002000Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed19002000(rs.getRotateSpeed19002000() + 1);
					}			
				}else if ("2000_2100".equals(rs.getRpmSection())) {

					rs.setRotateSpeed20002100Time(rs.getRotateSpeed20002100Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed20002100(rs.getRotateSpeed20002100() + 1);
					}			
				}else if ("2100_2200".equals(rs.getRpmSection())) {

					rs.setRotateSpeed21002200Time(rs.getRotateSpeed21002200Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed21002200(rs.getRotateSpeed21002200() + 1);
					}			
				}else if ("2200_2300".equals(rs.getRpmSection())) {

					rs.setRotateSpeed22002300Time(rs.getRotateSpeed22002300Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed22002300(rs.getRotateSpeed22002300() + 1);
					}			
				}else if ("2300_2400".equals(rs.getRpmSection())) {

					rs.setRotateSpeed23002400Time(rs.getRotateSpeed23002400Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed23002400(rs.getRotateSpeed23002400() + 1);
					}			
				}else if ("2400_2500".equals(rs.getRpmSection())) {

					rs.setRotateSpeed24002500Time(rs.getRotateSpeed24002500Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed24002500(rs.getRotateSpeed24002500() + 1);
					}			
				}else if ("2500_2600".equals(rs.getRpmSection())) {

					rs.setRotateSpeed25002600Time(rs.getRotateSpeed25002600Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed25002600(rs.getRotateSpeed25002600() + 1);
					}			
				}else if ("2600_2700".equals(rs.getRpmSection())) {

					rs.setRotateSpeed26002700Time(rs.getRotateSpeed26002700Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed26002700(rs.getRotateSpeed26002700() + 1);
					}			
				}else if ("2700_2800".equals(rs.getRpmSection())) {

					rs.setRotateSpeed27002800Time(rs.getRotateSpeed27002800Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed27002800(rs.getRotateSpeed27002800() + 1);
					}			
				}else if ("2800_2900".equals(rs.getRpmSection())) {

					rs.setRotateSpeed28002900Time(rs.getRotateSpeed28002900Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed28002900(rs.getRotateSpeed28002900() + 1);
					}			
				}else if ("2900_3000".equals(rs.getRpmSection())) {

					rs.setRotateSpeed29003000Time(rs.getRotateSpeed29003000Time()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeed29003000(rs.getRotateSpeed29003000() + 1);
					}			
				}else if ("3000_max".equals(rs.getRpmSection())) {

					rs.setRotateSpeedMaxTime(rs.getRotateSpeedMaxTime()+time);				
					
					if (!currRpmSection.equals(rs.getRpmSection())){
						rs.setRotateSpeedMax(rs.getRotateSpeedMax() + 1);
					}			
				}
				
				//记录本次命中区间
				rs.setRpmSection(currRpmSection);
				rs.setPeriorGpsTime(gpsTime);

			}
		}
		//统计当日车辆行驶时发动机转速转速分布次数和时长 实际转速小于100Rpm时视为转速为0

		/*if (1000 <= roate && roate <= 1600) {
			float f = Float.valueOf(torque) * 100;
			if (60 <= f && f <= 80) {

			}
		}*/
		
		return rs;
	}

	public String getRpmSection() {
		return rpmSection;
	}

	public void setRpmSection(String rpmSection) {
		this.rpmSection = rpmSection;
	}

	public long getPeriorGpsTime() {
		return periorGpsTime;
	}

	public void setPeriorGpsTime(long periorGpsTime) {
		this.periorGpsTime = periorGpsTime;
	}
	
	private String rotateSection(double rotateSpeed){
		if (100 <= rotateSpeed && rotateSpeed < 200){
			return "100_200";
		}else if (200 <= rotateSpeed && rotateSpeed < 300){
			return "200_300";
		}else if (300 <= rotateSpeed && rotateSpeed < 400){
			return "300_400";
		}else if (400 <= rotateSpeed && rotateSpeed < 500){
			return "400_500";
		}else if (500 <= rotateSpeed && rotateSpeed < 600){
			return "500_600";
		}else if (600 <= rotateSpeed && rotateSpeed < 700){
			return "600_700";
		}else if (700 <= rotateSpeed && rotateSpeed < 800){
			return "700_800";
		}else if (800 <= rotateSpeed && rotateSpeed < 900){
			return "800_900";
		}else if (900 <= rotateSpeed && rotateSpeed < 1000){
			return "900_1000";
		}else if (1000 <= rotateSpeed && rotateSpeed < 1100){
			return "1000_1100";
		}else if (1100 <= rotateSpeed && rotateSpeed < 1200){
			return "1100_1200";
		}else if (1200 <= rotateSpeed && rotateSpeed < 1300){
			return "1200_1300";
		}else if (1300 <= rotateSpeed && rotateSpeed < 1400){
			return "1300_1400";
		}else if (1400 <= rotateSpeed && rotateSpeed < 1500){
			return "1400_1500";
		}else if (1500 <= rotateSpeed && rotateSpeed < 1600){
			return "1500_1600";
		}else if (1600 <= rotateSpeed && rotateSpeed < 1700){
			return "1600_1700";
		}else if (1700 <= rotateSpeed && rotateSpeed < 1800){
			return "1700_1800";
		}else if (1800 <= rotateSpeed && rotateSpeed < 1900){
			return "1800_1900";
		}else if (1900 <= rotateSpeed && rotateSpeed < 2000){
			return "1900_2000";
		}else if (2000 <= rotateSpeed && rotateSpeed < 2100){
			return "2000_2100";
		}else if (2100 <= rotateSpeed && rotateSpeed < 2200){
			return "2100_2200";
		}else if (2200 <= rotateSpeed && rotateSpeed < 2300){
			return "2200_2300";
		}else if (2300 <= rotateSpeed && rotateSpeed < 2400){
			return "2300_2400";
		}else if (2400 <= rotateSpeed && rotateSpeed < 2500){
			return "2400_2500";
		}else if (2500 <= rotateSpeed && rotateSpeed < 2600){
			return "2500_2600";
		}else if (2600 <= rotateSpeed && rotateSpeed < 2700){
			return "2600_2700";
		}else if (2700 <= rotateSpeed && rotateSpeed < 2800){
			return "2700_2800";
		}else if (2800 <= rotateSpeed && rotateSpeed < 2900){
			return "2800_2900";
		}else if (2900 <= rotateSpeed && rotateSpeed < 3000){
			return "1900_2000";
		}else if (rotateSpeed >= 3000){
			return "3000_max";
		}else{
			return "";
		}
	}

	public long getStatDate() {
		return statDate;
	}

	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}

}
