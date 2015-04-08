package com.ocean.socketAndstorm.test;

import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.RandomAccessFile;
import java.net.Socket;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

/*
 * 监测数据，通过socket远程发送到另外服务器 ，见MyServerMulti
 * ClientRead再通过服务器从socket里读
 * 
 */

public class LogViewToSocket {
	private long lastTimeFileSize = 0; // 上次文件大小
	private RandomAccessFile randomFile = null;
	private String newfile = null;
	private String thisfile = null;

	/**
	 * 实时输出日志信息
	 * 
	 * @param logFile
	 *            日志文件
	 * @throws IOException
	 */
	public String getNewFile(File file) {
		File[] fs = file.listFiles();
		long maxtime = 0;
		String newfilename = "";
		for (int i = 0; i < fs.length; i++) {
			if (fs[i].lastModified() > maxtime) {
				maxtime = fs[i].lastModified();
				newfilename = fs[i].getAbsolutePath();
			}
		}
		return newfilename;
	}

	public void realtimeShowLog(final File logFile, final PrintWriter out)
			throws IOException {
		newfile = getNewFile(logFile);
		// 指定文件可读可写
		randomFile = new RandomAccessFile(new File(newfile), "r");
		// 启动一个线程每1秒钟读取新增的日志信息
		ScheduledExecutorService exec = Executors.newScheduledThreadPool(1);
		exec.scheduleWithFixedDelay(new Runnable() {
			@Override
			public void run() {
				try {
					// 获得变化部分的
					randomFile.seek(lastTimeFileSize);
					String tmp = "";
					while ((tmp = randomFile.readLine()) != null) {
						System.out.println(new String(tmp.getBytes("ISO8859-1")));
						out.println(new String(tmp.getBytes("ISO8859-1")));
						out.flush();
					}
					thisfile = getNewFile(logFile);
					if (!thisfile.equals(newfile)) {
						randomFile = new RandomAccessFile(new File(newfile),
								"r");
						lastTimeFileSize = 0;
					} else

						lastTimeFileSize = randomFile.length();

				} catch (IOException e) {
					throw new RuntimeException(e);
				}
			}
		}, 0, 1, TimeUnit.SECONDS);
	}

	public static void main(String[] args) throws Exception {
		LogViewToSocket view = new LogViewToSocket();

		Socket socket = new Socket("127.0.0.1", 5678);

		PrintWriter out = new PrintWriter(socket.getOutputStream());

		final File tmpLogFile = new File("/home/hadoop/test");
		view.realtimeShowLog(tmpLogFile, out);
		socket.close();

	}

}
