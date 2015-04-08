package com.ctfo.basic.dao;

import com.ctfo.basic.beans.TbOrganizationMultiple;
import com.ctfo.common.local.dao.GenericIbatisDao;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 主中心组织管理<br>
 * 描述： 主中心组织管理<br>
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
 * <td>2014-6-26</td>
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
public interface TbOrganizationMultipleDAO extends GenericIbatisDao<TbOrganizationMultiple, String> {

	/**
	 * 根据组织id获取该组织下所有id
	 * 
	 * @param param
	 * @return
	 */
	public String findEntIds(DynamicSqlParameter param);

	/**
	 * 删除组织(逻辑删除)
	 * 
	 * @param tbOrganization
	 * @return
	 */
	public int deleteOrganization(TbOrganizationMultiple tbOrganizationMuli);

	/**
	 * 吊销、启用组织
	 * 
	 * @param tbOrganization
	 * @return
	 */
	public int revokeOpenOrg(TbOrganizationMultiple tbOrganizationMuli);

	/**
	 * 查询组织下是否有子企业和车队
	 * 
	 * @param org
	 * @return
	 * @throws CtfoAppException
	 */
	public int countExist(TbOrganizationMultiple tbOrganizationMuli);

}
