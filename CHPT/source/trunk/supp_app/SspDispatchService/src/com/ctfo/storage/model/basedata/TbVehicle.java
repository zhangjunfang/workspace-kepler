package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 车辆档案<br>
 * 描述： 车辆档案<br>
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
public class TbVehicle implements Serializable {

	/** */
	private static final long serialVersionUID = -3413600898877179980L;

	/** 车辆档案ID */
	private String vId;

	/** 数据来源，关联字典码表 */
	private String dataSource;

	/** 车牌号 */
	private String licensePlate;

	/** 车辆品牌，关联字典码表 */
	private String vBrand;

	/** 厂家，关联字典码表 */
	private String vFactory;

	/** VIN */
	private String vin;

	/** 车厂编号 */
	private String carFactoryNum;

	/** 车辆颜色，关联码表字典 */
	private String vColor;

	/** 车型，关联字典码表 */
	private String vModel;

	/** 车辆等级，关联字典码表 */
	private String vClass;

	/** 车辆类别，关联字典码表 */
	private String vType;

	/** 所属客户 */
	private String custId;

	/** 出厂日期 */
	private Long exfactoryDate;

	/** 购车日期 */
	private Long carbuyDate;

	/** 备注 */
	private String remark;

	/** 车长 */
	private Integer vLong;

	/** 车宽 */
	private Integer vWide;

	/** 车高 */
	private Integer vHigh;

	/** 轴数 */
	private Integer axleCount;

	/** 轴距 */
	private Integer axleDistance;

	/** 轮距 */
	private Integer wheelDistance;

	/** 总质量 */
	private Integer totalQuality;

	/** 核定质量 */
	private Integer approvedQuality;

	/** 发动机号 */
	private String engineNum;

	/** 排量 */
	private Integer discharge;

	/** 燃油类型，关联字典码表 */
	private String fuelType;

	/** 空调厂家，关字典码表联 */
	private String airFactory;

	/** 空调型号 */
	private String airModel;

	/** 空调编号 */
	private String airNum;

	/** 车桥厂家，关联字典码表 */
	private String axleFactory;

	/** 车桥型号 */
	private String axleModel;

	/** 车桥编号 */
	private String axleNum;

	/** 低盘厂家，关联字典码表 */
	private String lowdiscFactory;

	/** 轮胎数 */
	private Integer wheelCount;

	/** 轮胎厂家，关联字典码表 */
	private String wheelFactory;

	/** 轮胎型号 */
	private String wheelModel;

	/** 变速厂家，关联字典码表 */
	private String shiftFactory;

	/** 变速箱型号 */
	private String shiftModel;

	/** 变速箱号 */
	private String shiftNum;

	/** 变速类型，关联字典码表 */
	private String shiftType;

	/** DVR厂家，关联字典码表 */
	private String dvrFactory;

	/** DVR型号 */
	private String dvrModel;

	/** DVR编号 */
	private String dvrNum;

	/** GPS厂家，关联字典码表 */
	private String gpsFactory;

	/** GPS型号 */
	private String gpsModel;

	/** GPS编号 */
	private String gpsNum;

	/** 发动机型号 */
	private String engineModel;

	/** 发动机厂家，关联字典码表 */
	private String engineFactory;

	/** 车工号--宇通 */
	private String turner;

	/** 客户单位--宇通 */
	private String customerUnit;

	/** 使用单位--宇通 */
	private String useUnit;

	/** 车辆用途--宇通 */
	private String vehicleUse;

	/** 营运路线--宇通 */
	private String operatingLine;

	/** 出发地点--宇通 */
	private String pointDeparture;

	/** 车主---宇通 关联联系人 */
	private String contName;

	/** 车主电话---宇通 */
	private String contPhone;

	/** 主司机---宇通 关联联系人 */
	private String driverName;

	/** 主司机电话---宇通 */
	private String driverPhone;

	/** 到达地点--宇通 */
	private String placeArrival;

	/** 协议保修时限(月)--宇通 */
	private Integer warrantyPeriod;

	/** 协议保修里程--宇通 */
	private BigDecimal warrantyMileage;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 删除标记，0为删除，1未删除 默认1 */
	private String enableFlag;

	/** 最后修改时间 */
	private Long updateTime;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后修改人，关联人员表 */
	private String updateBy;

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

	public String getVId() {
		return vId;
	}

	public void setVId(String vId) {
		this.vId = vId;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
	}

	public String getLicensePlate() {
		return licensePlate;
	}

	public void setLicensePlate(String licensePlate) {
		this.licensePlate = licensePlate;
	}

	public String getVBrand() {
		return vBrand;
	}

	public void setVBrand(String vBrand) {
		this.vBrand = vBrand;
	}

	public String getVFactory() {
		return vFactory;
	}

	public void setVFactory(String vFactory) {
		this.vFactory = vFactory;
	}

	public String getVin() {
		return vin;
	}

	public void setVin(String vin) {
		this.vin = vin;
	}

	public String getCarFactoryNum() {
		return carFactoryNum;
	}

	public void setCarFactoryNum(String carFactoryNum) {
		this.carFactoryNum = carFactoryNum;
	}

	public String getVColor() {
		return vColor;
	}

	public void setVColor(String vColor) {
		this.vColor = vColor;
	}

	public String getVModel() {
		return vModel;
	}

	public void setVModel(String vModel) {
		this.vModel = vModel;
	}

	public String getVClass() {
		return vClass;
	}

	public void setVClass(String vClass) {
		this.vClass = vClass;
	}

	public String getVType() {
		return vType;
	}

	public void setVType(String vType) {
		this.vType = vType;
	}

	public String getCustId() {
		return custId;
	}

	public void setCustId(String custId) {
		this.custId = custId;
	}

	public Long getExfactoryDate() {
		return exfactoryDate;
	}

	public void setExfactoryDate(Long exfactoryDate) {
		this.exfactoryDate = exfactoryDate;
	}

	public Long getCarbuyDate() {
		return carbuyDate;
	}

	public void setCarbuyDate(Long carbuyDate) {
		this.carbuyDate = carbuyDate;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public Integer getVLong() {
		return vLong;
	}

	public void setVLong(Integer vLong) {
		this.vLong = vLong;
	}

	public Integer getVWide() {
		return vWide;
	}

	public void setVWide(Integer vWide) {
		this.vWide = vWide;
	}

	public Integer getVHigh() {
		return vHigh;
	}

	public void setVHigh(Integer vHigh) {
		this.vHigh = vHigh;
	}

	public Integer getAxleCount() {
		return axleCount;
	}

	public void setAxleCount(Integer axleCount) {
		this.axleCount = axleCount;
	}

	public Integer getAxleDistance() {
		return axleDistance;
	}

	public void setAxleDistance(Integer axleDistance) {
		this.axleDistance = axleDistance;
	}

	public Integer getWheelDistance() {
		return wheelDistance;
	}

	public void setWheelDistance(Integer wheelDistance) {
		this.wheelDistance = wheelDistance;
	}

	public Integer getTotalQuality() {
		return totalQuality;
	}

	public void setTotalQuality(Integer totalQuality) {
		this.totalQuality = totalQuality;
	}

	public Integer getApprovedQuality() {
		return approvedQuality;
	}

	public void setApprovedQuality(Integer approvedQuality) {
		this.approvedQuality = approvedQuality;
	}

	public String getEngineNum() {
		return engineNum;
	}

	public void setEngineNum(String engineNum) {
		this.engineNum = engineNum;
	}

	public Integer getDischarge() {
		return discharge;
	}

	public void setDischarge(Integer discharge) {
		this.discharge = discharge;
	}

	public String getFuelType() {
		return fuelType;
	}

	public void setFuelType(String fuelType) {
		this.fuelType = fuelType;
	}

	public String getAirFactory() {
		return airFactory;
	}

	public void setAirFactory(String airFactory) {
		this.airFactory = airFactory;
	}

	public String getAirModel() {
		return airModel;
	}

	public void setAirModel(String airModel) {
		this.airModel = airModel;
	}

	public String getAirNum() {
		return airNum;
	}

	public void setAirNum(String airNum) {
		this.airNum = airNum;
	}

	public String getAxleFactory() {
		return axleFactory;
	}

	public void setAxleFactory(String axleFactory) {
		this.axleFactory = axleFactory;
	}

	public String getAxleModel() {
		return axleModel;
	}

	public void setAxleModel(String axleModel) {
		this.axleModel = axleModel;
	}

	public String getAxleNum() {
		return axleNum;
	}

	public void setAxleNum(String axleNum) {
		this.axleNum = axleNum;
	}

	public String getLowdiscFactory() {
		return lowdiscFactory;
	}

	public void setLowdiscFactory(String lowdiscFactory) {
		this.lowdiscFactory = lowdiscFactory;
	}

	public Integer getWheelCount() {
		return wheelCount;
	}

	public void setWheelCount(Integer wheelCount) {
		this.wheelCount = wheelCount;
	}

	public String getWheelFactory() {
		return wheelFactory;
	}

	public void setWheelFactory(String wheelFactory) {
		this.wheelFactory = wheelFactory;
	}

	public String getWheelModel() {
		return wheelModel;
	}

	public void setWheelModel(String wheelModel) {
		this.wheelModel = wheelModel;
	}

	public String getShiftFactory() {
		return shiftFactory;
	}

	public void setShiftFactory(String shiftFactory) {
		this.shiftFactory = shiftFactory;
	}

	public String getShiftModel() {
		return shiftModel;
	}

	public void setShiftModel(String shiftModel) {
		this.shiftModel = shiftModel;
	}

	public String getShiftNum() {
		return shiftNum;
	}

	public void setShiftNum(String shiftNum) {
		this.shiftNum = shiftNum;
	}

	public String getShiftType() {
		return shiftType;
	}

	public void setShiftType(String shiftType) {
		this.shiftType = shiftType;
	}

	public String getDvrFactory() {
		return dvrFactory;
	}

	public void setDvrFactory(String dvrFactory) {
		this.dvrFactory = dvrFactory;
	}

	public String getDvrModel() {
		return dvrModel;
	}

	public void setDvrModel(String dvrModel) {
		this.dvrModel = dvrModel;
	}

	public String getDvrNum() {
		return dvrNum;
	}

	public void setDvrNum(String dvrNum) {
		this.dvrNum = dvrNum;
	}

	public String getGpsFactory() {
		return gpsFactory;
	}

	public void setGpsFactory(String gpsFactory) {
		this.gpsFactory = gpsFactory;
	}

	public String getGpsModel() {
		return gpsModel;
	}

	public void setGpsModel(String gpsModel) {
		this.gpsModel = gpsModel;
	}

	public String getGpsNum() {
		return gpsNum;
	}

	public void setGpsNum(String gpsNum) {
		this.gpsNum = gpsNum;
	}

	public String getEngineModel() {
		return engineModel;
	}

	public void setEngineModel(String engineModel) {
		this.engineModel = engineModel;
	}

	public String getEngineFactory() {
		return engineFactory;
	}

	public void setEngineFactory(String engineFactory) {
		this.engineFactory = engineFactory;
	}

	public String getTurner() {
		return turner;
	}

	public void setTurner(String turner) {
		this.turner = turner;
	}

	public String getCustomerUnit() {
		return customerUnit;
	}

	public void setCustomerUnit(String customerUnit) {
		this.customerUnit = customerUnit;
	}

	public String getUseUnit() {
		return useUnit;
	}

	public void setUseUnit(String useUnit) {
		this.useUnit = useUnit;
	}

	public String getVehicleUse() {
		return vehicleUse;
	}

	public void setVehicleUse(String vehicleUse) {
		this.vehicleUse = vehicleUse;
	}

	public String getOperatingLine() {
		return operatingLine;
	}

	public void setOperatingLine(String operatingLine) {
		this.operatingLine = operatingLine;
	}

	public String getPointDeparture() {
		return pointDeparture;
	}

	public void setPointDeparture(String pointDeparture) {
		this.pointDeparture = pointDeparture;
	}

	public String getContName() {
		return contName;
	}

	public void setContName(String contName) {
		this.contName = contName;
	}

	public String getContPhone() {
		return contPhone;
	}

	public void setContPhone(String contPhone) {
		this.contPhone = contPhone;
	}

	public String getDriverName() {
		return driverName;
	}

	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}

	public String getDriverPhone() {
		return driverPhone;
	}

	public void setDriverPhone(String driverPhone) {
		this.driverPhone = driverPhone;
	}

	public String getPlaceArrival() {
		return placeArrival;
	}

	public void setPlaceArrival(String placeArrival) {
		this.placeArrival = placeArrival;
	}

	public Integer getWarrantyPeriod() {
		return warrantyPeriod;
	}

	public void setWarrantyPeriod(Integer warrantyPeriod) {
		this.warrantyPeriod = warrantyPeriod;
	}

	public BigDecimal getWarrantyMileage() {
		return warrantyMileage;
	}

	public void setWarrantyMileage(BigDecimal warrantyMileage) {
		this.warrantyMileage = warrantyMileage;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
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
}