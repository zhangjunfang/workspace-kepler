package com.ctfo.analysis.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.analysis.beans.RepairSingle;
import com.ctfo.analysis.dao.RepairSingleDao;
import com.ctfo.analysis.service.RepairSingleService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Controller
public class RepairSingleServiceImpl implements RepairSingleService{
	@Autowired
	RepairSingleDao repairSingleDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairSingleDao.count(param);
	}

	@Override
	public PaginationResult<RepairSingle> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairSingleDao.selectPagination(param);
	}
	@Override
	public RepairSingle selectPK(String comId) {
		return repairSingleDao.selectPK(comId);
	}
}
