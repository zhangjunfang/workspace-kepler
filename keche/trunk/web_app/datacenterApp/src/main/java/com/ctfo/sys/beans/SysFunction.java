package com.ctfo.sys.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 角色权限<br>
 * 描述： 角色权限<br>
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
 * <td>2014-5-21</td>
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
	private static final long serialVersionUID = 494835172340955893L;

	/** 自定义编码 */
	private String funId;

	/** 功能名称 */
	private String funName;

	/** 菜单英文名 */
	private String funEname;

	/** 功能对应地址 */
	private String funUri;

	/** 功能图标：对应的图标文件地址 */
	private String funImg;

	/** 上一级功能编号 */
	private String funParentId;

	/** 功能适用范围0-系统功能,1-表示BS的功能,2-表示CS的功能,3-车厂功能,4-管理报表,5-手机客户端,6-旅游,7-主中心 */
	private String funCbs;

	/** 菜单层次0-9 */
	private Short funLevel;

	/** 执行方法名 */
	private String funIdx;

	/** 功能状态0：未启用1：启用 */
	private Short funFlag;

	/** 是否为能运行功能1：可运行功能0：不可运行功能，属于菜单 */
	private Short funRun;

	/** 备注 */
	private String mem;

	/** 创建人id */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 修改人id */
	private String updateBy;

	/** 修改时间 */
	private Long updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	// 附加信息
	/** 是否被选中 */
	private String isChecked;

	public String getFunId() {
		return funId;
	}

	public void setFunId(String funId) {
		this.funId = funId;
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

	public String getFunParentId() {
		return funParentId;
	}

	public void setFunParentId(String funParentId) {
		this.funParentId = funParentId;
	}

	public String getFunCbs() {
		return funCbs;
	}

	public void setFunCbs(String funCbs) {
		this.funCbs = funCbs;
	}

	public Short getFunLevel() {
		return funLevel;
	}

	public void setFunLevel(Short funLevel) {
		this.funLevel = funLevel;
	}

	public String getFunIdx() {
		return funIdx;
	}

	public void setFunIdx(String funIdx) {
		this.funIdx = funIdx;
	}

	public Short getFunFlag() {
		return funFlag;
	}

	public void setFunFlag(Short funFlag) {
		this.funFlag = funFlag;
	}

	public Short getFunRun() {
		return funRun;
	}

	public void setFunRun(Short funRun) {
		this.funRun = funRun;
	}

	public String getMem() {
		return mem;
	}

	public void setMem(String mem) {
		this.mem = mem;
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

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getIsChecked() {
		return isChecked;
	}

	public void setIsChecked(String isChecked) {
		this.isChecked = isChecked;
	}

}