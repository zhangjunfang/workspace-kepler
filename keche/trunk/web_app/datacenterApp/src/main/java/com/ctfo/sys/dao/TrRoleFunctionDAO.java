package com.ctfo.sys.dao;

import java.util.Map;

import com.ctfo.common.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.TrRoleFunction;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 菜单权限角色关系<br>
 * 描述： 菜单权限角色关系<br>
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
 * <td>2014-5-28</td>
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
public interface TrRoleFunctionDAO extends GenericIbatisDao<TrRoleFunction, String> {

	/**
	 * 删除菜单权限角色关系
	 * 
	 * @param map
	 * @return
	 */
	public int deleteTrRoleFun(Map<String, String> map);

}
