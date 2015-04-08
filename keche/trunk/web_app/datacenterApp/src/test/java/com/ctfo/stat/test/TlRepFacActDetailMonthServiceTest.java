package com.ctfo.stat.test;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.test.BaseTest;
import com.ctfo.stat.service.TlRepFacActDetailMonthService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 考核指标按月统计<br>
 * 描述： 考核指标按月统计<br>
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
public class TlRepFacActDetailMonthServiceTest extends BaseTest {

	TlRepFacActDetailMonthService tlRepFacActDetailMonthService = (TlRepFacActDetailMonthService) BaseTest.getClassPath().getBean("tlRepFacActDetailMonthService");

	/**
	 * 月统计活跃度指标汇总：平台台接入量，企业活跃度，车辆活跃度，活跃度，续约率
	 */
	public void testQuerySumActivityDegreeMonth() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		List<Map<String, Object>> list = tlRepFacActDetailMonthService.querySumActivityDegreeMonth(param);
		for (Map<String, Object> map : list) {
			System.out.print(map.get("ent_degree"));
			System.out.print("\t");
			System.out.print(map.get("vh_degree"));
			System.out.print("\t");
			System.out.print(map.get("renewal_rate"));
			System.out.print("\t");
			System.out.print(map.get("activity_degree"));
			System.out.print("\t");
			System.out.print(map.get("net_vh_num"));
			System.out.println();
		}
	}

}
