package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 故障总成部件<br>
 * 描述： 故障总成部件<br>
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
public class TbFaultComponent implements Serializable {

	/** */
	private static final long serialVersionUID = 6863816371738639459L;

	/** id */
	private String faultComponentId;

	/** 部件代码 */
	private String partCode;

	/** 部件名称 */
	private String partName;

	/** 部件检索码 */
	private String partIndexCode;

	/** 故障总成 */
	private String faultAssemblyId;

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

	public String getFaultComponentId() {
		return faultComponentId;
	}

	public void setFaultComponentId(String faultComponentId) {
		this.faultComponentId = faultComponentId;
	}

	public String getPartCode() {
		return partCode;
	}

	public void setPartCode(String partCode) {
		this.partCode = partCode;
	}

	public String getPartName() {
		return partName;
	}

	public void setPartName(String partName) {
		this.partName = partName;
	}

	public String getPartIndexCode() {
		return partIndexCode;
	}

	public void setPartIndexCode(String partIndexCode) {
		this.partIndexCode = partIndexCode;
	}

	public String getFaultAssemblyId() {
		return faultAssemblyId;
	}

	public void setFaultAssemblyId(String faultAssemblyId) {
		this.faultAssemblyId = faultAssemblyId;
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