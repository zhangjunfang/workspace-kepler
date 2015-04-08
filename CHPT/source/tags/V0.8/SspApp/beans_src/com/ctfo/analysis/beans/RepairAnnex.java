package com.ctfo.analysis.beans;

import java.io.Serializable;

public class RepairAnnex implements Serializable{
    /**
	 * 
	 */
	private static final long serialVersionUID = 4616680149964878969L;

	/**
     * tb_maintain_accessory.accessory_id
     * 
     */
    private String accessoryId;

    /**
     * tb_maintain_accessory.accessory_name
     * 
     */
    private String accessoryName;

    /**
     * tb_maintain_accessory.accessory_type
     * 
     */
    private String accessoryType;

    /**
     * tb_maintain_accessory.accessory_details
     * 
     */
    private String accessoryDetails;

    /**
     * tb_maintain_accessory.remarks
     * 
     */
    private String remarks;

    /**
     * tb_maintain_accessory.maintain_id
     * 
     */
    private String maintainId;

    /**
     * tb_maintain_accessory.enable_flag
     * 
     */
    private String enableFlag;

    public String getAccessoryId() {
        return accessoryId;
    }

    public void setAccessoryId(String accessoryId) {
        this.accessoryId = accessoryId;
    }

    public String getAccessoryName() {
        return accessoryName;
    }

    public void setAccessoryName(String accessoryName) {
        this.accessoryName = accessoryName;
    }

    public String getAccessoryType() {
        return accessoryType;
    }

    public void setAccessoryType(String accessoryType) {
        this.accessoryType = accessoryType;
    }

    public String getAccessoryDetails() {
        return accessoryDetails;
    }

    public void setAccessoryDetails(String accessoryDetails) {
        this.accessoryDetails = accessoryDetails;
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
