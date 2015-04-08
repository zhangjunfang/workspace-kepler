package com.ctfo.basic.service;

import com.ctfo.basic.beans.TbBranchCenter;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;

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
public interface TbBranchCenterService {

	/**
	 * 查询分中心列表
	 * 
	 * @param param
	 * @return
	 */
	public PaginationResult<TbBranchCenter> findBranchCenterByParamPage(DynamicSqlParameter dynamicSqlParameter);
	
	/**
	 * 添加分中心
	 * 
	 * @param tbBranchCenter
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbBranchCenter> addBranchCenter(TbBranchCenter tbBranchCenter) throws CtfoAppException;

	/**
	 * 删除分中心（逻辑删除）
	 * 
	 * @param tbBranchCenter
	 * @return
	 * @throws CtfoAppException
	 */
	public PaginationResult<TbBranchCenter> deleteBranchCenter(TbBranchCenter tbBranchCenter) throws CtfoAppException;
}
