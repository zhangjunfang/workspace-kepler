package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 旧件收/发明细表<br>
 * 描述： 旧件收/发明细表<br>
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
public class TbMaintainOldpartMaterialDetail implements Serializable {

	/** */
	private static final long serialVersionUID = -1964910069721847362L;

	/** 维修用料信息id */
	private String partsId;

	/** 配件编码 */
	private String partsCode;

	/** 配件名称 */
	private String partsName;

	/** 业务单号 */
	private String ordersNo;

	/** 车牌号 */
	private String vehicleNo;

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

	/** 库存数量 */
	private BigDecimal inventoryNum;

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

	/** 处理方式 */
	private String processMode;

	public String getPartsId() {
		return partsId;
	}

	public void setPartsId(String partsId) {
		this.partsId = partsId;
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

	public String getOrdersNo() {
		return ordersNo;
	}

	public void setOrdersNo(String ordersNo) {
		this.ordersNo = ordersNo;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
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

	public BigDecimal getInventoryNum() {
		return inventoryNum;
	}

	public void setInventoryNum(BigDecimal inventoryNum) {
		this.inventoryNum = inventoryNum;
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

	public String getProcessMode() {
		return processMode;
	}

	public void setProcessMode(String processMode) {
		this.processMode = processMode;
	}

}