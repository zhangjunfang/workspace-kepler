package com.ctfo.sys.dao.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.dao.SysSpOperatorDAO;
import com.ctfo.util.GeneratorUUID;

public class SysSpOperatorDAOImpl extends GenericIbatisAbstract<SysSpOperator, String> implements SysSpOperatorDAO {

	public void insert(SysSpOperator sysSpOperator){
		this.getSqlMapClientTemplate().insert("SysSpOperator.insert", sysSpOperator);
		this.batchInsertOpRole(sysSpOperator);
	}
	
	public int update(SysSpOperator sysSpOperator){
		//更新用户信息
		int i = this.getSqlMapClientTemplate().update("SysSpOperator.update", sysSpOperator);
		
		Map<String, String> map=new HashMap<String,String>();
		map.put("opId", sysSpOperator.getOpId());
		//删除用户和角色关联关系
		this.getSqlMapClientTemplate().update("SysSpOperator.deleteOpRoleFunByOpId", map);
		
		//从新保存用户和角色关联关系
		this.batchInsertOpRole(sysSpOperator);
		
		return i;
	}
	
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpen(Map map){
		return this.getSqlMapClientTemplate().update("SysSpOperator.updateRevokeOpen",map);
	}
	
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map){
		//删除用户和角色关联关系
		this.getSqlMapClientTemplate().update("SysSpOperator.deleteOpRoleFunByOpId", map);
		
		return this.getSqlMapClientTemplate().update("SysSpOperator.updateDelete",map);
	}
	
	@SuppressWarnings("rawtypes")
	public int updatePass(Map map) {
		return this.getSqlMapClientTemplate().update("SysSpOperator.updatePass",map);
	}
	
	@SuppressWarnings("rawtypes")
	public int existOpLoginname(Map map){
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("SysSpOperator.existOpLoginname", map);
		return count.intValue();
	}

	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorLogin(Map map) {
		return (SysSpOperator)this.getSqlMapClientTemplate().queryForObject("SysSpOperator.selectOperatorLogin", map);
	}
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorLoginPd(Map map) {
		return (SysSpOperator)this.getSqlMapClientTemplate().queryForObject("SysSpOperator.selectOperatorLoginPd", map);
	}
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorLoginOc(Map map) {
		return (SysSpOperator)this.getSqlMapClientTemplate().queryForObject("SysSpOperator.selectOperatorLoginOc", map);
	}
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorHomePage(Map map) {
		return (SysSpOperator)this.getSqlMapClientTemplate().queryForObject("SysSpOperator.selectOperatorHomePage", map);
	}
	@SuppressWarnings("rawtypes")
	public void batchInsertOpRole(SysSpOperator sysSpOperator){
		if(null != sysSpOperator.getRoleId()){
			List<Map> opRoleList = new ArrayList<Map>();
			String roleIds[] = sysSpOperator.getRoleId().split(";");
			for(String roleId : roleIds){
				if(!"".equals(roleId)){
					Map<String, Object> OpRole = new HashMap<String, Object>();
					OpRole.put("autoId", GeneratorUUID.generateUUID());
					OpRole.put("opId", sysSpOperator.getOpId());
					OpRole.put("roleId", roleId);
					
					opRoleList.add(OpRole);
				}
			}
			
			this.batchInsert(opRoleList, "insertOpRole");
		}
	}

	@Override
	public List<SysSpOperator> queryOperatorList() {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("SysSpOperator.queryOperatorList");
	}

	@Override
	public int queryMaxCode() {
		// TODO Auto-generated method stub
		Integer maxId = (Integer) this.getSqlMapClientTemplate().queryForObject("SysSpOperator.queryComCode");
		return maxId.intValue();
	}

}