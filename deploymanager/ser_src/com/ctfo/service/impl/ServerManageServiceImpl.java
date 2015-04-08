package com.ctfo.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.ServerManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.ServerManageService;


@Service
public class ServerManageServiceImpl implements ServerManageService {
	@Autowired
	@Qualifier("serverManageDAO")
	private ServerManageDAO serverManageDAO;

	//@Override
	public Map<String, Object> getServerList(DynamicSqlParameter param) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = serverManageDAO.getServerList(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditServer(DynamicSqlParameter requestParam) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = serverManageDAO.getEditServer(requestParam);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public int addServer(DynamicSqlParameter requestParam) {
		int addServerId = 0;
		try {
			addServerId = serverManageDAO.addServer(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (addServerId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return addServerId;
	}

	//@Override
	public int delServer(DynamicSqlParameter requestParam) {
		int delServerId = 0;
		try {
			delServerId = serverManageDAO.delServer(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (delServerId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return delServerId;
	}

	//@Override
	public int editServer(DynamicSqlParameter requestParam) {
		int editServerId = 0;
		try {
			editServerId = serverManageDAO.editServer(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (editServerId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return editServerId;
	}

}
