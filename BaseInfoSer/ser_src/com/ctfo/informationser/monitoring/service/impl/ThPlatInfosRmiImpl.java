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
import com.ctfo.informationser.monitoring.beans.ThPlatInfos;
import com.ctfo.informationser.monitoring.dao.ThPlatInfosDao;
import com.ctfo.informationser.monitoring.service.ThPlatInfosRmi;
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
@AnnotationName(name = "平台信息交互")
public class ThPlatInfosRmiImpl extends RemoteJavaServiceRmiAbstract implements ThPlatInfosRmi {

	private ThPlatInfosDao thPlatInfosDao;

	public ThPlatInfosDao getThPlatInfosDao() {
		return thPlatInfosDao;
	}

	public void setThPlatInfosDao(ThPlatInfosDao thPlatInfosDao) {
		this.thPlatInfosDao = thPlatInfosDao;
	}

	/**
	 * 添加报文
	 * 
	 * @param entity
	 */
	@Override
	public String addForMsgInfo(DynamicSqlParameter param, String id)  throws Exception{
		Date d = new Date();
		long longtime = d.getTime();
		Map<String, String> map = new HashMap<String, String>();
		try {
			map = param.getEqual();
			ThPlatInfos entity = new ThPlatInfos();
			entity.setMessageContent(map.get("messageContent"));
			entity.setMessageId(map.get("messageId"));
			entity.setObjectId(map.get("objectId") == null ? "" : map.get("objectId"));
			entity.setObjectType(Short.parseShort(map.get("objectType")));
			entity.setOpType(Short.parseShort("1"));
			entity.setUtc(longtime);
			entity.setPid(PID.getUUID());
			entity.setAreaId(map.get("areaId"));
			entity.setSeq(id);
			thPlatInfosDao.insert(entity);
		} catch (CtfoAppException e) {
			throw e;
//			return XMLParse.getResponse(param.getEqual(), -1, id).asXML();
		}
		return XMLParse.getResponse(param.getEqual(), 0, id).asXML();
	}

	/**
	 * 查询报文
	 * 
	 * @param param
	 * @return
	 */
	@Override
	public List<ThPlatInfos> findThPlatInfosByparam(DynamicSqlParameter param) {
		return thPlatInfosDao.select(param);
	}

	/**
	 * 修改报文
	 * 
	 * @param param
	 * @return
	 */
	@Override
	public void update(DynamicSqlParameter param) {
		// @TODO 查岗回复下发到PCC
		thPlatInfosDao.update(param);
	}

	/**
	 * 添加查岗
	 * 
	 * @param param
	 * @return
	 */
	@Override
	public String addForMsgPost(DynamicSqlParameter param, String id) throws Exception {
		Date d = new Date();
		long longtime = d.getTime();
		Map<String, String> map = new HashMap<String, String>();
		try {
			map = param.getEqual();
			ThPlatInfos entity = new ThPlatInfos();
			entity.setMessageContent(map.get("messageContent") == null ? "" : map.get("messageContent"));
			entity.setMessageId(map.get("messageId"));
			entity.setObjectId(map.get("objectId") == null ? "" : map.get("objectId"));
			entity.setObjectType(Short.parseShort(map.get("objectType")));
			entity.setOpType(Short.parseShort("0"));
			entity.setUtc(longtime);
			entity.setPid(PID.getUUID());
			entity.setAreaId(map.get("areaId"));
			entity.setSeq(id);
			thPlatInfosDao.insert(entity);
		} catch (CtfoAppException e) {
			throw e;
//			return XMLParse.getResponse(param.getEqual(), -1, id).asXML();
		}
		return XMLParse.getResponse(param.getOutputValue(), 0, id).asXML();
	}

	public static void main(String[] args) {
		
		
		Date d = new Date();
		System.out.println(d.getTime());
		
		
		/*MonitorManagerBean bean = new MonitorManagerBean();
		bean.setAreaID("6401");
		bean.setConnType(MonitorStaticData.CONN_TYPE_BY_WEBSERVICE);
		bean.setMacId("6401_0");
		bean.setSeq("USERID_UTC_10");
		bean.setType(MonitorStaticData.TYPE_PLAT_BUSINESS);
		Map<String, String> map = new HashMap<String, String>();
		String value = "测试";
		map.put("PLATQUERY", "1|1|1|" + Base64_URl.base64Encode(value));
		bean.setTypeValue(map);
		MonitorManagerCommand.sendCommand(bean);*/
	}
}
