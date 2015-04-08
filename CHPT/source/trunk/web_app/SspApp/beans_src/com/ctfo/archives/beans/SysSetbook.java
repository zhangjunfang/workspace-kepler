package com.ctfo.archives.beans;

public class SysSetbook {
    /**
     * sys_setbook.id
     * id
     */
    private String id;

    /**
     * sys_setbook.setbook_name
     * 帐套名称
     */
    private String setbookName;

    /**
     * sys_setbook.setbook_code
     * 帐套代码
     */
    private String setbookCode;

    /**
     * sys_setbook.is_main_set_book
     * 是否主帐套
     */
    private String isMainSetBook;

    /**
     * sys_setbook.com_name
     * 所属公司名称
     */
    private String comName;

    /**
     * sys_setbook.organization_code
     * 组织结构代码
     */
    private String organizationCode;

    /**
     * sys_setbook.legal_person
     * 法人
     */
    private String legalPerson;

    /**
     * sys_setbook.opening_bank
     * 开户银行
     */
    private String openingBank;

    /**
     * sys_setbook.bank_account
     * 银行帐号
     */
    private String bankAccount;

    /**
     * sys_setbook.province
     * 所属省，关联字典码表
     */
    private String province;

    /**
     * sys_setbook.city
     * 所属市，关联字典码表
     */
    private String city;

    /**
     * sys_setbook.county
     * 所属区县，关联字典码表
     */
    private String county;

    /**
     * sys_setbook.postal_address
     * 通讯地址
     */
    private String postalAddress;

    /**
     * sys_setbook.zip_code
     * 邮政编码
     */
    private String zipCode;

    /**
     * sys_setbook.company_web_site
     * 公司网址
     */
    private String companyWebSite;

    /**
     * sys_setbook.email
     * 电子邮件
     */
    private String email;

    /**
     * sys_setbook.contact
     * 联系人
     */
    private String contact;

    /**
     * sys_setbook.contact_telephone
     * 联系电话
     */
    private String contactTelephone;

    /**
     * sys_setbook.status
     * 状态   0停用，1启用
     */
    private String status;

    /**
     * sys_setbook.create_by
     * 创建人，关联人员表
     */
    private String createBy;

    /**
     * sys_setbook.create_time
     * 创建时间
     */
    private Long createTime;

    /**
     * sys_setbook.update_by
     * 最后编辑人，关联人员表
     */
    private String updateBy;

    /**
     * sys_setbook.update_time
     * 最后编辑时间
     */
    private Long updateTime;

    /**
     * sys_setbook.ser_station_id
     * 服务站id，云平台用
     */
    private String serStationId;

    /**
     * sys_setbook.set_book_id
     * 帐套id，云平台用
     */
    private String setBookId;

    /**
     * sys_setbook.enable_flag
     * 删除标记，0删除，1未删除
     */
    private String enableFlag;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getSetbookName() {
        return setbookName;
    }

    public void setSetbookName(String setbookName) {
        this.setbookName = setbookName;
    }

    public String getSetbookCode() {
        return setbookCode;
    }

    public void setSetbookCode(String setbookCode) {
        this.setbookCode = setbookCode;
    }

    public String getIsMainSetBook() {
        return isMainSetBook;
    }

    public void setIsMainSetBook(String isMainSetBook) {
        this.isMainSetBook = isMainSetBook;
    }

    public String getComName() {
        return comName;
    }

    public void setComName(String comName) {
        this.comName = comName;
    }

    public String getOrganizationCode() {
        return organizationCode;
    }

    public void setOrganizationCode(String organizationCode) {
        this.organizationCode = organizationCode;
    }

    public String getLegalPerson() {
        return legalPerson;
    }

    public void setLegalPerson(String legalPerson) {
        this.legalPerson = legalPerson;
    }

    public String getOpeningBank() {
        return openingBank;
    }

    public void setOpeningBank(String openingBank) {
        this.openingBank = openingBank;
    }

    public String getBankAccount() {
        return bankAccount;
    }

    public void setBankAccount(String bankAccount) {
        this.bankAccount = bankAccount;
    }

    public String getProvince() {
        return province;
    }

    public void setProvince(String province) {
        this.province = province;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getCounty() {
        return county;
    }

    public void setCounty(String county) {
        this.county = county;
    }

    public String getPostalAddress() {
        return postalAddress;
    }

    public void setPostalAddress(String postalAddress) {
        this.postalAddress = postalAddress;
    }

    public String getZipCode() {
        return zipCode;
    }

    public void setZipCode(String zipCode) {
        this.zipCode = zipCode;
    }

    public String getCompanyWebSite() {
        return companyWebSite;
    }

    public void setCompanyWebSite(String companyWebSite) {
        this.companyWebSite = companyWebSite;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getContact() {
        return contact;
    }

    public void setContact(String contact) {
        this.contact = contact;
    }

    public String getContactTelephone() {
        return contactTelephone;
    }

    public void setContactTelephone(String contactTelephone) {
        this.contactTelephone = contactTelephone;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
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

    public String getEnableFlag() {
        return enableFlag;
    }

    public void setEnableFlag(String enableFlag) {
        this.enableFlag = enableFlag;
    }
}
