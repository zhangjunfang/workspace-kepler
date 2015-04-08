package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 库存业务参数<br>
 * 描述： 库存业务参数<br>
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
public class SysStockParam implements Serializable {

	/** */
	private static final long serialVersionUID = 5875178173935029458L;

	/** id */
	private String stockParamId;

	/** 启用货位管理 */
	private String storageManage;

	/** 制单和审核为同一人 */
	private String makingAuditOnePerson;

	/** 允许账面零（负）库存出库 */
	private String allowZeroLibStock;

	/** 允许期末零（负）库存账面结账 */
	private String allowZeroLibJunction;

	/** 制单人和编辑人可以为同一人 */
	private String singleEditorsOnePerson;

	/** 制单人和审核人可以为同一人 */
	private String singleAuditOnePerson;

	/** 制单人和作废人可以为同一人 */
	private String singleDisabledOnePerson;

	/** 制单人和删除人可以为同一人 */
	private String singleDeleteOnePerson;

	/** 月加权平均法 */
	private String monthlyAverageMethod;

	/** 移动加权平均法 */
	private String movingAverageMethod;

	/** 先进先出法 */
	private String fifoMethod;

	/** 数量 */
	private String counts;

	/** 数量补零 */
	private String countsZero;

	/** 价格 */
	private String price;

	/** 价格补零 */
	private String priceZero;

	/** 启用出入库单无引用生成 */
	private String warehousSingleReference;

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

	/** 启用批号管理 */
	private String batchManage;

	/** 服务站帐套 */
	private String bookId;

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

	public String getStockParamId() {
		return stockParamId;
	}

	public void setStockParamId(String stockParamId) {
		this.stockParamId = stockParamId;
	}

	public String getStorageManage() {
		return storageManage;
	}

	public void setStorageManage(String storageManage) {
		this.storageManage = storageManage;
	}

	public String getMakingAuditOnePerson() {
		return makingAuditOnePerson;
	}

	public void setMakingAuditOnePerson(String makingAuditOnePerson) {
		this.makingAuditOnePerson = makingAuditOnePerson;
	}

	public String getAllowZeroLibStock() {
		return allowZeroLibStock;
	}

	public void setAllowZeroLibStock(String allowZeroLibStock) {
		this.allowZeroLibStock = allowZeroLibStock;
	}

	public String getAllowZeroLibJunction() {
		return allowZeroLibJunction;
	}

	public void setAllowZeroLibJunction(String allowZeroLibJunction) {
		this.allowZeroLibJunction = allowZeroLibJunction;
	}

	public String getSingleEditorsOnePerson() {
		return singleEditorsOnePerson;
	}

	public void setSingleEditorsOnePerson(String singleEditorsOnePerson) {
		this.singleEditorsOnePerson = singleEditorsOnePerson;
	}

	public String getSingleAuditOnePerson() {
		return singleAuditOnePerson;
	}

	public void setSingleAuditOnePerson(String singleAuditOnePerson) {
		this.singleAuditOnePerson = singleAuditOnePerson;
	}

	public String getSingleDisabledOnePerson() {
		return singleDisabledOnePerson;
	}

	public void setSingleDisabledOnePerson(String singleDisabledOnePerson) {
		this.singleDisabledOnePerson = singleDisabledOnePerson;
	}

	public String getSingleDeleteOnePerson() {
		return singleDeleteOnePerson;
	}

	public void setSingleDeleteOnePerson(String singleDeleteOnePerson) {
		this.singleDeleteOnePerson = singleDeleteOnePerson;
	}

	public String getMonthlyAverageMethod() {
		return monthlyAverageMethod;
	}

	public void setMonthlyAverageMethod(String monthlyAverageMethod) {
		this.monthlyAverageMethod = monthlyAverageMethod;
	}

	public String getMovingAverageMethod() {
		return movingAverageMethod;
	}

	public void setMovingAverageMethod(String movingAverageMethod) {
		this.movingAverageMethod = movingAverageMethod;
	}

	public String getFifoMethod() {
		return fifoMethod;
	}

	public void setFifoMethod(String fifoMethod) {
		this.fifoMethod = fifoMethod;
	}

	public String getCounts() {
		return counts;
	}

	public void setCounts(String counts) {
		this.counts = counts;
	}

	public String getCountsZero() {
		return countsZero;
	}

	public void setCountsZero(String countsZero) {
		this.countsZero = countsZero;
	}

	public String getPrice() {
		return price;
	}

	public void setPrice(String price) {
		this.price = price;
	}

	public String getPriceZero() {
		return priceZero;
	}

	public void setPriceZero(String priceZero) {
		this.priceZero = priceZero;
	}

	public String getWarehousSingleReference() {
		return warehousSingleReference;
	}

	public void setWarehousSingleReference(String warehousSingleReference) {
		this.warehousSingleReference = warehousSingleReference;
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

	public String getBatchManage() {
		return batchManage;
	}

	public void setBatchManage(String batchManage) {
		this.batchManage = batchManage;
	}

	public String getBookId() {
		return bookId;
	}

	public void setBookId(String bookId) {
		this.bookId = bookId;
	}

}