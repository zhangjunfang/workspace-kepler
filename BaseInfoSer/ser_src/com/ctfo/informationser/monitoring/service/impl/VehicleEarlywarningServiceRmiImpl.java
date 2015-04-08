/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.basic.service.RemoteJavaServiceRmiAbstract;
import com.ctfo.informationser.monitoring.beans.ThVehicleEarlywarning;
import com.ctfo.informationser.monitoring.beans.VehicleInfo;
import com.ctfo.informationser.monitoring.dao.ThVehicleEarlywarningDao;
import com.ctfo.informationser.monitoring.dao.VehicleInfoDao;
import com.ctfo.informationser.monitoring.service.VehicleEarlywarningServiceRmi;
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
@AnnotationName(name = "预警管理")
public class VehicleEarlywarningServiceRmiImpl extends RemoteJavaServiceRmiAbstract implements VehicleEarlywarningServiceRmi {

	private ThVehicleEarlywarningDao vehicleEarlywarningDao;

	private VehicleInfoDao vehicleBaseInfoDao;

	public VehicleInfoDao getVehicleBaseInfoDao() {
		return vehicleBaseInfoDao;
	}

	public void setVehicleBaseInfoDao(VehicleInfoDao vehicleBaseInfoDao) {
		this.vehicleBaseInfoDao = vehicleBaseInfoDao;
	}

	public ThVehicleEarlywarningDao getVehicleEarlywarningDao() {
		return vehicleEarlywarningDao;
	}

	public void setVehicleEarlywarningDao(ThVehicleEarlywarningDao vehicleEarlywarningDao) {
		this.vehicleEarlywarningDao = vehicleEarlywarningDao;
	}

	/**
	 * 查询预警
	 * 
	 * @param param
	 * @return
	 */
	@Override
	public List<ThVehicleEarlywarning> findVehicleEarlywarningByparam(DynamicSqlParameter param) {
		return vehicleEarlywarningDao.select(param);
	}

	/**
	 * 添加预警
	 * 
	 * @param entity
	 */
	@Override
	public String add(DynamicSqlParameter param, String id) throws Exception {
		Map<String, String> map = new HashMap<String, String>();
		try {
			// 通过车牌号以及车牌颜色获取车辆VID
			VehicleInfo vehicleInfo = vehicleBaseInfoDao.getVidVehicleInfoMap(param);
			map = param.getEqual();
			ThVehicleEarlywarning entity = new ThVehicleEarlywarning();
			entity.setAlarmType(map.get("alarmType"));
			entity.setAlarmTime(Long.parseLong(map.get("alarmTime")) * 1000);
			entity.setAlarmFrom(Short.parseShort(map.get("alarmFrom")));
			entity.setAlarmDescr(map.get("alarmDescr"));
			entity.setPid(PID.getUUID());
			if (vehicleInfo != null)
				entity.setVid(vehicleInfo.getVid() == null ? "" : vehicleInfo.getVid());
			else
				entity.setVid("00000");
			vehicleEarlywarningDao.insert(entity);
		} catch (CtfoAppException e) {
			throw e;
			// return XMLParse.getResponse(param.getEqual(), -1, id).asXML();
		}
		return XMLParse.getResponse(param.getEqual(), 0, id).asXML();
	}

	/**
	 * 修改预警
	 * 
	 * @param param
	 */
	@Override
	public void update(DynamicSqlParameter param) {
		vehicleEarlywarningDao.update(param);
	}
}
