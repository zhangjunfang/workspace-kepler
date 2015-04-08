package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 数据同步<br>
 * 描述： 数据同步<br>
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
public class SysDataSync implements Serializable {

	/**  */
	private static final long serialVersionUID = -2901079316369800155L;

	/** id */
	private String dataSyncId;

	/** 车厂 */
	private String factory;

	/** 业务名 */
	private String businessName;

	/** 本地总条目数 */
	private Integer localTotalNum;

	/** 更新总条目数 */
	private Integer updateTotalNum;

	/** 最后同步时间 */
	private Long lastSyncTime;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getDataSyncId() {
		return dataSyncId;
	}

	public void setDataSyncId(String dataSyncId) {
		this.dataSyncId = dataSyncId;
	}

	public String getFactory() {
		return factory;
	}

	public void setFactory(String factory) {
		this.factory = factory;
	}

	public String getBusinessName() {
		return businessName;
	}

	public void setBusinessName(String businessName) {
		this.businessName = businessName;
	}

	public Integer getLocalTotalNum() {
		return localTotalNum;
	}

	public void setLocalTotalNum(Integer localTotalNum) {
		this.localTotalNum = localTotalNum;
	}

	public Integer getUpdateTotalNum() {
		return updateTotalNum;
	}

	public void setUpdateTotalNum(Integer updateTotalNum) {
		this.updateTotalNum = updateTotalNum;
	}

	public Long getLastSyncTime() {
		return lastSyncTime;
	}

	public void setLastSyncTime(Long lastSyncTime) {
		this.lastSyncTime = lastSyncTime;
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