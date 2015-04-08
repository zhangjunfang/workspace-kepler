package com.ctfo.storage.process.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.dao.MySqlDataSource;
import com.ctfo.storage.process.model.Vehicle;
import com.ctfo.storage.process.model.VehicleTemp;
import com.ctfo.storage.process.util.Cache;

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
				v.setOnline(result.getInt("ISONLINE"));
				
				map.put(phoneNumber, v);
			}
			int size = map.size();
			if(size > 0){
				Cache.setAllVehicle(map); 
			}
			log.info("【缓存同步】车辆缓存同步任务执行完成，---查询数据:[{}]条，---耗时:[{}]ms", size, (System.currentTimeMillis() - start));
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

	public void insertVehicle(List<VehicleTemp> list){
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement stat = null;
		int size = 0;
		try {
			conn = MySqlDataSource.getConnection();
			stat = conn.prepareStatement(sql_insertVehicle);
			for(VehicleTemp v : list){
				stat.setLong(1, v.getVid());
				stat.setLong(2, v.getEntId());
				stat.setLong(3, v.getPhoneNumber());
				stat.setString(4, v.getPlate());
				stat.setInt(5, v.getPlateColor());
				stat.setString(6, v.getVinCode());
				stat.setLong(7, v.getInnerCode());
				stat.setLong(8, v.getTid());
				stat.setLong(9, v.getTerminalType());
				stat.setString(10, v.getOemCode());
				stat.setString(11, v.getEntName());
				stat.setLong(12, v.getTeamId());
				stat.setString(13, v.getTeamName());
				stat.setLong(14, v.getStaffId());
				stat.setString(15, v.getStaffName());
				stat.setInt(16, v.getOnline());
				stat.addBatch();
				size++;
			}
			stat.executeBatch();
			
			log.info("批量存入不存在车辆完成，---存储数据:[{}]条，---耗时:[{}]ms", size, (System.currentTimeMillis() - start));
		} catch (SQLException e) {
			log.error("批量存入不存在车辆异常:" +e.getMessage(), e); 
		}finally{
			try {
				if(stat != null){
					stat.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e2) {
				log.error("批量存入不存在车辆-关闭资源异常:"+e2.getMessage(), e2); 
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
