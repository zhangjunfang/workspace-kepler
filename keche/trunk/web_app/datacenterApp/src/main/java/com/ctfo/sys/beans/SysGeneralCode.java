package com.ctfo.sys.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 字典码表<br>
 * 描述： 字典码表<br>
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
 * <td>2014-6-3</td>
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
public class SysGeneralCode implements Serializable {

	/** */
	private static final long serialVersionUID = 4093649208123206822L;

	/** 主键 */
	private String autoId;

	/** 编码编号 */
	private String generalCode;

	/** 编码名称 */
	private String codeName;

	/** 编码描述 */
	private String codeDesc;

	/** 所属编码 */
	private String parentGeneralCode;

	/** 编码序号（同编码编号） */
	private Short codeIdx;

	/** 编码级别(0 编码分类 1编码) */
	private Short codeLevel;

	/** 有效标记1-有效0-无效 默认为1 */
	private String enableFlag;

	public String getAutoId() {
		return autoId;
	}

	public void setAutoId(String autoId) {
		this.autoId = autoId;
	}

	public String getGeneralCode() {
		return generalCode;
	}

	public void setGeneralCode(String generalCode) {
		this.generalCode = generalCode;
	}

	public String getCodeName() {
		return codeName;
	}

	public void setCodeName(String codeName) {
		this.codeName = codeName;
	}

	public String getCodeDesc() {
		return codeDesc;
	}

	public void setCodeDesc(String codeDesc) {
		this.codeDesc = codeDesc;
	}

	public String getParentGeneralCode() {
		return parentGeneralCode;
	}

	public void setParentGeneralCode(String parentGeneralCode) {
		this.parentGeneralCode = parentGeneralCode;
	}

	public Short getCodeIdx() {
		return codeIdx;
	}

	public void setCodeIdx(Short codeIdx) {
		this.codeIdx = codeIdx;
	}

	public Short getCodeLevel() {
		return codeLevel;
	}

	public void setCodeLevel(Short codeLevel) {
		this.codeLevel = codeLevel;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

}
