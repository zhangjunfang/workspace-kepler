package com.ctfo.sys.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.service.TbSpOperatorService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 用户管理<br>
 * 描述： 用户管理<br>
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
 * <td>2014-5-15</td>
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
public class TbSpOperatorServiceTest extends BaseTest {

	TbSpOperatorService tbSpOperatorService = (TbSpOperatorService) BaseTest.getClassPath().getBean("tbSpOperatorService");

	/**
	 * 添加用户
	 */
	public void testAddOperator() {
		TbSpOperator tbSpOperator = new TbSpOperator();
		tbSpOperator.setOpLoginname("xhtest");
		tbSpOperator.setEntId("200");
		tbSpOperator.setOpPass("49129d20c19e5e9ac00d48e2b7e2b25cccf2e729");
		tbSpOperator.setOpName("薛晖测试");
		tbSpOperator.setOpSuper("0");
		tbSpOperator.setOpProvince("430000");
		tbSpOperator.setOpCity("430100");
		tbSpOperator.setOpType("0");
		tbSpOperator.setCreateBy("1");
		tbSpOperator.setOpStatus("1");
		tbSpOperator.setEnableFlag("1");
		tbSpOperator.setIsCenter("1");
		tbSpOperator.setRoleId("501"); // 角色id
		tbSpOperator.setCenterCode("100001");
		// tbSpOperatorService.addOperator(tbSpOperator);
	}

	/**
	 * 删除用户（逻辑删除）
	 */
	public void testDeleteOperator() {
		TbSpOperator tbSpOperator = new TbSpOperator();
		tbSpOperator.setOpId("6342013320938366761");
		tbSpOperator.setCenterCode("100001");
		tbSpOperator.setUpdateBy("1");
		tbSpOperatorService.deleteOperator(tbSpOperator);
	}

	/**
	 * 查询用户信息详情
	 */
	public void testFindOperatorDetail() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("opId", "283");
		map.put("centerCode", "100001");
		TbSpOperator tbSpOperator = tbSpOperatorService.findOperatorDetail(map);
		System.out.println(tbSpOperator.getOpLoginname());
		System.out.println(tbSpOperator.getRoleName());
	}

	/**
	 * 查询用户列表
	 */
	public void testFindOperatorByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("opStatus", "1");
		equal.put("entId", "200");
		equal.put("centerCode", "100001");
		PaginationResult<TbSpOperator> result = tbSpOperatorService.findOperatorByParamPage(param);
		List<TbSpOperator> list = (List<TbSpOperator>) result.getData();
		for (TbSpOperator tbSpOperator : list) {
			System.out.println(tbSpOperator.getOpLoginname());
			System.out.println(tbSpOperator.getOpId());
		}
	}

	/**
	 * 查询用户列表
	 */
	public void testFindCenterOperatorByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		// equal.put("opStatus", "1");
		equal.put("entId", "101");
		equal.put("centerCode", "110001");
		PaginationResult<TbSpOperator> result = tbSpOperatorService.findCenterOperatorByParamPage(param);
		List<TbSpOperator> list = (List<TbSpOperator>) result.getData();
		for (TbSpOperator tbSpOperator : list) {
			System.out.println(tbSpOperator.getOpLoginname());
			System.out.println(tbSpOperator.getOpId());
		}
	}
}
