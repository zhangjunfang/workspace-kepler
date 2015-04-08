package com.ctfo.sysmanage.dao.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.annouce.beans.TbAttachment;
import com.ctfo.local.daoImpl.GenericIbatisAbstract;
import com.ctfo.operation.beans.BulletinManage;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.beans.TbSetbook;
import com.ctfo.sysmanage.dao.BulletinManageDao;

public class BulletinManageDaoImpl extends GenericIbatisAbstract<BulletinManage, String> implements BulletinManageDao {
	/**
	 * 新增公告		
	 */
	public void insert(BulletinManage bulletinManage){
		this.getSqlMapClientTemplate().insert("BulletinManage.insert",bulletinManage);
	}
	/**
	 * 编辑公告
	 */
	public int update(Map map){
		return this.getSqlMapClientTemplate().update("BulletinManage.update", map);
	}
	/**
	 * 删除公告
	 */
	public int delete(Map map){
		return this.getSqlMapClientTemplate().update("BulletinManage.delete",map);
	}
	/**
	 * 查询公司列表
	 */
	@SuppressWarnings("unchecked")
	@Override
	public List<CompanyInfo> queryCompanyList() {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("BulletinManage.queryComInfoList");
	}
	/**
	 * 根据公司编码查询帐套集合
	 */
	@SuppressWarnings("unchecked")
	@Override
	public List<TbSetbook> queryCompanySetbookList(String comId) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("BulletinManage.queryComSetbookList",comId);
	}
	/**
	 * 根据公司Id 获取公司名称
	 */
	@Override
	public String getCompanyNameById(String comId) {
		// TODO Auto-generated method stub
		return (String) this.getSqlMapClientTemplate().queryForObject("BulletinManage.getCompanyNameById", comId);
	}
	/**
	 * 根据公司帐套Id 获取公司帐套名称
	 */
	@Override
	public String getCompanySetbookNameById(String setbookId) {
		// TODO Auto-generated method stub
		return (String)this.getSqlMapClientTemplate().queryForObject("BulletinManage.getCompanySetbookNameById", setbookId);
	}
	/**
	 * 根据部门Id 获取部门名称
	 */
	@Override
	public String getDeptNameById(String deptId) {
		// TODO Auto-generated method stub
		return (String) this.getSqlMapClientTemplate().queryForObject("BulletinManage.getDeptNameById", deptId);
	}
	/**
	 * 根据员工Id 获取员工姓名
	 */
	@Override
	public String getDeptEmployeeName(String employeeId) {
		// TODO Auto-generated method stub
		return (String)this.getSqlMapClientTemplate().queryForObject("BulletinManage.getDeptEmployeeName", employeeId);
	}
	/**
	 * 保存附件信息
	 */
	@Override
	public void insertAttachment(TbAttachment attachment) {
		// TODO Auto-generated method stub
		this.getSqlMapClientTemplate().insert("tbAttachment.insertAttachment", attachment);
	}
	/**
	 * 根据附件ID查询附件列表
	 */
	@SuppressWarnings("unchecked")
	@Override
	public List<TbAttachment> selectListByPrimaryKey(String annoucId) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("BulletinManage.selectListByPrimaryKey", annoucId);
	}
	
	
	/**
	 * 发布公告
	 */
	@Override
	public int updatePulishStatus(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("BulletinManage.updatePulishStatus", map);
	}
	/**
	 * 撤销公告
	 */
	@Override
	public int cancelAnnouce(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("BulletinManage.cancelAnnouce", map);
	}
	/**
	 * 部门列表
	 */
	@SuppressWarnings("rawtypes")
	@Override
	public List queryAnnouceDeptList(Map map) {
		// TODO Auto-generated method stub
		return  this.getSqlMapClientTemplate().queryForList("BulletinManage.queryAnnouceDeptList",map);
	}
	/**
	 * 根据部门ID获取部门人员列表
	 */
	@SuppressWarnings("rawtypes")
	@Override
	public List queryAnnouceDeptEmployeeList(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().queryForList("BulletinManage.queryAnnouceDeptEmployeeList", map);
	}
	/**
	 * 根据公告ID 和 附件ID 删除 附件
	 */
	@Override
	public int deleteAnnouceFileById(String annoucId, String attachId) {
		// TODO Auto-generated method stub
		Map<String,String> delFile = new HashMap<String,String>();
		delFile.put("annoucId", annoucId);
		delFile.put("attachId", attachId);
		return this.getSqlMapClientTemplate().delete("BulletinManage.deleteAnnouceFileById",delFile);
	}
	@Override
	public int updateAttach(Map<String, Object> attachMap) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("BulletinManage.updateAttach", attachMap);
	}
	@Override
	public int updatePulishStatusToExamine(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("BulletinManage.updatePulishStatusToExamine", map);
	}
	@Override
	public int updatePulishStatusToReject(Map map) {
		// TODO Auto-generated method stub
		return this.getSqlMapClientTemplate().update("BulletinManage.updatePulishStatusToReject", map);
	}
	
}
