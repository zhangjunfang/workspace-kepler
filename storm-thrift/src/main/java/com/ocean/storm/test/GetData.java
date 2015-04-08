package com.ocean.storm.test;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Random;

public class GetData {

	/**
	 * 生成数据
	 * 
	 * @param args
	 */
	public static void main(String[] args) {
		// System.out.println(GetData.class.getClass().getResource("/").getPath()+"track.log");
		File logFile = new File(GetData.class.getClass().getResource("/")
				.getPath()
				+ "track.log");
		Random random = new Random();

		String[] hosts = { "www.taobao.com" };
		String[] session_id = { "ABYH6Y4V4SCVXTG6DPB4VH9U123",
				"XXYH6YCGFJYERTT834R52FDXV9U34",
				"BBYH61456FGHHJ7JL89RG5VV9UYU7", "CYYH6Y2345GHI899OFG4V9U567",
				"VVVYH6Y4V4SFXZ56JIPDPB4V678" };
		String[] time = { "2014-01-07 08:40:50", "2014-01-07 08:40:51",
				"2014-01-07 08:40:52", "2014-01-07 08:40:53",
				"2014-01-07 09:40:49", "2014-01-07 10:40:49",
				"2014-01-07 11:40:49", "2014-01-07 12:40:49" };

		StringBuffer sbBuffer = new StringBuffer();
		for (int i = 0; i < 50; i++) {
			sbBuffer.append(hosts[0] + "\t" + session_id[random.nextInt(5)]
					+ "\t" + time[random.nextInt(8)] + "\n");
		}
		if (!logFile.exists()) {
			try {
				logFile.createNewFile();
			} catch (IOException e) {
				System.out.println("Create logFile fail !");
			}
		}
		byte[] b = (sbBuffer.toString()).getBytes();

		FileOutputStream fs;
		try {
			fs = new FileOutputStream(logFile, true);
			fs.write(b);
			fs.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
