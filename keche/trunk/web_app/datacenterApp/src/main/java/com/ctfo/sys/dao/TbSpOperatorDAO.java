package com.ctfo.sys.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.common.local.dao.GenericIbatisDao;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbSpOperator;

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
public interface TbSpOperatorDAO extends GenericIbatisDao<TbSpOperator, String> {

	/**
	 * 用户吊销与启用
	 * 
	 * @param tbSpOperator
	 * @return
	 */
	public int revokeOpenOperator(TbSpOperator tbSpOperator);

	/**
	 * 删除用户（逻辑删除）
	 * 
	 * @param tbSpOperator
	 * @return
	 */
	public int deleteOperator(TbSpOperator tbSpOperator);

	/**
	 * 更新密码
	 * 
	 * @param tbSpOperator
	 * @return
	 */
	public int modifyPass(TbSpOperator tbSpOperator);

	/**
	 * 用户登录
	 * 
	 * @param map
	 * @return
	 */
	public List<TbSpOperator> findOperatorLogin(Map<String, String> map);

	/**
	 * 根据主键id查询
	 * 
	 * @param map
	 * @return
	 */
	public TbSpOperator findOpDetail(Map<String, String> map);

	/**
	 * 查询主中心用户列表
	 * 
	 * @param param
	 * @return
	 */
	public PaginationResult<TbSpOperator> selectOperatorPagination(DynamicSqlParameter param);

	/**
	 * 判断用户密码是否正确
	 * 
	 * @param tbSpOperator
	 * @return
	 */
	public int countExist(TbSpOperator tbSpOperator);

}
