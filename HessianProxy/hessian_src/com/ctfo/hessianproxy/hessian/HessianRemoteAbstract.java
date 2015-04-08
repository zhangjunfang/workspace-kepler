/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.hessianproxy.hessian;


import com.ctfo.hessian.service.HessianServer;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoExceptionLevel;



/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： BsWeb <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2011-9-20</td>
 * <td>wangpeng</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wangpeng
 * @since JDK1.6
 */
public abstract class HessianRemoteAbstract implements HessianRemote {

	
	/**
	 * 实现远程hessian调用接口
	 * 
	 * 
	 * @param hessianServer
	 *            需要请求的HessianServer地址
	 * @param beanId
	 *            需要请求的远程服务service 的beanID
	 * @param methodName
	 *            需要请求的远程服务service 方法名
	 * @param hessianServer
	 *            需要请求的HessianServer地址
	 * @return Object对象
	 */
	@SuppressWarnings("rawtypes")
	@Override
	public Object execute(HessianServer hessianServer,String beanId,String methodName, Object... params) throws CtfoAppException {
		// 获取当前运行类名及方法名
		//StackTraceElement[] stacks = new Throwable().getStackTrace();

		// 当前运行类名
		//String className = stacks[2].getClassName();
		//className = className.substring(className.lastIndexOf(".") + 1).replace("ServiceImpl", StaticSession.SUFFIX);
		// 替换后缀为需要访问的后缀
		//className = className.substring(0, 1).toLowerCase() + className.substring(1);

		// 当前运行方法名
		//String methodName = stacks[2].getMethodName();
		if (params == null ) {
			try {
				Object obj = hessianServer.hessianInvoke(beanId, methodName, null, null);
				return obj;
			}    //捕获下层抛出的异常
			catch (CtfoAppException e) {
				throw e;
			}
			//捕获本层抛出的异常
			catch (Exception e) {
				throw new CtfoAppException(e);
			}
		} else {
			int length = params.length;
			Class[] classTypes = new Class[length];
			
			// 当前参数归属类
			
			for (int i = 0; i < params.length; i++) {
				if(params[i]!=null){
				 classTypes[i] = params[i].getClass();
				}else{
			     throw new CtfoAppException("hessian远程方法调用execute方法params参数不能为null",
			    		 CtfoExceptionLevel.systemError,"Hessian远程方法调用方法参数值不支持为null");
				}
			}
			try {
				System.out.println("请求的URL地址[" + hessianServer.toString() + "]:spring接口：" + beanId + "#" + methodName);
				Object obj = hessianServer.hessianInvoke(beanId, methodName, classTypes, params);
				return obj;
			}     //捕获下层抛出的异常
			catch (CtfoAppException e) {
				throw e;
			}
			//捕获本层抛出的异常
			catch (Exception e) {
				throw new CtfoAppException(e);
			}
		}
	}

}
