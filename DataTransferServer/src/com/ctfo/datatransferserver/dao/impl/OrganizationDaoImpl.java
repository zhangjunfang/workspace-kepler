package com.ctfo.datatransferserver.dao.impl;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.beans.OrganizationBean;
import com.ctfo.datatransferserver.dao.BaseDao;
import com.ctfo.datatransferserver.dao.OrganizationDao;
import com.ctfo.datatransferserver.dao.RowMapper;
import com.lingtu.xmlconf.XmlConf;

/**
 * 组织结构接口实现
 * 
 * @author yangyi
 * 
 */
public class OrganizationDaoImpl extends BaseDao implements OrganizationDao, RowMapper<OrganizationBean> {
	String queryAllOrganizationSQL;
	private static final Logger logger = LoggerFactory.getLogger(OrganizationDaoImpl.class);

	/**
	 * 查询结果集封装
	 */
	@Override
	public OrganizationBean mapRow(ResultSet rs) throws SQLException {
		OrganizationBean organizationBean = new OrganizationBean();
		organizationBean.setEntid(rs.getString("entid"));// 组织机构编码
		organizationBean.setLevel(rs.getInt("level"));// 等级
		organizationBean.setEnttype(rs.getInt("enttype"));// 类型
		return organizationBean;
	}

	/**
	 * 初始化SQL
	 * 
	 * @param config
	 */
	@Override
	public void initDBAdapter(XmlConf config) {
		// 查询权限
		queryAllOrganizationSQL = config.getStringValue("database|sqlstatement|sql_queryAllOrganization");
		logger.debug("初始化OrganizationDaoImpl");
	}

	@SuppressWarnings("unchecked")
	public List<OrganizationBean> queryAllOrganization() {
		List<OrganizationBean> list = (List<OrganizationBean>) queryList(queryAllOrganizationSQL, null, this);
		logger.info("查询到权限总数[" + list.size() + "]");
		return list;
	}

}
