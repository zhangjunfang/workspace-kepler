package com.ctfo.analysis.beans;

import java.io.Serializable;

public class RepairProject implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = -7693878080116560220L;

	/**
     * 维修项目信息id
     */
    private String itemId;

    /**
     * 维修项目编码
     */
    private String itemNo;

    /**
     * 维修项目类别
     */
    private String itemType;

    /**
     * 维修项目名称
     */
    private String itemName;

    /**
     * 工时类型
     */
    private String manHourType;

    /**
     * 工时数量
     */
    private double manHourQuantity;

    /**
     * 原工时单价
     */
    private double manHourNormUnitprice;

    /**
     * 会员折扣
     */
    private Integer memberDiscount;

    /**
     * 会员工时费
     */
    private double memberPrice;

    /**
     * 会员折扣金额
     */
    private double memberSumMoney;

    /**
     * 工时单价
     */
    private double manHourUnitprice;

    /**
     * 金额
     */
    private double sumMoney;

    /**
     * 货款
     */
    private double sumMoneyGoods;

    /**
     * 维修进度
     */
    private String repairProgress;

    /**
     * 维修工位
     */
    private String repairStation;

    /**
     * 三包（是否三包1|是；0|否）
     */
    private String threeWarranty;

    /**
     * 开工时间
     */
    private Long startWorkTime;

    /**
     * 实际完工时间
     */
    private Long completeWorkTime;

    /**
     * 停工时间
     */
    private Long shutDownTime;

    /**
     * 停工原因
     */
    private String shutDownReason;

    /**
     * 停工累计时长（单位：分钟）
     */
    private double shutDownDuration;

    /**
     * 继续开工时间
     */
    private Long continueTime;

    /**
     * tb_maintain_item.remarks
     * 备注
     */
    private String remarks;

    /**
     * 关联id（关联预约单，维修单）
     */
    private String maintainId;

    /**
     * 逻辑删除标志（1|有效；0|删除）
     */
    private String enableFlag;

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

    public double getManHourQuantity() {
        return manHourQuantity;
    }

    public void setManHourQuantity(double manHourQuantity) {
        this.manHourQuantity = manHourQuantity;
    }


    public double getManHourNormUnitprice() {
		return manHourNormUnitprice;
	}

	public void setManHourNormUnitprice(double manHourNormUnitprice) {
		this.manHourNormUnitprice = manHourNormUnitprice;
	}

	public Integer getMemberDiscount() {
        return memberDiscount;
    }

    public void setMemberDiscount(Integer memberDiscount) {
        this.memberDiscount = memberDiscount;
    }

    public double getMemberPrice() {
        return memberPrice;
    }

    public void setMemberPrice(double memberPrice) {
        this.memberPrice = memberPrice;
    }

    public double getMemberSumMoney() {
        return memberSumMoney;
    }

    public void setMemberSumMoney(double memberSumMoney) {
        this.memberSumMoney = memberSumMoney;
    }

    public double getManHourUnitprice() {
        return manHourUnitprice;
    }

    public void setManHourUnitprice(double manHourUnitprice) {
        this.manHourUnitprice = manHourUnitprice;
    }

    public double getSumMoney() {
        return sumMoney;
    }

    public void setSumMoney(double sumMoney) {
        this.sumMoney = sumMoney;
    }

    public double getSumMoneyGoods() {
        return sumMoneyGoods;
    }

    public void setSumMoneyGoods(double sumMoneyGoods) {
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

    public double getShutDownDuration() {
        return shutDownDuration;
    }

    public void setShutDownDuration(double shutDownDuration) {
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
	
}
