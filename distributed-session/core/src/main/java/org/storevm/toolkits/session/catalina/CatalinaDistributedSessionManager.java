/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.catalina;

import javax.servlet.ServletContext;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.apache.commons.lang3.math.NumberUtils;
import org.storevm.toolkits.session.DefaultSessionManager;
import org.storevm.toolkits.session.config.Configuration;
import org.storevm.toolkits.session.helper.CookieHelper;
import org.storevm.toolkits.session.metadata.SessionMetaData;
import org.storevm.toolkits.session.zookeeper.handler.CreateNodeHandler;
import org.storevm.toolkits.session.zookeeper.handler.RemoveNodeHandler;
import org.storevm.toolkits.session.zookeeper.handler.UpdateMetadataHandler;

/**
 * Tomcat容器下的Session管理器
 * @author  ocean
 * @version $Id: CatalinaDistributedSessionManager.java, v 0.1 2010-12-31 下午05:26:13  ocean Exp $
 */
public class CatalinaDistributedSessionManager extends DefaultSessionManager {
    /**
     * 构造方法
     * @param config
     */
    public CatalinaDistributedSessionManager(ServletContext sc) {
        super(sc);
    }

    @Override
    public HttpSession getHttpSession(String id, HttpServletRequest request) {
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
            CatalinaDistributedSession sess = new CatalinaDistributedSession(this, id);
            sess.access(); //表示已经被访问过了
            session = new CatalinaDistributedSessionFacade(sess);
            addHttpSession(session);
            return session;
        }
    }

    @Override
    public HttpSession newHttpSession(HttpServletRequest request) {
        String id = getNewSessionId(request); //获取新的Session ID
        CatalinaDistributedSession sess = new CatalinaDistributedSession(this, id);
        HttpSession session = new CatalinaDistributedSessionFacade(sess);
        // 写cookie
        Cookie cookie = CookieHelper.writeSessionIdToCookie(id, request, getResponse(),
            COOKIE_EXPIRY);
        if (cookie != null) {
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("Wrote sid to Cookie,name:[" + cookie.getName() + "],value:["
                            + cookie.getValue() + "]");
            }
        }
        //创建元数据
        SessionMetaData metadata = new SessionMetaData();
        metadata.setId(id);
        Long sessionTimeout = NumberUtils.toLong(config.getString(Configuration.SESSION_TIMEOUT));
        metadata.setMaxIdle(sessionTimeout * 60 * 1000); //转换成毫秒
        //在ZooKeeper服务器上创建session节点，节点名称为Session ID
        try {
            client.execute(new CreateNodeHandler(id, metadata));
        } catch (Exception ex) {
            LOGGER.error("创建节点时发生异常，", ex);
        }
        addHttpSession(session);
        return session;
    }
}
