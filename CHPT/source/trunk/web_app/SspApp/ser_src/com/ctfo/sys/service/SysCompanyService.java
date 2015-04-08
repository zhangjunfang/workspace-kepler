package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysComInfo;

public interface SysCompanyService extends RemoteJavaService {
	public int count(DynamicSqlParameter param);
	public PaginationResult<SysComInfo> selectPagination(DynamicSqlParameter param);
	public SysComInfo selectPK(String comId);
	/**
	 * 查询公司列表
	 * @return
	 */
	public List<SysComInfo> queryCompanyList(Map<String,String> map);
	/**
	 * 
	 * @description:用户管理-用户登录名称是否存在
	 * @param:
	 * @author: 马驰
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int existLoginname(Map map);

	public void insert(SysComInfo sysComInfo);
	
	@SuppressWarnings("rawtypes")
	public void updateRevoke(Map map);
	
	@SuppressWarnings("rawtypes")
	public int deleteSysCom(Map map);
	
	public int update(SysComInfo sysComInfo);
}
