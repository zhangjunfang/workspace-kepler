package com.ctfo.sys.dao.impl;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.SysSpRole;
import com.ctfo.sys.dao.SysSpRoleDAO;
import com.ctfo.util.DateUtil;

public class SysSpRoleDAOImpl extends GenericIbatisAbstract<SysSpRole, String> implements SysSpRoleDAO {

	@SuppressWarnings("rawtypes")
	public boolean isExistRoleName(Map map) {
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("SysSpRole.existRoleName", map);
		if(0 < count){
			return true;
		}else{
			return false;
		}
	}
	
	public void insert(SysSpRole sysSpRole){
		this.getSqlMapClientTemplate().insert("SysSpRole.insert", sysSpRole);
		
		this.batchInsertRoleFun(sysSpRole);
	}
	
	@SuppressWarnings("unchecked")
	public int update(SysSpRole sysSpRole)
	{
		//更新角色信息
		int i = this.getSqlMapClientTemplate().update("SysSpRole.update", sysSpRole);
		
		Map<String, String> map=new HashMap<String,String>();
		map.put("roleId", sysSpRole.getRoleId());
		map.put("roleName", sysSpRole.getRoleName());
		map.put("updateBy", sysSpRole.getUpdateBy());
		map.put("updateTime", String.valueOf(sysSpRole.getUpdateTime()));
		//拼接新的权限
		String newFunId = "'" + sysSpRole.getFunctionId().replace(",", "','") + "'";
		map.put("funId", newFunId);
		//新权限的个数
		int newFunLen = sysSpRole.getFunctionId().split(",").length;
		
		Map<String,String> m = (Map<String, String>) this.getSqlMapClientTemplate().queryForObject("SysSpRole.checkChangeByRoleId", map);
		int notInCount = new Integer(String.valueOf(m.get("notincount")));
		int inCount = new Integer(String.valueOf(m.get("incount")));
		
		
		//删除角色和权限关联关系
		this.getSqlMapClientTemplate().update("SysSpRole.deleteRoleFunByRoleId", map);
		
		//添加到操作记录：当inCount=newFunLen && notInCount=0 的时候，权限没有变化，不存日志
		if(!(inCount==newFunLen && notInCount==0)){
			this.getSqlMapClientTemplate().insert("SysSpRole.insertOpLog", map);
		}
		
		//从新保存角色和权限关联关系
		this.batchInsertRoleFun(sysSpRole);
		
		return i;
	}
	
	@SuppressWarnings("rawtypes")
	public void batchInsertRoleFun(SysSpRole sysSpRole){
		if(null != sysSpRole.getFunctionId()){
			List<Map> roleFunList = new ArrayList<Map>();
			String funIds[] = sysSpRole.getFunctionId().split(",");
			for(String funId : funIds){
				if(!"".equals(funId)){
					Map<String, Object> roleFunction = new HashMap<String, Object>();
					roleFunction.put("roleId", sysSpRole.getRoleId());
					roleFunction.put("funId", funId);
					roleFunction.put("createBy", "");
					roleFunction.put("createTime", DateUtil.dateToUtcTime(new Date()));
					roleFunction.put("enableFlag", "1");
					
					roleFunList.add(roleFunction);
				}
			}
			
			this.batchInsert(roleFunList, "insertRoleFun");
		}
	}
	
	@SuppressWarnings({ "unchecked", "rawtypes" })
	public List<String> selectRoleByEntId(Map map){
		return this.getSqlMapClientTemplate().queryForList("SysSpRole.roleListByEntId", map);
	}

	/**
	 * 角色管理-删除
	 */
	@SuppressWarnings("rawtypes")
	@Override
	public int updateDelete(Map map) {
		//删除角色和权限关联关系
		this.getSqlMapClientTemplate().update("SysSpRole.deleteRoleFunByRoleId", map);
		
		return this.getSqlMapClientTemplate().update("SysSpRole.updateDelete",map);
	}

	@SuppressWarnings("rawtypes")
	public boolean haveTrOperator(Map map) {
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("SysSpRole.haveTrOperator", map);
		if(0 < count){
			return true;
		}else{
			return false;
		}
	}

	@Override
	public int queryMaxCode() {
		// TODO Auto-generated method stub
		Integer maxId = (Integer) this.getSqlMapClientTemplate().queryForObject("SysSpRole.queryRoleCode");
		return maxId.intValue();
	}

	@Override
	public int updateRevoke(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("SysSpRole.updateRevoke",map);
	}

	@Override
	public List<SysSpRole> queryRoleList() {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("SysSpRole.queryRoleList");
	}

}