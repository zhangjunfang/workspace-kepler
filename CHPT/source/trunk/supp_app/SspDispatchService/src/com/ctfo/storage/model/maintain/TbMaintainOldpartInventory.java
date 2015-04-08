package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 旧件库存表<br>
 * 描述： 旧件库存表<br>
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
public class TbMaintainOldpartInventory implements Serializable {

	/** */
	private static final long serialVersionUID = 2351985713147485962L;

	/** 库存信息id */
	private String inventoryId;

	/** 所属公司 */
	private String orgId;

	/** 配件编码 */
	private String partsCode;

	/** 配件名称 */
	private String partsName;

	/** 配件类型id，关联字典表 */
	private String partsTypeId;

	/** 单位（米，千克，个） */
	private String unit;

	/** 规格 */
	private String norms;

	/** 图号 */
	private String drawnNo;

	/** 品牌 */
	private String trademark;

	/** 适用车型 */
	private String vehicleModel;

	/** 产地 */
	private String originPlace;

	/** 重量 */
	private BigDecimal weight;

	/** 金额 */
	private BigDecimal sumMoney;

	/** 条码 */
	private String barCode;

	/** 账面库存数量 */
	private BigDecimal bookInventoryNum;

	/** 库存数量 */
	private BigDecimal inventoryNum;

	/** 备注 */
	private String remarks;

	/** 平均价格 */
	private BigDecimal averagePrice;

	/** 信息状态（1|有效；0|删除） */
	private String enableFlag;

	/** 创建时间 */
	private Long createTime;

	/** 修改时间 */
	private Long updateTime;

	/** 创建人（制单人） */
	private String createBy;

	/** 最后修改人id */
	private String updateBy;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getInventoryId() {
		return inventoryId;
	}

	public void setInventoryId(String inventoryId) {
		this.inventoryId = inventoryId;
	}

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
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

	public String getPartsTypeId() {
		return partsTypeId;
	}

	public void setPartsTypeId(String partsTypeId) {
		this.partsTypeId = partsTypeId;
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

	public String getDrawnNo() {
		return drawnNo;
	}

	public void setDrawnNo(String drawnNo) {
		this.drawnNo = drawnNo;
	}

	public String getTrademark() {
		return trademark;
	}

	public void setTrademark(String trademark) {
		this.trademark = trademark;
	}

	public String getVehicleModel() {
		return vehicleModel;
	}

	public void setVehicleModel(String vehicleModel) {
		this.vehicleModel = vehicleModel;
	}

	public String getOriginPlace() {
		return originPlace;
	}

	public void setOriginPlace(String originPlace) {
		this.originPlace = originPlace;
	}

	public BigDecimal getWeight() {
		return weight;
	}

	public void setWeight(BigDecimal weight) {
		this.weight = weight;
	}

	public BigDecimal getSumMoney() {
		return sumMoney;
	}

	public void setSumMoney(BigDecimal sumMoney) {
		this.sumMoney = sumMoney;
	}

	public String getBarCode() {
		return barCode;
	}

	public void setBarCode(String barCode) {
		this.barCode = barCode;
	}

	public BigDecimal getBookInventoryNum() {
		return bookInventoryNum;
	}

	public void setBookInventoryNum(BigDecimal bookInventoryNum) {
		this.bookInventoryNum = bookInventoryNum;
	}

	public BigDecimal getInventoryNum() {
		return inventoryNum;
	}

	public void setInventoryNum(BigDecimal inventoryNum) {
		this.inventoryNum = inventoryNum;
	}

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
	}

	public BigDecimal getAveragePrice() {
		return averagePrice;
	}

	public void setAveragePrice(BigDecimal averagePrice) {
		this.averagePrice = averagePrice;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
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