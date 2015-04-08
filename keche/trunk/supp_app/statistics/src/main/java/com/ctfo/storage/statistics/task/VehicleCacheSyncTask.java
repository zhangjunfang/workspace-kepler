/**
 * 
 */
package com.ctfo.storage.statistics.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.statistics.model.Vehicle;
import com.ctfo.storage.statistics.util.Cache;
import com.ctfo.storage.statistics.util.TaskAdapter;


/**
 * 车辆缓存同步任务
 *
 */
public class VehicleCacheSyncTask extends TaskAdapter {
	private static Logger log = LoggerFactory.getLogger(VehicleCacheSyncTask.class);
	
	
	public VehicleCacheSyncTask(){
		setName("VehicleCacheSyncTask");
	}
	
	/**
	 * 
	 */
	@Override
	public void init() {
		execute();
	}

	/**
	 * 更新车辆缓存
	 */
	@SuppressWarnings("static-access")
	@Override
	public void execute() {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement stat = null;
		ResultSet result = null;
		try {
			Map<String, Vehicle> map = new HashMap<String, Vehicle>();
			conn = mysql.getConnection();
			stat = conn.prepareStatement(config.get("sql_initVehicleCache"));
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
}
