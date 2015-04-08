package com.ctfo.commandservice.model;
/**
 * 	报警督办
 */
public class Supervision {
	/**	编号	*/
	private String id;
	/**	车牌号	*/
	private String plate;
	/**	车牌颜色	*/
	private String plateColor;
	/**	来源	1:车载终端,2:企业监控平台,3:政府监管平台,9其它*/
	private int source;
	/**	类型	*/
	private int type;
	/**	报警时间	*/
	private long alarmUtc;
	/**	督办编号	*/
	private String supervisionId;
	/**	督办截止时间	*/
	private long supervisionDeadline;
	/**	级别	0:紧急,1:一般*/
	private int level;
	/**	督办人	*/
	private String supervisor;
	/**	联系电话	*/
	private String tel;
	/**	电子邮箱	*/
	private String email;
	/**	系统接收时间	*/
	private long utc;
	/**	状态	0：处理中 1：已处理完毕 2：不 作处理 3：将来处理*/
	private int status;
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
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
	public int getSource() {
		return source;
	}
	public void setSource(int source) {
		this.source = source;
	}
	public int getType() {
		return type;
	}
	public void setType(int type) {
		this.type = type;
	}
	public long getAlarmUtc() {
		return alarmUtc;
	}
	public void setAlarmUtc(long alarmUtc) {
		this.alarmUtc = alarmUtc;
	}
	public String getSupervisionId() {
		return supervisionId;
	}
	public void setSupervisionId(String supervisionId) {
		this.supervisionId = supervisionId;
	}
	public long getSupervisionDeadline() {
		return supervisionDeadline;
	}
	public void setSupervisionDeadline(long supervisionDeadline) {
		this.supervisionDeadline = supervisionDeadline;
	}
	public int getLevel() {
		return level;
	}
	public void setLevel(int level) {
		this.level = level;
	}
	public String getSupervisor() {
		return supervisor;
	}
	public void setSupervisor(String supervisor) {
		this.supervisor = supervisor;
	}
	public String getTel() {
		return tel;
	}
	public void setTel(String tel) {
		this.tel = tel;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public long getUtc() {
		return utc;
	}
	public void setUtc(long utc) {
		this.utc = utc;
	}
	public int getStatus() {
		return status;
	}
	public void setStatus(int status) {
		this.status = status;
	}
	
}
