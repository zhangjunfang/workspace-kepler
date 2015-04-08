package com.ctfo.dao.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.beans.Operator;
import com.ctfo.dao.SelectOptionsManageDAO;
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
 * <td>gem</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author gem
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class SelectOptionsManageDAOImpl extends GenericIbatisAbstract<String, String> implements SelectOptionsManageDAO {

	public static final String GET_ROLE_OPTIONS = "getRoleOptions";
	public static final String GET_PLAT_OPTIONS = "getPlatOptions";
	public static final String GET_SERVICE_TYPE_OPTIONS = "getServiceTypeOptions";
	public static final String GET_SERVICE_LAUNCH_OPTIONS = "getServiceLaunchOptions";

	public Map<String,Object> getSysOptions(DynamicSqlParameter param) {
		Map<String,Object> map = new HashMap<String, Object>();
		try {
			List<String> roleOptions = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_ROLE_OPTIONS, param);
			List<String> platOptions = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_PLAT_OPTIONS, param);
			List<String> serviceTypeOptions = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_SERVICE_TYPE_OPTIONS, param);
			List<String> serviceLaunchOptions = getSqlMapClientTemplate().queryForList(sqlmapNamespace + "." + GET_SERVICE_LAUNCH_OPTIONS, param);
			
			map.put("roleOptions", roleOptions);
			map.put("platOptions", platOptions);
			map.put("serviceTypeOptions", serviceTypeOptions);
			map.put("serviceLaunchOptions", serviceLaunchOptions);
		} catch (Exception e) {
			map = null;
			throw new CtfoAppException(e.fillInStackTrace());
		}
		return map;
	}
}
