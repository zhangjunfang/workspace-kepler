package com.ctfo.version.service.impl;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.version.beans.Version;
import com.ctfo.version.dao.VersionServiceDao;
import com.ctfo.version.service.VersionService;

@Service
public class VersionServiceImpl implements VersionService{

	@Autowired
	VersionServiceDao versionServiceDao;
	
	@SuppressWarnings("rawtypes")
	public List<Version> findVersionNew(Map map){
		return versionServiceDao.findVersionNew(map);
	}

	@SuppressWarnings("rawtypes")
	public List<String> findVersionDb(Map map){
		return versionServiceDao.findVersionDb(map);
	}
}
