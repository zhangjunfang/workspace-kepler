package com.ctfo.archives.controller;

import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.archives.beans.Archives;
import com.ctfo.archives.beans.ArchivesDetail;
import com.ctfo.archives.beans.SysSetbook;
import com.ctfo.archives.service.ArchivesDetailService;
import com.ctfo.archives.service.ArchivesService;
import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.service.OnlineUsersService;
import com.ctfo.operation.service.AuthManageService;
import com.ctfo.operation.service.TbSetbookService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;
@Controller
@RequestMapping("/archives/profiles")
public class ArchivesController extends BaseController{
	
	@Autowired
	ArchivesService archivesService;
	@Autowired
	OnlineUsersService onlineUsersService;
	@Autowired
	ArchivesDetailService archivesDetailService;
	@Autowired
	AuthManageService authManageService;
	@Autowired
	TbSetbookService tbSetbookService;
	/**
	 * 
	 * @description:用户档案
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月14日11:14
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
	    String registerTimeStart = request.getParameter("requestParam.equal.registerTimeStart");  //注册开始时间
	    String registertTimeEnd = request.getParameter("requestParam.equal.registertTimeEnd");          //注册结束时间
	    String validTimeStart = request.getParameter("requestParam.equal.validTimeStart");  //有效期开始时间
	    String validTimeEnd = request.getParameter("requestParam.equal.validTimeEnd");          //有效期结束时间	    
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(registerTimeStart)){
			equal.put("registerTimeStart", DateUtil.dateToUtcTime(sdf.parse(registerTimeStart)));
		}
		if(StringUtil.isNotBlank(registertTimeEnd)){
			equal.put("registertTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registertTimeEnd)));
		}
		if(StringUtil.isNotBlank(validTimeStart)){
			equal.put("validTimeStart", DateUtil.dateToUtcTime(sdf.parse(validTimeStart)));
		}
		if(StringUtil.isNotBlank(validTimeEnd)){
			equal.put("validTimeEnd", DateUtil.dateToUtcTime(sdf.parse(validTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = archivesService.count(param);
	    
	    PaginationResult<Archives> list = archivesService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:用户档案列表
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月14日11:14
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryDetailList.do")
	@ResponseBody
	public Map<String, Object> queryDetailList(HttpServletRequest request) throws Exception{
		
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //帐套
	    String clientAccount = request.getParameter("requestParam.like.clientAccount");    //账号
	    String registerTimeStart = request.getParameter("requestParam.equal.registerTimeStart");  //注册开始时间
	    String registertTimeEnd = request.getParameter("requestParam.equal.registertTimeEnd");          //注册结束时间
	    String loadTimeStart = request.getParameter("requestParam.equal.loadTimeStart");  //最后开始时间
	    String loadTimeEnd = request.getParameter("requestParam.equal.loadTimeEnd");          //最后结束时间	   
	    String comId = request.getParameter("requestParam.equal.comId");
		
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
		if(StringUtil.isNotBlank(registerTimeStart)){
			equal.put("registerTimeStart", DateUtil.dateToUtcTime(sdf.parse(registerTimeStart)));
		}
		if(StringUtil.isNotBlank(registertTimeEnd)){
			equal.put("registertTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registertTimeEnd)));
		}
		if(StringUtil.isNotBlank(loadTimeStart)){
			equal.put("loadTimeStart", DateUtil.dateToUtcTime(sdf.parse(loadTimeStart)));
		}
		if(StringUtil.isNotBlank(loadTimeEnd)){
			equal.put("loadTimeEnd", DateUtil.dateToUtcTime(sdf.parse(loadTimeEnd)));
		}
		if(StringUtil.isNotBlank(comId)){
			equal.put("comId", comId);
		}
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = archivesDetailService.count(param);
	    
	    PaginationResult<ArchivesDetail> list = archivesDetailService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:用户档案明细
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月13日11:11
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryUserFileSingleDetail.do")
	@ResponseBody
	public ArchivesDetail queryUserFileSingleDetail(HttpServletRequest request) throws Exception{
		String tbUserOnlineId = request.getParameter("tbUserOnlineId");
		return archivesDetailService.selectPK(tbUserOnlineId);
	}
	/**
	 * 
	 * @description:获取公司详情
	 * @param:
	 * @author: 马驰
	 */
	@RequestMapping(value="/queryCompanyDetail.do")
	@ResponseBody
	public SysSetbook selectPK(HttpServletRequest request) throws Exception {
		String comId = request.getParameter("comId");
		return tbSetbookService.selectPKByCom(comId);
	}
	/**
	 * 
	 * @description:导出用户档案
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月23日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportArchivesGridExcelData.do")
	@ResponseBody
	public void exportArchivesGridExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		PaginationResult<Archives> list = archivesService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comName=公司名称&registTime=注册时间&validDate=有效期&setbookName=帐套名&createTime=帐套创建时间&total=客户端用户数";
		String excel_result = onlineUsersService.exportData(exportDataHeader, json, this.getUrl(request),"Archives","用户档案");
	    this.printWriter(response, excel_result);
	}
	
	/**
	 * 
	 * @description:导出用户档案列表
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月23日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportUserFileExcelData.do")
	@ResponseBody
	public void exportUserFileExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //帐套
	    String registerTimeStart = request.getParameter("requestParam.equal.registerTimeStart");  //注册开始时间
	    String registertTimeEnd = request.getParameter("requestParam.equal.registertTimeEnd");          //注册结束时间
	    String validTimeStart = request.getParameter("requestParam.equal.validTimeStart");  //有效期开始时间
	    String validTimeEnd = request.getParameter("requestParam.equal.validTimeEnd");          //有效期结束时间	    
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(registerTimeStart)){
			equal.put("registerTimeStart", DateUtil.dateToUtcTime(sdf.parse(registerTimeStart)));
		}
		if(StringUtil.isNotBlank(registertTimeEnd)){
			equal.put("registertTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registertTimeEnd)));
		}
		if(StringUtil.isNotBlank(validTimeStart)){
			equal.put("validTimeStart", DateUtil.dateToUtcTime(sdf.parse(validTimeStart)));
		}
		if(StringUtil.isNotBlank(validTimeEnd)){
			equal.put("validTimeEnd", DateUtil.dateToUtcTime(sdf.parse(validTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		PaginationResult<ArchivesDetail> list = archivesDetailService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "userCode=人员编码&userId=账号&userName=人员姓名&roleName=角色&comName=所属公司&orgName=所属组织&setbookName=所在帐套&isOperator=是否操作员&createTime=创建时间&loginTime=最后登录时间";
		String excel_result = archivesDetailService.exportData(exportDataHeader, json, this.getUrl(request),"ArchivesDetail","用户档案列表");
	    this.printWriter(response, excel_result);
	}
}
