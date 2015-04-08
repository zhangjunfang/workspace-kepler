package com.ctfo.datatransferserver.util;

import java.io.IOException;
import java.io.OutputStream;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.Socket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * Socket工具类
 * 
 * @author yangyi
 * 
 */
public class SocketUtil {
	private static final Logger logger = LoggerFactory.getLogger(SocketUtil.class);

	/**
	 * 使用TCP 发送数据
	 * 
	 * @param IP
	 * @param port
	 * @param data
	 * @throws IOException
	 */
	public static synchronized boolean sendTCPData(String IP, int port, byte[] data) {
		Socket socket = null;
		OutputStream out = null;
		boolean state = false;
		try {
			socket = new Socket(IP, port);
			out = socket.getOutputStream();
			out.write(data);
			out.flush();
			state = true;
			return state;
		} catch (Exception e) {
			logger.error("向[" + IP + ":" + port + "]发送数据异常:" + e.getMessage(), e);
			return state;
		} finally {
			try {
				if (out != null)
					out.close();
				if (socket != null)
					socket.close();

			} catch (Exception e) {
				logger.error("关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 使用UDP发送数据
	 * 
	 * @param IP
	 * @param port
	 * @param data
	 */
	public static synchronized void sendUDPData(String IP, int port, byte[] data) {
		DatagramSocket socket = null;
		try {
			InetAddress address = InetAddress.getByName(IP);
			DatagramPacket dataGramPacket = new DatagramPacket(data, data.length, address, port);
			socket = new DatagramSocket();
			socket.send(dataGramPacket);
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			socket.close();
		}
	}

	/**
	 * 使用TCP 发送数据
	 * 
	 * @param IP
	 * @param port
	 * @param data
	 * @throws IOException
	 */
	public static synchronized boolean testsendTCPData(String IP, int port, byte[] data) {
		Socket socket = null;
		OutputStream out = null;
		boolean state = false;
		try {
			socket = new Socket(IP, port);
			out = socket.getOutputStream();

			out.write(data);
			out.flush();
			state = true;
			return state;
		} catch (Exception e) {
			e.printStackTrace();
			return state;
		}
	}

	public static void main(String[] args) throws IOException {
		String a = "0000000000000000000000000000000000000000000000000000000001";
		byte[] cc = a.getBytes();
		sendTCPData("127.0.0.1", 4000, cc);
		byte[] aa = ZipUtil.GZIP(cc);
		String s = "";
		for (int i = 0; i < aa.length; i++) {
			s = s + (char) aa[i];
		}
		System.out.println(ByteConvert.bytesToHexString(cc));
		System.out.println(ByteConvert.bytesToHexString(aa));
		System.out.println(s);
		// 1F 8B 08 00 00 00 00 00 00 00 33 30 20 13 18 02 00 E7 89 EF C2 3A 00
		// 00 00
	}

}
