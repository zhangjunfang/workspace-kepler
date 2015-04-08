package com.ctfo.analy.beans;


/**
 * 道路等级报警缓存类
 * 
 * @author LiangJian
 * 2012年12月11日16:04:39
 */
public class RoadCacheBean {

	private String vid;//车ID
	private String configId;//配置ID
	private String addressNo;//道路等级
	
	private Long bufferTime;//报警缓冲时间
	
	private Long limitSpeed;//限速值
	
	private String configId2;//配置ID
	
	private String configName;//配置名称
	
	private String alarmId;//报警编号
	
	private boolean saved = false;
	
	private VehicleMessageBean alarmBeginBean = null;
	
	/** 0高速超速 已存 下标编号*/
	public static final int isSaveGaoSu = 0;
	/** 1国道超速 已存 下标编号*/
	public static final int isSaveGuoDao = 1;
	/** 2省道超速 已存 下标编号*/
	public static final int isSaveShengDao = 2;
	/** 3城区超速 已存 下标编号*/
	public static final int isSaveChengQu = 3;
	/** 4其他超速 已存 下标编号*/
	public static final int isSaveQiTa = 4;
	
	/** 各种类型道路超速已存状态  和上述下标对应  */
	private Boolean[] isSave = new Boolean[]{false,false,false,false,false};

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getConfigId() {
		return configId;
	}

	public void setConfigId(String configId) {
		this.configId = configId;
	}

	public Boolean[] getIsSave() {
		return isSave;
	}

	public void setIsSave(Boolean[] isSave) {
		this.isSave = isSave;
	}

	public String getAddressNo() {
		return addressNo;
	}

	public void setAddressNo(String addressNo) {
		this.addressNo = addressNo;
	}

	public VehicleMessageBean getAlarmBeginBean() {
		return alarmBeginBean;
	}

	public void setAlarmBeginBean(VehicleMessageBean alarmBeginBean) {
		this.alarmBeginBean = alarmBeginBean;
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

	public String getAlarmId() {
		return alarmId;
	}

	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}

	public String getConfigName() {
		return configName;
	}

	public void setConfigName(String configName) {
		this.configName = configName;
	}

	public boolean isSaved() {
		return saved;
	}

	public void setSaved(boolean saved) {
		this.saved = saved;
	} 

}
