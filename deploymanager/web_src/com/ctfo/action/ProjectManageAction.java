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
import com.ctfo.service.ProjectManageService;
import com.ctfo.service.RoleManageService;

@Controller
@SuppressWarnings({ "rawtypes" })
public class ProjectManageAction {

	@Autowired
	private ProjectManageService projectManageService;
	
	@RequestMapping(value="getProjectList.do",method=RequestMethod.POST)
	public void getProjectList(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		int page = new Integer(request.getParameter("page").trim());
		int rows = new Integer(request.getParameter("rows").trim());
		
		Map<String, String> equal = new HashMap<String, String>();
		
		requestParam.setEqual(equal);
		requestParam.setPage((page-1)*rows);
		requestParam.setRows(rows);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = projectManageService.getProjectList(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="getEditProject.do",method=RequestMethod.POST)
	public void getEditProject(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		requestParam.setEqual(equal);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = projectManageService.getEditProject(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="addProject.do",method=RequestMethod.POST)
	public void addProject(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String projectName = request.getParameter("projectname").trim();
		String projectVersion = request.getParameter("projectversion").trim();
		String branchName = request.getParameter("branchname").trim();
		String branchPath = request.getParameter("branchpath").trim();
		String dbscriptPath = request.getParameter("dbscriptpath").trim();
		String deployDesc = request.getParameter("deploydesc").trim();
		String compileDate = request.getParameter("compiledate").trim();
		
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("projectName", projectName);
		equal.put("projectVersion", projectVersion);
		equal.put("branchName", branchName);
		equal.put("branchPath", branchPath);
		equal.put("dbscriptPath", dbscriptPath);
		equal.put("deployDesc", deployDesc);
		equal.put("compileDate", compileDate);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = projectManageService.addProject(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="editProject.do",method=RequestMethod.POST)
	public void editProject(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String projectName = request.getParameter("projectname").trim();
		String projectVersion = request.getParameter("projectversion").trim();
		String branchName = request.getParameter("branchname").trim();
		String branchPath = request.getParameter("branchpath").trim();
		String dbscriptPath = request.getParameter("dbscriptpath").trim();
		String deployDesc = request.getParameter("deploydesc").trim();
		String compileDate = request.getParameter("compiledate").trim();
		String editId = request.getParameter("editId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("projectName", projectName);
		equal.put("projectVersion", projectVersion);
		equal.put("branchName", branchName);
		equal.put("branchPath", branchPath);
		equal.put("dbscriptPath", dbscriptPath);
		equal.put("deployDesc", deployDesc);
		equal.put("compileDate", compileDate);
		equal.put("editId", editId);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = projectManageService.editProject(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}

	@RequestMapping(value="delProject.do",method=RequestMethod.POST)
	public void delProject(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String projectId = request.getParameter("delId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("projectId", projectId);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = projectManageService.delProject(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
}
