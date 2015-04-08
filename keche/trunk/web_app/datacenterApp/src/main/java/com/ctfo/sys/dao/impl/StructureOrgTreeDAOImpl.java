package com.ctfo.sys.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.sys.beans.StructureOrgTree;
import com.ctfo.sys.dao.StructureOrgTreeDAO;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织树<br>
 * 描述： 组织树<br>
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
 * <td>2014-6-6</td>
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
public class StructureOrgTreeDAOImpl extends GenericIbatisAbstract<StructureOrgTree, String> implements StructureOrgTreeDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.StructureOrgTreeDAO#asynchronousFindById(java.util.Map)
	 */
	@Override
	public List<StructureOrgTree> asynchronousFindById(Map<String, String> map) throws CtfoAppException {
		return this.getSqlMapClientTemplate().queryForList("StructureOrgTree.asynchronousFindById", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.StructureOrgTreeDAO#asynchronousDataFindById(java.util.Map)
	 */
	@Override
	public List<StructureOrgTree> asynchronousDataFindById(Map<String, String> map) throws CtfoAppException {
		return this.getSqlMapClientTemplate().queryForList("StructureOrgTree.asynchronousDataFindById", map);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.StructureOrgTreeDAO#findAreaByLevel()
	 */
	@Override
	public List<StructureOrgTree> findAreaByLevel() {
		return this.getSqlMapClientTemplate().queryForList("StructureOrgTree.selectAreaByLevel");
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.dao.StructureOrgTreeDAO#synchronizedOrgByParam(java.util.Map)
	 */
	@Override
	public List<StructureOrgTree> synchronizedOrgByParam(Map<String, String> map) throws CtfoAppException {
		return this.getSqlMapClientTemplate().queryForList("StructureOrgTree.synchronizedOrgByParam", map);
	}

}
