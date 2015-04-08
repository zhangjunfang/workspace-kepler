package com.ctfo.analysis.service;

import com.ctfo.analysis.beans.RepairInfo;
import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface RepairService extends RemoteJavaService{
	/**
	 * 
	 * @description:分页时获取记录总数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月30日下午15:55:38
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月14日下午15:45:02
	 * @modifyInformation：
	 */
	public PaginationResult<RepairInfo> selectPagination(DynamicSqlParameter param);
}
