package com.ctfo.monitor.dao;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.CompanyInfo;

public interface OnlineDao extends GenericIbatisDao<CompanyInfo, String>{
	public PaginationResult<CompanyInfo> selectPageForOnline(DynamicSqlParameter param);
}
