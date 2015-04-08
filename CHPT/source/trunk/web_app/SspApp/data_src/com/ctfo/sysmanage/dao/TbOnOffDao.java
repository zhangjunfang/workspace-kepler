package com.ctfo.sysmanage.dao;

import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.sys.beans.TbOnOff;

public interface TbOnOffDao extends GenericIbatisDao<TbOnOff, String>{
	@SuppressWarnings("rawtypes")
	public int updateOnOff(Map map);
}
