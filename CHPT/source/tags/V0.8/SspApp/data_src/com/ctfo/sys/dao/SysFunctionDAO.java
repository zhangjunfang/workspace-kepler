package com.ctfo.sys.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.SysFunction;

public interface SysFunctionDAO extends GenericIbatisDao<SysFunction, String>{

	/**
	 * 
	 * @description:查看角色已分配的权限树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日上午9:24:48
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<SysFunction> selectByRoleId(Map map);
	
	/**
	 * 
	 * @description:角色编辑时，初始化权限树同时选中已关联的
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午1:56:14
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<SysFunction> selectFunTreeRoleEdit(Map map);
	
	/**
	 * 
	 * @description:查询用户登录后的权限集合
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月8日下午3:04:49
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<String> selectFunListByOpId(Map map);
	
}