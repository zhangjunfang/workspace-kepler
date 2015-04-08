package com.ctfo.storage.dispatch.model;


import java.io.Serializable;


@SuppressWarnings("serial")
public class TbDvrSer extends BaseModel implements Serializable {
	/**
	 * 
	 */
	/** 3G视频服务器ID，由SEQ_TB_DVRSER生成 */
	private String dvrSerId ;
	/** 3G视频服务器名称 */
	private String dvrSerName = "";
	/** 3G视频服务器ip地址 */
	private String dvrSerIp = "";
	/** 3G视频服务器端口 */
	private String dvrSerPort = "";
	/** 3G视频服务器用户名 */
	private String dvrSerUsername = "";
	/** 3G视频服务器密码 */
	private String dvrSerPassword = "";
	/** 3G视频服务器所属省 */
	private String dvrSerProvince = "";
	/** 3G视频服务器所属市 */
	private String dvrSerCity = "";
	/** 3G视频品牌 */
	private String dvrSerMakerCode = "";
	/** 创建人 */
	private String createBy ;
	private String createByName = "";
	/** 创建时间 */
	private String createTime ;
	/** 最后编辑人 */
	private String updateBy ;
	private String updateByName = "";
	private String regIp = "";
	private String regPort = "";
	private String channelNum ;
	
	public String getRegIp() {
		return regIp;
	}
	public void setRegIp(String regIp) {
		this.regIp = regIp;
	}
	public String getRegPort() {
		return regPort;
	}
	public void setRegPort(String regPort) {
		this.regPort = regPort;
	}
	public String getCreateByName() {
		return createByName;
	}
	public void setCreateByName(String createByName) {
		this.createByName = createByName;
	}
	public String getUpdateByName() {
		return updateByName;
	}
	public void setUpdateByName(String updateByName) {
		this.updateByName = updateByName;
	}
	/** 最后编辑时间 */
	private String updateTime ;
	/** 有效标记 1:有效 0:无效  默认为1 */
	private String enableFlag = "";
	/** 序号 */
	private String sqlid;
	public String getSqlid() {
		return sqlid;
	}
	public void setSqlid(String sqlid) {
		this.sqlid = sqlid;
	}
	public String getDvrSerId() {
		return dvrSerId;
	}
	public void setDvrSerId(String dvrSerId) {
		this.dvrSerId = dvrSerId;
	}
	public String getDvrSerName() {
		return dvrSerName;
	}
	public void setDvrSerName(String dvrSerName) {
		this.dvrSerName = dvrSerName;
	}
	public String getDvrSerIp() {
		return dvrSerIp;
	}
	public void setDvrSerIp(String dvrSerIp) {
		this.dvrSerIp = dvrSerIp;
	}
	public String getDvrSerPort() {
		return dvrSerPort;
	}
	public void setDvrSerPort(String dvrSerPort) {
		this.dvrSerPort = dvrSerPort;
	}
	public String getDvrSerUsername() {
		return dvrSerUsername;
	}
	public void setDvrSerUsername(String dvrSerUsername) {
		this.dvrSerUsername = dvrSerUsername;
	}
	public String getDvrSerPassword() {
		return dvrSerPassword;
	}
	public void setDvrSerPassword(String dvrSerPassword) {
		this.dvrSerPassword = dvrSerPassword;
	}
	public String getDvrSerProvince() {
		return dvrSerProvince;
	}
	public void setDvrSerProvince(String dvrSerProvince) {
		this.dvrSerProvince = dvrSerProvince;
	}
	public String getDvrSerCity() {
		return dvrSerCity;
	}
	public void setDvrSerCity(String dvrSerCity) {
		this.dvrSerCity = dvrSerCity;
	}
	public String getDvrSerMakerCode() {
		return dvrSerMakerCode;
	}
	public void setDvrSerMakerCode(String dvrSerMakerCode) {
		this.dvrSerMakerCode = dvrSerMakerCode;
	}
	public String getCreateBy() {
		return createBy;
	}
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}
	public String getCreateTime() {
		return createTime;
	}
	public void setCreateTime(String createTime) {
		this.createTime = createTime;
	}
	public String getUpdateBy() {
		return updateBy;
	}
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	public String getUpdateTime() {
		return updateTime;
	}
	public void setUpdateTime(String updateTime) {
		this.updateTime = updateTime;
	}
	public String getEnableFlag() {
		return enableFlag;
	}
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	/**
	 * 获取channelNum的值
	 * @return channelNum  
	 */
	public String getChannelNum() {
		return channelNum;
	}
	/**
	 * 设置channelNum的值
	 * @param channelNum
	 */
	public void setChannelNum(String channelNum) {
		this.channelNum = channelNum;
	}
	
	
}