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
import com.ctfo.service.PlatFormManageService;

@Controller
@SuppressWarnings({ "rawtypes" })
public class PlatFormManageAction {

	@Autowired
	private PlatFormManageService platFormManageService;
	
	@RequestMapping(value="getPlatFormList.do",method=RequestMethod.POST)
	public void getPlatFormList(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		int page = new Integer(request.getParameter("page").trim());
		int rows = new Integer(request.getParameter("rows").trim());
		
		Map<String, String> equal = new HashMap<String, String>();
		
		requestParam.setEqual(equal);
		requestParam.setPage((page-1)*rows);
		requestParam.setRows(rows);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = platFormManageService.getPlatFormList(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="getEditPlatForm.do",method=RequestMethod.POST)
	public void getEditPlatForm(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {

		DynamicSqlParameter requestParam = new DynamicSqlParameter();
		
		String editId = request.getParameter("editId").trim();
		
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("editId", editId);
		requestParam.setEqual(equal);
		
		try {
			Map<String, Object> map = new HashMap<String, Object>(); 
			map = platFormManageService.getEditPlatForm(requestParam);
			String res = JsonUtil.map2json(map);
			PrintWriter out = response.getWriter();
			out.print(res);
			
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="addPlatForm.do",method=RequestMethod.POST)
	public void addPlatForm(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String platname = request.getParameter("platname").trim();
		String remark = request.getParameter("remark").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("platname", platname);
		equal.put("remark", remark);
		//equal.put("creater", (String) session.getAttribute("opId"));

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = platFormManageService.addPlatForm(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="editPlatForm.do",method=RequestMethod.POST)
	public void editPlatForm(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String platname = request.getParameter("platname").trim();
		String remark = request.getParameter("remark").trim();
		String editId = request.getParameter("editId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("platname", platname);
		equal.put("remark", remark);
		equal.put("editId", editId);
		//equal.put("updater", (String) session.getAttribute("opId"));

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = platFormManageService.editPlatForm(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}

	@RequestMapping(value="delPlatForm.do",method=RequestMethod.POST)
	public void delPlatForm(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String platid = request.getParameter("delId").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("platid", platid);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = platFormManageService.delPlatForm(requestParam);
			if(result == 1){
				out.print("done");
			}
		} catch (CtfoAppException e) {
			
		}
	}
	
	@RequestMapping(value="checkPlatExist.do",method=RequestMethod.POST)
	public void checkPlatExist(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		int result = 0;

		String platname = request.getParameter("platname").trim();
		
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("platname", platname);

		requestParam.setEqual(equal);

		try {
			PrintWriter out = response.getWriter();
			result = platFormManageService.checkPlatExist(requestParam);
			if(result == 1){
				out.print("used");
			}
		} catch (CtfoAppException e) {
			
		}
	}
}
