package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 部件和故障模式关系<br>
 * 描述： 部件和故障模式关系<br>
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
public class TrComponentModel implements Serializable {

	/** */
	private static final long serialVersionUID = 7543873746787016642L;

	/** id */
	private String componentModelId;

	/** 部件代码 */
	private String rulePartCode;

	/** 故障模式代码 */
	private String ruleFmeaCode;

	/** 限定条件(1为允许,0为不允许) */
	private String limitCondition;

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

	public String getComponentModelId() {
		return componentModelId;
	}

	public void setComponentModelId(String componentModelId) {
		this.componentModelId = componentModelId;
	}

	public String getRulePartCode() {
		return rulePartCode;
	}

	public void setRulePartCode(String rulePartCode) {
		this.rulePartCode = rulePartCode;
	}

	public String getRuleFmeaCode() {
		return ruleFmeaCode;
	}

	public void setRuleFmeaCode(String ruleFmeaCode) {
		this.ruleFmeaCode = ruleFmeaCode;
	}

	public String getLimitCondition() {
		return limitCondition;
	}

	public void setLimitCondition(String limitCondition) {
		this.limitCondition = limitCondition;
	}
}