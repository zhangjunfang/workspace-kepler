package com.ctfo.dao.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.beans.Services;
import com.ctfo.dao.ServiceManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.service.DynamicSqlParameter;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
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
 * <td>2012-12-12</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class ServiceManageDAOImpl extends GenericIbatisAbstract<Services, Long> implements ServiceManageDAO {

	/***
	 * 根据条件查询登陆信息
	 */
	public static final String GET_SERVICE_LIST = "getServiceList";
	public static final String GET_SERVICE_LIST_COUNT = "getServiceListCount";
	private static final String GET_EDIT_SERVICE = "getEditService";
	private static final String ADD_SERVICE = "addService";
	private static final String DEL_SERVICE = "delService";
	private static final String EDIT_SERVICE = "editService";

	//@Override
	public Map<String,Object> getServiceList(DynamicSqlParameter param) throws Exception {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<Services> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_SERVICE_LIST, param);
			int total = (Integer) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + "." + GET_SERVICE_LIST_COUNT, param);
			map.put("data", data);
			map.put("total", total);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditService(DynamicSqlParameter requestParam) {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<Services> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_EDIT_SERVICE, requestParam);
			map.put("data", data);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public int addService(DynamicSqlParameter requestParam) {
		int isertServiceId = 0;
		try {
			isertServiceId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + ADD_SERVICE, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return isertServiceId;
	}

	//@Override
	public int delService(DynamicSqlParameter requestParam) {
		int delServiceId = 0;
		try {
			delServiceId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + DEL_SERVICE, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return delServiceId;
	}

	//@Override
	public int editService(DynamicSqlParameter requestParam) {
		int editServiceId = 0;
		try {
			editServiceId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + EDIT_SERVICE, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return editServiceId;
	}

}
