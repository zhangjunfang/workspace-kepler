package com.ctfo.regionfileser.memorymysql.dao;

import java.util.List;

import com.ctfo.memorymysql.beans.MemTbServiceviewManage;

public interface MemTbServiceviewManageDao {
	
	/**
	 * 查询所有轨迹信息
	 */
	public List<MemTbServiceviewManage> queryAll();
	
	/**
	 * 插入测试数据
	 */
	public void insertTbService();
}
