package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.basic.beans.TbOrganization;
import com.ctfo.basic.service.TbOrganizationService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织管理单元测试<br>
 * 描述： 组织管理单元测试<br>
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
 * <td>2014-5-20</td>
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
public class TbOrganizationServiceTest extends BaseTest {

	TbOrganizationService tbOrganizationService = (TbOrganizationService) BaseTest.getClassPath().getBean("tbOrganizationService");

	/**
	 * 根据组织id获取该组织下所有id
	 */
	public void testFindEntIds() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "101");
		String result = tbOrganizationService.findEntIds(param);
		System.out.println(result);
	}

	/**
	 * 查询组织列表
	 */
	public void testFindOrgByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "200");
		equal.put("centerCode", "11");
		// equal.put("parentId", "200");
		PaginationResult<TbOrganization> result = tbOrganizationService.findOrgByParamPage(param);
		List<TbOrganization> list = (List<TbOrganization>) result.getData();
		for (TbOrganization tbOrganization : list) {
			System.out.print(tbOrganization.getEntId());
			System.out.print("\t");
			System.out.print(tbOrganization.getParentId());
			System.out.print("\t");
			System.out.print(tbOrganization.getEntName());
			System.out.println();
		}
		System.out.println(result.getTotalCount());
	}

	/**
	 * 添加组织
	 */
	public void testAddOrganization() {
		TbOrganization org = new TbOrganization();
		org.setEntName("北京分中心测试企业");
		org.setEntType(1);
		org.setParentId("200");
		org.setCreateBy("1");
		org.setUpdateBy("1");
		org.setEnableFlag("1");
		org.setEntState("1"); // 实体状态：1为正常，0为吊销
		org.setMemo("分中心测试");
		org.setIscompany(0);
		org.setCenterCode("100001");
		// tb_org_info信息
		org.setCorpCode("601260");
		org.setCorpQuale("1"); // 企业性质
		org.setCorpLevel("1");
		org.setOrgShortname("分中心企业");
		org.setOrgAddress("海淀区");
		org.setUrl("www.kypt.cn");
		org.setOrgCmail("123456");
		org.setOrgCzip("100039");
		org.setCorpProvince("110000");
		org.setCorpCity("110101");
		org.setOrgCname("this is name");
		org.setOrgCphone("15110081234");
		org.setOrgCfax("66701231");
		// PaginationResult<TbOrganization> result = tbOrganizationService.addOrganization(org);
		// System.out.println(result);
	}

	/**
	 * 修改组织
	 */
	public void testModifyOrganization() {
		TbOrganization org = new TbOrganization();
		org.setEntId("5902493396268180428");
		org.setEntName("北京分中心测试企业");
		org.setCenterCode("100001");
		// tb_org_info信息
		org.setCorpQuale("1"); // 企业性质
		org.setCorpLevel("1");
		org.setOrgShortname("分中心企业1");
		org.setOrgAddress("海淀区1");
		org.setUrl("www.kypt.cn1");
		org.setOrgCmail("1234561");
		org.setOrgCzip("100139");
		org.setCorpProvince("110000");
		org.setCorpCity("110108");
		org.setOrgCname("this is name1");
		org.setOrgCphone("151100812341");
		org.setOrgCfax("667012311");
		PaginationResult<TbOrganization> result = tbOrganizationService.modifyOrganization(org);
		System.out.println(result);
	}
}
