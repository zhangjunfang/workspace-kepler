package com.ctfo.operation.dao;

import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.AddApp;
import com.ctfo.operation.beans.CompanyInfo;

public interface AddAppDao extends GenericIbatisDao<AddApp, String>{
	/**
	 * 
	 * @description:吊销功能
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月27日上午9:37:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpenCloud(Map map);
	
	/**
	 * 查询符合条件的记录数
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件（其他参数内容不起作用）。此参数设置为null，则相当于count()
	 * @return
	 */
	public abstract int countAddApp(DynamicSqlParameter param) throws CtfoAppException;
	/**
	 * 按条件查询记录，并处理成分页结果
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件
	 * @return PaginationResult对象，包括（符合条件的）总记录数、页实体对象List等
	 * @throws Exception
	 */
	public abstract PaginationResult<AddApp> selectAddApp(DynamicSqlParameter param) throws CtfoAppException;
	/**
	 * 
	 * @description:添加增值应用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月19日09:28
	 * @modifyInformation：
	 */
	public void insert(AddApp addApp);
	/**
	 * 
	 * @description:重新授权
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月09日
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int reAuthorizationCloud(Map map);
	
	/**
	 * @description:查询comid对应增值应用数量
	 * @param:
	 * @author: 马驰
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int countAddAppByComId(DynamicSqlParameter param);
}
