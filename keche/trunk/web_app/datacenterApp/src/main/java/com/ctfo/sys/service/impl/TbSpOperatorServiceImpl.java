package com.ctfo.sys.service.impl;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.exception.CtfoExceptionLevel;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.service.impl.BaseServiceImpl;
import com.ctfo.common.util.StaticSession;
import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.beans.TrOperatorRole;
import com.ctfo.sys.dao.TbSpOperatorDAO;
import com.ctfo.sys.dao.TrOperatorRoleDAO;
import com.ctfo.sys.service.TbSpOperatorService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 用户管理service<br>
 * 描述： 用户管理service<br>
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
 * <td>2014-5-6</td>
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
public class TbSpOperatorServiceImpl extends BaseServiceImpl<TbSpOperator, String> implements TbSpOperatorService {

	/** */
	private static final long serialVersionUID = -9004626071504980390L;

	private static Log log = LogFactory.getLog(TbSpOperatorServiceImpl.class);

	/** 用户 */
	@Autowired
	private TbSpOperatorDAO tbSpOperatorDAO;

	/** 用户角色关系 */
	@Autowired
	private TrOperatorRoleDAO trOperatorRoleDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.TbSpOperatorService#addOperator(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public PaginationResult<TbSpOperator> addOperator(TbSpOperator tbSpOperator) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			if (null != tbSpOperator) {
				Long currentTime = System.currentTimeMillis();
				tbSpOperator.setOpId(GeneratorPK.instance().getPKString());
				tbSpOperator.setCreateTime(currentTime);
				tbSpOperator.setUpdateTime(currentTime);
				tbSpOperator.setEnableFlag("1");
				tbSpOperatorDAO.insert(tbSpOperator);
				if (null != tbSpOperator.getRoleId() && !"".equals(tbSpOperator.getRoleId())) {
					TrOperatorRole trOperatorRole = new TrOperatorRole();
					trOperatorRole.setAutoId(GeneratorPK.instance().getPKString());
					trOperatorRole.setOpId(tbSpOperator.getOpId());
					trOperatorRole.setRoleId(tbSpOperator.getRoleId());
					trOperatorRole.setCenterCode(tbSpOperator.getCenterCode());
					trOperatorRoleDAO.insert(trOperatorRole);
				}
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#deleteOperator(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public PaginationResult<TbSpOperator> deleteOperator(TbSpOperator tbSpOperator) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			if (null != tbSpOperator && null != tbSpOperator.getOpId()) {
				Long currentTime = System.currentTimeMillis();
				tbSpOperator.setUpdateTime(currentTime);
				tbSpOperatorDAO.deleteOperator(tbSpOperator); // 删除用户

				Map<String, String> map = new HashMap<String, String>();
				map.put("opId", tbSpOperator.getOpId());
				map.put("centerCode", tbSpOperator.getCenterCode());
				trOperatorRoleDAO.deleteTrOperatorRole(map); // 删除用户角色绑定关系
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#revokeOpenOperator(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public PaginationResult<TbSpOperator> revokeOpenOperator(TbSpOperator tbSpOperator) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			if (null != tbSpOperator && null != tbSpOperator.getOpId()) {
				Long currentTime = System.currentTimeMillis();
				tbSpOperator.setUpdateTime(currentTime);
				tbSpOperatorDAO.revokeOpenOperator(tbSpOperator);
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#modifyPass(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public PaginationResult<TbSpOperator> modifyPass(TbSpOperator tbSpOperator) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			if (null != tbSpOperator && null != tbSpOperator.getOpId()) {
				tbSpOperatorDAO.modifyPass(tbSpOperator);
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#modifyOperator(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public PaginationResult<TbSpOperator> modifyOperator(TbSpOperator tbSpOperator) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			if (null != tbSpOperator && null != tbSpOperator.getOpId()) {
				tbSpOperator.setUpdateTime(System.currentTimeMillis());
				tbSpOperatorDAO.update(tbSpOperator);
				if (null != tbSpOperator.getRoleId() && !"".equals(tbSpOperator.getRoleId())) {
					Map<String, String> map = new HashMap<String, String>();
					map.put("opId", tbSpOperator.getOpId());
					map.put("centerCode", tbSpOperator.getCenterCode());
					trOperatorRoleDAO.deleteTrOperatorRole(map); // 删除绑定关系

					TrOperatorRole trOpRl = new TrOperatorRole();
					trOpRl.setAutoId(GeneratorPK.instance().getPKString());
					trOpRl.setOpId(tbSpOperator.getOpId());
					trOpRl.setRoleId(tbSpOperator.getRoleId());
					trOpRl.setCenterCode(tbSpOperator.getCenterCode());
					trOperatorRoleDAO.insert(trOpRl); // 重新绑定关系
					result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
				}
			} else {
				result.setResultJudge(StaticSession.DISMESSAGE_PARAMETERS); // 参数为空;
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#findOperatorDetail(java.util.Map)
	 */
	@Override
	public TbSpOperator findOperatorDetail(Map<String, String> map) throws CtfoAppException {
		try {
			if (null != map) {
				return tbSpOperatorDAO.findOpDetail(map);
			} else {
				throw new CtfoAppException(StaticSession.DISMESSAGE_PARAMETERS, CtfoExceptionLevel.recoverError);
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.TbSpOperatorService#findOperatorByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbSpOperator> findOperatorByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			result = tbSpOperatorDAO.selectPagination(param);
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#findCenterOperatorByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbSpOperator> findCenterOperatorByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			result = tbSpOperatorDAO.selectOperatorPagination(param);
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
	 * @see com.ctfo.sys.service.TbSpOperatorService#retPassword(com.ctfo.sys.beans.TbSpOperator)
	 */
	@Override
	public PaginationResult<TbSpOperator> retPassword(TbSpOperator tbSpOperator) throws CtfoAppException {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			if (null != tbSpOperator && null != tbSpOperator.getOpId()) {
				// 判断用户密码是否正确
				int count = tbSpOperatorDAO.countExist(tbSpOperator);
				if (count > 0) {
					tbSpOperatorDAO.modifyPass(tbSpOperator);
					result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
				} else {
					result.setResultJudge(StaticSession.MESSAGE_OLDPASS); // 旧密码错误
				}
			} else {
				result.setResultJudge(StaticSession.DISMESSAGE_PARAMETERS); // 参数为空;
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return result;
	}

	public void setTbSpOperatorDAO(TbSpOperatorDAO tbSpOperatorDAO) {
		this.tbSpOperatorDAO = tbSpOperatorDAO;
	}

	public void setTrOperatorRoleDAO(TrOperatorRoleDAO trOperatorRoleDAO) {
		this.trOperatorRoleDAO = trOperatorRoleDAO;
	}

}
