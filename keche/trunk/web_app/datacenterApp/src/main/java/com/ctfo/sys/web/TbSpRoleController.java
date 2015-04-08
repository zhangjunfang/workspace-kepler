package com.ctfo.sys.web;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.web.BaseController;
import com.ctfo.common.util.StaticSession;
import com.ctfo.common.util.StringUtil;
import com.ctfo.sys.beans.TbSpRole;
import com.ctfo.sys.service.TbSpRoleService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 角色管理<br>
 * 描述： 角色管理<br>
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
 * <td>2014-5-27</td>
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
@RequestMapping("/spRole")
public class TbSpRoleController extends BaseController {

	@Autowired
	private TbSpRoleService tbSpRoleService;

	/**
	 * 查询角色详情
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findRoleDetail.do")
	@ResponseBody
	public TbSpRole findRoleDetail(HttpServletRequest request) {
		Map<String, String> map = new HashMap<String, String>();
		map.put("roleId", request.getParameter("roleId"));
		map.put("centerCode", request.getParameter("centerCode"));
		return tbSpRoleService.findRoleDetail(map);
	}

	/**
	 * 查询角色列表
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findRoleByParamPage.do")
	@ResponseBody
	public Map<String, Object> findRoleByParamPage(@RequestBody DynamicSqlParameter requestParam) {
		Map<String, Object> result = new HashMap<String, Object>(2);
		PaginationResult<TbSpRole> list = tbSpRoleService.findRoleByParamPage(requestParam);
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;
	}

	/**
	 * 查询角色集合
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findRoleList.do")
	@ResponseBody
	public Map<String, Object> findRoleList(HttpServletRequest request) {
		DynamicSqlParameter param = super.getPageParam(request);
		Map<String, Object> equal = new HashMap<String, Object>();
		String entId = request.getParameter("requestParam.equal.entIds");
		String centerCode = request.getParameter("requestParam.equal.centerCode");
		String treeType = request.getParameter("requestParam.equal.treeType");
		if (StringUtil.isNotBlank(entId)) {
			equal.put("entId", "#" + entId + "#");
		}
		if (StringUtil.isNotBlank(centerCode)) {
			equal.put("centerCode", centerCode);
		}
		if (StringUtil.isNotBlank(treeType)) {
			equal.put("treeType", treeType);
		}
		param.setEqual(equal);

		Map<String, Object> result = new HashMap<String, Object>(2);
		List<TbSpRole> list = tbSpRoleService.findRoleList(param);
		result.put("Rows", list);
		result.put("Total", list.size());
		return result;
	}

	/**
	 * 添加角色
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/addRole.do")
	public String addRole(@RequestBody TbSpRole tbSpRole, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpRole> result = tbSpRoleService.addRole(tbSpRole);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 删除角色
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/deleteRole.do")
	public String deleteRole(@RequestBody TbSpRole tbSpRole, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpRole> result = tbSpRoleService.deleteRole(tbSpRole);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 修改角色
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/modifyRole.do")
	public String modifyRole(@RequestBody TbSpRole tbSpRole, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpRole> result = tbSpRoleService.modifyRole(tbSpRole);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}
}
