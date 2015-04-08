package com.ctfo.common.util;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.exception.CtfoExceptionLevel;
import com.ctfo.storage.redis.core.RedisDaoSupport;
import com.ctfo.sys.beans.TbSpOperator;
import com.google.gson.reflect.TypeToken;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 获取登陆用工具类<br>
 * 描述： 获取登陆用工具类<br>
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
 * <td>2014-5-28</td>
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
public class OperatorInfoUtil {

	public static final String DISMESSAGE_LOGIN_DATEFORMAT = "登录错误!";

	public static final String MESSAGE_LOGIN_OPERATORULL = "用户id为空!";

	public static final String DISMESSAGE_LOGIN_REDISINIT = "filter过滤Url!";

	public static final String MESSAGE_LOGIN_REDISINIT = "获取redis对象出错!";

	/** redis服务-主 */
	private static RedisDaoSupport writeJedisDao = (RedisDaoSupport) SpringBUtils.getBean("writeJedisDao");;

	/** redis服务-从 */
	private static RedisDaoSupport readJedisDao = (RedisDaoSupport) SpringBUtils.getBean("readJedisDao");;

	/**
	 * 获取登陆用户信息
	 * 
	 * @return
	 */
	public static TbSpOperator getInstrance(String opId) throws CtfoAppException {
		TbSpOperator operator = null;
		try {
			if (opId != null) {
				String value = readJedisDao.getTempCacheValue(opId + StaticSession.SYS_MARKING_PREFIX_CENTER);
				if (null != value && !"".equals(value)) {
					operator = (TbSpOperator) RedisJsonUtil.jsonToObject(value, new TypeToken<TbSpOperator>() {
					});
				}
			} else {
				throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_OPERATORULL);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return operator;
	}

	/**
	 * 判断用户是否在redis登陆过
	 * 
	 * @return
	 */
	public static boolean getUserIsLoginRedis(String opId) throws CtfoAppException {
		boolean state = false;
		try {
			if (opId != null) {
				String value = readJedisDao.getTempCacheValue(opId + StaticSession.SYS_MARKING_PREFIX_CENTER);
				if (null != value && !"".equals(value) && !"null".equals(value)) {
					state = true;
				}
			} else {
				throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_OPERATORULL);
			}
		} catch (Exception e) {
			throw new CtfoAppException(DISMESSAGE_LOGIN_REDISINIT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_REDISINIT);
		}
		return state;
	}

	/**
	 * 更新用户redis有效时间
	 * 
	 * @return
	 */
	public static void updateUserLoginRedisTime(String opId) throws CtfoAppException {
		try {
			if (opId != null) {
				String key = opId + StaticSession.SYS_MARKING_PREFIX_CENTER;
				writeJedisDao.setTempExpireTime(key, 2 * 60 * 60);
			} else {
				throw new CtfoAppException(DISMESSAGE_LOGIN_DATEFORMAT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_OPERATORULL);
			}
		} catch (Exception e) {
			throw new CtfoAppException(DISMESSAGE_LOGIN_REDISINIT, CtfoExceptionLevel.recoverError, MESSAGE_LOGIN_REDISINIT);
		}
	}

	/**
	 * 获取登陆用户企业名称
	 * 
	 * @return
	 */
	public static String getOperatorEntName(String opId) throws CtfoAppException {
		TbSpOperator operator = OperatorInfoUtil.getInstrance(opId);
		String entName = operator.getEntName();
		return entName;
	}

	/**
	 * 获取登陆用户企业id
	 * 
	 * @return
	 */
	public static String getOperatorEntId(String opId) throws CtfoAppException {
		TbSpOperator operator = OperatorInfoUtil.getInstrance(opId);
		String entId = operator.getEntId();
		return entId;
	}

	/**
	 * 获取登陆用户Id
	 * 
	 * @return
	 */
	public static String getMeMOperatorId(String sessionOpId) throws CtfoAppException {
		TbSpOperator operator = OperatorInfoUtil.getInstrance(sessionOpId);
		String opId = operator.getOpId();
		return opId;
	}

}
