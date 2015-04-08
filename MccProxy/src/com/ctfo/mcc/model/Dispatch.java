package com.ctfo.mcc.model;

public class Dispatch {
	/**	车辆编号	*/
	private String id;
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车牌颜色	*/
	private String plateColor;
	/**	消息创建时间	*/
	private long createUtc;
	/**	消息发送时间	*/
	private long sendUtc;
	/**	消息类型（紧急:1 ; 语音TTS播报:8; 终端显示:4; 广告屏显示:16 ; 注:选择多个逗号分隔	*/
	private String type;
	/**	信息类型（紧急:1 ; 语音TTS播报:8; 终端显示:4; 广告屏显示:16 ; 注:选择多个逗号分隔	*/
	private int typeFlag;
	/**	发送内容	*/
	private String content;
	/**	发送序列号	*/
	private String seq;
	/**	发送人	*/
	private String createBy;
	/**
	 * 获得车辆编号的值
	 * @return the id 车辆编号  
	 */
	public String getId() {
		return id;
	}
	/**
	 * 设置车辆编号的值
	 * @param id 车辆编号  
	 */
	public void setId(String id) {
		this.id = id;
	}
	/**
	 * 获得车辆编号的值
	 * @return the vid 车辆编号  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid 车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获得车牌号的值
	 * @return the plate 车牌号  
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号的值
	 * @param plate 车牌号  
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * 获得车牌颜色的值
	 * @return the plateColor 车牌颜色  
	 */
	public String getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌颜色的值
	 * @param plateColor 车牌颜色  
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	/**
	 * 获得消息创建时间的值
	 * @return the createUtc 消息创建时间  
	 */
	public long getCreateUtc() {
		return createUtc;
	}
	/**
	 * 设置消息创建时间的值
	 * @param createUtc 消息创建时间  
	 */
	public void setCreateUtc(long createUtc) {
		this.createUtc = createUtc;
	}
	/**
	 * 获得消息发送时间的值
	 * @return the sendUtc 消息发送时间  
	 */
	public long getSendUtc() {
		return sendUtc;
	}
	/**
	 * 设置消息发送时间的值
	 * @param sendUtc 消息发送时间  
	 */
	public void setSendUtc(long sendUtc) {
		this.sendUtc = sendUtc;
	}
	/**
	 * 获得消息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔的值
	 * @return the type 消息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔  
	 */
	public String getType() {
		return type;
	}
	/**
	 * 设置消息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔的值
	 * @param type 消息类型（紧急:1;语音TTS播报:8;终端显示:4;广告屏显示:16;注:选择多个逗号分隔  
	 */
	public void setType(String type) {
		this.type = type;
	}
	/**
	 * 获得发送内容的值
	 * @return the content 发送内容  
	 */
	public String getContent() {
		return content;
	}
	/**
	 * 设置发送内容的值
	 * @param content 发送内容  
	 */
	public void setContent(String content) {
		this.content = content;
	}
	/**
	 * 获得发送序列号的值
	 * @return the seq 发送序列号  
	 */
	public String getSeq() {
		return seq;
	}
	/**
	 * 设置发送序列号的值
	 * @param seq 发送序列号  
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}
	/**
	 * 获得发送人的值
	 * @return the createBy 发送人  
	 */
	public String getCreateBy() {
		return createBy;
	}
	/**
	 * 设置发送人的值
	 * @param createBy 发送人  
	 */
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
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

}
