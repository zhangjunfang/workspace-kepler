package com.ctfo.sysmanage.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.SysComInfo;

public interface SysCompanyDao extends GenericIbatisDao<SysComInfo, String>{
	/**
	 * 查询公司列表
	 * @return
	 */
	public List<SysComInfo> queryCompanyList(Map map);
	
	public int queryMaxCode();
	
	@SuppressWarnings("rawtypes")
	public int existLoginname(Map map);
	
	@SuppressWarnings("rawtypes")
	public int updateRevoke(Map map);
	@SuppressWarnings("rawtypes")
	public int deleteSysCom(Map map);
	
	public int update(SysComInfo sysComInfo);
	
	@SuppressWarnings("rawtypes")
	public boolean haveSubOperator(Map map);
}
