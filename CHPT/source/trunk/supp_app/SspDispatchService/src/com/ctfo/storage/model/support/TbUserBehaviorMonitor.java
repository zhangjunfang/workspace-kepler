package com.ctfo.storage.model.support;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 用户行为监控<br>
 * 描述： 用户行为监控<br>
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
public class TbUserBehaviorMonitor implements Serializable {

	/** */
	private static final long serialVersionUID = 1331090424188492990L;

	/** 用户操作记录表id */
	private String uOperId;

	/** 公司名称 */
	private String comName;

	/** 帐套 */
	private String setbookName;

	/** 账号 */
	private String clientAccount;

	/** 角色 */
	private String roleName;

	/** 所属组织 */
	private String orgName;

	/** IP地址 */
	private String loadIdAddr;

	/** mac地址 */
	private String clientMac;

	/** 监控时间 */
	private Long watchTime;

	/** 在线行为类型 */
	private String onlineType;

	public String getUOperId() {
		return uOperId;
	}

	public void setUOperId(String uOperId) {
		this.uOperId = uOperId;
	}

	public String getComName() {
		return comName;
	}

	public void setComName(String comName) {
		this.comName = comName;
	}

	public String getSetbookName() {
		return setbookName;
	}

	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}

	public String getClientAccount() {
		return clientAccount;
	}

	public void setClientAccount(String clientAccount) {
		this.clientAccount = clientAccount;
	}

	public String getRoleName() {
		return roleName;
	}

	public void setRoleName(String roleName) {
		this.roleName = roleName;
	}

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
	}

	public String getLoadIdAddr() {
		return loadIdAddr;
	}

	public void setLoadIdAddr(String loadIdAddr) {
		this.loadIdAddr = loadIdAddr;
	}

	public String getClientMac() {
		return clientMac;
	}

	public void setClientMac(String clientMac) {
		this.clientMac = clientMac;
	}

	public Long getWatchTime() {
		return watchTime;
	}

	public void setWatchTime(Long watchTime) {
		this.watchTime = watchTime;
	}

	public String getOnlineType() {
		return onlineType;
	}

	public void setOnlineType(String onlineType) {
		this.onlineType = onlineType;
	}

}
