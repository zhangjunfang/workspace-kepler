package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.FunctionTree;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 权限<br>
 * 描述： 权限<br>
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
 * <td>2014-5-21</td>
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
public interface SysFunctionService {

	/**
	 * 查看角色已分配的权限树
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public List<FunctionTree> findByRoleId(Map<String, String> map) throws CtfoAppException;

	/**
	 * 角色编辑时，初始化权限树同时选中已关联的
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public List<FunctionTree> findFunTreeRoleEdit(Map<String, String> map) throws CtfoAppException;

	/**
	 * 查询用户登录后的权限集合
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public List<String> findFunListByOpId(Map<String, String> map) throws CtfoAppException;

	/**
	 * 初始化权限树(所有)
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public List<FunctionTree> initFunTree(Map<String, String> map) throws CtfoAppException;

}
