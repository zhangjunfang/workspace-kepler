package com.ctfo.syncservice.task;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.syncservice.model.OrgInfo;
import com.ctfo.syncservice.util.Base64_URl;
import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：四川企业同步
 * <li>时        间：2014-07-16  下午14:55:09	</li><br>
 * 
 * @author huangjincheng
 *
 */
public class OrgInfoTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(OrgInfoTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
	/**	清理标记	*/
	private static boolean clearFlag = false;
	/** 最近更新时间		*/
	private static long authUtc = System.currentTimeMillis();
	
	
	@Override
	public void init() {
		try {
			//setName("OrgInfoTask");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			execute();
		} catch (Exception e) {
			logger.error("初始化企业静态信息同步任务异常:" + e.getMessage(), e); 
		}
	}
	
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br> 
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		long lastUtc = authUtc;
		int index = 0;
		int error = 0;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
//			判断清理时间间隔
			if((start - lastTime) > clearInterval){
				clearFlag = true;
				lastTime = System.currentTimeMillis();
			}
//			判断当天
//			String currentDate = new SimpleDateFormat("yyyyMMddHH").format(new Date());
//			String currentStr = currentDate.substring(0, 8);
//			if(!currentStr.equals(lastDay)){ // 隔天
//				String hours = currentDate.substring(8);
//				if(hours.equals(clearTime)){ 
//					clearFlag = true;
//				}
//			}
			ArrayList<OrgInfo> list =  new ArrayList<OrgInfo>();
			long s1 = System.currentTimeMillis();
			if("OrgInfoTask_byOracle".equals(name)){
				conn = this.oracle.getConnection();
				if(clearFlag){
					ps = conn.prepareStatement(conf.get("sql_orgInfoListAll"));
				} else {
					ps = conn.prepareStatement(conf.get("sql_orgInfoListIncremental"));
					ps.setLong(1, lastUtc);
					ps.setLong(2, lastUtc);
				}
				rs = ps.executeQuery();
				
				while (rs.next()) {
					try {
						OrgInfo orgInfo = new OrgInfo();
						orgInfo.setOrgName(notNull(rs.getString("ENT_NAME")));
						orgInfo.setTradeCode(notNull(rs.getString("SPECIAL_ID")));
						orgInfo.setBusinessScope(notNull(rs.getString("BUSINESS_SCOPE")));
						orgInfo.setOrigin(notNull(rs.getString("CORP_CITY")));
						orgInfo.setBusinessCertificateCode(notNull(rs.getString("LICENCE_WORD")));
						orgInfo.setAddress(notNull(rs.getString("ORG_ADDRESS")));
						orgInfo.setContacts(notNull(rs.getString("ORG_CNAME")));
						orgInfo.setContactsNo(notNull(rs.getString("ORG_CNO")));
						list.add(orgInfo);
						index++;
					}  catch (Exception e) {
						logger.error("--OrgInfoTask-- Oracle取静态信息异常:" + e.getMessage(), e);
						list.clear();
						error++;
						continue;
					}
				}
			} else if ("OrgInfoTask_byFile".equals(name)) {
				String str = "";
				File file = new File(conf.get("filePath"));
				if (file.exists()) {
					FileInputStream fi = new FileInputStream(file);
					BufferedReader br = new BufferedReader(new InputStreamReader(fi, "gbk"));
					while ((str = br.readLine()) != null) {
						String[] contentArr = str.split(";");
						long utc = Long.parseLong(contentArr[14].trim());
						if (utc > lastUtc || clearFlag) {
							OrgInfo orgInfo = new OrgInfo();
							orgInfo.setOrgCode(notNull(contentArr[0]));
							orgInfo.setOrgName(notNull(contentArr[1]));
							orgInfo.setTradeCode(notNull(contentArr[2]));
							orgInfo.setEntOrgCode(notNull(contentArr[3]));
							orgInfo.setManageOrgCode(notNull(contentArr[4]));
							orgInfo.setOrigin(notNull(contentArr[5]));
							orgInfo.setBusinessCertificateCode(notNull(contentArr[6]));
							orgInfo.setBusinessScope(notNull(contentArr[7]));
							orgInfo.setCertificateStart(notNull(contentArr[8]));
							orgInfo.setCertificateEnd(notNull(contentArr[9]));
							orgInfo.setAddress(notNull(contentArr[10]));
							orgInfo.setCorporate(notNull(contentArr[11]));
							orgInfo.setContacts(notNull(contentArr[12]));
							orgInfo.setContactsNo(notNull(contentArr[13]));
							index++;
							list.add(orgInfo);
						}
					}
					br.close();
					fi.close();
				} else {
					logger.error("--EmployeeInfoTask--驾驶员静态信息同步任务异常,文件不存在:" + conf.get("filePath"));
				}
			}
			
			long s2 = System.currentTimeMillis();
			if(list.size() > 0){
				String head = "CAITS 1_2_0 "+conf.get("dispatchCode")+" 4 L_PLAT {TYPE:D_INFO,TEXT:COMPANY|";
				if(clearFlag){
					clearFlag = false;
				}
                for(OrgInfo orgInfo:list){
                	String str = head+Base64_URl.base64Encode(getStr(orgInfo))+"} \r\n";
                	readContentFromPost(str,conf.get("mccUrl"));
                }

				list.clear();
			
			}
			long end = System.currentTimeMillis();
			logger.info("--OrgInfoTask--企业静态信息同步任务结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, 文件写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s2 - s1,end - s2, end -start);
		} catch (Exception e) {	
			logger.error("--OrgInfoTask--企业静态信息同步任务异常:" + e.getMessage(), e);
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("--OrgInfoTask--oracle关闭异常："+e.getMessage(),e);
			}
		}
		authUtc = start - 60000;
	}
	/**
	 

	/**
	 * 获得拼接字符串
	 * 
	 */
	public static String getStr(OrgInfo orgInfo){
		String str = "ORG_CA:="+orgInfo.getOrgCode()+
				 ";NAME:="+orgInfo.getOrgName()+
				 ";TRANS_TYPE:="+orgInfo.getTradeCode()+
				 ";UP_CA:="+orgInfo.getEntOrgCode()+
				 ";MANAGE_CA:="+orgInfo.getManageOrgCode()+
				 ";ZONE:="+orgInfo.getOrigin()+
				 ";BUSINESS_NUMBER:="+orgInfo.getBusinessCertificateCode()+
				 ";BUSINESS_SCOPE:="+orgInfo.getBusinessScope()+
				 ";VALIDITY_BEGIN:="+orgInfo.getCertificateStart()+
				 ";VALIDITY_END:="+orgInfo.getCertificateEnd()+
				 ";ADDRESS:="+orgInfo.getAddress()+
				 ";CORPORATION:="+orgInfo.getCorporate()+
				 ";LINKMAN:="+orgInfo.getContacts()+
				 ";TELEPHONE:="+orgInfo.getContactsNo();
		return str;
	}
	/**
	 * 获得非null的字符串
	 * @param str
	 * @return
	 */
	public static String notNull(String str){
		if(str != null){
			return str;
		}
		return "";
	}
	
	/**
	 * 发送mcc
	 * @return
	 * @throws IOException
	 */
	public void readContentFromPost(String content,String url) throws IOException {   
	    String ret = "";
	  	URL postUrl = new URL(url); 
        HttpURLConnection connection = (HttpURLConnection) postUrl.openConnection(); 
        connection.setDoOutput(true);               
        connection.setDoInput(true); 

        // 设置请求方式，默认为GET 
        connection.setRequestMethod("POST"); 
        // Post 请求不能使用缓存 
        connection.setUseCaches(false); 

        // URLConnection.setFollowRedirects是static 函数，作用于所有的URLConnection对象。 

        // connection.setFollowRedirects(true); 
        //URLConnection.setInstanceFollowRedirects 是成员函数，仅作用于当前函数 
        connection.setInstanceFollowRedirects(true);
        connection.setRequestProperty("Content-Type", "gbk");
        // 连接，从postUrl.openConnection()至此的配置必须要在 connect之前完成， 
        // 要注意的是connection.getOutputStream()会隐含的进行调用 connect()，所以这里可以省略 
        //connection.connect(); 
        DataOutputStream out = new DataOutputStream(connection.getOutputStream()); 

        //正文内容其实跟get的URL中'?'后的参数字符串一致 
        // DataOutputStream.writeBytes将字符串中的位的 unicode字符以位的字符形式写道流里面 
        out.writeBytes(content); 
        out.flush(); 
        out.close(); // flush and close       
        BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream())); 
        String line; 
        while ((line = reader.readLine()) != null) { 
        	ret += Base64_URl.base64Decode(line); 
        } 
        reader.close();
        logger.info("发送MCC返回结果："+ret);
        connection.disconnect(); 
	} 
}
