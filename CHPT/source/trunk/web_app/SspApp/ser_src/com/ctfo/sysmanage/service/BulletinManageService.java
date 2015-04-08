package com.ctfo.sysmanage.service;

import java.util.List;
import java.util.Map;

import com.ctfo.annouce.beans.TbAttachment;
import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.operation.beans.BulletinManage;
import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.beans.TbSetbook;
import com.ctfo.sys.beans.TbOnOff;

public interface BulletinManageService extends RemoteJavaService{
	
	/**
	 * 
	 * @param param
	 * @return 查询分页总条数
	 */
	public int count(DynamicSqlParameter param);
	/**
	 * 
	 * @param param
	 * @return 返回分页数据
	 */
	public PaginationResult<BulletinManage> selectPagination(DynamicSqlParameter param);
	/**
	 * 
	 * @param cloudId
	 * @return  
	 */
	public BulletinManage selectByPrimaryKey(String annoucId);
	public List<Map<String,Object>> selectByPrimaryKey(String annoucId,String s);
	/**
	 *  新增公告
	 * @param bulletinManage
	 */
	public void insert(BulletinManage bulletinManage);
	/**
	 *  修改公告
	 * @param bulletinManage
	 * @return
	 */
	public int update(Map map);
	/**
	 * 删除公告
	 * @param map
	 * @return
	 */
	public int delete(Map<String, String> map);
	/**
	 * 查询公司列表
	 * @return
	 */
	public List<CompanyInfo> queryCompanyList();
	/**
	 * 
	 * @param comId 公司编码
	 * @return 该公司所有帐套信息
	 */
	public List<TbSetbook> queryCompanySetbookList(String comId);
	/**
	 * 
	 * @param comId 公司ID
	 * @return 公司名称
	 */
	public  String getCompanyNameById(String comId);
	/**
	 * 
	 * @param setbookId 帐套ID
	 * @return 帐套名称
	 */
	public  String getCompanySetbookNameById(String setbookId);
	
	/**
	 * 
	 * @param deptId 部门编码
	 * @return 部门名称
	 */
	public  String getDeptNameById(String deptId);
	
	/**
	 * 
	 * @param employeeId 员工ID
	 * @return 员工名称（发布人）
	 */
	public  String getDeptEmployeeName(String employeeId);
	/**
	 * 
	 * @param attachment
	 * 保存公告附件
	 */
	public void insertAttachment(TbAttachment attachment);
	/**
	 * 
	 * @param annoucId
	 * @return 返回一条公告的附件列表
	 */
	public List<TbAttachment> selectListByPrimaryKey(String annoucId);
	/**
	 * 公告发布，开关关
	 * @param annoucId
	 * @return
	 */
	public int updatePulishStatus(Map map);
	/**
	 * 公告发布，开关开(审核-通过审核/已发布)
	 * @param annoucId
	 * @return
	 */
	public int updatePulishStatusToExamine(Map map);
	/**
	 * 公告发布，开关开(审核-驳回)
	 * @param annoucId
	 * @return
	 */
	public int updatePulishStatusToReject(Map map);	
	/**
	 * 撤销公告
	 * @param annoucId
	 * @return
	 */
	public int cancelAnnouce(Map map);
	/**
	 * 查询部门列表
	 * @return
	 */
	public List queryAnnouceDeptList(Map map);
	/**
	 * 根据部门编号查询部门人员列表
	 * @param dicCode
	 * @return
	 */
	public List queryAnnouceDeptEmployeeList(Map map);
	/**
	 * 
	 * @param annoucId 公告ID
	 * @param attachId 附件ID
	 * 
	 */
	public int deleteAnnouceFileById(String annoucId, String attachId);
	/**
	 * 
	 * @param attachMap 附件信息
	 */
	public int updateAttach(Map<String, Object> attachMap);
	/**
	 * 查询开关表
	 * @return
	 */
	public TbOnOff selectPK(String onOffId);
	/**
	 * 将公告存储Jedis(新增-发布)
	 */
	public void addAnnouceToJedis(BulletinManage bulletinManage);
	/***
	 * 将公告状态更新到Jedis(审核-发布,撤销,发布)
	 * @param map
	 */
	public void refreshAnnouceToJedis(Map map);
}
