/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service OracleJdbcService.java	</li><br>
 * <li>时        间：2013-9-12  下午6:50:16	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.dao.OracleConnectionPool;
import com.ctfo.statusservice.model.AlarmEnd;
import com.ctfo.statusservice.model.AlarmStart;
import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.model.ParentInfo;
import com.ctfo.statusservice.model.Region;
import com.ctfo.statusservice.model.ServiceUnit;
import com.ctfo.statusservice.util.Cache;
import com.ctfo.statusservice.util.Constant;

/*****************************************
 * <li>描 述：oracle数据服务
 * 
 *****************************************/
public class OracleJdbcService {
	private static final Logger logger = LoggerFactory.getLogger(OracleJdbcService.class);
	/*---------------------------------------参数-------------------------------------------*/
	/** 初始化所有车辆信息缓存SQL */
	private String sql_initAllVehilceCache;
	/** 初始化所有3g视频车辆缓存SQL */
	private String sql_update3GPhotoVehicleInfo;
	/** 更新增量车辆缓存SQL */
	private String sql_updateVehicle;
	/** 更新增量3g视频车辆缓存SQL */
	private String sql_update3GVehicle;
	/** 查询车辆对应企业SQL */
	private String sql_queryVehicleOrgMap;
	/** 查询企业对应报警设置SQL */
	private String sql_queryOrgAlarmCodeMap;
	/** 跨域统计SQL */
	private String sql_insertSpannedStatistics;
	/** 存储报警开始SQL */
	private String sql_saveAlarmStart;
	/** 存储报警结束SQL */
	private String sql_saveAlarmEnd;
	/** 车队父级组织查询SQL */
	private String sql_orgParentSync;
	
	public OracleJdbcService(OracleProperties oracleProperties){
		/** 初始化所有车辆信息缓存SQL */
		this.sql_initAllVehilceCache = oracleProperties.getSql_initAllVehilceCache();
		/** 初始化所有3g视频车辆缓存SQL */
		this.sql_update3GPhotoVehicleInfo = oracleProperties.getSql_update3GPhotoVehicleInfo();
		/** 更新增量车辆缓存SQL */
		this.sql_updateVehicle = oracleProperties.getSql_updateVehicle();
		/** 更新增量3g视频车辆缓存SQL */
		this.sql_update3GVehicle = oracleProperties.getSql_update3GVehicle();
		/** 查询车辆对应企业SQL */
		this.sql_queryVehicleOrgMap = oracleProperties.getSql_queryVehicleOrgMap();
		/** 查询企业对应报警设置SQL */
		this.sql_queryOrgAlarmCodeMap = oracleProperties.getSql_queryOrgAlarmCodeMap();
		/** 跨域统计SQL */
		this.sql_insertSpannedStatistics = oracleProperties.getSql_insertSpannedStatistics();
		/** 存储报警开始SQL */
		this.sql_saveAlarmStart = oracleProperties.getSql_saveAlarmStart();
		/** 存储报警结束SQL */
		this.sql_saveAlarmEnd = oracleProperties.getSql_saveAlarmEnd();
		/** 车队父级组织查询SQL */
		this.sql_orgParentSync = oracleProperties.getSql_orgParentSync();
	}

	/*---------------------------------------业务方法-------------------------------------------*/
	/*****************************************
	 * <li>描 述：初始化所有车辆信息缓存</li><br>
	 * <li>时 间：2013-9-10 上午10:53:43</li><br>
	 * <li>参数：</li><br>
	 * TODO
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
			conn = OracleConnectionPool.getConnection();
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
				su.setAreacode(rs.getString("CITY_ID"));
				su.setVinCode(rs.getString("VIN_CODE"));
				su.setTeamId(rs.getString("TEAM_ID"));
				su.setTeamName(rs.getString("TEAM_NAME")); 
				su.setEntId(rs.getString("ENT_ID"));
				su.setEntName(rs.getString("ENT_NAME"));
				Cache.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
//				logger.info(su.toString());
				index++;
			}
		} catch (SQLException e) {
			logger.error("--initAllVehilceCache--初始化所有车辆信息异常:" + e.getMessage(), e);
		} finally {
			try {
				rs.close();
				stQueryAllVehicle.close();
				conn.close();
			} catch (SQLException e) {
				logger.error("--initAllVehilceCache--初始化所有车辆信息--关闭连接资源异常:" + e.getMessage(), e);
			}

		}
		long end = System.currentTimeMillis();
		logger.info("--initAllVehilceCache--车辆服务信息更新结束,更新车辆数:" + index + ",耗时:" + (end - start));
	}

	/*****************************************
	 * <li>描 述：更新3g手机号对应的车辆缓存信息</li><br>
	 * <li>时 间：2013-7-24 上午9:57:13</li><br>
	 * <li>参数： @param sql <li>参数： @throws SQLException</li><br>
	 * TODO
	 *****************************************/
	public void update3GPhotoVehicleInfo() {
		logger.info("更新3g手机号对应的车辆缓存信息...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		String oemcode;
		String t_identifyno;
		String mac_id;
		ServiceUnit su;
		int index = 0;
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
				su.setAreacode(rs.getString("CITY_ID"));
				su.setVinCode(rs.getString("VIN_CODE"));
				su.setTeamId(rs.getString("TEAM_ID"));
				su.setTeamName(rs.getString("TEAM_NAME")); 
				su.setEntId(rs.getString("ENT_ID"));
				su.setEntName(rs.getString("ENT_NAME"));
				Cache.setVehicleMapValue(mac_id, su);
				index++;
			}
		} catch (SQLException e) {
			logger.error("查询所有车辆" + e.getMessage());
		} finally {
			try {
				rs.close();
				ps.close();
				conn.close();
			} catch (SQLException e) {
				logger.error("--initAllVehilceCache--初始化所有车辆信息--关闭连接资源异常:" + e.getMessage(), e);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("更新3g手机号对应的车辆缓存信息结束,更新数量:" + index + ",耗时:" + (end - start));
	}

	/*****************************************
	 * <li>描 述：更新增量车辆缓存</li><br>
	 * <li>参数：</li><br>
	 * <li>时 间：2013-9-20 下午8:34:21</li><br>
	 * TODO
	 *****************************************/
	public void updateVehilceCache(Long interval) {
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
			ps = conn.prepareStatement(sql_updateVehicle);
			ps.setLong(1, interval);
			ps.setLong(2, interval);
			ps.setLong(3, interval);
			ps.setLong(4, interval);
			ps.setLong(5, interval);
			ps.setLong(6, interval);
			ps.setLong(7, interval);
			ps.setLong(8, interval);
			ps.setLong(9, interval);
			ps.setLong(10, interval);
			ps.setLong(11, interval);
			ps.setLong(12, interval);
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
				su.setAreacode(rs.getString("CITY_ID"));
				su.setVinCode(rs.getString("VIN_CODE"));
				su.setTeamId(rs.getString("TEAM_ID"));
				su.setTeamName(rs.getString("TEAM_NAME")); 
				su.setEntId(rs.getString("ENT_ID"));
				su.setEntName(rs.getString("ENT_NAME"));
				Cache.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				index++;
			}
			conn3g = OracleConnectionPool.getConnection();
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
			ps3g.setLong(11, interval);
			ps3g.setLong(12, interval);
			ps3g.setLong(13, interval);
			ps3g.setLong(14, interval);
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
				su.setAreacode(rs3g.getString("CITY_ID"));
				su.setVinCode(rs3g.getString("VIN_CODE"));
				su.setTeamId(rs3g.getString("TEAM_ID"));
				su.setTeamName(rs3g.getString("TEAM_NAME")); 
				su.setEntId(rs3g.getString("ENT_ID"));
				su.setEntName(rs3g.getString("ENT_NAME"));
				Cache.setVehicleMapValue(mac_id, su);
				index3g++;
			}
		} catch (SQLException e) {
			logger.error("更新增量车辆缓存异常:" + e.getMessage(), e);
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (rs3g != null) {
					rs3g.close();
				}
				if (ps != null) {
					ps.close();
				}
				if (ps3g != null) {
					ps3g.close();
				}
				if (conn != null) {
					conn.close();
				}
				if (conn3g != null) {
					conn3g.close();
				}
			} catch (SQLException e) {
				logger.error("更新增量车辆缓存--关闭连接资源异常:" + e.getMessage(), e);
			}

		}
		long end = System.currentTimeMillis();
		logger.info("更新增量车辆缓存结束,更新车辆数:[" + index + "]条, 3g视频终端[" + index3g + "],耗时:" + (end - start));

	}

	/*****************************************
	 * <li>描 述：车辆清除更新任务</li><br>
	 * 
	 * <pre>
	 * 每天凌晨3点半对缓存的车辆信息进行重新更新，删除所有缓存，然后更新最新数据。
	 * 解决部分车辆删除后还存在缓存中的问题
	 * </pre>
	 * 
	 * <li>时 间：2013-9-22 上午9:47:26</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void clearUpdateVehicle() {
		long start = System.currentTimeMillis();
		synchronized (Cache.registerVehicleMap) {
			initAllVehilceCache();
			update3GPhotoVehicleInfo();
		}
		long end = System.currentTimeMillis();
		logger.info("车辆清除更新任务结束,耗时(ms):" + (end - start));
	}

	/*****************************************
	 * <li>描 述：更新车辆报警设置</li><br>
	 * <li>时 间：2013-7-22 上午9:37:19</li><br>
	 * <li>参数：</li><br>
	 * TODO
	 *****************************************/
	public void updateVehicleAlarmSetting() {
		long start = System.currentTimeMillis();
		PreparedStatement ps_vehicleOrg = null;
		PreparedStatement ps_orgAlarmCode = null;
		ResultSet rs_vehicleOrg = null;
		ResultSet rs_orgAlarmCode = null;
		Connection conn = null;
		int vidEntMapIndex = 0;
		int entAlarmMapIndex = 0;
		try {
			// 查询车辆对应企业列表
			conn = OracleConnectionPool.getConnection();
			ps_vehicleOrg = conn.prepareStatement(sql_queryVehicleOrgMap);
			rs_vehicleOrg = ps_vehicleOrg.executeQuery();
			while (rs_vehicleOrg.next()) {
				Cache.vidEntMap.put(rs_vehicleOrg.getString("VID"), rs_vehicleOrg.getString("ENT_ID"));
				vidEntMapIndex++;
			}
			ps_orgAlarmCode = conn.prepareStatement(sql_queryOrgAlarmCodeMap);
			rs_orgAlarmCode = ps_orgAlarmCode.executeQuery();
			while (rs_orgAlarmCode.next()) {
				Cache.entAlarmMap.put(rs_orgAlarmCode.getString("ENT_ID"), rs_orgAlarmCode.getString("ALARM_CODE"));
			}

			boolean N1Exist = Cache.entAlarmMap.containsKey(Constant.N1);
			for (Map.Entry<String, String> map : Cache.vidEntMap.entrySet()) {
				// 将车辆企业对应表中的企业编号进行替换
				if (Cache.entAlarmMap.containsKey(map.getValue())) {
					Cache.vidEntMap.put(map.getKey(), Cache.entAlarmMap.get(map.getValue()));
					entAlarmMapIndex++;
				} else {
					if (N1Exist) {
						Cache.vidEntMap.put(map.getKey(), Cache.entAlarmMap.get(Constant.N1));
						entAlarmMapIndex++;
					}
				}
			}
		} catch (SQLException e) {
			logger.error("更新车辆报警设置ERROR" + e.getMessage(), e);
		} finally {
			try {
				if (null != rs_orgAlarmCode) {
					rs_orgAlarmCode.close();
				}
				if (null != rs_vehicleOrg) {
					rs_vehicleOrg.close();
				}
				if (null != ps_vehicleOrg) {
					ps_vehicleOrg.close();
				}
				if (null != ps_orgAlarmCode) {
					ps_orgAlarmCode.close();
				}
				if (null != conn) {
					conn.close();
				}
			} catch (Exception ex) {
				logger.error("更新车辆报警设置ERROR:" + ex.getMessage(), ex);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("更新车辆报警设置结束,耗时(ms):" + (end - start) + ",车辆企业对应表数据:[" + vidEntMapIndex + "],企业报警表更新大小:[" + entAlarmMapIndex + "]");
	}
	
	/*****************************************
	 * <li>描 述：存储报警开始</li><br>
	 * <li>时 间：2013-9-22 下午6:26:44</li><br>
	 * <li>参数： @param dataMap 
	 * <br>
	 * TODO
	 * @param batchSize 
	 *****************************************/
	public void saveAlarmStart(List<AlarmStart> list, int batchSize) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveAlarmStart);
			for(AlarmStart as : list){
				index++;
				ps.setString(1, as.getAlarmId());
				ps.setString(2, as.getVid());
				ps.setLong(3, as.getUtc());
				ps.setLong(4, as.getLon());
				ps.setLong(5, as.getLat());
				ps.setLong(6, as.getMaplon());
				ps.setLong(7, as.getMaplat());
				ps.setLong(8, as.getElevation());
				ps.setInt(9,  as.getDirection());
				ps.setInt(10, as.getGpsSpeed());
				ps.setLong(11, as.getMileage());
				ps.setLong(12, as.getOilTotal());
				ps.setString(13, as.getAlarmCode()); 
				ps.setLong(14, as.getSysUtc());
				ps.setInt(15, as.getAlarmStatus());
				ps.setLong(16, as.getAlarmStartUtc());
				ps.setString(17, as.getAlarmDriver());
				ps.setString(18, as.getPlate());
				ps.setString(19, as.getAlarmLevel());
				ps.setString(20, as.getBaseStatus());
				ps.setString(21, as.getExtendStatus());
				ps.setString(22, as.getAlarmAdded());
				ps.setString(23, as.getTeamId());
				ps.setString(24, as.getTeamName());
				ps.setString(25, as.getEntId());
				ps.setString(26, as.getEntName());
				ps.setString(27, as.getDriverId());
				ps.setInt(28, as.getDriverSource());
//				logger.info("------------------写入报警编号:" +as.getAlarmId());  
				ps.addBatch();
				if(index == batchSize){
					ps.executeBatch();
//					int[] inArray = ps.executeBatch();
//					logger.info("------------------写入结果:" + Arrays.toString(inArray));  
					ps.clearBatch();
					index = 0;
				}
			}
			if(index > 0){
				ps.executeBatch();
//				int[] inArray = ps.executeBatch();
//				logger.info("------------------写入结果:" + Arrays.toString(inArray)); 
				ps.clearBatch();
			}
		} catch (SQLException e) {
			logger.error("存储报警开始异常:" + e.getMessage(), e);
		      
		} finally {
			try {
				
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储报警开始关闭资源出错," + e.getMessage(), e);
			}
		} 
	}
	/*****************************************
	 * <li>描 述：存储报警结束</li><br>
	 * <li>时 间：2013-9-22 下午6:26:08</li><br>
	 * <li>参数： @param dataMap 
	 * TODO
	 * @param batchSize 
	 *****************************************/
	public void saveAlarmEnd(List<AlarmEnd> list, int batchSize) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveAlarmEnd);
			for(AlarmEnd as : list){
				index++;
				ps.setLong(1, as.getEndUtc());// ALARM_END_UTC
				ps.setLong(2, as.getLon());// END_LAT
				ps.setLong(3, as.getLat());// END_LON
				ps.setLong(4, as.getMaplon());
				ps.setLong(5, as.getMaplat());
				ps.setLong(6, as.getElevation());
				ps.setInt(7,  as.getDirection());
				ps.setInt(8, as.getGpsSpeed());
				ps.setLong(9, as.getMileage());
				ps.setLong(10, as.getOilTotal());
				ps.setString(11, as.getAlarmAdded());
				ps.setLong(12, as.getMaxRpm());
				ps.setLong(13, as.getMaxSpeed());
				ps.setLong(14, as.getAverageSpeed());
				ps.setString(15, as.getAlarmId());
//				logger.info("------------------更新报警编号:" +as.getAlarmId());  
				ps.addBatch();
				if(index == batchSize){
					ps.executeBatch();
//					int[] inArray = ps.executeBatch();
//					logger.info("------------------更新结果:" + Arrays.toString(inArray));  
					ps.clearBatch();
					index = 0;
				}
			}
			if(index != 0){
				ps.executeBatch();
//				int[] inArray = ps.executeBatch();
//				logger.info("------------------更新结果:" + Arrays.toString(inArray));  
				ps.clearBatch();
			}
		} catch (SQLException e) {
			logger.error("存储报警结束时间异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储报警结束关闭资源出错," + e.getMessage(), e);
			}
		}
	}


	/*****************************************
	 * <li>描 述：保存跨域信息</li><br>
	 * <li>时 间：2013-9-23 下午4:18:56</li><br>
	 * @param batchSize 
	 * @param spannedStatistics  跨域信息
	 * TODO
	 *****************************************/
	public void saveRegion(List<Region> list, int batchSize) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_insertSpannedStatistics);
			for(Region region : list){
				index++;
				ps.setString(1, region.getRegionId());
				ps.setString(2, region.getLocalCode());
				ps.setString(3, region.getCurrentCode());
				ps.setLong(4, region.getCurrentTime());
				ps.setString(5, region.getLocalCityCode());
				ps.setString(6, region.getCurrentCityCode());
				ps.setString(7, region.getLocalProvinceCode());
				ps.setString(8, region.getCurrentProvinceCode());
				ps.addBatch();
				if(index == batchSize){
					ps.executeBatch();
					ps.clearBatch();
					index = 0;
				}
			}
			if(index != 0){
				ps.executeBatch();
			}
		} catch (SQLException e) {
			logger.error("保存跨域信息出错," + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("跨域信息关闭资源出错," + e.getMessage(), e);
			}
		}
	}
	/*****************************************
	 * <li>描        述：组织父级同步任务 		</li><br>
	 * <li>时        间：2013-10-28  下午5:51:37	</li><br>
	 * <li>参数： 组织父级表 key:车队编号 ,value:父级组织编号 以‘,’逗号隔开			</li><br>
	 * TODO
	 *****************************************/
	public void orgParentSync() {
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps= null;
		ResultSet rs = null;
		int index = 0;
		int error = 0;
		Map<String, ParentInfo> orgParent = new ConcurrentHashMap<String, ParentInfo>();
		// 从连接池获得连接
		try {
			conn = OracleConnectionPool.getConnection();
			long q1 = System.currentTimeMillis();
//			查询车队编号的父级企业编号
			ps = conn.prepareStatement(sql_orgParentSync);
			rs = ps.executeQuery();
			while (rs.next()) {
				try{
					ParentInfo bi = new ParentInfo();
					bi.setEntName(rs.getString("ENT_NAME"));
					bi.setParent(rs.getString("PARENT_ID"));
//					组织父级表 key:车队编号 , 企业名称, 父级组织编号 以‘,’逗号隔开
					orgParent.put(rs.getString("MOTORCADE"), bi);
					index++;
				}catch(Exception e){
					error++;
					continue;
				}
			}
			long q2 = System.currentTimeMillis();
			if(logger.isTraceEnabled()){
				logger.trace("orgParentSync--查询组织信息耗时[{}]ms, 组织信息[{}]条", q2-q1, orgParent.size());
				for (Map.Entry<String, ParentInfo> map : orgParent.entrySet()) {
					logger.trace("key:" + map.getKey() + ",value:" + map.getValue().toString());
				}
			}
			if(orgParent.size() > 0){
				Cache.orgParentClear();  
				Cache.putAllOrgParent(orgParent);  
			}
			orgParent.clear();
		} catch (SQLException e) {
			logger.error("orgParentSync--组织父级同步任务异常:"+ e.getMessage(), e);
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
				logger.error("orgParentSync--组织父级同步任务--关闭连接资源异常:" + e.getMessage(), e);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("orgParentSync--组织父级同步,有效数据({}), 异常数据({}), 总数据:[{}], 耗时:[{}]ms", index, error, (index + error), (end - start));
	}
	
	/*-------------------------------------getter & setter-----------------------------------------*/
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

	public String getSql_queryVehicleOrgMap() {
		return sql_queryVehicleOrgMap;
	}

	public void setSql_queryVehicleOrgMap(String sql_queryVehicleOrgMap) {
		this.sql_queryVehicleOrgMap = sql_queryVehicleOrgMap;
	}

	public String getSql_queryOrgAlarmCodeMap() {
		return sql_queryOrgAlarmCodeMap;
	}

	public void setSql_queryOrgAlarmCodeMap(String sql_queryOrgAlarmCodeMap) {
		this.sql_queryOrgAlarmCodeMap = sql_queryOrgAlarmCodeMap;
	}


	public String getSql_saveAlarmStart() {
		return sql_saveAlarmStart;
	}
	public void setSql_saveAlarmStart(String sql_saveAlarmStart) {
		this.sql_saveAlarmStart = sql_saveAlarmStart;
	}
	public String getSql_saveAlarmEnd() {
		return sql_saveAlarmEnd;
	}
	public void setSql_saveAlarmEnd(String sql_saveAlarmEnd) {
		this.sql_saveAlarmEnd = sql_saveAlarmEnd;
	}
	public String getSql_insertSpannedStatistics() {
		return sql_insertSpannedStatistics;
	}
	public void setSql_insertSpannedStatistics(String sql_insertSpannedStatistics) {
		this.sql_insertSpannedStatistics = sql_insertSpannedStatistics;
	}

	
}
