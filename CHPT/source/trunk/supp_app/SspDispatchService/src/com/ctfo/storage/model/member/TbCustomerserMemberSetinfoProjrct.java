package com.ctfo.storage.model.member;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 会员参数设置特殊维修项目折扣表<br>
 * 描述： 会员参数设置特殊维修项目折扣表<br>
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
public class TbCustomerserMemberSetinfoProjrct implements Serializable {

	/** */
	private static final long serialVersionUID = 428500247094668729L;

	/** id */
	private String id;

	/** 关联会员信息 */
	private String setinfoId;

	/** 关联维修项目id */
	private String serviceProjectId;

	/** 特殊维修项目折扣 */
	private BigDecimal serviceProjectDiscount;

	/** 备注 */
	private String remark;

	/** 删除标记，0删除，1未删除 */
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

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getSetinfoId() {
		return setinfoId;
	}

	public void setSetinfoId(String setinfoId) {
		this.setinfoId = setinfoId;
	}

	public String getServiceProjectId() {
		return serviceProjectId;
	}

	public void setServiceProjectId(String serviceProjectId) {
		this.serviceProjectId = serviceProjectId;
	}

	public BigDecimal getServiceProjectDiscount() {
		return serviceProjectDiscount;
	}

	public void setServiceProjectDiscount(BigDecimal serviceProjectDiscount) {
		this.serviceProjectDiscount = serviceProjectDiscount;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
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
}