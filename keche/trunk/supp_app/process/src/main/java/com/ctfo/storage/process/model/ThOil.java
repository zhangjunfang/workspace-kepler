package com.ctfo.storage.process.model;

/**
 * ThOil 油量文件记录
 * 
 * 
 * @author huangjincheng
 * 2014-5-21下午03:48:26
 * 
 */
public class ThOil {
	/** 车辆编号*/
	private String vid ;
	/** 上报时间		*/
	private long upTime ;
	/** 基本状态位*/
	private String baseStatus ;
	/** 油位异常标志	*/
	private String oilBoxState ;
	/** 燃油液位*/
	private int oilBoxLevelTemp ;
	/** 本次加油量*/
	private int addOilTemp ;
	/** 邮箱燃油油量*/
	private int oilBoxMassTemp ;
	/**
	 * 获取车辆编号的值
	 * @return vid  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return 获取 上报时间
	 */
	public long getUpTime() {
		return upTime;
	}
	/**
	 * 设置上报时间
	 * @param upTime 上报时间 
	 */
	public void setUpTime(long upTime) {
		this.upTime = upTime;
	}

	/**
	 * 获取基本状态位的值
	 * @return baseStatus  
	 */
	public String getBaseStatus() {
		return baseStatus;
	}

	/**
	 * 设置基本状态位的值
	 * @param baseStatus
	 */
	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}

	/**
	 * 获取油位异常标志的值
	 * @return oilBoxState  
	 */
	public String getOilBoxState() {
		return oilBoxState;
	}

	/**
	 * 设置油位异常标志的值
	 * @param oilBoxState
	 */
	public void setOilBoxState(String oilBoxState) {
		this.oilBoxState = oilBoxState;
	}

	/**
	 * 获取燃油液位的值
	 * @return oilBoxLevelTemp  
	 */
	public int getOilBoxLevelTemp() {
		return oilBoxLevelTemp;
	}

	/**
	 * 设置燃油液位的值
	 * @param oilBoxLevelTemp
	 */
	public void setOilBoxLevelTemp(int oilBoxLevelTemp) {
		this.oilBoxLevelTemp = oilBoxLevelTemp;
	}

	/**
	 * 获取本次加油量的值
	 * @return addOilTemp  
	 */
	public int getAddOilTemp() {
		return addOilTemp;
	}

	/**
	 * 设置本次加油量的值
	 * @param addOilTemp
	 */
	public void setAddOilTemp(int addOilTemp) {
		this.addOilTemp = addOilTemp;
	}

	/**
	 * 获取邮箱燃油油量的值
	 * @return oilBoxMassTemp  
	 */
	public int getOilBoxMassTemp() {
		return oilBoxMassTemp;
	}

	/**
	 * 设置邮箱燃油油量的值
	 * @param oilBoxMassTemp
	 */
	public void setOilBoxMassTemp(int oilBoxMassTemp) {
		this.oilBoxMassTemp = oilBoxMassTemp;
	}
	
	
}
