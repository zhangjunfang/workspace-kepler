package com.ctfo.sys.dao;

import java.util.List;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.SysOperateLog;

public interface SysOperateLogDAO extends GenericIbatisDao<SysOperateLog, String>{
	/**
	 * 功能选择下拉列表
	 * */
	public List<String> opSelectList(String str);
}