package com.ctfo.commandservice.model;
/**
 * 发动机故障信息
 * @author Administrator
 *
 */
public class EngineFaultInfo {
	/**  序列号  */
	private String bugId;
	/**  VID  */
	private String vid;
	/**  车牌号  */
	private String vehicleNo;
	/**  所属车辆识别码  */
	private String vinCode;
	/**  终端手机号   可为空  */
	private String commaddr;
	/**  诊断盒状态(00:正常 01:故障)  */
	private String status;
	/**  故障代码(当诊断盒故障时，如果终端没有上传此值，则对应赋为 9999，诊断盒正常，如果中断没有上传此值，则赋为0000)  */
	private String bugCode;
	/**  故障描述  */
	private String bugDesc;
	/**  故障状态说明  */
	private String bugFlag;
	/**  原始故障码  */
	private String basicCode;
	/**  纬度  */
	private long lat;
	/**  经度  */
	private long lon;
	/**  偏转后维度  */
	private long maplat;
	/**  偏转后经度  */
	private long maplon;
	/**  海拔  */
	private int elevation;
	/**  方向  */
	private int direction;
	/**  GPS速度  */
	private int gpsSpeeding;
	/**  上报时间  */
	private long reportTime;
	/**  终端数据     */
	private byte[] diagnosisBytes;
	/**  版本数据号     */
	private String versionCode;
	/**  版本识别号    */
	private String versionValue;
	/**  故障序列ID    */
	private String bugSeqId;
	/**  故障清除标记    */
	private String clearFlag;

	

	/**
	 * 构造方法
	 */
	public EngineFaultInfo() {
	}
	
	public String getBugId() {
		return bugId;
	}
	public void setBugId(String bugId) {
		this.bugId = bugId;
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
	public String getVinCode() {
		return vinCode;
	}
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	public String getCommaddr() {
		return commaddr;
	}
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
	public String getBugCode() {
		return bugCode;
	}
	public void setBugCode(String bugCode) {
		this.bugCode = bugCode;
	}
	public String getBugDesc() {
		return bugDesc;
	}
	public void setBugDesc(String bugDesc) {
		this.bugDesc = bugDesc;
	}
	public String getBugFlag() {
		return bugFlag;
	}
	public void setBugFlag(String bugFlag) {
		this.bugFlag = bugFlag;
	}
	public String getBasicCode() {
		return basicCode;
	}
	public void setBasicCode(String basicCode) {
		this.basicCode = basicCode;
	}
	public long getLat() {
		return lat;
	}
	public void setLat(long lat) {
		this.lat = lat;
	}
	public long getLon() {
		return lon;
	}
	public void setLon(long lon) {
		this.lon = lon;
	}
	public long getMaplat() {
		return maplat;
	}
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}
	public long getMaplon() {
		return maplon;
	}
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}
	public int getElevation() {
		return elevation;
	}
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}
	public int getDirection() {
		return direction;
	}
	public void setDirection(int direction) {
		this.direction = direction;
	}
	public int getGpsSpeeding() {
		return gpsSpeeding;
	}
	public void setGpsSpeeding(int gpsSpeeding) {
		this.gpsSpeeding = gpsSpeeding;
	}
	public long getReportTime() {
		return reportTime;
	}
	public void setReportTime(long reportTime) {
		this.reportTime = reportTime;
	}
	public byte[] getDiagnosisBytes() {
		return diagnosisBytes;
	}
	public void setDiagnosisBytes(byte[] diagnosisBytes) {
		this.diagnosisBytes = diagnosisBytes;
	}
	public String getVersionValue() {
		return versionValue;
	}
	public void setVersionValue(String versionValue) {
		this.versionValue = versionValue;
	}
	public String getVersionCode() {
		return versionCode;
	}
	public void setVersionCode(String versionCode) {
		this.versionCode = versionCode;
	}
	public String getBugSeqId() {
		return bugSeqId;
	}
	public void setBugSeqId(String bugSeqId) {
		this.bugSeqId = bugSeqId;
	}
	public String getClearFlag() {
		return clearFlag;
	}
	public void setClearFlag(String clearFlag) {
		this.clearFlag = clearFlag;
	}
}
