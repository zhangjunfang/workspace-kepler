package com.ctfo.beans;

import com.ctfo.utils.PwdDigest;


public class Operator {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5207267911718976666L;

	/** */
	private Long opId;

	private String opLoginname;

	private String opPass;
	
	private String createOp;
	
	private String createDate;
	
	private String updateOp;
	
	private String updateDate;

	private String opSuper;

	private String opDuty;
	
	private Long roleId;
	
	private String roleName;
	
	private String realName;

	public Long getOpId() {
		return opId;
	}

	public void setOpId(Long opId) {
		this.opId = opId;
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
		if(opPass.length() < 20){
			this.opPass = PwdDigest.passwordDigest(opPass);
		}
		else{
			this.opPass = opPass;
		}
	}

	public String getCreateOp() {
		return createOp;
	}

	public void setCreateOp(String createOp) {
		this.createOp = createOp;
	}

	public String getCreateDate() {
		return createDate;
	}

	public void setCreateDate(String createDate) {
		this.createDate = createDate;
	}

	public String getUpdateOp() {
		return updateOp;
	}

	public void setUpdateOp(String updateOp) {
		this.updateOp = updateOp;
	}

	public String getUpdateDate() {
		return updateDate;
	}

	public void setUpdateDate(String updateDate) {
		this.updateDate = updateDate;
	}

	public String getOpSuper() {
		return opSuper;
	}

	public void setOpSuper(String opSuper) {
		this.opSuper = opSuper;
	}

	public String getOpDuty() {
		return opDuty;
	}

	public void setOpDuty(String opDuty) {
		this.opDuty = opDuty;
	}

	public static long getSerialversionuid() {
		return serialVersionUID;
	}

	public Long getRoleId() {
		return roleId;
	}

	public void setRoleId(Long roleId) {
		this.roleId = roleId;
	}

	public String getRoleName() {
		return roleName;
	}

	public void setRoleName(String roleName) {
		this.roleName = roleName;
	}

	public String getRealName() {
		return realName;
	}

	public void setRealName(String realName) {
		this.realName = realName;
	}
	
}