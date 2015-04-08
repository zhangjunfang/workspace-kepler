package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 车型档案<br>
 * 描述： 车型档案<br>
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
public class TbVehicleModels implements Serializable {

	/** */
	private static final long serialVersionUID = -3650287222202590535L;

	/** 车型档案ID */
	private String vmId;

	/** 数据来源，关联字典码表 */
	private String dataSource;

	/** 车辆品牌，关联字典码表 */
	private String vBrand;

	/** 车型类别，关联字典码表 */
	private String vmType;

	/** 车型分类，字典 */
	private String vmClass;

	/** 车型编号 */
	private String vmCode;

	/** 车型名称 */
	private String vmName;

	/** 备注 */
	private String remark;

	/** 外出里程单价---宇通 */
	private BigDecimal outPrice;

	/** 外出里程单价(特殊)---宇通 */
	private BigDecimal outSpecialPrice;

	/** 可售车型---宇通 */
	private String vSaleType;

	/** 报到单价---宇通 */
	private BigDecimal reportPrice;

	/** 特殊里程单价时间(开始)---宇通 */
	private String beginDate;

	/** 特殊里程单价时间（结束）---宇通 */
	private String endDate;

	/** 保养单价---宇通 */
	private BigDecimal repairPrice;

	/** 删除标记，0为删除，1未删除 默认1 */
	private String enableFlag;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 最后编辑时间 */
	private Long updateTime;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 宇通CRMid */
	private String modelsCrmId;

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

	public String getVmId() {
		return vmId;
	}

	public void setVmId(String vmId) {
		this.vmId = vmId;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
	}

	public String getVBrand() {
		return vBrand;
	}

	public void setVBrand(String vBrand) {
		this.vBrand = vBrand;
	}

	public String getVmType() {
		return vmType;
	}

	public void setVmType(String vmType) {
		this.vmType = vmType;
	}

	public String getVmClass() {
		return vmClass;
	}

	public void setVmClass(String vmClass) {
		this.vmClass = vmClass;
	}

	public String getVmCode() {
		return vmCode;
	}

	public void setVmCode(String vmCode) {
		this.vmCode = vmCode;
	}

	public String getVmName() {
		return vmName;
	}

	public void setVmName(String vmName) {
		this.vmName = vmName;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public BigDecimal getOutPrice() {
		return outPrice;
	}

	public void setOutPrice(BigDecimal outPrice) {
		this.outPrice = outPrice;
	}

	public BigDecimal getOutSpecialPrice() {
		return outSpecialPrice;
	}

	public void setOutSpecialPrice(BigDecimal outSpecialPrice) {
		this.outSpecialPrice = outSpecialPrice;
	}

	public String getVSaleType() {
		return vSaleType;
	}

	public void setVSaleType(String vSaleType) {
		this.vSaleType = vSaleType;
	}

	public BigDecimal getReportPrice() {
		return reportPrice;
	}

	public void setReportPrice(BigDecimal reportPrice) {
		this.reportPrice = reportPrice;
	}

	public String getBeginDate() {
		return beginDate;
	}

	public void setBeginDate(String beginDate) {
		this.beginDate = beginDate;
	}

	public String getEndDate() {
		return endDate;
	}

	public void setEndDate(String endDate) {
		this.endDate = endDate;
	}

	public BigDecimal getRepairPrice() {
		return repairPrice;
	}

	public void setRepairPrice(BigDecimal repairPrice) {
		this.repairPrice = repairPrice;
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

	public String getModelsCrmId() {
		return modelsCrmId;
	}

	public void setModelsCrmId(String modelsCrmId) {
		this.modelsCrmId = modelsCrmId;
	}

}