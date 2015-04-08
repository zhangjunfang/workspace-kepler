package com.ctfo.basic.web;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.beans.TbDvr;
import com.ctfo.basic.service.TbDvrService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.web.BaseController;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 3G视频终端<br>
 * 描述： 3G视频终端<br>
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
 * <td>2014-5-26</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
@Controller
@RequestMapping("/tbDvr")
public class TbDvrController extends BaseController {

	@Autowired
	private TbDvrService tbDvrService;

	/**
	 * 3G视频终端 分页数据
	 * 
	 * @param request
	 * @return
	 */
	@RequestMapping(value = "findDvrByParamPage.do")
	@ResponseBody
	public Map<String, Object> findDvrByParamPage(@RequestBody DynamicSqlParameter requestParam) {
		Map<String, Object> result = new HashMap<String, Object>(2);
		PaginationResult<TbDvr> list = tbDvrService.findDvrByParamPage(requestParam);
		result.put("Rows", list.getData());
		result.put("Total", list.getTotalCount());
		return result;

	}

}
