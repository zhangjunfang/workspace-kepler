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

import com.ctfo.basic.beans.TbBranchCenter;
import com.ctfo.basic.service.TbBranchCenterService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.web.BaseController;
import com.ctfo.common.util.StaticSession;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：分中心<br>
 * 描述：分中心<br>
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
 * <td>2014年6月9日</td>
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
@Controller
@RequestMapping("/tbBranchCenter")
public class TbBranchCenterController extends BaseController {

	@Autowired
	private TbBranchCenterService tbBranchCenterService;

	/**
	 * 分中心分页数据
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/findBranchCenterByParamPage.do")
	@ResponseBody
	public Map<String, Object> findBranchCenterByParamPage(@RequestBody DynamicSqlParameter requestParam) {
		Map<String, Object> result = new HashMap<String, Object>(2);
		PaginationResult<TbBranchCenter> list = tbBranchCenterService.findBranchCenterByParamPage(requestParam);
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;
	}

	/**
	 * 添加分中心
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/addBranchCenter.do")
	@ResponseBody
	public String addRole(@RequestBody TbBranchCenter tbBranchCenter, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbBranchCenter> result = tbBranchCenterService.addBranchCenter(tbBranchCenter);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 删除分中心
	 * 
	 * @param tbSpRole
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/deleteBranchCenter.do")
	public String deleteBranchCenter(@RequestBody TbBranchCenter tbBranchCenter, HttpServletRequest request, HttpServletResponse response) {
		try {
			PaginationResult<TbBranchCenter> result = tbBranchCenterService.deleteBranchCenter(tbBranchCenter);
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}
}
