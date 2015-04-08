package com.ctfo.storage.model.support;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-12-4</td>
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
public class TbUserOnline implements Serializable {

	/** */
	private static final long serialVersionUID = -5668463195636725828L;

	/** 用户在线ID */
	private String tbUserOnlineId;

	/** 公司编码，关联公司表 */
	private String comCode;

	/** 公司名称 */
	private String comName;

	/** 帐套编码 */
	private String setbookId;

	/** 帐套名称 */
	private String setbookName;

	/** CS的客户端账号 */
	private String clientAccount;

	/** 姓名 */
	private String realName;

	/** CS的客户端登陆人所属组织 */
	private String clientAccountOrgid;

	/** 角色名称 */
	private String roleName;

	/** 是否操作员1是0不是 */
	private String isOperater;

	/** 注册时间 */
	private Long regTime;

	/** 登陆时间(指CS端C登陆到S的时间) */
	private Long loadTime;

	/** 登陆IP地址 */
	private String loadIdAddr;

	/** 在线状态 */
	private String onlineStatus;

	/** cs端的客户端的mac地址 */
	private String clientMac;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getTbUserOnlineId() {
		return tbUserOnlineId;
	}

	public void setTbUserOnlineId(String tbUserOnlineId) {
		this.tbUserOnlineId = tbUserOnlineId;
	}

	public String getComCode() {
		return comCode;
	}

	public void setComCode(String comCode) {
		this.comCode = comCode;
	}

	public String getSetbookId() {
		return setbookId;
	}

	public void setSetbookId(String setbookId) {
		this.setbookId = setbookId;
	}

	public String getSetbookName() {
		return setbookName;
	}

	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}

	public String getComName() {
		return comName;
	}

	public void setComName(String comName) {
		this.comName = comName;
	}

	public String getClientAccount() {
		return clientAccount;
	}

	public void setClientAccount(String clientAccount) {
		this.clientAccount = clientAccount;
	}

	public String getRealName() {
		return realName;
	}

	public void setRealName(String realName) {
		this.realName = realName;
	}

	public String getClientAccountOrgid() {
		return clientAccountOrgid;
	}

	public void setClientAccountOrgid(String clientAccountOrgid) {
		this.clientAccountOrgid = clientAccountOrgid;
	}

	public String getRoleName() {
		return roleName;
	}

	public void setRoleName(String roleName) {
		this.roleName = roleName;
	}

	public String getIsOperater() {
		return isOperater;
	}

	public void setIsOperater(String isOperater) {
		this.isOperater = isOperater;
	}

	public Long getRegTime() {
		return regTime;
	}

	public void setRegTime(Long regTime) {
		this.regTime = regTime;
	}

	public Long getLoadTime() {
		return loadTime;
	}

	public void setLoadTime(Long loadTime) {
		this.loadTime = loadTime;
	}

	public String getLoadIdAddr() {
		return loadIdAddr;
	}

	public void setLoadIdAddr(String loadIdAddr) {
		this.loadIdAddr = loadIdAddr;
	}

	public String getOnlineStatus() {
		return onlineStatus;
	}

	public void setOnlineStatus(String onlineStatus) {
		this.onlineStatus = onlineStatus;
	}

	public String getClientMac() {
		return clientMac;
	}

	public void setClientMac(String clientMac) {
		this.clientMac = clientMac;
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
