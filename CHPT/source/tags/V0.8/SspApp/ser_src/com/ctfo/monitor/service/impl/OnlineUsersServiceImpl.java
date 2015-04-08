package com.ctfo.monitor.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.beans.OnlineUsers;
import com.ctfo.monitor.dao.OnlineUsersDao;
import com.ctfo.monitor.service.OnlineUsersService;

@Controller
public class OnlineUsersServiceImpl  extends RemoteJavaServiceAbstract implements OnlineUsersService{
	@Autowired
	OnlineUsersDao onlineUsersDao;
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return onlineUsersDao.count(param);
	}

	@Override
	public PaginationResult<OnlineUsers> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return onlineUsersDao.selectPagination(param);
	}
}
