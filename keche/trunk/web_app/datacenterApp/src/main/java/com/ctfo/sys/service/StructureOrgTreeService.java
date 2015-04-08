package com.ctfo.sys.service;

import java.util.Map;

import com.ctfo.common.local.exception.CtfoAppException;

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
public interface StructureOrgTreeService {

	/**
	 * 分中心组织树查询-异步树
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public String findAsynchronousOrgTree(Map<String, String> map) throws CtfoAppException;

	/**
	 * 分中心组织树查询-同步树
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public String findSynchronizedOrgTree(Map<String, String> map) throws CtfoAppException;

	/**
	 * 分中心按省市查询组织树(支持模糊)-同步树
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public String findSynchronizedOrgTreeByProvince(Map<String, String> map) throws CtfoAppException;

	/**
	 * 主中心组织树查询-异步树
	 * 
	 * @param map
	 * @return
	 * @throws CtfoAppException
	 */
	public String findAsynchronousCenterOrgTree(Map<String, String> map) throws CtfoAppException;

}
