package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TrServiceunit;
import com.ctfo.basic.dao.TrServiceunitDAO;
import com.ctfo.basic.service.TrServiceunitService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.service.impl.BaseServiceImpl;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 车辆注册<br>
 * 描述： 车辆注册<br>
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
 * <td>2014-6-13</td>
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
public class TrServiceunitServiceImpl extends BaseServiceImpl<TrServiceunit, String> implements TrServiceunitService {

	/** */
	private static final long serialVersionUID = 6461839174369147547L;

	private static Log log = LogFactory.getLog(TrServiceunitServiceImpl.class);

	/** 车辆注册信息 */
	@Autowired
	private TrServiceunitDAO trServiceunitDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TrServiceunitService#findServiceunitByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TrServiceunit> findServiceunitByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TrServiceunit> result = new PaginationResult<TrServiceunit>();
		try {
			result = trServiceunitDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
		}
		return result;
	}

	public void setTrServiceunitDAO(TrServiceunitDAO trServiceunitDAO) {
		this.trServiceunitDAO = trServiceunitDAO;
	}

}
