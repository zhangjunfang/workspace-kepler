package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： CS服务端设置<br>
 * 描述： CS服务端设置<br>
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
 * <td>2014-12-5</td>
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
public class SysCsServerSet implements Serializable {

	/** */
	private static final long serialVersionUID = 3207853804535950998L;

	/** id */
	private String csServerSetId;

	/** 服务器IP */
	private String serverIp;

	/** 文件服务端口 */
	private String fileServerPort;

	/** t 数据通信端口 */
	private String dataCommunicationPort;

	/** 附件默认存放目录 */
	private String attachmentDefaultDirectory;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getCsServerSetId() {
		return csServerSetId;
	}

	public void setCsServerSetId(String csServerSetId) {
		this.csServerSetId = csServerSetId;
	}

	public String getServerIp() {
		return serverIp;
	}

	public void setServerIp(String serverIp) {
		this.serverIp = serverIp;
	}

	public String getFileServerPort() {
		return fileServerPort;
	}

	public void setFileServerPort(String fileServerPort) {
		this.fileServerPort = fileServerPort;
	}

	public String getDataCommunicationPort() {
		return dataCommunicationPort;
	}

	public void setDataCommunicationPort(String dataCommunicationPort) {
		this.dataCommunicationPort = dataCommunicationPort;
	}

	public String getAttachmentDefaultDirectory() {
		return attachmentDefaultDirectory;
	}

	public void setAttachmentDefaultDirectory(String attachmentDefaultDirectory) {
		this.attachmentDefaultDirectory = attachmentDefaultDirectory;
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