package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-11-5</td>
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
public class TbParts implements Serializable {

	/** */
	private static final long serialVersionUID = 8643663565365050125L;

	/** 配件档案ID */
	private String partsId;

	/** 数据来源 */
	private String dataSource;

	/** 配件名称 */
	private String partsName;

	/** 配件品牌 */
	private String partsBrand;

	/** 配件条码 */
	private String partsBarcode;

	/** 服务站配件编码 */
	private String serPartsCode;

	/** 车厂配件编码 */
	private String carPartsCode;

	/** 速查码 */
	private String quickCode;

	/** 规格型号 */
	private String model;

	/** 图号 */
	private String drawingNum;

	/** 产地 */
	private String placeOrigin;

	/** 默认单位 */
	private String defaultUnit;

	/** 销售单位编码-sap的数据 */
	private String salesUnitCode;

	/** 销售单位名称-sap的数据 */
	private String salesUnitName;

	/** 基本单位编码--宇通 */
	private String baseUnitCode;

	/** 基本单位名称--宇通 */
	private String baseUnitName;

	/** 销售单位数量--宇通 */
	private Integer salesUnitQuantity;

	/** 基本单位数量--宇通 */
	private Integer baseUnitQuantity;

	/** 重量 */
	private String weight;

	/** 3A价格 */
	private BigDecimal price3a;

	/** 2A价格 */
	private BigDecimal price2a;

	/** 零售价 */
	private BigDecimal retail;

	/** 参考进价 */
	private BigDecimal refInPrice;

	/** 参考售价 */
	private BigDecimal refOutPrice;

	/** 最高进价 */
	private BigDecimal highestInPrice;

	/** 最低售价 */
	private BigDecimal highestOutPrice;

	/** 是否通用件 0不通用，1通用 */
	private String isGeneral;

	/** 替代关系(单向、双向) */
	private String replaced;

	/** 是否可被替代，0不可 1可 */
	private String isReplac;

	/** 是否进口 */
	private String isImport;

	/** 供应商1，关联供应商档案表 */
	private String supplierOne;

	/** 供应商2，关联供应商档案表 */
	private String supplierTwo;

	/** 配件分类，关联字典表 */
	private String partsType;

	/** 配件厂家，关联字典表 */
	private String partsFactory;

	/** 删除标记，0删除，1未删除 默认1 */
	private String enableFlag;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 备注 */
	private String remark;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 最后编辑时间 */
	private Long updateTime;

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

	public String getPartsId() {
		return partsId;
	}

	public void setPartsId(String partsId) {
		this.partsId = partsId;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
	}

	public String getPartsName() {
		return partsName;
	}

	public void setPartsName(String partsName) {
		this.partsName = partsName;
	}

	public String getPartsBrand() {
		return partsBrand;
	}

	public void setPartsBrand(String partsBrand) {
		this.partsBrand = partsBrand;
	}

	public String getPartsBarcode() {
		return partsBarcode;
	}

	public void setPartsBarcode(String partsBarcode) {
		this.partsBarcode = partsBarcode;
	}

	public String getSerPartsCode() {
		return serPartsCode;
	}

	public void setSerPartsCode(String serPartsCode) {
		this.serPartsCode = serPartsCode;
	}

	public String getCarPartsCode() {
		return carPartsCode;
	}

	public void setCarPartsCode(String carPartsCode) {
		this.carPartsCode = carPartsCode;
	}

	public String getQuickCode() {
		return quickCode;
	}

	public void setQuickCode(String quickCode) {
		this.quickCode = quickCode;
	}

	public String getModel() {
		return model;
	}

	public void setModel(String model) {
		this.model = model;
	}

	public String getDrawingNum() {
		return drawingNum;
	}

	public void setDrawingNum(String drawingNum) {
		this.drawingNum = drawingNum;
	}

	public String getPlaceOrigin() {
		return placeOrigin;
	}

	public void setPlaceOrigin(String placeOrigin) {
		this.placeOrigin = placeOrigin;
	}

	public String getDefaultUnit() {
		return defaultUnit;
	}

	public void setDefaultUnit(String defaultUnit) {
		this.defaultUnit = defaultUnit;
	}

	public String getSalesUnitCode() {
		return salesUnitCode;
	}

	public void setSalesUnitCode(String salesUnitCode) {
		this.salesUnitCode = salesUnitCode;
	}

	public String getSalesUnitName() {
		return salesUnitName;
	}

	public void setSalesUnitName(String salesUnitName) {
		this.salesUnitName = salesUnitName;
	}

	public String getBaseUnitCode() {
		return baseUnitCode;
	}

	public void setBaseUnitCode(String baseUnitCode) {
		this.baseUnitCode = baseUnitCode;
	}

	public String getBaseUnitName() {
		return baseUnitName;
	}

	public void setBaseUnitName(String baseUnitName) {
		this.baseUnitName = baseUnitName;
	}

	public Integer getSalesUnitQuantity() {
		return salesUnitQuantity;
	}

	public void setSalesUnitQuantity(Integer salesUnitQuantity) {
		this.salesUnitQuantity = salesUnitQuantity;
	}

	public Integer getBaseUnitQuantity() {
		return baseUnitQuantity;
	}

	public void setBaseUnitQuantity(Integer baseUnitQuantity) {
		this.baseUnitQuantity = baseUnitQuantity;
	}

	public String getWeight() {
		return weight;
	}

	public void setWeight(String weight) {
		this.weight = weight;
	}

	public BigDecimal getPrice3a() {
		return price3a;
	}

	public void setPrice3a(BigDecimal price3a) {
		this.price3a = price3a;
	}

	public BigDecimal getPrice2a() {
		return price2a;
	}

	public void setPrice2a(BigDecimal price2a) {
		this.price2a = price2a;
	}

	public BigDecimal getRetail() {
		return retail;
	}

	public void setRetail(BigDecimal retail) {
		this.retail = retail;
	}

	public BigDecimal getRefInPrice() {
		return refInPrice;
	}

	public void setRefInPrice(BigDecimal refInPrice) {
		this.refInPrice = refInPrice;
	}

	public BigDecimal getRefOutPrice() {
		return refOutPrice;
	}

	public void setRefOutPrice(BigDecimal refOutPrice) {
		this.refOutPrice = refOutPrice;
	}

	public BigDecimal getHighestInPrice() {
		return highestInPrice;
	}

	public void setHighestInPrice(BigDecimal highestInPrice) {
		this.highestInPrice = highestInPrice;
	}

	public BigDecimal getHighestOutPrice() {
		return highestOutPrice;
	}

	public void setHighestOutPrice(BigDecimal highestOutPrice) {
		this.highestOutPrice = highestOutPrice;
	}

	public String getIsGeneral() {
		return isGeneral;
	}

	public void setIsGeneral(String isGeneral) {
		this.isGeneral = isGeneral;
	}

	public String getReplaced() {
		return replaced;
	}

	public void setReplaced(String replaced) {
		this.replaced = replaced;
	}

	public String getIsReplac() {
		return isReplac;
	}

	public void setIsReplac(String isReplac) {
		this.isReplac = isReplac;
	}

	public String getIsImport() {
		return isImport;
	}

	public void setIsImport(String isImport) {
		this.isImport = isImport;
	}

	public String getSupplierOne() {
		return supplierOne;
	}

	public void setSupplierOne(String supplierOne) {
		this.supplierOne = supplierOne;
	}

	public String getSupplierTwo() {
		return supplierTwo;
	}

	public void setSupplierTwo(String supplierTwo) {
		this.supplierTwo = supplierTwo;
	}

	public String getPartsType() {
		return partsType;
	}

	public void setPartsType(String partsType) {
		this.partsType = partsType;
	}

	public String getPartsFactory() {
		return partsFactory;
	}

	public void setPartsFactory(String partsFactory) {
		this.partsFactory = partsFactory;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}
}