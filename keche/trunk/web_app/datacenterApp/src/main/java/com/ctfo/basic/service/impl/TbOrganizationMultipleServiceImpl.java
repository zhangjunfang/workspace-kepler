package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbOrganizationMultiple;
import com.ctfo.basic.dao.TbOrganizationMultipleDAO;
import com.ctfo.basic.service.TbOrganizationMultipleService;
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
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-6-26</td>
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
public class TbOrganizationMultipleServiceImpl extends BaseServiceImpl<TbOrganizationMultiple, String> implements TbOrganizationMultipleService {

	/** */
	private static final long serialVersionUID = 4994485259529674261L;

	private static Log log = LogFactory.getLog(TbOrganizationMultipleServiceImpl.class);

	/** 组织 */
	@Autowired
	private TbOrganizationMultipleDAO tbOrganizationMultipleDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationMultipleService#addOrganizationMult(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public PaginationResult<TbOrganizationMultiple> addOrganizationMult(TbOrganizationMultiple org) throws CtfoAppException {
		PaginationResult<TbOrganizationMultiple> result = new PaginationResult<TbOrganizationMultiple>();
		try {
			if (null != org) {
				Long currentTime = System.currentTimeMillis();
				org.setEntId(GeneratorPK.instance().getPKString());
				org.setCreateTime(currentTime);
				org.setUpdateTime(currentTime);
				org.setEnableFlag(StaticSession.ONE); // 有效标记 1:有效 0:无效
				org.setEntState(StaticSession.ONE); // 实体状态：1为正常，0为吊销
				tbOrganizationMultipleDAO.insert(org); // 增加组织
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
	 * @see com.ctfo.basic.service.TbOrganizationMultipleService#deleteOrganizationMult(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public PaginationResult<TbOrganizationMultiple> deleteOrganizationMult(TbOrganizationMultiple org) throws CtfoAppException {
		PaginationResult<TbOrganizationMultiple> result = new PaginationResult<TbOrganizationMultiple>();
		try {
			if (null != org && null != org.getEntId()) {
				// 查询组织下是否有子企业
				int count = tbOrganizationMultipleDAO.countExist(org);
				if (count == 0) {
					org.setUpdateTime(System.currentTimeMillis());
					tbOrganizationMultipleDAO.deleteOrganization(org); // 删除组织
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
	 * @see com.ctfo.basic.service.TbOrganizationMultipleService#findEntIds(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public String findEntIds(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return tbOrganizationMultipleDAO.findEntIds(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbOrganizationMultipleService#findOrgMultByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbOrganizationMultiple> findOrgMultByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbOrganizationMultiple> result = new PaginationResult<TbOrganizationMultiple>();
		try {
			result = tbOrganizationMultipleDAO.selectPagination(param);
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
	 * @see com.ctfo.basic.service.TbOrganizationMultipleService#modifyOrganizationMult(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public PaginationResult<TbOrganizationMultiple> modifyOrganizationMult(TbOrganizationMultiple org) throws CtfoAppException {
		PaginationResult<TbOrganizationMultiple> result = new PaginationResult<TbOrganizationMultiple>();
		try {
			if (null != org && null != org.getEntId()) {
				org.setUpdateTime(System.currentTimeMillis());
				tbOrganizationMultipleDAO.update(org); // 更新组织
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
	 * @see com.ctfo.basic.service.TbOrganizationMultipleService#revokeOpenOrgMult(com.ctfo.basic.beans.TbOrganizationMultiple)
	 */
	@Override
	public PaginationResult<TbOrganizationMultiple> revokeOpenOrgMult(TbOrganizationMultiple org) throws CtfoAppException {
		PaginationResult<TbOrganizationMultiple> result = new PaginationResult<TbOrganizationMultiple>();
		try {
			if (null != org && null != org.getEntId()) {
				org.setUpdateTime(System.currentTimeMillis());
				tbOrganizationMultipleDAO.revokeOpenOrg(org); // 吊销与启用组织
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

	public void setTbOrganizationMultipleDAO(TbOrganizationMultipleDAO tbOrganizationMultipleDAO) {
		this.tbOrganizationMultipleDAO = tbOrganizationMultipleDAO;
	}

}
