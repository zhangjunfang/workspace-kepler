package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.dao.TbSpOperatorDAO;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 用户管理<br>
 * 描述： 用户管理<br>
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
@SuppressWarnings("unchecked")
public class TbSpOperatorDAOImpl extends GenericIbatisAbstract<TbSpOperator, String> implements TbSpOperatorDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#revokeOpenOperator(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public int revokeOpenOperator(TbSpOperator tbSpOperator) {
		return this.getSqlMapClientTemplate().update("TbSpOperator.updateRevokeOpen", tbSpOperator);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#deleteOperator(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public int deleteOperator(TbSpOperator tbSpOperator) {
		return this.getSqlMapClientTemplate().update("TbSpOperator.updateDelete", tbSpOperator);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#modifyPass(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public int modifyPass(TbSpOperator tbSpOperator) {
		return this.getSqlMapClientTemplate().update("TbSpOperator.updatePass", tbSpOperator);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#findOperatorLogin(java.util.Map)
	 */
	@Override
	public List<TbSpOperator> findOperatorLogin(Map<String, String> map) {
		return this.getSqlMapClientTemplate().queryForList("TbSpOperator.selectParamForLogin", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#findOpDetail(java.util.Map)
	 */
	@Override
	public TbSpOperator findOpDetail(Map<String, String> map) {
		return (TbSpOperator) this.getSqlMapClientTemplate().queryForObject("TbSpOperator.selectOpDetail", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#selectOperatorPagination(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbSpOperator> selectOperatorPagination(DynamicSqlParameter param) {
		return selectPaginationDynamic(param, "selectOperatorPageForParam", "countOperatorParam");
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.TbSpOperatorDAO#countExist(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public int countExist(TbSpOperator tbSpOperator) {
		return (Integer) this.getSqlMapClientTemplate().queryForObject("TbSpOperator.countExist", tbSpOperator);
	}

}
