package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 救援人员表<br>
 * 描述： 救援人员表<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2015-1-7</td>
 * <td>Administrator</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author Administrator
 * @since JDK1.6
 */
public class TbMaintainRescueWorker implements Serializable {

	/** */
	private static final long serialVersionUID = -8088377541894726802L;

	/** 信息id */
	private String rescuerId;

	/** 人员编码 */
	private String rescuerNo;

	/** 人员名称 */
	private String rescuerName;

	/** 部门 */
	private String orgName;

	/** 班组 */
	private String teamName;

	/** 分配工时 */
	private BigDecimal manHour;

	/** 分配金额 */
	private BigDecimal sumMoney;

	/** 备注 */
	private String remarks;

	/** 关联id */
	private String maintainId;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getRescuerId() {
		return rescuerId;
	}

	public void setRescuerId(String rescuerId) {
		this.rescuerId = rescuerId;
	}

	public String getRescuerNo() {
		return rescuerNo;
	}

	public void setRescuerNo(String rescuerNo) {
		this.rescuerNo = rescuerNo;
	}

	public String getRescuerName() {
		return rescuerName;
	}

	public void setRescuerName(String rescuerName) {
		this.rescuerName = rescuerName;
	}

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
	}

	public String getTeamName() {
		return teamName;
	}

	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	public BigDecimal getManHour() {
		return manHour;
	}

	public void setManHour(BigDecimal manHour) {
		this.manHour = manHour;
	}

	public BigDecimal getSumMoney() {
		return sumMoney;
	}

	public void setSumMoney(BigDecimal sumMoney) {
		this.sumMoney = sumMoney;
	}

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
	}

	public String getMaintainId() {
		return maintainId;
	}

	public void setMaintainId(String maintainId) {
		this.maintainId = maintainId;
	}

	public String getSerStationId() {
		return serStationId;
	}

	public void setSerStationId(String serStationId) {
		this.serStationId = serStationId;
	}

	public String getSetBookId() {
		return setBookId;
	}

	public void setSetBookId(String setBookId) {
		this.setBookId = setBookId;
	}
}