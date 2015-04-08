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
import com.ctfo.service.ServerManageService;

@Controller
@SuppressWarnings({ "rawtypes" })
public class ServerManageAction {

	@Autowired
	private ServerManageService serverManageService;
	
	@RequestMapping(value="getServerList.do",method=RequestMethod.POST)
	public void getServerList(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		int page = new Integer(request.getParameter("page").trim());
		int rows = new Integer(request.getParameter("rows").trim());
		
		Map<String, String> equal = new HashMap<String, String>();
		
		requestParam.setEqual(equal);
		requestParam.setPage((page-1)*rows);
		requestParam.setRows(rows);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = serverManageService.getServerList(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="getEditServer.do",method=RequestMethod.POST)
	public void getEditServer(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		requestParam.setEqual(equal);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = serverManageService.getEditServer(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="addServer.do",method=RequestMethod.POST)
	public void addServer(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String serverName = request.getParameter("servername").trim();
		String sshIp = request.getParameter("sship").trim();
		String sshPort = request.getParameter("sshport").trim();
		String sshUsername = request.getParameter("sshusername").trim();
		String sshUserpwd = request.getParameter("sshuserpwd").trim();
		String pid = request.getParameter("pid").trim();
		String remark = request.getParameter("remark").trim();
		
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("serverName", serverName);
		equal.put("sshIp", sshIp);
		equal.put("sshPort", sshPort);
		equal.put("sshUsername", sshUsername);
		equal.put("sshUserpwd", sshUserpwd);
		equal.put("pid", pid);
		equal.put("remark", remark);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = serverManageService.addServer(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="editServer.do",method=RequestMethod.POST)
	public void editServer(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String serverName = request.getParameter("servername").trim();
		String sshIp = request.getParameter("sship").trim();
		String sshPort = request.getParameter("sshport").trim();
		String sshUsername = request.getParameter("sshusername").trim();
		String sshUserpwd = request.getParameter("sshuserpwd").trim();
		String pid = request.getParameter("pid").trim();
		String remark = request.getParameter("remark").trim();
		String editId = request.getParameter("editId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("serverName", serverName);
		equal.put("sshIp", sshIp);
		equal.put("sshPort", sshPort);
		equal.put("sshUsername", sshUsername);
		equal.put("sshUserpwd", sshUserpwd);
		equal.put("pid", pid);
		equal.put("remark", remark);
		equal.put("editId", editId);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = serverManageService.editServer(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}

	@RequestMapping(value="delServer.do",method=RequestMethod.POST)
	public void delServer(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String sid = request.getParameter("delId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("sid", sid);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = serverManageService.delServer(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
}
