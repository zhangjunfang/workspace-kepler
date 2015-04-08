package com.ctfo.datatransferserver.dao;

import java.util.List;

import com.ctfo.datatransferserver.beans.OrganizationBean;
import com.lingtu.xmlconf.XmlConf;

/**
 * 组织结构接口
 * 
 * @author yangyi
 * 
 */
public interface OrganizationDao {
	/**
	 * 初始化SQL
	 * 
	 * @param config
	 */
	public void initDBAdapter(XmlConf config);

	/**
	 * 查询所有组织结构
	 * 
	 * @return
	 */
	public List<OrganizationBean> queryAllOrganization();

}
