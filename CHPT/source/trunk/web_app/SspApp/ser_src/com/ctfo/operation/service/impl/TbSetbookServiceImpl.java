package com.ctfo.operation.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.archives.beans.SysSetbook;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.operation.dao.TbSetbookDao;
import com.ctfo.operation.service.TbSetbookService;
@Service
public class TbSetbookServiceImpl implements TbSetbookService{
	@Autowired
	TbSetbookDao tbSetbookDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return tbSetbookDao.count(param);
	}

	@Override
	public SysSetbook selectPKByCom(String comId) {
		// TODO Auto-generated method stub
		return tbSetbookDao.selectPKByCom(comId);
	}
	
}
