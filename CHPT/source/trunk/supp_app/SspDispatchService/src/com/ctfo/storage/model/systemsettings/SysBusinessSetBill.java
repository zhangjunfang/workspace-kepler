package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 业务设置-单据<br>
 * 描述： 业务设置-单据<br>
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
public class SysBusinessSetBill implements Serializable {

	/** */
	private static final long serialVersionUID = -8982364885935613385L;

	/** id */
	private String businessSetBillId;

	/** 单据关闭时自动保存草稿 */
	private String closedAutoSaveDraft;

	/** 销售单保存时判断库存 */
	private String salesSaveJudgingStock;

	/** 启用采购单审核生成入库单 */
	private String purchaseOrderGenerationIn;

	/** 启用采购单审核生成付款单 */
	private String purchaseOrderGenerationOut;

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

	/** 服务站帐套id */
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

	public String getBusinessSetBillId() {
		return businessSetBillId;
	}

	public void setBusinessSetBillId(String businessSetBillId) {
		this.businessSetBillId = businessSetBillId;
	}

	public String getClosedAutoSaveDraft() {
		return closedAutoSaveDraft;
	}

	public void setClosedAutoSaveDraft(String closedAutoSaveDraft) {
		this.closedAutoSaveDraft = closedAutoSaveDraft;
	}

	public String getSalesSaveJudgingStock() {
		return salesSaveJudgingStock;
	}

	public void setSalesSaveJudgingStock(String salesSaveJudgingStock) {
		this.salesSaveJudgingStock = salesSaveJudgingStock;
	}

	public String getPurchaseOrderGenerationIn() {
		return purchaseOrderGenerationIn;
	}

	public void setPurchaseOrderGenerationIn(String purchaseOrderGenerationIn) {
		this.purchaseOrderGenerationIn = purchaseOrderGenerationIn;
	}

	public String getPurchaseOrderGenerationOut() {
		return purchaseOrderGenerationOut;
	}

	public void setPurchaseOrderGenerationOut(String purchaseOrderGenerationOut) {
		this.purchaseOrderGenerationOut = purchaseOrderGenerationOut;
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

	public String getBookId() {
		return bookId;
	}

	public void setBookId(String bookId) {
		this.bookId = bookId;
	}

}