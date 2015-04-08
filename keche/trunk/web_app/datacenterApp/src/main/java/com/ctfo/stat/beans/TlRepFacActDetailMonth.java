package com.ctfo.stat.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 平台监控车厂活跃度统计指标月明细表<br>
 * 描述： 平台监控车厂活跃度统计指标月明细表<br>
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
 * <td>2014-6-18</td>
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
public class TlRepFacActDetailMonth implements Serializable {

	/**  */
	private static final long serialVersionUID = 3187713999626819546L;

	/** 主键 */
	private String id;

	/** 统计月份 */
	private Long statisticsMonth;

	/** 省编码 */
	private String provinceId;

	/** 省名称 */
	private String provinceName;

	/** 市编码 */
	private String cityId;

	/** 市名称 */
	private String cityName;

	/** 企业名称 */
	private String entName;

	/** 企业ID */
	private String entId;

	/** 企业编码 */
	private String entCode;

	/** 开通时间或第一次登录时间 */
	private Long openingTime;

	/** 车辆数 */
	private Long vhNum;

	/** 上线车辆数 */
	private Long vhOnlineNum;

	/** 登录次数 */
	private Long loginNum;

	/** 免费服务期到期后签约车辆数量 */
	private Long signVhNum;

	/** 所有免费服务期到期车辆数量 */
	private Long expireVhNum;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public Long getStatisticsMonth() {
		return statisticsMonth;
	}

	public void setStatisticsMonth(Long statisticsMonth) {
		this.statisticsMonth = statisticsMonth;
	}

	public String getProvinceId() {
		return provinceId;
	}

	public void setProvinceId(String provinceId) {
		this.provinceId = provinceId;
	}

	public String getProvinceName() {
		return provinceName;
	}

	public void setProvinceName(String provinceName) {
		this.provinceName = provinceName;
	}

	public String getCityId() {
		return cityId;
	}

	public void setCityId(String cityId) {
		this.cityId = cityId;
	}

	public String getCityName() {
		return cityName;
	}

	public void setCityName(String cityName) {
		this.cityName = cityName;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getEntCode() {
		return entCode;
	}

	public void setEntCode(String entCode) {
		this.entCode = entCode;
	}

	public Long getOpeningTime() {
		return openingTime;
	}

	public void setOpeningTime(Long openingTime) {
		this.openingTime = openingTime;
	}

	public Long getVhNum() {
		return vhNum;
	}

	public void setVhNum(Long vhNum) {
		this.vhNum = vhNum;
	}

	public Long getVhOnlineNum() {
		return vhOnlineNum;
	}

	public void setVhOnlineNum(Long vhOnlineNum) {
		this.vhOnlineNum = vhOnlineNum;
	}

	public Long getLoginNum() {
		return loginNum;
	}

	public void setLoginNum(Long loginNum) {
		this.loginNum = loginNum;
	}

	public Long getSignVhNum() {
		return signVhNum;
	}

	public void setSignVhNum(Long signVhNum) {
		this.signVhNum = signVhNum;
	}

	public Long getExpireVhNum() {
		return expireVhNum;
	}

	public void setExpireVhNum(Long expireVhNum) {
		this.expireVhNum = expireVhNum;
	}

}
