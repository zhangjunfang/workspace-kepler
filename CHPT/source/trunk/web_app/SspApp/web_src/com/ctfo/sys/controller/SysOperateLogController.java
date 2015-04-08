package com.ctfo.sys.controller;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysOperateLog;
import com.ctfo.sys.service.SysOperateLogService;
import com.ctfo.util.StringUtil;

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
 * 操作记录
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2014年12月15日</td><td>gemin</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author gemin
 * @date 2014年12月15日上午9:50:55
 * @since JDK1.6
 */

@Controller
@RequestMapping("/sys/opLog")
public class SysOperateLogController extends BaseController{

	@Autowired
	SysOperateLogService sysOperateLogService;
	
	/**
	 * 
	 * @description:操作记录查询
	 * @param:
	 * @author: gemin
	 * @creatTime:  2014年12月15日上午9:50:55
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryOpLogList.do")
	@ResponseBody
	public Map<String, Object> queryList(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
		Map<String, Object> equal=new HashMap<String,Object>();
	    			
		
	    String funId = request.getParameter("requestParam.equal.funId");
	    String opType = request.getParameter("requestParam.equal.opType");
	    String opName = request.getParameter("requestParam.like.opName");
	    String content = request.getParameter("requestParam.like.content");
	    String startTime = request.getParameter("requestParam.equal.startTime");
	    String endTime = request.getParameter("requestParam.equal.endTime");
	    String opId = request.getParameter("requestParam.equal.opId");
	    String entId = request.getParameter("requestParam.equal.entId");
	    String comId = request.getParameter("requestParam.equal.comId");
	    String sortName = request.getParameter("requestParam.equal.sortname");          //排序字段
	    String sortOrder = request.getParameter("requestParam.equal.sortorder");        //排序方式
	    
	    equal.put("funId", funId);
	    equal.put("opType", opType);
	    equal.put("startTime", startTime);
	    equal.put("endTime", endTime);
	    equal.put("opId", opId);
	    equal.put("entId", entId);
	    equal.put("comId", comId);
	    
		if(StringUtil.isNotBlank(opName)){
			like.put("opName", opName);
		}
		if(StringUtil.isNotBlank(content)){
			like.put("content", content);
		}
		if(StringUtil.isNotBlank(funId)){
			equal.put("funId", funId);
		}
		if(StringUtil.isNotBlank(opType)){
			equal.put("opType", opType);
		}
		if(StringUtil.isNotBlank(startTime)){
			equal.put("startTime", startTime);
		}
		if(StringUtil.isNotBlank(endTime)){
			equal.put("endTime", endTime);
		}
		if(StringUtil.isNotBlank(sortName)){
			param.setSort(sortName);
		}
		if(StringUtil.isNotBlank(sortOrder)){
			param.setOrder(sortOrder);
		}
		
	    param.setLike(like);
	    param.setEqual(equal);
	    
	    Map<String, Object> result = new HashMap<String, Object>(2);    
		int total = sysOperateLogService.count(param);
		PaginationResult<SysOperateLog> list = sysOperateLogService.selectPagination(param);
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;   
	}
	
	/**
	 * 
	 * @description:查询所有角色对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月4日上午10:57:18
	 * @modifyInformation：
	 */
	/*@RequestMapping(value="/query.do")
	@ResponseBody
	public List<SysOperateLog> query(HttpServletRequest request){
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, Object> equal =new HashMap<String, Object>();
	    String entId=request.getParameter("entId");
		if(StringUtil.isNotBlank(entId)){
			equal.put("entId", entId);
		}
		
	    param.setEqual(equal);
	    
		return sysOperateLogService.select(param);
	}*/
	
	@RequestMapping(value="/selectQuery.do")
	@ResponseBody
	public List<String> selectQuery(HttpServletRequest request){
		
	    String roleId=request.getParameter("roleId");
	    List<String> ls = sysOperateLogService.selectQuery(roleId);
		return ls;
	}
	
}
