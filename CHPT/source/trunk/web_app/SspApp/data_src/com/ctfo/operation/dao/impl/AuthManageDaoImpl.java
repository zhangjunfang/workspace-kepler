package com.ctfo.operation.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.dao.AuthManageDao;

public class AuthManageDaoImpl extends GenericIbatisAbstract<CompanyInfo, String> implements AuthManageDao{

	public void insert(CompanyInfo companyInfo){
		this.getSqlMapClientTemplate().insert("CompanyInfo.insert", companyInfo);
	}
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpen(Map map){
		return this.getSqlMapClientTemplate().update("CompanyInfo.updateRevokeOpen",map);
	}
	@Override
	public int updateAuthApproval(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("CompanyInfo.updateAuthApproval",map);
	}
	@Override
	public int updateAuthmanage(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("CompanyInfo.updateAuthManage",map);
	}
	@Override
	public int updateByMachineCode(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("CompanyInfo.updateByMachineCode",map);
	}
	@Override
	public int countForRemote(Map map) {
		// TODO Auto-generated method stub
		return (Integer)this.getSqlMapClientTemplate().queryForObject("CompanyInfo.countForRemote",map);
	}
	public int countForRemoteByComId(Map map) {
		// TODO Auto-generated method stub
		return (Integer)this.getSqlMapClientTemplate().queryForObject("CompanyInfo.countForRemoteByComId",map);
	}
	@Override
	public int updateForRemote(CompanyInfo companyInfo) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("CompanyInfo.updateForRemote",companyInfo);
	}
	@Override
	public List<CompanyInfo> getCompanyList() {
		return this.getSqlMapClientTemplate().queryForList("CompanyInfo.selectComanyInfo");
	}
	@Override
	public CompanyInfo selectByMachineCode(String machineCode) {
		// TODO Auto-generated method stub
		try {
			List<CompanyInfo> backCompanyInfoList = this.getSqlMapClientTemplate().queryForList("CompanyInfo.selectByMachineCode", machineCode);
			CompanyInfo backCompanyInfo = null;
			if(backCompanyInfoList!=null&&backCompanyInfoList.size()>0){
				backCompanyInfo = backCompanyInfoList.get(0);
			}
			return backCompanyInfo;
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	@Override
	public CompanyInfo selectPKByCom(String comId) {
		// TODO Auto-generated method stub
		try {
			List<CompanyInfo> list = this.getSqlMapClientTemplate().queryForList("CompanyInfo.selectPKByCom", comId);
			CompanyInfo tt = null;
			if(list!=null&&list.size()>0){
				tt=list.get(0);
			}
			return tt;
		} catch (Exception e) {
		throw new CtfoAppException(e.fillInStackTrace());
	}
	}
	@Override
	public CompanyInfo selectPKById(Map map) {
		// TODO Auto-generated method stub
		try {
			List<CompanyInfo> list = this.getSqlMapClientTemplate().queryForList("CompanyInfo.selectPKById", map);
			CompanyInfo tt = null;
			if(list!=null&&list.size()>0){
				tt=list.get(0);
			}
			return tt;
		} catch (Exception e) {
		throw new CtfoAppException(e.fillInStackTrace());
	}
	}
}
