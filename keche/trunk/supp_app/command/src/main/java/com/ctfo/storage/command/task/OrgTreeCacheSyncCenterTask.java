package com.ctfo.storage.command.task;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.model.SysOrgTreeModel;
import com.ctfo.storage.command.service.RedisService;
import com.ctfo.storage.command.util.TaskAdapter;



/**
 * 组织树缓存同步任务
 *
 */
public class OrgTreeCacheSyncCenterTask extends TaskAdapter {
	private static Logger log = LoggerFactory.getLogger(OrgTreeCacheSyncCenterTask.class);
	private RedisService redisService = new RedisService();

	
	
	public OrgTreeCacheSyncCenterTask(){
		setName("OrgTreeCacheSyncCenterTask");
	}
	
	/**
	 * 
	 */
	@Override
	public void init() {
		setName("OrgTreeCacheSyncCenterTask");
		log.info("主中心组织树同步程序启动初始化！");
		execute();
	}

	/**
	 * 更新组织树缓存
	 */
	@SuppressWarnings("static-access")
	@Override
	public void execute() {
		long start = System.currentTimeMillis();
		PreparedStatement stat = null;
		ResultSet result = null;
		List<String> orgTreeKeylist = new ArrayList<String>();
		
		
		try {
			Map<String, String> map = new HashMap<String, String>();
			conn = mysql.getConnection();
			stat = conn.prepareStatement(config.get("sql_initAllCenterParentId"));
			result = stat.executeQuery();
			while(result.next()){
				StringBuffer buffKey = new StringBuffer();
				buffKey.append("HS_ENTID").append(result.getString("ENT_ID")).append("_10");
				orgTreeKeylist.add(buffKey.toString());
				
			}
			//log.info("--OrgTreeCacheSyncTask--组织树缓存KEY列表获取完成，KEY数目:[{}]",orgTreeKeylist.size());
			List<SysOrgTreeModel> list = new ArrayList<SysOrgTreeModel>();
			stat = conn.prepareStatement(config.get("sql_allCenterOrgTree"));
			result = stat.executeQuery();
			while(result.next()){
				SysOrgTreeModel sysOrgTreeModel = new SysOrgTreeModel();
				sysOrgTreeModel.setEntId(result.getString("ENT_ID"));
				sysOrgTreeModel.setParentId(result.getString("PARENT_ID"));
				sysOrgTreeModel.setCenterCode("10");
				list.add(sysOrgTreeModel);			
			}
			for(String key:orgTreeKeylist){
				StringBuffer buffValue = new StringBuffer();
				getOrgTree(key,buffValue,list);
				map.put(key, key.split("_")[1].substring(5)+buffValue.toString());
				//log.info("组织树缓存MAP集合[{}]正在同步，集合大小:[{}]",orgTreeKeylist.size(),map.size());
			}
			redisService.saveOrgTreeList(map);
			log.info("--OrgTreeCacheSyncCenterTask--主中心组织树缓存同步任务执行完成, ---同步数据:[{}]条, 正常处理:[{}]条, 异常：[{}]条, ---总耗时:[{}]秒 ", map.size(),map.size(),(orgTreeKeylist.size()-map.size()), (System.currentTimeMillis() - start)/1000.0);
		} catch (SQLException e) {
			log.error("初始化组织树缓存异常:" +e.getMessage(), e); 
		}finally{
			try {
				if(result != null){
					result.close();
				}
				if(stat != null){
					stat.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e2) {
				log.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	
	public void getOrgTree(String treeKey,StringBuffer buff,List<SysOrgTreeModel> list){
		String[] arr = treeKey.split("_");
		String orgId = arr[1].substring(5);
		String centerCode = arr[2];
		try {
			for(SysOrgTreeModel sysOrgTreeModel:list){
				if(sysOrgTreeModel.getCenterCode().equals(centerCode) && sysOrgTreeModel.getParentId().equals(orgId)){
					String key = "HS_ENTID"+sysOrgTreeModel.getEntId()+"_"+sysOrgTreeModel.getCenterCode();
					String value = ","+sysOrgTreeModel.getEntId();
					buff.append(value);
					getOrgTree(key, buff, list);
				}
			}
			
		} catch (Exception e) {
			log.error("递归组织树缓存异常:" +e.getMessage(), e); 
		}
	}
}
