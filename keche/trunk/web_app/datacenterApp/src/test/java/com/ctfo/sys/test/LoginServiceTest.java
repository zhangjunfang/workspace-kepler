package com.ctfo.sys.test;

import java.util.HashMap;
import java.util.Map;

import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.service.LoginService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 登录测试<br>
 * 描述： 登录测试<br>
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
 * <td>2014-5-23</td>
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
public class LoginServiceTest extends BaseTest {

	LoginService loginService = (LoginService) BaseTest.getClassPath().getBean("loginService");

	/**
	 * 登录
	 */
	public void testFindOperatorLogin() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("opLoginname", "cadmin");
		map.put("opPass", "49129d20c19e5e9ac00d48e2b7e2b25cccf2e729");
		map.put("parentOrgCode", "600001");
		PaginationResult<TbSpOperator> result = loginService.findOperatorLogin(map);
		System.out.println(result.getResultJudge());
	}
}
