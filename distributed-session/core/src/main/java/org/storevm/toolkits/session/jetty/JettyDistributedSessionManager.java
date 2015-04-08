/**
 * 
 */
package org.storevm.toolkits.session.jetty;

import javax.servlet.ServletContext;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.apache.commons.lang3.math.NumberUtils;
import org.mortbay.jetty.Request;
import org.mortbay.jetty.servlet.AbstractSessionManager;
import org.mortbay.jetty.servlet.AbstractSessionManager.Session;
import org.storevm.toolkits.session.DefaultSessionManager;
import org.storevm.toolkits.session.config.Configuration;
import org.storevm.toolkits.session.helper.CookieHelper;
import org.storevm.toolkits.session.metadata.SessionMetaData;
import org.storevm.toolkits.session.zookeeper.handler.CreateNodeHandler;
import org.storevm.toolkits.session.zookeeper.handler.RemoveNodeHandler;
import org.storevm.toolkits.session.zookeeper.handler.UpdateMetadataHandler;

/**
 * Jetty容器下的Session管理器
 * 
 * @author  ocean
 * @version $Id: JettyDistributedSessionManager.java, v 0.1 2010-12-29 下午09:48:13  ocean Exp $
 */
public class JettyDistributedSessionManager extends DefaultSessionManager {
    /**
     * 构造方法
     * @param config
     */
    public JettyDistributedSessionManager(ServletContext sc) {
        super(sc);
    }

    /**
     * 获取容器中的session
     * 
     * @param id
     * @return
     */
    @Override
    public HttpSession getHttpSession(String id, HttpServletRequest request) {
        //类型检查
        if (!(request instanceof Request)) {
            LOGGER.warn("不是Jetty容器下的Request对象");
            return null;
        }
        //将HttpServletRequest转换成Jetty容器的Request类型
        Request req = (Request) request;
        HttpSession session = sessions.get(id);
        //ZooKeeper服务器上查找指定节点是否有效，并更新节点元数据
        Boolean valid = Boolean.FALSE;
        try {
            valid = client.execute(new UpdateMetadataHandler(id));
        } catch (Exception ex) {
            LOGGER.error("更新节点元数据时发生异常，", ex);
        }
        //如果为false，表示服务器上无该Session节点，需要重新创建(返回null)
        if (!valid) {
            //删除本地的副本
            if (session != null) {
                session.invalidate();
            } else {
                //删除ZooKeeper上孤儿Session节点
                try {
                    client.execute(new RemoveNodeHandler(id));
                } catch (Exception ex) {
                    LOGGER.error("删除节点元数据时发生异常，", ex);
                }
            }
            return null;
        } else {
            //如果存在，则直接返回
            if (session != null) {
                return session;
            }
            //否则创建指定ID的Session并返回(用于同步分布式环境中的其他机器上的Session本地副本)
            session = new JettyDistributedSession((AbstractSessionManager) req.getSessionManager(),
                System.currentTimeMillis(), id, this);
            addHttpSession(session);
            return session;
        }
    }

    /**
     * 创建一个新的session
     * 
     * @param request
     * @return
     */
    @Override
    public HttpSession newHttpSession(HttpServletRequest request) {
        //类型检查
        if (!(request instanceof Request)) {
            LOGGER.warn("不是Jetty容器下的Request对象");
            return null;
        }
        //将HttpServletRequest转换成Jetty容器的Request类型
        Request req = (Request) request;
        Session session = new JettyDistributedSession(
            (AbstractSessionManager) req.getSessionManager(), request, this);
        String id = session.getId();
        // 写cookie
        Cookie cookie = CookieHelper.writeSessionIdToCookie(id, req, req.getConnection()
            .getResponse(), COOKIE_EXPIRY);
        if (cookie != null) {
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("Wrote sid to Cookie,name:[" + cookie.getName() + "],value:["
                            + cookie.getValue() + "]");
            }
        }
        //在ZooKeeper服务器上创建session节点，节点名称为Session ID
        //创建元数据
        SessionMetaData metadata = new SessionMetaData();
        metadata.setId(id);
        long sessionTimeout = NumberUtils.toLong(config.getString(Configuration.SESSION_TIMEOUT)) * 60 * 1000;
        metadata.setMaxIdle(sessionTimeout); //转换成毫秒
        try {
            client.execute(new CreateNodeHandler(id, metadata));
        } catch (Exception ex) {
            LOGGER.error("创建节点时发生异常，", ex);
        }
        addHttpSession(session);
        return session;
    }
}
