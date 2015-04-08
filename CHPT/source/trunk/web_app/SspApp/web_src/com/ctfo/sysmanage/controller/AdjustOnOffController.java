package com.ctfo.sysmanage.controller;

import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.sys.beans.TbOnOff;
import com.ctfo.sysmanage.service.AdjustOnOffService;

@Controller
@RequestMapping("/adjust")
public class AdjustOnOffController extends BaseController{
	@Autowired
	AdjustOnOffService adjustOnOffService;
	
	@RequestMapping("/adjustOnOff.do")
	@ResponseBody
	public String adjustOnOff(HttpServletRequest request) {
		String adjustValue = request.getParameter("adjustValue");
	    String autoMaticStatus = "0";//开启/关闭自动授权
	    String announceAuditStatus = "0";//开启公告审核
	    String permissionAudit = "0";//开启权限管理审核
		String sarray[] = adjustValue.split(",");
		for(int i=0;i<sarray.length;i++){
			if(sarray[i].equals("autoMaticStatus")){
				autoMaticStatus = "1";
			}else if(sarray[i].equals("announceAuditStatus")){
				announceAuditStatus = "1";
			}else if(sarray[i].equals("permissionAudit")){
				permissionAudit = "1";
			}
		}
		Map<String,String> map = new HashMap<String,String>();
		map.put("onOffId", "1");
		map.put("autoMaticStatus", autoMaticStatus);
		map.put("announceAuditStatus", announceAuditStatus);
		map.put("permissionAudit", permissionAudit);
		try {
			adjustOnOffService.updateOnOff(map);
		} catch (Exception e) {
			e.printStackTrace();
			
		}
		return "success";
	}
	
	@RequestMapping("/queryOnOff.do")
	@ResponseBody
	public Map<String, Object> queryOnOff(HttpServletRequest request) {
		Map<String, Object> result = new HashMap<String, Object>(1); 
		String onOffId = "1";
		TbOnOff tbOnOff = new TbOnOff();
		try {
			tbOnOff = adjustOnOffService.selectPK(onOffId);
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
		}
		
	    String autoMaticStatus = tbOnOff.getAutoMaticStatus();//开启/关闭自动授权
	    String announceAuditStatus = tbOnOff.getAnnounceAuditStatus();//开启公告审核
	    String permissionAudit = tbOnOff.getPermissionAudit();//开启权限管理审核
	    
	    result.put("result", autoMaticStatus + ":" + announceAuditStatus + ":" + permissionAudit);
	    
		return result;
	}
}
