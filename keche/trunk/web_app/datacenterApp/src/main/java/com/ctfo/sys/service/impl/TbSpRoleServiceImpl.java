package com.ctfo.sys.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
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
import com.ctfo.common.util.StringUtil;
import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.sys.beans.TbSpRole;
import com.ctfo.sys.beans.TrRoleFunction;
import com.ctfo.sys.dao.TbSpRoleDAO;
import com.ctfo.sys.dao.TrOperatorRoleDAO;
import com.ctfo.sys.dao.TrRoleFunctionDAO;
import com.ctfo.sys.service.TbSpRoleService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 角色管理<br>
 * 描述： 角色管理<br>
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
 * <td>2014-5-8</td>
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
public class TbSpRoleServiceImpl extends BaseServiceImpl<TbSpRole, String> implements TbSpRoleService {

	/** */
	private static final long serialVersionUID = 7401151337203261956L;

	private static Log log = LogFactory.getLog(TbSpRoleServiceImpl.class);

	/** 角色 */
	@Autowired
	private TbSpRoleDAO tbSpRoleDAO;

	/** 角色权限关系 */
	@Autowired
	private TrRoleFunctionDAO trRoleFunctionDAO;

	/** 用户角色关系 */
	@Autowired
	private TrOperatorRoleDAO trOperatorRoleDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.TbSpRoleService#addRole(com.ctfo.sys.beans.TbSpRole)
	 */
	@Override
	public PaginationResult<TbSpRole> addRole(TbSpRole tbSpRole) throws CtfoAppException {
		PaginationResult<TbSpRole> result = new PaginationResult<TbSpRole>();
		try {
			if (null != tbSpRole) {
				Long currentTime = System.currentTimeMillis();
				tbSpRole.setRoleId(GeneratorPK.instance().getPKString());
				tbSpRole.setCreateTime(currentTime);
				tbSpRole.setUpdateTime(currentTime);
				tbSpRole.setEnableFlag("1");
				tbSpRoleDAO.insert(tbSpRole);
				String[] funtions = tbSpRole.getFunctionId().split(StaticSession.SPLIT);
				this.addTrRoleFun(tbSpRole, funtions); // 添加角色权限绑定关系
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
	 * @see com.ctfo.sys.service.TbSpRoleService#deleteRole(com.ctfo.sys.beans.TbSpRole)
	 */
	@Override
	public PaginationResult<TbSpRole> deleteRole(TbSpRole tbSpRole) throws CtfoAppException {
		PaginationResult<TbSpRole> result = new PaginationResult<TbSpRole>();
		try {
			if (null != tbSpRole.getRoleId()) {
				Map<String, String> map = new HashMap<String, String>();
				map.put("roleId", tbSpRole.getRoleId());
				map.put("centerCode", tbSpRole.getCenterCode());
				int count = trOperatorRoleDAO.countExist(map); // 判断角色是否被用户使用
				if (count <= 0) {
					tbSpRole.setUpdateTime(System.currentTimeMillis());
					tbSpRoleDAO.deleteRole(tbSpRole); // 删除角色
					trRoleFunctionDAO.deleteTrRoleFun(map); // 删除角色权限关系
					result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
				} else {
					result.setResultJudge(StaticSession.MESSAGE_SPROLE_FAIL); // 角色被引用 删除失败
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
	 * @see com.ctfo.sys.service.TbSpRoleService#modifyRole(com.ctfo.sys.beans.TbSpRole)
	 */
	@Override
	public PaginationResult<TbSpRole> modifyRole(TbSpRole tbSpRole) throws CtfoAppException {
		PaginationResult<TbSpRole> result = new PaginationResult<TbSpRole>();
		try {
			if (null != tbSpRole.getRoleId()) {
				tbSpRole.setUpdateTime(System.currentTimeMillis());
				tbSpRoleDAO.update(tbSpRole); // 更新角色信息
				if (!StringUtil.isBlank(tbSpRole.getFunctionId())) {
					Map<String, String> map = new HashMap<String, String>();
					map.put("roleId", tbSpRole.getRoleId());
					map.put("centerCode", tbSpRole.getCenterCode());
					String[] funtions = tbSpRole.getFunctionId().split(StaticSession.SPLIT);
					trRoleFunctionDAO.deleteTrRoleFun(map); // 删除角色权限关系
					this.addTrRoleFun(tbSpRole, funtions); // 更新角色最新权限

					result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 操作成功
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
	 * @see com.ctfo.sys.service.TbSpRoleService#findRoleDetail(java.util.Map)
	 */
	@Override
	public TbSpRole findRoleDetail(Map<String, String> map) throws CtfoAppException {
		try {
			if (null != map) {
				return tbSpRoleDAO.findRoleDetail(map);
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
	 * @see com.ctfo.sys.service.TbSpRoleService#findRoleByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbSpRole> findRoleByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbSpRole> result = new PaginationResult<TbSpRole>();
		try {
			Map<String, Object> equal = param.getEqual();
			String treeType = (String) equal.get("treeType");
			if (StaticSession.TWO.equals(treeType)) { // 判断查询主中心或分中心数据
				equal.put("isOrg", "1");
			}
			result = tbSpRoleDAO.selectPagination(param);
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
	 * @see com.ctfo.sys.service.TbSpRoleService#findRoleList(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<TbSpRole> findRoleList(DynamicSqlParameter param) throws CtfoAppException {
		List<TbSpRole> list = new ArrayList<TbSpRole>();
		try {
			Map<String, Object> equal = param.getEqual();
			String treeType = (String) equal.get("treeType");
			if (StaticSession.TWO.equals(treeType)) { // 判断查询主中心或分中心数据
				equal.put("isOrg", "1");
			}
			list = tbSpRoleDAO.findRoleList(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
		}
		return list;
	}

	/**
	 * 添加角色的权限
	 * 
	 * @param tbSpRole
	 * @param funtions
	 */
	private void addTrRoleFun(TbSpRole tbSpRole, String[] funtions) {
		List<TrRoleFunction> list = new ArrayList<TrRoleFunction>();
		for (String fun : funtions) {
			TrRoleFunction function = new TrRoleFunction();
			function.setRoleId(tbSpRole.getRoleId());
			function.setFunId(fun);
			function.setEnableFlag("1");
			function.setCenterCode(tbSpRole.getCenterCode());
			list.add(function);
		}
		trRoleFunctionDAO.batchInsert(list);
	}

	public void setTbSpRoleDAO(TbSpRoleDAO tbSpRoleDAO) {
		this.tbSpRoleDAO = tbSpRoleDAO;
	}

	public void setTrRoleFunctionDAO(TrRoleFunctionDAO trRoleFunctionDAO) {
		this.trRoleFunctionDAO = trRoleFunctionDAO;
	}

	public void setTrOperatorRoleDAO(TrOperatorRoleDAO trOperatorRoleDAO) {
		this.trOperatorRoleDAO = trOperatorRoleDAO;
	}

}
