package com.ctfo.common.filter;

import java.io.IOException;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import com.ctfo.common.util.CookieUtil;
import com.ctfo.common.util.DesUtil;
import com.ctfo.common.util.OperatorInfoUtil;
import com.ctfo.common.util.StaticSession;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 过滤器<br>
 * 描述： 过滤器<br>
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
 * <td>2014-5-27</td>
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
public class CheckLoginFilter implements Filter {

	private static Logger logger = Logger.getLogger("OPEN_URL");

	/** 不用做权限判断的URL */
	private final static String[] eixt_url = { "login.do", "login.html", "rondamImage.do","index.html" };

	public void doFilter(ServletRequest arg0, ServletResponse arg1, FilterChain arg2) throws IOException, ServletException {

		HttpServletRequest request = (HttpServletRequest) arg0;
		HttpServletResponse response = (HttpServletResponse) arg1;
		String url = request.getRequestURI();
		if (isNeedValidateUrl(url)) {
			String[] objId = CookieUtil.getInstance().getOpIdByCookie(request, StaticSession.COOKIE_USERID);
			if (objId != null && !"null".equals(objId) && objId.length > 0) {
				try {
					String opId = DesUtil.getInstance().decryptStr(objId[0]); // 用户id
					if (OperatorInfoUtil.getUserIsLoginRedis(opId)) {
						long loginTime = Long.parseLong(objId[1]); // 取出session保存的用户登录时间
						long curreTime = System.currentTimeMillis(); // 当前时间
						if (curreTime - loginTime > 6480000) { // 累计登录满 1.8 * 60 * 60 *1000个小时的，更新redis中用户信息
							// 更新redis的用户有效时间
							OperatorInfoUtil.updateUserLoginRedisTime(opId);
							// 更新cookie里的登录时间
							String newValue = objId[0] + "_" + curreTime;// 存入cookie的内容
							CookieUtil.setCookie(request, response, StaticSession.COOKIE_USERID, newValue, 2 * 3600 * 1000);
						}
					} else {
						isAjaxRequest(request, response);
						return;
					}
				} catch (Exception e) {
					logger.error(e);
					isAjaxRequest(request, response);
					return;
				}
			} else {
				isAjaxRequest(request, response);
				return;
			}
		}
		arg2.doFilter(arg0, arg1);
	}

	public void destroy() {

	}

	public void init(FilterConfig arg0) throws ServletException {

	}

	/**
	 * 是否需要验证的请求
	 * 
	 * @param reqUrl
	 * @return
	 */
	protected boolean isNeedValidateUrl(String reqUrl) {
		boolean haveFind = true;
		if (reqUrl.indexOf(".jsp") >= 0) { // 如果是js/css/png/jpg等不验证
			for (int i = 0; i < eixt_url.length; i++) {
				if (reqUrl.indexOf(eixt_url[i]) >= 0) {
					haveFind = false;
					break;
				}
			}
		} else {
			haveFind = false;
		}
		return haveFind;
	}

	/**
	 * 判断请求是否ajax请求， 一般http请求的失效跳转
	 * 
	 * @param req
	 * @param res
	 */
	protected void isAjaxRequest(HttpServletRequest req, HttpServletResponse res) {
		if (req.getHeader("x-requested-with") != null && req.getHeader("x-requested-with").equalsIgnoreCase("XMLHttpRequest")) {
			res.setHeader("sessionTimeOut", "sessionTimeOut");
		} else {
			try {
				res.sendRedirect(req.getContextPath() + "/login.html");
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}
}
