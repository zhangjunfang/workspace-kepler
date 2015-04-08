package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.beans.Project;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface ProjectManageDAO extends GenericIbatisDao<Project, Long> {
	public Map<String, Object> getProjectList(DynamicSqlParameter param) throws Exception;

	public Map<String, Object> getEditProject(DynamicSqlParameter requestParam);

	public int addProject(DynamicSqlParameter requestParam);

	public int delProject(DynamicSqlParameter requestParam);

	public int editProject(DynamicSqlParameter requestParam);
}
