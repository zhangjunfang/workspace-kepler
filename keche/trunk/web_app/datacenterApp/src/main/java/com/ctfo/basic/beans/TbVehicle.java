package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：车辆<br>
 * 描述：车辆<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月29日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbVehicle implements Serializable {

	private static final long serialVersionUID = 8246612183161040647L;

	/** 车辆ID */
	private String vid;

	/** 车牌号码 */
	private String vehicleNo;

	/** 车牌颜色 */
	private String plateColor;

	/** 车身颜色 */
	private String vehicleColor;

	/** 车辆分类ID */
	private String vtypeId;

	/** 车辆品牌 */
	private String vbrandCode;

	/** 车型编码，车厂根据品牌定义的车型，对应车型编码表 */
	private String prodCode;

	/** 车辆来源，参见车辆来源码表 */
	private String originCode;

	/** 车辆内部编码,保证企业内唯一； */
	private String innerCode;

	/** 燃油类型ID */
	private String oiltypeId;

	/** 发动机品牌 */
	private String ebrandCode;

	/** 发动机号型号 */
	private String emodelCode;

	/** 发动机号 */
	private String engineNo;

	/** 车架号(vin) */
	private String vinCode;

	/** 车工号 */
	private String autoSn;

	/** 购置证号 */
	private String buyNo;

	/** 营运证号 */
	private String serviceNo;

	/** 百公里油耗，升标准油耗 */
	private Long km100Oiluse;

	/** 载客限员 */
	private Long vehicleMennum;

	/** 标准油耗 */
	private Long standardOil;

	/** 标准转速 */
	private Long standardRotate;

	/** 自重 */
	private Double vehicleTon;

	/** 售价 */
	private Long salePrice;

	/** 排量 */
	private Long outNumber;

	/** 车辆状态参考值id，对应车辆状态参考值信息表（TB_VSTATUS_REF）中的 */
	private String vsRefId;

	/** 轮胎滚动半径 */
	private Double tyreR;

	/** 后桥速比 */
	private Double rearAxleRate;

	/** 邮箱容量升 */
	private Long vehicleCap;

	/** 购买日期 */
	private Long vehicleBuydate;

	/** 上牌日期 */
	private Long vehicleRegdate;

	/** 汽车图片url */
	private String vehiclePic;

	/** 备注 */
	private String vehicleMem;

	/** 创建人id */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 修改人id */
	private String updateBy;

	/** 修改时间 */
	private Long updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 所属实体id */
	private String entId;

	/** 车辆状态：1：空闲 2：已绑定 3：吊销 */
	private String vehicleState;

	/** 运输行业证照类别，JT415标准中的5.5.1章节 */
	private String certificateType;

	/** 运输行业证照状态，JT415标准中的5.5.2章节,1有效，0无效 */
	private String certificateState;

	/** 道路运输证号 */
	private String roadTransport;

	/** 年度审验状态，JT415标准中的5.4.4章节 */
	private String reviewState;

	/** 车辆类型，JT415标准中的5.4.9章节，参照运输行业车型编码表 */
	private String vehicleType;

	/** 运输行业类别编码，JT415标准中的5.2.1章节 */
	private String transtypeCode;

	/** 二级维护状态，JT415标准中的5.4.3章节 */
	private String maintenanceState;

	/** 车辆运营状态，JT415标准中的5.4.6章节 */
	private String vehicleOperationState;

	/** 投保状态, JT415标准中的5.4.7章节 */
	private String insuranceState;

	/** 客车等级, JT415标准中的5.4.8章节 */
	private String coachLevel;

	/** 电压 */
	private String voltage;

	/** 运营机构ID */
	private String vehicleOperationId;

	/** 省域编码 */
	private String areaCode;

	/** 市编码 */
	private String cityId;

	/** -1未注册，0已注册 */
	private Integer regStatus;

	/** 车辆配置方案Id,TB_VEHICLE_CONFIGUER_PROGRAMME */
	private String progId;

	/** */
	private String county;

	/** 初次安装终端时间 */
	private Long firstInstalTime;

	/** 排量(ml) */
	private Double engineDisplacement;

	/** 功率(kw) */
	private Double watt;

	/** 轴距(mm) */
	private Double wheelbase;

	/** 总质量(kg) */
	private Double totalMass;

	/** 整备质量(kg) */
	private Double curbWeight;

	/** 核定载客人数(人) */
	private Integer maximalPeople;

	/** 车辆出厂日期 */
	private Long releaseDate;

	/** 交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过) */
	private Long deliveryStatus;

	/** 排放标准 */
	private String emissionStandard;

	/** 车辆性质（挂靠车/自购车） */
	private String vehicleProperties;

	/** 挂靠时间 */
	private Long attachedToTime;

	/** 车辆上牌时间 */
	private Long vehicleRegisterNoTime;

	/** 年审时间 */
	private Long annualAuditTime;

	/** 年审有效期至 */
	private Long annualAuditValidityTime;

	/** 年审提醒天数（天） */
	private Long auditAlarmDays;

	/** 到期时间 */
	private Long endtime;

	/** 签约时间 */
	private Long signtime;

	/** 接驳车辆(1:是;0或非1:否) */
	private Long vhAccess;

	/** 所属分中心编码 */
	private String centerCode;

	// 附加字段
	/** 创建人姓名 */
	private String createName;

	/** 编辑人姓名 */
	private String updateName;

	/** 所属车队名称 **/
	private String entName;

	/** 所属企业名称 **/
	private String parentEntName;

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getPlateColor() {
		return plateColor;
	}

	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}

	public String getVehicleColor() {
		return vehicleColor;
	}

	public void setVehicleColor(String vehicleColor) {
		this.vehicleColor = vehicleColor;
	}

	public String getVtypeId() {
		return vtypeId;
	}

	public void setVtypeId(String vtypeId) {
		this.vtypeId = vtypeId;
	}

	public String getVbrandCode() {
		return vbrandCode;
	}

	public void setVbrandCode(String vbrandCode) {
		this.vbrandCode = vbrandCode;
	}

	public String getProdCode() {
		return prodCode;
	}

	public void setProdCode(String prodCode) {
		this.prodCode = prodCode;
	}

	public String getOriginCode() {
		return originCode;
	}

	public void setOriginCode(String originCode) {
		this.originCode = originCode;
	}

	public String getInnerCode() {
		return innerCode;
	}

	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}

	public String getOiltypeId() {
		return oiltypeId;
	}

	public void setOiltypeId(String oiltypeId) {
		this.oiltypeId = oiltypeId;
	}

	public String getEbrandCode() {
		return ebrandCode;
	}

	public void setEbrandCode(String ebrandCode) {
		this.ebrandCode = ebrandCode;
	}

	public String getEmodelCode() {
		return emodelCode;
	}

	public void setEmodelCode(String emodelCode) {
		this.emodelCode = emodelCode;
	}

	public String getEngineNo() {
		return engineNo;
	}

	public void setEngineNo(String engineNo) {
		this.engineNo = engineNo;
	}

	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}

	public String getAutoSn() {
		return autoSn;
	}

	public void setAutoSn(String autoSn) {
		this.autoSn = autoSn;
	}

	public String getBuyNo() {
		return buyNo;
	}

	public void setBuyNo(String buyNo) {
		this.buyNo = buyNo;
	}

	public String getServiceNo() {
		return serviceNo;
	}

	public void setServiceNo(String serviceNo) {
		this.serviceNo = serviceNo;
	}

	public Long getKm100Oiluse() {
		return km100Oiluse;
	}

	public void setKm100Oiluse(Long km100Oiluse) {
		this.km100Oiluse = km100Oiluse;
	}

	public Long getVehicleMennum() {
		return vehicleMennum;
	}

	public void setVehicleMennum(Long vehicleMennum) {
		this.vehicleMennum = vehicleMennum;
	}

	public Long getStandardOil() {
		return standardOil;
	}

	public void setStandardOil(Long standardOil) {
		this.standardOil = standardOil;
	}

	public Long getStandardRotate() {
		return standardRotate;
	}

	public void setStandardRotate(Long standardRotate) {
		this.standardRotate = standardRotate;
	}

	public Double getVehicleTon() {
		return vehicleTon;
	}

	public void setVehicleTon(Double vehicleTon) {
		this.vehicleTon = vehicleTon;
	}

	public Long getSalePrice() {
		return salePrice;
	}

	public void setSalePrice(Long salePrice) {
		this.salePrice = salePrice;
	}

	public Long getOutNumber() {
		return outNumber;
	}

	public void setOutNumber(Long outNumber) {
		this.outNumber = outNumber;
	}

	public String getVsRefId() {
		return vsRefId;
	}

	public void setVsRefId(String vsRefId) {
		this.vsRefId = vsRefId;
	}

	public Double getTyreR() {
		return tyreR;
	}

	public void setTyreR(Double tyreR) {
		this.tyreR = tyreR;
	}

	public Double getRearAxleRate() {
		return rearAxleRate;
	}

	public void setRearAxleRate(Double rearAxleRate) {
		this.rearAxleRate = rearAxleRate;
	}

	public Long getVehicleCap() {
		return vehicleCap;
	}

	public void setVehicleCap(Long vehicleCap) {
		this.vehicleCap = vehicleCap;
	}

	public Long getVehicleBuydate() {
		return vehicleBuydate;
	}

	public void setVehicleBuydate(Long vehicleBuydate) {
		this.vehicleBuydate = vehicleBuydate;
	}

	public Long getVehicleRegdate() {
		return vehicleRegdate;
	}

	public void setVehicleRegdate(Long vehicleRegdate) {
		this.vehicleRegdate = vehicleRegdate;
	}

	public String getVehiclePic() {
		return vehiclePic;
	}

	public void setVehiclePic(String vehiclePic) {
		this.vehiclePic = vehiclePic;
	}

	public String getVehicleMem() {
		return vehicleMem;
	}

	public void setVehicleMem(String vehicleMem) {
		this.vehicleMem = vehicleMem;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getVehicleState() {
		return vehicleState;
	}

	public void setVehicleState(String vehicleState) {
		this.vehicleState = vehicleState;
	}

	public String getCertificateType() {
		return certificateType;
	}

	public void setCertificateType(String certificateType) {
		this.certificateType = certificateType;
	}

	public String getCertificateState() {
		return certificateState;
	}

	public void setCertificateState(String certificateState) {
		this.certificateState = certificateState;
	}

	public String getRoadTransport() {
		return roadTransport;
	}

	public void setRoadTransport(String roadTransport) {
		this.roadTransport = roadTransport;
	}

	public String getReviewState() {
		return reviewState;
	}

	public void setReviewState(String reviewState) {
		this.reviewState = reviewState;
	}

	public String getVehicleType() {
		return vehicleType;
	}

	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}

	public String getTranstypeCode() {
		return transtypeCode;
	}

	public void setTranstypeCode(String transtypeCode) {
		this.transtypeCode = transtypeCode;
	}

	public String getMaintenanceState() {
		return maintenanceState;
	}

	public void setMaintenanceState(String maintenanceState) {
		this.maintenanceState = maintenanceState;
	}

	public String getVehicleOperationState() {
		return vehicleOperationState;
	}

	public void setVehicleOperationState(String vehicleOperationState) {
		this.vehicleOperationState = vehicleOperationState;
	}

	public String getInsuranceState() {
		return insuranceState;
	}

	public void setInsuranceState(String insuranceState) {
		this.insuranceState = insuranceState;
	}

	public String getCoachLevel() {
		return coachLevel;
	}

	public void setCoachLevel(String coachLevel) {
		this.coachLevel = coachLevel;
	}

	public String getVoltage() {
		return voltage;
	}

	public void setVoltage(String voltage) {
		this.voltage = voltage;
	}

	public String getVehicleOperationId() {
		return vehicleOperationId;
	}

	public void setVehicleOperationId(String vehicleOperationId) {
		this.vehicleOperationId = vehicleOperationId;
	}

	public String getAreaCode() {
		return areaCode;
	}

	public void setAreaCode(String areaCode) {
		this.areaCode = areaCode;
	}

	public String getCityId() {
		return cityId;
	}

	public void setCityId(String cityId) {
		this.cityId = cityId;
	}

	public Integer getRegStatus() {
		return regStatus;
	}

	public void setRegStatus(Integer regStatus) {
		this.regStatus = regStatus;
	}

	public String getProgId() {
		return progId;
	}

	public void setProgId(String progId) {
		this.progId = progId;
	}

	public String getCounty() {
		return county;
	}

	public void setCounty(String county) {
		this.county = county;
	}

	public Long getFirstInstalTime() {
		return firstInstalTime;
	}

	public void setFirstInstalTime(Long firstInstalTime) {
		this.firstInstalTime = firstInstalTime;
	}

	public Double getEngineDisplacement() {
		return engineDisplacement;
	}

	public void setEngineDisplacement(Double engineDisplacement) {
		this.engineDisplacement = engineDisplacement;
	}

	public Double getWatt() {
		return watt;
	}

	public void setWatt(Double watt) {
		this.watt = watt;
	}

	public Double getWheelbase() {
		return wheelbase;
	}

	public void setWheelbase(Double wheelbase) {
		this.wheelbase = wheelbase;
	}

	public Double getTotalMass() {
		return totalMass;
	}

	public void setTotalMass(Double totalMass) {
		this.totalMass = totalMass;
	}

	public Double getCurbWeight() {
		return curbWeight;
	}

	public void setCurbWeight(Double curbWeight) {
		this.curbWeight = curbWeight;
	}

	public Integer getMaximalPeople() {
		return maximalPeople;
	}

	public void setMaximalPeople(Integer maximalPeople) {
		this.maximalPeople = maximalPeople;
	}

	public Long getReleaseDate() {
		return releaseDate;
	}

	public void setReleaseDate(Long releaseDate) {
		this.releaseDate = releaseDate;
	}

	public Long getDeliveryStatus() {
		return deliveryStatus;
	}

	public void setDeliveryStatus(Long deliveryStatus) {
		this.deliveryStatus = deliveryStatus;
	}

	public String getEmissionStandard() {
		return emissionStandard;
	}

	public void setEmissionStandard(String emissionStandard) {
		this.emissionStandard = emissionStandard;
	}

	public String getVehicleProperties() {
		return vehicleProperties;
	}

	public void setVehicleProperties(String vehicleProperties) {
		this.vehicleProperties = vehicleProperties;
	}

	public Long getAttachedToTime() {
		return attachedToTime;
	}

	public void setAttachedToTime(Long attachedToTime) {
		this.attachedToTime = attachedToTime;
	}

	public Long getVehicleRegisterNoTime() {
		return vehicleRegisterNoTime;
	}

	public void setVehicleRegisterNoTime(Long vehicleRegisterNoTime) {
		this.vehicleRegisterNoTime = vehicleRegisterNoTime;
	}

	public Long getAnnualAuditTime() {
		return annualAuditTime;
	}

	public void setAnnualAuditTime(Long annualAuditTime) {
		this.annualAuditTime = annualAuditTime;
	}

	public Long getAnnualAuditValidityTime() {
		return annualAuditValidityTime;
	}

	public void setAnnualAuditValidityTime(Long annualAuditValidityTime) {
		this.annualAuditValidityTime = annualAuditValidityTime;
	}

	public Long getAuditAlarmDays() {
		return auditAlarmDays;
	}

	public void setAuditAlarmDays(Long auditAlarmDays) {
		this.auditAlarmDays = auditAlarmDays;
	}

	public Long getEndtime() {
		return endtime;
	}

	public void setEndtime(Long endtime) {
		this.endtime = endtime;
	}

	public Long getSigntime() {
		return signtime;
	}

	public void setSigntime(Long signtime) {
		this.signtime = signtime;
	}

	public Long getVhAccess() {
		return vhAccess;
	}

	public void setVhAccess(Long vhAccess) {
		this.vhAccess = vhAccess;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

	public String getUpdateName() {
		return updateName;
	}

	public void setUpdateName(String updateName) {
		this.updateName = updateName;
	}

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getParentEntName() {
		return parentEntName;
	}

	public void setParentEntName(String parentEntName) {
		this.parentEntName = parentEntName;
	}

}
