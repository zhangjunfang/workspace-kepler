package com.ctfo.operation.service;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.TbCloudBackUp;

public interface CloudBackUpService extends RemoteJavaService{
	/**
	 * 
	 * @param param
	 * @return 查询分页总条数
	 */
	public int count(DynamicSqlParameter param);
	/**
	 * 
	 * @param param
	 * @return 返回分页数据
	 */
	public PaginationResult<TbCloudBackUp> selectPagination(DynamicSqlParameter param);
	/**
	 * 
	 * @param cloudId
	 * @return  返回云备份对象
	 */
	public TbCloudBackUp selectByPrimaryKey(String cloudId);
	/**
	 * 删除一条云备份数据（逻辑删除）
	 * @param cloudId 云备份ID
	 * @return 
	 */
	public int deleteCloudyBackupById(String cloudId);
}
