package com.ctfo.storage.init.model;



import java.io.Serializable;

@SuppressWarnings("serial")
public class TbTerminal extends BaseModel implements Serializable {

	/** 视频设备编号 */
	private String deviceName = "";

	/** */
	private String tid ="";

	/** */
	private String tmac = "";

	/** */
	private String authCode = "";

	/** */
	private String oemCode = "";

	/** */
	private String terHardver = "";

	/** */
	private String terSoftver = "";

	/** */
	private String communicateId = "";

	/** */
	private String videoId = "";

	/** */
	private long terUtype = 0;

	/** */
	private Long terMdate =-1l;

	/** */
	private Long terEdate =-1l;

	/** */
	private String terEperson = "";

	/** */
	private Long terPrice =-1l;

	/** */
	private Long terEcost =-1l;

	/** */
	private String terMem = "";

	/** */
	private String createBy ="";

	/** */
	private Long createTime =-1l;

	/** */
	private String createTimeStr = "";

	/** */
	private String vidioOemCode = "";

	/** */
	private String updateBy ="";

	/** */
	private Long updateTime =-1l;

	/** */
	private String enableFlag = "";

	/** */
	private String entId = "";

	/** */
	private int terState;

	/** */
	private String tprotocolId = "";

	/** */
	private Long configId =-1l;

	/** */
	private String tmodelCode = "";

	/** -1未注册，0已注�?*/
	private Integer regStatus;
	/** 交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过)*/
	private Integer deliveryStatus = -1;

	/** 固件版本*/
	private String firmwareVersion = "";
	
	/** 硬件版本号*/
	private String hardwareVersion = "";
	
	public String getDeviceName() {
		return deviceName;
	}

	public void setDeviceName(String deviceName) {
		this.deviceName = deviceName;
	}

	public String getTid() {
		return tid;
	}

	public void setTid(String tid) {
		this.tid = tid;
	}

	public String getTmac() {
		return tmac;
	}

	public void setTmac(String tmac) {
		this.tmac = tmac;
	}

	public String getAuthCode() {
		return authCode;
	}

	public void setAuthCode(String authCode) {
		this.authCode = authCode;
	}

	public String getOemCode() {
		return oemCode;
	}

	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}

	public String getTerHardver() {
		return terHardver;
	}

	public void setTerHardver(String terHardver) {
		this.terHardver = terHardver;
	}

	public String getTerSoftver() {
		return terSoftver;
	}

	public void setTerSoftver(String terSoftver) {
		this.terSoftver = terSoftver;
	}

	public String getCommunicateId() {
		return communicateId;
	}

	public void setCommunicateId(String communicateId) {
		this.communicateId = communicateId;
	}

	public String getVideoId() {
		return videoId;
	}

	public void setVideoId(String videoId) {
		this.videoId = videoId;
	}

	public long getTerUtype() {
		return terUtype;
	}

	public void setTerUtype(long terUtype) {
		this.terUtype = terUtype;
	}

	public Long getTerMdate() {
		return terMdate;
	}

	public void setTerMdate(Long terMdate) {
		this.terMdate = terMdate;
	}

	public Long getTerEdate() {
		return terEdate;
	}

	public void setTerEdate(Long terEdate) {
		this.terEdate = terEdate;
	}

	public String getTerEperson() {
		return terEperson;
	}

	public void setTerEperson(String terEperson) {
		this.terEperson = terEperson;
	}

	public Long getTerPrice() {
		return terPrice;
	}

	public void setTerPrice(Long terPrice) {
		this.terPrice = terPrice;
	}

	public Long getTerEcost() {
		return terEcost;
	}

	public void setTerEcost(Long terEcost) {
		this.terEcost = terEcost;
	}

	public String getTerMem() {
		return terMem;
	}

	public void setTerMem(String terMem) {
		this.terMem = terMem;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getCreateTimeStr() {
		return createTimeStr;
	}

	public void setCreateTimeStr(String createTimeStr) {
		this.createTimeStr = createTimeStr;
	}

	public String getVidioOemCode() {
		return vidioOemCode;
	}

	public void setVidioOemCode(String vidioOemCode) {
		this.vidioOemCode = vidioOemCode;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public int getTerState() {
		return terState;
	}

	public void setTerState(int terState) {
		this.terState = terState;
	}

	public String getTprotocolId() {
		return tprotocolId;
	}

	public void setTprotocolId(String tprotocolId) {
		this.tprotocolId = tprotocolId;
	}

	public Long getConfigId() {
		return configId;
	}

	public void setConfigId(Long configId) {
		this.configId = configId;
	}

	public String getTmodelCode() {
		return tmodelCode;
	}

	public void setTmodelCode(String tmodelCode) {
		this.tmodelCode = tmodelCode;
	}

	public Integer getRegStatus() {
		return regStatus;
	}

	public void setRegStatus(Integer regStatus) {
		this.regStatus = regStatus;
	}

	/**
	 * 获取交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过)的值
	 * @return deliveryStatus  
	 */
	public Integer getDeliveryStatus() {
		return deliveryStatus;
	}

	/**
	 * 设置交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过)的值
	 * @param deliveryStatus
	 */
	public void setDeliveryStatus(Integer deliveryStatus) {
		this.deliveryStatus = deliveryStatus;
	}

	/**
	 * 获取固件版本的值
	 * @return firmwareVersion  
	 */
	public String getFirmwareVersion() {
		return firmwareVersion;
	}

	/**
	 * 设置固件版本的值
	 * @param firmwareVersion
	 */
	public void setFirmwareVersion(String firmwareVersion) {
		this.firmwareVersion = firmwareVersion;
	}

	/**
	 * 获取硬件版本号的值
	 * @return hardwareVersion  
	 */
	public String getHardwareVersion() {
		return hardwareVersion;
	}

	/**
	 * 设置硬件版本号的值
	 * @param hardwareVersion
	 */
	public void setHardwareVersion(String hardwareVersion) {
		this.hardwareVersion = hardwareVersion;
	}
	
	
}