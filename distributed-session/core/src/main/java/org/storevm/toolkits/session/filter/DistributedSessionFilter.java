/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.filter;

import java.io.IOException;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;

import org.apache.log4j.Logger;
import org.storevm.toolkits.session.SessionManager;
import org.storevm.toolkits.session.config.Configuration;
import org.storevm.toolkits.session.pool.ZookeeperPoolManager;
import org.storevm.toolkits.session.servlet.ContainerRequestWrapper;
import org.storevm.toolkits.session.zookeeper.DefaultZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.ZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.handler.CreateGroupNodeHandler;

/**
 * 分布式Session过滤器抽象实现类，用于实现公有方法
 * @author  ocean
 * @version $Id: DistributedSessionFilter.java, v 0.1 2010-12-29 下午09:12:46  ocean Exp $
 */
public abstract class DistributedSessionFilter implements Filter {
    /** log4j */
    protected static final Logger LOGGER = Logger.getLogger(DistributedSessionFilter.class);

    /**Session管理器*/
    protected SessionManager      sessionManager;

    /** ZK客户端操作 */
    protected ZooKeeperClient     client = DefaultZooKeeperClient.getInstance();

    /**
     * 初始化
     * @see javax.servlet.Filter#init(javax.servlet.FilterConfig)
     */
    @Override
    public void init(FilterConfig filterConfig) throws ServletException {
        //初始化系统属性配置
        Configuration conf = Configuration.getInstance();
        if (LOGGER.isInfoEnabled()) {
            LOGGER.info("1. 读取系统配置属性成功，" + conf);
        }

        //将配置属性放入上下文中
        ServletContext sc = filterConfig.getServletContext();
        sc.setAttribute(Configuration.CFG_NAME, conf);

        //初始化ZK客户端对象池
        ZookeeperPoolManager.getInstance().init(conf);
        if (LOGGER.isInfoEnabled()) {
            LOGGER.info("2. 初始化ZK客户端对象池完成");
        }

        try {
            client.execute(new CreateGroupNodeHandler());
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("3. 创建SESSIONS组节点完成");
            }
        } catch (Exception ex) {
            LOGGER.error("创建组节点时发生异常，", ex);
        }
    }

    /**
     *
     * @see javax.servlet.Filter#doFilter(javax.servlet.ServletRequest, javax.servlet.ServletResponse, javax.servlet.FilterChain)
     */
    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
                                                                                             throws IOException,
                                                                                             ServletException {
        //Request的包装器，用于重写HttpServletRequest的getSession方法
        HttpServletRequest req = new ContainerRequestWrapper(request, sessionManager);
        chain.doFilter(req, response);
    }

    /**
     * 销毁
     * @see javax.servlet.Filter#destroy()
     */
    @Override
    public void destroy() {
        //关闭Session管理器
        if (sessionManager != null) {
            try {
                sessionManager.stop();
            } catch (Exception ex) {
                LOGGER.error("关闭Session管理器时发生异常，", ex);
            }
        }

        //关闭ZK实例池
        ZookeeperPoolManager.getInstance().close();

        if (LOGGER.isInfoEnabled()) {
            LOGGER.info("DistributedSessionFilter.destroy completed.");
        }
    }

}
