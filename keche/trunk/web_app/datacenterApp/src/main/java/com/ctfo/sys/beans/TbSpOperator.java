package com.ctfo.sys.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 系统访问用户表<br>
 * 描述： 系统访问用户表<br>
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
 * <td>2014-5-6</td>
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
public class TbSpOperator implements Serializable {

	/** */
	private static final long serialVersionUID = -5781296186179082637L;

	/** 帐号id */
	private String opId;

	/** 所属实体ID */
	private String entId;

	/** 登录名 */
	private String opLoginname;

	/** 密码(Base64+MD5) */
	private String opPass;

	/** 固定鉴权码：后期扩展登陆用户和硬件绑定功能，使用uKey还是绑定电脑硬件编号 */
	private String opAuthcode;

	/** 姓名 */
	private String opName;

	/** 帐号类型 0: 管理账号1:应用系统账号 */
	private String opSuper;

	/** 性别 (0:男 1:女) */
	private String opSex;

	/** 出生日期 */
	private Long opBirth;

	/** 所属国家ID */
	private String opCountry;

	/** 所属省ID */
	private String opProvince;

	/** 所属市ID */
	private String opCity;

	/** 地址 */
	private String opAddress;

	/** 手机 */
	private String opMobile;

	/** 电话 */
	private String opPhone;

	/** 传真 */
	private String opFax;

	/** 邮件 */
	private String opEmail;

	/** 身份证号 */
	private String opIdentityId;

	/** 职位 */
	private String opDuty;

	/** 工号 */
	private String opWorkid;

	/** 帐号开通日期 */
	private Long opStartutc;

	/** 用户类型 0平台管理员 1企业用户 2代理商用户 3车厂用户 4车主用户 */
	private String opType;

	/** 备注 */
	private String opMem;

	/** 创建人id */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 修改人id */
	private String updateBy;

	/** 修改时间 */
	private Long updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 邮编 */
	private String opZip;

	/** 帐号状态，禁用帐号不能登录系统 1:启用 0: 禁用 */
	private String opStatus;

	/** 是否会员 1:是 0:不是 */
	private String isMember;

	/** IMSI号码 */
	private String imsi;

	/** 会员照片 */
	private String photo;

	/** opEndutc */
	private Long opEndutc;

	/** 换肤皮肤样式 */
	private String skinStyle;

	/** 是否为数据中心用户（1：分中心用户，0：数据中心用户，默认为1，分中心用户） */
	private String isCenter;

	/** 所属分中心编码 */
	private String centerCode;

	// 附加信息
	/** 企业名称 */
	private String entName;

	/** 企业编码 */
	private String corpCode;

	/** 角色id */
	private String roleId;

	/** 角色名称 */
	private String roleName;

	/** 创建人姓名 */
	private String createName;

	/** 修改密码验证时用的旧密码 */
	private String oldOpPass;

	public String getOpId() {
		return opId;
	}

	public void setOpId(String opId) {
		this.opId = opId;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
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

	public Long getOpEndutc() {
		return opEndutc;
	}

	public void setOpEndutc(Long opEndutc) {
		this.opEndutc = opEndutc;
	}

	public String getSkinStyle() {
		return skinStyle;
	}

	public void setSkinStyle(String skinStyle) {
		this.skinStyle = skinStyle;
	}

	public String getIsCenter() {
		return isCenter;
	}

	public void setIsCenter(String isCenter) {
		this.isCenter = isCenter;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getCorpCode() {
		return corpCode;
	}

	public void setCorpCode(String corpCode) {
		this.corpCode = corpCode;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getRoleId() {
		return roleId;
	}

	public void setRoleId(String roleId) {
		this.roleId = roleId;
	}

	public String getRoleName() {
		return roleName;
	}

	public void setRoleName(String roleName) {
		this.roleName = roleName;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

	public String getOldOpPass() {
		return oldOpPass;
	}

	public void setOldOpPass(String oldOpPass) {
		this.oldOpPass = oldOpPass;
	}

}
