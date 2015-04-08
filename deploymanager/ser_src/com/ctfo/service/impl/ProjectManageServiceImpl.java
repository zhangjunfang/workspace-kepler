package com.ctfo.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.ProjectManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.ProjectManageService;


@Service
public class ProjectManageServiceImpl implements ProjectManageService {
	@Autowired
	@Qualifier("projectManageDAO")
	private ProjectManageDAO projectManageDAO;

	//@Override
	public Map<String, Object> getProjectList(DynamicSqlParameter param) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = projectManageDAO.getProjectList(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditProject(DynamicSqlParameter requestParam) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = projectManageDAO.getEditProject(requestParam);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public int addProject(DynamicSqlParameter requestParam) {
		int addProjectId = 0;
		try {
			addProjectId = projectManageDAO.addProject(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (addProjectId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return addProjectId;
	}

	//@Override
	public int delProject(DynamicSqlParameter requestParam) {
		int delProjectId = 0;
		try {
			delProjectId = projectManageDAO.delProject(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (delProjectId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return delProjectId;
	}

	//@Override
	public int editProject(DynamicSqlParameter requestParam) {
		int editProjectId = 0;
		try {
			editProjectId = projectManageDAO.editProject(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (editProjectId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return editProjectId;
	}

}
