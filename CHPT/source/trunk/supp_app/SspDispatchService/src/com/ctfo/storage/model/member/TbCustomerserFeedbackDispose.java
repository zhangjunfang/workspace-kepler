package com.ctfo.storage.model.member;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 信息反馈记录处理记录<br>
 * 描述： 信息反馈记录处理记录<br>
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
 * <td>2014-11-5</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class TbCustomerserFeedbackDispose implements Serializable {

	/** */
	private static final long serialVersionUID = 8217283655160508498L;

	/** 信息反馈记录ID */
	private String feedbackId;

	/** 处理人id */
	private String disposeById;

	/** 处理人名称 */
	private String disposeByName;

	/** 处理人部门名称 */
	private String disposeOrg;

	/** 处理时间 */
	private Long disposeTime;

	/** 处理意见 */
	private String disposeIdea;

	/** 状态 */
	private Integer status;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

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

	public String getFeedbackId() {
		return feedbackId;
	}

	public void setFeedbackId(String feedbackId) {
		this.feedbackId = feedbackId;
	}

	public String getDisposeById() {
		return disposeById;
	}

	public void setDisposeById(String disposeById) {
		this.disposeById = disposeById;
	}

	public String getDisposeByName() {
		return disposeByName;
	}

	public void setDisposeByName(String disposeByName) {
		this.disposeByName = disposeByName;
	}

	public String getDisposeOrg() {
		return disposeOrg;
	}

	public void setDisposeOrg(String disposeOrg) {
		this.disposeOrg = disposeOrg;
	}

	public Long getDisposeTime() {
		return disposeTime;
	}

	public void setDisposeTime(Long disposeTime) {
		this.disposeTime = disposeTime;
	}

	public String getDisposeIdea() {
		return disposeIdea;
	}

	public void setDisposeIdea(String disposeIdea) {
		this.disposeIdea = disposeIdea;
	}

	public Integer getStatus() {
		return status;
	}

	public void setStatus(Integer status) {
		this.status = status;
	}
}