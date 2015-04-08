package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 领料，退料明细表<br>
 * 描述： 领料，退料明细表<br>
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
public class TbMaintainMaterialDetailFetchRefund implements Serializable {

	/** */
	private static final long serialVersionUID = -8384709719088982113L;

	/** 维修用料信息id */
	private String materialId;

	/** 配件编码 */
	private String partsCode;

	/** 配件名称 */
	private String partsName;

	/** 单位（米，千克，个） */
	private String unit;

	/** 规格 */
	private String norms;

	/** 进口(是否进口;1|是；0|否) */
	private String whetherImported;

	/** 数量 */
	private BigDecimal quantity;

	/** 单价 */
	private BigDecimal unitPrice;

	/** 货款 */
	private BigDecimal sumMoney;

	/** 会员折扣 */
	private Integer memberDiscount;

	/** 会员价格 */
	private BigDecimal memberPrice;

	/** 会员金额 */
	private BigDecimal memberSumMoney;

	/** 库存数量 */
	private BigDecimal inventoryNum;

	/** 应领数量 */
	private BigDecimal pickingNum;

	/** 已领数量 */
	private BigDecimal receivedNum;

	/** 应退数量 */
	private BigDecimal retreatNum;

	/** 仓库 */
	private String warehouse;

	/** 货位 */
	private String goodsAllocation;

	/** 图号 */
	private String drawnNo;

	/** 适用车型 */
	private String vehicleModel;

	/** 车辆品牌 */
	private String vehicleBrand;

	/** 是否三包（1|是；0|否） */
	private String threeWarranty;

	/** 备注 */
	private String remarks;

	/** 维修关联id */
	private String maintainId;

	/** 信息状态（1|有效；0|删除） */
	private String enableFlag;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

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

	public String getUnit() {
		return unit;
	}

	public void setUnit(String unit) {
		this.unit = unit;
	}

	public String getNorms() {
		return norms;
	}

	public void setNorms(String norms) {
		this.norms = norms;
	}

	public String getWhetherImported() {
		return whetherImported;
	}

	public void setWhetherImported(String whetherImported) {
		this.whetherImported = whetherImported;
	}

	public BigDecimal getQuantity() {
		return quantity;
	}

	public void setQuantity(BigDecimal quantity) {
		this.quantity = quantity;
	}

	public BigDecimal getUnitPrice() {
		return unitPrice;
	}

	public void setUnitPrice(BigDecimal unitPrice) {
		this.unitPrice = unitPrice;
	}

	public BigDecimal getSumMoney() {
		return sumMoney;
	}

	public void setSumMoney(BigDecimal sumMoney) {
		this.sumMoney = sumMoney;
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

	public BigDecimal getInventoryNum() {
		return inventoryNum;
	}

	public void setInventoryNum(BigDecimal inventoryNum) {
		this.inventoryNum = inventoryNum;
	}

	public BigDecimal getPickingNum() {
		return pickingNum;
	}

	public void setPickingNum(BigDecimal pickingNum) {
		this.pickingNum = pickingNum;
	}

	public BigDecimal getReceivedNum() {
		return receivedNum;
	}

	public void setReceivedNum(BigDecimal receivedNum) {
		this.receivedNum = receivedNum;
	}

	public BigDecimal getRetreatNum() {
		return retreatNum;
	}

	public void setRetreatNum(BigDecimal retreatNum) {
		this.retreatNum = retreatNum;
	}

	public String getWarehouse() {
		return warehouse;
	}

	public void setWarehouse(String warehouse) {
		this.warehouse = warehouse;
	}

	public String getGoodsAllocation() {
		return goodsAllocation;
	}

	public void setGoodsAllocation(String goodsAllocation) {
		this.goodsAllocation = goodsAllocation;
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