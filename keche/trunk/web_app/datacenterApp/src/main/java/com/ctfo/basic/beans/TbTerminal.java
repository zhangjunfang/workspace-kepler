package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：终端<br>
 * 描述：终端<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月28日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbTerminal implements Serializable {

	private static final long serialVersionUID = 4128236884652198740L;

	/** 设备ID，由序列SEQ_TERMINAL_ID生成 */
	private String tid;

	/** 硬件标识号 */
	private String tmac;

	/** 鉴权码 */
	private String authCode;

	/** 设备厂家 */
	private String oemCode;

	/** 硬件地址 */
	private String terHardver;

	/** 终端软件版本 */
	private String terSoftver;

	/** 终端通讯码 */
	private String communicateId;

	/** 终端视频ID */
	private String videoId;

	/** 是否车载终端1：车载终端0：手持终端 */
	private String terUtype;

	/** 生产日期 */
	private long terMdate;

	/** 安装日期 */
	private long terEdate;

	/** 终端安装人 */
	private String terEperson;

	/** 价格 */
	private long terPrice;

	/** 安装费用 */
	private long terEcost;

	/** 备注 */
	private String terMem;

	/** 创建人 */
	private String createBy;

	/** 创建时间 */
	private long createTime;

	/** 修改人 */
	private String updateBy;

	/** 修改时间 */
	private long updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 所属实体ID */
	private String entId;

	/** 终端状态 1：空闲 2：已绑定3:吊销 */
	private long terState;

	/** 终端通讯协议编码（关联终端通讯协议表TB_TERMINAL_PROTOCOL） */
	private String tprotocolId;

	/** 终端配置方案ID */
	private long configId;

	/** 终端型号(参见终端型号码表) */
	private String tmodelCode;

	/** -1未注册，0已注册 */
	private long regStatus;

	/** 硬件版本号 */
	private String hardwareVersion;

	/** 固件版本 */
	private String firmwareVersion;

	/** 交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过) */
	private long deliveryStatus;

	/** 所属分中心编码 */
	private String centerCode;

	/** 国际版本号 */
	private String standardVersion;

	// 附加字段
	/** 终端通讯协议名称 */
	private String tprotocolName;

	/** 终端型号名称 */
	private String tmodelName;

	/** 厂家名称 */
	private String oemName;

	/** 创建人姓名 */
	private String createName;

	/** 编辑人姓名 */
	private String updateName;

	/** 所属车队名称 */
	private String entName;

	/** 所属企业名称 **/
	private String parentEntName;

	public String getTmodelName() {
		return tmodelName;
	}

	public void setTmodelName(String tmodelName) {
		this.tmodelName = tmodelName;
	}

	public String getTprotocolName() {
		return tprotocolName;
	}

	public void setTprotocolName(String tprotocolName) {
		this.tprotocolName = tprotocolName;
	}

	public String gettModelName() {
		return tmodelName;
	}

	public void settModelName(String tModelName) {
		this.tmodelName = tModelName;
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

	public String getTerUtype() {
		return terUtype;
	}

	public void setTerUtype(String terUtype) {
		this.terUtype = terUtype;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public long getTerMdate() {
		return terMdate;
	}

	public void setTerMdate(long terMdate) {
		this.terMdate = terMdate;
	}

	public long getTerEdate() {
		return terEdate;
	}

	public void setTerEdate(long terEdate) {
		this.terEdate = terEdate;
	}

	public String getTerEperson() {
		return terEperson;
	}

	public void setTerEperson(String terEperson) {
		this.terEperson = terEperson;
	}

	public long getTerPrice() {
		return terPrice;
	}

	public void setTerPrice(long terPrice) {
		this.terPrice = terPrice;
	}

	public long getTerEcost() {
		return terEcost;
	}

	public void setTerEcost(long terEcost) {
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

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}

	public long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(long updateTime) {
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

	public long getTerState() {
		return terState;
	}

	public void setTerState(long terState) {
		this.terState = terState;
	}

	public String getTprotocolId() {
		return tprotocolId;
	}

	public void setTprotocolId(String tprotocolId) {
		this.tprotocolId = tprotocolId;
	}

	public long getConfigId() {
		return configId;
	}

	public void setConfigId(long configId) {
		this.configId = configId;
	}

	public String getTmodelCode() {
		return tmodelCode;
	}

	public void setTmodelCode(String tmodelCode) {
		this.tmodelCode = tmodelCode;
	}

	public long getRegStatus() {
		return regStatus;
	}

	public void setRegStatus(long regStatus) {
		this.regStatus = regStatus;
	}

	public String getHardwareVersion() {
		return hardwareVersion;
	}

	public void setHardwareVersion(String hardwareVersion) {
		this.hardwareVersion = hardwareVersion;
	}

	public String getFirmwareVersion() {
		return firmwareVersion;
	}

	public void setFirmwareVersion(String firmwareVersion) {
		this.firmwareVersion = firmwareVersion;
	}

	public long getDeliveryStatus() {
		return deliveryStatus;
	}

	public void setDeliveryStatus(long deliveryStatus) {
		this.deliveryStatus = deliveryStatus;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getOemName() {
		return oemName;
	}

	public void setOemName(String oemName) {
		this.oemName = oemName;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

	public String getUpdateName() {
		return updateName;
	}

	public void setUpdateName(String updateName) {
		this.updateName = updateName;
	}

	public String getParentEntName() {
		return parentEntName;
	}

	public void setParentEntName(String parentEntName) {
		this.parentEntName = parentEntName;
	}

	public String getStandardVersion() {
		return standardVersion;
	}

	public void setStandardVersion(String standardVersion) {
		this.standardVersion = standardVersion;
	}

}
