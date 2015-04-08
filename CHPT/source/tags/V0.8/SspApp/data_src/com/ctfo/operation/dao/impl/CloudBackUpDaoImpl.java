package com.ctfo.operation.dao.impl;

import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.operation.beans.TbCloudBackUp;
import com.ctfo.operation.dao.CloudBackUpDao;

public class CloudBackUpDaoImpl extends GenericIbatisAbstract<TbCloudBackUp, String>implements CloudBackUpDao {
	/**
	 * 根据云备份ID 逻辑删除与备份
	 * @param cloudId
	 * @return
	 */
	public int deleteCloudyBackupById(String cloudId){
		return this.getSqlMapClientTemplate().update("TbCloudBackUp.deleteCloudyBackupById", cloudId);
	}

}
