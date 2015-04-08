package com.ctfo.sys.test;

import com.ctfo.common.test.BaseTest;
import com.ctfo.sys.service.SysGeneralCodeService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 字典码表单元测试<br>
 * 描述： 字典码表单元测试<br>
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
 * <td>2014-6-3</td>
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
public class SysGeneralCodeServiceTest extends BaseTest {

	SysGeneralCodeService sysGeneralCodeService = (SysGeneralCodeService) BaseTest.getClassPath().getBean("sysGeneralCodeService");

	/**
	 * 字典数据信息查询
	 */
	public void testFindSysGeneralCodeByCode() {
		String result = sysGeneralCodeService.findSysGeneralCodeByCode();
		System.out.println(result);
	}

}
