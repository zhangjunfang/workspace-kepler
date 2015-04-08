package com.ctfo.operation.dao.impl;

import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.AddApp;
import com.ctfo.operation.dao.AddAppDao;

public class AddAppDaoImpl extends GenericIbatisAbstract<AddApp, String> implements AddAppDao{
	
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpenCloud(Map map){
		return this.getSqlMapClientTemplate().update("AddApp.updateRevokeOpenCloud",map);
	}

	@Override
	public int countAddApp(DynamicSqlParameter param) throws CtfoAppException {
		// TODO Auto-generated method stub
		return this.countDynamic(param, "countAddApp");
	}

	@Override
	public PaginationResult<AddApp> selectAddApp(DynamicSqlParameter param) throws CtfoAppException {
		// TODO Auto-generated method stub
		return this.selectPaginationDynamic(param, "selectAddApp", "countAddApp");
	}
	public void insert(AddApp addApp){
		this.getSqlMapClientTemplate().insert("AddApp.insert", addApp);
	}

	@Override
	public int reAuthorizationCloud(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("AddApp.reAuthorizationCloud",map);
	}

	@Override
	public int countAddAppByComId(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return this.countDynamic(param, "countAddAppByComId");
	}
}