package com.ctfo.sys.web;

import javax.servlet.http.HttpServletResponse;

import net.sf.json.JSONObject;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import com.ctfo.common.local.web.BaseController;
import com.ctfo.sys.service.SysGeneralCodeService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-6-3</td>
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
@RequestMapping("/sysGeneralCode")
public class SysGeneralCodeController extends BaseController {

	@Autowired
	private SysGeneralCodeService sysGeneralCodeService;

	/**
	 * 字典数据信息查询
	 * 
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/findInitSysGeneralCode.do")
	public String findInitSysGeneralCode(HttpServletResponse response) {
		String json = null;
		try {
			json = readJedisDao.getStaticGeneralCode(); // 从缓存中获取通用编码
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (null == json || ("{}").equals(json) || ("null").equals(json)) { // 从数据库取编码
			json = sysGeneralCodeService.findSysGeneralCodeByCode();
		}
		JSONObject jsonResult = JSONObject.fromObject(json);
		return this.writeHTML(response, jsonResult.toString());
	}
}
