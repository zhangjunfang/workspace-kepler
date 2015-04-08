/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.basic.service.RemoteJavaServiceRmiAbstract;
import com.ctfo.informationser.monitoring.beans.ThLinkStatus;
import com.ctfo.informationser.monitoring.dao.ThLinkStatusDao;
import com.ctfo.informationser.monitoring.service.ThLinkStatusServiceRmi;
import com.ctfo.informationser.util.PID;
import com.ctfo.informationser.util.XMLParse;
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
@AnnotationName(name = "链路通断")
public class ThLinkStatusServiceRmiImpl extends RemoteJavaServiceRmiAbstract implements ThLinkStatusServiceRmi {

	private ThLinkStatusDao thLinkStatusDao;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.ThLinkStatusServiceRmi#findThPlatInfosByparam(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<ThLinkStatus> findThPlatInfosByparam(DynamicSqlParameter param) {
		return thLinkStatusDao.select(param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.ThLinkStatusServiceRmi#add(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String add(DynamicSqlParameter param, String id) throws Exception{
		try {
			DynamicSqlParameter paramForDb = new DynamicSqlParameter();
			if (param != null) {
				Map<String, String> map = param.getEqual();
				if (map != null) {
					Map<String, String> equal = new HashMap<String, String>();
					String type = map.get("status");
					if ("0".equals(type)) {// 链路连接
						ThLinkStatus thLinkStatus = new ThLinkStatus();
						thLinkStatus.setAreaId(map.get("areaId"));
						thLinkStatus.setConnectUtc(map.get("utc") == null ? System.currentTimeMillis() : Long.parseLong(map.get("utc"))*1000);
						if (map.get("linkType") == null) {
							return XMLParse.getResponse(param.getEqual(), 0, id).asXML();
						} else {
							thLinkStatus.setLinkType(Short.parseShort(map.get("linkType")));
						}
						thLinkStatus.setPid(PID.getUUID());
						thLinkStatus.setUtc(System.currentTimeMillis());
						thLinkStatusDao.insert(thLinkStatus);
					} else {
						equal.put("areaId", map.get("areaId"));
						if (map.get("linkType") == null) {
							return XMLParse.getResponse(param.getEqual(), -1, id).asXML();
						} else {
							equal.put("linkType", map.get("linkType"));
						}
						Map<String, Object> updateValue = new HashMap<String, Object>();
						updateValue.put("disconnectUtc", map.get("utc") == null ? System.currentTimeMillis() : Long.parseLong(map.get("utc"))*1000);
						paramForDb.setEqual(equal);
						paramForDb.setUpdateValue(updateValue);
						thLinkStatusDao.update(paramForDb);
					}
				} else {
					return XMLParse.getResponse(param.getEqual(), -1, id).asXML();
				}
			}
			return XMLParse.getResponse(param.getEqual(), 0, id).asXML();
		} catch (Exception e) {
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.ThLinkStatusServiceRmi#update(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public String update(DynamicSqlParameter param, String id) {
		return null;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.ThLinkStatusServiceRmi#findThLinkStatusByUTC(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<ThLinkStatus> findThLinkStatusByUTC(DynamicSqlParameter param) {
		return thLinkStatusDao.findThLinkStatusByUTC(param);
	}

	/**
	 * @return the thLinkStatusDao
	 */
	public ThLinkStatusDao getThLinkStatusDao() {
		return thLinkStatusDao;
	}

	/**
	 * @param thLinkStatusDao
	 *            the thLinkStatusDao to set
	 */
	public void setThLinkStatusDao(ThLinkStatusDao thLinkStatusDao) {
		this.thLinkStatusDao = thLinkStatusDao;
	}

}
