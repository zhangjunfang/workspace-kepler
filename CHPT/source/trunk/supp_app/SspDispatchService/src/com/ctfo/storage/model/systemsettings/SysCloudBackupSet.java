package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 云备份设置<br>
 * 描述： 云备份设置<br>
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
 * <td>2015-1-7</td>
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
public class SysCloudBackupSet implements Serializable {

	/** */
	private static final long serialVersionUID = -5830085406541028713L;

	/** id */
	private String id;

	/** 任务名称 */
	private String taskName;

	/** 是否启动自动备份 0未启动，1启动 */
	private String isStartAutoBackup;

	/** 帐套信息 */
	private String setbookName;

	/** 当有帐套使用时 0不进行备份，1发信息通知用户退出 */
	private String whenSetBooksUse;

	/** 自动备份周期类型 1天，2周，3，月 */
	private String autoBackupType;

	/** 自动备份周期间隔 */
	private String autoBackupInterval;

	/** 周一 0未选中，1选中 */
	private String monday;

	/** 周二 0未选中，1选中 */
	private String tuesday;

	/** 周三 0未选中，1选中 */
	private String wednesday;

	/** 周四 0未选中，1选中 */
	private String thursday;

	/** 周五 0未选中，1选中 */
	private String friday;

	/** 周六 0未选中，1选中 */
	private String saturday;

	/** 周日 0未选中，1选中 */
	private String sunday;

	/** 自动备份持续时间起 */
	private Long autoBackupUp;

	/** 自动备份持续时间止 */
	private Long autoBackupCheck;

	/** 自动备份时间 */
	private String autoBackupTime;

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

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getTaskName() {
		return taskName;
	}

	public void setTaskName(String taskName) {
		this.taskName = taskName;
	}

	public String getIsStartAutoBackup() {
		return isStartAutoBackup;
	}

	public void setIsStartAutoBackup(String isStartAutoBackup) {
		this.isStartAutoBackup = isStartAutoBackup;
	}

	public String getSetbookName() {
		return setbookName;
	}

	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}

	public String getWhenSetBooksUse() {
		return whenSetBooksUse;
	}

	public void setWhenSetBooksUse(String whenSetBooksUse) {
		this.whenSetBooksUse = whenSetBooksUse;
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

	public String getMonday() {
		return monday;
	}

	public void setMonday(String monday) {
		this.monday = monday;
	}

	public String getTuesday() {
		return tuesday;
	}

	public void setTuesday(String tuesday) {
		this.tuesday = tuesday;
	}

	public String getWednesday() {
		return wednesday;
	}

	public void setWednesday(String wednesday) {
		this.wednesday = wednesday;
	}

	public String getThursday() {
		return thursday;
	}

	public void setThursday(String thursday) {
		this.thursday = thursday;
	}

	public String getFriday() {
		return friday;
	}

	public void setFriday(String friday) {
		this.friday = friday;
	}

	public String getSaturday() {
		return saturday;
	}

	public void setSaturday(String saturday) {
		this.saturday = saturday;
	}

	public String getSunday() {
		return sunday;
	}

	public void setSunday(String sunday) {
		this.sunday = sunday;
	}

	public Long getAutoBackupUp() {
		return autoBackupUp;
	}

	public void setAutoBackupUp(Long autoBackupUp) {
		this.autoBackupUp = autoBackupUp;
	}

	public Long getAutoBackupCheck() {
		return autoBackupCheck;
	}

	public void setAutoBackupCheck(Long autoBackupCheck) {
		this.autoBackupCheck = autoBackupCheck;
	}

	public String getAutoBackupTime() {
		return autoBackupTime;
	}

	public void setAutoBackupTime(String autoBackupTime) {
		this.autoBackupTime = autoBackupTime;
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
