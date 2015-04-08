package com.ctfo.mcc.action;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.util.Map;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mcc.service.LinkService;
import com.ctfo.mcc.utils.Base64_URl;


public class MccServlet extends HttpServlet {
	private static Logger logger = LoggerFactory.getLogger(MccServlet.class);
	/**	*/
	private static final long serialVersionUID = -2978332181248186084L;
	private final static String ContentType = "ISO8859-1";
	private final static String ContentTypeXML = "text/xml";
	
	private String xmlStr;
	@Override
	public void init() throws ServletException {
		super.init();
	}
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
		PrintWriter pw = null;
		try {
//			String parma = req.getParameter("xmlMessage");
//			String url = req.getRequestURI();
//			String query = req.getQueryString();
//			logger.info("收到请求:url:[{}], query:[{}], parma[{}]", url, query, parma);
			
			resp.setContentType(ContentType);
			resp.setContentType(ContentTypeXML);
			
			StringBuffer buff = new StringBuffer();
			req.setCharacterEncoding(ContentType);
			InputStream is = req.getInputStream();
			BufferedReader br = new BufferedReader(new InputStreamReader(is, "UTF-8"));
			String buffer = null;
			while ((buffer = br.readLine()) != null) {
				buff.append(buffer);
			}
			if (xmlStr != null) {
				buff.append(xmlStr);
			}
			String msg = buff.toString();
			int indx = msg.indexOf("CAITS");
			if (indx > 1){
				msg = msg.substring(indx);
			}
			
			
			logger.info("收到请求:[{}]", msg);
			if(msg != null){
				Map<String, String> str = LinkService.sendMessage(msg);
				logger.debug("发送指令:[" + msg + "],结果：" + str.get("online") + "_" + str.get("msg"));
				pw = resp.getWriter();
				pw.print(Base64_URl.base64Encode(str.get("online") + "_" + str.get("msg")));
				
				pw.flush();
			}
		}catch(Exception e){
			logger.error("处理请求异常:" + e.getMessage(), e); 
		} finally {
			if(pw != null){
				pw.close();
			}
		}
	}

}
