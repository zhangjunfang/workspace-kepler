package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 宇通三包维修项目表<br>
 * 描述： 宇通三包维修项目表<br>
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
public class TbMaintainThreeGuarantyItem implements Serializable {

	/** */
	private static final long serialVersionUID = -2407420896666984440L;

	/** 维修项目信息id */
	private String itemId;

	/** 维修项目编码 */
	private String itemNo;

	/** 维修项目类别 */
	private String itemType;

	/** 维修项目名称 */
	private String itemName;

	/** 工时类型 */
	private String manHourType;

	/** 工时数量 */
	private BigDecimal manHourQuantity;

	/** 原工时单价 */
	private BigDecimal manHourNormUnitprice;

	/** 工时单价 */
	private BigDecimal manHourUnitprice;

	/** 金额 */
	private BigDecimal sumMoney;

	/** 备注 */
	private String remarks;

	/** 关联id（三包服务单id） */
	private String tgId;

	/** 逻辑删除标志（1|有效；0|删除） */
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

	public String getItemId() {
		return itemId;
	}

	public void setItemId(String itemId) {
		this.itemId = itemId;
	}

	public String getItemNo() {
		return itemNo;
	}

	public void setItemNo(String itemNo) {
		this.itemNo = itemNo;
	}

	public String getItemType() {
		return itemType;
	}

	public void setItemType(String itemType) {
		this.itemType = itemType;
	}

	public String getItemName() {
		return itemName;
	}

	public void setItemName(String itemName) {
		this.itemName = itemName;
	}

	public String getManHourType() {
		return manHourType;
	}

	public void setManHourType(String manHourType) {
		this.manHourType = manHourType;
	}

	public BigDecimal getManHourQuantity() {
		return manHourQuantity;
	}

	public void setManHourQuantity(BigDecimal manHourQuantity) {
		this.manHourQuantity = manHourQuantity;
	}

	public BigDecimal getManHourNormUnitprice() {
		return manHourNormUnitprice;
	}

	public void setManHourNormUnitprice(BigDecimal manHourNormUnitprice) {
		this.manHourNormUnitprice = manHourNormUnitprice;
	}

	public BigDecimal getManHourUnitprice() {
		return manHourUnitprice;
	}

	public void setManHourUnitprice(BigDecimal manHourUnitprice) {
		this.manHourUnitprice = manHourUnitprice;
	}

	public BigDecimal getSumMoney() {
		return sumMoney;
	}

	public void setSumMoney(BigDecimal sumMoney) {
		this.sumMoney = sumMoney;
	}

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
	}

	public String getTgId() {
		return tgId;
	}

	public void setTgId(String tgId) {
		this.tgId = tgId;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
}