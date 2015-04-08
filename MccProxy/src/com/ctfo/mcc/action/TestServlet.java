package com.ctfo.mcc.action;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.Map;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class TestServlet extends HttpServlet {
	private static Logger logger = LoggerFactory.getLogger(TestServlet.class);
	/**	*/
	private static final long serialVersionUID = -2978332181248186084L;

	/**
	 * get
	 */
	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		doPost(req, resp);
	}
	/**
	 * post
	 */
	@Override
	protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
//		返回请求 URI 指示请求上下文的那一部分。请求 URI 中首先出现的总是上下文路径。路径以 "/" 字符开头但不以 "/" 字符结束。对于默认（根）上下文中的 servlet，此方法返回 ""。容器不会解码此字符串。
		
		/**
		 * 返回此请求的 URL 的一部分，从协议名称一直到 HTTP 请求的第一行中的查询字符串。Web 容器不会解码此 String。例如：
			HTTP 请求的第一行	 返回的值
			POST /some/path.html HTTP/1.1		/some/path.html
			GET http://foo.bar/a.html HTTP/1.0		/a.html
			HEAD /xyz?a=b HTTP/1.1		/xyz
			
			
			http://localhost:8080/Mcc/remoteMcc.action?xmlMessage=123&MSG=ABCD&abc=123
			request:[/Mcc] 
			url:[/Mcc/remoteMcc.action]
			content:/Mcc 
			ServletPath:/remoteMcc.action 
			query:xmlMessage=123&MSG=ABCD&abc=123 
			parma:123
			parmaMap: key:xmlMessage values[123,] key:MSG values[ABCD,] key:abc values[123,]
			*/
		String request = req.getContextPath();
		String content = req.getContextPath();
		String url = req.getRequestURI();
		String servletPath = req.getServletPath();
		String query = req.getQueryString();
		String parma = req.getParameter("xmlMessage");
		Map<String, String[]> map = req.getParameterMap();
		StringBuffer sb = new StringBuffer();
		for(Map.Entry<String, String[]> m : map.entrySet()){
			sb.append(" key:").append(m.getKey());
			sb.append(" values[");
			for(String s : m.getValue()){
				sb.append(s).append(",");
			}
			sb.append("]");
		}
		logger.info("收到request:[{}] \r\n url:[{}]\r\ncontent:{} \r\nServletPath:{} \r\nquery:{} \r\nparma:{}\r\nparmaMap:{}", request, url, content,servletPath,query,parma, sb.toString()); 
		
		PrintWriter pw = resp.getWriter();
		pw.write("test");
		pw.flush();
	}

}
