package com.ctfo.version.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.version.beans.Version;

/**
 * @version 1.0
 * @description:C/S 版本升级控制
 * @author 蒋东卿
 * @date 2015年2月5日上午9:45:20
 * @since JDK1.6
 */
public interface VersionServiceDao extends GenericIbatisDao<Version, String>{

	/**
	 * 
	 * @description:获取最新版本号
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月5日上午9:45:14
	 * @modifyInformation：
	 */
	public List<Version> findVersionNew(Map map);
	
	/**
	 * 
	 * @description:获取所需数据库版本
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月9日下午3:09:58
	 * @modifyInformation：
	 */
	public List<String> findVersionDb(Map map);
	
}
