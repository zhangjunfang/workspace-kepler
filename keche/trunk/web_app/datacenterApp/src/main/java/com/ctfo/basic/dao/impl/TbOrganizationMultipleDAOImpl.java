package com.ctfo.basic.dao.impl;

import com.ctfo.basic.beans.TbOrganizationMultiple;
import com.ctfo.basic.dao.TbOrganizationMultipleDAO;
import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
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
public class TbOrganizationMultipleDAOImpl extends GenericIbatisAbstract<TbOrganizationMultiple, String> implements TbOrganizationMultipleDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationMultipleDAO#countExist(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public int countExist(TbOrganizationMultiple tbOrganizationMuli) {
		return (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrganizationMultiple.countExist", tbOrganizationMuli);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationMultipleDAO#deleteOrganization(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public int deleteOrganization(TbOrganizationMultiple tbOrganizationMuli) {
		return this.getSqlMapClientTemplate().update("TbOrganizationMultiple.updateDelete", tbOrganizationMuli);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationMultipleDAO#findEntIds(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public String findEntIds(DynamicSqlParameter param) {
		return (String) this.getSqlMapClientTemplate().queryForObject("TbOrganizationMultiple.selectEndIds", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationMultipleDAO#revokeOpenOrg(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public int revokeOpenOrg(TbOrganizationMultiple tbOrganizationMuli) {
		return this.getSqlMapClientTemplate().update("TbOrganizationMultiple.updateRevokeOpen", tbOrganizationMuli);
	}

}
