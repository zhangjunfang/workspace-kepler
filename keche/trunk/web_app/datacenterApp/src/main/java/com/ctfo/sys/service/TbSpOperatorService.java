package com.ctfo.sys.service;

import java.util.Map;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbSpOperator;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 用户管理service<br>
 * 描述： 用户管理service<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-5-6</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public interface TbSpOperatorService {

	/**
	 * 添加用户
	 * 
	 * @param tbSpOperator
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> addOperator(TbSpOperator tbSpOperator) throws CtfoAppException;

	/**
	 * 删除用户（逻辑删除）
	 * 
	 * @param tbSpOperator
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> deleteOperator(TbSpOperator tbSpOperator) throws CtfoAppException;

	/**
	 * 用户吊销与启用
	 * 
	 * @param tbSpOperator
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> revokeOpenOperator(TbSpOperator tbSpOperator) throws CtfoAppException;

	/**
	 * 更新密码
	 * 
	 * @param tbSpOperator
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> modifyPass(TbSpOperator tbSpOperator) throws CtfoAppException;

	/**
	 * 修改系统用户信息
	 * 
	 * @param tbSpOperator
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> modifyOperator(TbSpOperator tbSpOperator) throws CtfoAppException;

	/**
	 * 查询用户信息详情
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public TbSpOperator findOperatorDetail(Map<String, String> map) throws CtfoAppException;

	/**
	 * 查询用户列表
	 * 
	 * @param param
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> findOperatorByParamPage(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 查询主中心用户列表
	 * 
	 * @param param
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> findCenterOperatorByParamPage(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 修改登录用户密码
	 * 
	 * @param tbSpOperator
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpOperator> retPassword(TbSpOperator tbSpOperator) throws CtfoAppException;

}
