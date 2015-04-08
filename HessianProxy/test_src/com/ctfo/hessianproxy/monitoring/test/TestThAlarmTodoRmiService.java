package com.ctfo.hessianproxy.monitoring.test;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;

/**
 * @author zhengzhong
 * 报警添加测试
 *
 */
public class TestThAlarmTodoRmiService {

	public static void main(String[] args) throws IOException {
		StringBuilder requestXml = new StringBuilder();
		requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		requestXml.append("<Request service=\"thAlarmTodoRmi\" method=\"add\" id=\"id\">");
		requestXml.append("<Param>");
		requestXml.append("<Item supervisionEndUtc=\"100\" supervisionId=\"101\" supervisionLevel=\"1\" " +
				"supervisor =\"11\" supervisorEmail=\"xxx@163.com\" supervisorTel=\"5212372\" vehicleColor=\"2\" vehicleNo=\"3\" " +
				"wanSrc=\"4\" wanType=\"103\" warUtc=\"104\"/>");
		requestXml.append("</Param>");
		requestXml.append("</Request>");
		System.out.println(requestXml);
		InputStream is = null;
		BufferedReader br = null;

		// 发送
		URL url = new URL("http://127.0.0.1:8080/HessianProxy/VehicleService");
		URLConnection connection = url.openConnection();
		if (connection instanceof HttpURLConnection) {
			((HttpURLConnection) connection).setRequestMethod("POST");
		}
		connection.setDoInput(true);
		connection.setDoOutput(true);
		connection.setUseCaches(false);
		connection.setRequestProperty("Content-Type", "text/plain");

		OutputStream output = connection.getOutputStream();
		output.write(requestXml.toString().getBytes("UTF-8"));
		output.flush();
		output.close();

		// 接收
		is = connection.getInputStream();
		br = new BufferedReader(new InputStreamReader(is, "UTF-8"));
		StringBuffer sb = new StringBuffer();
		String temp = null;
		while ((temp = br.readLine()) != null) {
			sb.append(temp);
		}
		br.close();
		is.close();
		System.out.println(sb.toString());
	}
}
