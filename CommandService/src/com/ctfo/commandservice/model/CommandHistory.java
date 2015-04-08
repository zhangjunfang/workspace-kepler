package com.ctfo.commandservice.model;

public class CommandHistory {
	/** 用户编号(OP_ID) **/
	private String opId; 
	/**	车牌号(VEHICLE_NO)	*/
	private String plate;
	/**	发送时间utc(CO_SUTC)	*/
	private long sendUtc; 
	/**	指令类型(CO_TYPE)	*/
	private String commandType;
	/**	指令来源（0本平台 1监管平台）(CO_FROM)	*/
	private int commandSource; 
	/**	业务序列号(CO_SEQ)	*/
	private String sequence; 
	/**	通讯方式(CO_CHANNEL)	*/
	private String channel; 
	/**	指令参数,键值对形式(CO_PARM)	*/
	private String param; 
	/**	原始指令字符串(CO_COMMAND)	*/
	private String command; 
	/**	发送状态(1:等待回应; 0:成功)(CO_STATUS)	*/
	private int sendStatus; 	
	/**	指令回应结果描述(CR_RESULT)	*/
	private String result;	 		
	/**	指令回复时间(CR_TIME)	*/
	private long receiveTime; 
	/**	厂商编号(CO_OEMCODE)	*/
	private String oemCode;	 			
	/**	已发送次数(CO_SENDTIMES)	*/
	private int sendNumber;				
	/**	尝试次数(CO_TRYTIMES)	*/
	private int attempts;				
	/**	指令子类型(CO_SUBTYPE)	*/
	private String subType;			
	/**	创建人(CREATE_BY)	*/
	private String createBy;			
	/**	创建时间(CREATE_TIME)	*/
	private long createTime;				
	/**	车辆编号(VID)	*/
	private String vid;					
	/**	指令页面显示内容(CO_TEXT)	*/
	private String pageContent;				
	/**	自增序列(AUTO_ID)	*/
	private String autoId;				
	/**	更新用户(UPDATE_BY)	*/
	private String updateBy;		
	/**	更新时间(UPDATE_TIME)	*/
	private long updateTime;		
	/**	省域编码(AREA_ID)	*/
	private String areaId;	
	/**	拍摄序号(TAKINGSEQ)	*/
	private String takingSeq;
	
	/**
	 * 获得用户编号(OP_ID)的值
	 * @return the opId 用户编号(OP_ID)  
	 */
	public String getOpId() {
		return opId;
	}
	/**
	 * 设置用户编号(OP_ID)的值
	 * @param opId 用户编号(OP_ID)  
	 */
	public void setOpId(String opId) {
		this.opId = opId;
	}
	/**
	 * 获得车牌号(VEHICLE_NO)的值
	 * @return the plate 车牌号(VEHICLE_NO)  
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号(VEHICLE_NO)的值
	 * @param plate 车牌号(VEHICLE_NO)  
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * 获得发送时间utc(CO_SUTC)的值
	 * @return the sendUtc 发送时间utc(CO_SUTC)  
	 */
	public long getSendUtc() {
		return sendUtc;
	}
	/**
	 * 设置发送时间utc(CO_SUTC)的值
	 * @param sendUtc 发送时间utc(CO_SUTC)  
	 */
	public void setSendUtc(long sendUtc) {
		this.sendUtc = sendUtc;
	}
	/**
	 * 获得指令类型(CO_TYPE)的值
	 * @return the commandType 指令类型(CO_TYPE)  
	 */
	public String getCommandType() {
		return commandType;
	}
	/**
	 * 设置指令类型(CO_TYPE)的值
	 * @param commandType 指令类型(CO_TYPE)  
	 */
	public void setCommandType(String commandType) {
		this.commandType = commandType;
	}
	/**
	 * 获得指令来源（0本平台1监管平台）(CO_FROM)的值
	 * @return the commandSource 指令来源（0本平台1监管平台）(CO_FROM)  
	 */
	public int getCommandSource() {
		return commandSource;
	}
	/**
	 * 设置指令来源（0本平台1监管平台）(CO_FROM)的值
	 * @param commandSource 指令来源（0本平台1监管平台）(CO_FROM)  
	 */
	public void setCommandSource(int commandSource) {
		this.commandSource = commandSource;
	}
	/**
	 * 获得业务序列号(CO_SEQ)的值
	 * @return the sequence 业务序列号(CO_SEQ)  
	 */
	public String getSequence() {
		return sequence;
	}
	/**
	 * 设置业务序列号(CO_SEQ)的值
	 * @param sequence 业务序列号(CO_SEQ)  
	 */
	public void setSequence(String sequence) {
		this.sequence = sequence;
	}
	/**
	 * 获得通讯方式(CO_CHANNEL)的值
	 * @return the channel 通讯方式(CO_CHANNEL)  
	 */
	public String getChannel() {
		return channel;
	}
	/**
	 * 设置通讯方式(CO_CHANNEL)的值
	 * @param channel 通讯方式(CO_CHANNEL)  
	 */
	public void setChannel(String channel) {
		this.channel = channel;
	}
	/**
	 * 获得指令参数键值对形式(CO_PARM)的值
	 * @return the param 指令参数键值对形式(CO_PARM)  
	 */
	public String getParam() {
		return param;
	}
	/**
	 * 设置指令参数键值对形式(CO_PARM)的值
	 * @param param 指令参数键值对形式(CO_PARM)  
	 */
	public void setParam(String param) {
		this.param = param;
	}
	/**
	 * 获得原始指令字符串(CO_COMMAND)的值
	 * @return the command 原始指令字符串(CO_COMMAND)  
	 */
	public String getCommand() {
		return command;
	}
	/**
	 * 设置原始指令字符串(CO_COMMAND)的值
	 * @param command 原始指令字符串(CO_COMMAND)  
	 */
	public void setCommand(String command) {
		this.command = command;
	}
	/**
	 * 获得发送状态(1:等待回应;0:成功)(CO_STATUS)的值
	 * @return the sendStatus 发送状态(1:等待回应;0:成功)(CO_STATUS)  
	 */
	public int getSendStatus() {
		return sendStatus;
	}
	/**
	 * 设置发送状态(1:等待回应;0:成功)(CO_STATUS)的值
	 * @param sendStatus 发送状态(1:等待回应;0:成功)(CO_STATUS)  
	 */
	public void setSendStatus(int sendStatus) {
		this.sendStatus = sendStatus;
	}
	/**
	 * 获得指令回应结果描述(CR_RESULT)的值
	 * @return the result 指令回应结果描述(CR_RESULT)  
	 */
	public String getResult() {
		return result;
	}
	/**
	 * 设置指令回应结果描述(CR_RESULT)的值
	 * @param result 指令回应结果描述(CR_RESULT)  
	 */
	public void setResult(String result) {
		this.result = result;
	}
	/**
	 * 获得指令回复时间(CR_TIME)的值
	 * @return the receiveTime 指令回复时间(CR_TIME)  
	 */
	public long getReceiveTime() {
		return receiveTime;
	}
	/**
	 * 设置指令回复时间(CR_TIME)的值
	 * @param receiveTime 指令回复时间(CR_TIME)  
	 */
	public void setReceiveTime(long receiveTime) {
		this.receiveTime = receiveTime;
	}
	/**
	 * 获得厂商编号(CO_OEMCODE)的值
	 * @return the oemCode 厂商编号(CO_OEMCODE)  
	 */
	public String getOemCode() {
		return oemCode;
	}
	/**
	 * 设置厂商编号(CO_OEMCODE)的值
	 * @param oemCode 厂商编号(CO_OEMCODE)  
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}
	/**
	 * 获得已发送次数(CO_SENDTIMES)的值
	 * @return the sendNumber 已发送次数(CO_SENDTIMES)  
	 */
	public int getSendNumber() {
		return sendNumber;
	}
	/**
	 * 设置已发送次数(CO_SENDTIMES)的值
	 * @param sendNumber 已发送次数(CO_SENDTIMES)  
	 */
	public void setSendNumber(int sendNumber) {
		this.sendNumber = sendNumber;
	}
	/**
	 * 获得尝试次数(CO_TRYTIMES)的值
	 * @return the attempts 尝试次数(CO_TRYTIMES)  
	 */
	public int getAttempts() {
		return attempts;
	}
	/**
	 * 设置尝试次数(CO_TRYTIMES)的值
	 * @param attempts 尝试次数(CO_TRYTIMES)  
	 */
	public void setAttempts(int attempts) {
		this.attempts = attempts;
	}
	/**
	 * 获得指令子类型(CO_SUBTYPE)的值
	 * @return the subType 指令子类型(CO_SUBTYPE)  
	 */
	public String getSubType() {
		return subType;
	}
	/**
	 * 设置指令子类型(CO_SUBTYPE)的值
	 * @param subType 指令子类型(CO_SUBTYPE)  
	 */
	public void setSubType(String subType) {
		this.subType = subType;
	}
	/**
	 * 获得创建人(CREATE_BY)的值
	 * @return the createBy 创建人(CREATE_BY)  
	 */
	public String getCreateBy() {
		return createBy;
	}
	/**
	 * 设置创建人(CREATE_BY)的值
	 * @param createBy 创建人(CREATE_BY)  
	 */
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}
	/**
	 * 获得创建时间(CREATE_TIME)的值
	 * @return the createTime 创建时间(CREATE_TIME)  
	 */
	public long getCreateTime() {
		return createTime;
	}
	/**
	 * 设置创建时间(CREATE_TIME)的值
	 * @param createTime 创建时间(CREATE_TIME)  
	 */
	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}
	/**
	 * 获得车辆编号(VID)的值
	 * @return the vid 车辆编号(VID)  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号(VID)的值
	 * @param vid 车辆编号(VID)  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获得指令页面显示内容(CO_TEXT)的值
	 * @return the pageContent 指令页面显示内容(CO_TEXT)  
	 */
	public String getPageContent() {
		return pageContent;
	}
	/**
	 * 设置指令页面显示内容(CO_TEXT)的值
	 * @param pageContent 指令页面显示内容(CO_TEXT)  
	 */
	public void setPageContent(String pageContent) {
		this.pageContent = pageContent;
	}
	/**
	 * 获得自增序列(AUTO_ID)的值
	 * @return the autoId 自增序列(AUTO_ID)  
	 */
	public String getAutoId() {
		return autoId;
	}
	/**
	 * 设置自增序列(AUTO_ID)的值
	 * @param autoId 自增序列(AUTO_ID)  
	 */
	public void setAutoId(String autoId) {
		this.autoId = autoId;
	}
	/**
	 * 获得更新用户(UPDATE_BY)的值
	 * @return the updateBy 更新用户(UPDATE_BY)  
	 */
	public String getUpdateBy() {
		return updateBy;
	}
	/**
	 * 设置更新用户(UPDATE_BY)的值
	 * @param updateBy 更新用户(UPDATE_BY)  
	 */
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	/**
	 * 获得更新时间(UPDATE_TIME)的值
	 * @return the updateTime 更新时间(UPDATE_TIME)  
	 */
	public long getUpdateTime() {
		return updateTime;
	}
	/**
	 * 设置更新时间(UPDATE_TIME)的值
	 * @param updateTime 更新时间(UPDATE_TIME)  
	 */
	public void setUpdateTime(long updateTime) {
		this.updateTime = updateTime;
	}
	/**
	 * 获得省域编码(AREA_ID)的值
	 * @return the areaId 省域编码(AREA_ID)  
	 */
	public String getAreaId() {
		return areaId;
	}
	/**
	 * 设置省域编码(AREA_ID)的值
	 * @param areaId 省域编码(AREA_ID)  
	 */
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}
	/**
	 * 获得拍摄序号(TAKINGSEQ)的值
	 * @return the takingSeq 拍摄序号(TAKINGSEQ)  
	 */
	public String getTakingSeq() {
		return takingSeq;
	}
	/**
	 * 设置拍摄序号(TAKINGSEQ)的值
	 * @param takingSeq 拍摄序号(TAKINGSEQ)  
	 */
	public void setTakingSeq(String takingSeq) {
		this.takingSeq = takingSeq;
	}	

}
