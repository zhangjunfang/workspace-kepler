/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session;

import javax.servlet.ServletContext;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.storevm.toolkits.component.LifeCycle;

/**
 * HttpSession生命周期管理器接口
 * @author  ocean
 * @version $Id: SessionManager.java, v 0.1 2010-12-29 下午08:57:26  ocean Exp $
 */
public interface SessionManager extends LifeCycle {
    /**Cookie的过期时间，默认1年*/
    public static final int COOKIE_EXPIRY = 365 * 24 * 60 * 60;

    /**
     * 返回指定ID的HttpSession对象
     * @param id Session ID
     * @param request HTTP请求
     * @return
     */
    public HttpSession getHttpSession(String id, HttpServletRequest request);

    /**
     * 创建一个新的HttpSession对象
     * @param request HTTP请求
     * @return
     */
    public HttpSession newHttpSession(HttpServletRequest request);

    /**
     * 获取请求对象中的Session对象的ID，一般是从Cookie中获取
     * @param request HTTP请求
     * @return
     */
    public String getRequestSessionId(HttpServletRequest request);

    /**
     * 将一个HttpSession对象放入管理容器中
     * @param session HTTP Session对象
     * @param request HTTP请求
     */
    public void addHttpSession(HttpSession session);

    /**
     * 删除Session
     * @param session
     */
    public void removeHttpSession(HttpSession session);

    /**
     * 返回一个唯一的Session ID
     * @return 
     */
    public String getNewSessionId(HttpServletRequest request);

    /**
     * 返回Servlet上下文
     * @return
     */
    public ServletContext getServletContext();

    /**
     * 设置Servlet上下文
     * @param sc
     */
    public void setServletContext(ServletContext sc);

    /**
     * 返回HttpServletResponse对象
     * @return
     */
    public HttpServletResponse getResponse();

    /**
     * 设置HttpServletResponse对象
     * @param response
     */
    public void setHttpServletResponse(HttpServletResponse response);
}
