/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.catalina;

import javax.servlet.http.HttpSession;

import org.apache.catalina.session.StandardSessionFacade;

/**
 * Session门面，兼容Tomcat的处理方法
 * @author  ocean
 * @version $Id: CatalinaDistributedSession.java, v 0.1 2010-12-31 下午05:16:01  ocean Exp $
 */
public class CatalinaDistributedSessionFacade extends StandardSessionFacade {
    /**
     * 构造方法
     * @param session
     */
    public CatalinaDistributedSessionFacade(HttpSession session) {
        super(session);
    }

}
