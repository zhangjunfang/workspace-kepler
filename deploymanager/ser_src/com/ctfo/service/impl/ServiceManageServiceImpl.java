package com.ctfo.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.ServiceManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.ServiceManageService;


@Service
public class ServiceManageServiceImpl implements ServiceManageService {
	@Autowired
	@Qualifier("serviceManageDAO")
	private ServiceManageDAO serviceManageDAO;

	//@Override
	public Map<String, Object> getServiceList(DynamicSqlParameter param) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = serviceManageDAO.getServiceList(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditService(DynamicSqlParameter requestParam) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = serviceManageDAO.getEditService(requestParam);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public int addService(DynamicSqlParameter requestParam) {
		int addServiceId = 0;
		try {
			addServiceId = serviceManageDAO.addService(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (addServiceId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return addServiceId;
	}

	//@Override
	public int delService(DynamicSqlParameter requestParam) {
		int delServiceId = 0;
		try {
			delServiceId = serviceManageDAO.delService(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (delServiceId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return delServiceId;
	}

	//@Override
	public int editService(DynamicSqlParameter requestParam) {
		int editServiceId = 0;
		try {
			editServiceId = serviceManageDAO.editService(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (editServiceId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return editServiceId;
	}

}
