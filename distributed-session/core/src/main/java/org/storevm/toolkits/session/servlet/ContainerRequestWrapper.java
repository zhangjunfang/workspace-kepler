/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.servlet;

import java.security.Principal;
import java.util.Enumeration;

import javax.servlet.ServletRequest;
import javax.servlet.ServletRequestWrapper;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.apache.log4j.Logger;
import org.storevm.toolkits.session.SessionManager;

/**
 * HttpServletRequest的包装器实现
 * @author  ocean
 * @version $Id: RequestWrapper.java, v 0.1 2010-12-31 下午04:22:15  ocean Exp $
 */
public class ContainerRequestWrapper extends ServletRequestWrapper implements HttpServletRequest {
    protected Logger       log = Logger.getLogger(getClass());
    private SessionManager sessionManager;
    private HttpSession    session;

    /**
     * 构造方法
     * @param request
     */
    public ContainerRequestWrapper(ServletRequest request, SessionManager sessionManager) {
        super(request);
        this.sessionManager = sessionManager;
    }

    /**
     * 
     * @return
     */
    private HttpServletRequest getHttpServletRequest() {
        return (HttpServletRequest) super.getRequest();
    }

    /** 
     * @see javax.servlet.http.HttpServletRequest#getSession(boolean)
     */
    @Override
    public HttpSession getSession(boolean create) {
        HttpServletRequest request = (HttpServletRequest) getRequest();
        //检查Session管理器
        if (sessionManager == null && create) {
            throw new IllegalStateException("No SessionHandler or SessionManager");
        }
        if (session != null && sessionManager != null) {
            return session;
        }

        session = null;

        //从客户端cookie中查找Session ID
        String id = sessionManager.getRequestSessionId(request);
        log.debug("获取客户端的Session ID:[" + id + "]");
        if (id != null && sessionManager != null) {
            //如果存在，则先从管理器中取
            session = sessionManager.getHttpSession(id, request);
            if (session == null && !create) {
                return null;
            }
        }
        //否则实例化一个新的Session对象
        if (session == null && sessionManager != null && create) {
            session = sessionManager.newHttpSession(request);
        }
        return session;
    }

    @Override
    public String getAuthType() {
        return getHttpServletRequest().getAuthType();
    }

    @Override
    public Cookie[] getCookies() {
        return getHttpServletRequest().getCookies();
    }

    @Override
    public long getDateHeader(String name) {
        return getHttpServletRequest().getDateHeader(name);
    }

    @Override
    public String getHeader(String name) {
        return getHttpServletRequest().getHeader(name);
    }
    @SuppressWarnings("rawtypes")
	@Override
    public Enumeration getHeaders(String name) {
        return getHttpServletRequest().getHeaders(name);
    }
    @SuppressWarnings("rawtypes")
    @Override
    public Enumeration getHeaderNames() {
        return getHttpServletRequest().getHeaderNames();
    }

    @Override
    public int getIntHeader(String name) {
        return getHttpServletRequest().getIntHeader(name);
    }

    @Override
    public String getMethod() {
        return getHttpServletRequest().getMethod();
    }

    @Override
    public String getPathInfo() {
        return getHttpServletRequest().getPathInfo();
    }

    @Override
    public String getPathTranslated() {
        return getHttpServletRequest().getPathTranslated();
    }

    @Override
    public String getContextPath() {
        return getHttpServletRequest().getContextPath();
    }

    @Override
    public String getQueryString() {
        return getHttpServletRequest().getQueryString();
    }

    @Override
    public String getRemoteUser() {
        return getHttpServletRequest().getRemoteUser();
    }

    @Override
    public boolean isUserInRole(String role) {
        return getHttpServletRequest().isUserInRole(role);
    }

    @Override
    public Principal getUserPrincipal() {
        return getHttpServletRequest().getUserPrincipal();
    }

    @Override
    public String getRequestedSessionId() {
        return getHttpServletRequest().getRequestedSessionId();
    }

    @Override
    public String getRequestURI() {
        return getHttpServletRequest().getRequestURI();
    }

    @Override
    public StringBuffer getRequestURL() {
        return getHttpServletRequest().getRequestURL();
    }

    @Override
    public String getServletPath() {
        return getHttpServletRequest().getServletPath();
    }

    @Override
    public HttpSession getSession() {
        return getSession(true);
    }

    @Override
    public boolean isRequestedSessionIdValid() {
        return getHttpServletRequest().isRequestedSessionIdValid();
    }

    @Override
    public boolean isRequestedSessionIdFromCookie() {
        return getHttpServletRequest().isRequestedSessionIdFromCookie();
    }

    @Override
    public boolean isRequestedSessionIdFromURL() {
        return getHttpServletRequest().isRequestedSessionIdFromURL();
    }

    @SuppressWarnings("deprecation")
    @Override
    public boolean isRequestedSessionIdFromUrl() {
        return getHttpServletRequest().isRequestedSessionIdFromUrl();
    }
}
