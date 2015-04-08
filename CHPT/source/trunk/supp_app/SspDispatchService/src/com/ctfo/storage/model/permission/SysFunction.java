package com.ctfo.storage.model.permission;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 功能菜单表<br>
 * 描述： 功能菜单表<br>
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
public class SysFunction implements Serializable {

	/** */
	private static final long serialVersionUID = 1196651202199647894L;

	/** 自定义编码 */
	private String funId;

	/** 序号 */
	private Integer num;

	/** 功能名称 */
	private String funName;

	/** 菜单英文名 */
	private String funEname;

	/** 功能对应地址 */
	private String funUri;

	/** 对应的图标文件地址 */
	private String funImg;

	/** 上一级功能编号 */
	private String parentId;

	/** 1，服务端功能 2，客户端功能 */
	private String funCbs;

	/** 0-9 */
	private Integer funLevel;

	/** 执行方法 */
	private String funIdx;

	/** 0：未启用1：启用 */
	private Integer funFlag;

	/** 1：可运行功能0：不可 运行功能，属于菜单 */
	private Integer funRun;

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

	/** 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 备注 */
	private String remark;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后修改人，关联人员表 */
	private String updateBy;

	/** 最后修改时间 */
	private Long updateTime;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	private String buttonSync;

	private String buttonBalance;

	private String buttonRevoke;

	private String buttonCommit;

	private String buttonVerify;

	private String buttonStatus;

	private String buttonConfirm;

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

	public String getFunId() {
		return funId;
	}

	public void setFunId(String funId) {
		this.funId = funId;
	}

	public Integer getNum() {
		return num;
	}

	public void setNum(Integer num) {
		this.num = num;
	}

	public String getFunName() {
		return funName;
	}

	public void setFunName(String funName) {
		this.funName = funName;
	}

	public String getFunEname() {
		return funEname;
	}

	public void setFunEname(String funEname) {
		this.funEname = funEname;
	}

	public String getFunUri() {
		return funUri;
	}

	public void setFunUri(String funUri) {
		this.funUri = funUri;
	}

	public String getFunImg() {
		return funImg;
	}

	public void setFunImg(String funImg) {
		this.funImg = funImg;
	}

	public String getParentId() {
		return parentId;
	}

	public void setParentId(String parentId) {
		this.parentId = parentId;
	}

	public String getFunCbs() {
		return funCbs;
	}

	public void setFunCbs(String funCbs) {
		this.funCbs = funCbs;
	}

	public Integer getFunLevel() {
		return funLevel;
	}

	public void setFunLevel(Integer funLevel) {
		this.funLevel = funLevel;
	}

	public String getFunIdx() {
		return funIdx;
	}

	public void setFunIdx(String funIdx) {
		this.funIdx = funIdx;
	}

	public Integer getFunFlag() {
		return funFlag;
	}

	public void setFunFlag(Integer funFlag) {
		this.funFlag = funFlag;
	}

	public Integer getFunRun() {
		return funRun;
	}

	public void setFunRun(Integer funRun) {
		this.funRun = funRun;
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

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
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

	public String getButtonSync() {
		return buttonSync;
	}

	public void setButtonSync(String buttonSync) {
		this.buttonSync = buttonSync;
	}

	public String getButtonBalance() {
		return buttonBalance;
	}

	public void setButtonBalance(String buttonBalance) {
		this.buttonBalance = buttonBalance;
	}

	public String getButtonRevoke() {
		return buttonRevoke;
	}

	public void setButtonRevoke(String buttonRevoke) {
		this.buttonRevoke = buttonRevoke;
	}

	public String getButtonCommit() {
		return buttonCommit;
	}

	public void setButtonCommit(String buttonCommit) {
		this.buttonCommit = buttonCommit;
	}

	public String getButtonVerify() {
		return buttonVerify;
	}

	public void setButtonVerify(String buttonVerify) {
		this.buttonVerify = buttonVerify;
	}

	public String getButtonStatus() {
		return buttonStatus;
	}

	public void setButtonStatus(String buttonStatus) {
		this.buttonStatus = buttonStatus;
	}

	public String getButtonConfirm() {
		return buttonConfirm;
	}

	public void setButtonConfirm(String buttonConfirm) {
		this.buttonConfirm = buttonConfirm;
	}

}