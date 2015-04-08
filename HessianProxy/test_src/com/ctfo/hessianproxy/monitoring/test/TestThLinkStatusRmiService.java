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
public class TestThLinkStatusRmiService {

	public static void main(String[] args) throws IOException {
		
		StringBuilder requestXml = new StringBuilder();
		requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		requestXml.append("<Request service=\"thLinkStatusServiceRmi\" method=\"add\" id=\"id\">");
		requestXml.append("<Param>");
		requestXml.append("<Item areaId=\"100000\" utc=\"1111111\" " +
				"linkType =\"0\" status=\"-1\"/>");
		requestXml.append("</Param>");
		requestXml.append("</Request>");
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
