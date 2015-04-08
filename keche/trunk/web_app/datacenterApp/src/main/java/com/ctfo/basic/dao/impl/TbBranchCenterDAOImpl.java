package com.ctfo.basic.dao.impl;

import com.ctfo.basic.beans.TbBranchCenter;
import com.ctfo.basic.dao.TbBranchCenterDAO;
import com.ctfo.common.local.daoImpl.GenericIbatisAbstract;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：分中心<br>
 * 描述：分中心<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年6月9日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbBranchCenterDAOImpl extends GenericIbatisAbstract<TbBranchCenter, String> implements TbBranchCenterDAO {

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.dao.TbBranchCenterDAO#deleteBranchCenter(com.ctfo.basic.beans.TbBranchCenter)
	 */
	@Override
	public int deleteBranchCenter(TbBranchCenter tbBranchCenter) {

		return this.getSqlMapClientTemplate().update("TbBranchCenter.updateDelete", tbBranchCenter);
	}

}
