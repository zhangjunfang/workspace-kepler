package com.ctfo.action;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.Consts;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;

import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.util.Base64_URl;
import com.ctfo.util.RedisJsonUtil;

public class TestMobileApp {

	public static void main(String[] args) {
		StringBuilder url = new StringBuilder();
		// url.append("http://localhost/MobileApp/login/login!login.action");
		// url.append("http://125.46.82.42:8282/WebApp/login/login!login.action");
		url.append("http://localhost:8082/sspapp/operation/auth/remoteRegisterAuth.do");
//		url.append("http://192.168.1.125:8082/sspapp/operation/auth/remoteRegisterAuth.do");
//		url.append("http://localhost:8082/SspApp/operation/auth/remoteSendAuthResult.do?machineCodeSequence=NjNlOGMwZTZhNjBkYmQ0MjQwZjNmYTI0YjIzMjc1NDU=");
		// url.append("http://localhost/MobileApp/report/report!findOilVehicleReport.action");
		HttpClient httpclient = new DefaultHttpClient();
		try {
			HttpPost httpPost = new HttpPost(url.toString());

			// 添加参数
			List<NameValuePair> nvps = new ArrayList<NameValuePair>();
			nvps.add(new BasicNameValuePair("jsonStr", getJson()));

			// 设置请求的编码格式
			httpPost.setEntity(new UrlEncodedFormEntity(nvps, Consts.UTF_8));
			// 登录一遍
			HttpResponse response = httpclient.execute(httpPost);
			System.out.println(EntityUtils.toString(response.getEntity()));
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				httpclient.getConnectionManager().shutdown();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}

	private static String getJson() {
		/*return "eyJzVXNlciI6IjA3OTdFRjJEMTFBMyIsImdyYW50QXV0aG9yaXphdGlvbiI6Ijg1MEQ3MjYyNzI1QjdCNDg5MDM0MkYwRjk3MUY0N0ZEIiwiYXV0aGVudGljYXRpb24iOiIwMzY2NzJBRjYxIiwiZXJyTXNnIjoiIiwiaXNTdWNjZXNzIjoiMSIsInNQd2QiOiI5RDQ2OTI0RTA4IiwidmFsaWRhdGUiOiIxNDQ5MDQxMzg2MzY5IiwibWFjaGluZUNvZGVTZXF1ZW5jZSI6IjYzZThjMGU2YTYwZGJkNDI0MGYzZmEyNGIyMzI3NTQzIn0=";*/
		String json = null;
		CompanyInfo companyInfo = new CompanyInfo();
		companyInfo.setComName("测试中交慧联");
		companyInfo.setServiceStationSap("0000152877");
		companyInfo.setAccessCode("PC00000001");
		companyInfo.setMachineSerial("63e8c0e6a60dbd4240f3fa24b2327545");
		json = RedisJsonUtil.objectToJson(companyInfo);
		System.out.println("~~~~"+Base64_URl.base64Encode(json));
		return Base64_URl.base64Encode(json);
	}

}
