package com.ctfo.syn.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.driver.OracleResultSet;

import org.apache.log4j.Logger;

import com.ctfo.combusiness.beans.TbFeedback;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.syn.membeans.TbOrganization;
import com.google.gson.reflect.TypeToken;

/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： KCPTDataAnaly		</li><br>
 * <li>文件名称：com.ctfo.analy.dao </li><br>
 * <li>时        间：2013-5-8  下午12:43:49	</li><br>
 * <li>描        述：缓存管理类			</li><br>
 * </ul>
 *****************************************/
public class CacheUpdateDBAdapter {
	private static final Logger logger = Logger.getLogger(CacheUpdateDBAdapter.class);
	
	public static List<TbOrganization> orgList = null;
	
	/*****************************************
	 * <li>描       述： 缓存管理类初始化		</li><br>
	 * <li>参        数：@param config		</li><br>
	 *****************************************/
	public void initCacheUpdateDBAdapter(){
		initOrgList(); 
	}
	
	private void initOrgList() {
		Connection connection = null;	
		Statement oraclestmt = null;
		ResultSet oraclestmtrs = null;
		orgList = new ArrayList<TbOrganization>();
		try {	
			connection = OracleConnectionPool.getConnection();
			oraclestmt = connection.createStatement();
			oraclestmtrs = oraclestmt.executeQuery(SynPool.getinstance().getSql("oracle_memcache_org"));
			while (oraclestmtrs.next()) {
				TbOrganization tbOrganization = new TbOrganization();
				tbOrganization.setEntId(oraclestmtrs.getLong("ENT_ID"));
				tbOrganization.setEntName(oraclestmtrs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}
			if(orgList!=null && orgList.size() > 0){
				RedisServer.getJedisServiceInstance().saveOrgEntIdNameList(RedisJsonUtil.objectToJson(orgList));
				logger.debug("基础消息更新 - 初始化企业信息结束，共更新"+orgList.size()+" 条数据。");
				initFeedback(orgList);
			}
			
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：",e);
			try {
				connection.close();
			} catch (SQLException e1) {
				logger.error(e1);
			}
		} finally {
			try {
				if(oraclestmtrs != null) {
					oraclestmtrs.close();
				}
				if(oraclestmt != null) {
					oraclestmt.close();
				}
				if(connection != null) {
					connection.close();
				}
			} catch (Exception ex) {
				logger.error("查询企业信息异常:",ex);
			}
			if(orgList != null){
				orgList.clear();
			}	
		}	
		
	}
	/*****************************************
	 * <li>描        述：初始化告警等级设置缓存 		</li><br>
	 * <li>参数：  		</li><br>
	 * <li>时        间：2013-6-3  下午5:08:21	</li><br>
	 * 
	 *****************************************/
	public void initAlarmSettingCache(){
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		orgList = new ArrayList<TbOrganization>();
		try {	
//			logger.debug("oracle数据库连接成功");
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_org"));
			rs = (OracleResultSet) ops.executeQuery();
			while (rs.next()) {
				TbOrganization tbOrganization = new TbOrganization();
				tbOrganization.setEntId(rs.getLong("ENT_ID"));
				tbOrganization.setEntName(rs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}
			if(orgList!=null && orgList.size() > 0){
				RedisServer.getJedisServiceInstance().saveOrgEntIdNameList(RedisJsonUtil.objectToJson(orgList));
				logger.debug("基础消息更新 - 企业信息结束，共更新"+orgList.size()+" 条数据。");
			}
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：",e);
			try {
				conn.close();
			} catch (SQLException e1) {
				logger.error(e1);
			}
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				logger.error("关闭连接异常:",ex);
			}
		}	
	}
	/*****************************************
	 * <li>描       述： 反馈信息缓存更新		</li><br>
	 * <li>参        数：@param name		</li><br
	 * <li>参        数：@param value	</li><br>
	 *****************************************/
	@SuppressWarnings("unchecked")
	public void feedbackUpdate(String name,String value){
		PreparedStatement oracleps = null;
		// 信息反馈结果集
		ResultSet oraclers = null;
		Connection connection = null;	
		try {		
			connection = OracleConnectionPool.getConnection();
			String orgStr = RedisServer.getJedisServiceInstance().getOrgEntIdNameList();
			List<TbOrganization> orgList = (List<TbOrganization>) RedisJsonUtil.jsonToObject(orgStr, new TypeToken<List<TbOrganization>>(){});
			int index = 0;
			Map<String, List<TbFeedback>> mapClient = new HashMap<String, List<TbFeedback>>();
			for (TbOrganization org : orgList) {
				try{
					List<TbFeedback> list = null;
					oracleps = connection.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_feedback"));
					boolean flag = true;
					oracleps.setLong(1, org.getEntId());
					oracleps.setLong(2, org.getEntId());
					oraclers = oracleps.executeQuery();
	
					while (oraclers.next()) {
						if(flag){
							list = new ArrayList<TbFeedback>();
							flag = false;
						}
						TbFeedback feedback = new TbFeedback();
						feedback.setReplyId(oraclers.getLong("REPLY_ID"));
						feedback.setReplyType(oraclers.getString("REPLY_TYPE"));
						feedback.setReplyTheme(oraclers.getString("REPLY_THEME"));
						feedback.setPublishStatus(oraclers.getString("PUBLISH_STATUS"));
						feedback.setPublishTime(oraclers.getLong("PUBLISH_TIME"));
						feedback.setReplyFlag(oraclers.getString("REPLY_FLAG"));
						feedback.setParentReplyId(oraclers.getLong("PARENT_REPLY_ID"));
						feedback.setEntId(oraclers.getLong("ENT_ID"));
						feedback.setCreateBy(oraclers.getLong("CREATE_BY"));
						feedback.setCreateTime(oraclers.getLong("CREATE_TIME"));
						feedback.setUpdateBy(oraclers.getLong("UPDATE_BY"));
						feedback.setUpdateTime(oraclers.getLong("UPDATE_TIME"));
						feedback.setEnableFlag(oraclers.getString("ENABLE_FLAG"));
						feedback.setReplyContent(oraclers.getString("REPLY_CONTENT"));
						feedback.setCreateName(oraclers.getString("CREATE_NAME"));
						list.add(feedback);
					}// End while
					
					if(list != null && list.size() > 0) {
						index++; 
						mapClient.put(String.valueOf(org.getEntId()), list);
					}
				}finally{
					if(oracleps != null) {
						oracleps.close();
					}
					if(oraclers != null) {
						oraclers.close();
					}
				}
			}// End for
			RedisServer.getJedisServiceInstance().saveFeedback(RedisJsonUtil.objectToJson(mapClient));
			logger.debug("基础消息更新 - 反馈信息结束，共更新"+index+" 条企业的反馈数据。");
//			logger.info("mapClient.size:" + mapClient.size());
			
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：",e);
			try {
				if(connection != null){
					connection.close();
				}
			} catch (SQLException e1) {
				logger.error(e1);
			}
		}finally{
			try{
				
				if(connection != null){
					connection.close();
				}
			} catch (SQLException e1) {
				logger.error(e1);
			}
		}
	}

	
	/*****************************************
	 * <li>描       述： 企业缓存全量更新		</li><br>
	 *****************************************/
	public void orgUpdate() {
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		orgList = new ArrayList<TbOrganization>();
		try {	
//			logger.debug("oracle数据库连接成功");
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_org"));
			rs = (OracleResultSet) ops.executeQuery();
			while (rs.next()) {
				TbOrganization tbOrganization = new TbOrganization();
				tbOrganization.setEntId(rs.getLong("ENT_ID"));
				tbOrganization.setEntName(rs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}
			if(orgList!=null && orgList.size() > 0){
				RedisServer.getJedisServiceInstance().saveOrgEntIdNameList(RedisJsonUtil.objectToJson(orgList));
				logger.debug("基础消息更新 - 企业信息结束，共更新"+orgList.size()+" 条数据。");
			}
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：",e);
			try {
				conn.close();
			} catch (SQLException e1) {
				logger.error(e1);
			}
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				logger.error("关闭连接异常:",ex);
			}
		}	
		
	}
	/** 
	 * 初始化缓存信息反馈集合
	 * @return Map
	 */
	private void initFeedback( List<TbOrganization> orgList) {
		
		PreparedStatement oracleps = null;
		// 信息反馈结果集
		ResultSet oraclers = null;
		Connection connection = null;	
		try {		
			Map<String, List<TbFeedback>> mapClient = new HashMap<String, List<TbFeedback>>();
			connection = OracleConnectionPool.getConnection();
			for (TbOrganization org : orgList) {
				try{
					List<TbFeedback> list = null;
					oracleps = connection.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_feedback"));
					boolean flag = true;
					oracleps.setLong(1, org.getEntId());
					oracleps.setLong(2, org.getEntId());
					oraclers = oracleps.executeQuery();
					while (oraclers.next()) {
						if(flag){
							list = new ArrayList<TbFeedback>();
							flag = false;
						}
						TbFeedback feedback = new TbFeedback();
						feedback.setReplyId(oraclers.getLong("REPLY_ID"));
						feedback.setReplyType(oraclers.getString("REPLY_TYPE"));
						feedback.setReplyTheme(oraclers.getString("REPLY_THEME"));
						feedback.setPublishStatus(oraclers.getString("PUBLISH_STATUS"));
						feedback.setPublishTime(oraclers.getLong("PUBLISH_TIME"));
						feedback.setReplyFlag(oraclers.getString("REPLY_FLAG"));
						feedback.setParentReplyId(oraclers.getLong("PARENT_REPLY_ID"));
						feedback.setEntId(oraclers.getLong("ENT_ID"));
						feedback.setCreateBy(oraclers.getLong("CREATE_BY"));
						feedback.setCreateTime(oraclers.getLong("CREATE_TIME"));
						feedback.setUpdateBy(oraclers.getLong("UPDATE_BY"));
						feedback.setUpdateTime(oraclers.getLong("UPDATE_TIME"));
						feedback.setEnableFlag(oraclers.getString("ENABLE_FLAG"));
						feedback.setReplyContent(oraclers.getString("REPLY_CONTENT"));
						feedback.setCreateName(oraclers.getString("CREATE_NAME"));
						list.add(feedback);
					}// End while
					if(list != null && list.size() > 0) { 
						mapClient.put(String.valueOf(org.getEntId()), list);
					}
				}finally{
					if(oracleps != null) {
						oracleps.close();
					}
					if(oraclers != null) {
						oraclers.close();
					}
				}
			}// End for
			RedisServer.getJedisServiceInstance().saveFeedback(RedisJsonUtil.objectToJson(mapClient));
			
			logger.info("信息反馈初始化缓存结束.");
			
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：",e);
			try {
				if(connection != null){
					connection.close();
				}
			} catch (SQLException e1) {
				logger.error(e1);
			}
		}finally{
			try{
				
				if(connection != null){
					connection.close();
				}
			} catch (SQLException e1) {
				logger.error(e1);
			}
		}
	}
}
