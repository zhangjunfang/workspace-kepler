package com.ctfo.analysis.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.analysis.beans.RepairMaterials;
import com.ctfo.analysis.dao.RepairMaterialsDao;
import com.ctfo.analysis.service.RepairMaterialsService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Controller
public class RepairMaterialsServiceImpl implements RepairMaterialsService{
	@Autowired
	RepairMaterialsDao repairMaterialsDao;
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairMaterialsDao.count(param);
	}

	@Override
	public PaginationResult<RepairMaterials> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairMaterialsDao.selectPagination(param);
	}

}
