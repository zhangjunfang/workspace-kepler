package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 数据同步日志<br>
 * 描述： 数据同步日志<br>
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
 * <td>2014-12-5</td>
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
public class SysDataSyncLog implements Serializable {

	/** */
	private static final long serialVersionUID = -3096323488595708117L;

	/** id */
	private String dataSyncLogId;

	/** 业务名称 */
	private String businessName;

	/** 表名 */
	private String tableName;

	/** 同步开始时间 */
	private Long syncStartTime;

	/** 同步结束时间 */
	private Long syncEndTime;

	/** 同步方向 */
	private String syncDirection;

	/** 变化条目数 */
	private Integer changesNum;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	private String syncResult;

	public String getDataSyncLogId() {
		return dataSyncLogId;
	}

	public void setDataSyncLogId(String dataSyncLogId) {
		this.dataSyncLogId = dataSyncLogId;
	}

	public String getBusinessName() {
		return businessName;
	}

	public void setBusinessName(String businessName) {
		this.businessName = businessName;
	}

	public String getTableName() {
		return tableName;
	}

	public void setTableName(String tableName) {
		this.tableName = tableName;
	}

	public Long getSyncStartTime() {
		return syncStartTime;
	}

	public void setSyncStartTime(Long syncStartTime) {
		this.syncStartTime = syncStartTime;
	}

	public Long getSyncEndTime() {
		return syncEndTime;
	}

	public void setSyncEndTime(Long syncEndTime) {
		this.syncEndTime = syncEndTime;
	}

	public String getSyncDirection() {
		return syncDirection;
	}

	public void setSyncDirection(String syncDirection) {
		this.syncDirection = syncDirection;
	}

	public Integer getChangesNum() {
		return changesNum;
	}

	public void setChangesNum(Integer changesNum) {
		this.changesNum = changesNum;
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

	public String getSyncResult() {
		return syncResult;
	}

	public void setSyncResult(String syncResult) {
		this.syncResult = syncResult;
	}

}