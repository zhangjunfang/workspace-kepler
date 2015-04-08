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
import com.ctfo.local.obj.FunctionTree;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.beans.SysSpRole;
import com.ctfo.sys.service.SysFunctionService;
import com.ctfo.sys.service.SysSpRoleService;
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
 * <tr><td>1.0</td><td>2014年3月31日</td><td>蒋东卿</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2014年3月31日上午9:41:26
 * @since JDK1.6
 */

@Controller
@RequestMapping("/sys/spRole")
public class SysSpRoleController extends BaseController{

	@Autowired
	SysSpRoleService sysSpRoleService;
	
	@Autowired
	SysFunctionService sysFunctionService;
	
	/**
	 * 
	 * @description:角色管理-查询角色列表
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:06:28
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryList.do")
	@ResponseBody
	public Map<String, Object> queryList(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
		Map<String, Object> equal=new HashMap<String,Object>();
	    											
	    String roleName=request.getParameter("requestParam.like.roleName");
	    String roleStatus = request.getParameter("requestParam.equal.roleStatus");
	    String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd");
	    
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
	    equal.put("entId", opInfo.getEntId());
	    
		if(StringUtil.isNotBlank(roleName)){
			like.put("roleName", roleName);
		}
		if(StringUtil.isNotBlank(roleStatus)){
			equal.put("roleStatus",roleStatus);
		}
		if(StringUtil.isNotBlank(createTimeStart)){
			equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
		}
		if(StringUtil.isNotBlank(createTimeEnd)){
			equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
		}	
		
	    param.setLike(like);
	    param.setEqual(equal);
	    
	    Map<String, Object> result = new HashMap<String, Object>(2);    
		int total = sysSpRoleService.count(param);
		PaginationResult<SysSpRole> list = sysSpRoleService.selectPagination(param);
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;   
	}
	/**
	 * 查询角色编码
	 */
	@ResponseBody
	@RequestMapping(value = "/queryAutoCodeOfRole.do")
	public String queryAutoCode() {
		return GeneratingCode.getRoleCode()+"";
	}	
	/**
	 * 
	 * @description:查询所有角色对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月4日上午10:57:18
	 * @modifyInformation：
	 */
	@RequestMapping(value="/query.do")
	@ResponseBody
	public List<SysSpRole> query(HttpServletRequest request){
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, Object> equal =new HashMap<String, Object>();
	    String entId=request.getParameter("entId");
		if(StringUtil.isNotBlank(entId)){
			equal.put("entId", entId);
		}
		
	    param.setEqual(equal);
	    
		return sysSpRoleService.select(param);
	}
	/**
	 * 查询角色列表
	 */
	@ResponseBody
	@RequestMapping(value = "/queryRoleList.do")
	public List<SysSpRole> queryRoleList() {
		List<SysSpRole> rolelist = new ArrayList<SysSpRole>();
		rolelist = sysSpRoleService.queryRoleList();
		return rolelist;
	}
	/**
	 * 
	 * @description:角色管理-角色名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:18:51
	 * @modifyInformation：
	 */
	@RequestMapping(value="/isExistRole.do")
	public String isExistRole(HttpServletRequest request, HttpServletResponse response){
		boolean exist = false;
		try {
			String roleName = request.getParameter("requestParam.equal.roleName");
			//String entId = request.getParameter("requestParam.noId");
			
			Map<String,String> map = new HashMap<String,String>();
			map.put("roleName", roleName);
			exist = sysSpRoleService.isExistRoleName(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		
		if (!exist) {
			return this.returnInfoForJS(response, false, CtfoAppExceptionDefinition.ROLE_A_HAVENAME);
		} else {
			return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
		}
	}
	
	/**
	 * 
	 * @description:角色管理-删除
	 * @param:
	 * @author: Administrator
	 * @creatTime:  2014年4月24日下午4:06:52
	 * @modifyInformation：
	 */
	@RequestMapping(value="/deleteItem.do")
	public String updateDelete(HttpServletRequest request, HttpServletResponse response) { 
		try {
			String roleId = request.getParameter("roleId");
			Map<String,String> map = new HashMap<String,String>();
			map.put("roleId", roleId);
			sysSpRoleService.updateDelete(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_REMOVE);
	}
	
	/**
	 * 
	 * @description:角色管理-根据ID获取角色对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午1:21:56
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryById.do")
	@ResponseBody
	public SysSpRole selectPK(HttpServletRequest request) throws Exception {
		String roleId = request.getParameter("roleId");
		return sysSpRoleService.selectPK(roleId);
	}
	
	/**
	 * 
	 * @description:角色管理-添加，同时保存与权限的关系
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:49:50
	 * @modifyInformation：
	 */
	@RequestMapping(value="/addItem.do")
	public String addItem(@RequestBody SysSpRole sysSpRole,HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			sysSpRole.setRoleId(GeneratorUUID.generateUUID());
			sysSpRole.setCreateTime(DateUtil.dateToUtcTime(new Date()));
			sysSpRole.setCreateBy(opInfo.getOpId());
			
			sysSpRoleService.insert(sysSpRole);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:角色管理-更新，同时更新与权限的关系
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午3:31:16
	 * @modifyInformation：
	 */
	@RequestMapping(value="/updateItem.do")
	public String updateItem(@RequestBody SysSpRole sysSpRole,HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			sysSpRole.setUpdateTime(DateUtil.dateToUtcTime(new Date()));
			SysSpRole spRole = sysSpRoleService.selectPK(sysSpRole.getRoleId());
			sysSpRole.setRoleName(spRole.getRoleName());
//			sysSpRole.setEnableFlag(spRole.getEnableFlag());
			sysSpRole.setCreateTime(spRole.getCreateTime());
			sysSpRole.setUpdateBy(opInfo.getOpId());
			
			sysSpRoleService.update(sysSpRole);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:查看角色已分配的权限树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日上午9:46:02
	 * @modifyInformation：
	 */
	@RequestMapping(value="/selectFunTreeByRoleId.do")
	@ResponseBody
	public List<SysSpRole> selectFunTreeByRoleId(HttpServletRequest request) throws Exception{
		String roleId = request.getParameter("roleId");
		Map<String,String> map = new HashMap<String,String>();
		map.put("roleId", roleId);
		
		List<SysFunction> functionList = sysFunctionService.selectByRoleId(map);
		List<FunctionTree> treeList = new ArrayList<FunctionTree>();
		if(null != functionList){
			for(SysFunction sysFunction : functionList){
				if("-1".equals(sysFunction.getFunParentId())){
					ControllerCommon controllerCommon = new ControllerCommon();
					
					FunctionTree functionTree = new FunctionTree();
					functionTree.setChildren("");
					functionTree.setChildrenList(controllerCommon.findSubFunction(functionList, sysFunction.getFunId()));
					functionTree.setENT_TYPE_NOCORP_AND_NOTEAM("");
					functionTree.setId("");
					functionTree.setIschecked(sysFunction.getIschecked());
					functionTree.setIsexpand("true");
					functionTree.setNODE_TYPE_DRIVER("");
					functionTree.setNODE_TYPE_LINE("");
					functionTree.setNODE_TYPE_TEAM("");
					functionTree.setNODE_TYPE_VEHICLE("");
					functionTree.setNodeId(sysFunction.getFunId());
					functionTree.setParentId("-1");
					functionTree.setText(sysFunction.getFunName());
					functionTree.setValue("");
					
					treeList.add(functionTree);
				}
			}
		}
		
		List<Map<String, Object>> roleFunTree = new ArrayList<Map<String, Object>>();
		Map<String, Object> rootMap = new HashMap<String, Object>();
		rootMap.put("id", "");
		rootMap.put("text", "-1");
		rootMap.put("rootName", "企业应用系统");
		rootMap.put("childrenList", treeList);
		roleFunTree.add(rootMap);
		
		List<SysSpRole> roleList = new ArrayList<SysSpRole>();
		SysSpRole sysSpRole = sysSpRoleService.selectPK(roleId);
		sysSpRole.setRoleFunTree(roleFunTree);
		roleList.add(sysSpRole);
		
		return roleList;
	}
	
	/**
	 * 
	 * @description:角色编辑时，初始化权限树同时选中已关联的
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午2:05:11
	 * @modifyInformation：
	 */
	@RequestMapping(value="/selectFunTreeRoleEdit.do")
	@ResponseBody
	public List<FunctionTree> selectFunTreeRoleEdit(HttpServletRequest request) throws Exception{
		String roleId = request.getParameter("roleId");
		Map<String,String> map = new HashMap<String,String>();
		map.put("roleId", roleId);
		
		List<SysFunction> functionList = sysFunctionService.selectFunTreeRoleEdit(map);
		List<FunctionTree> treeList = new ArrayList<FunctionTree>();
		if(null != functionList){
			for(SysFunction sysFunction : functionList){
				if("-1".equals(sysFunction.getFunParentId())){
					ControllerCommon controllerCommon = new ControllerCommon();
					
					FunctionTree functionTree = new FunctionTree();
					functionTree.setChildren("");
					functionTree.setChildrenList(controllerCommon.findSubFunction(functionList, sysFunction.getFunId()));
					functionTree.setENT_TYPE_NOCORP_AND_NOTEAM("");
					functionTree.setId("");
					functionTree.setIschecked(sysFunction.getIschecked());
					functionTree.setIsexpand("true");
					functionTree.setNODE_TYPE_DRIVER("");
					functionTree.setNODE_TYPE_LINE("");
					functionTree.setNODE_TYPE_TEAM("");
					functionTree.setNODE_TYPE_VEHICLE("");
					functionTree.setNodeId(sysFunction.getFunId());
					functionTree.setParentId("-1");
					functionTree.setText(sysFunction.getFunName());
					functionTree.setValue("");
					
					treeList.add(functionTree);
				}
			}
		}
		return treeList;
	}
	/**
	 * 
	 * @description:吊销&启用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月22日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/revokeEditOpen.do")
	public String updateRevoke(HttpServletRequest request, HttpServletResponse response){
		try {
			String roleIds = request.getParameter("roleId");
			String roleStatus = request.getParameter("roleStatus");
			Map<String,String> map = new HashMap<String,String>();
			String roleId = Tools.turnString(roleIds);
			
			map.put("roleId", roleId);
			map.put("roleStatus", roleStatus);
			sysSpRoleService.updateRevoke(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:导出角色列表
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportRoleManageExcelData.do")
	@ResponseBody
	public void exportRoleManageExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		Map<String, String> like=new HashMap<String,String>();
		Map<String, Object> equal=new HashMap<String,Object>();
	    											
	    String roleName=request.getParameter("requestParam.like.roleName");
	    String roleStatus = request.getParameter("requestParam.equal.roleStatus");
	    String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd");
	    
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
	    equal.put("entId", opInfo.getEntId());
	    
		if(StringUtil.isNotBlank(roleName)){
			like.put("roleName", roleName);
		}
		if(StringUtil.isNotBlank(roleStatus)){
			equal.put("roleStatus",roleStatus);
		}
		if(StringUtil.isNotBlank(createTimeStart)){
			equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
		}
		if(StringUtil.isNotBlank(createTimeEnd)){
			equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
		}	
		
	    param.setLike(like);
	    param.setEqual(equal);
		PaginationResult<SysSpRole> list = sysSpRoleService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "roleCode=角色编码&roleName=角色名称&createBy=创建人&createTime=创建时间&roleStatus=状态";
		String excel_result = sysSpRoleService.exportData(exportDataHeader, json, this.getUrl(request),"SysSpRole","角色列表");
	    this.printWriter(response, excel_result);
	}
	
}
