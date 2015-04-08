package com.ctfo.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.RoleManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.RoleManageService;


@Service
public class RoleManageServiceImpl implements RoleManageService {
	@Autowired
	@Qualifier("roleManageDAO")
	private RoleManageDAO roleManageDAO;

	//@Override
	public Map<String, Object> getRoleList(DynamicSqlParameter param) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = roleManageDAO.getRoleList(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditRole(DynamicSqlParameter requestParam) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = roleManageDAO.getEditRole(requestParam);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public int addRole(DynamicSqlParameter requestParam) {
		int addRoleId = 0;
		try {
			addRoleId = roleManageDAO.addRole(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (addRoleId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return addRoleId;
	}

	//@Override
	public int delRole(DynamicSqlParameter requestParam) {
		int delRoleId = 0;
		try {
			delRoleId = roleManageDAO.delRole(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (delRoleId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return delRoleId;
	}

	//@Override
	public int editRole(DynamicSqlParameter requestParam) {
		int editRoleId = 0;
		try {
			editRoleId = roleManageDAO.editRole(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (editRoleId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return editRoleId;
	}

	public int checkRoleExist(DynamicSqlParameter requestParam) {
		int roleExist = 0;
		try {
			roleExist = roleManageDAO.checkRoleExist(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (roleExist == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return roleExist;
	}

}
