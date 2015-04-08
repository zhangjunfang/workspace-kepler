package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 车厂旧件返厂单明细表<br>
 * 描述： 车厂旧件返厂单明细表<br>
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
public class TbMaintainOldpartRecycleMaterialDetail implements Serializable {

	/** */
	private static final long serialVersionUID = 2951124949093039689L;

	/** 返厂旧件信息id */
	private String partsId;

	/** 服务单号 */
	private String serviceNo;

	/** 配件编码 */
	private String partsCode;

	/** 配件名称 */
	private String partsName;

	/** 更换数量 */
	private BigDecimal changeNum;

	/** 发送数量 */
	private BigDecimal sendNum;

	/** 收到数量 */
	private BigDecimal receiveNum;

	/** 单位（米，千克，个） */
	private String unit;

	/** 规格 */
	private String norms;

	/** 单价 */
	private BigDecimal unitPrice;

	/** 需回收标记 */
	private String needRecycleMark;

	/** 需回收标记 */
	private String allRecycleMark;

	/** 原厂件 */
	private String original;

	/** 状态一致 */
	private String identityState;

	/** 二级站编码 */
	private String secondStationCode;

	/** 处理方式 */
	private String processMode;

	/** 备注 */
	private String remarks;

	/** 接收说明 */
	private String receiveExplain;

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

	public String getPartsId() {
		return partsId;
	}

	public void setPartsId(String partsId) {
		this.partsId = partsId;
	}

	public String getServiceNo() {
		return serviceNo;
	}

	public void setServiceNo(String serviceNo) {
		this.serviceNo = serviceNo;
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

	public BigDecimal getChangeNum() {
		return changeNum;
	}

	public void setChangeNum(BigDecimal changeNum) {
		this.changeNum = changeNum;
	}

	public BigDecimal getSendNum() {
		return sendNum;
	}

	public void setSendNum(BigDecimal sendNum) {
		this.sendNum = sendNum;
	}

	public BigDecimal getReceiveNum() {
		return receiveNum;
	}

	public void setReceiveNum(BigDecimal receiveNum) {
		this.receiveNum = receiveNum;
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

	public BigDecimal getUnitPrice() {
		return unitPrice;
	}

	public void setUnitPrice(BigDecimal unitPrice) {
		this.unitPrice = unitPrice;
	}

	public String getNeedRecycleMark() {
		return needRecycleMark;
	}

	public void setNeedRecycleMark(String needRecycleMark) {
		this.needRecycleMark = needRecycleMark;
	}

	public String getAllRecycleMark() {
		return allRecycleMark;
	}

	public void setAllRecycleMark(String allRecycleMark) {
		this.allRecycleMark = allRecycleMark;
	}

	public String getOriginal() {
		return original;
	}

	public void setOriginal(String original) {
		this.original = original;
	}

	public String getIdentityState() {
		return identityState;
	}

	public void setIdentityState(String identityState) {
		this.identityState = identityState;
	}

	public String getSecondStationCode() {
		return secondStationCode;
	}

	public void setSecondStationCode(String secondStationCode) {
		this.secondStationCode = secondStationCode;
	}

	public String getProcessMode() {
		return processMode;
	}

	public void setProcessMode(String processMode) {
		this.processMode = processMode;
	}

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
	}

	public String getReceiveExplain() {
		return receiveExplain;
	}

	public void setReceiveExplain(String receiveExplain) {
		this.receiveExplain = receiveExplain;
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