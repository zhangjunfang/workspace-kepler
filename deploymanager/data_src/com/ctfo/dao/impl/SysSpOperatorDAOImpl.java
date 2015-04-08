package com.ctfo.dao.impl;

import java.util.List;

import com.ctfo.beans.SysSpOperator;
import com.ctfo.dao.SysSpOperatorDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.service.DynamicSqlParameter;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
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
 * <td>2012-12-12</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class SysSpOperatorDAOImpl extends GenericIbatisAbstract<SysSpOperator, Long> implements SysSpOperatorDAO {

	/***
	 * 根据条件查询登陆信息
	 */
	public static final String SQLID_SELECTPARAMFORLOGIN = "selectParamForLogin";

	//@Override
	public List<SysSpOperator> findSpOperatorLogin(DynamicSqlParameter param) throws Exception {
		try {
			List<SysSpOperator> data = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + SQLID_SELECTPARAMFORLOGIN, param);
			return data;
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

}
