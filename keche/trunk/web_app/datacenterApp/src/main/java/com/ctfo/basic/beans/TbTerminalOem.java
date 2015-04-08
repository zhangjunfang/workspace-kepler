package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：终端厂家<br>
 * 描述：终端厂家<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年6月6日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbTerminalOem implements Serializable  {

    private static final long serialVersionUID = 7518106771288912954L;

    /** 终端厂家编码*/
    private String oemCode;

    /** 终端厂家名称*/
    private String fullName;

    /** 厂家简称*/
    private String shortName;

    /** 生产厂家地址*/
    private String address;

    /** 厂家简介*/
    private String oemDesc;

    /** 厂家类型*/
    private String oemType;

    /** 所属国家*/
    private String enterpriseCountry;

    /** 所属省*/
    private String enterpriseProvince;

    /** 所属市*/
    private String enterpriseCity;

    /** 联系地址*/
    private String concateAddress;

    /** 网址*/
    private String webAddress;

    /** 企业法人*/
    private String boss;

    /** 邮编*/
    private String zipCode;

    /** 联系人*/
    private String concatePerson;

    /** 联系人手机*/
    private String cellphone;

    /** 联系人固定电话*/
    private String tel;

    /** 联系人邮箱*/
    private String email;

    /** 创建人*/
    private long createBy;

    /** 创建时间*/
    private long createTime;

    /** 修改人*/
    private long updateBy;

    /** 传真号*/
    private String fax;

    /** 修改时间*/
    private long updateTime;

    /** 有效标记 1:有效 0:无效 默认为1*/
    private String enableFlag;




    public String getOemCode() {
        return oemCode;
    }

    public void setOemCode(String oemCode) {
        this.oemCode = oemCode;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getShortName() {
        return shortName;
    }

    public void setShortName(String shortName) {
        this.shortName = shortName;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getOemDesc() {
        return oemDesc;
    }

    public void setOemDesc(String oemDesc) {
        this.oemDesc = oemDesc;
    }

    public String getOemType() {
        return oemType;
    }

    public void setOemType(String oemType) {
        this.oemType = oemType;
    }

    public String getEnterpriseCountry() {
        return enterpriseCountry;
    }

    public void setEnterpriseCountry(String enterpriseCountry) {
        this.enterpriseCountry = enterpriseCountry;
    }

    public String getEnterpriseProvince() {
        return enterpriseProvince;
    }

    public void setEnterpriseProvince(String enterpriseProvince) {
        this.enterpriseProvince = enterpriseProvince;
    }

    public String getEnterpriseCity() {
        return enterpriseCity;
    }

    public void setEnterpriseCity(String enterpriseCity) {
        this.enterpriseCity = enterpriseCity;
    }

    public String getConcateAddress() {
        return concateAddress;
    }

    public void setConcateAddress(String concateAddress) {
        this.concateAddress = concateAddress;
    }

    public String getWebAddress() {
        return webAddress;
    }

    public void setWebAddress(String webAddress) {
        this.webAddress = webAddress;
    }

    public String getBoss() {
        return boss;
    }

    public void setBoss(String boss) {
        this.boss = boss;
    }

    public String getZipCode() {
        return zipCode;
    }

    public void setZipCode(String zipCode) {
        this.zipCode = zipCode;
    }

    public String getConcatePerson() {
        return concatePerson;
    }

    public void setConcatePerson(String concatePerson) {
        this.concatePerson = concatePerson;
    }

    public String getCellphone() {
        return cellphone;
    }

    public void setCellphone(String cellphone) {
        this.cellphone = cellphone;
    }

    public String getTel() {
        return tel;
    }

    public void setTel(String tel) {
        this.tel = tel;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
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

    public String getFax() {
        return fax;
    }

    public void setFax(String fax) {
        this.fax = fax;
    }

    public long getUpdateTime() {
        return updateTime;
    }

    public void setUpdateTime(long updateTime) {
        this.updateTime = updateTime;
    }

    public String getEnableFlag() {
        return enableFlag;
    }

    public void setEnableFlag(String enableFlag) {
        this.enableFlag = enableFlag;
    }



}

