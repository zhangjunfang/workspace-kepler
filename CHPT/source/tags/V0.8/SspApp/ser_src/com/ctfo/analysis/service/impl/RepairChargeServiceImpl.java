package com.ctfo.analysis.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.analysis.beans.RepairCharge;
import com.ctfo.analysis.dao.RepairChargeDao;
import com.ctfo.analysis.service.RepairChargeService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Controller
public class RepairChargeServiceImpl implements RepairChargeService{
	@Autowired
	RepairChargeDao repairChargeDao;
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairChargeDao.count(param);
	}

	@Override
	public PaginationResult<RepairCharge> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairChargeDao.selectPagination(param);
	}

}
