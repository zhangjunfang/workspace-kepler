/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.hessianproxy.remoteservice.impl;

import com.ctfo.hessian.service.HessianServer;
import com.ctfo.hessianproxy.hessian.HessianRemoteAbstract;
import com.ctfo.hessianproxy.remoteservice.RemoteManager;
import com.ctfo.local.exception.CtfoAppException;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： <br>
 * 功能： 业务支撑服务远程hession接口实现类<br>
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
public class RemoteManagerImpl extends HessianRemoteAbstract implements RemoteManager {

	/**
	 * hessian 远程调用对象接口
	 */
	private HessianServer hessianServer;

	/**
	 * Manager方法公共类远程调用Hessian的对象 此方法必须在相应的Service中执行。
	 * 
	 * 这会涉及到获取当前方法名及类名通过规则去后台匹配
	 * 
	 * @param params
	 *            [] 传递的参数值
	 * @return Object对象
	 */
	@Override
	public Object execute(String beanId, String methodName, Object... params) throws CtfoAppException {
		try {
			return this.execute(hessianServer, beanId, methodName, params);
		}
		// 捕获下层抛出的异常
		catch (CtfoAppException e) {
			throw e;
		}
		// 捕获本层抛出的异常
		catch (Exception e) {
			throw new CtfoAppException(e);
		}
	}

	public void setHessianServer(HessianServer hessianServer) {
		this.hessianServer = hessianServer;
	}

}
