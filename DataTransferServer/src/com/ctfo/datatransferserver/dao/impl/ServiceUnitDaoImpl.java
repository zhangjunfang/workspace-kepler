package com.ctfo.datatransferserver.dao.impl;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.DataPool;
import com.ctfo.datatransferserver.beans.ServiceUnitBean;
import com.ctfo.datatransferserver.dao.BaseDao;
import com.ctfo.datatransferserver.dao.RowMapper;
import com.ctfo.datatransferserver.dao.ServiceUnitDao;
import com.lingtu.xmlconf.XmlConf;

/**
 * 查询车辆信息接口实现
 * 
 * @author yangyi
 * 
 */
public class ServiceUnitDaoImpl extends BaseDao implements ServiceUnitDao, RowMapper<ServiceUnitBean> {

	private static final Logger logger = LoggerFactory.getLogger(ServiceUnitDaoImpl.class);
	String queryAllVehicleSQL;
	String queryVehicleByMacidSQL;
	String queryOfflineVehicleSQL;
	int flag = 0;

	/**
	 * 初始化SQL
	 * 
	 * @param config
	 */
	@Override
	public void initDBAdapter(XmlConf config) {

		// 查询实体ID所属企业，车队
		queryAllVehicleSQL = config.getStringValue("database|sqlstatement|sql_queryAllVehicle");

		// 根据macid查询车辆基本信息
		queryVehicleByMacidSQL = config.getStringValue("database|sqlstatement|sql_queryVehicleByMacid");

		// 查询下线车辆
		queryOfflineVehicleSQL = config.getStringValue("database|sqlstatement|sql_queryOfflineVehicle");

		logger.debug("init ServiceUnitDaoImpl");
	}

	/**
	 * 查询车辆信息
	 */
	@Override
	public void queryAllVehicle() {
		flag = 0;
		query(queryAllVehicleSQL, null, this);
		logger.info("查询到车辆总数[" + DataPool.getVehicleMapSize() + "]");
	}

	/**
	 * 查询下线车辆
	 */
	@Override
	public void queryOfflineVehicle() {
		flag = 1;
		query(queryOfflineVehicleSQL, null, this);

	}

	/**
	 * 查询指定车辆
	 * 
	 */
	@Override
	public ServiceUnitBean queryVehicleByMacid(String macid) {
		Object[] objs = new Object[2];
		objs[0] = macid.split("_")[1];
		objs[1] = macid.split("_")[0];
		ServiceUnitBean su = (ServiceUnitBean) queryObject(queryVehicleByMacidSQL, objs, this);
		logger.debug("查询指定车辆[" + macid + "]");
		return su;

	}

	/**
	 * 查询结果集封装
	 */
	@Override
	public ServiceUnitBean mapRow(ResultSet rs) throws SQLException {
		ServiceUnitBean su = new ServiceUnitBean();
		String oemcode = rs.getString("oemcode");// 车机类型码
		String t_identifyno = rs.getString("t_identifyno");// 终端标识号
		String macid = oemcode + "_" + t_identifyno;
		su.setSuid(rs.getString("suid"));
		su.setPlatecolorid(rs.getString("plate_color_id"));
		su.setVid(rs.getString("vid"));
		su.setTeminalCode(rs.getString("tmodel_code"));
		su.setTid(rs.getString("tid"));
		su.setCommaddr(t_identifyno);
		su.setOemcode(oemcode);
		su.setMacid(macid);
		su.setAreacode(rs.getString("area_code"));
		su.setVehicleno(rs.getString("vehicle_no"));
		su.setEntid(rs.getString("entid"));

		su.setLon(rs.getLong("lon"));
		su.setLat(rs.getLong("lat"));
		su.setUtc(rs.getLong("utc"));
		su.setDir(rs.getInt("Direction"));

		su.setAlarmcode(rs.getString("alarmcode"));
		su.setIsonline(rs.getInt("isonline"));
		su.setAlarmutc(rs.getLong("alarmutc"));
		su.setSpeed(rs.getInt("speed"));
		if (flag == 0) {
			DataPool.setVehicleMapValue(macid, su);
		} else {
			DataPool.setTempOfflineVehicleMap(macid, su);
		}
		return su;
	}

	public static void main(String[] args) {

	}
}
