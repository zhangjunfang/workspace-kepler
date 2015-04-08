package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 业务设置-维修套餐设置-维修项目<br>
 * 描述： 业务设置-维修套餐设置-维修项目<br>
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
public class SysBSetRepairPackageSetProject implements Serializable {

	/** */
	private static final long serialVersionUID = -8872921114788882719L;

	/** id */
	private String repairPackageSetProjectId;

	/** 项目编码 */
	private String projectNum;

	/** 项目名称 */
	private String projectName;

	/** 项目类别 */
	private String repairType;

	/** 工时类别 */
	private String whoursType;

	/** 标准工时 */
	private String standardWhours;

	/** 工时数量 */
	private Integer whoursCounts;

	/** 工时单价 */
	private BigDecimal whoursPrice;

	/** 税率 */
	private BigDecimal taxRate;

	/** 含税金额 */
	private BigDecimal taxAmount;

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

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 关联维修套餐设置表Id */
	private String repairPackageSetId;

	/** 删除标记:0-已删除1-未删除 */
	private String enableFlag;

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

	public String getRepairPackageSetProjectId() {
		return repairPackageSetProjectId;
	}

	public void setRepairPackageSetProjectId(String repairPackageSetProjectId) {
		this.repairPackageSetProjectId = repairPackageSetProjectId;
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

	public String getRepairType() {
		return repairType;
	}

	public void setRepairType(String repairType) {
		this.repairType = repairType;
	}

	public String getWhoursType() {
		return whoursType;
	}

	public void setWhoursType(String whoursType) {
		this.whoursType = whoursType;
	}

	public String getStandardWhours() {
		return standardWhours;
	}

	public void setStandardWhours(String standardWhours) {
		this.standardWhours = standardWhours;
	}

	public Integer getWhoursCounts() {
		return whoursCounts;
	}

	public void setWhoursCounts(Integer whoursCounts) {
		this.whoursCounts = whoursCounts;
	}

	public BigDecimal getWhoursPrice() {
		return whoursPrice;
	}

	public void setWhoursPrice(BigDecimal whoursPrice) {
		this.whoursPrice = whoursPrice;
	}

	public BigDecimal getTaxRate() {
		return taxRate;
	}

	public void setTaxRate(BigDecimal taxRate) {
		this.taxRate = taxRate;
	}

	public BigDecimal getTaxAmount() {
		return taxAmount;
	}

	public void setTaxAmount(BigDecimal taxAmount) {
		this.taxAmount = taxAmount;
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

	public String getRepairPackageSetId() {
		return repairPackageSetId;
	}

	public void setRepairPackageSetId(String repairPackageSetId) {
		this.repairPackageSetId = repairPackageSetId;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

}