package com.ctfo.analysis.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.analysis.beans.RepairInfo;
import com.ctfo.analysis.dao.RepairDao;
import com.ctfo.analysis.service.RepairService;
import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Controller
public class RepairServiceImpl extends RemoteJavaServiceAbstract implements RepairService{
	@Autowired
	RepairDao repairDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairDao.count(param);
	}

	@Override
	public PaginationResult<RepairInfo> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return repairDao.selectPagination(param);
	}

}
