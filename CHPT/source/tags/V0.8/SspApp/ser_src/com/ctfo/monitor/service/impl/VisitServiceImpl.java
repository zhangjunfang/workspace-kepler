package com.ctfo.monitor.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.beans.VisitStat;
import com.ctfo.monitor.dao.VisitDao;
import com.ctfo.monitor.service.VisitService;

@Controller
public class VisitServiceImpl extends RemoteJavaServiceAbstract implements VisitService{
	@Autowired
	VisitDao visitDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return visitDao.count(param);
	}

	@Override
	public PaginationResult<VisitStat> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return visitDao.selectPagination(param);
	}

}
