package com.ctfo.syn.kcpt_oracle2mysql;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.apache.commons.lang.StringUtils;
import org.apache.log4j.Logger;

import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

public class SynData implements Runnable {
	public static Logger logger = Logger.getLogger(SynData.class);
	private long time_flag = System.currentTimeMillis();
//	private long org_time_flag = System.currentTimeMillis();
	public SynData() {
	}
	
	public void run(){
		syndata();
		synOrganization();
		try {
			logger.info(OracleConnectionPool.listCacheInfos());
		} catch (SQLException e) {
			logger.error(e);
		}
	}
	/*****************************************
	 * <li>描        述：同步增量车辆信息
	 * <li> 1. 删除数据库中已经删除的车辆
	 * <li>	2. 同步增量车辆到redis
	 * <li> 3. 同步增量车辆到MySql		</li><br>
	 * <li>时        间：2013-8-26  上午11:12:44	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	private void syndata() {
		Connection oraclecon = null;
		Connection mysqlConn = null;
		PreparedStatement oraclestmt = null;
		PreparedStatement oracleDelstmt = null;
		PreparedStatement mysqlStmt = null;
		ResultSet oracleDelstmtrset = null;
		ResultSet oraclestmtrset = null;
//		Jedis jedis = null;
		JedisUnifiedStorageService jedis = null;
		try {
			
			SynPool synPool = SynPool.getinstance();
			
			// 从ORACLE连接池获取连接
			oraclecon = OracleConnectionPool.getConnection();
//			jedis = RedisConnectionPool.getJedisConnection();
			jedis = RedisServer.getJedisServiceInstance();
//			jedis.select(1); // 连接位置信息库
			logger.info("redis+oracle数据库连接成功");
			
			int count =0;
			long starttime = System.currentTimeMillis();
			oracleDelstmt = oraclecon.prepareStatement(synPool.getSql("oracle_vehicle")); //删除车辆
			oracleDelstmt.setLong(1, time_flag);
			
			String kcpt_oracle_syn_mysql = synPool.getSql("kcpt_oracle_syn_mysql");
			// 同步车辆数据
			logger.debug("同步车辆SQL："+kcpt_oracle_syn_mysql+"\r\n 时间标记:"+time_flag);
			oraclestmt = oraclecon.prepareStatement(kcpt_oracle_syn_mysql);
			oraclestmt.setLong(1, time_flag);
			oraclestmt.setLong(2, time_flag);
			oraclestmt.setLong(3, time_flag);
			oraclestmt.setLong(4, time_flag);
			oraclestmt.setLong(5, time_flag);
			oraclestmt.setLong(6, time_flag);
			oraclestmt.setLong(7, time_flag);
			oraclestmt.setLong(8, time_flag);
			
			oraclestmt.setLong(9, time_flag);
			oraclestmt.setLong(10, time_flag);
			
			//获取时间节点
			oraclestmtrset = oraclestmt.executeQuery();
			
			time_flag = System.currentTimeMillis(); 
			StringBuffer strBuf = new StringBuffer();
			
			// mysql连接
			Class.forName(synPool.getSql("mysql_driverClass"));
			mysqlConn = DriverManager.getConnection(synPool.getSql("mysql_jdbcUrl"),synPool.getSql("mysql_user"),
					synPool.getSql("mysql_password"));
			mysqlStmt = mysqlConn.prepareStatement(synPool.getSql("kcpt_mysql_realTime_tb_vehicle"));
			while (oraclestmtrset.next()) {
				strBuf.setLength(0);
				String vid = String.valueOf(oraclestmtrset.getLong("VID"));
				Long corpid = oraclestmtrset.getLong("CORP_ID");
				String corpname = oraclestmtrset.getString("CORP_NAME");
				Long teamid = oraclestmtrset.getLong("TEAM_ID");
				String teamname = oraclestmtrset.getString("TEAM_NAME");
				String oemcode = oraclestmtrset.getString("OEMCODE");
//				String oemname = oraclestmtrset.getString("OEMNAME");
				String commaddr = oraclestmtrset.getString("COMMADDR");
				Long tid = oraclestmtrset.getLong("TID");
				String tmac = oraclestmtrset.getString("T_MAC");
//				String cid = oraclestmtrset.getString("CID");
				String cname = oraclestmtrset.getString("CNAME");
//				String ccardid = oraclestmtrset.getString("CCARD_ID");
//				Long sid = oraclestmtrset.getLong("SID");
				String vehicleno = oraclestmtrset.getString("VEHICLENO");
				Short platecolorid = oraclestmtrset.getShort("PLATE_COLOR_ID");
//				String transtypecode = oraclestmtrset.getString("TRANSTYPE_CODE");
//				String vehicletypeid = oraclestmtrset.getString("VEHICLETYPE_ID");
//				String areacode = oraclestmtrset.getString("AREA_CODE");
//				int ent_type = oraclestmtrset.getInt("ENT_TYPE");
//				String delivery_status = oraclestmtrset.getString("DELIVERY_STATUS");
				try{
					if(jedis.exists(6,vid)){
						String value = jedis.get(6,vid);
//						String[] arr = value.split(":");
						String[] arr = StringUtils.splitPreserveAllTokens(value, ":");
						if(arr.length != 45){
							logger.error("----非法车辆信息:----"+value);
							continue;
						}
						strBuf.append(arr[0]);
						strBuf.append(":"); 
						strBuf.append(arr[1]);
						strBuf.append(":");
						strBuf.append(arr[2]);
						strBuf.append(":");
						strBuf.append(arr[3]);
						strBuf.append(":");
						strBuf.append(arr[4]);
						strBuf.append(":");
						strBuf.append(arr[5]);
						strBuf.append(":");
						strBuf.append(arr[6]);
						strBuf.append(":");
						strBuf.append(arr[7]);
						strBuf.append(":");
						strBuf.append(arr[8]);
						strBuf.append(":");
						strBuf.append(arr[9]);
						strBuf.append(":"); 
						strBuf.append(arr[10]);
						strBuf.append(":");
						strBuf.append(arr[11]);
						strBuf.append(":");
						strBuf.append(arr[12]);
						strBuf.append(":");
						strBuf.append(arr[13]);
						strBuf.append(":");
						strBuf.append(arr[14]);
						strBuf.append(":");
						strBuf.append(arr[15]);
						strBuf.append(":");
						strBuf.append(arr[16]);
						strBuf.append(":"); 
						strBuf.append(arr[17]);
						strBuf.append(":");
						strBuf.append(arr[18]);
						strBuf.append(":");
						strBuf.append(arr[19]);
						strBuf.append(":");
						strBuf.append(arr[20]);
						strBuf.append(":");
						strBuf.append(arr[21]);
						strBuf.append(":");
						strBuf.append(arr[22]);
						strBuf.append(":");
						strBuf.append(arr[23]);
						strBuf.append(":");
						strBuf.append(arr[24]);
						strBuf.append(":");
						strBuf.append(arr[25]);
						strBuf.append(":");
						strBuf.append(arr[26]);
						strBuf.append(":");
						strBuf.append(arr[27]);
						strBuf.append(":");
						strBuf.append(arr[28]);
						strBuf.append(":");
						strBuf.append(arr[29]);
						strBuf.append(":");
						strBuf.append(platecolorid); // 车牌颜色
						strBuf.append(":");
						strBuf.append(vehicleno); // 车牌号码
						strBuf.append(":");
						strBuf.append(commaddr); // 手机号
						strBuf.append(":");
						strBuf.append(tid); // 终端ID
						strBuf.append(":");
						strBuf.append(tmac); // 终端型号
						strBuf.append(":");
						strBuf.append(cname); //驾驶员姓名
						strBuf.append(":");
						strBuf.append(corpname); // 所属组织
						strBuf.append(":");
						strBuf.append(teamid); // 车队id
						strBuf.append(":");
						
						strBuf.append(corpid); // 企业id 38
						strBuf.append(":");
						strBuf.append(oemcode); // OEMCODE 39
						strBuf.append(":");
						strBuf.append(arr[40]);
						strBuf.append(":");
						strBuf.append(arr[41]);
						strBuf.append(":");
						strBuf.append(arr[42]);
						strBuf.append(":");
						strBuf.append(arr[43]);
						strBuf.append(":");
						strBuf.append(arr[44]); //  TEAM_NAME 车队名称  44 
						
//						logger.debug("redis同步车辆VID：" + vid+" ,\n同步内容"+strBuf.toString());
						jedis.set(6,vid, strBuf.toString());
//						strBuf.delete(0, strBuf.length());
//						strBuf = null;
					}else{
						strBuf.append(0); // 0
						strBuf.append(":"); 
						strBuf.append(0); // 1
						strBuf.append(":");
						strBuf.append(0); // 2
						strBuf.append(":");
						strBuf.append(0); // 3
						strBuf.append(":");
						strBuf.append(0); // 4
						strBuf.append(":");
						strBuf.append(0); // 5
						strBuf.append(":");
						strBuf.append(System.currentTimeMillis()); // 6
						strBuf.append(":");
						
						strBuf.append(":");
						// 引擎转速（发动机转速） // 8
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 瞬时油耗 // 9
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 机油压力 // 10
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 机油温度（随位置汇报上传） 11
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传） 12
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 累计油耗 13
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）14
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 进气温度（随位置汇报上传）15
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 大气压力（随位置汇报上传）16
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 脉冲车速（随位置汇报上传）17
						strBuf.append("0");
						
						strBuf.append(":");
						
						// 终端内置电池电压（随位置汇报上传）18
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 冷却液温度（随位置汇报上传）19
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 车辆蓄电池电压（随位置汇报上传）20
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 发动机扭矩（随位置汇报上传）21
						strBuf.append("-1");
						
						strBuf.append(":");
						
						// 累计里程 22
						strBuf.append("-1");
						
						strBuf.append(":");
						
						//基本状态位 23
						strBuf.append("0");
						
						strBuf.append(":");
						
						//扩展状态位 24
						strBuf.append("0");
						
			
						strBuf.append(":");
						
						strBuf.append("1"); // 25车速来源
						strBuf.append(":");
					
						//计量仪油耗 // 26
						strBuf.append("-1");
						
						strBuf.append(":");
						
						strBuf.append("0"); // 海拔 27
						
						strBuf.append(":");
						
						strBuf.append("0"); // 油箱存油量(升) 28
						
						strBuf.append(":");
						
						strBuf.append(vid); // vid 29
						
						strBuf.append(":");
						
						strBuf.append(platecolorid); // 车牌颜色 30
						
						strBuf.append(":");
						
						strBuf.append(vehicleno); // 车牌号 31
						
						strBuf.append(":");
						
						strBuf.append(commaddr); // 手机号 32
						
						strBuf.append(":");
						
						strBuf.append(tid); // 终端ID 33
						
						strBuf.append(":");
						
						strBuf.append(tmac); // 终端型号 34
						
						strBuf.append(":");
						
						if(null != cname){
							strBuf.append(cname); // 驾驶员姓名 35
						}
						strBuf.append(":");
						
						strBuf.append(corpname);  //所属组织 36
						
						strBuf.append(":");
						
						strBuf.append(teamid); // 车队id 37
						
						strBuf.append(":");
						
						strBuf.append(corpid); // 企业id 38
						
						strBuf.append(":");
						strBuf.append(oemcode); // OEMCODE 39
						
						strBuf.append(":");
						
						strBuf.append(System.currentTimeMillis()); // 系统时间40
						strBuf.append(":");
						
						strBuf.append("0"); //  是否在线41
						
						strBuf.append(":");
						
						strBuf.append("0"); //  是否有效 42
						
						strBuf.append(":");
						
						strBuf.append("0"); //  MSGID 43
						
						strBuf.append(":");
						
						strBuf.append(""); //  TEAM_NAME 车队名称  44
						
//						logger.debug("redis同步车辆VID：" + vid+" ,\n同步内容"+strBuf.toString());
						jedis.set(6,vid, strBuf.toString());
//						strBuf.delete(0, strBuf.length());
//						strBuf = null;
					}
					
					mysqlStmt.setLong(1, Long.parseLong(vid));
					mysqlStmt.setLong(2, corpid);
					mysqlStmt.setString(3, corpname);
					mysqlStmt.setLong(4, teamid);
					mysqlStmt.setString(5, teamname);
					mysqlStmt.setString(6, vehicleno);
					mysqlStmt.setInt(7, platecolorid);
					mysqlStmt.setString(8, oraclestmtrset.getString("AREA_CODE"));
					mysqlStmt.setString(9, oemcode);
					mysqlStmt.setString(10, oraclestmtrset.getString("COMMADDR"));
					mysqlStmt.setLong(11, Long.parseLong(vid));
					mysqlStmt.setLong(12, corpid);
					mysqlStmt.setString(13, corpname);
					mysqlStmt.setLong(14, teamid);
					mysqlStmt.setString(15, teamname);
					mysqlStmt.setString(16, vehicleno);
					mysqlStmt.setInt(17, platecolorid);
					mysqlStmt.setString(18, oraclestmtrset.getString("AREA_CODE"));
					mysqlStmt.setString(19, oemcode);
					mysqlStmt.setString(20, oraclestmtrset.getString("COMMADDR"));
					mysqlStmt.execute();
					logger.info("同步车辆VID：" + vid);
				}catch(Exception e){
					logger.error("同步VID" + vid + "出错",e);
				}
				count++;
				
			}// End while
			
			logger.info("同步车辆数据成功：" + count + "条,"+ (System.currentTimeMillis() - starttime) + "毫秒");
			int delcount = 0;
			oracleDelstmtrset = oracleDelstmt.executeQuery();
			while(oracleDelstmtrset.next()){
				String vid = String.valueOf(oracleDelstmtrset.getLong("VID"));
				if(jedis.exists(6,vid)){
					jedis.del(6,vid);
				}
				
				logger.info("删除车辆VID：" + vid);
				delcount++;
			}// End while
			
			logger.info("删除车辆数据成功：" + String.valueOf(delcount)+ "条");
		} catch (Exception e) {
			logger.error("同步车辆数据失败：",e);
			try {
				if(oraclecon != null){
					oraclecon.close();
				}
			} catch (SQLException e1) {
				logger.error(e1);
			}

		}finally{
				
			try{
				if(oraclestmtrset != null){
					oraclestmtrset.close();
				}
				if(oraclestmt != null){
					oraclestmt.close();
				}
				
				if(null != oracleDelstmtrset){
					oracleDelstmtrset.close();
				}
				
				if(null !=  oracleDelstmt){
					oracleDelstmt.close();
				}		
				if(oraclecon != null){
					oraclecon.close();
				}
				
				if(null != mysqlStmt){
					mysqlStmt.close();
				}
				
				if(null != mysqlConn){
					mysqlConn.close();
				}
				
//				if(null != jedis)
//				RedisConnectionPool.returnJedisConnection(jedis);
				
			}catch(Exception ex){
				logger.error("CLOSED PROPERTIES,ORACLE AND MYSQL TO FAIL.",ex);
			}
		}
	}
	
	/*****
	 * 同步组织   oracle中组织同步到MySql中
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
	
//	/*****
//	 * 同步组织
//	 */
//	private void synOrganization(){
//		Connection oraclecon = null;
//		Connection mysqlcon = null;
//		PreparedStatement oraclestmt = null;
//		PreparedStatement mysqlstmt = null;
//		PreparedStatement mysqlDelStmt = null;
//		ResultSet oraclestmtrset = null;
//		SynPool synPool = SynPool.getinstance();
//		// mysql连接
//		try {
//			Class.forName(synPool.getSql("mysql_driverClass"));
//			mysqlcon = DriverManager.getConnection(synPool.getSql("mysql_jdbcUrl"), synPool.getSql("mysql_user"),synPool.getSql("mysql_password"));
//			mysqlstmt = mysqlcon.prepareStatement(synPool.getSql("mysql_organization"));
//			mysqlDelStmt = mysqlcon.prepareStatement(synPool.getSql("mysql_organizationDel"));
//			// 从ORACLE连接池获取连接
//			oraclecon = OracleConnectionPool.getConnection();
//			int i = 0;
//			int count =0;
//			long starttime = System.currentTimeMillis();
//			
//			// 同步车辆数据
//			oraclestmt = oraclecon.prepareStatement(synPool.getSql("oracle_organizationInc"));
//			oraclestmt.setLong(1, org_time_flag);
//			oraclestmt.setLong(2, org_time_flag);
//			
//			oraclestmtrset = oraclestmt.executeQuery();
//			while(oraclestmtrset.next()){
//				String enableFlag = oraclestmtrset.getString("ENABLE_FLAG");
//				long ent_id = oraclestmtrset.getLong("ENT_ID");
//				if(enableFlag.equals("0")){
//					mysqlDelStmt.setLong(1, ent_id);
//					logger.info("同步删除组织:" + ent_id);
//					mysqlDelStmt.executeUpdate();
//				}else{
//					mysqlstmt.setLong(1,ent_id);
//					mysqlstmt.setString(2,oraclestmtrset.getString("ENT_NAME"));
//					mysqlstmt.setInt(3, oraclestmtrset.getInt("ENT_TYPE"));
//					mysqlstmt.setLong(4, oraclestmtrset.getLong("PARENT_ID"));
//					mysqlstmt.setLong(5, oraclestmtrset.getLong("CREATE_BY"));
//					mysqlstmt.setLong(6, oraclestmtrset.getLong("CREATE_TIME"));
//					mysqlstmt.setLong(7, oraclestmtrset.getLong("UPDATE_BY"));
//					mysqlstmt.setLong(8, oraclestmtrset.getLong("UPDATE_TIME"));
//					mysqlstmt.setString(9, oraclestmtrset.getString("ENABLE_FLAG"));
//					mysqlstmt.setString(10, oraclestmtrset.getString("ENT_STATE"));
//					mysqlstmt.setString(11, oraclestmtrset.getString("MEMO"));
//					mysqlstmt.setString(12, oraclestmtrset.getString("SEQ_CODE"));
//					mysqlstmt.setString(13, formatURL(oraclestmtrset.getString("URL")));
//					mysqlstmt.setLong(14,oraclestmtrset.getLong("ENT_ID"));
//					mysqlstmt.setString(15,oraclestmtrset.getString("ENT_NAME"));
//					mysqlstmt.setInt(16, oraclestmtrset.getInt("ENT_TYPE"));
//					mysqlstmt.setLong(17, oraclestmtrset.getLong("PARENT_ID"));
//					mysqlstmt.setLong(18, oraclestmtrset.getLong("CREATE_BY"));
//					mysqlstmt.setLong(19, oraclestmtrset.getLong("CREATE_TIME"));
//					mysqlstmt.setLong(20, oraclestmtrset.getLong("UPDATE_BY"));
//					mysqlstmt.setLong(21, oraclestmtrset.getLong("UPDATE_TIME"));
//					mysqlstmt.setString(22, enableFlag);
//					mysqlstmt.setString(23, oraclestmtrset.getString("ENT_STATE"));
//					mysqlstmt.setString(24, oraclestmtrset.getString("MEMO"));
//					mysqlstmt.setString(25, oraclestmtrset.getString("SEQ_CODE"));
//					mysqlstmt.setString(26, formatURL(oraclestmtrset.getString("URL")));
//					mysqlstmt.addBatch();
//					count++;
//					i++;
//					if(i % 500 == 0){
//						mysqlstmt.executeBatch();
//						i = 0;
//					}
//					logger.info("同步组织:" + ent_id);
//				}
//			}// End while
//			
//			if(count > 0){
//				mysqlstmt.executeBatch();
//			}
//
//			logger.info("同步组织数据成功：" + String.valueOf(count) + "条,"+ (System.currentTimeMillis() - starttime) + "毫秒");
//
//		} catch (Exception e) {
//			logger.error("同步组织表数据失败：",e);
//		}finally{
//			try{
//				if(mysqlstmt != null){
//					mysqlstmt.close();
//				}
//				
//				if(oraclestmtrset != null){
//					oraclestmtrset.close();
//				}
//				if(oraclestmt != null){
//					oraclestmt.close();
//				}
//				
//				if(mysqlcon !=null){
//					mysqlcon.close();
//				}
//				
//				if(oraclecon != null){
//					oraclecon.close();
//				}
//			
//			}catch(Exception ex){
//				logger.error("CLOSED PROPERTIES,ORACLE AND MYSQL TO FAIL.",ex);
//			}
//		}
//	}
	
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
