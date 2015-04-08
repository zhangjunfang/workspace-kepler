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

import com.ctfo.syncservice.model.VehicleInfo;
import com.ctfo.syncservice.util.Base64_URl;
import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：四川车辆同步
 * <li>时        间：2014-07-16  下午14:55:09	</li><br>
 * 
 * @author huangjincheng
 *
 */
public class VehicleInfoTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(VehicleInfoTask.class);
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
			//setName("VehicleInfoTask11");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			execute();
		} catch (Exception e) {
			logger.error("初始化车辆静态信息同步任务异常:" + e.getMessage(), e); 
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
			ArrayList<VehicleInfo> list =  new ArrayList<VehicleInfo>();
			long s1 = System.currentTimeMillis();
			if("VehicleInfoTask_byOracle".equals(name)){
				conn = this.oracle.getConnection();		
				if(clearFlag){
					ps = conn.prepareStatement(conf.get("sql_vehicleInfoListAll"));
				} else {
					ps = conn.prepareStatement(conf.get("sql_vehicleInfoListIncremental"));
					ps.setLong(1, lastUtc);
					ps.setLong(2, lastUtc);
				}
				rs = ps.executeQuery();
				
				while (rs.next()) {
					try {
						VehicleInfo vehicleInfo = new VehicleInfo();
						vehicleInfo.setVin(notNull(rs.getString("VIN_CODE")));
						vehicleInfo.setPlate(notNull(rs.getString("VEHICLE_NO")));
						vehicleInfo.setColor(notNull(rs.getString("PLATE_COLOR")));
						
						
						vehicleInfo.setOrigin(notNull(rs.getString("CITY_ID")));
						vehicleInfo.setType(notNull(rs.getString("PROD_CODE")));
						
						vehicleInfo.setTransNo(notNull(rs.getString("ROAD_TRANSPORT")));
						vehicleInfo.setBusinessScope(notNull(rs.getString("CERTIFICATE_TYPE")));
						
						
						vehicleInfo.setSeatTon(notNull(rs.getString("MAXIMAL_PEOPLE")));
						vehicleInfo.setMotorNo(notNull(rs.getString("ENGINE_NO")));
						vehicleInfo.setOwner(notNull(rs.getString("STAFF_NAME")));
						vehicleInfo.setOwnerTel(notNull(rs.getString("COMMADDR")));
	
						
						list.add(vehicleInfo);
						index++;
					}  catch (Exception e) {
						logger.error("--VehicleInfoTask-- Oracle取静态信息异常:" + e.getMessage(), e);
						list.clear();
						error++;
						continue;
					}
				}
			} else if ("VehicleInfoTask_byFile".equals(name)) {
				String str = "";
				File file = new File(conf.get("filePath"));
				if (file.exists()) {
					FileInputStream fi = new FileInputStream(file);
					BufferedReader br = new BufferedReader(new InputStreamReader(fi, "gbk"));
					while ((str = br.readLine()) != null) {
						String[] contentArr = str.split(";");
						long utc = Long.parseLong(contentArr[18].trim());
						// System.out.println("utc:"+utc+":"+lastUtc+":"+System.currentTimeMillis());
						if (utc > lastUtc || clearFlag) {
							VehicleInfo vehicleInfo = new VehicleInfo();
							vehicleInfo.setVin(notNull(contentArr[0]));
							vehicleInfo.setPlate(notNull(contentArr[1]));
							vehicleInfo.setColor(notNull(contentArr[2]));
							vehicleInfo.setOrgCode(notNull(contentArr[3]));
							vehicleInfo.setManageOrgCode(notNull(contentArr[4]));
							vehicleInfo.setOrigin(notNull(contentArr[5]));
							vehicleInfo.setType(notNull(contentArr[6]));
							vehicleInfo.setOption(notNull(contentArr[7]));
							vehicleInfo.setTransNo(notNull(contentArr[8]));
							vehicleInfo.setBusinessScope(notNull(contentArr[9]));
							vehicleInfo.setCertificateStart(notNull(contentArr[10]));
							vehicleInfo.setCertificateEnd(notNull(contentArr[11]));
							vehicleInfo.setSeatTon(notNull(contentArr[12]));
							vehicleInfo.setMotorNo(notNull(contentArr[13]));
							vehicleInfo.setOwner(notNull(contentArr[14]));
							vehicleInfo.setOwnerTel(notNull(contentArr[15]));
							vehicleInfo.setPhotoParam(notNull(contentArr[16]));
							vehicleInfo.setVedioParam(notNull(contentArr[17]));
							index++;
							list.add(vehicleInfo);
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
				String head = "CAITS 1_2_0 "+conf.get("dispatchCode")+" 4 L_PLAT {TYPE:D_INFO,TEXT:VEHICLE|";
				if(clearFlag){
					clearFlag = false;
				}
                for(VehicleInfo VehicleInfo:list){
                	String str = head+Base64_URl.base64Encode(getStr(VehicleInfo))+"} \r\n";
                	readContentFromPost(str,conf.get("mccUrl"));
                }
				list.clear();			
			}
			long end = System.currentTimeMillis();
			logger.info("--VehicleInfoTask--车辆静态信息同步任务结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, 文件写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s2 - s1,end - s2, end -start);
		} catch (Exception e) {	
			logger.error("--VehicleInfoTask--车辆静态信息同步任务异常:" + e.getMessage(), e);
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
				logger.error("--VehicleInfoTask--oracle关闭异常："+e.getMessage(),e);
			}
		
		}
		authUtc = start ;
	}
	 

	/**
	 * 获得拼接字符串                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 b  
	 * 
	 */
	public static String getStr(VehicleInfo vehicleInfo){
		String str = "VIN:="+vehicleInfo.getVin()+
				 ";VEHICLE_PLATE:="+vehicleInfo.getPlate()+
				 ";PLATE_COLOR:="+vehicleInfo.getColor()+
				 ";ORG_CA:="+vehicleInfo.getOrgCode()+
				 ";MANAGE_CA:="+vehicleInfo.getManageOrgCode()+
				 ";ZONE:="+vehicleInfo.getOrigin()+
				 ";VEHICLE_TYPE:="+vehicleInfo.getType()+
				 ";OPTIONAL:="+vehicleInfo.getOption()+
				 ";TRANS_NO:="+vehicleInfo.getTransNo()+
				 ";BUSINESS_SCOPE:="+vehicleInfo.getBusinessScope()+
				 ";VALIDITY_BEGIN:="+vehicleInfo.getCertificateStart()+
				 ";VALIDITY_END:="+vehicleInfo.getCertificateEnd()+
				 ";SEAT_TON:="+vehicleInfo.getSeatTon()+
				 ";MOTOR_NO:="+vehicleInfo.getMotorNo()+
				 ";OWNER:="+vehicleInfo.getOwner()+
				 ";OWNER_TEL:="+vehicleInfo.getOwnerTel()+
				 ";PHOTO_PARAM:="+vehicleInfo.getPhotoParam()+
				 ";VEDIO_PARAM:="+vehicleInfo.getVedioParam();
				    
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
	public static void readContentFromPost(String content,String url) throws IOException {   
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
