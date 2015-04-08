package com.ctfo.informationser.monitoring.beans;

import com.ctfo.local.bean.ETB_Base;

public class ThVehicleChecked extends ETB_Base{
    /**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 1484083040422993305L;

	/**
     * 主键标识
     */
    private String pid;

    /**
     * 手机号
     */
    private String vid;

    /**
     * 鉴权码
     */
    private String akey;

    /**
     * 原始命令
     */
    private String command;

    /**
     * 鉴权结果-1失败 0 成功
     */
    private String result;

    /**
     *SEQ标识
     */
    private String seq;

    /**
     * 记录时间
     */
    private Long utc;

    /**
     * 响应时间
     */
    private Long resultutc;

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
	 * @return the akey
	 */
	public String getAkey() {
		return akey;
	}

	/**
	 * @param akey the akey to set
	 */
	public void setAkey(String akey) {
		this.akey = akey;
	}

	/**
	 * @return the command
	 */
	public String getCommand() {
		return command;
	}

	/**
	 * @param command the command to set
	 */
	public void setCommand(String command) {
		this.command = command;
	}

	/**
	 * @return the result
	 */
	public String getResult() {
		return result;
	}

	/**
	 * @param result the result to set
	 */
	public void setResult(String result) {
		this.result = result;
	}

	/**
	 * @return the seq
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * @param seq the seq to set
	 */
	public void setSeq(String seq) {
		this.seq = seq;
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
	 * @return the resultutc
	 */
	public Long getResultutc() {
		return resultutc;
	}

	/**
	 * @param resultutc the resultutc to set
	 */
	public void setResultutc(Long resultutc) {
		this.resultutc = resultutc;
	}

}