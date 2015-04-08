package com.ctfo.archives.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.archives.beans.Archives;
import com.ctfo.archives.dao.ArchivesDao;
import com.ctfo.archives.service.ArchivesService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Service
public class ArchivesServiceImpl implements ArchivesService{
	@Autowired
	ArchivesDao archivesDao;
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return archivesDao.count(param);
	}

	@Override
	public PaginationResult<Archives> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return archivesDao.selectPagination(param);
	}

}
