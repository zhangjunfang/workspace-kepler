package com.ctfo.monitor.service;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.CompanyInfo;

public interface OnlineService extends RemoteJavaService{
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<CompanyInfo> selectPagination(DynamicSqlParameter param);
	public PaginationResult<CompanyInfo> selectPageForOnline(DynamicSqlParameter param);
}
