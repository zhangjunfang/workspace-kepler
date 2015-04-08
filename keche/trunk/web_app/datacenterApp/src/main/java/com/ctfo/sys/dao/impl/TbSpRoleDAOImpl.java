package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.sys.beans.TbSpRole;
import com.ctfo.sys.dao.TbSpRoleDAO;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 角色管理<br>
 * 描述： 角色管理<br>
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
 * <td>2014-5-8</td>
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
public class TbSpRoleDAOImpl extends GenericIbatisAbstract<TbSpRole, String> implements TbSpRoleDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpRoleDAO#deleteRole(com.ctfo.sys.beans.TbSpRole)
	 */
	@Override
	public int deleteRole(TbSpRole tbSpRole) {
		return this.getSqlMapClientTemplate().update("TbSpRole.updateDelete", tbSpRole);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpRoleDAO#findRoleDetail(java.util.Map)
	 */
	@Override
	public TbSpRole findRoleDetail(Map<String, String> map) {
		return (TbSpRole) this.getSqlMapClientTemplate().queryForObject("TbSpRole.selectRoleDetail", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpRoleDAO#findRoleList(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<TbSpRole> findRoleList(DynamicSqlParameter param) {
		return (List<TbSpRole>) this.getSqlMapClientTemplate().queryForList("TbSpRole.selectRoleList", param);
	}

}
