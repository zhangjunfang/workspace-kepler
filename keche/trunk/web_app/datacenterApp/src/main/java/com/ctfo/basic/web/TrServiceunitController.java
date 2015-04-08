package com.ctfo.basic.web;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.beans.TrServiceunit;
import com.ctfo.basic.service.TrServiceunitService;
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
 * 功能： 车卡终端绑定<br>
 * 描述： 车卡终端绑定<br>
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
 * <td>2014-6-16</td>
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
@RequestMapping("/trServiceunit")
public class TrServiceunitController extends BaseController {

	@Autowired
	private TrServiceunitService trServiceunitService;

	/**
	 * 车辆分页数据
	 * 
	 * @param request
	 * @return
	 * @throws CtfoAppException
	 */
	@RequestMapping(value = "/findServiceunitByParamPage.do")
	@ResponseBody
	public Map<String, Object> findServiceunitByParamPage(@RequestBody DynamicSqlParameter requestParam) throws CtfoAppException {
		Map<String, Object> result = new HashMap<String, Object>(2);
		PaginationResult<TrServiceunit> list = trServiceunitService.findServiceunitByParamPage(requestParam);
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;
	}

}
