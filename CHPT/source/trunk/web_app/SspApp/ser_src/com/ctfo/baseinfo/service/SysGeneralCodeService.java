package com.ctfo.baseinfo.service;

import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;

public interface SysGeneralCodeService {
	/**
	 * 根据参数获取通用编码
	 * @param param
	 * @return
	 */
	public String findSysGeneralCodeByCode(DynamicSqlParameter params)throws CtfoAppException;
}
