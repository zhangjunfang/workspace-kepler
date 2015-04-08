package com.ctfo.sys.test;

import java.util.HashMap;
import java.util.Map;

import com.ctfo.common.test.BaseTest;
import com.ctfo.sys.service.StructureOrgTreeService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织树单元测试<br>
 * 描述： 组织树单元测试<br>
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
 * <td>2014-6-6</td>
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
public class StructureOrgTreeServiceTest extends BaseTest {

	StructureOrgTreeService structureOrgTreeService = (StructureOrgTreeService) BaseTest.getClassPath().getBean("structureOrgTreeService");

	/**
	 * 组织树查询-异步树
	 */
	public void testFindAsynchronousOrgTree() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("entId", "200");
		map.put("centerCode", "11");
		map.put("treeType", "0");
		String json = structureOrgTreeService.findAsynchronousOrgTree(map);
		System.out.println(json);
	}

	/**
	 * 组织树查询-同步树
	 */
	public void testFindSynchronizedOrgTree() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("entId", "-1");
		// map.put("centerCode", "100001");
		map.put("treeType", "1");
		String json = structureOrgTreeService.findSynchronizedOrgTree(map);
		System.out.println(json);
	}

	/**
	 * 分中心按省市查询组织树(支持模糊)-同步树
	 */
	public void testFindSynchronizedOrgTreeByProvince() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("entId", "#1#");
		map.put("centerCode", "11");
		map.put("corpProvince", "130000");
		// map.put("entName", "总部");
		String json = structureOrgTreeService.findSynchronizedOrgTreeByProvince(map);
		System.out.println(json);
	}

}
