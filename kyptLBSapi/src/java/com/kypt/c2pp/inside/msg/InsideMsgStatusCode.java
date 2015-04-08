/*******************************************************************************
 * @(#)StatusCode.java 2008-9-12
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg;

import java.util.concurrent.ConcurrentLinkedQueue;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-9-12 下午02:34:55
 */
public final class InsideMsgStatusCode extends ConcurrentLinkedQueue<String> {

    private static final long serialVersionUID = -5614074991876594794L;

    public static final String STATUS_CODE_SUCCESS = "0000";

    public static final String STATUS_COMMAND_WRONG = "0001";

    public static final String STATUS_NO_THIS_COMMAND = "0002";

    public static final String STATUS_INVALID_TERMINAL = "0003";

    public static final String STATUS_TERMINAL_NOT_SUBSCRIBE_BUSINESS = "0004";

    public static final String STATUS_TERMINAL_OFFLINE = "0005";

    public static final String STATUS_TERMINAL_NOT_SUPPORT_COMMAND = "0006";

    public static final String STATUS_CANNOT_SEND = "0007";
    
    public static final String STATUS_NOT_THIS_ELEMENT = "0008";
    
    public static final String STATUS_LOGIN_COMMON_OP="0000";// 一般操作员登陆监管平台
    
    public static final String STATUS_LOGIN_MIDDLE_OP="0001";// 中级操作员登陆监管平台
    
    public static final String STATUS_LOGIN_ADMIN_OP="0002";// 系统管理员登陆监管平台
    
    public static final String STATUS_LOGIN_PASSWORD_ERROR="-0001";// 登陆密码错误
    
    public static final String STATUS_LOGIN_USER_ONLINE="-0002";// 帐号已经登录
    
    public static final String STATUS_LOGIN_USER_LOGOFF="-0003";// 帐号已经停用
    
    public static final String STATUS_LOGIN_USER_UNEXIST="-0004";// 帐号不存在
    
    public static final String STATUS_LOGIN_QUERY_FAILURE="-0005";// sql查询失败
    
    public static final String STATUS_LOGIN_DATABASE_ERROR="-0006";// 未登录数据库
    

    private static final InsideMsgStatusCode STATUS = new InsideMsgStatusCode();

    public static InsideMsgStatusCode getInstance() {
        return STATUS;
    }

    private InsideMsgStatusCode() {
        this.add(STATUS_CODE_SUCCESS);
        this.add(STATUS_COMMAND_WRONG);
        this.add(STATUS_NO_THIS_COMMAND);
        this.add(STATUS_INVALID_TERMINAL);
        this.add(STATUS_TERMINAL_NOT_SUBSCRIBE_BUSINESS);
        this.add(STATUS_TERMINAL_OFFLINE);
        this.add(STATUS_TERMINAL_NOT_SUPPORT_COMMAND);
        this.add(STATUS_CANNOT_SEND);
        this.add(STATUS_LOGIN_COMMON_OP);
        this.add(STATUS_LOGIN_MIDDLE_OP);
        this.add(STATUS_LOGIN_ADMIN_OP);
        this.add(STATUS_LOGIN_PASSWORD_ERROR);
        this.add(STATUS_LOGIN_USER_ONLINE);
        this.add(STATUS_LOGIN_USER_LOGOFF);
        this.add(STATUS_LOGIN_USER_UNEXIST);
        this.add(STATUS_LOGIN_QUERY_FAILURE);
        this.add(STATUS_LOGIN_DATABASE_ERROR);
    }
}
