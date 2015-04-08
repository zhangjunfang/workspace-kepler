package com.ctfo.storage.statistics.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.statistics.dao.MySqlDataSource;
import com.ctfo.storage.statistics.model.Vehicle;
import com.ctfo.storage.statistics.util.Cache;


/**
 * MySqlService
 * 
 * 
 * @author huangjincheng
 * 2014-5-15上午10:15:38
 * 
 */
public class MySqlService {
	private static final Logger log = LoggerFactory.getLogger(MySqlService.class);
	
	private String sql_initVehicleCache;
	
	private String sql_insertVehicle;
	
	public MySqlService(){
	}

	private Map<String, String> sqlMap;

	/**
	 * 初始化车辆缓存
	 */
	public void initVehicleCache() {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement stat = null;
		ResultSet result = null;
		try {
			Map<String, Vehicle> map = new HashMap<String, Vehicle>();
			conn = MySqlDataSource.getConnection();
			stat = conn.prepareStatement(sql_initVehicleCache);
			result = stat.executeQuery();
			while(result.next()){
				Vehicle v = new Vehicle();
				v.setVid(result.getString("VID")); 
				v.setEntId(result.getString("ENTID"));
				String phoneNumber = result.getString("PHONE");
				v.setPhoneNumber(phoneNumber);
				v.setPlate(result.getString("PLATE"));
				v.setPlateColor(result.getString("PLATECOLOR"));
				v.setVinCode(result.getString("VINCODE"));
				v.setInnerCode(result.getString("INNERCODE"));
				v.setTid(result.getString("TID"));
				v.setTerminalType(result.getString("TERMINALTYPE"));
				v.setOemCode(result.getString("OEMCODE"));
				v.setEntName(result.getString("ENTNAME"));
				v.setTeamId(result.getString("TEAMID"));
				v.setTeamName(result.getString("TEAMNAME"));
				v.setStaffId(result.getString("STAFFID"));
				v.setStaffName(result.getString("STAFFNAME"));
				v.setOnline(result.getInt("LINE"));
				
				map.put(phoneNumber, v);
			}
			int size = map.size();
			long end = System.currentTimeMillis();
			if(size > 0){
				Cache.setAllVehicle(map); 
			}
			log.info("车辆缓存同步任务执行完成，---查询数据:[{}]条，---耗时:[{}]ms ---更新缓存耗时[{}]ms", size, (end - start),(System.currentTimeMillis() - end));
		} catch (SQLException e) {
			log.error("初始化车辆缓存异常:" +e.getMessage(), e); 
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

	
	/**
	 * @return 获取 sql_initVehicleCache
	 */
	public String getSql_initVehicleCache() {
		return sql_initVehicleCache;
	}
	/**
	 * 设置sql_initVehicleCache
	 * @param sql_initVehicleCache sql_initVehicleCache 
	 */
	public void setSql_initVehicleCache(String sql_initVehicleCache) {
		this.sql_initVehicleCache = sql_initVehicleCache;
	}
	/**
	 * @return 获取 sql_insertVehicle
	 */
	public String getSql_insertVehicle() {
		return sql_insertVehicle;
	}
	/**
	 * 设置sql_insertVehicle
	 * @param sql_insertVehicle sql_insertVehicle 
	 */
	public void setSql_insertVehicle(String sql_insertVehicle) {
		this.sql_insertVehicle = sql_insertVehicle;
	}
	/**
	 * @return 获取 sqlMap
	 */
	public Map<String, String> getSqlMap() {
		return sqlMap;
	}
	/**
	 * 设置sqlMap
	 * @param sqlMap sqlMap 
	 */
	public void setSqlMap(Map<String, String> sqlMap) {
		this.sqlMap = sqlMap;
	}

}
