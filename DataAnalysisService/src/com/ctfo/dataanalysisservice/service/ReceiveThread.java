package com.ctfo.dataanalysisservice.service;

import java.io.BufferedReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.text.SimpleDateFormat;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.ctfo.dataanalysisservice.beans.Message;
import com.ctfo.dataanalysisservice.beans.UserInfo;
import com.ctfo.dataanalysisservice.io.DataPool;
import com.ctfo.dataanalysisservice.io.SocketUtil;

/**
 * 数据收发服务
 */
public class ReceiveThread extends Thread {

	public Socket socket;
	private static final Logger logger = Logger.getLogger(ReceiveThread.class);
	/**
	 * IP,端口,断线重连时间
	 */
	UserInfo luserInfo;

	String msginfo = "";
	PrintWriter out;
	BufferedReader in;
	long lastNoopTime;
	long lastReceiveNoopTime;

	long receivecount;

	SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

	public ReceiveThread(UserInfo userInfo) throws Exception {

		luserInfo = userInfo;
		connect();
		// 记录线程数
		DataAnalysisServiceMain.threadCount++;
	}

	/**
	 * 建立Socket连接
	 */
	private void connect() {
		socket = SocketUtil.createSocket(luserInfo.getMsgServiceAddr(),
				luserInfo.getMsgServicePort(), luserInfo.getReConnectTime());
		in = SocketUtil.createReader(socket);
		out = SocketUtil.createWriter(socket);
	}

	/**
	 * 心跳
	 */
	private void sendNoopData() {
		// 发送心跳30s
		if (System.currentTimeMillis() - lastNoopTime >= 30000) {
			logger.info(msginfo + " 30s接收：" + receivecount);
			receivecount = 0;

			wirteData("NOOP \r\n");

			lastNoopTime = System.currentTimeMillis();

		}
	}

	/**
	 * 登陆
	 */
	private void sendLoginData() {

		String logindata = "LOGI " + luserInfo.getLogintype() + " "
				+ luserInfo.getUserid() + " " + luserInfo.getPassword()
				+ " \r\n";
		wirteData(logindata);
	}

	/**
	 * 发送数据
	 */
	private void sendData() {
		String message = DataPool.getSendPacketValue();
		if (message != null)
			wirteData(message);
	}

	/**
	 * 发送数据
	 */
	private void wirteData(String data) {
		out.write(data);
		out.flush();
		logger.info(msginfo + "发送数据 :" + data.replace("\r\n", ""));
	}

	/**
	 * 线程执行
	 */
	public void run() {

		logger.info("ReceiveThread");

		msginfo = luserInfo.getMsgServiceAddr() + ":"
				+ luserInfo.getMsgServicePort();
		String s;
		// 登陆
		sendLoginData();

		lastReceiveNoopTime = System.currentTimeMillis();
		sendNoopData();
		receivecount = 0;
		// 发送消息的msg服务唯一ID
		String msgid = "0";

		while (true) {
			try {
				if (in.ready()) {
					s = in.readLine();
					receivecount++;

				// logger.debug(msginfo + "收到:" + s);
					if ("LACK".equals(s.substring(0, 4))) {
						String[] command = s.split("\\s+");
						if (command.length == 5) {
							msgid = command[4];
						}
					}
					Message message = new Message();
					message.setCommand(s);
					message.setMsgid(msgid);
					// 保存原始指令
					DataPool.setReceivePacketValue(message);
					lastReceiveNoopTime = System.currentTimeMillis();
					// logger.info(System.currentTimeMillis()+"原始指令数"+DataPool.getReceivePacketSize());
					// System.out.println("threadCount="+DataAnalysisServiceMain.threadCount);
				} else {
					sleep(1);
				}

				sendNoopData();
				sendData();

				if (lastNoopTime - lastReceiveNoopTime >= luserInfo
						.getConnectStateTime()) {
					logger.info(msginfo + "长时间没收到数据重新连接");
					lastReceiveNoopTime = lastNoopTime = System
							.currentTimeMillis();

					SocketUtil.closeReader(in);
					SocketUtil.closeWriter(out);
					SocketUtil.closeSocket(socket);
					connect();
					sendLoginData();
				}

			} catch (Exception e) {
				logger.error(msginfo + "接收数据错误:" + e.getMessage());

				try {
					// 关闭socket
					sleep(2000);
					SocketUtil.closeReader(in);
					SocketUtil.closeWriter(out);
					SocketUtil.closeSocket(socket);
					connect();
				} catch (Exception ee) {
					logger.error("socket关闭异常" + ee);
				}

			}

		}
	}
}
