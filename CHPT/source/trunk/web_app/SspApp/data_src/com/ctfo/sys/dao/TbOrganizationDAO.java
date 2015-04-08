package com.ctfo.sys.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.TbOrganization;

public interface TbOrganizationDAO extends GenericIbatisDao<TbOrganization, String>{

	/**
	 * 
	 * @description:初始化机构树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日下午1:27:26
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<TbOrganization> selectOrgTree(Map map);
	
	/**
	 * 
	 * @description:机构下是否有子机构
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午1:26:19
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveSubOrg(Map map);
	
	/**
	 * 
	 * @description:机构下是否关联有用户
	 * @param:
	 * @author: Administrator
	 * @creatTime:  2014年4月24日下午2:44:39
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveSubOperator(Map map);
	
	/**
	 * 
	 * @description:机构下是否关联有角色
	 * @param:
	 * @author: Administrator
	 * @creatTime:  2014年4月24日下午2:46:48
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean haveSubRole(Map map);
	
	/**
	 * 
	 * @description:机构管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日下午3:57:32
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map);
	
}