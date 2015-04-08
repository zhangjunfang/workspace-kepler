package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 联系人<br>
 * 描述： 联系人<br>
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
 * <td>2014-11-5</td>
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
public class TbContacts implements Serializable {

	/** */
	private static final long serialVersionUID = 3743744592711193115L;

	/** id */
	private String contId;

	/** 联系人名称 */
	private String contName;

	/** 联系人职务，关联字典 */
	private String contPost;

	/** 联系人手机 */
	private String contPhone;

	/** 联系人固话 */
	private String contTel;

	/** 联系人邮箱 */
	private String contEmail;

	/** 联系人生日 */
	private Long contBirthday;

	/** 性别 */
	private String sex;

	/** 民族 */
	private String nation;

	/** 是否车主 0否，1是 默认0 */
	private String isCarOwner;

	/** 是否默认 0否，1是 默认0 */
	private String isDefault;

	/** 备注 */
	private String remark;

	/** CRM联系人GUID--宇通 */
	private String contCrmGuid;

	/** 上级客户---宇通 */
	private String parentCustomer;

	/** 职务备注---宇通 */
	private String postRemark;

	/** 删除标记，0为删除，1未删除 默认1 */
	private String enableFlag;

	/** 数据来源 1,自建 2，宇通 */
	private String dataSource;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 最后修改时间 */
	private Long updateTime;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后修改人，关联人员表 */
	private String updateBy;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 联系人类型 */
	private String contactsType;

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

	public String getContId() {
		return contId;
	}

	public void setContId(String contId) {
		this.contId = contId;
	}

	public String getContName() {
		return contName;
	}

	public void setContName(String contName) {
		this.contName = contName;
	}

	public String getContPost() {
		return contPost;
	}

	public void setContPost(String contPost) {
		this.contPost = contPost;
	}

	public String getContPhone() {
		return contPhone;
	}

	public void setContPhone(String contPhone) {
		this.contPhone = contPhone;
	}

	public String getContTel() {
		return contTel;
	}

	public void setContTel(String contTel) {
		this.contTel = contTel;
	}

	public String getContEmail() {
		return contEmail;
	}

	public void setContEmail(String contEmail) {
		this.contEmail = contEmail;
	}

	public Long getContBirthday() {
		return contBirthday;
	}

	public void setContBirthday(Long contBirthday) {
		this.contBirthday = contBirthday;
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

	public String getIsCarOwner() {
		return isCarOwner;
	}

	public void setIsCarOwner(String isCarOwner) {
		this.isCarOwner = isCarOwner;
	}

	public String getIsDefault() {
		return isDefault;
	}

	public void setIsDefault(String isDefault) {
		this.isDefault = isDefault;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public String getContCrmGuid() {
		return contCrmGuid;
	}

	public void setContCrmGuid(String contCrmGuid) {
		this.contCrmGuid = contCrmGuid;
	}

	public String getParentCustomer() {
		return parentCustomer;
	}

	public void setParentCustomer(String parentCustomer) {
		this.parentCustomer = parentCustomer;
	}

	public String getPostRemark() {
		return postRemark;
	}

	public void setPostRemark(String postRemark) {
		this.postRemark = postRemark;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
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

	public String getContactsType() {
		return contactsType;
	}

	public void setContactsType(String contactsType) {
		this.contactsType = contactsType;
	}

}