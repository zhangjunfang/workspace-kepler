package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.Map;

import com.ctfo.basic.beans.TbOrganizationMultiple;
import com.ctfo.basic.service.TbOrganizationMultipleService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 主中心组织管理<br>
 * 描述： 主中心组织管理<br>
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
 * <td>2014-6-26</td>
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
public class TbOrganizationMultipleServiceTest extends BaseTest {

	TbOrganizationMultipleService tbOrganizationMultipleService = (TbOrganizationMultipleService) BaseTest.getClassPath().getBean("tbOrganizationMultipleService");

	/**
	 * 查询组织列表
	 */
	public void testFindOrgMultByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "101");
		equal.put("centerCode", "110001");
		PaginationResult<TbOrganizationMultiple> result = tbOrganizationMultipleService.findOrgMultByParamPage(param);
		System.out.println(result.getTotalCount());
	}

}
