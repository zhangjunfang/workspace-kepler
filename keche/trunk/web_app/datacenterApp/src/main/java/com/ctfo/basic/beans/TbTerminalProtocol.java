package com.ctfo.basic.beans;

import java.io.Serializable;

public class TbTerminalProtocol implements Serializable  {

    private static final long serialVersionUID = 7205000881787760280L;

    /** 主键，终端协议编码*/
    private String tprotocolId;

    /** 协议版本号*/
    private String tprotocolName;

    /** 所属厂家 参见终端厂家信息表*/
    private String oemCode;

    /** 适配终端设备型号*/
    private String terminalTypeId;

    /** 创建人*/
    private long createBy;

    /** 创建时间*/
    private long createTime;

    /** 修改人*/
    private long updateBy;

    /** 修改时间*/
    private long updateTime;

    /** 终端设备是否有效标记*/
    private String validFlag;

    /** 无效设置用户*/
    private String vasetUserId;

    /** 无效时间*/
    private long vasetTime;




    public String getTprotocolId() {
        return tprotocolId;
    }

    public void setTprotocolId(String tprotocolId) {
        this.tprotocolId = tprotocolId;
    }

    public String getTprotocolName() {
        return tprotocolName;
    }

    public void setTprotocolName(String tprotocolName) {
        this.tprotocolName = tprotocolName;
    }

    public String getOemCode() {
        return oemCode;
    }

    public void setOemCode(String oemCode) {
        this.oemCode = oemCode;
    }

    public String getTerminalTypeId() {
        return terminalTypeId;
    }

    public void setTerminalTypeId(String terminalTypeId) {
        this.terminalTypeId = terminalTypeId;
    }

    public long getCreateBy() {
        return createBy;
    }

    public void setCreateBy(long createBy) {
        this.createBy = createBy;
    }

    public long getCreateTime() {
        return createTime;
    }

    public void setCreateTime(long createTime) {
        this.createTime = createTime;
    }

    public long getUpdateBy() {
        return updateBy;
    }

    public void setUpdateBy(long updateBy) {
        this.updateBy = updateBy;
    }

    public long getUpdateTime() {
        return updateTime;
    }

    public void setUpdateTime(long updateTime) {
        this.updateTime = updateTime;
    }

    public String getValidFlag() {
        return validFlag;
    }

    public void setValidFlag(String validFlag) {
        this.validFlag = validFlag;
    }

    public String getVasetUserId() {
        return vasetUserId;
    }

    public void setVasetUserId(String vasetUserId) {
        this.vasetUserId = vasetUserId;
    }

    public long getVasetTime() {
        return vasetTime;
    }

    public void setVasetTime(long vasetTime) {
        this.vasetTime = vasetTime;
    }



}

