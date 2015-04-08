package com.ctfo.analy.dao;

import java.sql.SQLException;
import java.util.UUID;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.apache.log4j.Logger;

import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.connpool.OracleConnectionPool;
import com.ctfo.analy.util.GetAddressUtil;

public class GPSInspectionAdapter {
	private static final Logger logger = Logger.getLogger(GPSInspectionAdapter.class);
	
	public GPSInspectionAdapter(){
	}
	
	/*****
	 * 存储巡检记录
	 * @param vehicleMessage
	 */
	public void saveGPSInspectionRecord(VehicleMessageBean vehicleMessage){
		OracleConnection dbCon = null;
		OraclePreparedStatement stGPSInspection = null;
		// 从连接池获得连接
		try {
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stGPSInspection = (OraclePreparedStatement) dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveGpsInspection"));
			stGPSInspection.setExecuteBatch(1);
			stGPSInspection.setString(1, UUID.randomUUID().toString().replace("-", ""));
			stGPSInspection.setString(2, vehicleMessage.getVid());
			stGPSInspection.setLong(3, System.currentTimeMillis());
			stGPSInspection.setInt(4,vehicleMessage.getSpeed());
			stGPSInspection.setInt(5,check("1",Long.toBinaryString(Long.parseLong(vehicleMessage.getBaseStatus())))?1:0);
			stGPSInspection.setLong(6, vehicleMessage.getLat());
			stGPSInspection.setLong(7, vehicleMessage.getLon());
			stGPSInspection.setLong(8, vehicleMessage.getMaplon());
			stGPSInspection.setLong(9, vehicleMessage.getMaplat());
			stGPSInspection.setString(10, GetAddressUtil.getAddress((vehicleMessage.getMaplon()/600000.0)+"" , (vehicleMessage.getMaplat()/600000.0) + ""));
			stGPSInspection.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储GPS巡检记录SQL出错.",e);
		}catch (Exception e) {
			logger.error("存储GPS巡检记录出错.",e);
		}finally{
			try{
				if(null != stGPSInspection){
					stGPSInspection.close();
				}
				if(null != dbCon){
					dbCon.close();
				}
			}catch(Exception e){
				logger.error(e);
			}
		}
		
	}
	
	/**
	 * 判断二进制某位是否是1或0
	 * @param args
	 */
	private boolean check(String num, String result) {
		logger.info("vehicleMessage.getBaseStatus() : " +result );
		boolean bool = false;
		if (result.matches(".*0\\d{"+ num +"}")) { 
			bool = false;
		}
		if (result.matches(".*1\\d{"+ num +"}")) { 
			bool = true;
		}

		return bool;

	}
}
