package com.ctfo.monitor.controller;

import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.beans.UserBehavior;
import com.ctfo.monitor.service.UserBehaviorService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;


@Controller
@RequestMapping("/monitor/userBehavior")
public class UserBehaviorController extends BaseController{
	
	@Autowired
	UserBehaviorService userBehaviorService;
	
	/**
	 * 
	 * @description:用户行为监控
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月06日下午20:40
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryList.do")
	@ResponseBody
	public Map<String, Object> queryList(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //帐套
	    String clientAccount = request.getParameter("requestParam.like.clientAccount");    //联系人
	    String timeStart = request.getParameter("requestParam.equal.timeStart");  //注册开始时间
	    String timeEnd = request.getParameter("requestParam.equal.timeEnd");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(clientAccount)){
			like.put("clientAccount", clientAccount);
		}
		if(StringUtil.isNotBlank(timeStart)){
			equal.put("timeStart", DateUtil.dateToUtcTime(sdf.parse(timeStart)));
		}
		if(StringUtil.isNotBlank(timeEnd)){
			equal.put("timeEnd", DateUtil.dateToUtcTime(sdf.parse(timeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = userBehaviorService.count(param);
	    PaginationResult<UserBehavior> list = userBehaviorService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	/**
	 * 
	 * @description:导出用户行为监控
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportMonitorUserBehaviorExcelData.do")
	@ResponseBody
	public void exportMonitorUserBehaviorExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //帐套
	    String clientAccount = request.getParameter("requestParam.like.clientAccount");    //联系人
	    String timeStart = request.getParameter("requestParam.equal.timeStart");  //注册开始时间
	    String timeEnd = request.getParameter("requestParam.equal.timeEnd");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(clientAccount)){
			like.put("clientAccount", clientAccount);
		}
		if(StringUtil.isNotBlank(timeStart)){
			equal.put("timeStart", DateUtil.dateToUtcTime(sdf.parse(timeStart)));
		}
		if(StringUtil.isNotBlank(timeEnd)){
			equal.put("timeEnd", DateUtil.dateToUtcTime(sdf.parse(timeEnd)));
		}
		param.setLike(like);
	    param.setEqual(equal);
		PaginationResult<UserBehavior> list = userBehaviorService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "watchTime=时间&onlineType=在线行为类型&clientAccount=账号&roleName=角色&clientAccountOrgid=所属组织&comName=所属公司&setbookName=所属帐套&loadIdAddr=IP地址&clientMac=计算机MAC地址";
		String excel_result = userBehaviorService.exportData(exportDataHeader, json, this.getUrl(request),"UserBehavior","用户行为监控");
	    this.printWriter(response, excel_result);
	}
}
