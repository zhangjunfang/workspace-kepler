/*******************************************************************************
 * @(#)NioHandler.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.nio.client;

import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.RejectedExecutionHandler;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

import org.apache.mina.common.IoHandlerAdapter;
import org.apache.mina.common.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.configuration.C2ppCfg;
import com.kypt.log.LogFormatter;

public abstract class AbstractNioHandler<E extends ICommunicateService> implements INioHandler<E> {

    private Logger log = LoggerFactory.getLogger(AbstractNioHandler.class);
    
    private static final String NAME = "AbstractNioHandler";

    private E nioService;

    public AbstractNioHandler(E nioService) {
        this.nioService = nioService;
    }

    private final Adapter adapter = new Adapter();

    @SuppressWarnings("unchecked")
    private ThreadPoolExecutor threadPool = new ThreadPoolExecutor(Integer.parseInt(C2ppCfg.props
            .getProperty("corePoolSize")), Integer
            .parseInt(C2ppCfg.props.getProperty("maxPoolSize")), Long.parseLong(C2ppCfg.props
            .getProperty("keepAliveTime")), TimeUnit.SECONDS, new ArrayBlockingQueue(Integer
            .parseInt(C2ppCfg.props.getProperty("queueSize"))), new RejectedExecutionHandler() {
        public void rejectedExecution(Runnable r, ThreadPoolExecutor executor) {
            log.error(LogFormatter.formatMsg(NAME, "threadPool queue is full."));
        }
    });

    class Adapter extends IoHandlerAdapter {

        @Override
        public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
            try {
                super.exceptionCaught(session, cause);
                log.error(LogFormatter.formatMsg(NAME, "the session with "
                        + nioService.getRemoteAddress() + " occurs exception, it will be closed."),
                        cause);
                session.close();
            } catch (Throwable t) {
                log.error(LogFormatter.formatMsg(NAME, ""), t);
            }
        }

        @Override
        public void messageReceived(IoSession session, final Object message) throws Exception {
            super.messageReceived(session, message);
            threadPool.execute(new Runnable() {
                public void run() {
                    try {
                    	System.out.println("doMsg begin"+message);
                        doMsg(nioService, (String) message);
                    } catch (Throwable t) {
                        log.error(LogFormatter.formatMsg(NAME,
                                "deal with message failed."), t);
                    }
                }
            });
        }

        @Override
        public void sessionClosed(IoSession session) throws Exception {
            try {
                super.sessionClosed(session);
                nioService.setAvailable(false);
                connectionClosed(nioService);
                log.info(LogFormatter.formatMsg(NAME, "the session with "
                        + nioService.getRemoteAddress() + " is closed."));
            } catch (Throwable t) {
                log.error(LogFormatter.formatMsg(NAME, "the session with "
                        + nioService.getRemoteAddress() + " closed failed."), t);
            }
        }

        @Override
        public void sessionOpened(IoSession session) throws Exception {
            try {
                super.sessionOpened(session);
                nioService.setSession(session);
                nioService.setAvailable(true);
                connectionOpen(nioService);
                log.info(LogFormatter.formatMsg(NAME, "the session with "
                        + nioService.getRemoteAddress() + " is open."));
            } catch (Throwable t) {
                log.error(LogFormatter.formatMsg(NAME, "the session with "
                        + nioService.getRemoteAddress() + " open failed."), t);
            }
        }
    }

    /**
     * @return Returns the handler.
     */
    public Adapter getAdapter() {
        return adapter;
    }

    /**
     * @return Returns the nioService.
     */
    public final E getNioService() {
        return nioService;
    }
}
