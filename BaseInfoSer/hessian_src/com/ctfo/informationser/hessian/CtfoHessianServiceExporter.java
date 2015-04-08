/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.hessian;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;
import org.springframework.remoting.caucho.HessianServiceExporter;


/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： BsSer <br>
 * 功能： hessian后台服务总代理<br>
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
 * <td>2011-9-21</td>
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
public class CtfoHessianServiceExporter extends HessianServiceExporter {

	public static final String AUTH = "ctfo";

	protected static Logger logger = Logger.getLogger(CtfoHessianServiceExporter.class);

	public static ThreadLocal<String> mythred = new ThreadLocal<String>();

	@Override
	public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
//		String auth = request.getHeader("auth");
//		String clientIp = request.getRemoteAddr();
//		logger.debug("用户登录名称：" + auth + ":是否在列表中" + StaticSession.sessionNames);
		/** // 默认登录认证参数|| 用户已经登录
		if (AUTH.equalsIgnoreCase(auth) || StaticSession.sessionNames.indexOf(auth + ":" + clientIp) > -1) {
			mythred.set(auth + ":" + clientIp);
			super.handleRequest(request, response);
			mythred.remove();
			return;
		} else {
			// 记录异常日志
			logger.debug("用户未登录");
			response.sendError(-111111, "用户未登录");
			return;
		}
*/
		super.handleRequest(request, response);
	}
}
