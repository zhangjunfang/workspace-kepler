/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session;

import java.util.Map;
import java.util.concurrent.Callable;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

import javax.servlet.ServletContext;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.math.NumberUtils;
import org.apache.log4j.Logger;
import org.storevm.toolkits.session.config.Configuration;
import org.storevm.toolkits.session.helper.CookieHelper;
import org.storevm.toolkits.session.zookeeper.DefaultZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.ZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.handler.TimeoutHandler;

/**
 * Session管理器抽象实现
 * @author  ocean
 * @version $Id: AbstractSessionManager.java, v 0.1 2010-12-29 下午09:49:18  ocean Exp $
 */
public abstract class DefaultSessionManager implements SessionManager {
    protected static final Logger      LOGGER  = Logger.getLogger(DefaultSessionManager.class);
    /**本地的session容器*/
    protected Map<String, HttpSession> sessions;
    /** 定时任务执行器 */
    protected ExecutorService          executor;
    /** 参数配置 */
    protected Configuration            config;
    private boolean                    started = false;
    private boolean                    stopped = false;
    /**Session ID管理器*/
    private SessionIdManager           sessionIdManager;
    private ServletContext             sc;
    private HttpServletResponse        response;
    /** ZK客户端操作 */
    protected ZooKeeperClient          client  = DefaultZooKeeperClient.getInstance();

    /**
     * 构造方法
     * @param sc
     */
    public DefaultSessionManager(ServletContext sc) {
        this.sc = sc;
        config = (Configuration) sc.getAttribute(Configuration.CFG_NAME);
    }

    /**
     *
     * @see org.storevm.toolkits.component.LifeCycle#start()
     */
    @Override
    public void start() throws Exception {
        if (!isStarted()) {
            if (sessions == null) {
                sessions = new ConcurrentHashMap<String, HttpSession>();
            }
            if (sessionIdManager == null) {
                sessionIdManager = new DefaultSessionIdManager();
                sessionIdManager.start();
            }
            // 启动定时任务执行器
            int poolSize = NumberUtils.toInt(config.getString(Configuration.POOLSIZE));
            executor = Executors.newFixedThreadPool(poolSize);
            started = true;
        }
    }

    /**
     *
     * @see org.storevm.toolkits.component.LifeCycle#stop()
     */
    @Override
    public void stop() throws Exception {
        if (!isStopped()) {
            if (sessions != null) {
                //如果存在本地Session，则全部失效掉
                for (HttpSession s : sessions.values()) {
                    s.invalidate();
                }
                sessions.clear();
            }
            sessions = null;
            if (sessionIdManager != null) {
                sessionIdManager.stop();
            }
            // 关闭定时任务
            executor.shutdown(); // Disable new tasks from being submitted
            try {
                // Wait a while for existing tasks to terminate
                if (!executor.awaitTermination(60, TimeUnit.MILLISECONDS)) {
                    executor.shutdownNow(); // Cancel currently executing tasks
                    // Wait a while for tasks to respond to being cancelled
                    if (!executor.awaitTermination(60, TimeUnit.MILLISECONDS))
                        System.err.println("Pool did not terminate");
                }
            } catch (InterruptedException ie) {
                // (Re-)Cancel if current thread also interrupted
                executor.shutdownNow();
                // Preserve interrupt status
                Thread.currentThread().interrupt();
            } finally {
                stopped = true;
            }
        }
    }

    /**
     *
     * @see org.storevm.toolkits.component.LifeCycle#isStarted()
     */
    @Override
    public boolean isStarted() {
        return started;
    }

    /**
     *
     * @see org.storevm.toolkits.component.LifeCycle#isStopped()
     */
    @Override
    public boolean isStopped() {
        return stopped;
    }

    /** 
     * @see org.storevm.toolkits.session.SessionManager#getServletContext()
     */
    @Override
    public ServletContext getServletContext() {
        return sc;
    }

    /** 
     * @see org.storevm.toolkits.session.SessionManager#setServletContext(javax.servlet.ServletContext)
     */
    @Override
    public void setServletContext(ServletContext sc) {
        this.sc = sc;
    }

    /** 
     * @see org.storevm.toolkits.session.SessionManager#getResponse()
     */
    @Override
    public HttpServletResponse getResponse() {
        return response;
    }

    /** 
     * @see org.storevm.toolkits.session.SessionManager#setHttpServletResponse(javax.servlet.http.HttpServletResponse)
     */
    @Override
    public void setHttpServletResponse(HttpServletResponse response) {
        this.response = response;
    }

    /**
     * 
     * @see org.storevm.toolkits.session.SessionManager#getNewSessionId(javax.servlet.http.HttpServletRequest)
     */
    @Override
    public String getNewSessionId(HttpServletRequest request) {
        if (sessionIdManager != null) {
            return sessionIdManager.newSessionId(request, System.currentTimeMillis());
        }
        return null;
    }

    /**
     * 查找指定的Cookie
     * 
     * @param request
     * @return
     */
    @Override
    public String getRequestSessionId(HttpServletRequest request) {
        return CookieHelper.findSessionId(request);
    }

    /**
     *
     * @see org.storevm.toolkits.session.SessionManager#removeHttpSession(javax.servlet.http.HttpSession)
     */
    @Override
    public void removeHttpSession(HttpSession session) {
        if (session != null) {
            String id = session.getId();
            if (StringUtils.isNotBlank(id)) {
                sessions.remove(id);
            }
        }
    }

    /**
     * 加入一个受管理的Session对象
     * 
     * @param session
     * @param request
     */
    @Override
    public void addHttpSession(HttpSession session) {
        if (session == null) {
            return;
        }
        String id = session.getId();
        if (!sessions.containsKey(id)) {
            sessions.put(id, session);
            //启动一个线程，用于轮询session的超时
            executor.submit(new checkSessionTimeoutTask(session));
        }
    }

    /**
     * 检查Session是否超时的定时任务
     * 
     * @author  ocean
     * @version $Id: DefaultSessionManager.java, v 0.1 2011-1-9 下午04:09:21  ocean Exp $
     */
    protected class checkSessionTimeoutTask implements Callable<Boolean> {
        private HttpSession       session;
        private static final long SLEEP_TIMEOUT = 10L;

        /**
         * 构造方法
         * @param session
         */
        public checkSessionTimeoutTask(HttpSession session) {
            this.session = session;
        }

        @Override
        public Boolean call() throws Exception {
            if (session == null) {
                return false;
            }
            boolean running = true;
            while (running) {
                try {
                    Boolean timeout = client.execute(new TimeoutHandler(session.getId()));
                    if (timeout) {
                        session.invalidate();
                        //如果超时成功则退出轮询
                        break;
                    }
                    //sleep
                    TimeUnit.SECONDS.sleep(SLEEP_TIMEOUT);
                } catch (Exception ex) {
                    LOGGER.error("Session超时定时任务发生异常，", ex);
                }
            }
            return true;
        }
    }
}
