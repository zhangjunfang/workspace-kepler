package com.ctfo.analysis.service;

import com.ctfo.analysis.beans.RepairAnnex;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface RepairAnnexService {
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<RepairAnnex> selectPagination(DynamicSqlParameter param);
}
