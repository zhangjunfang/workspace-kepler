package com.ctfo.version.controller;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.util.StringUtil;
import com.ctfo.version.beans.UpExe;
import com.ctfo.version.beans.Version;
import com.ctfo.version.service.VersionService;


@Controller
@RequestMapping("/version")
public class VersionController extends BaseController{

	@Autowired
	VersionService versionService;
	
	/**
	 * 
	 * @description:获取所需数据库版本
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月9日下午3:09:58
	 * @modifyInformation：
	 */
	@RequestMapping("/findVersionDb.do")
	@ResponseBody
	public List<String> findVersionDb(HttpServletRequest request){
		List<String> stList = new ArrayList<String>();
		Map<String, Object> equal=new HashMap<String,Object>();
		String currSoftVersion = request.getParameter("currSoftVersion");
		String currDbVersion = request.getParameter("currDbVersion");
		if(StringUtil.isNotBlank(currSoftVersion)){
			equal.put("currSoftVersion", currSoftVersion);
		}
		if(StringUtil.isNotBlank(currDbVersion)){
			equal.put("currDbVersion", currDbVersion);
		}
		
		List<String> li = versionService.findVersionDb(equal);
		if(null != li && li.size()>0){
			for(String str : li){
				if(null != str && !"".equals(li)){
					stList.add(str);
				}
			}
		}
		
		return stList;
	}  
	
	/**
	 * 获取最新版本信息
	 * 
	 * @param  RequestType   (1-慧修车服务端 2-慧修车客户端)
	 * @return String
	 * @throws MalformedURLException 
	 */
	@RequestMapping("/findVersionNew.do")
	@ResponseBody
	public UpExe findVersionNew(HttpServletRequest request) throws MalformedURLException {
		Map<String, Object> equal=new HashMap<String,Object>();
		String RequestType = request.getParameter("RequestType");
		if(StringUtil.isNotBlank(RequestType)){
			equal.put("version_type", RequestType);
		}
		
		String flag = "";
		UpExe upExe = new UpExe();
		
		if(null==RequestType || RequestType.equals("")){
			flag = "fail";
		}else{
			Version version_ = null;
			List<Version> versionList = versionService.findVersionNew(equal);
			if(null != versionList && versionList.size()>0){
				version_ = versionList.get(0);
			}
			
			upExe.setAddress(version_.getAddress());
			upExe.setVersion(version_.getVersion());
			flag = "success";
		}
		
		upExe.setFlag(flag);
		upExe.setSize(this.getFileSize(upExe.getAddress()));
		
		return upExe;
	}
	
	//判断文件大小
	public long getFileSize(String filePath) throws MalformedURLException{
	  HttpURLConnection urlcon = null;
	  long filesize = 0;
	  //create url link
	  URL url=new URL(filePath);
	  try {
	   //open url
	   urlcon = (HttpURLConnection)url.openConnection();
	   //get url properties
	   filesize=urlcon.getContentLength();
	   //format output
	   //transfer byte to kb
	   //size=fnum.format(filesize/1024);
	  } catch (IOException e) {
	   e.printStackTrace();
	  } finally{
	   //close connect
	   urlcon.disconnect();
	  }
	  return filesize;
	}
	
}
