package com.ctfo.stat.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.stat.beans.TlRepFacActDetailMonth;
import com.ctfo.stat.dao.TlRepFacActDetailMonthDAO;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 考核指标按月统计<br>
 * 描述： 考核指标按月统计 <br>
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
 * <td>2014-6-18</td>
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
public class TlRepFacActDetailMonthDAOImpl extends GenericIbatisAbstract<TlRepFacActDetailMonth, String> implements TlRepFacActDetailMonthDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.stat.dao.TlRepFacActDetailMonthDAO#querySumActivityDegreeMonth(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<Map<String, Object>> querySumActivityDegreeMonth(DynamicSqlParameter param) {
		return this.getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + "querySumActivityDegreeMonth", param);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.stat.dao.TlRepFacActDetailMonthDAO#querySumJoinNetVhMonth(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<Map<String, Object>> querySumJoinNetVhMonth(DynamicSqlParameter param) {
		return this.getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + "querySumJoinNetVhMonth", param);
	}

}
