package com.ctfo.sysmanage.service;

import java.util.Map;

import com.ctfo.sys.beans.TbOnOff;

public interface AdjustOnOffService {
	
	@SuppressWarnings("rawtypes")
	public int updateOnOff(Map map);
	
	public TbOnOff selectPK(String onOffId);
}
