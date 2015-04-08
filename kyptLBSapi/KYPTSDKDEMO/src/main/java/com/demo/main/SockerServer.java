package com.demo.main;

import java.net.*;
import java.io.*;

/**
 * 模拟服务器
 * @author yujch
 *
 */
public class SockerServer {
	private int port = 18888;
	private ServerSocket serverSocket;

	public SockerServer() throws IOException {
		serverSocket = new ServerSocket(port);
		System.out.println("服务器已启动！");
	}

	public String echo(String msg) {
		return "echo:" + msg;
	}

	private PrintWriter getWriter(Socket socket) throws IOException {
		OutputStream socketOut = socket.getOutputStream();
		return new PrintWriter(socketOut, true);
	}

	private BufferedReader getReader(Socket socket) throws IOException {
		InputStream socketIn = socket.getInputStream();
		return new BufferedReader(new InputStreamReader(socketIn));
	}

	public void server() {
		while (true) {
			Socket socket = null;
			try {
				socket = serverSocket.accept();
				System.out.println("New connection accepted"
						+ socket.getInetAddress() + ":" + socket.getPort());
				BufferedReader br = getReader(socket);
				PrintWriter pw = getWriter(socket);

				String msg = null;
				int i=1;
				while ((msg = br.readLine()) != null) {
					System.out.println("Line"+i+":"+msg);
					pw.write("LACK 0\r\n");
					pw.flush();
					i++;
					if (msg.equals("bye"))
						break;
				}
			} catch (IOException e) {
				e.printStackTrace();
			} finally {
				try {
					if (socket != null)
						socket.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

	public static void main(String[] args) throws IOException {
		new SockerServer().server();
	}
}