/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.catalina;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Enumeration;
import java.util.List;
import java.util.Map;
import java.util.Set;

import javax.servlet.ServletContext;
import javax.servlet.http.HttpSession;
import javax.servlet.http.HttpSessionBindingEvent;
import javax.servlet.http.HttpSessionBindingListener;
import javax.servlet.http.HttpSessionContext;

import org.apache.commons.lang3.StringUtils;
import org.apache.log4j.Logger;
import org.storevm.toolkits.session.SessionManager;
import org.storevm.toolkits.session.zookeeper.DefaultZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.ZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.handler.GetDataHandler;
import org.storevm.toolkits.session.zookeeper.handler.GetNodeNames;
import org.storevm.toolkits.session.zookeeper.handler.PutDataHandler;
import org.storevm.toolkits.session.zookeeper.handler.RemoveDataHandler;
import org.storevm.toolkits.session.zookeeper.handler.RemoveNodeHandler;

/**
 * Tomcat容器下的HttpSession实现
 * @author  ocean
 * @version $Id: CatalinaDistributedSession.java, v 0.1 2010-12-31 下午05:23:45  ocean Exp $
 */
@SuppressWarnings("deprecation")
public class CatalinaDistributedSession implements HttpSession {
    /** log4j */
    private static final Logger LOGGER = Logger.getLogger(CatalinaDistributedSession.class);

    /**Session管理器*/
    private SessionManager      sessionManager;
    /**Session ID*/
    private String              id;
    /**Session创建时间*/
    private long                creationTm;
    /**Session最后一次访问时间*/
    private long                lastAccessedTm;
    /**Session的最大空闲时间间隔*/
    private int                 maxInactiveInterval;
    /**是否是新建Session*/
    private boolean             newSession;

    /** ZK客户端操作 */
    private ZooKeeperClient     client = DefaultZooKeeperClient.getInstance();

    /**
     * 构造方法
     * @param sessionManager
     */
    public CatalinaDistributedSession(SessionManager sessionManager) {
        this.sessionManager = sessionManager;
        this.creationTm = System.currentTimeMillis();
        this.lastAccessedTm = this.creationTm;
        this.newSession = true;
    }

    /**
     * 构造方法,指定ID
     * @param sessionManager
     * @param id
     */
    public CatalinaDistributedSession(SessionManager sessionManager, String id) {
        this(sessionManager);
        this.id = id;
    }

    @Override
    public long getCreationTime() {
        return creationTm;
    }

    @Override
    public String getId() {
        return id;
    }

    @Override
    public long getLastAccessedTime() {
        return lastAccessedTm;
    }

    @Override
    public ServletContext getServletContext() {
        return sessionManager.getServletContext();
    }

    @Override
    public void setMaxInactiveInterval(int interval) {
        this.maxInactiveInterval = interval;
    }

    @Override
    public int getMaxInactiveInterval() {
        return maxInactiveInterval;
    }

    @Override
    @Deprecated
    public HttpSessionContext getSessionContext() {
        return null;
    }

    @Override
    public Object getAttribute(String name) {
        access();
        //获取session ID
        String id = getId();
        if (StringUtils.isNotBlank(id)) {
            //返回Session节点下的数据
            try {
                return client.execute(new GetDataHandler(id, name));
            } catch (Exception ex) {
                LOGGER.error("调用getAttribute方法时发生异常，", ex);
            }
        }
        return null;
    }

    @Override
    public Object getValue(String name) {
        return getAttribute(name);
    }

    @Override
    public Enumeration<String> getAttributeNames() {
        access();
        //获取session ID
        String id = getId();
        if (StringUtils.isNotBlank(id)) {
            //返回Session节点下的数据名字
            try {
                List<String> names = client.execute(new GetNodeNames(id));
                if (names != null) {
                    return Collections.enumeration(names);
                }
            } catch (Exception ex) {
                LOGGER.error("调用getAttributeNames方法时发生异常，", ex);
            }

        }
        return null;
    }

    @Override
    public String[] getValueNames() {
        List<String> names = new ArrayList<String>();
        Enumeration<String> n = getAttributeNames();
        while (n.hasMoreElements()) {
            names.add((String) n.nextElement());
        }
        return names.toArray(new String[] {});
    }

    @Override
    public void setAttribute(String name, Object value) {
        //没有实现序列化接口的直接返回
        if (!(value instanceof Serializable)) {
            LOGGER.warn("对象[" + value + "]没有实现Serializable接口，无法保存到分布式Session中");
            return;
        }
        access();
        //获取session ID
        String id = getId();
        if (StringUtils.isNotBlank(id)) {
            //将数据添加到ZooKeeper服务器上
            try {
                value = client.execute(new PutDataHandler(id, name, (Serializable) value));
            } catch (Exception ex) {
                LOGGER.error("调用setAttribute方法时发生异常，", ex);
            }
        }
        //处理Session的监听器
        fireHttpSessionBindEvent(name, value);
    }

    @Override
    public void putValue(String name, Object value) {
        setAttribute(name, value);
    }

    @Override
    public void removeAttribute(String name) {
        access();
        Object value = null;
        //获取session ID
        String id = getId();
        if (StringUtils.isNotBlank(id)) {
            //删除Session节点下的数据
            try {
                value = client.execute(new RemoveDataHandler(id, name));
            } catch (Exception ex) {
                LOGGER.error("调用removeAttribute方法时发生异常，", ex);
            }
        }
        //处理Session的监听器
        fireHttpSessionUnbindEvent(name, value);
    }

    @Override
    public void removeValue(String name) {
        removeAttribute(name);
    }

    @Override
    public void invalidate() {
        //获取session ID
        String id = getId();
        if (StringUtils.isNotBlank(id)) {
            //删除Session节点
            try {
                Map<String, Object> sessionMap = client.execute(new RemoveNodeHandler(id));
                if (sessionMap != null) {
                    Set<String> keys = sessionMap.keySet();
                    for (String key : keys) {
                        Object value = sessionMap.get(key);
                        fireHttpSessionUnbindEvent(key, value);
                    }
                }
            } catch (Exception ex) {
                LOGGER.error("调用invalidate方法时发生异常，", ex);
            }
        }
        //删除本地容器中的Session对象
        sessionManager.removeHttpSession(this);
    }

    @Override
    public boolean isNew() {
        return newSession;
    }

    /**
     * 被访问
     */
    public void access() {
        this.newSession = false;
        this.lastAccessedTm = System.currentTimeMillis();
    }

    /**
     * 触发Session的事件
     * @param value
     */
    protected void fireHttpSessionBindEvent(String name, Object value) {
        //处理Session的监听器
        if (value != null && value instanceof HttpSessionBindingListener) {
            HttpSessionBindingEvent event = new HttpSessionBindingEvent(this, name, value);
            ((HttpSessionBindingListener) value).valueBound(event);
        }
    }

    /**
     * 触发Session的事件
     * @param value
     */
    protected void fireHttpSessionUnbindEvent(String name, Object value) {
        //处理Session的监听器
        if (value != null && value instanceof HttpSessionBindingListener) {
            HttpSessionBindingEvent event = new HttpSessionBindingEvent(this, name, value);
            ((HttpSessionBindingListener) value).valueUnbound(event);
        }
    }
}
