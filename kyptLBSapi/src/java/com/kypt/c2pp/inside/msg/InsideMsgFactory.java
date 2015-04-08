/*******************************************************************************
 * @(#)MessageFactory.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg;

import com.kypt.c2pp.inside.msg.req.ActiveTestReq;
import com.kypt.c2pp.inside.msg.req.LoginReq;
import com.kypt.c2pp.inside.msg.req.LogoReq;
import com.kypt.c2pp.inside.msg.req.UpLoadDataReq;
import com.kypt.c2pp.inside.msg.resp.ActiveTestResp;
import com.kypt.c2pp.inside.msg.resp.UploadDataResp;
import com.kypt.configuration.C2ppCfg;


public final class InsideMsgFactory {

    private InsideMsgFactory() {
    }

    public static InsideMsg createActiveTestReq() {
        ActiveTestReq activeTest = new ActiveTestReq();
        return activeTest;
    }

    public static InsideMsg createActiveTestResp(String seq) {
        ActiveTestResp resp = new ActiveTestResp();

        return resp;
    }
    
    public static InsideMsg createBindReq() {
        LoginReq req = new LoginReq();
        req.setUserId(C2ppCfg.props.getProperty("superviseUserId"));
        req.setUserType(C2ppCfg.props.getProperty("superviseUserType"));
        req.setPassword(C2ppCfg.props.getProperty("supervisePassWord"));
        req.setSystemId(C2ppCfg.props.getProperty("superviseSystemId"));
        System.out.println("loginreq:"+req.toString());
        return req;
    }

    public static InsideMsg createLoginReq() {
        LoginReq req = new LoginReq();
        req.setUserId(C2ppCfg.props.getProperty("superviseUserId"));
        req.setUserType(C2ppCfg.props.getProperty("superviseUserType"));
        req.setPassword(C2ppCfg.props.getProperty("supervisePassWord"));
        req.setSystemId(C2ppCfg.props.getProperty("superviseSystemId"));
        System.out.println("loginreq:"+req.toString());
        return req;
    }
    
    public static InsideMsg createLogoReq() {
        LogoReq req = new LogoReq();
        return req;
    }

    public static InsideMsg createUpLoadDataReq() {
        return new UpLoadDataReq();
    }

    public static InsideMsg createUpLoadDataResp(String seq) {
        UploadDataResp resp = new UploadDataResp();
        resp.setMsgLength(String.valueOf(UploadDataResp.MSGHEADERSIZE));
        resp.setCommand(UploadDataResp.COMMAND);
        resp.setStatusCode(UploadDataResp.STATUS);
        resp.setSeq(seq);
        return resp;
    }
}
