package com.ctfo.storage.model.finance;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 往来帐核销<br>
 * 描述： 往来帐核销<br>
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
public class TbAccountVerification implements Serializable {

	/** */
	private static final long serialVersionUID = 7787875708631488042L;

	/** id */
	private String accountVerificationId;

	/** 单号 */
	private String orderNum;

	/** 日期 */
	private Long orderDate;

	/** 单据状态 */
	private String orderStatus;

	/** 单据类型 包含：预收冲应收、预付冲应付、应付转应付、应收转应收、应收冲应付、应付冲应收、预收转预收、预付转预付 */
	private String orderType;

	/** 预收余额 */
	private BigDecimal advanceBalance;

	/** 往来单位ID1 */
	private String custId1;

	/** 往来单位编码1 */
	private String custCode1;

	/** 往来单位名称1 */
	private String custName1;

	/** 往来单位ID2 */
	private String custId2;

	/** 往来单位编码，包2 */
	private String custCode2;

	/** 往来单位名称2 */
	private String custName2;

	/** 部门，关联组织表 */
	private String orgId;

	/** 经办人，关联人员表 */
	private String handle;

	/** 操作人，关联人员表 */
	private String operator;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 最后编辑时间 */
	private Long updateTime;

	/** enable_flag */
	private String enableFlag;

	/** status */
	private String status;

	/** remark */
	private String remark;

	/** 审核意见 */
	private String verifyAdvice;

	/** 公司id */
	private String comId;

	public String getAccountVerificationId() {
		return accountVerificationId;
	}

	public void setAccountVerificationId(String accountVerificationId) {
		this.accountVerificationId = accountVerificationId;
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

	public String getOrderStatus() {
		return orderStatus;
	}

	public void setOrderStatus(String orderStatus) {
		this.orderStatus = orderStatus;
	}

	public String getOrderType() {
		return orderType;
	}

	public void setOrderType(String orderType) {
		this.orderType = orderType;
	}

	public BigDecimal getAdvanceBalance() {
		return advanceBalance;
	}

	public void setAdvanceBalance(BigDecimal advanceBalance) {
		this.advanceBalance = advanceBalance;
	}

	public String getCustId1() {
		return custId1;
	}

	public void setCustId1(String custId1) {
		this.custId1 = custId1;
	}

	public String getCustCode1() {
		return custCode1;
	}

	public void setCustCode1(String custCode1) {
		this.custCode1 = custCode1;
	}

	public String getCustName1() {
		return custName1;
	}

	public void setCustName1(String custName1) {
		this.custName1 = custName1;
	}

	public String getCustId2() {
		return custId2;
	}

	public void setCustId2(String custId2) {
		this.custId2 = custId2;
	}

	public String getCustCode2() {
		return custCode2;
	}

	public void setCustCode2(String custCode2) {
		this.custCode2 = custCode2;
	}

	public String getCustName2() {
		return custName2;
	}

	public void setCustName2(String custName2) {
		this.custName2 = custName2;
	}

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
	}

	public String getHandle() {
		return handle;
	}

	public void setHandle(String handle) {
		this.handle = handle;
	}

	public String getOperator() {
		return operator;
	}

	public void setOperator(String operator) {
		this.operator = operator;
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

	public String getVerifyAdvice() {
		return verifyAdvice;
	}

	public void setVerifyAdvice(String verifyAdvice) {
		this.verifyAdvice = verifyAdvice;
	}

	public String getComId() {
		return comId;
	}

	public void setComId(String comId) {
		this.comId = comId;
	}

}