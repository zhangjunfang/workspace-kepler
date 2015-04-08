package com.ctfo.operation.service.impl;

import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.AddApp;
import com.ctfo.operation.dao.AddAppDao;
import com.ctfo.operation.service.AddAppService;

@Controller
public class AddAppServiceImpl implements AddAppService{
	@Autowired
	AddAppDao addAppDao;
	
	@Override
	public AddApp selectPK(String comId) {
		return addAppDao.selectPK(comId);
	}

	@Override
	public int count(DynamicSqlParameter param) {
		return addAppDao.count(param);
	}

	@Override
	public PaginationResult<AddApp> selectPagination(DynamicSqlParameter param) {
		return addAppDao.selectPagination(param);
	}

	@Override
	public int updateRevokeOpenCloud(Map map) {
		// TODO Auto-generated method stub
		return addAppDao.updateRevokeOpenCloud(map);
	}

	@Override
	public int countAddApp(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return addAppDao.countAddApp(param);
	}

	@Override
	public PaginationResult<AddApp> selectAddApp(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return addAppDao.selectAddApp(param);
	}

	@Override
	public void insert(AddApp addApp) {
		// TODO Auto-generated method stub
		addAppDao.insert(addApp);
	}

	@Override
	public int reAuthorizationCloud(Map map) {
		// TODO Auto-generated method stub
		return addAppDao.reAuthorizationCloud(map);
	}

	@Override
	public int countAddAppByComId(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return addAppDao.countAddAppByComId(param);
	}
}
