package com.ctfo.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.PlatFormManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.PlatFormManageService;


@Service
public class PlatFormManageServiceImpl implements PlatFormManageService {
	@Autowired
	@Qualifier("platFormManageDAO")
	private PlatFormManageDAO platFormManageDAO;

	//@Override
	public Map<String, Object> getPlatFormList(DynamicSqlParameter param) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = platFormManageDAO.getPlatFormList(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditPlatForm(DynamicSqlParameter requestParam) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = platFormManageDAO.getEditPlatForm(requestParam);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public int addPlatForm(DynamicSqlParameter requestParam) {
		int addPlatId = 0;
		try {
			addPlatId = platFormManageDAO.addPlatForm(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (addPlatId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return addPlatId;
	}

	//@Override
	public int delPlatForm(DynamicSqlParameter requestParam) {
		int delPlatId = 0;
		try {
			delPlatId = platFormManageDAO.delPlatForm(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (delPlatId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return delPlatId;
	}

	//@Override
	public int editPlatForm(DynamicSqlParameter requestParam) {
		int editPlatId = 0;
		try {
			editPlatId = platFormManageDAO.editPlatForm(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (editPlatId == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return editPlatId;
	}

	public int checkPlatExist(DynamicSqlParameter requestParam) {
		int platExist = 0;
		try {
			platExist = platFormManageDAO.checkPlatExist(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (platExist == 0) {
			// 参数为空
			throw new CtfoAppException("", CtfoExceptionLevel.recoverError, "");
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return platExist;
	}

}
