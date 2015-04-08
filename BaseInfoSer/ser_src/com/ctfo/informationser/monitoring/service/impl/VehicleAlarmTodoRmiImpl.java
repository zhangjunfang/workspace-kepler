/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.impl;

import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.basic.service.RemoteJavaServiceRmiAbstract;
import com.ctfo.informationser.monitoring.beans.ThVehicleAlarmtodo;
import com.ctfo.informationser.monitoring.dao.ThVehicleAlarmTodoDao;
import com.ctfo.informationser.monitoring.service.VehicleAlarmTodoRmi;
import com.ctfo.informationser.util.PID;
import com.ctfo.informationser.util.XMLParse;
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
@AnnotationName(name = "报警督办")
public class VehicleAlarmTodoRmiImpl extends RemoteJavaServiceRmiAbstract implements VehicleAlarmTodoRmi {

	private ThVehicleAlarmTodoDao vehicleAlarmTodoDao;

	public ThVehicleAlarmTodoDao getVehicleAlarmTodoDao() {
		return vehicleAlarmTodoDao;
	}

	public void setVehicleAlarmTodoDao(ThVehicleAlarmTodoDao vehicleAlarmTodoDao) {
		this.vehicleAlarmTodoDao = vehicleAlarmTodoDao;
	}

	/**
	 * 添加报警督办
	 * 
	 * @param entity
	 */
	@Override
	public String add(DynamicSqlParameter param, String id) throws Exception {
		Date d = new Date();
		long longtime = d.getTime();
		Map<String, String> map = new HashMap<String, String>();
		try {
			map = param.getEqual();
			ThVehicleAlarmtodo entity = new ThVehicleAlarmtodo();
			entity.setSupervisionEndUtc(Long.parseLong(map.get("supervisionEndUtc")));
			entity.setSupervisionId(map.get("supervisionId"));
			entity.setSupervisionLevel(Short.parseShort(map.get("supervisionLevel")));
			entity.setSupervisor(map.get("supervisor"));
			entity.setSupervisorEmail(map.get("supervisorEmail"));
			entity.setSupervisorTel(map.get("supervisorTel"));
			entity.setVehicleColor(Short.parseShort(map.get("vehicleColor")));
			entity.setVehicleno(map.get("vehicleno"));
			entity.setWanSrc(Short.parseShort(map.get("wanSrc")));
			entity.setWanType(Long.parseLong(map.get("wanType")));
			entity.setWarUtc(Long.parseLong(map.get("warUtc")) * 1000);
			entity.setStatus(Short.parseShort("0"));
			entity.setUtc(longtime);
			entity.setPid(PID.getUUID());
			vehicleAlarmTodoDao.insert(entity);
		} catch (CtfoAppException e) {
			throw e;
			// return XMLParse.getResponse(param.getEqual(), -1, id).asXML();
		}
		return XMLParse.getResponse(param.getEqual(), 0, id).asXML();

	}

	/**
	 * 查询报警督办
	 * 
	 * @param param
	 * @return
	 */
	@Override
	public List<ThVehicleAlarmtodo> findVehicleAlarmTodoByparam(DynamicSqlParameter param) {
		return vehicleAlarmTodoDao.select(param);
	}

	/**
	 * 修改报警督办
	 * 
	 * @param param
	 * @return
	 */
	@Override
	public void update(DynamicSqlParameter param) {
		// TODO Auto-generated method stub

	}

}
