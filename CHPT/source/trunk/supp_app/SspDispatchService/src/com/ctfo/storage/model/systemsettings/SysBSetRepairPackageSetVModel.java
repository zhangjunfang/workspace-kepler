package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 业务设置-维修套餐设置-车型<br>
 * 描述： 业务设置-维修套餐设置-车型<br>
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
public class SysBSetRepairPackageSetVModel implements Serializable {

	/** */
	private static final long serialVersionUID = -8925715061914445392L;

	/** id */
	private String repairPackageSetVModelId;

	/** 车型ID */
	private String vModelId;

	/** 车型名称 */
	private String vModelName;

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

	public String getRepairPackageSetVModelId() {
		return repairPackageSetVModelId;
	}

	public void setRepairPackageSetVModelId(String repairPackageSetVModelId) {
		this.repairPackageSetVModelId = repairPackageSetVModelId;
	}

	public String getVModelId() {
		return vModelId;
	}

	public void setVModelId(String vModelId) {
		this.vModelId = vModelId;
	}

	public String getVModelName() {
		return vModelName;
	}

	public void setVModelName(String vModelName) {
		this.vModelName = vModelName;
	}
}