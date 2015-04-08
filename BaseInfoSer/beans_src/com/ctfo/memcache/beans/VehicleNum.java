package com.ctfo.memcache.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>2011-11-16</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@SuppressWarnings("serial")
public class VehicleNum extends ETB_Base {

	/**
	 * key
	 */
	private String key;

	/**
	 * 入网车辆数
	 */
	private Integer vehicleNetworkNum;

	/**
	 * 在线车辆数
	 */
	private Integer vehicleOnlineNum;

	/**
	 * 接入企业数
	 */
	private Integer corpNetworkNum;

	/**
	 * 企业接入车辆数
	 */
	private Integer corpVehicleNetworkNum;

	/**
	 * 企业在线车辆数
	 */
	private Integer corpVehicleOnlineNum;

	/**
	 * 企业在线行驶车辆数
	 */
	private Integer corpVehicleOnlineDrivingNum;

	public String getKey() {
		return key;
	}

	public void setKey(String key) {
		this.key = key;
	}

	public Integer getVehicleNetworkNum() {
		return vehicleNetworkNum;
	}

	public void setVehicleNetworkNum(Integer vehicleNetworkNum) {
		this.vehicleNetworkNum = vehicleNetworkNum;
	}

	public Integer getVehicleOnlineNum() {
		return vehicleOnlineNum;
	}

	public void setVehicleOnlineNum(Integer vehicleOnlineNum) {
		this.vehicleOnlineNum = vehicleOnlineNum;
	}

	public Integer getCorpNetworkNum() {
		return corpNetworkNum;
	}

	public void setCorpNetworkNum(Integer corpNetworkNum) {
		this.corpNetworkNum = corpNetworkNum;
	}

	public Integer getCorpVehicleNetworkNum() {
		return corpVehicleNetworkNum;
	}

	public void setCorpVehicleNetworkNum(Integer corpVehicleNetworkNum) {
		this.corpVehicleNetworkNum = corpVehicleNetworkNum;
	}

	public Integer getCorpVehicleOnlineNum() {
		return corpVehicleOnlineNum;
	}

	public void setCorpVehicleOnlineNum(Integer corpVehicleOnlineNum) {
		this.corpVehicleOnlineNum = corpVehicleOnlineNum;
	}

	public Integer getCorpVehicleOnlineDrivingNum() {
		return corpVehicleOnlineDrivingNum;
	}

	public void setCorpVehicleOnlineDrivingNum(Integer corpVehicleOnlineDrivingNum) {
		this.corpVehicleOnlineDrivingNum = corpVehicleOnlineDrivingNum;
	}
}
