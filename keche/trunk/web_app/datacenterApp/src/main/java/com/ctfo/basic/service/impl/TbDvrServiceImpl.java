package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbDvr;
import com.ctfo.basic.dao.TbDvrDAO;
import com.ctfo.basic.service.TbDvrService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.service.impl.BaseServiceImpl;
import com.ctfo.sys.service.impl.TbSpOperatorServiceImpl;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：3G视频终端<br>
 * 描述：3G视频终端<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月22日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbDvrServiceImpl extends BaseServiceImpl<TbDvr, String> implements TbDvrService {

	/** */
	private static final long serialVersionUID = -1997955525316679343L;

	private static Log log = LogFactory.getLog(TbSpOperatorServiceImpl.class);

	@Autowired
	private TbDvrDAO tbDvrDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbDvrService#findDvrByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbDvr> findDvrByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbDvr> result = new PaginationResult<TbDvr>();
		try {
			result = tbDvrDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
		}
		return result;
	}

	public void setTbDvrDAO(TbDvrDAO tbDvrDAO) {
		this.tbDvrDAO = tbDvrDAO;
	}

}
