package com.ctfo.operation.controller;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.TbCloudBackUp;
import com.ctfo.operation.service.CloudBackUpService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;

@Controller
@RequestMapping("/operation/cloud")
public class CloudBackUpController extends BaseController {
	
	@Autowired
	CloudBackUpService cloudBackUpService;
		
	@RequestMapping("/findCloudBackUpList.do")
	@ResponseBody
	public Map<String, Object> findList(HttpServletRequest request) throws ParseException{
		
		DynamicSqlParameter param=super.getPageParam(request);//获取分页的参数
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		String comName = request.getParameter("requestParam.equal.comName");//公司名称
		String setbookName = request.getParameter("requestParam.equal.setbookName");//公司的帐套名称
		String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");  //备份启始时间
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd"); //备份结束时间
	    
	    if(StringUtil.isNotBlank(comName)){
	    	like.put("comName", comName);
	    }
	    if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
	    if(StringUtil.isNotBlank(createTimeStart)){
	    	equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
	    	
	    }
	    if(StringUtil.isNotBlank(createTimeEnd)){
	    	equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
	    	
		}	    
	    
	    param.setLike(like);
	    param.setEqual(equal);
	    
	    int total = cloudBackUpService.count(param);
	    PaginationResult<TbCloudBackUp> list = cloudBackUpService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);    		
	    
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	/**
	 * 根据主键查看实体bean
	 */
	@RequestMapping("/findById.do")
	@ResponseBody
	public TbCloudBackUp selectByPrimaryKey(HttpServletRequest request){
		String cloudId = request.getParameter("cloudId");
		TbCloudBackUp cloudBackUp = new TbCloudBackUp();
		cloudBackUp = cloudBackUpService.selectByPrimaryKey(cloudId);
		return cloudBackUp;
	}
	/**
	 * 
	 * @param cloudId 云备份ID
	 * @return 删除成功失败
	 */
	@RequestMapping(value = "deleteCloudyBackupById.do")
	@ResponseBody
	public String deleteCloudyBackupById(String cloudId,HttpServletResponse response){
		//System.out.println("删除云备份！！！" + cloudId);
		try {
			
			cloudBackUpService.deleteCloudyBackupById(cloudId);
			
			
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage()
					.replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}

	/**
	 * 
	 * @description:导出云备份
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportCloudManageExcelData.do")
	@ResponseBody
	public void exportCloudManageExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		String comName = request.getParameter("requestParam.equal.comName");//公司名称
		String setbookName = request.getParameter("requestParam.equal.setbookName");//公司的帐套名称
		String createTimeStart = request.getParameter("requestParam.equal.createTimeStart");  //备份启始时间
	    String createTimeEnd = request.getParameter("requestParam.equal.createTimeEnd"); //备份结束时间
	    
	    if(StringUtil.isNotBlank(comName)){
	    	like.put("comName", comName);
	    }
	    if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
		}
	    if(StringUtil.isNotBlank(createTimeStart)){
	    	equal.put("createTimeStart", DateUtil.dateToUtcTime(sdf.parse(createTimeStart)));
	    	
	    }
	    if(StringUtil.isNotBlank(createTimeEnd)){
	    	equal.put("createTimeEnd", DateUtil.dateToUtcTime(sdf.parse(createTimeEnd)));
	    	
		}	    
	    param.setLike(like);
	    param.setEqual(equal);
	    
		PaginationResult<TbCloudBackUp> list = cloudBackUpService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comCode=公司编码&comName=公司名称&setbookName=帐套名称&createTime=云备份时间&cloudSize=云空间大小(G)&cloudValidTime=云空间有效期&usedSpace=已用空间(G)&remainSpace=可用空间(G)&fileNums=文件数目";
		String excel_result = cloudBackUpService.exportData(exportDataHeader, json, this.getUrl(request),"TbCloudBackUp","云备份数据");
	    this.printWriter(response, excel_result);
	}
}
