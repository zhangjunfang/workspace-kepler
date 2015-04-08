package com.ctfo.dataanalysisservice.beans;

import java.io.Serializable;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： DataAnalysisService <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
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
 * <td>Feb 8, 2012</td>
 * <td>DEVELOPER</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOPER
 * @since JDK1.6
 */
public class BaseDataObject implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	/**
	 * 纬度
	 */
	private long lat;
	/**
	 * 经度
	 */
	private long lon;
	/**
	 * 车ID
	 */
	private String vid;
	/**
	 * 最大速度
	 */
	private String maxSpeed;
	/**
	 * 最大速度持续时间
	 */
	private String maxSpeedTime;
	/**
	 * 业务类型
	 */
	private String businessType;

	private String id;

	/**
	 * @return the lat
	 */
	public long getLat() {
		return lat;
	}

	/**
	 * @param lat
	 *            the lat to set
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}

	/**
	 * @return the lon
	 */
	public long getLon() {
		return lon;
	}

	/**
	 * @param lon
	 *            the lon to set
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}

	/**
	 * @return the vid
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * @param vid
	 *            the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

 

	/**
	 * @return the maxSpeedTime
	 */
	public String getMaxSpeedTime() {
		return maxSpeedTime;
	}

	/**
	 * @param maxSpeedTime
	 *            the maxSpeedTime to set
	 */
	public void setMaxSpeedTime(String maxSpeedTime) {
		this.maxSpeedTime = maxSpeedTime;
	}

	/**
	 * @return the businessType
	 */
	public String getBusinessType() {
		return businessType;
	}

	/**
	 * @param businessType
	 *            the businessType to set
	 */
	public void setBusinessType(String businessType) {
		this.businessType = businessType;
	}

	/**
	 * @return the id
	 */
	public String getId() {
		return id;
	}

	/**
	 * @param id
	 *            the id to set
	 */
	public void setId(String id) {
		this.id = id;
	}

	public String getMaxSpeed() {
		return maxSpeed;
	}

	public void setMaxSpeed(String maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

 

}
