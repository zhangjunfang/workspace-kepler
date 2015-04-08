package com.ctfo.baseinfo.controller;

import java.io.IOException;
import java.io.OutputStream;

import javax.servlet.http.HttpServletResponse;

import net.sf.json.JSONObject;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.baseinfo.service.SysGeneralCodeService;
import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.obj.DynamicSqlParameter;

@Controller
@RequestMapping("/baseinfo")
public class SysGeneralCodeController extends BaseController{
	
	@Autowired
	SysGeneralCodeService sysGeneralCodeService;
	
	private String jsonResult;
	// 动态参数对象
	protected DynamicSqlParameter requestParam = new DynamicSqlParameter();
	protected final static String ContentENCOD = "UTF-8";
	protected final static String ContentTypeHTML = "text/html";
	
	/**
	 * 登陆成功后初始化通用编码到客户端
	 */
	@RequestMapping(value="/findInitSysGeneralCode.do")
	@ResponseBody
	public void findInitSysGeneralCode(HttpServletResponse response) {
		if (jsonResult == null || ("").equals(jsonResult) || ("null").equals(jsonResult)) {// 从数据库取编码
			jsonResult = sysGeneralCodeService.findSysGeneralCodeByCode(this.requestParam);
		}
		JSONObject json = JSONObject.fromObject(jsonResult);
		String resultStr = null;
		OutputStream out = null;
		try {
			if (null != json) {
				resultStr = String.valueOf(json);
			} else {
				resultStr = "";
			}
			response.setCharacterEncoding(ContentENCOD);
			response.setContentType(ContentTypeHTML);
			out = response.getOutputStream();
			out.write(resultStr.getBytes(ContentENCOD));
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			if (out != null) {
				try {
					out.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}
}
