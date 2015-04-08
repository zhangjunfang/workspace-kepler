package com.ctfo.storage.model.support;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 公司表<br>
 * 描述： 公司表<br>
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
public class TbComInfo implements Serializable {

	/** */
	private static final long serialVersionUID = -4170834732088079878L;

	/** 服务端在线状态0离线；1在线 */
	private String serviceStatus;

	/** 宇通CRM系统链路0异常1正常 */
	private String ytCrmLinkedStatus;

	/** CS的服务端公司Id，服务站Id */
	private String serStationId;

	/** 公司档案ID */
	private String setBookId;

	/** mac地址 */
	private String macAddress;

	/** 服务端版本 */
	private String serviceVersion;

	public String getServiceStatus() {
		return serviceStatus;
	}

	public void setServiceStatus(String serviceStatus) {
		this.serviceStatus = serviceStatus;
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

	public String getYtCrmLinkedStatus() {
		return ytCrmLinkedStatus;
	}

	public void setYtCrmLinkedStatus(String ytCrmLinkedStatus) {
		this.ytCrmLinkedStatus = ytCrmLinkedStatus;
	}

	public String getMacAddress() {
		return macAddress;
	}

	public void setMacAddress(String macAddress) {
		this.macAddress = macAddress;
	}

	public String getServiceVersion() {
		return serviceVersion;
	}

	public void setServiceVersion(String serviceVersion) {
		this.serviceVersion = serviceVersion;
	}

}
