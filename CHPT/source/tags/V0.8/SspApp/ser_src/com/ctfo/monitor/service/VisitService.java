package com.ctfo.monitor.service;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.beans.VisitStat;

public interface VisitService extends RemoteJavaService{
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<VisitStat> selectPagination(DynamicSqlParameter param);
}
