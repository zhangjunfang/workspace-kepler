package com.ctfo.sysmanage.service.impl;

import java.util.Date;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.annouce.beans.TbAttachment;
import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;
import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.BulletinManage;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.beans.TbSetbook;
import com.ctfo.sys.beans.TbOnOff;
import com.ctfo.sysmanage.dao.BulletinManageDao;
import com.ctfo.sysmanage.dao.TbOnOffDao;
import com.ctfo.sysmanage.service.BulletinManageService;
import com.ctfo.util.Base64_URl;
import com.ctfo.util.DateUtil;
import com.ctfo.util.JsonUtil;
import com.ctfo.util.SerialUtil;
import com.ctfo.util.Tools;
@Service
public class BulletinManageServiceImpl extends RemoteJavaServiceAbstract implements BulletinManageService {
	@Autowired
	BulletinManageDao  bulletinManageDao;
	@Autowired
	TbOnOffDao tbOnOffDao;
	/** redis主连接池 */
	@Autowired
	private JedisPool writeJedisPool;
	/**
	 * 获取分页的总条数
	 */
	public int count(DynamicSqlParameter param) {
		return bulletinManageDao.count(param);
	}
	/**
	 * 返回分页数据
	 */
	@Override
	public PaginationResult<BulletinManage> selectPagination(DynamicSqlParameter param) {
		return bulletinManageDao.selectPagination(param);
	}
	/**
	 * 根据主键查询对象信息
	 */
	@Override
	public BulletinManage selectByPrimaryKey(String annoucId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.selectPK(annoucId);
	}
	public List<Map<String,Object>> selectByPrimaryKey(String annoucId,String s) {
		// TODO Auto-generated method stub
		return bulletinManageDao.selectPK(annoucId,"");
	}
	/**
	 * 新增公告
	 */
	@Override
	public void insert(BulletinManage bulletinManage) {
		// TODO Auto-generated method stub
		bulletinManageDao.insert(bulletinManage);
	}
	/**
	 * 修改公告
	 */
	@Override
	public int update(Map map) {
		// TODO Auto-generated method stub
		return bulletinManageDao.update(map);
	}
	/**
	 * 删除公告
	 */
	@Override
	public int delete(Map<String,String> map) {
		// TODO Auto-generated method stub
		return bulletinManageDao.delete(map);
	}
	/**
	 * 查询公司列表
	 */
	@Override
	public List<CompanyInfo> queryCompanyList() {
		// TODO Auto-generated method stub
		return bulletinManageDao.queryCompanyList();
	}
	/**
	 * 根据公司编码查询帐套集合
	 */
	@Override
	public List<TbSetbook> queryCompanySetbookList(String comId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.queryCompanySetbookList(comId);
	}
	@Override
	public String getCompanyNameById(String comId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.getCompanyNameById(comId);
	}
	@Override
	public String getCompanySetbookNameById(String setbookId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.getCompanySetbookNameById(setbookId);
	}
	@Override
	public String getDeptNameById(String deptId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.getDeptNameById(deptId);
	}
	@Override
	public String getDeptEmployeeName(String employeeId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.getDeptEmployeeName(employeeId);
	}
	@Override
	public void insertAttachment(TbAttachment attachment) {
		// TODO Auto-generated method stub
		bulletinManageDao.insertAttachment(attachment);
		
	}
	@Override
	public List<TbAttachment> selectListByPrimaryKey(String annoucId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.selectListByPrimaryKey(annoucId);
		
	}
	@Override
	public int updatePulishStatus(Map map) {
		// TODO Auto-generated method stub	
		int i =  bulletinManageDao.updatePulishStatus(map);
		return i;
	}
	@Override
	public int cancelAnnouce(Map map) {
		// TODO Auto-generated method stub
		int i = bulletinManageDao.cancelAnnouce(map);
		return i;
	}
	@Override
	public List queryAnnouceDeptList(Map map) {
		// TODO Auto-generated method stub
		return bulletinManageDao.queryAnnouceDeptList(map);
	}
	@Override
	public List queryAnnouceDeptEmployeeList(Map map) {
		// TODO Auto-generated method stub
		return bulletinManageDao.queryAnnouceDeptEmployeeList(map);
	}
	@Override
	public int deleteAnnouceFileById(String annoucId, String attachId) {
		// TODO Auto-generated method stub
		return bulletinManageDao.deleteAnnouceFileById(annoucId,attachId);
	}
	@Override
	public int updateAttach(Map<String, Object> attachMap) {
		// TODO Auto-generated method stub
		return bulletinManageDao.updateAttach(attachMap);
	}
	@Override
	public TbOnOff selectPK(String onOffId) {
		// TODO Auto-generated method stub
		return tbOnOffDao.selectPK(onOffId);
	}
	@Override
	public int updatePulishStatusToExamine(Map map) {
		// TODO Auto-generated method stub
		return bulletinManageDao.updatePulishStatusToExamine(map);
	}
	@Override
	public int updatePulishStatusToReject(Map map) {
		// TODO Auto-generated method stub
		return bulletinManageDao.updatePulishStatusToReject(map);
	}
	public JedisPool getWriteJedisPool() {
		return writeJedisPool;
	}
	public void setWriteJedisPool(JedisPool writeJedisPool) {
		this.writeJedisPool = writeJedisPool;
	}
	@Override
	public void addAnnouceToJedis(BulletinManage bulletinManage) {
		// TODO Auto-generated method stub
	    Jedis client = writeJedisPool.getResource();
		String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
		//转json
		String json=null;
		try
		{
		   //公告内容base64加密
		   bulletinManage.setAnnouceContent(Base64_URl.base64Encode(bulletinManage.getAnnouceContent()));
		   json = JsonUtil.obj2Json(bulletinManage);
		}catch(Exception e)
		{
		   e.printStackTrace();
		}
		StringBuffer msg = new StringBuffer();
		msg.append(serviceStationId).append("$"); //虚拟服务站id
		msg.append(SerialUtil.getInt()).append("$");	//消息流水
		msg.append("S").append("$");	//消息主类型
		msg.append("S1").append("$");	//消息子类型
		msg.append(DateUtil.dateToUtcTime(new Date())).append("$");	//时间戳
		msg.append(bulletinManage.getComCode()).append("$");//要发送的服务站ID
		msg.append("1").append("$");	//操作类型 1添加；2更新
		msg.append(Base64_URl.base64Encode(json)); //操作对象 对象序列化json串，进行base64编码
		
		//校验码
		String msgString = msg.toString();
		String checkCode = Tools.getCheckCode(msgString);
		
		StringBuffer msg_ = new StringBuffer();
		msg_.append("["); 
		msg_.append(msgString).append("$");
		msg_.append(checkCode);
		msg_.append("]");
		
		client.sadd("C_"+bulletinManage.getComCode(), msg_.toString());
	}
	@Override
	public void refreshAnnouceToJedis(Map map) {
		// TODO Auto-generated method stub
		Jedis client = writeJedisPool.getResource();
		String[] annoucId = (String[]) map.get("annoucId");
		for (int j = 0; j < annoucId.length; j++) {
			String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
			//转json
			String json=null;
			List<TbAttachment> tbAttachmentList = this.selectListByPrimaryKey(annoucId[j]);
			BulletinManage bulletinManage = this.selectByPrimaryKey(annoucId[j]);
			bulletinManage.setTbAttachment(tbAttachmentList);
			try
			{
			   json = JsonUtil.obj2Json(bulletinManage);
			}catch(Exception e)
			{
			   e.printStackTrace();
			}
			StringBuffer msg = new StringBuffer();
			msg.append(serviceStationId).append("$"); //虚拟服务站id
			msg.append(SerialUtil.getInt()).append("$");	//消息流水
			msg.append("S").append("$");	//消息主类型
			msg.append("S1").append("$");	//消息子类型
			msg.append(DateUtil.dateToUtcTime(new Date())).append("$");	//时间戳
			msg.append(bulletinManage.getComCode()).append("$");//要发送的服务站ID
			msg.append("2").append("$");	//操作类型 1添加；2更新
			msg.append(Base64_URl.base64Encode(json)); //操作对象 对象序列化json串，进行base64编码
			
			//校验码
			String msgString = msg.toString();
			String checkCode = Tools.getCheckCode(msgString);
			
			StringBuffer msg_ = new StringBuffer();
			msg_.append("["); 
			msg_.append(msgString).append("$");
			msg_.append(checkCode);
			msg_.append("]");
			
			client.sadd("C_"+bulletinManage.getComCode(), msg_.toString());
		}
	}
}
