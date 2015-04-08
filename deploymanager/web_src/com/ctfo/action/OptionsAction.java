package com.ctfo.action;

import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.ctfo.utils.JsonUtil;
import com.ctfo.dao.SelectOptionsManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.service.DynamicSqlParameter;

@Controller
@SuppressWarnings({ "rawtypes" })
public class OptionsAction {

	@Autowired
	private SelectOptionsManageDAO selectOptionsManageDAO;

	/**
	 * 获取select option内容
	 * 
	 * @param request
	 *            请求
	 * @param session
	 *            域
	 * @param response
	 *            响应
	 * @throws Exception
	 *             异常
	 */
	@RequestMapping(value="getSysOptions.do",method=RequestMethod.POST)
	public void getRoleOptions(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("creater", (String) session.getAttribute("opId"));

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			Map<String,Object> map = new HashMap<String,Object>();
			map = selectOptionsManageDAO.getSysOptions(requestParam);
			String res = JsonUtil.map2json(map);
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
}
