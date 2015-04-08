package com.ctfo.sysmanage.dao.impl;

import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.sys.beans.TbOnOff;
import com.ctfo.sysmanage.dao.TbOnOffDao;
import com.ctfo.util.MongoDataSource;

public class TbOnOffDaoImpl extends GenericIbatisAbstract<TbOnOff, String> implements TbOnOffDao{
	@Autowired
	private MongoDataSource mongoDB;
	public MongoDataSource getMongoDB() {
		return mongoDB;
	}
	public void setMongoDB(MongoDataSource mongoDB) {
		this.mongoDB = mongoDB;
	}



	@Override
	public int updateOnOff(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("TbOnOff.updateOnOff",map);
	}

}
