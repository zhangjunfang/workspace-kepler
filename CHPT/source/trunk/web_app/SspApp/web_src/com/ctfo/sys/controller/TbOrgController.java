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
import com.ctfo.local.obj.OrgTree;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.service.TbOrgService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.GeneratingCode;
import com.ctfo.util.GeneratorUUID;
import com.ctfo.util.StringUtil;
import com.ctfo.util.Tools;

@Controller
@RequestMapping("/sys/org")
public class TbOrgController extends BaseController {

	@Autowired
	TbOrgService tbOrgService;
	
	//机构树搜索缓存变量
	List<OrgTree> treeListByEntName = new ArrayList<OrgTree>();
	
	/**
	 * 
	 * @description:组织添加
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年3月29日下午8:12:10
	 * @modifyInformation：
	 */
	@RequestMapping(value="/addItem.do")
	public String addItem(@RequestBody TbOrg tbOrg, HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			tbOrg.setEntId(GeneratorUUID.generateUUID());
			tbOrg.setCreateTime(DateUtil.dateToUtcTime(new Date()));
			tbOrg.setCreateBy(opInfo.getOpId());
			
			tbOrgService.insert(tbOrg);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:机构管理-修改
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:13:29
	 * @modifyInformation：
	 */
	@RequestMapping(value="/updateItem.do")
	public String updateItem(@RequestBody TbOrg tbOrg, HttpServletResponse response){
		try {
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			tbOrg.setUpdateTime(DateUtil.dateToUtcTime(new Date()));
			tbOrg.setUpdateBy(opInfo.getOpId());
			tbOrg.setEnableFlag("1");
			
			tbOrgService.update(tbOrg);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:获取机构对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月18日下午2:57:59
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryById.do")
	@ResponseBody
	public TbOrg selectPK(HttpServletRequest request) throws Exception {
		String entId = request.getParameter("orgId");
		return tbOrgService.selectPK(entId);
	}
	
	/**
	 * 
	 * @description:机构管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @throws Exception 
	 * @creatTime:  2014年3月29日下午8:36:58
	 * @modifyInformation：
	 */
	@RequestMapping(value="/deleteItem.do")
	public String updateDelete(HttpServletRequest request, HttpServletResponse response) {
		try {
			String orgId = request.getParameter("orgId");
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			Map<String,String> map = new HashMap<String,String>();
			map.put("entId", orgId);
			map.put("enableFlag", "0");
			map.put("updateBy", opInfo.getOpId());
			map.put("updateTime", String.valueOf(DateUtil.dateToUtcTime(new Date())));
			tbOrgService.updateDelete(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	
	/**
	 * 
	 * @description:机构管理-查询机构列表
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:50:13
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryList.do")
	@ResponseBody
	public Map<String, Object> queryList(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param = super.getPageParam(request);
		
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		
	    Map<String, Object> equal = new HashMap<String,Object>();
	    Map<String, String> like=new HashMap<String,String>();				
	    
	    String entName = request.getParameter("requestParam.like.entName");
	    String comName = request.getParameter("requestParam.like.comName");
	    String entState = request.getParameter("requestParam.equal.entState");          //状态
	    String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");  //创建开始时间
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd");          //注册结束时间
	    
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
	    
	    if(StringUtil.isNotBlank(entName)){
			like.put("entName", entName);
		}
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(entState)){
			equal.put("entState", entState);
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
		
		Map<String, Object> result = new HashMap<String, Object>(2);    
		int total = tbOrgService.count(param);
		PaginationResult<TbOrg> list = tbOrgService.selectPagination(param);
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;   
	}
	/**
	 * 查询组织编码
	 */
	@ResponseBody
	@RequestMapping(value = "/queryAutoCode.do")
	public String queryAutoCode() {
		return GeneratingCode.getEntCode()+"";
	}
	/**
	 * 查询组织列表
	 */
	@ResponseBody
	@RequestMapping(value = "/queryEntList.do")
	public List<TbOrg> queryEntList(HttpServletRequest request) {
		List<TbOrg> entlist = new ArrayList<TbOrg>();
		
		String comId = request.getParameter("comId");
		Map<String,String> map = new HashMap<String,String>();
		map.put("comId", comId);
		
		entlist = tbOrgService.queryEntList(map);
		return entlist;
	}
	/**
	 * 
	 * @description:吊销&启用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月16日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/revokeEditSysEnt.do")
	public String updateRevoke(HttpServletRequest request, HttpServletResponse response){
		try {
			String entIds = request.getParameter("entId");
			String entState = request.getParameter("entState");
			Map<String,String> map = new HashMap<String,String>();
			String entId = Tools.turnString(entIds);
			
			map.put("entId", entId);
			map.put("entState", entState);
			tbOrgService.updateRevoke(map);
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 查询组织名称是否存在
	 */
	@ResponseBody
	@RequestMapping(value = "/isExistEntName.do")
	public String isExistSpOperator(HttpServletRequest request, HttpServletResponse response) {
		int count = 0;
		try {
			String Loginname = request.getParameter("entName");
			
			Map<String,String> map = new HashMap<String,String>();
			map.put("Loginname", Loginname);
			count = tbOrgService.existLoginname(map);
			
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
	 * @description:初始化机构树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日上午9:04:35
	 * @modifyInformation：
	 */
/*	@RequestMapping(value="/initOrgTree.do")
	@ResponseBody
	public List<OrgTree> initOrgTree(HttpServletRequest request) throws Exception{
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		List<OrgTree> treeList = new ArrayList<OrgTree>();
		
		String orgName = request.getParameter("orgNameString");
		Map<String, Object> map = new HashMap<String, Object>();
		map.put("corpCode", opInfo.getCorpCode());
		map.put("entId", opInfo.getEntId());
		
		List<TbOrganization> orgList = tbOrgService.selectOrgTree(map);
		if(null != orgList){
			//获取跟节点
			Set<String> rootNodeList = this.getRootNode(orgList);
			for(String rootNode : rootNodeList){
				for(TbOrganization tbOrganization : orgList){
					if(rootNode.equals(tbOrganization.getParentId())){
						ControllerCommon controllerCommon = new ControllerCommon();
						OrgTree orgTree = new OrgTree();
						orgTree.setNODE_TYPE_DRIVER("");
						orgTree.setNODE_TYPE_LINE("");
						orgTree.setNODE_TYPE_TEAM("");
						orgTree.setNODE_TYPE_VEHICLE("");
						orgTree.setText(tbOrganization.getEntName());
						orgTree.setIsexpand("true");
						orgTree.setENT_TYPE_NOCORP_AND_NOTEAM("");
						orgTree.setId(tbOrganization.getEntId());
						orgTree.setParentId(tbOrganization.getParentId());
						orgTree.setEntType(tbOrganization.getEntType());
						orgTree.setCorpLevel(tbOrganization.getCorpLevel());
						orgTree.setValue(tbOrganization.getCorpCode());
						orgTree.setChildrenList(controllerCommon.findSubOrg(orgList, tbOrganization.getEntId()));
						
						treeList.add(orgTree);
					}
				}
			}
			
		}
		
		//在已有的树中，根据机构名称搜索
		if(StringUtil.isNotBlank(orgName)){
			treeListByEntName = new ArrayList<OrgTree>();;
			treeList = this.getTreeByEntName(treeList, orgName);
		}
		
		return treeList;
	}
	
	//根据机构名称，在结果集中搜索树
	public List<OrgTree> getTreeByEntName(List<OrgTree> treeList, String orgName){
		for(OrgTree tree : treeList){
			if(tree.getText().contains(orgName)){
				treeListByEntName.add(tree);
			}else{
				List<OrgTree> subTree = tree.getChildrenList();
				if(subTree.size()>0){
					//递归
					this.getTreeByEntName(subTree, orgName);
				}
			}
		}
		return treeListByEntName;
	}
	
	//获取树跟节点
	public Set<String> getRootNode(List<TbOrganization> orgList){
		//去重
		Set<String> rootNodeList = new HashSet<String>();
		List<String> idList = new ArrayList<String>(); //ID 集合
		List<String> parentIdList = new ArrayList<String>(); //parentId 集合
		for(TbOrganization org : orgList){
			idList.add(org.getEntId());
			parentIdList.add(org.getParentId());
		}
		
		//如果当前结果集中的parentId 在  id集合中不存在，则为根
		for(String parentId : parentIdList){
			if(!idList.contains(parentId)){
				rootNodeList.add(parentId);
			}
		}
		return rootNodeList;
	}
	*/
	
	/**
	 * 
	 * @description:导出组织列表
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportVehicleTeamExcelData.do")
	@ResponseBody
	public void exportVehicleTeamExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		  Map<String, Object> equal = new HashMap<String,Object>();
		    Map<String, String> like=new HashMap<String,String>();				
		    
		    String entName = request.getParameter("requestParam.like.entName");
		    String comName = request.getParameter("requestParam.like.comName");
		    String entState = request.getParameter("requestParam.equal.entState");          //状态
		    String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");  //创建开始时间
		    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd");          //注册结束时间
		    
		    
		    OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		    equal.put("entId", opInfo.getEntId());
		    
		    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		    
		    
		    if(StringUtil.isNotBlank(entName)){
				like.put("entName", entName);
			}
			if(StringUtil.isNotBlank(comName)){
				like.put("comName", comName);
			}
			if(StringUtil.isNotBlank(entState)){
				equal.put("entState", entState);
			}	
			if(StringUtil.isNotBlank(createTimeStart)){
				equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
			}
			if(StringUtil.isNotBlank(createTimeEnd)){
				equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
			}	
			
			param.setLike(like);
		    param.setEqual(equal);
			
		PaginationResult<TbOrg> list = tbOrgService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "corpCode=组织编码&entName=组织名称&comName=所属公司&orgCname=联系人&orgCphone=联系电话&createBy=创建人&createTime=创建时间&entState=状态&memo=备注";
		String excel_result = tbOrgService.exportData(exportDataHeader, json, this.getUrl(request),"TbOrg","组织列表");
	    this.printWriter(response, excel_result);
	}
}
