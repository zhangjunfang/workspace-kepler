package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.beans.Role;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface RoleManageDAO extends GenericIbatisDao<Role, Long> {
	public Map<String, Object> getRoleList(DynamicSqlParameter param) throws Exception;

	public Map<String, Object> getEditRole(DynamicSqlParameter requestParam);

	public int addRole(DynamicSqlParameter requestParam);

	public int delRole(DynamicSqlParameter requestParam);

	public int editRole(DynamicSqlParameter requestParam);

	public int checkRoleExist(DynamicSqlParameter requestParam);
}
