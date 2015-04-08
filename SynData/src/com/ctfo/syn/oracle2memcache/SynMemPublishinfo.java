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

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


import com.ctfo.combusiness.beans.TbPublishInfo;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.syn.membeans.StaticMemcache;
import com.ctfo.syn.membeans.TbOrganization;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

/**
 * 公告信息同步到memcache服务 (20分钟)
 * @author xuehui
 *
 */
public class SynMemPublishinfo implements Runnable{

	private Logger logger = LoggerFactory.getLogger(SynMemPublishinfo.class);
//	private Map<String, List<TbPublishInfo>> mapClient = new HashMap<String, List<TbPublishInfo>>();	
	private List<TbOrganization> orgList = new ArrayList<TbOrganization>();
	
	public SynMemPublishinfo() {
	}
	
	public void run() {
		logger.info("开始执行公告管理查询");
		try{
			// 查詢企業列表
			selectOrgInfo();
			initMemPublishinfo(StaticMemcache.TBPUBLISHINFO_SYS);
			
			initMemPublishinfo(StaticMemcache.TBPUBLISHINFO_ORG);
		}finally{
			if(orgList != null){
				orgList.clear();
			}
		}
	}
	
	/**
	 * 初始化同步服务
	 * @param type memcachedClient的key 01是系统公告， 02是企业咨询
	 */
	private void initMemPublishinfo(String type) {		
//		JedisUnifiedStorageService jedis = null;
		try {
			// 查询公告信息集合
			synMemPublishinfo(type);
//			ConnectMemcachePool.getSqlMap(SynPool.getinstance().getSql("memcacheMainAddr")).set(key.toString(), 0, mapClient);
		} catch (Exception e) {
			logger.error("同步公告异常:"+e.getMessage(),e);
		}finally {
//			if(mapClient != null){
//				mapClient.clear();
//			}
		}
	}
	
	private void selectOrgInfo(){
		Connection connection = null;
		Statement oraclestmt = null;
		// 企业用户结果集
		ResultSet oraclestmtrs = null;
		try {
			connection = OracleConnectionPool.getConnection();
			logger.debug("oracle数据库连接成功");
			
			oraclestmt = connection.createStatement();
			
			oraclestmtrs = oraclestmt.executeQuery(SynPool.getinstance().getSql("oracle_memcache_org"));
			while (oraclestmtrs.next()) {
				TbOrganization tbOrganization = new TbOrganization();
				tbOrganization.setEntId(oraclestmtrs.getLong("ENT_ID"));
				tbOrganization.setEntName(oraclestmtrs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}// End while
		} catch (SQLException e) {
			logger.error("同步公告异常:"+e.getMessage(),e);
		}finally{
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
		}
	}
	
	/**
	 * 查询公告信息
	 * @param String
	 * @return Map
	 */
	private void synMemPublishinfo(String type) {	
		Connection connection = null;	
		PreparedStatement oracleps = null;
		// 公告信息结果集
		ResultSet oraclers = null;
		try {	
			connection = OracleConnectionPool.getConnection();
			if(type != null && !type.equals("")) {
				// 系统公告
				if(type.equals(StaticMemcache.TBPUBLISHINFO_SYS)) {
					oracleps = connection.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_sysPublish"));
				}
				// 企业资讯
				if(type.equals(StaticMemcache.TBPUBLISHINFO_ORG)) {
					oracleps = connection.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_orgPublish"));
				}
			}
			getMemPublishinfoMap(oracleps, oraclers,type);
			
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：" ,e);
			try {
				connection.close();
			} catch (SQLException e1) {
				logger.error("同步公告异常:"+e.getMessage(),e1);
			}
		} finally {
			try {
				
				if(connection != null) {
					connection.close();
				}
			} catch (Exception ex) {
				logger.error("CLOSED PROPERTIES,ORACLE TO FAIL.",ex);
			}
		}
	}
	
	/** 
	 * 遍历所有企业id对应的公告信息,放到Map里
	 * @return Map
	 */
	private void getMemPublishinfoMap(PreparedStatement oracleps, ResultSet oraclers, String type) {
		
		if(type.equals(StaticMemcache.TBPUBLISHINFO_SYS)){
			type = "SYS_";
		}else {
			type = "ORG_";
		}
		String result = null;
		JedisUnifiedStorageService jedis = null;
		logger.info("LoopOrgPublish--beginning。。。");
		try {
			long start = System.currentTimeMillis();
			for (TbOrganization org : orgList) {
				oracleps.setLong(1, org.getEntId());
				oracleps.setLong(2, org.getEntId());
				oracleps.setLong(3, System.currentTimeMillis());
				oraclers = oracleps.executeQuery();
				List<TbPublishInfo> list = null;
				boolean flag = true;
				while (oraclers.next()) {
					if(flag){
						list = new ArrayList<TbPublishInfo>();
						flag = false;
					}
					TbPublishInfo publishinfo = new TbPublishInfo();
					publishinfo.setInfoId(oraclers.getLong("INFO_ID"));
					publishinfo.setInfoType(oraclers.getString("INFO_TYPE"));
					publishinfo.setInfoTheme(oraclers.getString("INFO_THEME"));
					publishinfo.setInfoStatus(oraclers.getString("INFO_STATUS"));
					publishinfo.setPublishUser(oraclers.getLong("PUBLISH_USER"));
					publishinfo.setPublishTime(oraclers.getLong("PUBLISH_TIME"));
					publishinfo.setAccessoryFlag(oraclers.getShort("ACCESSORY_FLAG"));
					publishinfo.setEntId(oraclers.getLong("ENT_ID"));
					publishinfo.setIsSettop(oraclers.getShort("IS_SETTOP"));
					publishinfo.setCreateBy(oraclers.getLong("CREATE_BY"));
					publishinfo.setCreateTime(oraclers.getLong("CREATE_TIME"));
					publishinfo.setUpdateBy(oraclers.getLong("UPDATE_BY"));
					publishinfo.setUpdateTime(oraclers.getLong("UPDATE_TIME"));
					publishinfo.setEnableFlag(oraclers.getString("ENABLE_FLAG"));
					publishinfo.setEnddate(oraclers.getLong("ENDDATE"));
					publishinfo.setInfoContent(oraclers.getString("INFO_CONTENT"));
					list.add(publishinfo);				
				}// End while
				if(list != null && list.size() > 0) {
					result = RedisJsonUtil.objectToJson(list);
					jedis = RedisServer.getJedisServiceInstance();
					jedis.set(4, type + org.getEntId(), result);
				}	
			}// End for		
			
//			logger.info("--publishInfo--SUCCESS-mapClient.size:" + mapClient.size());
			
			long end = System.currentTimeMillis();
			logger.info("LoopOrgPublish--org size:"+orgList.size()+",end:Processed(ms):"+(end - start)+",type:"+type);
		} catch (Exception e) {
			logger.error("--publishInfo--ERROR--获取公告管理数据失败",e);
		}finally{
			try{
				if(oraclers != null) {
					oraclers.close();
				}
				if(oracleps != null) {
					oracleps.close();
				}
			}catch(Exception e){
				logger.error("同步公告异常:"+e.getMessage(),e);
			}
		}
	}
	
}
