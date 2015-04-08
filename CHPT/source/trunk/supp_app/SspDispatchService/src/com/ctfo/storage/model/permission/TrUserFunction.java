package com.ctfo.storage.model.permission;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 人员与常用功能关系<br>
 * 描述： 人员与常用功能关系<br>
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
 * <td>2014-11-4</td>
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
public class TrUserFunction implements Serializable {

	/** */
	private static final long serialVersionUID = 6989645434307299574L;

	/** user_fun_id */
	private String userFunId;

	/** 人员ID，关联人员档案 */
	private String userId;

	/** 系统功能ID，关联系统功能表 */
	private String funId;

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

	public String getUserFunId() {
		return userFunId;
	}

	public void setUserFunId(String userFunId) {
		this.userFunId = userFunId;
	}

	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public String getFunId() {
		return funId;
	}

	public void setFunId(String funId) {
		this.funId = funId;
	}
}