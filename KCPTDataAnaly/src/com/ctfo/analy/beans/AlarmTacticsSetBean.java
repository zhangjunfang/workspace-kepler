package com.ctfo.analy.beans;

/**
 * 超速、疲劳驾驶、非法营运 告警设置
 * @author yujch
 *
 */
public class AlarmTacticsSetBean {
	private String startTime;//超速判定开始时间
	private String endTime;// 非法运营结束时间
	private Long speedScale;//限速比例
	private Long deferred; //非法营运、疲劳驾驶 持续时间
	
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
	public Long getSpeedScale() {
		return speedScale;
	}
	public void setSpeedScale(Long speedScale) {
		this.speedScale = speedScale;
	}
	public Long getDeferred() {
		return deferred;
	}
	public void setDeferred(Long deferred) {
		this.deferred = deferred;
	}
}
