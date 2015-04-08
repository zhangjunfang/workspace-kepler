package com.ctfo.combusiness.beans;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.local.bean.ETB_Base;

/**
 * 公告信息
 * @author xuehui
 *
 */
@SuppressWarnings("serial")
public class TbPublishInfo extends ETB_Base {
	
	/**
	 * 日志
	 */
	private static Log log = LogFactory.getLog(TbPublishInfo.class);

	/** 当前系统时间 */
	private Long sysTime;

	private String userName;

	private String msgType;

	private String publicInfoTime;

	private Long infoId;

	private String infoType;

	private String infoTheme;

	private String infoStatus;

	private Long publishUser;

	private Long publishTime;

	private Short accessoryFlag;

	private Long entId;

	private Short isSettop;

	private Long createBy;

	private Long createTime;

	private Long updateBy;

	private Long updateTime;

	private String enableFlag;

	private Long enddate;

	private String infoContent;

	public Long getSysTime() {
		return sysTime;
	}

	public void setSysTime(Long sysTime) {
		this.sysTime = sysTime;
	}

	public Long getInfoId() {
		return infoId;
	}

	public void setInfoId(Long infoId) {
		this.infoId = infoId;
	}

	public String getInfoType() {
		return infoType;
	}

	public void setInfoType(String infoType) {
		this.infoType = infoType;
	}

	public String getInfoTheme() {
		return infoTheme;
	}

	public void setInfoTheme(String infoTheme) {
		this.infoTheme = infoTheme;
	}

	public String getInfoStatus() {
		return infoStatus;
	}

	public void setInfoStatus(String infoStatus) {
		this.infoStatus = infoStatus;
	}

	public Long getPublishUser() {
		return publishUser;
	}

	public void setPublishUser(Long publishUser) {
		this.publishUser = publishUser;
	}

	public Long getPublishTime() {
		return publishTime;
	}

	public void setPublishTime(Long publishTime) {
		this.publishTime = publishTime;
	}

	public Short getAccessoryFlag() {
		return accessoryFlag;
	}

	public void setAccessoryFlag(Short accessoryFlag) {
		this.accessoryFlag = accessoryFlag;
	}

	public Long getEntId() {
		return entId;
	}

	public void setEntId(Long entId) {
		this.entId = entId;
	}

	public Short getIsSettop() {
		return isSettop;
	}

	public void setIsSettop(Short isSettop) {
		this.isSettop = isSettop;
	}

	public Long getCreateBy() {
		return createBy;
	}

	public void setCreateBy(Long createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public Long getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(Long updateBy) {
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

	public Long getEnddate() {
		return enddate;
	}

	public void setEnddate(Long enddate) {
		this.enddate = enddate;
	}

	public String getInfoContent() {
		return infoContent;
	}

	public String getUserName() {
		return userName;
	}

	public void setUserName(String userName) {
		this.userName = userName;
	}

	public String getMsgType() {
		return msgType;
	}

	public void setMsgType(String msgType) {
		this.msgType = msgType;
	}

	public void setInfoContent(String infoContent) {
		this.infoContent = infoContent;
	}

	public String getPublicInfoTime() {
		try {
			if (null != this.getPublishTime()) {
				Calendar calendar = Calendar.getInstance();
				calendar.setTimeInMillis(this.getPublishTime());
				Date resultDate = calendar.getTime();
				SimpleDateFormat sdf = new SimpleDateFormat(
						"yyyy-MM-dd HH:mm:ss");
				publicInfoTime = sdf.format(resultDate);
			}
		} catch (Exception e) {
			log.debug(e);
			e.printStackTrace();
		}
		return publicInfoTime;
	}

	public void setPublicInfoTime(String publicInfoTime) {
		this.publicInfoTime = publicInfoTime;
	}

}
