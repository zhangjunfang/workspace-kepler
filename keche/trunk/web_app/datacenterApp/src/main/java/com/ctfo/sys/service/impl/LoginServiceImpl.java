package com.ctfo.sys.service.impl;

import java.util.List;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.util.StaticSession;
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.dao.TbSpOperatorDAO;
import com.ctfo.sys.service.LoginService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 用户登录<br>
 * 描述： 用户登录<br>
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
 * <td>2014-5-23</td>
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
public class LoginServiceImpl implements LoginService {

	private static Log log = LogFactory.getLog(LoginServiceImpl.class);

	@Autowired
	private TbSpOperatorDAO tbSpOperatorDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.LoginService#findOperatorLogin(java.util.Map)
	 */
	@Override
	public PaginationResult<TbSpOperator> findOperatorLogin(Map<String, String> map) throws CtfoAppException {
		try {
			PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
			List<TbSpOperator> list = tbSpOperatorDAO.findOperatorLogin(map);
			if (null == list || 0 == list.size()) {
				result.setResultJudge(StaticSession.OP_EXIST); // 账号不存在
				return result;
			}
			if (list.size() > 1) {
				result.setResultJudge(StaticSession.OP_EXISTS); // 账号存在多个
				return result;
			}
			TbSpOperator tbSpOperator = list.get(0);
			Long currentTime = System.currentTimeMillis();
			if (!((null != tbSpOperator.getOpStartutc() && null != tbSpOperator.getOpEndutc() && currentTime >= tbSpOperator.getOpStartutc() && currentTime <= tbSpOperator.getOpEndutc() + 86400000) // 开始和结束时间都不为空
					|| (null == tbSpOperator.getOpEndutc() && null != tbSpOperator.getOpStartutc() && currentTime >= tbSpOperator.getOpStartutc()) // 结束时间为空和开始时间不为空
					|| (null == tbSpOperator.getOpStartutc() && null != tbSpOperator.getOpEndutc() && currentTime <= tbSpOperator.getOpEndutc() + 86400000) // 开始时间为空和结束时间不为空
			|| (null == tbSpOperator.getOpEndutc() && null == tbSpOperator.getOpStartutc()))) { // 开始和结束时间都为空
				result.setResultJudge(StaticSession.OP_EXPIRED); // 账号已过期
				return result;
			}
			if ("0".equals(tbSpOperator.getOpStatus())) {
				result.setResultJudge(StaticSession.OP_LOGOUT); // 账号已注销
				return result;
			}
			result.setResultJudge(StaticSession.MESSAGE_SUCCESS); // 登陆成功
			result.setData(list);
			return result;
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	public void setTbSpOperatorDAO(TbSpOperatorDAO tbSpOperatorDAO) {
		this.tbSpOperatorDAO = tbSpOperatorDAO;
	}

}
