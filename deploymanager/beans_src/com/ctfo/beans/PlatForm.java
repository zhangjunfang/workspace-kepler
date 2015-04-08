package com.ctfo.beans;

public class PlatForm {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5207267911718976666L;

	/** */
	private Long platId;

	private String platName;

	private String remark;

	public Long getPlatId() {
		return platId;
	}

	public void setPlatId(Long platId) {
		this.platId = platId;
	}

	public String getPlatName() {
		return platName;
	}

	public void setPlatName(String platName) {
		this.platName = platName;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

}