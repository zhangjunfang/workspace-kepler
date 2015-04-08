package com.ctfo.datatransferserver.dao;

import com.ctfo.datatransferserver.beans.ServiceUnitBean;
import com.lingtu.xmlconf.XmlConf;

/**
 * 查询车辆信息接口
 * 
 * @author yangyi
 * 
 */
public interface ServiceUnitDao {

	/**
	 * 初始化SQL
	 * 
	 * @param config
	 */
	public void initDBAdapter(XmlConf config);

	/**
	 * 查询所有车辆
	 * 
	 */
	public void queryAllVehicle();
	/**
	 * 查询下线车辆
	 * 
	 */
	public void queryOfflineVehicle();
	
	/**
	 * 查询指定车辆
	 * 
	 */
	public ServiceUnitBean queryVehicleByMacid(String macid);

}
