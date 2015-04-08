package com.ctfo.analy.beans;

/**
 * 企业设置告警下发消息实体类
 *  
 * @author 崔松
 * @date 2013-4-24上午08:23:37
 */
public class AlarmNotice {

    private String entId;//企业ID

    private String alarmClass;//告警类别

    private String alarmCode;//告警编码

    private Short displayFlag;//屏显标志

    private Short ttsFlag;//tts播报标志

    private String msg;//消息体内容
 
    private String  vid;  
    
    private String commaddr;//手机号
     
 	private String msgid;//消息服务器ID	
	
	public String getMsgid() {
		return msgid;
	}

	public void setMsgid(String msgid) {
		this.msgid = msgid;
	}

	public String getOemcode() {
		return oemcode;
	}

	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}

	private String oemcode;//终端识别码
    

    public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getEntId() {
        return entId;
    }

    public void setEntId(String entId) {
        this.entId = entId;
    }

    public String getAlarmClass() {
        return alarmClass;
    }

    public void setAlarmClass(String alarmClass) {
        this.alarmClass = alarmClass == null ? null : alarmClass.trim();
    }

    public String getAlarmCode() {
        return alarmCode;
    }
  
    public void setAlarmCode(String alarmCode) {
        this.alarmCode = alarmCode == null ? null : alarmCode.trim();
    }

    public Short getDisplayFlag() {
        return displayFlag;
    }

    public void setDisplayFlag(Short displayFlag) {
        this.displayFlag = displayFlag;
    }

    public Short getTtsFlag() {
        return ttsFlag;
    }

    public void setTtsFlag(Short ttsFlag) {
        this.ttsFlag = ttsFlag;
    }

    public String getMsg() {
        return msg;
    }

    public void setMsg(String msg) {
        this.msg = msg == null ? null : msg.trim();
    }

}