package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbDvrser;
import com.ctfo.basic.dao.TbDvrserDAO;
import com.ctfo.basic.service.TbDvrserService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp <br>
 * 功能：3G视频服务器管理 <br>
 * 描述：3G视频服务器管理 <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * -----------------------------------------------------------------------------<br>
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
 * <td>2014年5月21日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbDvrserServiceImpl implements TbDvrserService {

	private static Log log = LogFactory.getLog(TbDvrserServiceImpl.class);

	/** 视频服务 */
	@Autowired
	private TbDvrserDAO tbDvrserDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbDvrserService#selectPagination(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbDvrser> findDvrserByParamPage(DynamicSqlParameter param) {
		try {
			return tbDvrserDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	public void setTbDvrserDAO(TbDvrserDAO tbDvrserDAO) {
		this.tbDvrserDAO = tbDvrserDAO;
	}

}
