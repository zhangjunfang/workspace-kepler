package com.ctfo.basic.controller;

import java.util.concurrent.TimeoutException;

import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.sys.dao.SysSpOperatorDAO;
import com.ctfo.sys.dao.SysSpRoleDAO;
import com.ctfo.sys.dao.TbOrgDAO;
import com.ctfo.sysmanage.dao.SysCompanyDao;
import com.ctfo.util.GeneratingCode;

public class AutoCodeInit {
	@Autowired
	private SysCompanyDao sysCompanyDao;
	@Autowired
	private TbOrgDAO tbOrgDAO;
	@Autowired
	private SysSpOperatorDAO sysSpOperatorDAO;
	@Autowired
	private SysSpRoleDAO sysSpRoleDAO;

	public void init() throws TimeoutException {
		new GeneratingCode(sysCompanyDao.queryMaxCode(),tbOrgDAO.queryMaxCode(),sysSpOperatorDAO.queryMaxCode(),sysSpRoleDAO.queryMaxCode());
	}

	public void setSysCompanyDao(SysCompanyDao sysCompanyDao) {
		this.sysCompanyDao = sysCompanyDao;
	}

	public void setTbOrgDAO(TbOrgDAO tbOrgDAO) {
		this.tbOrgDAO = tbOrgDAO;
	}

	public void setSysSpOperatorDAO(SysSpOperatorDAO sysSpOperatorDAO) {
		this.sysSpOperatorDAO = sysSpOperatorDAO;
	}
	
	public void setSysSpRoleDAO(SysSpRoleDAO sysSpRoleDAO) {
		this.sysSpRoleDAO = sysSpRoleDAO;
	}
}
