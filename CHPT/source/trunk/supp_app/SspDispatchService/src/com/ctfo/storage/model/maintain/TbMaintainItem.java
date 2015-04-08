package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 维修项目表<br>
 * 描述： 维修项目表<br>
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
 * <td>2014-10-31</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class TbMaintainItem implements Serializable {

	/** */
	private static final long serialVersionUID = 186617273668658687L;

	/** 维修项目信息id */
	private String itemId;

	/** 维修项目编码 */
	private String itemNo;

	/** 维修项目类别 */
	private String itemType;

	/** 维修项目名称 */
	private String itemName;

	/** 工时类型 */
	private String manHourType;

	/** 工时数量 */
	private BigDecimal manHourQuantity;

	/** 原工时单价 */
	private BigDecimal manHourNormUnitprice;

	/** 会员折扣 */
	private Integer memberDiscount;

	/** 会员工时费 */
	private BigDecimal memberPrice;

	/** 会员折扣金额 */
	private BigDecimal memberSumMoney;

	/** 工时单价 */
	private BigDecimal manHourUnitprice;

	/** 金额 */
	private BigDecimal sumMoney;

	/** 货款 */
	private BigDecimal sumMoneyGoods;

	/** 维修进度 */
	private String repairProgress;

	/** 维修工位 */
	private String repairStation;

	/** 三包（是否三包1|是；0|否） */
	private String threeWarranty;

	/** 开工时间 */
	private Long startWorkTime;

	/** 实际完工时间 */
	private Long completeWorkTime;

	/** 停工时间 */
	private Long shutDownTime;

	/** 停工原因 */
	private String shutDownReason;

	/** 停工累计时长（单位：分钟） */
	private BigDecimal shutDownDuration;

	/** 继续开工时间 */
	private Long continueTime;

	/** 备注 */
	private String remarks;

	/** 关联id（关联预约单，维修单） */
	private String maintainId;

	/** 逻辑删除标志（1|有效；0|删除） */
	private String enableFlag;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** */
	private String whoursId;

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

	public String getItemId() {
		return itemId;
	}

	public void setItemId(String itemId) {
		this.itemId = itemId;
	}

	public String getItemNo() {
		return itemNo;
	}

	public void setItemNo(String itemNo) {
		this.itemNo = itemNo;
	}

	public String getItemType() {
		return itemType;
	}

	public void setItemType(String itemType) {
		this.itemType = itemType;
	}

	public String getItemName() {
		return itemName;
	}

	public void setItemName(String itemName) {
		this.itemName = itemName;
	}

	public String getManHourType() {
		return manHourType;
	}

	public void setManHourType(String manHourType) {
		this.manHourType = manHourType;
	}

	public BigDecimal getManHourQuantity() {
		return manHourQuantity;
	}

	public void setManHourQuantity(BigDecimal manHourQuantity) {
		this.manHourQuantity = manHourQuantity;
	}

	public BigDecimal getManHourNormUnitprice() {
		return manHourNormUnitprice;
	}

	public void setManHourNormUnitprice(BigDecimal manHourNormUnitprice) {
		this.manHourNormUnitprice = manHourNormUnitprice;
	}

	public Integer getMemberDiscount() {
		return memberDiscount;
	}

	public void setMemberDiscount(Integer memberDiscount) {
		this.memberDiscount = memberDiscount;
	}

	public BigDecimal getMemberPrice() {
		return memberPrice;
	}

	public void setMemberPrice(BigDecimal memberPrice) {
		this.memberPrice = memberPrice;
	}

	public BigDecimal getMemberSumMoney() {
		return memberSumMoney;
	}

	public void setMemberSumMoney(BigDecimal memberSumMoney) {
		this.memberSumMoney = memberSumMoney;
	}

	public BigDecimal getManHourUnitprice() {
		return manHourUnitprice;
	}

	public void setManHourUnitprice(BigDecimal manHourUnitprice) {
		this.manHourUnitprice = manHourUnitprice;
	}

	public BigDecimal getSumMoney() {
		return sumMoney;
	}

	public void setSumMoney(BigDecimal sumMoney) {
		this.sumMoney = sumMoney;
	}

	public BigDecimal getSumMoneyGoods() {
		return sumMoneyGoods;
	}

	public void setSumMoneyGoods(BigDecimal sumMoneyGoods) {
		this.sumMoneyGoods = sumMoneyGoods;
	}

	public String getRepairProgress() {
		return repairProgress;
	}

	public void setRepairProgress(String repairProgress) {
		this.repairProgress = repairProgress;
	}

	public String getRepairStation() {
		return repairStation;
	}

	public void setRepairStation(String repairStation) {
		this.repairStation = repairStation;
	}

	public String getThreeWarranty() {
		return threeWarranty;
	}

	public void setThreeWarranty(String threeWarranty) {
		this.threeWarranty = threeWarranty;
	}

	public Long getStartWorkTime() {
		return startWorkTime;
	}

	public void setStartWorkTime(Long startWorkTime) {
		this.startWorkTime = startWorkTime;
	}

	public Long getCompleteWorkTime() {
		return completeWorkTime;
	}

	public void setCompleteWorkTime(Long completeWorkTime) {
		this.completeWorkTime = completeWorkTime;
	}

	public Long getShutDownTime() {
		return shutDownTime;
	}

	public void setShutDownTime(Long shutDownTime) {
		this.shutDownTime = shutDownTime;
	}

	public String getShutDownReason() {
		return shutDownReason;
	}

	public void setShutDownReason(String shutDownReason) {
		this.shutDownReason = shutDownReason;
	}

	public BigDecimal getShutDownDuration() {
		return shutDownDuration;
	}

	public void setShutDownDuration(BigDecimal shutDownDuration) {
		this.shutDownDuration = shutDownDuration;
	}

	public Long getContinueTime() {
		return continueTime;
	}

	public void setContinueTime(Long continueTime) {
		this.continueTime = continueTime;
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

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getWhoursId() {
		return whoursId;
	}

	public void setWhoursId(String whoursId) {
		this.whoursId = whoursId;
	}

}