package com.ctfo.analysis.service;

import com.ctfo.analysis.beans.RepairCharge;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface RepairChargeService {
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<RepairCharge> selectPagination(DynamicSqlParameter param);
}
