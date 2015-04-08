package com.caits.analysisserver.bean;


/**
 * 非法运营软报警实体对象
 * @author yujch
 *
 */
public class IllegalOptionsAlarmBean {
	private String vid;//车辆ID
	private String entId;//所属企业
	private String startTime;//非法运营开始时间
	private String endTime;// 非法运营结束时间
	private Long deferred; //持续时间
	private String isDefault;//是否默认值
	
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getEntId() {
		return entId;
	}
	public void setEntId(String entId) {
		this.entId = entId;
	}
	public String getStartTime() {
		return startTime;
	}
	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}
	public String getEndTime() {
		return endTime;
	}
	public void setEndTime(String endTime) {
		this.endTime = endTime;
	}
	public Long getDeferred() {
		return deferred;
	}
	public void setDeferred(Long deferred) {
		this.deferred = deferred;
	}
	public String getIsDefault() {
		return isDefault;
	}
	public void setIsDefault(String isDefault) {
		this.isDefault = isDefault;
	}
	

	 
}
