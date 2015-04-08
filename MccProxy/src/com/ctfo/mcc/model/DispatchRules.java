package com.ctfo.mcc.model;

import com.ctfo.mcc.utils.Base64_URl;


public class DispatchRules {
	/**	调度信息编号	*/
	private String id;
	/**	调度信息名称	*/
	private String name;
	/**	创建人编号	*/
	private String createBy;
	/**	创建人编号	*/
	private long createUtc;
	/**	调度信息内容	*/
	private String content;
	/**	调度信息BASE64内容	*/
	private String contentBase64;
	/**	开始日期	*/
	private long startUtc;
	/**	结束日期	*/
	private long endUtc;
	/**	发送时间	*/
	private String sendTime;
	/**	信息类型（紧急:1 ; 语音TTS播报:8; 终端显示:4; 广告屏显示:16 ; 注:选择多个逗号分隔	*/
	private String type;
	/**	信息类型（紧急:1 ; 语音TTS播报:8; 终端显示:4; 广告屏显示:16 ; 注:选择多个逗号分隔	*/
	private int typeFlag;
	/**	离线状态 (是否离线发送 1:是; 0:否	*/
	private int isOffline;
	/**	离线周期  (单位:小时)	*/
	private int offlineCycle;
	/**	发送周期(发送周期，1:周日; 2:周一; 3:周二; 4:周三; 5:周四; 6:周五; 7:周六; 注：多个逗号分隔)	*/
	private String sendCycle;
	/**	系统时间	*/
	private long oracleSysUtc;
	/**
	 * 获得调度信息编号的值
	 * @return the id 调度信息编号  
	 */
	public String getId() {
		return id;
	}
	/**
	 * 设置调度信息编号的值
	 * @param id 调度信息编号  
	 */
	public void setId(String id) {
		this.id = id;
	}

	/**
	 * 获得调度信息名称的值
	 * @return the name 调度信息名称  
	 */
	public String getName() {
		return name;
	}

	/**
	 * 设置调度信息名称的值
	 * @param name 调度信息名称  
	 */
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * 获得创建人编号的值
	 * @return the createBy 创建人编号  
	 */
	public String getCreateBy() {
		return createBy;
	}
	/**
	 * 获得创建人编号的值
	 * @return the createUtc 创建人编号  
	 */
	public long getCreateUtc() {
		return createUtc;
	}
	/**
	 * 设置创建人编号的值
	 * @param createUtc 创建人编号  
	 */
	public void setCreateUtc(long createUtc) {
		this.createUtc = createUtc;
	}
	/**
	 * 设置创建人编号的值
	 * @param createBy 创建人编号  
	 */
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	/**
	 * 获得调度信息内容的值
	 * @return the content 调度信息内容  
	 */
	public String getContent() {
		return content;
	}

	/**
	 * 获得离线状态(是否离线发送1:是;0:否的值
	 * @return the isOffline 离线状态(是否离线发送1:是;0:否  
	 */
	public int getIsOffline() {
		return isOffline;
	}
	/**
	 * 设置离线状态(是否离线发送1:是;0:否的值
	 * @param isOffline 离线状态(是否离线发送1:是;0:否  
	 */
	public void setIsOffline(int isOffline) {
		this.isOffline = isOffline;
	}
	/**
	 * 设置调度信息内容的值
	 * @param content 调度信息内容  
	 */
	public void setContent(String content) {
		this.content = content;
	}

	/**
	 * 获得调度信息BASE64内容的值
	 * @return the contentBase64 调度信息BASE64内容  
	 */
	public String getContentBase64() {
		return contentBase64;
	}

	/**
	 * 设置调度信息BASE64内容的值
	 * @param contentBase64 调度信息BASE64内容  
	 */
	public void setContentBase64(String contentBase64) {
		this.contentBase64 = contentBase64;
	}

	/**
	 * 获得开始日期的值
	 * @return the startUtc 开始日期  
	 */
	public long getStartUtc() {
		return startUtc;
	}

	/**
	 * 设置开始日期的值
	 * @param startUtc 开始日期  
	 */
	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}

	/**
	 * 获得结束日期的值
	 * @return the endUtc 结束日期  
	 */
	public long getEndUtc() {
		return endUtc;
	}

	/**
	 * 设置结束日期的值
	 * @param endUtc 结束日期  
	 */
	public void setEndUtc(long endUtc) {
		this.endUtc = endUtc;
	}

	/**
	 * 获得发送时间的值
	 * @return the sendTime 发送时间  
	 */
	public String getSendTime() {
		return sendTime;
	}

	/**
	 * 设置发送时间的值
	 * @param sendTime 发送时间  
	 */
	public void setSendTime(String sendTime) {
		this.sendTime = sendTime;
	}

	/**
	 * 获得信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔的值
	 * @return the type 信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔  
	 */
	public String getType() {
		return type;
	}

	/**
	 * 设置信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔的值
	 * @param type 信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔  
	 */
	public void setType(String type) {
		this.type = type;
	}

	/**
	 * 获得信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔的值
	 * @return the typeFlag 信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔  
	 */
	public int getTypeFlag() {
		return typeFlag;
	}
	/**
	 * 设置信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔的值
	 * @param typeFlag 信息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔  
	 */
	public void setTypeFlag(int typeFlag) {
		this.typeFlag = typeFlag;
	}

	/**
	 * 获得离线周期(单位:小时)的值
	 * @return the offlineCycle 离线周期(单位:小时)  
	 */
	public int getOfflineCycle() {
		return offlineCycle;
	}

	/**
	 * 设置离线周期(单位:小时)的值
	 * @param offlineCycle 离线周期(单位:小时)  
	 */
	public void setOfflineCycle(int offlineCycle) {
		this.offlineCycle = offlineCycle;
	}

	/**
	 * 获得发送周期(发送周期，1:周日;2:周一;3:周二;4:周三;5:周四;6:周五;7:周六;注：多个逗号分隔)的值
	 * @return the sendCycle 发送周期(发送周期，1:周日;2:周一;3:周二;4:周三;5:周四;6:周五;7:周六;注：多个逗号分隔)  
	 */
	public String getSendCycle() {
		return sendCycle;
	}

	/**
	 * 设置发送周期(发送周期，1:周日;2:周一;3:周二;4:周三;5:周四;6:周五;7:周六;注：多个逗号分隔)的值
	 * @param sendCycle 发送周期(发送周期，1:周日;2:周一;3:周二;4:周三;5:周四;6:周五;7:周六;注：多个逗号分隔)  
	 */
	public void setSendCycle(String sendCycle) {
		this.sendCycle = sendCycle;
	}


	/**
	 * 获得系统时间的值
	 * @return the oracleSysUtc 系统时间  
	 */
	public long getOracleSysUtc() {
		return oracleSysUtc;
	}
	/**
	 * 设置系统时间的值
	 * @param oracleSysUtc 系统时间  
	 */
	public void setOracleSysUtc(long oracleSysUtc) {
		this.oracleSysUtc = oracleSysUtc;
	}
	/**
	 * 将中文内容转为BASE64
	 */
	public void generageBase64() {
		if(this.content != null){
			setContentBase64(Base64_URl.base64Encode(this.content));
		} else {
			setContentBase64("");
		}
	}
	
}
