package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbSpRole;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 角色管理<br>
 * 描述： 角色管理<br>
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
 * <td>2014-5-8</td>
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
public interface TbSpRoleService {

	/**
	 * 添加角色
	 * 
	 * @param tbSpRole
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpRole> addRole(TbSpRole tbSpRole) throws CtfoAppException;

	/**
	 * 删除角色（逻辑删除）
	 * 
	 * @param tbSpRole
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpRole> deleteRole(TbSpRole tbSpRole) throws CtfoAppException;

	/**
	 * 修改角色
	 * 
	 * @param tbSpRole
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpRole> modifyRole(TbSpRole tbSpRole) throws CtfoAppException;

	/**
	 * 查询角色信息
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public TbSpRole findRoleDetail(Map<String, String> map) throws CtfoAppException;

	/**
	 * 查询角色列表
	 * 
	 * @param param
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbSpRole> findRoleByParamPage(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 根据组织查询角色集合
	 * 
	 * @param param
	 * @return
	 * @throws CtfoAppException
	 */
	public List<TbSpRole> findRoleList(DynamicSqlParameter param) throws CtfoAppException;
}
