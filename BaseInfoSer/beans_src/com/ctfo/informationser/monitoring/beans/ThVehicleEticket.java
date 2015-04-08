package com.ctfo.informationser.monitoring.beans;

import com.ctfo.local.bean.ETB_Base;

public class ThVehicleEticket extends ETB_Base{
    /**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = -3393980473439654627L;

	/**
     * 表唯一标识
     */
    private String pid;

    /**
     * 车辆标识
     */
    private String vid;

    /**
     * 电子运单内容
     */
    private String eticketContent;

    /**
     * 上传时间
     */
    private Long utc;

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

	/**
	 * @return the eticketContent
	 */
	public String getEticketContent() {
		return eticketContent;
	}

	/**
	 * @param eticketContent the eticketContent to set
	 */
	public void setEticketContent(String eticketContent) {
		this.eticketContent = eticketContent;
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
}