package com.ctfo.analy.beans;

	/**
	 * 非法运营缓存实体对象
	 * @author yujch
	 *
	 */
	public class IllegalOptionsCacheBean {

		private Long utc=0l;//上一点UTC
		private Boolean isTriggerAlarm=false;//是否触发实时报警
		private Long initTime=0l;//非法运营初始时间
		private Long begintime=0l;//非法运营开始时间
		private Long endTime=0l;//非法运营结束时间
		private Long endTime2=0l;//非法运营结束时间
		
		private String alarmcode;//报警码
		private String alarmlevel;//报警等级
		private String alarmadd;//报警附加信息
		
		private VehicleMessageBean beginVmb;//非法运营起始点轨迹对象缓存
		private VehicleMessageBean endVmb;//非法运营结束点轨迹对象缓存
		
		private long maxSpeed;
		
		private Long mileage=0L;//里程
		private Long oil=0L;//油量
		private Long metOil=0L;//精准油耗
		
		private String AREA_ID = ""; // 电子围栏编号
		
		private String mtypeCode = null; // 多媒体类型
		
		private String mediaUrl = null; // 多媒体信息
		
		private  String vlineId = ""; // 线路ID
		
		private String lineName = null; // 线路名称
		
		private int count=0;
		
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
		
		public Long getBegintime() {
			return begintime;
		}
		public void setBegintime(Long begintime) {
			this.begintime = begintime;
		}
		public Boolean getIsTriggerAlarm() {
			return isTriggerAlarm;
		}
		public void setIsTriggerAlarm(Boolean isTriggerAlarm) {
			this.isTriggerAlarm = isTriggerAlarm;
		}
		public Long getInitTime() {
			return initTime;
		}
		public void setInitTime(Long initTime) {
			this.initTime = initTime;
		}
		public Long getEndTime() {
			return endTime;
		}
		public void setEndTime(Long endTime) {
			this.endTime = endTime;
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
		public String getAREA_ID() {
			return AREA_ID;
		}
		public void setAREA_ID(String aREA_ID) {
			AREA_ID = aREA_ID;
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
		public int getCount() {
			return count;
		}
		public void setCount(int count) {
			this.count = count;
		}
		public Long getEndTime2() {
			return endTime2;
		}
		public void setEndTime2(Long endTime2) {
			this.endTime2 = endTime2;
		}
	}

