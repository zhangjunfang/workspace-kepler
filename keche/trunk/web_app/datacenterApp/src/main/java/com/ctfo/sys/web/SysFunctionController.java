package com.ctfo.sys.web;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.common.local.obj.FunctionTree;
import com.ctfo.common.local.web.BaseController;
import com.ctfo.sys.service.SysFunctionService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 权限<br>
 * 描述： 权限<br>
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
 * <td>2014-5-28</td>
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
@Controller
@RequestMapping("/sysFunction")
public class SysFunctionController extends BaseController {

	@Autowired
	private SysFunctionService sysFunctionService;

	/**
	 * 初始化权限树
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/initFunTree.do")
	@ResponseBody
	public List<FunctionTree> initFunTree(HttpServletRequest request) {
		Map<String, String> map = new HashMap<String, String>();
		map.put("treeType", request.getParameter("treeType"));
		List<FunctionTree> treeList = sysFunctionService.initFunTree(map);
		return treeList;
	}

	/**
	 * 查看角色已分配的权限树
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findByRoleId.do")
	@ResponseBody
	public List<FunctionTree> findByRoleId(HttpServletRequest request) {
		Map<String, String> map = new HashMap<String, String>();
		map.put("roleId", request.getParameter("roleId"));
		map.put("centerCode", request.getParameter("centerCode"));
		List<FunctionTree> treeList = sysFunctionService.findByRoleId(map);
		return treeList;
	}

	/**
	 * 角色编辑时，初始化权限树同时选中已关联的
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findFunTreeRoleEdit.do")
	@ResponseBody
	public List<FunctionTree> findFunTreeRoleEdit(HttpServletRequest request) {
		Map<String, String> map = new HashMap<String, String>();
		map.put("roleId", request.getParameter("roleId"));
		map.put("centerCode", request.getParameter("centerCode"));
		map.put("treeType", request.getParameter("treeType"));
		List<FunctionTree> treeList = sysFunctionService.findFunTreeRoleEdit(map);
		return treeList;
	}

	/**
	 * 查询用户登录后的权限集合
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findFunListByOpId.do")
	@ResponseBody
	public List<String> findFunListByOpId(HttpServletRequest request) {
		Map<String, String> map = new HashMap<String, String>();
		map.put("opId", this.getSessionOperatorId(request));
		List<String> list = sysFunctionService.findFunListByOpId(map);
		return list;
	}
}
