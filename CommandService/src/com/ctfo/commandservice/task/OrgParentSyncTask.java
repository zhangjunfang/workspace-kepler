/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService		</li><br>
 * <li>文件名称：com.ctfo.statusservice.task EntAlarmSettingSyncTask.java	</li><br>
 * <li>时        间：2013-9-27  下午2:30:46	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.dao.OracleConnectionPool;
import com.ctfo.commandservice.util.Cache;



/*****************************************
 * <li>描        述:组织父级同步任务		
 * 
 *****************************************/
public class OrgParentSyncTask extends TaskAdapter{
	private static final Logger logger = LoggerFactory.getLogger(OrgParentSyncTask.class);
	/**	全量间隔时间	*/
	private static long intervalTime;
	/**	最近时间	*/
	private static long lastTime = 0;
	/**	全量同步SQL	*/
	private static String sql_syncAll;
	/**	增量同步SQL	*/
	private static String sql_syncIncrements;
	/**	同步时间	*/
	private static long syncTime;
	
	@Override
	public void init() {
		syncTime = System.currentTimeMillis() - 60000;
		intervalTime = Integer.parseInt(config.get("intervalTime")) * 60 * 1000; 
		sql_syncAll 		= config.get("sql_syncAll");
		sql_syncIncrements 	= config.get("sql_syncIncrements");
		execute();
	}

	@Override
	public void execute() {
		try {
			long currentTime = System.currentTimeMillis();
			if((currentTime - lastTime) > intervalTime){
				orgParentSync(true); 
				lastTime = System.currentTimeMillis();
			} else {
				orgParentSync(false); 
			}
		} catch (Exception e) {
			logger.error("组织父级同步任务异常:" + e.getMessage(), e);
		}
	}
	
	private void orgParentSync(boolean allFlag) {
		long start = System.currentTimeMillis();
		long s1 = start;
		long s2 = start;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		int index = 0;
		int error = 0;
		Map<String, String> orgParent = new ConcurrentHashMap<String, String>();
		String type = null;
		// 从连接池获得连接
		try {
			conn = OracleConnectionPool.getConnection();
			s1 = System.currentTimeMillis();
			if(allFlag){
				ps = conn.prepareStatement(sql_syncAll);
				type = "全量同步父级组织";
			} else {
				ps = conn.prepareStatement(sql_syncIncrements);
				for(int i = 1; i <= 4; i++){
					ps.setLong(i, syncTime);
				} 
				type = "增量同步父级组织";
			}
			rs = ps.executeQuery();
			s2 = System.currentTimeMillis();
			while (rs.next()) {
				try {
					// 组织父级表 key:车队编号 ,value:父级组织编号 以‘,’逗号隔开
					orgParent.put(rs.getString("MOTORCADE"), "," + rs.getString("PARENT_ID") + ",");
					index++;
				} catch (Exception e) {
					error++;
					continue;
				}
			}
			if (orgParent.size() > 0) {
				if(allFlag){
					Cache.clearOrgParent(); 
					Cache.putAllOrgParent(orgParent);
				} else {
					Cache.putAllOrgParent(orgParent);
				}
				orgParent.clear();
			}
		} catch (SQLException e) {
			logger.error("组织父级同步任务异常:" + e.getMessage(), e);
		} finally {
			try {
				if (null != rs) {
					rs.close();
				}
				if (null != ps) {
					ps.close();
				}
				if (null != conn) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("组织父级同步任务--关闭连接资源异常:" + e.getMessage(), e);
			}
			syncTime = start - 60000;
		}
		long end = System.currentTimeMillis();
		logger.info("{}任务结束,有效[{}]条, 异常[{}]条, 合计:[{}]条, 获取连接[{}]ms, 查询[{}]ms, 处理[{}]ms, 总耗时[{}]ms",type, index, error, (index + error), s1-start, s2-s1, end-s2, end-start);
	}
	
}
