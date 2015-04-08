package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbTerminal;
import com.ctfo.basic.beans.TbTerminalOem;
import com.ctfo.basic.beans.TbTerminalProtocol;
import com.ctfo.basic.dao.TbTerminalDAO;
import com.ctfo.basic.dao.TbTerminalOemDAO;
import com.ctfo.basic.dao.TbTerminalProtocolDAO;
import com.ctfo.basic.service.TbTerminalService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.exception.CtfoExceptionLevel;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.service.impl.BaseServiceImpl;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 终端管理<br>
 * 描述： 终端管理<br>
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
 * <td>2014-6-18</td>
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
public class TbTerminalServiceImpl extends BaseServiceImpl<TbTerminal, String> implements TbTerminalService {

	/** */
	private static final long serialVersionUID = -3640463967838344050L;

	private static Log log = LogFactory.getLog(TbOrganizationServiceImpl.class);

	/** 终端DAO */
	@Autowired
	private TbTerminalDAO tbTerminalDAO;

	/** 终端厂家DAO */
	@Autowired
	private TbTerminalOemDAO tbTerminalOemDAO;

	/** 终端协议DAO */
	@Autowired
	private TbTerminalProtocolDAO tbTerminalProtocolDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbTerminalService#findTerminalByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbTerminal> findTerminalByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbTerminal> result = new PaginationResult<TbTerminal>();
		try {
			result = tbTerminalDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw new CtfoAppException(e, CtfoExceptionLevel.recoverError, "TbTerminalServiceImpl.findTerminalByParamPage方法");
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
		}
		return result;

	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbTerminalService#findOemNames()
	 */
	@Override
	public PaginationResult<TbTerminalOem> findOemNames(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return tbTerminalOemDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw new CtfoAppException(e, CtfoExceptionLevel.recoverError, "TbTerminalServiceImpl.findOemNames方法");
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbTerminalService#findProtocolNames(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbTerminalProtocol> findProtocolNames(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return tbTerminalProtocolDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw new CtfoAppException(e, CtfoExceptionLevel.recoverError, "TbTerminalServiceImpl.findProtocolNames方法");
		}
	}

	public void setTbTerminalDAO(TbTerminalDAO tbTerminalDAO) {
		this.tbTerminalDAO = tbTerminalDAO;
	}

	public void setTbTerminalOemDAO(TbTerminalOemDAO tbTerminalOemDAO) {
		this.tbTerminalOemDAO = tbTerminalOemDAO;
	}

	public void setTbTerminalProtocolDAO(TbTerminalProtocolDAO tbTerminalProtocolDAO) {
		this.tbTerminalProtocolDAO = tbTerminalProtocolDAO;
	}

}
