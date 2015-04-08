package com.ctfo.storage.model.finance;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 往来帐核销-业务单据<br>
 * 描述： 往来帐核销-业务单据<br>
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
 * <td>2014-12-3</td>
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
public class TbVerificationnDocuments implements Serializable {

	/** */
	private static final long serialVersionUID = 3026457374893423031L;

	/** id */
	private String verificationnDocumentsId;

	/** 往来账核销id */
	private String accountVerificationId;

	/** 业务单据名称 */
	private String orderName;

	/** 总金额 */
	private BigDecimal money;

	/** 业务单据编号 */
	private String orderNum;

	/** 业务单据日期 */
	private Long orderDate;

	/** 已结算金额 */
	private BigDecimal settledMoney;

	/** 未结算金额 */
	private BigDecimal waitSettledMoney;

	/** 本次核销 */
	private BigDecimal verificationMoney;

	/** 是否核销 0：否 1：是 */
	private String isVerification;

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

	/** 单据所在datagridview */
	private String orderIndex;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getVerificationnDocumentsId() {
		return verificationnDocumentsId;
	}

	public void setVerificationnDocumentsId(String verificationnDocumentsId) {
		this.verificationnDocumentsId = verificationnDocumentsId;
	}

	public String getAccountVerificationId() {
		return accountVerificationId;
	}

	public void setAccountVerificationId(String accountVerificationId) {
		this.accountVerificationId = accountVerificationId;
	}

	public String getOrderName() {
		return orderName;
	}

	public void setOrderName(String orderName) {
		this.orderName = orderName;
	}

	public BigDecimal getMoney() {
		return money;
	}

	public void setMoney(BigDecimal money) {
		this.money = money;
	}

	public String getOrderNum() {
		return orderNum;
	}

	public void setOrderNum(String orderNum) {
		this.orderNum = orderNum;
	}

	public Long getOrderDate() {
		return orderDate;
	}

	public void setOrderDate(Long orderDate) {
		this.orderDate = orderDate;
	}

	public BigDecimal getSettledMoney() {
		return settledMoney;
	}

	public void setSettledMoney(BigDecimal settledMoney) {
		this.settledMoney = settledMoney;
	}

	public BigDecimal getWaitSettledMoney() {
		return waitSettledMoney;
	}

	public void setWaitSettledMoney(BigDecimal waitSettledMoney) {
		this.waitSettledMoney = waitSettledMoney;
	}

	public BigDecimal getVerificationMoney() {
		return verificationMoney;
	}

	public void setVerificationMoney(BigDecimal verificationMoney) {
		this.verificationMoney = verificationMoney;
	}

	public String getIsVerification() {
		return isVerification;
	}

	public void setIsVerification(String isVerification) {
		this.isVerification = isVerification;
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

	public String getOrderIndex() {
		return orderIndex;
	}

	public void setOrderIndex(String orderIndex) {
		this.orderIndex = orderIndex;
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