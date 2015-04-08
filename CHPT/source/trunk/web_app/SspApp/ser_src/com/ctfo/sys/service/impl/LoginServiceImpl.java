package com.ctfo.sys.service.impl;

import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.dao.SysSpOperatorDAO;
import com.ctfo.sys.service.LoginService;

@Service
public class LoginServiceImpl implements LoginService{

	@Autowired
	SysSpOperatorDAO sysSpOperatorDAO;
	
	@SuppressWarnings("rawtypes")
	public SysSpOperator login(Map map) {
		return sysSpOperatorDAO.selectOperatorLogin(map);
	}
	@SuppressWarnings("rawtypes")
	public SysSpOperator loginPd(Map map) {
		// TODO Auto-generated method stub
		return sysSpOperatorDAO.selectOperatorLoginPd(map);
	}
	@SuppressWarnings("rawtypes")
	public SysSpOperator loginOc(Map map) {
		// TODO Auto-generated method stub
		return sysSpOperatorDAO.selectOperatorLoginOc(map);
	}
	@SuppressWarnings("rawtypes")
	public SysSpOperator basicInfo(Map map) {
		// TODO Auto-generated method stub
		return sysSpOperatorDAO.selectOperatorHomePage(map);
	}
}
