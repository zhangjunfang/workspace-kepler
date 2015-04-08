package com.ctfo.monitor.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.dao.OnlineDao;
import com.ctfo.monitor.service.OnlineService;
import com.ctfo.operation.beans.CompanyInfo;

@Service
public class OnlineServiceImpl extends RemoteJavaServiceAbstract implements OnlineService{
	@Autowired
	OnlineDao onlineDao;
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return onlineDao.count(param);
	}
	@Override
	public PaginationResult<CompanyInfo> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return onlineDao.selectPagination(param);
	}
	@Override
	public PaginationResult<CompanyInfo> selectPageForOnline(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return onlineDao.selectPageForOnline(param);
	}

}
