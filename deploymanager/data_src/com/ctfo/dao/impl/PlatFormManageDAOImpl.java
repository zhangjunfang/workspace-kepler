package com.ctfo.dao.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.beans.PlatForm;
import com.ctfo.dao.PlatFormManageDAO;
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
 * @author gemin
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class PlatFormManageDAOImpl extends GenericIbatisAbstract<PlatForm, Long> implements PlatFormManageDAO {

	/***
	 * 根据条件查询登陆信息
	 */
	public static final String GET_PLATEFORM_LIST = "getPlatFormList";
	public static final String GET_PLATEFORM_LIST_COUNT = "getPlatFormListCount";
	private static final String GET_EDIT_PLATEFORM = "getEditPlatForm";
	private static final String ADD_PLATEFORM = "addPlatForm";
	private static final String DEL_PLATEFORM = "delPlatForm";
	private static final String EDIT_PLATEFORM = "editPlatForm";
	private static final String CHECK_PLAT_EXIST = "checkPlatExist";

	//@Override
	public Map<String,Object> getPlatFormList(DynamicSqlParameter param) throws Exception {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<PlatForm> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_PLATEFORM_LIST, param);
			int total = (Integer) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + "." + GET_PLATEFORM_LIST_COUNT, param);
			map.put("data", data);
			map.put("total", total);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditPlatForm(DynamicSqlParameter requestParam) {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<PlatForm> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_EDIT_PLATEFORM, requestParam);
			map.put("data", data);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public int addPlatForm(DynamicSqlParameter requestParam) {
		int isertPlatId = 0;
		try {
			isertPlatId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + ADD_PLATEFORM, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return isertPlatId;
	}

	//@Override
	public int delPlatForm(DynamicSqlParameter requestParam) {
		int delPlatId = 0;
		try {
			delPlatId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + DEL_PLATEFORM, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return delPlatId;
	}

	//@Override
	public int editPlatForm(DynamicSqlParameter requestParam) {
		int editPlatId = 0;
		try {
			editPlatId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + EDIT_PLATEFORM, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return editPlatId;
	}

	public int checkPlatExist(DynamicSqlParameter requestParam) {
		int platExist = 0;
		try {
			platExist = (Integer) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + "." + CHECK_PLAT_EXIST, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return platExist;
	}

}
