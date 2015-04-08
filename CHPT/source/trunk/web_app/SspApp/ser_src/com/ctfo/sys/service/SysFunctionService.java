package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.sys.beans.SysFunction;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： FrameworkApp
 * <br>
 * 功能：
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交慧联信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2014年3月29日</td><td>蒋东卿</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2014年3月29日下午6:53:03
 * @since JDK1.6
 */
public interface SysFunctionService {

	/**
	 * 
	 * @description:初始化权限树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午6:33:18
	 * @modifyInformation：
	 */
	public List<SysFunction> select();
	
	/**
	 * 
	 * @description:查看角色已分配的权限树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日上午9:22:14
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<SysFunction> selectByRoleId(Map map);
	
	/**
	 * 
	 * @description:角色编辑时，初始化权限树同时选中已关联的
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午1:59:06
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<SysFunction> selectFunTreeRoleEdit(Map map);
	
	/**
	 * 
	 * @description:查询用户登录后的权限集合
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月8日下午3:04:49
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<String> selectFunListByOpId(Map map);
	
}
