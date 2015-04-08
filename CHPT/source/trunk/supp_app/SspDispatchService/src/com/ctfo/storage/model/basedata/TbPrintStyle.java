package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 打印模板表<br>
 * 描述： 打印模板表<br>
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
public class TbPrintStyle implements Serializable {

	/** */
	private static final long serialVersionUID = 4278318003721461756L;

	/**
	 * tb_print_style.style_id
	 * 
	 */
	private String styleId;

	/**
	 * tb_print_style.style_object
	 * 
	 */
	private String styleObject;

	/**
	 * tb_print_style.style_name
	 * 
	 */
	private String styleName;

	/**
	 * tb_print_style.style_url
	 * 
	 */
	private String styleUrl;

	/**
	 * tb_print_style.create_by
	 * 
	 */
	private String createBy;

	/**
	 * tb_print_style.create_time
	 * 
	 */
	private Long createTime;

	/**
	 * tb_print_style.update_by
	 * 
	 */
	private String updateBy;

	/**
	 * tb_print_style.update_time
	 * 
	 */
	private Long updateTime;

	/**
	 * tb_print_style.is_default
	 * 
	 */
	private String isDefault;

	/**
	 * tb_print_style.ser_station_id 服务站id，云平台用
	 */
	private String serStationId;

	/**
	 * tb_print_style.set_book_id 帐套id，云平台用
	 */
	private String setBookId;

	public String getStyleId() {
		return styleId;
	}

	public void setStyleId(String styleId) {
		this.styleId = styleId;
	}

	public String getStyleObject() {
		return styleObject;
	}

	public void setStyleObject(String styleObject) {
		this.styleObject = styleObject;
	}

	public String getStyleName() {
		return styleName;
	}

	public void setStyleName(String styleName) {
		this.styleName = styleName;
	}

	public String getStyleUrl() {
		return styleUrl;
	}

	public void setStyleUrl(String styleUrl) {
		this.styleUrl = styleUrl;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public String getIsDefault() {
		return isDefault;
	}

	public void setIsDefault(String isDefault) {
		this.isDefault = isDefault;
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
}