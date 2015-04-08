package com.ctfo.sysmanage.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.SysComInfo;
import com.ctfo.sysmanage.dao.SysCompanyDao;

public class SysCompanyDaoImpl extends GenericIbatisAbstract<SysComInfo, String> implements SysCompanyDao{

	@Override
	public List<SysComInfo> queryCompanyList(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("SysComInfo.queryComInfoList",map);
	}

	@Override
	public int queryMaxCode() {
		// TODO Auto-generated method stub
		Integer maxId = (Integer) this.getSqlMapClientTemplate().queryForObject("SysComInfo.queryComCode");
		return maxId.intValue();
	}
	@SuppressWarnings("rawtypes")
	public int existLoginname(Map map){
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("SysComInfo.existLoginname", map);
		return count.intValue();
	}

	@Override
	public int updateRevoke(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("SysComInfo.updateRevoke",map);
	}

	@Override
	public int deleteSysCom(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("SysComInfo.deleteSysCom",map);
	}
	
	public int update(SysComInfo sysComInfo){
		int i = this.getSqlMapClientTemplate().update("SysComInfo.update", sysComInfo);
		return i;
	}

	@Override
	public boolean haveSubOperator(Map map) {
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("SysComInfo.haveSubOperator", map);
		if(0 < count){
			return true;
		}else{
			return false;
		}
	}
}
