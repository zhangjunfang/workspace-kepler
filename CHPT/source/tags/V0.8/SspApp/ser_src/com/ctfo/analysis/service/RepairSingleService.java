package com.ctfo.analysis.service;

import com.ctfo.analysis.beans.RepairSingle;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;





public interface RepairSingleService {
	/**
	 * 
	 * @description:分页时获取记录总数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月31日9:27:38
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月31日10:11:02
	 * @modifyInformation：
	 */
	public PaginationResult<RepairSingle> selectPagination(DynamicSqlParameter param);
	/**
	 * 
	 * @description:根据主键获取对象
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月3日上午11:14:18
	 * @modifyInformation：
	 */
	public RepairSingle selectPK(String maintain_id);
}