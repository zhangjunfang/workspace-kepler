package com.ctfo.commandservice.model;
/**
 * 预警
 */
public class Warning {
	/**	编号	*/
	private String id;
	/**	来源	*/
	private String source;
	/**	类型	*/
	private int type;
	/**	预警时间	*/
	private long warnUtc;
	/**	描述	*/
	private String desc;
	/**	车辆编号	*/
	private String vid;
	/**	系统接收时间	*/
	private long utc;
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getSource() {
		return source;
	}
	public void setSource(String source) {
		this.source = source;
	}
	public int getType() {
		return type;
	}
	public void setType(int type) {
		this.type = type;
	}
	public long getWarnUtc() {
		return warnUtc;
	}
	public void setWarnUtc(long warnUtc) {
		this.warnUtc = warnUtc;
	}
	public String getDesc() {
		return desc;
	}
	public void setDesc(String desc) {
		this.desc = desc;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public long getUtc() {
		return utc;
	}
	public void setUtc(long utc) {
		this.utc = utc;
	}
	
	
}
