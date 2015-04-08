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
import com.ctfo.monitor.beans.VisitStat;
import com.ctfo.monitor.service.VisitService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;
@Controller
@RequestMapping("/monitor/visit")
public class VisitController extends BaseController{

	@Autowired
	VisitService visitService;	
	
	/**
	 * 
	 * @description:访问统计
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月11日10:40
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
	    String accessTimeStart = request.getParameter("requestParam.equal.accessTimeStart");  //注册开始时间
	    String accessTimeEnd = request.getParameter("requestParam.equal.accessTimeEnd");  //注册开始时间
	    String funId = request.getParameter("requestParam.equal.funId");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(funId)){
			equal.put("funId", funId);
		}
		if(StringUtil.isNotBlank(accessTimeStart)){
			equal.put("accessTimeStart", DateUtil.dateToUtcTime(sdf.parse(accessTimeStart)));
		}
		if(StringUtil.isNotBlank(accessTimeEnd)){
			equal.put("accessTimeEnd", DateUtil.dateToUtcTime(sdf.parse(accessTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = visitService.count(param);
	    PaginationResult<VisitStat> list = visitService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    for(VisitStat object: list.getData()){
	    	object.setAccessTimeStart(accessTimeStart);
	    	object.setAccessTimeEnd(accessTimeEnd);
	    }
	    
	    
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:导出访问统计
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportMonitorVisitExcelData.do")
	@ResponseBody
	public void exportMonitorVisitExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //帐套
	    String accessTimeStart = request.getParameter("requestParam.equal.accessTimeStart");  //注册开始时间
	    String accessTimeEnd = request.getParameter("requestParam.equal.accessTimeEnd");  //注册开始时间
	    String funId = request.getParameter("requestParam.equal.funId");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(funId)){
			equal.put("funId", funId);
		}
		if(StringUtil.isNotBlank(accessTimeStart)){
			equal.put("accessTimeStart", DateUtil.dateToUtcTime(sdf.parse(accessTimeStart)));
		}
		if(StringUtil.isNotBlank(accessTimeEnd)){
			equal.put("accessTimeEnd", DateUtil.dateToUtcTime(sdf.parse(accessTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
	    
		PaginationResult<VisitStat> list = visitService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comName=公司名称&setbookName=帐套&accessTimeStart=开始时间&accessTimeEnd=结束时间&funName=访问功能模块&total=次数";
		String excel_result = visitService.exportData(exportDataHeader, json, this.getUrl(request),"VisitStat","访问统计");
	    this.printWriter(response, excel_result);
	}
}
