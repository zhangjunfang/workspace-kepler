package com.ctfo.sys.service;

import java.util.Map;

import com.ctfo.sys.beans.SysSpOperator;

public interface LoginService {

	@SuppressWarnings("rawtypes")
	public SysSpOperator login(Map map);
	@SuppressWarnings("rawtypes")
	public SysSpOperator loginPd(Map map);
	@SuppressWarnings("rawtypes")
	public SysSpOperator loginOc(Map map);
	@SuppressWarnings("rawtypes")
	public SysSpOperator basicInfo(Map map);
	
}
