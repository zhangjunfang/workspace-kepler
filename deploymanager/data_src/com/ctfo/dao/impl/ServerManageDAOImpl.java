package com.ctfo.dao.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.beans.Server;
import com.ctfo.dao.ServerManageDAO;
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
public class ServerManageDAOImpl extends GenericIbatisAbstract<Server, Long> implements ServerManageDAO {

	/***
	 * 根据条件查询登陆信息
	 */
	public static final String GET_SERVER_LIST = "getServerList";
	public static final String GET_SERVER_LIST_COUNT = "getServerListCount";
	private static final String GET_EDIT_SERVER = "getEditServer";
	private static final String ADD_SERVER = "addServer";
	private static final String DEL_SERVER = "delServer";
	private static final String EDIT_SERVER = "editServer";

	//@Override
	public Map<String,Object> getServerList(DynamicSqlParameter param) throws Exception {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<Server> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_SERVER_LIST, param);
			int total = (Integer) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + "." + GET_SERVER_LIST_COUNT, param);
			map.put("data", data);
			map.put("total", total);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditServer(DynamicSqlParameter requestParam) {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<Server> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_EDIT_SERVER, requestParam);
			map.put("data", data);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public int addServer(DynamicSqlParameter requestParam) {
		int isertServerId = 0;
		try {
			isertServerId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + ADD_SERVER, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return isertServerId;
	}

	//@Override
	public int delServer(DynamicSqlParameter requestParam) {
		int delServerId = 0;
		try {
			delServerId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + DEL_SERVER, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return delServerId;
	}

	//@Override
	public int editServer(DynamicSqlParameter requestParam) {
		int editServerId = 0;
		try {
			editServerId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + EDIT_SERVER, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return editServerId;
	}

}
