package com.ctfo.storage.command.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.dao.MySqlDataSource;
import com.ctfo.storage.command.model.SysAreaInfoModel;
import com.ctfo.storage.command.model.SysGeneralCodeModel;

/**
 * MySqlService
 * 
 * 
 * @author huangjincheng
 * 2014-5-15上午10:15:38
 * 
 */
public class MySqlService {
	private static final Logger logger = LoggerFactory.getLogger(MySqlService.class);
	
	private static String GET_VID_SQL = "select tr.vid from TB_SIM tb,TR_SERVICEUNIT tr where tb.sid = tr.sid and tb.commaddr = ?" ;
	private static String GET_VEHICLENO_SQL = "select tv.vehicle_no from TB_SIM tb,TR_SERVICEUNIT tr ,TB_VEHICLE tv where tb.sid = tr.sid and tr.vid = tv.vid and tb.commaddr = ?";
	private static String GET_OEMCODE_SQL = "select tt.oem_code from TB_SIM tb,TR_SERVICEUNIT tr ,TB_TERMINAL tt where  tr.tid = tt.tid and tb.sid = tr.sid and tb.commaddr = ?";
	private Connection conn = null;
	
	private PreparedStatement statement = null;
	
	
	
	
	
	/**
	 * 通过手机号获取VID
	 * @param sql
	 * @param tbDvr3G
	 */
	public String getVidByPhone(String phoneNo){
		String str = "" ;
		try {		
			conn = MySqlDataSource.getConnection();
			statement = conn.prepareStatement(GET_VID_SQL);
			statement.setString(1, phoneNo);
			ResultSet rs = null;
			rs = statement.executeQuery();
			while(rs.next()){
				str = rs.getString("VID");
			}
			
				
		} catch (SQLException e) {
			logger.error("通过手机号获取VID异常："+e.getMessage(), e); 					
		}finally{
			try {
				if(statement != null){
					statement.close();
				}
				if(conn != null){
					conn.close();
				}
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		return str;
	}
	
	/**
	 * 通过手机号获取车牌号
	 * @param phoneNo
	 * @return
	 */
	public String getVehicleNoByPhone(String phoneNo){
		String str = "" ;
		try {		
			conn = MySqlDataSource.getConnection();
			statement = conn.prepareStatement(GET_VEHICLENO_SQL);
			statement.setString(1, phoneNo);
			ResultSet rs = null;
			rs = statement.executeQuery();
			while(rs.next()){
				str = rs.getString("VEHICLE_NO");
			}
			
				
		} catch (SQLException e) {
			logger.error("通过手机号获取VEHICLE_NO异常："+e.getMessage(), e); 					
		}finally{
			try {
				if(statement != null){
					statement.close();
				}
				if(conn != null){
					conn.close();
				}
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		return str;
	}
	/**
	 * 通过手机号获取oemcode
	 * @param phoneNo
	 * @return
	 */
	public String getOemCodeByPhone(String phoneNo){
		String str = "" ;
		try {		
			conn = MySqlDataSource.getConnection();
			statement = conn.prepareStatement(GET_OEMCODE_SQL);
			statement.setString(1, phoneNo);
			ResultSet rs = null;
			rs = statement.executeQuery();
			while(rs.next()){
				str = rs.getString("OEM_CODE");
			}
			
				
		} catch (SQLException e) {
			logger.error("通过手机号获取OEM_CODE异常："+e.getMessage(), e); 					
		}finally{
			try {
				if(statement != null){
					statement.close();
				}
				if(conn != null){
					conn.close();
				}
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		return str;
	}
	/**
	 * TODO
	 */
	public void generageArea() {
		long start = System.currentTimeMillis();
		int index = 0;
		int error = 0;	
		RedisService redisService = new RedisService();
		Connection areaconn = null;
		Connection codeconn = null;
		PreparedStatement mysqlstmt = null;
		PreparedStatement areastmt = null;
		ResultSet mysqlstmtrsetCode = null;
		ResultSet mysqlstmtrsetAreal = null;
		String jsonResult = "";
		List<SysGeneralCodeModel> codeList = new ArrayList<SysGeneralCodeModel>();
		List<SysAreaInfoModel> areaList = new ArrayList<SysAreaInfoModel>();
		try {
			codeconn = MySqlDataSource.getConnection();
			jsonResult += "{";
			String sqlGeneral ="select t.general_code,t.code_name,t.parent_general_code from SYS_GENERAL_CODE t where t.enable_flag !=0";
			String sqlArea = "select t.area_code,t.area_name,t.area_level from SYS_AREA_INFO t where t.area_level in (0,1) order by area_code";
	

//			logger.debug("mysql数据库连接成功");
			// 同步车辆数据
			mysqlstmt = codeconn.prepareStatement(sqlGeneral);
			mysqlstmtrsetCode = mysqlstmt.executeQuery(); 
//			mysqlstmtrsetCode = mysqlstmt.executeQuery(sqlGeneral);
			
			while (mysqlstmtrsetCode.next()) {
				SysGeneralCodeModel generalBean = new SysGeneralCodeModel();
				String generalCode=mysqlstmtrsetCode.getString("GENERAL_CODE");
				String codeName =mysqlstmtrsetCode.getString("CODE_NAME");
				String parentGeneralCode=mysqlstmtrsetCode.getString("PARENT_GENERAL_CODE");
				generalBean.setGeneralCode(generalCode);
				generalBean.setCodeName(codeName);
				generalBean.setParentGeneralCode(parentGeneralCode);
				codeList.add(generalBean);
				index++;
			}// End while
			if(codeList != null && codeList.size()>0){
				jsonResult += getGeneralCodeJson(codeList);
			}
			
			/***获取行政区域编码**/
			areaconn = MySqlDataSource.getConnection();
			areastmt = areaconn.prepareStatement(sqlArea);
			mysqlstmtrsetAreal = areastmt.executeQuery();
//			mysqlstmtrsetAreal = mysqlstmt.executeQuery(sqlArea);
			
			while (mysqlstmtrsetAreal.next()) {
				SysAreaInfoModel arealBean = new SysAreaInfoModel();
				String areaCode=mysqlstmtrsetAreal.getString("AREA_CODE");
				String areaName =mysqlstmtrsetAreal.getString("AREA_NAME");
				int areaLevel =mysqlstmtrsetAreal.getInt("AREA_LEVEL");
				arealBean.setAreaName(areaName);
				arealBean.setAreaCode(areaCode);
				arealBean.setAreaLevel(areaLevel);
				areaList.add(arealBean);
			}// End while
			if(areaList != null  && areaList.size()>0){
				if(!jsonResult.trim().equals("{")){
					jsonResult +=",";
				}
				jsonResult += getAreaInfoJson(areaList);
			}
			jsonResult +="}";
			redisService.saveGeneralCode(jsonResult);
			long end = System.currentTimeMillis();
			logger.info("--syncGeneralCode--同步码表信息同步结束, 处理数据:({})条, 正常处理:({})条, 异常:({})条, 总耗时:[{}]ms", (index + error), index, error, (end -start));
		}catch (Exception e) {
			logger.error("获取码表数据失败：",e);
		}finally{
			try{
				if(mysqlstmtrsetCode != null){
					mysqlstmtrsetCode.close();
				}
				if(mysqlstmtrsetAreal != null){
					mysqlstmtrsetAreal.close();
				}
				if(mysqlstmt != null){
					mysqlstmt.close();
				}
				if(areastmt != null){
					areastmt.close();
				}
				if(areaconn != null){
					areaconn.close();
				}
				if(codeconn != null){
					codeconn.close();
				}
			}catch(Exception ex){
				logger.error("同步码表信息同步异常:"+ ex.getMessage(),ex);
			}
			if(codeList !=null && codeList.size() > 0){
				codeList.clear();
				codeList = null;
			}
			
			if(areaList != null && areaList.size() > 0){
				areaList.clear();
				areaList = null;
			}
		}
	}
	
	/**
	 * 
	 * 得到区域编码json数据
	 * 
	 */
	private String getAreaInfoJson(List<SysAreaInfoModel> list) {
		if (list.size() == 0)
			return "";
		List<SysAreaInfoModel> rootList = new ArrayList<SysAreaInfoModel>();
		List<SysAreaInfoModel> chileList = new ArrayList<SysAreaInfoModel>();
		try{
			for (int i=0;i<list.size();i++) {
				SysAreaInfoModel info = list.get(i);
				if (info.getAreaLevel() == 0) {
					rootList.add(info);
				} else {
					chileList.add(info);
				}
			} // End for
			return creatAreaInfoJson(rootList, chileList);
		}finally{
			if(rootList != null && rootList.size() > 0){
				rootList.clear();
			}
			if(chileList != null && chileList.size() > 0){
				chileList.clear();
			}
		}
	}
	/**
	 * 创建区域编码json
	 * @param rootList
	 * @param chileList
	 * @return
	 */
	private String creatAreaInfoJson(List<SysAreaInfoModel> rootList, List<SysAreaInfoModel> chileList) {
		StringBuilder sb = new StringBuilder();
		sb.append("'SYS_AREA_INFO':[");
		for (SysAreaInfoModel info : rootList) {
			sb.append("{'code':'").append(info.getAreaCode()).append("',");
			sb.append("'name':'").append(info.getAreaName()).append("',");
			sb.append("children:[");
			String cJson = this.getChileAreaInfoJson(chileList, info.getAreaCode());
			sb.append(cJson).append("]},");
		} // End for
		return sb.substring(0, sb.length() - 1)+"]";
	}
	
	/**
	 * 
	 * 递归得到省下的市
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileAreaInfoJson(List<SysAreaInfoModel> chileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i=0;i<chileList.size();i++) {
			SysAreaInfoModel info = chileList.get(i);
			String rtCode = code.substring(0, 2);
			String ctCode = info.getAreaCode().substring(0, 2);
			if (ctCode.equals(rtCode)){
				sb.append("{'code':'").append(info.getAreaCode()).append("',");;
				sb.append("'name':'").append(info.getAreaName()).append("'},");
			}
		} // End for
		
		if(sb != null && !"".equals(sb)&& sb.length()>0){
			return sb.substring(0, sb.length() - 1);
		}else{
			return "";
		}
	}
	
	/**
	 * 得到通用编码json 数据
	 */
	private String getGeneralCodeJson(List<SysGeneralCodeModel> list) {
		if (list.size() == 0)
			return "";
		List<SysGeneralCodeModel> rootList = new ArrayList<SysGeneralCodeModel>();
		List<SysGeneralCodeModel> chileList = new ArrayList<SysGeneralCodeModel>();
		try{
			for (int i=0;i<list.size();i++) {
				SysGeneralCodeModel info =list.get(i);
				if (info.getParentGeneralCode().equals("0")) {
					rootList.add(info);
				} else {
					chileList.add(info);
				}
			}// End for
	
			return creatGeneralCodeJson(rootList, chileList);
		}finally{
			if(rootList != null && rootList.size() > 0){
				rootList.clear();
				rootList = null;
			}
			
			if(chileList != null && chileList.size() > 0){
				chileList.clear();
				chileList = null;
			}
		}
	}
	/**
	 * 创建通用编码 json
	 * @param rootList
	 * @param chileList
	 * @return
	 */
	private String creatGeneralCodeJson(List<SysGeneralCodeModel> rootList, List<SysGeneralCodeModel> chileList) {
		StringBuilder sb = new StringBuilder();
		for (SysGeneralCodeModel info : rootList) {
			sb.append("'").append(info.getGeneralCode()).append("':[");
			String cJson = getChileGeneralCodeJson(chileList, info.getGeneralCode());
			sb.append(cJson).append("],");
		}// End for
		return sb.substring(0, sb.length() - 1);
	}
	
	/**
	 * 递归得到通用编码子类型
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileGeneralCodeJson(List<SysGeneralCodeModel> chileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i=0;i<chileList.size();i++) {
			SysGeneralCodeModel info = chileList.get(i);
			String pCode = info.getParentGeneralCode();
			if (pCode.equals(code)){
				sb.append("{'code':'").append(info.getGeneralCode()).append("',");;
				sb.append("'name':'").append(info.getCodeName()).append("'},");
			}
		}// End for
		if(sb != null && !"".equals(sb)&& sb.length()>0){
			return sb.substring(0, sb.length() - 1);
		}else{
			return "";
		}
	}
}
