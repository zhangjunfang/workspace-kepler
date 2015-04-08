package com.ctfo.archives.service;

import com.ctfo.archives.beans.ArchivesDetail;
import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;

public interface ArchivesDetailService extends RemoteJavaService{
	/**
	 * 
	 * @description:根据主键获取对象
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月3日上午11:14:18
	 * @modifyInformation：
	 */
	public ArchivesDetail selectPK(String tbUserOnlineId);
	
	public int count(DynamicSqlParameter param);
	
	public PaginationResult<ArchivesDetail> selectPagination(DynamicSqlParameter param);
}
