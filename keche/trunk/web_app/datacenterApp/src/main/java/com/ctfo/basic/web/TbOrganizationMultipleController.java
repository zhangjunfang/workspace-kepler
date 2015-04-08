package com.ctfo.basic.web;

import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.beans.TbOrganizationMultiple;
import com.ctfo.basic.service.TbOrganizationMultipleService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.web.BaseController;
import com.ctfo.common.util.StaticSession;

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
@Controller
@RequestMapping("/tbOrgMult")
public class TbOrganizationMultipleController extends BaseController {

	@Autowired
	private TbOrganizationMultipleService tbOrganizationMultipleService;

	/**
	 * 查询组织列表
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findOrgMultByParamPage.do")
	@ResponseBody
	public Map<String, Object> findOrgMultByParamPage(@RequestBody DynamicSqlParameter requestParam) {
		Map<String, Object> result = new HashMap<String, Object>(2);
		PaginationResult<TbOrganizationMultiple> list = tbOrganizationMultipleService.findOrgMultByParamPage(requestParam);
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;
	}

	/**
	 * 添加组织
	 * 
	 * @param org
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/addOrganizationMult.do")
	public String addOrganizationMult(@RequestBody TbOrganizationMultiple org, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbOrganizationMultiple> result = tbOrganizationMultipleService.addOrganizationMult(org);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 修改组织
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/modifyOrganizationMult.do")
	public String modifyOrganizationMult(@RequestBody TbOrganizationMultiple org, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbOrganizationMultiple> result = tbOrganizationMultipleService.modifyOrganizationMult(org);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 删除组织
	 * 
	 * @param org
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/deleteOrganizationMult.do")
	public String deleteOrganizationMult(@RequestBody TbOrganizationMultiple org, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbOrganizationMultiple> result = tbOrganizationMultipleService.deleteOrganizationMult(org);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 吊销、启用组织
	 * 
	 * @param org
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/revokeOpenOrgMult.do")
	public String revokeOpenOrgMult(@RequestBody TbOrganizationMultiple org, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbOrganizationMultiple> result = tbOrganizationMultipleService.revokeOpenOrgMult(org);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

}
