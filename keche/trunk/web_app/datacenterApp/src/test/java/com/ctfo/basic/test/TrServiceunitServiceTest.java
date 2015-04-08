package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.Map;

import com.ctfo.basic.beans.TrServiceunit;
import com.ctfo.basic.service.TrServiceunitService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 车辆注册信息测试<br>
 * 描述： 车辆注册信息测试<br>
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
 * <td>2014-6-13</td>
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
public class TrServiceunitServiceTest extends BaseTest {

	TrServiceunitService trServiceunitService = (TrServiceunitService) BaseTest.getClassPath().getBean("trServiceunitService");

	/**
	 * 查询车辆注册信息列表
	 */
	public void testFindServiceunitByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "200");
		equal.put("centerCode", "100001");
		PaginationResult<TrServiceunit> result = trServiceunitService.findServiceunitByParamPage(param);
		System.out.println(result.getTotalCount());
	}

}
