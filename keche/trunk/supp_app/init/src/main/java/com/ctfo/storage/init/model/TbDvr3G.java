package com.ctfo.storage.init.model;



import java.io.Serializable;

public class TbDvr3G extends BaseModel implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	/*
	 * 3G视频终端ID，由SEQ_TB_DVR生成
	 */
	private String dvrId = "";
	/*
	 * 3G视频终端编号
	 */
	private String dvrNo = "";
	/*
	 * 所属企业ID(TB_ORGANIZATION.ENT_ID)
	 */
	private String entId = "";
	/*
	 * 3G视频服务器ID（TB_DVRSER.DVRSER_ID）
	 */
	private String dvrserId = "";
	/*
	 * 创建人
	 */
	private String createBy = "";
	private String creatName = "";
	
	/*
	 * 创建时间
	 */
	private long createTime= -1l;
	/*
	 * 修改人
	 */
	private String updateBy= "";
	private String updateName= "";
	/*
	 * 修改时间
	 */
	private String updateTime= "";
	
	private String maker= "";
	
	private String dvrserName= "";
	
	/*
	 * 有效标记 1:有效 0:无效 默认为1
	 */
	private String enableFlag= "";
	private String dvrMakerCode= "";
	private String dvrSerIp= "";
	private String dvrSerPort= "";
	private Integer regStatus= -1;
	private String entName= "";
	
	private long channelNum= -1l;   //视频通道个数
	private String dvrSimNum= ""; //3GSIM卡号
	
	public String getEntName() {
		return entName;
	}
	public void setEntName(String entName) {
		this.entName = entName;
	}
	public Integer getRegStatus() {
		return regStatus;
	}
	public void setRegStatus(Integer regStatus) {
		this.regStatus = regStatus;
	}
	public String getDvrSerPort() {
		return dvrSerPort;
	}
	public void setDvrSerPort(String dvrSerPort) {
		this.dvrSerPort = dvrSerPort;
	}
	public String getDvrSerIp() {
		return dvrSerIp;
	}
	public void setDvrSerIp(String dvrSerIp) {
		this.dvrSerIp = dvrSerIp;
	}
	public String getDvrMakerCode() {
		return dvrMakerCode;
	}
	public void setDvrMakerCode(String dvrMakerCode) {
		this.dvrMakerCode = dvrMakerCode;
	}
	public String getMaker() {
		return maker;
	}
	public void setMaker(String maker) {
		this.maker = maker;
	}
	public String getDvrserName() {
		return dvrserName;
	}
	public void setDvrserName(String dvrserName) {
		this.dvrserName = dvrserName;
	}
	public String getDvrId() {
		return dvrId;
	}
	public void setDvrId(String dvrId) {
		this.dvrId = dvrId;
	}
	public String getDvrNo() {
		return dvrNo;
	}
	public void setDvrNo(String dvrNo) {
		this.dvrNo = dvrNo;
	}
	public String getEntId() {
		return entId;
	}
	public void setEntId(String entId) {
		this.entId = entId;
	}
	public String getDvrserId() {
		return dvrserId;
	}
	public void setDvrserId(String dvrserId) {
		this.dvrserId = dvrserId;
	}
	public long getCreateTime() {
		return createTime;
	}
	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}
	public String getUpdateTime() {
		return updateTime;
	}
	public void setUpdateTime(String updateTime) {
		this.updateTime = updateTime;
	}
	public String getEnableFlag() {
		return enableFlag;
	}
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	public String getCreateBy() {
		return createBy;
	}
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}
	public String getCreatName() {
		return creatName;
	}
	public void setCreatName(String creatName) {
		this.creatName = creatName;
	}
	public String getUpdateBy() {
		return updateBy;
	}
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	public String getUpdateName() {
		return updateName;
	}
	public void setUpdateName(String updateName) {
		this.updateName = updateName;
	}
	public long getChannelNum() {
		return channelNum;
	}
	public void setChannelNum(long channelNum) {
		this.channelNum = channelNum;
	}
	public String getDvrSimNum() {
		return dvrSimNum;
	}
	public void setDvrSimNum(String dvrSimNum) {
		this.dvrSimNum = dvrSimNum;
	}
}


