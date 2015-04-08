package com.ctfo.regionfileser.filemanager.service;

import java.util.List;

import com.ctfo.memorymysql.beans.MemTbServiceviewManage;

public interface MemTbServiceviewManageServiceRmi{
	
	public void saveRegionFile();
	
	public List<MemTbServiceviewManage> findListMemTbServiceview();
	
	public void insertTbService();
}
