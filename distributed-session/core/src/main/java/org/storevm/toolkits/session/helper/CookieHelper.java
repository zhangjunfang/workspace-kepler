/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.helper;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

/**
 * 操作Cookie的助手类
 * @author  ocean
 * @version $Id: CookieHelper.java, v 0.1 2010-12-29 下午09:03:39  ocean Exp $
 */
public class CookieHelper {
    private static final String DISTRIBUTED_SESSION_ID = "STOREVMJSESSIONID";
    protected static Logger     log                    = Logger.getLogger(CookieHelper.class);

    /**
     * 将Session ID写到客户端的Cookie中
     * @param id Session ID
     * @param response HTTP响应
     * @return
     */
    public static Cookie writeSessionIdToNewCookie(String id, HttpServletResponse response,
                                                   int expiry) {
        Cookie cookie = new Cookie(DISTRIBUTED_SESSION_ID, id);
        cookie.setMaxAge(expiry);
        response.addCookie(cookie);
        return cookie;
    }

    /**
     * 将Session ID写到客户端的Cookie中
     * @param id Session ID
     * @param response HTTP响应
     * @return
     */
    public static Cookie writeSessionIdToCookie(String id, HttpServletRequest request,
                                                HttpServletResponse response, int expiry) {
        //先查找
        Cookie cookie = findCookie(DISTRIBUTED_SESSION_ID, request);
        //如果不存在，则新建一个
        if (cookie == null) {
            return writeSessionIdToNewCookie(id, response, expiry);
        } else {
            //直接改写cookie的值
            cookie.setValue(id);
            cookie.setMaxAge(expiry);
            response.addCookie(cookie);
        }
        return cookie;
    }

    /**
     * 查询指定名称的Cookie值
     * @param name cookie名称
     * @param request HTTP请求
     * @return
     */
    public static String findCookieValue(String name, HttpServletRequest request) {
        Cookie cookie = findCookie(name, request);
        if (cookie != null) {
            return cookie.getValue();
        }
        return null;
    }

    /**
     * 查询指定名称的Cookie
     * @param name cookie名称
     * @param request HTTP请求
     * @return
     */
    public static Cookie findCookie(String name, HttpServletRequest request) {
        Cookie[] cookies = request.getCookies();
        if (cookies == null) {
            return null;
        }
        // 迭代查找
        for (int i = 0, n = cookies.length; i < n; i++) {
            if (cookies[i].getName().equalsIgnoreCase(name)) {
                return cookies[i];
            }
        }
        return null;
    }

    /**
     * 在Cookie中查找Session ID
     * @param request HTTP请求
     * @return
     */
    public static String findSessionId(HttpServletRequest request) {
        return findCookieValue(DISTRIBUTED_SESSION_ID, request);
    }
}
