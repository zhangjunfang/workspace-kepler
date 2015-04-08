package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.beans.Services;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface ServiceManageDAO extends GenericIbatisDao<Services, Long> {
	public Map<String, Object> getServiceList(DynamicSqlParameter param) throws Exception;

	public Map<String, Object> getEditService(DynamicSqlParameter requestParam);

	public int addService(DynamicSqlParameter requestParam);

	public int delService(DynamicSqlParameter requestParam);

	public int editService(DynamicSqlParameter requestParam);
}
