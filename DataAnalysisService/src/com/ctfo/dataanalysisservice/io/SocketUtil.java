package com.ctfo.dataanalysisservice.io;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;

import org.apache.log4j.Logger;

public class SocketUtil {
	private static final Logger logger = Logger.getLogger(SocketUtil.class);

	public SocketUtil() {
	}

	public static Socket createSocket(String ip, int port, int reConnectTime) {
		Socket socket = null;
		do {
			try {
				socket = new Socket(ip, port);
				socket.setReceiveBufferSize(10 * 1024 * 1024);
				logger.info(ip + ":" + String.valueOf(port) + "网络连接成功！");
			} catch (Exception e) {
				logger.error(ip + ":" + String.valueOf(port) + "连接失败"
						+ e.getMessage());
				closeSocket(socket);
				try {
					Thread.sleep(reConnectTime);
				} catch (Exception e2) {
				}
			}
		} while (socket == null);
		return socket;
	}

	public static BufferedReader createReader(Socket socket) {
		BufferedReader in;
		try {
			in = new BufferedReader(new InputStreamReader(
					socket.getInputStream(), "GBK"));
			return in;
		} catch (IOException e) {
			logger.error(e.getMessage());
			return null;
		}

	}

	public static PrintWriter createWriter(Socket socket) {

		PrintWriter out;
		try {
			out = new PrintWriter(new OutputStreamWriter(
					socket.getOutputStream(), "GBK"));
			return out;
		} catch (IOException e) {
			logger.error(e.getMessage());
			return null;

		}

	}

	public static void closeSocket(Socket socket) {
		try {
			if (socket != null)
				socket.close();
		} catch (IOException e) {
			logger.error(e.getMessage());
		}

	}

	public static void closeReader(BufferedReader in) {
		try {
			if (in != null)
				in.close();
		} catch (IOException e) {
			logger.error(e.getMessage());
		}

	}

	public static void closeWriter(PrintWriter out) {
		try {
			if (out != null)
				out.close();
		} catch (Exception e) {
			logger.error(e.getMessage());
		}
	}

}
