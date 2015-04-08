package com.ctfo.sys.beans;

public class SysComInfo {
    /**
     * sys_com_info.com_id
     * 公司id
     */
    private String comId;

    /**
     * sys_com_info.com_code
     * 公司编码
     */
    private long comCode;

    /**
     * sys_com_info.com_name
     * 公司名称
     */
    private String comName;

    /**
     * sys_com_info.province
     * 省
     */
    private String province;

    /**
     * sys_com_info.city
     * 市
     */
    private String city;

    /**
     * sys_com_info.county
     * 县
     */
    private String county;

    /**
     * sys_com_info.com_fax
     * 传真
     */
    private String comFax;

    /**
     * sys_com_info.zip_code
     * 邮政编码
     */
    private String zipCode;

    /**
     * sys_com_info.com_email
     * 电子邮件
     */
    private String comEmail;

    /**
     * sys_com_info.com_website
     * 网址
     */
    private String comWebsite;

    /**
     * sys_com_info.com_contact
     * 联系人
     */
    private String comContact;

    /**
     * sys_com_info.com_tel
     * 联系电话
     */
    private String comTel;

    /**
     * sys_com_info.status
     * 状态:0吊销；1启用
     */
    private String status;

    /**
     * sys_com_info.remark
     * 备注
     */
    private String remark;

    /**
     * sys_com_info.create_by
     * 创建人
     */
    private String createBy;

    /**
     * sys_com_info.create_time
     * 创建时间
     */
    private Long createTime;

    /**
     * sys_com_info.update_by
     * 最后编辑人
     */
    private String updateBy;

    /**
     * sys_com_info.update_time
     * 最后编辑时间
     */
    private Long updateTime;

    /**
     * sys_com_info.parent_comid
     * 上级公司id
     */
    private String parentComid;

    
    private String comAddress;
    private String comLegal;
    private String comDelete;
    
    
    public String getComDelete() {
		return comDelete;
	}

	public void setComDelete(String comDelete) {
		this.comDelete = comDelete;
	}

	public String getComAddress() {
		return comAddress;
	}

	public void setComAddress(String comAddress) {
		this.comAddress = comAddress;
	}

	public String getComLegal() {
		return comLegal;
	}

	public void setComLegal(String comLegal) {
		this.comLegal = comLegal;
	}

    public String getComId() {
		return comId;
	}

	public void setComId(String comId) {
		this.comId = comId;
	}

    public long getComCode() {
		return comCode;
	}

	public void setComCode(long comCode) {
		this.comCode = comCode;
	}

	public String getComName() {
        return comName;
    }

    public void setComName(String comName) {
        this.comName = comName;
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

    public String getComFax() {
        return comFax;
    }

    public void setComFax(String comFax) {
        this.comFax = comFax;
    }

    public String getZipCode() {
        return zipCode;
    }

    public void setZipCode(String zipCode) {
        this.zipCode = zipCode;
    }

    public String getComEmail() {
        return comEmail;
    }

    public void setComEmail(String comEmail) {
        this.comEmail = comEmail;
    }

    public String getComWebsite() {
        return comWebsite;
    }

    public void setComWebsite(String comWebsite) {
        this.comWebsite = comWebsite;
    }

    public String getComContact() {
        return comContact;
    }

    public void setComContact(String comContact) {
        this.comContact = comContact;
    }

    public String getComTel() {
        return comTel;
    }

    public void setComTel(String comTel) {
        this.comTel = comTel;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
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

    public String getParentComid() {
        return parentComid;
    }

    public void setParentComid(String parentComid) {
        this.parentComid = parentComid;
    }
}
