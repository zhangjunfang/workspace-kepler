package com.ctfo.syn.membeans;

import java.io.Serializable;

/**
 * 企业信息
 * @author xuehui
 *
 */
@SuppressWarnings("serial")
public class TbOrganization implements Serializable {

	/**
	 * 企业id
	 */
	private Long entId;

	/**
	 * 企业名称
	 */
	private String entName;
	

	public Long getEntId() {
		return entId;
	}

	public void setEntId(Long entId) {
		this.entId = entId;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

}
