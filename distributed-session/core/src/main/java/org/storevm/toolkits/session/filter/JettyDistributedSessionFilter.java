/**
 * 
 */
package org.storevm.toolkits.session.filter;

import javax.servlet.FilterConfig;
import javax.servlet.ServletException;

import org.apache.log4j.Logger;
import org.storevm.toolkits.session.jetty.JettyDistributedSessionManager;

/**
 * 用于Jetty容器的分布式Session的过滤器实现
 * 
 * @author  ocean
 * @version $Id: JettyDistributedSessionFilter.java, v 0.1 2010-12-29 下午09:10:29
 *           ocean Exp $
 */
public class JettyDistributedSessionFilter extends DistributedSessionFilter {
    private Logger log = Logger.getLogger(getClass());

    @Override
    public void init(FilterConfig filterConfig) throws ServletException {
        super.init(filterConfig);
        // 实例化Jetty容器下的Session管理器
        sessionManager = new JettyDistributedSessionManager(filterConfig.getServletContext());
        try {
            sessionManager.start(); // 启动初始化
            log.debug("DistributedSessionFilter.init completed.");
        } catch (Exception e) {
            log.error(e);
        }
    }
}
