package com.ctfo.analysis.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.analysis.beans.RepairAnnex;
import com.ctfo.analysis.dao.RepairAnnexDao;
import com.ctfo.analysis.service.RepairAnnexService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
@Controller
public class RepairAnnexServiceImpl implements RepairAnnexService {
	@Autowired
	RepairAnnexDao repairAnnexDao;
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairAnnexDao.count(param);
	}

	@Override
	public PaginationResult<RepairAnnex> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairAnnexDao.selectPagination(param);
	}

}
