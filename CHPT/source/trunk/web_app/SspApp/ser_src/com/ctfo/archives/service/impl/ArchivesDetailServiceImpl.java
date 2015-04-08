package com.ctfo.archives.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.archives.beans.ArchivesDetail;
import com.ctfo.archives.dao.ArchivesDetailDao;
import com.ctfo.archives.service.ArchivesDetailService;
import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

@Service
public class ArchivesDetailServiceImpl extends RemoteJavaServiceAbstract implements ArchivesDetailService{
	@Autowired
	ArchivesDetailDao archivesDetailDao;
	@Override
	public ArchivesDetail selectPK(String tbUserOnlineId) {
		// TODO Auto-generated method stub
		return archivesDetailDao.selectPK(tbUserOnlineId);
	}
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return archivesDetailDao.count(param);
	}
	@Override
	public PaginationResult<ArchivesDetail> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return archivesDetailDao.selectPagination(param);
	}
}
