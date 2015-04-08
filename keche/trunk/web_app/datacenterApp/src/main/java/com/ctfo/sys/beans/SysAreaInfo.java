package com.ctfo.sys.beans;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-6-3</td>
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
public class SysAreaInfo implements Serializable {

	/** */
	private static final long serialVersionUID = 3995024804962547034L;

	/** 行政区划代码 */
	private String areaCode;

	/** 行政区划名称 */
	private String areaName;

	/** 行政区划简称 */
	private String shortName;

	/** 经度，单位毫秒 */
	private BigDecimal lon;

	/** 纬度，单位毫秒 */
	private BigDecimal lat;

	/** 地区级别，1中央，2省，4市、地区，5县，6街道、镇、乡，9其他 */
	private int areaLevel;

	/** 0其他，1直辖市 */
	private int areaFlag;

	public String getAreaCode() {
		return areaCode;
	}

	public void setAreaCode(String areaCode) {
		this.areaCode = areaCode;
	}

	public String getAreaName() {
		return areaName;
	}

	public void setAreaName(String areaName) {
		this.areaName = areaName;
	}

	public String getShortName() {
		return shortName;
	}

	public void setShortName(String shortName) {
		this.shortName = shortName;
	}

	public BigDecimal getLon() {
		return lon;
	}

	public void setLon(BigDecimal lon) {
		this.lon = lon;
	}

	public BigDecimal getLat() {
		return lat;
	}

	public void setLat(BigDecimal lat) {
		this.lat = lat;
	}

	public int getAreaLevel() {
		return areaLevel;
	}

	public void setAreaLevel(int areaLevel) {
		this.areaLevel = areaLevel;
	}

	public int getAreaFlag() {
		return areaFlag;
	}

	public void setAreaFlag(int areaFlag) {
		this.areaFlag = areaFlag;
	}

}
