package com.ctfo.basic.service;

import com.ctfo.basic.beans.TbSim;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;

public interface TbSimService{

	/**
	 * 查询SIM卡分页信息
	 * 
	 * @param param
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSim> findSimByParamPage(DynamicSqlParameter param) throws CtfoAppException;

}
