package com.ctfo.archives.service;

import com.ctfo.archives.beans.Archives;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface ArchivesService {
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<Archives> selectPagination(DynamicSqlParameter param);
}
