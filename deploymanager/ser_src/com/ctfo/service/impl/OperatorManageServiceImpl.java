package com.ctfo.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.OperatorManageDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.OperatorManageService;


@Service
public class OperatorManageServiceImpl implements OperatorManageService {
	
	/***
	 * 错误提示:登陆错误
	 */
	public static final String DISMESSAGE_LOGIN_DATEFORMAT = "登录错误!";
	
	/***
	 * 错误提示：输入登录信息错误!
	 */
	public static final String MESSAGE_LOGIN_NULL = "输入登录信息错误!";
	
	/***
	 * 错误提示：企业已注销!
	 */
	public static final String MESSAGE_LOGIN_CORPLOGOUT = "企业已注销!";
	
	/***
	 * 错误提示：用户已注销!
	 */
	public static final String MESSAGE_LOGIN_OPERATORSTATE = "用户已注销!";
	
	/***
	 * 错误提示：账号不在有效使用期内!
	 */
	public static final String MESSAGE_LOGIN_OPERATORDATEOUT = "账号不在有效使用期内!";
	
	@Autowired
	@Qualifier("operatorManageDAO")
	private OperatorManageDAO operatorManageDAO;

	//@Override
	public int addOperator(DynamicSqlParameter param) {
		int addUserId = 0;
		try {
			addUserId = operatorManageDAO.addOperator(param);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (addUserId == 0) {
			// 参数为空
			throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_NULL);
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			
		}
		return addUserId;
	}

	//@Override
	public Map<String, Object> getUserList(DynamicSqlParameter param) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = operatorManageDAO.getUserList(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public Map<String, Object> getEditUser(DynamicSqlParameter requestParam) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			map = operatorManageDAO.getEditUser(requestParam);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}

	//@Override
	public int editUser(DynamicSqlParameter requestParam) {
		int ifUpdate = 0;
		try {
			ifUpdate = operatorManageDAO.editUser(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return ifUpdate;
	}

	//@Override
	public int delUser(DynamicSqlParameter requestParam) {
		int delUserId = 0;
		try {
			delUserId = operatorManageDAO.delUser(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return delUserId;
	}

	public int checkUserExist(DynamicSqlParameter requestParam) {
		int userExist = 0;
		try {
			userExist = operatorManageDAO.checkUserExist(requestParam);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return userExist;
	}
}
