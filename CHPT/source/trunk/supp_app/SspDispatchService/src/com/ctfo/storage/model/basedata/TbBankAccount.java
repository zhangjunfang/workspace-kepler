package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 银行帐号设置<br>
 * 描述： 银行帐号设置<br>
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
 * <td>2015-1-8</td>
 * <td>Administrator</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author Administrator
 * @since JDK1.6
 */
public class TbBankAccount implements Serializable {

	/** */
	private static final long serialVersionUID = 5382501188001729721L;

	/**
	 * tb_bank_account.bank_account_id
	 * 
	 */
	private String bankAccountId;

	/**
	 * tb_bank_account.bank_name
	 * 
	 */
	private String bankName;

	/**
	 * tb_bank_account.bank_account
	 * 
	 */
	private String bankAccount;

	/**
	 * tb_bank_account.status
	 * 
	 */
	private String status;

	/**
	 * tb_bank_account.enable_flag
	 * 
	 */
	private String enableFlag;

	/**
	 * tb_bank_account.create_by
	 * 
	 */
	private String createBy;

	/**
	 * tb_bank_account.create_time
	 * 
	 */
	private Long createTime;

	/**
	 * tb_bank_account.update_by
	 * 
	 */
	private String updateBy;

	/**
	 * tb_bank_account.update_time
	 * 
	 */
	private Long updateTime;

	/**
	 * tb_bank_account.ser_station_id 服务站id，云平台用
	 */
	private String serStationId;

	/**
	 * tb_bank_account.set_book_id 帐套id，云平台用
	 */
	private String setBookId;

	public String getBankAccountId() {
		return bankAccountId;
	}

	public void setBankAccountId(String bankAccountId) {
		this.bankAccountId = bankAccountId;
	}

	public String getBankName() {
		return bankName;
	}

	public void setBankName(String bankName) {
		this.bankName = bankName;
	}

	public String getBankAccount() {
		return bankAccount;
	}

	public void setBankAccount(String bankAccount) {
		this.bankAccount = bankAccount;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
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