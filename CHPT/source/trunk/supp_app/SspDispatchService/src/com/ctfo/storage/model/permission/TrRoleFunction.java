package com.ctfo.storage.model.permission;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 角色与系统功能关系<br>
 * 描述： 角色与系统功能关系<br>
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
 * <td>2014-11-4</td>
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
public class TrRoleFunction implements Serializable {

	/** */
	private static final long serialVersionUID = -5904320436994380091L;

	/** role_fun_id */
	private String roleFunId;

	/** 角色ID，关联角色表 */
	private String roleId;

	/** 系统功能ID，关联系统功能表 */
	private String funId;

	/** 按钮--浏览 0无，1有 */
	private String buttonBrowse;

	/** 按钮--保存 0无，1有 */
	private String buttonSave;

	/** 按钮--新增 0无，1有 */
	private String buttonAdd;

	/** 按钮--编辑 0无，1有 */
	private String buttonEdit;

	/** 按钮--复制 0无，1有 */
	private String buttonCopy;

	/** 按钮--删除 0无，1有 */
	private String buttonDelete;

	/** 按钮--作废 0无，1有 */
	private String buttonCancel;

	/** 按钮--激活 0无，1有 */
	private String buttonActivate;

	/** 按钮--启用 0无，1有 */
	private String buttonEnable;

	/** 按钮--停用 0无，1有 */
	private String buttonDisable;

	/** 按钮--提交 0无，1有 */
	private String buttonSubmit;

	/** 按钮--导入 0无，1有 */
	private String buttonImport;

	/** 按钮--导出 0无，1有 */
	private String buttonExport;

	/** 按钮--打印 0无，1有 */
	private String buttonPrint;

	/** 按钮--操作记录 0无，1有 */
	private String buttonOperationRecord;

	/** 按钮--派工 0无，1有 */
	private String buttonDispatching;

	/** 按钮--结算 0无，1有 */
	private String buttonSettleAccounts;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getRoleFunId() {
		return roleFunId;
	}

	public void setRoleFunId(String roleFunId) {
		this.roleFunId = roleFunId;
	}

	public String getRoleId() {
		return roleId;
	}

	public void setRoleId(String roleId) {
		this.roleId = roleId;
	}

	public String getFunId() {
		return funId;
	}

	public void setFunId(String funId) {
		this.funId = funId;
	}

	public String getButtonBrowse() {
		return buttonBrowse;
	}

	public void setButtonBrowse(String buttonBrowse) {
		this.buttonBrowse = buttonBrowse;
	}

	public String getButtonSave() {
		return buttonSave;
	}

	public void setButtonSave(String buttonSave) {
		this.buttonSave = buttonSave;
	}

	public String getButtonAdd() {
		return buttonAdd;
	}

	public void setButtonAdd(String buttonAdd) {
		this.buttonAdd = buttonAdd;
	}

	public String getButtonEdit() {
		return buttonEdit;
	}

	public void setButtonEdit(String buttonEdit) {
		this.buttonEdit = buttonEdit;
	}

	public String getButtonCopy() {
		return buttonCopy;
	}

	public void setButtonCopy(String buttonCopy) {
		this.buttonCopy = buttonCopy;
	}

	public String getButtonDelete() {
		return buttonDelete;
	}

	public void setButtonDelete(String buttonDelete) {
		this.buttonDelete = buttonDelete;
	}

	public String getButtonCancel() {
		return buttonCancel;
	}

	public void setButtonCancel(String buttonCancel) {
		this.buttonCancel = buttonCancel;
	}

	public String getButtonActivate() {
		return buttonActivate;
	}

	public void setButtonActivate(String buttonActivate) {
		this.buttonActivate = buttonActivate;
	}

	public String getButtonEnable() {
		return buttonEnable;
	}

	public void setButtonEnable(String buttonEnable) {
		this.buttonEnable = buttonEnable;
	}

	public String getButtonDisable() {
		return buttonDisable;
	}

	public void setButtonDisable(String buttonDisable) {
		this.buttonDisable = buttonDisable;
	}

	public String getButtonSubmit() {
		return buttonSubmit;
	}

	public void setButtonSubmit(String buttonSubmit) {
		this.buttonSubmit = buttonSubmit;
	}

	public String getButtonImport() {
		return buttonImport;
	}

	public void setButtonImport(String buttonImport) {
		this.buttonImport = buttonImport;
	}

	public String getButtonExport() {
		return buttonExport;
	}

	public void setButtonExport(String buttonExport) {
		this.buttonExport = buttonExport;
	}

	public String getButtonPrint() {
		return buttonPrint;
	}

	public void setButtonPrint(String buttonPrint) {
		this.buttonPrint = buttonPrint;
	}

	public String getButtonOperationRecord() {
		return buttonOperationRecord;
	}

	public void setButtonOperationRecord(String buttonOperationRecord) {
		this.buttonOperationRecord = buttonOperationRecord;
	}

	public String getButtonDispatching() {
		return buttonDispatching;
	}

	public void setButtonDispatching(String buttonDispatching) {
		this.buttonDispatching = buttonDispatching;
	}

	public String getButtonSettleAccounts() {
		return buttonSettleAccounts;
	}

	public void setButtonSettleAccounts(String buttonSettleAccounts) {
		this.buttonSettleAccounts = buttonSettleAccounts;
	}

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

}