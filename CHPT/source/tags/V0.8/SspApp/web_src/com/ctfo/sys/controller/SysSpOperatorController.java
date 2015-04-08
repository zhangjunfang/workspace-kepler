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

import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoAppExceptionDefinition;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.service.SysSpOperatorService;
import com.ctfo.sys.service.SysSpRoleService;
import com.ctfo.sys.service.TbOrgService;
import com.ctfo.travel.basic.controller.BaseController;
import com.ctfo.util.DateUtil;
import com.ctfo.util.GeneratingCode;
import com.ctfo.util.GeneratorUUID;
import com.ctfo.util.StringUtil;
import com.ctfo.util.Tools;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： FrameworkApp
 * <br>
 * 功能：
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交慧联信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2014年3月25日</td><td>蒋东卿</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2014年3月25日下午4:22:04
 * @since JDK1.6
 */

@Controller
@RequestMapping("/sys/spOperator")
public class SysSpOperatorController extends BaseController{
	
	@Autowired
	SysSpOperatorService sysSpOperatorService;
	
	@Autowired
    TbOrgService tbOrgService;
	
	@Autowired
	SysSpRoleService sysSpRoleService; 
	
	/**
	 * 
	 * @description:获取在线用户信息
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月3日上午10:18:44
	 * @modifyInformation：
	 */
	@RequestMapping("/online.do")
	@ResponseBody
	public OperatorInfo findOperatorOnline(){
		OperatorInfo info = OperatorInfo.getOperatorInfo();
		
		TbOrg org = tbOrgService.selectPK(info.getEntId());
		info.setEntId(org.getEntId());
		info.setEntName(org.getEntName());
		info.setEntState(org.getEntState());
		info.setComId(org.getComId());
		
		return info;
	}
	
	/**
	 * 
	 * @description:用户管理-添加
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日下午4:21:58
	 * @modifyInformation：
	 */
	@RequestMapping(value="/addItem.do")
	public String addItem(@RequestBody SysSpOperator sysSpOperator,HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			sysSpOperator.setOpId(GeneratorUUID.generateUUID());
			sysSpOperator.setEnableFlag("1");
			sysSpOperator.setOpSuper("1");
			sysSpOperator.setCreateTime(DateUtil.dateToUtcTime(new Date()));
			sysSpOperator.setCreateBy(opInfo.getOpId());
			
			sysSpOperatorService.insert(sysSpOperator);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 查询公司编码
	 */
	@ResponseBody
	@RequestMapping(value = "/queryAutoCodeOfOp.do")
	public String queryAutoCode() {
		return GeneratingCode.getOpCode()+"";
	}	
	/**
	 * 
	 * @description:用户管理-修改
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日下午5:52:33
	 * @modifyInformation：
	 */
	@RequestMapping(value="/updateItem.do")
	public String updateItem(@RequestBody SysSpOperator sysSpOperator, HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			sysSpOperator.setUpdateTime(DateUtil.dateToUtcTime(new Date()));
			sysSpOperator.setUpdateBy(opInfo.getOpId());
			
			sysSpOperatorService.update(sysSpOperator);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:用户管理-吊销&启用
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午10:27:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/revokeOpen.do")
	public String updateRevokeOpen(HttpServletRequest request, HttpServletResponse response){
		try {
			String opIds = request.getParameter("opId");
			String opStatus = request.getParameter("status");
			Map<String,String> map = new HashMap<String,String>();
			String opId = Tools.turnString(opIds);
			map.put("opId", opId);
			map.put("opStatus", opStatus);
			sysSpOperatorService.updateRevokeOpen(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:用户管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午10:32:08
	 * @modifyInformation：
	 */
	@RequestMapping(value="/deleteItem.do")
	public String updateDelete(HttpServletRequest request, HttpServletResponse response){
		try {
			String opIds = request.getParameter("opId");
			Map<String,String> map = new HashMap<String,String>();
			String opId = Tools.turnString(opIds);
			
			map.put("opId", opId);
			sysSpOperatorService.updateDelete(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:用户管理-修改密码
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月28日下午2:15:13
	 * @modifyInformation：
	 */
	@RequestMapping(value="/updatePass.do")
	public String updatePass(HttpServletRequest request, HttpServletResponse response){
		try {
			String opId = request.getParameter("spOperator.opId");
			String opPass = request.getParameter("spOperator.opPass");
			
			Map<String,String> map = new HashMap<String,String>();
			map.put("opId", opId);
			map.put("opPass", opPass);
			sysSpOperatorService.updatePass(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 查询人员列表
	 */
	@ResponseBody
	@RequestMapping(value = "/queryOperatorList.do")
	public List<SysSpOperator> queryOperatorList() {
		List<SysSpOperator> operatorlist = new ArrayList<SysSpOperator>();
		operatorlist = sysSpOperatorService.queryOperatorList();
		return operatorlist;
	}
	/**
	 * 
	 * @description:用户管理-用户登录名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日下午3:26:53
	 * @modifyInformation：
	 */
	@RequestMapping(value="/isExistSpOperator.do")
	public String isExistSpOperator(HttpServletRequest request, HttpServletResponse response){
		int count = 0;
		try {
			String opLoginname = request.getParameter("opLoginname");
			String opId = request.getParameter("noId");
			
			Map<String,String> map = new HashMap<String,String>();
			map.put("opLoginname", opLoginname);
			count = sysSpOperatorService.existOpLoginname(map);
			
			//如果是更新操作，再判断一次，覆盖之前的值
			if(!StringUtil.isBlank(opId)) {
				String opLoginnameUpdate = sysSpOperatorService.selectPK(opId).getOpLoginname();
				if(opLoginnameUpdate.equals(opLoginname)){
					count = 0;
				}
			}
			
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
	 * @description:用户管理-根据ID获取用户对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月28日下午1:45:27
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryById.do")
	@ResponseBody
	public SysSpOperator selectPK(HttpServletRequest request) throws Exception {
		String opId = request.getParameter("opId");
		SysSpOperator op = sysSpOperatorService.selectPK(opId);
		
		//获取已关联的角色
		Map<String, String> equ=new HashMap<String,String>();
		equ.put("opId", opId);
		List<String> roleIds = sysSpRoleService.selectRoleByEntId(equ);
		if(null==roleIds || roleIds.size()==0){
			op.setRoleId("");
		}else{
			StringBuffer sb = new StringBuffer();
			for(int i = 0; i < roleIds.size(); i++){
				sb.append(roleIds.get(i));
				if(i!=(roleIds.size()-1)){
					sb.append(";");
				}
			}
			op.setRoleId(sb.toString());
		}
		
		return op;
	}
	
	/**
	 * 
	 * @description:用户管理-查询用户列表
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日下午6:09:13
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
	    String opLoginname=request.getParameter("requestParam.like.opLoginname"); //登录名称
	    String opName=request.getParameter("requestParam.like.opName"); 
	    String roleId=request.getParameter("requestParam.equal.roleId");          //关联角色
	    String comId=request.getParameter("requestParam.equal.comId");          //关联公司
	    String entId=request.getParameter("requestParam.equal.entId");          //关联組織
	    String createTimeStart=request.getParameter("requestParam.equal.createTimeStart");          
	    String createTimeEnd=request.getParameter("requestParam.equal.createTimeEnd");
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
	    
		if(StringUtil.isNotBlank(opLoginname)){
			like.put("opLoginname", opLoginname);
		}
		if(StringUtil.isNotBlank(opName)){
			like.put("opName", opName);
		}
		if(StringUtil.isNotBlank(roleId)){
			equal.put("roleId", roleId);
		}
		if(StringUtil.isNotBlank(comId)){
			equal.put("comId", comId);
		}
		if(StringUtil.isNotBlank(entId)){
			equal.put("entId", entId);
		}	
		if(StringUtil.isNotBlank(createTimeStart)){
			equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
		}
		if(StringUtil.isNotBlank(createTimeEnd)){
			equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
		}
		if(opInfo.getIsOperator().equals("0")){
			equal.put("isOperator", "0");
		}
	    param.setLike(like);
	    param.setEqual(equal);
		
		Map<String, Object> result = new HashMap<String, Object>(2);    
		int total = sysSpOperatorService.count(param);
		PaginationResult<SysSpOperator> list = sysSpOperatorService.selectPagination(param);
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;   
	}
	
	/**
	 * 
	 * @description:导出用户列表
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportUserManageExcelData.do")
	@ResponseBody
	public void exportUserManageExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String opLoginname=request.getParameter("requestParam.like.opLoginname"); //登录名称
	    String opName=request.getParameter("requestParam.like.opName"); 
	    String roleId=request.getParameter("requestParam.equal.roleId");          //关联角色
	    String comId=request.getParameter("requestParam.equal.comId");          //关联公司
	    String entId=request.getParameter("requestParam.equal.entId");          //关联組織
	    String createTimeStart=request.getParameter("requestParam.equal.createTimeStart");          
	    String createTimeEnd=request.getParameter("requestParam.equal.createTimeEnd");
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
	    OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
	    equal.put("entId", opInfo.getEntId());
	    
		if(StringUtil.isNotBlank(opLoginname)){
			like.put("opLoginname", opLoginname);
		}
		if(StringUtil.isNotBlank(opName)){
			like.put("opName", opName);
		}
		if(StringUtil.isNotBlank(roleId)){
			equal.put("roleId", roleId);
		}
		if(StringUtil.isNotBlank(comId)){
			equal.put("comId", comId);
		}
		if(StringUtil.isNotBlank(entId)){
			equal.put("entId", entId);
		}	
		if(StringUtil.isNotBlank(createTimeStart)){
			equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
		}
		if(StringUtil.isNotBlank(createTimeEnd)){
			equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
		}
	    param.setLike(like);
	    param.setEqual(equal);
		PaginationResult<SysSpOperator> list = sysSpOperatorService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "opCode=人员编码&opLoginname=账号&opName=姓名&comName=所属公司&entName=所属组织&roleName=所属角色&createTime=创建时间&createBy=创建人&opStatus=状态&opMem=备注";
		String excel_result = sysSpOperatorService.exportData(exportDataHeader, json, this.getUrl(request),"SysSpOperator","用户列表");
	    this.printWriter(response, excel_result);
	}
	
}
