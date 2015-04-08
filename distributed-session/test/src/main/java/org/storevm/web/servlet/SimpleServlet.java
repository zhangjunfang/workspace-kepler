/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2011 All Rights Reserved.
 */
package org.storevm.web.servlet;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 * 测试用Servlet
 * @author  ocean
 * @version $Id: SimpleServlet.java, v 0.1 2011-1-20 下午03:55:28  ocean Exp $
 */
public class SimpleServlet extends HttpServlet {
    /** UID */
    private static final long serialVersionUID = -4832272032995117655L;

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException,
                                                                          IOException {
        PrintWriter out = resp.getWriter();
        //获取Session
        HttpSession session = req.getSession();
        //获取参数
        String param = req.getParameter("param");
        if (param.equalsIgnoreCase("1")) {
            session.setAttribute("password", "password");
            out.println("get session attri: id = " + session.getId() + ", attri="
                        + session.getAttribute("password"));
            try {
                Thread.sleep(1000 * 60);
            } catch (InterruptedException e) {
                //
            }
        } else {
            session.setAttribute("password", "password-new");
        }
        out.println("get session attri again: id = " + session.getId() + ", attri="
                    + session.getAttribute("password"));
        out.flush();
        out.close();
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp)
                                                                           throws ServletException,
                                                                           IOException {
        super.doGet(req, resp);
    }

}
