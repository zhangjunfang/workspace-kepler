package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.beans.Server;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface ServerManageDAO extends GenericIbatisDao<Server, Long> {
	public Map<String, Object> getServerList(DynamicSqlParameter param) throws Exception;

	public Map<String, Object> getEditServer(DynamicSqlParameter requestParam);

	public int addServer(DynamicSqlParameter requestParam);

	public int delServer(DynamicSqlParameter requestParam);

	public int editServer(DynamicSqlParameter requestParam);
}
