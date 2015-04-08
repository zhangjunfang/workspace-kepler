package com.ctfo.sys.service;

import java.util.List;

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysOperateLog;

public interface SysOperateLogService {

	/**
	 * 
	 * @description:分页时获取记录总数量
	 * @param:
	 * @author: gmein
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: gemin
	 * @modifyInformation：
	 */
	public PaginationResult<SysOperateLog> selectPagination(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:查询所有角色对象，用户分配角色权限时
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月4日上午10:58:35
	 * @modifyInformation：
	 */
	public List<SysOperateLog> select(DynamicSqlParameter param);

	public List<String> selectQuery(String str);
	
}
