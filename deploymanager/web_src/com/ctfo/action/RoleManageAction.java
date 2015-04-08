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
import com.ctfo.exception.CtfoAppException;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.RoleManageService;

@Controller
@SuppressWarnings({ "rawtypes" })
public class RoleManageAction {

	@Autowired
	private RoleManageService roleManageService;
	
	@RequestMapping(value="getRoleList.do",method=RequestMethod.POST)
	public void getRoleList(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		int page = new Integer(request.getParameter("page").trim());
		int rows = new Integer(request.getParameter("rows").trim());
		
		Map<String, String> equal = new HashMap<String, String>();
		
		requestParam.setEqual(equal);
		requestParam.setPage((page-1)*rows);
		requestParam.setRows(rows);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = roleManageService.getRoleList(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="getEditRole.do",method=RequestMethod.POST)
	public void getEditRole(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		requestParam.setEqual(equal);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = roleManageService.getEditRole(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="addRole.do",method=RequestMethod.POST)
	public void addRole(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String rolename = request.getParameter("rolename").trim();
		String roledesc = request.getParameter("roledesc").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("rolename", rolename);
		equal.put("roledesc", roledesc);
		equal.put("creater", (String) session.getAttribute("opId"));

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = roleManageService.addRole(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="editRole.do",method=RequestMethod.POST)
	public void editRole(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String rolename = request.getParameter("rolename").trim();
		String roledesc = request.getParameter("roledesc").trim();
		String editId = request.getParameter("editId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("rolename", rolename);
		equal.put("roledesc", roledesc);
		equal.put("editId", editId);
		equal.put("updater", (String) session.getAttribute("opId"));

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = roleManageService.editRole(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}

	@RequestMapping(value="delRole.do",method=RequestMethod.POST)
	public void delRole(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String roleid = request.getParameter("delId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("roleid", roleid);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = roleManageService.delRole(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="checkRoleExist.do",method=RequestMethod.POST)
	public void checkRoleExist(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String rolename = request.getParameter("rolename").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("rolename", rolename);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = roleManageService.checkRoleExist(requestParam);
			if(result > 0){
				out.print("used");
			}
		} catch (CtfoAppException e) {
			
		}
	}
}
