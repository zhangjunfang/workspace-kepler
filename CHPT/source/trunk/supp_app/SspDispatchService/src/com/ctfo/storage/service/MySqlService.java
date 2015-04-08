package com.ctfo.storage.service;

import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dao.MySqlDataSource;
import com.ctfo.storage.model.support.TbComInfo;
import com.ctfo.storage.model.support.TbUserBehaviorMonitor;
import com.ctfo.storage.model.support.TbUserOnline;
import com.ctfo.storage.model.support.TlUserFunctionLog;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： MySql服务接口<br>
 * 描述： MySql服务接口<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
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
 * <td>2014-11-3</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class MySqlService extends BaseService {

	private static final Logger logger = LoggerFactory.getLogger(MySqlService.class);

	private PreparedStatement tbComInfoStatement = null;

	private PreparedStatement tbTbUserOnlineStatement = null;

	private PreparedStatement monitorStatement = null;

	private PreparedStatement logStatement = null;

	private Map<String, String> sqlMap;

	/**
	 * 修改服务站在线状态
	 * 
	 * @param list
	 */
	public void tbComInfoUpdate(List<Object> list) {
		try {
			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			tbComInfoStatement = conn.prepareStatement(sqlMap.get("sql_updateTbComInfo"));
			for (Object object : list) {
				TbComInfo tbComInfo = (TbComInfo) object;
				tbComInfoStatement.setString(1, tbComInfo.getServiceStatus());
				tbComInfoStatement.setString(2, tbComInfo.getYtCrmLinkedStatus());
				tbComInfoStatement.setString(3, tbComInfo.getMacAddress());
				tbComInfoStatement.setString(4, tbComInfo.getServiceVersion());
				tbComInfoStatement.setString(5, tbComInfo.getSerStationId());
				tbComInfoStatement.addBatch();
			}
			tbComInfoStatement.executeBatch();
			conn.commit();
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (tbComInfoStatement != null) {
					tbComInfoStatement.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 添加在线用户信息
	 * 
	 * @param list
	 */
	public void tbUserOnlineInsert(List<Object> list) {
		try {
			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			tbTbUserOnlineStatement = conn.prepareStatement(sqlMap.get("sql_deleteTbUserOnline"));
			for (Object object : list) {
				TbUserOnline tbUserOnline = (TbUserOnline) object;
				tbTbUserOnlineStatement.setString(1, tbUserOnline.getTbUserOnlineId());
				tbTbUserOnlineStatement.addBatch();
			}
			tbTbUserOnlineStatement.executeBatch();
			tbTbUserOnlineStatement = conn.prepareStatement(sqlMap.get("sql_insertTbUserOnline"));
			for (Object object : list) {
				TbUserOnline tbUserOnline = (TbUserOnline) object;
				tbTbUserOnlineStatement.setString(1, tbUserOnline.getTbUserOnlineId());
				tbTbUserOnlineStatement.setString(2, tbUserOnline.getComCode());
				tbTbUserOnlineStatement.setString(3, tbUserOnline.getComName());
				tbTbUserOnlineStatement.setString(4, tbUserOnline.getSetbookId());
				tbTbUserOnlineStatement.setString(5, tbUserOnline.getSetbookName());
				tbTbUserOnlineStatement.setString(6, tbUserOnline.getClientAccount());
				tbTbUserOnlineStatement.setString(7, tbUserOnline.getRealName());
				tbTbUserOnlineStatement.setString(8, tbUserOnline.getClientAccountOrgid());
				tbTbUserOnlineStatement.setString(9, tbUserOnline.getRoleName());
				tbTbUserOnlineStatement.setString(10, tbUserOnline.getIsOperater());
				tbTbUserOnlineStatement.setLong(11, tbUserOnline.getRegTime());
				tbTbUserOnlineStatement.setLong(12, tbUserOnline.getLoadTime() == null ? 0 : tbUserOnline.getLoadTime());
				tbTbUserOnlineStatement.setString(13, tbUserOnline.getLoadIdAddr());
				tbTbUserOnlineStatement.setString(14, tbUserOnline.getOnlineStatus());
				tbTbUserOnlineStatement.setString(15, tbUserOnline.getClientMac());
				tbTbUserOnlineStatement.addBatch();
			}
			tbTbUserOnlineStatement.executeBatch();
			conn.commit();
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (tbTbUserOnlineStatement != null) {
					tbTbUserOnlineStatement.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 添加用户行为监控信息
	 * 
	 * @param list
	 */
	public void tbUserBehaviorMonitorInsert(List<Object> list) {
		try {
			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			monitorStatement = conn.prepareStatement(sqlMap.get("sql_insertTbUserBehaviorMonitor"));
			for (Object object : list) {
				TbUserBehaviorMonitor monitor = (TbUserBehaviorMonitor) object;
				monitorStatement.setString(1, monitor.getUOperId());
				monitorStatement.setString(2, monitor.getComName());
				monitorStatement.setString(3, monitor.getSetbookName());
				monitorStatement.setString(4, monitor.getClientAccount());
				monitorStatement.setString(5, monitor.getRoleName());
				monitorStatement.setString(6, monitor.getOrgName());
				monitorStatement.setString(7, monitor.getLoadIdAddr());
				monitorStatement.setString(8, monitor.getClientMac());
				monitorStatement.setLong(9, monitor.getWatchTime());
				monitorStatement.setString(10, monitor.getOnlineType());
				monitorStatement.addBatch();
			}
			monitorStatement.executeBatch();
			conn.commit();
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (monitorStatement != null) {
					monitorStatement.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 添加访问统计信息
	 * 
	 * @param list
	 */
	public void tlUserFunctionLogInsert(List<Object> list) {
		try {
			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			logStatement = conn.prepareStatement(sqlMap.get("sql_insertTlUserFunctionLog"));
			for (Object object : list) {
				TlUserFunctionLog log = (TlUserFunctionLog) object;
				logStatement.setString(1, log.getUFLogId());
				logStatement.setString(2, log.getUserId());
				logStatement.setString(3, log.getComId());
				logStatement.setString(4, log.getSetbookId());
				logStatement.setLong(5, log.getAccessTime());
				logStatement.setString(6, log.getFunId());
				logStatement.addBatch();
			}
			logStatement.executeBatch();
			conn.commit();
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (logStatement != null) {
					logStatement.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	public Map<String, String> getSqlMap() {
		return sqlMap;
	}

	public void setSqlMap(Map<String, String> sqlMap) {
		this.sqlMap = sqlMap;
	}

}
