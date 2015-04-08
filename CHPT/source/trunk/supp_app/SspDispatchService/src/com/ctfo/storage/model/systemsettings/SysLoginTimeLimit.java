package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 登录时间限制<br>
 * 描述： 登录时间限制<br>
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
public class SysLoginTimeLimit implements Serializable {

	/** */
	private static final long serialVersionUID = -3791673475900255691L;

	/** id */
	private String loginTimeLimitId;

	/** 限制类型 1，按周期 2，按星期 */
	private String limitType;

	/** 星期一 */
	private String monday;

	/** 星期二 */
	private String tuesday;

	/** 星期三 */
	private String wednesday;

	/** 星期四 */
	private String thursday;

	/** 星期五 */
	private String friday;

	/** 星期六 */
	private String saturday;

	/** 星期日 */
	private String sunday;

	/** 周期起始时间 */
	private Long cycleStartTime;

	/** 周期结束时间 */
	private Long cycleEndTime;

	/** 星期起始时间 */
	private Long weekStartTime;

	/** 星期结束时间 */
	private Long weekEndTime;

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

	public String getLoginTimeLimitId() {
		return loginTimeLimitId;
	}

	public void setLoginTimeLimitId(String loginTimeLimitId) {
		this.loginTimeLimitId = loginTimeLimitId;
	}

	public String getLimitType() {
		return limitType;
	}

	public void setLimitType(String limitType) {
		this.limitType = limitType;
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

	public Long getCycleStartTime() {
		return cycleStartTime;
	}

	public void setCycleStartTime(Long cycleStartTime) {
		this.cycleStartTime = cycleStartTime;
	}

	public Long getCycleEndTime() {
		return cycleEndTime;
	}

	public void setCycleEndTime(Long cycleEndTime) {
		this.cycleEndTime = cycleEndTime;
	}

	public Long getWeekStartTime() {
		return weekStartTime;
	}

	public void setWeekStartTime(Long weekStartTime) {
		this.weekStartTime = weekStartTime;
	}

	public Long getWeekEndTime() {
		return weekEndTime;
	}

	public void setWeekEndTime(Long weekEndTime) {
		this.weekEndTime = weekEndTime;
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
}