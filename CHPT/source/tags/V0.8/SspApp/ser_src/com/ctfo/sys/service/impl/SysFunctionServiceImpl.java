package com.ctfo.sys.service.impl;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.dao.SysFunctionDAO;
import com.ctfo.sys.service.SysFunctionService;

@Service
public class SysFunctionServiceImpl implements SysFunctionService {

	@Autowired
	SysFunctionDAO sysFunctionDAO;
	
	public List<SysFunction> select() {
		return sysFunctionDAO.select();
	}

	@SuppressWarnings("rawtypes")
	public List<SysFunction> selectByRoleId(Map map) {
		return sysFunctionDAO.selectByRoleId(map);
	}

	@SuppressWarnings("rawtypes")
	public List<SysFunction> selectFunTreeRoleEdit(Map map) {
		return sysFunctionDAO.selectFunTreeRoleEdit(map);
	}
	
	@SuppressWarnings("rawtypes")
	public List<String> selectFunListByOpId(Map map){
		return sysFunctionDAO.selectFunListByOpId(map);
	}

}
