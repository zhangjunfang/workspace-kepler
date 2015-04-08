package com.ctfo.sys.controller;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoAppExceptionDefinition;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.SysComInfo;
import com.ctfo.sys.service.SysCompanyService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.GeneratingCode;
import com.ctfo.util.GeneratorUUID;
import com.ctfo.util.StringUtil;
import com.ctfo.util.Tools;

@Controller
@RequestMapping("/sys/company")
public class SysCompanyController extends BaseController{
	
	@Autowired
	SysCompanyService sysCompanyService;
	
	/**
	 * 
	 * @description:公司管理-查询公司列表
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月02日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryList.do")
	@ResponseBody
	public Map<String, Object> queryList(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String comContact = request.getParameter("requestParam.like.comContact");    //联系人
	    String status = request.getParameter("requestParam.equal.status");          //状态
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //县
	    String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");  //创建开始时间
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd");          //注册结束时间
	    
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(comContact)){
			like.put("comContact", comContact);
		}
		if(StringUtil.isNotBlank(status)){
			equal.put("status", status);
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
		if(StringUtil.isNotBlank(createTimeStart)){
			equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
		}
		if(StringUtil.isNotBlank(createTimeEnd)){
			equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
		}	
		if(opInfo.getIsOperator().equals("0")){
			equal.put("comId", opInfo.getComId());
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = sysCompanyService.count(param);
	    
	    PaginationResult<SysComInfo> list = sysCompanyService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	/**
	 * 
	 * @description:获取公司对象
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月02日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryById.do")
	@ResponseBody
	public SysComInfo selectPK(HttpServletRequest request) throws Exception {
		String comId = request.getParameter("comId");
		return sysCompanyService.selectPK(comId);
	}
	/**
	 * 查询公司列表
	 */
	@ResponseBody
	@RequestMapping(value = "/queryCompanyList.do")
	public List<SysComInfo> queryCompanyList() {
		
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		List<SysComInfo> companylist = new ArrayList<SysComInfo>();
		Map<String,String> map = new HashMap<String,String>();
		
		if(opInfo.getIsOperator().equals("0")){
			map.put("isOperator", "0");
		}
		
		companylist = sysCompanyService.queryCompanyList(map);
		return companylist;
	}
	/**
	 * 查询公司编码
	 */
	@ResponseBody
	@RequestMapping(value = "/queryAutoCode.do")
	public String queryAutoCode() {
		return GeneratingCode.getComCode()+"";
	}
	
	/**
	 * 查询公司名称是否存在
	 */
	@ResponseBody
	@RequestMapping(value = "/isExistComName.do")
	public String isExistSpOperator(HttpServletRequest request, HttpServletResponse response) {
		int count = 0;
		try {
			String Loginname = request.getParameter("comName");
			
			Map<String,String> map = new HashMap<String,String>();
			map.put("Loginname", Loginname);
			count = sysCompanyService.existLoginname(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		
		if (0<count) {
			return this.returnInfoForJS(response, true, CtfoAppExceptionDefinition.OP_A_HAVENAME);
		} else {
			return this.returnInfoForJS(response, false, MES_SUCCESS_ADD);
		}
	}
	/**
	 * 
	 * @description:公司管理-添加
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月08日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/addSysComInfo.do")
	public String addItem(@RequestBody SysComInfo sysComInfo,HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			sysComInfo.setComId(GeneratorUUID.generateUUID());
			sysComInfo.setStatus("1");
			sysComInfo.setCreateBy(opInfo.getOpId());
			sysComInfo.setCreateTime(DateUtil.dateToUtcTime(new Date()));
			sysComInfo.setUpdateBy(sysComInfo.getCreateBy());
			sysComInfo.setUpdateTime(sysComInfo.getCreateTime());

			sysCompanyService.insert(sysComInfo);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 
	 * @description:公司管理-修改
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月09日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/modifySysComInfo.do")
	public String updateItem(@RequestBody SysComInfo sysComInfo,HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			sysComInfo.setUpdateTime(DateUtil.dateToUtcTime(new Date()));
			sysComInfo.setUpdateBy(opInfo.getOpId());
			
			sysCompanyService.update(sysComInfo);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:吊销&启用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月27日上午16:02:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/revokeEditSysCom.do")
	public String updateRevoke(HttpServletRequest request, HttpServletResponse response){
		try {
			String comIds = request.getParameter("comId");
			String status = request.getParameter("status");
			Map<String,String> map = new HashMap<String,String>();
			String comId = Tools.turnString(comIds);
			
			map.put("comId", comId);
			map.put("status", status);
			sysCompanyService.updateRevoke(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 
	 * @description:删除公司
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月09日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/deleteSysCom.do")
	public String deleteSysCom(HttpServletRequest request, HttpServletResponse response){
		try {
			String comId = request.getParameter("comId");
			String comDelete = request.getParameter("comDelete");
			Map<String,String> map = new HashMap<String,String>();
			
			map.put("comId", comId);
			map.put("comDelete", comDelete);
			sysCompanyService.deleteSysCom(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:导出公司列表
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportCompanyListExcelData.do")
	@ResponseBody
	public void exportCompanyListExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String comContact = request.getParameter("requestParam.like.comContact");    //联系人
	    String status = request.getParameter("requestParam.equal.status");          //状态
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //县
	    String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");  //创建开始时间
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd");          //注册结束时间
	    
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(comContact)){
			like.put("comContact", comContact);
		}
		if(StringUtil.isNotBlank(status)){
			equal.put("status", status);
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
		if(StringUtil.isNotBlank(createTimeStart)){
			equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
		}
		if(StringUtil.isNotBlank(createTimeEnd)){
			equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
		}	
		
		param.setLike(like);
	    param.setEqual(equal);
		
		PaginationResult<SysComInfo> list = sysCompanyService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comCode=公司编码&comName=企业名称&province=所在地&comContact=联系人&comTel=联系电话&createBy=创建人&createTime=创建时间&status=状态&remark=备注";
		String excel_result = sysCompanyService.exportData(exportDataHeader, json, this.getUrl(request),"SysComInfo","公司列表");
	    this.printWriter(response, excel_result);
	}
}
