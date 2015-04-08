package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 报表自定义列<br>
 * 描述： 报表自定义列<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
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
 * <td>2015-1-8</td>
 * <td>Administrator</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author Administrator
 * @since JDK1.6
 */
public class TbReportSet implements Serializable {

	/** */
	private static final long serialVersionUID = 5898218432276066410L;

	/**
	 * tb_report_set.set_id
	 * 
	 */
	private String setId;

	/**
	 * tb_report_set.set_num
	 * 
	 */
	private Integer setNum;

	/**
	 * tb_report_set.set_object
	 * 
	 */
	private String setObject;

	/**
	 * tb_report_set.set_user
	 * 
	 */
	private String setUser;

	/**
	 * tb_report_set.set_name
	 * 
	 */
	private String setName;

	/**
	 * tb_report_set.set_data_name
	 * 
	 */
	private String setDataName;

	/**
	 * tb_report_set.set_width
	 * 
	 */
	private Integer setWidth;

	/**
	 * tb_report_set.is_show
	 * 
	 */
	private String isShow;

	/**
	 * tb_report_set.is_print
	 * 
	 */
	private String isPrint;

	/**
	 * tb_report_set.ser_station_id 服务站id，云平台用
	 */
	private String serStationId;

	/**
	 * tb_report_set.set_book_id 帐套id，云平台用
	 */
	private String setBookId;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人 */
	private Long updateTime;

	public String getSetId() {
		return setId;
	}

	public void setSetId(String setId) {
		this.setId = setId;
	}

	public Integer getSetNum() {
		return setNum;
	}

	public void setSetNum(Integer setNum) {
		this.setNum = setNum;
	}

	public String getSetObject() {
		return setObject;
	}

	public void setSetObject(String setObject) {
		this.setObject = setObject;
	}

	public String getSetUser() {
		return setUser;
	}

	public void setSetUser(String setUser) {
		this.setUser = setUser;
	}

	public String getSetName() {
		return setName;
	}

	public void setSetName(String setName) {
		this.setName = setName;
	}

	public String getSetDataName() {
		return setDataName;
	}

	public void setSetDataName(String setDataName) {
		this.setDataName = setDataName;
	}

	public Integer getSetWidth() {
		return setWidth;
	}

	public void setSetWidth(Integer setWidth) {
		this.setWidth = setWidth;
	}

	public String getIsShow() {
		return isShow;
	}

	public void setIsShow(String isShow) {
		this.isShow = isShow;
	}

	public String getIsPrint() {
		return isPrint;
	}

	public void setIsPrint(String isPrint) {
		this.isPrint = isPrint;
	}

	public String getSerStationId() {
		return serStationId;
	}

	public void setSerStationId(String serStationId) {
		this.serStationId = serStationId;
	}

	public String getSetBookId() {
		return setBookId;
	}

	public void setSetBookId(String setBookId) {
		this.setBookId = setBookId;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

}