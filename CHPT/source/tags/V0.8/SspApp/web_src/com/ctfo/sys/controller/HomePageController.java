package com.ctfo.sys.controller;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.operation.service.AuthManageService;
import com.ctfo.operation.service.TbSetbookService;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.service.LoginService;
import com.ctfo.travel.basic.controller.BaseController;
import com.ctfo.util.DateUtil;

@Controller
@RequestMapping("/homepage")
public class HomePageController extends BaseController{
	@Autowired
	TbSetbookService tbSetbookService;
	@Autowired
	private LoginService loginService;
	@Autowired
	AuthManageService authManageService;
	/**
	 * 
	 * @description:基本信息
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月03日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/basicInfo.do")
	@ResponseBody
	public Map<String, Object> queryBasicInfo(HttpServletRequest request) throws Exception{
		
		Map<String, Object> result = new HashMap<String, Object>();
		try {
			String opId = request.getParameter("opId");
			HttpSession session = request.getSession();
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo();
			SysSpOperator operator = null;
			Map<String, Object> map = new HashMap<String, Object>();
			map.put("opId", opId);
			operator = loginService.basicInfo(map);
			
			result.put("photo", opInfo.getPhoto());
			result.put("opLoginname", operator.getOpLoginname());
			result.put("comName", operator.getComName());
			result.put("entName", operator.getEntName());
			result.put("roleName", operator.getRoleName());
			result.put("loginTime", session.getAttribute("loadTime"));
			result.put("loginIp", session.getAttribute("ip"));
			return result;
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
			return result;
		}
	}
	
	/**
	 * 
	 * @description:待办鉴权
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月03日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/todoAuth.do")
	@ResponseBody
	public Map<String, Object> toDoAuth(HttpServletRequest request) throws Exception{
		
		Map<String, Object> result = new HashMap<String, Object>();
		try {
			DynamicSqlParameter param = super.getPageParam(request);
			Map<String, Object> equal = new HashMap<String, Object>();
			equal.put("registerAuthentication", "2");
			param.setEqual(equal);
			int total = authManageService.count(param);
			result.put("totaltodoAuth", total);
			return result;
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
			return result;
		}
	}
	/**
	 * 
	 * @description:系统信息
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月03日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/sysInfo.do")
	@ResponseBody
	public Map<String, Object> sysInfo(HttpServletRequest request) throws Exception{
		Map<String, Object> result = new HashMap<String, Object>();
		try {
			DynamicSqlParameter param = super.getPageParam(request);
			Map<String, Object> equal = new HashMap<String, Object>();
			int serStaTotal = authManageService.count(param);
			equal.put("serviceStatus", "1");
			param.setEqual(equal);
			int onlineStaTotal = authManageService.count(param);
			int setbookTotal = tbSetbookService.count(param);
			equal.put("status", "1");
			param.setEqual(equal);
			int useSetbookTotal = tbSetbookService.count(param);
			result.put("serStaTotal", serStaTotal);
			result.put("onlineStaTotal", onlineStaTotal);
			result.put("setbookTotal", setbookTotal);
			result.put("useSetbookTotal", useSetbookTotal);
			return result;
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
			return result;
		}
	}
	/***
	 * 获取当前系统时间
	 * @param request
	 * @return
	 * @throws Exception
	 * 马驰
	 */
	@RequestMapping(value="/getCurrentTime.do")
	@ResponseBody
	public String getCurrentTime(HttpServletRequest request) throws Exception{
		String currentTime =  Long.toString(DateUtil.dateToUtcTime(new Date()));
		return currentTime;
	}	
}
