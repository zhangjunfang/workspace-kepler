package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-12-29</td>
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
public class SysAutoBackupSet implements Serializable {

	/** */
	private static final long serialVersionUID = 8645804660853937614L;

	/** id */
	private String autoBackupSetId;

	/** 任务名称 */
	private String taskName;

	/** 当有用户使用时 0不进行备份，1发信息通知用户退出 */
	private String whenUseHandleMethod;

	/** 自动备份周期类型 1天，2周，3，月 */
	private String autoBackupType;

	/** 自动备份周期间隔 */
	private String autoBackupInterval;

	/** 自动备份开始时间 */
	private Long autoBackupStarttime;

	/** 状态:0-未启用，1-使用中 */
	private String status;

	/** 删除标记:0-已删除1-未删除 */
	private String enableFlag;

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

	/** 服务站帐套 */
	private String bookId;

	public String getAutoBackupSetId() {
		return autoBackupSetId;
	}

	public void setAutoBackupSetId(String autoBackupSetId) {
		this.autoBackupSetId = autoBackupSetId;
	}

	public String getTaskName() {
		return taskName;
	}

	public void setTaskName(String taskName) {
		this.taskName = taskName;
	}

	public String getWhenUseHandleMethod() {
		return whenUseHandleMethod;
	}

	public void setWhenUseHandleMethod(String whenUseHandleMethod) {
		this.whenUseHandleMethod = whenUseHandleMethod;
	}

	public String getAutoBackupType() {
		return autoBackupType;
	}

	public void setAutoBackupType(String autoBackupType) {
		this.autoBackupType = autoBackupType;
	}

	public String getAutoBackupInterval() {
		return autoBackupInterval;
	}

	public void setAutoBackupInterval(String autoBackupInterval) {
		this.autoBackupInterval = autoBackupInterval;
	}

	public Long getAutoBackupStarttime() {
		return autoBackupStarttime;
	}

	public void setAutoBackupStarttime(Long autoBackupStarttime) {
		this.autoBackupStarttime = autoBackupStarttime;
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

	public String getBookId() {
		return bookId;
	}

	public void setBookId(String bookId) {
		this.bookId = bookId;
	}

}
