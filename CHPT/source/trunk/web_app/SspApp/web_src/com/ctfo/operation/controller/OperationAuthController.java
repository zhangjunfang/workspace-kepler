package com.ctfo.operation.controller;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
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
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.AddApp;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.service.AddAppService;
import com.ctfo.operation.service.AuthManageService;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.util.Base64_URl;
import com.ctfo.util.DateUtil;
import com.ctfo.util.GeneratorUUID;
import com.ctfo.util.MD5Util;
import com.ctfo.util.RedisJsonUtil;
import com.ctfo.util.StringUtil;
import com.ctfo.util.Tools;
import com.google.gson.reflect.TypeToken;


@Controller
@RequestMapping("/operation/auth")
public class OperationAuthController extends BaseController {
	
	@Autowired
	AuthManageService authManageService;
	@Autowired
	AddAppService addAppService;	
	/**
	 * 
	 * @description:注册鉴权-查询用户列表
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月14日下午09:50
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryList.do")
	@ResponseBody
	public Map<String, Object> queryList(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.equal.comName");    //公司名称
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //县
	    String registTimeStart = request.getParameter("requestParam.equal.registTimeStart");  //注册开始时间
	    String registEndTimeEnd = request.getParameter("requestParam.equal.registEndTimeEnd");          //注册结束时间
	    String validTimeStart = request.getParameter("requestParam.equal.validTimeStart");  //有效期开始时间
	    String validEndTimeEnd = request.getParameter("requestParam.equal.validEndTimeEnd");  //有效期开始时间
	    String registerAuthentication = request.getParameter("requestParam.equal.registerAuthentication");          //注册鉴权情况
	    String status = request.getParameter("requestParam.equal.status");          //状态
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
/*	    OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
	    equal.put("entId", opInfo.getEntId());*/
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
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
		if(StringUtil.isNotBlank(registTimeStart)){
			equal.put("registTimeStart", DateUtil.dateToUtcTime(sdf.parse(registTimeStart)));
		}
		if(StringUtil.isNotBlank(registEndTimeEnd)){
			equal.put("registEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registEndTimeEnd)));
		}
		if(StringUtil.isNotBlank(validTimeStart)){
			equal.put("validTimeStart", DateUtil.dateToUtcTime(sdf.parse(validTimeStart)));
		}
		if(StringUtil.isNotBlank(validEndTimeEnd)){
			equal.put("validEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(validEndTimeEnd)));
		}		
		if(StringUtil.isNotBlank(registerAuthentication)){
			equal.put("registerAuthentication", registerAuthentication);
		}
		if(StringUtil.isNotBlank(status)){
			equal.put("status", status);
		}
		equal.put("nowDate", DateUtil.dateToUtcTime(new Date()));
		
		param.setRegisterAuthentication(registerAuthentication);
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
	 * @description:获取注册ip
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月03日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/getSessionIP.do")
	@ResponseBody
	public Map<String, Object> getSessionIP(HttpServletRequest request) throws Exception {
		Map<String, Object> result = new HashMap<String, Object>(1); 
		String ip = request.getHeader("x-forwarded-for");  
	    if(ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ip)) {  
	        ip = request.getHeader("Proxy-Client-IP");  
	    }  
	    if(ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ip)) {  
	        ip = request.getHeader("WL-Proxy-Client-IP");  
	    }  
	    if(ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ip)) {  
	        ip = request.getRemoteAddr();  
	    }  
	    result.put("authIP", ip);
		return result;
	}
	/**
	 * 
	 * @description:获取对象
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月16日上午11:40
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryById.do")
	@ResponseBody
	public CompanyInfo selectPK(HttpServletRequest request) throws Exception {
		String comId = request.getParameter("comId");
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("comId", comId);
		map.put("nowDate", DateUtil.dateToUtcTime(new Date()));
		
		return authManageService.selectPKById(map);
	}
	/**
	 * 
	 * @description:获取公司增值应用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月16日上午11:40
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryAddApp.do")
	@ResponseBody
	public Map<String, Object> selectAddApp(HttpServletRequest request) throws Exception {
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    
	    String comId = request.getParameter("comId");
		if(StringUtil.isNotBlank(comId)){
			equal.put("comId", comId);
		}
		equal.put("nowDate", DateUtil.dateToUtcTime(new Date()));
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = addAppService.count(param);
	    
	    PaginationResult<AddApp> list = addAppService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);
	    
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	/**
	 * 
	 * @description 新增离线授权
	 * @param:
	 * @author: 马驰
	 * @throws ParseException 
	 * @creatTime:  2014年10月21日下午17:09
	 * @modifyInformation：
	 */
	@RequestMapping(value="/insertDownDetail.do")
	public String insertDownDetail(@RequestBody CompanyInfo companyInfo,HttpServletResponse response) throws ParseException{
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo();
			String comId = GeneratorUUID.generateUUID();
			companyInfo.setCreateTime(DateUtil.dateToUtcTime(new Date()));
			companyInfo.setComId(comId);
			companyInfo.setCreateBy(opInfo.getOpId());
			if(companyInfo.getApprovalAdvice().equals("0")||companyInfo.getApprovalAdvice() == "0"){
				companyInfo.setRegisterAuthentication("1");
				companyInfo.setApprovalTime(DateUtil.dateToUtcTime(new Date()));
				companyInfo.setStatus("1");
			}else if(companyInfo.getApprovalAdvice().equals("1") || companyInfo.getApprovalAdvice() == "1"){
				companyInfo.setRegisterAuthentication("2");
				companyInfo.setStatus("0");
			}
			authManageService.insert(companyInfo);
			
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
			AddApp app = new AddApp();
			String addApp =companyInfo.getAddApp();//获取多个增值应用信息
			String addAppDetailList [] = addApp.split(";");
			String bizName = "" ;
			String registerAuthentication = "" ;
			String validDate = "";
			String addAppRemark = "" ;
			
			for (int i = 0; i < addAppDetailList.length; i++) {
				String addAppDetail[] = addAppDetailList [i].split(",",-1);
					if(null!=addAppDetail[0]||!addAppDetail[0].equals("")){
						bizName = addAppDetail[0];
					}
					if(null!=addAppDetail[1]||!addAppDetail[1].equals("")){
						registerAuthentication = addAppDetail[1];
					}
					if(null!=addAppDetail[2]||!addAppDetail[2].equals("")){
						validDate = addAppDetail[2];
					}
					if(null!=addAppDetail[3]||!addAppDetail[3].equals("")){
						addAppRemark = addAppDetail[3];
					}
					app.setValueAddId(GeneratorUUID.generateUUID());
					app.setCreateTime(DateUtil.dateToUtcTime(new Date()));
					app.setBizName(bizName);
					app.setComId(comId);
					app.setCreateBy(opInfo.getOpLoginname());
					app.setValidDate(DateUtil.dateToUtcTime(sdf.parse(validDate)));
					app.setRemark(addAppRemark);
					if(registerAuthentication.equals("1")){
						app.setRegisterAuthentication("1");
						app.setStatus("1");
					}else if(registerAuthentication.equals("3")){
						app.setRegisterAuthentication("3");
						app.setStatus("0");
					}
				addAppService.insert(app);
			}

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
	@RequestMapping(value="/revokeOpen.do")
	public String updateRevokeOpen(HttpServletRequest request, HttpServletResponse response){
		try {
			String comIds = request.getParameter("comId");
			String status = request.getParameter("status");
			Map<String,Object> map = new HashMap<String,Object>();
			
			String[] comId = comIds.split(",", -1);
			map.put("comId", comId);
			map.put("status", status);
			
			authManageService.updateRevokeOpen(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 
	 * @description:审批
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月28日上午18:47:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/updateAuthApproval.do")
	@ResponseBody
	public String updateAuthApproval(@RequestBody CompanyInfo companyInfo,HttpServletResponse response){
		try {
			Map<String,String> map = new HashMap<String,String>();
			map.put("comId", companyInfo.getComId());
			map.put("approvalTime",  Long.toString(DateUtil.dateToUtcTime(new Date())));
			map.put("approver", companyInfo.getApprover());
			map.put("approvalAdvice", companyInfo.getApprovalAdvice());
			map.put("remark", companyInfo.getRemark());
			map.put("status", "1");
			
			companyInfo.getAuthorizationCode();
			
			if(companyInfo.getApprovalAdvice().equals("0")){
				map.put("registerAuthentication", "1");
			}else if(companyInfo.getApprovalAdvice().equals("1")){
				map.put("registerAuthentication", "3");
			}
			authManageService.updateAuthApproval(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		if(companyInfo.getApprovalAdvice().equals("0")){
			return this.returnInfoForJS(response, true, companyInfo.getAuthorizationCode());
		}
		return this.returnInfoForJS(response, false, MES_SUCCESS_OPERATE);
	}
	/**
	 * 
	 * @description:管理
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月29日上午 09:47:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/updateAuthManage.do")
	@ResponseBody
	public String updateAuthManage(@RequestBody CompanyInfo companyInfo,HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo();
			Map<String,String> map = new HashMap<String,String>();
			map.put("comId", companyInfo.getComId());
			map.put("updateTime",  Long.toString(DateUtil.dateToUtcTime(new Date())));//编辑时间
			map.put("updateBy",  opInfo.getOpId());//编辑人
			
			map.put("approvalTime", Long.toString(DateUtil.dateToUtcTime(new Date())));//审批人
			map.put("approver", companyInfo.getApprover());//审批人
			map.put("approvalAdvice", companyInfo.getApprovalAdvice());//审批意见
			map.put("remark", companyInfo.getRemark());//备注
			map.put("validDate", Long.toString(companyInfo.getValidDate()));//有效期
			if(companyInfo.getApprovalAdvice().equals("0")){
				map.put("registerAuthentication", "1");
			}else if(companyInfo.getApprovalAdvice().equals("1")){
				map.put("registerAuthentication", "3");
			}
			
			authManageService.updateAuthManage(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 
	 * @description:增值应用吊销&启用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月29日上午14:02:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/revokeOpenCloud.do")
	public String revokeOpenCloud(HttpServletRequest request, HttpServletResponse response){
		try {
			String comId = request.getParameter("comId");
			String valueAddId = request.getParameter("valueAddId");
			String status = request.getParameter("status");
			Map<String,String> map = new HashMap<String,String>();
			
			map.put("comId", comId);
			map.put("valueAddId", valueAddId);
			map.put("status", status);
			addAppService.updateRevokeOpenCloud(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 
	 * @description:增值应用重新授权
	 * @param:
	 * @author: 马驰
	 * @throws ParseException 
	 * @creatTime:  2014年12月09日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/reAuthorizationCloud.do")
	public String reAuthorizationCloud(HttpServletRequest request, HttpServletResponse response) throws ParseException{
		try {
			String comId = request.getParameter("comId");
			String valueAddId = request.getParameter("valueAddId");
			String addValidDate = request.getParameter("addValidDate");
			String addAppRemark = request.getParameter("addAppRemark");
			Map<String,String> map = new HashMap<String,String>();
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
			
			map.put("comId", comId);
			map.put("valueAddId", valueAddId);
			map.put("addValidDate", DateUtil.dateToUtcTime(sdf.parse(addValidDate)).toString());
			map.put("addAppRemark", addAppRemark);
			addAppService.reAuthorizationCloud(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 
	 * @description:离线授权增值应用查询
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月14日上午14:02:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/getAddApp.do")
	@ResponseBody
	public Map<String, Object> getAddApp(HttpServletRequest request, HttpServletResponse response){
		DynamicSqlParameter param=super.getPageParam(request);
		
	    int total = addAppService.countAddApp(param);
	    PaginationResult<AddApp> list = addAppService.selectAddApp(param);
	    Map<String, Object> result = new HashMap<String, Object>(2); 

	    for(AddApp app:list.getData()){
	    	app.setRegisterAuthentication("2");
	    	app.setStatus("处理中");
	    }
	    
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    
	    return result;
	}
	/**
	 * 
	 * @description:生成账号,密码,鉴权码
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月19日16:47:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/buildAuth.do")
	@ResponseBody
	public Map<String, Object> buildAuth(HttpServletRequest request){
		try {
			String machineSerial = request.getParameter("machineSerial");
			Map<String, Object> result = new HashMap<String, Object>(1); 
			if(""==machineSerial||machineSerial.equals("")){
				result.put("result", "");
			}
			else{
				String resultMD5 = MD5Util.getMd5(machineSerial);
				String account = resultMD5.substring(0, 12);
				String passWord = resultMD5.substring(12, 22);
				String authCode = resultMD5.substring(22, 32);
				String authorizationCode =  MD5Util.getMd5(machineSerial+"hxc123456!@#$%^");
				
				
				result.put("result", account + ":" + passWord + ":" + authCode +":"+authorizationCode);
			}
			
			return result;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}
	/**
	 * cs端提交企业注册信息接口
	 * @param request
	 * @return
	 * @throws Exception 
	 */
	@RequestMapping(value="/remoteRegisterAuth.do")
	@ResponseBody
	public void remoteRegisterAuth(HttpServletRequest request,HttpServletResponse response) throws Exception{
		Map<String, String> returnJson = new HashMap<String, String>(7); 
		String remoteJson = request.getParameter("jsonStr");
		
		String remoteIp = Tools.getRemoteAddress(request);
		
		returnJson = authManageService.remoteRegisterAuth(remoteJson,remoteIp);
		String sendJson = RedisJsonUtil.objectToJson(returnJson);
		String base64SendJson = Base64_URl.base64Encode(sendJson);
		response.getOutputStream().write(base64SendJson.getBytes());
		
		DynamicSqlParameter param = super.getPageParam(request);
		Map<String, Object> equal=new HashMap<String,Object>();
		param.setOrder("ACS");
		param.setPage(1);
		param.setPagesize(5);
		param.setRows(5);
		param.setSort("createTime");
		try{
			CompanyInfo comInfo = null;
			CompanyInfo comInfoFin = null;
			AddApp app = new AddApp();
			String base64Json = Base64_URl.base64Decode(remoteJson);
			if (null != base64Json && !"".equals(base64Json)) {
				comInfo = (CompanyInfo) RedisJsonUtil.jsonToObject(base64Json, new TypeToken<CompanyInfo>() {
				});
			}
			comInfoFin = authManageService.selectPKByMachine(comInfo.getMachineSerial());
			PaginationResult<AddApp> addAppList = addAppService.selectAddApp(param);
			Calendar curr = Calendar.getInstance();
			curr.clear();
			curr.set(2049, 11, 31);
			Date dateOfNextYear=curr.getTime();
			
			if(StringUtil.isNotBlank(comInfoFin.getComId())){
				equal.put("comId", comInfoFin.getComId());
			}
			param.setEqual(equal);
			//查询comid是否存在增值应用表里
			int callBack = addAppService.countAddAppByComId(param);
			if(callBack<=0){
				for(AddApp addApp : addAppList.getData()){
					app.setValueAddId(GeneratorUUID.generateUUID());
					app.setCreateTime(DateUtil.dateToUtcTime(new Date()));
					app.setBizName(addApp.getBizName());
					app.setComId(comInfoFin.getComId());
					app.setCreateBy("接口");
					app.setValidDate(DateUtil.dateToUtcTime(dateOfNextYear));
					app.setRemark(addApp.getRemark());
					app.setRegisterAuthentication("1");
					app.setStatus("1");
					addAppService.insert(app);
				}
			}
		}catch(Exception e){
			e.printStackTrace();
		}
	}
	/**
	 * cs端提交企业注册结果
	 * @param request
	 * @return
	 * @throws Exception 
	 */
	@RequestMapping(value="/remoteSendAuthResult.do")
	@ResponseBody
	public void  remoteSendAuthResult(HttpServletRequest request,HttpServletResponse response) throws Exception{
		Map<String, String> returnJson = new HashMap<String, String>(2); 
		String remoteMachineCode = "";
		try {
			remoteMachineCode = request.getParameter("machineCodeSequence");
		} catch (Exception e) {
			// TODO: handle exception
			returnJson.put("isSuccess", "0");
			returnJson.put("errMsg", "参数不正确");

			String sendJson = RedisJsonUtil.objectToJson(returnJson);
			String base64SendJson = Base64_URl.base64Encode(sendJson);
			response.getOutputStream().write(base64SendJson.getBytes());
		}
		String base64Json = Base64_URl.base64Decode(remoteMachineCode);
		returnJson = authManageService.registerAuthResult(base64Json);
		String sendJson = RedisJsonUtil.objectToJson(returnJson);
		String base64SendJson = Base64_URl.base64Encode(sendJson);
		response.getOutputStream().write(base64SendJson.getBytes());
	}
	/**
	 * cs端获取软件使用有效期
	 * @param request
	 * @return
	 * @throws Exception 
	 */
	@RequestMapping(value="/getValidate.do")
	@ResponseBody
	public void  getValidate(HttpServletRequest request,HttpServletResponse response) throws Exception{
		Map<String, String> returnJson = new HashMap<String, String>(3); 
		String remoteComId = "";
		remoteComId = request.getParameter("signId");
		String base64Json = "";
		try{
			base64Json = Base64_URl.base64Decode(remoteComId);
			returnJson = authManageService.getValidate(base64Json);
			String sendJson = RedisJsonUtil.objectToJson(returnJson);
			String base64SendJson = Base64_URl.base64Encode(sendJson);
			response.getOutputStream().write(base64SendJson.getBytes());
		}catch (Exception e) {
			// TODO: handle exception
			returnJson.put("isSuccess", "0");
			returnJson.put("errMsg", "参数格式错误");
			returnJson.put("validate", "");
	
			String sendJson = RedisJsonUtil.objectToJson(returnJson);
			String base64SendJson = Base64_URl.base64Encode(sendJson);
			response.getOutputStream().write(base64SendJson.getBytes());
		}
	}	
	/**
	 * 
	 * @description:导出注册鉴权列表
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月23日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportAuthManageExcelData.do")
	@ResponseBody
	public void exportAuthManageExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    //查询条件
	    String comName = request.getParameter("requestParam.equal.comName");    //公司名称
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //县
	    String registTimeStart = request.getParameter("requestParam.equal.registTimeStart");  //注册开始时间
	    String registEndTimeEnd = request.getParameter("requestParam.equal.registEndTimeEnd");          //注册结束时间
	    String validTimeStart = request.getParameter("requestParam.equal.validTimeStart");  //有效期开始时间
	    String validEndTimeEnd = request.getParameter("requestParam.equal.validEndTimeEnd");  //有效期开始时间
	    String registerAuthentication = request.getParameter("requestParam.equal.registerAuthentication");          //注册鉴权情况
	    String status = request.getParameter("requestParam.equal.status");          //状态
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
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
		if(StringUtil.isNotBlank(registTimeStart)){
			equal.put("registTimeStart", DateUtil.dateToUtcTime(sdf.parse(registTimeStart)));
		}
		if(StringUtil.isNotBlank(registEndTimeEnd)){
			equal.put("registEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registEndTimeEnd)));
		}
		if(StringUtil.isNotBlank(validTimeStart)){
			equal.put("validTimeStart", DateUtil.dateToUtcTime(sdf.parse(validTimeStart)));
		}
		if(StringUtil.isNotBlank(validEndTimeEnd)){
			equal.put("validEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(validEndTimeEnd)));
		}		
		if(StringUtil.isNotBlank(registerAuthentication)){
			equal.put("registerAuthentication", registerAuthentication);
		}
		if(StringUtil.isNotBlank(status)){
			equal.put("status", status);
		}
		equal.put("nowDate", DateUtil.dateToUtcTime(new Date()));
		
		param.setLike(like);
	    param.setEqual(equal);
	    
		PaginationResult<CompanyInfo> list = authManageService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comName=公司名称&machineSerial=机器序列号&registTime=注册时间&registIp=注册IP&registerAuthentication=注册鉴权情况&status=状态&authorizationCode=授权码&validDate=有效期&approver=审批人&approvalTime=审批时间&remark=备注";
		String excel_result = authManageService.exportData(exportDataHeader, json, this.getUrl(request),"CompanyInfo","注册鉴权");
	    this.printWriter(response, excel_result);
	}
}
