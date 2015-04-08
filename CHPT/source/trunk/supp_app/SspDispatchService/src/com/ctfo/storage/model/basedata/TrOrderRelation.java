package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 表单关联表<br>
 * 描述： 表单关联表<br>
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
 * <td>2015-1-4</td>
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
public class TrOrderRelation implements Serializable {

	/** */
	private static final long serialVersionUID = 8894224960241325983L;

	/** id */
	private String orderRelationId;

	/** 前置表单ID */
	private String preOrderId;

	/** 前置表单编码 */
	private String preOrderCode;

	/** 后置表单ID */
	private String postOrderId;

	/** 后置表单编码 */
	private String postOrderCode;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getOrderRelationId() {
		return orderRelationId;
	}

	public void setOrderRelationId(String orderRelationId) {
		this.orderRelationId = orderRelationId;
	}

	public String getPreOrderId() {
		return preOrderId;
	}

	public void setPreOrderId(String preOrderId) {
		this.preOrderId = preOrderId;
	}

	public String getPreOrderCode() {
		return preOrderCode;
	}

	public void setPreOrderCode(String preOrderCode) {
		this.preOrderCode = preOrderCode;
	}

	public String getPostOrderId() {
		return postOrderId;
	}

	public void setPostOrderId(String postOrderId) {
		this.postOrderId = postOrderId;
	}

	public String getPostOrderCode() {
		return postOrderCode;
	}

	public void setPostOrderCode(String postOrderCode) {
		this.postOrderCode = postOrderCode;
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
