package com.ocean.socket.raw.client;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.Socket;
import java.util.UUID;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.ThreadLocalRandom;
import java.util.concurrent.atomic.AtomicInteger;

public class MultiThreadClient {

	static AtomicInteger atomicInteger = new AtomicInteger();

	public static void main(String[] args) {

		ExecutorService exec = Executors.newCachedThreadPool();
		File logFile = new File(GetData.class.getClass().getResource("/")
				.getPath());
		if (logFile.isDirectory()) {
			File[] files = logFile.listFiles();
			for (int i = 0; i < files.length; i++) {
				exec.execute(createTask(files[i]));
			}
		} else {
			exec.execute(createTask(logFile));
		}

	}

	// 定义一个简单的任务
	private static Runnable createTask(final File file) {

		return new Runnable() {
			private Socket socket = null;
			private int port = 8888;

			@Override
			public void run() {
				System.out.println("Task " + file.getName() + ":start");
				try {
					socket = new Socket("localhost", port);
					OutputStream socketOut = socket.getOutputStream();
					// -----------------------------------------
					FileReader fileReader = new FileReader(file);
					BufferedReader bufferedReader = new BufferedReader(
							fileReader);
					String data = null;
					int i = 0;
					while (null != (data = bufferedReader.readLine())) {
						socketOut.write(((i++) + "----" + data + "\r\n")
								.getBytes());
					}
					bufferedReader.close();
					fileReader.close();
					// -----------------------------------------

					if (atomicInteger.getAndIncrement() % 5 == 0) {
						socketOut.write((UUID.randomUUID().toString() + "\r\n")
								.getBytes());
					} else {
						socketOut.write((ThreadLocalRandom.current()
								.nextDouble() + "\r\n").getBytes());
					}

					// 接收服务器的反馈
					BufferedReader br = new BufferedReader(
							new InputStreamReader(socket.getInputStream()));
					String msg = null;
					while ((msg = br.readLine()) != null) {
						System.out.println(msg);
					}
				} catch (IOException e) {
					e.printStackTrace();
				}
			}

		};
	}

}