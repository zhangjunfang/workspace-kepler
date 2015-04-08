package com.ctfo.syn.oracle2memcache;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.TimeoutException;

import org.apache.log4j.Logger;

import com.ctfo.combusiness.beans.TbFeedback;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.syn.membeans.StaticMemcache;
import com.ctfo.syn.membeans.TbOrganization;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

/**
 * 信息反馈同步到memcache服务 (5分钟)
 * @author xuehui
 *
 */
public class SynMemFeedback implements Runnable{

	public static Logger logger = Logger.getLogger(SynMemFeedback.class);
	
	private Map<String, List<TbFeedback>> mapClient = new HashMap<String, List<TbFeedback>>();
	
	public SynMemFeedback() {
	}
	
	public void run() {
		logger.info("开始执行信息反馈查询");
		initMemFeedback();
	}
	
	private void initMemFeedback() {
		JedisUnifiedStorageService jedis = null;
		try {
			jedis = RedisServer.getJedisServiceInstance();
			// 查询信息反馈集合
			synMemFeedback();
			if(mapClient.size() > 0)
				jedis.saveFeedback(RedisJsonUtil.objectToJson(mapClient));
//			ConnectMemcachePool.getSqlMap(SynPool.getinstance().getSql("memcacheMainAddr")).set(StaticMemcache.MEMCACHE_TBFEEDBACK, 0, mapClient);
			//logger.info("*********:"+(Map)mcc.get(StaticMemcache.MEMCACHE_TBFEEDBACK));
		} catch (TimeoutException e) {
			logger.error(e);
		} catch (InterruptedException e) {
			logger.error(e);
		} catch (Exception e) {
			logger.error(e);
		} finally {
			if(mapClient.size() > 0){
				mapClient.clear();
			}
		}
	}
	
	/**
	 * 查询信息反馈集合
	 * @return Map
	 */
	private void synMemFeedback() {		
		Connection connection = null;	
		Statement oraclestmt = null;
		// 企业用户结果集
		ResultSet oraclestmtrs = null;
		List<TbOrganization> orgList = new ArrayList<TbOrganization>();
		try {	
			logger.debug("oracle数据库连接成功");
			connection = OracleConnectionPool.getConnection();
			
			oraclestmt = connection.createStatement();
			oraclestmtrs = oraclestmt.executeQuery(SynPool.getinstance().getSql("oracle_memcache_org"));
			while (oraclestmtrs.next()) {
				TbOrganization tbOrganization = new TbOrganization();
				tbOrganization.setEntId(oraclestmtrs.getLong("ENT_ID"));
				tbOrganization.setEntName(oraclestmtrs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}// End while
			
			if(orgList != null && orgList.size() > 0){
				getMemFeedbackMap(orgList);
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
				logger.error("CLOSED PROPERTIES,ORACLE TO FAIL.",ex);
			}
			if(orgList != null){
				orgList.clear();
			}	
		}	
	}
	
	/** 
	 * 遍历所有企业id对应的信息反馈,放到Map里
	 * @return Map
	 */
	private void getMemFeedbackMap( List<TbOrganization> orgList) {
		
		PreparedStatement oracleps = null;
		// 信息反馈结果集
		ResultSet oraclers = null;
		Connection connection = null;	
		try {		
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
			
			logger.info("mapClient.size:" + mapClient.size());
			
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
