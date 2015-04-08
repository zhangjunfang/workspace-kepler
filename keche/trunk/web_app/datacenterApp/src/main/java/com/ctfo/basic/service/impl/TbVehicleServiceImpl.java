package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbVehicle;
import com.ctfo.basic.dao.TbVehicleDAO;
import com.ctfo.basic.service.TbVehicleService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.exception.CtfoExceptionLevel;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.service.impl.BaseServiceImpl;
import com.ctfo.common.util.StaticSession;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：车辆<br>
 * 描述：车辆<br>
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
 * <td>2014年5月29日</td>
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
public class TbVehicleServiceImpl extends BaseServiceImpl<TbVehicle, String> implements TbVehicleService {

	/** */
	private static final long serialVersionUID = 6126365438533409220L;

	private static Log log = LogFactory.getLog(TbOrganizationServiceImpl.class);

	@Autowired
	private TbVehicleDAO tbVehicleDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbVehicleService#findVehicleById(java.lang.String)
	 */
	@SuppressWarnings("unchecked")
	@Override
	public PaginationResult<TbVehicle> findVehicleById(String id) throws CtfoAppException {
		try {
			if (id != null) {
				return PaginationResult.setSimpleData(tbVehicleDAO.selectPK(id));
			} else {
				// 参数为空
				throw new CtfoAppException(StaticSession.DISMESSAGE_PARAMETERS, CtfoExceptionLevel.recoverError);
			}
		} catch (CtfoAppException appe) {
			throw appe;
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
			throw new CtfoAppException(e, CtfoExceptionLevel.recoverError, "TbVehicleServiceImpl.findVehicleById方法");
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbVehicleService#findVehicleByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbVehicle> findVehicleByParamPage(DynamicSqlParameter param) throws CtfoAppException {
		PaginationResult<TbVehicle> result = new PaginationResult<TbVehicle>();
		try {
			result = tbVehicleDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		} catch (Exception e) {
			log.error(e.fillInStackTrace());
		}
		return result;
	}

	public void setTbVehicleDAO(TbVehicleDAO tbVehicleDAO) {
		this.tbVehicleDAO = tbVehicleDAO;
	}

}
