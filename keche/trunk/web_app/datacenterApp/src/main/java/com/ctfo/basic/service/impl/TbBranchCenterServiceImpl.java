package com.ctfo.basic.service.impl;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.basic.beans.TbBranchCenter;
import com.ctfo.basic.dao.TbBranchCenterDAO;
import com.ctfo.basic.service.TbBranchCenterService;
import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.util.StaticSession;
import com.ctfo.generator.pk.GeneratorPK;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：分中心<br>
 * 描述：分中心<br>
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
 * <td>2014年6月9日</td>
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
public class TbBranchCenterServiceImpl implements TbBranchCenterService {

	private static Log log = LogFactory.getLog(TbBranchCenterServiceImpl.class);

	/** 分中心 */
	@Autowired
	private TbBranchCenterDAO tbBranchCenterDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbBranchCenterService#findBranchCenterByParamPage(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public PaginationResult<TbBranchCenter> findBranchCenterByParamPage(DynamicSqlParameter param) {
		try {
			return tbBranchCenterDAO.selectPagination(param);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.basic.service.TbBranchCenterService#addBranchCenter(com.ctfo.basic.beans.TbBranchCenter)
	 */
	@Override
	public PaginationResult<TbBranchCenter> addBranchCenter(TbBranchCenter tbBranchCenter) throws CtfoAppException {
		PaginationResult<TbBranchCenter> result = new PaginationResult<TbBranchCenter>();
		try {
			if (null != tbBranchCenter) {
				tbBranchCenter.setId(GeneratorPK.instance().getPKString());
				tbBranchCenter.setEnableFlag("1");
				tbBranchCenterDAO.insert(tbBranchCenter);
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
	 * @see com.ctfo.basic.service.TbBranchCenterService#deleteBranchCenter(com.ctfo.basic.beans.TbBranchCenter)
	 */
	@Override
	public PaginationResult<TbBranchCenter> deleteBranchCenter(TbBranchCenter tbBranchCenter) throws CtfoAppException {
		PaginationResult<TbBranchCenter> result = new PaginationResult<TbBranchCenter>();
		try {
			if (null != tbBranchCenter.getId()) {
				tbBranchCenterDAO.deleteBranchCenter(tbBranchCenter); // 删除分中心
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

	public void setTbBranchCenterDAO(TbBranchCenterDAO tbBranchCenterDAO) {
		this.tbBranchCenterDAO = tbBranchCenterDAO;
	}

}
