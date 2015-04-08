package com.ctfo.sys.beans;

import java.io.Serializable;
import java.util.List;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织树<br>
 * 描述： 组织树<br>
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
 * <td>2014-6-6</td>
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
public class StructureOrgTree implements Serializable {

	/** */
	private static final long serialVersionUID = 957077231894215025L;

	/** 组织id */
	private String entId;

	/** 组织名称或者分中心名称 */
	private String entName;

	/** 实体类型，1为企业，2为车队 */
	private Integer entType;

	/** 父节点ID，根节点为-1 */
	private String parentId;

	/** 所属分中心编码 */
	private String centerCode;

	/** 所属分中心名称 */
	private String centerName;

	/** 组织编码 */
	private String corpCode;

	/** 所属省市 */
	private String corpProvince;

	/** 子节点List */
	private List<StructureOrgTree> nodeList;

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public Integer getEntType() {
		return entType;
	}

	public void setEntType(Integer entType) {
		this.entType = entType;
	}

	public String getParentId() {
		return parentId;
	}

	public void setParentId(String parentId) {
		this.parentId = parentId;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getCenterName() {
		return centerName;
	}

	public void setCenterName(String centerName) {
		this.centerName = centerName;
	}

	public String getCorpCode() {
		return corpCode;
	}

	public void setCorpCode(String corpCode) {
		this.corpCode = corpCode;
	}

	public List<StructureOrgTree> getNodeList() {
		return nodeList;
	}

	public void setNodeList(List<StructureOrgTree> nodeList) {
		this.nodeList = nodeList;
	}

	public String getCorpProvince() {
		return corpProvince;
	}

	public void setCorpProvince(String corpProvince) {
		this.corpProvince = corpProvince;
	}

}
