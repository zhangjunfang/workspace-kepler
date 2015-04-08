package com.ctfo.sysmanage.controller;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.lang.StringUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import com.ctfo.annouce.beans.TbAttachment;
import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.BulletinManage;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.beans.TbSetbook;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.TbOnOff;
import com.ctfo.sysmanage.service.BulletinManageService;
import com.ctfo.util.DateUtil;
import com.ctfo.util.GeneratorUUID;
import com.ctfo.util.MongoDataSource;
import com.ctfo.util.Tools;
import com.mongodb.gridfs.GridFS;
import com.mongodb.gridfs.GridFSInputFile;

/**
 * 公告管理
 * @author Administrator
 *
 */
/**
 * @author 于博洋
 * 
 *         2014-10-30
 */

@Controller
@RequestMapping("/bulletinManage")
public class BulletinManageController extends BaseController {
	
	/**
	 * 日志
	 */
	private static Log log = LogFactory.getLog(BulletinManageController.class);
	
	@Autowired
	BulletinManageService bulletinManageService;
	@Autowired
	MongoDataSource  mongoDB;
	
	PrintWriter outMsg = null;

	/**
	 * 分页查询公告管理列表
	 * 
	 * @param request
	 * @return
	 * @throws ParseException
	 */
	@RequestMapping("/findList.do")
	@ResponseBody
	public Map<String, Object> findList(HttpServletRequest request)throws ParseException {

		DynamicSqlParameter param = super.getPageParam(request);// 获取分页的参数

		Map<String, String> like = new HashMap<String, String>();
		Map<String, Object> equal = new HashMap<String, Object>();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");

		String mainWord = request.getParameter("requestParam.equal.mainWord");// 关键字
		String comCode = request.getParameter("requestParam.equal.comCode");// 接收方名称
		String annouceStatus = request
				.getParameter("requestParam.equal.annouceStatus");// 状态
		String releaseDateStart = request
				.getParameter("requestParam.equal.releaseDateStart");// 发布开始时间
		String releaseDateEnd = request
				.getParameter("requestParam.equal.releaseDateEnd");// 发布结束时间
		String annouceDept = request
				.getParameter("requestParam.equal.annouceDept");// 发布部门
		String annoucePeopleId = request
				.getParameter("requestParam.equal.annoucePeopleId");// 发布人

		if (StringUtils.isNotBlank(mainWord)) {
			like.put("mainWord", mainWord);
		}
		if (StringUtils.isNotBlank(comCode)) {
			equal.put("comCode", comCode);
		}
		if (StringUtils.isNotBlank(annouceStatus)) {
			equal.put("annouceStatus", annouceStatus);
		}
		if (StringUtils.isNotBlank(releaseDateStart)) {
			equal.put("releaseDateStart",
					DateUtil.dateToUtcTime(sdf.parse(releaseDateStart)));
		}
		if (StringUtils.isNotBlank(releaseDateEnd)) {
			equal.put("releaseDateEnd",
					DateUtil.dateToUtcTime(sdf.parse(releaseDateEnd)));
		}
		if (StringUtils.isNotBlank(annouceDept)) {
			equal.put("annouceDept", annouceDept);
		}

		if (StringUtils.isNotBlank(annoucePeopleId)) {
			equal.put("annoucePeopleId", annoucePeopleId);
		}

		param.setLike(like);
		param.setEqual(equal);
		int total = bulletinManageService.count(param);
		PaginationResult<BulletinManage> list = bulletinManageService
				.selectPagination(param);
		Map<String, Object> result = new HashMap<String, Object>(2);

		result.put("Rows", list.getData());
		result.put("Total", total);
		return result;
	}

	/**
	 * 根据主键查看实体bean
	 */
	@RequestMapping("/findById.do")
	@ResponseBody
	public Map<String,Object> selectByPrimaryKey(HttpServletRequest request) {
		String annoucId = request.getParameter("annoucId");
		Map<String,Object> results = new HashMap<String,Object>();
		BulletinManage bulletinManage = bulletinManageService.selectByPrimaryKey(annoucId);//根据公告ID查询公告对象
		List<TbAttachment> tbAttachmentList = bulletinManageService.selectListByPrimaryKey(annoucId);//根据公告ID查询公告附件列表
/*		for (int i = 0; i < tbAttachmentList.size(); i++) {
			tbAttachmentList.get(i).setFilePath(mongoDB.getUrl());
		}*/
		results.put("model", bulletinManage);
		results.put("list", tbAttachmentList);
		return results;
	}

	/**
	 * 根据主键修改公告
	 * @throws ParseException 
	 * @RequestParam MultipartFile[] attachName,HttpServletRequest request,
	 * @RequestBody BulletinManage bulletinManage,
	 */
	@RequestMapping(value = "/modifyById.do")
	public String modifyById(@RequestParam MultipartFile[] attachName,HttpServletRequest request,HttpServletResponse response) throws ParseException {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			/**
			 * 公告主体信息获取
			 */
			String annoucId = request.getParameter("annoucId");//获取公告编码			
			String annoucSubject = request.getParameter("annoucSubject");// 公告主题
			String releaseDate = request.getParameter("releaseDate");// 发布时间
			String comCode = request.getParameter("comCode");// 公司（接收方）
			String comName = request.getParameter("comName");// 公司名称（接收方）
			String annouceDept = request.getParameter("annouceDept");// 部门ID
			String annouceDeptName = request.getParameter("annouceDeptName");// 部门名称
			String annoucePeopleId = request.getParameter("annoucePeopleId");// 发布人ID
			String setbookId = request.getParameter("setbookId");//帐套ID
			String setbookName = request.getParameter("setbookName");//帐套名称
			String annoucePeople = request.getParameter("annoucePeople");// 发布人名称
			String annouceContent = request.getParameter("annouceContent");// 发布内容
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			
			map.put("annoucId",annoucId);
			map.put("annoucSubject",annoucSubject);
			map.put("mainWord",annoucSubject);
			map.put("releaseDate",DateUtil.dateToUtcTime(DateUtil.parse(releaseDate)));
			map.put("comCode",comCode);
			map.put("comName",comName);
			map.put("annouceDept",annouceDept);
			map.put("annouceDeptName",annouceDeptName);
			map.put("setbookId",setbookId);
			map.put("setbookName",setbookName);
			map.put("annoucePeopleId",annoucePeopleId);
			map.put("annoucePeople",annoucePeople);
			map.put("annouceContent", annouceContent);
			map.put("updateBy", opInfo.getOpId());
			map.put("updateTime", DateUtil.dateToUtcTime(new Date()));
			map.put("annouceStatus", "0");//未删除状态
			
			bulletinManageService.update(map);//保存信息到公告表中
			
			/**
			 * 公告附件部门信息获取
			 */
			TbAttachment attachment = null;
			String[] remarks = request.getParameterValues("remark");// 附件备注
			String[] attachIds = request.getParameterValues("attachId");//附件表ID
			String[] attachAliasNames = request.getParameterValues("attachAliasName");// 附件名称
			
			if(attachIds != null){
				Map<String,Object> attachMap = new HashMap<String,Object>();
				for (int i = 0; i < attachIds.length; i++) {
					if(attachName[i].getOriginalFilename() != null && !"".equals(attachName[i].getOriginalFilename().trim())){
					//保存公告附件信息
					//	attachMap.put("attachId", GeneratorUUID.generateUUID());
						attachMap.put("attachId",attachIds[i]);
						attachMap.put("annoucId",annoucId);
						attachMap.put("attachName", attachName[i].getOriginalFilename());
						attachMap.put("attachAliasName", attachName[i].getOriginalFilename());
						attachMap.put("attachCategory", attachName[i].getOriginalFilename().substring(attachName[i].getOriginalFilename().lastIndexOf(".")+1,attachName[i].getOriginalFilename().length()));
						attachMap.put("remark", remarks[i]);
						attachMap.put("filePath", mongoDB.getUrl());
						bulletinManageService.updateAttach(attachMap);
					}
				}
			}else{
				if (attachName.length > 0) {
					for (int i = 0; i < attachName.length; i++) {
						attachment = new TbAttachment();
						attachment.setAttachId(GeneratorUUID.generateUUID());//附件表ID
						attachment.setAnnoucId(annoucId);//附件表中公告ID 关联字段
						attachment.setAttachName(attachName[i].getOriginalFilename());//附件真实名称
						attachment.setAttachAliasName(attachAliasNames[i]);//附件别名
						attachment.setAttachCategory(attachName[i].getOriginalFilename().substring(attachName[i].getOriginalFilename().lastIndexOf(".")+1,attachName[i].getOriginalFilename().length()));//文件类型
						attachment.setRemark(remarks[i]);
						attachment.setFilePath(mongoDB.getUrl());
						if(!"".equals(attachName[i].getOriginalFilename())){
							bulletinManageService.insertAttachment(attachment);//保存附件
						}
						saveMongo(attachName[i]);
						log.debug("附件存储完成");
					}
				}
			}
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}

	/**
	 * 根据主键删除公告
	 */
	@RequestMapping("/deleteById.do")
	@ResponseBody
	public String deleteById(String annoucId, HttpServletResponse response) {
		System.out.println("删除公告！！！");
		Map<String,String> map = new HashMap<String,String>();
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		try {
			// String annouceId = bulletinManage.getAnnoucId();
			// Map<String,String> map = new HashMap<String,String>();
			// map.put("annouceId", annoucId);
			String comId = Tools.turnString(annoucId);
			map.put("annouceId", comId);
			map.put("updateBy", opInfo.getOpId());
			map.put("updateTime", String.valueOf(DateUtil.dateToUtcTime(new Date())));
			
			bulletinManageService.delete(map);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage()
					.replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 根据主键删除公告附件
	 */
	@RequestMapping("/deleteAnnouceFileById.do")
	@ResponseBody
	public String deleteAnnouceFileById(String annoucId, String attachId,HttpServletResponse response) {
		System.out.println("删除公告附件信息！！！");
		try {
			bulletinManageService.deleteAnnouceFileById(annoucId,attachId);
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage()
					.replaceAll("'|\\n", " "));
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}

	/***
	 * 新增公告
	 * @param attachName
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping("/insertBulletinMange.do")
	@ResponseBody
	public String insertBulletinMange(@RequestParam MultipartFile[] attachName,HttpServletRequest request, HttpServletResponse response) {
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		String id = GeneratorUUID.generateUUID();//公告表和公告附件表共用ID
		String onOffId = "1";
		TbOnOff tbOnOff = bulletinManageService.selectPK(onOffId);
		try {
			String isRelease = request.getParameter("isRelease");//前台标示
			String annoucSubject = request.getParameter("annoucSubject");// 公告主题
			String releaseDate = request.getParameter("releaseDate");// 发布时间
			String comCode = request.getParameter("comCode");// 公司（接收方）
			String comName = request.getParameter("comName");// 公司名称（接收方）
			String annouceDept = request.getParameter("annouceDept");// 部门ID
			String annouceDeptName = request.getParameter("annouceDeptName");// 部门名称
			String annoucePeopleId = request.getParameter("annoucePeopleId");// 发布人ID
			String annoucePeople = request.getParameter("annoucePeople");// 发布人名称
			String setbookId = request.getParameter("setbookId");//帐套ID
			String setbookName = request.getParameter("setbookName");//帐套名称
			String annouceContent = request.getParameter("annouceContent");// 发布内容
			String createBy = opInfo.getOpId();
			long createTime = DateUtil.dateToUtcTime(new Date());
			
			BulletinManage bulletinManage = new BulletinManage();
			bulletinManage.setAnnoucId(id);
			bulletinManage.setAnnoucSubject(annoucSubject);
			bulletinManage.setMainWord(annoucSubject);
			bulletinManage.setReleaseDate(DateUtil.dateToUtcTime(DateUtil.parse(releaseDate)));
			bulletinManage.setComCode(comCode);
			bulletinManage.setComName(comName);
			bulletinManage.setAnnouceDept(annouceDept);
			bulletinManage.setAnnouceDeptName(annouceDeptName);
			bulletinManage.setAnnoucePeopleId(annoucePeopleId);
			bulletinManage.setAnnoucePeople(annoucePeople);
			bulletinManage.setSetbookId(setbookId);
			bulletinManage.setSetbookName(setbookName);
			bulletinManage.setAnnouceContent(annouceContent);
			bulletinManage.setCreateBy(createBy);
			bulletinManage.setCreateTime(createTime);
			
			if(isRelease.equals("1")||isRelease == "1"){
				//判断审核开关
				if(tbOnOff.getAnnounceAuditStatus().equals("1")){
					bulletinManage.setAnnouceStatus("2");
				}else{
					bulletinManage.setAnnouceStatus("1");
				}
			}else{
				bulletinManage.setAnnouceStatus("0");
			}
			bulletinManage.setEnableFlag("1");
			bulletinManage.setUpdateBy((String)request.getSession().getAttribute("opLoginname"));
			bulletinManage.setUpdateTime(DateUtil.dateToUtcTime(new Date()));
//			bulletinManageService.insert(bulletinManage);// 新增公告保存到公告表中
			
			/**
			 * 公告附件
			 */
			TbAttachment attachment = null;
			List<TbAttachment> tbAttachmentList = new ArrayList<TbAttachment>();
			String[] attachAliasNames = request.getParameterValues("attachAliasName");// 附件名称
			String[] remarks = request.getParameterValues("remark");// 附件备注

			if (attachName.length > 0) {
				for (int i = 0; i < attachName.length; i++) {
					attachment = new TbAttachment();
					attachment.setAttachId(GeneratorUUID.generateUUID());//附件表ID
					attachment.setAnnoucId(id);//附件表中公告ID 关联字段
					attachment.setAttachName(attachName[i].getOriginalFilename());//附件真实名称
					attachment.setAttachAliasName(attachAliasNames[i]);//附件别名
					attachment.setAttachCategory(attachName[i].getOriginalFilename().substring(attachName[i].getOriginalFilename().lastIndexOf(".")+1,attachName[i].getOriginalFilename().length()));//文件类型
					attachment.setRemark(remarks[i]);
					attachment.setFilePath(mongoDB.getUrl());
					if(!"".equals(attachName[i].getOriginalFilename())){
						bulletinManageService.insertAttachment(attachment);//保存附件
						tbAttachmentList.add(attachment);
					}
					saveMongo(attachName[i]);
					log.debug("附件存储完成");
				}
			}
			bulletinManage.setTbAttachment(tbAttachmentList);
			bulletinManageService.insert(bulletinManage);// 新增公告保存到公告表中
			//判断前台传过来的标示isRelease  /*  0 :保存  1:发布 */
			if(isRelease.equals("0")||isRelease == "0"){
				
			}else if(isRelease.equals("1")||isRelease == "1"){
				//判断开关 1开启审核0关闭审核
				if(tbOnOff.getAnnounceAuditStatus().equals("0")){
					//发布公告新增至Jedis
					bulletinManageService.addAnnouceToJedis(bulletinManage);	
					log.debug("公告新增至Jedis(关闭审核-发布)");
				}
			}
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage()
					.replaceAll("'|\\n", " "));
		}  catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return this.returnInfoForJS(response, true, MES_SUCCESS_ADD);
	}
	/**
	 * 将公告附件存入mongoDB中
	 * @param attachName
	 * @return
	 * @throws IOException 
	 */
	public void saveMongo(MultipartFile attachName){
	  try{
		DataInputStream dis = new DataInputStream(attachName.getInputStream());
		int length = dis.available();
		byte[] buf = new byte[length];
		dis.read(buf);
		dis.close();
		
		GridFS gfsPhoto = new GridFS(MongoDataSource.getDb(mongoDB.getDbname()), mongoDB.getDbnamemongoFileDir());
		GridFSInputFile gfsFile = gfsPhoto.createFile(buf);
		gfsFile.setFilename(attachName.getOriginalFilename());
		gfsFile.setContentType(attachName.getOriginalFilename().substring(attachName.getOriginalFilename().lastIndexOf(".")+1,attachName.getOriginalFilename().length()));
		gfsFile.save();
	  }catch(Exception e){
		  log.debug("mongoDB存储公告附件错误");
	  }
	}
	
	/**
	 * 查询公司列表
	 */
	@ResponseBody
	@RequestMapping(value = "/queryCompanyList.do")
	public List<CompanyInfo> queryCompanyList() {
		System.out.println("查询公司列表");
		List<CompanyInfo> companylist = new ArrayList<CompanyInfo>();
		companylist = bulletinManageService.queryCompanyList();
		return companylist;
	}
	/**
	 * 查询部门列表
	 * @return 部门列表
	 */
	@ResponseBody
	@RequestMapping("/queryAnnouceDeptList.do")
	public List queryAnnouceDeptList(){
		OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
		
	    Map<String, Object> equal=new HashMap<String,Object>();
		
		if(opInfo.getIsOperator().equals("0")){
			equal.put("comId", opInfo.getComId());
		}
	    
		List deptlist = new ArrayList();
		deptlist = bulletinManageService.queryAnnouceDeptList(equal);
		return deptlist;
	}
	/**
	 * 根据部门ID查询部门人员列表
	 * @return 部门列表
	 */
	@ResponseBody
	@RequestMapping("/queryAnnouceDeptEmployeeList.do")
	public List queryAnnouceDeptEmployeeList(HttpServletRequest request){
		
//		String comId = request.getParameter("comId");
		String entId = request.getParameter("entId");
		
		Map<String,Object> map = new HashMap<String,Object>();
//		map.put("comId", comId);
		map.put("entId", entId);
		
		List employeelist = bulletinManageService.queryAnnouceDeptEmployeeList(map);
		return employeelist;
	}
	

	/**
	 * 根据公司编码查询公司所有的帐套信息
	 * 
	 * @param comId
	 *            公司编码
	 * @return 公司帐套集合
	 */
	@RequestMapping(value = "/queryCompanySetbookList.do")
	@ResponseBody
	public List<TbSetbook> queryCompanySetbookList(String comId) {
		List<TbSetbook> setbooklist = bulletinManageService
				.queryCompanySetbookList(comId);
		return setbooklist;
	}

	/**
	 * 
	 * @param comId
	 *            公司ID
	 * @return 公司名称
	 */
	public String getCompanyNameById(String comId) {
		return bulletinManageService.getCompanyNameById(comId);
	}

	/**
	 * 
	 * @param setbookId
	 *            帐套ID
	 * @return 帐套名称
	 */
	public String getCompanySetbookNameById(String setbookId) {
		return bulletinManageService.getCompanySetbookNameById(setbookId);
	}

	/**
	 * 
	 * @param deptId
	 *            部门编码
	 * @return 部门名称
	 */
	public String getDeptNameById(String deptId) {
		return bulletinManageService.getDeptNameById(deptId);
	}

	/**
	 * 
	 * @param employeeId
	 *            员工ID
	 * @return 员工名称（发布人）
	 */
	public String getDeptEmployeeName(String employeeId) {
		return bulletinManageService.getDeptEmployeeName(employeeId);
	}

	/**
	 * 
	 * @param filePath
	 * @param fileName
	 * @param response
	 * @return 下载公告附件
	 */
	@RequestMapping(value = "/downloadFile.do")
	@ResponseBody
	public String downloadFileByUrl(String filePath, String fileName,
			HttpServletResponse response) {
		BufferedInputStream bis = null;
		BufferedOutputStream bos = null;
		String downLoadPath = null;// 下载路径
		try {
			fileName = new String(fileName.getBytes("ISO8859-1"), "UTF-8");
			response.reset();
			response.setContentType("application/octet-stream;charset=UTF-8");
			response.setHeader("Content-disposition", "attachment; filename="
					+ new String(fileName.getBytes("utf-8"), "ISO8859-1"));
			downLoadPath = filePath.replaceAll("/", File.separator)
					+ File.separator + fileName;
			bis = new BufferedInputStream(new FileInputStream(downLoadPath));
			bos = new BufferedOutputStream(response.getOutputStream());
			byte[] buff = new byte[2048];
			int bytesRead;
			while (-1 != (bytesRead = bis.read(buff, 0, buff.length))) {
				bos.write(buff, 0, bytesRead);
			}

		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally {
			try {
				if (bis != null)
					bis.close();
				if (bos != null)
					bos.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}

		return null;
	}

	/**
	 * 多附件上传方法
	 * 
	 * @param myfiles
	 *            公告附件
	 * @param request
	 * @return
	 */
//	@RequestMapping(value = "/filesUpload.do")
//	@ResponseBody
//	public String uploadFile(@RequestParam MultipartFile[] myfiles,
//			HttpServletRequest request) {
//
//		return null;
//	}
	/**
	 * 发布公告
	 * @param annoucId 公告编码
	 * @return 修改发布状态成功
	 * @throws IOException 
	 */
	@RequestMapping(value = "/publishAnnouceInfo.do")
	@ResponseBody
	public String publishAnnouce(String annoucId,HttpServletResponse response) throws IOException{
		try{
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			String updateBy = opInfo.getOpId();
			long updateTime = DateUtil.dateToUtcTime(new Date());
			String onOffId = "1";
			TbOnOff tbOnOff = bulletinManageService.selectPK(onOffId);
			String[] comId = annoucId.split(",", -1);
			
			Map<String,Object> map = new HashMap<String,Object>();
			map.put("annoucId", comId);
			map.put("updateBy", updateBy);
			map.put("updateTime", updateTime);
			
			int num = 0;
			if(tbOnOff.getAnnounceAuditStatus().equals("1")){
				map.put("status", "2");
				num = bulletinManageService.updatePulishStatusToExamine(map);
			}else if(tbOnOff.getAnnounceAuditStatus().equals("0")){
				map.put("status", "1");
				num = bulletinManageService.updatePulishStatus(map);
			   if(num!= 0){
				   //将发布的公告增加至Jedis
				   bulletinManageService.refreshAnnouceToJedis(map);
			   }
			}
			outMsg = response.getWriter();
			if(num <=0)
			return this.returnInfoForJS(response, true, "2");
			return this.returnInfoForJS(response, true, "1");
		}catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}

	}
	/**
	 * 撤销公告
	 * @param annoucId 公告编码
	 * @param response
	 * @throws IOException
	 */
	@RequestMapping(value = "/cancelPublishAnnouce.do")
	@ResponseBody
	public String publishChecked(String annoucId ,HttpServletResponse response) throws IOException{
		try{
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			String updateBy = opInfo.getOpId();
			long updateTime = DateUtil.dateToUtcTime(new Date());
			Map<String,Object> map = new HashMap<String,Object>();
			String[] comId = annoucId.split(",", -1);
			map.put("annoucId", comId);
			map.put("updateBy", updateBy);
			map.put("updateTime", updateTime);
			map.put("status", "3");
			int num = bulletinManageService.cancelAnnouce(map);
			if(num!=0){
				//将撤销状态的公告增加至Jedis
				bulletinManageService.refreshAnnouceToJedis(map);
			}
			if(num <= 0)
			return this.returnInfoForJS(response, true, "2");
			return this.returnInfoForJS(response, true, "1");
		}catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
	}
	/**
	 * 审核
	 * @param annoucId 公告编码
	 * @param response
	 * @throws IOException
	 */
	@RequestMapping(value = "/examineAnnouce.do")
	@ResponseBody
	public String examineAnnouce(String annoucId,String msgExamineType,HttpServletResponse response) throws IOException{
		try {
			String[] comId = annoucId.split(",", -1);
			OperatorInfo opInfo = OperatorInfo.getOperatorInfo(); 
			String updateBy = opInfo.getOpId();
			long updateTime = DateUtil.dateToUtcTime(new Date());
			Map<String,Object> map = new HashMap<String,Object>();
			map.put("annoucId", comId);
			map.put("updateBy", updateBy);
			map.put("updateTime", updateTime);
			
			int num = 0;
			if(!msgExamineType.equals("")&&msgExamineType.equals("1")){
			   map.put("status", "1");
			   num = bulletinManageService.updatePulishStatus(map);
			   if(num!= 0){
				   //将审核通过的公告增加至Jedis
				   bulletinManageService.refreshAnnouceToJedis(map);
			   }
			}
			else if(!msgExamineType.equals("")&&msgExamineType.equals("2")){
			   map.put("status", "4");
			   num = bulletinManageService.updatePulishStatusToReject(map);
			}
			
			if(num <= 0)
			return this.returnInfoForJS(response, true, "2");
			return this.returnInfoForJS(response, true, "1");

		}catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
	}

	/**
	 * 
	 * @description:导出公告管理
	 * @param:
	 * @author:  张恒
	 * @creatTime:  2014年12月22日上午10:49
	 * @modifyInformation：
	 */
	@RequestMapping(value="/exportBulletinListExcelData.do")
	@ResponseBody
	public void exportBulletinListExcelData(HttpServletRequest request,HttpServletResponse response) throws Exception{
		DynamicSqlParameter param=super.getPageParam(request);
		
		Map<String, String> like = new HashMap<String, String>();
		Map<String, Object> equal = new HashMap<String, Object>();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");

		String mainWord = request.getParameter("requestParam.equal.mainWord");// 关键字
		String comCode = request.getParameter("requestParam.equal.comCode");// 接收方名称
		String annouceStatus = request
				.getParameter("requestParam.equal.annouceStatus");// 状态
		String releaseDateStart = request
				.getParameter("requestParam.equal.releaseDateStart");// 发布开始时间
		String releaseDateEnd = request
				.getParameter("requestParam.equal.releaseDateEnd");// 发布结束时间
		String annouceDept = request
				.getParameter("requestParam.equal.annouceDept");// 发布部门
		String annoucePeopleId = request
				.getParameter("requestParam.equal.annoucePeopleId");// 发布人

		if (StringUtils.isNotBlank(mainWord)) {
			like.put("mainWord", mainWord);
		}
		if (StringUtils.isNotBlank(comCode)) {
			equal.put("comCode", comCode);
		}
		if (StringUtils.isNotBlank(annouceStatus)) {
			equal.put("annouceStatus", annouceStatus);
		}
		if (StringUtils.isNotBlank(releaseDateStart)) {
			equal.put("releaseDateStart",
					DateUtil.dateToUtcTime(sdf.parse(releaseDateStart)));
		}
		if (StringUtils.isNotBlank(releaseDateEnd)) {
			equal.put("releaseDateEnd",
					DateUtil.dateToUtcTime(sdf.parse(releaseDateEnd)));
		}
		if (StringUtils.isNotBlank(annouceDept)) {
			equal.put("annouceDept", annouceDept);
		}

		if (StringUtils.isNotBlank(annoucePeopleId)) {
			equal.put("annoucePeopleId", annoucePeopleId);
		}

		param.setLike(like);
		param.setEqual(equal);

		PaginationResult<BulletinManage> list = bulletinManageService.selectPagination(param);
		String json = jsonFormatToGrid(list);
		String exportDataHeader = "annoucSubject=标题&comName=接收方&annouceStatus=状态&releaseDate=发布时间&annouceDeptName=发布部门&annoucePeople=发布人";
		String excel_result = bulletinManageService.exportData(exportDataHeader, json, this.getUrl(request),"BulletinManage","公告列表");
	    this.printWriter(response, excel_result);
	}
}
