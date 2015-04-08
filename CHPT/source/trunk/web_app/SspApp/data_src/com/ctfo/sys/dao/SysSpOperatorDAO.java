package com.ctfo.sys.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.SysSpOperator;

public interface SysSpOperatorDAO extends GenericIbatisDao<SysSpOperator, String>{
	public int queryMaxCode();
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
	 * @description:用户管理-吊销功能
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午9:37:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpen(Map map);
	
	/**
	 * 
	 * @description:用户管理-删除功能
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午9:58:29
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map);
	
	/**
	 * 
	 * @description:用户管理-更新密码
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月28日下午2:09:55
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updatePass(Map map);
	
	/**
	 * 
	 * @description:用户管理-用户登录名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日下午3:13:24
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int existOpLoginname(Map map);
	
	/**
	 * 
	 * @description:登录时
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午4:49:47
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorLogin(Map map);
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorLoginPd(Map map);
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorLoginOc(Map map);
	/**
	 * 
	 * @description:首页基本信息
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月4日
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public SysSpOperator selectOperatorHomePage(Map map);
	
}