/**
 * 
 */
package com.ctfo.storage.init.core;

import java.io.IOException;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


/**
 * 字符集过滤
 * 
 */
public class EncodingFilter implements Filter {
	private static Logger log = LoggerFactory.getLogger(EncodingFilter.class);
	/**	解码字符集	*/
	private String encoding;

	/**
	 * 初始化
	 */
	public void init(FilterConfig config) throws ServletException {
		try {
			encoding = config.getInitParameter("encoding").trim();
			if (encoding == null) {
				throw new ServletException("初始化字符集异常");
			}
			log.info("字符过滤器初始化加载完成!");
		} catch (Exception e) {
			throw new ServletException("初始化日志异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 应用关闭
	 */
	public void destroy() {

	}

	/**
	 * 字符过滤
	 */
	public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain) throws IOException, ServletException {
		
//		for(Entry<String, String[]> s : pn.entrySet()){
//			StringBuffer sb = new StringBuffer();
//			for(String str : s.getValue()){
//				sb.append(str).append(" ");
//			}
//			log.info("---------------pn:key={} ,  value={}" ,request.getServletContext().getRealPath("") , sb.toString());
//		}
	/*	request.setCharacterEncoding(encoding);
		response.setCharacterEncoding(encoding);
		chain.doFilter(request, response);*/
	}
}
