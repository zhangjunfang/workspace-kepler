package com.ctfo.dao;

import java.util.List;

import org.springframework.stereotype.Service;

import com.ctfo.beans.SysSpOperator;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface SysSpOperatorDAO extends GenericIbatisDao<SysSpOperator, Long> {

	public List<SysSpOperator> findSpOperatorLogin(DynamicSqlParameter param) throws Exception;
}
