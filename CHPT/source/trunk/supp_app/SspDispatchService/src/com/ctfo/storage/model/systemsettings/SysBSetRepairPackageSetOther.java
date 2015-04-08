package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 业务设置-维修套餐设置-其他收费项目<br>
 * 描述： 业务设置-维修套餐设置-其他收费项目<br>
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
public class SysBSetRepairPackageSetOther implements Serializable {

	/** */
	private static final long serialVersionUID = 7032142019670375444L;

	/** id */
	private String repairPackageSetOtherId;

	/** 其他费用类型 */
	private String otherExpenseType;

	/** 其他费用金额 */
	private BigDecimal otherExpenseAmount;

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

	public String getRepairPackageSetOtherId() {
		return repairPackageSetOtherId;
	}

	public void setRepairPackageSetOtherId(String repairPackageSetOtherId) {
		this.repairPackageSetOtherId = repairPackageSetOtherId;
	}

	public String getOtherExpenseType() {
		return otherExpenseType;
	}

	public void setOtherExpenseType(String otherExpenseType) {
		this.otherExpenseType = otherExpenseType;
	}

	public BigDecimal getOtherExpenseAmount() {
		return otherExpenseAmount;
	}

	public void setOtherExpenseAmount(BigDecimal otherExpenseAmount) {
		this.otherExpenseAmount = otherExpenseAmount;
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

}