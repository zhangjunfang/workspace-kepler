package com.ctfo.mileageservice.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mileageservice.dao.OracleConnectionPool;
import com.ctfo.mileageservice.dao.ThreadPool;
import com.ctfo.mileageservice.model.DriverDetailBean;
import com.ctfo.mileageservice.model.VehicleInfo;
import com.ctfo.mileageservice.model.VehicleMessageBean;
import com.ctfo.mileageservice.model.VehicleStatus;
import com.ctfo.mileageservice.util.ConfigLoader;


/**
 * 文件名：OracleService.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-25上午9:45:20
 * 
 */
public class OracleService {
	private static final Logger logger = LoggerFactory.getLogger(OracleService.class);
	/**	查询车辆信息SQL	*/
	private static String sql_queryVehicleInfo;
	/**	更新车辆状态SQL	*/
	private static String sql_saveDayServiceStInfo;
	/** 获取每天上报里程*/
	private static String sql_queryTotalMileageInfo;
	/** 更新总里程 */
	private static String sql_updateStaInfo;	
	/**	保存驾驶员明细SQL*/
	private static String sql_saveDriverDetailInfo;
	/**	 查询驾驶员上下打卡信息SQL*/
	private static String sql_queryDriverClockinDetail;
	/** 补录数据删除SQL*/
	private static String sql_deleteVehicleInfo;
	/** 补录数据删除SQL*/
	private static String sql_deleteDriverInfo;
	/**
	 * 初始化SQL脚本
	 */
	public static void init(){
		sql_saveDayServiceStInfo = ConfigLoader.config.get("sql_saveDayServiceStInfo");
		sql_queryVehicleInfo = ConfigLoader.config.get("sql_queryVehicleInfo");
		sql_saveDriverDetailInfo = ConfigLoader.config.get("sql_saveDriverDetailInfo");
		sql_queryDriverClockinDetail = ConfigLoader.config.get("sql_queryDriverClockinDetail");
		sql_queryTotalMileageInfo = ConfigLoader.config.get("sql_queryTotalMileageInfo");
		sql_updateStaInfo = ConfigLoader.config.get("sql_updateStaInfo");
		sql_deleteVehicleInfo = ConfigLoader.config.get("sql_deleteVehicleInfo");
		sql_deleteDriverInfo = ConfigLoader.config.get("sql_deleteDriverInfo");
		
	}
	
	/***
	 *  存储车辆运行状态信息
	 * @throws SQLException
	 */
	public static void saveStaDayInfo(Connection dbCon,List<VehicleStatus> list) throws Exception{
		PreparedStatement stSaveDayStInfo = null;
		stSaveDayStInfo = dbCon.prepareStatement(sql_saveDayServiceStInfo);
		String vid = "";
		try{	
			for(VehicleStatus vehicleStatus : list){
				vid = vehicleStatus.getVid();
				VehicleInfo info = queryVechileInfo(vid);		
				stSaveDayStInfo.setLong(1, vehicleStatus.getStatDate()); // 日期UTC
				stSaveDayStInfo.setString(2, vehicleStatus.getVid()); // 车辆编号
				if(info != null){
					stSaveDayStInfo.setString(3, info.getVehicleNo()); // 车牌号码
					stSaveDayStInfo.setString(4, info.getVinCode()); // 车架号(VIN)
					stSaveDayStInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDayStInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDayStInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDayStInfo.setString(8, info.getTeamName()); // 车队名称		
					stSaveDayStInfo.setLong(9, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程			
					stSaveDayStInfo.setLong(10, vehicleStatus.getGis_milege()); // GIS计算里程		
					stSaveDayStInfo.setLong(11, vehicleStatus.getMileage());//里程
					stSaveDayStInfo.setLong(12, System.currentTimeMillis());//utc
					stSaveDayStInfo.addBatch();
				}
			}
			stSaveDayStInfo.executeBatch();
		}catch(SQLException e){
			logger.debug("存储车辆行驶里程统计出错：VID="+vid+" ",e);
		}finally{
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		
		
	}
	
	
	
	/**
	 * 查询车辆编号，车架号，企业编号，企业名称，车队编号，车队名称,车牌号、车辆内部编码、线路、司机
	 * 
	 * @param vid
	 * @return
	 * @throws SQLException
	 */
	public static synchronized VehicleInfo queryVechileInfo(String vid){
		VehicleInfo info = (VehicleInfo) ThreadPool.vehicleInfoMap.get(vid);
		if (info == null) {
			ResultSet rs = null;
			Connection dbCon = null;

			PreparedStatement stQueryVehicleInfo = null;
			try {
				dbCon = OracleConnectionPool.getConnection();
				stQueryVehicleInfo = dbCon.prepareStatement(sql_queryVehicleInfo);
				stQueryVehicleInfo.setString(1, vid);
				rs = stQueryVehicleInfo.executeQuery();
				if (rs.next()) {
					info = new VehicleInfo();
					info.setVid(rs.getString("VID"));
					info.setVehicleNo(rs.getString("VEHICLE_NO"));
					info.setVinCode(rs.getString("VIN_CODE"));
					info.setEntId(rs.getString("EID"));
					info.setEntName(rs.getString("ENAME"));
					info.setTeamId(rs.getString("ENT_ID"));
					info.setTeamName(rs.getString("ENT_NAME"));
					info.setInnerCode(rs.getString("INNER_CODE"));
					info.setVehicleType(rs.getString("VEHICLE_TYPE"));
					info.setCfgFlag(rs.getString("CFG_FLAG"));
				}
			} catch (SQLException e) {
				logger.debug("查询基本信息出错", e);
			} finally {
				try{
				if (rs != null) {
					rs.close();
				}

				if (stQueryVehicleInfo != null) {
					stQueryVehicleInfo.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
				}catch(Exception ex){
					logger.error("释放数据库连接出错.",ex);
				}
			}
			ThreadPool.vehicleInfoMap.put(vid, info);
		}

		return info;
	}



	/**
	 * 更新里程累计表
	 * @throws SQLException
	 */
	public static void updateVehicleStat(Long utc) throws SQLException{
		PreparedStatement stUpdateStaInfo = null;
		PreparedStatement stSelectStatInfo = null;
		ResultSet rs = null;
		Connection dbCon = null;
		try{
			dbCon = OracleConnectionPool.getConnection();
			stSelectStatInfo = dbCon.prepareStatement(sql_queryTotalMileageInfo);
			stUpdateStaInfo = dbCon.prepareStatement(sql_updateStaInfo);
			stSelectStatInfo.setLong(1, utc);
			int count = 0;
			rs = stSelectStatInfo.executeQuery();
			while(rs.next()){
				stUpdateStaInfo.setLong(1, rs.getLong("MILEAGE"));
				stUpdateStaInfo.setString(2, rs.getString("VID"));
				stUpdateStaInfo.addBatch();
				count++;
				if(count % 100 == 0){
					stUpdateStaInfo.executeBatch();
					count = 0;
					stUpdateStaInfo.clearBatch();
				}
			}
			
			if(count >0){
				stUpdateStaInfo.executeBatch(); // 更新车辆总累计表
			}
		}catch (Exception e) {
			logger.debug("更新总里程出错！",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stSelectStatInfo != null){
				stSelectStatInfo.close();
			}
			
			if(stUpdateStaInfo != null){
				stUpdateStaInfo.close();
			}
			
			if(dbCon != null){
				dbCon.close();
			}
		}
	}
	
	public static void deleteRestoreInfo(long utc){
		Connection dbCon = null;
		PreparedStatement stdeleteVehicleInfo = null;
		PreparedStatement stdeleteDriverInfo = null;
		try {
			dbCon = OracleConnectionPool.getConnection();
		
			stdeleteVehicleInfo = dbCon.prepareStatement(sql_deleteVehicleInfo);
			stdeleteVehicleInfo.setLong(1, utc);
			stdeleteVehicleInfo.execute();
			
			stdeleteDriverInfo = dbCon.prepareStatement(sql_deleteDriverInfo);
			stdeleteDriverInfo.setLong(1, utc);
			stdeleteDriverInfo.execute();
			logger.info("--------------------------------行驶里程补录数据删除成功");
		} catch (SQLException e) {
			logger.debug("里程统计补录删除数据异常！",e);
		}finally{
			try {
				if(stdeleteVehicleInfo != null){	
					stdeleteVehicleInfo.close();			
				}
				if(stdeleteDriverInfo != null){	
					stdeleteDriverInfo.close();			
				}
				if(dbCon != null){
					dbCon.close();
				}
			} catch (SQLException ex) {
				logger.error("释放数据库连接出错.",ex);
			}
		}
		
	}
	
	/***
	 *  存储驾驶员驾驶明细数据
	 * @throws SQLException
	 */
	public static void saveDriverDetail(Connection connection,List<DriverDetailBean> driverDetaillist) throws Exception{
		if (driverDetaillist.size()==0){
			return ;
		}
		PreparedStatement stSaveDriverDetailInfo = null;
		try{
			
			stSaveDriverDetailInfo = connection.prepareStatement(sql_saveDriverDetailInfo);
			
			for (int i = 0;i < driverDetaillist.size(); i++){
				DriverDetailBean ddb = driverDetaillist.get(i);
				stSaveDriverDetailInfo.setString(1, ddb.getDetailId());
				stSaveDriverDetailInfo.setLong(2, ddb.getStatDate()); // 日期UTC
				stSaveDriverDetailInfo.setString(3, ddb.getVid()); // 车辆编号
				
				VehicleInfo info = queryVechileInfo(ddb.getVid());
				if(info != null){
					stSaveDriverDetailInfo.setString(4, info.getVehicleNo()); // 车牌号码
					stSaveDriverDetailInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDriverDetailInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDriverDetailInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDriverDetailInfo.setString(8, info.getTeamName()); // 车队名称
				} else {
					stSaveDriverDetailInfo.setString(4, null); // 车牌号码
					stSaveDriverDetailInfo.setString(5, null); // 企业ID
					stSaveDriverDetailInfo.setString(6, null); // 企业名称
					stSaveDriverDetailInfo.setString(7, null); // 车队ID
					stSaveDriverDetailInfo.setString(8, null); // 车队名称
				}

				VehicleMessageBean beginVmb = ddb.getBeginVmb();
				
				stSaveDriverDetailInfo.setString(9, beginVmb.getDriverId()); // 驾驶员编号
				stSaveDriverDetailInfo.setString(10, beginVmb.getDriverName()); // 驾驶员名称
				stSaveDriverDetailInfo.setString(11, beginVmb.getDriverSrc()); // 驾驶员信息来源
				
				stSaveDriverDetailInfo.setLong(12, beginVmb.getUtc()); // 开始时间
				stSaveDriverDetailInfo.setLong(13, beginVmb.getLon()); // 经度
				stSaveDriverDetailInfo.setLong(14, beginVmb.getLat()); // 维度
				stSaveDriverDetailInfo.setLong(15, beginVmb.getMaplon()); // 地图经度
				stSaveDriverDetailInfo.setLong(16, beginVmb.getMaplat()); // 地图维度
				stSaveDriverDetailInfo.setLong(17, beginVmb.getMileage()); //里程
				
				VehicleMessageBean endVmb = ddb.getEndVmb();
				stSaveDriverDetailInfo.setLong(18, endVmb.getUtc()); // 开始时间
				stSaveDriverDetailInfo.setLong(19, endVmb.getLon()); // 经度
				stSaveDriverDetailInfo.setLong(20, endVmb.getLat()); // 维度
				stSaveDriverDetailInfo.setLong(21, endVmb.getMaplon()); // 地图经度
				stSaveDriverDetailInfo.setLong(22, endVmb.getMaplat()); // 地图维度
				stSaveDriverDetailInfo.setLong(23, endVmb.getMileage()); //里程
				
				stSaveDriverDetailInfo.setLong(24, ddb.getMileage()); //驾驶员驾驶里程
				stSaveDriverDetailInfo.addBatch();
			}
			
			stSaveDriverDetailInfo.executeBatch();
		}catch (Exception e) {
			logger.debug("驾驶员明细录入出错", e);
		}finally{
			try{
				if (stSaveDriverDetailInfo != null) {
					stSaveDriverDetailInfo.close();
				}
				if (connection != null) {
					connection.close();
				}
				}catch(Exception ex){
					logger.error("释放数据库连接出错.",ex);
				}
		}
	}
			
	
	
	
	/**
	 * 查询驾驶员在给定时段内的打卡明细
	 * @param dbCon
	 * @param vid
	 * @param beginUtc
	 * @param endUtc
	 * @return
	 * @throws Exception
	 */
	public static List<String> selectDriverClockinDetail(String vid,long beginUtc,long endUtc) throws Exception{
		PreparedStatement stSelectClockin = null;
		Connection dbCon = null;
		ResultSet rs = null;
		List<String> ls = new ArrayList<String>();
		 try {
			 dbCon= OracleConnectionPool.getConnection();
			 stSelectClockin = dbCon.prepareStatement(sql_queryDriverClockinDetail);
			 stSelectClockin.setString(1, vid);
			 stSelectClockin.setLong(2, beginUtc);
			 stSelectClockin.setLong(3, endUtc);
			 stSelectClockin.setLong(4, beginUtc);
			 stSelectClockin.setLong(5, endUtc);
			 stSelectClockin.setString(6, vid);
			 
			 rs = stSelectClockin.executeQuery();
			 while(rs.next()){
				 ls.add(rs.getString("DEVICE_ID") + ";" + rs.getString("DRIVER_NAME") + ";"+ rs.getString("DRIVER_SRC") + ";" + rs.getLong("ON_LINE_TIME") + ";" +  rs.getLong("OFF_LINE_TIME"));
			 }
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			if(rs != null){
				rs.close();
			}
			if(stSelectClockin != null){
				stSelectClockin.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
		return ls;
	}

			
	public static String notNull(String s){
		if("".equals(s) || null == s){
			return "0";
		}
		return s;
	}
	
	

}
