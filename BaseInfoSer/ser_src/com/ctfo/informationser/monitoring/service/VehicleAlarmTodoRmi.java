/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service;

import java.util.List;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.basic.service.RemoteJavaServiceRmi;
import com.ctfo.informationser.monitoring.beans.ThVehicleAlarmtodo;
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
public interface VehicleAlarmTodoRmi extends RemoteJavaServiceRmi{
	/**
	 * 查询报警督办
	 * @param param
	 * @return
	 */
	@AnnotationName(name = "查询报警督办")
	public List<ThVehicleAlarmtodo> findVehicleAlarmTodoByparam(DynamicSqlParameter param);
	
	/**
	 * 添加报警督办
	 * @param entity
	 */
	@AnnotationName(name = "添加报警督办")
	public String add(DynamicSqlParameter param,String id)  throws Exception;
	
	/**
	 * 修改报警督办
	 * @param param
	 */
	@AnnotationName(name = "修改报警")
	public void update(DynamicSqlParameter param) throws Exception;
}
