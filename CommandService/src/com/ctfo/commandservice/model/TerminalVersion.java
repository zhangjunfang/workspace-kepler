package com.ctfo.commandservice.model;

public class TerminalVersion {
	/**	主键uuid	*/
	private String uuid;
	/**	终端硬件版本号	*/
	private String terminalHardVersion;//终端硬件版本号
	/**	终端固件版本号	*/
	private String terminalFirmwareVersion;//终端固件版本号
	/**	SIM卡ICCID	*/
	private String iccid;//SIM卡ICCID
	/**	显示屏硬件版本号	*/
	private String lcdHardVersion;//显示屏硬件版本号
	/**	显示屏固件版本号	*/
	private String lcdFirmwareVersion;//显示屏固件版本号
	/**	硬盘录像机硬件版本号	*/
	private String dvrHardVersion;//硬盘录像机硬件版本号
	/**	硬盘录像机固件版本号	*/
	private String dvrFirmwareVersion;//硬盘录像机固件版本号
	/**	射频读卡器硬件版本号	*/
	private String rfCardHardVersion;//射频读卡器硬件版本号
	/**	射频读卡器固件版本号	*/
	private String rfCardFirmwareVersion;//射频读卡器固件版本号
	/**	车牌号	*/
	private String plate;//车牌号
	/**	车牌颜色	*/
	private String plateColor;//车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
	/**	VIN	*/
	private String vinCode;//
	/**	终端号（ID）	*/
	private String tMac;//
	/**	终端协议版本号	*/
	private String tProtocolVersion;//
	/**	车辆编号	*/
	private String vid;//
	/**	手机号	*/
	private String phoneNumber;//
	/**	终端编号	*/
	private String tId;//
	/**	系统时间	*/
	private long sysUtc;//
	
	
	public String getUuid() {
		return uuid;
	}
	public void setUuid(String uuid) {
		this.uuid = uuid;
	}
	public String getTerminalHardVersion() {
		return terminalHardVersion;
	}
	public void setTerminalHardVersion(String terminalHardVersion) {
		this.terminalHardVersion = terminalHardVersion;
	}
	public String getTerminalFirmwareVersion() {
		return terminalFirmwareVersion;
	}
	public void setTerminalFirmwareVersion(String terminalFirmwareVersion) {
		this.terminalFirmwareVersion = terminalFirmwareVersion;
	}
	public String getIccid() {
		return iccid;
	}
	public void setIccid(String iccid) {
		this.iccid = iccid;
	}
	public String getLcdHardVersion() {
		return lcdHardVersion;
	}
	public void setLcdHardVersion(String lcdHardVersion) {
		this.lcdHardVersion = lcdHardVersion;
	}
	public String getLcdFirmwareVersion() {
		return lcdFirmwareVersion;
	}
	public void setLcdFirmwareVersion(String lcdFirmwareVersion) {
		this.lcdFirmwareVersion = lcdFirmwareVersion;
	}
	public String getDvrHardVersion() {
		return dvrHardVersion;
	}
	public void setDvrHardVersion(String dvrHardVersion) {
		this.dvrHardVersion = dvrHardVersion;
	}
	public String getDvrFirmwareVersion() {
		return dvrFirmwareVersion;
	}
	public void setDvrFirmwareVersion(String dvrFirmwareVersion) {
		this.dvrFirmwareVersion = dvrFirmwareVersion;
	}
	public String getRfCardHardVersion() {
		return rfCardHardVersion;
	}
	public void setRfCardHardVersion(String rfCardHardVersion) {
		this.rfCardHardVersion = rfCardHardVersion;
	}
	public String getRfCardFirmwareVersion() {
		return rfCardFirmwareVersion;
	}
	public void setRfCardFirmwareVersion(String rfCardFirmwareVersion) {
		this.rfCardFirmwareVersion = rfCardFirmwareVersion;
	}
	public String getPlate() {
		return plate;
	}
	public void setPlate(String plate) {
		this.plate = plate;
	}
	public String getPlateColor() {
		return plateColor;
	}
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	public String getVinCode() {
		return vinCode;
	}
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	public String gettMac() {
		return tMac;
	}
	public void settMac(String tMac) {
		this.tMac = tMac;
	}
	public String gettProtocolVersion() {
		return tProtocolVersion;
	}
	public void settProtocolVersion(String tProtocolVersion) {
		this.tProtocolVersion = tProtocolVersion;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getPhoneNumber() {
		return phoneNumber;
	}
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	public String gettId() {
		return tId;
	}
	public void settId(String tId) {
		this.tId = tId;
	}
	public long getSysUtc() {
		return sysUtc;
	}
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	/**
	 * 更新终端版本信息
	 * @param terminalVersion
	 */
	public void update(TerminalVersion terminalVersion) { 
		this.terminalHardVersion = terminalVersion.getTerminalHardVersion();//终端硬件版本号
		/**	终端固件版本号	*/
		this.terminalFirmwareVersion = terminalVersion.getTerminalFirmwareVersion();//终端固件版本号
		/**	SIM卡ICCID	*/
		this.iccid = terminalVersion.getIccid();//SIM卡ICCID
		/**	显示屏硬件版本号	*/
		this.lcdHardVersion = terminalVersion.getLcdHardVersion();//显示屏硬件版本号
		/**	显示屏固件版本号	*/
		this.lcdFirmwareVersion = terminalVersion.getLcdFirmwareVersion();//显示屏固件版本号
		/**	硬盘录像机硬件版本号	*/
		this.dvrHardVersion = terminalVersion.getDvrHardVersion();//硬盘录像机硬件版本号
		/**	硬盘录像机固件版本号	*/
		this.dvrFirmwareVersion = terminalVersion.getDvrFirmwareVersion();//硬盘录像机固件版本号
		/**	射频读卡器硬件版本号	*/
		this.rfCardHardVersion = terminalVersion.getRfCardHardVersion();//射频读卡器硬件版本号
		/**	射频读卡器固件版本号	*/
		this.rfCardFirmwareVersion = terminalVersion.getRfCardFirmwareVersion();//射频读卡器固件版本号
		/**	车牌号	*/
		this.plate = terminalVersion.getPlate();//车牌号
		/**	车牌颜色	*/
		this.plateColor = terminalVersion.getPlateColor();//车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
		/**	VIN	*/
		this.vinCode = terminalVersion.getVinCode();//
		/**	终端号（ID）	*/
		this.tMac = terminalVersion.gettMac();//
		/**	终端协议版本号	*/
		this.tProtocolVersion = terminalVersion.gettProtocolVersion();//
		/**	车辆编号	*/
		this.vid = terminalVersion.getVid();//
		/**	手机号	*/
		this.phoneNumber = terminalVersion.getPhoneNumber();//
		/**	终端编号	*/
		this.tId = terminalVersion.gettId();//
		/**	系统时间	*/
		this.sysUtc = terminalVersion.getSysUtc();//
	}
	
	
}
