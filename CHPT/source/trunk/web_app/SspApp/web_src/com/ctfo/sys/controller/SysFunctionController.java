package com.ctfo.sys.controller;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.local.obj.FunctionTree;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.service.SysFunctionService;

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
 * <tr><td>1.0</td><td>2014年3月29日</td><td>蒋东卿</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2014年3月29日下午6:53:13
 * @since JDK1.6
 */

@Controller
@RequestMapping("/sys/function")
public class SysFunctionController {

	@Autowired
	SysFunctionService sysFunctionService;
	
	/**
	 * 
	 * @description:权限管理-初始化权限树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午2:17:12
	 * @modifyInformation：
	 */
	@RequestMapping(value="/initFunctionTree.do")
	@ResponseBody
	public List<FunctionTree> initFunctionTree(HttpServletRequest request) throws Exception{
		List<FunctionTree> treeList = new ArrayList<FunctionTree>();
		List<SysFunction> functionList = sysFunctionService.select();
		if(null != functionList){
			for(SysFunction sysFunction : functionList){
				if("-1".equals(sysFunction.getFunParentId())){
					ControllerCommon controllerCommon = new ControllerCommon();
					
					FunctionTree functionTree = new FunctionTree();
					functionTree.setNODE_TYPE_DRIVER("");
					functionTree.setNODE_TYPE_LINE("");
					functionTree.setNODE_TYPE_TEAM("");
					functionTree.setNODE_TYPE_VEHICLE("");
					functionTree.setENT_TYPE_NOCORP_AND_NOTEAM("");
					functionTree.setNodeId(sysFunction.getFunId());
					functionTree.setText(sysFunction.getFunName());
					functionTree.setIsexpand("true");
					functionTree.setChildren("");
					functionTree.setIschecked(sysFunction.getIschecked());
					functionTree.setId(sysFunction.getFunId());
					functionTree.setParentId("-1");
					functionTree.setValue("");
					functionTree.setChildrenList(controllerCommon.findSubFunction(functionList, sysFunction.getFunId()));
					
					treeList.add(functionTree);
				}
			}
		}
		return treeList;
	}
	
	/**
	 * 
	 * @description:查询用户权限集合
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午2:29:22
	 * @modifyInformation：
	 */
	@SuppressWarnings({ "rawtypes", "unchecked" })
	@RequestMapping(value="/selectFunListByOpId.do")
	@ResponseBody
	public List<String> selectFunListByOpId(HttpServletRequest request) throws Exception{
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		Map map = new HashMap();
		map.put("opId", opInfo.getOpId());
		return sysFunctionService.selectFunListByOpId(map);
	}
	
}
