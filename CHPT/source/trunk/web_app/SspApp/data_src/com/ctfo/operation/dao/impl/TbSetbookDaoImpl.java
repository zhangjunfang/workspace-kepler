package com.ctfo.operation.dao.impl;

import java.util.List;

import com.ctfo.archives.beans.SysSetbook;
import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.operation.beans.TbSetbook;
import com.ctfo.operation.dao.TbSetbookDao;

public class TbSetbookDaoImpl extends GenericIbatisAbstract<TbSetbook, String> implements TbSetbookDao{
	@Override
	public SysSetbook selectPKByCom(String comId) {
		// TODO Auto-generated method stub
		try {
			List<SysSetbook> list = this.getSqlMapClientTemplate().queryForList("SysSetbook.selectPKByCom", comId);
			SysSetbook tt = null;
			if(list!=null&&list.size()>0){
				tt=list.get(0);
			}
			return tt;
		} catch (Exception e) {
		throw new CtfoAppException(e.fillInStackTrace());
	}
	}
}
