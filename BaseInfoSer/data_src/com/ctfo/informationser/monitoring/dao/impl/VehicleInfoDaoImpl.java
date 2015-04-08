/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.dao.impl;

import java.util.List;

import com.ctfo.informationser.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.informationser.monitoring.beans.VehicleInfo;
import com.ctfo.informationser.monitoring.dao.VehicleInfoDao;
import com.ctfo.local.obj.DynamicSqlParameter;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>Dec 22, 2011</td>
 * <td>DEVELOPER</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOPER
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class VehicleInfoDaoImpl extends GenericIbatisAbstract<VehicleInfo, Long> implements VehicleInfoDao {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getRegVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getRegVehicleInfo(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getRegVehicleInfo", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getBaseVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public VehicleInfo getBaseVehicleInfo(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getBaseVehicleInfo", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getDriverOfVehicleByType(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getDriverOfVehicleByType(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getDriverOfVehicleByType", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getEticketByVehicle(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getEticketByVehicle(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getEticketByVehicle", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getTernimalByVehicleByType(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getTernimalByVehicleByType(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getTernimalByVehicleByType", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#isDriverOfVehicle(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public Long isDriverOfVehicle(DynamicSqlParameter param) {
		return (Long) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".isDriverOfVehicle", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getAllBaseInfo(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getAllBaseInfo(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getAllBaseInfo", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getAllBaseInfo(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getAllBaseInfoByVIN(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getAllBaseInfoByVin", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getCountForServiceunit(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public Long getCountForServiceunit(DynamicSqlParameter param) {
		return (Long) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getCountForServiceunit", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#updateByRegStatus(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public void modifyByRegStatus(DynamicSqlParameter param) {
		getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".modifyByRegStatus", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getDetailOfVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getDetailOfVehicleInfo(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getDetailOfVehicleInfo", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getAkeyVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getAkeyVehicleInfo(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getAkeyVehicleInfo", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getVidVehicleInfoMap(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getVidVehicleInfoMap(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getVidVehicleInfoMap", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getAllIdForLogoff(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getAllIdForLogoff(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getAllIdForLogoff", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#get2gBy3g(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo get2gBy3g(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".get2gBy3g", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getAllBaseInfoByPhoneNumber(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getAllBaseInfoByPhoneNumber(DynamicSqlParameter parameter) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getAllBaseInfoByPhoneNumber", parameter);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#get3gBy2g(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo get3gBy2g(DynamicSqlParameter param) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".get3gBy2g", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#get2g3gSimMapping()
	 */
	@Override
	public List<VehicleInfo> get2g3gSimMapping() {
		return getSqlMapClientTemplate().queryForList(sqlmapNamespace + ".get2g3gSimMapping");
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.dao.VehicleInfoDao#getAllBaseInfoByTid(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public VehicleInfo getAllBaseInfoByTmac(DynamicSqlParameter parameter) {
		return (VehicleInfo) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + ".getAllBaseInfoByTmac", parameter);
	}
}
