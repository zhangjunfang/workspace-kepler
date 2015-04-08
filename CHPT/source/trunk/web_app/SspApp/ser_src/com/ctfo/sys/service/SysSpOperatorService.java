package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysSpOperator;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： FrameworkApp
 * <br>
 * 功能：
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交慧联信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2014年3月25日</td><td>Administrator</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2014年3月25日上午11:12:13
 * @since JDK1.6
 */
public interface SysSpOperatorService extends RemoteJavaService {

	/**
	 * 查询人员列表
	 * @return
	 */
	public List<SysSpOperator> queryOperatorList();
	/**
	 * 
	 * @description:添加用户
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:33:17
	 * @modifyInformation：
	 */
	public void insert(SysSpOperator sysSpOperator);
	
	/**
	 * 
	 * @description:更新用户
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:34:34
	 * @modifyInformation：
	 */
	public int update(SysSpOperator sysSpOperator);
	
	/**
	 * 
	 * @description:用户管理-更新密码
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月28日下午2:12:14
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updatePass(Map map);
	
	/**
	 * 
	 * @description:根据主键获取用户对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:35:38
	 * @modifyInformation：
	 */
	public SysSpOperator selectPK(String tbId);
	
	/**
	 * 
	 * @description:用户管理-吊销功能
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午10:20:44
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpen(Map map);
	
	/**
	 * 
	 * @description:用户管理-删除功能
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午10:21:18
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map);
	
	/**
	 * 
	 * @description:用户管理-用户登录名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日下午3:29:11
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int existOpLoginname(Map map);
	
	/**
	 * 
	 * @description:分页时获取记录总数量
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:36:14
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:36:33
	 * @modifyInformation：
	 */
	public PaginationResult<SysSpOperator> selectPagination(DynamicSqlParameter param);
}
