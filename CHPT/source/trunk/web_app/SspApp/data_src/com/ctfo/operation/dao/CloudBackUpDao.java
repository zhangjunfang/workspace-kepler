package com.ctfo.operation.dao;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.operation.beans.TbCloudBackUp;

public interface CloudBackUpDao extends GenericIbatisDao<TbCloudBackUp, String>{
	
	/**
	 * 删除一条云备份数据（逻辑删除）
	 * @param cloudId 云备份ID
	 * @return 
	 */
	public int deleteCloudyBackupById(String cloudId);



}
