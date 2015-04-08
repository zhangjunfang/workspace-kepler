package com.ctfo.syn.kcpt_oracle2mysql;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.HashMap;

import org.apache.log4j.Logger;

import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

public class SynDataSQL {
	public static Logger logger = Logger.getLogger(SynDataSQL.class);
	
	private HashMap<String,String> driverMap = new HashMap<String,String>();
	
	public SynDataSQL() {
	}

	public void syn(){
		synDriver();
		synVehicle(); 
		//synOrganization();
	}
	
	/*****
	 * 初始化同步所有车辆
	 */
	public void synVehicle() {
		Connection conn = null;
		Connection mysqlConn = null;
		SynPool synPool = SynPool.getinstance();
//		Jedis jedis = null;
		PreparedStatement mysqlstmt = null;
		JedisUnifiedStorageService jedis = null;
		try {
			long starttime = System.currentTimeMillis();
			jedis = RedisServer.getJedisServiceInstance();
//			jedis = RedisConnectionPool.getJedisConnection();
//			jedis.select(1); // 连接位置信息库
			
			conn = OracleConnectionPool.getConnection();
			
			logger.info("oracle连接成功");
			
			// mysql连接
			Class.forName(synPool.getSql("mysql_driverClass"));
			mysqlConn = DriverManager.getConnection(synPool.getSql("mysql_jdbcUrl"),synPool.getSql("mysql_user"),
					synPool.getSql("mysql_password"));
			
			mysqlConn.prepareStatement("DELETE FROM MEM_TB_VEHICLE").execute(); //清空历史
			
			mysqlstmt = mysqlConn.prepareStatement(synPool.getSql("kcpt_mysql_tb_vehicle"));
			
			logger.info("mysql连接成功");
			
			int i = 0;
			int count =0;
			// 车辆基本信息
			Statement stmt1 = conn.createStatement();
			
			ResultSet rset1 = stmt1.executeQuery(synPool.getSql("kcptsql_tbserviceview"));
			StringBuffer strBuf = null;
			while (rset1.next()) {
				try {
					String vid = String.valueOf(rset1.getLong("VID"));
					strBuf = new StringBuffer();
					strBuf.append(rset1.getLong("LAT")); // 0
					strBuf.append(":"); 
					strBuf.append(rset1.getLong("LON")); // 1
					strBuf.append(":");
					strBuf.append(rset1.getLong("MAPLON")); // 2
					strBuf.append(":");
					strBuf.append(rset1.getLong("MAPLAT")); // 3
					strBuf.append(":");
					strBuf.append(rset1.getLong("GPS_SPEED")); // 4
					strBuf.append(":");
					strBuf.append(rset1.getLong("DIRECTION")); // 5
					strBuf.append(":");
					strBuf.append(rset1.getLong("UTC")); // 6
					strBuf.append(":");
					if(null != rset1.getString("ALARM_CODE")){
						strBuf.append(rset1.getString("ALARM_CODE")); // 7
					}
					
					strBuf.append(":");
					if (rset1.getLong("ENGINE_ROTATE_SPEED") >= -1) { // 引擎转速（发动机转速） // 8
						strBuf.append(rset1.getLong("ENGINE_ROTATE_SPEED"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("OIL_INSTANT") >= -1) { // 瞬时油耗 // 9
						strBuf.append(rset1.getLong("OIL_INSTANT"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("OIL_PRESSURE") >= -1) { // 机油压力 // 10
						strBuf.append(rset1.getLong("OIL_PRESSURE"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("OIL_TEMPERATURE") >= -1) { // 机油温度（随位置汇报上传） 11
						strBuf.append(rset1.getLong("OIL_TEMPERATURE"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("EEC_APP") >= -1) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传） 12
						strBuf.append(rset1.getLong("EEC_APP"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("OIL_TOTAL") >= -1) { // 累计油耗 13
						strBuf.append(rset1.getLong("OIL_TOTAL"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("ENGINE_WORKING_LONG") >= -1) { // 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）14
						strBuf.append(rset1.getLong("ENGINE_WORKING_LONG"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("AIR_INFLOW_TPR") >= -1) { // 进气温度（随位置汇报上传）15
						strBuf.append(rset1.getLong("AIR_INFLOW_TPR"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("AIR_PRESSURE") >= -1) { // 大气压力（随位置汇报上传）16
						strBuf.append(rset1.getLong("AIR_PRESSURE"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("VEHICLE_SPEED") > 0) { // 脉冲车速（随位置汇报上传）17
						strBuf.append(rset1.getLong("VEHICLE_SPEED"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("BATTERY_VOLTAGE") >= -1) { // 终端内置电池电压（随位置汇报上传）18
						strBuf.append(rset1.getLong("BATTERY_VOLTAGE"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("E_WATER_TEMP") >= -1) { // 冷却液温度（随位置汇报上传）19
						strBuf.append(rset1.getLong("E_WATER_TEMP"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("EXT_VOLTAGE") >= -1) { // 车辆蓄电池电压（随位置汇报上传）20
						strBuf.append(rset1.getLong("EXT_VOLTAGE"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("E_TORQUE") >= -1) { // 发动机扭矩（随位置汇报上传）21
						strBuf.append(rset1.getLong("E_TORQUE"));
					}
					strBuf.append(":");
					
					if (rset1.getLong("MILEAGE") >= -1) { // 累计里程 22
						strBuf.append(rset1.getLong("MILEAGE"));
					}
					strBuf.append(":");
					
					if(null != rset1.getString("BASESTATUS")){ //基本状态位 23
						strBuf.append(rset1.getString("BASESTATUS"));
					}
					strBuf.append(":");
					
					if(null != rset1.getString("EXTENDSTATUS")){ //扩展状态位 24
						strBuf.append(rset1.getString("EXTENDSTATUS"));
					}
		
					strBuf.append(":");
					if(null != rset1.getString("SPEED_FROM")){
						strBuf.append(rset1.getString("SPEED_FROM")); // 25车速来源
					}
					strBuf.append(":");
				
					if(rset1.getLong("PRECISE_OIL") >= -1){ //计量仪油耗 // 26
						strBuf.append(rset1.getLong("PRECISE_OIL"));
					}
					strBuf.append(":");
					
					if(rset1.getLong("ELEVATION") >= -1){
						strBuf.append(rset1.getLong("ELEVATION")); // 海拔 27
					}
					
					strBuf.append(":");
					
					if(rset1.getLong("OIL_MEASURE") >= -1){
						strBuf.append(rset1.getLong("OIL_MEASURE")); // 油箱存油量(升) 28
					}
					
					strBuf.append(":");
					
					strBuf.append(vid); // vid 29
					
					strBuf.append(":");
					
					strBuf.append(rset1.getInt("PLATE_COLOR_ID")); // 车牌颜色 30
					
					strBuf.append(":");
					
					strBuf.append(rset1.getString("VEHICLENO")); // 车牌号 31
					
					strBuf.append(":");
					
					strBuf.append(rset1.getString("COMMADDR")); // 手机号 32
					
					strBuf.append(":");
					
					strBuf.append(rset1.getInt("TID")); // 终端ID 33
					
					strBuf.append(":");
					
					strBuf.append(rset1.getString("T_MAC")); // 终端型号 34
					
					strBuf.append(":");
					
					if(driverMap.containsKey(vid)){
						strBuf.append(driverMap.get(vid)); // 驾驶员姓名 35
					}
					strBuf.append(":");
					
					strBuf.append(rset1.getString("CORP_NAME"));  //所属组织 36
					
					strBuf.append(":");
					
					strBuf.append(rset1.getInt("TEAM_ID")); // 车队id 37
					
					strBuf.append(":");
					
					strBuf.append(rset1.getInt("CORP_ID")); // 企业id 38
					
					strBuf.append(":");
					
					strBuf.append(rset1.getString("OEM_CODE")); // OEMCODE 39
					
					strBuf.append(":");
					
					strBuf.append(rset1.getLong("SYSUTC")); // 系统时间40
					strBuf.append(":");
					
					strBuf.append(rset1.getInt("ISONLINE")); //  是否在线41
					
					strBuf.append(":");
					
					strBuf.append(rset1.getInt("ISVALID")); //  是否有效 42
					
					strBuf.append(":");
					
					if(null != rset1.getString("MSGID")){
						strBuf.append(rset1.getString("MSGID")); //  MSGID 43
					}
					
					strBuf.append(":");
					
					if(null != rset1.getString("TEAM_NAME")){
						strBuf.append(rset1.getString("TEAM_NAME")); //  TEAM_NAME 车队名称  44
					}
					
					
					
					count++;
					jedis.saveVehicleInfo(vid, strBuf.toString());
					strBuf.delete(0, strBuf.length());
					strBuf = null;
					
					mysqlstmt.setLong(1, Long.parseLong(vid));
					mysqlstmt.setLong(2, rset1.getInt("CORP_ID"));
					mysqlstmt.setString(3, rset1.getString("CORP_NAME"));
					mysqlstmt.setLong(4, rset1.getLong("TEAM_ID"));
					mysqlstmt.setString(5, rset1.getString("TEAM_NAME"));
					mysqlstmt.setString(6, rset1.getString("VEHICLENO"));
					mysqlstmt.setInt(7, rset1.getInt("PLATE_COLOR_ID"));
					mysqlstmt.setString(8, rset1.getString("AREA_CODE"));
					mysqlstmt.setString(9, rset1.getString("OEM_CODE"));
					mysqlstmt.setString(10, rset1.getString("COMMADDR"));
					
					mysqlstmt.addBatch();
					i++;
					if(i % 500 == 0){
						mysqlstmt.executeBatch();
						i = 0;
					}
					
				} catch (Exception e) {
					logger.error("初始化错误:",e);
				}
			}//End while

			if(i > 0)
			mysqlstmt.executeBatch();
			i = 0;
			
			logger.info("车辆基本信息同步成功" + count);
		
			logger.debug("已同步" + count + "车辆,耗时"
					+ (System.currentTimeMillis() - starttime) / 1000
					+ "秒");
		} catch (Exception e) {
			logger.error("初始化错误:",e);
		}finally{
			try {
				if(conn !=null){
					conn.close();
				}
				
				if( null != mysqlConn){
					mysqlConn.close();
				}
				if(null != mysqlstmt){
					mysqlstmt.close();
				}
				
			} catch (SQLException e) {
				logger.error(e);
			}
			
			driverMap.clear();
			
//			if(null != jedis){
//				RedisConnectionPool.returnJedisConnection(jedis);
//			}
		}
	}
	
	/*****
	 * 同步驾驶员信息
	 */
	private void synDriver(){
		Connection conn = null;
		SynPool synPool = SynPool.getinstance();
		try{
			conn = OracleConnectionPool.getConnection();
			logger.info("oracle连接成功");
			
			// 驾驶员基本信息
			Statement stmt1 = conn.createStatement();
			
			ResultSet rset1 = stmt1.executeQuery(synPool.getSql("kcptsql_driver"));
		
			while (rset1.next()) {
				String vid = String.valueOf(rset1.getLong("VID"));
				driverMap.put(vid, rset1.getString("CNAME"));
			}// End while
			
		} catch (SQLException e) {
			logger.error("Operating oracle to fail.",e);
		}finally{
			if(conn !=null){
				try {
					conn.close();
				} catch (SQLException e) {
					logger.error(e);
				}
			}
		}
	}
	
	
	
	/*****
	 * 初始化同步所有组织
	 */
	private void synOrganization(){
		Connection oraclecon = null;
		Connection mysqlcon = null;
		Statement oraclestmt = null;
		PreparedStatement mysqlstmt = null;
		ResultSet oraclestmtrset = null;
		SynPool synPool = SynPool.getinstance();
		// mysql连接
		try {
			Class.forName(synPool.getSql("mysql_driverClass"));
			mysqlcon = DriverManager.getConnection(synPool.getSql("mysql_jdbcUrl"), synPool.getSql("mysql_user"),synPool.getSql("mysql_password"));
			mysqlstmt = mysqlcon.prepareStatement(synPool.getSql("mysql_organization"));
			// 从ORACLE连接池获取连接
			oraclecon = OracleConnectionPool.getConnection();
			int i = 0;
			int count =0;
			long starttime = System.currentTimeMillis();
			// 同步车辆数据
			oraclestmt = oraclecon.createStatement();
			oraclestmtrset = oraclestmt.executeQuery(synPool.getSql("oracle_organization"));
			while(oraclestmtrset.next()){
				mysqlstmt.setLong(1,oraclestmtrset.getLong("ENT_ID"));
				mysqlstmt.setString(2,oraclestmtrset.getString("ENT_NAME"));
				mysqlstmt.setInt(3, oraclestmtrset.getInt("ENT_TYPE"));
				mysqlstmt.setLong(4, oraclestmtrset.getLong("PARENT_ID"));
				mysqlstmt.setLong(5, oraclestmtrset.getLong("CREATE_BY"));
				mysqlstmt.setLong(6, oraclestmtrset.getLong("CREATE_TIME"));
				mysqlstmt.setLong(7, oraclestmtrset.getLong("UPDATE_BY"));
				mysqlstmt.setLong(8, oraclestmtrset.getLong("UPDATE_TIME"));
				mysqlstmt.setString(9, oraclestmtrset.getString("ENABLE_FLAG"));
				mysqlstmt.setString(10, oraclestmtrset.getString("ENT_STATE"));
				mysqlstmt.setString(11, oraclestmtrset.getString("MEMO"));
				mysqlstmt.setString(12, oraclestmtrset.getString("SEQ_CODE"));
				mysqlstmt.setString(13, formatURL(oraclestmtrset.getString("URL")));
				mysqlstmt.setLong(14,oraclestmtrset.getLong("ENT_ID"));
				mysqlstmt.setString(15,oraclestmtrset.getString("ENT_NAME"));
				mysqlstmt.setInt(16, oraclestmtrset.getInt("ENT_TYPE"));
				mysqlstmt.setLong(17, oraclestmtrset.getLong("PARENT_ID"));
				mysqlstmt.setLong(18, oraclestmtrset.getLong("CREATE_BY"));
				mysqlstmt.setLong(19, oraclestmtrset.getLong("CREATE_TIME"));
				mysqlstmt.setLong(20, oraclestmtrset.getLong("UPDATE_BY"));
				mysqlstmt.setLong(21, oraclestmtrset.getLong("UPDATE_TIME"));
				mysqlstmt.setString(22, oraclestmtrset.getString("ENABLE_FLAG"));
				mysqlstmt.setString(23, oraclestmtrset.getString("ENT_STATE"));
				mysqlstmt.setString(24, oraclestmtrset.getString("MEMO"));
				mysqlstmt.setString(25, oraclestmtrset.getString("SEQ_CODE"));
				mysqlstmt.setString(26, formatURL(oraclestmtrset.getString("URL")));
				mysqlstmt.addBatch();
				count++;
				i++;
				if(i % 500 == 0){
					mysqlstmt.executeBatch();
					i = 0;
				}
			}// End while
			
			if(count > 0){
				mysqlstmt.executeBatch();
			}

			logger.info("同步组织数据成功：" + String.valueOf(count) + "条,"+ (System.currentTimeMillis() - starttime) + "毫秒");

		} catch (Exception e) {
			logger.error("同步组织表数据失败：",e);
		}finally{
			try{
				if(mysqlstmt != null){
					mysqlstmt.close();
				}
				
				if(oraclestmtrset != null){
					oraclestmtrset.close();
				}
				if(oraclestmt != null){
					oraclestmt.close();
				}
				
				if(mysqlcon !=null){
					mysqlcon.close();
				}
				
				if(oraclecon != null){
					oraclecon.close();
				}
			
			}catch(Exception ex){
				logger.error("CLOSED PROPERTIES,ORACLE AND MYSQL TO FAIL.",ex);
			}
		}
	}
	
	private String formatURL(String url){
		String[] arr = url.split(",");
		StringBuffer newUrl = new StringBuffer("#");
		for(int i= arr.length -1; i >=0; i-- ){
			newUrl.append(arr[i]);
			newUrl.append("#");
		}//End for
		return newUrl.toString();
	}
}
