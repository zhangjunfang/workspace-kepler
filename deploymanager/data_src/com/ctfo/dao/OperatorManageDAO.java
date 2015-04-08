package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.beans.Operator;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface OperatorManageDAO extends GenericIbatisDao<Operator, Long> {
	
	public int addOperator(DynamicSqlParameter param) throws Exception;

	public Map<String,Object> getUserList(DynamicSqlParameter param);

	public Map<String, Object> getEditUser(DynamicSqlParameter requestParam);

	public int editUser(DynamicSqlParameter requestParam);

	public int delUser(DynamicSqlParameter requestParam);

	public int checkUserExist(DynamicSqlParameter requestParam);
}
