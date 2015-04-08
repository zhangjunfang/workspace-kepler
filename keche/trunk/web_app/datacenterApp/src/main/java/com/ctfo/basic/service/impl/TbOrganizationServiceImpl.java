package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbOrgInfo;
import com.ctfo.basic.beans.TbOrganization;
import com.ctfo.basic.dao.TbOrgInfoDAO;
import com.ctfo.basic.dao.TbOrganizationDAO;
import com.ctfo.basic.service.TbOrganizationService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.service.impl.BaseServiceImpl;
import com.ctfo.common.util.StaticSession;
import com.ctfo.generator.pk.GeneratorPK;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织管理<br>
 * 描述： 组织管理<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-5-14</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class TbOrganizationServiceImpl extends BaseServiceImpl<TbOrganization, String> implements TbOrganizationService {

	/** */
	private static final long serialVersionUID = 1432916941712149199L;

	private static Log log = LogFactory.getLog(TbOrganizationServiceImpl.class);

	/** 组织 */
	@Autowired
	private TbOrganizationDAO tbOrganizationDAO;

	/** 组织基本信息 */
	@Autowired
	private TbOrgInfoDAO tbOrgInfoDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationService#findEntIds(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public String findEntIds(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return tbOrganizationDAO.findEntIds(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationService#findOrgByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbOrganization> findOrgByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbOrganization> result = new PaginationResult<TbOrganization>();
		try {
			result = tbOrganizationDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
		}
		return result;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationService#addOrganization(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public PaginationResult<TbOrganization> addOrganization(TbOrganization org) throws CtfoAppException {
		PaginationResult<TbOrganization> result = new PaginationResult<TbOrganization>();
		try {
			if (null != org) {
				Long currentTime = System.currentTimeMillis();
				org.setEntId(GeneratorPK.instance().getPKString());
				org.setCreateTime(currentTime);
				org.setUpdateTime(currentTime);
				org.setEnableFlag("1");
				tbOrganizationDAO.insert(org); // 增加组织
				TbOrgInfo orgInfo = this.queryTbCorpInfo(org);
				tbOrgInfoDAO.insert(orgInfo); // 增加组织基本信息
				result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
			} else {
				result.setResultJudge(StaticSession.DISMESSAGE_PARAMETERS); // 参数为空
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return result;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationService#modifyOrganization(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public PaginationResult<TbOrganization> modifyOrganization(TbOrganization org) throws CtfoAppException {
		PaginationResult<TbOrganization> result = new PaginationResult<TbOrganization>();
		try {
			if (null != org && null != org.getEntId()) {
				org.setUpdateTime(System.currentTimeMillis());
				tbOrganizationDAO.update(org); // 更新组织
				TbOrgInfo orgInfo = this.queryTbCorpInfo(org);
				tbOrgInfoDAO.update(orgInfo); // 增加组织基本信息
				result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
			} else {
				result.setResultJudge(StaticSession.DISMESSAGE_PARAMETERS); // 参数为空
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return result;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationService#deleteOrganization(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public PaginationResult<TbOrganization> deleteOrganization(TbOrganization org) throws CtfoAppException {
		PaginationResult<TbOrganization> result = new PaginationResult<TbOrganization>();
		try {
			if (null != org && null != org.getEntId()) {
				// 查询组织下是否有子企业和车队
				int count = tbOrganizationDAO.countExist(org);
				if (count == 0) {
					org.setUpdateTime(System.currentTimeMillis());
					tbOrganizationDAO.deleteOrganization(org); // 删除组织
					result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
				} else {
					result.setResultJudge(StaticSession.MESSAGE_REMOVE_ORG); // 先删除下级组织
				}
			} else {
				result.setResultJudge(StaticSession.DISMESSAGE_PARAMETERS); // 参数为空
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return result;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationService#revokeOpenOrg(com.ctfo.basic.beans.TbOrganization)
	 */
	@Override
	public PaginationResult<TbOrganization> revokeOpenOrg(TbOrganization org) throws CtfoAppException {
		PaginationResult<TbOrganization> result = new PaginationResult<TbOrganization>();
		try {
			if (null != org && null != org.getEntId()) {
				org.setUpdateTime(System.currentTimeMillis());
				tbOrganizationDAO.revokeOpenOrg(org); // 吊销与启用组织
				result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
			} else {
				result.setResultJudge(StaticSession.DISMESSAGE_PARAMETERS); // 参数为空
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return result;
	}

	/**
	 * 封装tb_org_info对象
	 * 
	 * @param org
	 * @return
	 */
	private TbOrgInfo queryTbCorpInfo(TbOrganization org) {
		TbOrgInfo orgInfo = new TbOrgInfo();
		orgInfo.setEntId(org.getEntId());
		orgInfo.setCorpCode(org.getCorpCode());
		orgInfo.setCorpQuale(org.getCorpQuale());
		orgInfo.setOrgShortname(org.getOrgShortname());
		orgInfo.setOrgAddress(org.getOrgAddress());
		orgInfo.setOrgCzip(org.getOrgCzip());
		orgInfo.setUrl(org.getUrl());
		orgInfo.setOrgCmail(org.getOrgCmail());
		orgInfo.setCorpProvince(org.getCorpProvince());
		orgInfo.setCorpCity(org.getCorpCity());
		orgInfo.setCorpLevel(org.getCorpLevel());
		orgInfo.setOrgCfax(org.getOrgCfax());
		orgInfo.setOrgCname(org.getOrgCname());
		orgInfo.setOrgCphone(org.getOrgCphone());
		orgInfo.setCenterCode(org.getCenterCode());
		return orgInfo;
	}

	public void setTbOrganizationDAO(TbOrganizationDAO tbOrganizationDAO) {
		this.tbOrganizationDAO = tbOrganizationDAO;
	}

	public void setTbOrgInfoDAO(TbOrgInfoDAO tbOrgInfoDAO) {
		this.tbOrgInfoDAO = tbOrgInfoDAO;
	}

}
