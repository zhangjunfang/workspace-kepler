package com.caits.analysisserver.bean;


/**
 * 缓存实体对象
 * @author yangyi
 *
 */
public class AlarmCacheBean{

	private String vid;//车ID
	private Long utc=0l;//上一点UTC
	private Boolean issaveorverspeed=false;//超速已存
	private Long overspeedbegintime=0l;//超速开始时间
	private double speedThreshold;//超速阀值
	private Long alarmbegintime=0l;//线路围栏报警开始时间
	
	private Long begintime=0l;//非法运营开始时间
	private Long endTime=0l;//非法运营结束时间
	
	private Long stopVehicleTime=0l;//疲劳驾驶停车时间
	
	private String alarmId;//报警编号
	private String alarmcode;//报警码
	private String alarmlevel;//报警等级
	private String alarmadd;//报警附加信息
	
	private String alarmaddInfo;//报警描述信息
	
	private int alarmSrc;
	
	private boolean saved = false;//告警是否保存
	
	private long maxSpeed = -1;
	
	private Long mileage=0L;//里程
	private Long oil=0L;//油量
	private Long metOil=0L;//精准油耗
	
	private String areaId; // 电子围栏编号
	
	private String mtypeCode = null; // 多媒体类型
	
	private String mediaUrl = null; // 多媒体信息
	
	private  String vlineId; // 线路ID
	
	private String lineName = null; // 线路名称
	
	private int count=0; //非法运营软报警使用的计数器
	
	private String configId;//配置ID
	private String addressNo;//道路等级
	
	private Long bufferTime;//报警缓冲时间
	
	private Long limitSpeed;//限速值
	
	private String configId2;//配置ID
	
	private String configName;//配置名称
	
	private String alarmType;//标注是轨迹文件分析的告警TRACK 还是驾驶行为事件中的告警EVENT
	
	private VehicleMessageBean beginVmb;//告警起始点轨迹对象缓存
	private VehicleMessageBean endVmb;//告警结束点轨迹对象缓存
	
	
	private long totalSpeed=0;
	
	private int avgNum=0;
	
	private String inoutAreaAlarmDir;//进出区域告警方向 0进 1出
	
	private String isRunningenough;//路段行驶时间不足或过长结果 0：不足，1：过长
	
	private long runningEnoughTime;//路段行驶告警时长 
	
	public String getAlarmcode() {
		return alarmcode;
	}
	public void setAlarmcode(String alarmcode) {
		this.alarmcode = alarmcode;
	}
	public String getAlarmlevel() {
		return alarmlevel;
	}
	public void setAlarmlevel(String alarmlevel) {
		this.alarmlevel = alarmlevel;
	}
	public String getAlarmadd() {
		return alarmadd;
	}
	public void setAlarmadd(String alarmadd) {
		this.alarmadd = alarmadd;
	}
	public Long getUtc() {
		return utc;
	}
	public void setUtc(Long utc) {
		this.utc = utc;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public Long getBegintime() {
		return begintime;
	}
	public void setBegintime(Long begintime) {
		this.begintime = begintime;
	}
	public Long getEndTime() {
		return endTime;
	}
	public void setEndTime(Long endTime) {
		this.endTime = endTime;
	}
	public String getAlarmId() {
		return alarmId;
	}
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}
	public boolean isSaved() {
		return saved;
	}
	public void setSaved(boolean saved) {
		this.saved = saved;
	}
	public VehicleMessageBean getBeginVmb() {
		return beginVmb;
	}
	public void setBeginVmb(VehicleMessageBean beginVmb) {
		this.beginVmb = beginVmb;
	}
	public VehicleMessageBean getEndVmb() {
		return endVmb;
	}
	public void setEndVmb(VehicleMessageBean endVmb) {
		this.endVmb = endVmb;
	}
	public long getMaxSpeed() {
		return maxSpeed;
	}
	public void setMaxSpeed(long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}
	public Long getMileage() {
		return mileage;
	}
	public void setMileage(Long mileage) {
		this.mileage = mileage;
	}
	public Long getOil() {
		return oil;
	}
	public void setOil(Long oil) {
		this.oil = oil;
	}
	public Long getMetOil() {
		return metOil;
	}
	public void setMetOil(Long metOil) {
		this.metOil = metOil;
	}
	public String getAreaId() {
		return areaId;
	}
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}
	public String getMtypeCode() {
		return mtypeCode;
	}
	public void setMtypeCode(String mtypeCode) {
		this.mtypeCode = mtypeCode;
	}
	public String getMediaUrl() {
		return mediaUrl;
	}
	public void setMediaUrl(String mediaUrl) {
		this.mediaUrl = mediaUrl;
	}
	public String getVlineId() {
		return vlineId;
	}
	public void setVlineId(String vlineId) {
		this.vlineId = vlineId;
	}
	public String getLineName() {
		return lineName;
	}
	public void setLineName(String lineName) {
		this.lineName = lineName;
	}
	public int getCount() {
		return count;
	}
	public void setCount(int count) {
		this.count = count;
	}
	public Boolean getIssaveorverspeed() {
		return issaveorverspeed;
	}
	public void setIssaveorverspeed(Boolean issaveorverspeed) {
		this.issaveorverspeed = issaveorverspeed;
	}
	public Long getOverspeedbegintime() {
		return overspeedbegintime;
	}
	public void setOverspeedbegintime(Long overspeedbegintime) {
		this.overspeedbegintime = overspeedbegintime;
	}
	public Long getAlarmbegintime() {
		return alarmbegintime;
	}
	public void setAlarmbegintime(Long alarmbegintime) {
		this.alarmbegintime = alarmbegintime;
	}
	public String getConfigId() {
		return configId;
	}
	public void setConfigId(String configId) {
		this.configId = configId;
	}
	public String getAddressNo() {
		return addressNo;
	}
	public void setAddressNo(String addressNo) {
		this.addressNo = addressNo;
	}
	public Long getBufferTime() {
		return bufferTime;
	}
	public void setBufferTime(Long bufferTime) {
		this.bufferTime = bufferTime;
	}
	public Long getLimitSpeed() {
		return limitSpeed;
	}
	public void setLimitSpeed(Long limitSpeed) {
		this.limitSpeed = limitSpeed;
	}
	public String getConfigId2() {
		return configId2;
	}
	public void setConfigId2(String configId2) {
		this.configId2 = configId2;
	}
	public String getConfigName() {
		return configName;
	}
	public void setConfigName(String configName) {
		this.configName = configName;
	}
	public double getSpeedThreshold() {
		return speedThreshold;
	}
	public void setSpeedThreshold(double speedThreshold) {
		this.speedThreshold = speedThreshold;
	}
	public Long getStopVehicleTime() {
		return stopVehicleTime;
	}
	public void setStopVehicleTime(Long stopVehicleTime) {
		this.stopVehicleTime = stopVehicleTime;
	}
	public int getAlarmSrc() {
		return alarmSrc;
	}
	public void setAlarmSrc(int alarmSrc) {
		this.alarmSrc = alarmSrc;
	}
	public String getAlarmaddInfo() {
		return alarmaddInfo;
	}
	public void setAlarmaddInfo(String alarmaddInfo) {
		this.alarmaddInfo = alarmaddInfo;
	}
	
	public void setAvgSpeed(long speed){
		this.totalSpeed += speed;
		this.avgNum ++;
	}
	
	public Long getAvgSpeed(){
		if (this.avgNum>0&&this.totalSpeed>=0){
			return this.totalSpeed/this.avgNum;
		}else{
			return 0L;
		}
	}
	public String getAlarmType() {
		return alarmType;
	}
	public void setAlarmType(String alarmType) {
		this.alarmType = alarmType;
	}
	public String getInoutAreaAlarmDir() {
		return inoutAreaAlarmDir;
	}
	public void setInoutAreaAlarmDir(String inoutAreaAlarmDir) {
		this.inoutAreaAlarmDir = inoutAreaAlarmDir;
	}
	public String getIsRunningenough() {
		return isRunningenough;
	}
	public void setIsRunningenough(String isRunningenough) {
		this.isRunningenough = isRunningenough;
	}
	public long getRunningEnoughTime() {
		return runningEnoughTime;
	}
	public void setRunningEnoughTime(long runningEnoughTime) {
		this.runningEnoughTime = runningEnoughTime;
	}

}