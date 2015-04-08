package com.ctfo.storage.model.support;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 访问统计<br>
 * 描述： 访问统计<br>
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
 * <td>2014-12-4</td>
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
public class TlUserFunctionLog implements Serializable {

	/** */
	private static final long serialVersionUID = 3323082074875722830L;

	/** id */
	private String uFLogId;

	/** 用户ID */
	private String userId;

	/** 公司ID */
	private String comId;

	/** 帐套ID */
	private String setbookId;

	/** 访问时间 */
	private Long accessTime;

	/** 功能模块ID */
	private String funId;

	public String getUFLogId() {
		return uFLogId;
	}

	public void setUFLogId(String uFLogId) {
		this.uFLogId = uFLogId;
	}

	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public String getComId() {
		return comId;
	}

	public void setComId(String comId) {
		this.comId = comId;
	}

	public String getSetbookId() {
		return setbookId;
	}

	public void setSetbookId(String setbookId) {
		this.setbookId = setbookId;
	}

	public Long getAccessTime() {
		return accessTime;
	}

	public void setAccessTime(Long accessTime) {
		this.accessTime = accessTime;
	}

	public String getFunId() {
		return funId;
	}

	public void setFunId(String funId) {
		this.funId = funId;
	}

}
