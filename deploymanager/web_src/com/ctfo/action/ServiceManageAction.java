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
import com.ctfo.service.ServiceManageService;

@Controller
@SuppressWarnings({ "rawtypes" })
public class ServiceManageAction {

	@Autowired
	private ServiceManageService serviceManageService;
	
	@RequestMapping(value="getServiceList.do",method=RequestMethod.POST)
	public void getServiceList(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		int page = new Integer(request.getParameter("page").trim());
		int rows = new Integer(request.getParameter("rows").trim());
		
		Map<String, String> equal = new HashMap<String, String>();
		
		requestParam.setEqual(equal);
		requestParam.setPage((page-1)*rows);
		requestParam.setRows(rows);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = serviceManageService.getServiceList(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="getEditService.do",method=RequestMethod.POST)
	public void getEditService(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		requestParam.setEqual(equal);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = serviceManageService.getEditService(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="addService.do",method=RequestMethod.POST)
	public void addService(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String serviceName = request.getParameter("servicename").trim();
		String serviceType = request.getParameter("servicetype").trim();
		String launchType = request.getParameter("launchtype").trim();
		String launchShell = request.getParameter("launchshell").trim();
		
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("serviceName", serviceName);
		equal.put("serviceType", serviceType);
		equal.put("launchType", launchType);
		equal.put("launchShell", launchShell);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = serviceManageService.addService(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="editService.do",method=RequestMethod.POST)
	public void editService(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String serviceName = request.getParameter("servicename").trim();
		String serviceType = request.getParameter("servicetype").trim();
		String launchType = request.getParameter("launchtype").trim();
		String launchShell = request.getParameter("launchshell").trim();
		String editId = request.getParameter("editId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("serviceName", serviceName);
		equal.put("serviceType", serviceType);
		equal.put("launchType", launchType);
		equal.put("launchShell", launchShell);
		equal.put("editId", editId);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = serviceManageService.editService(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}

	@RequestMapping(value="delService.do",method=RequestMethod.POST)
	public void delService(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String delId = request.getParameter("delId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("delId", delId);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = serviceManageService.delService(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
}
