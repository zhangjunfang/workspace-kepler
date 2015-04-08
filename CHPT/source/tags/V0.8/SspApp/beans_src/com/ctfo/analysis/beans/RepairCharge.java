package com.ctfo.analysis.beans;

import java.io.Serializable;

public class RepairCharge implements Serializable{
	   /**
	 * 
	 */
	private static final long serialVersionUID = -7022893874997188976L;

	/**
     * tb_maintain_toll.toll_id
     * 
     */
    private String tollId;

    /**
     * tb_maintain_toll.cost_types
     * 
     */
    private String costTypes;

    /**
     * tb_maintain_toll.sum_money
     * 
     */
    private double sumMoney;

    /**
     * tb_maintain_toll.remarks
     * 
     */
    private String remarks;

    /**
     * tb_maintain_toll.maintain_id
     * 
     */
    private String maintainId;

    /**
     * tb_maintain_toll.enable_flag
     * 
     */
    private String enableFlag;

    public String getTollId() {
        return tollId;
    }

    public void setTollId(String tollId) {
        this.tollId = tollId;
    }

    public String getCostTypes() {
        return costTypes;
    }

    public void setCostTypes(String costTypes) {
        this.costTypes = costTypes;
    }

    public double getSumMoney() {
        return sumMoney;
    }

    public void setSumMoney(double sumMoney) {
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

    public String getEnableFlag() {
        return enableFlag;
    }

    public void setEnableFlag(String enableFlag) {
        this.enableFlag = enableFlag;
    }
}
