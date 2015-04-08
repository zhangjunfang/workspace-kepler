package com.ctfo.operation.service;

import com.ctfo.archives.beans.SysSetbook;
import com.ctfo.local.obj.DynamicSqlParameter;

public interface TbSetbookService {
	public int count(DynamicSqlParameter param);
	
	public SysSetbook selectPKByCom(String comId);
}
