package com.ctfo.informationser.monitoring.beans;

import com.ctfo.local.bean.ETB_Base;

public class ThLinkStatus extends ETB_Base{
    /**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = -8435072657027930332L;

	/**
     * 主键
     */
    private String pid;

    /**
     * 省域编码
     */
    private String areaId;

    /**
     *链路类型，0：主链路，1：从链路
     */
    private Short linkType;

    /**
     * 断开时间
     */
    private Long disconnectUtc;

    /**
     * 链路连接时间
     */
    private Long connectUtc;

    /**
     * 记录时间
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
	 * @return the areaId
	 */
	public String getAreaId() {
		return areaId;
	}

	/**
	 * @param areaId the areaId to set
	 */
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}

	/**
	 * @return the linkType
	 */
	public Short getLinkType() {
		return linkType;
	}

	/**
	 * @param linkType the linkType to set
	 */
	public void setLinkType(Short linkType) {
		this.linkType = linkType;
	}

	/**
	 * @return the disconnectUtc
	 */
	public Long getDisconnectUtc() {
		return disconnectUtc;
	}

	/**
	 * @param disconnectUtc the disconnectUtc to set
	 */
	public void setDisconnectUtc(Long disconnectUtc) {
		this.disconnectUtc = disconnectUtc;
	}

	/**
	 * @return the connectUtc
	 */
	public Long getConnectUtc() {
		return connectUtc;
	}

	/**
	 * @param connectUtc the connectUtc to set
	 */
	public void setConnectUtc(Long connectUtc) {
		this.connectUtc = connectUtc;
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