package com.ctfo.sys.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.beans.TbOrgInfo;
import com.ctfo.sys.beans.TbOrganization;

public interface TbOrgDAO extends GenericIbatisDao<TbOrg, String>{

	public List<TbOrg> queryEntList(Map map);
	
	public int queryMaxCode();
	/**
	 * 
	 * @description:机构管理-添加
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午4:19:44
	 * @modifyInformation：
	 */
	public void insert(TbOrg tbOrg);
	
	/**
	 * 
	 * @description:机构管理-更新
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午4:20:03
	 * @modifyInformation：
	 */
	public int update(TbOrg tbOrg);
	
	/**
	 * 
	 * @description:机构管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午4:20:14
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map);
	
	/**
	 * 
	 * @description:获取同一级别的最大机构ID
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月21日下午1:20:36
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public String getMaxOrgId(Map map);
	
	@SuppressWarnings("rawtypes")
	public int updateRevoke(Map map);
	
	@SuppressWarnings("rawtypes")
	public int existLoginname(Map map);
}
