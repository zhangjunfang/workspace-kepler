/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.dao;

import java.util.List;

import com.ctfo.informationser.local.dao.GenericIbatisDao;
import com.ctfo.informationser.monitoring.beans.VehicleInfo;
import com.ctfo.local.obj.DynamicSqlParameter;

/**
 * ar
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
 * <td>zz</td>
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
public interface VehicleInfoDao extends GenericIbatisDao<VehicleInfo, Long> {
	/**
	 * 根据车牌号车牌颜色查询车辆信息
	 * 
	 * @param param
	 *            动态参数
	 * @return 车辆信息
	 */
	public VehicleInfo getRegVehicleInfo(DynamicSqlParameter param);

	/**
	 * 根据SIM卡和终端型号查询车辆信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public VehicleInfo getBaseVehicleInfo(DynamicSqlParameter param);

	/**
	 * 根据车牌号，车牌颜色查询车辆及驾驶员信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public VehicleInfo getDriverOfVehicleByType(DynamicSqlParameter param);

	/**
	 * 根据车牌号，车牌颜色查询车辆及电子运单
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public VehicleInfo getEticketByVehicle(DynamicSqlParameter param);

	/**
	 * 根据车牌号，车牌颜色查询车辆终端信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public VehicleInfo getTernimalByVehicleByType(DynamicSqlParameter param);

	/**
	 * 根据电话号码，终端型号，驾驶员从业资格证，驾驶员身份证验证驾驶员信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public Long isDriverOfVehicle(DynamicSqlParameter param);

	/**
	 * 获取所有的车，卡，终端的基本信息
	 * 
	 * @return
	 */
	public VehicleInfo getAllBaseInfo(DynamicSqlParameter param);

	/**
	 * 获取所有的车，卡，终端的基本信息,通过VIN号
	 * 
	 * @return
	 */
	public VehicleInfo getAllBaseInfoByVIN(DynamicSqlParameter param);

	/**
	 * 根据车，卡，终端的id查询绑定
	 * 
	 * @param param
	 *            动态参数
	 * @return
	 */
	public Long getCountForServiceunit(DynamicSqlParameter param);

	/**
	 * 更新车辆表及终端表的注册状态
	 * 
	 * @param param
	 */
	public void modifyByRegStatus(DynamicSqlParameter param);

	/**
	 * 获取车辆静态信息
	 * 
	 * @param param
	 */
	public VehicleInfo getDetailOfVehicleInfo(DynamicSqlParameter param);

	/**
	 * 获取车辆AKEY
	 * 
	 * @param param
	 * @return
	 */
	public VehicleInfo getAkeyVehicleInfo(DynamicSqlParameter param);

	/**
	 * 获取车辆VID
	 * 
	 * @param param
	 * @return
	 */
	public VehicleInfo getVidVehicleInfoMap(DynamicSqlParameter param);

	/**
	 * 根据SIM卡获取车辆和终端ID
	 * 
	 * @param param
	 *            参数
	 * @return 信息
	 */
	public VehicleInfo getAllIdForLogoff(DynamicSqlParameter param);

	/**
	 * 根据3G卡号获取2G卡号
	 * 
	 * @param param
	 * @return
	 */
	public VehicleInfo get2gBy3g(DynamicSqlParameter param);

	/**
	 * 根据手机号获取车辆注册信息
	 * 
	 * @param parameter
	 * @return
	 */
	public VehicleInfo getAllBaseInfoByPhoneNumber(DynamicSqlParameter parameter);

	/**
	 * 根据2G卡号获取3G卡号
	 * 
	 * @param param
	 * @return
	 */
	public VehicleInfo get3gBy2g(DynamicSqlParameter param);

	/**
	 * 获得2g3gSim卡号映射表
	 * 
	 * @param parameter
	 * @return
	 */
	public List<VehicleInfo> get2g3gSimMapping();

	/**
	 * 根据终端id获取车辆注册信息
	 * 
	 * @param parameter
	 * @return
	 */
	public VehicleInfo getAllBaseInfoByTmac(DynamicSqlParameter parameter);
}
