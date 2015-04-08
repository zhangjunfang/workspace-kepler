package com.ctfo.common.util;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 操作Cookie的工具类<br>
 * 描述： 操作Cookie的工具类<br>
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
public class CookieUtil {

	private static CookieUtil instance = new CookieUtil();

	private CookieUtil() {

	}

	public static CookieUtil getInstance() {
		return instance;
	}

	/**
	 * 根据键获取cookie的值
	 * 
	 * @param request
	 * @param name
	 * @return
	 */
	public Cookie getCookie(HttpServletRequest request, String name) {
		Cookie cookies[] = request.getCookies();
		if (cookies == null || name == null || name.length() == 0) {
			return null;
		}
		for (int i = 0; i < cookies.length; i++) {
			if (name.equals(cookies[i].getName())) {
				return cookies[i];
			}
		}
		return null;
	}

	public String[] getOpIdByCookie(HttpServletRequest request, String name) {
		Cookie cookie = this.getCookie(request, name);
		if (cookie != null && cookie.getValue() != null) {
			String values = cookie.getValue();
			String[] value = values.split("_");
			return value;
		}
		return null;
	}

	/**
	 * 删除cookie
	 * 
	 * @param request
	 * @param response
	 * @param cookie
	 */
	public void deleteCookie(HttpServletRequest request, HttpServletResponse response, Cookie cookie) {
		if (cookie != null) {
			cookie.setPath(getPath(request));
			cookie.setValue("");
			cookie.setMaxAge(0);
			response.addCookie(cookie);
		}
	}

	/**
	 * 设置cookie
	 * 
	 * @param request
	 * @param response
	 * @param name
	 * @param value
	 */
	public void setCookie(HttpServletRequest request, HttpServletResponse response, String name, String value) {
		setCookie(request, response, name, value, 0x278d00);
	}

	/**
	 * 设置cookie的值
	 * 
	 * @param request
	 * @param response
	 * @param name
	 * @param value
	 * @param maxAge
	 */
	public static void setCookie(HttpServletRequest request, HttpServletResponse response, String name, String value, int maxAge) {
		Cookie cookie = new Cookie(name, null == value ? "" : value);
		// cookie.setMaxAge(maxAge);
		cookie.setPath(getPath(request));
		response.addCookie(cookie);
	}

	/**
	 * 设置cookie的值
	 * 
	 * @param request
	 * @param response
	 * @param name
	 * @param value
	 * @param maxAge
	 */
	public static void setCookieWithPath(HttpServletRequest request, HttpServletResponse response, String name, String value, int maxAge, String path) {
		Cookie cookie = new Cookie(name, null == value ? "" : value);
		// cookie.setMaxAge(maxAge);
		cookie.setPath(path);
		response.addCookie(cookie);
	}

	/**
	 * 
	 * @param request
	 * @return
	 */
	private static String getPath(HttpServletRequest request) {
		String path = request.getContextPath();
		return (path == null || path.length() == 0) ? "/" : path;
	}

}
