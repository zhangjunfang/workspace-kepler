package com.ctfo.analysis.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.analysis.beans.RepairProject;
import com.ctfo.analysis.dao.RepairProjectDao;
import com.ctfo.analysis.service.RepairProjectService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Service
public class RepairProjectImpl implements RepairProjectService{
	@Autowired
	RepairProjectDao repairProjectDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairProjectDao.count(param);
	}

	@Override
	public PaginationResult<RepairProject> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairProjectDao.selectPagination(param);
	}

}
