package com.ctfo.version.dao.impl;

import java.util.List;
import java.util.Map;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.version.beans.Version;
import com.ctfo.version.dao.VersionServiceDao;

public class VersionServiceDaoImpl extends GenericIbatisAbstract<Version, String> implements VersionServiceDao{

	/**
	 * 
	 * @description:获取最新版本号
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月5日上午9:45:14
	 * @modifyInformation：
	 */
	@SuppressWarnings({ "unchecked", "rawtypes" })
	public List<Version> findVersionNew(Map map) {
		return this.getSqlMapClientTemplate().queryForList("Version.select",map);
	}
	
	/**
	 * 
	 * @description:获取所需数据库版本
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2015年2月9日下午3:09:58
	 * @modifyInformation：
	 */
	@SuppressWarnings({ "rawtypes", "unchecked" })
	public List<String> findVersionDb(Map map){
		return this.getSqlMapClientTemplate().queryForList("Version.selectVersionDb",map);
	}
	
}
