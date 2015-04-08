package com.ctfo.portalmng.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 路况
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-10-20</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@SuppressWarnings("serial")
public class RoadConditions extends ETB_Base {

	/**
	 * 省、直辖市编码
	 */
	private Long provinceCode;

	/**
	 * 图幅号
	 */
	private Integer symbols;

	/**
	 * 路段等级
	 */
	private Integer roadRank;

	/**
	 * LOCID
	 */
	private Integer locid;

	/**
	 * 事件编号
	 */
	private Integer eventId;

	/**
	 * 事件类型1
	 * 
	 * @param 1-事件，2-交通管制
	 */
	private String eventType1;

	/**
	 * 事件类型2
	 * 
	 * @param 1-事件: 0-无原因现象，1-交通事故，2-火灾，3-路上障碍物，4-道路施工，5-道路作业，6-活动，7-气象，8-灾害
	 * @param 2-交通管制: 0-无限制，1-通行限制，2-转弯限制，3-速度限制，4-入口匝道限制，5-出口匝道限制，6-双向道路单侧通行限制，7-车辆类型限制，8-车道通行限制，9-其他
	 */
	private String eventType2;

	/**
	 * 事件类型3 TODO 暂时不解
	 */
	private String eventType3;

	/**
	 * 开始时间
	 */
	private String startTime;

	/**
	 * 结束时间
	 */
	private String endTime;

	/**
	 * 事件与道路的关系
	 * 
	 * @param 0-事件发生路段 1-事件影响路段 2-事件预警路段
	 */
	private String relation;

	/**
	 * 事件描述字节数
	 */
	private Integer descriptionLength;

	/**
	 * 事件描述
	 */
	private String description;

	public Long getProvinceCode() {
		return provinceCode;
	}

	public void setProvinceCode(Long provinceCode) {
		this.provinceCode = provinceCode;
	}

	public Integer getSymbols() {
		return symbols;
	}

	public void setSymbols(Integer symbols) {
		this.symbols = symbols;
	}

	public Integer getRoadRank() {
		return roadRank;
	}

	public void setRoadRank(Integer roadRank) {
		this.roadRank = roadRank;
	}

	public Integer getLocid() {
		return locid;
	}

	public void setLocid(Integer locid) {
		this.locid = locid;
	}

	public Integer getEventId() {
		return eventId;
	}

	public void setEventId(Integer eventId) {
		this.eventId = eventId;
	}

	public String getEventType1() {
		return eventType1;
	}

	public void setEventType1(String eventType1) {
		this.eventType1 = eventType1;
	}

	public String getEventType2() {
		return eventType2;
	}

	public void setEventType2(String eventType2) {
		this.eventType2 = eventType2;
	}

	public String getEventType3() {
		return eventType3;
	}

	public void setEventType3(String eventType3) {
		this.eventType3 = eventType3;
	}

	public String getStartTime() {
		return startTime;
	}

	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}

	public String getEndTime() {
		return endTime;
	}

	public void setEndTime(String endTime) {
		this.endTime = endTime;
	}

	public String getRelation() {
		return relation;
	}

	public void setRelation(String relation) {
		this.relation = relation;
	}

	public Integer getDescriptionLength() {
		return descriptionLength;
	}

	public void setDescriptionLength(Integer descriptionLength) {
		this.descriptionLength = descriptionLength;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}
}
