package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AnalysisCorpThread <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-11-12</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6
 * @ Description: 用于企业日统计
 */
public class AnalysisCorpThread{
	
	private static final Logger logger = LoggerFactory.getLogger(AnalysisCorpThread.class);
	
	private Map<String,Integer> corpNewUserMap = new HashMap<String,Integer>();
	
	//存储企业对应所有用户
	private Map<String,Integer> corpAllUserMap = new HashMap<String,Integer>();
	
	//存储企业对应当日注销用户
	private Map<String,Integer> corpLogOffUserMap = new HashMap<String,Integer>();
	
	//存储企业当日登录用户
	private Map<String,Integer> corpLogInUserMap = new HashMap<String,Integer>();
	
	//存储企业当日新注册车数
	private Map<String,Integer> corpRegVehicleMap = new HashMap<String,Integer>();
	
	//存储企业当日注册车辆
	private Map<String,Integer> corpLogOffVehicleMap = new HashMap<String,Integer>();
	
	//存储企业当日在线车辆数
	private Map<String,Integer> corpOnLineVehicleMap = new HashMap<String,Integer>();
	
	//存储企业当日在线车辆数
	private Map<String,Integer> corpNewRegOnLineVehicleMap = new HashMap<String,Integer>();
	
	//存储企业行驶车辆数
	private Map<String,Integer> corpMoveVehicleMap = new HashMap<String,Integer>();
	
	//存储企业报警车辆数
	private Map<String,Integer> corpAlarmVehicleMap = new HashMap<String,Integer>();
	
	//存储企业监管车辆数
	private Map<String,Integer> corpSendCommandVehicleMap = new HashMap<String,Integer>();
	
	//存储企业监管次数
	private Map<String,Integer> corpSendCommandNumMap = new HashMap<String,Integer>();
	
	//存储企业查岗次数
	private Map<String,Integer> corpCheckOffLineUserMap = new HashMap<String,Integer>();
	
	private String queryRegUser = null;
	
	private String saveCorpDayStat = null;
	
	private String queryAllRegUser = null;

	private String queryLogOffUser = null;
	
	private String queryLogInUser = null;
	
	private String saveRegVehicle = null;
	
	private String saveLogOffVehicle = null;
	
	private String saveOnLineVehicle = null;
	
	private String saveNewRegOnLineVehicle = null;
	
	private String queryMoveVehicle = null;
	
	private String queryAlarmVehicle = null;
	
	private String querySendCommandVehicle = null;
	
	private String querySendCommandNum = null;
	
	private String queryCheckOffLineUser = null;
	
	private Connection dbCon = null;
	
	public void initAnalyser() throws Exception {
		// 根据企业分组查询前一天新注册用户数量
		queryRegUser = SQLPool.getinstance().getSql("sql_queryRegUser"); 
		
		// 存储企业业务日报
		saveCorpDayStat = SQLPool.getinstance().getSql("sql_saveCorpDayStat"); 
		
		//查询企业总注册用户
		queryAllRegUser = SQLPool.getinstance().getSql("sql_queryAllRegUser"); 
		
		//查询企业当日注销用户
		queryLogOffUser = SQLPool.getinstance().getSql("sql_queryLogOffUser"); 
		
		//查询企业当日登录用户
		queryLogInUser = SQLPool.getinstance().getSql("sql_queryLogInUser"); 
		
		//查询企业当日注册车辆
		saveRegVehicle = SQLPool.getinstance().getSql("sql_queryRegVehicle"); 
		
		//查询企业当日注册车辆
		saveLogOffVehicle = SQLPool.getinstance().getSql("sql_queryLogOffVehicle"); 
		
		//查询企业当日注册车辆
		saveOnLineVehicle = SQLPool.getinstance().getSql("sql_queryOnLineVehicle"); 
		
		//查询企业当日新上线车辆数
		saveNewRegOnLineVehicle = SQLPool.getinstance().getSql("sql_queryNewRegOnLineVehicle"); 
		
		//查询企业行驶车辆数
		queryMoveVehicle = SQLPool.getinstance().getSql("sql_queryMoveVehicle"); 
		
		//查询企业报警车辆
		queryAlarmVehicle = SQLPool.getinstance().getSql("sql_queryAlarmVehicle"); 
		
		//查询企业报警车辆
		querySendCommandVehicle = SQLPool.getinstance().getSql("sql_querySendCommandVehicle"); 
		
		//查询企业监管次数
		querySendCommandNum = SQLPool.getinstance().getSql("sql_querySendCommandNum"); 
		
		//查询企业查岗次数
		queryCheckOffLineUser = SQLPool.getinstance().getSql("sql_queryCheckOffLineUser"); 
	}
	
	public void run(){
		try {
			logger.info("开始企业业务日统计");
			dbCon = OracleConnectionPool.getConnection();
			queryYesNewUser();
			if(!corpNewUserMap.isEmpty()){
				queryAllUser();
				queryLogOffUser();
				queryLogInUser();
				queryRegVehicle();
				queryLogOffVehicle();
				queryOnLineVehicle();
				queryNewRegOnLineVehicle();
				queryMoveVehicle();
				queryAlarmVehicle();
				querySendCommandVehicle();
				querySendCommandNum();
				queryCheckOffLineUser();

				PreparedStatement stSaveCorpDayStat = null;
				try{
					stSaveCorpDayStat = dbCon.prepareStatement(saveCorpDayStat);
					Set<String> corpKy = corpNewUserMap.keySet();
					Iterator<String> corpIt = corpKy.iterator();
					while(corpIt.hasNext()){
						String corpId = corpIt.next();
						saveCorpDayStat(stSaveCorpDayStat,corpId);
					}// End while
					
					//stSaveCorpDayStat.executeBatch();
				}catch(SQLException e){
					logger.error("企业业务日统计出错",e);
				}finally{
					if(stSaveCorpDayStat != null){
						stSaveCorpDayStat.close();
					}
				
					corpNewUserMap.clear();
					
					corpAllUserMap.clear();
					
					corpLogOffUserMap.clear();
					
					corpLogInUserMap.clear();
					
					corpRegVehicleMap.clear();
					
					corpLogOffVehicleMap.clear();
					
					corpOnLineVehicleMap.clear();
					
					corpNewRegOnLineVehicleMap.clear();
					
					corpMoveVehicleMap.clear();
					
					corpAlarmVehicleMap.clear();
					
					corpSendCommandVehicleMap.clear();
					
					corpSendCommandNumMap.clear();
					
					corpCheckOffLineUserMap.clear(); 
				}
			}
		} catch (SQLException e) {
			logger.error("企业日统计出错.",e);
		}finally{
			try {
				dbCon.close();
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
		}
	}
	
	/**
	 * 统计企业新注册用户
	 * @throws SQLException
	 */
	private void queryYesNewUser() throws SQLException{
		PreparedStatement stQueryRegUser = null;
		ResultSet rs = null;
		try{
			stQueryRegUser = dbCon.prepareStatement(queryRegUser);
			stQueryRegUser.setLong(1, CDate.getYesDayUTC());
			rs = stQueryRegUser.executeQuery();
		
			while(rs.next()){
				corpNewUserMap.put(rs.getString("ENT_ID"), rs.getInt("NUM"));
			}// End while
		}catch(SQLException e){
			logger.error("统计企业新注册用户出错.",e);
		}finally{
			if(rs != null){
				rs.close();
				rs = null;
			}
			
			if(stQueryRegUser != null){
				stQueryRegUser.close();
			}
		}
	}
	
	/**
	 * 查询企业总用户
	 * @param corpId
	 * @return
	 * @throws SQLException
	 */
	private void queryAllUser() throws SQLException{
		PreparedStatement stQueryAllRegUser = null;
		ResultSet rs = null;
		try{
			stQueryAllRegUser = dbCon.prepareStatement(queryAllRegUser);
			rs = stQueryAllRegUser.executeQuery();
			while(rs.next()){
				corpAllUserMap.put(rs.getString("ENT_ID"), rs.getInt("ALLUSER"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业总用户出错.",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryAllRegUser != null){
				stQueryAllRegUser.close();
			}
		}
	}
	
	/***
	 * 查询企业当日注销用户
	 * @param corpId
	 * @return
	 * @throws SQLException 
	 */
	private void queryLogOffUser() throws SQLException{
		PreparedStatement stQueryLogOffUser = null;
		ResultSet rs = null;
		try{
			stQueryLogOffUser = dbCon.prepareStatement(queryLogOffUser);
			
			stQueryLogOffUser.setLong(1, CDate.getYesDayUTC());
			rs = stQueryLogOffUser.executeQuery();
			
			while(rs.next()){
				corpLogOffUserMap.put(rs.getString("ENT_ID"), rs.getInt("LOGOFFUSER"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业当日注销用户.",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryLogOffUser != null ){
				stQueryLogOffUser.close();
			}
		}
	}
	
	/**
	 * 查询企业当日登录用户
	 * @throws SQLException 
	 */
	private void queryLogInUser() throws SQLException{
		PreparedStatement stQueryLogInUser = null;
		ResultSet rs = null;
		try{
			stQueryLogInUser = dbCon.prepareStatement(queryLogInUser);
			stQueryLogInUser.setLong(1, CDate.getYesDayUTC());
			
			rs = stQueryLogInUser.executeQuery();
			while(rs.next()){
				corpLogInUserMap.put(rs.getString("ENT_ID"), rs.getInt("LOGINUSER"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业当日登录用户出错.",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryLogInUser != null){
				stQueryLogInUser.close();
			}
		}
	}
	
	/**
	 * 查询企业当日注册车辆
	 * @throws SQLException
	 */
	private void queryRegVehicle() throws SQLException{
		PreparedStatement stQueryRegVehicle = null;
		ResultSet rs = null;
		
		try{
			stQueryRegVehicle = dbCon.prepareStatement(saveRegVehicle);
			stQueryRegVehicle.setLong(1, CDate.getYesDayUTC());
			rs = stQueryRegVehicle.executeQuery();
			while(rs.next()){
				corpRegVehicleMap.put(rs.getString("ENT_ID"), rs.getInt("REGVEHICLE"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业当日注册车辆出错.",e);
		}finally{
			if(rs  != null){
				rs.close();
			}
			
			if(stQueryRegVehicle != null){
				stQueryRegVehicle.close();
			}
		}
	}
	
	/***
	 * 查询企业当日吊销车辆数
	 * @throws SQLException
	 */
	private void queryLogOffVehicle() throws SQLException{
		PreparedStatement stQueryLogOffVehicle = null;
		ResultSet rs = null;
		try{
			stQueryLogOffVehicle = dbCon.prepareStatement(saveLogOffVehicle);
			stQueryLogOffVehicle.setLong(1, CDate.getYesDayUTC());
			rs = stQueryLogOffVehicle.executeQuery();
			while(rs.next()){
				corpLogOffVehicleMap.put(rs.getString("ENT_ID"), rs.getInt("LOGOFFVEHICLE"));
			}// End while
		}catch(Exception e){
			logger.error("查询企业当日吊销车辆数出错.",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryLogOffVehicle != null){
				stQueryLogOffVehicle.close();
			}
		}
	}
	
	/**
	 * 查询企业当日在线车辆数
	 * @throws SQLException
	 */
	private void queryOnLineVehicle() throws SQLException{
		PreparedStatement stQueryOnLineVehicle = null;
		ResultSet rs = null;
		try{
			stQueryOnLineVehicle = dbCon.prepareStatement(saveOnLineVehicle);
			stQueryOnLineVehicle.setLong(1, CDate.getYesDayUTC());
			stQueryOnLineVehicle.setLong(2, CDate.getCurrentDayUTC());
			rs = stQueryOnLineVehicle.executeQuery();
			while(rs.next()){
				corpOnLineVehicleMap.put(rs.getString("CORP_ID"), rs.getInt("ONLINENUM"));
			} //End while
		}catch(SQLException e){
			logger.error("查询企业当日在线车辆数出错.",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryOnLineVehicle != null){
				stQueryOnLineVehicle.close();
			}
		}
	}
	
	/**
	 * 查询企业当日新上线车辆数
	 * @throws SQLException 
	 */
	private void queryNewRegOnLineVehicle() throws SQLException{
		PreparedStatement stQueryNewRegOnLineVehicle = null;
		ResultSet rs = null;
		try{
			stQueryNewRegOnLineVehicle = dbCon.prepareStatement(saveNewRegOnLineVehicle);
			stQueryNewRegOnLineVehicle.setLong(1, CDate.getYesDayUTC());
			stQueryNewRegOnLineVehicle.setLong(2, CDate.getCurrentDayUTC() );
			rs = stQueryNewRegOnLineVehicle.executeQuery();
			
			while(rs.next()){
				corpNewRegOnLineVehicleMap.put(rs.getString("ENT_ID"), rs.getInt("NEWREGONLINE"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业当日新上线车辆数",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryNewRegOnLineVehicle != null){
				stQueryNewRegOnLineVehicle.close();
			}
		}
	}
	
	/**
	 * 查询企业行驶车辆数
	 * @throws SQLException
	 */
	private void queryMoveVehicle() throws SQLException{
		PreparedStatement stQueryMoveVehicle = null;
		ResultSet rs = null;
		try{
			stQueryMoveVehicle = dbCon.prepareStatement(queryMoveVehicle);
			stQueryMoveVehicle.setLong(1, CDate.getYesDayUTC());
			stQueryMoveVehicle.setLong(2, CDate.getCurrentDayUTC());
			rs = stQueryMoveVehicle.executeQuery();
			while(rs.next()){
				corpMoveVehicleMap.put(rs.getString("CORP_ID"), rs.getInt("MOVENUM"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业行驶车辆数",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryMoveVehicle != null){
				stQueryMoveVehicle.close();
			}
		}
	}
	
	/**
	 * 查询企业报警车辆
	 * @throws SQLException
	 */
	private void queryAlarmVehicle() throws SQLException{
		PreparedStatement stQueryAlarmVehicle = null;
		ResultSet rs = null;
		
		try{
			stQueryAlarmVehicle = dbCon.prepareStatement(queryAlarmVehicle);
			stQueryAlarmVehicle.setLong(1, CDate.getYesDayUTC());
			stQueryAlarmVehicle.setLong(2, CDate.getCurrentDayUTC());
			rs = stQueryAlarmVehicle.executeQuery();
			while(rs.next()){
				corpAlarmVehicleMap.put(rs.getString("CORP_ID"), rs.getInt("ALARMV"));	
			}// End while
		}catch(SQLException e){
			logger.error("查询企业报警车辆",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryAlarmVehicle != null){
				stQueryAlarmVehicle.close();
			}
		}
	}
	
	
	
	/**
	 * 查询企业监管车辆数
	 * @throws SQLException
	 */
	private void querySendCommandVehicle() throws SQLException{
		PreparedStatement stQuerySendCommandVehicle = null;
		ResultSet rs = null;
		try{
			stQuerySendCommandVehicle = dbCon.prepareStatement(querySendCommandVehicle);
			stQuerySendCommandVehicle.setLong(1, CDate.getYesDayUTC());
			stQuerySendCommandVehicle.setLong(2, CDate.getCurrentDayUTC());
			
			rs = stQuerySendCommandVehicle.executeQuery();
			while(rs.next()){
				corpSendCommandVehicleMap.put(rs.getString("ENT_ID"), rs.getInt("VNUM"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业监管车辆数出错",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQuerySendCommandVehicle != null){
				stQuerySendCommandVehicle.close();
			}
		}
	}
	
	/**
	 * 查询企业监管次数
	 * @throws SQLException
	 */
	private void querySendCommandNum() throws SQLException{
		PreparedStatement stQuerySendCommandNum = null;
		ResultSet rs = null;
		
		try{
			stQuerySendCommandNum = dbCon.prepareStatement(querySendCommandNum);
			stQuerySendCommandNum.setLong(1, CDate.getYesDayUTC());
			stQuerySendCommandNum.setLong(2, CDate.getCurrentDayUTC());
			
			rs = stQuerySendCommandNum.executeQuery();
			while(rs.next()){
				corpSendCommandNumMap.put(rs.getString("ENT_ID"), rs.getInt("VNUM"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业监管次数",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQuerySendCommandNum != null){
				stQuerySendCommandNum.close();
			}
		}
	}
	
	/**
	 * 查询企业查岗次数
	 * @throws SQLException
	 */
	private void queryCheckOffLineUser() throws SQLException{
		PreparedStatement stQueryCheckOffLineUser = null;
		ResultSet rs = null;
		try{
			stQueryCheckOffLineUser = dbCon.prepareStatement(queryCheckOffLineUser);
			stQueryCheckOffLineUser.setLong(1, CDate.getYesDayUTC());
			stQueryCheckOffLineUser.setLong(2, CDate.getCurrentDayUTC());
			
			rs = stQueryCheckOffLineUser.executeQuery();
			while(rs.next()){
				corpCheckOffLineUserMap.put(rs.getString("ENT_ID"), rs.getInt("CHNUM"));
			}// End while
		}catch(SQLException e){
			logger.error("查询企业查岗次数",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stQueryCheckOffLineUser != null){
				stQueryCheckOffLineUser.close();
			}
		}
	}
	
	/**
	 * 存储企业日统计
	 * @param corpId
	 * @throws SQLException
	 */
	private void saveCorpDayStat(PreparedStatement stSaveCorpDayStat,String corpId) throws SQLException{
		//logger.info("date:" + CDate.getYesDayYearMonthDay() + ",crop ID:" + corpId);
		stSaveCorpDayStat.setLong(1, CDate.getYesDayYearMonthDay());
		stSaveCorpDayStat.setString(2, corpId);
		stSaveCorpDayStat.setInt(3, corpNewUserMap.get(corpId)); // 新注册操作员数
		int allUser = 0;
		if(corpAllUserMap.containsKey(corpId)){
			allUser = corpAllUserMap.get(corpId);
		}
		if(corpLogOffUserMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(4, corpLogOffUserMap.get(corpId)); // 当日注销操作员数
		}else{
			stSaveCorpDayStat.setInt(4, 0); // 当日注销操作员数
		}
		int logInUser = 0;
		if(corpLogInUserMap.containsKey(corpId)){
			logInUser =  corpLogInUserMap.get(corpId);
		}
		stSaveCorpDayStat.setInt(5,logInUser); // 登陆操作员数
		
		stSaveCorpDayStat.setInt(6,(allUser - logInUser)); // 未登录操作员数
		
		if(corpRegVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(7,corpRegVehicleMap.get(corpId)); // 新注册车辆数(在管理平台完成注册的车辆个数)
		}else{
			stSaveCorpDayStat.setInt(7,0);
		}
		
		if(corpLogOffVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(8, corpLogOffVehicleMap.get(corpId)); // 当日吊销车辆数（在管理平台完成吊销车辆个数
		}else{
			stSaveCorpDayStat.setInt(8, 0);
		}
		
		if(corpNewRegOnLineVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(9, corpNewRegOnLineVehicleMap.get(corpId)); // 当日新上线车辆数(向平台发送过注册指令的车辆个数
		}else{
			stSaveCorpDayStat.setInt(9, 0);
		}
		if(corpOnLineVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(10, corpOnLineVehicleMap.get(corpId)); // 在线车辆数（上线+未上线车辆总数）
		}else{
			stSaveCorpDayStat.setInt(10, 0); 
		}
		
		if(corpMoveVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(11, corpMoveVehicleMap.get(corpId)); // 行驶车辆数
		}else{
			stSaveCorpDayStat.setInt(11, 0);
		}
		
		if(corpAlarmVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(12, corpAlarmVehicleMap.get(corpId)); // 报警状态车辆数
		}else{
			stSaveCorpDayStat.setInt(12, 0);
		}
		
		//有数据上报的车辆
		stSaveCorpDayStat.setInt(13, 0);

		if(corpCheckOffLineUserMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(14, corpCheckOffLineUserMap.get(corpId)); // 查岗次数
		}else{
			stSaveCorpDayStat.setInt(14, 0);
		}
		
		
		if(corpSendCommandVehicleMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(15, corpSendCommandVehicleMap.get(corpId)); // 监管车辆数
		}else{
			stSaveCorpDayStat.setInt(15, 0);
		}
		
		if(corpSendCommandNumMap.containsKey(corpId)){
			stSaveCorpDayStat.setInt(16, corpSendCommandNumMap.get(corpId)); // 监管次数
		}else{
			stSaveCorpDayStat.setInt(16, 0);
		}
		
		//stSaveCorpDayStat.addBatch();
		stSaveCorpDayStat.executeUpdate();
	}
	
}
