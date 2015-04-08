package com.ctfo.memcache.beans;

import com.ctfo.local.bean.ETB_Base;

@SuppressWarnings("serial")
public class AlarmNum extends ETB_Base implements Comparable<AlarmNum> {

	/** 严重告警  */
	private String seriousCount="0";

	/** 紧急告警  */
	private String urgentCount="0";

	/** 一般告警  */
	private String generalCount="0";

	/** 报警提示  */
	private String suggestionCount="0";

	private String alarmDate;
	
	public String getAlarmDate() {
		return alarmDate;
	}

	public void setAlarmDate(String alarmDate) {
		this.alarmDate = alarmDate;
	}

	public String getSeriousCount() {
		return seriousCount;
	}

	public void setSeriousCount(String seriousCount) {
		this.seriousCount = seriousCount;
	}

	public String getUrgentCount() {
		return urgentCount;
	}

	public void setUrgentCount(String urgentCount) {
		this.urgentCount = urgentCount;
	}

	public String getGeneralCount() {
		return generalCount;
	}

	public void setGeneralCount(String generalCount) {
		this.generalCount = generalCount;
	}

	public String getSuggestionCount() {
		return suggestionCount;
	}

	public void setSuggestionCount(String suggestionCount) {
		this.suggestionCount = suggestionCount;
	}
	
	
	@Override

    public int compareTo(AlarmNum arg0) {

        return this.getAlarmDate().compareTo(arg0.getAlarmDate());

    }

}
