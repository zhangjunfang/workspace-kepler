package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import junit.framework.TestCase;

import com.ctfo.basic.beans.TbTerminal;
import com.ctfo.basic.beans.TbTerminalOem;
import com.ctfo.basic.beans.TbTerminalProtocol;
import com.ctfo.basic.service.TbTerminalService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：终端单元测试<br>
 * 描述：终端单元测试<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月28日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbTerminalServiceTest extends TestCase {

	TbTerminalService tbTerminalService = (TbTerminalService) BaseTest.getClassPath().getBean("tbTerminalService");

	/**
	 * 查询终端列表
	 */
	public void testFindTerminalByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "200");
		PaginationResult<TbTerminal> result = tbTerminalService.findTerminalByParamPage(param);
		List<TbTerminal> list = (List<TbTerminal>) result.getData();
		for (TbTerminal tbTerminal : list) {
			System.out.println(tbTerminal.getEntName());
		}
		PaginationResult<TbTerminalOem> result2 = tbTerminalService.findOemNames(param);
		List<TbTerminalOem> list2 = (List<TbTerminalOem>) result2.getData();
		for (TbTerminalOem tbTerminalOem : list2) {
			System.out.println(tbTerminalOem.getFullName());
		}		
		System.out.println(result.getTotalCount());
	}
	
	public void testFindProtocolNames(){
		DynamicSqlParameter param = new DynamicSqlParameter();
		PaginationResult<TbTerminalProtocol> result3 = tbTerminalService.findProtocolNames(param);
		List<TbTerminalProtocol> list3 = (List<TbTerminalProtocol>) result3.getData();
		for (TbTerminalProtocol tbTerminalProtocol : list3) {
			System.out.println(tbTerminalProtocol.getTprotocolName());
		}		
		System.out.println(result3.getTotalCount());
	}
}
