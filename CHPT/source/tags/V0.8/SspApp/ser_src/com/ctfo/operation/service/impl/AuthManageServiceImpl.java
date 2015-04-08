package com.ctfo.operation.service.impl;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import org.codehaus.jackson.map.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;
import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.dao.AuthManageDao;
import com.ctfo.operation.service.AuthManageService;
import com.ctfo.sys.beans.OperatorInfo;
import com.ctfo.sys.beans.TbOnOff;
import com.ctfo.sysmanage.dao.TbOnOffDao;
import com.ctfo.util.Base64_URl;
import com.ctfo.util.DateUtil;
import com.ctfo.util.GeneratorUUID;
import com.ctfo.util.MD5Util;
import com.ctfo.util.RedisJsonUtil;
import com.ctfo.util.SerialUtil;
import com.ctfo.util.Tools;
import com.ctfo.ypt.client.YptClient;
import com.google.gson.reflect.TypeToken;

@Controller
public class AuthManageServiceImpl extends RemoteJavaServiceAbstract implements AuthManageService{
	@Autowired
	AuthManageDao authManageDAO;
	@Autowired
	TbOnOffDao tbOnOffDao;
	@Autowired
	private JedisPool writeJedisPool;
	
	//云平台通信客户端
	@Autowired
	private YptClient yptClient;
	/**
	 * 
	 * @description:分页时获取记录总数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月14日下午1:56:38
	 * @modifyInformation：
	 */
	@Override
	public int count(DynamicSqlParameter param) {
		return authManageDAO.count(param);
	}
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月14日下午5:47:02
	 * @modifyInformation：
	 */
	@Override
	public PaginationResult<CompanyInfo> selectPagination(
			DynamicSqlParameter param) {
		return authManageDAO.selectPagination(param);
	}
	/**
	 * 
	 * @description:根据主键获取对象
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月16日下午11:42:18
	 * @modifyInformation：
	 */
	@Override
	public CompanyInfo selectPKById(Map map) {
		return authManageDAO.selectPKById(map);
	}
	/**
	 * 
	 * @description:添加公司
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月21日下午17:16:17
	 * @modifyInformation：
	 */
	@Override
	public void insert(CompanyInfo companyInfo) {
		// TODO Auto-generated method stub
		authManageDAO.insert(companyInfo);
		Jedis client = writeJedisPool.getResource();
		client.hset("HS_LOGIN", companyInfo.getComAccount(), companyInfo.getComPassWord() + "_" + companyInfo.getAuthCode());
	}
	@Override
	public void updateRevokeOpen(Map map) {
		// TODO Auto-generated method stub
		String[] comId = (String[]) map.get("comId");
		int i = 0;
		try {
			i = authManageDAO.updateRevokeOpen(map);
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
		}
		for (int j = 0; j < comId.length; j++) {
			if(i!=0){
			String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
			//转json
			String json=null;
			ObjectMapper mapper=null;
/*			try
			{
			   Map<String,Object> jsonMap = new HashMap<String,Object>();
			   jsonMap.put("status", map.get("status"));
			   jsonMap.put("comId", comId[j]);
			   mapper = new ObjectMapper();
			   json=mapper.writeValueAsString(jsonMap);//把map或者是list,object转换成 json
			}catch(Exception e)
			{
			   e.printStackTrace();
			}*/
			StringBuffer msg = new StringBuffer();
			msg.append(serviceStationId).append("$"); //虚拟服务站id
			msg.append(SerialUtil.getInt()).append("$");	//消息流水
			msg.append("C").append("$");	//消息主类型
			msg.append("C1").append("$");	//消息子类型
			msg.append(DateUtil.dateToUtcTime(new Date())).append("$");	//时间戳
			msg.append(comId[j]).append("$");//服务站id
			msg.append(map.get("status")).append("$");	//操作类型 1吊销；2启用
//			msg.append(Base64_URl.base64Encode(json)); //操作对象 对象序列化json串，进行base64编码
			
			//校验码
			String msgString = msg.toString();
			String checkCode = Tools.getCheckCode(msgString);
			
			StringBuffer msg_ = new StringBuffer();
			msg_.append("["); 
			msg_.append(msgString).append("$");
			msg_.append(checkCode);
			msg_.append("]");
			
			yptClient.getSession().write(msg_);
		}
		}

	}
	@Override
	public int updateAuthApproval(Map map) {
		// TODO Auto-generated method stub
		return authManageDAO.updateAuthApproval(map);
		
	}
	@Override
	public int updateAuthManage(Map map) {
		// TODO Auto-generated method stub
		String comId = (String) map.get("comId");
		String validDate = (String) map.get("validDate");
		int i = 0;
		
		try {
			i = authManageDAO.updateAuthmanage(map);
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
		}
		if(i!=0){
			String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
			
			StringBuffer msg = new StringBuffer();
			msg.append(serviceStationId).append("$"); //虚拟服务站id
			msg.append(SerialUtil.getInt()).append("$");	//消息流水
			msg.append("C").append("$");	//消息主类型
			msg.append("C3").append("$");	//消息子类型
			msg.append(DateUtil.dateToUtcTime(new Date())).append("$");	//时间戳
			msg.append(comId).append("$");//服务站id
			msg.append(validDate).append("$");	//有效时间UTC格式
			
			//校验码
			String msgString = msg.toString();
			String checkCode = Tools.getCheckCode(msgString);
			
			StringBuffer msg_ = new StringBuffer();
			msg_.append("["); 
			msg_.append(msgString).append("$");
			msg_.append(checkCode);
			msg_.append("]");
			
			yptClient.getSession().write(msg_);
		}
		
		return i;
	}
	/**
	 * cs接口:查询机器码是否存在数据库
	 * @param map
	 * @return
	 */
	public int countForRemote(Map map) {
		// TODO Auto-generated method stub
		return authManageDAO.countForRemote(map);
	}
	/**
	 * cs接口:更新存在机器码的资料
	 * @param map
	 * @return
	 */
	public int updateForRemote(CompanyInfo companyInfo) {
		// TODO Auto-generated method stub
		return authManageDAO.updateForRemote(companyInfo);
	}
	/**
	 * cs提交企业注册信息接口
	 */
	@Override
	public Map<String, String> remoteRegisterAuth(String json,String remoteIp) {
		// TODO Auto-generated method stub
		
		CompanyInfo comInfo = null;
		Map<String, String> returnJson = new HashMap<String, String>(8); 
		Map<String,String> map = new HashMap<String,String>();
		String base64Json = Base64_URl.base64Decode(json);
		Jedis client = writeJedisPool.getResource();
		try {
			//将接口获得的json转换成对象
			if (null != base64Json && !"".equals(base64Json)) {
				comInfo = (CompanyInfo) RedisJsonUtil.jsonToObject(base64Json, new TypeToken<CompanyInfo>() {
				});
			}
		} catch (Exception e) {
			returnJson.put("isSuccess", "0");
			returnJson.put("errMsg", "json格式错误");
			returnJson.put("sign_id", "");
			returnJson.put("validate", "");
			returnJson.put("machineCodeSequence", "");
			returnJson.put("grantAuthorization", "");
			returnJson.put("sUser", "");
			returnJson.put("sPwd", "");
			returnJson.put("authentication", "");
			
			return returnJson;
		}
		map.put("machineSerial", comInfo.getMachineSerial());
		
		//判断机器码在数据库中是否存在
		int isExist = this.countForRemote(map);
		
		//获取开关表信息
		String onOffId = "1";
		TbOnOff tbOnOff = tbOnOffDao.selectPK(onOffId);
		
		//创建账号,密码,鉴权码,软件授权码
		String account = "";
		String passWord = "";
		String authCode = "";
		String authorizationCode = "";
		if(""!=comInfo.getMachineSerial()||!comInfo.getMachineSerial().equals("")){
			String resultMD5 = MD5Util.getMd5(comInfo.getMachineSerial());
			account = resultMD5.substring(0, 12);
			passWord = resultMD5.substring(12, 22);
			authCode = resultMD5.substring(22, 32);
			authorizationCode =  MD5Util.getMd5(comInfo.getMachineSerial()+"hxc123456!@#$%^");
		}
		//有效期时间(当前时间加2049/12/31)
		Calendar curr = Calendar.getInstance();
		curr.clear();
		curr.set(2049, 11, 31);
		Date dateOfNextYear=curr.getTime();
		//根据接口传过来的数据将公司信息入库
		comInfo.setCreateBy("接口");//创建人
		comInfo.setCreateTime(DateUtil.dateToUtcTime(new Date()));//创建时间
		comInfo.setComAccount(account);//云平台账号
		comInfo.setComPassWord(passWord);//云平台密码
		comInfo.setAuthCode(authCode);//鉴权码
		comInfo.setAuthorizationCode(authorizationCode);
		comInfo.setValidDate(DateUtil.dateToUtcTime(dateOfNextYear));//有效期
		comInfo.setRegistIp(remoteIp);
		comInfo.setStatus("1");
		if(tbOnOff.getAutoMaticStatus()== "0"||tbOnOff.getAutoMaticStatus().equals("0")){
			comInfo.setRegisterAuthentication("2");//注册鉴权情况 待授权
			comInfo.setApprover("");
			comInfo.setApprovalAdvice("");
		}else if(tbOnOff.getAutoMaticStatus()== "1"||tbOnOff.getAutoMaticStatus().equals("1")){
			comInfo.setRegisterAuthentication("1");//注册鉴权情况 已授权
			comInfo.setApprover("");
			comInfo.setApprovalAdvice("0");
			comInfo.setApprovalTime(DateUtil.dateToUtcTime(new Date()));
		}
		comInfo.setRegistTime(DateUtil.dateToUtcTime(new Date()));//注册时间
		
		if(isExist > 0){
			try {
				comInfo.setUpdateTime(DateUtil.dateToUtcTime(new Date()));//最后更新时间
				this.updateForRemote(comInfo);
				client.hset("HS_LOGIN", comInfo.getComAccount(), comInfo.getComPassWord() + "_" + comInfo.getAuthCode());
			} catch (Exception e) {
				returnJson.put("isSuccess", "0");
				returnJson.put("errMsg", "数据库异常");
				returnJson.put("sign_id", "");
				returnJson.put("validate", "");
				returnJson.put("machineCodeSequence", "");
				returnJson.put("grantAuthorization", "");
				returnJson.put("sUser", "");
				returnJson.put("sPwd", "");
				returnJson.put("authentication", "");
				
				return returnJson;
			}
		}else{
			try {
				comInfo.setComId(GeneratorUUID.generateUUID());//公司id
				this.insert(comInfo);
				//向redis缓存机制中添加新增纪录
				client.hset("HS_LOGIN", comInfo.getComAccount(), comInfo.getComPassWord() + "_" + comInfo.getAuthCode());
			} catch (Exception e) {
				returnJson.put("isSuccess", "0");
				returnJson.put("errMsg", "数据库异常");
				returnJson.put("sign_id", "");
				returnJson.put("validate", "");
				returnJson.put("machineCodeSequence", "");
				returnJson.put("grantAuthorization", "");
				returnJson.put("sUser", "");
				returnJson.put("sPwd", "");
				returnJson.put("authentication", "");
				
				return returnJson;
			}
		}
		
		CompanyInfo comInfoBack = this.selectByMachineCode(comInfo.getMachineSerial());
		//判断是否是自动授权
		if(tbOnOff.getAutoMaticStatus()== "0"||tbOnOff.getAutoMaticStatus().equals("0")){
			//0为关闭自动授权
			returnJson.put("isSuccess", "1");
			returnJson.put("errMsg", "");
			returnJson.put("validate", DateUtil.dateToUtcTime(dateOfNextYear).toString());
			returnJson.put("sign_id", comInfoBack.getComId());
			returnJson.put("machineCodeSequence", comInfo.getMachineSerial());
			returnJson.put("grantAuthorization", "");
			returnJson.put("sUser", account);
			returnJson.put("sPwd", passWord);
			returnJson.put("authentication", authCode);
		}else if(tbOnOff.getAutoMaticStatus()== "1"||tbOnOff.getAutoMaticStatus().equals("1")){
			//1为开启自动授权
			try {
				client.hset("HS_STATIONID", comInfoBack.getServiceStationSap(), comInfoBack.getComId());
			} catch (Exception e) {
				returnJson.put("isSuccess", "0");
				returnJson.put("errMsg", "宇通sap代码为空");
				returnJson.put("sign_id", "");
				returnJson.put("validate", "");
				returnJson.put("machineCodeSequence", "");
				returnJson.put("grantAuthorization", "");
				returnJson.put("sUser", "");
				returnJson.put("sPwd", "");
				returnJson.put("authentication", "");
				
				return returnJson;
			}
			returnJson.put("isSuccess", "1");
			returnJson.put("errMsg", "");
			returnJson.put("validate", DateUtil.dateToUtcTime(dateOfNextYear).toString());
			returnJson.put("sign_id", comInfoBack.getComId());
			returnJson.put("machineCodeSequence", comInfo.getMachineSerial());
			returnJson.put("grantAuthorization", authorizationCode);
			returnJson.put("sUser", account);
			returnJson.put("sPwd", passWord);
			returnJson.put("authentication", authCode);
		}
		
		return returnJson;
	}
	/**
	 * cs提交企业注册结果接口
	 */
	@Override
	public Map<String, String> registerAuthResult(String remoteMachineCode) {
		// TODO Auto-generated method stub
		Map<String, String> returnJson = new HashMap<String, String>(2);
		Map<String,String> map = new HashMap<String,String>();
		map.put("machineSerial", remoteMachineCode);
		Jedis client = writeJedisPool.getResource();
		
		int isUpdate = this.updateByMachineCode(map);
		if(isUpdate!=0){
			CompanyInfo redisCompanyInfo = this.selectByMachineCode(remoteMachineCode);
			//更新状态成功后，像redis中增加宇通sap代码和服务站id
			try {
				client.hset("HS_STATIONID", redisCompanyInfo.getServiceStationSap(),redisCompanyInfo.getComId());
			} catch (Exception e) {
				returnJson.put("isSuccess", "0");
				returnJson.put("errMsg", "宇通sap代码为空");
				returnJson.put("sign_id", "");
				returnJson.put("validate", "");
				returnJson.put("machineCodeSequence", "");
				returnJson.put("grantAuthorization", "");
				returnJson.put("sUser", "");
				returnJson.put("sPwd", "");
				returnJson.put("authentication", "");
				
				return returnJson;
			}
			returnJson.put("isSuccess", "1");
			returnJson.put("errMsg", "");
			return returnJson;
		}else{
			returnJson.put("isSuccess", "0");
			returnJson.put("errMsg", "无该公司数据");
			return returnJson;
		}
	}
	/**
	 * 根据机器码更新鉴权状态
	 * @param map
	 * @return
	 */
	public int updateByMachineCode(Map map) {
		return authManageDAO.updateByMachineCode(map);
	}
	
	/**
	 * 根据机器码查询公司信息
	 * @param map
	 * @return
	 */
	public CompanyInfo selectByMachineCode(String machineCode) {
		return authManageDAO.selectByMachineCode(machineCode);
	}
	@Override
	public CompanyInfo selectPKByCom(String comId) {
		// TODO Auto-generated method stub
		return authManageDAO.selectPKByCom(comId);
	}
	@Override
	public CompanyInfo selectPKByMachine(String MachineNo) {
		// TODO Auto-generated method stub
		return authManageDAO.selectByMachineCode(MachineNo);
	}
}
