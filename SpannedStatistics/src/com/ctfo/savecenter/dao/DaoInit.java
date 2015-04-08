package com.ctfo.savecenter.dao;

import com.lingtu.xmlconf.XmlConf;

public class DaoInit {

	XmlConf config;

	public DaoInit(XmlConf config) {
		this.config = config;
	}

	public void init() throws Exception {
		//初始化全国拓区域拓扑树
		String areaTreeFileName = config.getStringValue("areaTreeFileName");
		TempMemory.buildTree(areaTreeFileName);
		// 得到数据库驱动
		String strDB = config.getStringValue("database|DbImpl");
		Class<?> cldb = Class.forName(strDB);
		DBAdapter dba = (DBAdapter) cldb.newInstance();
		dba.initDBAdapter(config);
	}
}
