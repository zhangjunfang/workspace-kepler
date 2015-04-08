package com.ctfo.basic.service;

import com.ctfo.basic.beans.TbVehicle;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：车辆<br>
 * 描述：车辆<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月29日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public interface TbVehicleService {

	/**
	 * 根据终端id查询车辆信息
	 * 
	 * @param id
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbVehicle> findVehicleById(String id) throws CtfoAppException;
	
	/**
	 * 查询车辆分页信息
	 * 
	 * @param param
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbVehicle> findVehicleByParamPage(DynamicSqlParameter param) throws CtfoAppException;	

}
