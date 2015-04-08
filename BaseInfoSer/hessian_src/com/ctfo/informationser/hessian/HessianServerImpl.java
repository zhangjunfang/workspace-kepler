package com.ctfo.informationser.hessian;
 
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.informationser.util.SpringBUtils;
import com.ctfo.hessian.service.HessianServer;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoExceptionLevel;


/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： BsSer <br>
 * 功能：Hessian统一调用接口实现 <br>
 * 描述：Hessian统一调用接口实现 <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>2011-9-21</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
public class HessianServerImpl implements HessianServer {

	private static Log log = LogFactory.getLog(HessianServerImpl.class);

	/**
	 * 后台提供服务的后缀名过滤
	 */
	private String suffix;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.hessian.service.HessianServer#hessianInvoke(java.lang.String, java.lang.String, java.lang.Class<?>[], java.lang.Object[])
	 */
	@Override
	public Object hessianInvoke(String className, String methodName, Class<?>[] paramTypes, Object[] paramValues) throws CtfoAppException {
		Object result = null;
		if (null == suffix) {
			log.info("服务端不提供任何方法！");
			throw new CtfoAppException("服务端不提供任何方法!", CtfoExceptionLevel.systemError);
		}
		// 通过SpringBean获取远程对象
		Object object = SpringBUtils.getBean(className);
		// Class<?> classTypes = Class.forName(className);
		Method method;
		try {
			method = object.getClass().getMethod(methodName, paramTypes);
			if (!className.endsWith(suffix)) {
				log.info("服务端不提供此方法：[" + className + "]请检查前台调用接口类名是否符合请求的后台服务！");
				throw new CtfoAppException("服务端不提供此方法：[" + className + "]", CtfoExceptionLevel.systemError);
			} else {
				result = method.invoke(object, paramValues);
			}
		} catch (InvocationTargetException e) {
			//得到invoke方法执行的原始异常
			if(e.getTargetException() instanceof CtfoAppException){
				
					//抛出原异常
					throw (CtfoAppException)e.getTargetException();
				
			}else{
				log.info("服务端执行方法：[" + className + "]#" + methodName + "().出现异常，请检查后台服务是否正常！");
				throw new CtfoAppException(e.fillInStackTrace(),CtfoExceptionLevel.systemError,"服务端方法出现异常：[" + className + "]#" + methodName);
			}
			
		}catch (Exception e) {
			log.info("服务端执行方法：[" + className + "]#" + methodName + "().出现异常，请检查后台服务是否正常！");
			throw new CtfoAppException(e.fillInStackTrace(),CtfoExceptionLevel.systemError,"服务端方法出现异常：[" + className + "]#" + methodName);
		}
		method = null;
		//不是所有方法都需要返回值
	/*	if (result == null) {
			throw new CtfoAppException("服务端方法执行：[" + className + "]#" + methodName + ",结果为空!", CtfoExceptionLevel.systemError);
		}*/
		return result;
	}

	public void setSuffix(String suffix) {
		this.suffix = suffix;
	}
}
