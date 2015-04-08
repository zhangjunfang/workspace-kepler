package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 结算方式设置<br>
 * 描述： 结算方式设置<br>
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
public class TbBalanceWay implements Serializable {

	/** */
	private static final long serialVersionUID = 6310904214028392518L;

	/**
	 * tb_balance_way.balance_way_id
	 * 
	 */
	private String balanceWayId;

	/**
	 * tb_balance_way.balance_way_name
	 * 
	 */
	private String balanceWayName;

	/**
	 * tb_balance_way.default_account
	 * 
	 */
	private String defaultAccount;

	/**
	 * tb_balance_way.status
	 * 
	 */
	private String status;

	/**
	 * tb_balance_way.enable_flag
	 * 
	 */
	private String enableFlag;

	/**
	 * tb_balance_way.create_by
	 * 
	 */
	private String createBy;

	/**
	 * tb_balance_way.create_time
	 * 
	 */
	private Long createTime;

	/**
	 * tb_balance_way.update_by
	 * 
	 */
	private String updateBy;

	/**
	 * tb_balance_way.update_time
	 * 
	 */
	private Long updateTime;

	/**
	 * tb_balance_way.ser_station_id 服务站id，云平台用
	 */
	private String serStationId;

	/**
	 * tb_balance_way.set_book_id 帐套id，云平台用
	 */
	private String setBookId;

	public String getBalanceWayId() {
		return balanceWayId;
	}

	public void setBalanceWayId(String balanceWayId) {
		this.balanceWayId = balanceWayId;
	}

	public String getBalanceWayName() {
		return balanceWayName;
	}

	public void setBalanceWayName(String balanceWayName) {
		this.balanceWayName = balanceWayName;
	}

	public String getDefaultAccount() {
		return defaultAccount;
	}

	public void setDefaultAccount(String defaultAccount) {
		this.defaultAccount = defaultAccount;
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