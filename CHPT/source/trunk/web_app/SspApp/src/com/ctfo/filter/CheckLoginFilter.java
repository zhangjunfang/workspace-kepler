package com.ctfo.filter;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Enumeration;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.context.FrameworkContext;
import com.ctfo.sys.beans.OperatorInfo;

@SuppressWarnings({ "unused" })
public class CheckLoginFilter implements Filter
{
	static Logger logger = Logger.getLogger("OPEN_URL");
	JedisPool jedisPool = (JedisPool)FrameworkContext.getBean("writeJedisPool");
	
    public void init(FilterConfig arg0) throws ServletException {
    	
    }

    public void doFilter(ServletRequest arg0, 
            ServletResponse arg1, 
            FilterChain arg2) 
            throws IOException, ServletException {

        HttpServletRequest request = (HttpServletRequest)arg0;
        HttpServletResponse response = (HttpServletResponse) arg1;
        
    	Jedis client = jedisPool.getResource();
//    	client.select(2);
        //登录操作员信息
        OperatorInfo opInfo=OperatorInfo.getOperatorInfo(); 
    	if(opInfo==null){
    		String URLContextPath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort();
		    String loginPage = request.getContextPath()+"/login.html";
		    response.sendRedirect(URLContextPath+loginPage); 
			//response.getWriter().write("<script>location.href='"+URLContextPath+loginPage+"';</script>");
			return;
    	}
    	String opLoginname = opInfo.getOpLoginname();
 
    	client.hset("LOGIN", opLoginname, String.valueOf(System.currentTimeMillis()));
        arg2.doFilter(arg0,arg1);
    }
    
	private String getPagePath(HttpServletRequest request) {
		StringBuffer buff = new StringBuffer(request.getRequestURL().toString());
		Enumeration<String> en = request.getParameterNames();
		String pname = null,pvalue = null;
		while(en.hasMoreElements()){
			pname = en.nextElement();
			pvalue = request.getParameter(pname);
			buff.append(pvalue.indexOf("?")>0?"&":"?").append(pname).append("=").append(pvalue);
		}
		return buff.toString();
	}	
	public void destroy() {
        
        
    }

}
