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

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.service.AuthManageService;
import com.ctfo.travel.basic.controller.BaseController;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;


@Controller
@RequestMapping("/monitor/link")
public class LinkController extends BaseController{
	
	@Autowired
	AuthManageService authManageService;
	
	/**
	 * 
	 * @description:宇通链路状态
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月06日下午14:30
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
	    String comContact = request.getParameter("requestParam.like.comContact");    //联系人
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //地市
	    String serviceStatus = request.getParameter("requestParam.equal.serviceStatus");  //服务器在线状态
	    String registTimeStart = request.getParameter("requestParam.equal.registTimeStart");  //注册开始时间
	    String registEndTimeEnd = request.getParameter("requestParam.equal.registEndTimeEnd");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(comContact)){
			like.put("comContact", comContact);
		}
		if(StringUtil.isNotBlank(corpProvince)){
			equal.put("province", corpProvince);
		}
		if(StringUtil.isNotBlank(corpCity)){
			equal.put("city", corpCity);
		}
		if(StringUtil.isNotBlank(corpCounty)){
			equal.put("county", corpCounty);
		}		
		if(StringUtil.isNotBlank(serviceStatus)){
			equal.put("serviceStatus", serviceStatus);
		}
		if(StringUtil.isNotBlank(registTimeStart)){
			equal.put("registTimeStart", DateUtil.dateToUtcTime(sdf.parse(registTimeStart)));
		}
		if(StringUtil.isNotBlank(registEndTimeEnd)){
			equal.put("registEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registEndTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = authManageService.count(param);
	    PaginationResult<CompanyInfo> list = authManageService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	/**
	 * 
	 * @description:导出链路状态
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportMonitorLinkExcelData.do")
	@ResponseBody
	public void exportOnlineUsersManageExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String comContact = request.getParameter("requestParam.like.comContact");    //联系人
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //地市
	    String serviceStatus = request.getParameter("requestParam.equal.serviceStatus");  //服务器在线状态
	    String registTimeStart = request.getParameter("requestParam.equal.registTimeStart");  //注册开始时间
	    String registEndTimeEnd = request.getParameter("requestParam.equal.registEndTimeEnd");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(comContact)){
			like.put("comContact", comContact);
		}
		if(StringUtil.isNotBlank(corpProvince)){
			equal.put("province", corpProvince);
		}
		if(StringUtil.isNotBlank(corpCity)){
			equal.put("city", corpCity);
		}
		if(StringUtil.isNotBlank(corpCounty)){
			equal.put("county", corpCounty);
		}		
		if(StringUtil.isNotBlank(serviceStatus)){
			equal.put("serviceStatus", serviceStatus);
		}
		if(StringUtil.isNotBlank(registTimeStart)){
			equal.put("registTimeStart", DateUtil.dateToUtcTime(sdf.parse(registTimeStart)));
		}
		if(StringUtil.isNotBlank(registEndTimeEnd)){
			equal.put("registEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registEndTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		PaginationResult<CompanyInfo> list = authManageService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comName=公司名称&comContact=联系人&registTime=注册时间&province=所在地&machineSerial=机器码&authorizationCode=宇通授权码&macAddress=计算机MAC地址&serviceVersion=服务端安装版本&registIp=IP&ytCrmLinkedStatus=宇通CRM系统链路&serviceStatus=在线状态";
		String excel_result = authManageService.exportData(exportDataHeader, json, this.getUrl(request),"CompanyInfo","链路状态");
	    this.printWriter(response, excel_result);
	}
}
