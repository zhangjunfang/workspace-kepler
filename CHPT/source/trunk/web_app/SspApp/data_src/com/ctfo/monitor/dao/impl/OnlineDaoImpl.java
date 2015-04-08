package com.ctfo.monitor.dao.impl;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.monitor.dao.OnlineDao;
import com.ctfo.operation.beans.CompanyInfo;

public class OnlineDaoImpl extends GenericIbatisAbstract<CompanyInfo, String> implements OnlineDao{

	@Override
	public PaginationResult<CompanyInfo> selectPageForOnline(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return super.selectPaginationDynamic(param, "selectPageForOnline", "countParam");
	}
}