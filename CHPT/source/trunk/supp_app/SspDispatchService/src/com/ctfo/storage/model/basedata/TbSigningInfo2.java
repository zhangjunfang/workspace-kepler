package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 用户注册信息-宇通<br>
 * 描述： 用户注册信息-宇通<br>
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
 * <td>2014-11-5</td>
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
public class TbSigningInfo2 implements Serializable {

	/** */
	private static final long serialVersionUID = 7653193941532510201L;

	/** signYtId */
	private String signYtId;

	/** 签约信息id */
	private String signId;

	/** 级别 */
	private String category;

	/** 站别 */
	private String comLevel;

	/** 星级 */
	private String starLevel;

	/** 工时单价 */
	private BigDecimal workhoursPrice;

	/** 冬季补贴 */
	private BigDecimal winterSubsidy;

	/** 三包外工时单价 */
	private BigDecimal threeOutPrice;

	/** 中心库站代码 */
	private String centerLibrary;

	/** 外服人员 */
	private String outSerPerson;

	/** 否为维修NG */
	private String isRepairNg;

	/** 内否维修新能源 */
	private String isRepairNewenergy;

	/** 服务站sap代码 */
	private String serviceStationSap;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getSerStationId() {
		return serStationId;
	}

	public void setSerStationId(String serStationId) {
		this.serStationId = serStationId;
	}

	public String getSetBookId() {
		return setBookId;
	}

	public void setSetBookId(String setBookId) {
		this.setBookId = setBookId;
	}

	public String getSignYtId() {
		return signYtId;
	}

	public void setSignYtId(String signYtId) {
		this.signYtId = signYtId;
	}

	public String getSignId() {
		return signId;
	}

	public void setSignId(String signId) {
		this.signId = signId;
	}

	public String getCategory() {
		return category;
	}

	public void setCategory(String category) {
		this.category = category;
	}

	public String getComLevel() {
		return comLevel;
	}

	public void setComLevel(String comLevel) {
		this.comLevel = comLevel;
	}

	public String getStarLevel() {
		return starLevel;
	}

	public void setStarLevel(String starLevel) {
		this.starLevel = starLevel;
	}

	public BigDecimal getWorkhoursPrice() {
		return workhoursPrice;
	}

	public void setWorkhoursPrice(BigDecimal workhoursPrice) {
		this.workhoursPrice = workhoursPrice;
	}

	public BigDecimal getWinterSubsidy() {
		return winterSubsidy;
	}

	public void setWinterSubsidy(BigDecimal winterSubsidy) {
		this.winterSubsidy = winterSubsidy;
	}

	public BigDecimal getThreeOutPrice() {
		return threeOutPrice;
	}

	public void setThreeOutPrice(BigDecimal threeOutPrice) {
		this.threeOutPrice = threeOutPrice;
	}

	public String getCenterLibrary() {
		return centerLibrary;
	}

	public void setCenterLibrary(String centerLibrary) {
		this.centerLibrary = centerLibrary;
	}

	public String getOutSerPerson() {
		return outSerPerson;
	}

	public void setOutSerPerson(String outSerPerson) {
		this.outSerPerson = outSerPerson;
	}

	public String getIsRepairNg() {
		return isRepairNg;
	}

	public void setIsRepairNg(String isRepairNg) {
		this.isRepairNg = isRepairNg;
	}

	public String getIsRepairNewenergy() {
		return isRepairNewenergy;
	}

	public void setIsRepairNewenergy(String isRepairNewenergy) {
		this.isRepairNewenergy = isRepairNewenergy;
	}

	public String getServiceStationSap() {
		return serviceStationSap;
	}

	public void setServiceStationSap(String serviceStationSap) {
		this.serviceStationSap = serviceStationSap;
	}
}