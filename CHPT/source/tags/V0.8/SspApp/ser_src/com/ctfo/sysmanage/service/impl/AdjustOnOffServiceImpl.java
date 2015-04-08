package com.ctfo.sysmanage.service.impl;

import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.sys.beans.TbOnOff;
import com.ctfo.sysmanage.dao.TbOnOffDao;
import com.ctfo.sysmanage.service.AdjustOnOffService;

@Controller
public class AdjustOnOffServiceImpl implements AdjustOnOffService{
	@Autowired
	TbOnOffDao tbOnOffDao;
	
	@Override
	public int updateOnOff(Map map) {
		// TODO Auto-generated method stub
		return tbOnOffDao.updateOnOff(map);
	}

	@Override
	public TbOnOff selectPK(String onOffId) {
		// TODO Auto-generated method stub
		return tbOnOffDao.selectPK(onOffId);
	}

}
