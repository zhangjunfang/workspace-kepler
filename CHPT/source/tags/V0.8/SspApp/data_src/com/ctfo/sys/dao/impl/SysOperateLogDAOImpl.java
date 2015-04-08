package com.ctfo.sys.dao.impl;

import java.util.List;


import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.SysOperateLog;
import com.ctfo.sys.dao.SysOperateLogDAO;

public class SysOperateLogDAOImpl extends GenericIbatisAbstract<SysOperateLog, String> implements SysOperateLogDAO {

	@SuppressWarnings("unchecked")
	public List<String> opSelectList(String str){
		try {
			this.getSqlMapClientTemplate().delete("SysOperateLog.delNone");
		} catch (Exception e) {
			
		}
		List<String> ls = this.getSqlMapClientTemplate().queryForList("SysOperateLog.opSelectList", str);
		return ls;
	}
}