package com.ctfo.analysis.beans;

import java.io.Serializable;

public class RepairMaterials implements Serializable{
    /**
	 * 
	 */
	private static final long serialVersionUID = 3328021213857332704L;

	/**
     * tb_maintain_material_detail.material_id
     * 维修用料信息id
     */
    private String materialId;

    /**
     * tb_maintain_material_detail.parts_code
     * 配件编码
     */
    private String partsCode;

    /**
     * tb_maintain_material_detail.parts_name
     * 配件名称
     */
    private String partsName;

    /**
     * tb_maintain_material_detail.parts_type
     * 配件类型
     */
    private String partsType;

    /**
     * tb_maintain_material_detail.norms
     * 规格
     */
    private String norms;

    /**
     * tb_maintain_material_detail.unit
     * 单位（米，千克，个）
     */
    private String unit;

    /**
     * tb_maintain_material_detail.whether_imported
     * 进口(是否进口;1|是；0|否)
     */
    private String whetherImported;

    /**
     * tb_maintain_material_detail.quantity
     * 数量
     */
    private double quantity;

    /**
     * tb_maintain_material_detail.unit_price
     * 单价
     */
    private double unitPrice;

    /**
     * tb_maintain_material_detail.sum_money
     * 货款
     */
    private double sumMoney;

    /**
     * tb_maintain_material_detail.member_discount
     * 会员折扣
     */
    private Integer memberDiscount;

    /**
     * tb_maintain_material_detail.member_price
     * 会员价格
     */
    private double memberPrice;

    /**
     * tb_maintain_material_detail.member_sum_money
     * 会员金额
     */
    private double memberSumMoney;

    /**
     * tb_maintain_material_detail.drawn_no
     * 图号
     */
    private String drawnNo;

    /**
     * tb_maintain_material_detail.vehicle_model
     * 适用车型
     */
    private String vehicleModel;

    /**
     * tb_maintain_material_detail.vehicle_brand
     * 车辆品牌
     */
    private String vehicleBrand;

    /**
     * tb_maintain_material_detail.three_warranty
     * 是否三包（1|是；0|否）
     */
    private String threeWarranty;

    /**
     * tb_maintain_material_detail.remarks
     * 备注
     */
    private String remarks;

    /**
     * tb_maintain_material_detail.maintain_id
     * 维修关联id
     */
    private String maintainId;

    /**
     * tb_maintain_material_detail.enable_flag
     * 信息状态（1|有效；0|删除）
     */
    private String enableFlag;

    public String getMaterialId() {
        return materialId;
    }

    public void setMaterialId(String materialId) {
        this.materialId = materialId;
    }

    public String getPartsCode() {
        return partsCode;
    }

    public void setPartsCode(String partsCode) {
        this.partsCode = partsCode;
    }

    public String getPartsName() {
        return partsName;
    }

    public void setPartsName(String partsName) {
        this.partsName = partsName;
    }

    public String getPartsType() {
        return partsType;
    }

    public void setPartsType(String partsType) {
        this.partsType = partsType;
    }

    public String getNorms() {
        return norms;
    }

    public void setNorms(String norms) {
        this.norms = norms;
    }

    public String getUnit() {
        return unit;
    }

    public void setUnit(String unit) {
        this.unit = unit;
    }

    public String getWhetherImported() {
        return whetherImported;
    }

    public void setWhetherImported(String whetherImported) {
        this.whetherImported = whetherImported;
    }

    public double getQuantity() {
        return quantity;
    }

    public void setQuantity(double quantity) {
        this.quantity = quantity;
    }

    public double getUnitPrice() {
        return unitPrice;
    }

    public void setUnitPrice(double unitPrice) {
        this.unitPrice = unitPrice;
    }

    public double getSumMoney() {
        return sumMoney;
    }

    public void setSumMoney(double sumMoney) {
        this.sumMoney = sumMoney;
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

    public String getDrawnNo() {
        return drawnNo;
    }

    public void setDrawnNo(String drawnNo) {
        this.drawnNo = drawnNo;
    }

    public String getVehicleModel() {
        return vehicleModel;
    }

    public void setVehicleModel(String vehicleModel) {
        this.vehicleModel = vehicleModel;
    }

    public String getVehicleBrand() {
        return vehicleBrand;
    }

    public void setVehicleBrand(String vehicleBrand) {
        this.vehicleBrand = vehicleBrand;
    }

    public String getThreeWarranty() {
        return threeWarranty;
    }

    public void setThreeWarranty(String threeWarranty) {
        this.threeWarranty = threeWarranty;
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
