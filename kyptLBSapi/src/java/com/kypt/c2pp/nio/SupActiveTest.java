/*******************************************************************************
 * @(#)HeartBeat.java 2008-8-27
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.nio;

import java.util.Timer;
import java.util.TimerTask;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.configuration.C2ppCfg;
import com.kypt.c2pp.constant.SupConstant;
import com.kypt.log.LogFormatter;
import com.kypt.nio.client.ICommunicateService;

public class SupActiveTest {

    private Logger log = LoggerFactory.getLogger(SupActiveTest.class);

    private ICommunicateService nioService;

    private byte[] activeTestMsg;

    private int activeTestCurrentNum;

    private Timer timer = new Timer("ActiveTestTimer");

    private ActiveTestTimerTask task;

    public SupActiveTest(ICommunicateService nioService, byte[] activeTestMsg) {
        this.nioService = nioService;
        this.activeTestMsg = activeTestMsg;
    }

    /**
     * start the heart beat timer
     */
    public void start() {
        task = new ActiveTestTimerTask();
        long interval = Long.parseLong(C2ppCfg.props.getProperty("superviseActiveTestInterval"))
                * SupConstant.SECOND;
        timer.schedule(task, 0, interval);
        log.info(LogFormatter
                .formatMsg("TAG Noop", "start the NOOP message timer task."));
    }

    /**
     * cancel the heart beat timer task
     */
    public void cancel() {
        task.cancel();
        timer.cancel();
    }

    /**
     * active test timer task, it will send active test message when there is no mesasge on the
     * connection.
     * @author <a href="mailto:pud@neusoft.com">pu dong </a>
     * @version $Revision 1.1 $ 2008-9-25 上午11:00:13
     */
    class ActiveTestTimerTask extends TimerTask {

        @Override
        public void run() {
            try {
                synchronized (this) {
                    int max_num = Integer.parseInt(C2ppCfg.props
                            .getProperty("superviseActiveTestMaxNum"));
                    if (activeTestCurrentNum >= max_num) {
                        nioService.close();
                        log.info(LogFormatter.formatMsg("Supervise ActiveTest",
                                "there is no active test response message for "
                                        + C2ppCfg.props.getProperty("superviseActiveTestMaxNum")
                                        + " times. the connection has been disconnected by me."));
                        this.cancel();
                    } else {
                        if (activeTestCurrentNum >= 1) {
                            nioService.send(activeTestMsg);
                            log.info(LogFormatter.formatMsg("Supervise ActiveTest",
                                    "send active test message.currentNum=" + activeTestCurrentNum));
                        }
                        activeTestCurrentNum++;
                    }
                }
            } catch (Throwable t) {
                cancel();
                log.error(LogFormatter.formatMsg("Supervise ActiveTest",
                        "activeTest has some problem."), t);
            }
        }
    }

    /**
     * deal with the active test response
     */
    public void doActiveTestResp() {
        log.info(LogFormatter.formatMsg("TAG ActiveTest",
                "receive a active test response message.currentNum=" + activeTestCurrentNum));
        activeTestCurrentNum--;
    }

    /**
     * clear the current number, it stands for there are some message on the connection
     */
    public synchronized void clear() {
        activeTestCurrentNum = 0;
    }
}
