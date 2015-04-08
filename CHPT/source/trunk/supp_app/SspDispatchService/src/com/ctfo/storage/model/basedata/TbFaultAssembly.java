package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 故障总成<br>
 * 描述： 故障总成 <br>
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
public class TbFaultAssembly implements Serializable {

	/** */
	private static final long serialVersionUID = -2498146359942728018L;

	/** id */
	private String faultAssemblyId;

	/** 总成代码 总成代码 */
	private String assemblyCode;

	/** 总成名称 */
	private String assemblyName;

	/** 总成检索码 */
	private String assemblyIndexCode;

	/** 故障分类 */
	private String faultClassId;

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

	public String getFaultAssemblyId() {
		return faultAssemblyId;
	}

	public void setFaultAssemblyId(String faultAssemblyId) {
		this.faultAssemblyId = faultAssemblyId;
	}

	public String getAssemblyCode() {
		return assemblyCode;
	}

	public void setAssemblyCode(String assemblyCode) {
		this.assemblyCode = assemblyCode;
	}

	public String getAssemblyName() {
		return assemblyName;
	}

	public void setAssemblyName(String assemblyName) {
		this.assemblyName = assemblyName;
	}

	public String getAssemblyIndexCode() {
		return assemblyIndexCode;
	}

	public void setAssemblyIndexCode(String assemblyIndexCode) {
		this.assemblyIndexCode = assemblyIndexCode;
	}

	public String getFaultClassId() {
		return faultClassId;
	}

	public void setFaultClassId(String faultClassId) {
		this.faultClassId = faultClassId;
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