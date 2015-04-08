package com.ocean.socketAndstorm.test;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;

public class MyServerMulti {
	private static ServerSocket server;

	public static void main(String[] args) throws IOException {
		server = new ServerSocket(5678);
		int i = 0;
		ArrayList<PrintWriter> outs = new ArrayList<PrintWriter>();

		/*
		 * 一个client socket发送数据过来， server端再发到其他client socket端
		 */
		Socket socket1 = null;
		while (true) {
			Socket socket = server.accept();
			i++;
			System.out.println(i);
			System.out.println(socket.getInetAddress());
			PrintWriter out = new PrintWriter(socket.getOutputStream());
			outs.add(out);
			if (i == 1) {
				socket1 = socket;
			}
			if (i == 2) {
				invoke(socket1, outs);
			}

		}
	}

	private static void invoke(final Socket client,
			final ArrayList<PrintWriter> outs) throws IOException {
		new Thread(new Runnable() {
			public void run() {
				BufferedReader in = null;
				PrintWriter out = null;
				PrintWriter out1 = null;
				try {
					in = new BufferedReader(new InputStreamReader(
							client.getInputStream()));
					out = new PrintWriter(client.getOutputStream());

					while (true) {
						String msg = in.readLine();
						System.out.println(msg);
						out.println("Server received " + msg);
						out.flush();

						/* 数据转发送到多个client */
						for (int i = 0; i < outs.size(); i++) {
							out1 = outs.get(i);
							System.out.println(i);
							System.out.println("send msg:" + msg);
							out1.println(msg);
							out1.flush();
						}

						System.out.println(client.getInetAddress());
						if (msg.equals("bye")) {
							break;
						}
					}
				} catch (IOException ex) {
					ex.printStackTrace();
				} finally {
					try {
						in.close();
					} catch (Exception e) {
					}
					try {
						out.close();
					} catch (Exception e) {
					}
					try {
						client.close();
					} catch (Exception e) {
					}
				}
			}
		}).start();
	}
}