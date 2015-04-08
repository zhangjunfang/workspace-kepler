package com.ctfo.monitor.beans;

import java.io.Serializable;

public class OnlineUsers implements Serializable{
	 /**
	 * 
	 */
	private static final long serialVersionUID = 5027429168797804007L;

	/**
     * tb_user_online.tb_user_online_id
     * 用户在线ID
     */
    private String tbUserOnlineId;

    /**
     * tb_user_online.com_code
     * 公司编码，关联公司表
     */
    private String comCode;

    /**
     * tb_user_online.com_name
     * 公司名称
     */
    private String comName;

    /**
     * tb_user_online.setbook_id
     * 帐套编码
     */
    private String setbookId;

    /**
     * tb_user_online.setbook_name
     * 帐套名称
     */
    private String setbookName;

    /**
     * tb_user_online.client_account
     * CS的客户端账号
     */
    private String clientAccount;

    /**
     * tb_user_online.real_name
     * 姓名
     */
    private String realName;

    /**
     * tb_user_online.client_account_orgid
     * CS的客户端登陆人所属组织
     */
    private String clientAccountOrgid;

    /**
     * tb_user_online.role_name
     * 角色名称
     */
    private String roleName;

    /**
     * tb_user_online.is_operater
     * 是否操作员1是0不是
     */
    private String isOperater;

    /**
     * tb_user_online.reg_time
     * 注册时间
     */
    private Long regTime;

    /**
     * tb_user_online.load_time
     * 登陆时间(指CS端C登陆到S的时间)
     */
    private Long loadTime;

    /**
     * tb_user_online.load_id_addr
     * 登陆IP地址
     */
    private String loadIdAddr;

    /**
     * tb_user_online.online_status
     * 在线状态
     */
    private String onlineStatus;

    /**
     * 在线时长
     */
    private String onlineTime;
    

	public String getOnlineTime() {
		return onlineTime;
	}

	public void setOnlineTime(String onlineTime) {
		this.onlineTime = onlineTime;
	}

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

    public String getComName() {
        return comName;
    }

    public void setComName(String comName) {
        this.comName = comName;
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
}
