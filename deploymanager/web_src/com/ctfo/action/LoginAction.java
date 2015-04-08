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

import com.ctfo.beans.SysSpOperator;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.PaginationResult;
import com.ctfo.service.SysSpOperatorService;
import com.ctfo.utils.PwdDigest;

@Controller
@SuppressWarnings({ "rawtypes" })
public class LoginAction {

	@Autowired
	private SysSpOperatorService operatorService;

	/**
	 * 登陆
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
	@RequestMapping(value="login.do",method=RequestMethod.POST)
	public void login(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		PaginationResult result = new PaginationResult();

		// 用户名
		String userId = request.getParameter("userId").trim();
		// 密码
		String pass = PwdDigest.passwordDigest(request.getParameter("password").trim());
		// 查询参数
		DynamicSqlParameter requestParam = new DynamicSqlParameter();

		Map<String, String> equal = new HashMap<String, String>();
		equal.put("opLoginname", userId);

		requestParam.setEqual(equal);
		
		try {
			result = operatorService.login(requestParam);
			PrintWriter out = response.getWriter();
			if(result.getData().size() != 0){
				SysSpOperator oper = (SysSpOperator) result.getData().toArray()[0];
				if(oper.getOpPass().equals(pass)){
					session.setMaxInactiveInterval(24 * 60 * 60);
					session.setAttribute("opId", oper.getOpId().toString());
					session.setAttribute("opName", oper.getOpLoginname().toString());
					session.setAttribute("realName", oper.getRealName().toString());
					session.setAttribute("roleId", oper.getRoleId().toString());
					out.print("pass");
				}
				else{
					out.print("freeze");
				}
				
			}
		} catch (CtfoAppException e) {
			
		}

	}
	
	@RequestMapping(value="logout.do",method=RequestMethod.POST)
	public void logout(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		response.setContentType("text/plain");
		try {
			PrintWriter out = response.getWriter();
			if(request.getSession(false)!=null){
				session.invalidate();
			}
			out.print("exit");
		} catch (Exception e) {
			// TODO: handle exception
		}
	}

	@RequestMapping("/BridgeLogin")
	public void bridgeLogin(HttpServletRequest request, HttpSession session, HttpServletResponse response) throws Exception {
		String corpId = request.getParameter("corpId").trim();
		String pt = request.getParameter("pt").trim();
		String userId = request.getParameter("userId").trim();
		session.setAttribute("userId", userId);
		session.setAttribute("pt", pt);
		session.setAttribute("corpId", corpId);
		String path = request.getContextPath();
		String basePath = request.getScheme() + "://" + request.getServerName() + ":" + request.getServerPort() + path + "/";
		PrintWriter pw = response.getWriter();
		pw.print(basePath + "bridge.jsp");
	}

}
