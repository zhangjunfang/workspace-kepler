/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2011 All Rights Reserved.
 */
package org.storevm.toolkits.session;

import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.util.Random;

import javax.servlet.http.HttpServletRequest;

import org.apache.log4j.Logger;

/**
 * 
 * @author  ocean
 * @version $Id: DefaultSessionIdManager.java, v 0.1 2011-1-1 ÉÏÎç11:53:39  ocean Exp $
 */
public class DefaultSessionIdManager implements SessionIdManager {
    private final static String   __NEW_SESSION_ID                = "org.storevm.newSessionId";
    protected final static String SESSION_ID_RANDOM_ALGORITHM     = "SHA1PRNG";
    protected final static String SESSION_ID_RANDOM_ALGORITHM_ALT = "IBMSecureRandom";
    private Logger                log                             = Logger.getLogger(getClass());
    protected Random              random;
    private boolean               weakRandom;
    private boolean               started                         = false;
    private boolean               stopped                         = false;

    /**
     *
     * @see org.storevm.toolkits.component.LifeCycle#start()
     */
    @Override
    public void start() throws Exception {
        if (isStarted()) {
            return;
        }
        if (random == null) {
            try {
                random = SecureRandom.getInstance(SESSION_ID_RANDOM_ALGORITHM);
            } catch (NoSuchAlgorithmException e) {
                try {
                    random = SecureRandom.getInstance(SESSION_ID_RANDOM_ALGORITHM_ALT);
                    weakRandom = false;
                } catch (NoSuchAlgorithmException e_alt) {
                    log.warn("Could not generate SecureRandom for session-id randomness", e);
                    random = new Random();
                    weakRandom = true;
                }
            }
        }
        random.setSeed(random.nextLong() ^ System.currentTimeMillis() ^ hashCode()
                       ^ Runtime.getRuntime().freeMemory());
        started = true;
    }

    /**
     *
     * @see org.storevm.toolkits.component.LifeCycle#stop()
     */
    @Override
    public void stop() throws Exception {
        stopped = true;
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
     *
     * @see org.storevm.toolkits.session.SessionIdManager#newSessionId(javax.servlet.http.HttpServletRequest, long)
     */
    @Override
    public String newSessionId(HttpServletRequest request, long created) {
        synchronized (this) {
            // A requested session ID can only be used if it is in use already.
            String requestedId = request.getRequestedSessionId();

            if (requestedId != null) {
                return requestedId;
            }

            // Else reuse any new session ID already defined for this request.
            String newId = (String) request.getAttribute(__NEW_SESSION_ID);
            if (newId != null) {
                return newId;
            }

            // pick a new unique ID!
            String id = null;
            while (id == null || id.length() == 0) {
                long r = weakRandom ? (hashCode() ^ Runtime.getRuntime().freeMemory()
                                       ^ random.nextInt() ^ (((long) request.hashCode()) << 32))
                    : random.nextLong();
                r ^= created;
                if (request != null && request.getRemoteAddr() != null)
                    r ^= request.getRemoteAddr().hashCode();
                if (r < 0)
                    r = -r;
                id = Long.toString(r, 36);
            }

            request.setAttribute(__NEW_SESSION_ID, id);
            return id;
        }
    }

}
