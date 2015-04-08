package com.ctfo.operation.dao;

import java.util.List;
import java.util.Map;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.operation.beans.CompanyInfo;

public interface AuthManageDao extends GenericIbatisDao<CompanyInfo, String>{
	/**
	 * 
	 * @description:添加公司
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月21日下午17:17:17
	 * @modifyInformation：
	 */
	public void insert(CompanyInfo companyInfo);
	public CompanyInfo selectPKById(Map map);
	/**
	 * 
	 * @description:吊销功能
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月27日上午9:37:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateRevokeOpen(Map map);
	/**
	 * 
	 * @description:审批
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月28日上午19:51:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateAuthApproval(Map map);	
	/**
	 * 
	 * @description:管理
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年10月29日上午10:34:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateAuthmanage(Map map);
	/**
	 * @description:根据机器码更新鉴权状态
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年11月26日
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateByMachineCode(Map map);
	/**
	 * @description:cs接口:查询机器码数据库存在数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月01日
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int countForRemote(Map map);
	/**
	 * @description:cs接口:查询ComId在数据库存在数
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2015年02月28日
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int countForRemoteByComId(Map map);
	/**
	 * @description:cs接口:更新机器码对应数据
	 * @param:
	 * @author: 马驰
	 * @creatTime:  2014年12月01日
	 * @modifyInformation：
	 */
	public int updateForRemote(CompanyInfo companyInfo);
	
	/**
	 * 查询所有公司信息
	 * 
	 * @return
	 */
	public List<CompanyInfo> getCompanyList();
	/**
	 * 根据机器码查询公司信息
	 * @param machineCode
	 * @return
	 */
	public CompanyInfo selectByMachineCode(String machineCode);
	
	public CompanyInfo selectPKByCom(String comId);
}
