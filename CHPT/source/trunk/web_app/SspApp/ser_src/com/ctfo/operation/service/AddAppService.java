package com.ctfo.operation.service;

import java.util.Map;

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.AddApp;

public interface AddAppService {
	/**
	 * 
	 * @description:分页时获取记录总数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月17日17:05:38
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月17日17:05:02
	 * @modifyInformation：
	 */
	public PaginationResult<AddApp> selectPagination(DynamicSqlParameter param);
	/**
	 * 
	 * @description:根据根据公司id获取增值应用
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月17日上午15:25:18
	 * @modifyInformation：
	 */
	public AddApp selectPK(String comId);
	/**
	 * 
	 * @description:吊销功能
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月27日上午16:05:44
	 * @modifyInformation：
	 */
	public int updateRevokeOpenCloud(Map<String,String> map);
	
	/**
	 * 
	 * @description:分页时获取记录总数(增值应用)
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月17日17:05:38
	 * @modifyInformation：
	 */
	public int countAddApp(DynamicSqlParameter param);
	/**
	 * 
	 * @description:获取增值应用分页记录
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月4日14:32:02
	 * @modifyInformation：
	 */
	public PaginationResult<AddApp> selectAddApp(DynamicSqlParameter param);
	/**
	 * 
	 * @description:添加增值应用信息
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月19日09:25
	 * @modifyInformation：
	 */
	public void insert(AddApp addApp);
	/**
	 * 
	 * @description:重新授权
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月09日
	 * @modifyInformation：
	 */
	public int reAuthorizationCloud(Map<String,String> map);
	
	/**
	 * @description:查询comid对应增值应用数量
	 * @param:
	 * @author: 马驰
	 * @modifyInformation：
	 */
	public int countAddAppByComId(DynamicSqlParameter param);
}
