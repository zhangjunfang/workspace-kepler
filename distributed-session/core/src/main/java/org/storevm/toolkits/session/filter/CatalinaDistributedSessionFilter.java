/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.filter;

import java.io.IOException;

import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletResponse;

import org.storevm.toolkits.session.catalina.CatalinaDistributedSessionManager;

/**
 * 用于Tomcat容器的分布式Session的过滤器实现
 * @author  ocean
 * @version $Id: CatalinaDistributedSessionFilter.java, v 0.1 2010-12-31 下午03:07:55  ocean Exp $
 */
public class CatalinaDistributedSessionFilter extends DistributedSessionFilter {
    @Override
    public void init(FilterConfig filterConfig) throws ServletException {
        super.init(filterConfig);
        // 实例化Tomcat容器下的Session管理器
        this.sessionManager = new CatalinaDistributedSessionManager(
            filterConfig.getServletContext());
        try {
            sessionManager.start(); // 启动初始化
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("DistributedSessionFilter.init completed.");
            }
        } catch (Exception ex) {
            LOGGER.error("过滤器初始化失败，", ex);
        }
    }

    /** 
     * @see javax.servlet.Filter#doFilter(javax.servlet.ServletRequest, javax.servlet.ServletResponse, javax.servlet.FilterChain)
     */
    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
                                                                                             throws IOException,
                                                                                             ServletException {
        //设置Response
        sessionManager.setHttpServletResponse((HttpServletResponse) response);
        super.doFilter(request, response, chain);
    }

}
