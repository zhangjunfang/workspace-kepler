package com.ctfo.informationser.util;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.Socket;
import java.net.UnknownHostException;

public class ProxyClient {
	static Socket socket = null;

	public static void main(String[] args) {
		StringBuffer sb = new StringBuffer();
		sb.append("LOGI UWEB test test \r\n");
		ProxyClient.write(sb.toString());
		try {
			Thread.sleep(5000);
			sb.setLength(0);
			sb.append("DISCON 6401 0\r\n");
			ProxyClient.write(sb.toString());
		} catch (InterruptedException e) {
			e.printStackTrace();
		}

	}

	public ProxyClient() {
		// try {
		// socket = new Socket("127.0.0.1", 8009);
		// } catch (UnknownHostException e) {
		// e.printStackTrace();
		// } catch (IOException e) {
		// e.printStackTrace();
		// }
	}

	public static void sendMeaage(String message) {
		try {
			socket = new Socket("192.168.5.45", 7855);
			ProxyClient.write(login());
			Thread.sleep(5000);
			ProxyClient.write(message);

		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	public static String login() {
		StringBuffer sb = new StringBuffer();
		sb.append("LOGI UWEB test test \r\n");
		return sb.toString();
	}

	public static void write(String message) {
		OutputStream os = null;
		try {
			// socket = new Socket("127.0.0.1", 7855);
			os = socket.getOutputStream();
			if (message != null) {
				os.write(message.getBytes());
				message = null;
			}
			InputStream in = socket.getInputStream();
			BufferedReader br = new BufferedReader(new InputStreamReader(in));
			System.out.println(br.readLine());
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
//			if (os != null) {
//				try {
//					os.close();
//				} catch (IOException e) {
//					e.printStackTrace();
//				}
//			}
		}

	}
}
