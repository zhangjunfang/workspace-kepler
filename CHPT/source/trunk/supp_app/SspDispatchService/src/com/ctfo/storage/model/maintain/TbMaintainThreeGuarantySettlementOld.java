package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 旧件结算单<br>
 * 描述： 旧件结算单<br>
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
 * <td>2014-10-31</td>
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
public class TbMaintainThreeGuarantySettlementOld implements Serializable {

	/** */
	private static final long serialVersionUID = 7835007858210211857L;

	/** 信息id */
	private String oldId;

	/** 回收单号 */
	private String recycleNo;

	/** 服务站编码 */
	private String serviceStationCode;

	/** 服务站名称 */
	private String serviceStationName;

	/** 旧件总费用 */
	private BigDecimal sumCost;

	/** 关联id（三包结算单信息id） */
	private String stId;

	/** 信息状态（1|有效；0|删除） */
	private String enableFlag;

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

	public String getOldId() {
		return oldId;
	}

	public void setOldId(String oldId) {
		this.oldId = oldId;
	}

	public String getRecycleNo() {
		return recycleNo;
	}

	public void setRecycleNo(String recycleNo) {
		this.recycleNo = recycleNo;
	}

	public String getServiceStationCode() {
		return serviceStationCode;
	}

	public void setServiceStationCode(String serviceStationCode) {
		this.serviceStationCode = serviceStationCode;
	}

	public String getServiceStationName() {
		return serviceStationName;
	}

	public void setServiceStationName(String serviceStationName) {
		this.serviceStationName = serviceStationName;
	}

	public BigDecimal getSumCost() {
		return sumCost;
	}

	public void setSumCost(BigDecimal sumCost) {
		this.sumCost = sumCost;
	}

	public String getStId() {
		return stId;
	}

	public void setStId(String stId) {
		this.stId = stId;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
}