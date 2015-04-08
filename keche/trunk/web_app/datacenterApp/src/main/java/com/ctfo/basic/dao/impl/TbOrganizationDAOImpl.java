package com.ctfo.basic.dao.impl;

import com.ctfo.basic.beans.TbOrganization;
import com.ctfo.basic.dao.TbOrganizationDAO;
import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.common.local.obj.DynamicSqlParameter;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织管理<br>
 * 描述： 组织管理<br>
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
 * <td>2014-5-14</td>
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
public class TbOrganizationDAOImpl extends GenericIbatisAbstract<TbOrganization, String> implements TbOrganizationDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationDAO#findEntIds(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public String findEntIds(DynamicSqlParameter param) {
		return (String) this.getSqlMapClientTemplate().queryForObject("TbOrganization.selectEndIds", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationDAO#deleteOrganization(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public int deleteOrganization(TbOrganization tbOrganization) {
		return this.getSqlMapClientTemplate().update("TbOrganization.updateDelete", tbOrganization);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationDAO#revokeOpenOrg(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public int revokeOpenOrg(TbOrganization tbOrganization) {
		return this.getSqlMapClientTemplate().update("TbOrganization.updateRevokeOpen", tbOrganization);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbOrganizationDAO#countExist(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public int countExist(TbOrganization tbOrganization) {
		return (Integer) this.getSqlMapClientTemplate().queryForObject("TbOrganization.countExist", tbOrganization);
	}

}
