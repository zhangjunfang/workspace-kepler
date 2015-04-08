package com.ctfo.dao.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.beans.Operator;
import com.ctfo.dao.OperatorManageDAO;
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
 * <td>gem</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author gem
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class OperatorManageDAOImpl extends GenericIbatisAbstract<Operator, Long> implements OperatorManageDAO {

	/***
	 * 添加用户
	 */
	public static final String ADD_OPERATOR = "addOperator";
	public static final String GET_OPERATOR_LIST = "getUserList";
	public static final String GET_OPERATOR_LIST_COUNT = "getUserListCount";
	public static final String GET_EDIT_USER = "getEditUser";
	public static final String EDIT_USER = "editUser";
	private static final String DEL_USER = "delUser";
	private static final String CHECK_USER_EXIST = "checkUserExist";

	//@Override
	public int addOperator(DynamicSqlParameter param) throws Exception {
		int isertUserId = 0;
		try {
			isertUserId = getSqlMapClientTemplate().update(sqlmapNamespace + "." + ADD_OPERATOR, param);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return isertUserId;
	}

	//@Override
	public Map<String,Object> getUserList(DynamicSqlParameter param) {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<Operator> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_OPERATOR_LIST, param);
			int total = (Integer) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + "." + GET_OPERATOR_LIST_COUNT, param);
			map.put("data", data);
			map.put("total", total);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditUser(DynamicSqlParameter requestParam) {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			List<Operator> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_EDIT_USER, requestParam);
			map.put("data", data);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}

	//@Override
	public int editUser(DynamicSqlParameter requestParam) {
		int ifUpdate = 0;
		try {
			ifUpdate= getSqlMapClientTemplate().update(sqlmapNamespace + "." + EDIT_USER, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return ifUpdate;
	}

	//@Override
	public int delUser(DynamicSqlParameter requestParam) {
		int delUserId = 0;
		try {
			delUserId= getSqlMapClientTemplate().update(sqlmapNamespace + "." + DEL_USER, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return delUserId;
	}

	public int checkUserExist(DynamicSqlParameter requestParam) {
		int userExist = 0;
		try {
			userExist= (Integer) getSqlMapClientTemplate().queryForObject(sqlmapNamespace + "." + CHECK_USER_EXIST, requestParam);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return userExist;
	}

}
