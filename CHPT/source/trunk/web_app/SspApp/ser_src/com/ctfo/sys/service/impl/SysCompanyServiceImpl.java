package com.ctfo.sys.service.impl;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoAppExceptionDefinition;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysComInfo;
import com.ctfo.sys.service.SysCompanyService;
import com.ctfo.sysmanage.dao.SysCompanyDao;

@Service
public class SysCompanyServiceImpl extends RemoteJavaServiceAbstract implements SysCompanyService{
	@Autowired
	SysCompanyDao sysCompanyDao;
	
	@Override
	public int count(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return sysCompanyDao.count(param);
	}

	@Override
	public PaginationResult<SysComInfo> selectPagination(DynamicSqlParameter param) {
		// TODO Auto-generated method stub
		return sysCompanyDao.selectPagination(param);
	}

	@Override
	public SysComInfo selectPK(String comId) {
		// TODO Auto-generated method stub
		return sysCompanyDao.selectPK(comId);
	}

	@Override
	public List<SysComInfo> queryCompanyList(Map map) {
		// TODO Auto-generated method stub
		return sysCompanyDao.queryCompanyList(map);
	}

	@Override
	public int existLoginname(Map map) {
		// TODO Auto-generated method stub
		return sysCompanyDao.existLoginname(map);
	}

	@Override
	public void insert(SysComInfo sysComInfo) {
		// TODO Auto-generated method stub
		sysCompanyDao.insert(sysComInfo);
	}

	@Override
	public void updateRevoke(Map map) {
		// TODO Auto-generated method stub
		sysCompanyDao.updateRevoke(map);
	}

	@Override
	public int deleteSysCom(Map map) {
		// TODO Auto-generated method stub
		if(sysCompanyDao.haveSubOperator(map)){
			throw new CtfoAppException(CtfoAppExceptionDefinition.COM_D_HAVESUBORG);
		}
		return sysCompanyDao.deleteSysCom(map);
	}

	@Override
	public int update(SysComInfo sysComInfo) {
		// TODO Auto-generated method stub
		return sysCompanyDao.update(sysComInfo);
	}
	
}
