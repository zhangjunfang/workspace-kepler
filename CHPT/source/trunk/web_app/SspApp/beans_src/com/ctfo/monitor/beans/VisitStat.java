package com.ctfo.monitor.beans;

import java.io.Serializable;

public class VisitStat implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = -8830278722654925562L;
	private String comName;
	private String setbookName;
	private long accessTime;
	private String accessTimeStart;
	private String accessTimeEnd;
	private String funId;
	private String funName;
	private String total;
	
	
	
	public String getFunName() {
		return funName;
	}
	public void setFunName(String funName) {
		this.funName = funName;
	}
	public long getAccessTime() {
		return accessTime;
	}
	public void setAccessTime(long accessTime) {
		this.accessTime = accessTime;
	}
	public String getComName() {
		return comName;
	}
	public void setComName(String comName) {
		this.comName = comName;
	}
	public String getSetbookName() {
		return setbookName;
	}
	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}
	public String getAccessTimeStart() {
		return accessTimeStart;
	}
	public void setAccessTimeStart(String accessTimeStart) {
		this.accessTimeStart = accessTimeStart;
	}
	public String getAccessTimeEnd() {
		return accessTimeEnd;
	}
	public void setAccessTimeEnd(String accessTimeEnd) {
		this.accessTimeEnd = accessTimeEnd;
	}
	public String getFunId() {
		return funId;
	}
	public void setFunId(String funId) {
		this.funId = funId;
	}
	public String getTotal() {
		return total;
	}
	public void setTotal(String total) {
		this.total = total;
	}
}
