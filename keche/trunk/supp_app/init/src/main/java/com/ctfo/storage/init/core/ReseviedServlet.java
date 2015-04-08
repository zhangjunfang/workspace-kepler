/**
 * 
 */
package com.ctfo.storage.init.core;

import java.io.IOException;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.Date;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


/**
 * 请求接收处理器
 * 
 */
public class ReseviedServlet extends HttpServlet {
	private static Logger log = LoggerFactory.getLogger(ReseviedServlet.class);
	private static final long serialVersionUID = 1L;

	// 用于处理客户端发送的GET请求 　　
	public void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		log.info(request.getRequestURI());
		log.info(request.getContextPath());
		response.setContentType("text/html;charset=GB2312");
		// 这条语句指明了向客户端发送的内容格式和采用的字符编码． 　　
		PrintWriter out = response.getWriter();
		out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS").format(new Date()) + "] hello~!! 测试Servlet正常！");
		log.info(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS").format(new Date()) + "] hello~!! 测试Servlet正常！");
		
 
		
		out.close();
	} // 用于处理客户端发送的POST请求

	public void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		doGet(request, response);// 这条语句的作用是，当客户端发送POST请求时，调用doGet()方法进行处理
	}
}
