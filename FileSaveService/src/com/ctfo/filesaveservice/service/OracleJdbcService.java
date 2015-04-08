/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service OracleJdbcService.java	</li><br>
 * <li>时        间：2013-9-12  下午6:50:16	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.dao.OracleConnectionPool;
import com.ctfo.filesaveservice.model.ServiceUnit;
import com.ctfo.filesaveservice.util.Cache;

/*****************************************
 * <li>描 述：oracle数据服务
 * 
 *****************************************/
public class OracleJdbcService {
	private static final Logger logger = LoggerFactory.getLogger(OracleJdbcService.class);
	/*---------------------------------------参数-------------------------------------------*/
	/**	初始化所有车辆信息SQL	*/
	private String sql_initAllVehilceCache;
	/**	更新3g手机号对应的车辆缓存信息SQL	*/
	private String sql_update3GPhotoVehicleInfo;
	/**	更新车辆缓存SQL	*/
	private String sql_updateVehicle;
	/**	更新3g车辆缓存SQL	*/
	private String sql_update3GVehicle;
	
	/*---------------------------------------业务方法-------------------------------------------*/
	/*****************************************
	 * <li>描        述：初始化所有车辆信息 		</li><br>TODO
	 * <li>时        间：2013-9-10  上午10:53:43	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void initAllVehilceCache() {
		logger.info("--initAllVehilceCache--车辆服务信息更新开始...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement stQueryAllVehicle = null;
		ResultSet rs = null;
		String oemcode;
		String t_identifyno;
		ServiceUnit su;
		// 从连接池获得连接
		int index = 0;
		try {
			conn =OracleConnectionPool.getConnection();
			stQueryAllVehicle = conn.prepareStatement(sql_initAllVehilceCache);
			rs = stQueryAllVehicle.executeQuery();
			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("t_identifyno");// 终端标识号
				su.setMacid(oemcode + "_" + t_identifyno);
				su.setSuid(rs.getString("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getString("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getString("tid"));
				su.setCommaddr(t_identifyno);
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setVinCode(rs.getString("VIN_CODE"));
				Cache.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				index++;
			}
		} catch (SQLException e) {
			logger.error("--initAllVehilceCache--初始化所有车辆信息异常:" + e.getMessage(),e);
		} finally {
			try {
				rs.close();
				stQueryAllVehicle.close();
				conn.close();
			} catch (SQLException e) {
				logger.error("--initAllVehilceCache--初始化所有车辆信息--关闭连接资源异常:" + e.getMessage(),e);
			}

		}
		long end = System.currentTimeMillis();
		logger.info("--initAllVehilceCache--车辆服务信息更新结束,更新车辆数:" + index + ",耗时:" + (end - start));
	}

	/*****************************************
	 * <li>描        述：更新3g手机号对应的车辆缓存信息 		</li><br>TODO
	 * <li>时        间：2013-7-24  上午9:57:13	</li><br>
	 * <li>参数： @param sql
	 * <li>参数： @throws SQLException			</li><br>
	 * 
	 *****************************************/
	public void update3GPhotoVehicleInfo() {
		logger.info("更新3g手机号对应的车辆缓存信息...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps= null;
		ResultSet rs = null;
		String oemcode;
		String t_identifyno;
		String mac_id;
		ServiceUnit su;
		int index  = 0;
		// 从连接池获得连接
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_update3GPhotoVehicleInfo);
			rs = ps.executeQuery();

			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("dvr_simnum");// 终端标识号
				mac_id = oemcode + "_" + t_identifyno;
				su.setMacid(mac_id);
				su.setSuid(rs.getString("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getString("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getString("tid"));
				su.setCommaddr(rs.getString("t_identifyno"));
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setVinCode(rs.getString("VIN_CODE"));
				Cache.setVehicleMapValue(mac_id, su);
				index++;
			}
		} catch (SQLException e) {
			logger.error("查询所有车辆"+ e.getMessage());
		} finally {
			try {
				rs.close();
				ps.close();
				conn.close();
			} catch (SQLException e) {
				logger.error("--initAllVehilceCache--初始化所有车辆信息--关闭连接资源异常:" + e.getMessage(),e);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("更新3g手机号对应的车辆缓存信息结束,更新数量:"+index+",耗时:"+(end - start));
	}
	/*****************************************
	 * <li>描        述：更新车辆缓存 		</li><br> TODO
	 * <li>参数：  		</li><br>
	 * <li>时        间：2013-9-20  下午8:34:21	</li><br>
	 * 
	 *****************************************/
	public void updateVehilceCache(Long interval) {
//		logger.info("更新不存在车辆缓存...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Connection conn3g = null;
		PreparedStatement ps3g = null;
		ResultSet rs3g = null;
		
		String oemcode;
		String t_identifyno;
		ServiceUnit su;
		// 从连接池获得连接
		int index = 0;
		int index3g = 0;
		try {
			conn = OracleConnectionPool.getConnection();
//			logger.info(sql_updateVehicle);
			ps = conn.prepareStatement(sql_updateVehicle);
			ps.setLong(1, interval);
			ps.setLong(2, interval);
			ps.setLong(3, interval);
			ps.setLong(4, interval);
			ps.setLong(5, interval);
			ps.setLong(6, interval);
			ps.setLong(7, interval);
			ps.setLong(8, interval);
			rs = ps.executeQuery();
			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("t_identifyno");// 终端标识号
				su.setMacid(oemcode + "_" + t_identifyno);
				su.setSuid(rs.getString("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getString("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getString("tid"));
				su.setCommaddr(t_identifyno);
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setVinCode(rs.getString("VIN_CODE"));
				Cache.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				index++;
			}
			conn3g = OracleConnectionPool.getConnection();
//			logger.info(sql_update3GVehicle);
			ps3g = conn3g.prepareStatement(sql_update3GVehicle);
			ps3g.setLong(1, interval);
			ps3g.setLong(2, interval);
			ps3g.setLong(3, interval);
			ps3g.setLong(4, interval);
			ps3g.setLong(5, interval);
			ps3g.setLong(6, interval);
			ps3g.setLong(7, interval);
			ps3g.setLong(8, interval);
			ps3g.setLong(9, interval);
			ps3g.setLong(10, interval);
			rs3g = ps3g.executeQuery();
			String mac_id = "";
			while (rs3g.next()) {
				su = new ServiceUnit();
				oemcode = rs3g.getString("oemcode");// 车机类型码
				t_identifyno = rs3g.getString("dvr_simnum");// 终端标识号
				mac_id = oemcode + "_" + t_identifyno;
				su.setMacid(mac_id);
				su.setSuid(rs3g.getString("suid"));
				su.setPlatecolorid(rs3g.getString("plate_color_id"));
				su.setVid(rs3g.getString("vid"));
				su.setTeminalCode(rs3g.getString("tmodel_code"));
				su.setTid(rs3g.getString("tid"));
				su.setCommaddr(rs3g.getString("t_identifyno"));
				su.setOemcode(oemcode);
				su.setVehicleno(rs3g.getString("vehicle_no"));
				su.setVinCode(rs3g.getString("VIN_CODE"));
				Cache.setVehicleMapValue(mac_id, su);
				index3g++;
			}
		} catch (SQLException e) {
			logger.error("更新不存在车辆缓存异常:" + e.getMessage(),e);
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(rs3g != null){
					rs3g.close();
				}
				if(ps != null){
					ps.close();
				}
				if(ps3g != null){
					ps3g.close();
				}
				if(conn != null){
					conn.close();
				}
				if(conn3g != null){
					conn3g.close();
				}
			} catch (SQLException e) {
				logger.error("更新车辆缓存--关闭连接资源异常:" + e.getMessage(),e);
			}

		}
		long end = System.currentTimeMillis();
		logger.info("更新车辆缓存结束,更新车辆数:[" + index + "]条, 3g视频终端["+index3g+"],耗时:" + (end - start));
	
		
	}
	/*****************************************
	 * <li>描        述：车辆清除更新任务 		</li><br>  TODO
	 * <pre>
	 * 每天凌晨3点半对缓存的车辆信息进行重新更新，删除所有缓存，然后更新最新数据。
	 * 解决部分车辆删除后还存在缓存中的问题
	 * </pre>
	 * <li>时        间：2013-9-22  上午9:47:26	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public  void clearUpdateVehicle() {
		long start = System.currentTimeMillis();
		synchronized (Cache.registerVehicleMap) {
			Cache.registerVehicleMap.clear();
			initAllVehilceCache();
			update3GPhotoVehicleInfo();
		}
		long end = System.currentTimeMillis();
		logger.info("车辆清除更新任务结束,耗时(ms):" + (end - start));
	}
	
	/*-------------------------------------------get & set----------------------------------------*/
	public String getSql_initAllVehilceCache() {
		return sql_initAllVehilceCache;
	}
	public void setSql_initAllVehilceCache(String sql_initAllVehilceCache) {
		this.sql_initAllVehilceCache = sql_initAllVehilceCache;
	}
	public String getSql_update3GPhotoVehicleInfo() {
		return sql_update3GPhotoVehicleInfo;
	}
	public void setSql_update3GPhotoVehicleInfo(String sql_update3GPhotoVehicleInfo) {
		this.sql_update3GPhotoVehicleInfo = sql_update3GPhotoVehicleInfo;
	}
	public String getSql_updateVehicle() {
		return sql_updateVehicle;
	}
	public void setSql_updateVehicle(String sql_updateVehicle) {
		this.sql_updateVehicle = sql_updateVehicle;
	}
	public String getSql_update3GVehicle() {
		return sql_update3GVehicle;
	}
	public void setSql_update3GVehicle(String sql_update3GVehicle) {
		this.sql_update3GVehicle = sql_update3GVehicle;
	}
}
