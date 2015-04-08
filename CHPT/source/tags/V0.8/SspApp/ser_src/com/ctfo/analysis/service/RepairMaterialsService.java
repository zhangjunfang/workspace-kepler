package com.ctfo.analysis.service;

import com.ctfo.analysis.beans.RepairMaterials;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface RepairMaterialsService {
	
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<RepairMaterials> selectPagination(DynamicSqlParameter param);
}
