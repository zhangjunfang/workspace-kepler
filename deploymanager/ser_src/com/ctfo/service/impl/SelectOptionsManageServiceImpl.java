package com.ctfo.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.dao.SelectOptionsManageDAO;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.SelectOptionsManageService;


@Service
public class SelectOptionsManageServiceImpl implements SelectOptionsManageService {
	
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
	@Qualifier("selectOptionsManageDAO")
	private SelectOptionsManageDAO selectOptionsManageDAO;

	public Map<String,Object> getSysOptions(DynamicSqlParameter param) {
		Map<String,Object> map = new HashMap<String,Object>();
		try {
			map = selectOptionsManageDAO.getSysOptions(param);
		} catch (Exception e) {
			map = null;
			e.printStackTrace();
		}
		return map;
	}
}
