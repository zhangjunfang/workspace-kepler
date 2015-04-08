package com.ctfo.action;

import java.io.PrintWriter;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.ctfo.utils.JsonUtil;
import com.ctfo.utils.PwdDigest;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.OperatorManageService;

@Controller
@SuppressWarnings({ "rawtypes" })
public class OperatorAction {

	@Autowired
	private OperatorManageService operatorManageService;

	/**
	 * 增加用户
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
	@RequestMapping(value="addOperator.do",method=RequestMethod.POST)
	public void addOperator(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		// 用户名
		String username = request.getParameter("username").trim();
		// 密码
		String password = request.getParameter("password").trim();
		password = PwdDigest.passwordDigest(password);
		//角色ID
		String roleid = request.getParameter("roleid").trim();
		//姓名
		String realname = request.getParameter("realname").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("uname", username);
		equal.put("upass", password);
		equal.put("roleid", roleid);
		equal.put("realname", realname);
		equal.put("creater", (String) session.getAttribute("opId"));

		requestParam.setEqual(equal);
		PrintWriter out = null;
		String res = null;
		try {
			out = response.getWriter();
			result = operatorManageService.addOperator(requestParam);
			if(result == 1){
				res = "done";
			}
		} catch (CtfoAppException e) {
			
		}
		out.print(JsonUtil.string2json(res));
	}
	
	@RequestMapping(value="getUserList.do",method=RequestMethod.POST)
	public void getUserList(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		int page = new Integer(request.getParameter("page").trim());
		int rows = new Integer(request.getParameter("rows").trim());
		
		Map<String, String> equal = new HashMap<String, String>();
		
		if(!session.getAttribute("opName").toString().equals("admin")){
			equal.put("opId", session.getAttribute("opId").toString());
			//equal.put("creater", session.getAttribute("opName").toString());
		}

		requestParam.setEqual(equal);
		requestParam.setPage((page-1)*rows);
		requestParam.setRows(rows);
		
		Map<String, Object> map = new HashMap<String, Object>(); 
		PrintWriter out = response.getWriter();
		String res = JsonUtil.map2json(map);
		try {
			map = operatorManageService.getUserList(requestParam);
			res = JsonUtil.map2json(map);
		} catch (CtfoAppException e) {
			
		}
		out.print(res);
	}
	
	@RequestMapping(value="getEditUser.do",method=RequestMethod.POST)
	public void getEditUser(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		
		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		requestParam.setEqual(equal);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = operatorManageService.getEditUser(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="editUser.do",method=RequestMethod.POST)
	public void editUser(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		String pass = request.getParameter("password").trim();
		pass = PwdDigest.passwordDigest(pass);
		String roleid = request.getParameter("roleid").trim();
		String realname = request.getParameter("realname").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		equal.put("opPass", pass);
		equal.put("roleId", roleid);
		equal.put("realName", realname);
		equal.put("updater", (String) session.getAttribute("opId"));
		
		requestParam.setEqual(equal);
		
		try {
			PrintWriter out = response.getWriter();
			int update = operatorManageService.editUser(requestParam);
			if(update == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	@RequestMapping(value="delUser.do",method=RequestMethod.POST)
	public void delUser(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String delId = request.getParameter("delId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("delId", delId);
		requestParam.setEqual(equal);
		
		try {
			PrintWriter out = response.getWriter();
			int update = operatorManageService.delUser(requestParam);
			if(update == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="checkUserExist.do",method=RequestMethod.POST)
	public void checkUserExist(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String username = request.getParameter("username").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("username", username);
		requestParam.setEqual(equal);
		
		try {
			PrintWriter out = response.getWriter();
			int userExist = operatorManageService.checkUserExist(requestParam);
			if(userExist > 0){
				out.print("used");
			}
		} catch (CtfoAppException e) {
			
		}
	}
}
