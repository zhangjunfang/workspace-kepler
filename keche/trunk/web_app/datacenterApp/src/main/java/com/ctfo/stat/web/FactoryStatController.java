package com.ctfo.stat.web;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.web.BaseController;
import com.ctfo.stat.service.TlRepFacActDetailMonthService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-6-18</td>
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
@Controller
@RequestMapping("/factoryStat")
public class FactoryStatController extends BaseController {

	@Autowired
	private TlRepFacActDetailMonthService tlRepFacActDetailMonthService;

	/**
	 * 月统计活跃度指标汇总：平台台接入量，企业活跃度，车辆活跃度，活跃度，续约率
	 * 
	 * @return
	 */
	@RequestMapping(value = "/querySumActivityDegreeMonth.do")
	@ResponseBody
	public List<Map<String, Object>> querySumActivityDegreeMonth() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		return tlRepFacActDetailMonthService.querySumActivityDegreeMonth(param);
	}

}
