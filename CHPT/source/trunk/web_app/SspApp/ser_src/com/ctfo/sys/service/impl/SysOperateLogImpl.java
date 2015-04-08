package com.ctfo.sys.service.impl;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysOperateLog;
import com.ctfo.sys.dao.SysOperateLogDAO;
import com.ctfo.sys.service.SysOperateLogService;

@Service
public class SysOperateLogImpl implements SysOperateLogService {

	@Autowired
	SysOperateLogDAO sysOperateLogDAO;
	
	/**
	 * 
	 * @description:分页时获取记录总数量
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:00:41
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param) {
		return sysOperateLogDAO.count(param);
	}

	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:01:04
	 * @modifyInformation：
	 */
	public PaginationResult<SysOperateLog> selectPagination(
			DynamicSqlParameter param) {
		return sysOperateLogDAO.selectPagination(param);
	}

	/**
	 * 
	 * @description:查询所有角色对象，用户分配角色权限时
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月4日上午10:58:35
	 * @modifyInformation：
	 */
	public List<SysOperateLog> select(DynamicSqlParameter param){
		return sysOperateLogDAO.select(param);
	}


	public List<String> selectQuery(String str) {
		return sysOperateLogDAO.opSelectList(str);
	}
	
}
