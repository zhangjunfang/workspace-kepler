/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.hessian.remote;

import com.ctfo.local.exception.CtfoAppException;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： monitorser
 * <br>
 * 功能：
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交兴路信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2011-10-10</td><td>yangjian</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author yangjian
 * @since JDK1.6
 */
public interface HessianRemoteManager {
	
	/**
	 * Manager方法公共类远程调用Hessian的对象 此方法必须在相应的Service中执行。
	 * 
	 * 这会涉及到获取当前方法名及类名通过规则去后台匹配
	 * 
	 * @param params
	 *            [] 传递的参数值
	 * @return Object对象
	 */
	
	public Object execute(String beanId,String methodName, Object... params) throws CtfoAppException ;

}
