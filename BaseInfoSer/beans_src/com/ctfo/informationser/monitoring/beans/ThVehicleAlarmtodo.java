package com.ctfo.informationser.monitoring.beans;

import com.ctfo.local.bean.ETB_Base;

public class ThVehicleAlarmtodo extends ETB_Base{
    /**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = -8862018562252562916L;

	/**
     *主键
     */
    private String pid;

    /**
     * 车牌号
     */
    private String vehicleno;

    /**
     * 车牌颜色
     */
    private Short vehicleColor;

    /**
     * 1:车载终端,2:企业监控平台,3:政府监管平台,9其它
     */
    private Short wanSrc;

    /**
     * 报警类型
     */
    private Long wanType;

    /**
     * 报警时间
     */
    private Long warUtc;

    /**
     * 报警督办ID
     */
    private String supervisionId;

    /**
     * 督办截止时间
     */
    private Long supervisionEndUtc;

    /**
     * 督办级别,0:紧急,1:一般
     */
    private Short supervisionLevel;

    /**
     * 督办人
     */
    private String supervisor;

    /**
     * 督办联系电话
     */
    private String supervisorTel;

    /**
     * 督办联系电子邮件
     */
    private String supervisorEmail;

    /**
     * 督办时间
     */
    private Long utc;

    /**
     * 0：处理中 1：已处理完毕 2：不作处理 3：将来处理
     */
    private Short status;

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
	 * @return the vehicleno
	 */
	public String getVehicleno() {
		return vehicleno;
	}

	/**
	 * @param vehicleno the vehicleno to set
	 */
	public void setVehicleno(String vehicleno) {
		this.vehicleno = vehicleno;
	}

	/**
	 * @return the vehicleColor
	 */
	public Short getVehicleColor() {
		return vehicleColor;
	}

	/**
	 * @param vehicleColor the vehicleColor to set
	 */
	public void setVehicleColor(Short vehicleColor) {
		this.vehicleColor = vehicleColor;
	}

	/**
	 * @return the wanSrc
	 */
	public Short getWanSrc() {
		return wanSrc;
	}

	/**
	 * @param wanSrc the wanSrc to set
	 */
	public void setWanSrc(Short wanSrc) {
		this.wanSrc = wanSrc;
	}

	/**
	 * @return the wanType
	 */
	public Long getWanType() {
		return wanType;
	}

	/**
	 * @param wanType the wanType to set
	 */
	public void setWanType(Long wanType) {
		this.wanType = wanType;
	}

	/**
	 * @return the warUtc
	 */
	public Long getWarUtc() {
		return warUtc;
	}

	/**
	 * @param warUtc the warUtc to set
	 */
	public void setWarUtc(Long warUtc) {
		this.warUtc = warUtc;
	}

	/**
	 * @return the supervisionId
	 */
	public String getSupervisionId() {
		return supervisionId;
	}

	/**
	 * @param supervisionId the supervisionId to set
	 */
	public void setSupervisionId(String supervisionId) {
		this.supervisionId = supervisionId;
	}

	/**
	 * @return the supervisionEndUtc
	 */
	public Long getSupervisionEndUtc() {
		return supervisionEndUtc;
	}

	/**
	 * @param supervisionEndUtc the supervisionEndUtc to set
	 */
	public void setSupervisionEndUtc(Long supervisionEndUtc) {
		this.supervisionEndUtc = supervisionEndUtc;
	}

	/**
	 * @return the supervisionLevel
	 */
	public Short getSupervisionLevel() {
		return supervisionLevel;
	}

	/**
	 * @param supervisionLevel the supervisionLevel to set
	 */
	public void setSupervisionLevel(Short supervisionLevel) {
		this.supervisionLevel = supervisionLevel;
	}

	/**
	 * @return the supervisor
	 */
	public String getSupervisor() {
		return supervisor;
	}

	/**
	 * @param supervisor the supervisor to set
	 */
	public void setSupervisor(String supervisor) {
		this.supervisor = supervisor;
	}

	/**
	 * @return the supervisorTel
	 */
	public String getSupervisorTel() {
		return supervisorTel;
	}

	/**
	 * @param supervisorTel the supervisorTel to set
	 */
	public void setSupervisorTel(String supervisorTel) {
		this.supervisorTel = supervisorTel;
	}

	/**
	 * @return the supervisorEmail
	 */
	public String getSupervisorEmail() {
		return supervisorEmail;
	}

	/**
	 * @param supervisorEmail the supervisorEmail to set
	 */
	public void setSupervisorEmail(String supervisorEmail) {
		this.supervisorEmail = supervisorEmail;
	}

	/**
	 * @return the utc
	 */
	public Long getUtc() {
		return utc;
	}

	/**
	 * @param utc the utc to set
	 */
	public void setUtc(Long utc) {
		this.utc = utc;
	}

	/**
	 * @return the status
	 */
	public Short getStatus() {
		return status;
	}

	/**
	 * @param status the status to set
	 */
	public void setStatus(Short status) {
		this.status = status;
	}

}