package com.ctfo.version.service;

import java.util.List;
import java.util.Map;

import com.ctfo.version.beans.Version;

/**
 * @description:获取最新版本号
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2015年2月5日上午10:52:13
 * @since JDK1.6
 */
public interface VersionService {

	/**
	 * 
	 * @description:获取最新版本号
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月5日上午9:45:14
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<Version> findVersionNew(Map map);
	
	/**
	 * 
	 * @description:获取所需数据库版本
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月9日下午3:09:58
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<String> findVersionDb(Map map);
	
}
