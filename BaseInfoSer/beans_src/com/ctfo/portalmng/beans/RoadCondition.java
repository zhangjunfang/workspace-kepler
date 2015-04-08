package com.ctfo.portalmng.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 路况
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>2011-10-20</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@SuppressWarnings("serial")
public class RoadCondition extends ETB_Base {

	/**
	 * 省、直辖市编码
	 */
	private Long provinceCode;

	/**
	 * 时间
	 */
	private String roadConditionTime;

	/**
	 * 事件描述
	 */
	private String descriptions;

	public Long getProvinceCode() {
		return provinceCode;
	}

	public void setProvinceCode(Long provinceCode) {
		this.provinceCode = provinceCode;
	}

	public String getRoadConditionTime() {
		return roadConditionTime;
	}

	public void setRoadConditionTime(String roadConditionTime) {
		this.roadConditionTime = roadConditionTime;
	}

	public String getDescriptions() {
		return descriptions;
	}

	public void setDescriptions(String descriptions) {
		this.descriptions = descriptions;
	}
}
