package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.dao.SysFunctionDAO;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 权限<br>
 * 描述： 权限<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
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
 * <td>2014-5-21</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class SysFunctionDAOImpl extends GenericIbatisAbstract<SysFunction, String> implements SysFunctionDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.SysFunctionDAO#findByRoleId(java.util.Map)
	 */
	@Override
	public List<SysFunction> findByRoleId(Map<String, String> map) {
		return this.getSqlMapClientTemplate().queryForList("SysFunction.selectByRoleId", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.SysFunctionDAO#findFunListByOpId(java.util.Map)
	 */
	@Override
	public List<String> findFunListByOpId(Map<String, String> map) {
		return this.getSqlMapClientTemplate().queryForList("SysFunction.selectFunListByOpId", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.SysFunctionDAO#findFunTreeRoleEdit(java.util.Map)
	 */
	@Override
	public List<SysFunction> findFunTreeRoleEdit(Map<String, String> map) {
		return this.getSqlMapClientTemplate().queryForList("SysFunction.selectFunTreeRoleEdit", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.SysFunctionDAO#initFunTree(java.util.Map)
	 */
	@Override
	public List<SysFunction> initFunTree(Map<String, String> map) {
		return this.getSqlMapClientTemplate().queryForList("SysFunction.initFunTree", map);
	}

}
