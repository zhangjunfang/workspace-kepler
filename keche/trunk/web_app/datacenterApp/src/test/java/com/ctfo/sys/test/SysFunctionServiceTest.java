package com.ctfo.sys.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.common.local.obj.FunctionTree;
import com.ctfo.common.test.BaseTest;
import com.ctfo.sys.service.SysFunctionService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 权限测试用例<br>
 * 描述： 权限测试用例<br>
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
 * <td>2014-5-21</td>
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
public class SysFunctionServiceTest extends BaseTest {

	SysFunctionService sysFunctionService = (SysFunctionService) BaseTest.getClassPath().getBean("sysFunctionService");

	/**
	 * 查看角色已分配的权限树
	 */
	public void testFindByRoleId() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("roleId", "3254154646033512557");
		map.put("centerCode", "100001");
		List<FunctionTree> treeList = sysFunctionService.findByRoleId(map);
		for (FunctionTree functionTree : treeList) {
			System.out.println("-------------------------------");
			System.out.print(functionTree.getText());
			System.out.print("\t");
			System.out.print(functionTree.getIsexpand());
			System.out.print("\t");
			System.out.print(functionTree.getIschecked());
			System.out.print("\t");
			System.out.print(functionTree.getNodeId());
			System.out.print("\t");
			System.out.print(functionTree.getParentId());
			System.out.println();
			this.printTree(functionTree.getChildrenList(), "\t");
		}
	}

	/**
	 * 初始化权限树
	 */
	public void testInitFunTree() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("treeType", "2");
		List<FunctionTree> treeList = sysFunctionService.initFunTree(map);
		for (FunctionTree functionTree : treeList) {
			System.out.println("-------------------------------");
			System.out.print(functionTree.getText());
			System.out.print("\t");
			System.out.print(functionTree.getIsexpand());
			System.out.print("\t");
			System.out.print(functionTree.getIschecked());
			System.out.print("\t");
			System.out.print(functionTree.getNodeId());
			System.out.print("\t");
			System.out.print(functionTree.getParentId());
			System.out.println();
			this.printTree(functionTree.getChildrenList(), "\t");
		}
	}

	/**
	 * 角色编辑时，初始化权限树同时选中已关联的
	 */
	public void testFindFunTreeRoleEdit() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("roleId", "3254154646033512557");
		map.put("centerCode", "100001");
		List<FunctionTree> treeList = sysFunctionService.findFunTreeRoleEdit(map);
		for (FunctionTree functionTree : treeList) {
			System.out.println("-------------------------------");
			System.out.print(functionTree.getText());
			System.out.print("\t");
			System.out.print(functionTree.getIsexpand());
			System.out.print("\t");
			System.out.print(functionTree.getIschecked());
			System.out.print("\t");
			System.out.print(functionTree.getNodeId());
			System.out.print("\t");
			System.out.print(functionTree.getParentId());
			System.out.println();
			this.printTree(functionTree.getChildrenList(), "\t");
		}
	}

	/**
	 * 打印测试结果
	 * 
	 * @param rows
	 *            结果集
	 * @param type
	 *            制表符
	 */
	private void printTree(List<FunctionTree> rows, String type) {
		if (null != rows) {
			for (FunctionTree functionTree : rows) {
				System.out.println("-------------------------------");
				System.out.print(functionTree.getText());
				System.out.print("\t");
				System.out.print(functionTree.getIsexpand());
				System.out.print("\t");
				System.out.print(functionTree.getIschecked());
				System.out.print("\t");
				System.out.print(functionTree.getNodeId());
				System.out.print("\t");
				System.out.print(functionTree.getParentId());
				System.out.println();
				this.printTree(functionTree.getChildrenList(), type + "\t");
			}
		}
	}
}
