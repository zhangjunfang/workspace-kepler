package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.service.DynamicSqlParameter;
@Service
public interface SelectOptionsManageDAO extends GenericIbatisDao<String, String> {
	
	public Map<String,Object> getSysOptions(DynamicSqlParameter param);
}
