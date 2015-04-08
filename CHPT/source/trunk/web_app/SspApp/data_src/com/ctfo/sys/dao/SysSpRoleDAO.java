package com.ctfo.sys.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.SysSpRole;

public interface SysSpRoleDAO extends GenericIbatisDao<SysSpRole, String>{
	
	public List<SysSpRole> queryRoleList();
	
	
	public int queryMaxCode();
	/**
	 * 
	 * @description:角色名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:02:12
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean isExistRoleName(Map map);
	
	/**
	 * 
	 * @description:保存角色同时保存角色与权限关系
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:02:12
	 * @modifyInformation：
	 */
	public void insert(SysSpRole sysSpRole);
	
	/**
	 * 
	 * @description:修改角色同时更新角色与权限关系
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:02:12
	 * @modifyInformation：
	 */
	public int update(SysSpRole sysSpRole);
	
	/**
	 * 
	 * @description:新建用户时，角色多选下拉数据
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月9日下午1:47:22
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<String> selectRoleByEntId(Map map);
	
	/**
	 * 
	 * @description:角色管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午2:48:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map);
	
	/**
	 * 
	 * @description:删除角色时判断，是否关联有用户
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午3:06:39
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveTrOperator(Map map);
	
	@SuppressWarnings("rawtypes")
	public int updateRevoke(Map map);
}