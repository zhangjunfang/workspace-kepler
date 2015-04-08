package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.TbOrganization;
import com.ctfo.sys.dao.TbOrganizationDAO;

public class TbOrganizationDAOImpl extends GenericIbatisAbstract<TbOrganization, String> implements TbOrganizationDAO {

	/**
	 * 
	 * @description:初始化机构树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日下午1:27:26
	 * @modifyInformation：
	 */
	@SuppressWarnings({ "rawtypes", "unchecked" })
	public List<TbOrganization> selectOrgTree(Map map){
		return this.getSqlMapClientTemplate().queryForList("TbOrganization.selectOrgTree", map);
	}
	
	/**
	 * 
	 * @description:机构下是否有子机构
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午1:26:19
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveSubOrg(Map map){
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrganization.haveSubOrg", map);
		if(0 < count){
			return true;
		}else{
			return false;
		}
	}
	
	/**
	 * 
	 * @description:机构下是否关联有用户
	 * @param:
	 * @author: Administrator
	 * @creatTime:  2014年4月24日下午2:44:39
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveSubOperator(Map map) {
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrganization.haveSubOperator", map);
		if(0 < count){
			return true;
		}else{
			return false;
		}
	}

	/**
	 * 
	 * @description:机构下是否关联有角色
	 * @param:
	 * @author: Administrator
	 * @creatTime:  2014年4月24日下午2:46:48
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveSubRole(Map map) {
		Integer count = (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrganization.haveSubRole", map);
		if(0 < count){
			return true;
		}else{
			return false;
		}
	}
	
	/**
	 * 
	 * @description:机构管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日下午3:57:32
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map){
		return this.getSqlMapClientTemplate().update("TbOrganization.delete", map);
	}

}