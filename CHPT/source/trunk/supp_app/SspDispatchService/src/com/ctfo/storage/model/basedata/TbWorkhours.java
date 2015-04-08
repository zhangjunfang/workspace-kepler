package com.ctfo.storage.model.basedata;

import java.math.BigDecimal;

public class TbWorkhours {
	/**
	 * tb_workhours.whours_id 工时档案ID
	 */
	private String whoursId;

	/**
	 * tb_workhours.data_source 数据来源
	 */
	private String dataSource;

	/**
	 * tb_workhours.repair_type 维修项目类别，关联字典码表
	 */
	private String repairType;

	/**
	 * tb_workhours.project_num 项目编号
	 */
	private String projectNum;

	/**
	 * tb_workhours.project_name 项目名称
	 */
	private String projectName;

	/**
	 * tb_workhours.project_remark 项目备注
	 */
	private String projectRemark;

	/**
	 * tb_workhours.whours_change 工时调整
	 */
	private String whoursChange;

	/**
	 * tb_workhours.whours_type 工时类型，1工时，2定额
	 */
	private String whoursType;

	/**
	 * tb_workhours.whours_num_a A类工时数
	 */
	private BigDecimal whoursNumA;

	/**
	 * tb_workhours.whours_num_b B类工时数
	 */
	private BigDecimal whoursNumB;

	/**
	 * tb_workhours.whours_num_c C类工时数
	 */
	private BigDecimal whoursNumC;

	/**
	 * tb_workhours.quota_price 定额单价
	 */
	private BigDecimal quotaPrice;

	/**
	 * tb_workhours.status 状态 0，停用 1，启用
	 */
	private String status;

	/**
	 * tb_workhours.enable_flag 删除标记，0为删除，1未删除 默认1
	 */
	private String enableFlag;

	/**
	 * tb_workhours.update_time 最后编辑时间
	 */
	private Long updateTime;

	/**
	 * tb_workhours.create_by 创建人，关联人员表
	 */
	private String createBy;

	/**
	 * tb_workhours.create_time 创建时间
	 */
	private Long createTime;

	/**
	 * tb_workhours.update_by 最后编辑人，关联人员表
	 */
	private String updateBy;

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

	public String getWhoursId() {
		return whoursId;
	}

	public void setWhoursId(String whoursId) {
		this.whoursId = whoursId;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
	}

	public String getRepairType() {
		return repairType;
	}

	public void setRepairType(String repairType) {
		this.repairType = repairType;
	}

	public String getProjectNum() {
		return projectNum;
	}

	public void setProjectNum(String projectNum) {
		this.projectNum = projectNum;
	}

	public String getProjectName() {
		return projectName;
	}

	public void setProjectName(String projectName) {
		this.projectName = projectName;
	}

	public String getProjectRemark() {
		return projectRemark;
	}

	public void setProjectRemark(String projectRemark) {
		this.projectRemark = projectRemark;
	}

	public String getWhoursChange() {
		return whoursChange;
	}

	public void setWhoursChange(String whoursChange) {
		this.whoursChange = whoursChange;
	}

	public String getWhoursType() {
		return whoursType;
	}

	public void setWhoursType(String whoursType) {
		this.whoursType = whoursType;
	}

	public BigDecimal getWhoursNumA() {
		return whoursNumA;
	}

	public void setWhoursNumA(BigDecimal whoursNumA) {
		this.whoursNumA = whoursNumA;
	}

	public BigDecimal getWhoursNumB() {
		return whoursNumB;
	}

	public void setWhoursNumB(BigDecimal whoursNumB) {
		this.whoursNumB = whoursNumB;
	}

	public BigDecimal getWhoursNumC() {
		return whoursNumC;
	}

	public void setWhoursNumC(BigDecimal whoursNumC) {
		this.whoursNumC = whoursNumC;
	}

	public BigDecimal getQuotaPrice() {
		return quotaPrice;
	}

	public void setQuotaPrice(BigDecimal quotaPrice) {
		this.quotaPrice = quotaPrice;
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

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
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
}