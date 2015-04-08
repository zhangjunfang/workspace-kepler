/**
 * 
 */
package com.ctfo.storage.process.model;

/**
 * @author zjhl
 *
 */
public class Oil {
	/**	车辆编号	*/
	private String vid;
	/**	透传数据	*/
	private String passThroughStr;
	/**
	 * @return 获取 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号
	 * @param vid 车辆编号 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return 获取 透传数据
	 */
	public String getPassThroughStr() {
		return passThroughStr;
	}
	/**
	 * 设置透传数据
	 * @param passThroughStr 透传数据 
	 */
	public void setPassThroughStr(String passThroughStr) {
		this.passThroughStr = passThroughStr;
	}
}
