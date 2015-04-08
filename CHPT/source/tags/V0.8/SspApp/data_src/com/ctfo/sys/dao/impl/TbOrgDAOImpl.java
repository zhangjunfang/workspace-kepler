package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.beans.TbOrganization;
import com.ctfo.sys.dao.TbOrgDAO;

public class TbOrgDAOImpl extends GenericIbatisAbstract<TbOrg, String> implements TbOrgDAO {

	public void insert(TbOrg tbOrg) {
//		this.getSqlMapClientTemplate().insert("TbOrganization.insert", tbOrganization);
		this.getSqlMapClientTemplate().insert("TbOrg.insert", tbOrg);
	}

	public int update(TbOrg tbOrg) {
		return this.getSqlMapClientTemplate().update("TbOrg.update", tbOrg);
	}

	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map) {
		return this.getSqlMapClientTemplate().delete("TbOrganization.delete", map);
	}

	/**
	 * 
	 * @description:获取同一级别的最大机构ID
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月21日下午1:20:36
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public String getMaxOrgId(Map map) {
		return (String)this.getSqlMapClientTemplate().queryForObject("TbOrg.maxOrgSql", map);
	}

	@Override
	public int queryMaxCode() {
		// TODO Auto-generated method stub
		Integer maxId = (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrg.queryComCode");
		return maxId.intValue();
	}

	@Override
	public int updateRevoke(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("TbOrg.updateRevoke",map);
	}

	@SuppressWarnings("rawtypes")
	public int existLoginname(Map map){
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrg.existLoginname", map);
		return count.intValue();
	}

	@Override
	public List<TbOrg> queryEntList(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("TbOrg.queryEntList",map);
	}
}
