package com.ctfo.sys.dao.impl;

import java.util.Map;

import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.TrRoleFunction;
import com.ctfo.sys.dao.TrRoleFunctionDAO;

public class TrRoleFunctionDAOImpl extends GenericIbatisAbstract<TrRoleFunction, String> implements TrRoleFunctionDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TrRoleFunctionDAO#deleteTrRoleFun(java.util.Map)
	 */
	@Override
	public int deleteTrRoleFun(Map<String, String> map) {
		return this.getSqlMapClientTemplate().delete("TrRoleFunction.deleteTrRoleFun", map);
	}

}
