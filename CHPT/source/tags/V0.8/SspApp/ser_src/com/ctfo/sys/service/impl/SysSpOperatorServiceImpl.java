package com.ctfo.sys.service.impl;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.dao.SysSpOperatorDAO;
import com.ctfo.sys.service.SysSpOperatorService;

@Service
public class SysSpOperatorServiceImpl extends RemoteJavaServiceAbstract implements SysSpOperatorService{

	@Autowired
	private SysSpOperatorDAO sysSpOperatorDAO;
	
	/**
	 * 
	 * @description:添加用户
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:33:17
	 * @modifyInformation：
	 */
	public void insert(SysSpOperator entity) {
		sysSpOperatorDAO.insert(entity);
	}

	/**
	 * 
	 * @description:更新用户
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:34:34
	 * @modifyInformation：
	 */
	public int update(SysSpOperator entity) {
		return sysSpOperatorDAO.update(entity);
	}

	/**
	 * 
	 * @description:根据主键获取用户对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:35:38
	 * @modifyInformation：
	 */
	public SysSpOperator selectPK(String primaryKey) {
		return sysSpOperatorDAO.selectPK(primaryKey);
	}

	/**
	 * 
	 * @description:分页时获取记录总数量
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:36:14
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param) {
		return sysSpOperatorDAO.count(param);
	}

	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月25日上午11:36:33
	 * @modifyInformation：
	 */
	public PaginationResult<SysSpOperator> selectPagination(DynamicSqlParameter param) {
		return sysSpOperatorDAO.selectPagination(param);
	}

	/**
	 * 
	 * @description:用户管理-吊销功能
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午10:20:44
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpen(Map map) {
		return sysSpOperatorDAO.updateRevokeOpen(map);
	}

	/**
	 * 
	 * @description:用户管理-删除功能
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日上午10:21:18
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map) {
		return sysSpOperatorDAO.updateDelete(map);
	}
	
	/**
	 * 
	 * @description:用户管理-用户登录名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月26日下午3:29:11
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int existOpLoginname(Map map){
		return sysSpOperatorDAO.existOpLoginname(map);
	}

	/**
	 * 
	 * @description:用户管理-更新密码
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月28日下午2:12:14
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updatePass(Map map) {
		return sysSpOperatorDAO.updatePass(map);
	}

	@Override
	public List<SysSpOperator> queryOperatorList() {
		// TODO Auto-generated method stub
		return sysSpOperatorDAO.queryOperatorList();
	}

}
