package com.ctfo.monitor.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.beans.UserBehavior;
import com.ctfo.monitor.dao.UserBehaviorDao;
import com.ctfo.monitor.service.UserBehaviorService;

@Controller
public class UserBehaviorServiceImpl extends RemoteJavaServiceAbstract implements UserBehaviorService{

	@Autowired
	UserBehaviorDao userBehaviorDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return userBehaviorDao.count(param);
	}

	@Override
	public PaginationResult<UserBehavior> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return userBehaviorDao.selectPagination(param);
	}

}
