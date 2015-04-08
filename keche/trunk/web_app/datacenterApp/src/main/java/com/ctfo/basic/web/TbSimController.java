package com.ctfo.basic.web;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.beans.TbSim;
import com.ctfo.basic.service.TbSimService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.web.BaseController;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： SIM卡管理<br>
 * 描述： SIM卡管理<br>
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
 * <td>2014-6-12</td>
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
@RequestMapping("/tbSim")
public class TbSimController extends BaseController {

	@Autowired
	private TbSimService tbSimService;

	/**
	 * Sim卡分页数据
	 * 
	 * @param request
	 * @return
	 * @throws CtfoAppException
	 */
	@RequestMapping(value = "/findSimByParamPage.do")
	@ResponseBody
	public Map<String, Object> findSimByParamPage(@RequestBody DynamicSqlParameter requestParam) throws CtfoAppException {
		Map<String, Object> result = new HashMap<String, Object>(2);
		PaginationResult<TbSim> list = tbSimService.findSimByParamPage(requestParam);
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;
	}

}
