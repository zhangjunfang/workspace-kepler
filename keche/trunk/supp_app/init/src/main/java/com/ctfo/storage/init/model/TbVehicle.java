package com.ctfo.storage.init.model;



import java.io.Serializable;

@SuppressWarnings("serial")
public class TbVehicle extends BaseModel implements Serializable {

	/** 序号 */
	private long vindex =-1l;

	/** 企业名称 */
	private String pentName = "";

	/** 终端厂家名称 */
	private String oemName = "";

	/** 硬件版本�?*/
	private String terHardver = "";

	/** 软件版本�?*/
	private String terSoftver = "";

	/** 车辆ID */
	private String vid = "";

	/** 车牌号码 */
	private String vehicleNo = "";

	/** 车牌颜色 */
	private String plateColor = "";

	/** 车身颜色 */
	private String vehicleColor = "";

	/** 车辆分类ID */
	private String vtypeId = "";

	/** 车辆品牌 */
	private String vbrandCode = "";

	/** 车型编码，车厂根据品牌定义的车型，对应车型编码表 */
	private String prodCode = "";

	/** 车辆来源，参见车辆来源码�?*/
	private String originCode = "";

	/**  */
	private String vlineId = "";

	/** 车辆内部编码,保证企业内唯�?*/
	private String innerCode = "";

	/** 燃油类型ID */
	private String oiltypeId = "";

	/** 发动机品�?*/
	private String ebrandCode = "";

	/** 发动机号型号 */
	private String emodelCode = "";

	/** 发动机号 */
	private String engineNo = "";

	/** 车架�?vin) */
	private String vinCode = "";

	/** 车工�?*/
	private String autoSn = "";

	/** 购置证号 */
	private String buyNo = "";

	/** 营运证号 */
	private String serviceNo = "";

	/** 百公里油耗，升标准油�?*/
	private long km100Oiluse =-1l;

	/** 载客限员 */
	private long vehicleMennum =-1l;

	/** 标准油�? */
	private Long standardOil =-1l;

	/** 标准转�? */
	private long standardRotate =-1l;

	/** 自重 */
	private Double vehicleTon =-1d;

	/** 售价 */
	private long salePrice =-1l;

	/** 排量 */
	private long outNumber =-1l;

	/** 车辆状 */
	private String vsRefId = "";

	/** 轮胎滚动半径 */
	private Double tyreR =-1d;

	/** 后桥速比 */
	private Double rearAxleRate =-1d;

	/** 邮箱容量�?*/
	private long vehicleCap =-1l;

	/** 购买日期 */
	private long vehicleBuydate =-1l;

	/** 上牌日期 */
	private long vehicleRegdate =-1l;
	
	/**  车辆上牌时间 */
	private long vehicleRegisterNoTime =-1l;

	/** 汽车图片url */
	private String vehiclePic = "";

	/** 备注 */
	private String vehicleMem = "";

	/** 创建人id */
	private String createBy ="";

	/** 创建时间 */
	private long createTime =-1l;

	/** 修改人id */
	private String updateBy ="";

	/** 修改时间 */
	private long updateTime =-1l;

	/** 有效标记 1:有效 0:无效 默认�? */
	private String enableFlag = "";

	/** �?��实体id */
	private String entId = "";

	/** 车辆状�?�?：空�?2：已绑定 3：吊�?*/
	private String vehicleState = "";

	/** 运输行业证照类别，JT415标准中的5.5.1章节 */
	private String certificateType = "";

	/** 运输行业证照状�?，JT415标准中的5.5.2章节,1有效�?无效 */
	private String certificateState = "";

	/** 道路运输证号 */
	private String roadTransport = "";

	/** 年度审验状�?，JT415标准中的5.4.4章节 */
	private String reviewState = "";

	/** 车辆类型，JT415标准中的5.4.9章节，参照运输行业车型编码表 */
	private String vehicleType = "";

	/** 运输行业类别编码，JT415标准中的5.2.1章节 */
	private String transtypeCode = "";

	/** 二级维护状�?，JT415标准中的5.4.3章节 */
	private String maintenanceState = "";

	/** 车辆运营状�?，JT415标准中的5.4.6章节 */
	private String vehicleOperationState = "";

	/** 投保状�?, JT415标准中的5.4.7章节 */
	private String insuranceState = "";

	/** 客车等级, JT415标准中的5.4.8章节 */
	private String coachLevel = "";

	/** 电压 */
	private String voltage = "";

	/** 车队名称 */
	private String entName = "";

	/** 市域编码 */
	private String cityId = "";

	/** 省域编码 */
	private String areaCode = "";
	
	/** 县级编码 */
	private String county = "";

	/** 车辆配置方案id */
	private String progId ="";

	/** 用户Id */
	private long opId =-1l;

	/** 车辆服务品牌编码 */
	private String servicebrandCode = "";

	/** -1未注册，0已注�?*/
	private int regStatus =-1;
	
	/**组织Id**/
	private String pentId = "";
	
	private String modelName;//模板名称  另存为模板专用，与车辆信息无�?
	
	
	/**车身（长*�?高）�?（如果是厢车,填高度）**/
	private String bodySize = "";
	
	/**额定载重（吨�?*/
	private long loadTon =-1l;
	
	/**产权人身份证�?*/
	private String propertyIdentityNo = "";
	
	/**产权单位名称**/
	private String propertyUnitName = "";
	
	/**产权单位营业执照�?*/
	private String propertyLicenseNo = "";
	
	/**车辆出厂时间**/
	private long releaseDate =-1l;
	
	/**车辆购置方式:1.分期付款 2.�?��性交�?*/
	private long purchaseType =-1l;
	
	/**车辆保险种类:1.交强�?2.盗抢�?3.三�? 4.车损�?5.车上人员�?6.货物运输�?7.其他**/
	private String insuranceType = "";
	
	/**车辆保险种类:7.其他（填写）**/
	private String insuranceTypeOther= "";
	
	/**车辆保险到期时间**/
	private long insuranceExpirateTime ;
	
	/**
	 * 车辆�?��线路名称
	 */
	private String lineName = "";
	
	/** 年审时间*/
	private Long annualAuditTime = -1l;
	
	/** 年审有效期至*/
	private Long annualAuditValidityTime = -1l;
	
	/** 挂靠时间*/
	private Long attachedToTime= -1l;
	
	/** 年审提醒天数*/
	private Long auditAlarmDays = -1l;
	
	/** 整备质量*/
	private Long curbWeight = -1l;
	
	/** 交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过)*/
	private Long deliveryStatus = -1l;
	
	/** 排放标准*/
	private String emissionStandard = "";
	
	/** 到期时间*/
	private Long endTime = -1l;
	
	/** 排量(ml)*/
	private Long engineDisplacement = -1l;
	
	/** 初次安装时间*/
	private Long firstInstalTime = -1l;
	
	/** 核定载客人数(人)*/
	private Long maximalPeople = -1l;
	
	/** 签约时间*/
	private Long signTime = -1l;
	
	/** 总质量(kg)*/
	private Long totalMass = -1l;
	
	/** 运营机构ID*/
	private String vehicleOperationId = "";
	
	/** 车辆性质(挂靠车/自购车)*/
	private String vehicleProperties = "";
	
	/** 接驳车辆(1:是;0或非1:否)*/
	private Long vhAccess = -1l;
	
	/** 功率(kw)*/
	private Long watt = -1l;
	
	/** 轴距(mm)*/
	private Long wheelBase = -1l;
	public String getLineName() {
		return lineName;
	}

	public void setLineName(String lineName) {
		this.lineName = lineName;
	}

	public Long getVindex() {
		return vindex;
	}

	public void setVindex(Long vindex) {
		this.vindex = vindex;
	}

	public String getPentName() {
		return pentName;
	}

	public void setPentName(String pentName) {
		this.pentName = pentName;
	}

	public String getOemName() {
		return oemName;
	}

	public void setOemName(String oemName) {
		this.oemName = oemName;
	}

	public String getTerHardver() {
		return terHardver;
	}

	public void setTerHardver(String terHardver) {
		this.terHardver = terHardver;
	}

	public String getTerSoftver() {
		return terSoftver;
	}

	public void setTerSoftver(String terSoftver) {
		this.terSoftver = terSoftver;
	}

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

	public String getVlineId() {
		return vlineId;
	}

	public void setVlineId(String vlineId) {
		this.vlineId = vlineId;
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

	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getCityId() {
		return cityId;
	}

	public void setCityId(String cityId) {
		this.cityId = cityId;
	}

	public String getAreaCode() {
		return areaCode;
	}

	public void setAreaCode(String areaCode) {
		this.areaCode = areaCode;
	}

	public String getProgId() {
		return progId;
	}

	public void setProgId(String progId) {
		this.progId = progId;
	}

	public Long getOpId() {
		return opId;
	}

	public void setOpId(Long opId) {
		this.opId = opId;
	}

	public String getServicebrandCode() {
		return servicebrandCode;
	}

	public void setServicebrandCode(String servicebrandCode) {
		this.servicebrandCode = servicebrandCode;
	}

	public Integer getRegStatus() {
		return regStatus;
	}

	public void setRegStatus(Integer regStatus) {
		this.regStatus = regStatus;
	}


	public String getPentId() {
		return pentId;
	}

	public void setPentId(String pentId) {
		this.pentId = pentId;
	}

	public String getModelName() {
		return modelName;
	}

	public void setModelName(String modelName) {
		this.modelName = modelName;
	}

	public String getBodySize() {
		return bodySize;
	}

	public void setBodySize(String bodySize) {
		this.bodySize = bodySize;
	}

	public Long getLoadTon() {
		return loadTon;
	}

	public void setLoadTon(Long loadTon) {
		this.loadTon = loadTon;
	}

	public String getPropertyIdentityNo() {
		return propertyIdentityNo;
	}

	public void setPropertyIdentityNo(String propertyIdentityNo) {
		this.propertyIdentityNo = propertyIdentityNo;
	}

	public String getPropertyUnitName() {
		return propertyUnitName;
	}

	public void setPropertyUnitName(String propertyUnitName) {
		this.propertyUnitName = propertyUnitName;
	}

	public String getPropertyLicenseNo() {
		return propertyLicenseNo;
	}

	public void setPropertyLicenseNo(String propertyLicenseNo) {
		this.propertyLicenseNo = propertyLicenseNo;
	}

	public Long getReleaseDate() {
		return releaseDate;
	}

	public void setReleaseDate(Long releaseDate) {
		this.releaseDate = releaseDate;
	}

	public Long getPurchaseType() {
		return purchaseType;
	}

	public void setPurchaseType(Long purchaseType) {
		this.purchaseType = purchaseType;
	}

	public String getInsuranceType() {
		return insuranceType;
	}

	public void setInsuranceType(String insuranceType) {
		this.insuranceType = insuranceType;
	}

	public String getInsuranceTypeOther() {
		return insuranceTypeOther;
	}

	public void setInsuranceTypeOther(String insuranceTypeOther) {
		this.insuranceTypeOther = insuranceTypeOther;
	}

	public Long getInsuranceExpirateTime() {
		return insuranceExpirateTime;
	}

	public void setInsuranceExpirateTime(Long insuranceExpirateTime) {
		this.insuranceExpirateTime = insuranceExpirateTime;
	}

	public String getCounty() {
		return county;
	}

	public void setCounty(String county) {
		this.county = county;
	}

	/**
	 * 获取年审时间的值
	 * @return annualAuditTime  
	 */
	public Long getAnnualAuditTime() {
		return annualAuditTime;
	}

	/**
	 * 设置年审时间的值
	 * @param annualAuditTime
	 */
	public void setAnnualAuditTime(Long annualAuditTime) {
		this.annualAuditTime = annualAuditTime;
	}

	/**
	 * 获取年审有效期至的值
	 * @return annualAuditValidityTime  
	 */
	public Long getAnnualAuditValidityTime() {
		return annualAuditValidityTime;
	}

	/**
	 * 设置年审有效期至的值
	 * @param annualAuditValidityTime
	 */
	public void setAnnualAuditValidityTime(Long annualAuditValidityTime) {
		this.annualAuditValidityTime = annualAuditValidityTime;
	}

	/**
	 * 获取挂靠时间的值
	 * @return attachedToTime  
	 */
	public Long getAttachedToTime() {
		return attachedToTime;
	}

	/**
	 * 设置挂靠时间的值
	 * @param attachedToTime
	 */
	public void setAttachedToTime(Long attachedToTime) {
		this.attachedToTime = attachedToTime;
	}

	/**
	 * 获取年审提醒天数的值
	 * @return auditAlarmDays  
	 */
	public Long getAuditAlarmDays() {
		return auditAlarmDays;
	}

	/**
	 * 设置年审提醒天数的值
	 * @param auditAlarmDays
	 */
	public void setAuditAlarmDays(Long auditAlarmDays) {
		this.auditAlarmDays = auditAlarmDays;
	}

	/**
	 * 获取整备质量的值
	 * @return curbWeight  
	 */
	public Long getCurbWeight() {
		return curbWeight;
	}

	/**
	 * 设置整备质量的值
	 * @param curbWeight
	 */
	public void setCurbWeight(Long curbWeight) {
		this.curbWeight = curbWeight;
	}

	/**
	 * 获取交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过)的值
	 * @return deliveryStatus  
	 */
	public Long getDeliveryStatus() {
		return deliveryStatus;
	}

	/**
	 * 设置交付状态(0:交付中;1:未交付;2:已交付;3:交付未通过)的值
	 * @param deliveryStatus
	 */
	public void setDeliveryStatus(Long deliveryStatus) {
		this.deliveryStatus = deliveryStatus;
	}

	/**
	 * 获取排放标准的值
	 * @return emissionStandard  
	 */
	public String getEmissionStandard() {
		return emissionStandard;
	}

	/**
	 * 设置排放标准的值
	 * @param emissionStandard
	 */
	public void setEmissionStandard(String emissionStandard) {
		this.emissionStandard = emissionStandard;
	}

	/**
	 * 获取到期时间的值
	 * @return endTime  
	 */
	public Long getEndTime() {
		return endTime;
	}

	/**
	 * 设置到期时间的值
	 * @param endTime
	 */
	public void setEndTime(Long endTime) {
		this.endTime = endTime;
	}

	/**
	 * 获取排量(ml)的值
	 * @return engineDisplacement  
	 */
	public Long getEngineDisplacement() {
		return engineDisplacement;
	}

	/**
	 * 设置排量(ml)的值
	 * @param engineDisplacement
	 */
	public void setEngineDisplacement(Long engineDisplacement) {
		this.engineDisplacement = engineDisplacement;
	}

	/**
	 * 获取初次安装时间的值
	 * @return firstInstalTime  
	 */
	public Long getFirstInstalTime() {
		return firstInstalTime;
	}

	/**
	 * 设置初次安装时间的值
	 * @param firstInstalTime
	 */
	public void setFirstInstalTime(Long firstInstalTime) {
		this.firstInstalTime = firstInstalTime;
	}

	/**
	 * 获取核定载客人数(人)的值
	 * @return maximalPeople  
	 */
	public Long getMaximalPeople() {
		return maximalPeople;
	}

	/**
	 * 设置核定载客人数(人)的值
	 * @param maximalPeople
	 */
	public void setMaximalPeople(Long maximalPeople) {
		this.maximalPeople = maximalPeople;
	}

	/**
	 * 获取签约时间的值
	 * @return signTime  
	 */
	public Long getSignTime() {
		return signTime;
	}

	/**
	 * 设置签约时间的值
	 * @param signTime
	 */
	public void setSignTime(Long signTime) {
		this.signTime = signTime;
	}

	/**
	 * 获取总质量(kg)的值
	 * @return totalMass  
	 */
	public Long getTotalMass() {
		return totalMass;
	}

	/**
	 * 设置总质量(kg)的值
	 * @param totalMass
	 */
	public void setTotalMass(Long totalMass) {
		this.totalMass = totalMass;
	}

	/**
	 * 获取运营机构ID的值
	 * @return vehicleOperationId  
	 */
	public String getVehicleOperationId() {
		return vehicleOperationId;
	}

	/**
	 * 设置运营机构ID的值
	 * @param vehicleOperationId
	 */
	public void setVehicleOperationId(String vehicleOperationId) {
		this.vehicleOperationId = vehicleOperationId;
	}

	/**
	 * 获取车辆性质(挂靠车自购车)的值
	 * @return vehicleProperties  
	 */
	public String getVehicleProperties() {
		return vehicleProperties;
	}

	/**
	 * 设置车辆性质(挂靠车自购车)的值
	 * @param vehicleProperties
	 */
	public void setVehicleProperties(String vehicleProperties) {
		this.vehicleProperties = vehicleProperties;
	}

	/**
	 * 获取车辆上牌时间的值
	 * @return vehicleRegisterNoTime  
	 */
	public Long getVehicleRegisterNoTime() {
		return vehicleRegisterNoTime;
	}

	/**
	 * 设置车辆上牌时间的值
	 * @param vehicleRegisterNoTime
	 */
	public void setVehicleRegisterNoTime(Long vehicleRegisterNoTime) {
		this.vehicleRegisterNoTime = vehicleRegisterNoTime;
	}

	/**
	 * 获取接驳车辆(1:是;0或非1:否)的值
	 * @return vhAccess  
	 */
	public Long getVhAccess() {
		return vhAccess;
	}

	/**
	 * 设置接驳车辆(1:是;0或非1:否)的值
	 * @param vhAccess
	 */
	public void setVhAccess(Long vhAccess) {
		this.vhAccess = vhAccess;
	}

	/**
	 * 获取功率(kw)的值
	 * @return watt  
	 */
	public Long getWatt() {
		return watt;
	}

	/**
	 * 设置功率(kw)的值
	 * @param watt
	 */
	public void setWatt(Long watt) {
		this.watt = watt;
	}

	/**
	 * 获取轴距(mm)的值
	 * @return wheelBase  
	 */
	public Long getWheelBase() {
		return wheelBase;
	}

	/**
	 * 设置轴距(mm)的值
	 * @param wheelBase
	 */
	public void setWheelBase(Long wheelBase) {
		this.wheelBase = wheelBase;
	}
	
	
	
}