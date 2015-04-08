/**
 * 
 */
package org.storevm.toolkits.session.jetty;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSessionBindingEvent;
import javax.servlet.http.HttpSessionBindingListener;

import org.apache.commons.lang3.StringUtils;
import org.apache.log4j.Logger;
import org.mortbay.jetty.servlet.AbstractSessionManager;
import org.mortbay.jetty.servlet.AbstractSessionManager.Session;
import org.storevm.toolkits.session.SessionManager;
import org.storevm.toolkits.session.zookeeper.DefaultZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.ZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.handler.GetDataHandler;
import org.storevm.toolkits.session.zookeeper.handler.PutDataHandler;
import org.storevm.toolkits.session.zookeeper.handler.RemoveDataHandler;
import org.storevm.toolkits.session.zookeeper.handler.RemoveNodeHandler;

/**
 * Jetty容器下的HttpSession接口的实现
 * 
 * @author  ocean
 * @version $Id: JettyDistributedSession.java, v 0.1 2010-12-29 下午10:26:09  ocean Exp $
 */
public class JettyDistributedSession extends Session {
    private static final long   serialVersionUID = -6089477971984554624L;

    /** log4j */
    private static final Logger LOGGER           = Logger.getLogger(JettyDistributedSession.class);

    /** Session管理器 */
    private SessionManager      sessionManager;

    /** ZK客户端操作 */
    private ZooKeeperClient     client           = DefaultZooKeeperClient.getInstance();

    /**
     * 构造方法
     * 
     * @param sessionManager
     * @param request
     */
    public JettyDistributedSession(AbstractSessionManager sessionManager,
                                   HttpServletRequest request, SessionManager sm) {
        sessionManager.super(request);
        this.sessionManager = sm;
    }

    /**
     * 构造方法
     * @param arg0
     * @param arg1
     */
    public JettyDistributedSession(AbstractSessionManager sessionManager, long create, String id,
                                   SessionManager sm) {
        sessionManager.super(create, id);
        this.sessionManager = sm;
    }

    @SuppressWarnings("rawtypes")
	@Override
    protected Map newAttributeMap() {
        return new HashMap(3);
    }

    /* (non-Javadoc)
     * @see org.mortbay.jetty.servlet.AbstractSessionManager.Session#getAttribute(java.lang.String)
     */
    @Override
    public Object getAttribute(String name) {
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

    /* (non-Javadoc)
     * @see org.mortbay.jetty.servlet.AbstractSessionManager.Session#removeAttribute(java.lang.String)
     */
    @Override
    public void removeAttribute(String name) {
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

    /**
     * 
     * @see org.mortbay.jetty.servlet.AbstractSessionManager.Session#setAttribute(java.lang.String, java.lang.Object)
     */
    @Override
    public void setAttribute(String name, Object value) {
        //没有实现序列化接口的直接返回
        if (!(value instanceof Serializable)) {
            LOGGER.warn("对象[" + value + "]没有实现Serializable接口，无法保存到分布式Session中");
            return;
        }
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

    /** 
     * @see org.mortbay.jetty.servlet.AbstractSessionManager.Session#invalidate()
     */
    @Override
    public void invalidate() throws IllegalStateException {
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
        if (sessionManager != null) {
            sessionManager.removeHttpSession(this);
        }
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
