package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.dao.SysFunctionDAO;

public class SysFunctionDAOImpl extends GenericIbatisAbstract<SysFunction, String> implements SysFunctionDAO {

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public List<SysFunction> selectByRoleId(Map map) {
		return this.getSqlMapClientTemplate().queryForList("SysFunction.selectParam", map);
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public List<SysFunction> selectFunTreeRoleEdit(Map map) {
		return this.getSqlMapClientTemplate().queryForList("SysFunction.selectFunTreeRoleEdit", map);
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public List<String> selectFunListByOpId(Map map){
		return this.getSqlMapClientTemplate().queryForList("SysFunction.selectFunListByOpId", map);
	}
}