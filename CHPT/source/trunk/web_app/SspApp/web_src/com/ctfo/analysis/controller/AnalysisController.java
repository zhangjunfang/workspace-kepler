package com.ctfo.analysis.controller;

import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.analysis.beans.RepairAnnex;
import com.ctfo.analysis.beans.RepairCharge;
import com.ctfo.analysis.beans.RepairInfo;
import com.ctfo.analysis.beans.RepairMaterials;
import com.ctfo.analysis.beans.RepairProject;
import com.ctfo.analysis.beans.RepairSingle;
import com.ctfo.analysis.service.RepairAnnexService;
import com.ctfo.analysis.service.RepairChargeService;
import com.ctfo.analysis.service.RepairMaterialsService;
import com.ctfo.analysis.service.RepairProjectService;
import com.ctfo.analysis.service.RepairService;
import com.ctfo.analysis.service.RepairSingleService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.basic.controller.BaseController;
import com.ctfo.util.DateUtil;
import com.ctfo.util.StringUtil;

@Controller
@RequestMapping("/analysis/repair")
public class AnalysisController extends BaseController{
	
	@Autowired
	RepairService repairService;
	@Autowired
	RepairSingleService repairSingleService;
	@Autowired
	RepairProjectService repairProjectService;
	@Autowired
	RepairMaterialsService repairMaterialsService;
	@Autowired
	RepairChargeService repairChargeService;
	@Autowired
	RepairAnnexService repairAnnexService;
	
	/**
	 * 
	 * @description:维修单统计
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月30日下午14:21
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
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //地市
	    String settlementTimeStart = request.getParameter("requestParam.equal.settlementTimeStart");  //结算开始时间
	    String settlementTimeEnd = request.getParameter("requestParam.equal.settlementTimeEnd");          //结算结束时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
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
		if(StringUtil.isNotBlank(settlementTimeStart)){
			equal.put("settlementTimeStart", DateUtil.dateToUtcTime(sdf.parse(settlementTimeStart)));
		}
		if(StringUtil.isNotBlank(settlementTimeEnd)){
			equal.put("settlementTimeEnd", DateUtil.dateToUtcTime(sdf.parse(settlementTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
	    int total = repairService.count(param);
	    
	    PaginationResult<RepairInfo> list = repairService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:维修单列表
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月30日下午20:14
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryListForRepairSingle.do")
	@ResponseBody
	public Map<String, Object> queryListForRepairSingle(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
	    Map<String, Object> equal=new HashMap<String,Object>();
		String maintainStatisId = request.getParameter("maintainStatisId");
		String serStationId = request.getParameter("serStationId");
		if(StringUtil.isNotBlank(maintainStatisId)){
			equal.put("maintainStatisId", maintainStatisId);
		}
		if(StringUtil.isNotBlank(serStationId)){
			equal.put("serStationId", serStationId);
		}
		param.setEqual(equal);
		
		int total = repairSingleService.count(param);
		
	    PaginationResult<RepairSingle> list = repairSingleService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:维修单明细
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月3日11:14
	 * @modifyInformation：
	 */
	@RequestMapping(value="/queryrepairSingleDetail.do")
	@ResponseBody
	public RepairSingle queryrepairSingleDetail(HttpServletRequest request) throws Exception{
		String maintain_id = request.getParameter("maintain_id");
		return repairSingleService.selectPK(maintain_id);
	}
	
	/**
	 * 
	 * @description:维修项目
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月4日14:14
	 * @modifyInformation：
	 */
	@RequestMapping(value="/repairProject.do")
	@ResponseBody
	public Map<String, Object> repairProject(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		String maintainId = request.getParameter("maintain_id");
		Map<String, Object> equal=new HashMap<String,Object>();
		
		if(StringUtil.isNotBlank(maintainId)){
			equal.put("maintainId", maintainId);
		}
		
		param.setEqual(equal);
		
	    int total = repairProjectService.count(param);
	    
	    PaginationResult<RepairProject> list = repairProjectService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    
	    double manHourQuantitySum = 0 ;
	    double sumMoneyGoodsSum = 0;
	    
	    for(RepairProject sum : list.getData()){
	    	manHourQuantitySum += sum.getManHourQuantity() ;
	    	sumMoneyGoodsSum +=sum.getSumMoneyGoods();
	    }
	    result.put("ManHourQuantitySum",manHourQuantitySum);
	    result.put("SumMoneyGoodsSum", sumMoneyGoodsSum);  
	    return result;
	}
	
	/**
	 * 
	 * @description:维修用料
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月4日16:14
	 * @modifyInformation：
	 */
	@RequestMapping(value="/repairMaterials.do")
	@ResponseBody
	public Map<String, Object> repairMaterials(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		String maintainId = request.getParameter("maintain_id");
		Map<String, Object> equal=new HashMap<String,Object>();
		
		if(StringUtil.isNotBlank(maintainId)){
			equal.put("maintainId", maintainId);
		}
		
		param.setEqual(equal);
		
	    int total = repairMaterialsService.count(param);
	    
	    PaginationResult<RepairMaterials> list = repairMaterialsService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);
	    
	    double quantitySum = 0;
	    double memberPriceSum = 0;
	    double sumMoneySum = 0;

	    for(RepairMaterials sum : list.getData()){
	    	quantitySum += sum.getQuantity();
	    	memberPriceSum +=sum.getMemberPrice();
	    	sumMoneySum +=sum.getSumMoney();
	    }
	    result.put("QuantitySum",quantitySum);
	    result.put("MemberPriceSum", memberPriceSum);  
	    result.put("SumMoneySum", sumMoneySum);  
	    
	    return result;
	}
	
	/**
	 * 
	 * @description:其他项目收费
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月4日16:14
	 * @modifyInformation：
	 */
	@RequestMapping(value="/repairCharge.do")
	@ResponseBody
	public Map<String, Object> repairCharge(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		String maintainId = request.getParameter("maintain_id");
		Map<String, Object> equal=new HashMap<String,Object>();
		
		if(StringUtil.isNotBlank(maintainId)){
			equal.put("maintainId", maintainId);
		}
		
		param.setEqual(equal);
		
	    int total = repairChargeService.count(param);
	    
	    PaginationResult<RepairCharge> list = repairChargeService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    
	    double sumMoneySum = 0;
	    
	    for(RepairCharge sum : list.getData()){
	    	sumMoneySum += sum.getSumMoney();
	    }
	    result.put("Charge_SumMoneySum",sumMoneySum);
	    
	    return result;
	}
	
	/**
	 * 
	 * @description:附件信息
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月4日18:40
	 * @modifyInformation：
	 */
	@RequestMapping(value="/repairAnnex.do")
	@ResponseBody
	public Map<String, Object> repairAnnex(HttpServletRequest request) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		String maintainId = request.getParameter("maintain_id");
		Map<String, Object> equal=new HashMap<String,Object>();
		
		if(StringUtil.isNotBlank(maintainId)){
			equal.put("maintainId", maintainId);
		}
		
		param.setEqual(equal);
		
	    int total = repairAnnexService.count(param);
	    
	    PaginationResult<RepairAnnex> list = repairAnnexService.selectPagination(param);
	    Map<String, Object> result = new HashMap<String, Object>(2);  
		
	    result.put("Rows",list.getData());
	    result.put("Total", total);   
	    return result;
	}
	
	/**
	 * 
	 * @description:导出维修单统计
	 * @param:
	 * @author: 张恒
	 * @creatTime:  2014年12月24日上午10:33:09
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportRepairStatExcelData.do")
	@ResponseBody
	public void exportRepair(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like=new HashMap<String,String>();
	    Map<String, Object> equal=new HashMap<String,Object>();
	    							
	    //查询条件
	    String comName = request.getParameter("requestParam.like.comName");    //公司名称
	    String setbookName = request.getParameter("requestParam.like.setbookName");    //帐套
	    String corpProvince = request.getParameter("requestParam.equal.corpProvince");  //省
	    String corpCity = request.getParameter("requestParam.equal.corpCity");          //地市
	    String corpCounty = request.getParameter("requestParam.equal.corpCounty");          //地市
	    String settlementTimeStart = request.getParameter("requestParam.equal.settlementTimeStart");  //结算开始时间
	    String settlementTimeEnd = request.getParameter("requestParam.equal.settlementTimeEnd");          //结算结束时间
		
	    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
	    
		if(StringUtil.isNotBlank(comName)){
			like.put("comName", comName);
		}
		if(StringUtil.isNotBlank(setbookName)){
			like.put("setbookName", setbookName);
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
		if(StringUtil.isNotBlank(settlementTimeStart)){
			equal.put("settlementTimeStart", DateUtil.dateToUtcTime(sdf.parse(settlementTimeStart)));
		}
		if(StringUtil.isNotBlank(settlementTimeEnd)){
			equal.put("settlementTimeEnd", DateUtil.dateToUtcTime(sdf.parse(settlementTimeEnd)));
		}
		
		param.setLike(like);
	    param.setEqual(equal);
		
		PaginationResult<RepairInfo> list = repairService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "comCode=公司编码&comName=公司名称&setbookName=帐套名称&province=所在地&startTime=开始时间&endTime=结算时间&repairCount=维修单数量&repairProject=维修单项目数&repairSettlement=维修结算金额(元)&manHourCost=工时费用(元)&accessories=配件数量&accessoriesSettlement=配件结算金额(元)";
		String excel_result = repairService.exportData(exportDataHeader, json, this.getUrl(request),"RepairInfo","维修单统计");
	    this.printWriter(response, excel_result);
	}
}
