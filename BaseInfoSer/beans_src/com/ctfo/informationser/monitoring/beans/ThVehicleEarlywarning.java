package com.ctfo.informationser.monitoring.beans;

import com.ctfo.local.bean.ETB_Base;

public class ThVehicleEarlywarning extends ETB_Base{
    /**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5892597310240247247L;

	/**
     * 表唯一标识
     */
    private String pid;

    /**
     * 报警来源
     */
    private Short alarmFrom;

    /**
     * 报警类型
     */
    private String alarmType;

    /**
     * 报警时间
     */
    private Long alarmTime;

    /**
     * 报警描述
     */
    private String alarmDescr;

    /**
     * 车辆标识
     */
    private String vid;
    
	/**
	 * @return the pid
	 */
	public String getPid() {
		return pid;
	}

	/**
	 * @param pid the pid to set
	 */
	public void setPid(String pid) {
		this.pid = pid;
	}

	/**
	 * @return the alarmFrom
	 */
	public Short getAlarmFrom() {
		return alarmFrom;
	}

	/**
	 * @param alarmFrom the alarmFrom to set
	 */
	public void setAlarmFrom(Short alarmFrom) {
		this.alarmFrom = alarmFrom;
	}

	/**
	 * @return the alarmType
	 */
	public String getAlarmType() {
		return alarmType;
	}

	/**
	 * @param alarmType the alarmType to set
	 */
	public void setAlarmType(String alarmType) {
		this.alarmType = alarmType;
	}

	/**
	 * @return the alarmTime
	 */
	public Long getAlarmTime() {
		return alarmTime;
	}

	/**
	 * @param alarmTime the alarmTime to set
	 */
	public void setAlarmTime(Long alarmTime) {
		this.alarmTime = alarmTime;
	}

	/**
	 * @return the alarmDescr
	 */
	public String getAlarmDescr() {
		return alarmDescr;
	}

	/**
	 * @param alarmDescr the alarmDescr to set
	 */
	public void setAlarmDescr(String alarmDescr) {
		this.alarmDescr = alarmDescr;
	}

	/**
	 * @return the vid
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * @param vid the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}


}