package com.ctfo.monitor.controller;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.beans.OnlineUsers;
import com.ctfo.monitor.service.OnlineUsersService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;


@Controller
@RequestMapping("/monitor/online")
public class OnlineUsersController extends BaseController{
	@Autowired
	OnlineUsersService onlineUsersService;
	
	/** redis主连接池 */
	@Autowired
	private JedisPool writeJedisPool;
	/**
	 * 
	 * @description:在线用户状态
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月06日下午15:18
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
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //联系人
	    String landTimeStart = request.getParameter("requestParam.equal.landTimeStart");  //省
	    String landEndTimeEnd = request.getParameter("requestParam.equal.landEndTimeEnd");          //地市
	    String registTimeStart = request.getParameter("requestParam.equal.registTimeStart");  //注册开始时间
	    String registEndTimeEnd = request.getParameter("requestParam.equal.registEndTimeEnd");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(landTimeStart)){
			equal.put("landTimeStart", DateUtil.dateToUtcTime(sdf.parse(landTimeStart)));
		}
		if(StringUtil.isNotBlank(landEndTimeEnd)){
			equal.put("landEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(landEndTimeEnd)));
		}
		if(StringUtil.isNotBlank(registTimeStart)){
			equal.put("registTimeStart", DateUtil.dateToUtcTime(sdf.parse(registTimeStart)));
		}
		if(StringUtil.isNotBlank(registEndTimeEnd)){
			equal.put("registEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registEndTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = onlineUsersService.count(param);
	    PaginationResult<OnlineUsers> resultObject = onlineUsersService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  

	    long nowUtcTime = DateUtil.dateToUtcTime(new Date());
	    
	    for (OnlineUsers ou : resultObject.getData()) {
	    	ou.setOnlineTime(DateUtil.getHMSColonFormateOfTimePeriodBySeconds((nowUtcTime-ou.getLoadTime())/1000));
		}
	    
	    result.put("Rows",resultObject.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:查询在线用户数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月25日
	 * @modifyInformation：
	 */
	@RequestMapping(value="/countOnline.do")
	@ResponseBody
	public Map<String, Object> countOnline(HttpServletRequest request) throws Exception{
		
		Jedis client = writeJedisPool.getResource();
//		client.select(2);
		Map<String,String> map = client.hgetAll("LOGIN");
		int total = map.size();
	    Map<String, Object> result = new HashMap<String, Object>();  
	    result.put("onlineCount", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:导出在线用户状态
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportOnlineUsersManageExcelData.do")
	@ResponseBody
	public void exportOnlineUsersManageExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //联系人
	    String landTimeStart = request.getParameter("requestParam.equal.landTimeStart");  //省
	    String landEndTimeEnd = request.getParameter("requestParam.equal.landEndTimeEnd");          //地市
	    String registTimeStart = request.getParameter("requestParam.equal.registTimeStart");  //注册开始时间
	    String registEndTimeEnd = request.getParameter("requestParam.equal.registEndTimeEnd");  //注册开始时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
		if(StringUtil.isNotBlank(landTimeStart)){
			equal.put("landTimeStart", DateUtil.dateToUtcTime(sdf.parse(landTimeStart)));
		}
		if(StringUtil.isNotBlank(landEndTimeEnd)){
			equal.put("landEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(landEndTimeEnd)));
		}
		if(StringUtil.isNotBlank(registTimeStart)){
			equal.put("registTimeStart", DateUtil.dateToUtcTime(sdf.parse(registTimeStart)));
		}
		if(StringUtil.isNotBlank(registEndTimeEnd)){
			equal.put("registEndTimeEnd", DateUtil.dateToUtcTime(sdf.parse(registEndTimeEnd)));
		}
		param.setLike(like);
	    param.setEqual(equal);
		PaginationResult<OnlineUsers> result = onlineUsersService.selectPagination(param);
		String json = jsonFormatToGrid(result);
		String exportDataHeader = "clientAccount=账号&realName=姓名&comName=所属公司&clientAccountOrgid=所属组织&roleName=角色&setbookId=所在帐套&regTime=注册时间&loadTime=登陆时间&onlineTime=在线时长&loadIdAddr=IP&onlineStatus=在线状态";
		String excel_result = onlineUsersService.exportData(exportDataHeader, json, this.getUrl(request),"OnlineUsers","在线用户状态");
	    this.printWriter(response, excel_result);
	}
}
