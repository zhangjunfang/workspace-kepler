package com.ctfo.analysis.service;

import com.ctfo.analysis.beans.RepairProject;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface RepairProjectService {
	
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<RepairProject> selectPagination(DynamicSqlParameter param);
}
