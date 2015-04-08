package com.ctfo.service.impl;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import com.ctfo.beans.SysSpOperator;
import com.ctfo.dao.SysSpOperatorDAO;
import com.ctfo.exception.CtfoAppException;
import com.ctfo.exception.CtfoExceptionLevel;
import com.ctfo.service.DynamicSqlParameter;
import com.ctfo.service.PaginationResult;
import com.ctfo.service.SysSpOperatorService;


@Service
public class SysSpOperatorServiceImpl implements SysSpOperatorService {
	
	/***
	 * 错误提示:登陆错误
	 */
	public static final String DISMESSAGE_LOGIN_DATEFORMAT = "登录错误!";
	
	/***
	 * 错误提示：输入登录信息错误!
	 */
	public static final String MESSAGE_LOGIN_NULL = "输入登录信息错误!";
	
	/***
	 * 错误提示：企业已注销!
	 */
	public static final String MESSAGE_LOGIN_CORPLOGOUT = "企业已注销!";
	
	/***
	 * 错误提示：用户已注销!
	 */
	public static final String MESSAGE_LOGIN_OPERATORSTATE = "用户已注销!";
	
	/***
	 * 错误提示：账号不在有效使用期内!
	 */
	public static final String MESSAGE_LOGIN_OPERATORDATEOUT = "账号不在有效使用期内!";
	
	@Autowired
	@Qualifier("sysSpOperatorDAO")
	private SysSpOperatorDAO spOperatorDAO;

	@SuppressWarnings("unchecked")
	//@Override
	public PaginationResult<SysSpOperator> login(DynamicSqlParameter param) {
		List<SysSpOperator> list = new ArrayList<SysSpOperator>();
		try {
			list = spOperatorDAO.findSpOperatorLogin(param);
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (list == null || list.size() == 0) {
			// 参数为空
			throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_NULL);
		} else {   // 不能满足以上四种情况，账号都不在有效期范围
			SysSpOperator oper = list.get(0);
			Long currentTime = System.currentTimeMillis();
			if (checkTerm(oper, currentTime)) {
				if (!"0".equals(oper.getOpStatus())) {   // 判断用户是否注销
					if (!"0".equals(oper.getEntState())) {    // 判断用户所属组织是否注销
						return PaginationResult.setSimpleData(oper);
					} else {     // 登陆验证成功
						throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_CORPLOGOUT);
					}
				} else {       // 用户已注销
					throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_OPERATORSTATE);
				}
			} else {       // 账号已经过期
				throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_OPERATORDATEOUT);
			}
		}
	}
	
	/***
	 * 判断登陆条件
	 * @param oper 登陆用户信息
	 * @param currentTime 当前时间
	 * @author: 刘杰
	 * @creatTime:  2013-3-26上午08:42:40
	 * 
	 */
	private boolean checkTerm(SysSpOperator oper,Long currentTime) {
		//开始和结束时间都不为空
		if (null != oper.getOpStartutc() && null != oper.getOpEndutc() && currentTime >= oper.getOpStartutc() && currentTime <= oper.getOpEndutc() + 86400000) {
			return true;
		}
		//结束时间为空和开始时间不为空
		if (null == oper.getOpEndutc() && null != oper.getOpStartutc() && currentTime >= oper.getOpStartutc()) {
			return true;
		}
		//开始时间为空和结束时间不为空
		if (null == oper.getOpStartutc() && null != oper.getOpEndutc() && currentTime <= oper.getOpEndutc() + 86400000) {
			return true;
		}
		//开始和结束时间都为空
		if (null == oper.getOpEndutc() && null == oper.getOpStartutc()) {
			return true;
		}
		return false;
	}

	
}
