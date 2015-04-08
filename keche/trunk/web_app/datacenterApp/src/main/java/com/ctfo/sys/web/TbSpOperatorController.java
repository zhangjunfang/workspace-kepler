package com.ctfo.sys.web;

import java.util.HashMap;
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
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.service.TbSpOperatorService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 用户<br>
 * 描述： 用户<br>
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
 * <td>2014-5-29</td>
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
@RequestMapping("/spOperator")
public class TbSpOperatorController extends BaseController {

	@Autowired
	private TbSpOperatorService tbSpOperatorService;

	/**
	 * 从缓存中获取数据
	 * 
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/findOperatorFromMem.do")
	@ResponseBody
	public String findOperatorFromMem(HttpServletRequest request, HttpServletResponse response) {
		try {
			String objId = this.getSessionOperatorId(request); // 用户id
			if (null != objId && !"null".equals(objId) && !"".equals(objId)) {
				objId = objId + StaticSession.SYS_MARKING_PREFIX_CENTER;
			} else {
				return this.returnInfoForJS(response, StaticSession.OP_LOGIN);
			}

			String value = this.readJedisDao.getTempCacheValue(objId);
			if (null != value && !"".equals(value)) {
				return this.writeHTML(response, value);
			} else {
				return this.returnInfoForJS(response, StaticSession.OP_LOGIN);
			}
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 查询用户列表
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findOperatorByParamPage.do")
	@ResponseBody
	public Map<String, Object> findOperatorByParamPage(@RequestBody DynamicSqlParameter requestParam) {
		Map<String, Object> result = new HashMap<String, Object>(2);
		String treeType = String.valueOf(requestParam.getEqual().get("treeType"));
		PaginationResult<TbSpOperator> list = new PaginationResult<TbSpOperator>();
		if (StaticSession.ONE.equals(treeType)) { // 判断查询主中心或分中心数据
			list = tbSpOperatorService.findOperatorByParamPage(requestParam);
		} else if (StaticSession.TWO.equals(treeType)) {
			list = tbSpOperatorService.findCenterOperatorByParamPage(requestParam);
		}
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;
	}

	/**
	 * 查询用户详情
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "/findOperatorDetail.do")
	@ResponseBody
	public TbSpOperator findOperatorDetail(HttpServletRequest request) {
		Map<String, String> map = new HashMap<String, String>();
		map.put("opId", request.getParameter("opId"));
		map.put("centerCode", request.getParameter("centerCode"));
		return tbSpOperatorService.findOperatorDetail(map);
	}

	/**
	 * 添加用户
	 * 
	 * @param tbSpOperator
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/addOperator.do")
	public String addOperator(@RequestBody TbSpOperator tbSpOperator, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpOperator> result = tbSpOperatorService.addOperator(tbSpOperator);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 删除用户
	 * 
	 * @param tbSpOperator
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/deleteOperator.do")
	public String deleteRole(@RequestBody TbSpOperator tbSpOperator, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpOperator> result = tbSpOperatorService.deleteOperator(tbSpOperator);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 修改用户
	 * 
	 * @param tbSpOperator
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/modifyOperator.do")
	public String modifyRole(@RequestBody TbSpOperator tbSpOperator, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpOperator> result = tbSpOperatorService.modifyOperator(tbSpOperator);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 启用与吊销用户
	 * 
	 * @param tbSpOperator
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/revokeOpenOperator.do")
	public String revokeOpenOperator(@RequestBody TbSpOperator tbSpOperator, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpOperator> result = tbSpOperatorService.revokeOpenOperator(tbSpOperator);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 修改密码
	 * 
	 * @param tbSpOperator
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/modifyPass.do")
	public String modifyPass(@RequestBody TbSpOperator tbSpOperator, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbSpOperator> result = tbSpOperatorService.modifyPass(tbSpOperator);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 修改登录用户密码
	 * 
	 * @param tbSpOperator
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/retPassword.do")
	public String retPassword(@RequestBody TbSpOperator tbSpOperator, HttpServletResponse response) {
		try {
			PaginationResult<TbSpOperator> result = tbSpOperatorService.retPassword(tbSpOperator);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}
}
