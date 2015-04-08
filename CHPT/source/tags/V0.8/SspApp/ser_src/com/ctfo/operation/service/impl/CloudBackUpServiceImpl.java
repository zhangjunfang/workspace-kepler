package com.ctfo.operation.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.TbCloudBackUp;
import com.ctfo.operation.dao.CloudBackUpDao;
import com.ctfo.operation.service.CloudBackUpService;
@Controller
public class CloudBackUpServiceImpl extends RemoteJavaServiceAbstract implements CloudBackUpService {
	
	@Autowired
	CloudBackUpDao cloudBackUpDao;	
	/**
	 * 获取分页的总条数
	 */
	public int count(DynamicSqlParameter param) {
		return cloudBackUpDao.count(param);
	}
	/**
	 * 返回分页数据
	 */
	@Override
	public PaginationResult<TbCloudBackUp> selectPagination(
			DynamicSqlParameter param) {
		return cloudBackUpDao.selectPagination(param);
	}
	/**
	 * 根据主键查询对象信息
	 */
	@Override
	public TbCloudBackUp selectByPrimaryKey(String cloudId) {
		// TODO Auto-generated method stub
		return cloudBackUpDao.selectPK(cloudId);
	}
	/**
	 * 删除云备份（逻辑删除）
	 */
	@Override
	public int deleteCloudyBackupById(String cloudId) {
		// TODO Auto-generated method stub
		return cloudBackUpDao.deleteCloudyBackupById(cloudId);
	}
	

}
