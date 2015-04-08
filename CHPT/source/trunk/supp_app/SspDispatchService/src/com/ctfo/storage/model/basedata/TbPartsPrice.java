package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 配件价格信息<br>
 * 描述： 配件价格信息<br>
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
public class TbPartsPrice implements Serializable {

	/** */
	private static final long serialVersionUID = -3428813468604842365L;

	/** ppId */
	private String ppId;

	/** 配件ID */
	private String partsId;

	/** 是否库存单位 */
	private String isStock;

	/** 是否采购单位 */
	private String isPurchase;

	/** 是否销售单位 */
	private String isSale;

	/** 换算率 */
	private BigDecimal rate;

	/** 发票类型 */
	private String fapType;

	/** 单位 */
	private String unit;

	/** 参考售价 */
	private BigDecimal tbPartsPrice;

	/** 参考进价 */
	private BigDecimal refInPrice;

	/** 最高进价 */
	private BigDecimal highestInPrice;

	/** 最低售价 */
	private BigDecimal lowOutPrice;

	/** 进价1 */
	private BigDecimal intoPriceOne;

	/** 进价2 */
	private BigDecimal intoPriceTwo;

	/** 进价3 */
	private BigDecimal intoPriceThree;

	/** 销价1 */
	private BigDecimal outPriceOne;

	/** 销价2 */
	private BigDecimal outPriceTwo;

	/** 销价3 */
	private BigDecimal outPriceThree;

	/** 排序 */
	private int sortIndex;

	/** 删除标记，0为删除，1未删除 默认1 */
	private String enableFlag;

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

	public String getPpId() {
		return ppId;
	}

	public void setPpId(String ppId) {
		this.ppId = ppId;
	}

	public String getPartsId() {
		return partsId;
	}

	public void setPartsId(String partsId) {
		this.partsId = partsId;
	}

	public String getFapType() {
		return fapType;
	}

	public void setFapType(String fapType) {
		this.fapType = fapType;
	}

	public String getUnit() {
		return unit;
	}

	public void setUnit(String unit) {
		this.unit = unit;
	}

	public BigDecimal getIntoPriceOne() {
		return intoPriceOne;
	}

	public void setIntoPriceOne(BigDecimal intoPriceOne) {
		this.intoPriceOne = intoPriceOne;
	}

	public BigDecimal getIntoPriceTwo() {
		return intoPriceTwo;
	}

	public void setIntoPriceTwo(BigDecimal intoPriceTwo) {
		this.intoPriceTwo = intoPriceTwo;
	}

	public BigDecimal getIntoPriceThree() {
		return intoPriceThree;
	}

	public void setIntoPriceThree(BigDecimal intoPriceThree) {
		this.intoPriceThree = intoPriceThree;
	}

	public BigDecimal getOutPriceOne() {
		return outPriceOne;
	}

	public void setOutPriceOne(BigDecimal outPriceOne) {
		this.outPriceOne = outPriceOne;
	}

	public BigDecimal getOutPriceTwo() {
		return outPriceTwo;
	}

	public void setOutPriceTwo(BigDecimal outPriceTwo) {
		this.outPriceTwo = outPriceTwo;
	}

	public BigDecimal getOutPriceThree() {
		return outPriceThree;
	}

	public void setOutPriceThree(BigDecimal outPriceThree) {
		this.outPriceThree = outPriceThree;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
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

	public String getIsStock() {
		return isStock;
	}

	public void setIsStock(String isStock) {
		this.isStock = isStock;
	}

	public String getIsPurchase() {
		return isPurchase;
	}

	public void setIsPurchase(String isPurchase) {
		this.isPurchase = isPurchase;
	}

	public String getIsSale() {
		return isSale;
	}

	public void setIsSale(String isSale) {
		this.isSale = isSale;
	}

	public BigDecimal getRate() {
		return rate;
	}

	public void setRate(BigDecimal rate) {
		this.rate = rate;
	}

	public BigDecimal getRefInPrice() {
		return refInPrice;
	}

	public void setRefInPrice(BigDecimal refInPrice) {
		this.refInPrice = refInPrice;
	}

	public BigDecimal getTbPartsPrice() {
		return tbPartsPrice;
	}

	public void setTbPartsPrice(BigDecimal tbPartsPrice) {
		this.tbPartsPrice = tbPartsPrice;
	}

	public int getSortIndex() {
		return sortIndex;
	}

	public void setSortIndex(int sortIndex) {
		this.sortIndex = sortIndex;
	}

	public BigDecimal getHighestInPrice() {
		return highestInPrice;
	}

	public void setHighestInPrice(BigDecimal highestInPrice) {
		this.highestInPrice = highestInPrice;
	}

	public BigDecimal getLowOutPrice() {
		return lowOutPrice;
	}

	public void setLowOutPrice(BigDecimal lowOutPrice) {
		this.lowOutPrice = lowOutPrice;
	}

}