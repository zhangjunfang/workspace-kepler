package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：3G视频服务器<br>
 * 描述：3G视频服务器<br>
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
 * <td>2014年5月21日</td>
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
public class TbDvrser implements Serializable {

	/** */
	private static final long serialVersionUID = -4465808705724327563L;

	/** 3G视频服务器ID，由SEQ_TB_DVRSER生成 */
	private String dvrserId;

	/** 3G视频服务器名称 */
	private String dvrserName;

	/** 3G视频服务器ip地址 */
	private String dvrserIp;

	/** 3G视频服务器端口 */
	private String dvrserPort;

	/** 3G视频服务器密码 */
	private String dvrserPassword;

	/** 3G视频服务器所属省 */
	private String dvrserProvince;

	/** 3G视频服务器所属市 */
	private String dvrserCity;

	/** 3G视频品牌，tb_general_code中SYS_DVR_MAKER_CODE */
	private String dvrMakerCode;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑时间 */
	private Long updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 视频通道个数 */
	private Long channelNum;

	/** 3G视频注册服务器ip地址 */
	private String regIp;

	/** 3G视频注册服务器端口 */
	private String regPort;

	/** 所属分中心编码 */
	private String centerCode;

	/** 3G视频服务器用户名 */
	private String dvrserUsername;

	/** 创建人 */
	private String createBy;

	/** 最后编辑人 */
	private String updateBy;

	// 附加信息
	/** 品牌名 */
	private String maker;

	/** 创建人 */
	private String createName;

	/** 最后编辑人 */
	private String updateName;

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

	public String getMaker() {
		return maker;
	}

	public void setMaker(String maker) {
		this.maker = maker;
	}

	public String getDvrserId() {
		return dvrserId;
	}

	public void setDvrserId(String dvrserId) {
		this.dvrserId = dvrserId;
	}

	public String getDvrserName() {
		return dvrserName;
	}

	public void setDvrserName(String dvrserName) {
		this.dvrserName = dvrserName;
	}

	public String getDvrserIp() {
		return dvrserIp;
	}

	public void setDvrserIp(String dvrserIp) {
		this.dvrserIp = dvrserIp;
	}

	public String getDvrserPort() {
		return dvrserPort;
	}

	public void setDvrserPort(String dvrserPort) {
		this.dvrserPort = dvrserPort;
	}

	public String getDvrserUsername() {
		return dvrserUsername;
	}

	public void setDvrserUsername(String dvrserUsername) {
		this.dvrserUsername = dvrserUsername;
	}

	public String getDvrserPassword() {
		return dvrserPassword;
	}

	public void setDvrserPassword(String dvrserPassword) {
		this.dvrserPassword = dvrserPassword;
	}

	public String getDvrserProvince() {
		return dvrserProvince;
	}

	public void setDvrserProvince(String dvrserProvince) {
		this.dvrserProvince = dvrserProvince;
	}

	public String getDvrserCity() {
		return dvrserCity;
	}

	public void setDvrserCity(String dvrserCity) {
		this.dvrserCity = dvrserCity;
	}

	public String getDvrMakerCode() {
		return dvrMakerCode;
	}

	public void setDvrMakerCode(String dvrMakerCode) {
		this.dvrMakerCode = dvrMakerCode;
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

	public Long getChannelNum() {
		return channelNum;
	}

	public void setChannelNum(Long channelNum) {
		this.channelNum = channelNum;
	}

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

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

}
