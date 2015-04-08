package com.ctfo.synmodelser.http;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;

public class HttpClient {
	public static void synData(String server) {
		try {
			InputStream is = null;
			BufferedReader br = null;
			// 发送
			URL url = new URL(server);
			URLConnection connection = url.openConnection();
			if (connection instanceof HttpURLConnection) {
				((HttpURLConnection) connection).setRequestMethod("POST");
			}
			connection.setDoInput(true);
			connection.setDoOutput(true);
			connection.setUseCaches(false);
			connection.setRequestProperty("Content-Type", "text/plain");
			OutputStream output = connection.getOutputStream();
			StringBuffer requestXml = new StringBuffer();
			requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
			requestXml.append("<Request service=\"vehicleInforService\" method=\"synCheckData\" id=\"id\">");
			requestXml.append("<Param>");
			requestXml.append("<Item  phoneNum=\"1\" akey=\"1\" />");
			requestXml.append("</Param>");
			requestXml.append("</Request>");
			output.write((requestXml.toString()).getBytes("UTF-8"));
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
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
