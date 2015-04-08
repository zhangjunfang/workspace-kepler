package com.ctfo.sys.beans;

public class TbOnOff {
	 /**
     * tb_on_off.on_off_id
     * 开关表主键ID
     */
    private String onOffId;

    /**
     * tb_on_off.auto_matic_status
     * 自动授权状态
     */
    private String autoMaticStatus;

    /**
     * tb_on_off.announce_audit_status
     * 公告审核状态
     */
    private String announceAuditStatus;

    /**
     * tb_on_off.permission_audit
     * 权限审核状态
     */
    private String permissionAudit;

    public String getOnOffId() {
        return onOffId;
    }

    public void setOnOffId(String onOffId) {
        this.onOffId = onOffId;
    }

    public String getAutoMaticStatus() {
        return autoMaticStatus;
    }

    public void setAutoMaticStatus(String autoMaticStatus) {
        this.autoMaticStatus = autoMaticStatus;
    }

    public String getAnnounceAuditStatus() {
        return announceAuditStatus;
    }

    public void setAnnounceAuditStatus(String announceAuditStatus) {
        this.announceAuditStatus = announceAuditStatus;
    }

    public String getPermissionAudit() {
        return permissionAudit;
    }

    public void setPermissionAudit(String permissionAudit) {
        this.permissionAudit = permissionAudit;
    }
}
