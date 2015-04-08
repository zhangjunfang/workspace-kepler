package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 故障模式<br>
 * 描述： 故障模式 <br>
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
public class TbFaultModel implements Serializable {

	/** */
	private static final long serialVersionUID = -7757420186034001722L;

	/** id */
	private String faultModelId;

	/** 故障模式代码 */
	private String fmeaCode;

	/** 故障模式名称 */
	private String fmeaName;

	/** 故障模式检索码 */
	private String fmeaIndexCode;

	/** 备注 */
	private String remark;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人 */
	private Long updateTime;

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

	public String getFaultModelId() {
		return faultModelId;
	}

	public void setFaultModelId(String faultModelId) {
		this.faultModelId = faultModelId;
	}

	public String getFmeaCode() {
		return fmeaCode;
	}

	public void setFmeaCode(String fmeaCode) {
		this.fmeaCode = fmeaCode;
	}

	public String getFmeaName() {
		return fmeaName;
	}

	public void setFmeaName(String fmeaName) {
		this.fmeaName = fmeaName;
	}

	public String getFmeaIndexCode() {
		return fmeaIndexCode;
	}

	public void setFmeaIndexCode(String fmeaIndexCode) {
		this.fmeaIndexCode = fmeaIndexCode;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

}