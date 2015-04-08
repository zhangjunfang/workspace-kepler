package com.ctfo.hessianproxy.monitoring.test;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;

public class TestVehicleService {

	public static void main(String[] args) throws IOException {

		InputStream is = null;
		BufferedReader br = null;
		// 发送
		URL url = new URL("http://10.8.3.164:8080/HessianProxy/VehicleService");
		URLConnection connection = url.openConnection();
		if (connection instanceof HttpURLConnection) {
			((HttpURLConnection) connection).setRequestMethod("POST");
		}
		connection.setDoInput(true);
		connection.setDoOutput(true);
		connection.setUseCaches(false);
		connection.setRequestProperty("Content-Type", "text/plain");

		OutputStream output = connection.getOutputStream();
		byte[] str = isRegVehicleNOGBNew().toString().getBytes("UTF-8");
		System.out.println(isRegVehicleNOGBNew().toString());
		output.write(str);
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

	// public static StringBuffer getIsRegVehicle() {
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"isRegVehicle\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item vehicleColor=\"1\" vehicleno=\"测A11111\" phoneNum=\"15391989344\" terminaltype=\"15000000001\" terminalid=\"333333\" manufacturerid=\"00000\" cityid=\"1\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//
	// public static StringBuffer getCheck() {
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"isCheckVehicle\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  phoneNum=\"15810964021\" akey=\"33333345\" />");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//
	// public static StringBuffer isLogoffVehicle() {
	//
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"isLogoffVehicle\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  phoneNum=\"15810964021\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//
	// public static StringBuffer getRegVehicleInfo() {
	//
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"getRegVehicleInfo\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  phoneNum=\"15000000001\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//
	// public static StringBuffer getBaseVehicleInfo() {
	//
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"getBaseVehicleInfo\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  phoneNum=\"15000000001\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//
	// public static StringBuffer getDriverOfVehicleByType() {
	//
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"getDriverOfVehicleByType\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  vehicleColor=\"2\" vehicleno=\"京A00001\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//
	// public static StringBuffer getEticketByVehicle() {
	//
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"getEticketByVehicle\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  vehicleColor=\"2\" vehicleno=\"宁A00001\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	//
	//
	// // <Request id="USERID_UTC_19" service="vehicleInforService"
	// method="getEticketByVehicle">
	// // <Param>
	// // <Item vehicleno="2" vehicleColor="京A00001" />
	// // </Param>
	// // </Request>
	// //
	//
	// return requestXml;
	// }
	// public static StringBuffer getDetailOfVehicleInfo() {
	//
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"getDetailOfVehicleInfo\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  vehicleColor=\"2\" vehicleno=\"宁A00001\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	// public static StringBuffer getTernimalByVehicleByType() {
	//	
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"getTernimalByVehicleByType\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  vehicleColor=\"2\" vehicleno=\"宁A00001\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	// public static StringBuffer get2gsimBy3sim() {
	//	
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"get2gBy3g\" id=\"get2gBy3g\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  sim3=\"13333333333\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	//	 
	// public static StringBuffer get3gsimBy2sim() {
	//			
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"get3gBy2g\" id=\"get3gBy2g\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  sim2=\"13523452764\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }

	// public static StringBuffer get2g3gSimMapping() {
	//			
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"get2g3gSimMapping\" id=\"get2g3gSimMapping\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item  registerNum=\"13523452764\"/>");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }
	// public static StringBuffer getIsRegVehicleByPhoneNumber() {
	// StringBuffer requestXml = new StringBuffer();
	// requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
	// requestXml.append("<Request service=\"vehicleInforService\" method=\"isRegVehicleByPhoneNumber\" id=\"id\">");
	// requestXml.append("<Param>");
	// requestXml.append("<Item phoneNumber=\"15294602474\" />");
	// requestXml.append("</Param>");
	// requestXml.append("</Request>");
	// return requestXml;
	// }

	public static StringBuffer isRegVehicleNOGBNew() {
		StringBuffer requestXml = new StringBuffer();
		requestXml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		requestXml.append("<Request service=\"vehicleInforService\" method=\"isRegVehicleNOGBNew\" id=\"id\">");
		requestXml.append("<Param>");
		requestXml.append("<Item terminalid=\"3001617631\" />");
		requestXml.append("</Param>");
		requestXml.append("</Request>");
		return requestXml;
	}
}
