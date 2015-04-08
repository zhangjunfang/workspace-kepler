package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：3G视频终端<br>
 * 描述：3G视频终端<br>
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
 * <td>2014年5月22日</td>
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
public class TbDvr implements Serializable {

	private static final long serialVersionUID = 4387654435237742098L;

	/** 3G视频终端ID，由SEQ_TB_DVR生成 */
	private String dvrId;

	/** 3G视频终端编号 */
	private String dvrNo;

	/** 所属企业ID(TB_ORGANIZATION.ENT_ID) */
	private String entId;

	/** 3G视频服务器ID（TB_DVRSER.DVRSER_ID） */
	private String dvrserId;

	/** 创建人 */
	private String createBy;

	/** 创建时间 */
	private String createTime;

	/** 修改人 */
	private String updateBy;

	/** 修改时间 */
	private String updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 视频通道个数 */
	private Long channelNum;

	/** 3GSIM卡号 */
	private String dvrSimNum;

	/** 所属分中心编码 */
	private String centerCode;

	// 附加信息
	/** 修改人姓名 */
	private String updateName;

	/** 创建人姓名 */
	private String creatName;

	/** 3G视频品牌，tb_general_code中SYS_DVR_MAKER_CODE */
	private String dvrMakerCode;

	/** 3G视频服务器ip地址 */
	private String dvrSerIp;

	/** 注册标记 -1:未注册 0:已注册 */
	private Integer regStatus;

	/** 所属企业名称 */
	private String entName;

	/** 3G视频品牌名称 */
	private String maker;

	/** 3G视频服务器端口 */
	private String dvrSerPort;

	/** 3G视频服务器名称 */
	private String dvrserName;

	public String getDvrId() {
		return dvrId;
	}

	public void setDvrId(String dvrId) {
		this.dvrId = dvrId;
	}

	public String getDvrNo() {
		return dvrNo;
	}

	public void setDvrNo(String dvrNo) {
		this.dvrNo = dvrNo;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getDvrserId() {
		return dvrserId;
	}

	public void setDvrserId(String dvrserId) {
		this.dvrserId = dvrserId;
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

	public Long getChannelNum() {
		return channelNum;
	}

	public void setChannelNum(Long channelNum) {
		this.channelNum = channelNum;
	}

	public String getDvrSimNum() {
		return dvrSimNum;
	}

	public void setDvrSimNum(String dvrSimNum) {
		this.dvrSimNum = dvrSimNum;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getUpdateName() {
		return updateName;
	}

	public void setUpdateName(String updateName) {
		this.updateName = updateName;
	}

	public String getCreatName() {
		return creatName;
	}

	public void setCreatName(String creatName) {
		this.creatName = creatName;
	}

	public String getDvrMakerCode() {
		return dvrMakerCode;
	}

	public void setDvrMakerCode(String dvrMakerCode) {
		this.dvrMakerCode = dvrMakerCode;
	}

	public String getDvrSerIp() {
		return dvrSerIp;
	}

	public void setDvrSerIp(String dvrSerIp) {
		this.dvrSerIp = dvrSerIp;
	}

	public Integer getRegStatus() {
		return regStatus;
	}

	public void setRegStatus(Integer regStatus) {
		this.regStatus = regStatus;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getMaker() {
		return maker;
	}

	public void setMaker(String maker) {
		this.maker = maker;
	}

	public String getDvrSerPort() {
		return dvrSerPort;
	}

	public void setDvrSerPort(String dvrSerPort) {
		this.dvrSerPort = dvrSerPort;
	}

	public String getDvrserName() {
		return dvrserName;
	}

	public void setDvrserName(String dvrserName) {
		this.dvrserName = dvrserName;
	}

}
