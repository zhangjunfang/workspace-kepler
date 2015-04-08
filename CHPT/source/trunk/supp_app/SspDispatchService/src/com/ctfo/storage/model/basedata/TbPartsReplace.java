package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 替代配件<br>
 * 描述： 替代配件<br>
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
public class TbPartsReplace implements Serializable {

	/** */
	private static final long serialVersionUID = 882271302086229642L;

	/** id */
	private String replaceId;

	/** 配件id */
	private String partsId;

	/** 替代配件ID */
	private String replId;

	/** 替代配件车厂编码--宇通 */
	private String replPartsCode;

	/** 配件替换状态--宇通 */
	private String replPartsStatus;

	/** 替代说明 */
	private String replRemark;

	/** 互换性 */
	private String changes;

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

	public String getReplaceId() {
		return replaceId;
	}

	public void setReplaceId(String replaceId) {
		this.replaceId = replaceId;
	}

	public String getPartsId() {
		return partsId;
	}

	public void setPartsId(String partsId) {
		this.partsId = partsId;
	}

	public String getReplId() {
		return replId;
	}

	public void setReplId(String replId) {
		this.replId = replId;
	}

	public String getReplPartsCode() {
		return replPartsCode;
	}

	public void setReplPartsCode(String replPartsCode) {
		this.replPartsCode = replPartsCode;
	}

	public String getReplPartsStatus() {
		return replPartsStatus;
	}

	public void setReplPartsStatus(String replPartsStatus) {
		this.replPartsStatus = replPartsStatus;
	}

	public String getReplRemark() {
		return replRemark;
	}

	public void setReplRemark(String replRemark) {
		this.replRemark = replRemark;
	}

	public String getChanges() {
		return changes;
	}

	public void setChanges(String changes) {
		this.changes = changes;
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