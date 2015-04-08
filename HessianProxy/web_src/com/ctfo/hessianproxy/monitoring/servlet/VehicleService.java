package com.ctfo.hessianproxy.monitoring.servlet;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

import javax.servlet.ServletException;
import javax.servlet.ServletInputStream;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.hessianproxy.remoteservice.RemoteManager;
import com.ctfo.hessianproxy.util.CtfoException;
import com.ctfo.hessianproxy.util.Monitoring;
import com.ctfo.hessianproxy.util.MonitoringParameterUtil;
import com.ctfo.hessianproxy.util.SpringBUtils;
import com.ctfo.hessianproxy.util.StaticSession;
import com.ctfo.hessianproxy.util.XMLParse;
import com.ctfo.monitoring.beans.MonitoringDataParameter;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： HessianProxy <br>
 * 功能：配置Servelet，读取HTTP Post请求 <br>
 * 描述：配置Servelet，读取HTTP Post请求 ，响应请求结果<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>Dec 29, 2011</td>
 * <td>DEVELOPER</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOPER
 * @since JDK1.6
 */
public class VehicleService extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private static final Log log = LogFactory.getLog(VehicleService.class);

	public static Queue<String> queue = new LinkedList<String>();
	HttpServletRequest request;
	HttpServletResponse response;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public VehicleService() {
		super();
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		response.setCharacterEncoding("utf-8");
		response.setContentType("text/xml;charset=utf-8");
		PrintWriter pw;
		pw = response.getWriter();
		String resultValue = "ERROR.";
		try {
			pw.write(Monitoring.data.toString());
		} catch (Exception e) {
			pw.write(resultValue);
		} finally {
		}
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		PrintWriter pw;
		try {
			StaticSession.RECEIVE_COUNT++;
			System.out.println("===========================[共接收" + StaticSession.RECEIVE_COUNT + "请求。]===========================");
			long startTime = System.currentTimeMillis();
			this.request = request;
			this.response = response;
			// 设置响应参数端
			response.setCharacterEncoding("utf-8");
			response.setContentType("text/plain;charset=utf-8");
			
			MonitoringDataParameter monitoringDataParameter = null;
			String resultValue = "";
			String requestValue = getRequestBody(request);
			log.debug("The post data:\n" + requestValue);
			try {
				List<MonitoringDataParameter> list = MonitoringParameterUtil.getMonitoringDataParameter(requestValue);
				if (null != list && 0 < list.size()) {
					monitoringDataParameter = list.get(0);
				}
			} catch (Exception e) {
				CtfoException.printException(e);
				log.error("=================解析XML失败,请确认XML====================");
				log.error("解析XML异常信息：" + e.getMessage());
				resultValue = "解析请求数据异常，请确认参数!";
			}
			try {
				if (null != monitoringDataParameter) {
					RemoteManager remoteManager = (RemoteManager) SpringBUtils.getBean("serHessianRemoteManager");
					resultValue = remoteManager.execute(monitoringDataParameter.getServiceName(), monitoringDataParameter.getMethodName(), monitoringDataParameter.getParameter(), monitoringDataParameter.getId()).toString();
					StaticSession.ACK_COUNT++;
					log.debug("The response data:\n" + resultValue);
				}
			} catch (Exception e) {
				CtfoException.printException(e);
				log.error("请求服务[" + "" + "]执行方法[" + "" + "]异常，请确认服务及方法.异常信息：" + e.getMessage());
				log.error(e.getMessage());
				String keys[] = null;
				resultValue = XMLParse.getResponse(keys,-1,monitoringDataParameter.getId()).asXML();
			}
			try {
				pw = response.getWriter();
				pw.write(resultValue);
				long endTime = System.currentTimeMillis();
				System.out.println("===========================[处理第" + StaticSession.ACK_COUNT + "个请求,共耗时" + (endTime - startTime) + "秒]===========================");
			} catch (IOException e) {
				CtfoException.printException(e);
			}

		} catch (Exception e) {
			CtfoException.printException(e);
			pw = response.getWriter();
			pw.write("解析请求数据异常，请确认参数!");
		}
	}

	/**
	 * 获取Request中Post数据
	 * 
	 * @param request
	 *            HttpServletRequest
	 * @return 字符串数据
	 * @throws IOException
	 * @throws UnsupportedEncodingException
	 */
	private String getRequestBody(HttpServletRequest request) throws IOException, UnsupportedEncodingException {
		final int BUFFER_SIZE = 8 * 1024;
		byte[] buffer = new byte[BUFFER_SIZE];
		ServletInputStream sis = request.getInputStream();
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		int bLen = 0;
		while ((bLen = sis.read(buffer)) > 0) {
			baos.write(buffer, 0, bLen);
		}
		String bodyData = new String(baos.toByteArray(), "UTF-8");
		return bodyData;
	}

	public static void main(String[] args) {
		Queue<String> queue = new LinkedList<String>();
		for (int i = 0; i < 10; i++) {
			queue.add("" + i);
		}
//		int count = 0;
		String str = "";
		while ((str = queue.poll()) != null) {
			System.out.print(str);
		}
	}
}
