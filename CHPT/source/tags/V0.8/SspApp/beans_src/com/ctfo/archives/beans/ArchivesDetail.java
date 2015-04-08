package com.ctfo.archives.beans;

import java.io.Serializable;

public class ArchivesDetail implements Serializable{
   
	/**
	 * 
	 */
	private static final long serialVersionUID = -6961309191108916589L;
	private String comName;
	/**
     * sys_user.user_id
     * 
     */
    private String userId;

    /**
     * sys_user.org_id
     * 
     */
    private String orgId;

    /**
     * sys_user.user_code
     * 
     */
    private String userCode;

    /**
     * sys_user.user_name
     * 
     */
    private String userName;

    /**
     * sys_user.land_name
     * 
     */
    private String landName;

    /**
     * sys_user.sex
     * 
     */
    private String sex;

    /**
     * sys_user.nation
     * 
     */
    private String nation;

    /**
     * sys_user.birthday
     * 
     */
    private Long birthday;

    /**
     * sys_user.idcard_type
     * 
     */
    private String idcardType;

    /**
     * sys_user.idcard_num
     * 
     */
    private String idcardNum;

    /**
     * sys_user.register_address
     * 
     */
    private String registerAddress;

    /**
     * sys_user.native_place
     * 
     */
    private String nativePlace;

    /**
     * sys_user.political_status
     * 
     */
    private String politicalStatus;

    /**
     * sys_user.user_phone
     * 
     */
    private String userPhone;

    /**
     * sys_user.user_telephone
     * 
     */
    private String userTelephone;

    /**
     * sys_user.user_fax
     * 
     */
    private String userFax;

    /**
     * sys_user.user_email
     * 
     */
    private String userEmail;

    /**
     * sys_user.user_address
     * 
     */
    private String userAddress;

    /**
     * sys_user.user_height
     * 
     */
    private String userHeight;

    /**
     * sys_user.user_weight
     * 
     */
    private String userWeight;

    /**
     * sys_user.entry_date
     * 
     */
    private Long entryDate;

    /**
     * sys_user.post
     * 
     */
    private String post;

    /**
     * sys_user.position
     * 
     */
    private String position;

    /**
     * sys_user.level
     * 
     */
    private String level;

    /**
     * sys_user.graduate_institutions
     * 
     */
    private String graduateInstitutions;

    /**
     * sys_user.specialty
     * 
     */
    private String specialty;

    /**
     * sys_user.education
     * 
     */
    private String education;

    /**
     * sys_user.graduate_date
     * 
     */
    private Long graduateDate;

    /**
     * sys_user.technical_expertise
     * 
     */
    private String technicalExpertise;

    /**
     * sys_user.wage
     * 
     */
    private String wage;

    /**
     * sys_user.is_operator
     * 
     */
    private String isOperator;

    /**
     * sys_user.password
     * 
     */
    private String password;

    /**
     * sys_user.enable_flag
     * 
     */
    private String enableFlag;

    /**
     * sys_user.remark
     * 
     */
    private String remark;

    /**
     * sys_user.status
     * 
     */
    private String status;

    /**
     * sys_user.data_sources
     * 
     */
    private String dataSources;

    /**
     * sys_user.create_by
     * 
     */
    private String createBy;

    /**
     * sys_user.create_time
     * 
     */
    private Long createTime;

    /**
     * sys_user.update_by
     * 
     */
    private String updateBy;

    /**
     * sys_user.update_time
     * 
     */
    private Long updateTime;
    
    
    private String roleName;

    private String orgName;
    
    private String setbookName;
    
    private Long loginTime;
    
    private String health;
    
    
    
    public String getHealth() {
		return health;
	}

	public void setHealth(String health) {
		this.health = health;
	}

	public Long getLoginTime() {
		return loginTime;
	}

	public void setLoginTime(Long loginTime) {
		this.loginTime = loginTime;
	}

	public String getSetbookName() {
		return setbookName;
	}

	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
	}

	public String getRoleName() {
		return roleName;
	}

	public void setRoleName(String roleName) {
		this.roleName = roleName;
	}

	public String getComName() {
		return comName;
	}

	public void setComName(String comName) {
		this.comName = comName;
	}

	public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getOrgId() {
        return orgId;
    }

    public void setOrgId(String orgId) {
        this.orgId = orgId;
    }

    public String getUserCode() {
        return userCode;
    }

    public void setUserCode(String userCode) {
        this.userCode = userCode;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getLandName() {
        return landName;
    }

    public void setLandName(String landName) {
        this.landName = landName;
    }

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    public String getNation() {
        return nation;
    }

    public void setNation(String nation) {
        this.nation = nation;
    }

    public Long getBirthday() {
        return birthday;
    }

    public void setBirthday(Long birthday) {
        this.birthday = birthday;
    }

    public String getIdcardType() {
        return idcardType;
    }

    public void setIdcardType(String idcardType) {
        this.idcardType = idcardType;
    }

    public String getIdcardNum() {
        return idcardNum;
    }

    public void setIdcardNum(String idcardNum) {
        this.idcardNum = idcardNum;
    }

    public String getRegisterAddress() {
        return registerAddress;
    }

    public void setRegisterAddress(String registerAddress) {
        this.registerAddress = registerAddress;
    }

    public String getNativePlace() {
        return nativePlace;
    }

    public void setNativePlace(String nativePlace) {
        this.nativePlace = nativePlace;
    }

    public String getPoliticalStatus() {
        return politicalStatus;
    }

    public void setPoliticalStatus(String politicalStatus) {
        this.politicalStatus = politicalStatus;
    }

    public String getUserPhone() {
        return userPhone;
    }

    public void setUserPhone(String userPhone) {
        this.userPhone = userPhone;
    }

    public String getUserTelephone() {
        return userTelephone;
    }

    public void setUserTelephone(String userTelephone) {
        this.userTelephone = userTelephone;
    }

    public String getUserFax() {
        return userFax;
    }

    public void setUserFax(String userFax) {
        this.userFax = userFax;
    }

    public String getUserEmail() {
        return userEmail;
    }

    public void setUserEmail(String userEmail) {
        this.userEmail = userEmail;
    }

    public String getUserAddress() {
        return userAddress;
    }

    public void setUserAddress(String userAddress) {
        this.userAddress = userAddress;
    }

    public String getUserHeight() {
        return userHeight;
    }

    public void setUserHeight(String userHeight) {
        this.userHeight = userHeight;
    }

    public String getUserWeight() {
        return userWeight;
    }

    public void setUserWeight(String userWeight) {
        this.userWeight = userWeight;
    }

    public Long getEntryDate() {
        return entryDate;
    }

    public void setEntryDate(Long entryDate) {
        this.entryDate = entryDate;
    }

    public String getPost() {
        return post;
    }

    public void setPost(String post) {
        this.post = post;
    }

    public String getPosition() {
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public String getLevel() {
        return level;
    }

    public void setLevel(String level) {
        this.level = level;
    }

    public String getGraduateInstitutions() {
        return graduateInstitutions;
    }

    public void setGraduateInstitutions(String graduateInstitutions) {
        this.graduateInstitutions = graduateInstitutions;
    }

    public String getSpecialty() {
        return specialty;
    }

    public void setSpecialty(String specialty) {
        this.specialty = specialty;
    }

    public String getEducation() {
        return education;
    }

    public void setEducation(String education) {
        this.education = education;
    }

    public Long getGraduateDate() {
        return graduateDate;
    }

    public void setGraduateDate(Long graduateDate) {
        this.graduateDate = graduateDate;
    }

    public String getTechnicalExpertise() {
        return technicalExpertise;
    }

    public void setTechnicalExpertise(String technicalExpertise) {
        this.technicalExpertise = technicalExpertise;
    }

    public String getWage() {
        return wage;
    }

    public void setWage(String wage) {
        this.wage = wage;
    }

    public String getIsOperator() {
        return isOperator;
    }

    public void setIsOperator(String isOperator) {
        this.isOperator = isOperator;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
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

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getDataSources() {
        return dataSources;
    }

    public void setDataSources(String dataSources) {
        this.dataSources = dataSources;
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
}
