package com.ctfo.trackservice.model;

/**
 * 文件名：OilWear.java
 * 功能：油箱油量监控
 *
 * @author huangjincheng
 * 2014-9-25上午10:14:44
 * 
 */
public class OilWearBean {
	private String vid;
	
	private long statDate = 0;//统计日期
	
	private String changed_type = "00"; //油量变化状态 注：默认为油位正常
	
	private int addOil = 0; //加油量
	
	private int decreaseOil = 0; //减少量
	
	private int usedOil = 0; //消耗量
	
	private int runningOil = 0; //行车油耗消耗量

	public String getChanged_type() {
		return changed_type;
	}

	public void setChanged_type(String changedType) {
		changed_type = changedType;
	}

	public int getAddOil() {
		return addOil;
	}

	public void addAddOil(int addOil) {
		this.addOil = addOil + this.addOil;
	}

	public int getDecreaseOil() {
		return decreaseOil;
	}

	public void addDecreaseOil(int decreaseOil) {
		this.decreaseOil = decreaseOil + this.decreaseOil;
	}

	public int getUsedOil() {
		return usedOil;
	}

	public void addUsedOil(int usedOil) {
		this.usedOil = usedOil + this.usedOil;
	}

	/**
	 * @return the vid
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * @param vid the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * @return the statDate
	 */
	public long getStatDate() {
		return statDate;
	}

	/**
	 * @param statDate the statDate to set
	 */
	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}

	/**
	 * @return the runningOil
	 */
	public int getRunningOil() {
		return runningOil;
	}

	/**
	 * @param runningOil the runningOil to set
	 */
	public void setRunningOil(int runningOil) {
		this.runningOil = runningOil;
	}
	
	public void addRunningOil(int runningOil) {
		this.runningOil = this.runningOil + runningOil;
	}
	
}
