package com.ctfo.informationser.monitoring.beans;
import java.io.Serializable;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： BaseInfoSer <br>
 * 功能：Bean供PCC与前置机调用，包含PCC与前置机调用的相关属性对象 <br>
 * 描述：Bean供PCC与前置机调用，包含PCC与前置机调用的相关属性对象 <br>
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
 * <td>Dec 24, 2011</td>
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
public class VehicleInfo implements Serializable {

	/**
	 * serialVersionUID:long
	 */

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 1L;

	/** -1：等待回应0：成功，1：车辆已被注册，2：数据库中无该车辆，3：终端已被注册，4，数据库中无该终端 */
	private Integer result;

	/** 鉴权码 */
	private String akey;

	/** 设备厂家类型编号 */
	private String oemcode;
	/**
	 * 车牌号
	 */
	private String vehicleNo;
	/**
	 * 车牌颜色
	 */
	private String plateColor;
	/**
	 * 终端型号
	 */
	private String tmodelCode;
	/**
	 * 终端唯一标识号
	 */
	private String tmac;
	/**
	 * 手机卡
	 */
	private String commaddr;
	/**
	 * 城市
	 */
	private String cityId;
	/**
	 * 驾驶员姓名
	 */
	private String staffName;
	/**
	 * 身份证
	 */
	private String driverNo;
	/**
	 * 从业资格证
	 */
	private String bussinessId;
	/**
	 * 发证机关
	 */
	private String drivercardDep;
	/**
	 * 电子运单内容
	 */
	private String eticketContent;
	/**
	 * 车辆类型编码
	 */
	private String vehicleType;
	/**
	 * 车辆类型编码
	 */
	private String generalCode;
	/**
	 * 车型名称
	 */
	private String codeName;
	/**
	 * 行业类型
	 */
	private String transtypeCode;
	/**
	 * 行业类型名称
	 */
	private String transtypeName;
	/**
	 * 企业名称
	 */
	private String corpName;
	/**
	 * 运营许可证
	 */
	private String licenceNo;
	/**
	 * 联系人名称
	 */
	private String orgCname;
	/**
	 * 运营机构ID
	 */
	private String vehicleOperationId;
	/**
	 * 联系电话
	 */
	private String orgCphone;
	/**
	 * 车ID
	 */
	private String vid;
	/**
	 * 车状态
	 */
	private String vehicleState;
	/**
	 * 车注册状态
	 */
	private String vehicleRegStatus;
	/**
	 * 终端ID
	 */
	private String tid;
	/**
	 * 终端状态
	 */
	private String terState;
	/**
	 * 终端注册状态
	 */
	private String terRegStatus;
	/**
	 * 鉴权码
	 */
	private String authCode;
	/**
	 * 卡ID
	 */
	private String sid;

	/**
	 * 制造商ID
	 */
	private String manufacturerId;
	

	public String getManufacturerId() {
		return manufacturerId;
	}

	public void setManufacturerId(String manufacturerId) {
		this.manufacturerId = manufacturerId;
	}

	public Integer getResult() {
		return result;
	}

	public void setResult(Integer result) {
		this.result = result;
	}

	public String getAkey() {
		return akey;
	}

	public void setAkey(String akey) {
		this.akey = akey;
	}

	public String getOemcode() {
		return oemcode;
	}

	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}

	/**
	 * @return the vehicleNo
	 */
	public String getVehicleNo() {
		return vehicleNo;
	}

	/**
	 * @param vehicleNo
	 *            the vehicleNo to set
	 */
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	/**
	 * @return the plateColor
	 */
	public String getPlateColor() {
		return plateColor;
	}

	/**
	 * @param plateColor
	 *            the plateColor to set
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}

	/**
	 * @return the tmodelCode
	 */
	public String getTmodelCode() {
		return tmodelCode;
	}

	/**
	 * @param tmodelCode
	 *            the tmodelCode to set
	 */
	public void setTmodelCode(String tmodelCode) {
		this.tmodelCode = tmodelCode;
	}

	/**
	 * @return the tmac
	 */
	public String getTmac() {
		return tmac;
	}

	/**
	 * @param tmac
	 *            the tmac to set
	 */
	public void setTmac(String tmac) {
		this.tmac = tmac;
	}

	/**
	 * @return the commaddr
	 */
	public String getCommaddr() {
		return commaddr;
	}

	/**
	 * @param commaddr
	 *            the commaddr to set
	 */
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	/**
	 * @return the cityId
	 */
	public String getCityId() {
		return cityId;
	}

	/**
	 * @param cityId
	 *            the cityId to set
	 */
	public void setCityId(String cityId) {
		this.cityId = cityId;
	}

	/**
	 * @return the staffName
	 */
	public String getStaffName() {
		return staffName;
	}

	/**
	 * @param staffName
	 *            the staffName to set
	 */
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}

	/**
	 * @return the driverNo
	 */
	public String getDriverNo() {
		return driverNo;
	}

	/**
	 * @param driverNo
	 *            the driverNo to set
	 */
	public void setDriverNo(String driverNo) {
		this.driverNo = driverNo;
	}

	/**
	 * @return the bussinessId
	 */
	public String getBussinessId() {
		return bussinessId;
	}

	/**
	 * @param bussinessId
	 *            the bussinessId to set
	 */
	public void setBussinessId(String bussinessId) {
		this.bussinessId = bussinessId;
	}

	/**
	 * @return the drivercardDep
	 */
	public String getDrivercardDep() {
		return drivercardDep;
	}

	/**
	 * @param drivercardDep
	 *            the drivercardDep to set
	 */
	public void setDrivercardDep(String drivercardDep) {
		this.drivercardDep = drivercardDep;
	}

	/**
	 * @return the eticketContent
	 */
	public String getEticketContent() {
		return eticketContent;
	}

	/**
	 * @param eticketContent
	 *            the eticketContent to set
	 */
	public void setEticketContent(String eticketContent) {
		this.eticketContent = eticketContent;
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
	 * @return the vehicleState
	 */
	public String getVehicleState() {
		return vehicleState;
	}

	/**
	 * @param vehicleState
	 *            the vehicleState to set
	 */
	public void setVehicleState(String vehicleState) {
		this.vehicleState = vehicleState;
	}

	/**
	 * @return the vehicleRegStatus
	 */
	public String getVehicleRegStatus() {
		return vehicleRegStatus;
	}

	/**
	 * @param vehicleRegStatus
	 *            the vehicleRegStatus to set
	 */
	public void setVehicleRegStatus(String vehicleRegStatus) {
		this.vehicleRegStatus = vehicleRegStatus;
	}

	/**
	 * @return the tid
	 */
	public String getTid() {
		return tid;
	}

	/**
	 * @param tid
	 *            the tid to set
	 */
	public void setTid(String tid) {
		this.tid = tid;
	}

	/**
	 * @return the terState
	 */
	public String getTerState() {
		return terState;
	}

	/**
	 * @param terState
	 *            the terState to set
	 */
	public void setTerState(String terState) {
		this.terState = terState;
	}

	/**
	 * @return the terRegStatus
	 */
	public String getTerRegStatus() {
		return terRegStatus;
	}

	/**
	 * @param terRegStatus
	 *            the terRegStatus to set
	 */
	public void setTerRegStatus(String terRegStatus) {
		this.terRegStatus = terRegStatus;
	}

	/**
	 * @return the authCode
	 */
	public String getAuthCode() {
		return authCode;
	}

	/**
	 * @param authCode
	 *            the authCode to set
	 */
	public void setAuthCode(String authCode) {
		this.authCode = authCode;
	}

	/**
	 * @return the sid
	 */
	public String getSid() {
		return sid;
	}

	/**
	 * @param sid
	 *            the sid to set
	 */
	public void setSid(String sid) {
		this.sid = sid;
	}

	/**
	 * @return the vehicleType
	 */
	public String getVehicleType() {
		return vehicleType;
	}

	/**
	 * @param vehicleType
	 *            the vehicleType to set
	 */
	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}

	/**
	 * @return the generalCode
	 */
	public String getGeneralCode() {
		return generalCode;
	}

	/**
	 * @param generalCode
	 *            the generalCode to set
	 */
	public void setGeneralCode(String generalCode) {
		this.generalCode = generalCode;
	}

	/**
	 * @return the codeName
	 */
	public String getCodeName() {
		return codeName;
	}

	/**
	 * @param codeName
	 *            the codeName to set
	 */
	public void setCodeName(String codeName) {
		this.codeName = codeName;
	}

	/**
	 * @return the transtypeCode
	 */
	public String getTranstypeCode() {
		return transtypeCode;
	}

	/**
	 * @param transtypeCode
	 *            the transtypeCode to set
	 */
	public void setTranstypeCode(String transtypeCode) {
		this.transtypeCode = transtypeCode;
	}

	/**
	 * @return the transtypeName
	 */
	public String getTranstypeName() {
		return transtypeName;
	}

	/**
	 * @param transtypeName
	 *            the transtypeName to set
	 */
	public void setTranstypeName(String transtypeName) {
		this.transtypeName = transtypeName;
	}

	/**
	 * @return the corpName
	 */
	public String getCorpName() {
		return corpName;
	}

	/**
	 * @param corpName
	 *            the corpName to set
	 */
	public void setCorpName(String corpName) {
		this.corpName = corpName;
	}

	/**
	 * @return the licenceNo
	 */
	public String getLicenceNo() {
		return licenceNo;
	}

	/**
	 * @param licenceNo
	 *            the licenceNo to set
	 */
	public void setLicenceNo(String licenceNo) {
		this.licenceNo = licenceNo;
	}

	/**
	 * @return the orgCname
	 */
	public String getOrgCname() {
		return orgCname;
	}

	/**
	 * @param orgCname
	 *            the orgCname to set
	 */
	public void setOrgCname(String orgCname) {
		this.orgCname = orgCname;
	}

	/**
	 * @return the vehicleOperationId
	 */
	public String getVehicleOperationId() {
		return vehicleOperationId;
	}

	/**
	 * @param vehicleOperationId
	 *            the vehicleOperationId to set
	 */
	public void setVehicleOperationId(String vehicleOperationId) {
		this.vehicleOperationId = vehicleOperationId;
	}

	/**
	 * @return the orgCphone
	 */
	public String getOrgCphone() {
		return orgCphone;
	}

	/**
	 * @param orgCphone
	 *            the orgCphone to set
	 */
	public void setOrgCphone(String orgCphone) {
		this.orgCphone = orgCphone;
	}

}
