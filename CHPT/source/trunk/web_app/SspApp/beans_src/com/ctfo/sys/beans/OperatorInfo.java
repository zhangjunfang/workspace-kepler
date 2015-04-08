package com.ctfo.sys.beans;

import java.io.Serializable;

import javax.servlet.http.HttpServletRequest;

import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

import com.ctfo.util.BeanAccessUtil;

public class OperatorInfo implements Serializable {
	
		private static final long serialVersionUID = 5207267911718976666L;

		/** */
		private String opId;//用户id

		/** */
		private String entId;//企业id

		/** */
		private String opLoginname;//登录账号

		/** */
		private String opPass;//登录密码

		/** */
		private String opAuthcode;

		/** */
		private String opName;//用户姓名

		/** */
		private String opSuper;

		/** */
		private String opSex;//性别

		/** */
		private Long opBirth;

		/** */
		private String opCountry;

		/** */
		private String opProvince;

		/** */
		private String opCity;

		/** */
		private String opAddress;

		/** */
		private String opMobile;

		/** */
		private String opPhone;

		/** */
		private String opFax;

		/** */
		private String opEmail;

		/** */
		private String opIdentityId;

		/** */
		private String opDuty;

		/** */
		private String opWorkid;

		/** */
		private Long opStartutc;

		/** 账号失效日期 */
		private Long opEndutc;

		/** */
		private String opType;

		/** */
		private String opMem;

		/** */
		private String createBy;

		/** */
		private Long createTime;

		/** */
		private String updateBy;

		/** */
		private Long updateTime;

		/** */
		private String enableFlag;

		/** */
		private String opZip;

		/** */
		private String opStatus;
		
		private String isMember;// 是否是会员 0：不是 1：是

		private String imsi; // IMSI号码
		
		private String photo;// 会员照片
		
		private String opBirthStr;// 用户生日字符串


		private String corpCode;// 企业代码

		private String opUnName;// 创建人名字

		private String entName;// 所属企业名称、

		private String roleName;// 所属角色

		private String roleId;// 角色Id

		private String iPAddress;// 用户登录地址

		private String parentEntId;// 企业父id

		private Integer entType;// 组织类型

		private String entState;// 企业状态

		private String orgLogo;// 企业logo url

		private String seqCode;
		
		private String businessLicense; // 企业经营许可证号

		
		private String httpPhotoPath;// 页面显示路径

		private String loginSessionId;

		/** 用户在线情况使用 **/
		private String sysType;// 登录系统 0|管理系统；1|应用系统

		private Long loginSysTime;// 登录时间

		private Long loginCount;// 标示该账号同时有几处在登录

		private String oldOpPass;// 旧用户密码
		private String updateByStr;
		private String comId;;
		private String isOperator;;
	  
		
        public String getIsOperator() {
			return isOperator;
		}
		public void setIsOperator(String isOperator) {
			this.isOperator = isOperator;
		}
		public String getComId() {
			return comId;
		}
		public void setComId(String comId) {
			this.comId = comId;
		}
		public OperatorInfo(){
			
		}	
        public OperatorInfo(SysSpOperator operator){
        	BeanAccessUtil.copyBeanProperties(this, operator);
		}
		public static OperatorInfo getOperatorInfo(){
			HttpServletRequest request = ((ServletRequestAttributes)RequestContextHolder.getRequestAttributes()).getRequest();
			  OperatorInfo operatorInfo=new OperatorInfo();
			return operatorInfo.getOperatorInfo(request);
		}
		
        public  OperatorInfo getOperatorInfo(HttpServletRequest request){
        	if(request==null)return null;
    		Object obj = request.getSession().getAttribute("SysSpOperator");
    		if(obj instanceof SysSpOperator){
    			BeanAccessUtil.copyBeanProperties(this, (SysSpOperator)obj);
    			return this;
    		}
    		return null;
		}

		public String getOpLoginname() {
			return opLoginname;
		}

		public void setOpLoginname(String opLoginname) {
			this.opLoginname = opLoginname;
		}

		public String getOpPass() {
			return opPass;
		}

		public void setOpPass(String opPass) {
			this.opPass = opPass;
		}

		public String getOpAuthcode() {
			return opAuthcode;
		}

		public void setOpAuthcode(String opAuthcode) {
			this.opAuthcode = opAuthcode;
		}

		public String getOpName() {
			return opName;
		}

		public void setOpName(String opName) {
			this.opName = opName;
		}

		public String getOpSuper() {
			return opSuper;
		}

		public void setOpSuper(String opSuper) {
			this.opSuper = opSuper;
		}

		public String getOpSex() {
			return opSex;
		}

		public void setOpSex(String opSex) {
			this.opSex = opSex;
		}

		public Long getOpBirth() {
			return opBirth;
		}

		public void setOpBirth(Long opBirth) {
			this.opBirth = opBirth;
		}

		public String getOpCountry() {
			return opCountry;
		}

		public void setOpCountry(String opCountry) {
			this.opCountry = opCountry;
		}

		public String getOpProvince() {
			return opProvince;
		}

		public void setOpProvince(String opProvince) {
			this.opProvince = opProvince;
		}

		public String getOpCity() {
			return opCity;
		}

		public void setOpCity(String opCity) {
			this.opCity = opCity;
		}

		public String getOpAddress() {
			return opAddress;
		}

		public void setOpAddress(String opAddress) {
			this.opAddress = opAddress;
		}

		public String getOpMobile() {
			return opMobile;
		}

		public void setOpMobile(String opMobile) {
			this.opMobile = opMobile;
		}

		public String getOpPhone() {
			return opPhone;
		}

		public void setOpPhone(String opPhone) {
			this.opPhone = opPhone;
		}

		public String getOpFax() {
			return opFax;
		}

		public void setOpFax(String opFax) {
			this.opFax = opFax;
		}

		public String getOpEmail() {
			return opEmail;
		}

		public void setOpEmail(String opEmail) {
			this.opEmail = opEmail;
		}

		public String getOpIdentityId() {
			return opIdentityId;
		}

		public void setOpIdentityId(String opIdentityId) {
			this.opIdentityId = opIdentityId;
		}

		public String getOpDuty() {
			return opDuty;
		}

		public void setOpDuty(String opDuty) {
			this.opDuty = opDuty;
		}

		public String getOpWorkid() {
			return opWorkid;
		}

		public void setOpWorkid(String opWorkid) {
			this.opWorkid = opWorkid;
		}

		public Long getOpStartutc() {
			return opStartutc;
		}

		public void setOpStartutc(Long opStartutc) {
			this.opStartutc = opStartutc;
		}

		public Long getOpEndutc() {
			return opEndutc;
		}

		public void setOpEndutc(Long opEndutc) {
			this.opEndutc = opEndutc;
		}

		public String getOpType() {
			return opType;
		}

		public void setOpType(String opType) {
			this.opType = opType;
		}

		public String getOpMem() {
			return opMem;
		}

		public void setOpMem(String opMem) {
			this.opMem = opMem;
		}

		public Long getCreateTime() {
			return createTime;
		}

		public void setCreateTime(Long createTime) {
			this.createTime = createTime;
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

		public String getOpZip() {
			return opZip;
		}

		public void setOpZip(String opZip) {
			this.opZip = opZip;
		}

		public String getOpStatus() {
			return opStatus;
		}

		public void setOpStatus(String opStatus) {
			this.opStatus = opStatus;
		}

		public String getIsMember() {
			return isMember;
		}

		public void setIsMember(String isMember) {
			this.isMember = isMember;
		}

		public String getImsi() {
			return imsi;
		}

		public void setImsi(String imsi) {
			this.imsi = imsi;
		}

		public String getPhoto() {
			return photo;
		}

		public void setPhoto(String photo) {
			this.photo = photo;
		}

		public String getOpBirthStr() {
			return opBirthStr;
		}

		public void setOpBirthStr(String opBirthStr) {
			this.opBirthStr = opBirthStr;
		}

		public String getCorpCode() {
			return corpCode;
		}

		public void setCorpCode(String corpCode) {
			this.corpCode = corpCode;
		}

		public String getOpUnName() {
			return opUnName;
		}

		public void setOpUnName(String opUnName) {
			this.opUnName = opUnName;
		}

		public String getEntName() {
			return entName;
		}

		public void setEntName(String entName) {
			this.entName = entName;
		}

		public String getRoleName() {
			return roleName;
		}

		public void setRoleName(String roleName) {
			this.roleName = roleName;
		}

		public String getiPAddress() {
			return iPAddress;
		}

		public void setiPAddress(String iPAddress) {
			this.iPAddress = iPAddress;
		}

		/**
		 * @return the opId
		 */
		public String getOpId() {
			return opId;
		}
		/**
		 * @param opId the opId to set
		 */
		public void setOpId(String opId) {
			this.opId = opId;
		}
		/**
		 * @return the entId
		 */
		public String getEntId() {
			return entId;
		}
		/**
		 * @param entId the entId to set
		 */
		public void setEntId(String entId) {
			this.entId = entId;
		}
		/**
		 * @return the createBy
		 */
		public String getCreateBy() {
			return createBy;
		}
		/**
		 * @param createBy the createBy to set
		 */
		public void setCreateBy(String createBy) {
			this.createBy = createBy;
		}
		/**
		 * @return the updateBy
		 */
		public String getUpdateBy() {
			return updateBy;
		}
		/**
		 * @param updateBy the updateBy to set
		 */
		public void setUpdateBy(String updateBy) {
			this.updateBy = updateBy;
		}
		/**
		 * @return the roleId
		 */
		public String getRoleId() {
			return roleId;
		}
		/**
		 * @param roleId the roleId to set
		 */
		public void setRoleId(String roleId) {
			this.roleId = roleId;
		}
		/**
		 * @return the parentEntId
		 */
		public String getParentEntId() {
			return parentEntId;
		}
		/**
		 * @param parentEntId the parentEntId to set
		 */
		public void setParentEntId(String parentEntId) {
			this.parentEntId = parentEntId;
		}
		public Integer getEntType() {
			return entType;
		}

		public void setEntType(Integer entType) {
			this.entType = entType;
		}

		public String getEntState() {
			return entState;
		}

		public void setEntState(String entState) {
			this.entState = entState;
		}

		public String getOrgLogo() {
			return orgLogo;
		}

		public void setOrgLogo(String orgLogo) {
			this.orgLogo = orgLogo;
		}

		public String getSeqCode() {
			return seqCode;
		}

		public void setSeqCode(String seqCode) {
			this.seqCode = seqCode;
		}

		public String getBusinessLicense() {
			return businessLicense;
		}

		public void setBusinessLicense(String businessLicense) {
			this.businessLicense = businessLicense;
		}

		public String getHttpPhotoPath() {
			return httpPhotoPath;
		}

		public void setHttpPhotoPath(String httpPhotoPath) {
			this.httpPhotoPath = httpPhotoPath;
		}

		public String getLoginSessionId() {
			return loginSessionId;
		}

		public void setLoginSessionId(String loginSessionId) {
			this.loginSessionId = loginSessionId;
		}

		public String getSysType() {
			return sysType;
		}

		public void setSysType(String sysType) {
			this.sysType = sysType;
		}

		public Long getLoginSysTime() {
			return loginSysTime;
		}

		public void setLoginSysTime(Long loginSysTime) {
			this.loginSysTime = loginSysTime;
		}

		public Long getLoginCount() {
			return loginCount;
		}

		public void setLoginCount(Long loginCount) {
			this.loginCount = loginCount;
		}

		public String getOldOpPass() {
			return oldOpPass;
		}

		public void setOldOpPass(String oldOpPass) {
			this.oldOpPass = oldOpPass;
		}

		public String getUpdateByStr() {
			return updateByStr;
		}

		public void setUpdateByStr(String updateByStr) {
			this.updateByStr = updateByStr;
		}
}
