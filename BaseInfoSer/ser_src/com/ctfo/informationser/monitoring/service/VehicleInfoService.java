/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service;

import com.ctfo.informationser.basic.service.RemoteJavaServiceRmi;
import com.ctfo.local.exception.CtfoAppException;
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
public interface VehicleInfoService extends RemoteJavaServiceRmi {
	/**
	 * 根据参数查询车辆信息
	 * 
	 * @return XML
	 */
	public String getRegVehicleInfo(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 根据SIM卡和终端型号查询车辆信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public String getBaseVehicleInfo(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 根据车牌号，车牌颜色查询车辆及驾驶员信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public String getDriverOfVehicleByType(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 根据车牌号，车牌颜色查询车辆及电子运单
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public String getEticketByVehicle(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 根据车牌号，车牌颜色查询车辆终端信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public String getTernimalByVehicleByType(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 根据电话号码，终端型号，驾驶员从业资格证，驾驶员身份证验证驾驶员信息
	 * 
	 * @param param
	 *            动态参数
	 * @param bKey
	 *            业务序列号
	 * @return XML
	 */
	public String isDriverOfVehicle(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 获取车辆注册结果
	 * 
	 * @param parameter
	 *            动态参数对象
	 * @return
	 */
	public String isRegVehicle(DynamicSqlParameter parameter, String bKey) throws Exception;

	/**
	 * 获取车辆注册结果
	 * 
	 * @param parameter
	 *            动态参数对象
	 * @return
	 */
	public String isRegVehicleNOGB(DynamicSqlParameter parameter, String bKey) throws Exception;

	/**
	 * 车辆鉴权
	 * 
	 * @param parameter
	 *            动态参数对象
	 * @return
	 */
	public String isCheckVehicle(DynamicSqlParameter parameter, String bKey) throws CtfoAppException;

	/**
	 * 车辆注销
	 * 
	 * @param parameter
	 * @return
	 */
	public String isLogoffVehicle(DynamicSqlParameter parameter, String bKey) throws CtfoAppException;

	/**
	 * 获取车辆静态信息
	 * 
	 * @param param
	 */
	public String getDetailOfVehicleInfo(DynamicSqlParameter param, String bKey) throws CtfoAppException;

	/**
	 * 根据2G卡号获取3G卡号
	 * 
	 * @param parameter
	 * @param vehicleInfo
	 * @param bKey
	 * @return
	 */
	public String get2gBy3g(DynamicSqlParameter parameter, String key);

	/**
	 * 根据3G卡号获取2G卡号
	 * 
	 * @param parameter
	 * @param vehicleInfo
	 * @param bKey
	 * @return
	 */
	public String get3gBy2g(DynamicSqlParameter parameter, String key);

	/**
	 * 获得2g3gSim卡号映射
	 * 
	 * @param parameter
	 * @param vehicleInfo
	 * @param bKey
	 * @return
	 */
	public String get2g3gSimMapping(DynamicSqlParameter parameter, String key);

	/**
	 * 根据终端ID获取车辆注册结果
	 * 
	 * @param parameter
	 *            动态参数对象
	 * @return
	 */
	public String isRegVehicleNOGBNew(DynamicSqlParameter parameter, String bKey) throws Exception;
}
